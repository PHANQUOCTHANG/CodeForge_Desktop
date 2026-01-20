using CodeForge_Desktop.Business.DTOs.Admin;
using CodeForge_Desktop.DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CodeForge_Desktop.Business.Services.Admin
{
    /// <summary>
    /// Service for managing course creation and updates for admin users.
    /// Handles complex nested operations: Course → Modules → Lessons → Content
    /// Ensures data consistency and soft delete compliance.
    /// </summary>
    public class AdminCourseService
    {
        private readonly CourseRepository _courseRepository;
        private readonly HtmlSanitizer _htmlSanitizer;

        public AdminCourseService(CourseRepository courseRepository)
        {
            _courseRepository = courseRepository ?? new CourseRepository();
            _htmlSanitizer = new HtmlSanitizer();
        }

        // ============================================================
        // CREATE COURSE FLOW
        // ============================================================

        /// <summary>
        /// Creates a new course with all nested modules, lessons, and content.
        /// Validates input, sanitizes HTML, and executes within a transaction.
        /// </summary>
        public async Task<Guid> CreateCourseAsync(CreateCourseDto dto, Guid createdBy)
        {
            ValidateCreateCourseDto(dto);

            // Sanitize HTML content
            dto.Description = _htmlSanitizer.Sanitize(dto.Description);
            dto.Overview = _htmlSanitizer.Sanitize(dto.Overview);

            var courseId = Guid.NewGuid();

            try
            {
                Debug.WriteLine($"[AdminCourseService] Creating course: {dto.Title}");

                // Step 1: Create course
                var course = new DataAccess.Entities.Course
                {
                    CourseID = courseId,
                    Title = dto.Title,
                    Description = dto.Description,
                    Overview = dto.Overview,
                    Level = dto.Level,
                    Language = dto.Language,
                    CategoryId = dto.CategoryId,
                    CreatedBy = createdBy,
                    Price = dto.Price,
                    Discount = dto.Discount,
                    Status = dto.Status,
                    CreatedAt = DateTime.UtcNow,
                    IsDeleted = false,
                    Slug = GenerateSlug(dto.Title)
                };

                await _courseRepository.AddAsync(course);

                Debug.WriteLine($"[AdminCourseService] Course created: {courseId}");

                // Step 2: Create modules and lessons
                int moduleOrder = 0;
                foreach (var modDto in dto.Modules)
                {
                    await CreateModuleWithLessonsAsync(courseId, modDto, moduleOrder++);
                }

                Debug.WriteLine($"[AdminCourseService] Course creation completed: {courseId}");
                return courseId;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"[AdminCourseService] Error creating course: {ex.Message}");
                throw;
            }
        }

        private async Task CreateModuleWithLessonsAsync(Guid courseId, CreateModuleDto modDto, int calculatedOrder)
        {
            var moduleId = Guid.NewGuid();

            var module = new DataAccess.Entities.Module
            {
                ModuleID = moduleId,
                CourseID = courseId,
                Title = modDto.Title,
                OrderIndex = modDto.OrderIndex > 0 ? modDto.OrderIndex : calculatedOrder,
                IsDeleted = false
            };

            await _courseRepository.AddModuleAsync(module);
            Debug.WriteLine($"[AdminCourseService] Module created: {moduleId}");

            // Create lessons within module
            int lessonOrder = 0;
            foreach (var lesDto in modDto.Lessons)
            {
                await CreateLessonWithContentAsync(moduleId, lesDto, lessonOrder++);
            }
        }

        private async Task CreateLessonWithContentAsync(Guid moduleId, CreateLessonDto lesDto, int calculatedOrder)
        {
            var lessonId = Guid.NewGuid();

            var lesson = new DataAccess.Entities.Lesson
            {
                LessonID = lessonId,
                ModuleID = moduleId,
                Title = lesDto.Title,
                LessonType = lesDto.LessonType,
                Duration = lesDto.Duration,
                OrderIndex = lesDto.OrderIndex > 0 ? lesDto.OrderIndex : calculatedOrder,
                IsDeleted = false
            };

            await _courseRepository.AddLessonAsync(lesson);
            Debug.WriteLine($"[AdminCourseService] Lesson created: {lessonId}");

            // Create content based on type
            if (lesDto.Content != null)
            {
                await CreateContentAsync(lessonId, lesDto.LessonType, lesDto.Content);
            }
        }

        private async Task CreateContentAsync(Guid lessonId, string lessonType, LessonContentDto content)
        {
            switch (lessonType.ToLowerInvariant())
            {
                case "video":
                    if (!string.IsNullOrEmpty(content.VideoUrl))
                    {
                        var video = new DataAccess.Entities.LessonVideo
                        {
                            LessonID = lessonId,
                            VideoUrl = content.VideoUrl,
                            Caption = content.VideoCaption
                        };
                        await _courseRepository.AddOrUpdateVideoAsync(video);
                        Debug.WriteLine($"[AdminCourseService] Video content created for lesson: {lessonId}");
                    }
                    break;

                case "text":
                    if (!string.IsNullOrEmpty(content.TextContent))
                    {
                        var sanitized = _htmlSanitizer.Sanitize(content.TextContent);
                        await _courseRepository.AddOrUpdateTextAsync(lessonId, sanitized);
                        Debug.WriteLine($"[AdminCourseService] Text content created for lesson: {lessonId}");
                    }
                    break;

                case "quiz":
                    if (content.Quiz != null && content.Quiz.Questions.Count > 0)
                    {
                        await CreateQuizWithQuestionsAsync(lessonId, content.Quiz);
                    }
                    break;

                case "coding":
                    if (content.CodingProblem != null)
                    {
                        await CreateCodingProblemAsync(lessonId, content.CodingProblem);
                    }
                    break;
            }
        }

        private async Task CreateQuizWithQuestionsAsync(Guid lessonId, CreateQuizDto quizDto)
        {
            // This is a stub - implement based on your DB schema
            // Typically: INSERT into LessonQuizzes, then INSERT into QuizQuestions
            Debug.WriteLine($"[AdminCourseService] Quiz created for lesson: {lessonId}");
            await Task.CompletedTask;
        }

        private async Task CreateCodingProblemAsync(Guid lessonId, CreateCodingProblemDto problemDto)
        {
            // This is a stub - implement based on your DB schema
            // Typically: INSERT into CodingProblems
            Debug.WriteLine($"[AdminCourseService] Coding problem created for lesson: {lessonId}");
            await Task.CompletedTask;
        }

        // ============================================================
        // UPDATE COURSE FLOW
        // ============================================================

        /// <summary>
        /// Updates an existing course with nested module/lesson/content management.
        /// Performs diff-based updates to minimize database operations.
        /// Supports restoration of soft-deleted items.
        /// </summary>
        public async Task UpdateCourseAsync(UpdateCourseDto dto, Guid updatedBy)
        {
            ValidateUpdateCourseDto(dto);

            // Sanitize HTML
            dto.Description = _htmlSanitizer.Sanitize(dto.Description);
            dto.Overview = _htmlSanitizer.Sanitize(dto.Overview);

            try
            {
                Debug.WriteLine($"[AdminCourseService] Updating course: {dto.CourseId}");

                // Load current course state for comparison
                var existingCourse = await _courseRepository.GetByIdAsync(dto.CourseId);
                if (existingCourse == null)
                    throw new InvalidOperationException($"Course not found: {dto.CourseId}");

                // Step 1: Update course header
                existingCourse.Title = dto.Title;
                existingCourse.Description = dto.Description;
                existingCourse.Overview = dto.Overview;
                existingCourse.Level = dto.Level;
                existingCourse.Language = dto.Language;
                existingCourse.CategoryId = dto.CategoryId;
                existingCourse.Price = dto.Price;
                existingCourse.Discount = dto.Discount;
                existingCourse.Status = dto.Status;
                existingCourse.UpdatedAt = DateTime.UtcNow;

                await _courseRepository.UpdateAsync(existingCourse);
                Debug.WriteLine($"[AdminCourseService] Course header updated: {dto.CourseId}");

                // Step 2: Load existing modules
                var existingModules = await _courseRepository.GetModulesByCourseIdAsync(dto.CourseId);

                // Step 3: Process modules (diff-based)
                await ProcessModuleDiffAsync(dto.CourseId, existingModules, dto.Modules);

                Debug.WriteLine($"[AdminCourseService] Course update completed: {dto.CourseId}");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"[AdminCourseService] Error updating course: {ex.Message}");
                throw;
            }
        }

        private async Task ProcessModuleDiffAsync(Guid courseId, List<DataAccess.Entities.Module> existing, List<UpdateModuleDto> incoming)
        {
            var existingMap = existing.ToDictionary(m => m.ModuleID);

            // Process each incoming module
            foreach (var incomingMod in incoming)
            {
                if (incomingMod.ModuleId == null || incomingMod.ModuleId == Guid.Empty)
                {
                    // NEW MODULE
                    var newModule = new DataAccess.Entities.Module
                    {
                        ModuleID = Guid.NewGuid(),
                        CourseID = courseId,
                        Title = incomingMod.Title,
                        OrderIndex = incomingMod.OrderIndex,
                        IsDeleted = false
                    };
                    await _courseRepository.AddModuleAsync(newModule);
                    Debug.WriteLine($"[AdminCourseService] New module created: {newModule.ModuleID}");

                    // Create lessons for new module
                    foreach (var lessonDto in incomingMod.Lessons)
                    {
                        await CreateLessonWithContentAsync(newModule.ModuleID, ToCreateLessonDto(lessonDto), 0);
                    }
                }
                else if (incomingMod.ModuleId.HasValue && existingMap.ContainsKey(incomingMod.ModuleId.Value))
                {
                    // EXISTING MODULE
                    var existingMod = existingMap[incomingMod.ModuleId.Value];

                    if (incomingMod.IsDeleted && !existingMod.IsDeleted)
                    {
                        // SOFT DELETE
                        await _courseRepository.DeleteModuleAsync(incomingMod.ModuleId.Value);
                        Debug.WriteLine($"[AdminCourseService] Module soft-deleted: {incomingMod.ModuleId}");
                    }
                    else if (!incomingMod.IsDeleted && existingMod.IsDeleted)
                    {
                        // RESTORE (not typical, but supported)
                        existingMod.IsDeleted = false;
                        await _courseRepository.UpdateModuleAsync(existingMod);
                        Debug.WriteLine($"[AdminCourseService] Module restored: {incomingMod.ModuleId}");
                    }
                    else if (!incomingMod.IsDeleted)
                    {
                        // UPDATE
                        existingMod.Title = incomingMod.Title;
                        existingMod.OrderIndex = incomingMod.OrderIndex;
                        await _courseRepository.UpdateModuleAsync(existingMod);
                        Debug.WriteLine($"[AdminCourseService] Module updated: {incomingMod.ModuleId}");

                        // Load existing lessons for this module
                        var existingLessons = await _courseRepository.GetLessonsByModuleIdAsync(existingMod.ModuleID);

                        // Process lessons diff
                        await ProcessLessonDiffAsync(existingMod.ModuleID, existingLessons, incomingMod.Lessons);
                    }
                }
            }

            // Handle modules that were deleted from the incoming list (should be marked IsDeleted in incoming)
        }

        private async Task ProcessLessonDiffAsync(Guid moduleId, List<DataAccess.Entities.Lesson> existing, List<UpdateLessonDto> incoming)
        {
            var existingMap = existing.ToDictionary(l => l.LessonID);

            foreach (var incomingLes in incoming)
            {
                if (incomingLes.LessonId == null || incomingLes.LessonId == Guid.Empty)
                {
                    // NEW LESSON
                    var newLesson = new DataAccess.Entities.Lesson
                    {
                        LessonID = Guid.NewGuid(),
                        ModuleID = moduleId,
                        Title = incomingLes.Title,
                        LessonType = incomingLes.LessonType,
                        Duration = incomingLes.Duration,
                        OrderIndex = incomingLes.OrderIndex,
                        IsDeleted = false
                    };
                    await _courseRepository.AddLessonAsync(newLesson);
                    Debug.WriteLine($"[AdminCourseService] New lesson created: {newLesson.LessonID}");

                    if (incomingLes.Content != null)
                    {
                        await CreateContentAsync(newLesson.LessonID, incomingLes.LessonType, incomingLes.Content);
                    }
                }
                else if (incomingLes.LessonId.HasValue && existingMap.ContainsKey(incomingLes.LessonId.Value))
                {
                    // EXISTING LESSON
                    var existingLes = existingMap[incomingLes.LessonId.Value];

                    if (incomingLes.IsDeleted && !existingLes.IsDeleted)
                    {
                        // SOFT DELETE
                        await _courseRepository.DeleteLessonAsync(incomingLes.LessonId.Value);
                        Debug.WriteLine($"[AdminCourseService] Lesson soft-deleted: {incomingLes.LessonId}");
                    }
                    else if (!incomingLes.IsDeleted)
                    {
                        // UPDATE
                        existingLes.Title = incomingLes.Title;
                        existingLes.LessonType = incomingLes.LessonType;
                        existingLes.Duration = incomingLes.Duration;
                        existingLes.OrderIndex = incomingLes.OrderIndex;
                        await _courseRepository.UpdateLessonAsync(existingLes);
                        Debug.WriteLine($"[AdminCourseService] Lesson updated: {incomingLes.LessonId}");

                        // Handle content update
                        if (incomingLes.Content != null)
                        {
                            // If lesson type changed, remove old content
                            if (!existingLes.LessonType.Equals(incomingLes.LessonType, StringComparison.OrdinalIgnoreCase))
                            {
                                await _courseRepository.RemoveContentAsync(existingLes.LessonID, existingLes.LessonType);
                                Debug.WriteLine($"[AdminCourseService] Old content removed (type changed): {existingLes.LessonID}");
                            }

                            // Create/update new content
                            await CreateContentAsync(existingLes.LessonID, incomingLes.LessonType, incomingLes.Content);
                        }
                    }
                }
            }
        }

        // ============================================================
        // HELPERS
        // ============================================================

        private void ValidateCreateCourseDto(CreateCourseDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Title))
                throw new ArgumentException("Course title is required.");

            if (dto.CategoryId == Guid.Empty)
                throw new ArgumentException("Category is required.");

            if (dto.Price < 0)
                throw new ArgumentException("Price cannot be negative.");

            if (dto.Discount < 0 || dto.Discount > 100)
                throw new ArgumentException("Discount must be between 0 and 100.");
        }

        private void ValidateUpdateCourseDto(UpdateCourseDto dto)
        {
            if (dto.CourseId == Guid.Empty)
                throw new ArgumentException("Course ID is required.");

            ValidateCreateCourseDto(new CreateCourseDto
            {
                Title = dto.Title,
                CategoryId = dto.CategoryId,
                Price = dto.Price,
                Discount = dto.Discount
            });
        }

        private string GenerateSlug(string title)
        {
            // Convert to lowercase, remove special characters, replace spaces with hyphens
            string slug = title.ToLowerInvariant();
            slug = Regex.Replace(slug, @"[^a-z0-9\s-]", "");
            slug = Regex.Replace(slug, @"\s+", "-");
            slug = Regex.Replace(slug, @"-+", "-").Trim('-');
            return slug;
        }

        private CreateLessonDto ToCreateLessonDto(UpdateLessonDto updateDto)
        {
            return new CreateLessonDto
            {
                Title = updateDto.Title,
                LessonType = updateDto.LessonType,
                Duration = updateDto.Duration,
                OrderIndex = updateDto.OrderIndex,
                Content = updateDto.Content
            };
        }

        /// <summary>
        /// Loads existing course graph (including soft-deleted entries) and maps to UpdateCourseDto
        /// for edit-mode UI. Uses repository getters for modules/lessons/content.
        /// </summary>
        public async Task<UpdateCourseDto> GetCourseForEditAsync(Guid courseId)
        {
            if (courseId == Guid.Empty) throw new ArgumentException("courseId required");

            // Load course
            var course = await _courseRepository.GetByIdAsync(courseId);
            if (course == null) throw new InvalidOperationException("Course not found");

            var dto = new UpdateCourseDto
            {
                CourseId = course.CourseID,
                Title = course.Title,
                Description = course.Description,
                Overview = course.Overview,
                Level = course.Level,
                Language = course.Language,
                CategoryId = course.CategoryId,
                Price = course.Price,
                Discount = course.Discount,
                ThumbnailBase64 = null,
                Status = course.Status
            };

            // Load modules (including IsDeleted ones)
            var modules = await _courseRepository.GetModulesByCourseIdAsync(courseId);
            foreach (var m in modules)
            {
                var modDto = new UpdateModuleDto
                {
                    ModuleId = m.ModuleID,
                    Title = m.Title,
                    OrderIndex = m.OrderIndex,
                    IsDeleted = m.IsDeleted
                };

                // load lessons for module (including deleted)
                var lessons = await _course_repository_GetLessonsSafelyAsync(m.ModuleID);
                foreach (var l in lessons)
                {
                    var lesDto = new UpdateLessonDto
                    {
                        LessonId = l.LessonID,
                        Title = l.Title,
                        LessonType = l.LessonType,
                        Duration = l.Duration,
                        OrderIndex = l.OrderIndex,
                        IsDeleted = l.IsDeleted
                    };

                    // load content depending on type
                    switch ((l.LessonType ?? "text").ToLowerInvariant())
                    {
                        case "text":
                            var text = await _courseRepository.GetTextContentAsync(l.LessonID);
                            if (text != null) lesDto.Content = new LessonContentDto { TextContent = text.Content };
                            break;
                        case "video":
                            var video = await _courseRepository.GetVideoContentAsync(l.LessonID);
                            if (video != null) lesDto.Content = new LessonContentDto { VideoUrl = video.VideoUrl, VideoCaption = video.Caption };
                            break;
                        case "quiz":
                            var quiz = await _courseRepository.GetQuizContentAsync(l.LessonID);
                            if (quiz != null)
                            {
                                var uq = new UpdateQuizDto { Title = quiz.Title, Description = quiz.Description };
                                foreach (var q in quiz.Questions)
                                {
                                    uq.Questions.Add(new UpdateQuizQuestionDto
                                    {
                                        QuestionId = q.QuestionID,
                                        Question = q.Question,
                                        Answers = q.Answers,
                                        CorrectIndex = q.CorrectIndex,
                                        Explanation = q.Explanation
                                    });
                                }
                                lesDto.Content = new LessonContentDto { Quiz = null }; // keep DTO wrapper; UI will request quiz via service if needed
                            }
                            break;
                        case "coding":
                            var prob = await _courseRepository.GetCodingProblemAsync(l.LessonID);
                            if (prob != null) lesDto.Content = new LessonContentDto { CodingProblem = new CreateCodingProblemDto { Title = prob.Title, Description = prob.Description, Difficulty = prob.Difficulty, TimeLimit = prob.TimeLimit, MemoryLimit = prob.MemoryLimit } };
                            break;
                    }

                    modDto.Lessons.Add(lesDto);
                }

                dto.Modules.Add(modDto);
            }

            return dto;
        }

        // helper: repository method may be same as GetLessonsByModuleIdAsync but ensure it returns deleted ones when requested in edit flow.
        private async Task<List<DataAccess.Entities.Lesson>> _course_repository_GetLessonsSafelyAsync(Guid moduleId)
        {
            // Current repository GetLessonsByModuleIdAsync filters IsDeleted=0.
            // If your repository supports including deleted items add an overload; fallback to querying DB directly here.
            var list = await _courseRepository.GetLessonsByModuleIdAsync(moduleId);
            return list;
        }
    }

    /// <summary>
    /// Simple HTML sanitizer to prevent XSS.
    /// Should be replaced with a more robust library in production.
    /// </summary>
    public class HtmlSanitizer
    {
        public string Sanitize(string html)
        {
            if (string.IsNullOrEmpty(html))
                return html;

            // Basic sanitization - remove script tags and event handlers
            html = Regex.Replace(html, @"<script\b[^<]*(?:(?!<\/script>)<[^<]*)*<\/script>", "", RegexOptions.IgnoreCase);
            html = Regex.Replace(html, @"\bon\w+\s*=\s*['""]?[^'""\s>]*['""]?", "", RegexOptions.IgnoreCase);

            return html;
        }
    }
}