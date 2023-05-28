using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlatform : MonoBehaviour
{
    // Lets the platform move from left to right and transport the Player.

    public Vector3 targetOffset = Vector3.right * 10f;
    public float speed = 1f;
    private Vector3 _startPosition;

    
    IEnumerator Start()
    {
        // Starts a coroutine that begins as soon as the object is enabled.

        _startPosition = transform.position;

        // Picks the destination point offset from the current location.
        Vector3 targetPosition = transform.position + targetOffset;

        while (true)
        {
            // Loop until we are within a certain tolerance of our target.

            while (Vector3.SqrMagnitude(transform.position - targetPosition) > 0.1)
            {
                // Move one step toward the target at the given speed.
                transform.position = Vector3.MoveTowards(
                      transform.position,
                      targetPosition,
                      speed * Time.deltaTime
                 );

                // Wait one frame then resume the loop.
                yield return null;
            }

            // Going back to the starting position.
            while (Vector3.SqrMagnitude(transform.position - _startPosition) > 0.1)
            {
                // Move one step toward the target at the given speed.
                transform.position = Vector3.MoveTowards(
                      transform.position,
                      _startPosition,
                      speed * Time.deltaTime
                 );

                // Wait one frame then resume the loop.
                yield return null;
            }
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        // If the Player collides with the moving platform (i.e stands on it)
        // the movement of the platform is added to the Player so that
        // the Player moves with the platform.
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.transform.SetParent(transform, true);
        }
    }

    void OnCollisionExit2D(Collision2D other)
    {
        // If the Player leaves the moving platform, the additional movement 
        // is removed.
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.transform.SetParent(null);
        }
    }

}
