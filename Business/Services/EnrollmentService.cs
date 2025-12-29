using CodeForge_Desktop.DataAccess.Entities;
using CodeForge_Desktop.DataAccess.Interfaces;
using CodeForge_Desktop.Business.Interfaces;
using System;

namespace CodeForge_Desktop.Business.Services
{
    public class EnrollmentService : IEnrollmentService
    {
        private readonly IEnrollmentRepository _enrollmentRepository;
        private readonly IProgressRepository _progressRepository;

        // Constructor Injection
        public EnrollmentService(IEnrollmentRepository enrollmentRepository, IProgressRepository progressRepository)
        {
            _enrollmentRepository = enrollmentRepository ?? throw new ArgumentNullException(nameof(enrollmentRepository));
            _progressRepository = progressRepository ?? throw new ArgumentNullException(nameof(progressRepository));
        }

        public bool IsUserEnrolled(Guid userId, Guid courseId)
        {
            // Gọi Repository để check
            return _enrollmentRepository.IsUserEnrolled(userId, courseId);
        }

        public bool EnrollUserToCourse(Guid userId, Guid courseId)
        {
            // 1. Kiểm tra đã đăng ký chưa
            if (_enrollmentRepository.IsUserEnrolled(userId, courseId))
            {
                return true; // Đã đăng ký rồi coi như thành công
            }

            try
            {
                // 2. Tạo Enrollment mới
                var enrollment = new Enrollment
                {
                    EnrollmentID = Guid.NewGuid(),
                    UserID = userId,
                    CourseID = courseId,
                    EnrolledAt = DateTime.Now,
                    Status = "enrolled" // Mặc định là đã tham gia (nếu cần thanh toán thì logic sẽ phức tạp hơn ở đây)
                };

                int result = _enrollmentRepository.Add(enrollment);

                // 3. (Optional) Khởi tạo Progress ban đầu nếu cần thiết
                // _progressService.InitProgress(userId, courseId);

                return result > 0;
            }
            catch (Exception ex)
            {
                // Log error here if needed
                Console.WriteLine("Enroll Error: " + ex.Message);
                return false;
            }
        }

        public int GetEnrolledStudentCount(Guid courseId)
        {
            return _enrollmentRepository.GetEnrolledStudentCount(courseId);
        }
    }
}