using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialCoinCollider : MonoBehaviour
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
            string coinCollider_text = "Finish at the plastic flag.";//"Good job. Finish the level by touching the plastic flag.";
            messageScript.Tutorial(ref coinCollider_text);
        }
    }
}
