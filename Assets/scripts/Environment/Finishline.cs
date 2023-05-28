using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Finishline : MonoBehaviour
{
    // Switches the scene to the next level when the Player has 
    // reached the finish line of a level.
    [SerializeField]
    private GameObject _player;
    private PlayerController _playerScript;
    
    void Start()
    {
        // Gets the script of the Player to switch the level.
        _playerScript = _player.GetComponent<PlayerController>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // If the Player collides with the finish line, the level is switched.
        if (other.CompareTag("Player"))
        {
            _playerScript.OnLevelSwitch();
        }
    }
}
