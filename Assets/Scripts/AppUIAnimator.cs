using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UIElements;

public class AppUIAnimator
{

    public static T FadeIn<T>(T element) where T: VisualElement
    {
        element.RemoveFromClassList("fade-out");
        element.AddToClassList("fade-in");

        return element;
    }

    public static T FadeOut<T>(T element) where T: VisualElement
    {
        element.RemoveFromClassList("fade-in");
        element.AddToClassList("fade-out");

        return element;
    }
}
