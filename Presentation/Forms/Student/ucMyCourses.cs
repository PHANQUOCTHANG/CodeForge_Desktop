using CodeForge_Desktop.Business.DTOs;
using CodeForge_Desktop.Business.Helpers;
using CodeForge_Desktop.Business.Services;
using CodeForge_Desktop.DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CodeForge_Desktop.Presentation.Forms.Student.UserControls
{
    public partial class ucMyCourses : UserControl
    {
        private readonly CourseService _courseService;

        public ucMyCourses()
        {
            InitializeComponent();
            _courseService = new CourseService(new CourseRepository()); // Init Service

            this.Load += async (s, e) => {
                if (!DesignMode) await LoadMyCourses();
            };
        }

        private async Task LoadMyCourses()
        {
            var user = GlobalStore.user;
            if (user == null)
            {
                ShowMessage("Vui lòng đăng nhập để xem khóa học.");
                return;
            }

            try
            {
                // Lấy danh sách khóa học ĐÃ ENROLL
                var courses = await _courseService.GetEnrolledCoursesAsync(user.UserID);
                RenderCourses(courses);
            }
            catch (Exception ex)
            {
                ShowMessage("Lỗi tải dữ liệu: " + ex.Message);
            }
        }

        private void RenderCourses(List<CourseDto> courses)
        {
            flpCourses.Controls.Clear();

            if (courses == null || courses.Count == 0)
            {
                ShowMessage("Bạn chưa đăng ký khóa học nào.");
                return;
            }

            foreach (var course in courses)
            {
                flpCourses.Controls.Add(CreateCourseCard(course));
            }
        }

        private Control CreateCourseCard(CourseDto course)
        {
            // 1. Panel Container (Thẻ bài)
            var pnlCard = new Panel
            {
                Width = 280,
                Height = 320,
                BackColor = Color.White,
                Margin = new Padding(10),
                Cursor = Cursors.Hand
            };

            // Hiệu ứng Hover đơn giản
            pnlCard.MouseEnter += (s, e) => pnlCard.BackColor = Color.AliceBlue;
            pnlCard.MouseLeave += (s, e) => pnlCard.BackColor = Color.White;
            // Click vào thẻ -> Vào học
            pnlCard.Click += (s, e) => OpenLearningView(course.CourseID);

            // 2. Thumbnail
            var pbThumb = new PictureBox
            {
                Dock = DockStyle.Top,
                Height = 160,
                SizeMode = PictureBoxSizeMode.StretchImage,
                BackColor = Color.Gray
            };
            if (!string.IsNullOrEmpty(course.Thumbnail))
                try { pbThumb.LoadAsync(course.Thumbnail); } catch { }
            pbThumb.Click += (s, e) => OpenLearningView(course.CourseID);

            // 3. Title
            var lblTitle = new Label
            {
                Text = course.Title,
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                ForeColor = Color.Black,
                AutoSize = false,
                Height = 50,
                Dock = DockStyle.Top,
                TextAlign = ContentAlignment.MiddleLeft,
                Padding = new Padding(5, 5, 5, 0)
            };
            lblTitle.Click += (s, e) => OpenLearningView(course.CourseID);

            // 4. Progress Section
            var pnlProgress = new Panel { Dock = DockStyle.Bottom, Height = 60, Padding = new Padding(10) };

            var lblProgressText = new Label
            {
                Text = $"Hoàn thành: {course.Progress}%",
                Dock = DockStyle.Top,
                Font = new Font("Segoe UI", 9),
                ForeColor = Color.DimGray
            };

            var progressBar = new ProgressBar
            {
                Dock = DockStyle.Bottom,
                Height = 10,
                Maximum = 100,
                Value = Math.Min(100, Math.Max(0, (int)course.Progress))
            };

            // Nút "Học tiếp"
            var btnContinue = new Button
            {
                Text = "Học tiếp",
                BackColor = Color.FromArgb(164, 53, 240), // Tím
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Dock = DockStyle.Right,
                Width = 80,
                Cursor = Cursors.Hand
            };
            btnContinue.FlatAppearance.BorderSize = 0;
            btnContinue.Click += (s, e) => OpenLearningView(course.CourseID);

            // Layout Progress
            pnlProgress.Controls.Add(progressBar);
            pnlProgress.Controls.Add(lblProgressText);
            // Nếu muốn thêm nút "Học tiếp" vào card thì add vào đây, 
            // nhưng click vào card đã chuyển trang rồi nên có thể bỏ qua cho gọn.

            // Add Controls to Card
            pnlCard.Controls.Add(pnlProgress); // Add Bottom trước
            pnlCard.Controls.Add(lblTitle);    // Add Top sau
            pnlCard.Controls.Add(pbThumb);     // Add Top đầu tiên

            return pnlCard;
        }

        private void OpenLearningView(Guid courseId)
        {
            // Điều hướng sang trang học (ucCourseLearning)
            MainFormStudent.Instance?.NavigateTo(new ucCourseLearning(courseId));
        }

        private void ShowMessage(string msg)
        {
            flpCourses.Controls.Clear();
            var lbl = new Label
            {
                Text = msg,
                AutoSize = true,
                Font = new Font("Segoe UI", 12),
                ForeColor = Color.Gray,
                Margin = new Padding(20)
            };
            flpCourses.Controls.Add(lbl);
        }
    }
}