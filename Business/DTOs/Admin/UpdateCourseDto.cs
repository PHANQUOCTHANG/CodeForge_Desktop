using System;
using System.Collections.Generic;

namespace CodeForge_Desktop.Business.DTOs.Admin
{
    /// <summary>
    /// DTO for updating an existing course (client → server)
    /// Used for PUT /api/admin/courses/{courseId}
    /// Supports partial updates and nested resource management
    /// </summary>
    public class UpdateCourseDto
    {
        public Guid CourseId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string? Overview { get; set; }
        public string Level { get; set; } = string.Empty;
        public string Language { get; set; } = string.Empty;
        public Guid CategoryId { get; set; }
        public decimal Price { get; set; }
        public decimal Discount { get; set; }
        public string? ThumbnailBase64 { get; set; }
        public string Status { get; set; } = string.Empty;

        /// <summary>
        /// All modules (including IsDeleted ones) for diff-based update
        /// </summary>
        public List<UpdateModuleDto> Modules { get; set; } = new List<UpdateModuleDto>();

        public UpdateCourseDto()
        {
        }
    }

    /// <summary>
    /// DTO for updating modules
    /// ModuleId = null means new module
    /// IsDeleted = true means soft delete
    /// </summary>
    public class UpdateModuleDto
    {
        public Guid? ModuleId { get; set; }
        public string Title { get; set; } = string.Empty;
        public int OrderIndex { get; set; }
        public bool IsDeleted { get; set; } = false;

        public List<UpdateLessonDto> Lessons { get; set; } = new List<UpdateLessonDto>();

        public UpdateModuleDto()
        {
        }
    }

    /// <summary>
    /// DTO for updating lessons
    /// LessonId = null means new lesson
    /// IsDeleted = true means soft delete
    /// </summary>
    public class UpdateLessonDto
    {
        public Guid? LessonId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string LessonType { get; set; } = "text";
        public int Duration { get; set; }
        public int OrderIndex { get; set; }
        public bool IsDeleted { get; set; } = false;

        /// <summary>
        /// Content for this lesson
        /// </summary>
        public LessonContentDto? Content { get; set; }

        public UpdateLessonDto()
        {
        }
    }

    /// <summary>
    /// DTO for updating quiz with diff-based question management
    /// </summary>
    public class UpdateQuizDto
    {
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        
        /// <summary>
        /// Questions: 
        /// - QuestionId = null → INSERT
        /// - QuestionId != null → UPDATE
        /// - IsDeleted = true → DELETE
        /// </summary>
        public List<UpdateQuizQuestionDto> Questions { get; set; } = new List<UpdateQuizQuestionDto>();

        public UpdateQuizDto()
        {
        }
    }

    /// <summary>
    /// DTO for each quiz question in update flow
    /// </summary>
    public class UpdateQuizQuestionDto
    {
        public Guid? QuestionId { get; set; }
        public string Question { get; set; } = string.Empty;
        public string[] Answers { get; set; } = Array.Empty<string>();
        public int CorrectIndex { get; set; }
        public string? Explanation { get; set; }
        public bool IsDeleted { get; set; } = false;

        public UpdateQuizQuestionDto()
        {
        }
    }
}