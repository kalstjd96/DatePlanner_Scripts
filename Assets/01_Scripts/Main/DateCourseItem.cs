using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Firebase.Database;
using Firebase.Auth;

public class DateCourseItem : MonoBehaviour
{
    public TextMeshProUGUI Title;
    public TextMeshProUGUI Description;
    public Image[] TitleImageArray;
    public Button LikeButton;
    public Image LikeImage;
    public Button DetailButton;

    private bool isLiked = false; // 현재 아이템이 즐겨찾기 상태인지 여부
    private string userId; // 현재 로그인한 사용자 ID
    private string courseId; // 이 아이템에 해당하는 데이트 코스 ID

    private void Start()
    {
        userId = FirebaseAuth.DefaultInstance.CurrentUser?.UserId;
        LikeButton.onClick.AddListener(OnLikeButtonClicked);
    }

    public void Initialize(string courseId, bool liked)
    {
        this.courseId = courseId;
        this.isLiked = liked;

        // 좋아요 상태를 업데이트
        UpdateLikeUI();
    }

    private void OnLikeButtonClicked()
    {
        if (string.IsNullOrEmpty(userId))
        {
            Debug.LogError("로그인된 사용자가 없습니다.");
            return;
        }

        // 좋아요 상태를 토글
        isLiked = !isLiked;

        // Firebase에 즐겨찾기 정보 저장/삭제
        if (isLiked)
        {
            AddToFavorites();
        }
        else
        {
            RemoveFromFavorites();
        }

        // UI 업데이트
        UpdateLikeUI();
    }

    private void UpdateLikeUI()
    {
        LikeImage.color = isLiked ? Color.red : Color.gray; // 좋아요 상태에 따라 색상 변경
    }

    private void AddToFavorites()
    {
        DatabaseReference databaseRef = FirebaseDatabase.DefaultInstance.GetReference("favorites").Child(userId);
        databaseRef.Child(courseId).SetValueAsync(true).ContinueWith(task =>
        {
            if (task.IsCompleted)
            {
                Debug.Log($"데이트 코스 {courseId}가 즐겨찾기에 추가되었습니다.");
            }
            else
            {
                Debug.LogError("즐겨찾기 추가 중 오류 발생: " + task.Exception);
            }
        });
    }

    private void RemoveFromFavorites()
    {
        DatabaseReference databaseRef = FirebaseDatabase.DefaultInstance.GetReference("favorites").Child(userId);
        databaseRef.Child(courseId).RemoveValueAsync().ContinueWith(task =>
        {
            if (task.IsCompleted)
            {
                Debug.Log($"데이트 코스 {courseId}가 즐겨찾기에서 제거되었습니다.");
            }
            else
            {
                Debug.LogError("즐겨찾기 제거 중 오류 발생: " + task.Exception);
            }
        });
    }
}
