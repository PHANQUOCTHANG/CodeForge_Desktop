using System;
using System.Collections.Generic;

namespace CodeForge_Desktop.Business.DTOs.Admin
{
    /// <summary>
    /// DTO for creating a new course (client → server)
    /// Used for POST /api/admin/courses
    /// </summary>
    public class CreateCourseDto
    {
        public Guid? CourseId { get; set; } // optional for edit-mode mapping
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string? Overview { get; set; }
        public string Level { get; set; } = "beginner";
        public string Language { get; set; } = "C#";
        public Guid CategoryId { get; set; }
        public decimal Price { get; set; } = 0m;
        public decimal Discount { get; set; } = 0m;
        public string? ThumbnailBase64 { get; set; }
        public string Status { get; set; } = "draft";

        /// <summary>
        /// List of modules to create with the course.
        /// Each module can contain lessons.
        /// </summary>
        public List<CreateModuleDto> Modules { get; set; } = new List<CreateModuleDto>();

        public CreateCourseDto()
        {
        }
    }

    /// <summary>
    /// DTO for creating modules during course creation
    /// </summary>
    public class CreateModuleDto
    {
        public Guid? ModuleId { get; set; } // added for edit mapping
        public string Title { get; set; } = string.Empty;
        public int OrderIndex { get; set; }

        /// <summary>
        /// Lessons within this module
        /// </summary>
        public List<CreateLessonDto> Lessons { get; set; } = new List<CreateLessonDto>();

        public CreateModuleDto()
        {
        }
    }

    /// <summary>
    /// DTO for creating lessons during course creation
    /// </summary>
    public class CreateLessonDto
    {
        public Guid? LessonId { get; set; } // added for edit mapping
        public string Title { get; set; } = string.Empty;
        public string LessonType { get; set; } = "text"; // video, text, quiz, coding
        public int Duration { get; set; } = 0;
        public int OrderIndex { get; set; }

        /// <summary>
        /// Content payload based on LessonType.
        /// Only ONE of these will be populated.
        /// </summary>
        public LessonContentDto? Content { get; set; }

        public CreateLessonDto()
        {
        }
    }

    /// <summary>
    /// Union-like DTO for lesson content (polymorphic)
    /// </summary>
    public class LessonContentDto
    {
        public string? VideoUrl { get; set; }
        public string? VideoCaption { get; set; }

        public string? TextContent { get; set; }

        public CreateQuizDto? Quiz { get; set; }

        public CreateCodingProblemDto? CodingProblem { get; set; }

        public LessonContentDto()
        {
        }
    }

    /// <summary>
    /// DTO for creating quiz with questions
    /// </summary>
    public class CreateQuizDto
    {
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public List<CreateQuizQuestionDto> Questions { get; set; } = new List<CreateQuizQuestionDto>();

        public CreateQuizDto()
        {
        }
    }

    /// <summary>
    /// DTO for each quiz question
    /// </summary>
    public class CreateQuizQuestionDto
    {
        public string Question { get; set; } = string.Empty;
        public string[] Answers { get; set; } = Array.Empty<string>();
        public int CorrectIndex { get; set; } = 0;
        public string? Explanation { get; set; }

        public CreateQuizQuestionDto()
        {
        }
    }

    /// <summary>
    /// DTO for creating coding problems
    /// </summary>
    public class CreateCodingProblemDto
    {
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string Difficulty { get; set; } = "Easy";
        public int TimeLimit { get; set; } = 1000; // milliseconds
        public int MemoryLimit { get; set; } = 256; // MB
        public string? FunctionName { get; set; }
        public string? InitialCode { get; set; }

        public CreateCodingProblemDto()
        {
        }
    }
}