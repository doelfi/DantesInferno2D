using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class TriggerMultipleSpikes : MonoBehaviour
{
    [FormerlySerializedAs("spikes")] [SerializeField]
    private GameObject[] _spikes;

    [FormerlySerializedAs("timeBetweenSpikes")] [SerializeField]
    private float _timeBetweenSpikes;

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
        foreach (var spike in _spikes)
        {
                       
            spike.GetComponent<Rigidbody2D>().gravityScale = 1;

            float counter = 0;
            while (counter < _timeBetweenSpikes)
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
