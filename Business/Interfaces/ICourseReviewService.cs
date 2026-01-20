using CodeForge_Desktop.Business.DTOs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CodeForge_Desktop.Business.Interfaces
{
    public interface ICourseReviewService
    {
        Task<List<CourseReviewDto>> GetReviewsByCourseIdAsync(Guid courseId);

        // Kiểm tra xem user có quyền review không (đã mua & chưa review)
        bool CanReviewCourse(Guid userId, Guid courseId);

        // Lấy review của chính user đó (để hiển thị cho họ sửa)
        CourseReviewDto GetUserReview(Guid userId, Guid courseId);

        Task<bool> SubmitReview(Guid userId, Guid courseId, int rating, string comment);
    }
}