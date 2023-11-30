using System;
using System.Collections;
using Google.Play.AppUpdate;
using UnityEngine;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine.SocialPlatforms;

public class GooglePlayAppUpdate : MonoBehaviour
{
    private AppUpdateManager mAppUpdateManager;

    void Start()
    {
        mAppUpdateManager = new AppUpdateManager();

        StartCoroutine(CheckForUpdate());
    }

    IEnumerator CheckForUpdate()
    {
        var appUpdateInfoOperation = mAppUpdateManager.GetAppUpdateInfo();

        yield return appUpdateInfoOperation;

        if (appUpdateInfoOperation.IsSuccessful)
        {
            var appUpdateInfoResult = appUpdateInfoOperation.GetResult();
            if (appUpdateInfoResult.UpdateAvailability == UpdateAvailability.UpdateAvailable)
            {
                Debug.Log("A new update is available!");

                var appUpdateOptions = AppUpdateOptions.ImmediateAppUpdateOptions(
                    allowAssetPackDeletion: false
                );

                StartCoroutine(StartUpdate(appUpdateInfoResult, appUpdateOptions));
            }
            else
            {
                Debug.LogWarning("No new update available!");
            }
        }
        else
        {
            Debug.LogWarning("No in app update feature available!");
        }
    }

    IEnumerator StartUpdate(AppUpdateInfo appUpdateInfoResult, AppUpdateOptions appUpdateOptions)
    {
        var startUpdateRequest = mAppUpdateManager.StartUpdate(
            appUpdateInfoResult,
            appUpdateOptions
        );

        yield return null;
    }
}
