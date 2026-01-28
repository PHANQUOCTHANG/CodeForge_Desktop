using CodeForge_Desktop.Business.DTOs;
using CodeForge_Desktop.Business.Helpers;
using CodeForge_Desktop.Business.Services;
using CodeForge_Desktop.Config;
using CodeForge_Desktop.DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
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

        // Label instance reused for progress text (avoid duplicates)
        private Label _lblProgressText;

        // Modern Color Palette
        private static class ModernColors
        {
            public static readonly Color Primary = Color.FromArgb(99, 102, 241);        // Indigo
            public static readonly Color PrimaryHover = Color.FromArgb(79, 82, 221);
            public static readonly Color Success = Color.FromArgb(16, 185, 129);        // Green
            public static readonly Color SuccessHover = Color.FromArgb(5, 150, 105);
            public static readonly Color Dark = Color.FromArgb(17, 24, 39);             // Gray-900
            public static readonly Color DarkLight = Color.FromArgb(31, 41, 55);        // Gray-800
            public static readonly Color Gray = Color.FromArgb(107, 114, 128);          // Gray-500
            public static readonly Color GrayLight = Color.FromArgb(243, 244, 246);     // Gray-100
            public static readonly Color GrayBorder = Color.FromArgb(229, 231, 235);    // Gray-200
            public static readonly Color White = Color.White;
            public static readonly Color Warning = Color.FromArgb(251, 191, 36);        // Amber
        }

        public ucCourseDetails()
        {
            InitializeComponent();
            ApplyModernStyling();

            // initialize reusable progress label
            _lblProgressText = new Label
            {
                Font = new Font("Segoe UI", 9, FontStyle.Regular),
                ForeColor = ModernColors.Gray,
                AutoSize = true,
                Visible = false
            };

            // Ensure progress bar configured
            try
            {
                pbProgress.Style = ProgressBarStyle.Continuous;
                pbProgress.Minimum = 0;
                pbProgress.Maximum = 100;
                pbProgress.Value = 0;
                pbProgress.Visible = false;
            }
            catch { /* designer may not have created pbProgress yet in some designer scenarios */ }
        }

        public ucCourseDetails(Guid courseId) : this()
        {
            _courseID = courseId;

            var courseRepo = new CourseRepository();
            var enrollRepo = new EnrollmentRepository();
            var progressRepo = new ProgressRepository();

            _courseService = new CourseService(courseRepo);
            _enrollmentService = new EnrollmentService(enrollRepo, progressRepo);
            _progressService = new ProgressService(progressRepo);

            SetupEvents();
        }

        private void ApplyModernStyling()
        {
            // Apply modern styling to all components
            this.BackColor = ModernColors.GrayLight;

            // Header styling
            pnlHeader.BackColor = ModernColors.Dark;
            lblTitle.Font = new Font("Segoe UI", 26, FontStyle.Bold);
            lblTitle.ForeColor = ModernColors.White;
            lblMeta.Font = new Font("Segoe UI", 11, FontStyle.Regular);
            lblMeta.ForeColor = ModernColors.Gray;

            // Back button modern style
            btnBack.FlatStyle = FlatStyle.Flat;
            btnBack.FlatAppearance.BorderSize = 0;
            btnBack.BackColor = Color.FromArgb(50, 55, 65);
            btnBack.ForeColor = ModernColors.White;
            btnBack.Font = new Font("Segoe UI", 10, FontStyle.Regular);
            btnBack.Cursor = Cursors.Hand;
            ApplyRoundedCorners(btnBack, 8);
            AddHoverEffect(btnBack, Color.FromArgb(60, 65, 75), Color.FromArgb(50, 55, 65));

            // Tab control modern style
            tabContent.Font = new Font("Segoe UI", 11, FontStyle.Regular);
            tabContent.ItemSize = new Size(140, 48);
            tabContent.Padding = new Point(20, 0);

            // Price card styling
            pnlPriceCard.BackColor = ModernColors.White;
            pnlPriceCard.BorderStyle = BorderStyle.None;
            ApplyShadow(pnlPriceCard);

            lblPrice.Font = new Font("Segoe UI", 28, FontStyle.Bold);
            lblPrice.ForeColor = ModernColors.Dark;

            // Enroll button modern style
            btnEnroll.FlatStyle = FlatStyle.Flat;
            btnEnroll.FlatAppearance.BorderSize = 0;
            btnEnroll.Font = new Font("Segoe UI", 13, FontStyle.Bold);
            btnEnroll.Cursor = Cursors.Hand;
            btnEnroll.Height = 56;
            ApplyRoundedCorners(btnEnroll, 12);

            // Progress bar modern style
            pbProgress.Height = 10;
            pbProgress.Style = ProgressBarStyle.Continuous;
            ApplyRoundedCorners(pbProgress, 5);

            // Reviews styling
            dgvReviews.BackgroundColor = ModernColors.White;
            dgvReviews.BorderStyle = BorderStyle.None;
            dgvReviews.GridColor = ModernColors.GrayBorder;
            dgvReviews.DefaultCellStyle.Font = new Font("Segoe UI", 10);
            dgvReviews.DefaultCellStyle.Padding = new Padding(10);
            dgvReviews.RowTemplate.Height = 80;
            dgvReviews.EnableHeadersVisualStyles = false;
            dgvReviews.ColumnHeadersDefaultCellStyle.BackColor = ModernColors.GrayLight;
            dgvReviews.ColumnHeadersDefaultCellStyle.ForeColor = ModernColors.Dark;
            dgvReviews.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 11, FontStyle.Bold);
            dgvReviews.ColumnHeadersHeight = 50;

            // Review input panel
            pnlReviewInput.BackColor = ModernColors.White;
            pnlReviewInput.Padding = new Padding(24);
            ApplyShadow(pnlReviewInput);

            // Review controls
            txtReviewComment.Font = new Font("Segoe UI", 11);
            txtReviewComment.BorderStyle = BorderStyle.FixedSingle;
            txtReviewComment.Padding = new Padding(12);

            cmbRating.Font = new Font("Segoe UI", 11);
            cmbRating.FlatStyle = FlatStyle.Flat;

            btnSubmitReview.FlatStyle = FlatStyle.Flat;
            btnSubmitReview.FlatAppearance.BorderSize = 0;
            btnSubmitReview.BackColor = ModernColors.Primary;
            btnSubmitReview.ForeColor = ModernColors.White;
            btnSubmitReview.Font = new Font("Segoe UI", 11, FontStyle.Bold);
            btnSubmitReview.Height = 44;
            btnSubmitReview.Cursor = Cursors.Hand;
            ApplyRoundedCorners(btnSubmitReview, 8);
            AddHoverEffect(btnSubmitReview, ModernColors.PrimaryHover, ModernColors.Primary);

            lblWriteReview.Font = new Font("Segoe UI", 12, FontStyle.Bold);
            lblWriteReview.ForeColor = ModernColors.Dark;

            // FlowLayoutPanel styling
            flpCurriculum.BackColor = ModernColors.White;
        }

        private void ApplyRoundedCorners(Control control, int radius)
        {
            control.Paint += (s, e) =>
            {
                var rect = control.ClientRectangle;
                using (var path = GetRoundedRectPath(rect, radius))
                using (var pen = new Pen(control.BackColor, 1))
                {
                    control.Region = new Region(path);
                }
            };
        }

        private void ApplyShadow(Panel panel)
        {
            panel.Paint += (s, e) =>
            {
                var rect = panel.ClientRectangle;
                rect.Inflate(-1, -1);

                using (var path = GetRoundedRectPath(rect, 12))
                using (var brush = new SolidBrush(panel.BackColor))
                {
                    e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

                    // Draw subtle shadow
                    for (int i = 0; i < 3; i++)
                    {
                        var shadowRect = rect;
                        shadowRect.Inflate(i, i);
                        shadowRect.Offset(0, i);
                        using (var shadowPath = GetRoundedRectPath(shadowRect, 12))
                        using (var shadowBrush = new SolidBrush(Color.FromArgb(10 - i * 3, 0, 0, 0)))
                        {
                            e.Graphics.FillPath(shadowBrush, shadowPath);
                        }
                    }

                    e.Graphics.FillPath(brush, path);
                }
            };
        }

        private GraphicsPath GetRoundedRectPath(Rectangle rect, int radius)
        {
            var path = new GraphicsPath();
            int diameter = radius * 2;

            path.AddArc(rect.X, rect.Y, diameter, diameter, 180, 90);
            path.AddArc(rect.Right - diameter, rect.Y, diameter, diameter, 270, 90);
            path.AddArc(rect.Right - diameter, rect.Bottom - diameter, diameter, diameter, 0, 90);
            path.AddArc(rect.X, rect.Bottom - diameter, diameter, diameter, 90, 90);
            path.CloseFigure();

            return path;
        }

        private void AddHoverEffect(Button button, Color hoverColor, Color normalColor)
        {
            button.MouseEnter += (s, e) =>
            {
                button.BackColor = hoverColor;
                button.Cursor = Cursors.Hand;
            };

            button.MouseLeave += (s, e) =>
            {
                button.BackColor = normalColor;
            };
        }

        private void SetupEvents()
        {
            this.Load += async (s, e) => {
                if (!this.DesignMode) await LoadDataAsync();
            };

            btnBack.Click += (s, e) => MainFormStudent.Instance?.GoBack();
            btnEnroll.Click += BtnEnroll_Click;
            btnSubmitReview.Click += async (s, e) => await BtnSubmitReview_Click();
            flpCurriculum.SizeChanged += (s, e) => ResizeCurriculum();

            tabContent.SelectedIndexChanged += (s, e) =>
            {
                if (tabContent.SelectedTab == tabCurriculum)
                {
                    if (flpCurriculum.Controls.Count == 0)
                    {
                        RenderCurriculum(_courseDetail?.Modules ?? new List<ModuleDto>());
                    }
                    ResizeCurriculum();
                }
            };
        }

        private async Task LoadDataAsync()
        {
            try
            {
                // Show loading state
                ShowLoadingState(true);

                _courseDetail = await _courseService.GetCourseDetailAsync(_courseID);
                if (_courseDetail == null)
                {
                    MessageBox.Show("Không tìm thấy khóa học.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                lblTitle.Text = _courseDetail.Title;
                lblMeta.Text = $"⭐ {_courseDetail.Rating:F1}  •  {_courseDetail.TotalStudents:N0} học viên  •  Cập nhật {DateTime.Now:MM/yyyy}";

                // Modern price display
                if (_courseDetail.Price == 0)
                {
                    lblPrice.Text = "Miễn phí";
                    lblPrice.ForeColor = ModernColors.Success;
                }
                else
                {
                    lblPrice.Text = $"{_courseDetail.Price:N0} ₫";
                    lblPrice.ForeColor = ModernColors.Dark;
                }

                if (!string.IsNullOrEmpty(_courseDetail.Thumbnail))
                {
                    try
                    {
                        await LoadThumbnailAsync(_courseDetail.Thumbnail);
                    }
                    catch
                    {
                        pbThumbnail.BackColor = ModernColors.GrayLight;
                    }
                }

                var html = BuildOverviewHtml(_courseDetail.Description ?? _courseDetail.Overview ?? "Chưa có mô tả");
                wbOverview.DocumentText = html;

                await UpdateEnrollmentUI();
                await LoadAndRenderReviewsAsync();

                ShowLoadingState(false);
            }
            catch (Exception ex)
            {
                ShowLoadingState(false);
                MessageBox.Show("Lỗi khi tải dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async Task LoadThumbnailAsync(string url)
        {
            await Task.Run(() =>
            {
                try
                {
                    pbThumbnail.LoadAsync(url);
                }
                catch { }
            });
        }

        private void ShowLoadingState(bool isLoading)
        {
            if (isLoading)
            {
                btnEnroll.Enabled = false;
                btnEnroll.Text = "Đang tải...";
            }
            else
            {
                btnEnroll.Enabled = true;
            }
        }

        private string BuildOverviewHtml(string contentHtml)
        {
            return $@"<!doctype html>
<html>
<head>
<meta charset='utf-8' />
<meta name='viewport' content='width=device-width, initial-scale=1' />
<style>
  body {{ 
    font-family: 'Segoe UI', -apple-system, BlinkMacSystemFont, sans-serif; 
    color: #111827; 
    margin: 24px; 
    line-height: 1.75; 
    max-width: 100%; 
    font-size: 16px;
  }}
  img {{ 
    max-width: 100%; 
    height: auto; 
    border-radius: 12px; 
    box-shadow: 0 4px 6px rgba(0,0,0,0.1);
    margin: 20px 0;
  }}
  pre {{ 
    background: #f3f4f6; 
    padding: 16px; 
    border-radius: 8px; 
    overflow: auto; 
    border-left: 4px solid #6366f1;
    font-size: 14px;
  }}
  h1, h2, h3 {{ 
    color: #111827;
    font-weight: 700;
    margin-top: 32px;
    margin-bottom: 16px;
    line-height: 1.3;
  }}
  h1 {{ font-size: 32px; }}
  h2 {{ font-size: 24px; }}
  h3 {{ font-size: 20px; }}
  p {{
    margin-bottom: 16px;
  }}
  a {{ 
    color: #6366f1; 
    text-decoration: none;
    border-bottom: 1px solid transparent;
    transition: border-color 0.2s;
  }}
  a:hover {{
    border-bottom-color: #6366f1;
  }}
  ul, ol {{
    padding-left: 28px;
    margin-bottom: 20px;
  }}
  li {{
    margin-bottom: 12px;
    line-height: 1.7;
  }}
  blockquote {{
    border-left: 4px solid #e5e7eb;
    padding-left: 20px;
    margin: 24px 0;
    color: #6b7280;
    font-style: italic;
  }}
  code {{
    background: #f3f4f6;
    padding: 2px 6px;
    border-radius: 4px;
    font-size: 14px;
    color: #dc2626;
  }}
</style>
</head>
<body>{contentHtml}</body>
</html>";
        }

        private async Task UpdateEnrollmentUI()
        {
            var user = GlobalStore.user;
            if (user == null)
            {
                SetupButtonState(false);
                // ensure progress hidden
                SafeInvoke(() =>
                {
                    pbProgress.Visible = false;
                    if (pnlPriceCard.Controls.Contains(_lblProgressText))
                        pnlPriceCard.Controls.Remove(_lblProgressText);
                });
                return;
            }

            bool isEnrolled = false;
            double prog = 0;

            await Task.Run(() =>
            {
                try
                {
                    isEnrolled = _enrollmentService.IsUserEnrolled(user.UserID, _courseID);
                    if (isEnrolled)
                    {
                        // use existing synchronous helper; it may call repository
                        prog = _progress_service_wrapper(user.UserID, _courseID);
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"Error checking enrollment: {ex.Message}");
                }
            });

            // Update UI on UI thread
            SafeInvoke(() =>
            {
                SetupButtonState(isEnrolled);

                if (isEnrolled)
                {
                    int val = (int)Math.Min(100, Math.Max(0, prog));
                    pbProgress.Value = val;
                    pbProgress.Visible = true;

                    // Update or add progress label (single instance)
                    _lblProgressText.Text = $"{val}% hoàn thành";
                    // Place label under progress bar; compute location safely
                    _lblProgressText.Location = new Point(pbProgress.Left + 4, pbProgress.Bottom + 8);
                    _lblProgressText.Visible = true;

                    if (!pnlPriceCard.Controls.Contains(_lblProgressText))
                    {
                        // add after pbProgress so it appears visually below
                        pnlPriceCard.Controls.Add(_lblProgressText);
                        _lblProgressText.BringToFront();
                    }
                }
                else
                {
                    pbProgress.Visible = false;
                    _lblProgressText.Visible = false;
                    if (pnlPriceCard.Controls.Contains(_lblProgressText))
                    {
                        pnlPriceCard.Controls.Remove(_lblProgressText);
                    }
                }
            });
        }

        // Helper wrapper to call progress service safely (keeps existing sync signature)
        private double _progress_service_wrapper(Guid userId, Guid courseId)
        {
            try
            {
                // ProgressService exposes GetProgressPercentage (sync)
                return _progressService.GetProgressPercentage(userId, courseId);
            }
            catch
            {
                return 0;
            }
        }

        // Public helper so other code can refresh enrollment/progress (call after payment/enroll)
        public async Task RefreshEnrollmentAndProgressAsync()
        {
            await UpdateEnrollmentUI();
        }

        // Safe invoke helper
        private void SafeInvoke(Action action)
        {
            if (this.IsHandleCreated && !this.Disposing && !this.IsDisposed)
            {
                if (this.InvokeRequired)
                    this.Invoke(action);
                else
                    action();
            }
        }

        private void SetupButtonState(bool isEnrolled)
        {
            if (isEnrolled)
            {
                btnEnroll.Text = "Vào học ngay →";
                btnEnroll.BackColor = ModernColors.Success;
                AddHoverEffect(btnEnroll, ModernColors.SuccessHover, ModernColors.Success);
                pnlReviewInput.Visible = true;
            }
            else
            {
                btnEnroll.Text = _courseDetail?.Price == 0 ? "Đăng ký miễn phí" : "Mua ngay";
                btnEnroll.BackColor = ModernColors.Primary;
                AddHoverEffect(btnEnroll, ModernColors.PrimaryHover, ModernColors.Primary);
                pbProgress.Visible = false;
                pnlReviewInput.Visible = false;
            }
        }

        private async void BtnEnroll_Click(object sender, EventArgs e)
        {
            var user = GlobalStore.user;
            if (user == null)
            {
                MessageBox.Show("Vui lòng đăng nhập để tiếp tục.", "Yêu cầu đăng nhập",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (_courseDetail == null)
            {
                MessageBox.Show("Không tìm thấy thông tin khóa học.", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var enrollService = new EnrollmentService(new EnrollmentRepository(), new ProgressRepository());
            if (enrollService.IsUserEnrolled(user.UserID, _courseID))
            {
                MainFormStudent.Instance?.NavigateTo(new ucCourseLearning(_courseID));
                return;
            }

            if (_courseDetail.Price <= 0m)
            {
                var confirm = MessageBox.Show(
                    $"Đăng ký khóa học miễn phí \"{_courseDetail.Title}\"?",
                    "Xác nhận",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (confirm == DialogResult.Yes)
                {
                    btnEnroll.Enabled = false;
                    btnEnroll.Text = "Đang xử lý...";

                    bool ok = enrollService.EnrollUserToCourse(user.UserID, _courseID);
                    if (ok)
                    {
                        MessageBox.Show("Đăng ký thành công!", "Thành công",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        MainFormStudent.Instance?.NavigateTo(new ucCourseLearning(_courseID));
                    }
                    else
                    {
                        MessageBox.Show("Đăng ký thất bại. Vui lòng thử lại.", "Lỗi",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        btnEnroll.Enabled = true;
                        btnEnroll.Text = "Đăng ký miễn phí";
                    }
                }
                return;
            }

            var amount = _courseDetail.Price;
            var res = MessageBox.Show(
                $"Thanh toán {amount:N0} ₫ cho khóa học \"{_courseDetail.Title}\"?",
                "Xác nhận thanh toán",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (res != DialogResult.Yes) return;

            try
            {
                btnEnroll.Enabled = false;
                btnEnroll.Text = "Đang xử lý thanh toán...";

                bool paid = await CodeForge_Desktop.Presentation.Helpers.PaymentHelper
                    .StartLocalVietQrSimulationAsync(_courseID, amount);

                if (paid)
                {
                    bool enrolled = enrollService.EnrollUserToCourse(user.UserID, _courseID);

                    if (enrolled)
                    {
                        MessageBox.Show("Thanh toán thành công! Chào mừng bạn đến với khóa học.",
                            "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        MainFormStudent.Instance?.NavigateTo(new ucCourseLearning(_courseID));
                    }
                    else
                    {
                        MessageBox.Show(
                            "Thanh toán đã được thực hiện nhưng không thể cập nhật đăng ký. Vui lòng liên hệ hỗ trợ.",
                            "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Thanh toán chưa hoàn tất.", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btnEnroll.Enabled = true;
                    btnEnroll.Text = "Mua ngay";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi khởi tạo thanh toán: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnEnroll.Enabled = true;
                btnEnroll.Text = "Mua ngay";
            }
        }

        // ========== CURRICULUM UI - MODERN VERSION ==========

        private void ResizeCurriculum()
        {
            int w = flpCurriculum.ClientSize.Width - 25;

            if (w <= 0 && this.Parent != null)
                w = this.Parent.Width - 100;
            if (w <= 0)
                w = 800;

            flpCurriculum.SuspendLayout();

            foreach (Control c in flpCurriculum.Controls)
            {
                if (c is Panel pnlModule)
                {
                    pnlModule.Width = w;

                    foreach (Control child in pnlModule.Controls)
                    {
                        if (child is Button btnHeader)
                        {
                            btnHeader.Width = w;
                        }
                        else if (child is FlowLayoutPanel flpLessons)
                        {
                            flpLessons.Width = w;

                            foreach (Control btnLesson in flpLessons.Controls)
                            {
                                if (btnLesson is Button)
                                {
                                    btnLesson.Width = w - 10;
                                }
                            }
                        }
                    }
                }
            }

            flpCurriculum.ResumeLayout(true);
            flpCurriculum.Refresh();
        }

        private void RenderCurriculum(List<ModuleDto> modules)
        {
            flpCurriculum.SuspendLayout();
            flpCurriculum.Controls.Clear();

            if (modules != null && modules.Count > 0)
            {
                foreach (var mod in modules)
                {
                    var widget = CreateModernModuleWidget(mod);
                    flpCurriculum.Controls.Add(widget);
                }
            }
            else
            {
                var lblEmpty = new Label
                {
                    Text = "📚 Nội dung khóa học đang được cập nhật",
                    AutoSize = true,
                    Padding = new Padding(24),
                    Font = new Font("Segoe UI", 13, FontStyle.Regular),
                    ForeColor = ModernColors.Gray
                };
                flpCurriculum.Controls.Add(lblEmpty);
            }

            flpCurriculum.ResumeLayout(true);
            ResizeCurriculum();
            flpCurriculum.Refresh();
        }

        private Control CreateModernModuleWidget(ModuleDto mod)
        {
            int width = flpCurriculum.ClientSize.Width;
            if (width <= 0)
                width = tabCurriculum.Width - 40;

            // Modern container with shadow
            var pnlContainer = new Panel
            {
                Height = 64,
                Width = width,
                BackColor = ModernColors.White,
                Margin = new Padding(0, 0, 0, 16)
            };

            // Apply rounded corners and shadow
            pnlContainer.Paint += (s, e) =>
            {
                var rect = pnlContainer.ClientRectangle;
                using (var path = GetRoundedRectPath(rect, 10))
                using (var brush = new SolidBrush(pnlContainer.BackColor))
                {
                    e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

                    // Subtle shadow
                    var shadowRect = rect;
                    shadowRect.Inflate(1, 1);
                    shadowRect.Offset(0, 2);
                    using (var shadowPath = GetRoundedRectPath(shadowRect, 10))
                    using (var shadowBrush = new SolidBrush(Color.FromArgb(15, 0, 0, 0)))
                    {
                        e.Graphics.FillPath(shadowBrush, shadowPath);
                    }

                    e.Graphics.FillPath(brush, path);

                    // Border
                    using (var pen = new Pen(ModernColors.GrayBorder, 1))
                    {
                        e.Graphics.DrawPath(pen, path);
                    }
                }
            };

            // Modern header button
            var btnHeader = new Button
            {
                Text = $"  ▶  {mod.Title}",
                TextAlign = ContentAlignment.MiddleLeft,
                Dock = DockStyle.Top,
                Height = 60,
                FlatStyle = FlatStyle.Flat,
                BackColor = ModernColors.White,
                ForeColor = ModernColors.Dark,
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            btnHeader.FlatAppearance.BorderSize = 0;

            // Lessons panel
            var pnlLessons = new FlowLayoutPanel
            {
                Dock = DockStyle.None,
                AutoSize = true,
                FlowDirection = FlowDirection.TopDown,
                WrapContents = false,
                Visible = false,
                Location = new Point(0, btnHeader.Bottom),
                Width = width,
                BackColor = ModernColors.GrayLight,
                Padding = new Padding(0, 8, 0, 8)
            };

            // Add lesson buttons
            if (mod.Lessons != null && mod.Lessons.Count > 0)
            {
                foreach (var les in mod.Lessons)
                {
                    var lessonIcon = les.LessonType?.ToLower() == "video" ? "▶" : "📄";

                    var btn = new Button
                    {
                        Text = $"      {lessonIcon}   {les.Title}",
                        TextAlign = ContentAlignment.MiddleLeft,
                        Width = pnlLessons.ClientSize.Width - 16,
                        Height = 48,
                        FlatStyle = FlatStyle.Flat,
                        BackColor = ModernColors.White,
                        ForeColor = ModernColors.Dark,
                        Cursor = Cursors.Hand,
                        Margin = new Padding(8, 4, 8, 4),
                        Tag = les,
                        Font = new Font("Segoe UI", 10, FontStyle.Regular)
                    };
                    btn.FlatAppearance.BorderSize = 0;
                    btn.Click += (s, e) => OnLessonButtonClicked(les);

                    // Modern hover effect
                    btn.MouseEnter += (s, e) =>
                    {
                        btn.BackColor = ModernColors.GrayLight;
                        btn.ForeColor = ModernColors.Primary;
                    };
                    btn.MouseLeave += (s, e) =>
                    {
                        btn.BackColor = ModernColors.White;
                        btn.ForeColor = ModernColors.Dark;
                    };

                    // Rounded corners for lesson buttons
                    ApplyRoundedCorners(btn, 6);

                    pnlLessons.Controls.Add(btn);
                }
            }

            // Toggle collapse/expand with animation
            btnHeader.Click += (s, e) =>
            {
                pnlLessons.Visible = !pnlLessons.Visible;

                if (pnlLessons.Visible)
                {
                    pnlLessons.PerformLayout();
                    pnlContainer.Height = btnHeader.Height + pnlLessons.PreferredSize.Height + 16;
                    btnHeader.Text = "  ▼  " + mod.Title;
                    btnHeader.ForeColor = ModernColors.Primary;
                }
                else
                {
                    pnlContainer.Height = 64;
                    btnHeader.Text = "  ▶  " + mod.Title;
                    btnHeader.ForeColor = ModernColors.Dark;
                }

                flpCurriculum.PerformLayout();
            };

            pnlLessons.SizeChanged += (s, e) =>
            {
                foreach (Control c in pnlLessons.Controls)
                {
                    c.Width = pnlLessons.ClientSize.Width - 16;
                }
            };

            // Hover effect for header
            btnHeader.MouseEnter += (s, e) =>
            {
                if (!pnlLessons.Visible)
                {
                    btnHeader.BackColor = ModernColors.GrayLight;
                }
            };
            btnHeader.MouseLeave += (s, e) =>
            {
                if (!pnlLessons.Visible)
                {
                    btnHeader.BackColor = ModernColors.White;
                }
            };

            pnlContainer.Controls.Add(pnlLessons);
            pnlContainer.Controls.Add(btnHeader);

            return pnlContainer;
        }

        private void OnLessonButtonClicked(LessonDto les)
        {
            if (btnEnroll.Text.Contains("Vào học"))
            {
                try
                {
                    MainFormStudent.Instance?.NavigateTo(new ucCourseLearning(_courseID, les.LessonID));
                }
                catch
                {
                    MainFormStudent.Instance?.NavigateTo(new ucCourseLearning(_courseID));
                }
            }
            else
            {
                MessageBox.Show("Vui lòng đăng ký khóa học để truy cập bài học.", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        // ========== REVIEWS ==========

        private async Task LoadAndRenderReviewsAsync()
        {
            try
            {
                var reviews = await Task.Run(() => FetchReviewsFromDb(_courseID));
                RenderModernReviewsGrid(reviews);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error loading reviews: {ex.Message}");
            }
        }

        private List<(Guid UserId, string UserName, int Rating, string Comment, DateTime CreatedAt)>
            FetchReviewsFromDb(Guid courseId)
        {
            var list = new List<(Guid, string, int, string, DateTime)>();
            try
            {
                var dt = DbContext.Query(@"
SELECT r.UserID, u.Username, ISNULL(r.Rating,0) AS Rating, 
       ISNULL(r.Comment,'') AS Comment, ISNULL(r.CreatedAt, GETDATE()) AS CreatedAt
FROM CourseReviews r
LEFT JOIN [Users] u ON u.UserID = r.UserID
WHERE r.CourseID = @C
ORDER BY r.CreatedAt DESC",
                    new System.Data.SqlClient.SqlParameter("@C", courseId)
                );

                if (dt != null)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        var uid = row["UserID"] != DBNull.Value ? (Guid)row["UserID"] : Guid.Empty;
                        var uname = row.Table.Columns.Contains("Username") && row["Username"] != DBNull.Value
                            ? row["Username"].ToString()
                            : "Người dùng";
                        var rating = row["Rating"] != DBNull.Value ? Convert.ToInt32(row["Rating"]) : 0;
                        var comment = row["Comment"] != DBNull.Value ? row["Comment"].ToString() : "";
                        var created = row["CreatedAt"] != DBNull.Value ?
                            Convert.ToDateTime(row["CreatedAt"]) : DateTime.Now;

                        list.Add((uid, uname, rating, comment, created));
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error fetching reviews: {ex.Message}");
            }
            return list;
        }

        private void RenderModernReviewsGrid(
            List<(Guid UserId, string UserName, int Rating, string Comment, DateTime CreatedAt)> reviews)
        {
            try
            {
                dgvReviews.Columns.Clear();
                dgvReviews.Rows.Clear();

                dgvReviews.Columns.Add("colUser", "Người dùng");
                dgvReviews.Columns.Add("colRating", "Đánh giá");
                dgvReviews.Columns.Add("colComment", "Nhận xét");
                dgvReviews.Columns.Add("colDate", "Ngày");

                dgvReviews.Columns["colUser"].Width = 150;
                dgvReviews.Columns["colRating"].Width = 140;
                dgvReviews.Columns["colComment"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dgvReviews.Columns["colDate"].Width = 160;

                foreach (var r in reviews)
                {
                    var stars = new string('⭐', r.Rating);
                    var timeAgo = GetTimeAgo(r.CreatedAt);
                    dgvReviews.Rows.Add(r.UserName, $"{stars} {r.Rating}/5", r.Comment, timeAgo);
                }

                dgvReviews.AlternatingRowsDefaultCellStyle.BackColor = ModernColors.GrayLight;
                dgvReviews.DefaultCellStyle.SelectionBackColor = Color.FromArgb(230, 230, 250);
                dgvReviews.DefaultCellStyle.SelectionForeColor = ModernColors.Dark;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error rendering reviews: {ex.Message}");
            }
        }

        private string GetTimeAgo(DateTime date)
        {
            var span = DateTime.Now - date;

            if (span.TotalDays > 365)
                return $"{(int)(span.TotalDays / 365)} năm trước";
            if (span.TotalDays > 30)
                return $"{(int)(span.TotalDays / 30)} tháng trước";
            if (span.TotalDays > 1)
                return $"{(int)span.TotalDays} ngày trước";
            if (span.TotalHours > 1)
                return $"{(int)span.TotalHours} giờ trước";
            if (span.TotalMinutes > 1)
                return $"{(int)span.TotalMinutes} phút trước";

            return "Vừa xong";
        }

        private async Task BtnSubmitReview_Click()
        {
            var user = GlobalStore.user;
            if (user == null)
            {
                MessageBox.Show("Vui lòng đăng nhập để đánh giá.", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            bool enrolled = false;
            try
            {
                enrolled = _enrollmentService.IsUserEnrolled(user.UserID, _courseID);
            }
            catch { }

            if (!enrolled)
            {
                MessageBox.Show("Bạn phải đăng ký khóa học để đánh giá.", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (cmbRating.SelectedIndex < 0)
            {
                MessageBox.Show("Vui lòng chọn số sao đánh giá.", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var rating = 5 - cmbRating.SelectedIndex;
            var comment = txtReviewComment.Text?.Trim() ?? "";

            if (string.IsNullOrWhiteSpace(comment))
            {
                MessageBox.Show("Vui lòng nhập nhận xét của bạn.", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                btnSubmitReview.Enabled = false;
                btnSubmitReview.Text = "Đang gửi...";

                int affected = DbContext.Execute(@"
INSERT INTO CourseReviews (CourseID, UserID, Rating, Comment, CreatedAt)
VALUES (@C, @U, @R, @Comment, @Now)",
                    new System.Data.SqlClient.SqlParameter("@C", _courseID),
                    new System.Data.SqlClient.SqlParameter("@U", user.UserID),
                    new System.Data.SqlClient.SqlParameter("@R", rating),
                    new System.Data.SqlClient.SqlParameter("@Comment", comment),
                    new System.Data.SqlClient.SqlParameter("@Now", DateTime.Now)
                );

                if (affected > 0)
                {
                    MessageBox.Show("Cảm ơn đánh giá của bạn! 🎉", "Thành công",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtReviewComment.Text = "";
                    cmbRating.SelectedIndex = -1;
                    await LoadAndRenderReviewsAsync();
                }
                else
                {
                    MessageBox.Show("Gửi đánh giá thất bại. Vui lòng thử lại.", "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi gửi đánh giá: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnSubmitReview.Enabled = true;
                btnSubmitReview.Text = "Gửi";
            }
        }
    }
}