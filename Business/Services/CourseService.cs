using CodeForge_Desktop.Business.Interfaces;
using CodeForge_Desktop.DataAccess.Entities;
using CodeForge_Desktop.DataAccess.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeForge_Desktop.Business.Services
{
    public class CourseService : ICourseService
    {
        private readonly ICourseRepository _courseRepository;

        // Constructor Injection: Nhận Repository qua constructor
        // Điều này giúp dễ dàng thay thế Repository (ví dụ Mock để test) mà không sửa code Service
        public CourseService(ICourseRepository courseRepository)
        {
            _courseRepository = courseRepository;
        }

        public async Task<List<Course>> GetAllCoursesAsync()
        {
            return await _courseRepository.GetAllAsync();
        }

        public async Task<List<Course>> SearchCoursesAsync(string keyword, string status)
        {
            // Có thể thêm logic chuẩn hóa từ khóa tìm kiếm ở đây nếu cần (Trim, Lower...)
            return await _courseRepository.SearchAsync(keyword, status);
        }

        public async Task<Course> GetCourseByIdAsync(Guid id)
        {
            var course = await _courseRepository.GetByIdAsync(id);
            return course;
        }

        public async Task CreateCourseAsync(Course course)
        {
            // 1. Validate dữ liệu đầu vào
            ValidateCourse(course);

            // 2. Kiểm tra nghiệp vụ: Tên khóa học không được trùng
            bool isExists = await _courseRepository.ExistsByTitleAsync(course.Title);
            if (isExists)
            {
                throw new InvalidOperationException($"Khóa học với tên '{course.Title}' đã tồn tại. Vui lòng chọn tên khác.");
            }

            // 3. Gọi Repository để thêm mới
            await _courseRepository.AddAsync(course);
        }

        public async Task UpdateCourseAsync(Course course)
        {
            // 1. Validate dữ liệu đầu vào
            ValidateCourse(course);

            // 2. Kiểm tra nghiệp vụ: Tên khóa học không được trùng (ngoại trừ chính nó)
            // course.CourseId được truyền vào để loại trừ chính bản ghi đang sửa
            bool isExists = await _courseRepository.ExistsByTitleAsync(course.Title, course.CourseId);
            if (isExists)
            {
                throw new InvalidOperationException($"Khóa học với tên '{course.Title}' đã tồn tại. Vui lòng chọn tên khác.");
            }

            // 3. Gọi Repository để cập nhật
            await _courseRepository.UpdateAsync(course);
        }

        public async Task DeleteCourseAsync(Guid id)
        {
            // Ở đây có thể thêm logic kiểm tra nghiệp vụ trước khi xóa
            // Ví dụ: Không được xóa khóa học đang có học viên đang học (Active Enrollments)
            // Hiện tại ta làm đơn giản là xóa luôn (Soft delete ở tầng Repo lo).
            await _courseRepository.DeleteAsync(id);
        }

        public async Task<int> CountCoursesAsync()
        {
            return await _courseRepository.CountAsync();
        }

        // --- Helper Methods (Private) ---

        /// <summary>
        /// Hàm validate chung cho cả Create và Update
        /// </summary>
        private void ValidateCourse(Course course)
        {
            if (course == null)
                throw new ArgumentNullException(nameof(course));

            if (string.IsNullOrWhiteSpace(course.Title))
                throw new ArgumentException("Tên khóa học không được để trống.");

            if (course.Title.Length > 200)
                throw new ArgumentException("Tên khóa học không được quá 200 ký tự.");

            if (course.Price < 0)
                throw new ArgumentException("Giá khóa học không được là số âm.");

            if (course.Discount < 0 || course.Discount > 100)
                throw new ArgumentException("Giảm giá phải nằm trong khoảng từ 0 đến 100%.");
        }
    }
}
