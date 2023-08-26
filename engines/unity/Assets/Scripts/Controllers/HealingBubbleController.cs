using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;

[CustomEditor(typeof(HealingBubbleController))]
public class HealingBubbleControllerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        var controller = target as HealingBubbleController;

        if (controller!.debug)
        {
            EditorGUILayout.FloatField("Resize Animation", controller._resizeBubbleAnimationSpeed);

            if (GUILayout.Button("Resize") && Application.isPlaying)
            {
                controller.ResizeBubble();
            }
        }
    }
}

// [ExecuteInEditMode]
public class HealingBubbleController : MonoBehaviour
{
    public bool debug;

    private SphereCollider _bubbleCollider;

    internal bool _resizeBubbleAnimation;
    internal float _resizeBubbleAnimationSpeed;
    public float _resizeBubbleSpeed;
    public float _resizeBubbleSize;

    private void Start()
    {
        InitBubble();
    }

    private void Update()
    {
        if (_resizeBubbleAnimation)
        {
            ResizeBubbleUpdateAnimation();
        }
    }

    private void OnDrawGizmos()
    {
        if (debug)
        {
            // var screenInputPoint = inputController.GetScreenInputPoint();
        }
    }

    private void InitBubble()
    {
        // Get collision sphere.
        _bubbleCollider = GetComponent<SphereCollider>();

        if (_bubbleCollider == null)
        {
            // Create collision sphere.
            _bubbleCollider = gameObject.AddComponent<SphereCollider>();
        }

        // Set collision sphere default parameters.
        _bubbleCollider.enabled = false;
        _bubbleCollider.radius = 0;
    }

    public void ResizeBubble(float? set = null, float? to = null)
    {
        // Start bubble resize animation.
        ResizeBubbleStartAnimation();
    }

    private void ResizeBubbleStartAnimation()
    {
        // Compute delta size.
        var deltaSize = _resizeBubbleSize - _bubbleCollider.radius;

        //  Set animation parameters.
        _resizeBubbleAnimation = true;
        _resizeBubbleAnimationSpeed = _resizeBubbleSpeed * Math.Sign(deltaSize != 0 ? deltaSize : -1) ;
    }

    private void ResizeBubbleStopAnimation()
    {
        //  Set animation parameters.
        _resizeBubbleAnimation = false;
        _resizeBubbleAnimationSpeed = 0;
    }

    private void ResizeBubbleUpdateAnimation()
    {
        // Initialize animation variables.
        var nextRadius = _bubbleCollider.radius;

        // Compute animation variables.
        nextRadius += _resizeBubbleAnimationSpeed;

        // Check animation state.
        if (Math.Sign(_resizeBubbleAnimationSpeed) <= 0)
        {
            // Decreasing resize animation.

            if (nextRadius <= 0)
            {
                // The bubble reach the decreasing limit.

                // Update sphere collision parameters.
                _bubbleCollider.radius = 0;

                // Stop animation.
                ResizeBubbleStopAnimation();
                return;
            }

            if (nextRadius <= _resizeBubbleSize)
            {
                // The bubble reach the resize limit.

                // Update sphere collision parameters.
                _bubbleCollider.radius = _resizeBubbleSize;

                // Stop animation.
                ResizeBubbleStopAnimation();
                return;
            }
        }
        else
        {
            // Increasing resize animation.

            if (nextRadius >= _resizeBubbleSize)
            {
                // The bubble still increasing.

                // Update sphere collision parameters.
                _bubbleCollider.radius = _resizeBubbleSize;

                // Stop animation.
                ResizeBubbleStopAnimation();
                return;
            }
        }

        // The animation still continuing.

        // Update sphere collision parameters.
        _bubbleCollider.radius = nextRadius;
    }
}
