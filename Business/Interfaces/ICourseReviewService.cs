using CodeForge_Desktop.DataAccess.Entities;
using System;

namespace CodeForge_Desktop.Business.Interfaces
{
    public interface ICourseReviewService
    {
        bool CanReviewCourse(Guid userId, Guid courseId);
        bool SubmitReview(Guid userId, Guid courseId, int rating, string comment);
        CourseReview GetUserReview(Guid userId, Guid courseId);
        double GetAverageRating(Guid courseId);
    }
}