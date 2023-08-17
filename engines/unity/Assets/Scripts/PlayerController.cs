using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public GameObject Planet;

    private GameObject Surface;

    // Start is called before the first frame update
    void Start()
    {
        if (Planet != null)
        {
            Debug.Log(Planet);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
