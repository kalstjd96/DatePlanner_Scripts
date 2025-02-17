using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DateCourseItem : MonoBehaviour
{
    [NonSerialized]
    public string CourseId;
    public TextMeshProUGUI Title;
    public TextMeshProUGUI Description;
    public TextMeshProUGUI Duration;
    public TextMeshProUGUI Budget;
    public TextMeshProUGUI Rating;
    public TextMeshProUGUI Tags;
    public Image LikeImage;
    public TextMeshProUGUI LikesCount;
    public Button LikeButton;
    public bool IsLikedByUser;

    private DateCourseViewModel viewModel;

    public void Initialize(DateCourseViewModel viewModel)
    {
        this.viewModel = viewModel;

        // ViewModel에서 UI 초기화
        UpdateUI();

        // 좋아요 버튼 클릭 이벤트 연결
        LikeButton.onClick.AddListener(() =>
        {
            viewModel.ToggleLikeCommand?.Invoke();
            UpdateUI(); // UI 갱신
        });

        // ViewModel 변경 감지 연결 (옵저버 패턴 활용 가능)
        viewModel.OnLikeStateChanged += UpdateUI;
    }

    private void UpdateUI()
    {
        if (viewModel == null) return;

        Title.text = viewModel.Title;
        Description.text = viewModel.Description;
        LikesCount.text = viewModel.LikesCount.ToString();
        LikeImage.color = viewModel.IsLikedByUser ? Color.red : Color.gray;
    }

    private void OnDestroy()
    {
        if (viewModel != null)
            viewModel.OnLikeStateChanged -= UpdateUI;
    }
}
