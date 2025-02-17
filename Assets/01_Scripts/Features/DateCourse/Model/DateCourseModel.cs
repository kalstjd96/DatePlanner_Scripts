using Firebase.Firestore;
using System.Collections.Generic;
using System.Threading.Tasks;

/// <summary>
/// 데이터를 표현하고 저장소와 직접 연관된 클래스를 의미
/// 용도:
// Firebase, 데이터베이스, 또는 API와의 직접적인 통신을 위해 사용.
// 데이터의 구조를 정의하고, 데이터베이스에서 가져온 값을 저장합니다.

///특징:
// Firestore와 같은 외부 서비스와 매핑되도록 설계됩니다([FirestoreProperty] 사용).
// 일반적으로 UI와 관련된 논리는 포함하지 않습니다.
/// </summary>
[FirestoreData]
public class DateCourseModel
{
    public enum CategoryType
    {
        Main,
        Favorite,
        Event,
        All
    }
    public string CourseId { get; set; }

    [FirestoreProperty(Name = "title")]
    public string Title { get; set; }

    //[FirestoreProperty]
    [FirestoreProperty(Name = "budget")]
    public int Budget { get; set; }

    [FirestoreProperty(Name = "description")]
    public string Description { get; set; }

    [FirestoreProperty(Name = "duration")]
    public int Duration { get; set; }

    [FirestoreProperty(Name = "image")]
    public string LikeImage { get; set; }

    [FirestoreProperty(Name = "isLikedByUser")]
    public bool IsLikedByUser { get; set; }

    [FirestoreProperty(Name = "likesCount")]
    public int LikesCount { get; set; }

    [FirestoreProperty(Name = "rating")]
    public float Rating { get; set; }

    [FirestoreProperty(Name = "region")]
    public int Region { get; set; }

    [FirestoreProperty(Name = "tags")]
    public List<string> Tags { get; set; }



    // Detail 필드 속성
    //[FirestoreProperty]
    //public DetailModel Detail { get; set; }

    //// Detail 필드 (나중에 추가적으로 로드)
    //public string CourseDescription { get; set; }
    //public List<DetailStep> DetailedSteps { get; set; }

    //[FirestoreProperty]
    //public string Author { get; set; }

    //[FirestoreProperty]
    //public string ReservationLink { get; set; }

    //기본 생성자 추가
    //생성자를 호출하지 않고 프로퍼티를 직접 초기화하는 편리함 때문에 사용
    public DateCourseModel(){}

    //기존 생성자 유지
    public DateCourseModel(string title, string description, int duration, int budget,
    float rating, List<string> tags, string likeimage, int likesCount, bool isLikedByUser)
    {
        Title = title;
        Description = description;
        Duration = duration;
        Budget = budget;
        Rating = rating;
        Tags = tags;
        LikeImage = likeimage;
        LikesCount = likesCount;
        IsLikedByUser = isLikedByUser;
    }

    // Detail 데이터 초기화 메서드
    //public void SetDetail(string courseDescription, List<DetailStep> detailedSteps)
    //{
    //    CourseDescription = courseDescription;
    //    DetailedSteps = detailedSteps;
    //}

    public async Task ToggleLikeAsync()
    {
        IsLikedByUser = !IsLikedByUser;
        LikesCount += IsLikedByUser ? 1 : -1;
        await Task.CompletedTask; // Replace with actual database call
    }

    // Firebase에서 좋아요 상태를 토글
    //public async Task<bool> ToggleLikeAsync()
    //{
    //    if (string.IsNullOrEmpty(userEmail)) return IsLiked;

    //    DocumentReference userDoc = firestore.Collection("UserLikes").Document(userEmail);
    //    var snapshot = await userDoc.GetSnapshotAsync();
    //    Dictionary<string, object> likedCourses;

    //    if (snapshot.Exists)
    //    {
    //        likedCourses = snapshot.GetValue<Dictionary<string, object>>("likedCourses") ?? new Dictionary<string, object>();
    //    }
    //    else
    //    {
    //        likedCourses = new Dictionary<string, object>();
    //    }

    //    bool newLikeState = !IsLiked;
    //    likedCourses[CourseId] = newLikeState;

    //    await userDoc.SetAsync(new Dictionary<string, object> { { "likedCourses", likedCourses } });
    //    IsLiked = newLikeState;
    //    return newLikeState;
    //}
}
