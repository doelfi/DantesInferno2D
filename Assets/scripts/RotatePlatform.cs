using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class RotatePlatform : MonoBehaviour
{
    private float rotationSpeed = 10f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // FIXME: currently it does not rotates around the center of the square but the center of the selected slope
    void Update()
    {
        transform.Rotate(0, 0, Time.deltaTime * rotationSpeed, Space.Self);
    }
}
