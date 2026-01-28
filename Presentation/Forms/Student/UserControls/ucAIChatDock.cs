using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using CodeForge_Desktop.Business.DTOs;

namespace CodeForge_Desktop.Presentation.Forms.Student.UserControls
{
    public enum DockSide
    {
        Left,
        Right
    }

    public class ucAIChatDock : UserControl
    {
        private readonly Panel pnlDock;
        private readonly Panel pnlHeader;
        private readonly Button btnToggle;
        private readonly Button btnClose;
        private readonly Button btnClear;
        private readonly Button btnRefresh;
        private readonly Button btnPin;
        private readonly ucAIChat aiChat;
        private bool _pinned = false;

        // Host references
        private Form _hostForm;
        private Control _hostContainer;

        // Animation
        private readonly Timer _animTimer;
        private readonly Timer _autoHideTimer;
        private readonly Timer _togglePulseTimer;
        private readonly Timer _toggleRotationTimer;
        private int _animTargetLeft;
        private bool _animOpening;
        private float _toggleScale = 1.0f;
        private float _toggleRotation = 0f;

        // Modern color palette
        private readonly Color COLOR_PRIMARY = Color.FromArgb(13, 110, 253);
        private readonly Color COLOR_PRIMARY_HOVER = Color.FromArgb(10, 88, 202);
        private readonly Color COLOR_SUCCESS = Color.FromArgb(25, 135, 84);
        private readonly Color COLOR_SURFACE = Color.FromArgb(255, 255, 255);
        private readonly Color COLOR_BACKGROUND = Color.FromArgb(250, 251, 252);
        private readonly Color COLOR_BORDER = Color.FromArgb(220, 224, 228);
        private readonly Color COLOR_TEXT = Color.FromArgb(33, 37, 41);
        private readonly Color COLOR_TEXT_SECONDARY = Color.FromArgb(108, 117, 125);

        // Configurable
        public DockSide ToggleSide { get; private set; } = DockSide.Right;
        public bool AutoHide { get; private set; } = false;
        public bool EnableAnimation { get; private set; } = true;
        public int AutoHideDelayMs { get; set; } = 1500;

        public ucAIChatDock()
        {
            this.Name = "ucAIChatDock";
            this.Width = 440;  // Slightly wider for better content display
            this.Height = 600; // Taller for more messages
            this.BackColor = Color.Transparent;

            // Enhanced toggle button with better design
            btnToggle = new Button
            {
                Width = 68,
                Height = 68,
                BackColor = COLOR_PRIMARY,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Text = "",
                Font = new Font("Segoe UI", 12f, FontStyle.Bold),
                Cursor = Cursors.Hand,
                TabStop = false
            };
            btnToggle.FlatAppearance.BorderSize = 0;
            btnToggle.Paint += BtnToggle_Paint;
            btnToggle.MouseEnter += (s, e) =>
            {
                btnToggle.BackColor = COLOR_PRIMARY_HOVER;
                _toggleScale = 1.12f;
                btnToggle.Invalidate();
            };
            btnToggle.MouseLeave += (s, e) =>
            {
                btnToggle.BackColor = pnlDock.Visible ? COLOR_SUCCESS : COLOR_PRIMARY;
                _toggleScale = 1.0f;
                btnToggle.Invalidate();
            };
            btnToggle.MouseDown += (s, e) =>
            {
                _toggleScale = 0.95f;
                btnToggle.Invalidate();
            };
            btnToggle.MouseUp += (s, e) =>
            {
                _toggleScale = 1.12f;
                btnToggle.Invalidate();
            };
            btnToggle.Click += (s, e) => ToggleDockVisibility();

            // Add tooltip to toggle button
            var toggleTooltip = new ToolTip();
            toggleTooltip.SetToolTip(btnToggle, "Click để mở/đóng CodeForge AI\nTrợ lý học tập thông minh của bạn");

            // Dock panel with modern glassmorphism effect
            pnlDock = new Panel
            {
                Width = this.Width,
                Height = this.Height,
                BackColor = COLOR_SURFACE,
                BorderStyle = BorderStyle.None,
                Visible = false
            };
            pnlDock.Paint += PnlDock_Paint;

            // Enhanced header with better visual hierarchy
            pnlHeader = new Panel
            {
                Dock = DockStyle.Top,
                Height = 64,
                BackColor = COLOR_SURFACE
            };
            pnlHeader.Paint += PnlHeader_Paint;

            // Title with icon
            var lblTitle = new Label
            {
                Text = "🤖 CodeForge AI",
                AutoSize = false,
                TextAlign = ContentAlignment.MiddleLeft,
                Dock = DockStyle.Fill,
                Padding = new Padding(24, 0, 0, 0),
                Font = new Font("Segoe UI Semibold", 12f, FontStyle.Bold),
                ForeColor = COLOR_TEXT
            };

            // Subtitle
            var lblSubtitle = new Label
            {
                Text = "Trợ lý học tập thông minh",
                AutoSize = false,
                TextAlign = ContentAlignment.MiddleLeft,
                Location = new Point(24, 36),
                Size = new Size(200, 20),
                Font = new Font("Segoe UI", 8.5f),
                ForeColor = COLOR_TEXT_SECONDARY
            };

            // Enhanced header buttons with better icons
            btnClose = CreateHeaderButton("✕", "Đóng (Esc)");
            btnPin = CreateHeaderButton("📌", "Ghim cửa sổ");
            btnClear = CreateHeaderButton("🗑", "Xóa lịch sử chat");
            btnRefresh = CreateHeaderButton("↻", "Làm mới");

            pnlHeader.Controls.Add(lblSubtitle);
            pnlHeader.Controls.Add(lblTitle);
            pnlHeader.Controls.Add(btnClose);
            pnlHeader.Controls.Add(btnPin);
            pnlHeader.Controls.Add(btnClear);
            pnlHeader.Controls.Add(btnRefresh);
            lblSubtitle.BringToFront();

            // AI chat control with improved styling
            aiChat = new ucAIChat
            {
                Dock = DockStyle.Fill
            };

            pnlDock.Controls.Add(aiChat);
            pnlDock.Controls.Add(pnlHeader);

            // Add to wrapper
            this.Controls.Add(btnToggle);
            this.Controls.Add(pnlDock);

            // Wire up actions
            btnClose.Click += (s, e) => HideDock();
            btnClear.Click += (s, e) =>
            {
                if (MessageBox.Show(
                    "Bạn có chắc muốn xóa toàn bộ lịch sử chat?",
                    "Xác nhận xóa",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    aiChat.ClearHistory();
                }
            };
            btnRefresh.Click += (s, e) => aiChat.RefreshConversation();
            btnPin.Click += (s, e) => TogglePin();

            // Enhanced animation timers
            _animTimer = new Timer { Interval = 16 }; // 60fps
            _animTimer.Tick += AnimTimer_Tick;

            _autoHideTimer = new Timer { Interval = AutoHideDelayMs };
            _autoHideTimer.Tick += (s, e) =>
            {
                _autoHideTimer.Stop();
                if (!_pinned && pnlDock.Visible && !IsMouseOverDock())
                {
                    HideDock();
                }
            };

            // Smooth rotation animation for toggle button
            _toggleRotationTimer = new Timer { Interval = 16 };
            _toggleRotationTimer.Tick += (s, e) =>
            {
                _toggleRotation += 2f;
                if (_toggleRotation >= 360f) _toggleRotation = 0f;
                btnToggle.Invalidate();
            };

            // Breathing pulse animation when closed
            _togglePulseTimer = new Timer { Interval = 3000 };
            _togglePulseTimer.Tick += (s, e) =>
            {
                if (!pnlDock.Visible)
                {
                    AnimateTogglePulse();
                }
            };
            _togglePulseTimer.Start();

            // Mouse interactions with better feedback
            pnlDock.MouseEnter += (s, e) =>
            {
                _autoHideTimer.Stop();
            };

            pnlDock.MouseLeave += (s, e) =>
            {
                if (AutoHide && !_pinned && !IsMouseOverDock())
                    _autoHideTimer.Start();
            };

            btnToggle.MouseEnter += (s, e) =>
            {
                _autoHideTimer.Stop();
            };

            this.Load += (s, e) => AttachToHost();

            // Keyboard shortcuts
        }

        private bool IsMouseOverDock()
        {
            if (pnlDock == null || !pnlDock.Visible) return false;

            var mousePos = pnlDock.PointToClient(Cursor.Position);
            return pnlDock.ClientRectangle.Contains(mousePos);
        }

        private void AnimateTogglePulse()
        {
            var pulseTimer = new Timer { Interval = 30 };
            float originalScale = 1.0f;
            int frames = 0;
            int maxFrames = 30;

            pulseTimer.Tick += (s, e) =>
            {
                frames++;

                // Smooth pulse using sine wave
                float progress = (float)frames / maxFrames;
                _toggleScale = originalScale + (float)(Math.Sin(progress * Math.PI * 2) * 0.1);

                btnToggle?.Invalidate();

                if (frames >= maxFrames)
                {
                    _toggleScale = originalScale;
                    btnToggle?.Invalidate();
                    pulseTimer.Stop();
                    pulseTimer.Dispose();
                }
            };
            pulseTimer.Start();
        }

        private Button CreateHeaderButton(string text, string tooltip)
        {
            var btn = new Button
            {
                Text = text,
                Width = 44,
                Height = 44,
                Dock = DockStyle.Right,
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.Transparent,
                Cursor = Cursors.Hand,
                Font = new Font("Segoe UI", 13f),
                ForeColor = COLOR_TEXT_SECONDARY,
                Margin = new Padding(4)
            };
            btn.FlatAppearance.BorderSize = 0;
            btn.FlatAppearance.MouseOverBackColor = Color.FromArgb(240, 242, 245);
            btn.FlatAppearance.MouseDownBackColor = Color.FromArgb(230, 232, 235);

            // Add rounded corners to header buttons
            btn.Paint += (s, e) =>
            {
                var b = s as Button;
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

                if (b.ClientRectangle.Contains(b.PointToClient(Cursor.Position)))
                {
                    using (var path = GetRoundedRectPath(new Rectangle(2, 2, b.Width - 4, b.Height - 4), 8))
                    using (var brush = new SolidBrush(b.FlatAppearance.MouseOverBackColor))
                    {
                        e.Graphics.FillPath(brush, path);
                    }
                }
            };

            var toolTip = new ToolTip
            {
                InitialDelay = 500,
                AutoPopDelay = 5000,
                ShowAlways = true
            };
            toolTip.SetToolTip(btn, tooltip);

            return btn;
        }

        // Enhanced custom paint methods
        private void BtnToggle_Paint(object sender, PaintEventArgs e)
        {
            var btn = sender as Button;
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            e.Graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

            // Apply scale transform
            var state = e.Graphics.Save();
            e.Graphics.TranslateTransform(btn.Width / 2, btn.Height / 2);
            e.Graphics.ScaleTransform(_toggleScale, _toggleScale);
            e.Graphics.TranslateTransform(-btn.Width / 2, -btn.Height / 2);

            // Multi-layer shadow for depth
            for (int i = 3; i > 0; i--)
            {
                int alpha = 15 * i;
                int offset = i * 2;
                using (var shadowPath = new GraphicsPath())
                {
                    shadowPath.AddEllipse(offset, offset, btn.Width - offset * 2, btn.Height - offset * 2);
                    using (var shadowBrush = new PathGradientBrush(shadowPath))
                    {
                        shadowBrush.CenterColor = Color.FromArgb(alpha, 0, 0, 0);
                        shadowBrush.SurroundColors = new[] { Color.FromArgb(0, 0, 0, 0) };
                        e.Graphics.FillPath(shadowBrush, shadowPath);
                    }
                }
            }

            // Gradient background with enhanced colors
            using (var path = new GraphicsPath())
            {
                path.AddEllipse(3, 3, btn.Width - 6, btn.Height - 6);

                using (var brush = new LinearGradientBrush(
                    new Point(0, 0),
                    new Point(btn.Width, btn.Height),
                    btn.BackColor,
                    Color.FromArgb(
                        Math.Max(0, btn.BackColor.R - 35),
                        Math.Max(0, btn.BackColor.G - 35),
                        Math.Max(0, btn.BackColor.B - 35))))
                {
                    // Add color blend for smoother gradient
                    ColorBlend colorBlend = new ColorBlend();
                    colorBlend.Colors = new Color[]
                    {
                        btn.BackColor,
                        Color.FromArgb(
                            Math.Max(0, btn.BackColor.R - 20),
                            Math.Max(0, btn.BackColor.G - 20),
                            Math.Max(0, btn.BackColor.B - 20)),
                        Color.FromArgb(
                            Math.Max(0, btn.BackColor.R - 35),
                            Math.Max(0, btn.BackColor.G - 35),
                            Math.Max(0, btn.BackColor.B - 35))
                    };
                    colorBlend.Positions = new float[] { 0.0f, 0.6f, 1.0f };
                    brush.InterpolationColors = colorBlend;

                    e.Graphics.FillPath(brush, path);
                }

                // Glass highlight effect
                using (var highlightPath = new GraphicsPath())
                {
                    highlightPath.AddArc(3, 3, btn.Width - 6, btn.Height - 6, 180, 180);
                    using (var highlightBrush = new LinearGradientBrush(
                        new Point(0, 3),
                        new Point(0, btn.Height / 2),
                        Color.FromArgb(40, 255, 255, 255),
                        Color.FromArgb(0, 255, 255, 255)))
                    {
                        e.Graphics.FillPath(highlightBrush, highlightPath);
                    }
                }

                // Outer glow when dock is visible
                if (pnlDock.Visible)
                {
                    using (var glowPen = new Pen(Color.FromArgb(60, 255, 255, 255), 2))
                    {
                        e.Graphics.DrawEllipse(glowPen, 1, 1, btn.Width - 2, btn.Height - 2);
                    }
                }
            }

            // AI icon (sparkle effect) with rotation
            var iconState = e.Graphics.Save();
            e.Graphics.TranslateTransform(btn.Width / 2, btn.Height / 2);

            if (pnlDock.Visible)
            {
                // Rotate when dock is visible
                e.Graphics.RotateTransform(_toggleRotation * 0.1f);
            }

            e.Graphics.TranslateTransform(-btn.Width / 2, -btn.Height / 2);
            DrawEnhancedSparkleIcon(e.Graphics, btn.Width / 2, btn.Height / 2, 20);
            e.Graphics.Restore(iconState);

            // Notification badge (if needed)
            if (!pnlDock.Visible)
            {
                DrawNotificationBadge(e.Graphics, btn.Width - 12, 12);
            }

            e.Graphics.Restore(state);
        }

        private void DrawEnhancedSparkleIcon(Graphics g, float centerX, float centerY, float size)
        {
            using (var pen = new Pen(Color.White, 3f))
            {
                pen.StartCap = LineCap.Round;
                pen.EndCap = LineCap.Round;

                // Main sparkle lines
                g.DrawLine(pen, centerX, centerY - size / 2, centerX, centerY + size / 2);
                g.DrawLine(pen, centerX - size / 2, centerY, centerX + size / 2, centerY);

                // Diagonal lines
                float diag = size * 0.35f;
                g.DrawLine(pen, centerX - diag, centerY - diag, centerX + diag, centerY + diag);
                g.DrawLine(pen, centerX - diag, centerY + diag, centerX + diag, centerY - diag);
            }

            // Add glow effect
            using (var glowPen = new Pen(Color.FromArgb(100, 255, 255, 255), 5f))
            {
                glowPen.StartCap = LineCap.Round;
                glowPen.EndCap = LineCap.Round;

                g.DrawLine(glowPen, centerX, centerY - size / 2, centerX, centerY + size / 2);
                g.DrawLine(glowPen, centerX - size / 2, centerY, centerX + size / 2, centerY);
            }
        }

        private void DrawNotificationBadge(Graphics g, float x, float y)
        {
            float badgeSize = 10;

            // Badge background
            using (var badgeBrush = new SolidBrush(Color.FromArgb(220, 53, 69)))
            {
                g.FillEllipse(badgeBrush, x - badgeSize / 2, y - badgeSize / 2, badgeSize, badgeSize);
            }

            // Badge border
            using (var badgePen = new Pen(Color.White, 2f))
            {
                g.DrawEllipse(badgePen, x - badgeSize / 2, y - badgeSize / 2, badgeSize, badgeSize);
            }
        }

        private void PnlDock_Paint(object sender, PaintEventArgs e)
        {
            var pnl = sender as Panel;
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            // Enhanced rounded corners with shadow
            using (var path = GetRoundedRectPath(new Rectangle(0, 0, pnl.Width - 1, pnl.Height - 1), 20))
            {
                // Multi-layer shadow
                for (int i = 3; i >= 0; i--)
                {
                    int alpha = 8 + (i * 4);
                    int offset = i * 2;
                    using (var shadowBrush = new SolidBrush(Color.FromArgb(alpha, 0, 0, 0)))
                    {
                        var shadowRect = new Rectangle(offset, offset + 4, pnl.Width - offset, pnl.Height - offset);
                        using (var shadowPath = GetRoundedRectPath(shadowRect, 20))
                        {
                            e.Graphics.FillPath(shadowBrush, shadowPath);
                        }
                    }
                }

                // Main background with gradient
                using (var brush = new LinearGradientBrush(
                    new Point(0, 0),
                    new Point(0, pnl.Height),
                    COLOR_SURFACE,
                    Color.FromArgb(248, 249, 250)))
                {
                    e.Graphics.FillPath(brush, path);
                }

                // Border with enhanced color
                using (var pen = new Pen(COLOR_BORDER, 1))
                {
                    e.Graphics.DrawPath(pen, path);
                }

                // Inner highlight
                using (var highlightPath = GetRoundedRectPath(
                    new Rectangle(1, 1, pnl.Width - 3, pnl.Height / 3), 19))
                {
                    using (var highlightBrush = new LinearGradientBrush(
                        new Point(0, 1),
                        new Point(0, pnl.Height / 3),
                        Color.FromArgb(20, 255, 255, 255),
                        Color.FromArgb(0, 255, 255, 255)))
                    {
                        e.Graphics.FillPath(highlightBrush, highlightPath);
                    }
                }
            }
        }

        private void PnlHeader_Paint(object sender, PaintEventArgs e)
        {
            var pnl = sender as Panel;
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            // Gradient background
            using (var brush = new LinearGradientBrush(
                new Point(0, 0),
                new Point(0, pnl.Height),
                COLOR_SURFACE,
                Color.FromArgb(250, 251, 252)))
            {
                e.Graphics.FillRectangle(brush, pnl.ClientRectangle);
            }

            // Bottom border with shadow
            using (var shadowBrush = new LinearGradientBrush(
                new Point(0, pnl.Height - 4),
                new Point(0, pnl.Height),
                Color.FromArgb(15, 0, 0, 0),
                Color.Transparent))
            {
                e.Graphics.FillRectangle(shadowBrush, 0, pnl.Height - 4, pnl.Width, 4);
            }

            // Bottom line
            using (var pen = new Pen(Color.FromArgb(233, 236, 239), 1))
            {
                e.Graphics.DrawLine(pen, 0, pnl.Height - 1, pnl.Width, pnl.Height - 1);
            }
        }

        private GraphicsPath GetRoundedRectPath(Rectangle rect, int radius)
        {
            var path = new GraphicsPath();
            int diameter = radius * 2;

            if (rect.Width < diameter || rect.Height < diameter)
            {
                path.AddRectangle(rect);
                return path;
            }

            path.AddArc(rect.X, rect.Y, diameter, diameter, 180, 90);
            path.AddArc(rect.Right - diameter, rect.Y, diameter, diameter, 270, 90);
            path.AddArc(rect.Right - diameter, rect.Bottom - diameter, diameter, diameter, 0, 90);
            path.AddArc(rect.X, rect.Bottom - diameter, diameter, diameter, 90, 90);
            path.CloseFigure();

            return path;
        }

        public void Configure(DockSide side = DockSide.Right, bool autoHide = false, bool enableAnimation = true, int autoHideDelayMs = 1500)
        {
            ToggleSide = side;
            AutoHide = autoHide;
            EnableAnimation = enableAnimation;
            AutoHideDelayMs = autoHideDelayMs;
            _autoHideTimer.Interval = AutoHideDelayMs;
            PositionElements();
        }

        private void AttachToHost()
        {
            _hostForm = this.FindForm();
            if (_hostForm == null) return;

            var preferred = _hostForm.Controls.Find("pnlContent", true).FirstOrDefault() as Control;
            _hostContainer = preferred ?? (Control)_hostForm;

            btnToggle.Parent = _hostForm;
            pnlDock.Parent = _hostForm;

            PositionElements();

            _hostForm.Resize += (s, e) => PositionElements();
            _hostForm.Move += (s, e) => PositionElements();
            _hostForm.ControlAdded += (s, e) => PositionElements();
            _hostForm.ControlRemoved += (s, e) => PositionElements();

            // Keyboard shortcuts
            _hostForm.KeyPreview = true;
            _hostForm.KeyDown += (s, e) =>
            {
                if (e.KeyCode == Keys.Escape && pnlDock.Visible)
                {
                    HideDock();
                    e.Handled = true;
                }
            };

            try
            {
                this.Visible = false;
                this.Size = new Size(0, 0);
                this.Location = new Point(-10000, -10000);
            }
            catch { }
        }

        private void PositionElements()
        {
            if (_hostForm == null) return;

            int margin = 28;
            int sidebarOffset = 0;

            try
            {
                var sidebarCtrl = _hostForm.Controls.Find("pnlSidebar", true).FirstOrDefault() as Control;
                if (sidebarCtrl != null && sidebarCtrl.Visible)
                {
                    if (sidebarCtrl.Dock == DockStyle.Left)
                        sidebarOffset = sidebarCtrl.Width;
                    else
                        sidebarOffset = Math.Max(0, sidebarCtrl.Right);
                }
            }
            catch { sidebarOffset = 0; }

            var cs = _hostForm.ClientSize;

            // Position toggle button
            btnToggle.Left = cs.Width - btnToggle.Width - margin;
            btnToggle.Top = cs.Height - btnToggle.Height - margin;
            btnToggle.BringToFront();

            // Position dock panel
            pnlDock.Left = Math.Max(margin, cs.Width - pnlDock.Width - margin);
            pnlDock.Top = Math.Max(margin, cs.Height - pnlDock.Height - margin - btnToggle.Height - 16);
            pnlDock.BringToFront();

            if (!pnlDock.Visible && !EnableAnimation)
            {
                pnlDock.Left = cs.Width + 20;
            }
        }

        private void ToggleDockVisibility()
        {
            if (pnlDock.Visible)
            {
                HideDock();
            }
            else
            {
                ShowDock();
            }
        }

        private void ShowDock()
        {
            if (_hostForm == null) AttachToHost();

            pnlDock.Visible = true;
            pnlDock.BringToFront();
            btnToggle.BackColor = COLOR_SUCCESS;

            // Start rotation animation
            _toggleRotationTimer.Start();

            if (EnableAnimation && _hostForm != null)
            {
                int margin = 28;
                int targetLeft = Math.Max(margin, _hostForm.ClientSize.Width - pnlDock.Width - margin);
                _animTargetLeft = targetLeft;
                _animOpening = true;

                pnlDock.Left = _hostForm.ClientSize.Width + 20;
                _animTimer.Start();
            }
            else
            {
                PositionElements();
                if (AutoHide && !_pinned) _autoHideTimer.Start();
            }
        }

        private void HideDock()
        {
            // Stop rotation animation
            _toggleRotationTimer.Stop();
            _toggleRotation = 0f;

            if (EnableAnimation && _hostForm != null)
            {
                _animOpening = false;
                _animTargetLeft = _hostForm.ClientSize.Width + 20;
                _animTimer.Start();
            }
            else
            {
                pnlDock.Visible = false;
                btnToggle.BackColor = COLOR_PRIMARY;
            }
        }

        private void AnimTimer_Tick(object sender, EventArgs e)
        {
            if (_hostForm == null)
            {
                _animTimer.Stop();
                return;
            }

            int current = pnlDock.Left;
            int target = _animTargetLeft;
            int distance = Math.Abs(target - current);

            // Easing function for smooth animation
            float t = 1f - ((float)distance / (_hostForm.ClientSize.Width + 20));
            t = t * t * (3f - 2f * t); // Smoothstep

            int step = Math.Max(1, (int)(distance * 0.25f));
            int dir = target > current ? 1 : -1;
            int next = current + dir * step;

            if (distance < 3)
            {
                pnlDock.Left = target;
                _animTimer.Stop();

                if (!_animOpening)
                {
                    pnlDock.Visible = false;
                    btnToggle.BackColor = COLOR_PRIMARY;
                }
                else
                {
                    if (AutoHide && !_pinned) _autoHideTimer.Start();
                }
            }
            else
            {
                pnlDock.Left = next;
            }
        }

        public async Task RunLearningPathAsync(CourseDto course)
        {
            if (course == null) return;

            ShowDock();

            string context = $"📚 Khóa học: {course.Title}\n" +
                           $"📊 Cấp độ: {course.Level}\n" +
                           $"💻 Ngôn ngữ: {course.Language}\n" +
                           $"📝 Tổng quan: {course.Overview ?? course.Description ?? ""}\n" +
                           $"📖 Số bài học: {course.LessonCount}";

            aiChat.SetContext(context);

            string prompt = $"🎯 Bạn là trợ lý học tập AI chuyên nghiệp.\n\n" +
                           $"Hãy xây dựng lộ trình học (learning path) chi tiết và có cấu trúc cho khóa học \"{course.Title}\".\n\n" +
                           "📋 Vui lòng bao gồm:\n" +
                           "1. 📚 Tổng quan khóa học\n" +
                           "2. 🎯 Mục tiêu học tập chính\n" +
                           "3. 📖 Danh sách module/chương (nếu có)\n" +
                           "4. ⏱️ Thời lượng ước tính cho từng phần\n" +
                           "5. 💡 Mẹo học hiệu quả\n" +
                           "6. ✅ Checklist theo dõi tiến độ\n" +
                           "7. 🎓 Kết quả đạt được sau khóa học\n\n" +
                           "Trả lời bằng tiếng Việt với format dễ đọc, có cấu trúc rõ ràng và professional.";

            await aiChat.SendMessageAsync(prompt);
        }

        private void TogglePin()
        {
            _pinned = !_pinned;
            btnPin.BackColor = _pinned ? Color.FromArgb(255, 243, 205) : Color.Transparent;
            btnPin.ForeColor = _pinned ? Color.FromArgb(133, 100, 4) : COLOR_TEXT_SECONDARY;
            btnPin.Text = _pinned ? "📍" : "📌";

            if (_pinned)
            {
                _autoHideTimer.Stop();
                pnlDock.Visible = true;

                // Show tooltip
                var tooltip = new ToolTip();
                tooltip.Show("Đã ghim cửa sổ", btnPin, 0, -30, 2000);
            }
            else
            {
                // Show tooltip
                var tooltip = new ToolTip();
                tooltip.Show("Đã bỏ ghim", btnPin, 0, -30, 2000);
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _animTimer?.Stop();
                _animTimer?.Dispose();
                _autoHideTimer?.Stop();
                _autoHideTimer?.Dispose();
                _togglePulseTimer?.Stop();
                _togglePulseTimer?.Dispose();
                _toggleRotationTimer?.Stop();
                _toggleRotationTimer?.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}