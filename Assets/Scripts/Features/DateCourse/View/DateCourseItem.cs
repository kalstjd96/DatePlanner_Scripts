using DateApp.Platform.Define;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
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

    private Button detailButton;
    private DateCourseViewModel viewModel;
    private SceneDefine.SceneName detailScene;

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

        detailButton = this.GetComponent<Button>();
        detailButton.onClick.AddListener(() =>
        {
            if (SceneDefine.SceneNames.TryGetValue(SceneDefine.SceneName.Detail, out string sceneName))
                SceneManager.LoadScene(sceneName);
            else
                Debug.LogError("지정한 씬이 없습니다.");
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

        if(detailButton != null)
        {
            detailButton.onClick.RemoveAllListeners();
        }
    }
}
