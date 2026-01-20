using CodeForge_Desktop.DataAccess.Interfaces;
using CodeForge_Desktop.DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CodeForge_Desktop.Business.Services
{
    public class ProgressService
    {
        private readonly IProgressRepository _repo;

        // Constructor Injection hoặc Default
        public ProgressService(IProgressRepository repo)
        {
            _repo = repo ?? new ProgressRepository();
        }

        public async Task<bool> MarkLessonCompletedAsync(Guid userId, Guid lessonId)
        {
            // Có thể thêm logic kiểm tra Enrollment tại đây nếu muốn chặt chẽ hơn
            return await _repo.MarkCompletedAsync(userId, lessonId);
        }

        public async Task<List<Guid>> GetCompletedLessonsAsync(Guid userId, Guid courseId)
        {
            return await _repo.GetCompletedLessonsAsync(userId, courseId);
        }

        public double GetProgressPercentage(Guid userId, Guid courseId)
        {
            // Hàm này có thể chạy Sync hoặc Async tùy nhu cầu UI
            // Ở đây mình wrap lại task result cho tiện gọi từ UI Event
            return _repo.GetProgressPercentageAsync(userId, courseId).Result;
        }
    }
}