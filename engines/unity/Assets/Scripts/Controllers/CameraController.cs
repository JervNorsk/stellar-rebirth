using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;

[ExecuteInEditMode]
public class CameraController : MonoBehaviour
{
    public bool debug;

    public Camera camera;

    public float distanceLimit;
    public float sideBoundsLimit;

    public Vector3 cameraOffset;

    private void OnEnable()
    {
        distanceLimit = GetCameraDistanceFrom(CameraDistanceType.GameObject);
    }

    private void Start()
    {
        if (Application.isPlaying)
        {
            if (camera.transform.parent == transform)
            {
                camera.transform.SetParent(null);
            }
        }
    }

    private void Update()
    {
        UpdateCameraTransform();
    }

    private void OnDrawGizmos()
    {
        if (debug)
        {
            // Debug the camera offset to script handler.
            Gizmos.DrawLine(
                cameraOffset,
                transform.position
            );

            // Debug the side bounds limit.
            Gizmos.DrawCube(
                new Vector3(
                    cameraOffset.x,
                    transform.position.y,
                    cameraOffset.z  + distanceLimit
                ),
                new Vector3(
                    sideBoundsLimit * 2,
                    0.1f,
                    1
                )
            );
        }
    }

    public enum CameraDistanceType
    {
        GameObject,
        SideBounds
    }

    // public float GetCameraDistance(bool toTarget = false, bool sideBounds = false)
    public float GetCameraDistanceFrom(CameraDistanceType type)
    {
        switch (type)
        {
            case CameraDistanceType.GameObject:
                return Vector3.Distance(
                    new Vector3(
                        0,
                        0,
                        transform.position.z
                    ),
                    new Vector3(
                        0,
                        0,
                        camera.transform.position.z
                    )
                );
            case CameraDistanceType.SideBounds:
                return Vector3.Distance(
                    new Vector3(
                        transform.position.x,
                        0,
                        0
                    ),
                    new Vector3(
                        camera.transform.position.x,
                        0,
                        0
                    )
                );
            default:
                throw new InvalidEnumArgumentException();
        }
    }

    private void UpdateCameraTransform()
    {
        cameraOffset = camera.transform.position;

        // Follow the handler position on Z Axis.
        cameraOffset.z = transform.position.z - distanceLimit;

        // TODO: Follow the handler position on Y Axis.

        // Follow the handler position on X Axis when side bounds limit are reached.
        if (GetCameraDistanceFrom(CameraDistanceType.SideBounds) > sideBoundsLimit)
        {
            if (Math.Sign(transform.position.x) > 0)
            {
                cameraOffset.x = transform.position.x - sideBoundsLimit;
            }
            else
            {
                cameraOffset.x = transform.position.x + sideBoundsLimit;
            }
        }

        // Apply the camera offset.
        camera.transform.position = cameraOffset;
    }
}
