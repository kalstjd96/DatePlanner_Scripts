
using System.Collections.Generic;
using System.Threading.Tasks;
using static DateCourseModel;

namespace interfaces
{
    public interface IDateCourseRepository
    {
        Task<List<DateCourseModel>> GetAllDateCoursesAsync();
        Task UpdateLikeStateAsync(string title, bool isLikedByUser);
        //Task<List<DateCourseModel>> GetCoursesByCategoryAsync(CategoryType category);
        Task<List<DateCourseModel>> GetFavoriteCoursesAsync(string userId);
        Task ToggleFavoriteAsync(string userId, string courseId);
    }

}
