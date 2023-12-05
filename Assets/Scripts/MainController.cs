using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UIElements;

public class MainController : MonoBehaviour
{
    private void Awake()
    {
        InitComponents();
        InitUI();
        InitDebug();
    }

    private void Start()
    {
        LoadUniverse(2);
    }

    #region Components

    // public GameObject app;

    private void InitComponents()
    {
        // if (app == null)
        // {
        //     throw new MissingReferenceException("App reference is missing!");
        // }
    }

    #endregion

    #region UI

    private Boolean mTitleAnimationInProgress;

    private void InitUI() {}

    private void UpdateLoadingProgression(float progressValue)
    {
        AppUIController.SetLoadingProgression(progressValue);

        if (!mTitleAnimationInProgress)
        {
            DoLoadingAnimation();
        }
    }

    public void DoLoadingAnimation()
    {
        Debug.Log("Start loading animation");
        mTitleAnimationInProgress = true;

        AppUIController.DoTitleShow().RegisterCallback<TransitionEndEvent>(DoTitleAnimationAfterTitleShow);
    }

    private void DoTitleAnimationAfterTitleShow(TransitionEndEvent @event) {
        while (mLevelLoadingOperationInProgress) {}

        AppUIController.DoHUDHide();
        AppUIController.DoTitleHide().RegisterCallback<TransitionEndEvent>(DoLoadingAnimationAfterTitleHide);
    }

    private void DoLoadingAnimationAfterTitleHide(TransitionEndEvent @event) {
        Debug.Log("Stop loading animation");
        mTitleAnimationInProgress = false;

        SceneManager.SetActiveScene(mLevelLoaded);
    }

    #endregion

    #region Level

    private AsyncOperation mLevelLoadingOperation;
    private Boolean mLevelLoadingOperationInProgress;
    private Scene mLevelLoaded;

    public Coroutine LoadUniverse(int delay = 0)
    {
        return StartCoroutine(LoadLevelAsync("Universe", delay));
    }

    private IEnumerator LoadLevelAsync(string levelToLoadName, int delay = 0)
    {
        yield return new WaitForSeconds(delay);

        if (SceneManager.GetSceneByName(levelToLoadName).isLoaded)
        {
            yield break;
        }

        AppUIController.DoHUDShow();

        Debug.Log("Loading " + levelToLoadName);
        mLevelLoadingOperation = SceneManager.LoadSceneAsync(levelToLoadName, LoadSceneMode.Additive);

        mLevelLoadingOperationInProgress = true;

        while (!mLevelLoadingOperation.isDone)
        {
            UpdateLoadingProgression(Mathf.Clamp01(mLevelLoadingOperation.progress / 0.9f) * 100);

            yield return null;
        }

        mLevelLoaded = SceneManager.GetSceneByName(levelToLoadName);

        mLevelLoadingOperationInProgress = false;
    }

    #endregion


    #region Debug

    private void InitDebug()
    {
        AppUIController.Q<Button>("Action1").clicked += () =>
        {
            var operation = SceneManager.UnloadSceneAsync(1);

            while (!operation.isDone) {}

            LoadUniverse();
        };
        AppUIController.Q<Button>("Action2").clicked += () =>
        {
            AppUIController.DoHUDShow();
        };
        AppUIController.Q<Button>("Action3").clicked += () =>
        {
            AppUIController.DoHUDHide();
        };
    }

    #endregion
}
