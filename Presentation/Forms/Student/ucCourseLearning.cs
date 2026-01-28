using CodeForge_Desktop.Business.DTOs;
using CodeForge_Desktop.Business.Helpers;
using CodeForge_Desktop.Business.Services;
using CodeForge_Desktop.DataAccess.Entities;
using CodeForge_Desktop.DataAccess.Repositories;
using Microsoft.Web.WebView2.WinForms;
using Microsoft.Web.WebView2.Core;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace CodeForge_Desktop.Presentation.Forms.Student.UserControls
{
    public partial class ucCourseLearning : UserControl
    {
        private readonly Guid _courseID;
        private Guid _courseId;
        private readonly CourseService _courseService;
        private readonly ProgressService _progressService;
        private CourseDetailDto _courseData;
        private LessonDto _currentLesson;
        private List<LessonDto> _flatLessonList = new List<LessonDto>();
        private WebView2 _currentWebView;
        private CourseRepository _courseRepository;

        private Dictionary<Guid, int> _currentQuizSelections = new Dictionary<Guid, int>();
        private List<GroupBox> _currentQuizQuestionBoxes = new List<GroupBox>();
        private Panel _quizPanelContainer;
        private Guid _initialLessonId;

        // ============= ENHANCED DESIGN SYSTEM =============
        private static class Colors
        {
            // Primary Colors - Modern Blue
            public static readonly Color Primary = Color.FromArgb(59, 130, 246);        // Blue-500
            public static readonly Color PrimaryHover = Color.FromArgb(37, 99, 235);    // Blue-600
            public static readonly Color PrimaryLight = Color.FromArgb(239, 246, 255);  // Blue-50
            public static readonly Color PrimaryDark = Color.FromArgb(30, 58, 138);     // Blue-900

            // Success Colors
            public static readonly Color Success = Color.FromArgb(34, 197, 94);         // Green-500
            public static readonly Color SuccessHover = Color.FromArgb(22, 163, 74);    // Green-600
            public static readonly Color SuccessLight = Color.FromArgb(240, 253, 244);  // Green-50
            public static readonly Color SuccessText = Color.FromArgb(21, 128, 61);     // Green-700

            // Error Colors
            public static readonly Color Error = Color.FromArgb(239, 68, 68);           // Red-500
            public static readonly Color ErrorLight = Color.FromArgb(254, 242, 242);    // Red-50
            public static readonly Color ErrorText = Color.FromArgb(127, 29, 29);       // Red-900

            // Warning Colors
            public static readonly Color Warning = Color.FromArgb(245, 158, 11);        // Amber-500
            public static readonly Color WarningLight = Color.FromArgb(254, 243, 199);  // Amber-100
            public static readonly Color WarningText = Color.FromArgb(120, 53, 15);     // Amber-900

            // Neutral Colors
            public static readonly Color Gray50 = Color.FromArgb(249, 250, 251);
            public static readonly Color Gray100 = Color.FromArgb(243, 244, 246);
            public static readonly Color Gray200 = Color.FromArgb(229, 231, 235);
            public static readonly Color Gray300 = Color.FromArgb(209, 213, 219);
            public static readonly Color Gray400 = Color.FromArgb(156, 163, 175);
            public static readonly Color Gray500 = Color.FromArgb(107, 114, 128);
            public static readonly Color Gray600 = Color.FromArgb(75, 85, 99);
            public static readonly Color Gray700 = Color.FromArgb(55, 65, 81);
            public static readonly Color Gray800 = Color.FromArgb(31, 41, 55);
            public static readonly Color Gray900 = Color.FromArgb(17, 24, 39);

            // Background
            public static readonly Color Background = Color.White;
            public static readonly Color BackgroundAlt = Gray50;
            public static readonly Color Surface = Color.FromArgb(248, 250, 252);

            // Text
            public static readonly Color TextPrimary = Gray900;
            public static readonly Color TextSecondary = Gray600;
            public static readonly Color TextTertiary = Gray500;
            public static readonly Color TextDisabled = Gray400;
        }

        // ============= CONSTRUCTORS =============
        public ucCourseLearning(Guid courseId) : this(courseId, Guid.Empty) { }

        public ucCourseLearning(Guid courseId, Guid initialLessonId)
        {
            _courseID = courseId;
            _courseId = courseId;
            _initialLessonId = initialLessonId;
            _courseRepository = new CourseRepository();
            _courseService = new CourseService(_courseRepository);
            _progressService = new ProgressService(new ProgressRepository());

            InitializeComponent();
            ApplyModernDesign();
            WireEvents();
            InitTreeImageList();
        }

        public ucCourseLearning(Guid courseId, CourseRepository courseRepository)
            : this(courseId, courseRepository, Guid.Empty) { }

        public ucCourseLearning(Guid courseId, CourseRepository courseRepository, Guid initialLessonId)
        {
            _courseID = courseId;
            _courseId = courseId;
            _initialLessonId = initialLessonId;
            _courseRepository = courseRepository ?? new CourseRepository();
            _courseService = new CourseService(_courseRepository);
            _progressService = new ProgressService(new ProgressRepository());

            InitializeComponent();
            ApplyModernDesign();
            WireEvents();
            InitTreeImageList();
        }

        // ============= ENHANCED DESIGN APPLICATION =============
        private void ApplyModernDesign()
        {
            // Main background
            this.BackColor = Colors.Surface;

            // ===== TOP BAR - Enhanced with better hierarchy =====
            pnlTopBar.BackColor = Colors.Background;
            pnlTopBar.Height = 80;
            pnlTopBar.Padding = new Padding(24, 16, 24, 16);
            AddShadow(pnlTopBar, 3, 8);

            // Back button - More prominent
            StyleButton(btnBack, Colors.Gray100, Colors.Gray700, 8, false);
            btnBack.FlatAppearance.MouseOverBackColor = Colors.Gray200;
            btnBack.Padding = new Padding(16, 10, 16, 10);
            btnBack.Font = new Font("Segoe UI", 9.5F, FontStyle.Regular);

            // Course title - Better typography
            lblCourseTitle.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblCourseTitle.ForeColor = Colors.TextPrimary;
            lblCourseTitle.AutoSize = false;
            lblCourseTitle.Width = 500;
            lblCourseTitle.TextAlign = ContentAlignment.MiddleLeft;

            // Progress label - Better visibility
            lblProgress.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblProgress.ForeColor = Colors.Primary;
            lblProgress.AutoSize = false;
            lblProgress.Width = 80;
            lblProgress.TextAlign = ContentAlignment.MiddleRight;

            // Progress bar - Modern rounded design
            pbProgress.Height = 10;
            pbProgress.ForeColor = Colors.Success;
            CustomProgressBar(pbProgress);

            // ===== NAVIGATION PANEL - Enhanced spacing and contrast =====
            pnlNavButtons.BackColor = Colors.Background;
            pnlNavButtons.Padding = new Padding(24, 16, 24, 16);
            pnlNavButtons.Height = 80;
            AddTopBorder(pnlNavButtons, Colors.Gray200, 1);

            // Navigation buttons with better sizing
            StyleButton(btnPrev, Colors.Gray100, Colors.Gray700, 8, false);
            btnPrev.Padding = new Padding(20, 12, 20, 12);
            btnPrev.Font = new Font("Segoe UI", 10F, FontStyle.Regular);
            btnPrev.FlatAppearance.MouseOverBackColor = Colors.Gray200;

            StyleButton(btnNext, Colors.Primary, Color.White, 8, true);
            btnNext.Padding = new Padding(20, 12, 20, 12);
            btnNext.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnNext.FlatAppearance.MouseOverBackColor = Colors.PrimaryHover;

            StyleButton(btnMarkCompleted, Colors.Success, Color.White, 8, true);
            btnMarkCompleted.Padding = new Padding(20, 12, 20, 12);
            btnMarkCompleted.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnMarkCompleted.FlatAppearance.MouseOverBackColor = Colors.SuccessHover;

            // ===== RIGHT SIDEBAR - Enhanced hierarchy =====
            pnlRightContainer.BackColor = Colors.Background;
            AddLeftBorder(pnlRightContainer, Colors.Gray200, 1);

            pnlSidebarHeader.BackColor = Colors.Background;
            pnlSidebarHeader.Height = 72;
            pnlSidebarHeader.Padding = new Padding(24, 20, 24, 20);
            AddBottomBorder(pnlSidebarHeader, Colors.Gray200, 1);

            lblCurriculumHeader.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblCurriculumHeader.ForeColor = Colors.TextPrimary;

            flpCurriculum.BackColor = Colors.BackgroundAlt;
            flpCurriculum.Padding = new Padding(12, 12, 12, 12);

            // ===== TAB CONTROL - Modern design =====
            tabInfo.Font = new Font("Segoe UI", 10F);
            tabInfo.Padding = new Point(16, 6);
            StyleTabControl(tabInfo);

            // ===== VIDEO AREA =====
            pnlVideoArea.BackColor = Colors.Gray900;
        }

        private void CustomProgressBar(ProgressBar pb)
        {
            pb.Paint += (s, e) =>
            {
                var g = e.Graphics;
                g.SmoothingMode = SmoothingMode.AntiAlias;

                // Background
                using (var bgPath = GetRoundedRectPath(pb.ClientRectangle, 5))
                using (var bgBrush = new SolidBrush(Colors.Gray200))
                {
                    g.FillPath(bgBrush, bgPath);
                }

                // Foreground
                if (pb.Value > 0)
                {
                    int width = (int)((pb.Width * pb.Value) / pb.Maximum);
                    var fgRect = new Rectangle(0, 0, width, pb.Height);

                    using (var fgPath = GetRoundedRectPath(fgRect, 5))
                    using (var fgBrush = new LinearGradientBrush(
                        fgRect,
                        Colors.Success,
                        Color.FromArgb(34, 197, 94),
                        LinearGradientMode.Horizontal))
                    {
                        g.FillPath(fgBrush, fgPath);
                    }
                }
            };
        }

        private void StyleTabControl(TabControl tab)
        {
            tab.DrawMode = TabDrawMode.OwnerDrawFixed;
            tab.DrawItem += (s, e) =>
            {
                var tabControl = s as TabControl;
                var tabPage = tabControl.TabPages[e.Index];
                var tabRect = e.Bounds;

                var g = e.Graphics;
                g.SmoothingMode = SmoothingMode.AntiAlias;

                // Background
                var bgColor = e.State == DrawItemState.Selected ? Colors.Background : Colors.Gray100;
                using (var bgBrush = new SolidBrush(bgColor))
                {
                    g.FillRectangle(bgBrush, tabRect);
                }

                // Bottom border for selected tab
                if (e.State == DrawItemState.Selected)
                {
                    using (var pen = new Pen(Colors.Primary, 3))
                    {
                        g.DrawLine(pen, tabRect.Left, tabRect.Bottom - 1, tabRect.Right, tabRect.Bottom - 1);
                    }
                }

                // Text
                var textColor = e.State == DrawItemState.Selected ? Colors.Primary : Colors.TextSecondary;
                var font = e.State == DrawItemState.Selected
                    ? new Font(tabControl.Font, FontStyle.Bold)
                    : tabControl.Font;

                TextRenderer.DrawText(g, tabPage.Text, font, tabRect, textColor,
                    TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
            };
        }

        private void StyleButton(Button btn, Color bgColor, Color textColor, int borderRadius, bool isPrimary)
        {
            btn.BackColor = bgColor;
            btn.ForeColor = textColor;
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;
            btn.Font = new Font("Segoe UI", 10F, isPrimary ? FontStyle.Bold : FontStyle.Regular);
            btn.Cursor = Cursors.Hand;
            btn.Height = 44; // Better touch target

            // Hover effect with smooth transition
            btn.MouseEnter += (s, e) =>
            {
                btn.FlatAppearance.MouseOverBackColor = isPrimary
                    ? (bgColor == Colors.Primary ? Colors.PrimaryHover : Colors.SuccessHover)
                    : Colors.Gray200;
            };

            btn.Paint += (s, e) =>
            {
                var button = s as Button;
                var graphics = e.Graphics;
                graphics.SmoothingMode = SmoothingMode.AntiAlias;

                using (var path = GetRoundedRectPath(button.ClientRectangle, borderRadius))
                {
                    button.Region = new Region(path);
                }
            };
        }

        

        private void AddShadow(Control control, int depth, int blur)
        {
            control.Paint += (s, e) =>
            {
                var rect = control.ClientRectangle;
                using (var shadowBrush = new SolidBrush(Color.FromArgb(5, 0, 0, 0)))
                {
                    for (int i = 0; i < blur; i++)
                    {
                        e.Graphics.FillRectangle(shadowBrush,
                            0, rect.Height - depth - i, rect.Width, 1);
                    }
                }
            };
        }

        private void AddTopBorder(Control control, Color color, int thickness)
        {
            control.Paint += (s, e) =>
            {
                using (var pen = new Pen(color, thickness))
                {
                    e.Graphics.DrawLine(pen, 0, 0, control.Width, 0);
                }
            };
        }

        private void AddBottomBorder(Control control, Color color, int thickness)
        {
            control.Paint += (s, e) =>
            {
                using (var pen = new Pen(color, thickness))
                {
                    e.Graphics.DrawLine(pen, 0, control.Height - thickness,
                        control.Width, control.Height - thickness);
                }
            };
        }

        private void AddLeftBorder(Control control, Color color, int thickness)
        {
            control.Paint += (s, e) =>
            {
                using (var pen = new Pen(color, thickness))
                {
                    e.Graphics.DrawLine(pen, 0, 0, 0, control.Height);
                }
            };
        }

        // ============= DATA LOADING =============
        private async Task LoadDataAsync()
        {
            try
            {
                _courseData = await _courseService.GetCourseDetailAsync(_courseID);
                if (_courseData == null)
                {
                    ShowModernMessageBox("Không thể tải dữ liệu khóa học.", "Lỗi", MessageBoxIcon.Error);
                    return;
                }

                lblCourseTitle.Text = _courseData.Title;

                var user = GlobalStore.user;
                List<Guid> completedLessonIds = new List<Guid>();
                if (user != null)
                {
                    completedLessonIds = await _progressService.GetCompletedLessonsAsync(user.UserID, _courseID);
                }

                foreach (var mod in _courseData.Modules)
                {
                    foreach (var les in mod.Lessons)
                    {
                        if (completedLessonIds.Contains(les.LessonID))
                            les.IsCompleted = true;
                    }
                }

                _flatLessonList.Clear();
                RenderSidebar(_courseData.Modules);
                ResizeCurriculumItems();
                flpCurriculum.PerformLayout();
                UpdateProgressBar();

                var targetLesson = _initialLessonId != Guid.Empty
                    ? _flatLessonList.FirstOrDefault(l => l.LessonID == _initialLessonId)
                    : _flatLessonList.FirstOrDefault(l => !l.IsCompleted) ?? _flatLessonList.FirstOrDefault();

                if (targetLesson != null)
                {
                    await LoadLessonContentAsync(targetLesson);
                }
            }
            catch (Exception ex)
            {
                ShowModernMessageBox($"Lỗi: {ex.Message}", "Lỗi", MessageBoxIcon.Error);
            }
        }

        // ============= ENHANCED SIDEBAR RENDERING =============
        private void RenderSidebar(List<ModuleDto> modules)
        {
            flpCurriculum.SuspendLayout();
            flpCurriculum.Controls.Clear();

            foreach (var mod in modules)
            {
                flpCurriculum.Controls.Add(CreateModernModuleWidget(mod));
            }

            flpCurriculum.ResumeLayout(true);
        }

        private Control CreateModernModuleWidget(ModuleDto mod)
        {
            int width = flpCurriculum.ClientSize.Width - 32;
            var lessons = mod.Lessons ?? new List<LessonDto>();
            int lessonHeight = 60; // Better touch target
            int headerHeight = 64;

            var pnlContainer = new Panel
            {
                Width = width,
                AutoSize = false,
                BackColor = Colors.Background,
                Margin = new Padding(0, 0, 0, 8) // Better spacing between modules
            };

            // Add subtle border and rounded corners
            pnlContainer.Paint += (s, e) =>
            {
                var g = e.Graphics;
                g.SmoothingMode = SmoothingMode.AntiAlias;

                using (var path = GetRoundedRectPath(pnlContainer.ClientRectangle, 12))
                using (var pen = new Pen(Colors.Gray200, 1))
                {
                    g.DrawPath(pen, path);
                }
            };

            // Enhanced header
            var btnHeader = new Button
            {
                Width = width,
                Height = headerHeight,
                FlatStyle = FlatStyle.Flat,
                TextAlign = ContentAlignment.MiddleLeft,
                Padding = new Padding(24, 0, 24, 0),
                Font = new Font("Segoe UI", 11F, FontStyle.Bold),
                BackColor = Colors.Background,
                ForeColor = Colors.TextPrimary,
                Cursor = Cursors.Hand,
                Text = $"▼  {mod.Title}",
                Tag = mod
            };
            btnHeader.FlatAppearance.BorderSize = 0;
            btnHeader.FlatAppearance.MouseOverBackColor = Colors.Gray50;

            // Completion badge with better design
            int completedCount = lessons.Count(l => l.IsCompleted);
            var lblBadge = new Label
            {
                Text = $"{completedCount}/{lessons.Count}",
                AutoSize = true,
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                ForeColor = completedCount == lessons.Count ? Colors.SuccessText : Colors.TextSecondary,
                BackColor = completedCount == lessons.Count ? Colors.SuccessLight : Colors.Gray100,
                Padding = new Padding(10, 6, 10, 6),
                Location = new Point(width - 80, (headerHeight - 28) / 2)
            };

            lblBadge.Paint += (s, e) =>
            {
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                using (var path = GetRoundedRectPath(lblBadge.ClientRectangle, 14))
                {
                    using (var brush = new SolidBrush(lblBadge.BackColor))
                    {
                        e.Graphics.FillPath(brush, path);
                    }
                }
            };
            btnHeader.Controls.Add(lblBadge);

            // Lessons panel
            var pnlLessons = new Panel
            {
                Width = width,
                AutoSize = false,
                Location = new Point(0, headerHeight),
                BackColor = Colors.Background,
                Padding = new Padding(0)
            };

            int yPos = 0;
            foreach (var les in lessons)
            {
                _flatLessonList.Add(les);
                var lessonItem = CreateModernLessonItem(les, width);
                lessonItem.Location = new Point(0, yPos);
                pnlLessons.Controls.Add(lessonItem);
                yPos += lessonHeight;
            }

            pnlLessons.Height = yPos;
            pnlContainer.Height = headerHeight + yPos;

            pnlContainer.Controls.Add(btnHeader);
            pnlContainer.Controls.Add(pnlLessons);

            // Toggle functionality
            btnHeader.Click += (s, e) =>
            {
                bool isCollapsing = pnlLessons.Visible;
                pnlLessons.Visible = !isCollapsing;
                btnHeader.Text = (pnlLessons.Visible ? "▼  " : "▶  ") + mod.Title;
                pnlContainer.Height = pnlLessons.Visible ? (headerHeight + yPos) : headerHeight;
                flpCurriculum.PerformLayout();
            };

            return pnlContainer;
        }

        private void ResizeCurriculumItems()
        {
            if (flpCurriculum == null) return;

            int w = flpCurriculum.ClientSize.Width - 32;
            if (w <= 0) return;

            flpCurriculum.SuspendLayout();

            foreach (Control mod in flpCurriculum.Controls)
            {
                try
                {
                    mod.Width = w;
                    foreach (Control child in mod.Controls)
                    {
                        child.Width = w;
                        if (child is Panel lessonsPanel)
                        {
                            foreach (Control lessonWrapper in lessonsPanel.Controls)
                            {
                                lessonWrapper.Width = w;
                                var btn = lessonWrapper.Controls.OfType<Button>().FirstOrDefault();
                                if (btn != null)
                                {
                                    btn.Width = w;
                                }
                            }
                        }
                    }
                }
                catch { }
            }

            flpCurriculum.ResumeLayout(true);
            flpCurriculum.Refresh();
        }

        private Control CreateModernLessonItem(LessonDto les, int width)
        {
            string icon = les.LessonType?.ToLower() == "video" ? "▶" :
                         les.LessonType?.ToLower() == "quiz" ? "📝" :
                         les.LessonType?.ToLower() == "coding" ? "💻" : "📄";

            var pnlWrapper = new Panel
            {
                Width = width,
                Height = 60,
                BackColor = Colors.Background,
                Margin = new Padding(0),
                Padding = new Padding(0),
                Tag = les
            };

            // Completion indicator with animation potential
            if (les.IsCompleted)
            {
                var indicator = new Panel
                {
                    Width = 5,
                    Height = 60,
                    BackColor = Colors.Success,
                    Location = new Point(0, 0)
                };
                pnlWrapper.Controls.Add(indicator);
            }

            var btn = new Button
            {
                Width = width,
                Height = 60,
                FlatStyle = FlatStyle.Flat,
                TextAlign = ContentAlignment.MiddleLeft,
                Padding = new Padding(les.IsCompleted ? 56 : 50, 0, 20, 0),
                BackColor = Colors.Background,
                ForeColor = Colors.TextPrimary,
                Font = new Font("Segoe UI", 10F, FontStyle.Regular),
                Cursor = Cursors.Hand,
                Tag = les
            };

            btn.FlatAppearance.BorderSize = 0;
            btn.FlatAppearance.MouseOverBackColor = Colors.PrimaryLight;

            // Icon with better positioning
            var lblIcon = new Label
            {
                Text = icon,
                Font = new Font("Segoe UI", 14F),
                AutoSize = true,
                Location = new Point(les.IsCompleted ? 24 : 20, 18),
                BackColor = Color.Transparent,
                ForeColor = les.IsCompleted ? Colors.Success : Colors.Primary
            };
            btn.Controls.Add(lblIcon);

            // Checkmark for completed lessons
            if (les.IsCompleted)
            {
                var lblCheck = new Label
                {
                    Text = "✓",
                    Font = new Font("Segoe UI", 11F, FontStyle.Bold),
                    AutoSize = true,
                    Location = new Point(width - 40, 20),
                    BackColor = Color.Transparent,
                    ForeColor = Colors.Success
                };
                btn.Controls.Add(lblCheck);
            }

            // Title with better typography
            var lblTitle = new Label
            {
                Text = les.Title,
                Font = new Font("Segoe UI", 10F, les.IsCompleted ? FontStyle.Bold : FontStyle.Regular),
                ForeColor = les.IsCompleted ? Colors.SuccessText : Colors.TextPrimary,
                AutoSize = false,
                Size = new Size(width - 140, 22),
                Location = new Point(les.IsCompleted ? 56 : 50, 12),
                BackColor = Color.Transparent,
                TextAlign = ContentAlignment.MiddleLeft
            };
            btn.Controls.Add(lblTitle);

            // Duration with icon
            if (les.Duration > 0)
            {
                var lblDuration = new Label
                {
                    Text = $"⏱ {les.Duration} phút",
                    Font = new Font("Segoe UI", 9F),
                    ForeColor = Colors.TextTertiary,
                    AutoSize = true,
                    Location = new Point(les.IsCompleted ? 56 : 50, 36),
                    BackColor = Color.Transparent
                };
                btn.Controls.Add(lblDuration);
            }

            btn.Click += async (s, e) => await LoadLessonContentAsync(les);
            pnlWrapper.Controls.Add(btn);

            return pnlWrapper;
        }

        // ============= LESSON CONTENT LOADING =============
        private async Task LoadLessonContentAsync(LessonDto lesson)
        {
            _currentLesson = lesson;
            HighlightCurrentLesson(lesson);
            pnlVideoArea.Controls.Clear();
            _currentWebView = null;
            ClearQuizArea();

            string type = (lesson.LessonType ?? "").ToLower();

            if (type == "video" && lesson.VideoContent != null)
            {
                await LoadVideoContent(lesson.VideoContent.VideoUrl);
            }
            else if (type == "text" && lesson.TextContent != null)
            {
                await LoadTextContent(lesson.TextContent.Content);
            }
            else if (type == "quiz" && lesson.QuizContent != null)
            {
                LoadQuizContent(lesson.QuizContent);
            }
            else if (type == "coding" && lesson.CodingProblem != null)
            {
                ShowCodingPlaceholder();
            }
            else
            {
                LoadEmptyContent();
            }

            UpdateDescription(lesson);
            UpdateNavButtons();
        }

        private void ShowCodingPlaceholder()
        {
            pnlVideoArea.BackColor = Colors.Surface;

            var pnlCenter = new Panel
            {
                Width = 480,
                Height = 280,
                BackColor = Colors.Background,
                Location = new Point(
                    (pnlVideoArea.Width - 480) / 2,
                    (pnlVideoArea.Height - 280) / 2
                )
            };

            pnlCenter.Paint += (s, e) =>
            {
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                using (var path = GetRoundedRectPath(pnlCenter.ClientRectangle, 16))
                using (var brush = new SolidBrush(Colors.Background))
                using (var pen = new Pen(Colors.Gray200, 2))
                {
                    e.Graphics.FillPath(brush, path);
                    e.Graphics.DrawPath(pen, path);
                }
            };

            var lblIcon = new Label
            {
                Text = "💻",
                Font = new Font("Segoe UI", 56F),
                AutoSize = true,
                Location = new Point(212, 40),
                BackColor = Color.Transparent
            };
            pnlCenter.Controls.Add(lblIcon);

            var lblTitle = new Label
            {
                Text = "Bài tập lập trình",
                Font = new Font("Segoe UI", 18F, FontStyle.Bold),
                ForeColor = Colors.TextPrimary,
                AutoSize = true,
                Location = new Point(140, 130),
                BackColor = Color.Transparent
            };
            pnlCenter.Controls.Add(lblTitle);

            var lblDesc = new Label
            {
                Text = "Tính năng coding exercise đang được phát triển",
                Font = new Font("Segoe UI", 11F),
                ForeColor = Colors.TextSecondary,
                AutoSize = true,
                Location = new Point(80, 170),
                BackColor = Color.Transparent
            };
            pnlCenter.Controls.Add(lblDesc);

            var btnComingSoon = new Button
            {
                Text = "Sắp ra mắt",
                Width = 160,
                Height = 44,
                BackColor = Colors.Gray100,
                ForeColor = Colors.TextSecondary,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                Location = new Point(160, 215),
                Enabled = false
            };
            btnComingSoon.FlatAppearance.BorderSize = 0;
            StyleButton(btnComingSoon, Colors.Gray100, Colors.TextSecondary, 8, false);
            pnlCenter.Controls.Add(btnComingSoon);

            pnlVideoArea.Controls.Add(pnlCenter);
        }

        private async Task LoadVideoContent(string youtubeUrl)
        {
            pnlVideoArea.Controls.Clear();

            if (_currentWebView == null)
            {
                _currentWebView = new WebView2 { Dock = DockStyle.Fill };
                pnlVideoArea.Controls.Add(_currentWebView);
                await _currentWebView.EnsureCoreWebView2Async();
            }
            else
            {
                pnlVideoArea.Controls.Add(_currentWebView);
            }

            var settings = _currentWebView.CoreWebView2.Settings;
            settings.AreDevToolsEnabled = false;
            settings.AreDefaultContextMenusEnabled = false;
            settings.IsZoomControlEnabled = false;

            _currentWebView.CoreWebView2.Navigate(youtubeUrl);
        }

        private async Task LoadTextContent(string content)
        {
            pnlVideoArea.BackColor = Colors.Background;

            if (_currentWebView == null)
            {
                _currentWebView = new WebView2 { Dock = DockStyle.Fill };
                pnlVideoArea.Controls.Add(_currentWebView);
                await _currentWebView.EnsureCoreWebView2Async(null);
            }
            else
            {
                if (!pnlVideoArea.Controls.Contains(_currentWebView))
                    pnlVideoArea.Controls.Add(_currentWebView);
            }

            string html = $@"
<!DOCTYPE html>
<html>
<head>
    <meta charset='utf-8'>
    <meta name='viewport' content='width=device-width, initial-scale=1'>
    <style>
        * {{ 
            box-sizing: border-box; 
            margin: 0;
            padding: 0;
        }}
        body {{ 
            font-family: -apple-system, BlinkMacSystemFont, 'Segoe UI', Roboto, sans-serif;
            padding: 56px 72px;
            font-size: 17px;
            color: #111827;
            line-height: 1.8;
            background: #fff;
            max-width: 920px;
            margin: 0 auto;
        }}
        h1 {{ 
            color: #111827;
            font-size: 2.5em;
            margin: 2em 0 0.8em;
            font-weight: 700;
            line-height: 1.2;
            letter-spacing: -0.02em;
        }}
        h1:first-child {{ margin-top: 0; }}
        h2 {{ 
            color: #1f2937;
            font-size: 2em;
            margin: 2em 0 0.8em;
            font-weight: 600;
            line-height: 1.3;
        }}
        h3 {{ 
            color: #374151;
            font-size: 1.5em;
            margin: 1.75em 0 0.7em;
            font-weight: 600;
        }}
        p {{ 
            margin: 0 0 1.75em;
            color: #374151;
        }}
        pre {{ 
            background: #f9fafb;
            padding: 1.5em;
            border-radius: 10px;
            border: 1px solid #e5e7eb;
            overflow-x: auto;
            font-size: 15px;
            line-height: 1.7;
            margin: 2em 0;
            font-family: 'Consolas', 'Monaco', monospace;
        }}
        code {{ 
            font-family: 'Consolas', 'Monaco', monospace;
            background: #f3f4f6;
            padding: 4px 10px;
            border-radius: 5px;
            color: #be185d;
            font-size: 0.92em;
        }}
        pre code {{ 
            background: transparent;
            padding: 0;
            color: #1f2937;
        }}
        img {{ 
            max-width: 100%;
            height: auto;
            display: block;
            margin: 2.5em auto;
            border-radius: 14px;
            box-shadow: 0 10px 25px -5px rgba(0,0,0,0.1);
        }}
        ul, ol {{ 
            margin: 0 0 1.75em 2em;
            padding: 0;
        }}
        li {{ 
            margin-bottom: 0.75em;
            color: #374151;
            line-height: 1.75;
        }}
        blockquote {{
            border-left: 5px solid #3b82f6;
            padding-left: 1.5em;
            margin: 2.5em 0;
            color: #4b5563;
            font-style: italic;
            font-size: 1.05em;
        }}
        a {{ 
            color: #3b82f6;
            text-decoration: none;
            font-weight: 500;
            border-bottom: 1px solid transparent;
            transition: border-color 0.2s;
        }}
        a:hover {{ 
            border-bottom-color: #3b82f6;
        }}
        strong {{
            font-weight: 600;
            color: #111827;
        }}
        em {{
            font-style: italic;
        }}
        table {{
            width: 100%;
            border-collapse: collapse;
            margin: 2.5em 0;
            border-radius: 8px;
            overflow: hidden;
            box-shadow: 0 1px 3px rgba(0,0,0,0.1);
        }}
        th, td {{
            padding: 14px 16px;
            text-align: left;
            border-bottom: 1px solid #e5e7eb;
        }}
        th {{
            background: #f9fafb;
            font-weight: 600;
            color: #111827;
        }}
        tr:last-child td {{
            border-bottom: none;
        }}
    </style>
</head>
<body>{content}</body>
</html>";

            _currentWebView.CoreWebView2.NavigateToString(html);
        }

        private void LoadEmptyContent()
        {
            _currentWebView = null;
            pnlVideoArea.BackColor = Colors.Surface;

            var pnlCenter = new Panel
            {
                Width = 420,
                Height = 220,
                BackColor = Colors.Background,
                Location = new Point(
                    (pnlVideoArea.Width - 420) / 2,
                    (pnlVideoArea.Height - 220) / 2
                )
            };

            pnlCenter.Paint += (s, e) =>
            {
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                using (var path = GetRoundedRectPath(pnlCenter.ClientRectangle, 16))
                using (var brush = new SolidBrush(Colors.Background))
                using (var pen = new Pen(Colors.Gray200, 2))
                {
                    e.Graphics.FillPath(brush, path);
                    e.Graphics.DrawPath(pen, path);
                }
            };

            var lblIcon = new Label
            {
                Text = "📝",
                Font = new Font("Segoe UI", 52F),
                AutoSize = true,
                Location = new Point(184, 35),
                BackColor = Color.Transparent
            };
            pnlCenter.Controls.Add(lblIcon);

            var lbl = new Label
            {
                Text = "Nội dung đang được cập nhật",
                Font = new Font("Segoe UI", 14F, FontStyle.Bold),
                ForeColor = Colors.TextSecondary,
                AutoSize = true,
                Location = new Point(85, 125),
                BackColor = Color.Transparent
            };
            pnlCenter.Controls.Add(lbl);

            var lblSub = new Label
            {
                Text = "Hãy quay lại sau nhé!",
                Font = new Font("Segoe UI", 11F),
                ForeColor = Colors.TextTertiary,
                AutoSize = true,
                Location = new Point(145, 160),
                BackColor = Color.Transparent
            };
            pnlCenter.Controls.Add(lblSub);

            pnlVideoArea.Controls.Add(pnlCenter);
        }

        // ============= ENHANCED QUIZ RENDERING =============
        private void ClearQuizArea()
        {
            _currentQuizSelections.Clear();
            _currentQuizQuestionBoxes.Clear();
            if (_quizPanelContainer != null)
            {
                if (pnlVideoArea.Controls.Contains(_quizPanelContainer))
                    pnlVideoArea.Controls.Remove(_quizPanelContainer);
                _quizPanelContainer.Dispose();
                _quizPanelContainer = null;
            }
        }

        private void LoadQuizContent(LessonQuizDto quiz)
        {
            _quizPanelContainer = new Panel
            {
                Dock = DockStyle.Fill,
                AutoScroll = true,
                BackColor = Colors.Surface,
                Padding = new Padding(40)
            };
            pnlVideoArea.Controls.Add(_quizPanelContainer);

            int y = 0;
            int questionWidth = Math.Min(820, _quizPanelContainer.ClientSize.Width - 80);

            // Enhanced Quiz Header
            var pnlHeader = CreateEnhancedQuizHeader(quiz, questionWidth);
            pnlHeader.Location = new Point(40, y);
            _quizPanelContainer.Controls.Add(pnlHeader);
            y += pnlHeader.Height + 40;

            // Questions
            _currentQuizQuestionBoxes.Clear();
            _currentQuizSelections.Clear();

            int qIndex = 0;
            foreach (var q in quiz.Questions ?? new List<QuizQuestionDto>())
            {
                var questionCard = CreateEnhancedQuestionCard(q, qIndex + 1, questionWidth);
                questionCard.Location = new Point(40, y);
                _quizPanelContainer.Controls.Add(questionCard);
                _currentQuizQuestionBoxes.Add(questionCard);

                y += questionCard.Height + 24;
                qIndex++;
            }

            // Enhanced Submit Button
            var btnSubmit = CreateEnhancedSubmitButton(questionWidth);
            btnSubmit.Location = new Point(40, y + 24);
            btnSubmit.Click += (s, e) => EvaluateQuiz(quiz);
            _quizPanelContainer.Controls.Add(btnSubmit);
        }

        private Panel CreateEnhancedQuizHeader(LessonQuizDto quiz, int width)
        {
            var pnl = new Panel
            {
                Width = width,
                Height = 180,
                BackColor = Colors.Background
            };

            pnl.Paint += (s, e) =>
            {
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                using (var path = GetRoundedRectPath(pnl.ClientRectangle, 16))
                using (var brush = new SolidBrush(Colors.Background))
                using (var pen = new Pen(Colors.Gray200, 2))
                {
                    e.Graphics.FillPath(brush, path);
                    e.Graphics.DrawPath(pen, path);
                }
            };

            // Icon with gradient background
            var pnlIcon = new Panel
            {
                Width = 80,
                Height = 80,
                Location = new Point(32, 32),
                BackColor = Colors.PrimaryLight
            };
            pnlIcon.Paint += (s, e) =>
            {
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                using (var path = GetRoundedRectPath(pnlIcon.ClientRectangle, 16))
                {
                    using (var brush = new LinearGradientBrush(
                        pnlIcon.ClientRectangle,
                        Colors.PrimaryLight,
                        Colors.Primary,
                        LinearGradientMode.ForwardDiagonal))
                    {
                        e.Graphics.FillPath(brush, path);
                    }
                }
            };

            var lblIcon = new Label
            {
                Text = "📝",
                Font = new Font("Segoe UI", 38F),
                AutoSize = true,
                Location = new Point(20, 16),
                BackColor = Color.Transparent
            };
            pnlIcon.Controls.Add(lblIcon);
            pnl.Controls.Add(pnlIcon);

            // Title
            var lblTitle = new Label
            {
                Text = quiz.Title ?? "Bài kiểm tra",
                Font = new Font("Segoe UI", 20F, FontStyle.Bold),
                ForeColor = Colors.TextPrimary,
                AutoSize = true,
                Location = new Point(130, 40),
                BackColor = Color.Transparent
            };
            pnl.Controls.Add(lblTitle);

            // Description
            if (!string.IsNullOrWhiteSpace(quiz.Description))
            {
                var lblDesc = new Label
                {
                    Text = quiz.Description,
                    Font = new Font("Segoe UI", 11F),
                    ForeColor = Colors.TextSecondary,
                    AutoSize = false,
                    Size = new Size(width - 160, 50),
                    Location = new Point(130, 80),
                    BackColor = Color.Transparent
                };
                pnl.Controls.Add(lblDesc);
            }

            // Info badges
            var lblQuestionCount = CreateInfoBadge($"📊 {quiz.Questions?.Count ?? 0} câu hỏi");
            lblQuestionCount.Location = new Point(32, 140);
            pnl.Controls.Add(lblQuestionCount);

            return pnl;
        }

        private Label CreateInfoBadge(string text)
        {
            var lbl = new Label
            {
                Text = text,
                Font = new Font("Segoe UI", 9.5F, FontStyle.Bold),
                ForeColor = Colors.Primary,
                BackColor = Colors.PrimaryLight,
                AutoSize = true,
                Padding = new Padding(12, 8, 12, 8)
            };

            lbl.Paint += (s, e) =>
            {
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                using (var path = GetRoundedRectPath(lbl.ClientRectangle, 16))
                {
                    using (var brush = new SolidBrush(lbl.BackColor))
                    {
                        e.Graphics.FillPath(brush, path);
                    }
                }
            };

            return lbl;
        }

        private GroupBox CreateEnhancedQuestionCard(QuizQuestionDto q, int questionNumber, int width)
        {
            var card = new GroupBox
            {
                Width = width,
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink,
                Font = new Font("Segoe UI", 9F),
                ForeColor = Colors.TextPrimary,
                BackColor = Colors.Background,
                Padding = new Padding(28, 16, 28, 28)
            };

            card.Paint += (s, e) =>
            {
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                using (var path = GetRoundedRectPath(card.ClientRectangle, 16))
                using (var brush = new SolidBrush(Colors.Background))
                using (var pen = new Pen(Colors.Gray200, 2))
                {
                    e.Graphics.FillPath(brush, path);
                    e.Graphics.DrawPath(pen, path);
                }
            };

            int currentY = 40;

            // Question Number Badge
            var pnlBadge = new Panel
            {
                Width = 100,
                Height = 32,
                BackColor = Colors.PrimaryLight,
                Location = new Point(28, currentY)
            };
            pnlBadge.Paint += (s, e) =>
            {
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                using (var path = GetRoundedRectPath(pnlBadge.ClientRectangle, 16))
                {
                    using (var brush = new SolidBrush(Colors.PrimaryLight))
                    {
                        e.Graphics.FillPath(brush, path);
                    }
                }
            };

            var lblNumber = new Label
            {
                Text = $"Câu {questionNumber}",
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                ForeColor = Colors.Primary,
                AutoSize = true,
                BackColor = Color.Transparent,
                Location = new Point(18, 7)
            };
            pnlBadge.Controls.Add(lblNumber);
            card.Controls.Add(pnlBadge);
            currentY += 48;

            // Question Text
            var lblQuestion = new Label
            {
                Text = q.Question,
                Font = new Font("Segoe UI", 12.5F, FontStyle.Bold),
                ForeColor = Colors.TextPrimary,
                AutoSize = false,
                Size = new Size(width - 76, 0),
                MaximumSize = new Size(width - 76, 400),
                Location = new Point(28, currentY),
                BackColor = Color.Transparent
            };
            lblQuestion.AutoSize = true;
            card.Controls.Add(lblQuestion);
            currentY += lblQuestion.Height + 28;

            // Answers Container
            var pnlAnswersContainer = new Panel
            {
                Name = "pnlAnswersContainer",
                Width = width - 56,
                Location = new Point(28, currentY),
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink,
                BackColor = Color.Transparent
            };
            card.Controls.Add(pnlAnswersContainer);

            int answerY = 0;
            int answerIndex = 0;
            var answers = q.Answers ?? Array.Empty<string>();

            foreach (var ans in answers)
            {
                var pnlAnswerBg = new Panel
                {
                    Name = "pnlAnswerBg",
                    Width = width - 76,
                    Height = 56,
                    BackColor = Colors.Gray50,
                    Location = new Point(0, answerY),
                    Tag = answerIndex
                };

                pnlAnswerBg.Paint += (s, e) =>
                {
                    e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                    using (var path = GetRoundedRectPath(pnlAnswerBg.ClientRectangle, 10))
                    {
                        using (var brush = new SolidBrush(pnlAnswerBg.BackColor))
                        {
                            e.Graphics.FillPath(brush, path);
                        }
                        using (var pen = new Pen(Colors.Gray200, 2))
                        {
                            e.Graphics.DrawPath(pen, path);
                        }
                    }
                };

                var rb = new RadioButton
                {
                    Name = $"rb_{q.QuestionID}_{answerIndex}",
                    Text = $"    {ans}",
                    Tag = new { QuestionId = q.QuestionID, AnswerIndex = answerIndex, BgPanel = pnlAnswerBg },
                    Font = new Font("Segoe UI", 10.5F),
                    ForeColor = Colors.TextPrimary,
                    AutoSize = false,
                    Size = new Size(width - 96, 54),
                    Location = new Point(20, answerY + 1),
                    Cursor = Cursors.Hand,
                    BackColor = Color.Transparent
                };

                rb.CheckedChanged += (s, e) =>
                {
                    QuizAnswer_CheckedChanged(s, e);

                    if (rb.Checked)
                    {
                        pnlAnswerBg.BackColor = Colors.PrimaryLight;

                        foreach (var otherPanel in pnlAnswersContainer.Controls.OfType<Panel>())
                        {
                            if (otherPanel != pnlAnswerBg && otherPanel.Name == "pnlAnswerBg")
                            {
                                otherPanel.BackColor = Colors.Gray50;
                            }
                        }
                    }
                };

                pnlAnswerBg.MouseEnter += (s, e) =>
                {
                    if (rb.Enabled && !rb.Checked)
                        pnlAnswerBg.BackColor = Colors.Gray100;
                };

                pnlAnswerBg.MouseLeave += (s, e) =>
                {
                    if (rb.Enabled && !rb.Checked)
                        pnlAnswerBg.BackColor = Colors.Gray50;
                };

                pnlAnswerBg.Click += (s, e) => rb.Checked = true;

                pnlAnswersContainer.Controls.Add(pnlAnswerBg);
                pnlAnswersContainer.Controls.Add(rb);
                rb.BringToFront();

                answerY += 64; // 56 height + 8 margin
                answerIndex++;
            }

            pnlAnswersContainer.Height = answerY;
            currentY += pnlAnswersContainer.Height + 16;

            // Explanation panel
            var pnlExplanation = new Panel
            {
                Name = "pnlExp",
                Width = width - 76,
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink,
                Location = new Point(28, currentY + 16),
                Padding = new Padding(24),
                BackColor = Colors.SuccessLight,
                Visible = false
            };

            pnlExplanation.Paint += (s, e) =>
            {
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                using (var path = GetRoundedRectPath(pnlExplanation.ClientRectangle, 12))
                {
                    using (var brush = new SolidBrush(pnlExplanation.BackColor))
                    {
                        e.Graphics.FillPath(brush, path);
                    }
                }
            };

            var lblExpIcon = new Label
            {
                Text = "💡",
                Font = new Font("Segoe UI", 20F),
                AutoSize = true,
                Location = new Point(24, 24),
                BackColor = Color.Transparent
            };
            pnlExplanation.Controls.Add(lblExpIcon);

            var lblExpTitle = new Label
            {
                Text = "Giải thích",
                Font = new Font("Segoe UI", 11F, FontStyle.Bold),
                ForeColor = Colors.SuccessText,
                AutoSize = true,
                Location = new Point(70, 28),
                BackColor = Color.Transparent
            };
            pnlExplanation.Controls.Add(lblExpTitle);

            var lblExp = new Label
            {
                Name = "lblExp",
                Text = q.Explanation ?? "Không có giải thích",
                Font = new Font("Segoe UI", 10.5F),
                ForeColor = Colors.SuccessText,
                AutoSize = false,
                Size = new Size(width - 164, 0),
                MaximumSize = new Size(width - 164, 800),
                Location = new Point(70, 56),
                BackColor = Color.Transparent
            };
            lblExp.AutoSize = true;
            pnlExplanation.Controls.Add(lblExp);

            pnlExplanation.Height = Math.Max(lblExpIcon.Height, lblExpTitle.Height + lblExp.Height + 16) + 48;
            card.Controls.Add(pnlExplanation);

            card.Height = currentY + 100;

            return card;
        }

        private Button CreateEnhancedSubmitButton(int width)
        {
            var btn = new Button
            {
                Text = "✓  Nộp bài kiểm tra",
                Width = 240,
                Height = 56,
                BackColor = Colors.Primary,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 12F, FontStyle.Bold),
                Cursor = Cursors.Hand
            };

            btn.FlatAppearance.BorderSize = 0;
            btn.FlatAppearance.MouseOverBackColor = Colors.PrimaryHover;

            btn.Paint += (s, e) =>
            {
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                using (var path = GetRoundedRectPath(btn.ClientRectangle, 10))
                {
                    btn.Region = new Region(path);
                }
            };

            btn.MouseEnter += (s, e) => btn.BackColor = Colors.PrimaryHover;
            btn.MouseLeave += (s, e) => btn.BackColor = Colors.Primary;

            return btn;
        }

        private void QuizAnswer_CheckedChanged(object sender, EventArgs e)
        {
            if (!(sender is RadioButton rb)) return;
            if (!rb.Checked) return;

            dynamic tag = rb.Tag;
            Guid qid = tag.QuestionId;
            int ansIndex = tag.AnswerIndex;

            if (_currentQuizSelections.ContainsKey(qid))
                _currentQuizSelections[qid] = ansIndex;
            else
                _currentQuizSelections.Add(qid, ansIndex);
        }

        private void EvaluateQuiz(LessonQuizDto quiz)
        {
            if (quiz?.Questions == null || quiz.Questions.Count == 0)
            {
                ShowModernMessageBox("Không có câu hỏi để đánh giá.", "Thông tin", MessageBoxIcon.Information);
                return;
            }

            int total = quiz.Questions.Count;
            int correct = 0;
            int wrong = 0;
            int skipped = 0;

            GroupBox firstWrongQuestion = null;

            foreach (var q in quiz.Questions)
            {
                int selected = _currentQuizSelections.TryGetValue(q.QuestionID, out var s) ? s : -1;
                bool isCorrect = selected == q.CorrectIndex;
                bool isSkipped = selected == -1;

                if (isCorrect) correct++;
                else if (isSkipped) skipped++;
                else wrong++;

                var card = _currentQuizQuestionBoxes.FirstOrDefault(g =>
                    g.Controls.OfType<Label>().Any(l => l.Text == q.Question));

                if (card == null) continue;

                if (!isCorrect && firstWrongQuestion == null)
                    firstWrongQuestion = card;

                // Enhanced Status Badge
                var lblStatus = card.Controls.OfType<Panel>()
                    .FirstOrDefault(p => p.Name == "statusBadge");

                if (lblStatus == null)
                {
                    lblStatus = new Panel
                    {
                        Name = "statusBadge",
                        Width = 160,
                        Height = 36,
                        Location = new Point(card.Width - 188, 28)
                    };

                    var lblStatusText = new Label
                    {
                        Name = "statusText",
                        AutoSize = true,
                        Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                        Location = new Point(14, 9),
                        BackColor = Color.Transparent
                    };
                    lblStatus.Controls.Add(lblStatusText);
                    card.Controls.Add(lblStatus);
                    lblStatus.BringToFront();
                }

                var statusText = lblStatus.Controls.OfType<Label>().FirstOrDefault(l => l.Name == "statusText");

                if (isSkipped)
                {
                    statusText.Text = "⊘ Chưa trả lời";
                    statusText.ForeColor = Colors.WarningText;
                    lblStatus.BackColor = Colors.WarningLight;
                    card.BackColor = Color.FromArgb(255, 251, 235);
                }
                else if (isCorrect)
                {
                    statusText.Text = "✓ Chính xác";
                    statusText.ForeColor = Color.White;
                    lblStatus.BackColor = Colors.Success;
                    card.BackColor = Color.FromArgb(240, 253, 244);
                }
                else
                {
                    statusText.Text = "✗ Sai";
                    statusText.ForeColor = Color.White;
                    lblStatus.BackColor = Colors.Error;
                    card.BackColor = Color.FromArgb(254, 242, 242);
                }

                lblStatus.Paint += (s, e) =>
                {
                    e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                    using (var path = GetRoundedRectPath(lblStatus.ClientRectangle, 18))
                    {
                        using (var brush = new SolidBrush(lblStatus.BackColor))
                        {
                            e.Graphics.FillPath(brush, path);
                        }
                    }
                };

                // Highlight answers
                var pnlAnswersContainer = card.Controls.OfType<Panel>()
                    .FirstOrDefault(p => p.Name == "pnlAnswersContainer");

                if (pnlAnswersContainer != null)
                {
                    var radioButtons = pnlAnswersContainer.Controls.OfType<RadioButton>().ToList();

                    foreach (var rb in radioButtons)
                    {
                        dynamic tag = rb.Tag;
                        int ansIndex = tag.AnswerIndex;
                        Panel bgPanel = tag.BgPanel;

                        rb.Enabled = false;
                        if (bgPanel != null) bgPanel.Cursor = Cursors.Default;

                        if (ansIndex == q.CorrectIndex)
                        {
                            if (bgPanel != null)
                            {
                                bgPanel.BackColor = Colors.SuccessLight;
                                bgPanel.Paint += (s, e) =>
                                {
                                    e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                                    using (var path = GetRoundedRectPath(bgPanel.ClientRectangle, 10))
                                    {
                                        using (var brush = new SolidBrush(bgPanel.BackColor))
                                        {
                                            e.Graphics.FillPath(brush, path);
                                        }
                                        using (var pen = new Pen(Colors.Success, 3))
                                        {
                                            e.Graphics.DrawPath(pen, path);
                                        }
                                    }
                                };
                            }
                            rb.ForeColor = Colors.SuccessText;
                            rb.Font = new Font("Segoe UI", 10.5F, FontStyle.Bold);

                            if (!rb.Text.TrimStart().StartsWith("✓"))
                                rb.Text = "    ✓  " + rb.Text.TrimStart();
                        }
                        else if (ansIndex == selected && !isCorrect)
                        {
                            if (bgPanel != null)
                            {
                                bgPanel.BackColor = Colors.ErrorLight;
                                bgPanel.Paint += (s, e) =>
                                {
                                    e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                                    using (var path = GetRoundedRectPath(bgPanel.ClientRectangle, 10))
                                    {
                                        using (var brush = new SolidBrush(bgPanel.BackColor))
                                        {
                                            e.Graphics.FillPath(brush, path);
                                        }
                                        using (var pen = new Pen(Colors.Error, 3))
                                        {
                                            e.Graphics.DrawPath(pen, path);
                                        }
                                    }
                                };
                            }
                            rb.ForeColor = Colors.ErrorText;
                            rb.Font = new Font("Segoe UI", 10.5F, FontStyle.Bold);

                            if (!rb.Text.TrimStart().StartsWith("✗"))
                                rb.Text = "    ✗  " + rb.Text.TrimStart();
                        }
                        else
                        {
                            if (bgPanel != null) bgPanel.BackColor = Colors.Gray100;
                            rb.ForeColor = Colors.Gray400;
                            rb.Font = new Font("Segoe UI", 10.5F, FontStyle.Regular);
                        }
                    }
                }

                // Show explanation
                var pnlExp = card.Controls.OfType<Panel>()
                    .FirstOrDefault(p => p.Name == "pnlExp");

                if (pnlExp != null)
                {
                    pnlExp.Visible = true;
                    pnlExp.BringToFront();

                    if (isCorrect)
                    {
                        pnlExp.BackColor = Colors.SuccessLight;
                    }
                    else
                    {
                        pnlExp.BackColor = Color.FromArgb(219, 234, 254); // Blue-100
                    }

                    int maxY = 0;
                    foreach (Control c in card.Controls)
                    {
                        if (c != pnlExp && c.Visible)
                            maxY = Math.Max(maxY, c.Bottom);
                    }
                    pnlExp.Top = maxY + 20;
                    card.Height = pnlExp.Bottom + 28;
                }
            }

            if (firstWrongQuestion != null)
            {
                _quizPanelContainer.ScrollControlIntoView(firstWrongQuestion);
            }

            ShowQuizResultDialog(correct, wrong, skipped, total);

            if (correct == total && _currentLesson != null)
            {
                var user = GlobalStore.user;
                if (user != null)
                {
                    _ = Task.Run(async () =>
                    {
                        try
                        {
                            await _progressService.MarkLessonCompletedAsync(
                                user.UserID, _currentLesson.LessonID);

                            this.Invoke((MethodInvoker)delegate
                            {
                                _currentLesson.IsCompleted = true;
                                UpdateNavButtons();
                                RefreshSidebar();
                                UpdateProgressBar();
                            });
                        }
                        catch { }
                    });
                }
            }
        }

        private void ShowQuizResultDialog(int correct, int wrong, int skipped, int total)
        {
            double percentage = (correct * 100.0) / total;

            string emoji, message, title;
            Color bgColor;

            if (percentage == 100)
            {
                emoji = "🎉";
                title = "Hoàn hảo!";
                message = "Chúc mừng! Bạn đã trả lời chính xác tất cả các câu hỏi.";
                bgColor = Colors.SuccessLight;
            }
            else if (percentage >= 80)
            {
                emoji = "🌟";
                title = "Xuất sắc!";
                message = "Kết quả tuyệt vời! Bạn đã nắm vững kiến thức.";
                bgColor = Colors.SuccessLight;
            }
            else if (percentage >= 60)
            {
                emoji = "👍";
                title = "Tốt!";
                message = "Kết quả khá tốt, hãy ôn lại một số phần còn thiếu.";
                bgColor = Color.FromArgb(219, 234, 254);
            }
            else
            {
                emoji = "📚";
                title = "Cần cố gắng thêm";
                message = "Đừng nản lòng! Hãy xem lại bài học và thử lại nhé.";
                bgColor = Colors.WarningLight;
            }

            string detailText = $@"{emoji} {title}

{message}

━━━━━━━━━━━━━━━━━━━━━━
Kết quả chi tiết:

✓  Đúng: {correct} câu
✗  Sai: {wrong} câu
⊘  Bỏ qua: {skipped} câu
━━━━━━━━━━━━━━━━━━━━━━

Tổng điểm: {correct}/{total} ({percentage:F1}%)";

            ShowModernMessageBox(detailText, "Kết quả bài kiểm tra", MessageBoxIcon.Information);
        }

        private void ShowModernMessageBox(string message, string title, MessageBoxIcon icon)
        {
            MessageBox.Show(message, title, MessageBoxButtons.OK, icon);
        }

        // ============= PROGRESS & NAVIGATION =============
        private void UpdateDescription(LessonDto lesson)
        {
            string type = (lesson.LessonType ?? "").ToLower();
            string typeText = type == "video" ? "📹 Video bài học" :
                             type == "text" ? "📄 Nội dung văn bản" :
                             type == "quiz" ? "📝 Bài kiểm tra" :
                             type == "coding" ? "💻 Bài tập lập trình" :
                             "📚 Bài học";

            wbDescription.DocumentText = $@"
<html>
<head>
<meta charset='utf-8'>
<style>
    body {{ 
        font-family: 'Segoe UI', system-ui;
        padding: 20px 24px;
        color: {ColorTranslator.ToHtml(Colors.TextPrimary)};
        margin: 0;
        background: white;
    }}
    h3 {{ 
        margin: 0 0 10px 0;
        font-size: 16px;
        font-weight: 600;
        color: {ColorTranslator.ToHtml(Colors.TextPrimary)};
    }}
    p {{ 
        color: {ColorTranslator.ToHtml(Colors.TextSecondary)};
        margin: 0;
        font-size: 14px;
        line-height: 1.6;
    }}
</style>
</head>
<body>
    <h3>{lesson.Title}</h3>
    <p>{typeText}</p>
</body>
</html>";
        }

        private async Task MarkLessonCompleted()
        {
            if (_currentLesson == null) return;

            var user = GlobalStore.user;
            if (user == null)
            {
                ShowModernMessageBox(
                    "Vui lòng đăng nhập để lưu tiến độ học tập.",
                    "Yêu cầu đăng nhập",
                    MessageBoxIcon.Information);
                return;
            }

            try
            {
                bool success = await _progressService.MarkLessonCompletedAsync(
                    user.UserID, _currentLesson.LessonID);

                if (success)
                {
                    _currentLesson.IsCompleted = true;
                    UpdateNavButtons();
                    RefreshSidebar();
                    UpdateProgressBar();

                    int idx = _flatLessonList.IndexOf(_currentLesson);
                    if (idx < _flatLessonList.Count - 1)
                    {
                        await Task.Delay(500);
                        await LoadLessonContentAsync(_flatLessonList[idx + 1]);
                    }
                    else
                    {
                        ShowModernMessageBox(
                            "Chúc mừng! Bạn đã hoàn thành khóa học này! 🎉",
                            "Hoàn thành",
                            MessageBoxIcon.Information);
                    }
                }
                else
                {
                    ShowModernMessageBox(
                        "Không thể cập nhật tiến độ. Vui lòng thử lại.",
                        "Lỗi",
                        MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                ShowModernMessageBox($"Lỗi: {ex.Message}", "Lỗi", MessageBoxIcon.Error);
            }
        }

        private void UpdateProgressBar()
        {
            if (_flatLessonList.Count == 0) return;

            int completedCount = _flatLessonList.Count(l => l.IsCompleted);
            int percent = (completedCount * 100) / _flatLessonList.Count;

            pbProgress.Value = Math.Min(percent, 100);
            lblProgress.Text = $"{completedCount}/{_flatLessonList.Count}";

            // Force repaint for custom progress bar
            pbProgress.Invalidate();
        }

        private void RefreshSidebar()
        {
            foreach (Control c in flpCurriculum.Controls)
            {
                if (c is Panel pnlMod)
                {
                    foreach (Control modChild in pnlMod.Controls)
                    {
                        if (modChild is Panel lessonsPanel)
                        {
                            foreach (Control lessonWrapper in lessonsPanel.Controls)
                            {
                                if (lessonWrapper is Panel wrapper && wrapper.Tag is LessonDto les)
                                {
                                    var btn = wrapper.Controls.OfType<Button>().FirstOrDefault();
                                    if (btn != null)
                                    {
                                        var lblIcon = btn.Controls.OfType<Label>().FirstOrDefault();
                                        if (lblIcon != null)
                                        {
                                            lblIcon.ForeColor = les.IsCompleted ? Colors.Success : Colors.Primary;
                                        }

                                        btn.ForeColor = les.IsCompleted ? Colors.SuccessText : Colors.TextPrimary;
                                        btn.Font = new Font("Segoe UI", 10F,
                                            les.IsCompleted ? FontStyle.Bold : FontStyle.Regular);

                                        // Update or add checkmark
                                        var existingCheck = btn.Controls.OfType<Label>()
                                            .FirstOrDefault(l => l.Text == "✓" && l.Location.X > btn.Width - 60);

                                        if (les.IsCompleted && existingCheck == null)
                                        {
                                            var lblCheck = new Label
                                            {
                                                Text = "✓",
                                                Font = new Font("Segoe UI", 11F, FontStyle.Bold),
                                                AutoSize = true,
                                                Location = new Point(btn.Width - 40, 20),
                                                BackColor = Color.Transparent,
                                                ForeColor = Colors.Success
                                            };
                                            btn.Controls.Add(lblCheck);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        private void UpdateNavButtons()
        {
            if (_currentLesson == null) return;

            int idx = _flatLessonList.IndexOf(_currentLesson);
            btnPrev.Enabled = idx > 0;
            btnNext.Enabled = idx < _flatLessonList.Count - 1;

            if (_currentLesson.IsCompleted)
            {
                btnMarkCompleted.Text = "✓ Đã hoàn thành";
                btnMarkCompleted.BackColor = Colors.Gray300;
                btnMarkCompleted.ForeColor = Colors.Gray600;
                btnMarkCompleted.Enabled = false;
            }
            else
            {
                btnMarkCompleted.Text = "✓ Hoàn thành bài học";
                btnMarkCompleted.BackColor = Colors.Success;
                btnMarkCompleted.ForeColor = Color.White;
                btnMarkCompleted.Enabled = true;
            }
        }

        private void WireEvents()
        {
            try
            {
                this.Load += async (s, e) =>
                {
                    if (!this.DesignMode)
                    {
                        await LoadDataAsync();

                        if (_initialLessonId != Guid.Empty)
                        {
                            var lesson = _flatLessonList.FirstOrDefault(x => x.LessonID == _initialLessonId);
                            if (lesson != null)
                                await LoadLessonContentAsync(lesson);
                        }
                    }
                };

                if (btnPrev != null) btnPrev.Click += async (s, e) => await NavigateLesson(-1);
                if (btnNext != null) btnNext.Click += async (s, e) => await NavigateLesson(1);
                if (btnMarkCompleted != null) btnMarkCompleted.Click += async (s, e) => await MarkLessonCompleted();

                this.Resize += (s, e) => ResizeCurriculumItems();
            }
            catch { }
        }

        private void InitTreeImageList()
        {
            try { } catch { }
        }

        private async Task NavigateLesson(int direction)
        {
            if (_currentLesson == null) return;

            int idx = _flatLessonList.IndexOf(_currentLesson);
            int newIdx = idx + direction;

            if (newIdx >= 0 && newIdx < _flatLessonList.Count)
            {
                await LoadLessonContentAsync(_flatLessonList[newIdx]);
            }
        }

        private void HighlightCurrentLesson(LessonDto current)
        {
            if (current == null) return;

            foreach (Control c in flpCurriculum.Controls)
            {
                if (!(c is Panel pnlMod)) continue;

                Panel pnlLessons = null;
                Button headerBtn = null;

                foreach (Control child in pnlMod.Controls)
                {
                    if (child is Panel p && p.Location.Y > 0) pnlLessons = p;
                    if (child is Button btn) headerBtn = btn;
                }

                if (pnlLessons == null) continue;

                bool foundInThisModule = false;

                foreach (Control item in pnlLessons.Controls)
                {
                    if (!(item is Panel wrapper)) continue;

                    var btn = wrapper.Controls.OfType<Button>().FirstOrDefault();
                    if (btn == null || !(btn.Tag is LessonDto les)) continue;

                    if (les.LessonID == current.LessonID)
                    {
                        btn.BackColor = Colors.PrimaryLight;
                        btn.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
                        btn.ForeColor = Colors.Primary;
                        foundInThisModule = true;
                    }
                    else
                    {
                        btn.BackColor = Colors.Background;
                        btn.Font = new Font("Segoe UI", 10F,
                            les.IsCompleted ? FontStyle.Bold : FontStyle.Regular);
                        btn.ForeColor = les.IsCompleted ? Colors.SuccessText : Colors.TextPrimary;
                    }
                }

                if (foundInThisModule && pnlLessons != null)
                {
                    if (!pnlLessons.Visible) pnlLessons.Visible = true;
                    if (headerBtn != null && headerBtn.Tag is ModuleDto mod)
                    {
                        headerBtn.Text = $"▼  {mod.Title}";
                    }
                }
            }
        }

        private (string videoId, string playlistId) ParseYouTubeUrl(string url)
        {
            string videoId = null;
            string playlistId = null;

            try
            {
                var uri = new Uri(url);
                var query = System.Web.HttpUtility.ParseQueryString(uri.Query);

                videoId = query["v"];
                playlistId = query["list"];

                if (videoId == null && uri.Host.Contains("youtu.be"))
                {
                    videoId = uri.AbsolutePath.Trim('/');
                }
            }
            catch { }

            return (videoId, playlistId);
        }

        private string ExtractYouTubeVideoId(string url)
        {
            try
            {
                if (url.Contains("youtu.be/"))
                {
                    var match = Regex.Match(url, @"youtu\.be/([a-zA-Z0-9_-]{11})");
                    if (match.Success) return match.Groups[1].Value;
                }

                if (url.Contains("youtube.com/watch"))
                {
                    var match = Regex.Match(url, @"[?&]v=([a-zA-Z0-9_-]{11})");
                    if (match.Success) return match.Groups[1].Value;
                }

                if (url.Contains("youtube.com/embed/"))
                {
                    var match = Regex.Match(url, @"embed/([a-zA-Z0-9_-]{11})");
                    if (match.Success) return match.Groups[1].Value;
                }
            }
            catch { }

            return null;
        }
    }
}