using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UIElements;

[RequireComponent(typeof(UIDocument))]
public class AppUIController : MonoBehaviour
{
    private void Awake()
    {
        InitComponents();
        InitTitle();
        InitInput();
    }

    private void Start()
    {
        ApplicationChrome.statusBarState = ApplicationChrome.States.Visible;
    }

    private void Update()
    {
        if (mTitle.ClassListContains("fade-in"))
        {
            var t = mTitle.style.transitionDuration;
        }
    }

    #region Components

    private UIDocument mUIDocument;

    private void InitComponents()
    {
        mUIDocument = GetComponent<UIDocument>();
    }

    #endregion

    #region Title

    private VisualElement mTitle;

    private void InitTitle()
    {
        mTitle = mUIDocument.rootVisualElement.Q<VisualElement>("Title");
        mTitle.AddToClassList("hidden");
    }

    public void ShowTitle()
    {
        AppUIAnimator.FadeIn(mTitle);
    }

    public void HideTitle()
    {
        AppUIAnimator.FadeOut(mTitle);
    }

    public void UpdateTitleAnimation()
    {

    }

    #endregion

    #region Input

    private void InitInput()
    {
        mUIDocument.rootVisualElement.Q<Button>("FadeIn").clicked += () =>
        {
            ShowTitle();
        };
        mUIDocument.rootVisualElement.Q<Button>("FadeOut").clicked += () =>
        {
            HideTitle();
        };
    }

    #endregion
}
