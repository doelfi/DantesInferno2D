using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialMoveCollider : MonoBehaviour
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
            string moveCollider_text = "Jump over spikes.";
            _messageScript.Tutorial(ref moveCollider_text);
        }
    }
}
