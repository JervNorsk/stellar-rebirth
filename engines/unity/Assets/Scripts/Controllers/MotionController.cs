using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;

public class MotionController : MonoBehaviour
{
    public bool debug;

    public InputController inputController;

    public NavMeshAgent navMeshAgent;

    public Vector3 target;

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        var screenInputPoint = inputController.GetScreenInputPoint();

        if (screenInputPoint.HasValue)
        {
            navMeshAgent.destination = screenInputPoint.Value;
        }
    }

    private void OnDrawGizmos()
    {
        if (debug)
        {
            // var screenInputPoint = inputController.GetScreenInputPoint();
        }
    }
}
