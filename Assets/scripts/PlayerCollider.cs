using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class PlayerCollider : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        UnityEngine.Debug.Log("logging works");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Finish"))
        {
            UnityEngine.Debug.Log("finish line reached.");
        }

        if (other.gameObject.CompareTag("Platform"))
        {
            UnityEngine.Debug.Log("Platform touched.");
        }

        UnityEngine.Debug.Log("collision detected");
    }
}
