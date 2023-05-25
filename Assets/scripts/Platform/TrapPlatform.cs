using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapPlatform : MonoBehaviour
{
    private bool _triggered = false;
    private SpriteRenderer _mSpriteRenderer;
    
    void Start()
    {
        _mSpriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!_triggered)
        {
           StartCoroutine(DisappearAnimation()); 
        }
        _triggered = true;
    }

    // reduces the alpha value over a short time so it looks like the platform disappears
    IEnumerator DisappearAnimation()
    {
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
