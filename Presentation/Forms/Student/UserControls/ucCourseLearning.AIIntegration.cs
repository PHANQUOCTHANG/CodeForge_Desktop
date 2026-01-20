using System;
using System.Drawing;
using System.Windows.Forms;

namespace CodeForge_Desktop.Presentation.Forms.Student.UserControls
{
    // Partial class bổ sung tích hợp AI chat vào ucCourseLearning mà không sửa trực tiếp file gốc.
    public partial class ucCourseLearning
    {
        private Button btnExplainCode;
        private bool _aiIntegrationInitialized = false;

        // Gọi từ OnLoad trong partial để khởi tạo một lần
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
        /// Khởi tạo UI tích hợp AI: thêm nút "Giải thích Code" lên pnlRightContainer.
        /// Nút này mở một Form modal chứa ucAIChat và truyền context bài học hiện tại.
        /// </summary>
        private void InitializeAIIntegration()
        {
            try
            {
                if (this.pnlRightContainer == null) return;

                // Nếu đã tồn tại control cùng tính năng, tránh thêm lần nữa
                foreach (Control c in pnlRightContainer.Controls)
                {
                    if (c is Button b && b.Name == "btnExplainCode")
                    {
                        btnExplainCode = b;
                        return;
                    }
                }

                btnExplainCode = new Button
                {
                    Name = "btnExplainCode",
                    Text = "Giải thích Code",
                    Height = 36,
                    Width = 140,
                    BackColor = Color.FromArgb(40, 167, 69),
                    ForeColor = Color.White,
                    FlatStyle = FlatStyle.Flat,
                    Font = new Font("Segoe UI", 9f, FontStyle.Bold),
                    Cursor = Cursors.Hand,
                    Anchor = AnchorStyles.Top | AnchorStyles.Right
                };
                btnExplainCode.FlatAppearance.BorderSize = 0;

                btnExplainCode.Location = new Point(
                    Math.Max(8, pnlRightContainer.ClientSize.Width - btnExplainCode.Width - 12),
                    6);

                pnlRightContainer.Resize += (s, e) =>
                {
                    btnExplainCode.Location = new Point(
                        Math.Max(8, pnlRightContainer.ClientSize.Width - btnExplainCode.Width - 12),
                        6);
                };

                btnExplainCode.Click += (s, e) => ShowAiChatForCurrentLesson();

                pnlRightContainer.Controls.Add(btnExplainCode);
                btnExplainCode.BringToFront();
            }
            catch
            {
                // Nếu thêm thất bại không làm crash UI
            }
        }

        /// <summary>
        /// Mở modal chứa ucAIChat và truyền ngữ cảnh của bài học hiện tại.
        /// Phương thức an toàn với thread (invoke nếu cần).
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
                MessageBox.Show("Vui lòng chọn một bài học trước khi yêu cầu giải thích.", "Thông tin", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // Chuẩn bị context ngắn gọn cho AI: meta + nội dung chính (ưu tiên TextContent)
            string lessonContent;
            try
            {
                lessonContent = "";
                if (_currentLesson.TextContent != null)
                    lessonContent = _currentLesson.TextContent.Content ?? "";
                else if (_currentLesson.VideoContent != null)
                    lessonContent = $"Video URL: {_currentLesson.VideoContent.VideoUrl}";
                else
                    lessonContent = _currentLesson.Title ?? "(Không có nội dung chi tiết)";

                var metadata = $"Title: {_currentLesson.Title}\nType: {_currentLesson.LessonType}\n\n";
                lessonContent = metadata + lessonContent;
            }
            catch
            {
                lessonContent = _currentLesson?.Title ?? "(Không có ngữ cảnh)";
            }

            var form = new Form
            {
                Text = $"Giải thích Code - { _currentLesson.Title }",
                Size = new Size(800, 620),
                StartPosition = FormStartPosition.CenterParent,
                MinimizeBox = false,
                MaximizeBox = false
            };

            var aiChat = new ucAIChat
            {
                Dock = DockStyle.Fill
            };
            aiChat.SetContext(lessonContent);

            form.Controls.Add(aiChat);

            var owner = this.FindForm();
            try
            {
                if (owner != null)
                    form.ShowDialog(owner);
                else
                    form.ShowDialog();
            }
            finally
            {
                form.Dispose();
            }
        }
    }
}