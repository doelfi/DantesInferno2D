using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapPlatform : MonoBehaviour
{
    // A platform without a Collider2D so that the Player cannot stand on it.
    // An animation is played to make the platform disappear.
    private bool _triggered = false;
    private SpriteRenderer _mSpriteRenderer;
    
    void Start()
    {
        // Get the SpriteRenderer of the platform for the animation.
        _mSpriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // If the Collider is not triggered the platform disappears.
        if (!_triggered)
        {
           StartCoroutine(DisappearAnimation()); 
        }
        _triggered = true;
    }

    
    IEnumerator DisappearAnimation()
    {
        // Reduces the alpha value over a short time so it looks like the platform disappears.
        for (int i = 0; i < 10; i++)
        {
            float alpha = 1 - (i / 10f);
            _mSpriteRenderer.material.SetColor("_Color", new Color(1f,1f,1f,alpha));
            foreach (Transform child in transform)
            {
                child.gameObject.GetComponent<SpriteRenderer>().material.SetColor("_Color", new Color(1f,1f,1f,alpha));
            }
            yield return new WaitForSeconds(0.1f);
        }
    }
}
