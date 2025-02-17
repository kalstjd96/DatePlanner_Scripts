using DateApp.Platform.Intro;
using Firebase.Auth;
using Firebase.Database;
using Firebase.Utility;
using Main.User;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Firebase
{
    /// <summary>
    ///  Firebase를 사용하는 상위 관리자 클래스
    /// </summary>
    public class FirebaseManager : MonoBehaviour
    {
        private FirebaseAuthManager _authManager;
        private FirebaseDatabaseManager _databaseManager;

        [SerializeField] private TextMeshProUGUI errorMsg;
        [SerializeField] private Button googleLoginButton;
        private string _googleClientID;
    
        private void Awake()
        {
            _googleClientID = FirebaseConfigLoader.LoadGoogleClientID();
            FirebaseInitializer.InitializeFirebase(OnFirebaseInitialized);
        }

        private void OnFirebaseInitialized(DependencyStatus status)
        {
            if (status == DependencyStatus.Available)
            {
                Debug.Log("Firebase initialized successfully.");

                _authManager = new FirebaseAuthManager(_googleClientID);
                _databaseManager = new FirebaseDatabaseManager();

                googleLoginButton.onClick.AddListener(OnGoogleSignInClicked);
            }
            else
            {
                Debug.LogError($"Could not initialize Firebase: {status}");
            }
        }

        private void OnGoogleSignInClicked()
        {
            _ = _authManager.SignInWithGoogleAsync(
                onError: error =>
                {
                    errorMsg.text = error;
                    Debug.LogError(error);
                },
                onSuccess: user =>
                {
                    Debug.Log($"User signed in: {user.Email}");
                    UserData.SetUserInfo(user.Email);

                    // Scene 이동
                    SceneDefine.SceneNames.TryGetValue(SceneDefine.SceneName.Main, out string sceneName);
                    if (!string.IsNullOrEmpty(sceneName))
                        SceneManager.LoadScene(sceneName);
                    else
                        Debug.LogError("지정한 씬이 없습니다.");
                }
            );
        }
    }
}
