using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class TriggerMovePlatform : MonoBehaviour
{
    // If the Player collides with the Trigger, the platform starts moving
    // by calling the MovePlatformonTrigger script.
    
    [SerializeField]
    private GameObject _platform;
    private MovePlatformOnTrigger _script;

    void Start()
    {
        _script = _platform.GetComponent<MovePlatformOnTrigger>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            _script.StartMoving();
        }
    }
}
