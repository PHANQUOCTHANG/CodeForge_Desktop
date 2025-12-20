using System;

namespace CodeForge_Desktop.DataAccess.Entities
{
    public class QuizQuestion
    {
        public Guid QuestionID { get; set; } = Guid.NewGuid();
        public Guid LessonQuizId { get; set; } // maps to LessonID of LessonQuizzes
        public string Question { get; set; }
        public string Answers { get; set; } // JSON array string
        public int CorrectIndex { get; set; }
        public string Explanation { get; set; }
    }
}