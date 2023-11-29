using UnityEngine;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine.SocialPlatforms;

public class GPGSAuthentication : MonoBehaviour
{
    void Start()
    {
        PlayGamesPlatform.DebugLogEnabled = true;
        PlayGamesPlatform.Activate();
    }

    void Native()
    {
        PlayGamesPlatform.Instance.Authenticate(NativeProcessAuthentication);
    }

    void Integration()
    {
        Social.localUser.Authenticate(IntegrationProcessAuthentication);
    }

    internal void NativeProcessAuthentication(SignInStatus status)
    {
        if (status == SignInStatus.Success)
        {
            IntegrationProcessAuthentication(true);
        }
        else
        {
            IntegrationProcessAuthentication(false);
        }
    }

    internal void IntegrationProcessAuthentication(bool status)
    {
        if (status == true)
        {
            Debug.Log("Logged in successfuly!");
        }
        else
        {
            Debug.LogError("Logged in failed!");
        }
    }
}
