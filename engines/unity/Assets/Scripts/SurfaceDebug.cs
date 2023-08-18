using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SurfaceDebug : MonoBehaviour
{
    public float speed;
    public Vector3 direction;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (transform.localEulerAngles.x >= 2)
        {
            direction.x = -speed;
        }
        if (transform.localEulerAngles.x >= 354)
        {
            direction.x = speed;
        }

        transform.Rotate(direction);
    }
}
