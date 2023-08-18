using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class InputController : MonoBehaviour
{
    public bool debug;

    public Camera camera;

    public RaycastHit _raycastScreenInputHit;

    private void Start()
    {
    }

    private void Update()
    {
    }

    private void OnDrawGizmos()
    {
        if (debug)
        {
            RaycastScreenInput(out _raycastScreenInputHit, debug = true);
        }
    }

    private bool RaycastScreenInput(out RaycastHit raycastHit, bool debug = false)
    {
        if (Physics.Raycast(camera.ScreenPointToRay(Input.mousePosition), out raycastHit, 1000))
        {
            if (debug)
            {
                Gizmos.DrawWireSphere(_raycastScreenInputHit.point, 0.1f);
            }

            return true;
        }

        return false;
    }

    public Vector3? GetScreenInputPoint()
    {
        if (RaycastScreenInput(out _raycastScreenInputHit))
        {
            return _raycastScreenInputHit.point;
        }

        return null;
    }
}
