using System;
using System.Drawing;
using System.Windows.Forms;

namespace CodeForge_Desktop.Presentation.Forms.Admin
{
    public partial class ucSystemLogs : UserControl
    {
        public ucSystemLogs()
        {
            InitializeComponent();
            SetupDataGridView();
            LoadMockData();

            // Gắn sự kiện vẽ ô để tạo Badges cho cột Level
            dgvLogs.CellPainting += DgvLogs_CellPainting;

            // Cập nhật thời gian last updated
            lblFooterStatus.Text = $"Hiển thị 10 logs | Last updated: {DateTime.Now:yyyy-MM-dd HH:mm:ss}";
        }

        private void SetupDataGridView()
        {
            // Cấu hình hiển thị Grid
            dgvLogs.DefaultCellStyle.SelectionBackColor = Color.FromArgb(240, 245, 255);
            dgvLogs.DefaultCellStyle.SelectionForeColor = Color.Black;
            dgvLogs.DefaultCellStyle.Font = new Font("Segoe UI", 9F);

            dgvLogs.Columns["colLevel"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
        }

        private void LoadMockData()
        {
            // Dữ liệu giả giống hình ảnh
            dgvLogs.Rows.Add("2025-11-16 14:30:25", "INFO", "Auth", "student_01", "User student_01 logged in successfully");
            dgvLogs.Rows.Add("2025-11-16 14:28:15", "INFO", "Submission", "student_02", "New submission received for Assignment #3");
            dgvLogs.Rows.Add("2025-11-16 14:25:10", "WARNING", "System", "system", "High memory usage detected (85%)");
            dgvLogs.Rows.Add("2025-11-16 14:20:05", "ERROR", "Database", "system", "Connection timeout to backup database");
            dgvLogs.Rows.Add("2025-11-16 14:15:00", "INFO", "Admin", "admin_01", "New assignment created: Bài 9 - Đồ thị");
            dgvLogs.Rows.Add("2025-11-16 14:10:30", "INFO", "Auth", "admin_01", "User admin_01 logged in successfully");
            dgvLogs.Rows.Add("2025-11-16 14:05:20", "WARNING", "Security", "unknown", "Multiple failed login attempts detected");
            dgvLogs.Rows.Add("2025-11-16 14:00:00", "INFO", "System", "system", "Automated backup completed successfully");
            dgvLogs.Rows.Add("2025-11-16 13:55:45", "ERROR", "Submission", "student_03", "Code execution timeout for submission #1234");
            dgvLogs.Rows.Add("2025-11-16 13:50:30", "INFO", "User", "student_04", "User profile updated");

            dgvLogs.ClearSelection();
        }

        private void DgvLogs_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex < 0) return;

            // 1. Vẽ Badge cho cột Level (Index = 1)
            if (e.ColumnIndex == dgvLogs.Columns["colLevel"].Index && e.Value != null)
            {
                e.Paint(e.CellBounds, DataGridViewPaintParts.All & ~DataGridViewPaintParts.ContentForeground);

                string level = e.Value.ToString();
                Color backColor = Color.Gray;
                Color foreColor = Color.White; // Hoặc màu đậm hơn nếu nền nhạt

                // Chọn màu dựa trên Level
                if (level == "INFO")
                {
                    backColor = Color.FromArgb(225, 245, 254); // Xanh nhạt
                    foreColor = Color.FromArgb(3, 169, 244);   // Xanh đậm
                }
                else if (level == "WARNING")
                {
                    backColor = Color.FromArgb(255, 248, 225); // Vàng nhạt
                    foreColor = Color.FromArgb(255, 160, 0);   // Cam đậm
                }
                else if (level == "ERROR")
                {
                    backColor = Color.FromArgb(255, 235, 238); // Đỏ nhạt
                    foreColor = Color.FromArgb(233, 30, 99);   // Đỏ hồng đậm
                }

                // Tính toán hình chữ nhật cho Badge (nhỏ hơn ô một chút)
                var badgeRect = new Rectangle(e.CellBounds.X + 10, e.CellBounds.Y + 5, e.CellBounds.Width - 20, e.CellBounds.Height - 10);

                // Vẽ nền Badge
                using (Brush brush = new SolidBrush(backColor))
                {
                    e.Graphics.FillRectangle(brush, badgeRect);
                }

                // Vẽ Text căn giữa Badge
                TextRenderer.DrawText(e.Graphics, level, new Font("Segoe UI", 8, FontStyle.Bold), badgeRect, foreColor, TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);

                e.Handled = true;
            }

            // 2. Tô màu xanh cho cột Source (Index = 2) - Giống đường link
            if (e.ColumnIndex == dgvLogs.Columns["colSource"].Index && e.Value != null)
            {
                e.Paint(e.CellBounds, DataGridViewPaintParts.All & ~DataGridViewPaintParts.ContentForeground);
                string source = e.Value.ToString();

                // Màu xanh dương giống link
                Color linkColor = Color.FromArgb(0, 120, 215);

                TextRenderer.DrawText(e.Graphics, source, e.CellStyle.Font, e.CellBounds, linkColor, TextFormatFlags.VerticalCenter | TextFormatFlags.Left);
                e.Handled = true;
            }
        }
    }
}