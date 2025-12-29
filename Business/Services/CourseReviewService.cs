using CodeForge_Desktop.Business.DTOs;
using CodeForge_Desktop.Business.Interfaces;
using CodeForge_Desktop.DataAccess.Entities;
using CodeForge_Desktop.DataAccess.Interfaces;
using CodeForge_Desktop.DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeForge_Desktop.Business.Services
{
    public class CourseReviewService : ICourseReviewService
    {
        private readonly ICourseReviewRepository _reviewRepository;
        private readonly IEnrollmentRepository _enrollmentRepository;
        private readonly ICourseRepository _courseRepository; // Cần repo này để update rating cho Course

        // Constructor
        public CourseReviewService(ICourseReviewRepository reviewRepo, IEnrollmentRepository enrollRepo)
        {
            _reviewRepository = reviewRepo ?? new CourseReviewRepository();
            _enrollmentRepository = enrollRepo ?? new EnrollmentRepository();
            _courseRepository = new CourseRepository(); // Tự khởi tạo nếu không được inject
        }

        public async Task<List<CourseReviewDto>> GetReviewsByCourseIdAsync(Guid courseId)
        {
            var reviews = await _reviewRepository.GetReviewsByCourseIdAsync(courseId);

            // Map thủ công Entity -> DTO
            return reviews.Select(r => new CourseReviewDto
            {
                ReviewID = r.ReviewID,
                CourseID = r.CourseID,
                User = r.User?.Username ?? "Anonymous", // Lấy tên user
                Rating = r.Rating,
                Comment = r.Comment,
                CreatedAt = r.CreatedAt
            }).ToList();
        }

        public bool CanReviewCourse(Guid userId, Guid courseId)
        {
            // 1. Phải đã đăng ký (Enrolled)
            if (!_enrollmentRepository.IsUserEnrolled(userId, courseId)) return false;

            // 2. Chưa từng review (Logic web cho phép sửa, nhưng ở đây check để hiện nút Submit hay Update)
            // (Tạm thời cho phép review nhiều lần hoặc update sau)
            return true;
        }

        public CourseReviewDto GetUserReview(Guid userId, Guid courseId)
        {
            // Hàm này chạy sync để UI bind dữ liệu nhanh
            var review = _reviewRepository.GetReviewByUserAndCourseAsync(userId, courseId).Result;
            if (review == null) return null;

            return new CourseReviewDto
            {
                ReviewID = review.ReviewID,
                Rating = review.Rating,
                Comment = review.Comment,
                CreatedAt = review.CreatedAt
            };
        }

        public async Task<bool> SubmitReview(Guid userId, Guid courseId, int rating, string comment)
        {
            // 1. Kiểm tra enrollment
            if (!_enrollmentRepository.IsUserEnrolled(userId, courseId))
                throw new Exception("Bạn phải đăng ký khóa học trước khi đánh giá.");

            // 2. Kiểm tra đã review chưa
            var existingReview = await _reviewRepository.GetReviewByUserAndCourseAsync(userId, courseId);
            var course = await _courseRepository.GetByIdAsync(courseId);

            if (course == null) throw new Exception("Khóa học không tồn tại.");

            if (existingReview != null)
            {
                // --- UPDATE ---
                int oldRating = existingReview.Rating;

                existingReview.Rating = rating;
                existingReview.Comment = comment;
                await _reviewRepository.UpdateAsync(existingReview);

                // Tính lại Rating trung bình (Logic Update)
                course.Rating = CalculateNewRatingOnUpdate(course.Rating, course.TotalRatings, oldRating, rating);
            }
            else
            {
                // --- CREATE ---
                var newReview = new CourseReview
                {
                    UserID = userId,
                    CourseID = courseId,
                    Rating = rating,
                    Comment = comment
                };
                await _reviewRepository.AddAsync(newReview);

                // Tăng bộ đếm & Tính lại Rating (Logic Create)
                course.TotalRatings += 1;
                course.Rating = CalculateNewRatingOnCreate(course.Rating, course.TotalRatings, rating);
            }

            // Cập nhật lại thông tin Course
            await _courseRepository.UpdateAsync(course);
            return true;
        }

        // ========================================================
        // 📊 LOGIC TÍNH TOÁN RATING (Mang từ Backend Web sang)
        // ========================================================

        private double CalculateNewRatingOnCreate(double currentAvg, int newTotalCount, int newRating)
        {
            if (newTotalCount <= 1) return (double)newRating;

            double oldTotalCount = (double)newTotalCount - 1;
            double oldTotalSum = currentAvg * oldTotalCount;
            return (oldTotalSum + newRating) / (double)newTotalCount;
        }

        private double CalculateNewRatingOnUpdate(double currentAvg, int totalCount, int oldRating, int newRating)
        {
            if (totalCount == 0) return 0;
            double totalCountDouble = (double)totalCount;
            double oldTotalSum = currentAvg * totalCountDouble;
            double newTotalSum = oldTotalSum - oldRating + newRating;
            return newTotalSum / totalCountDouble;
        }
    }
}