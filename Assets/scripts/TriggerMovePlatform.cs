using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerMovePlatform : MonoBehaviour
{

    [SerializeField]
    private GameObject platform;
    private MovePlatformOnTrigger script;

    void Start()
    {
        script = platform.GetComponent<MovePlatformOnTrigger>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            script.StartMoving();
        }
    }

}
