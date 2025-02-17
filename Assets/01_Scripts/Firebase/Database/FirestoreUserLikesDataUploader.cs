using Firebase.Extensions;
using Firebase.Firestore;
using System.Collections.Generic;
using UnityEngine;

namespace Firebase.Database
{ 
    public class FirestoreUserLikesDataUploader : MonoBehaviour
    {
        FirebaseFirestore firestore;

        void Start()
        {
            firestore = FirebaseFirestore.DefaultInstance;

            // UserLikes 데이터를 Firestore에 삽입
            AddUserLikes("user1@example.com");
        }

        void AddUserLikes(string userEmail)
        {
            DocumentReference userDoc = firestore.Collection("UserLikes").Document(userEmail);

            Dictionary<string, object> userLikesData = new Dictionary<string, object>
            {
                { "likedCourses", new Dictionary<string, bool> { { "courseID1", true }, { "courseID2", true } } },
                { "evaluatedCourses", new Dictionary<string, double> { { "courseID1", 4.0 }, { "courseID2", 5.0 } } }
            };

            userDoc.SetAsync(userLikesData).ContinueWithOnMainThread(task =>
            {
                if (task.IsCompleted)
                {
                    Debug.Log("UserLikes 데이터 삽입 성공");
                }
                else
                {
                    Debug.LogError($"UserLikes 데이터 삽입 실패: {task.Exception}");
                }
            });
        }
    }
}