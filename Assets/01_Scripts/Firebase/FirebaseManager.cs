using System;
using System.IO;
using System.Threading.Tasks;
using DateApp.Platform.Intro;
using Firebase;
using Firebase.Auth;
using Firebase.Extensions;
using Google;
using Main.User;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FirebaseManager : MonoBehaviour
{
    //private GoogleSignInConfiguration configuration;
    //Firebase.DependencyStatus dependencyStatus = Firebase.DependencyStatus.UnavailableOther;
    Firebase.Auth.FirebaseAuth auth;
    Firebase.Auth.FirebaseUser user;

    [SerializeField]
    private TextMeshProUGUI errorMsg;
    [SerializeField]
    private Button googleLoginButton;
    private readonly SceneDefine.SceneName Main;
    private string googleClientID;

    //public TextMeshProUGUI Username, UserEmail;

    private void Awake()
    {
        googleLoginButton.onClick.AddListener(GoogleSignInClick);
        googleClientID = GetFirebaseInfo();
        InitFirebase();
    }

    string GetFirebaseInfo()
    {
        // Resources 폴더에서 securedata.json 파일 로드
        TextAsset jsonFile = Resources.Load<TextAsset>("secureData");
        if (jsonFile == null)
        {
            Debug.LogError("securedata.json not found in Resources folder!");
            return null;
        }

        // JSON 파싱 및 데이터 반환
        SecureData config = JsonUtility.FromJson<SecureData>(jsonFile.text);
        if (config != null)
        {
            return config.googleClientID;
        }
        else
        {
            Debug.LogError("Failed to parse securedata.json!");
            return null;
        }
    }


    void InitFirebase()
    {
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task => {
            if (task.Result == DependencyStatus.Available)
            {
                auth = FirebaseAuth.DefaultInstance;
                Debug.Log("Firebase Auth initialized successfully.");
            }
            else
            {
                Debug.LogError("Could not resolve Firebase dependencies: " + task.Result);
            }
        });
    }

    public void GoogleSignInClick()
    {
        try
        {
            GoogleSignIn.Configuration = new GoogleSignInConfiguration
            {
                WebClientId = googleClientID,
                RequestIdToken = true,
                UseGameSignIn = false,
                RequestEmail = true
            };

            GoogleSignIn.DefaultInstance.SignIn().ContinueWith(task =>
            {
                if (task.IsFaulted)
                {
                    errorMsg.text = "SignIn Error: " + task.Exception;
                }
                else if (task.IsCanceled)
                {
                    errorMsg.text = "SignIn Canceled: ";
                }
                else
                {
                    OnGoogleAuthenticatedFinished(task);
                }
            });
        }
        catch (Exception ex)
        {
            errorMsg.text = "GoogleSignInClick Exception: " + ex.Message;
        }
    }

    void OnGoogleAuthenticatedFinished(Task<GoogleSignInUser> task)
    {
        if (task.IsFaulted)
        {
            Debug.LogError("Faulted");
        }
        else if (task.IsCanceled)
        {
            Debug.LogError("Cancelled");
        }
        else
        {
            Firebase.Auth.Credential credential = Firebase.Auth.GoogleAuthProvider.GetCredential(task.Result.IdToken, null);

            auth.SignInWithCredentialAsync(credential).ContinueWithOnMainThread(task => 
            {
                if (task.IsCanceled)
                {
                    return;
                }

                if (task.IsFaulted)
                {
                    Debug.LogError("SignInWithCredentialAsync encountered an error: " + task.Exception);
                    return;
                }

                user = auth.CurrentUser;
                UserData.SetUserInfo(user.Email);
                
                if (SceneDefine.SceneNames.TryGetValue(Main, out string sceneName))
                    SceneManager.LoadScene(sceneName);
                else
                    Debug.LogError("지정한 씬이 없습니다.");
                // StartCoroutine(LoadImage(CheckImageUrl(user.PhotoUrl.ToString())));
            });
        }
    }
}