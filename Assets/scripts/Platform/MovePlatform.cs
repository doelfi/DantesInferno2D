using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlatform : MonoBehaviour
{
    // Set your parameters in the Inspector.
    public Vector3 targetOffset = Vector3.right * 10f;
    public float speed = 1f;
    private Vector3 _startPosition;

    // Make Start a coroutine that begins 
    // as soon as our object is enabled.
    IEnumerator Start()
    {

        _startPosition = transform.position;

        // Then, pick our destination point offset from our current location.
        Vector3 targetPosition = transform.position + targetOffset;

        // I'm sure there is a better way to solve this
        // But it works.
        while (true)
        {
            // Loop until we're within a certain tolerance of our target.
            // might have to adjust the tolerance
            while (Vector3.SqrMagnitude(transform.position - targetPosition) > 0.1)
            {

                // Move one step toward the target at our given speed.
                transform.position = Vector3.MoveTowards(
                      transform.position,
                      targetPosition,
                      speed * Time.deltaTime
                 );

                // Wait one frame then resume the loop.
                yield return null;
            }

            // going back to the starting position
            while (Vector3.SqrMagnitude(transform.position - _startPosition) > 0.1)
            {

                // Move one step toward the target at our given speed.
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
        if (other.gameObject.CompareTag("Player")){
            other.gameObject.transform.SetParent(transform, true);
        }
    }

    void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.transform.SetParent(null);
        }
    }

}
