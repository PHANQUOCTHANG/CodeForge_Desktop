using CodeForge_Desktop.Business.DTOs;
using CodeForge_Desktop.Business.Interfaces;
using CodeForge_Desktop.DataAccess.Entities;
using CodeForge_Desktop.DataAccess.Interfaces;
using CodeForge_Desktop.DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CodeForge_Desktop.Business.Services
{
    public class CourseService : ICourseService
    {
        private readonly ICourseRepository _courseRepository;

        public CourseService(ICourseRepository courseRepository)
        {
            _courseRepository = courseRepository ?? new CourseRepository();
        }

        // =========================================================
        // 1. READ OPERATIONS (Lấy dữ liệu)
        // =========================================================

        public async Task<List<CourseDto>> GetAllCoursesAsync()
        {
            var courses = await _courseRepository.GetAllAsync();
            return courses.Select(c => MapToCourseDto(c)).ToList();
        }

        public async Task<List<CourseDto>> SearchCoursesAsync(string keyword, string level)
        {
            var courses = await _courseRepository.GetAllAsync(keyword, level);
            return courses.Select(c => MapToCourseDto(c)).ToList();
        }

        public async Task<CourseDetailDto> GetCourseDetailAsync(Guid courseId)
        {
            // 1. Lấy thông tin Course
            var course = await _courseRepository.GetByIdAsync(courseId);
            if (course == null) return null;

            var detailDto = new CourseDetailDto
            {
                CourseID = course.CourseID, // ID Viết hoa
                Title = course.Title,
                Description = course.Description,
                Overview = course.Overview,
                Level = course.Level,
                Language = course.Language,
                Rating = course.Rating,
                Price = course.Price,
                Thumbnail = course.Thumbnail,
                TotalStudents = course.TotalStudents,
                Duration = course.Duration,
                IsEnrolled = course.IsEnrolled,
                Progress = course.ProgressPercentage,
                Modules = new List<ModuleDto>()
            };

            // 2. Lấy Modules
            var modules = await _courseRepository.GetModulesByCourseIdAsync(courseId);

            // Ép kiểu Repository để gọi các hàm chi tiết (Video, Quiz...) nếu Interface chưa có
            var repo = (CourseRepository)_courseRepository;

            foreach (var mod in modules)
            {
                var modDto = new ModuleDto
                {
                    ModuleID = mod.ModuleID, // ID Viết hoa
                    CourseID = mod.CourseID, // ID Viết hoa
                    Title = mod.Title,
                    OrderIndex = mod.OrderIndex,
                    Lessons = new List<LessonDto>()
                };

                // 3. Lấy Lessons
                var lessons = await _courseRepository.GetLessonsByModuleIdAsync(mod.ModuleID);

                foreach (var les in lessons)
                {
                    var lesDto = new LessonDto
                    {
                        LessonID = les.LessonID, // ID Viết hoa
                        ModuleID = les.ModuleID, // ID Viết hoa
                        Title = les.Title,
                        LessonType = les.LessonType,
                        Duration = les.Duration,
                        OrderIndex = les.OrderIndex
                    };

                    // 4. 🔥 LOAD CHI TIẾT NỘI DUNG DỰA VÀO TYPE 🔥
                    string type = (les.LessonType ?? "").ToLower();
                    switch (type)
                    {
                        case "video":
                            lesDto.VideoContent = await repo.GetVideoContentAsync(les.LessonID);
                            break;

                        case "text":
                            lesDto.TextContent = await repo.GetTextContentAsync(les.LessonID);
                            break;

                        case "quiz":
                            lesDto.QuizContent = await repo.GetQuizContentAsync(les.LessonID);
                            break;

                        case "coding":
                            lesDto.CodingProblem = await repo.GetCodingProblemAsync(les.LessonID);
                            break;
                    }

                    modDto.Lessons.Add(lesDto);
                }
                detailDto.Modules.Add(modDto);
            }

            return detailDto;
        }

        public async Task<int> CountCoursesAsync()
        {
            var all = await _courseRepository.GetAllAsync();
            return all.Count;
        }

        // =========================================================
        // 2. WRITE OPERATIONS (Thêm, Sửa, Xóa)
        // =========================================================
            
        public async Task CreateCourseAsync(CourseDetailDto dto)
        {
            if (await _courseRepository.ExistsByTitleAsync(dto.Title))
                throw new Exception("Tên khóa học đã tồn tại.");

            // A. Tạo Course
            string slug = await GenerateUniqueSlugAsync(dto.Title);
            var course = new Course
            {
                CourseID = Guid.NewGuid(), // ID Viết hoa
                Title = dto.Title,
                Description = dto.Description,
                Overview = dto.Overview,
                Level = dto.Level,
                Language = dto.Language,
                Price = dto.Price,
                Thumbnail = dto.Thumbnail,
                Status = "draft",
                CreatedAt = DateTime.Now,
                Slug = slug
            };

            await _courseRepository.AddAsync(course);

            // B. Tạo Modules & Lessons
            if (dto.Modules != null)
            {
                foreach (var modDto in dto.Modules)
                {
                    var mod = new Module
                    {
                        ModuleID = Guid.NewGuid(),
                        CourseID = course.CourseID,
                        Title = modDto.Title,
                        OrderIndex = modDto.OrderIndex
                    };
                    await _courseRepository.AddModuleAsync(mod);

                    if (modDto.Lessons != null)
                    {
                        foreach (var lesDto in modDto.Lessons)
                        {
                            var les = new Lesson
                            {
                                LessonID = Guid.NewGuid(),
                                ModuleID = mod.ModuleID,
                                Title = lesDto.Title,
                                LessonType = lesDto.LessonType,
                                Duration = lesDto.Duration,
                                OrderIndex = lesDto.OrderIndex
                            };
                            await _courseRepository.AddLessonAsync(les);

                            // Lưu nội dung chi tiết
                            await SaveLessonContentAsync(les.LessonID, lesDto);
                        }
                    }
                }
            }
        }

        public async Task UpdateCourseAsync(Guid courseId, CourseDetailDto dto)
        {
            var existing = await _courseRepository.GetByIdAsync(courseId);
            if (existing == null) throw new Exception("Không tìm thấy khóa học.");

            // A. Update Course Info
            existing.Title = dto.Title;
            existing.Description = dto.Description;
            existing.Overview = dto.Overview;
            existing.Level = dto.Level;
            existing.Language = dto.Language;
            existing.Price = dto.Price;
            existing.Thumbnail = dto.Thumbnail;

            await _courseRepository.UpdateAsync(existing);

            // B. Update Modules (Diffing Logic)
            if (dto.Modules != null)
            {
                foreach (var modDto in dto.Modules)
                {
                    if (modDto.ModuleID != Guid.Empty)
                    {
                        if (modDto.IsDeleted)
                        {
                            await _courseRepository.DeleteModuleAsync(modDto.ModuleID);
                        }
                        else
                        {
                            var mod = new Module
                            {
                                ModuleID = modDto.ModuleID,
                                CourseID = courseId,
                                Title = modDto.Title,
                                OrderIndex = modDto.OrderIndex
                            };
                            await _courseRepository.UpdateModuleAsync(mod);
                            await UpdateLessonsAsync(mod.ModuleID, modDto.Lessons);
                        }
                    }
                    else if (!modDto.IsDeleted) // Insert New Module
                    {
                        var newMod = new Module
                        {
                            ModuleID = Guid.NewGuid(),
                            CourseID = courseId,
                            Title = modDto.Title,
                            OrderIndex = modDto.OrderIndex
                        };
                        await _courseRepository.AddModuleAsync(newMod);
                        await UpdateLessonsAsync(newMod.ModuleID, modDto.Lessons);
                    }
                }
            }
        }

        public async Task DeleteCourseAsync(Guid id)
        {
            await _courseRepository.DeleteAsync(id);
        }

        // =========================================================
        // 3. PRIVATE HELPER METHODS
        // =========================================================

        private CourseDto MapToCourseDto(Course c)
        {
            return new CourseDto
            {
                CourseID = c.CourseID, // ID Viết hoa
                Title = c.Title,
                Level = c.Level,
                Language = c.Language,
                Rating = c.Rating,
                Price = c.Price,
                Thumbnail = c.Thumbnail,
                TotalStudents = c.TotalStudents,
                Duration = c.Duration,
                IsEnrolled = c.IsEnrolled,
                Progress = c.ProgressPercentage
            };
        }

        private async Task UpdateLessonsAsync(Guid moduleId, List<LessonDto> lessons)
        {
            if (lessons == null) return;
            foreach (var lesDto in lessons)
            {
                if (lesDto.LessonID != Guid.Empty) // ID Viết hoa
                {
                    if (lesDto.IsDeleted)
                    {
                        await _courseRepository.DeleteLessonAsync(lesDto.LessonID);
                    }
                    else
                    {
                        var les = new Lesson
                        {
                            LessonID = lesDto.LessonID,
                            ModuleID = moduleId,
                            Title = lesDto.Title,
                            LessonType = lesDto.LessonType,
                            Duration = lesDto.Duration,
                            OrderIndex = lesDto.OrderIndex
                        };
                        await _courseRepository.UpdateLessonAsync(les);
                        await SaveLessonContentAsync(les.LessonID, lesDto);
                    }
                }
                else if (!lesDto.IsDeleted) // Insert New Lesson
                {
                    var newLes = new Lesson
                    {
                        LessonID = Guid.NewGuid(),
                        ModuleID = moduleId,
                        Title = lesDto.Title,
                        LessonType = lesDto.LessonType,
                        Duration = lesDto.Duration,
                        OrderIndex = lesDto.OrderIndex
                    };
                    await _courseRepository.AddLessonAsync(newLes);
                    await SaveLessonContentAsync(newLes.LessonID, lesDto);
                }
            }
        }

        private async Task SaveLessonContentAsync(Guid lessonId, LessonDto dto)
        {
            var repo = (CourseRepository)_courseRepository;
            string type = dto.LessonType?.ToLower();

            if (type == "video" && dto.VideoContent != null)
            {
                var vid = new LessonVideo
                {
                    LessonID = lessonId,
                    VideoUrl = dto.VideoContent.VideoUrl,
                    Duration = 0 // Có thể lấy từ dto.Duration nếu có
                };
                await repo.AddOrUpdateVideoAsync(vid);
            }
            else if (type == "text" && dto.TextContent != null)
            {
                await repo.AddOrUpdateTextAsync(lessonId, dto.TextContent.Content);
            }
            // TODO: Bổ sung logic lưu Quiz và Coding
        }
        // Trong CodeForge_Desktop.Business.Services.CourseService

        public async Task<List<CourseDto>> GetEnrolledCoursesAsync(Guid userId)
        {
            // Gọi Repository lấy danh sách Entity
            var entities = await _courseRepository.GetListHasEnrollAsync(userId);

            // Map sang DTO
            return entities.Select(c => new CourseDto
            {
                CourseID = c.CourseID,
                Title = c.Title,
                Thumbnail = c.Thumbnail,
                Description = c.Description,
                Price = c.Price,
                Rating = c.Rating,
                TotalStudents = c.TotalStudents,
                // Giả sử Repository đã tính ProgressPercentage trong câu SQL (như code bài 1)
                Progress = c.ProgressPercentage
            }).ToList();
        }
        // --- SLUG GENERATION ---

        private async Task<string> GenerateUniqueSlugAsync(string title)
        {
            string baseSlug = ConvertToSlug(title);
            string slug = baseSlug;
            int counter = 1;

            while (await _courseRepository.ExistsBySlugAsync(slug))
            {
                slug = $"{baseSlug}-{counter++}";
            }
            return slug;
        }

        private string ConvertToSlug(string input)
        {
            if (string.IsNullOrWhiteSpace(input)) return string.Empty;
            string str = input.Normalize(NormalizationForm.FormD);
            StringBuilder sb = new StringBuilder();

            foreach (char c in str)
            {
                if (CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark)
                    sb.Append(c);
            }

            str = sb.ToString().Normalize(NormalizationForm.FormC).ToLower();
            str = str.Replace("đ", "d");
            str = Regex.Replace(str, @"[^a-z0-9\s-]", "");
            str = Regex.Replace(str, @"\s+", "-").Trim();
            str = Regex.Replace(str, @"-+", "-");
            return str;
        }
    }
}