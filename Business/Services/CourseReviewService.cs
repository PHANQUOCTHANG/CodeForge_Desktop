using CodeForge_Desktop.Business.Interfaces;
using CodeForge_Desktop.DataAccess.Entities;
using CodeForge_Desktop.DataAccess.Interfaces;
using System;
using CodeForge_Desktop.DataAccess.Repositories;
using CodeForge_Desktop.Business.Helpers;

namespace CodeForge_Desktop.Business.Services
{
    public class CourseReviewService : ICourseReviewService
    {
        private readonly ICourseReviewRepository _reviewRepository;
        private readonly IEnrollmentRepository _enrollmentRepository;

        public CourseReviewService(ICourseReviewRepository reviewRepository, IEnrollmentRepository enrollmentRepository)
        {
            _reviewRepository = reviewRepository;
            _enrollmentRepository = enrollmentRepository;
        }

        public bool CanReviewCourse(Guid userId, Guid courseId)
        {
            // Chỉ người đã đăng ký mới có thể đánh giá
            return _enrollmentRepository.IsUserEnrolled(userId, courseId);
        }

        public bool SubmitReview(Guid userId, Guid courseId, int rating, string comment)
        {
            if (!CanReviewCourse(userId, courseId))
                return false;

            if (rating < 1 || rating > 5)
                return false;

            var existingReview = _reviewRepository.GetByUserAndCourse(userId, courseId);

            if (existingReview != null)
            {
                // Cập nhật đánh giá cũ
                existingReview.Rating = rating;
                existingReview.Comment = comment;
                existingReview.UpdatedAt = DateTime.UtcNow;
                return _reviewRepository.Update(existingReview) > 0;
            }
            else
            {
                // Tạo đánh giá mới
                var review = new CourseReview
                {
                    ReviewID = Guid.NewGuid(),
                    UserID = userId,
                    CourseID = courseId,
                    Rating = rating,
                    Comment = comment,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                };
                return _reviewRepository.Add(review) > 0;
            }
        }

        public CourseReview GetUserReview(Guid userId, Guid courseId)
        {
            return _reviewRepository.GetByUserAndCourse(userId, courseId);
        }

        public double GetAverageRating(Guid courseId)
        {
            return _reviewRepository.GetAverageRating(courseId);
        }
    }

    // ====================
    // MOVE METHOD TO CLASS
    // ====================
    //
    // NOTE: UI classes (WinForms UserControl like ucCourseList) and code that references
    // System.Windows.Forms / System.Drawing were accidentally placed in this business
    // service file. That produces numerous CS0246 / CS0103 errors because the business
    // layer file isn't expected to reference UI types. The UI code has been removed from
    // this file — move it into Presentation/Forms/Student/ucCourseList.cs (WinForms file)
    // and add the appropriate using directives there: System.Windows.Forms, System.Drawing.
    //
}