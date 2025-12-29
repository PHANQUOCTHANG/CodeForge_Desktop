using CodeForge_Desktop.Business.DTOs;
using CodeForge_Desktop.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CodeForge_Desktop.DataAccess.Interfaces
{
    public interface ICourseRepository
    {
        // --- COURSE ---
        Task<List<Course>> GetAllAsync(string search = null, string level = null);
        Task<List<Course>> GetListHasEnrollAsync(Guid userId);
        Task<Course> GetByIdAsync(Guid id);
        Task<bool> ExistsByTitleAsync(string title, Guid? excludeId = null);
        Task<bool> ExistsBySlugAsync(string slug);

        // CRUD Course
        Task AddAsync(Course course);
        Task UpdateAsync(Course course);
        Task DeleteAsync(Guid id); // Soft Delete

        // --- MODULES ---
        Task<List<Module>> GetModulesByCourseIdAsync(Guid courseId);
        Task AddModuleAsync(Module module);
        Task UpdateModuleAsync(Module module);
        Task DeleteModuleAsync(Guid moduleId);

        // --- LESSONS ---
        Task<List<Lesson>> GetLessonsByModuleIdAsync(Guid moduleId);
        Task AddLessonAsync(Lesson lesson);
        Task UpdateLessonAsync(Lesson lesson);
        Task DeleteLessonAsync(Guid lessonId);

        // --- CONTENT (Video, Text...) ---
        Task<LessonVideo> GetVideoByLessonIdAsync(Guid lessonId);
        Task AddOrUpdateVideoAsync(LessonVideo video);

        Task AddOrUpdateTextAsync(Guid lessonId, string content);

        Task RemoveContentAsync(Guid lessonId, string lessonType);
        Task<ProblemDto?> GetCodingProblemAsync(Guid lessonId);
        Task<LessonQuizDto?> GetQuizContentAsync(Guid lessonId);
        Task<LessonTextDto?> GetTextContentAsync(Guid lessonId);
        Task<LessonVideoDto?> GetVideoContentAsync(Guid lessonId);
    }
}