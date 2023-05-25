using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    // Animation
    private Animator _animator;
    private SpriteRenderer _mSpriteRenderer;

    // movement variables
    private bool _isJumping = true;
    private bool _isGoingRight = true;
    private float _moveHorizontal;
    private float _moveVertical;
    
    private Rigidbody2D _rb2D;
    [SerializeField]
    private float _speed = 8f;
    [SerializeField]
    private float _jumpingSpeed = 25f;

    // stats
    [SerializeField]
    private GameObject _UIcanvas;
    private UIcontroller _UIscript;
    private int _levelCoins = 0;

    // sounds
    [SerializeField]
    private GameObject _soundManager;
    private SoundManager _soundManagerScript;
    private bool _playingWalking = false;
    
    // misc
    private int _sceneID;
   

    // Start is called before the first frame update
    void Start()
    {
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

    // Update is called once per frame
    void Update()
    {
        // MOVEMENT
        float horizontalInput = Input.GetAxis("Horizontal");
        transform.Translate(Vector3.right * Time.deltaTime * _speed * horizontalInput);
        
        // sound only plays on platforms and only in a certain time interval (see coroutine for the time variable)
        if (!_playingWalking && !_isJumping && horizontalInput != 0)
        {
            _playingWalking = true;
            StartCoroutine(WalkingSound());
        }

        // Animation
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

        // JUMPING
        //float verticalInput = Input.GetAxis("Vertical"); // this gives an absurd boost, I don't know why
        if ((Input.GetKeyDown("space") || Input.GetKeyDown("up")) && !_isJumping)
        {
            _rb2D.velocity += new Vector2(0f, _jumpingSpeed);
            // Animation
            _animator.SetBool("IsJumpingAni", true);
            // Sound
            _soundManagerScript.PlaySound(SoundManager.SoundOptions.PlayerJump);
        }
        
        if (_UIscript.gameOver_panel.text == "Game Over" && Input.anyKeyDown)
        {
            SceneManager.LoadScene(0);
        }

        if (_UIscript.victory_panel.text == "You win" && Input.anyKeyDown)
        {
            SceneManager.LoadScene(0);
        }       
    }

    // plays walking sound and waits for a certain time so that walking doesn't sound like a machine gun
    IEnumerator WalkingSound()
    {
        _soundManagerScript.PlaySound(SoundManager.SoundOptions.PlayerMove);
        yield return new WaitForSeconds(0.5f);
        _playingWalking = false;
    }

    
    public void gameOver()
    {
        UnityEngine.Debug.Log("Enjoy your time in hell."); // Placeholder
        _mSpriteRenderer.enabled = false;
        _UIscript.gameOverUI();
    }
    
    // essentially we could convert this into the coroutine instead of just calling it here
    // but then we will have to change all instances in the code
    public void TakeDamage()
    {
        StartCoroutine(TakeDamageAfterClip());
    }
    
    // waits for the clip to end and calls the usual damage routines afterwards 
    IEnumerator TakeDamageAfterClip()
    {
        float cliplength = _soundManagerScript.PlaySoundFloat(SoundManager.SoundOptions.DamageTaken);
        StartCoroutine(PlayerDeathAnimation());
        yield return new WaitForSeconds(cliplength);
        GameStats.lifes -= 1;
        // reset coins collected in that level
        GameStats.coins = GameStats.coins - _levelCoins;
        _levelCoins = 0;
        _UIscript.updateUI();
        if (GameStats.lifes < 1)
        {
            gameOver();
        }
        else
        {
            UnityEngine.Debug.Log("took damage");
            // reloads level, so coins and enemies will reappear
            SceneManager.LoadScene(_sceneID);
        }
    }
    
    // adds a flashing animation so that the user knows something happened while the sound has time to play
    IEnumerator PlayerDeathAnimation()
    {
        StartCoroutine(PlayerFixPosition());
        while (true)
        {
            _mSpriteRenderer.material.SetColor("_Color", new Color(1f,1f,1f,0.2f));
            yield return new WaitForSeconds(0.1f);
            _mSpriteRenderer.material.SetColor("_Color", new Color(1f,1f,1f,0.8f));
            yield return new WaitForSeconds(0.1f);
        }
    }
    
    // fix player in position while Death Animation is played
    // I know it is not the nicest way using all these coroutines but it is quick and easy
    IEnumerator PlayerFixPosition()
    {
        Vector3 position = transform.position;
        while (true)
        {
            transform.position = position;
            yield return new WaitForSeconds(0.01f);
        }
    }
    
    // increases the coin counter in GameStats and for the current level
    // when 3 coins are reached, one life is added and the coins reset
    public void CollectCoin()
    {
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


    // has to be called by the finish line trigger event
    public void OnLevelSwitch()
    {
        _levelCoins = 0;
        float _cliplength = _soundManagerScript.PlaySoundFloat(SoundManager.SoundOptions.FinishReached);
        StartCoroutine(LevelSwitch(_cliplength));
    }
    
    // calculates new run times and saves the txt file
    // switches to the next scene afterwards
    IEnumerator LevelSwitch(float _cliplength)
    {
        // probably need a different animation for that but for testing this is okay
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


    // Checks for Collision with objects that are also rigidbodies (not triggers)
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Platform") || other.gameObject.CompareTag("Enemy"))
        {
            // prevents a jump if the player only touches the platform from the side
            if (transform.position.y >= (other.transform.position.y + 0.9))
                _isJumping = false; 
            // Animation
            _animator.SetBool("IsJumpingAni", false);
            // Sound
            _soundManagerScript.PlaySound(SoundManager.SoundOptions.PlayerLand);
        }

        if (other.gameObject.CompareTag("Enemy") || other.gameObject.CompareTag("Spike"))
        {
            // if player is above enemy (with some margin of error) the enemy dies otherwise the player gets dealt damage
            if (other.gameObject.CompareTag("Enemy") && transform.position.y > (other.transform.position.y + 0.8))
            {
                Destroy(other.gameObject);
                _rb2D.velocity += new Vector2(0f, _jumpingSpeed * 0.5f); // give a little boost upwards when enemy is destroyed
                // sound
                _soundManagerScript.PlaySound(SoundManager.SoundOptions.EnemyKilled);
            }

            else
            {
                UnityEngine.Debug.Log("Enemy touched.");
                TakeDamage();
            }
               
        }

        if (other.gameObject.CompareTag("BottomLine"))
        {
            UnityEngine.Debug.Log("Dante fell down.");
            TakeDamage();
        }

    }

    
    // prevents a bug where a player running into another platform while staying on one can't jump anymore afterwards
    void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Platform") || other.gameObject.CompareTag("Enemy"))
        {
            if (transform.position.y >= (other.transform.position.y + 0.9))
                _isJumping = false;
        }
    }
    

    // prevents that the player can jump mid-air if he only runs of a platform without jumping
    void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Platform") || other.gameObject.CompareTag("Enemy"))
        {
            _isJumping = true;
        }
    }
}
