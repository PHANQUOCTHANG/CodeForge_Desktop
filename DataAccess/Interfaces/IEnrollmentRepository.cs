using CodeForge_Desktop.DataAccess.Entities;
using System;
using System.Collections.Generic;

namespace CodeForge_Desktop.DataAccess.Interfaces
{
    public interface IEnrollmentRepository
    {
        Enrollment GetById(Guid id);
        List<Enrollment> GetByUserId(Guid userId);
        List<Enrollment> GetByCourseId(Guid courseId);

        // Thêm phương thức này để lấy chi tiết Enrollment
        Enrollment GetByUserIdAndCourseId(Guid userId, Guid courseId);

        int Add(Enrollment enrollment);
        int Update(Enrollment enrollment);
        int Delete(Guid id);

        bool IsUserEnrolled(Guid userId, Guid courseId);
        int GetEnrolledStudentCount(Guid courseId);

        // Kiểm tra tồn tại (tối ưu hơn GetByUserIdAndCourseId nếu chỉ cần check true/false)
        bool Exists(Guid userId, Guid courseId);
    }
}