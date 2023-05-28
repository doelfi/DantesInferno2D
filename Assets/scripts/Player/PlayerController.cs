using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    // Combines all actions on how the player interacts with the environment.

    // Animation
    private Animator _animator;
    private SpriteRenderer _mSpriteRenderer;

    // Movement variables
    private bool _isJumping = true;
    private bool _isGoingRight = true;
    private float _moveHorizontal;
    private float _moveVertical;
    
    private Rigidbody2D _rb2D;
    [SerializeField]
    private float _speed = 8f;
    [SerializeField]
    private float _jumpingSpeed = 25f;

    // Stats
    [SerializeField]
    private GameObject _UIcanvas;
    private UIcontroller _UIscript;
    private int _levelCoins = 0;

    // Sounds
    [SerializeField]
    private GameObject _soundManager;
    private SoundManager _soundManagerScript;
    private bool _playingWalking = false;
    
    // Misc
    private int _sceneID;
   
    void Start()
    {
        // Get components from RigidBody2D, UIcontroller, Animator, SpriteRenderer and SoundManager

        _rb2D = gameObject.GetComponent<Rigidbody2D>();
        _UIscript = _UIcanvas.GetComponent<UIcontroller>();
        _UIscript.updateUI();
        _sceneID = SceneManager.GetActiveScene().buildIndex;

        // Animation
        _animator = gameObject.GetComponent<Animator>();
        _mSpriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        
        // Sounds
        _soundManagerScript = _soundManager.GetComponent<SoundManager>();
    }

    void Update()
    {
        // Movement
        float horizontalInput = Input.GetAxis("Horizontal");
        transform.Translate(Vector3.right * Time.deltaTime * _speed * horizontalInput);
        
        // Sound only plays on platforms and only in a certain time 
        // interval (see coroutine for the time variable).
        if (!_playingWalking && !_isJumping && horizontalInput != 0)
        {
            _playingWalking = true;
            StartCoroutine(WalkingSound());
        }

        // Animation: Sets the animation of the player depending on if walking and direction.
        if (horizontalInput != 0)
        {
            _animator.SetBool("IsWalking", true);
        }
        else
        {
            _animator.SetBool("IsWalking", false);
        }
        if (Input.GetKeyDown("a"))
        {
            _isGoingRight = true;
            _mSpriteRenderer.flipX = _isGoingRight;
        }
        if (Input.GetKeyDown("d"))
        {
            _isGoingRight = false;
            _mSpriteRenderer.flipX = _isGoingRight;
        }

        // Jumping
        if ((Input.GetKeyDown("space") || Input.GetKeyDown("up")) && !_isJumping)
        {
            _rb2D.velocity += new Vector2(0f, _jumpingSpeed);
            // Animation: If the Player jumps, the animation is played.
            _animator.SetBool("IsJumpingAni", true);
            // Sound
            _soundManagerScript.PlaySound(SoundManager.SoundOptions.PlayerJump);
        }
        
        // If the Game is lost or won change the Scene to the end screen.
        if (_UIscript.gameOver_panel.text == "Game Over" && Input.anyKeyDown)
        {
            SceneManager.LoadScene(0);
        }
        if (_UIscript.victory_panel.text == "You win" && Input.anyKeyDown)
        {
            SceneManager.LoadScene(0);
        }       
    }


    IEnumerator WalkingSound()
    {
        // Plays walking sound and waits for a certain time so that walking does not sound like a machine gun.
        _soundManagerScript.PlaySound(SoundManager.SoundOptions.PlayerMove);
        yield return new WaitForSeconds(0.5f);
        _playingWalking = false;
    }


    public void gameOver()
    {
        // If the game is over, disable the Player and display the "Game Over" text.
        _mSpriteRenderer.enabled = false;
        _UIscript.gameOverUI();
    }
    
    public void TakeDamage()
    {
        StartCoroutine(TakeDamageAfterClip());
    }
    
     
    IEnumerator TakeDamageAfterClip()
    {
        // Waits for the sound clip to end and calls the usual damage routines afterwards.

        float cliplength = _soundManagerScript.PlaySoundFloat(SoundManager.SoundOptions.DamageTaken);
        StartCoroutine(PlayerDeathAnimation());
        yield return new WaitForSeconds(cliplength);

        GameStats.lifes -= 1;

        // Reset coins collected in that level.
        GameStats.coins = GameStats.coins - _levelCoins;
        _levelCoins = 0;
        _UIscript.updateUI();

        // If the Player loses his last life end the game.
        if (GameStats.lifes < 1)
        {
            gameOver();
        }
        else
        {
            // Reloads level, so coins and enemies will reappear.
            SceneManager.LoadScene(_sceneID);
        }
    }
    

    IEnumerator PlayerDeathAnimation()
    {
        // Adds a flashing animation so that the user knows something 
        // happens while the sound has time to play.

        // Fixes the position of the player as long as the sound is played.
        StartCoroutine(PlayerFixPosition());
        while (true)
        {
            _mSpriteRenderer.material.SetColor("_Color", new Color(1f,1f,1f,0.2f));
            yield return new WaitForSeconds(0.1f);
            _mSpriteRenderer.material.SetColor("_Color", new Color(1f,1f,1f,0.8f));
            yield return new WaitForSeconds(0.1f);
        }
    }

    IEnumerator PlayerFixPosition()
    {
        // Fix player in position while animation/ sound is played

        Vector3 position = transform.position;
        while (true)
        {
            transform.position = position;
            yield return new WaitForSeconds(0.01f);
        }
    }
    
 
    public void CollectCoin()
    {
        // Increases the coin counter in GameStats and for the current level.
        // When 3 coins are reached, one life is added and the coins reset.
        GameStats.coins += 1;
        _levelCoins += 1;
        _soundManagerScript.PlaySound(SoundManager.SoundOptions.CoinCollected);
        
        if (GameStats.coins >= 3)
        {
            GameStats.lifes += 1;
            GameStats.coins = 0;
            _levelCoins = 0;
            _soundManagerScript.PlaySound(SoundManager.SoundOptions.LifeGained);
        }
        _UIscript.updateUI();
    }


    
    public void OnLevelSwitch()
    {
        // Has to be called by the finish line trigger event.
        _levelCoins = 0;
        float _cliplength = _soundManagerScript.PlaySoundFloat(SoundManager.SoundOptions.FinishReached);
        StartCoroutine(LevelSwitch(_cliplength));
    }
    
    
    IEnumerator LevelSwitch(float _cliplength)
    {
        // Calculates new run times and saves the txt file,
        // switches to the next scene afterwards.

        StartCoroutine(PlayerDeathAnimation());
        yield return new WaitForSeconds(_cliplength);

        // during the first run the times are intialized as 0 and calling Min() would just leave them at 0
        if (GameStats.runTimes[_sceneID - 1] != 0)
        {
            GameStats.runTimes[_sceneID - 1] = Math.Min(GameStats.runTimes[_sceneID - 1], Time.timeSinceLevelLoad);
        }
        else
        {
            GameStats.runTimes[_sceneID - 1] = Time.timeSinceLevelLoad;
        }

        try
        {
            GameStats.SaveRunTimes();
        }
        catch
        {
            UnityEngine.Debug.Log("Path Empty, run times not saved!");
        }
        
        SceneManager.LoadScene(_sceneID + 1);
    }

    
    void OnCollisionEnter2D(Collision2D other)
    {
        // Checks for Collision with objects that are also rigidbodies (not triggers).
        if (other.gameObject.CompareTag("Platform") || other.gameObject.CompareTag("Enemy"))
        {
            // Prevents a jump if the player only touches the platform from the side
            if (transform.position.y >= (other.transform.position.y + 0.9))
            {
                _isJumping = false;
            }
                 
            // Animation
            _animator.SetBool("IsJumpingAni", false);
            // Sound
            _soundManagerScript.PlaySound(SoundManager.SoundOptions.PlayerLand);
        }

        if (other.gameObject.CompareTag("Enemy") || other.gameObject.CompareTag("Spike"))
        {
            // If player is above enemy (with some margin of error) the enemy 
            // dies otherwise the player gets dealt damage.
            if (other.gameObject.CompareTag("Enemy") && transform.position.y > (other.transform.position.y + 0.8))
            {
                Destroy(other.gameObject);
                // Gives a little boost upwards when enemy is destroyed.
                _rb2D.velocity += new Vector2(0f, _jumpingSpeed * 0.5f); 
                // Sound
                _soundManagerScript.PlaySound(SoundManager.SoundOptions.EnemyKilled);
            }
            else
            {
                TakeDamage();
            }   
        }

        // If the Player falls from a platform and reaches the bottom line of the level
        // he takes damage and the level is restarted.
        if (other.gameObject.CompareTag("BottomLine"))
        {
            TakeDamage();
        }
    }

    void OnCollisionStay2D(Collision2D other)
    {
        // Prevents a bug where a Player running into another platform 
        // while staying on one cannot jump anymore afterwards.
        if (other.gameObject.CompareTag("Platform") || other.gameObject.CompareTag("Enemy"))
        {
            if (transform.position.y >= (other.transform.position.y + 0.9))
                _isJumping = false;
        }
    }

    void OnCollisionExit2D(Collision2D other)
    {
        // Prevents that the PLayer can jump mid-air if he only runs off a platform without jumping.
        if (other.gameObject.CompareTag("Platform") || other.gameObject.CompareTag("Enemy"))
        {
            _isJumping = true;
        }
    }
}
