using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCollider : MonoBehaviour
{
    [SerializeField]
    public GameObject canvas;
    private TutorialUI messageScript;

    // Start is called before the first frame update
    void Start()
    {
        messageScript = canvas.GetComponent<TutorialUI>();
    }

    void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.CompareTag("Player"))
        {
            string moveCollider_text = "Jump over spikes.";//"Well done. Now be careful with spikes. Jump over it or loose a life.";
            messageScript.Tutorial(ref moveCollider_text);
        }
    }
}
