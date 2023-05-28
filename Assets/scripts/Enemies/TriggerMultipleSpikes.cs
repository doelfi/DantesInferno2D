using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class TriggerMultipleSpikes : MonoBehaviour
{
    // Triggers the spikes hanging from a platform to fall down if the Player
    // is colliding with the collider.

    [FormerlySerializedAs("spikes")] [SerializeField]
    private GameObject[] _spikes;

    [FormerlySerializedAs("timeBetweenSpikes")] [SerializeField]
    private float _timeBetweenSpikes;

    IEnumerator DropSpikes()
    {
        // Goes trough each spike in the _spikes group and let them fall down
        // by adding a gravity scale. Spikes only fall if the timeBetweenSpikes 
        // counter is satisfied.

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
        // If the Player collides with the Collider, the Coroutine DropSpikes is called.
        if (other.CompareTag("Player"))
        {
            StartCoroutine(DropSpikes());
        }
    }
}
