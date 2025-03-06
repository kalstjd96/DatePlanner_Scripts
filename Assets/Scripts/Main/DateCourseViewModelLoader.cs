using Firebase.Firestore;
using interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class DateCourseViewModelLoader : MonoBehaviour
{
    public DateCourseCacheSO courseCache;

    public Transform ContentParent; // 아이템 생성 위치
    public GameObject DateCourseItemPrefab; // 프리팹
    private IDateCourseRepository dataRepository;

    private void Start()
    {
        dataRepository = new FirebaseDateCourseRepository();
        LoadDateCourses();
    }

    private async void LoadDateCourses()
    {
        var dateCourses = await dataRepository.GetAllDateCoursesAsync();

        courseCache.cachedCourses = dateCourses;

        UpdateUI(dateCourses);
    }
    private void UpdateUI(List<DateCourseModel> dateCourses)
    {
        // 기존 아이템 제거 (중복 생성 방지)
        foreach (Transform child in ContentParent)
        {
            Destroy(child.gameObject);
        }

        // 새로운 아이템 생성
        foreach (var course in dateCourses)
        {
            CreateDateCourseItem(course);
        }
    }

    private void CreateDateCourseItem(DateCourseModel model)
    {
        var itemObject = Instantiate(DateCourseItemPrefab, ContentParent);
        var item = itemObject.GetComponent<DateCourseItem>();

        var viewModel = new DateCourseViewModel(
            model.Title,
            model.Description,
            model.Duration,
            model.Budget,
            model.Rating,
            model.Tags,
            model.LikeImage,
            model.LikesCount,
            model.IsLikedByUser,
            dataRepository
        );

        item.Initialize(viewModel);
    }

    public async void RefreshDateCourses()
    {
        // Firestore에서 최신 데이터 가져오기
        var updatedCourses = await dataRepository.GetAllDateCoursesAsync();

        // 캐시 업데이트
        courseCache.cachedCourses = updatedCourses;

        // UI 갱신
        UpdateUI(updatedCourses);
    }

}
