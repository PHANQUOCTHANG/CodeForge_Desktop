using CodeForge_Desktop.DataAccess.Entities; // Giả sử bạn đã có Entity Progress
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CodeForge_Desktop.DataAccess.Interfaces
{
    public interface IProgressRepository
    {
        // 1. Mark hoàn thành (Upsert: Thêm mới hoặc cập nhật)
        Task<bool> MarkCompletedAsync(Guid userId, Guid lessonId);

        // 2. Lấy danh sách LessonID đã hoàn thành trong 1 khóa học
        Task<List<Guid>> GetCompletedLessonsAsync(Guid userId, Guid courseId);

        // 3. Tính % hoàn thành của 1 khóa học
        Task<double> GetProgressPercentageAsync(Guid userId, Guid courseId);
    }
}