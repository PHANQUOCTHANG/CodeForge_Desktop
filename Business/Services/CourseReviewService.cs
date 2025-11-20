using CodeForge_Desktop.DataAccess.Entities;
using CodeForge_Desktop.DataAccess.Interfaces;
using CodeForge_Desktop.DataAccess.Repositories;
using System;
using System.Windows.Forms;
using CodeForge_Desktop.Business; // <-- Adjust namespace as needed
using CodeForge_Desktop.Business.Helpers; // <-- Correct namespace for GlobalStore
using System.Drawing;
using CodeForge_Desktop.Presentation.Forms.Student;
using CodeForge_Desktop.Presentation.Forms.Student.UserControls; // <-- Add this using directive

namespace CodeForge_Desktop.Business.Services
{
    public class CourseReviewService
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
                return _reviewRepository.Update(existingReview) > 0;
            }
            else
            {
                // Tạo đánh giá mới
                var review = new CourseReview
                {
                    UserID = userId,
                    CourseID = courseId,
                    Rating = rating,
                    Comment = comment
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

    public class ucCourseList : UserControl
    {
        // ... other members ...

        private Guid _selectedCourseId; // <-- Add this field
        private Button btnEnrollContinue; // <-- Add this field
        private DataGridView dgvCourses; // <-- Add this field

        private void UpdateCoursePreview(DataGridViewRow row)
        {
            if (row.Cells["CourseID"].Value == DBNull.Value) return;

            _selectedCourseId = (Guid)row.Cells["CourseID"].Value;
            // ... các dòng khác ...

            var enrollmentService = new EnrollmentService(
                new EnrollmentRepository(),
                new ProgressRepository());

            bool isEnrolled = enrollmentService.IsUserEnrolled(GlobalStore.user.UserID, _selectedCourseId);

            if (isEnrolled)
            {
                double progress = enrollmentService.GetEnrollmentProgress(
                    GlobalStore.user.UserID, _selectedCourseId);

                btnEnrollContinue.Text = $"▶️ Tiếp tục học ({progress:F0}%)";
                btnEnrollContinue.BackColor = Color.FromArgb(0, 177, 64);
                // Render progress bar ở đây
            }
            else
            {
                btnEnrollContinue.Text = "💰 Đăng ký";
                btnEnrollContinue.BackColor = Color.FromArgb(0, 120, 215);
            }
        }

        private void btnEnrollContinue_Click(object sender, EventArgs e)
        {
            if (_selectedCourseId == Guid.Empty) return;

            var enrollmentService = new EnrollmentService(
                new EnrollmentRepository(),
                new ProgressRepository());

            bool isEnrolled = enrollmentService.IsUserEnrolled(
                GlobalStore.user.UserID, _selectedCourseId);

            if (!isEnrolled)
            {
                bool success = enrollmentService.EnrollUserToCourse(
                    GlobalStore.user.UserID, _selectedCourseId);

                if (success)
                {
                    MessageBox.Show("Đăng ký khóa học thành công!", "Thành công",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    UpdateCoursePreview(dgvCourses.SelectedRows[0]);
                }
            }

            MainFormStudent.Instance?.NavigateTo(
                new ucCourseLearning(_selectedCourseId, new CourseRepository()));
        }
    }
}