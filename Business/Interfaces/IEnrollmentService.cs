using System;

namespace CodeForge_Desktop.Business.Interfaces
{
    public interface IEnrollmentService
    {
        bool IsUserEnrolled(Guid userId, Guid courseId);
        bool EnrollUserToCourse(Guid userId, Guid courseId);
        int GetEnrolledStudentCount(Guid courseId);
    }
}