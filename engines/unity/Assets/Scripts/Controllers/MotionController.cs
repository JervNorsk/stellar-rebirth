using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MotionController : MonoBehaviour
{
    public bool debug;

    public InputController inputController;

    public Vector3 target;

    void Start()
    {
    }

    void Update()
    {
    }

    private void OnDrawGizmos()
    {
        if (debug)
        {
        }
    }
}
