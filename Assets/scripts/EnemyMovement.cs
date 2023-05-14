using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    // Inspired by: https://www.noveltech.dev/simple-patrolling-monster-unity/
    public float mMovementSpeed = 3.0f;
    public bool bIsGoingRight = true;

    public float mRaycastingDistance = 0.5f;

    private SpriteRenderer _mSpriteRenderer;

    void Start()
    {
        _mSpriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        _mSpriteRenderer.flipX = bIsGoingRight;
    }

    void Update()
    {
        Vector3 directionTranslation = (bIsGoingRight) ? transform.right : -transform.right;
        directionTranslation *= Time.deltaTime * mMovementSpeed;

        transform.Translate(directionTranslation);


        CheckForWalls();
        CheckForPlatform();
    }

    private void CheckForWalls()
    {
        Vector3 raycastDirection = (bIsGoingRight) ? Vector3.right : Vector3.left;
        Vector3 offset = (bIsGoingRight) ? new Vector3(0.35f, 0.25f, 0f) : new Vector3(-0.35f, 0.25f, 0f);
        RaycastHit2D hit = Physics2D.Raycast(transform.position + raycastDirection - offset, raycastDirection, 0.1f); //* mRaycastingDistance 

        if (hit.collider != null)
        {
            if ((hit.transform.tag == "Platform") || (hit.transform.tag == "Spike") || (hit.transform.tag == "Enemy"))
            {
                bIsGoingRight = !bIsGoingRight;
                _mSpriteRenderer.flipX = bIsGoingRight;

            }
        }
    }

    private void CheckForPlatform()
    {
        Vector3 raycastDirection = Vector3.down;
        Vector3 offset = (bIsGoingRight) ? new Vector3(0.5f,0f,0f) : new Vector3(-0.5f,0f,0f);
        RaycastHit2D hit = Physics2D.Raycast(transform.position + raycastDirection + offset, raycastDirection, 0.5f); //* mRaycastingDistance 

        if (hit.collider == null)
        {
            bIsGoingRight = !bIsGoingRight;
            _mSpriteRenderer.flipX = bIsGoingRight;
        }
    }
}
