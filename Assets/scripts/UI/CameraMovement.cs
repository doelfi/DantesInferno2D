using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class CameraMovement : MonoBehaviour
{ 

    [SerializeField]
    private Transform _target;
    public Vector3 offset;

    // Start is called before the first frame update
    void Start()
    {
        offset = transform.position - _target.transform.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        // NULL CHECK  
        if (_target != null)
        {
            // ADD OFFSET - to our position in order to depict the player properly
            Vector3 newPosition = _target.transform.position + offset;
            transform.position = newPosition;
        }
    }
}
