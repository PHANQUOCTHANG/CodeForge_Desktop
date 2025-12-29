using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeForge_Desktop.Business.DTOs
{
   
        public class LessonDto
        {
            public Guid LessonID { get; set; }
            public Guid ModuleID { get; set; }
            public string Title { get; set; } = string.Empty;
            public bool IsDeleted { get; set; } = false;

            /// <summary>
            /// Type of lesson: "video", "text", "quiz", "coding"
            /// </summary>
            public string LessonType { get; set; } = string.Empty;
            public bool IsCompleted { get; set; } = false; // Completion status
            public int OrderIndex { get; set; }
            public int Duration { get; set; } // Duration in seconds or minutes

            // Detailed content of the lesson (only one of these will have data)
            public LessonVideoDto? VideoContent { get; set; }
            public LessonTextDto? TextContent { get; set; }
            // Updated Quiz DTO to contain Questions
            public LessonQuizDto? QuizContent { get; set; }
            public ProblemDto? CodingProblem { get; set; }
        }

        // ===================================
        // CHILD DTOs (Detailed Content)
        // ===================================

        public class LessonVideoDto
        {
            public string VideoUrl { get; set; } = string.Empty;
            public string? Caption { get; set; }
        }

        public class LessonTextDto
        {
            public string Content { get; set; } = string.Empty;
        }

        /// <summary>
        /// Quiz DTO: Contains questions
        /// </summary>
        public class LessonQuizDto
        {
            public string Title { get; set; } = string.Empty;
            public string? Description { get; set; }

            // ADDED: List of associated questions
            public ICollection<QuizQuestionDto> Questions { get; set; } = new List<QuizQuestionDto>();
        }

        /// <summary>
        /// DTO for each question in the Quiz
        /// </summary>
        public class QuizQuestionDto
        {
            public Guid QuestionID { get; set; }
            public string Question { get; set; } = string.Empty;

            // Use string[] to represent answer choices
            public string[] Answers { get; set; } = Array.Empty<string>();
            public string Explanation { get; set; } = string.Empty;
            // Only send CorrectIndex if this DTO is for Admin (or grading purposes)
            public int CorrectIndex { get; set; }
        }
    
}
