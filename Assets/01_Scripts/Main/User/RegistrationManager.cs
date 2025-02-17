using Firebase.Auth;
using Firebase.Extensions;
using Firebase.Firestore;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegistrationManager : MonoBehaviour
{
    private FirebaseFirestore firestore;
    private FirebaseAuth auth;

    void Start()
    {
        auth = FirebaseAuth.DefaultInstance;
        firestore = FirebaseFirestore.DefaultInstance;
    }

    public void RegisterNewUser(string displayName, string profileImageUrl)
    {
        FirebaseUser user = auth.CurrentUser;
        if (user == null)
        {
            Debug.LogError("No user is logged in.");
            return;
        }

        // Firestore에 사용자 데이터 추가
        DocumentReference userDocRef = firestore.Collection("Users").Document(user.UserId);

        var newUser = new
        {
            email = user.Email,
            displayName = displayName,
            profileImage = profileImageUrl,
            likedCourses = new string[] { },
            evaluatedCourses = new Dictionary<string, double>(),
            createdAt = Timestamp.GetCurrentTimestamp(),
            lastLogin = Timestamp.GetCurrentTimestamp()
        };

        userDocRef.SetAsync(newUser).ContinueWithOnMainThread(task =>
        {
            if (task.IsCompleted)
            {
                Debug.Log("User registered successfully.");
                // Main Scene으로 이동
                UnityEngine.SceneManagement.SceneManager.LoadScene("MainScene");
            }
            else
            {
                Debug.LogError("Failed to register user: " + task.Exception);
            }
        });
    }
}

