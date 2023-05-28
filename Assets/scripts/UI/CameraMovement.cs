using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class CameraMovement : MonoBehaviour
{ 
    // The Camera is always following the movement of the Player
    // so that the PLayer is always in the middle of the screen.

    [SerializeField]
    private Transform _target;
    public Vector3 offset;

    void Start()
    {
        offset = transform.position - _target.transform.position;
    }

    void LateUpdate()
    {
        // Null check  
        if (_target != null)
        {
            // Add an offset to our position in order to depict the player properly
            Vector3 newPosition = _target.transform.position + offset;
            transform.position = newPosition;
        }
    }
}
