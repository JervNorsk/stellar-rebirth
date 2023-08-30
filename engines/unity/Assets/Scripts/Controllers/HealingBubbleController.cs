using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;

#if UNITY_EDITOR
[CustomEditor(typeof(HealingBubbleController))]
public class HealingBubbleControllerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        var controller = target as HealingBubbleController;

        if (controller!.debug)
        {
            EditorGUILayout.Toggle("Resize Animation", controller.ResizeBubbleAnimation);
            EditorGUILayout.FloatField("Resize Animation Speed", controller.ResizeBubbleAnimationSpeed);

            if (GUILayout.Button("Resize") && Application.isPlaying)
            {
                controller.ResizeBubble();
            }
        }
    }
}
#endif

// [ExecuteInEditMode]
public class HealingBubbleController : MonoBehaviour
{
    internal SphereCollider BubbleCollider;

    public float resizeBubbleSpeed;
    public float resizeBubbleSize;
    internal bool ResizeBubbleAnimation;
    internal float ResizeBubbleAnimationSpeed;

    public bool debug;

    private void Start()
    {
        InitBubble();
    }

    private void Update()
    {
        if (ResizeBubbleAnimation)
        {
            ResizeBubbleUpdateAnimation();
        }
    }

    private void InitBubble()
    {
        // Get collision sphere.
        BubbleCollider = GetComponent<SphereCollider>();

        if (BubbleCollider == null)
        {
            // Create collision sphere.
            BubbleCollider = gameObject.AddComponent<SphereCollider>();
        }

        // Set collision sphere default parameters.
        BubbleCollider.enabled = false;
        BubbleCollider.radius = 0;
    }

    public void ResizeBubble(float? set = null, float? to = null)
    {
        // Start bubble resize animation.
        ResizeBubbleStartAnimation();
    }

    private void ResizeBubbleStartAnimation()
    {
        // Compute delta size.
        var deltaSize = resizeBubbleSize - BubbleCollider.radius;

        //  Set animation parameters.
        ResizeBubbleAnimation = true;
        ResizeBubbleAnimationSpeed = resizeBubbleSpeed * Math.Sign(deltaSize != 0 ? deltaSize : -1);
    }

    private void ResizeBubbleStopAnimation()
    {
        //  Set animation parameters.
        ResizeBubbleAnimation = false;
        ResizeBubbleAnimationSpeed = 0;
    }

    private void ResizeBubbleUpdateAnimation()
    {
        // Initialize animation variables.
        var nextRadius = BubbleCollider.radius;

        // Compute animation variables.
        nextRadius += ResizeBubbleAnimationSpeed;

        // Check animation state.
        if (Math.Sign(ResizeBubbleAnimationSpeed) <= 0)
        {
            // Decreasing resize animation.

            if (nextRadius <= 0)
            {
                // The bubble reach the decreasing limit.

                // Update sphere collision parameters.
                BubbleCollider.radius = 0;

                // Stop animation.
                ResizeBubbleStopAnimation();
                return;
            }

            if (nextRadius <= resizeBubbleSize)
            {
                // The bubble reach the resize limit.

                // Update sphere collision parameters.
                BubbleCollider.radius = resizeBubbleSize;

                // Stop animation.
                ResizeBubbleStopAnimation();
                return;
            }
        }
        else
        {
            // Increasing resize animation.

            if (nextRadius >= resizeBubbleSize)
            {
                // The bubble still increasing.

                // Update sphere collision parameters.
                BubbleCollider.radius = resizeBubbleSize;

                // Stop animation.
                ResizeBubbleStopAnimation();
                return;
            }
        }

        // The animation still continuing.

        // Update sphere collision parameters.
        BubbleCollider.radius = nextRadius;
    }
}
