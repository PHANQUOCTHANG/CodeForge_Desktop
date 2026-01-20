using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using CodeForge_Desktop.Business.Services;

namespace CodeForge_Desktop.Presentation.Forms.Student.UserControls
{
    public class ucAIChat : UserControl
    {
        // UI Controls
        private RichTextBox rtbHistory;
        private TextBox txtInput;
        private Button btnSend;
        private PictureBox pbLoading;
        private Panel pnlInputArea;

        // Service
        private readonly AIService _aiService;
        private string _context = string.Empty;

        public ucAIChat()
        {
            _aiService = new AIService();
            InitializeUI();
        }

        private void InitializeUI()
        {
            // 1. Cấu hình Form chính
            this.BackColor = Color.White;
            this.Font = new Font("Segoe UI", 10f);

            // 2. Khu vực nhập liệu (Dock Bottom trước để nó chiếm chỗ bên dưới)
            pnlInputArea = new Panel
            {
                Dock = DockStyle.Bottom,
                Height = 120, // Tăng chiều cao lên chút nữa cho thoải mái
                BackColor = Color.FromArgb(245, 247, 250),
                Padding = new Padding(10)
            };
            // Vẽ đường kẻ
            pnlInputArea.Paint += (s, e) => {
                e.Graphics.DrawLine(new Pen(Color.LightGray), 0, 0, pnlInputArea.Width, 0);
            };

            // Nút Gửi
            btnSend = new Button
            {
                Text = "Gửi ➤",
                Size = new Size(80, 40),
                BackColor = Color.FromArgb(13, 110, 253),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 10f, FontStyle.Bold),
                Cursor = Cursors.Hand,
                Anchor = AnchorStyles.Right | AnchorStyles.Bottom
            };
            btnSend.FlatAppearance.BorderSize = 0;
            // Bo tròn nút (dùng Region đơn giản)
            using (var path = new System.Drawing.Drawing2D.GraphicsPath())
            {
                path.AddArc(0, 0, 10, 10, 180, 90);
                path.AddArc(btnSend.Width - 10, 0, 10, 10, 270, 90);
                path.AddArc(btnSend.Width - 10, btnSend.Height - 10, 10, 10, 0, 90);
                path.AddArc(0, btnSend.Height - 10, 10, 10, 90, 90);
                path.CloseFigure();
                btnSend.Region = new Region(path);
            }
            btnSend.Click += async (s, e) => await HandleSendClick();

            // Nút Loading
            pbLoading = new PictureBox
            {
                Size = new Size(40, 40),
                SizeMode = PictureBoxSizeMode.Zoom,
                Visible = false,
                Anchor = AnchorStyles.Right | AnchorStyles.Bottom,
            };
            // Gán ảnh loading (nếu có resource thì dùng, không thì vẽ tạm vòng tròn)
            try { pbLoading.Image = SystemIcons.Shield.ToBitmap(); } catch { }

            // Ô nhập liệu (Multiline)
            txtInput = new TextBox
            {
                Multiline = true,
                ScrollBars = ScrollBars.Vertical,
                BorderStyle = BorderStyle.FixedSingle,
                Font = new Font("Segoe UI", 11f),
                Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top | AnchorStyles.Bottom,
                Location = new Point(15, 15),
                Size = new Size(pnlInputArea.Width - 115, 90) // Chừa chỗ cho nút
            };
            txtInput.KeyDown += TxtInput_KeyDown;

            // Layout Controls vào Panel Input
            pnlInputArea.Controls.Add(txtInput);
            pnlInputArea.Controls.Add(btnSend);
            pnlInputArea.Controls.Add(pbLoading);

            // Set vị trí nút Gửi
            btnSend.Location = new Point(pnlInputArea.Width - 95, pnlInputArea.Height - 55);
            pbLoading.Location = btnSend.Location;

            pnlInputArea.Resize += (s, e) => {
                txtInput.Width = pnlInputArea.Width - 115;
                btnSend.Location = new Point(pnlInputArea.Width - 95, pnlInputArea.Height - 55);
                pbLoading.Location = btnSend.Location;
            };

            // 3. Khu vực lịch sử chat (Dock Fill để chiếm phần còn lại)
            rtbHistory = new RichTextBox
            {
                Dock = DockStyle.Fill,
                BackColor = Color.White,
                BorderStyle = BorderStyle.None,
                ReadOnly = true,
                // --- QUAN TRỌNG: FIX LỖI SCROLL ---
                ScrollBars = RichTextBoxScrollBars.ForcedVertical, // Ép hiện thanh cuộn dọc
                HideSelection = false, // Giữ vùng chọn để auto-scroll hoạt động đúng
                Font = new Font("Segoe UI", 11f),
                Padding = new Padding(20, 20, 20, 20)
            };

            this.Controls.Add(rtbHistory); // Add RTB trước (hoặc sau tùy Dock order, ở đây RTB Fill nên add sau Input Dock Bottom)
            this.Controls.Add(pnlInputArea);

            // Đảo thứ tự Z-Index để đảm bảo layout đúng (Input nằm dưới đáy, RTB ở trên)
            pnlInputArea.BringToFront();
        }

        public void SetContext(string context)
        {
            _context = context ?? string.Empty;
            if (!string.IsNullOrEmpty(_context))
            {
                AppendSystemMessage("ℹ️ Đã nạp ngữ cảnh bài học hiện tại.");
            }
        }

        private void TxtInput_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (e.Shift) return; // Shift+Enter: xuống dòng
                e.SuppressKeyPress = true; // Chặn Enter thường
                btnSend.PerformClick();
            }
        }

        private async Task HandleSendClick()
        {
            var msg = txtInput.Text.Trim();
            if (string.IsNullOrWhiteSpace(msg)) return;

            // 1. Hiển thị User Message
            AppendMessage("Bạn", msg, Color.FromArgb(0, 0, 0), true);

            txtInput.Clear();
            ToggleLoading(true);

            try
            {
                // 2. Gọi AI
                string response = await _aiService.ChatWithAI(msg, _context);

                // 3. Hiển thị AI Message
                AppendMessage("CodeForge AI", response, Color.FromArgb(30, 30, 30), false);
            }
            catch (Exception ex)
            {
                AppendSystemMessage($"❌ Lỗi: {ex.Message}");
            }
            finally
            {
                ToggleLoading(false);
                txtInput.Focus();
            }
        }

        private void ToggleLoading(bool isLoading)
        {
            btnSend.Visible = !isLoading;
            pbLoading.Visible = isLoading;
            txtInput.Enabled = !isLoading; // Khóa input để tránh spam
        }

        // --- HÀM RENDER TIN NHẮN (ĐÃ TỐI ƯU SCROLL) ---
        private void AppendMessage(string sender, string content, Color textColor, bool isUser)
        {
            if (rtbHistory.InvokeRequired)
            {
                rtbHistory.BeginInvoke(new Action(() => AppendMessage(sender, content, textColor, isUser)));
                return;
            }

            // Lưu vị trí hiện tại
            rtbHistory.SelectionStart = rtbHistory.TextLength;
            rtbHistory.SelectionLength = 0;

            // 1. Vẽ Header (Tên người gửi)
            rtbHistory.SelectionFont = new Font("Segoe UI", 10f, FontStyle.Bold);
            rtbHistory.SelectionColor = isUser ? Color.FromArgb(13, 110, 253) : Color.FromArgb(40, 167, 69);
            rtbHistory.AppendText((isUser ? "👤 " : "🤖 ") + sender + "\n");

            // 2. Vẽ Nội dung
            rtbHistory.SelectionStart = rtbHistory.TextLength;
            rtbHistory.SelectionLength = 0;

            // Dùng font Consolas cho AI nếu nội dung chứa ký tự code (dấu `)
            bool isCode = !isUser && content.Contains("`");
            rtbHistory.SelectionFont = new Font(isCode ? "Consolas" : "Segoe UI", 11f, FontStyle.Regular);

            rtbHistory.SelectionColor = textColor;
            rtbHistory.SelectionIndent = 15; // Thụt đầu dòng
            rtbHistory.SelectionRightIndent = 15;
            rtbHistory.AppendText(content + "\n");

            // 3. Vẽ đường kẻ mờ phân cách
            rtbHistory.SelectionStart = rtbHistory.TextLength;
            rtbHistory.SelectionLength = 0;
            rtbHistory.SelectionFont = new Font("Arial", 6f);
            rtbHistory.SelectionColor = Color.LightGray;
            rtbHistory.SelectionIndent = 0; // Reset thụt lề
            rtbHistory.SelectionRightIndent = 0;
            rtbHistory.AppendText("__________________________________________________\n\n");

            // 4. AUTO SCROLL XUỐNG DƯỚI CÙNG
            rtbHistory.SelectionStart = rtbHistory.TextLength;
            rtbHistory.ScrollToCaret();
        }

        private void AppendSystemMessage(string msg)
        {
            if (rtbHistory.InvokeRequired)
            {
                rtbHistory.BeginInvoke(new Action(() => AppendSystemMessage(msg)));
                return;
            }

            rtbHistory.SelectionStart = rtbHistory.TextLength;
            rtbHistory.SelectionColor = Color.Gray;
            rtbHistory.SelectionFont = new Font("Segoe UI", 9f, FontStyle.Italic);
            rtbHistory.SelectionAlignment = HorizontalAlignment.Center;
            rtbHistory.AppendText(msg + "\n\n");
            rtbHistory.SelectionAlignment = HorizontalAlignment.Left;
            rtbHistory.ScrollToCaret();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing) _aiService?.Dispose();
            base.Dispose(disposing);
        }
    }
}