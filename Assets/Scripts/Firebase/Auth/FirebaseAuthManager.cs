using System;
using System.Threading.Tasks;
using DateApp.Platform.Define;
using Firebase.Extensions;
using Google;
using Main.User;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Firebase.Auth
{
    /// <summary>
    ///  Firebase 인증과 관련된 모든 로직
    /// </summary>
    public class FirebaseAuthManager
    {
        private FirebaseAuth _auth;
        private FirebaseUser _user;
        private readonly string _googleClientID;
        private readonly SceneDefine.SceneName Main;

        public FirebaseAuthManager(string googleClientID)
        {
            _googleClientID = googleClientID;
            _auth = FirebaseAuth.DefaultInstance;
        }

        public FirebaseUser CurrentUser => _user;

        public async Task SignInWithGoogleAsync(Action<string> onError, Action<FirebaseUser> onSuccess)
        {
            try
            {
                GoogleSignIn.Configuration = new GoogleSignInConfiguration
                {
                    WebClientId = _googleClientID,
                    RequestIdToken = true,
                    UseGameSignIn = false,
                    RequestEmail = true
                };

                GoogleSignInUser googleUser = await GoogleSignIn.DefaultInstance.SignIn();

                Credential credential = GoogleAuthProvider.GetCredential(googleUser.IdToken, null);
                await _auth.SignInWithCredentialAsync(credential).ContinueWithOnMainThread(task =>
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

                    _user = _auth.CurrentUser;
                    UserData.SetUserInfo(_user.Email);

                    if (SceneDefine.SceneNames.TryGetValue(Main, out string sceneName))
                        SceneManager.LoadScene(sceneName);
                    else
                        Debug.LogError("지정한 씬이 없습니다.");
                    // StartCoroutine(LoadImage(CheckImageUrl(user.PhotoUrl.ToString())));
                });

                _user = _auth.CurrentUser;
                onSuccess?.Invoke(_user);
            }
            catch (Exception ex)
            {
                onError?.Invoke($"Authentication error: {ex.Message}");
            }
        }
    }

}

