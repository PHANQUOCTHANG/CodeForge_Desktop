using System;
using System.Collections.Generic;
using CodeForge_Desktop.Business.DTOs.Admin;

namespace CodeForge_Desktop.Business.Validators
{
    /// <summary>
    /// Validation rules for course creation and updates.
    /// </summary>
    public static class CourseValidator
    {
        public class ValidationResult
        {
            public bool IsValid { get; set; }
            public List<string> Errors { get; } = new List<string>();

            public ValidationResult(bool isValid = true)
            {
                IsValid = isValid;
            }

            public void AddError(string message)
            {
                IsValid = false;
                Errors.Add(message);
            }
        }

        public static ValidationResult ValidateCreateCourse(CreateCourseDto course)
        {
            var result = new ValidationResult();

            if (string.IsNullOrWhiteSpace(course.Title))
                result.AddError("Course title is required.");

            if (course.Title?.Length > 255)
                result.AddError("Course title cannot exceed 255 characters.");

            if (course.CategoryId == Guid.Empty)
                result.AddError("Course category is required.");

            if (course.Price < 0)
                result.AddError("Price cannot be negative.");

            if (course.Discount < 0 || course.Discount > 100)
                result.AddError("Discount must be between 0 and 100.");

            if (course.Modules.Count == 0)
                result.AddError("Course must have at least one module.");

            foreach (var mod in course.Modules)
            {
                if (string.IsNullOrWhiteSpace(mod.Title))
                    result.AddError("Module title is required.");

                if (mod.Lessons.Count == 0)
                    result.AddError($"Module '{mod.Title}' must have at least one lesson.");

                foreach (var les in mod.Lessons)
                {
                    if (string.IsNullOrWhiteSpace(les.Title))
                        result.AddError("Lesson title is required.");

                    if (!IsValidLessonType(les.LessonType))
                        result.AddError($"Invalid lesson type: {les.LessonType}");

                    ValidateContent(les, result);
                }
            }

            return result;
        }

        private static bool IsValidLessonType(string type)
        {
            return type == "text" || type == "video" || type == "quiz" || type == "coding";
        }

        private static void ValidateContent(CreateLessonDto lesson, ValidationResult result)
        {
            if (lesson.Content == null)
            {
                result.AddError($"Lesson '{lesson.Title}' must have content.");
                return;
            }

            switch (lesson.LessonType.ToLowerInvariant())
            {
                case "video":
                    if (string.IsNullOrWhiteSpace(lesson.Content.VideoUrl))
                        result.AddError($"Video lesson '{lesson.Title}' must have a URL.");
                    break;

                case "text":
                    if (string.IsNullOrWhiteSpace(lesson.Content.TextContent))
                        result.AddError($"Text lesson '{lesson.Title}' must have content.");
                    break;

                case "quiz":
                    if (lesson.Content.Quiz?.Questions.Count == 0)
                        result.AddError($"Quiz lesson '{lesson.Title}' must have at least one question.");
                    break;

                case "coding":
                    if (string.IsNullOrWhiteSpace(lesson.Content.CodingProblem?.Title))
                        result.AddError($"Coding lesson '{lesson.Title}' must have a problem title.");
                    break;
            }
        }

        public static ValidationResult ValidateUpdateCourse(UpdateCourseDto course)
        {
            var result = new ValidationResult();

            if (course.CourseId == Guid.Empty)
                result.AddError("Course ID is required for update.");

            var createDto = new CreateCourseDto
            {
                Title = course.Title,
                Description = course.Description,
                Overview = course.Overview,
                CategoryId = course.CategoryId,
                Price = course.Price,
                Discount = course.Discount,
                Level = course.Level,
                Language = course.Language,
                Status = course.Status
            };

            // Validate course info (but not module requirement for updates)
            var courseValidation = ValidateCreateCourse(createDto);
            result.Errors.AddRange(courseValidation.Errors);
            result.IsValid = courseValidation.IsValid && result.IsValid;

            return result;
        }
    }
}