using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;

[CustomEditor(typeof(DecayController))]
public class DecayControllerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        var controller = target as DecayController;

        if (controller!.debug)
        {
            EditorGUILayout.ObjectField("State Mesh", controller.MeshFilter, typeof(MeshFilter), true);
            EditorGUILayout.ObjectField("State Material", controller.MeshRenderer, typeof(MeshRenderer), true);

            if (GUILayout.Button("Heal"))
            {
                controller.SetStateAsHealed();
            }

            if (GUILayout.Button("Decay"))
            {
                controller.SetStateAsDecayed();
            }
        }
    }
}

[ExecuteInEditMode]
public class DecayController : MonoBehaviour
{
    internal MeshFilter MeshFilter;
    internal MeshRenderer MeshRenderer;

    public Mesh healedMesh;
    public Material healedMaterial;

    public Mesh decayedMesh;
    public Material decayedMaterial;

    public bool debug;

    private void Start()
    {
        InitStateMeshFilter();
        InitStateMeshRenderer();
    }

    private void Update()
    {
    }

    private void InitStateMeshFilter()
    {
        // Get collision sphere.
        MeshFilter = GetComponent<MeshFilter>();

        if (MeshFilter == null)
        {
            // Create collision sphere.
            MeshFilter = gameObject.AddComponent<MeshFilter>();
        }
    }

    private void InitStateMeshRenderer()
    {
        // Get collision sphere.
        MeshRenderer = GetComponent<MeshRenderer>();

        if (MeshRenderer == null)
        {
            // Create collision sphere.
            MeshRenderer = gameObject.AddComponent<MeshRenderer>();
        }
    }

    public void SetStateAsHealed()
    {
        // Set current status from heal parameters.
        MeshFilter.mesh = healedMesh;
        MeshRenderer.material = healedMaterial;
    }

    public void SetStateAsDecayed()
    {
        // Set current status from decay parameters.
        MeshFilter.mesh = decayedMesh;
        MeshRenderer.material = decayedMaterial;
    }
}
