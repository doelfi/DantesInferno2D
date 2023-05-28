using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Victory : MonoBehaviour
{
    // Loads the victory script if the Player collides with the trigger.
    [SerializeField]
    public GameObject canvas;
    private UIcontroller _UIScript;

    void Start()
    {
        _UIScript = canvas.GetComponent<UIcontroller>();
    }

    void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.CompareTag("Player"))
        {
            _UIScript.Victory();
        }
    }
}
