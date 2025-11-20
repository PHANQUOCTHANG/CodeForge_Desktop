using System;

namespace CodeForge_Desktop.DataAccess.Entities
{
    public class CodingProblemLesson
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid LessonID { get; set; }
        public Guid ProblemID { get; set; }
        public int OrderIndex { get; set; } = 0;
        public bool IsDeleted { get; set; } = false;
    }
}