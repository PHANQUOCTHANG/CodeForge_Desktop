using CodeForge_Desktop.DataAccess.Entities;
using System;
using System.Collections.Generic;

namespace CodeForge_Desktop.DataAccess.Interfaces
{
    public interface ICourseReviewRepository
    {
        // CRUD
        CourseReview GetById(Guid id);
        List<CourseReview> GetByCourseId(Guid courseId);
        CourseReview GetByUserAndCourse(Guid userId, Guid courseId);
        int Add(CourseReview review);
        int Update(CourseReview review);
        int Delete(Guid id); // Soft delete

        // Queries
        double GetAverageRating(Guid courseId);
        int GetReviewCount(Guid courseId);
    }
}