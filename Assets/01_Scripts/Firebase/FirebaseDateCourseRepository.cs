using Firebase.Auth;
using Firebase.Database;
using Firebase.Firestore;
using interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using static DateCourseModel;

public class FirebaseDateCourseRepository : IDateCourseRepository
{
    private readonly DatabaseReference _databaseReference;
    private readonly FirebaseFirestore firestore;

    public FirebaseDateCourseRepository()
    {
        _databaseReference = FirebaseDatabase.DefaultInstance.RootReference;
        firestore = FirebaseFirestore.DefaultInstance;
    }

    public async Task<List<DateCourseModel>> GetAllDateCoursesAsync()
    {
        var snapshot = await firestore.Collection("DateCourses").GetSnapshotAsync();
        var dateCourses = new List<DateCourseModel>();


        foreach (var doc in snapshot.Documents)
        {
            if (doc.ContainsField("main"))
            {
                // "main" 필드를 Dictionary로 안전하게 가져오기
                if (doc.TryGetValue("main", out object mainDataObj) && mainDataObj is Dictionary<string, object> mainData)
                {
                    // Dictionary 데이터를 매핑하여 모델 생성
                    var model = new DateCourseModel
                    {
                        CourseId = doc.Id,
                        Title = mainData.ContainsKey("title") ? mainData["title"]?.ToString() ?? "Unknown" : "Unknown",
                        Description = mainData.ContainsKey("description") ? mainData["description"]?.ToString() ?? "" : "",
                        Duration = mainData.ContainsKey("duration") ? Convert.ToInt32(mainData["duration"]) : 0,
                        Budget = mainData.ContainsKey("budget") ? Convert.ToInt32(mainData["budget"]) : 0,
                        Rating = mainData.ContainsKey("rating") ? Convert.ToSingle(mainData["rating"]) : 0f,
                        Tags = mainData.ContainsKey("tags") ? ((IEnumerable<object>)mainData["tags"]).Select(tag => tag.ToString()).ToList() : new List<string>(),
                        LikeImage = mainData.ContainsKey("image") ? mainData["image"]?.ToString() ?? "" : "",
                        LikesCount = mainData.ContainsKey("likesCount") ? Convert.ToInt32(mainData["likesCount"]) : 0,
                        IsLikedByUser = mainData.ContainsKey("isLikedByUser") && Convert.ToBoolean(mainData["isLikedByUser"])
                    };

                    dateCourses.Add(model);
                }
            }
        }
        return dateCourses;
    }

    //public async Task LoadCourseDetailAsync(DateCourseModel model)
    //{
    //    var doc = await firestore.Collection("DateCourses").Document(model.CourseId).GetSnapshotAsync();
    //    var detailData = doc.Get("detail");

    //    var courseDescription = detailData.GetString("courseDescription");
    //    var steps = detailData.GetList("detailedSteps").Select(step => new DetailStep(
    //        step.GetString("stepTitle"),
    //        step.GetString("description"),
    //        step.GetInt32("estimatedTime"),
    //        step.GetString("image")
    //    )).ToList();

    //    model.SetDetail(courseDescription, steps);
    //}



    public async Task UpdateLikeStateAsync(string courseId, bool isLiked)
    {
        var userEmail = FirebaseAuth.DefaultInstance.CurrentUser?.Email;
        if (string.IsNullOrEmpty(userEmail)) return;

        var userDoc = firestore.Collection("UserLikes").Document(userEmail);
        var snapshot = await userDoc.GetSnapshotAsync();

        if (snapshot.Exists && snapshot.TryGetValue("likedCourses", out Dictionary<string, object> likedCourses))
        {
            likedCourses[courseId] = isLiked;
            await userDoc.UpdateAsync(new Dictionary<string, object> { { "likedCourses", likedCourses } });
        }
        else
        {
            await userDoc.SetAsync(new Dictionary<string, object>
            {
                { "likedCourses", new Dictionary<string, object> { { courseId, isLiked } } }
            });
        }

    }
    /// <summary>
    /// 특정 카테고리의 데이트 코스 데이터를 가져옵니다.
    /// </summary>
    //public async Task<List<DateCourseModel>> GetCoursesByCategoryAsync(CategoryType category)
    //{
    //    var courses = new List<DateCourseModel>();
    //    var snapshot = await _databaseReference.Child("items").GetValueAsync();

    //    foreach (var itemData in snapshot.Children)
    //    {
    //        var course = ParseDateCourse(itemData);
    //        if (course != null)
    //        {
    //            // 필터링 조건: 카테고리가 일치하는 경우에만 추가
    //            if (category == CategoryType.All || course.Tag.Equals(category.ToString()))
    //            {
    //                courses.Add(course);
    //            }
    //        }
    //    }

    //    return courses;
    //}

    /// <summary>
    /// 사용자가 좋아요를 표시한 데이트 코스를 가져옵니다.
    /// </summary>
    public async Task<List<DateCourseModel>> GetFavoriteCoursesAsync(string userId)
    {
        var favoriteCourses = new List<DateCourseModel>();
        var snapshot = await _databaseReference.Child("favorites").Child(userId).GetValueAsync();

        foreach (var child in snapshot.Children)
        {
            var courseId = child.Key;
            var courseSnapshot = await _databaseReference.Child("items").Child(courseId).GetValueAsync();

            if (courseSnapshot.Exists)
            {
                var course = ParseDateCourse(courseSnapshot);
                if (course != null)
                {
                    favoriteCourses.Add(course);
                }
            }
        }

        return favoriteCourses;
    }

    /// <summary>
    /// 사용자의 좋아요 상태를 토글합니다.
    /// </summary>
    public async Task ToggleFavoriteAsync(string userId, string courseId)
    {
        var favoriteRef = _databaseReference.Child("favorites").Child(userId).Child(courseId);
        var snapshot = await favoriteRef.GetValueAsync();

        if (snapshot.Exists)
        {
            await favoriteRef.RemoveValueAsync(); // 좋아요 해제
        }
        else
        {
            await favoriteRef.SetValueAsync(true); // 좋아요 설정
        }
    }

    /// <summary>
    /// Firebase Snapshot에서 DateCourse 객체를 생성합니다.
    /// </summary>
    private DateCourseModel ParseDateCourse(DataSnapshot snapshot)
    {
        try
        {
            var title = snapshot.Child("title").Value?.ToString() ?? "Unknown";
            var description = snapshot.Child("description").Value?.ToString() ?? "";
            var image = snapshot.Child("image").Value?.ToString() ?? "";
            var duration = int.TryParse(snapshot.Child("duration").Value?.ToString(), out var parsedDuration) ? parsedDuration : 0;
            var budget = int.TryParse(snapshot.Child("budget").Value?.ToString(), out var parsedBudget) ? parsedBudget : 0;
            var rating = float.TryParse(snapshot.Child("rating").Value?.ToString(), out var parsedRating) ? parsedRating : 0f;

            // "tag" 필드가 string 배열 형태로 저장되어 있는 경우
            var tagSnapshot = snapshot.Child("tag");
            var tags = new List<string>();
            foreach (var child in tagSnapshot.Children)
            {
                if (child.Value is string tagValue)
                {
                    tags.Add(tagValue);
                }
            }

            var likesCount = int.TryParse(snapshot.Child("likesCount").Value?.ToString(), out var parsedLikesCount) ? parsedLikesCount : 0;
            var isLikedByUser = bool.TryParse(snapshot.Child("isLikedByUser").Value?.ToString(), out var parsedIsLikedByUser) ? parsedIsLikedByUser : false;

            return new DateCourseModel(title, description, duration, budget, rating, tags, image, likesCount, isLikedByUser);
        }
        catch (System.Exception ex)
        {
            Debug.LogError($"ParseDateCourse 예외 발생: {ex.Message}");
            return null; // 예외 발생 시 null 반환
        }
    }

}
