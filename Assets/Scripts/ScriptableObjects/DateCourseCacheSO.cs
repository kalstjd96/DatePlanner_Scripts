using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "DateCourseCache", menuName = "DatePlanner/DateCourseCache")]
public class DateCourseCacheSO : ScriptableObject
{
    public List<DateCourseModel> cachedCourses = new List<DateCourseModel>();
}
