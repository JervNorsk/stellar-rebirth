using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public bool debug;

    public Vector3 _raycastDownPosition;
    public Vector3 _raycastDownDirection;
    public RaycastHit _raycastDownHit;
    public Vector3 _raycastDownHitPosition;
    public float _raycastDownHitDistance;

    void Start()
    {
    }

    void OnDrawGizmos()
    {
        if (debug)
        {
            if (RaycastDown(out _raycastDownHit, true))
            {
                _raycastDownHitPosition = _raycastDownHit.point;
                _raycastDownHitDistance = _raycastDownHit.distance;
            }
        }
    }

    void Update()
    {
    }

    void FixedUpdate()
    {
        if (RaycastDown(out _raycastDownHit))
        {
            // transform.Translate(_raycastDownDirection * _raycastDownHit.distance);
            // transform.position.Set(
            //     transform.position.x,
            //     _raycastDownHit.point.y,
            //     transform.position.z
            // );
        }
    }

    private bool RaycastDown(out RaycastHit raycastHit, bool debug = false)
    {
        _raycastDownPosition = transform.position - new Vector3(0, -1, 0);
        _raycastDownDirection = transform.TransformDirection(Vector3.down);

        if (Physics.Raycast(_raycastDownPosition, _raycastDownDirection, out raycastHit, Mathf.Infinity))
        {
            if (debug)
            {
                Debug.DrawRay(_raycastDownPosition, _raycastDownDirection * raycastHit.distance, Color.yellow);
                Gizmos.DrawSphere(_raycastDownHit.point, 0.1f);
            }

            return true;
        }
        else
        {
            if (debug)
            {
                Debug.DrawRay(_raycastDownPosition, _raycastDownDirection * 1000, Color.white);
            }

            return false;
        }
    }
}
