using System;
using System.Collections.Generic;

public class DateCourse
{
    public string Title { get; private set; }
    public string Description { get; private set; }
    public string Images { get; private set; }
    public int Duration { get; private set; }
    public int Budget { get; private set; }
    public string Preview { get; private set; }
    public float Rating { get; private set; }
    public string Tag { get; private set; }
    public bool Like { get; private set; }

    public DateCourse(string title, string description, string images, int duration, int budget, string preview, float rating, string tag, bool like)
    {
        Title = title;
        Description = description;
        Images = images;
        Duration = duration;
        Budget = budget;
        Preview = preview;
        Rating = rating;
        Tag = tag;
        Like = like;
    }
}
