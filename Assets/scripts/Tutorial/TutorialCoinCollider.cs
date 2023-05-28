using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialCoinCollider : MonoBehaviour
{
    // Displays a text on the screen in the tutorial level.

    [SerializeField]
    public GameObject canvas;
    private TutorialUI _messageScript;

    void Start()
    {
        _messageScript = canvas.GetComponent<TutorialUI>();
    }

    void OnTriggerEnter2D(Collider2D other) 
    {
        // If the player reaches the collider, the text is displayed. 
        
        if (other.CompareTag("Player"))
        {
            string coinCollider_text = "Finish at the plastic flag.";
            _messageScript.Tutorial(ref coinCollider_text);
        }
    }
}
