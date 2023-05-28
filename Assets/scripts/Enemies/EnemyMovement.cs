using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.Serialization;

public class EnemyMovement : MonoBehaviour
{
    // Is responsible for Enemy movement. 
    // Enemies walk in one direction until they reach a wall 
    // (i.e. platform, spike or other enemy) or end of platform. 
    // Then the sprite flips and walks in the other direction.
    //
    // Inspired by: https://www.noveltech.dev/simple-patrolling-monster-unity/
    public float mMovementSpeed = 3.0f;
    public bool isGoingRight = true;

    // Distance on which an Enemy is turning around when walking towards a wall/ end of platform.
    public float mRaycastingDistance = 0.5f;

    private SpriteRenderer _mSpriteRenderer;

    void Start()
    {
        // Gets the SpriteRenderer and assigns the first movement to be into the right direction.
        _mSpriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        _mSpriteRenderer.flipX = isGoingRight;
    }

    void Update()
    {
        // Moves the Enemy to the left or right depending on variable isGoingRight.
        Vector3 directionTranslation = (isGoingRight) ? transform.right : -transform.right;
        directionTranslation *= Time.deltaTime * mMovementSpeed;
        transform.Translate(directionTranslation);

        // Every Update check for walls or end of paltforms the Enemy is walking towards.
        CheckForWalls();
        CheckForPlatform();
    }

    private void CheckForWalls()
    {
        // Checks for platforms, spikes or other enemies the enemy is walking towards 
        // by checking for collision of a Raycast with a another object.

        // Direction of the raycast depends on the direction the Enemy is walking.
        Vector3 raycastDirection = (isGoingRight) ? Vector3.right : Vector3.left;
        // place the raycast with an offset in the middle and infront of the Enemy to prevent collsion
        // with the sprite themselves.
        Vector3 offset = (isGoingRight) ? new Vector3(0.35f, 0.25f, 0f) : new Vector3(-0.35f, 0.25f, 0f);
        RaycastHit2D hit = Physics2D.Raycast(transform.position + raycastDirection - offset, raycastDirection, 0.1f); //* mRaycastingDistance 

        if (hit.collider != null)
        {
            if ((hit.transform.tag == "Platform") || (hit.transform.tag == "Spike") || (hit.transform.tag == "Enemy"))
            {
                // If a collision appears, turn the sprite around.
                isGoingRight = !isGoingRight;
                _mSpriteRenderer.flipX = isGoingRight;

            }
        }
    }

    private void CheckForPlatform()
    {
        // Checks for an end of the platform the Enemy is walking towards.

        // Raycast is aiming down and is placed with an offset infront of the walking direction of the Enemy.
        Vector3 raycastDirection = Vector3.down;
        Vector3 offset = (isGoingRight) ? new Vector3(0.5f,0f,0f) : new Vector3(-0.5f,0f,0f);
        RaycastHit2D hit = Physics2D.Raycast(transform.position + raycastDirection + offset, raycastDirection, 0.5f); //* mRaycastingDistance 

        if (hit.collider == null)
        {
            // If the Raycast is not colliding with a platform (i.e. reached the end of the platform)
            // the Enemy turns around.
            isGoingRight = !isGoingRight;
            _mSpriteRenderer.flipX = isGoingRight;
        }
    }
}
