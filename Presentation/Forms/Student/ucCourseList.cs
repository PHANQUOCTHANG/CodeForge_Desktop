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
using CodeForge_Desktop.Business.Helpers;
using CodeForge_Desktop.Business.Services;
using CodeForge_Desktop.DataAccess.Repositories;

namespace CodeForge_Desktop.Presentation.Forms.Student.UserControls
{
    public partial class ucCourseList : UserControl
    {
        private readonly CourseService _courseService;
        private List<CourseDto> _allCourses;
        private List<CourseDto> _filteredCourses;
        private System.Windows.Forms.Timer _searchTimer;
        private System.Windows.Forms.Timer _animationTimer;
        private int _animationStep = 0;

        // --- ENHANCED COLOR SCHEME ---
        private readonly Color clrPrimary = Color.FromArgb(0, 122, 255);
        private readonly Color clrPrimaryHover = Color.FromArgb(0, 108, 230);
        private readonly Color clrSecondary = Color.FromArgb(108, 117, 125);
        private readonly Color clrSuccess = Color.FromArgb(40, 167, 69);
        private readonly Color clrSuccessLight = Color.FromArgb(230, 255, 235);
        private readonly Color clrWarning = Color.FromArgb(255, 193, 7);
        private readonly Color clrDanger = Color.FromArgb(220, 53, 69);
        private readonly Color clrDark = Color.FromArgb(33, 37, 41);
        private readonly Color clrLight = Color.FromArgb(248, 249, 250);
        private readonly Color clrBorder = Color.FromArgb(220, 220, 220);
        private readonly Color clrShadow = Color.FromArgb(30, 0, 0, 0);

        // Level colors
        private readonly Color clrBeginner = Color.FromArgb(40, 167, 69);
        private readonly Color clrIntermediate = Color.FromArgb(255, 193, 7);
        private readonly Color clrAdvanced = Color.FromArgb(220, 53, 69);

        // Constructor
        public ucCourseList() : this(new CourseRepository()) { }

        public ucCourseList(CourseRepository repo)
        {
            _courseService = new CourseService(repo);
            InitializeComponent();

            // Enhanced Double Buffer
            this.DoubleBuffered = true;
            SetStyle(ControlStyles.OptimizedDoubleBuffer |
                     ControlStyles.UserPaint |
                     ControlStyles.AllPaintingInWmPaint |
                     ControlStyles.ResizeRedraw, true);
            this.UpdateStyles();

            // Search Timer (Debounce)
            _searchTimer = new System.Windows.Forms.Timer { Interval = 300 };
            _searchTimer.Tick += async (s, e) => { _searchTimer.Stop(); await ApplyFilterAsync(); };

            // Animation Timer for loading
            _animationTimer = new System.Windows.Forms.Timer { Interval = 100 };
            _animationTimer.Tick += AnimationTimer_Tick;

            // Events
            this.Load += async (s, e) => await OnLoadAsync();
            this.txtSearch.TextChanged += (s, e) => { _searchTimer.Stop(); _searchTimer.Start(); };
            this.cmbFilterLevel.SelectedIndexChanged += async (s, e) => await ApplyFilterAsync();

            // Enhanced Hero Panel
            EnhanceHeroPanel();

            // Enhanced Search Container
            EnhanceSearchContainer();

            // Rounded corners for search elements
            RoundCorners(pnlSearchBox, 8);
            RoundCorners(pnlFilterBox, 8);
            RoundCorners(btnClearFilters, 6);
            RoundCorners(btnHeroAction, 6);
        }

        private async Task OnLoadAsync()
        {
            ProgressNotifier.ProgressUpdated += OnProgressUpdated;
            cmbFilterLevel.SelectedIndex = 0;
            await LoadCoursesAsync();
        }

        protected override void OnHandleDestroyed(EventArgs e)
        {
            ProgressNotifier.ProgressUpdated -= OnProgressUpdated;
            _searchTimer?.Dispose();
            _animationTimer?.Dispose();
            base.OnHandleDestroyed(e);
        }

        private void OnProgressUpdated(object sender, ProgressUpdatedEventArgs e)
        {
            Guid currentUserID = GlobalStore.user?.UserID ?? Guid.Empty;
            if (e.UserId == currentUserID && this.IsHandleCreated)
            {
                this.BeginInvoke(new Action(async () => await LoadCoursesAsync()));
            }
        }

        // =========================================================
        // ENHANCED UI METHODS
        // =========================================================

        private void EnhanceHeroPanel()
        {
            // Gradient background
            pnlHero.Paint += (s, e) =>
            {
                var rect = pnlHero.ClientRectangle;
                using (var brush = new LinearGradientBrush(rect,
                    Color.FromArgb(20, 30, 48),
                    Color.FromArgb(36, 59, 85),
                    LinearGradientMode.Horizontal))
                {
                    e.Graphics.FillRectangle(brush, rect);
                }

                // Decorative circles
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                using (var brush = new SolidBrush(Color.FromArgb(30, 255, 255, 255)))
                {
                    e.Graphics.FillEllipse(brush, rect.Width - 250, -50, 300, 300);
                    e.Graphics.FillEllipse(brush, rect.Width - 100, 100, 150, 150);
                }
            };

            // Create decoration graphic
            CreateHeroDecoration();
        }

        private void CreateHeroDecoration()
        {
            var bmp = new Bitmap(200, 140);
            using (var g = Graphics.FromImage(bmp))
            {
                g.SmoothingMode = SmoothingMode.AntiAlias;
                g.Clear(Color.Transparent);

                // Draw abstract code illustration
                using (var brush = new SolidBrush(Color.FromArgb(100, 255, 255, 255)))
                using (var pen = new Pen(Color.FromArgb(150, 255, 255, 255), 3))
                {
                    // Code brackets
                    g.DrawString("{ }", new Font("Consolas", 48, FontStyle.Bold), brush, 20, 20);
                    // Connection lines
                    g.DrawLine(pen, 60, 40, 140, 60);
                    g.DrawLine(pen, 60, 80, 140, 100);
                }
            }
            pbHeroDecoration.Image = bmp;
        }

        private void EnhanceSearchContainer()
        {
            // Bottom border
            pnlSearchContainer.Paint += (s, e) =>
            {
                using (var pen = new Pen(clrBorder))
                {
                    e.Graphics.DrawLine(pen, 0, pnlSearchContainer.Height - 1,
                        pnlSearchContainer.Width, pnlSearchContainer.Height - 1);
                }
            };

            // Stats bar border
            pnlStatsBar.Paint += (s, e) =>
            {
                using (var pen = new Pen(clrBorder))
                {
                    e.Graphics.DrawLine(pen, 50, pnlStatsBar.Height - 1,
                        pnlStatsBar.Width - 50, pnlStatsBar.Height - 1);
                }
            };
        }

        private void RoundCorners(Control control, int radius)
        {
            control.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, control.Width, control.Height, radius, radius));
        }

        [System.Runtime.InteropServices.DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn(int nLeftRect, int nTopRect, int nRightRect, int nBottomRect, int nWidthEllipse, int nHeightEllipse);

        // =========================================================
        // DATA LOADING
        // =========================================================

        private async Task LoadCoursesAsync()
        {
            try
            {
                ShowLoading(true);

                // 1. Load all courses (public listing)
                _allCourses = await _courseService.GetAllCoursesAsync() ?? new List<CourseDto>();

                // 2. If user logged in -> load enrolled courses for that user and merge
                var user = GlobalStore.user;
                if (user != null && user.UserID != Guid.Empty)
                {
                    try
                    {
                        var enrolledList = await _courseService.GetEnrolledCoursesAsync(user.UserID) ?? new List<CourseDto>();
                        var enrolledDict = enrolledList.ToDictionary(x => x.CourseID, x => x);

                        foreach (var c in _allCourses)
                        {
                            if (enrolledDict.TryGetValue(c.CourseID, out var enrolledDto))
                            {
                                c.IsEnrolled = true;
                                // Prefer server-side progress if available
                                c.Progress = enrolledDto.Progress;
                                // Optionally override other fields from enrolled DTO (TotalStudents, etc.)
                                c.TotalStudents = enrolledDto.TotalStudents != 0 ? enrolledDto.TotalStudents : c.TotalStudents;
                            }
                            else
                            {
                                c.IsEnrolled = false;
                                c.Progress = 0;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        // If enrolled fetch fails, keep courses but log silently
                        System.Diagnostics.Debug.WriteLine("Failed to load enrolled courses: " + ex.Message);
                        foreach (var c in _allCourses) { c.IsEnrolled = false; c.Progress = 0; }
                    }
                }
                else
                {
                    // Not logged in: mark all as not enrolled
                    foreach (var c in _allCourses) { c.IsEnrolled = false; c.Progress = 0; }
                }

                // 3. Update stats & UI
                UpdateStatistics();
                await ApplyFilterAsync();

                ShowLoading(false);
            }
            catch (Exception ex)
            {
                ShowLoading(false);
                ShowErrorMessage("Lỗi tải dữ liệu: " + ex.Message);
            }
        }

        private async Task ApplyFilterAsync()
        {
            if (_allCourses == null) return;

            string keyword = txtSearch.Text.Trim();
            if (keyword == "Tìm kiếm khóa học...") keyword = "";

            string level = cmbFilterLevel.SelectedItem?.ToString();
            if (level == "Tất cả level") level = null;

            _filteredCourses = _allCourses.Where(c =>
            {
                bool matchName = string.IsNullOrEmpty(keyword) ||
                                c.Title.IndexOf(keyword, StringComparison.OrdinalIgnoreCase) >= 0;
                bool matchLevel = string.IsNullOrEmpty(level) ||
                                 string.Equals(c.Level, level, StringComparison.OrdinalIgnoreCase);
                return matchName && matchLevel;
            }).ToList();

            UpdateFilteredResults();
            RenderCards(_filteredCourses);
            await Task.CompletedTask;
        }

        // =========================================================
        // STATISTICS & UI UPDATES
        // =========================================================

        private void UpdateStatistics()
        {
            if (_allCourses == null) return;

            int total = _allCourses.Count;
            int enrolled = _allCourses.Count(c => c.IsEnrolled);

            lblTotalCourses.Text = $"📚 {total} khóa học • ✅ {enrolled} đã đăng ký";
        }

        private void UpdateFilteredResults()
        {
            if (_filteredCourses == null || _allCourses == null) return;

            if (_filteredCourses.Count < _allCourses.Count)
            {
                lblFilteredResults.Text = $"Hiển thị {_filteredCourses.Count}/{_allCourses.Count} kết quả";
                lblFilteredResults.Visible = true;
            }
            else
            {
                lblFilteredResults.Visible = false;
            }
        }

        private void ShowLoading(bool show)
        {
            if (show)
            {
                pnlLoading.Visible = true;
                pnlLoading.BringToFront();
                _animationStep = 0;
                _animationTimer.Start();
            }
            else
            {
                _animationTimer.Stop();
                pnlLoading.Visible = false;
            }
        }

        private void AnimationTimer_Tick(object sender, EventArgs e)
        {
            _animationStep = (_animationStep + 1) % 4;
            lblLoading.Text = "⏳ Đang tải" + new string('.', _animationStep + 1);
        }

        private void ShowErrorMessage(string message)
        {
            MessageBox.Show(message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        // =========================================================
        // RENDER CARDS - ENHANCED
        // =========================================================

        private void RenderCards(List<CourseDto> courses)
        {
            flpCourseGrid.SuspendLayout();
            flpCourseGrid.Controls.Clear();

            if (courses.Count == 0)
            {
                var emptyPanel = CreateEmptyStatePanel();
                flpCourseGrid.Controls.Add(emptyPanel);
            }
            else
            {
                int cardWidth = 280;
                int cardHeight = 340;

                foreach (var course in courses)
                {
                    var card = CreateEnhancedCourseCard(course, cardWidth, cardHeight);
                    flpCourseGrid.Controls.Add(card);
                }
            }

            flpCourseGrid.ResumeLayout();
        }

        private Panel CreateEmptyStatePanel()
        {
            var panel = new Panel
            {
                Width = flpCourseGrid.Width - 100,
                Height = 200,
                BackColor = Color.White
            };

            var lblIcon = new Label
            {
                Text = "📚",
                Font = new Font("Segoe UI", 48F),
                AutoSize = true,
                Location = new Point((panel.Width - 80) / 2, 30)
            };

            var lblText = new Label
            {
                Text = "Không tìm thấy khóa học nào phù hợp",
                Font = new Font("Segoe UI", 14F),
                ForeColor = clrSecondary,
                AutoSize = true,
                Location = new Point((panel.Width - 300) / 2, 120)
            };

            var lblHint = new Label
            {
                Text = "Thử điều chỉnh bộ lọc hoặc từ khóa tìm kiếm",
                Font = new Font("Segoe UI", 10F),
                ForeColor = clrSecondary,
                AutoSize = true,
                Location = new Point((panel.Width - 280) / 2, 155)
            };

            panel.Controls.AddRange(new Control[] { lblIcon, lblText, lblHint });
            return panel;
        }

        private Panel CreateEnhancedCourseCard(CourseDto dto, int w, int h)
        {
            var card = new Panel
            {
                Width = w,
                Height = h,
                BackColor = Color.White,
                Margin = new Padding(12),
                Cursor = Cursors.Hand,
                Tag = dto
            };

            // Enhanced card shadow & hover effect
            card.Paint += (s, e) =>
            {
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

                // Shadow
                using (var shadowPath = GetRoundedPath(new Rectangle(2, 2, w - 4, h - 4), 12))
                using (var shadowBrush = new SolidBrush(Color.FromArgb(15, 0, 0, 0)))
                {
                    e.Graphics.FillPath(shadowBrush, shadowPath);
                }

                // Card border
                using (var path = GetRoundedPath(new Rectangle(0, 0, w - 1, h - 1), 12))
                using (var pen = new Pen(clrBorder, 1))
                {
                    e.Graphics.DrawPath(pen, path);
                }

                // Level indicator (top-left corner)
                Color levelColor = GetLevelColor(dto.Level);
                using (var brush = new SolidBrush(levelColor))
                {
                    var levelPath = new GraphicsPath();
                    levelPath.AddArc(0, 0, 24, 24, 180, 90);
                    levelPath.AddLine(12, 0, 50, 0);
                    levelPath.AddLine(50, 0, 50, 5);
                    levelPath.AddLine(50, 5, 5, 5);
                    levelPath.AddLine(5, 5, 0, 12);
                    levelPath.CloseFigure();
                    e.Graphics.FillPath(brush, levelPath);
                }
            };

            const int padding = 12;
            const int thumbHeight = 160;
            const int footerHeight = 56; // reserve space for footer/buttons
            int contentTop = thumbHeight + padding;

            // Thumbnail
            var pbThumbnail = new PictureBox
            {
                Width = w,
                Height = thumbHeight,
                Location = new Point(0, 0),
                SizeMode = PictureBoxSizeMode.Zoom,
                BackColor = clrLight
            };

            if (!string.IsNullOrEmpty(dto.Thumbnail))
            {
                LoadImageAsync(pbThumbnail, dto.Thumbnail);
            }
            else
            {
                pbThumbnail.Image = CreateEnhancedPlaceholder(dto.Language);
            }
            card.Controls.Add(pbThumbnail);

            // Level badge (overlay on thumbnail)
            var lblLevel = new Label
            {
                Text = GetLevelDisplayName(dto.Level),
                Font = new Font("Segoe UI", 8F, FontStyle.Bold),
                ForeColor = Color.White,
                BackColor = GetLevelColor(dto.Level),
                AutoSize = true,
                Location = new Point(padding, thumbHeight - 18),
                Padding = new Padding(8, 4, 8, 4)
            };
            lblLevel.BringToFront();
            card.Controls.Add(lblLevel);

            // Title - compute height to avoid overlap and allow wrapping
            var lblTitle = new Label
            {
                Text = dto.Title,
                Font = new Font("Segoe UI", 11F, FontStyle.Bold),
                ForeColor = clrDark,
                AutoSize = false,
                Width = w - (padding * 2),
                Location = new Point(padding, contentTop),
                TextAlign = ContentAlignment.TopLeft
            };

            // Measure required height with word wrap, cap to reasonable lines (2-3 lines)
            int maxTitleHeight = 80;
            var measured = TextRenderer.MeasureText(dto.Title, lblTitle.Font, new Size(lblTitle.Width, maxTitleHeight), TextFormatFlags.WordBreak);
            lblTitle.Height = Math.Min(Math.Max(36, measured.Height), maxTitleHeight);
            lblTitle.AutoEllipsis = true;
            card.Controls.Add(lblTitle);

            // Meta info (rating, students) placed after title
            int metaY = lblTitle.Bottom + 8;
            var lblMeta = new Label
            {
                Text = $"⭐ {dto.Rating:N1}  •  👥 {FormatNumber(dto.TotalStudents)}",
                Font = new Font("Segoe UI", 9F),
                ForeColor = clrSecondary,
                AutoSize = true,
                Location = new Point(padding, metaY)
            };
            card.Controls.Add(lblMeta);

            // Reserve footer area at bottom so content doesn't overlap
            int footerY = h - footerHeight + 8;

            // Status & Action
            // Place price/status above footer if space is tight
            CreateCardFooter(card, dto, w, h);

            // Progress bar for enrolled courses - place just above footer
            if (dto.IsEnrolled && dto.Progress > 0)
            {
                int progressY = footerY - 18;
                var pnlProgressBg = new Panel
                {
                    Width = w - (padding * 2),
                    Height = 6,
                    BackColor = Color.FromArgb(230, 230, 230),
                    Location = new Point(padding, progressY)
                };

                var pnlProgressVal = new Panel
                {
                    Width = (int)((pnlProgressBg.Width * dto.Progress) / 100.0),
                    Height = 6,
                    BackColor = clrSuccess,
                    Location = new Point(0, 0)
                };

                pnlProgressBg.Controls.Add(pnlProgressVal);
                card.Controls.Add(pnlProgressBg);

                // Progress percentage (small)
                var lblProgress = new Label
                {
                    Text = $"{dto.Progress:N0}",
                    Font = new Font("Segoe UI", 8F, FontStyle.Bold),
                    ForeColor = clrSuccess,
                    AutoSize = true,
                    Location = new Point(w - padding - 40, progressY - 2)
                };
                card.Controls.Add(lblProgress);
            }

            // Ensure footer controls don't overlap content: if meta bottom >= footerY - 30, shift meta up
            // (simple adjustment)
            foreach (Control ctl in card.Controls)
            {
                if (ctl == null) continue;
            }

            // Hover effects
            SetupCardHoverEffects(card);

            return card;
        }

        
   

        private void CreateCardFooter(Panel card, CourseDto dto, int w, int h)
        {
            if (dto.IsEnrolled)
            {
                // Enrolled status
                var lblStatus = new Label
                {
                    Text = "✓ Đã sở hữu",
                    Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                    ForeColor = clrSuccess,
                    AutoSize = true,
                    Location = new Point(12, 285)
                };
                card.Controls.Add(lblStatus);

                var btnContinue = new Button
                {
                    Text = "Tiếp tục học",
                    Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                    ForeColor = clrSuccess,
                    BackColor = clrSuccessLight,
                    FlatStyle = FlatStyle.Flat,
                    Size = new Size(100, 32),
                    Location = new Point(w - 112, 280),
                    Cursor = Cursors.Hand
                };
                btnContinue.FlatAppearance.BorderSize = 0;
                btnContinue.Click += (s, e) => NavigateToCourseDetails(dto.CourseID);
                card.Controls.Add(btnContinue);
            }
            else
            {
                // Price
                var lblPrice = new Label
                {
                    Text = dto.Price == 0 ? "MIỄN PHÍ" : $"{dto.Price:N0} đ",
                    Font = new Font("Segoe UI", 11F, FontStyle.Bold),
                    ForeColor = dto.Price == 0 ? clrSuccess : clrPrimary,
                    AutoSize = true,
                    Location = new Point(12, 285)
                };
                card.Controls.Add(lblPrice);

                var btnDetail = new Button
                {
                    Text = "Xem chi tiết →",
                    Font = new Font("Segoe UI", 9F),
                    ForeColor = clrPrimary,
                    BackColor = Color.FromArgb(230, 240, 255),
                    FlatStyle = FlatStyle.Flat,
                    Size = new Size(110, 32),
                    Location = new Point(w - 122, 280),
                    Cursor = Cursors.Hand
                };
                btnDetail.FlatAppearance.BorderSize = 0;
                btnDetail.Click += (s, e) => NavigateToCourseDetails(dto.CourseID);
                card.Controls.Add(btnDetail);
            }
        }

        private void CreateProgressBar(Panel card, double progress, int w, int h)
        {
            var pnlProgressBg = new Panel
            {
                Width = w - 24,
                Height = 6,
                BackColor = Color.FromArgb(230, 230, 230),
                Location = new Point(12, h - 18)
            };

            var pnlProgressVal = new Panel
            {
                Width = (int)((pnlProgressBg.Width * progress) / 100.0),
                Height = 6,
                BackColor = clrSuccess,
                Location = new Point(0, 0)
            };

            pnlProgressBg.Controls.Add(pnlProgressVal);
            card.Controls.Add(pnlProgressBg);

            // Progress percentage
            var lblProgress = new Label
            {
                Text = $"{progress:N0}%",
                Font = new Font("Segoe UI", 8F, FontStyle.Bold),
                ForeColor = clrSuccess,
                AutoSize = true,
                Location = new Point(w - 45, h - 20)
            };
            card.Controls.Add(lblProgress);
        }

        private void SetupCardHoverEffects(Panel card)
        {
            Color originalColor = card.BackColor;
            Color hoverColor = Color.FromArgb(252, 253, 255);

            EventHandler onEnter = (s, e) =>
            {
                card.BackColor = hoverColor;
                card.Invalidate();
            };

            EventHandler onLeave = (s, e) =>
            {
                card.BackColor = originalColor;
                card.Invalidate();
            };

            card.MouseEnter += onEnter;
            card.MouseLeave += onLeave;

            foreach (Control c in card.Controls)
            {
                c.MouseEnter += onEnter;
                c.MouseLeave += onLeave;

                if (c is PictureBox || c is Label)
                {
                    c.Click += (s, e) => NavigateToCourseDetails((card.Tag as CourseDto)?.CourseID ?? Guid.Empty);
                }
            }
        }

        // =========================================================
        // HELPER METHODS
        // =========================================================

        private Color GetLevelColor(string level)
        {
            return level?.ToLower() switch
            {
                "beginner" => clrBeginner,
                "advanced" => clrAdvanced,
                _ => clrIntermediate
            };
        }

        private string GetLevelDisplayName(string level)
        {
            return level?.ToLower() switch
            {
                "beginner" => "Cơ bản",
                "intermediate" => "Trung cấp",
                "advanced" => "Nâng cao",
                _ => "Trung cấp"
            };
        }

        private string FormatNumber(int number)
        {
            if (number >= 1000000) return $"{number / 1000000.0:N1}M";
            if (number >= 1000) return $"{number / 1000.0:N1}K";
            return number.ToString();
        }

        private void TruncateText(Label label)
        {
            using (var g = label.CreateGraphics())
            {
                var size = g.MeasureString(label.Text, label.Font);
                if (size.Width > label.Width || size.Height > label.Height)
                {
                    string text = label.Text;
                    while (g.MeasureString(text + "...", label.Font).Width > label.Width && text.Length > 0)
                    {
                        text = text.Substring(0, text.Length - 1);
                    }
                    label.Text = text + "...";
                }
            }
        }

        private async void LoadImageAsync(PictureBox pb, string url)
        {
            try
            {
                if (Uri.IsWellFormedUriString(url, UriKind.Absolute))
                {
                    using (var client = new HttpClient { Timeout = TimeSpan.FromSeconds(10) })
                    {
                        var bytes = await client.GetByteArrayAsync(url);
                        using (var ms = new MemoryStream(bytes))
                        {
                            pb.Image = Image.FromStream(ms);
                        }
                    }
                }
                else if (File.Exists(url))
                {
                    using (var fs = new FileStream(url, FileMode.Open, FileAccess.Read))
                    {
                        pb.Image = Image.FromStream(fs);
                    }
                }
            }
            catch
            {
                // Keep placeholder
            }
        }

        private Image CreateEnhancedPlaceholder(string language)
        {
            var bmp = new Bitmap(280, 160);
            using (var g = Graphics.FromImage(bmp))
            {
                g.SmoothingMode = SmoothingMode.AntiAlias;
                g.Clear(Color.FromArgb(245, 247, 250));

                // Gradient background
                using (var brush = new LinearGradientBrush(
                    new Rectangle(0, 0, 280, 160),
                    Color.FromArgb(240, 242, 245),
                    Color.FromArgb(250, 251, 252),
                    45f))
                {
                    g.FillRectangle(brush, 0, 0, 280, 160);
                }

                // Language icon
                Color iconColor = GetLanguageColor(language);
                using (var brush = new SolidBrush(iconColor))
                {
                    g.FillEllipse(brush, 90, 40, 100, 100);
                }

                // Language initial
                string letter = string.IsNullOrEmpty(language) ? "?" : language.Substring(0, 1).ToUpper();
                using (var brush = new SolidBrush(Color.White))
                {
                    var sf = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center };
                    g.DrawString(letter, new Font("Segoe UI", 36, FontStyle.Bold), brush, 140, 90, sf);
                }
            }
            return bmp;
        }

        private Color GetLanguageColor(string language)
        {
            if (string.IsNullOrEmpty(language)) return clrSecondary;

            return language.ToLower() switch
            {
                var l when l.Contains("c#") => Color.FromArgb(128, 0, 128),
                var l when l.Contains("python") => Color.FromArgb(54, 130, 195),
                var l when l.Contains("java") => Color.FromArgb(244, 67, 54),
                var l when l.Contains("javascript") => Color.FromArgb(240, 219, 79),
                var l when l.Contains("php") => Color.FromArgb(119, 123, 180),
                _ => Color.FromArgb(255, 152, 0)
            };
        }

        private GraphicsPath GetRoundedPath(Rectangle rect, int radius)
        {
            var path = new GraphicsPath();
            float d = radius * 2f;
            path.AddArc(rect.X, rect.Y, d, d, 180, 90);
            path.AddArc(rect.Right - d, rect.Y, d, d, 270, 90);
            path.AddArc(rect.Right - d, rect.Bottom - d, d, d, 0, 90);
            path.AddArc(rect.X, rect.Bottom - d, d, d, 90, 90);
            path.CloseFigure();
            return path;
        }

        private void NavigateToCourseDetails(Guid courseId)
        {
            if (courseId == Guid.Empty) return;
            MainFormStudent.Instance?.NavigateTo(new ucCourseDetails(courseId));
        }

        // =========================================================
        // EVENT HANDLERS
        // =========================================================

        private async void btnHeroAction_Click(object sender, EventArgs e)
        {
            try
            {
                var list = _allCourses ?? new List<CourseDto>();
                if (list.Count == 0)
                {
                    MessageBox.Show("Không có khóa học để tạo lộ trình.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                using (var dlg = new Form { Text = "Chọn khóa học để tạo lộ trình", Width = 420, Height = 460, StartPosition = FormStartPosition.CenterParent })
                {
                    var lb = new ListBox { Dock = DockStyle.Fill, DisplayMember = "Title" };
                    lb.Items.AddRange(list.ToArray());
                    if (lb.Items.Count > 0) lb.SelectedIndex = 0;

                    var pnlBottom = new Panel { Dock = DockStyle.Bottom, Height = 48, Padding = new Padding(8) };
                    var btnOk = new Button { Text = "Tạo lộ trình", Dock = DockStyle.Right, Width = 120, BackColor = Color.FromArgb(13, 110, 253), ForeColor = Color.White, FlatStyle = FlatStyle.Flat };
                    btnOk.FlatAppearance.BorderSize = 0;
                    var btnCancel = new Button { Text = "Hủy", Dock = DockStyle.Right, Width = 80 };
                    btnCancel.Click += (s, ev) => dlg.DialogResult = DialogResult.Cancel;
                    btnOk.Click += (s, ev) => dlg.DialogResult = DialogResult.OK;

                    pnlBottom.Controls.Add(btnCancel);
                    pnlBottom.Controls.Add(btnOk);

                    dlg.Controls.Add(lb);
                    dlg.Controls.Add(pnlBottom);

                    if (dlg.ShowDialog(this.FindForm()) != DialogResult.OK) return;

                    var selected = lb.SelectedItem as CourseDto ?? list.First();
                    // Find the dock and run learning path
                    var dock = MainFormStudent.Instance?.Controls.OfType<UserControl>().OfType<ucAIChatDock>().FirstOrDefault();
                    if (dock == null)
                    {
                        MessageBox.Show("AI Chat chưa sẵn sàng. Vui lòng thử lại sau.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    await dock.RunLearningPathAsync(selected);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tạo lộ trình: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnHeroAction_MouseEnter(object sender, EventArgs e)
        {
            btnHeroAction.BackColor = clrPrimaryHover;
        }

        private void btnHeroAction_MouseLeave(object sender, EventArgs e)
        {
            btnHeroAction.BackColor = clrPrimary;
        }

        private void btnClearFilters_Click(object sender, EventArgs e)
        {
            txtSearch.Text = "Tìm kiếm khóa học...";
            txtSearch.ForeColor = Color.Gray;
            cmbFilterLevel.SelectedIndex = 0;
        }

        private void txtSearch_Enter(object sender, EventArgs e)
        {
            if (txtSearch.Text == "Tìm kiếm khóa học...")
            {
                txtSearch.Text = "";
                txtSearch.ForeColor = clrDark;
            }
        }

        private void txtSearch_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtSearch.Text))
            {
                txtSearch.Text = "Tìm kiếm khóa học...";
                txtSearch.ForeColor = Color.Gray;
            }
        }
    }
}