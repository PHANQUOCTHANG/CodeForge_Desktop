using CodeForge_Desktop.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeForge_Desktop.Business.Interfaces
{
    public interface ICourseService
    {
        /// <summary>
        /// Lấy danh sách tất cả khóa học (cho hiển thị mặc định)
        /// </summary>
        Task<List<Course>> GetAllCoursesAsync();

        /// <summary>
        /// Tìm kiếm khóa học theo từ khóa và trạng thái
        /// </summary>
        /// <param name="keyword">Tên khóa học</param>
        /// <param name="status">Trạng thái (Active/Draft/All)</param>
        Task<List<Course>> SearchCoursesAsync(string keyword, string status);

        /// <summary>
        /// Lấy chi tiết một khóa học để xem hoặc sửa
        /// </summary>
        Task<Course> GetCourseByIdAsync(Guid id);

        /// <summary>
        /// Tạo mới khóa học (có validate dữ liệu)
        /// </summary>
        Task CreateCourseAsync(Course course);

        /// <summary>
        /// Cập nhật khóa học (có validate dữ liệu)
        /// </summary>
        Task UpdateCourseAsync(Course course);

        /// <summary>
        /// Xóa khóa học (Soft Delete)
        /// </summary>
        Task DeleteCourseAsync(Guid id);

        /// <summary>
        /// Đếm tổng số khóa học (cho thống kê Dashboard)
        /// </summary>
        Task<int> CountCoursesAsync();
    }
}
