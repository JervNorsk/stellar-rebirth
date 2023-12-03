using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UIElements;

public class AppUIAnimator
{

    public static void FadeIn(VisualElement element)
    {
        element.RemoveFromClassList("fade-out");
        element.AddToClassList("fade-in");
    }

    public static void FadeOut(VisualElement element)
    {
        element.RemoveFromClassList("fade-in");
        element.AddToClassList("fade-out");
    }
}
