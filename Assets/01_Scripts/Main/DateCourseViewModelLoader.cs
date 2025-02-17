using interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class DateCourseViewModelLoader : MonoBehaviour
{
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
}
