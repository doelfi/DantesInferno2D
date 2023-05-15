using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveCollider : MonoBehaviour
{
    [SerializeField]
    public GameObject canvas;
    private TutorialUI messageScript;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Start is running");
        messageScript = canvas.GetComponent<TutorialUI>();
    }

    void OnTriggerEnter2D(Collider2D other) 
    {
        Debug.Log("Tutorial is running");
        if (other.CompareTag("Player"))
        {
            Debug.Log("Tutorial is running after if");
            messageScript.Tutorial("Well done. Now be careful with spikes. Jump over it or loose a life.");
        }
    }
}
