using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerMultipleSpikes : MonoBehaviour
{
    [SerializeField]
    private GameObject[] spikes;

    [SerializeField]
    private float timeBetweenSpikes;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator DropSpikes()
    {
        foreach (var spike in spikes)
        {
                       
            spike.GetComponent<Rigidbody2D>().gravityScale = 1;

            float counter = 0;
            while (counter < timeBetweenSpikes)
            {
                counter += Time.deltaTime;
                yield return null;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(DropSpikes());
        }
    }
}
