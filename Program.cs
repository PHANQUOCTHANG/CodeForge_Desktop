using CodeForge_Desktop.Business.Interfaces;
using CodeForge_Desktop.Business.Services;
using CodeForge_Desktop.DataAccess.Interfaces;
using CodeForge_Desktop.DataAccess.Repositories;
using CodeForge_Desktop.Presentation.Forms;
using CodeForge_Desktop.Presentation.Forms.Student;
using CodeForge_Desktop.Presentation.Forms.Admin;
using System;
using System.Windows.Forms;

namespace CodeForge_Desktop
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // --- Thiết lập Dependency Injection thủ công ---

            // 1. Đăng ký Repositories
            IUserRepository userRepository = new UserRepository();
            IEnrollmentRepository enrollmentRepository = new EnrollmentRepository();
            IProgressRepository progressRepository = new ProgressRepository();
            ICourseReviewRepository courseReviewRepository = new CourseReviewRepository();
            ICourseRepository courseRepository = new CourseRepository();

            
            // NEW: Enrollment Service (for checking enrollment & enrolling user)
            EnrollmentService enrollmentService = new EnrollmentService(enrollmentRepository, progressRepository);
            
            // NEW: Progress Service (for getting course progress percentage)
            ProgressService progressService = new ProgressService(progressRepository);
            
            // NEW: Course Review Service (for submitting & retrieving reviews)
            CourseReviewService courseReviewService = new CourseReviewService(courseReviewRepository, enrollmentRepository);

            Application.Run(new Login());
            //Application.Run(new MainFormAdmin());
        }
    }
}