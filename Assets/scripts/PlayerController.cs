using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    // movement settings
    private Rigidbody2D rb2D;
    private float jumpForce = 30f;
    private float moveSpeed = 1f;
    private bool isJumping = true;
    private float moveHorizontal;
    private float moveVertical;

    // stats
    [SerializeField]
    private GameObject UIcanvas;
    private UIcontroller UIscript;
    private int lifes = 3;
    private int coins = 0;
    private int levelCoins = 0;
    private int levelLifes = 0; // in case coins get turned into a life
   

    // Start is called before the first frame update
    void Start()
    {
        rb2D = gameObject.GetComponent<Rigidbody2D>();
        UIscript = UIcanvas.GetComponent<UIcontroller>();
    }

    // Update is called once per frame
    void Update()
    {
        moveHorizontal = Input.GetAxisRaw("Horizontal");
        moveVertical = Input.GetAxisRaw("Vertical");
    }

    
    private void takeDamage()
    {
        lifes -= 1;
        coins = coins - levelCoins;
        levelCoins = 0;
        lifes = lifes - levelLifes;
        UIscript.updateUI(lifes, coins);
        if (lifes < 1)
        {
            UnityEngine.Debug.Log("Enjoy your time in hell."); // Placeholder
        }
        UnityEngine.Debug.Log("took damage, reset level"); // Placeholder
    }

    private void collectCoin()
    {
        coins += 1;
        levelCoins += 1;
        if (coins >= 9)
        {
            lifes += 1;
            levelLifes += 1;
            coins = 0;
        }
        UIscript.updateUI(lifes, coins);
    }

    public int getLifes() { return lifes; }
    public int getCoins() { return coins; }

    // has to be called by the finish line trigger event
    public void onLevelSwitch()
    {
        levelLifes = 0;
        levelCoins = 0;
    }
    


    // Checks for Collision with objects that are also rigidbodies (not triggers)
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Platform"))
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
