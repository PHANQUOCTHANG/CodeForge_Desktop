using System;
using System.Drawing;
using System.Windows.Forms;

namespace CodeForge_Desktop.Presentation.Forms.Admin
{
    public partial class ucUserManagement : UserControl
    {
        // Định nghĩa khoảng cách và kích thước cho các nút trong ô Action
        private const int ButtonWidth = 30;
        private const int ButtonHeight = 30;
        private const int ButtonSpacing = 10;
        private const int ButtonMarginX = 5;

        public ucUserManagement()
        {
            InitializeComponent();
            SetupDataGridView();
            LoadMockData();

            // Gắn sự kiện Vẽ giao diện nút và Tô màu text
            dgvUsers.CellPainting += DgvUsers_CellPainting;

            // Gắn sự kiện Click nút
            dgvUsers.CellMouseClick += DgvUsers_CellMouseClick;

            // Sự kiện Toolbar
            btnAdd.Click += (s, e) => MessageBox.Show("Chức năng Thêm User đang phát triển!", "Thông báo");
            btnEdit.Click += (s, e) => MessageBox.Show("Vui lòng chọn user cần sửa!", "Thông báo");
            btnDelete.Click += (s, e) => MessageBox.Show("Vui lòng chọn user cần xóa!", "Thông báo");

            SetupSearchBox();
        }

        private void SetupSearchBox()
        {
            txtSearch.GotFocus += (s, e) => { if (txtSearch.Text == "Tìm kiếm user...") { txtSearch.Text = ""; txtSearch.ForeColor = Color.Black; } };
            txtSearch.LostFocus += (s, e) => { if (string.IsNullOrWhiteSpace(txtSearch.Text)) { txtSearch.Text = "Tìm kiếm user..."; txtSearch.ForeColor = Color.Gray; } };
        }

        private void SetupDataGridView()
        {
            dgvUsers.DefaultCellStyle.SelectionBackColor = Color.FromArgb(240, 245, 255);
            dgvUsers.DefaultCellStyle.SelectionForeColor = Color.Black;
            dgvUsers.DefaultCellStyle.Font = new Font("Segoe UI", 9.5F);

            dgvUsers.Columns["colCreated"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgvUsers.Columns["colStatus"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            dgvUsers.Columns["colActions"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            dgvUsers.Columns["colActions"].Width = 100;
        }

        private void LoadMockData()
        {
            // Dữ liệu giả giống hình ảnh
            dgvUsers.Rows.Add(false, "1", "student_01", "student01@example.com", "Student", "Active", "2025-01-15");
            dgvUsers.Rows.Add(false, "2", "student_02", "student02@example.com", "Student", "Active", "2025-01-16");
            dgvUsers.Rows.Add(false, "3", "student_03", "student03@example.com", "Student", "Inactive", "2025-01-20");
            dgvUsers.Rows.Add(false, "4", "admin_01", "admin01@example.com", "Admin", "Active", "2025-01-10");
            dgvUsers.Rows.Add(false, "5", "student_04", "student04@example.com", "Student", "Active", "2025-02-01");
            dgvUsers.Rows.Add(false, "6", "student_05", "student05@example.com", "Student", "Active", "2025-02-05");
            dgvUsers.Rows.Add(false, "7", "admin_02", "admin02@example.com", "Admin", "Active", "2025-01-12");

            dgvUsers.ClearSelection();
        }

        private void DgvUsers_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex < 0) return;

            // 1. Tô màu cột "Role"
            if (e.ColumnIndex == dgvUsers.Columns["colRole"].Index && e.Value != null)
            {
                string role = e.Value.ToString();
                Color color = Color.Black;

                if (role == "Admin") color = Color.FromArgb(220, 53, 69); // Đỏ
                else if (role == "Student") color = Color.FromArgb(13, 110, 253); // Xanh dương

                e.Paint(e.CellBounds, DataGridViewPaintParts.All & ~DataGridViewPaintParts.ContentForeground);
                TextRenderer.DrawText(e.Graphics, role, e.CellStyle.Font, e.CellBounds, color, TextFormatFlags.VerticalCenter | TextFormatFlags.Left);
                e.Handled = true;
            }

            // 2. Tô màu cột "Status"
            if (e.ColumnIndex == dgvUsers.Columns["colStatus"].Index && e.Value != null)
            {
                string status = e.Value.ToString();
                Color color = Color.Black;

                if (status == "Active") color = Color.FromArgb(40, 167, 69); // Xanh lá
                else if (status == "Inactive") color = Color.Gray;

                e.Paint(e.CellBounds, DataGridViewPaintParts.All & ~DataGridViewPaintParts.ContentForeground);
                TextRenderer.DrawText(e.Graphics, status, e.CellStyle.Font, e.CellBounds, color, TextFormatFlags.VerticalCenter | TextFormatFlags.Left);
                e.Handled = true;
            }

            // 3. Vẽ nút Actions (Edit + Delete)
            if (e.ColumnIndex == dgvUsers.Columns["colActions"].Index)
            {
                e.Paint(e.CellBounds, DataGridViewPaintParts.All & ~DataGridViewPaintParts.ContentForeground);

                int centerY = e.CellBounds.Y + (e.CellBounds.Height - ButtonHeight) / 2;
                var rectEdit = new Rectangle(e.CellBounds.X + ButtonMarginX, centerY, ButtonWidth, ButtonHeight);
                var rectDel = new Rectangle(rectEdit.Right + ButtonSpacing, centerY, ButtonWidth, ButtonHeight);

                // Vẽ nút Edit (Viền xanh)
                using (Pen p = new Pen(Color.FromArgb(13, 110, 253))) e.Graphics.DrawRectangle(p, rectEdit);
                e.Graphics.FillRectangle(Brushes.White, rectEdit.X + 1, rectEdit.Y + 1, rectEdit.Width - 2, rectEdit.Height - 2);
                TextRenderer.DrawText(e.Graphics, "📝", new Font("Segoe UI Emoji", 12), rectEdit, Color.FromArgb(13, 110, 253), TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);

                // Vẽ nút Delete (Viền đỏ)
                using (Pen p = new Pen(Color.FromArgb(220, 53, 69))) e.Graphics.DrawRectangle(p, rectDel);
                e.Graphics.FillRectangle(Brushes.White, rectDel.X + 1, rectDel.Y + 1, rectDel.Width - 2, rectDel.Height - 2);
                TextRenderer.DrawText(e.Graphics, "🗑", new Font("Segoe UI Emoji", 12), rectDel, Color.FromArgb(220, 53, 69), TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);

                e.Handled = true;
            }
        }

        private void DgvUsers_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && e.RowIndex >= 0 && e.ColumnIndex == dgvUsers.Columns["colActions"].Index)
            {
                int rowHeight = dgvUsers.Rows[e.RowIndex].Height;
                int centerY = (rowHeight - ButtonHeight) / 2;

                var rectEdit = new Rectangle(ButtonMarginX, centerY, ButtonWidth, ButtonHeight);
                var rectDel = new Rectangle(rectEdit.Right + ButtonSpacing, centerY, ButtonWidth, ButtonHeight);

                if (rectEdit.Contains(e.Location))
                {
                    string username = dgvUsers.Rows[e.RowIndex].Cells["colUsername"].Value.ToString();
                    MessageBox.Show($"Sửa thông tin user: {username}", "Edit Action");
                }
                else if (rectDel.Contains(e.Location))
                {
                    string username = dgvUsers.Rows[e.RowIndex].Cells["colUsername"].Value.ToString();
                    if (MessageBox.Show($"Bạn có chắc chắn muốn xóa user: {username}?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                    {
                        dgvUsers.Rows.RemoveAt(e.RowIndex);
                    }
                }
            }
        }
    }
}