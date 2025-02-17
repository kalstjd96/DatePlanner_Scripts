using Firebase.Firestore;
using UnityEngine;
using System.Collections.Generic;
using Firebase.Extensions;

namespace Firebase.Database
{

    public class FirestoreDataUploader : MonoBehaviour
    {
        private FirebaseFirestore db;

        void Start()
        {
            FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task =>
            {
                if (task.Result == DependencyStatus.Available)
                {
                    db = FirebaseFirestore.DefaultInstance;
                    UploadData();
                }
                else
                {
                    Debug.LogError($"Firebase dependencies error: {task.Result}");
                }
            });
        }

        void UploadData()
        {
            // Firestore 컬렉션 참조
            CollectionReference dateCoursesCollection = db.Collection("DateCourses");

            // 데이터 준비 (여기서 JSON 구조를 사용)
            var courseID2 = new Dictionary<string, object>
        {
            { "main", new Dictionary<string, object>
                {
                    { "budget", 75000 },
                    { "description", "연인과의 백운호수 구경 코스" },
                    { "duration", 5 },
                    { "image", "https://example.com/image1.jpg" },
                    { "isLikedByUser", false },
                    { "likesCount", 20 },
                    { "rating", 2.5 },
                    { "region", 2 },
                    { "tags", new List<string> { "호수 명소 탐방", "백운호수 드라이브 코스", "비싼 가격으로 즐기는 하루" } },
                    { "title", "백운호수 코스" }
                }
            },
            { "detail", new Dictionary<string, object>
                {
                    { "courseDescription", "수원 화성행궁의 아름다운 풍경과 함께 강아지 카페" },
                    { "detailedSteps", new List<Dictionary<string, object>>
                        {
                            new Dictionary<string, object>
                            {
                                { "stepTitle", "백운호수 주차장" },
                                { "description", "역사적 명소인 화성행궁에서 사진 찍기와 관람." },
                                { "estimatedTime", 10 },
                                { "image", "https://example.com/image_step1.jpg" }
                            },
                            new Dictionary<string, object>
                            {
                                { "stepTitle", "강아지 카페 방문" },
                                { "description", "강아지와 함께 편안히 쉴 수 있는 카페 방문." },
                                { "estimatedTime", 90 },
                                { "image", "https://example.com/image_step2.jpg" }
                            }
                        }
                    }
                }
            }
        };

            // Firestore에 업로드
            dateCoursesCollection.Document("courseID2").SetAsync(courseID2).ContinueWithOnMainThread(task =>
            {
                if (task.IsCompleted)
                {
                    Debug.Log("Data uploaded to Firestore successfully!");
                }
                else
                {
                    Debug.LogError("Error uploading data: " + task.Exception);
                }
            });

            Debug.Log("Data Insert Success");
        }
    }
}
