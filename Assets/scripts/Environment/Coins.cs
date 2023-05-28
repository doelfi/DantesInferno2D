using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Coins : MonoBehaviour
{
    // Lets the Player collect coins.
    [SerializeField]
    private GameObject _player;
    private PlayerController _playerScript;

    void Start()
    {
        // Gets the script for the Player to later add coins.
        _playerScript = _player.GetComponent<PlayerController>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // If the Player collides with the coin the players routine CollectCoin is called
        // and the coin is destroyed.
        if (other.CompareTag("Player"))
        {
            _playerScript.CollectCoin();
            Destroy(this.gameObject);
        }
    }
}
