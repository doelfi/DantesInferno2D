using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Coins : MonoBehaviour
{

    [SerializeField]
    private GameObject _player;
    private PlayerController _playerScript;

    // Start is called before the first frame update
    void Start()
    {
        _playerScript = _player.GetComponent<PlayerController>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            UnityEngine.Debug.Log("Coin collected");
            _playerScript.CollectCoin();
            Destroy(this.gameObject);
        }
    }
}