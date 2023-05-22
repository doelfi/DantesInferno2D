using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    // Animation
    private Animator animator;
    private SpriteRenderer _mSpriteRenderer;

    // movement variables
    private bool isJumping = true;
    private bool bIsGoingRight = true;
    private float moveHorizontal;
    private float moveVertical;
    
    private Rigidbody2D rb2D;
    [SerializeField]
    private float _speed = 8f;
    [SerializeField]
    private float _jumpingSpeed = 25f;

    // stats
    [SerializeField]
    private GameObject UIcanvas;
    private UIcontroller UIscript;
    private int levelCoins = 0;

    // sounds
    [SerializeField] private GameObject soundManager;
    private SoundManager soundManagerScript;
    
    // misc
    private int sceneID;
   

    // Start is called before the first frame update
    void Start()
    {
        rb2D = gameObject.GetComponent<Rigidbody2D>();
        UIscript = UIcanvas.GetComponent<UIcontroller>();
        UIscript.updateUI();
        sceneID = SceneManager.GetActiveScene().buildIndex;

        // Animation
        animator = gameObject.GetComponent<Animator>();
        _mSpriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        
        // Sounds
        soundManagerScript = soundManager.GetComponent<SoundManager>();
    }

    // Update is called once per frame
    void Update()
    {
        // MOVEMENT
        float horizontalInput = Input.GetAxis("Horizontal");
        transform.Translate(Vector3.right * Time.deltaTime * _speed * horizontalInput);

        // Animation
        if (horizontalInput != 0)
        {
            animator.SetBool("IsWalking", true);
        }
        else
        {
            animator.SetBool("IsWalking", false);
        }
        if (Input.GetKeyDown("a"))
        {
            bIsGoingRight = true;
            _mSpriteRenderer.flipX = bIsGoingRight;
        }
        if (Input.GetKeyDown("d"))
        {
            bIsGoingRight = false;
            _mSpriteRenderer.flipX = bIsGoingRight;
        }

        // JUMPING
        //float verticalInput = Input.GetAxis("Vertical"); // this gives an absurd boost, I don't know why
        if ((Input.GetKeyDown("space") || Input.GetKeyDown("up")) && !isJumping)
        {
            rb2D.velocity += new Vector2(0f, _jumpingSpeed);
            // Animation
            animator.SetBool("IsJumpingAni", true);
            // Sound
            soundManagerScript.PlaySound(SoundManager.SoundOptions.PlayerJump);
        }
        
        if (UIscript.gameOver_panel.text == "Game Over" && Input.anyKeyDown) 
        // Bug: key gedrückt halten wird nicht erkannt...
        {
            SceneManager.LoadScene(0);
        }      
    }

    
    public void gameOver()
    {
        UnityEngine.Debug.Log("Enjoy your time in hell."); // Placeholder
        //bool visible = this.gameObject.GetComponent<Renderer>(); 
        //visible = false;// Destroy(this.gameObject); //Bug: Problem with Camera
        _mSpriteRenderer.enabled = false;
        UIscript.gameOverUI();

    }
    
    public void TakeDamage()
    {
        GameStats.lifes -= 1;
        // reset coins collected in that level
        GameStats.coins = GameStats.coins - levelCoins;
        levelCoins = 0;
        UIscript.updateUI();
        
        // Sound
        soundManagerScript.PlaySound(SoundManager.SoundOptions.DamageTaken);

        if (GameStats.lifes < 1)
        {
            gameOver();
        }
        else
        {
            UnityEngine.Debug.Log("took damage");
            // reloads level, so coins and enemies will reappear
            SceneManager.LoadScene(sceneID);
        }
    }

    public void CollectCoin()
    {
        GameStats.coins += 1;
        levelCoins += 1;
        soundManagerScript.PlaySound(SoundManager.SoundOptions.CoinCollected);
        
        if (GameStats.coins >= 3)
        {
            GameStats.lifes += 1;
            GameStats.coins = 0;
            levelCoins = 0;
            soundManagerScript.PlaySound(SoundManager.SoundOptions.LifeGained);
        }
        UIscript.updateUI();
    }


    // has to be called by the finish line trigger event
    public void OnLevelSwitch()
    {
        levelCoins = 0;
        soundManagerScript.PlaySound(SoundManager.SoundOptions.FinishReached);
        // during the first run the times are intialized as 0 and calling Min() would just leave them at 0
        if (GameStats.runTimes[sceneID - 1] != 0)
        {
            GameStats.runTimes[sceneID - 1] = Math.Min(GameStats.runTimes[sceneID - 1], Time.timeSinceLevelLoad);
        }
        else
        {
            GameStats.runTimes[sceneID - 1] = Time.timeSinceLevelLoad;
        }

        try
        {
            GameStats.SaveRunTimes();
        }
        catch
        {
            UnityEngine.Debug.Log("Path Empty, run times not saved!");
        }
        
        SceneManager.LoadScene(sceneID + 1);
    }
   

    // Checks for Collision with objects that are also rigidbodies (not triggers)
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Platform") || other.gameObject.CompareTag("Enemy"))
        {
            // prevents a jump if the player only touches the platform from the side
            if (transform.position.y >= (other.transform.position.y + 0.9))
                isJumping = false; 
            // Animation
            animator.SetBool("IsJumpingAni", false);
        }

        if (other.gameObject.CompareTag("Enemy") || other.gameObject.CompareTag("Spike"))
        {
            // if player is above enemy (with some margin of error) the enemy dies otherwise the player gets dealt damage
            if (other.gameObject.CompareTag("Enemy") && transform.position.y > (other.transform.position.y + 0.8))
            {
                Destroy(other.gameObject);
                rb2D.velocity += new Vector2(0f, _jumpingSpeed * 0.5f); // give a little boost upwards when enemy is destroyed
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
    // this happens because the new platform puts isJumping = true while the platform the player is standing only reacts on first collision
    // has the drawback that the jumping force seems increased by a lot probably because collision exist isn't fast checked fast enough and the player gets an extra boost
    void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Platform") || other.gameObject.CompareTag("Enemy"))
        {
            if (transform.position.y >= (other.transform.position.y + 0.9))
                isJumping = false;
        }
    }
    

    // prevents that the player can jump mid-air if he only runs of a platform without jumping
    void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Platform") || other.gameObject.CompareTag("Enemy"))
        {
            isJumping = true;
        }
    }
}
