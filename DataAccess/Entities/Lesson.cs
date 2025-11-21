using System;

namespace CodeForge_Desktop.DataAccess.Entities
{
    public class Lesson
    {
        public Guid LessonID { get; set; } = Guid.NewGuid();
        public Guid ModuleID { get; set; }
        public string Title { get; set; }
        public string LessonType { get; set; } // video | text | quiz | coding
        public int OrderIndex { get; set; }
        public int Duration { get; set; } // seconds
        public bool IsDeleted { get; set; } = false;

        // derived property (not stored in DB) — indicates whether the current user completed the lesson
        public bool IsCompleted { get; set; } = false;
    }
}