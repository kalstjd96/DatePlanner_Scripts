using Firebase;
using Firebase.Auth;
using Firebase.Database;
using Firebase.Extensions;
using System.Collections.Generic;
using UnityEngine;


public class DateCourseController : MonoBehaviour
{
    public DateCourseView dateCourseView;
    private Dictionary<CategoryType, List<DateCourse>> dateCourses = new Dictionary<CategoryType, List<DateCourse>>();
    private DatabaseReference databaseReference;

    public enum CategoryType
    {
        Main,
        Favorite,
        Event
    }

    void Start()
    {
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task =>
        {
            if (task.IsFaulted)
            {
                Debug.LogError("Firebase dependencies could not be resolved: " + task.Exception);
                return; 
            }

            //FirebaseApp app = FirebaseApp.DefaultInstance;
            databaseReference = FirebaseDatabase.DefaultInstance.RootReference;
            LoadDateCourses(CategoryType.Main);
            dateCourseView.SetButtonCallback(category => LoadDateCourses(category));
        });
    }

    private void LoadDateCourses(CategoryType category)
    {
        string categoryString = category.ToString().Trim();

        switch (category)
        {
            case CategoryType.Main:
                ShowMains();
                break;
            case CategoryType.Favorite:
                ShowFavorites();
                break;
            case CategoryType.Event:
                break;
            default:
                break;
        }

        
    }

    private void ShowMains()
    {
        databaseReference.Child("items").GetValueAsync().ContinueWithOnMainThread(task =>
        {
            if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;

                if (snapshot.Exists)
                {
                    List<DateCourse> courses = new List<DateCourse>();

                    foreach (DataSnapshot itemData in snapshot.Children)
                    {
                        string title = itemData.Child("title").Value?.ToString() ?? "Unknown";
                        string description = itemData.Child("description").Value?.ToString() ?? "";
                        string images = itemData.Child("Image").Value?.ToString() ?? "";
                        int duration = int.TryParse(itemData.Child("duration").Value?.ToString(), out int parsedDuration) ? parsedDuration : 0;
                        int budget = int.TryParse(itemData.Child("budget").Value?.ToString(), out int parsedBudget) ? parsedBudget : 0;
                        string preview = itemData.Child("preview").Value?.ToString() ?? "";
                        float rating = float.TryParse(itemData.Child("rating").Value?.ToString(), out float parsedRating) ? parsedRating : 0f;
                        string tag = itemData.Child("tag").Value?.ToString() ?? "";
                        bool like = bool.TryParse(itemData.Child("like").Value?.ToString(), out bool parsedLike) ? parsedLike : false;

                        courses.Add(new DateCourse(title, description, images, duration, budget, preview, rating, tag, like));
                    }

                    // 데이터를 View에 전달
                    dateCourses[CategoryType.Main] = courses;
                    dateCourseView.DisplayDateCourses(courses);
                }
                else
                {
                    Debug.LogWarning($"No data found in items.");
                }
            }
            else
            {
                Debug.LogError("Error fetching data from Firebase: " + task.Exception);
            }
        });
    }

    private void ShowFavorites()
    {
        string userId = FirebaseAuth.DefaultInstance.CurrentUser?.UserId;

        if (string.IsNullOrEmpty(userId))
        {
            Debug.LogError("로그인된 사용자가 없습니다.");
            return;
        }

        databaseReference.Child("favorites").Child(userId).GetValueAsync().ContinueWithOnMainThread(task =>
        {
            if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;
                List<DateCourse> favoriteCourses = new List<DateCourse>();

                foreach (var child in snapshot.Children)
                {
                    string courseId = child.Key;
                    DateCourse course = FindCourseById(courseId);

                    if (course != null)
                    {
                        favoriteCourses.Add(course);
                    }
                }

                dateCourseView.DisplayDateCourses(favoriteCourses);
            }
            else
            {
                Debug.LogError("즐겨찾기 데이터를 가져오는 중 오류 발생: " + task.Exception);
            }
        });
    }

    private DateCourse FindCourseById(string courseId)
    {
        foreach (var category in dateCourses.Values)
        {
            //DateCourse course = category.Find(c => c.== courseId);
            //if (course != null)
            //{
            //    return course;
            //}
        }

        return null;
    }


    public void AddDateCourse(DateCourse dateCourse)
    {
        DatabaseReference courseRef = databaseReference.Child("items").Push(); // 고유 ID 생성

        Dictionary<string, object> courseData = new Dictionary<string, object>
        {
            { "title", dateCourse.Title },
            { "description", dateCourse.Description },
            { "Image", dateCourse.Images },
            { "duration", dateCourse.Duration },
            { "budget", dateCourse.Budget },
            { "preview", dateCourse.Preview },
            { "rating", dateCourse.Rating },
            { "tag", dateCourse.Tag },
            { "like", dateCourse.Like }
        };

        courseRef.SetValueAsync(courseData).ContinueWithOnMainThread(task =>
        {
            if (task.IsCompleted)
            {
                Debug.Log("Date course added successfully.");
            }
            else
            {
                Debug.LogError("Error adding date course: " + task.Exception);
            }
        });
    }

    private void OnDestroy()
    {
        dateCourseView.CleanupListeners();
    }
}
