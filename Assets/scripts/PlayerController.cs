using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{

    // movement settings
    private Rigidbody2D rb2D;

    private bool isJumping = true;
    private float moveHorizontal;
    private float moveVertical;

    [SerializeField]
    private float jumpForce = 30f;
    [SerializeField]
    private float moveSpeed = 1.5f;

    // stats
    [SerializeField]
    private GameObject UIcanvas;
    private UIcontroller UIscript;
    private int levelCoins = 0;
    private int levelLifes = 0; // in case coins get turned into a life
   

    // Start is called before the first frame update
    void Start()
    {
        rb2D = gameObject.GetComponent<Rigidbody2D>();
        UIscript = UIcanvas.GetComponent<UIcontroller>();
        UIscript.updateUI();
    }

    // Update is called once per frame
    void Update()
    {
        moveHorizontal = Input.GetAxisRaw("Horizontal");
        moveVertical = Input.GetAxisRaw("Vertical");
    }

    
    private void takeDamage()
    {
        GameStats.lifes -= 1;
        GameStats.coins = GameStats.coins - levelCoins;
        levelCoins = 0;
        GameStats.lifes = GameStats.lifes - levelLifes;
        UIscript.updateUI();


        if (GameStats.lifes < 1)
        {
            UnityEngine.Debug.Log("Enjoy your time in hell."); // Placeholder
        }

        UnityEngine.Debug.Log("took damage");

        // doesn't look nice visually but brings the player back to start
        // Problem: coins don't get replaced
        // Vector3 newPosition = new Vector3(0,1,0);
        // transform.position = newPosition;

        // reloads level, so coins will reappear
        // Problem: lifes and coins are set to the standard value again
        SceneManager.LoadScene(1); // number might change over time

    }

    public void collectCoin()
    {
        GameStats.coins += 1;
        levelCoins += 1;
        if (GameStats.coins >= 9)
        {
            GameStats.lifes += 1;
            levelLifes += 1;
            GameStats.coins = 0;
        }
        UIscript.updateUI();
    }

    // public int getLifes() { return lifes; } // probably not needed anymore
    // public int getCoins() { return coins; }

    // has to be called by the finish line trigger event
    public void onLevelSwitch()
    {
        levelLifes = 0;
        levelCoins = 0;
    }
    


    // Checks for Collision with objects that are also rigidbodies (not triggers)
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Platform") || other.gameObject.CompareTag("Enemy"))
        {
            isJumping = false; 
        }

        if (other.gameObject.CompareTag("Enemy"))
        {
            UnityEngine.Debug.Log("Enemy touched.");
            takeDamage();
        }

    }


    void FixedUpdate()
    {
        if(moveHorizontal > 0.1f || moveHorizontal < -0.1f)
        {
            rb2D.AddForce(new Vector2(moveHorizontal * moveSpeed, 0f), ForceMode2D.Impulse);
        }

        if (moveVertical > 0.1f && !isJumping)
        {
            rb2D.AddForce(new Vector2(0f, moveVertical * jumpForce), ForceMode2D.Impulse);
            isJumping = true;
        }
    }
}
