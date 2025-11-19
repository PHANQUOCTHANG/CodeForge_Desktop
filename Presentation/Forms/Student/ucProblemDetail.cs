using System;
using System.Drawing;
using System.Windows.Forms;

namespace CodeForge_Desktop.Presentation.Forms.Student
{
    public partial class ucProblemDetail : UserControl
    {
        // Sự kiện để Form cha lắng nghe
        public event EventHandler BackButtonClicked;

        public ucProblemDetail()
        {
            InitializeComponent();

            // Gắn sự kiện click cho nút Quay lại
            btnBack.Click += (s, e) => BackButtonClicked?.Invoke(this, EventArgs.Empty);

            // Giả lập load dữ liệu
            LoadMockData();
        }

        public void SetProblemTitle(string title)
        {
            lblProblemTitle.Text = title;
        }

        private void LoadMockData()
        {
            // 1. Thêm tiêu đề "Mô tả bài tập"
            AddSectionTitle("Mô tả bài tập");

            // 2. Độ khó
            Label lblDiff = new Label();
            lblDiff.Text = "Độ khó: Dễ";
            lblDiff.ForeColor = Color.Orange;
            lblDiff.Font = new Font("Segoe UI", 9, FontStyle.Bold);
            lblDiff.AutoSize = true;
            lblDiff.Margin = new Padding(0, 0, 0, 10);
            flowDescription.Controls.Add(lblDiff);

            // 3. Nội dung đề bài
            AddLabel("Đề bài", true);
            AddLabel("Viết một hàm để tìm phần tử lớn nhất trong một mảng các số nguyên.", false);

            // 4. Input Box
            AddLabel("Input", true);
            AddCodeBox("arr = [3, 7, 2, 9, 1, 5]");

            // 5. Output Box
            AddLabel("Output", true);
            AddCodeBox("9");

            // 6. Constraints
            AddLabel("Constraints", true);
            AddLabel("• 1 ≤ arr.length ≤ 1000\n• -10^6 ≤ arr[i] ≤ 10^6", false);

            // 7. Test Cases
            AddLabel("Test Cases", true);
            AddCodeBox("Test 1:\nInput: [1, 2, 3]\nOutput: 3");
            AddCodeBox("Test 2:\nInput: [-5, -2, -10]\nOutput: -2");
        }

        // Hàm hỗ trợ thêm Label tiêu đề (đậm) hoặc nội dung thường
        private void AddLabel(string text, bool isBold)
        {
            Label lbl = new Label();
            lbl.Text = text;
            lbl.AutoSize = true;
            lbl.MaximumSize = new Size(flowDescription.Width - 25, 0); // Tự xuống dòng
            lbl.Font = new Font("Segoe UI", 9, isBold ? FontStyle.Bold : FontStyle.Regular);
            lbl.Margin = new Padding(0, 0, 0, isBold ? 5 : 15); // Cách dưới
            flowDescription.Controls.Add(lbl);
        }

        // Hàm hỗ trợ thêm Section Title (Có nền xám nhạt giống hình)
        private void AddSectionTitle(string text)
        {
            Label lbl = new Label();
            lbl.Text = text;
            lbl.AutoSize = false;
            lbl.Size = new Size(flowDescription.Width - 10, 30);
            lbl.TextAlign = ContentAlignment.MiddleLeft;
            lbl.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            lbl.BackColor = Color.WhiteSmoke;
            lbl.Margin = new Padding(0, 0, 0, 10);
            flowDescription.Controls.Add(lbl);
        }

        // Hàm hỗ trợ thêm hộp code màu xám
        private void AddCodeBox(string text)
        {
            TextBox txt = new TextBox();
            txt.Multiline = true;
            txt.ReadOnly = true;
            txt.Text = text;
            txt.BackColor = Color.FromArgb(245, 245, 245); // Xám nhạt
            txt.BorderStyle = BorderStyle.FixedSingle;
            txt.Font = new Font("Consolas", 9);
            txt.Width = flowDescription.Width - 10; // Trừ padding

            // Tính chiều cao tự động dựa trên số dòng
            int lines = text.Split('\n').Length;
            txt.Height = (lines * 15) + 20;

            txt.Margin = new Padding(0, 0, 0, 15);
            flowDescription.Controls.Add(txt);
        }
    }
}