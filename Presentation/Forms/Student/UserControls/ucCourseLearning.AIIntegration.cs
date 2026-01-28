using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace CodeForge_Desktop.Presentation.Forms.Student.UserControls
{
    public partial class ucCourseLearning
    {
        private Button btnExplainCode;
        private bool _aiIntegrationInitialized = false;
        private Timer _buttonHoverTimer;
        private Timer _buttonPulseTimer;
        private float _buttonScale = 1.0f;
        private float _iconRotation = 0f;
        private bool _isHovering = false;

        // Modern color palette
        private readonly Color COLOR_PRIMARY = Color.FromArgb(13, 110, 253);
        private readonly Color COLOR_PRIMARY_HOVER = Color.FromArgb(10, 88, 202);
        private readonly Color COLOR_PRIMARY_ACTIVE = Color.FromArgb(8, 70, 162);

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (!_aiIntegrationInitialized)
            {
                InitializeAIIntegration();
                _aiIntegrationInitialized = true;
            }
        }

        /// <summary>
        /// Khởi tạo UI tích hợp AI với thiết kế hiện đại và professional
        /// </summary>
        private void InitializeAIIntegration()
        {
            try
            {
                if (this.pnlRightContainer == null) return;

                // Check if button already exists
                foreach (Control c in pnlRightContainer.Controls)
                {
                    if (c is Button b && b.Name == "btnExplainCode")
                    {
                        btnExplainCode = b;
                        return;
                    }
                }

                // Create modern AI button with enhanced design
                btnExplainCode = new Button
                {
                    Name = "btnExplainCode",
                    Text = "",
                    Height = 48,  // Increased for better touch target
                    Width = 180,  // Wider for better text visibility
                    BackColor = COLOR_PRIMARY,
                    ForeColor = Color.White,
                    FlatStyle = FlatStyle.Flat,
                    Font = new Font("Segoe UI Semibold", 10.5f, FontStyle.Bold),
                    Cursor = Cursors.Hand,
                    Anchor = AnchorStyles.Top | AnchorStyles.Right,
                    TabStop = false
                };
                btnExplainCode.FlatAppearance.BorderSize = 0;
                btnExplainCode.Paint += BtnExplainCode_Paint;

                // Position button with better spacing
                btnExplainCode.Location = new Point(
                    Math.Max(16, pnlRightContainer.ClientSize.Width - btnExplainCode.Width - 20),
                    16);

                // Handle container resize
                pnlRightContainer.Resize += (s, e) =>
                {
                    btnExplainCode.Location = new Point(
                        Math.Max(16, pnlRightContainer.ClientSize.Width - btnExplainCode.Width - 20),
                        16);
                };

                // Smooth hover animation timer
                _buttonHoverTimer = new Timer { Interval = 16 }; // 60fps
                _buttonHoverTimer.Tick += (s, e) =>
                {
                    if (_isHovering)
                    {
                        // Rotate icon slightly during hover
                        _iconRotation += 2f;
                        if (_iconRotation >= 15f) _iconRotation = 15f;
                    }
                    else
                    {
                        // Rotate back
                        _iconRotation -= 2f;
                        if (_iconRotation <= 0f)
                        {
                            _iconRotation = 0f;
                            _buttonHoverTimer.Stop();
                        }
                    }
                    btnExplainCode.Invalidate();
                };

                // Subtle pulse animation when idle
                _buttonPulseTimer = new Timer { Interval = 3000 };
                _buttonPulseTimer.Tick += (s, e) =>
                {
                    if (!_isHovering && btnExplainCode.Visible)
                    {
                        AnimatePulse();
                    }
                };
                _buttonPulseTimer.Start();

                // Enhanced hover effects
                btnExplainCode.MouseEnter += (s, e) =>
                {
                    _isHovering = true;
                    btnExplainCode.BackColor = COLOR_PRIMARY_HOVER;
                    _buttonScale = 1.05f;
                    _buttonHoverTimer.Start();
                    btnExplainCode.Invalidate();
                };

                btnExplainCode.MouseLeave += (s, e) =>
                {
                    _isHovering = false;
                    btnExplainCode.BackColor = COLOR_PRIMARY;
                    _buttonScale = 1.0f;
                    _buttonHoverTimer.Start(); // Continue timer to rotate back
                    btnExplainCode.Invalidate();
                };

                // Active state (mouse down)
                btnExplainCode.MouseDown += (s, e) =>
                {
                    btnExplainCode.BackColor = COLOR_PRIMARY_ACTIVE;
                    _buttonScale = 0.98f;
                    btnExplainCode.Invalidate();
                };

                btnExplainCode.MouseUp += (s, e) =>
                {
                    btnExplainCode.BackColor = _isHovering ? COLOR_PRIMARY_HOVER : COLOR_PRIMARY;
                    _buttonScale = _isHovering ? 1.05f : 1.0f;
                    btnExplainCode.Invalidate();
                };

                btnExplainCode.Click += (s, e) => ShowAiChatForCurrentLesson();

                // Add tooltip for better UX
                var tooltip = new ToolTip
                {
                    InitialDelay = 500,
                    ReshowDelay = 200,
                    AutoPopDelay = 5000,
                    ShowAlways = true
                };
                tooltip.SetToolTip(btnExplainCode, "Sử dụng AI để giải thích code và trả lời câu hỏi\nClick để mở chat với CodeForge AI");

                pnlRightContainer.Controls.Add(btnExplainCode);
                btnExplainCode.BringToFront();
            }
            catch (Exception ex)
            {
                // Log error but fail silently for user
                System.Diagnostics.Debug.WriteLine($"AI Integration Error: {ex.Message}");
            }
        }

        private void AnimatePulse()
        {
            var pulseTimer = new Timer { Interval = 30 };
            float originalScale = _buttonScale;
            int frames = 0;
            int maxFrames = 20;

            pulseTimer.Tick += (s, e) =>
            {
                frames++;

                // Smooth pulse using sine wave
                float progress = (float)frames / maxFrames;
                _buttonScale = originalScale + (float)(Math.Sin(progress * Math.PI) * 0.08);

                btnExplainCode?.Invalidate();

                if (frames >= maxFrames)
                {
                    _buttonScale = originalScale;
                    btnExplainCode?.Invalidate();
                    pulseTimer.Stop();
                    pulseTimer.Dispose();
                }
            };
            pulseTimer.Start();
        }

        private void BtnExplainCode_Paint(object sender, PaintEventArgs e)
        {
            var btn = sender as Button;
            if (btn == null) return;

            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
            e.Graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

            // Apply scale transform
            var state = e.Graphics.Save();
            e.Graphics.TranslateTransform(btn.Width / 2, btn.Height / 2);
            e.Graphics.ScaleTransform(_buttonScale, _buttonScale);
            e.Graphics.TranslateTransform(-btn.Width / 2, -btn.Height / 2);

            // Draw multi-layer shadow for depth
            using (var path = GetRoundedRectPath(btn.ClientRectangle, 12))
            {
                // Shadow layers
                for (int i = 0; i < 3; i++)
                {
                    int alpha = 20 - (i * 6);
                    int offset = i * 2;
                    using (var shadowBrush = new SolidBrush(Color.FromArgb(alpha, 0, 0, 0)))
                    {
                        var shadowRect = new Rectangle(
                            offset,
                            offset + 2,
                            btn.Width - offset,
                            btn.Height - offset
                        );
                        using (var shadowPath = GetRoundedRectPath(shadowRect, 12))
                        {
                            e.Graphics.FillPath(shadowBrush, shadowPath);
                        }
                    }
                }

                e.Graphics.ResetTransform();

                // Reapply scale after reset
                e.Graphics.TranslateTransform(btn.Width / 2, btn.Height / 2);
                e.Graphics.ScaleTransform(_buttonScale, _buttonScale);
                e.Graphics.TranslateTransform(-btn.Width / 2, -btn.Height / 2);

                // Enhanced gradient background
                using (var brush = new LinearGradientBrush(
                    new Point(0, 0),
                    new Point(0, btn.Height),
                    btn.BackColor,
                    Color.FromArgb(
                        Math.Max(0, btn.BackColor.R - 30),
                        Math.Max(0, btn.BackColor.G - 30),
                        Math.Max(0, btn.BackColor.B - 30))))
                {
                    // Add color blend for smoother gradient
                    ColorBlend colorBlend = new ColorBlend();
                    colorBlend.Colors = new Color[]
                    {
                        btn.BackColor,
                        Color.FromArgb(
                            Math.Max(0, btn.BackColor.R - 15),
                            Math.Max(0, btn.BackColor.G - 15),
                            Math.Max(0, btn.BackColor.B - 15)),
                        Color.FromArgb(
                            Math.Max(0, btn.BackColor.R - 30),
                            Math.Max(0, btn.BackColor.G - 30),
                            Math.Max(0, btn.BackColor.B - 30))
                    };
                    colorBlend.Positions = new float[] { 0.0f, 0.5f, 1.0f };
                    brush.InterpolationColors = colorBlend;

                    e.Graphics.FillPath(brush, path);
                }

                // Subtle inner highlight for glass effect
                using (var highlightPath = GetRoundedRectPath(
                    new Rectangle(2, 2, btn.Width - 4, btn.Height / 2), 10))
                {
                    using (var highlightBrush = new LinearGradientBrush(
                        new Point(0, 2),
                        new Point(0, btn.Height / 2),
                        Color.FromArgb(30, 255, 255, 255),
                        Color.FromArgb(0, 255, 255, 255)))
                    {
                        e.Graphics.FillPath(highlightBrush, highlightPath);
                    }
                }
            }

            // Draw sparkle icon with rotation
            var iconState = e.Graphics.Save();
            DrawEnhancedSparkleIcon(e.Graphics, 30, btn.Height / 2, 16);
            e.Graphics.Restore(iconState);

            // Draw text with better positioning
            using (var textBrush = new SolidBrush(btn.ForeColor))
            using (var format = new StringFormat
            {
                Alignment = StringAlignment.Near,
                LineAlignment = StringAlignment.Center
            })
            {
                var textRect = new RectangleF(56, 0, btn.Width - 64, btn.Height);
                e.Graphics.DrawString("Giải thích Code", btn.Font, textBrush, textRect, format);
            }

            // Draw subtle badge indicator
            DrawBadgeIndicator(e.Graphics, btn.Width - 12, 12);

            e.Graphics.Restore(state);
        }

        private void DrawEnhancedSparkleIcon(Graphics g, float centerX, float centerY, float size)
        {
            // Save state for rotation
            var state = g.Save();

            // Apply icon rotation
            g.TranslateTransform(centerX, centerY);
            g.RotateTransform(_iconRotation);
            g.TranslateTransform(-centerX, -centerY);

            using (var pen = new Pen(Color.White, 2.5f))
            {
                pen.StartCap = LineCap.Round;
                pen.EndCap = LineCap.Round;

                // Main sparkle (larger)
                g.DrawLine(pen, centerX, centerY - size / 2, centerX, centerY + size / 2);
                g.DrawLine(pen, centerX - size / 2, centerY, centerX + size / 2, centerY);

                // Diagonals
                float diag = size * 0.35f;
                g.DrawLine(pen, centerX - diag, centerY - diag, centerX + diag, centerY + diag);
                g.DrawLine(pen, centerX - diag, centerY + diag, centerX + diag, centerY - diag);
            }

            // Add smaller sparkles for more dynamic look
            using (var smallPen = new Pen(Color.FromArgb(200, 255, 255, 255), 1.8f))
            {
                smallPen.StartCap = LineCap.Round;
                smallPen.EndCap = LineCap.Round;

                float smallSize = size * 0.4f;
                float offset = size * 0.65f;

                // Top-right small sparkle
                g.DrawLine(smallPen,
                    centerX + offset, centerY - offset - smallSize / 2,
                    centerX + offset, centerY - offset + smallSize / 2);
                g.DrawLine(smallPen,
                    centerX + offset - smallSize / 2, centerY - offset,
                    centerX + offset + smallSize / 2, centerY - offset);
            }

            g.Restore(state);
        }

        private void DrawBadgeIndicator(Graphics g, float x, float y)
        {
            // Small "AI" badge
            float badgeSize = 8;
            using (var badgeBrush = new SolidBrush(Color.FromArgb(255, 193, 7)))
            {
                g.FillEllipse(badgeBrush, x - badgeSize / 2, y - badgeSize / 2, badgeSize, badgeSize);
            }

            // Badge border
            using (var badgePen = new Pen(Color.White, 1.5f))
            {
                g.DrawEllipse(badgePen, x - badgeSize / 2, y - badgeSize / 2, badgeSize, badgeSize);
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

        /// <summary>
        /// Mở modal AI Chat với thiết kế hiện đại và responsive
        /// </summary>
        private void ShowAiChatForCurrentLesson()
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new Action(ShowAiChatForCurrentLesson));
                return;
            }

            if (_currentLesson == null)
            {
                ShowModernMessageBox(
                    "Vui lòng chọn một bài học trước khi sử dụng AI",
                    "Chọn bài học",
                    MessageBoxIcon.Information);
                return;
            }

            string lessonContent;
            try
            {
                lessonContent = "";
                if (_currentLesson.TextContent != null)
                    lessonContent = _currentLesson.TextContent.Content ?? "";
                else if (_currentLesson.VideoContent != null)
                    lessonContent = $"Video: {_currentLesson.VideoContent.VideoUrl}";
                else
                    lessonContent = _currentLesson.Title ?? "(Không có nội dung)";

                var metadata = $"📚 Bài học: {_currentLesson.Title}\n" +
                              $"📂 Loại: {_currentLesson.LessonType}\n" +
                              $"⏱️ Thời gian: {DateTime.Now:HH:mm}\n\n";
                lessonContent = metadata + lessonContent;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error preparing lesson content: {ex.Message}");
                lessonContent = _currentLesson?.Title ?? "(Không có ngữ cảnh)";
            }

            // Create modern form with enhanced styling
            var form = new Form
            {
                Text = $"💬 CodeForge AI - {_currentLesson.Title}",
                Size = new Size(900, 720),
                StartPosition = FormStartPosition.CenterParent,
                MinimizeBox = true,
                MaximizeBox = true,
                BackColor = Color.FromArgb(246, 248, 250),
                Font = new Font("Segoe UI", 10f),
                FormBorderStyle = FormBorderStyle.Sizable,
                MinimumSize = new Size(700, 500)
            };

            // Add form icon if available
            try
            {
                // form.Icon = Properties.Resources.AIIcon; // Uncomment if you have an icon
            }
            catch { }

            // Create AI chat control
            var aiChat = new ucAIChat
            {
                Dock = DockStyle.Fill
            };
            aiChat.SetContext(lessonContent);

            form.Controls.Add(aiChat);

            // Handle form closing
            form.FormClosing += (s, e) =>
            {
                // Save any state if needed
            };

            var owner = this.FindForm();
            try
            {
                if (owner != null)
                {
                    form.ShowDialog(owner);
                }
                else
                {
                    form.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error showing AI chat form: {ex.Message}");
                MessageBox.Show(
                    "Không thể mở cửa sổ AI Chat. Vui lòng thử lại.",
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            finally
            {
                form.Dispose();
            }
        }

        /// <summary>
        /// Clean up resources
        /// </summary>
       
    }
}