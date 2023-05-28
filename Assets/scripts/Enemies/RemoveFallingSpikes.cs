using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveFallingSpikes : MonoBehaviour
{
    // Removes the falling spikes if they collide with an Enemy, the bottom line of the level 
    // or the Player (the Player takes damage).
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy") || other.CompareTag("BottomLine"))
        {
            Destroy(this.gameObject);
        }
        else if (other.CompareTag("Player"))
        {
            other.gameObject.GetComponent<PlayerController>().TakeDamage();
        }
    }
}
