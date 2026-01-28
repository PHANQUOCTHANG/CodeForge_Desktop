using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Threading.Tasks;
using System.Windows.Forms;
using CodeForge_Desktop.Business.Services;

namespace CodeForge_Desktop.Presentation.Forms.Student.UserControls
{
    public class ucAIChat : UserControl, IDisposable
    {
        // UI Controls
        private Panel pnlChatContainer;
        private Panel pnlMessagesWrapper;
        private FlowLayoutPanel flpMessages;
        private Panel pnlInputArea;
        private Panel pnlInputWrapper;
        private TextBox txtInput;
        private Button btnSend;
        private Button btnAttach;
        private PictureBox pbLoading;
        private Label lblPlaceholder;
        private Panel pnlTypingIndicator;
        private Label lblCharCount;
        private Panel pnlButtons; // Add this field near other UI Controls

        // Animation
        private Timer typingAnimTimer;
        private Timer loadingAnimTimer;
        private int typingDotCount = 0;
        private float loadingRotation = 0;

        // Service
        private readonly AIService _aiService;
        private string _context = string.Empty;

        // Constants for modern design
        private readonly Color COLOR_PRIMARY = Color.FromArgb(13, 110, 253);
        private readonly Color COLOR_PRIMARY_HOVER = Color.FromArgb(10, 88, 202);
        private readonly Color COLOR_SURFACE = Color.FromArgb(255, 255, 255);
        private readonly Color COLOR_BACKGROUND = Color.FromArgb(246, 248, 250);
        private readonly Color COLOR_BORDER = Color.FromArgb(218, 225, 233);
        private readonly Color COLOR_TEXT_PRIMARY = Color.FromArgb(23, 28, 35);
        private readonly Color COLOR_TEXT_SECONDARY = Color.FromArgb(87, 96, 106);
        private readonly Color COLOR_TEXT_TERTIARY = Color.FromArgb(139, 148, 158);
        private readonly Color COLOR_USER_BUBBLE = Color.FromArgb(13, 110, 253);
        private readonly Color COLOR_AI_BUBBLE = Color.FromArgb(246, 248, 250);
        private readonly Color COLOR_SUCCESS = Color.FromArgb(25, 135, 84);
        private readonly Color COLOR_WARNING = Color.FromArgb(255, 193, 7);
        private readonly Color COLOR_ERROR = Color.FromArgb(220, 53, 69);

        public ucAIChat()
        {
            _aiService = new AIService();
            InitializeUI();
            SetupAnimations();
        }

        private void InitializeUI()
        {
            // Main container with modern background
            this.BackColor = COLOR_BACKGROUND;
            this.Font = new Font("Segoe UI", 10f);
            this.Padding = new Padding(0);

            // Messages wrapper for better scroll control
            pnlMessagesWrapper = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = COLOR_SURFACE,
                Padding = new Padding(0),
                AutoScroll = true
            };

            // FlowLayoutPanel for message bubbles — top-docked, autosizing list
            flpMessages = new FlowLayoutPanel
            {
                Dock = DockStyle.Top,                 // grow vertically inside the scrollable wrapper
                FlowDirection = FlowDirection.TopDown,
                WrapContents = false,
                AutoSize = true,                      // let it grow; wrapper will provide scrolling
                AutoSizeMode = AutoSizeMode.GrowAndShrink,
                Padding = new Padding(20, 20, 20, 20),
                BackColor = COLOR_SURFACE
            };

            pnlMessagesWrapper.Controls.Add(flpMessages);

            // Typing indicator: dock it now so order/docking is reliable
            pnlTypingIndicator = new Panel
            {
                Height = 60,
                BackColor = Color.Transparent,
                Visible = false,
                Padding = new Padding(20, 12, 20, 12),
                Dock = DockStyle.Bottom
            };
            pnlTypingIndicator.Paint += PnlTypingIndicator_Paint;

            // Input area (footer)
            pnlInputArea = new Panel
            {
                Dock = DockStyle.Bottom,
                Height = 120,
                BackColor = COLOR_SURFACE,
                Padding = new Padding(20, 16, 20, 20)
            };
            pnlInputArea.Paint += PnlInputArea_Paint;

            // Input wrapper with border and space reserved for single send button
            pnlInputWrapper = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = COLOR_SURFACE,
                Padding = new Padding(16, 12, 84, 12) // reserve right padding for send button
            };
            pnlInputWrapper.Paint += PnlInputWrapper_Paint;

            // Character count label
            lblCharCount = new Label
            {
                Text = "0 / 2000",
                ForeColor = COLOR_TEXT_TERTIARY,
                BackColor = Color.Transparent,
                Font = new Font("Segoe UI", 8f),
                AutoSize = true,
                Location = new Point(16, 2)
            };

            // Placeholder
            lblPlaceholder = new Label
            {
                Text = "💭 Đặt câu hỏi hoặc yêu cầu giải thích code...",
                ForeColor = COLOR_TEXT_TERTIARY,
                BackColor = Color.Transparent,
                Font = new Font("Segoe UI", 10.5f),
                AutoSize = false,
                Size = new Size(400, 24),
                Location = new Point(18, 14),
                Cursor = Cursors.IBeam
            };

            // Multi-line input
            txtInput = new TextBox
            {
                Multiline = true,
                BorderStyle = BorderStyle.None,
                BackColor = COLOR_SURFACE,
                Font = new Font("Segoe UI", 10.5f),
                Dock = DockStyle.Fill,
                ScrollBars = ScrollBars.Vertical,
                MaxLength = 2000
            };
            txtInput.TextChanged += TxtInput_TextChanged;
            txtInput.KeyDown += TxtInput_KeyDown;
            txtInput.GotFocus += (s, e) => { pnlInputWrapper.Invalidate(); lblPlaceholder.Visible = false; };
            txtInput.LostFocus += (s, e) => { pnlInputWrapper.Invalidate(); lblPlaceholder.Visible = string.IsNullOrEmpty(txtInput.Text); };

            // Single Send button placed inside the input wrapper (no separate pnlButtons)
            btnSend = new Button
            {
                Text = "",
                Size = new Size(52, 52),
                BackColor = COLOR_PRIMARY,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 16f, FontStyle.Bold),
                Cursor = Cursors.Hand,
                Enabled = false,
                Anchor = AnchorStyles.Right | AnchorStyles.Bottom
            };
            btnSend.FlatAppearance.BorderSize = 0;
            btnSend.Paint += BtnSend_Paint;
            btnSend.Click += async (s, e) => await HandleSendClick();

            // Place controls into wrapper
            pnlInputWrapper.Controls.Add(txtInput);
            pnlInputWrapper.Controls.Add(lblCharCount);
            pnlInputWrapper.Controls.Add(lblPlaceholder);
            pnlInputWrapper.Controls.Add(btnSend);

            // Position send button after layout and on resize so it doesn't get clipped
            pnlInputWrapper.Resize += (s, e) =>
            {
                int marginRight = 12;
                int x = pnlInputWrapper.ClientSize.Width - btnSend.Width - marginRight;
                int y = Math.Max(pnlInputWrapper.Padding.Top, (pnlInputWrapper.ClientSize.Height - btnSend.Height) / 2);
                btnSend.Location = new Point(x, y);
            };

            // Add wrapper into input area
            pnlInputArea.Controls.Add(pnlInputWrapper);

            // Add main sections in the order: messages (fill), typing (bottom), input (bottom)
            this.Controls.Add(pnlMessagesWrapper);
            this.Controls.Add(pnlTypingIndicator);
            this.Controls.Add(pnlInputArea);

            // Ensure initial layout positions
            this.Load += (s, e) =>
            {
                AddWelcomeMessage();
                pnlInputWrapper.PerformLayout();
                pnlInputWrapper.Refresh();
            };
        }

        private void AddWelcomeMessage()
        {
            var welcomePanel = CreateSystemMessageBubble(
                "👋 Xin chào! Tôi là CodeForge AI\n\n" +
                "Tôi có thể giúp bạn:\n" +
                "• 📖 Giải thích code chi tiết\n" +
                "• 🐛 Tìm và sửa lỗi\n" +
                "• 💡 Gợi ý cải thiện code\n" +
                "• 🎯 Trả lời câu hỏi lập trình\n\n" +
                "Hãy đặt câu hỏi hoặc chia sẻ đoạn code bạn muốn tôi xem xét!"
            );
            flpMessages.Controls.Add(welcomePanel);
            ScrollToBottom();
        }
        private void ScrollToBottom()
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new Action(ScrollToBottom));
                return;
            }

            try
            {
                if (flpMessages.Controls.Count > 0)
                {
                    var last = flpMessages.Controls[flpMessages.Controls.Count - 1];
                    // Scroll the wrapper so the last message is visible
                    pnlMessagesWrapper.ScrollControlIntoView(last);
                }
                else
                {
                    pnlMessagesWrapper.AutoScrollPosition = new Point(0, pnlMessagesWrapper.VerticalScroll.Maximum);
                }
            }
            catch
            {
                // ignore transient layout timing issues
            }

            pnlMessagesWrapper.PerformLayout();
            flpMessages.PerformLayout();
        }
        private void SetupAnimations()
        {
            typingAnimTimer = new Timer { Interval = 500 };
            typingAnimTimer.Tick += (s, e) =>
            {
                typingDotCount = (typingDotCount + 1) % 4;
                if (pnlTypingIndicator != null) pnlTypingIndicator.Invalidate();
            };

            loadingAnimTimer = new Timer { Interval = 30 };
            loadingAnimTimer.Tick += (s, e) =>
            {
                loadingRotation += 8f;
                if (loadingRotation >= 360f) loadingRotation = 0f;
                    // pbLoading can be null if the loading indicator was removed; guard it
                    if (pbLoading != null) pbLoading.Invalidate();
            };
        }

        private void TxtInput_TextChanged(object sender, EventArgs e)
        {
            lblPlaceholder.Visible = string.IsNullOrEmpty(txtInput.Text);
            lblCharCount.Text = $"{txtInput.Text.Length} / 2000";

            // Update character count color
            if (txtInput.Text.Length > 1800)
                lblCharCount.ForeColor = COLOR_ERROR;
            else if (txtInput.Text.Length > 1500)
                lblCharCount.ForeColor = COLOR_WARNING;
            else
                lblCharCount.ForeColor = COLOR_TEXT_TERTIARY;

            // Enable/disable send button
            btnSend.Enabled = !string.IsNullOrWhiteSpace(txtInput.Text);
            btnSend.BackColor = btnSend.Enabled ? COLOR_PRIMARY : Color.FromArgb(206, 212, 218);

            AdjustInputHeight();
        }

        private void AdjustInputHeight()
        {
            using (Graphics g = txtInput.CreateGraphics())
            {
                SizeF size = g.MeasureString(txtInput.Text + "W", txtInput.Font, txtInput.Width);
                int preferredHeight = (int)Math.Ceiling(size.Height) + 24;
                int newHeight = Math.Min(Math.Max(68, preferredHeight), 120);

                if (pnlInputArea.Height != newHeight + 52)
                {
                    pnlInputArea.Height = newHeight + 52;
                }
            }
        }

        // Custom painting methods - See Part 2
        private void PnlInputArea_Paint(object sender, PaintEventArgs e)
        {
            var pnl = sender as Panel;
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            // Top border với gradient shadow
            using (var brush = new LinearGradientBrush(
                new Point(0, 0),
                new Point(0, 12),
                Color.FromArgb(25, 0, 0, 0),
                Color.Transparent))
            {
                e.Graphics.FillRectangle(brush, 0, 0, pnl.Width, 12);
            }

            // Subtle top line
            using (var pen = new Pen(COLOR_BORDER, 1))
            {
                e.Graphics.DrawLine(pen, 0, 0, pnl.Width, 0);
            }
        }

        private void PnlInputWrapper_Paint(object sender, PaintEventArgs e)
        {
            var pnl = sender as Panel;
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            // Rounded border với shadow
            using (var path = GetRoundedRectPath(new Rectangle(0, 0, pnl.Width - 1, pnl.Height - 1), 14))
            {
                // Shadow effect
                for (int i = 0; i < 3; i++)
                {
                    using (var shadowPen = new Pen(Color.FromArgb(10 - i * 3, 0, 0, 0), 1))
                    {
                        var shadowRect = new Rectangle(i, i, pnl.Width - 1 - i * 2, pnl.Height - 1 - i * 2);
                        using (var shadowPath = GetRoundedRectPath(shadowRect, 14 - i))
                        {
                            e.Graphics.DrawPath(shadowPen, shadowPath);
                        }
                    }
                }

                // Border với màu đậm hơn khi focus
                Color borderColor = txtInput.Focused ? COLOR_PRIMARY : COLOR_BORDER;
                int borderWidth = txtInput.Focused ? 2 : 1;

                using (var pen = new Pen(borderColor, borderWidth))
                {
                    e.Graphics.DrawPath(pen, path);
                }

                // Background
                using (var brush = new SolidBrush(COLOR_SURFACE))
                {
                    e.Graphics.FillPath(brush, path);
                }
            }
        }

        private void BtnSend_Paint(object sender, PaintEventArgs e)
        {
            var btn = sender as Button;
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            e.Graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

            // Circular button với gradient
            using (var path = new GraphicsPath())
            {
                path.AddEllipse(2, 2, btn.Width - 4, btn.Height - 4);

                // Shadow
                using (var shadowBrush = new PathGradientBrush(path))
                {
                    shadowBrush.CenterPoint = new PointF(btn.Width / 2, btn.Height / 2);
                    shadowBrush.CenterColor = Color.FromArgb(50, 0, 0, 0);
                    shadowBrush.SurroundColors = new[] { Color.FromArgb(0, 0, 0, 0) };
                    e.Graphics.FillPath(shadowBrush, path);
                }

                // Gradient background
                if (btn.Enabled)
                {
                    using (var brush = new LinearGradientBrush(
                        new Point(0, 0),
                        new Point(0, btn.Height),
                        btn.BackColor,
                        Color.FromArgb(
                            Math.Max(0, btn.BackColor.R - 25),
                            Math.Max(0, btn.BackColor.G - 25),
                            Math.Max(0, btn.BackColor.B - 25))))
                    {
                        e.Graphics.FillPath(brush, path);
                    }
                }
                else
                {
                    using (var brush = new SolidBrush(btn.BackColor))
                    {
                        e.Graphics.FillPath(brush, path);
                    }
                }
            }

            // Send icon (paper plane)
            DrawSendIcon(e.Graphics, btn.Width / 2, btn.Height / 2, btn.Enabled ? Color.White : COLOR_TEXT_TERTIARY);
        }

        private void DrawSendIcon(Graphics g, float centerX, float centerY, Color color)
        {
            using (var pen = new Pen(color, 2.5f))
            {
                pen.StartCap = LineCap.Round;
                pen.EndCap = LineCap.Round;
                pen.LineJoin = LineJoin.Round;

                // Paper plane shape
                PointF[] planePoints = new PointF[]
                {
                    new PointF(centerX - 8, centerY + 6),
                    new PointF(centerX + 8, centerY),
                    new PointF(centerX - 8, centerY - 6),
                    new PointF(centerX - 4, centerY),
                    new PointF(centerX - 8, centerY + 6)
                };

                g.DrawLines(pen, planePoints);

                // Trail line
                g.DrawLine(pen, centerX - 4, centerY, centerX + 8, centerY);
            }
        }

        private void BtnAttach_Paint(object sender, PaintEventArgs e)
        {
            var btn = sender as Button;
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            // Rounded background on hover
            if (btn.ClientRectangle.Contains(btn.PointToClient(Cursor.Position)))
            {
                using (var path = GetRoundedRectPath(btn.ClientRectangle, 10))
                using (var brush = new SolidBrush(COLOR_BACKGROUND))
                {
                    e.Graphics.FillPath(brush, path);
                }
            }
        }

        private void PnlTypingIndicator_Paint(object sender, PaintEventArgs e)
        {
            var pnl = sender as Panel;
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            int bubbleWidth = 100;
            int bubbleHeight = 44;
            int x = 20;
            int y = (pnl.Height - bubbleHeight) / 2;

            // AI bubble background
            using (var path = GetRoundedRectPath(new Rectangle(x, y, bubbleWidth, bubbleHeight), 18))
            {
                using (var brush = new SolidBrush(COLOR_AI_BUBBLE))
                {
                    e.Graphics.FillPath(brush, path);
                }
            }

            // Animated dots
            DrawTypingDots(e.Graphics, x + bubbleWidth / 2, y + bubbleHeight / 2);

            // "AI đang trả lời" text
            using (var font = new Font("Segoe UI", 8.5f, FontStyle.Italic))
            using (var brush = new SolidBrush(COLOR_TEXT_SECONDARY))
            {
                string text = "CodeForge AI đang trả lời";
                var textSize = e.Graphics.MeasureString(text, font);
                e.Graphics.DrawString(text, font, brush, x + bubbleWidth + 12, y + (bubbleHeight - textSize.Height) / 2);
            }
        }

        private void DrawTypingDots(Graphics g, float centerX, float centerY)
        {
            int dotSize = 8;
            int spacing = 14;
            float[] alphas = new float[] { 0.3f, 0.6f, 1.0f };

            // Rotate alpha values based on animation
            for (int i = 0; i < 3; i++)
            {
                int alphaIndex = (i + typingDotCount) % 3;
                int x = (int)(centerX - spacing + (i * spacing));
                int y = (int)centerY - dotSize / 2;

                int alpha = (int)(alphas[alphaIndex] * 255);
                using (var brush = new SolidBrush(Color.FromArgb(alpha, COLOR_TEXT_SECONDARY)))
                {
                    g.FillEllipse(brush, x - dotSize / 2, y, dotSize, dotSize);
                }
            }
        }

        private void PbLoading_Paint(object sender, PaintEventArgs e)
        {
            var pb = sender as PictureBox;
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            int centerX = pb.Width / 2;
            int centerY = pb.Height / 2;
            int radius = 16;

            // Rotating arc
            using (var pen = new Pen(COLOR_PRIMARY, 3f))
            {
                pen.StartCap = LineCap.Round;
                pen.EndCap = LineCap.Round;

                e.Graphics.TranslateTransform(centerX, centerY);
                e.Graphics.RotateTransform(loadingRotation);
                e.Graphics.TranslateTransform(-centerX, -centerY);

                e.Graphics.DrawArc(pen, centerX - radius, centerY - radius, radius * 2, radius * 2, 0, 270);
            }
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

        // Message bubble creation
        private Panel CreateMessageBubble(string sender, string content, bool isUser, DateTime timestamp)
        {
            var containerPanel = new Panel
            {
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink,
                Padding = new Padding(0, 0, 0, 16),
                BackColor = Color.Transparent,
                Dock = DockStyle.Top
            };

            // compute available width with fallback (layout may run before control measured)
            int available = flpMessages.ClientSize.Width > 0 ? flpMessages.ClientSize.Width - 40 : 600;
            int maxBubbleWidth = Math.Max(200, (int)(available * 0.75));

            var bubblePanel = new Panel
            {
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink,
                MaximumSize = new Size(maxBubbleWidth, 0),
                Padding = new Padding(18, 14, 18, 14),
                BackColor = Color.Transparent
            };

            bubblePanel.Paint += (s, e) =>
            {
                var pnl = s as Panel;
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

                using (var path = GetRoundedRectPath(pnl.ClientRectangle, 16))
                {
                    // Shadow
                    if (isUser)
                    {
                        using (var shadowBrush = new SolidBrush(Color.FromArgb(15, 0, 0, 0)))
                        {
                            var shadowRect = new Rectangle(2, 2, pnl.Width - 2, pnl.Height - 2);
                            using (var shadowPath = GetRoundedRectPath(shadowRect, 16))
                            {
                                e.Graphics.FillPath(shadowBrush, shadowPath);
                            }
                        }
                    }

                    // Bubble background
                    Color bubbleColor = isUser ? COLOR_USER_BUBBLE : COLOR_AI_BUBBLE;

                    var rect = pnl.ClientRectangle;
                    if (rect.Width <= 0 || rect.Height <= 0)
                    {
                        using (var solid = new SolidBrush(bubbleColor))
                            e.Graphics.FillPath(solid, path);
                    }
                    else
                    {
                        if (isUser)
                        {
                            using (var brush = new LinearGradientBrush(
                                rect,
                                bubbleColor,
                                Color.FromArgb(
                                    Math.Max(0, bubbleColor.R - 20),
                                    Math.Max(0, bubbleColor.G - 20),
                                    Math.Max(0, bubbleColor.B - 20)),
                                LinearGradientMode.Vertical))
                            {
                                e.Graphics.FillPath(brush, path);
                            }
                        }
                        else
                        {
                            using (var brush = new SolidBrush(bubbleColor))
                            {
                                e.Graphics.FillPath(brush, path);
                            }
                            using (var pen = new Pen(COLOR_BORDER, 1))
                            {
                                e.Graphics.DrawPath(pen, path);
                            }
                        }
                    }
                }
            };

            // Avatar icon
            var lblAvatar = new Label
            {
                Text = isUser ? "👤" : "🤖",
                Font = new Font("Segoe UI", 14f),
                AutoSize = true,
                BackColor = Color.Transparent
            };

            var lblSender = new Label
            {
                Text = sender,
                Font = new Font("Segoe UI Semibold", 9f, FontStyle.Bold),
                ForeColor = isUser ? Color.White : COLOR_PRIMARY,
                AutoSize = true,
                BackColor = Color.Transparent,
                Padding = new Padding(0, 0, 0, 4)
            };

            var lblContent = new Label
            {
                Text = content,
                Font = new Font("Segoe UI", 10.5f),
                ForeColor = isUser ? Color.White : COLOR_TEXT_PRIMARY,
                AutoSize = true,
                MaximumSize = new Size(bubblePanel.MaximumSize.Width - 36, 0),
                BackColor = Color.Transparent,
                Padding = new Padding(0, 0, 0, 8)
            };

            var lblTime = new Label
            {
                Text = timestamp.ToString("HH:mm"),
                Font = new Font("Segoe UI", 7.5f),
                ForeColor = isUser ? Color.FromArgb(220, 230, 255) : COLOR_TEXT_TERTIARY,
                AutoSize = true,
                BackColor = Color.Transparent
            };

            var contentFlow = new FlowLayoutPanel
            {
                FlowDirection = FlowDirection.TopDown,
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink,
                WrapContents = false,
                BackColor = Color.Transparent,
                Padding = new Padding(0)
            };

            var headerFlow = new FlowLayoutPanel
            {
                FlowDirection = FlowDirection.LeftToRight,
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink,
                WrapContents = false,
                BackColor = Color.Transparent,
                Padding = new Padding(0, 0, 0, 2)
            };

            headerFlow.Controls.Add(lblAvatar);
            headerFlow.Controls.Add(lblSender);

            contentFlow.Controls.Add(headerFlow);
            contentFlow.Controls.Add(lblContent);
            contentFlow.Controls.Add(lblTime);

            bubblePanel.Controls.Add(contentFlow);
            containerPanel.Controls.Add(bubblePanel);

            return containerPanel;
        }

        private Panel CreateSystemMessageBubble(string message)
        {
            var containerPanel = new Panel
            {
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink,
                Padding = new Padding(0, 8, 0, 8),
                BackColor = Color.Transparent,
                Dock = DockStyle.Top
            };

            int available = flpMessages.ClientSize.Width > 0 ? flpMessages.ClientSize.Width - 40 : 600;
            int maxWidth = Math.Max(240, available - 80);

            var bubblePanel = new Panel
            {
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink,
                Padding = new Padding(20, 16, 20, 16),
                BackColor = Color.Transparent,
                MaximumSize = new Size(maxWidth, 0)
            };

            bubblePanel.Paint += (s, e) =>
            {
                var pnl = s as Panel;
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

                using (var path = GetRoundedRectPath(pnl.ClientRectangle, 12))
                {
                    var rect = pnl.ClientRectangle;
                    if (rect.Width <= 0 || rect.Height <= 0)
                    {
                        using (var solid = new SolidBrush(Color.FromArgb(255, 249, 235)))
                        {
                            e.Graphics.FillPath(solid, path);
                        }
                    }
                    else
                    {
                        using (var brush = new LinearGradientBrush(
                            rect,
                            Color.FromArgb(255, 249, 235),
                            Color.FromArgb(255, 243, 205),
                            LinearGradientMode.Vertical))
                        {
                            e.Graphics.FillPath(brush, path);
                        }
                    }

                    using (var pen = new Pen(Color.FromArgb(100, 255, 193, 7), 1))
                    {
                        e.Graphics.DrawPath(pen, path);
                    }
                }
            };

            var lblMessage = new Label
            {
                Text = message,
                Font = new Font("Segoe UI", 10f),
                ForeColor = Color.FromArgb(102, 77, 3),
                AutoSize = true,
                MaximumSize = new Size(bubblePanel.MaximumSize.Width - 40, 0),
                BackColor = Color.Transparent
            };

            bubblePanel.Controls.Add(lblMessage);
            containerPanel.Controls.Add(bubblePanel);

            return containerPanel;
        }

        // Event handlers
        private void TxtInput_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (e.Shift) return;
                e.SuppressKeyPress = true;
                if (btnSend.Enabled)
                {
                    btnSend.PerformClick();
                }
            }
        }

        private async Task HandleSendClick()
        {
            var msg = txtInput.Text.Trim();
            if (string.IsNullOrWhiteSpace(msg)) return;

            // Add user message
            var userBubble = CreateMessageBubble("Bạn", msg, true, DateTime.Now);
            flpMessages.Controls.Add(userBubble);

            txtInput.Clear();
            lblPlaceholder.Visible = true;
            lblCharCount.Text = "0 / 2000";

            ScrollToBottom();
            ToggleLoading(true);

            try
            {
                string response = await _aiService.ChatWithAI(msg, _context);
                var aiBubble = CreateMessageBubble("CodeForge AI", response, false, DateTime.Now);
                flpMessages.Controls.Add(aiBubble);
                ScrollToBottom();
            }
            catch (Exception ex)
            {
                var errorBubble = CreateSystemMessageBubble($"⚠️ Đã xảy ra lỗi: {ex.Message}\n\nVui lòng thử lại sau.");
                flpMessages.Controls.Add(errorBubble);
            }
            finally
            {
                ToggleLoading(false);
                txtInput.Focus();
            }
        }

        
        private void ToggleLoading(bool isLoading)
        {
            // Marshal to UI thread if needed
            if (this.InvokeRequired)
            {
                this.BeginInvoke((Action)(() => ToggleLoading(isLoading)));
                return;
            }

            // Null-safe UI updates (avoid preview 'null conditional assignment' feature)
            if (btnSend != null) btnSend.Visible = !isLoading;
            if (btnAttach != null) btnAttach.Visible = !isLoading;
            if (pbLoading != null) pbLoading.Visible = isLoading;
            if (txtInput != null) txtInput.Enabled = !isLoading;
            if (pnlTypingIndicator != null) pnlTypingIndicator.Visible = isLoading;

            // Null-safe timer control
            if (isLoading)
            {
                if (typingAnimTimer != null) typingAnimTimer.Start();
                if (loadingAnimTimer != null) loadingAnimTimer.Start();
            }
            else
            {
                if (typingAnimTimer != null) typingAnimTimer.Stop();
                if (loadingAnimTimer != null) loadingAnimTimer.Stop();
            }
        }

        // Public API
        public void SetContext(string context)
        {
            _context = context ?? string.Empty;
            if (!string.IsNullOrEmpty(_context))
            {
                var contextBubble = CreateSystemMessageBubble("📚 Đã tải ngữ cảnh bài học. Sẵn sàng trả lời câu hỏi!");
                flpMessages.Controls.Add(contextBubble);
                ScrollToBottom();
            }
        }

        public void ClearHistory()
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new Action(ClearHistory));
                return;
            }

            flpMessages.Controls.Clear();
            AddWelcomeMessage();

            var clearBubble = CreateSystemMessageBubble("🗑️ Đã xóa lịch sử trò chuyện");
            flpMessages.Controls.Add(clearBubble);
        }

        public void RefreshConversation()
        {
            var refreshBubble = CreateSystemMessageBubble("🔄 Đã làm mới cuộc hội thoại");
            flpMessages.Controls.Add(refreshBubble);
            ScrollToBottom();
        }

        public async Task SendMessageAsync(string message)
        {
            if (string.IsNullOrWhiteSpace(message)) return;

            if (this.InvokeRequired)
            {
                var tcs = new TaskCompletionSource<bool>();
                this.BeginInvoke(new Action(async () =>
                {
                    try
                    {
                        await SendMessageAsync(message);
                        tcs.SetResult(true);
                    }
                    catch (Exception ex)
                    {
                        tcs.SetException(ex);
                    }
                }));
                await tcs.Task;
                return;
            }

            var userBubble = CreateMessageBubble("Bạn", message, true, DateTime.Now);
            flpMessages.Controls.Add(userBubble);
            ScrollToBottom();

            ToggleLoading(true);

            try
            {
                string response = await _aiService.ChatWithAI(message, _context);
                var aiBubble = CreateMessageBubble("CodeForge AI", response, false, DateTime.Now);
                flpMessages.Controls.Add(aiBubble);
                ScrollToBottom();
            }
            catch (Exception ex)
            {
                var errorBubble = CreateSystemMessageBubble($"⚠️ Đã xảy ra lỗi: {ex.Message}");
                flpMessages.Controls.Add(errorBubble);
            }
            finally
            {
                ToggleLoading(false);
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _aiService?.Dispose();
                typingAnimTimer?.Dispose();
                loadingAnimTimer?.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}