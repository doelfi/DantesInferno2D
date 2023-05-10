using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveFallingSpikes : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            Destroy(this.gameObject);
        }
        else if (other.CompareTag("Player"))
        {
            other.gameObject.GetComponent<PlayerController>().takeDamage();
        }
    }
}