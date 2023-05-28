using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialSpikeCollider : MonoBehaviour
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
        if (other.CompareTag("Player"))
        {
            string spikeCollider_text = "Collect the coin.";
            _messageScript.Tutorial(ref spikeCollider_text);
        }
    }
}
