using CodeForge_Desktop.Business.DTOs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CodeForge_Desktop.Business.Interfaces
{
    public interface ICourseService
    {
        // --- READ OPERATIONS ---

        /// <summary>
        /// Lấy danh sách tất cả khóa học (DTO rút gọn cho Grid/List)
        /// </summary>
        Task<List<CourseDto>> GetAllCoursesAsync();

        /// <summary>
        /// Tìm kiếm và lọc khóa học
        /// </summary>
        Task<List<CourseDto>> SearchCoursesAsync(string keyword, string level);

        /// <summary>
        /// Lấy chi tiết đầy đủ (Course + Modules + Lessons + Content) để Edit hoặc View
        /// </summary>
        Task<CourseDetailDto> GetCourseDetailAsync(Guid courseId);

        // --- WRITE OPERATIONS ---

        /// <summary>
        /// Tạo mới khóa học cùng toàn bộ nội dung bên trong
        /// </summary>
        Task CreateCourseAsync(CourseDetailDto courseDto);

        /// <summary>
        /// Cập nhật khóa học (xử lý cả thêm/sửa/xóa Module/Lesson con)
        /// </summary>
        Task UpdateCourseAsync(Guid courseId, CourseDetailDto courseDto);

        /// <summary>
        /// Xóa khóa học (Soft Delete)
        /// </summary>
        Task DeleteCourseAsync(Guid courseId);
        Task<List<CourseDto>> GetEnrolledCoursesAsync(Guid userId);
    }
}