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

        public EnrollmentService(IEnrollmentRepository enrollmentRepository, IProgressRepository progressRepository)
        {
            _enrollmentRepository = enrollmentRepository ?? throw new ArgumentNullException(nameof(enrollmentRepository));
            _progressRepository = progressRepository ?? throw new ArgumentNullException(nameof(progressRepository));
        }

        public bool IsUserEnrolled(Guid userId, Guid courseId)
        {
            return _enrollmentRepository.IsUserEnrolled(userId, courseId);
        }

        public bool EnrollUserToCourse(Guid userId, Guid courseId)
        {
            try
            {
                var enrollment = new Enrollment
                {
                    EnrollmentID = Guid.NewGuid(),
                    UserID = userId,
                    CourseID = courseId,
                    EnrolledAt = DateTime.UtcNow,
                    Status = "enrolled",
                };

                int result = _enrollmentRepository.Add(enrollment);
                return result > 0;
            }
            catch
            {
                return false;
            }
        }

        public int GetEnrolledStudentCount(Guid courseId)
        {
            return _enrollmentRepository.GetEnrolledStudentCount(courseId);
        }
    }
}