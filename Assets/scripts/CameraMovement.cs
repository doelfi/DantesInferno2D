using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{ 

    [SerializeField]
    private Transform target;
    public Vector3 offset;

    // Start is called before the first frame update
    void Start()
    {
        offset = transform.position - target.transform.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        // NULL CHECK  
        if (target != null)
        {
            // ADD OFFSET - to our position in order to depict the player properly
            Vector3 newPosition = target.transform.position + offset;
            transform.position = newPosition;
        }
    }
}
