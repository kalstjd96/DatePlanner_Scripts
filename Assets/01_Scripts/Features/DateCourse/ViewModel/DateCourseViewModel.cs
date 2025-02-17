using interfaces;
using System;
using System.Collections.Generic;

/// <summary>
/// 화면(View)에서 사용하는 데이터를 정의하고 관리
/// 용도:
// UI에 필요한 데이터를 가공하거나 변환하여 제공.
// 화면과 상호작용하기 위한 로직(예: 좋아요 버튼 클릭 이벤트 처리)을 포함.
///특징:
// UI 중심으로 설계되며, 화면에서 필요한 데이터만 포함하거나, Model 데이터를 가공하여 저장.
// 화면(View) 와 데이터(Model) 를 연결하는 중간 역할.
/// </summary>
public class DateCourseViewModel
{
    public string Title { get; private set; }
    public string Description { get; private set; }
    public int Duration { get; private set; }
    public int Budget { get; private set; }
    public float Rating { get; private set; }
    public List<string> Tags { get; private set; }
    public string LikeImage { get; private set; }
    public int LikesCount { get; private set; }
    public bool IsLikedByUser { get; private set; }

    public Action ToggleLikeCommand { get; private set; }
    public event Action OnLikeStateChanged;

    private readonly IDateCourseRepository dataRepository;

    public DateCourseViewModel(string title, string description, int duration, int budget, 
        float rating, List<string> tags, string likeimage, int likesCount, bool isLikedByUser, IDateCourseRepository repository)
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
        dataRepository = repository;

        // 좋아요 상태를 토글하는 커맨드 정의
        ToggleLikeCommand = async () =>
        {
            IsLikedByUser = !IsLikedByUser;
            LikesCount += IsLikedByUser ? 1 : -1;

            // Firebase 업데이트
            await dataRepository.UpdateLikeStateAsync(title, IsLikedByUser);

            // 변경 알림
            OnLikeStateChanged?.Invoke();
        };
    }
}
