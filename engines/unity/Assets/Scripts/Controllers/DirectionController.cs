using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[Obsolete("This script not work with UnityEngine.AI - NavMeshAgent")]
public class DirectionController : MonoBehaviour
{
    public bool debug;

    public InputController inputController;

    public Vector3 target;

    void Start()
    {
    }

    void Update()
    {
        var screenInputPoint = inputController.GetScreenInputPoint();

        if (screenInputPoint.HasValue)
        {
            target = screenInputPoint.Value;
            target.y = transform.position.y;

            transform.LookAt(target, Vector3.up);
        }
    }

    private void OnDrawGizmos()
    {
        if (debug)
        {
            var direction = transform.TransformDirection(Vector3.forward);
            direction.Scale(transform.localScale);
            Gizmos.DrawLine(transform.position, transform.position + direction);
        }
    }
}
