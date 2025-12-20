using CodeForge_Desktop.DataAccess.Entities;
using System;
using System.Collections.Generic;

namespace CodeForge_Desktop.DataAccess.Interfaces
{
    public interface IEnrollmentRepository
    {
        // CRUD
        Enrollment GetById(Guid id);
        List<Enrollment> GetByUserId(Guid userId);
        List<Enrollment> GetByCourseId(Guid courseId);
        Enrollment GetByUserAndCourse(Guid userId, Guid courseId);
        int Add(Enrollment enrollment);
        int Update(Enrollment enrollment);
        int Delete(Guid id); // Soft delete

        // Queries
        bool IsUserEnrolled(Guid userId, Guid courseId);
        int GetEnrolledStudentCount(Guid courseId);
    }
}