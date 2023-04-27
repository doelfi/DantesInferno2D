using System.Collections;
using System.Collections.Generic;
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
    private int lifes = 3;
    private int coins = 0;
    private int levelCoins = 0;
   

    // Start is called before the first frame update
    void Start()
    {
        rb2D = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        moveHorizontal = Input.GetAxisRaw("Horizontal");
        moveVertical = Input.GetAxisRaw("Vertical");
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
