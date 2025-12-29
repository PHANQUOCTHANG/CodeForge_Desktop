using CodeForge_Desktop.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CodeForge_Desktop.DataAccess.Interfaces
{
    public interface ICourseReviewRepository
    {
        // Read
        Task<List<CourseReview>> GetReviewsByCourseIdAsync(Guid courseId);
        Task<CourseReview> GetReviewByUserAndCourseAsync(Guid userId, Guid courseId);
        Task<CourseReview> GetByIdAsync(Guid reviewId);

        // Write
        Task<CourseReview> AddAsync(CourseReview review);
        Task<CourseReview> UpdateAsync(CourseReview review);
        Task<bool> DeleteAsync(Guid reviewId);
    }
}