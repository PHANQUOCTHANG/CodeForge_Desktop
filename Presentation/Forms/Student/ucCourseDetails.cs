using CodeForge_Desktop.Business.DTOs;
using CodeForge_Desktop.Business.Helpers;
using CodeForge_Desktop.Business.Services;
using CodeForge_Desktop.DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CodeForge_Desktop.Presentation.Forms.Student.UserControls
{
    public partial class ucCourseDetails : UserControl
    {
        private readonly Guid _courseID;
        private readonly CourseService _courseService;
        private readonly EnrollmentService _enrollmentService;
        private readonly ProgressService _progressService;
        private CourseDetailDto _courseDetail;

        // Constructor cho Designer
        public ucCourseDetails()
        {
            InitializeComponent();
        }

        // Constructor Runtime
        public ucCourseDetails(Guid courseId) : this()
        {
            _courseID = courseId;

            // Init Services
            var courseRepo = new CourseRepository();
            var enrollRepo = new EnrollmentRepository();
            var progressRepo = new ProgressRepository();

            _courseService = new CourseService(courseRepo);
            _enrollmentService = new EnrollmentService(enrollRepo, progressRepo);
            _progressService = new ProgressService(progressRepo);

            SetupEvents();
        }

        private void SetupEvents()
        {
            this.Load += async (s, e) => {
                if (!this.DesignMode) await LoadDataAsync();
            };

            btnBack.Click += (s, e) => MainFormStudent.Instance?.GoBack();
            btnEnroll.Click += BtnEnroll_Click;

            // Fix Resize: Nếu màn hình thay đổi thì chỉnh lại layout module
            flpCurriculum.SizeChanged += (s, e) => ResizeCurriculum();

            // QUAN TRỌNG: Khi click sang tab "Nội dung", vẽ lại ngay để tránh bị trắng trơn
            tabContent.SelectedIndexChanged += (s, e) => {
                if (tabContent.SelectedTab == tabCurriculum)
                {
                    ResizeCurriculum();
                }
            };
        }

        // Hàm này được gọi từ file Designer
        private void PnlPriceCard_Paint(object sender, PaintEventArgs e)
        {
            using (var p = new Pen(Color.LightGray))
            {
                e.Graphics.DrawRectangle(p, 0, 0, pnlPriceCard.Width - 1, pnlPriceCard.Height - 1);
            }
        }

        private async Task LoadDataAsync()
        {
            try
            {
                _courseDetail = await _courseService.GetCourseDetailAsync(_courseID);
                if (_courseDetail == null) return;

                // Bind Info
                lblTitle.Text = _courseDetail.Title;
                lblMeta.Text = $"⭐ {_courseDetail.Rating:F1} ({_courseDetail.TotalStudents} học viên)";
                lblPrice.Text = _courseDetail.Price == 0 ? "Miễn phí" : $"{_courseDetail.Price:N0} ₫";
                if (!string.IsNullOrEmpty(_courseDetail.Thumbnail)) try { pbThumbnail.LoadAsync(_courseDetail.Thumbnail); } catch { }
                wbOverview.DocumentText = _courseDetail.Description;

                // Render Curriculum
                RenderCurriculum(_courseDetail.Modules);

                // Update Enrollment (Chạy ngầm để không đơ UI)
                await UpdateEnrollmentUI();
            }
            catch (Exception ex) { MessageBox.Show("Lỗi: " + ex.Message); }
        }

        private async Task UpdateEnrollmentUI()
        {
            var user = GlobalStore.user;
            if (user == null) { SetupButtonState(false); return; }

            bool isEnrolled = false;
            double prog = 0;

            // Xử lý nặng (DB) trong luồng phụ
            await Task.Run(() =>
            {
                try
                {
                    isEnrolled = _enrollmentService.IsUserEnrolled(user.UserID, _courseID);
                    if (isEnrolled)
                    {
                        prog = _progressService.GetProgressPercentage(user.UserID, _courseID);
                    }
                }
                catch { }
            });

            SetupButtonState(isEnrolled);

            if (isEnrolled)
            {
                int val = (int)Math.Min(100, Math.Max(0, prog));
                pbProgress.Value = val;
                pbProgress.Visible = true;
            }
        }

        private void SetupButtonState(bool isEnrolled)
        {
            if (isEnrolled)
            {
                btnEnroll.Text = "Vào học ngay";
                btnEnroll.BackColor = Color.SeaGreen;
                pnlReviewInput.Visible = true;
            }
            else
            {
                btnEnroll.Text = "Mua ngay";
                btnEnroll.BackColor = Color.FromArgb(164, 53, 240);
                pbProgress.Visible = false;
                pnlReviewInput.Visible = false;
            }
        }

        private void BtnEnroll_Click(object sender, EventArgs e)
        {
            var user = GlobalStore.user;
            if (user == null) { MessageBox.Show("Vui lòng đăng nhập."); return; }

            if (btnEnroll.Text.Contains("Vào học"))
            {
                MainFormStudent.Instance?.NavigateTo(new ucCourseLearning(_courseID));
                return;
            }

            var confirm = MessageBox.Show($"Mua khóa học: {_courseDetail?.Title}?", "Xác nhận", MessageBoxButtons.YesNo);
            if (confirm == DialogResult.Yes)
            {
                bool success = _enrollmentService.EnrollUserToCourse(user.UserID, _courseID);
                if (success) { MessageBox.Show("Thành công!"); _ = UpdateEnrollmentUI(); }
                else MessageBox.Show("Thất bại.");
            }
        }

        // --- PHẦN UI CURRICULUM ---

        private void ResizeCurriculum()
        {
            int w = flpCurriculum.ClientSize.Width - 25;

            // CỰC KỲ QUAN TRỌNG: Nếu tab ẩn (w=0), ép lấy width mặc định 800 để control không bị mất
            if (w <= 0 && this.Parent != null) w = this.Parent.Width - 100;
            if (w <= 0) w = 800;

            flpCurriculum.SuspendLayout();
            foreach (Control c in flpCurriculum.Controls)
            {
                if (c is Panel p)
                {
                    p.Width = w;
                    foreach (Control child in p.Controls)
                    {
                        // Resize Header và List
                        if (child is Button || child is FlowLayoutPanel) child.Width = w;

                        // Resize từng nút bài học
                        if (child is FlowLayoutPanel flpLessons)
                        {
                            foreach (Control btn in flpLessons.Controls) btn.Width = w;
                        }
                    }
                }
            }
            flpCurriculum.ResumeLayout();
        }

        private void RenderCurriculum(List<ModuleDto> modules)
        {
            flpCurriculum.FlowDirection = FlowDirection.TopDown;
            flpCurriculum.WrapContents = false;
            flpCurriculum.AutoScroll = true;

            flpCurriculum.SuspendLayout();
            flpCurriculum.Controls.Clear();

            if (modules != null)
            {
                foreach (var mod in modules)
                {
                    flpCurriculum.Controls.Add(CreateModuleWidget(mod));
                }
            }

            // Gọi Resize ngay sau khi vẽ xong
            ResizeCurriculum();
            flpCurriculum.ResumeLayout();
        }

        private Control CreateModuleWidget(ModuleDto mod)
        {
            // Mặc định 800px ngay từ đầu
            int width = 800;

            var pnlContainer = new Panel
            {
                Width = width,
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink,
                BackColor = Color.White,
                Padding = new Padding(0, 0, 0, 5),
                Margin = new Padding(0, 0, 0, 10)
            };

            var pnlLessons = new FlowLayoutPanel
            {
                Dock = DockStyle.Top,
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink,
                FlowDirection = FlowDirection.TopDown,
                WrapContents = false,
                Visible = false,
                Width = width,
                Padding = new Padding(0),
                BackColor = Color.White
            };

            if (mod.Lessons != null)
            {
                foreach (var les in mod.Lessons)
                {
                    var btn = new Button
                    {
                        Text = $"      {(les.LessonType == "video" ? "▶" : "📄")}   {les.Title}",
                        TextAlign = ContentAlignment.MiddleLeft,
                        Width = width,
                        Height = 40,
                        FlatStyle = FlatStyle.Flat,
                        BackColor = Color.White,
                        ForeColor = Color.DimGray,
                        Cursor = Cursors.Hand,
                        Margin = new Padding(0)
                    };
                    btn.FlatAppearance.BorderSize = 0;
                    btn.Click += (s, e) => {
                        if (btnEnroll.Text.Contains("Vào học"))
                            MainFormStudent.Instance?.NavigateTo(new ucCourseLearning(_courseID));
                        else
                            MessageBox.Show("Vui lòng đăng ký khóa học.");
                    };
                    pnlLessons.Controls.Add(btn);
                }
            }

            var btnHeader = new Button
            {
                Text = $"  ▼  {mod.Title}",
                TextAlign = ContentAlignment.MiddleLeft,
                Dock = DockStyle.Top,
                Height = 50,
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.FromArgb(245, 247, 250),
                ForeColor = Color.Black,
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                Cursor = Cursors.Hand,
                Width = width
            };
            btnHeader.FlatAppearance.BorderSize = 0;

            btnHeader.Click += (s, e) => {
                pnlLessons.Visible = !pnlLessons.Visible;
                btnHeader.Text = (pnlLessons.Visible ? "  ▲  " : "  ▼  ") + $"  {mod.Title}";
            };

            pnlContainer.Controls.Add(pnlLessons);
            pnlContainer.Controls.Add(btnHeader);

            return pnlContainer;
        }
    }
}