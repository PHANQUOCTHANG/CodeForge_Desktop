using CodeForge_Desktop.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeForge_Desktop.DataAccess.Interfaces
{
    public interface ICourseRepository
    {
        /// <summary>
        /// Lấy tất cả khóa học (thường dùng để đổ dữ liệu vào DataGridView)
        /// </summary>
        Task<List<Course>> GetAllAsync();

        /// <summary>
        /// Tìm kiếm khóa học theo từ khóa (Title) và lọc theo Level (Beginner/Intermediate/Advanced)
        /// </summary>
        Task<List<Course>> SearchAsync(string keyword, string level = null);
                
        /// <summary>
        /// Lấy chi tiết khóa học theo ID
        /// </summary>
        Task<Course> GetByIdAsync(Guid id);

        /// <summary>
        /// Thêm mới một khóa học
        /// </summary>
        Task AddAsync(Course course);

        /// <summary>
        /// Cập nhật thông tin khóa học
        /// </summary>
        Task UpdateAsync(Course course);

        /// <summary>
        /// Xóa khóa học (Thường là Soft Delete: set IsDeleted = true)
        /// </summary>
        Task DeleteAsync(Guid id);

        /// <summary>
        /// Kiểm tra trùng tên khóa học (để validate trên form)
        /// </summary>
        Task<bool> ExistsByTitleAsync(string title, Guid? excludeId = null);

        /// <summary>
        /// Thống kê nhanh (Ví dụ: đếm số lượng khóa học) - Dùng cho Dashboard
        /// </summary>
        Task<int> CountAsync();
    }
}
