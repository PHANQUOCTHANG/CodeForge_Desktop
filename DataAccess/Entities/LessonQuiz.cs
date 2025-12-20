using System;

namespace CodeForge_Desktop.DataAccess.Entities
{
    public class LessonQuiz
    {
        public Guid LessonID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }
}