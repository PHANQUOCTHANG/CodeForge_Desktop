using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;
using CodeForge_Desktop.Business.DTOs;
using CodeForge_Desktop.Business.Helpers; // Chứa GlobalStore, ProgressNotifier
using CodeForge_Desktop.Business.Services; // Chứa CourseService
using CodeForge_Desktop.DataAccess.Repositories; // Chứa CourseRepository

namespace CodeForge_Desktop.Presentation.Forms.Student.UserControls
{
    public partial class ucCourseList : UserControl
    {
        private readonly CourseService _courseService;
        private List<CourseDto> _allCourses; // Cache data để lọc tại client
        private System.Windows.Forms.Timer _searchTimer;

        // --- MÀU SẮC GIAO DIỆN ---
        private readonly Color clrTitle = Color.FromArgb(33, 37, 41);
        private readonly Color clrMeta = Color.FromArgb(108, 117, 125);
        private readonly Color clrPrimary = Color.FromArgb(0, 120, 215);
        private readonly Color clrGreen = Color.SeaGreen;

        // Constructor mặc định cho Designer
        public ucCourseList() : this(new CourseRepository()) { }

        // Constructor chính
        public ucCourseList(CourseRepository repo)
        {
            // Khởi tạo Service với Repository được truyền vào
            _courseService = new CourseService(repo);
            InitializeComponent();

            // Tối ưu render (Double Buffer)
            this.DoubleBuffered = true;
            SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint, true);
            this.UpdateStyles();

            // Timer tìm kiếm (Debounce)
            _searchTimer = new System.Windows.Forms.Timer { Interval = 400 };
            _searchTimer.Tick += async (s, e) => { _searchTimer.Stop(); await ApplyFilterAsync(); };

            // Sự kiện
            this.Load += async (s, e) => await OnLoadAsync();
            this.txtSearch.TextChanged += (s, e) => { _searchTimer.Stop(); _searchTimer.Start(); };
            this.cmbFilterLevel.SelectedIndexChanged += async (s, e) => { _searchTimer.Stop(); await ApplyFilterAsync(); };

            // Vẽ đường kẻ dưới Search Bar
            pnlSearchContainer.Paint += (s, e) => {
                using (var p = new Pen(Color.FromArgb(230, 230, 230)))
                    e.Graphics.DrawLine(p, 0, pnlSearchContainer.Height - 1, pnlSearchContainer.Width, pnlSearchContainer.Height - 1);
            };
        }

        private async Task OnLoadAsync()
        {
            ProgressNotifier.ProgressUpdated += OnProgressUpdated;
            cmbFilterLevel.SelectedIndex = 0; // "Tất cả level"
            await LoadCoursesAsync();
        }

        protected override void OnHandleDestroyed(EventArgs e)
        {
            ProgressNotifier.ProgressUpdated -= OnProgressUpdated;
            base.OnHandleDestroyed(e);
        }

        private void OnProgressUpdated(object sender, ProgressUpdatedEventArgs e)
        {
            // Sửa UserId -> UserID
            Guid currentUserID = GlobalStore.user?.UserID ?? Guid.Empty;
            if (e.UserId == currentUserID && this.IsHandleCreated)
            {
                // Reload lại để cập nhật tiến độ
                this.BeginInvoke(new Action(async () => await LoadCoursesAsync()));
            }
        }

        // =========================================================
        // 1. DATA LOADING (Dùng Service)
        // =========================================================
        private async Task LoadCoursesAsync()
        {
            try
            {
                // Gọi Service để lấy List<CourseDto>
                // Service đã map sẵn Entity -> DTO
                _allCourses = await _courseService.GetAllCoursesAsync();

                // Lọc và Render
                await ApplyFilterAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải dữ liệu: " + ex.Message);
            }
        }

        private async Task ApplyFilterAsync()
        {
            if (_allCourses == null) return;

            string keyword = txtSearch.Text.Trim();
            string level = cmbFilterLevel.SelectedItem?.ToString();
            if (level == "Tất cả level") level = null;

            // Lọc trên List<CourseDto> (Client-side)
            var filtered = _allCourses.Where(c =>
            {
                bool matchName = string.IsNullOrEmpty(keyword) || c.Title.IndexOf(keyword, StringComparison.OrdinalIgnoreCase) >= 0;
                bool matchLevel = string.IsNullOrEmpty(level) || string.Equals(c.Level, level, StringComparison.OrdinalIgnoreCase);
                return matchName && matchLevel;
            }).ToList();

            RenderCards(filtered);
            await Task.CompletedTask;
        }

        // =========================================================
        // 2. RENDER CARDS (UI)
        // =========================================================
        private void RenderCards(List<CourseDto> courses)
        {
            flpCourseGrid.SuspendLayout();
            flpCourseGrid.Controls.Clear();

            if (courses.Count == 0)
            {
                var lbl = new Label
                {
                    Text = "Không tìm thấy khóa học nào phù hợp.",
                    AutoSize = false,
                    Width = 400,
                    Height = 50,
                    TextAlign = ContentAlignment.MiddleCenter,
                    ForeColor = Color.Gray,
                    Font = new Font("Segoe UI", 12F)
                };
                flpCourseGrid.Controls.Add(lbl);
            }
            else
            {
                int w = 260; // Chiều rộng thẻ
                int h = 300; // Chiều cao thẻ
                foreach (var c in courses)
                {
                    flpCourseGrid.Controls.Add(CreateCourseCard(c, w, h));
                }
            }
            flpCourseGrid.ResumeLayout();
        }

        private Control CreateCourseCard(CourseDto dto, int w, int h)
        {
            var panel = new Panel
            {
                Width = w,
                Height = h,
                BackColor = Color.White,
                Margin = new Padding(15),
                Cursor = Cursors.Hand,
                Tag = dto
            };

            // Vẽ viền, bóng đổ & thanh màu level
            panel.Paint += (s, e) =>
            {
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                // Viền
                using (var path = GetRoundedPath(new Rectangle(0, 0, w - 1, h - 1), 8))
                using (var pen = new Pen(Color.FromArgb(220, 220, 220)))
                {
                    e.Graphics.DrawPath(pen, path);
                }

                // Chỉ thị màu Level (Trái)
                Color lvlColor = dto.Level == "Beginner" ? Color.LimeGreen : (dto.Level == "Advanced" ? Color.OrangeRed : Color.Gold);
                using (var brush = new SolidBrush(lvlColor)) e.Graphics.FillRectangle(brush, 0, 15, 4, 30);
            };

            // 1. Thumbnail (Hình ảnh)
            var pb = new PictureBox
            {
                Width = w - 2,
                Height = 140,
                Location = new Point(1, 1),
                SizeMode = PictureBoxSizeMode.Zoom,
                BackColor = Color.FromArgb(248, 248, 248)
            };
            // Load ảnh async
            if (!string.IsNullOrEmpty(dto.Thumbnail))
            {
                LoadImageAsync(pb, dto.Thumbnail);
            }
            else
            {
                pb.Image = CreatePlaceholderImage(dto.Language);
            }
            panel.Controls.Add(pb);

            // 2. Title
            var lblTitle = new Label
            {
                Text = dto.Title,
                Font = new Font("Segoe UI", 11F, FontStyle.Bold),
                ForeColor = clrTitle,
                AutoSize = false,
                Width = w - 20,
                Height = 45,
                Location = new Point(10, 150),
                TextAlign = ContentAlignment.MiddleLeft
            };
            panel.Controls.Add(lblTitle);

            // 3. Meta (Rating & Students)
            var lblMeta = new Label
            {
                Text = $"⭐ {dto.Rating:N1}  •  👥 {dto.TotalStudents}",
                Font = new Font("Segoe UI", 9F),
                ForeColor = clrMeta,
                AutoSize = true,
                Location = new Point(10, 200)
            };
            panel.Controls.Add(lblMeta);

            // 4. Footer (Price / Enrolled Status)
            var lblStatus = new Label
            {
                AutoSize = true,
                Location = new Point(10, 245),
                Font = new Font("Segoe UI", 10F, FontStyle.Bold)
            };

            // Nút hành động giả (Label)
            var btnAction = new Label
            {
                AutoSize = false,
                Size = new Size(80, 30),
                Location = new Point(w - 95, 240),
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                Cursor = Cursors.Hand
            };

            if (dto.IsEnrolled)
            {
                // Trạng thái: Đã đăng ký
                lblStatus.Text = "Đã sở hữu";
                lblStatus.ForeColor = clrGreen;

                btnAction.Text = "Vào học";
                btnAction.BackColor = Color.FromArgb(230, 255, 230);
                btnAction.ForeColor = clrGreen;

                // Thanh tiến độ mini
                var pnlBg = new Panel { Width = w, Height = 4, BackColor = Color.LightGray, Location = new Point(0, h - 4) };
                var pnlVal = new Panel { Width = (int)((w * dto.Progress) / 100.0), Height = 4, BackColor = clrGreen };
                pnlBg.Controls.Add(pnlVal);
                panel.Controls.Add(pnlBg);
            }
            else
            {
                // Trạng thái: Chưa mua
                lblStatus.Text = dto.Price == 0 ? "Miễn phí" : $"{dto.Price:N0} đ";
                lblStatus.ForeColor = clrPrimary;

                btnAction.Text = "Chi tiết";
                btnAction.BackColor = Color.FromArgb(230, 240, 255);
                btnAction.ForeColor = clrPrimary;
            }
            panel.Controls.Add(lblStatus);
            panel.Controls.Add(btnAction);

            // --- EVENTS ---
            void OnClick(object s, EventArgs e)
            {
                // Sử dụng CourseID (viết hoa)
                MainFormStudent.Instance?.NavigateTo(new ucCourseDetails(dto.CourseID));
            }
            void OnHover(object s, EventArgs e) => panel.BackColor = Color.FromArgb(250, 252, 255);
            void OnLeave(object s, EventArgs e) => panel.BackColor = Color.White;

            panel.Click += OnClick;
            pb.Click += OnClick;
            lblTitle.Click += OnClick;
            btnAction.Click += OnClick;

            panel.MouseEnter += OnHover;
            panel.MouseLeave += OnLeave;
            foreach (Control c in panel.Controls)
            {
                c.MouseEnter += OnHover;
                c.MouseLeave += OnLeave;
            }

            return panel;
        }

        // =========================================================
        // 3. HELPERS
        // =========================================================

        private async void LoadImageAsync(PictureBox pb, string url)
        {
            try
            {
                // Nếu là URL web
                if (Uri.IsWellFormedUriString(url, UriKind.Absolute))
                {
                    using (var client = new HttpClient())
                    {
                        var bytes = await client.GetByteArrayAsync(url);
                        using (var ms = new MemoryStream(bytes))
                        {
                            pb.Image = Image.FromStream(ms);
                        }
                    }
                }
                // Nếu là đường dẫn file
                else if (File.Exists(url))
                {
                    pb.Image = Image.FromFile(url);
                }
            }
            catch
            {
                // Lỗi load ảnh -> Giữ nguyên placeholder hoặc để trống
            }
        }

        private GraphicsPath GetRoundedPath(Rectangle rect, int radius)
        {
            GraphicsPath path = new GraphicsPath();
            float d = radius * 2;
            path.AddArc(rect.X, rect.Y, d, d, 180, 90);
            path.AddArc(rect.Right - d, rect.Y, d, d, 270, 90);
            path.AddArc(rect.Right - d, rect.Bottom - d, d, d, 0, 90);
            path.AddArc(rect.X, rect.Bottom - d, d, d, 90, 90);
            path.CloseFigure();
            return path;
        }

        private Image CreatePlaceholderImage(string lang)
        {
            var bmp = new Bitmap(260, 140);
            using (var g = Graphics.FromImage(bmp))
            {
                g.Clear(Color.FromArgb(240, 240, 240));
                Color c = lang?.Contains("C#") == true ? Color.Purple : (lang?.Contains("Python") == true ? Color.Green : Color.Orange);
                using (var b = new SolidBrush(c)) g.FillEllipse(b, 100, 40, 60, 60);

                string letter = string.IsNullOrEmpty(lang) ? "C" : lang.Substring(0, 1).ToUpper();
                TextRenderer.DrawText(g, letter, new Font("Segoe UI", 20, FontStyle.Bold), new Point(115, 55), Color.White);
            }
            return bmp;
        }
    }
}