using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UIElements;

[RequireComponent(typeof(UIDocument))]
public class AppUIController : MonoBehaviour
{
    private static AppUIController instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        InitComponents();
        InitHUD();
        InitTitle();
        InitLoadingBar();
    }

    private void Start()
    {
        ApplicationChrome.statusBarState = ApplicationChrome.States.Visible;
    }

    private void Update()
    {
        // if (mTitle.ClassListContains("fade-in"))
        // {
        //     var t = mTitle.style.transitionDuration;
        // }
    }

    #region Components

    private UIDocument mUIDocument;

    private void InitComponents()
    {
        mUIDocument = GetComponent<UIDocument>();
    }

    public  static UIDocument GetUIDocument()
    {
        return instance.mUIDocument;
    }

    public static T Q<T>(String id) where T: VisualElement
    {
        return GetUIDocument().rootVisualElement.Q<T>(id);
    }

    #endregion

    #region HUD

    private VisualElement mHUD;

    private void InitHUD()
    {
        mHUD = mUIDocument.rootVisualElement.Q<VisualElement>("HUD");

        if (mHUD == null)
        {
            throw new MissingReferenceException("HUD reference is missing!");
        }
    }

    public static VisualElement DoHUDShow()
    {
        // AppUIAnimator.FadeIn(instance.mHUD);
        // instance.mHUD.RemoveFromClassList("hidden");
        // Color bg = instance.mHUD.resolvedStyle.backgroundColor;
        // instance.mHUD.style.backgroundColor = new StyleColor(new Color(bg.r, bg.g, bg.b, 1));
        return AppUIAnimator.FadeIn(instance.mHUD);
    }

    public static VisualElement DoHUDHide()
    {
        // AppUIAnimator.FadeOut(instance.mHUD);
        // instance.mHUD.AddToClassList("hidden");
        // Color bg = instance.mHUD.resolvedStyle.backgroundColor;
        // instance.mHUD.style.backgroundColor = new StyleColor(new Color(bg.r, bg.g, bg.b, 0));
        return AppUIAnimator.FadeOut(instance.mHUD);
    }

    #endregion

    #region Title

    private VisualElement mTitle;

    private void InitTitle()
    {
        mTitle = mUIDocument.rootVisualElement.Q<VisualElement>("Title");
        mTitle.AddToClassList("hidden");
    }

    public static VisualElement DoTitleShow()
    {
        return AppUIAnimator.FadeIn(instance.mTitle);
    }

    public static VisualElement DoTitleHide()
    {
        return AppUIAnimator.FadeOut(instance.mTitle);
    }

    public void UpdateTitleAnimation()
    {

    }

    #endregion

    #region LoadingBar

    private ProgressBar mLoadingBar;

    private void InitLoadingBar()
    {
        mLoadingBar = mUIDocument.rootVisualElement.Q<VisualElement>("LoadingBar").Q<ProgressBar>();

        if (mLoadingBar == null)
        {
            throw new MissingReferenceException("LoadingBar reference is missing!");
        }
    }

    public static void SetLoadingProgression(float progressValue)
    {
        Debug.Log("Set loading progression to " + progressValue);
        // instance.mLoadingBar.value = Mathf.Clamp01(progressValue / 0.9f);
        instance.mLoadingBar.value = progressValue;
    }

    public static float GetLoadingProgression()
    {
        return instance.mLoadingBar.value;
    }

    #endregion
}
