using System;

namespace CodeForge_Desktop.DataAccess.Entities
{
    public class LessonVideo
    {
        public Guid LessonID { get; set; }
        public string VideoUrl { get; set; }
        public string Caption { get; set; }
        public int Duration { get; set; } // seconds
    }
}