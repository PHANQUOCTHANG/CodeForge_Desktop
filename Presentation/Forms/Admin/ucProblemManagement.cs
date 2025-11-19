using System;
using System.Drawing;
using System.Windows.Forms;

namespace CodeForge_Desktop.Presentation.Forms.Admin
{
    public partial class ucProblemManagement : UserControl
    {
        // Định nghĩa kích thước nút để dùng chung cho Vẽ và Click
        private const int ButtonWidth = 30;
        private const int ButtonHeight = 30;
        private const int ButtonSpacing = 10;
        private const int ButtonMarginX = 5; // Khoảng cách từ mép trái ô

        public ucProblemManagement()
        {
            InitializeComponent();
            SetupDataGridView();
            LoadMockData();

            // 1. Gắn sự kiện VẼ giao diện nút
            dgvAssignments.CellPainting += DgvAssignments_CellPainting;

            // 2. Gắn sự kiện CLICK (Dùng CellMouseClick thay vì CellContentClick để chính xác hơn)
            dgvAssignments.CellMouseClick += DgvAssignments_CellMouseClick;

            // Sự kiện Toolbar
            btnAdd.Click += (s, e) => MessageBox.Show("Chức năng Thêm đang phát triển!", "Thêm mới");
            btnEdit.Click += (s, e) => MessageBox.Show("Vui lòng chọn bài tập cần sửa trong danh sách!", "Sửa");

            SetupSearchBox();
        }

        private void SetupSearchBox()
        {
            txtSearch.GotFocus += (s, e) => { if (txtSearch.Text == "Tìm kiếm assignment...") { txtSearch.Text = ""; txtSearch.ForeColor = Color.Black; } };
            txtSearch.LostFocus += (s, e) => { if (string.IsNullOrWhiteSpace(txtSearch.Text)) { txtSearch.Text = "Tìm kiếm assignment..."; txtSearch.ForeColor = Color.Gray; } };
        }

        private void SetupDataGridView()
        {
            dgvAssignments.DefaultCellStyle.SelectionBackColor = Color.FromArgb(240, 245, 255);
            dgvAssignments.DefaultCellStyle.SelectionForeColor = Color.Black;
            dgvAssignments.DefaultCellStyle.Font = new Font("Segoe UI", 9.5F);

            dgvAssignments.Columns["colDeadline"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgvAssignments.Columns["colStatus"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            dgvAssignments.Columns["colActions"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            dgvAssignments.Columns["colActions"].Width = 100;
        }

        private void LoadMockData()
        {
            dgvAssignments.Rows.Add(false, "Bài 1: Hello World", "Dễ", "Cơ bản", "2025-11-20", "45", "Active");
            dgvAssignments.Rows.Add(false, "Bài 2: Vòng lặp For", "Dễ", "Cơ bản", "2025-11-22", "42", "Active");
            dgvAssignments.Rows.Add(false, "Bài 3: Mảng (Array)", "Trung bình", "Cấu trúc dữ liệu", "2025-11-25", "28", "Active");
            dgvAssignments.Rows.Add(false, "Bài 4: Hàm (Function)", "Trung bình", "Cơ bản", "2025-11-28", "15", "Active");
            dgvAssignments.Rows.Add(false, "Bài 5: Đệ quy", "Khó", "Thuật toán", "2025-12-01", "8", "Active");
            dgvAssignments.Rows.Add(false, "Bài 6: Tìm kiếm", "Trung bình", "Thuật toán", "2025-12-05", "5", "Draft");
            dgvAssignments.Rows.Add(false, "Bài 7: Sắp xếp", "Khó", "Thuật toán", "2025-12-08", "2", "Draft");

            dgvAssignments.ClearSelection();
        }

        // --- LOGIC VẼ NÚT (GIỮ NGUYÊN) ---
        private void DgvAssignments_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex < 0) return;

            // Tô màu Độ khó
            if (e.ColumnIndex == dgvAssignments.Columns["colDifficulty"].Index && e.Value != null)
            {
                string diff = e.Value.ToString();
                Color color = Color.Black;
                if (diff == "Dễ") color = Color.FromArgb(40, 167, 69);
                else if (diff == "Trung bình") color = Color.FromArgb(255, 140, 0);
                else if (diff == "Khó") color = Color.FromArgb(220, 53, 69);

                e.Paint(e.CellBounds, DataGridViewPaintParts.All & ~DataGridViewPaintParts.ContentForeground);
                TextRenderer.DrawText(e.Graphics, diff, e.CellStyle.Font, e.CellBounds, color, TextFormatFlags.VerticalCenter | TextFormatFlags.Left);
                e.Handled = true;
            }
            // Tô màu Status
            else if (e.ColumnIndex == dgvAssignments.Columns["colStatus"].Index && e.Value != null)
            {
                string status = e.Value.ToString();
                Color color = status == "Active" ? Color.FromArgb(40, 167, 69) : Color.Gray;

                e.Paint(e.CellBounds, DataGridViewPaintParts.All & ~DataGridViewPaintParts.ContentForeground);
                TextRenderer.DrawText(e.Graphics, status, e.CellStyle.Font, e.CellBounds, color, TextFormatFlags.VerticalCenter | TextFormatFlags.Left);
                e.Handled = true;
            }
            // Vẽ 2 nút Actions
            else if (e.ColumnIndex == dgvAssignments.Columns["colActions"].Index)
            {
                e.Paint(e.CellBounds, DataGridViewPaintParts.All & ~DataGridViewPaintParts.ContentForeground);

                // Tính toán vị trí tuyệt đối trên bảng để vẽ
                int centerY = e.CellBounds.Y + (e.CellBounds.Height - ButtonHeight) / 2;
                var rectEdit = new Rectangle(e.CellBounds.X + ButtonMarginX, centerY, ButtonWidth, ButtonHeight);
                var rectDel = new Rectangle(rectEdit.Right + ButtonSpacing, centerY, ButtonWidth, ButtonHeight);

                // Vẽ nút Edit
                using (Pen p = new Pen(Color.FromArgb(13, 110, 253))) e.Graphics.DrawRectangle(p, rectEdit);
                e.Graphics.FillRectangle(Brushes.White, rectEdit.X + 1, rectEdit.Y + 1, rectEdit.Width - 2, rectEdit.Height - 2);
                TextRenderer.DrawText(e.Graphics, "📝", new Font("Segoe UI Emoji", 12), rectEdit, Color.FromArgb(13, 110, 253), TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);

                // Vẽ nút Delete
                using (Pen p = new Pen(Color.FromArgb(220, 53, 69))) e.Graphics.DrawRectangle(p, rectDel);
                e.Graphics.FillRectangle(Brushes.White, rectDel.X + 1, rectDel.Y + 1, rectDel.Width - 2, rectDel.Height - 2);
                TextRenderer.DrawText(e.Graphics, "🗑", new Font("Segoe UI Emoji", 12), rectDel, Color.FromArgb(220, 53, 69), TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);

                e.Handled = true;
            }
        }

        // --- LOGIC XỬ LÝ CLICK (ĐÃ SỬA ĐỔI) ---
        private void DgvAssignments_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            // Chỉ xử lý khi click chuột trái vào cột Actions và không phải header
            if (e.Button == MouseButtons.Left && e.RowIndex >= 0 && e.ColumnIndex == dgvAssignments.Columns["colActions"].Index)
            {
                // e.X và e.Y trong sự kiện này là tọa độ TƯƠNG ĐỐI so với góc trên-trái của ô
                // Điều này giúp ta không cần tính toán phức tạp với CellBounds hay Scrolling

                int rowHeight = dgvAssignments.Rows[e.RowIndex].Height;
                int centerY = (rowHeight - ButtonHeight) / 2;

                // Định nghĩa lại vùng hình chữ nhật của nút TRONG HỆ TỌA ĐỘ CỦA Ô
                // Nút Edit: X bắt đầu từ ButtonMarginX (5)
                var rectEdit = new Rectangle(ButtonMarginX, centerY, ButtonWidth, ButtonHeight);

                // Nút Delete: Nằm bên phải Edit
                var rectDel = new Rectangle(rectEdit.Right + ButtonSpacing, centerY, ButtonWidth, ButtonHeight);

                // Kiểm tra xem điểm click (e.Location) có nằm trong vùng nút không
                if (rectEdit.Contains(e.Location))
                {
                    string name = dgvAssignments.Rows[e.RowIndex].Cells["colName"].Value.ToString();
                    MessageBox.Show($"Sửa bài tập: {name}", "Edit Action", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if (rectDel.Contains(e.Location))
                {
                    string name = dgvAssignments.Rows[e.RowIndex].Cells["colName"].Value.ToString();
                    if (MessageBox.Show($"Bạn có chắc chắn muốn xóa bài: {name}?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                    {
                        dgvAssignments.Rows.RemoveAt(e.RowIndex);
                    }
                }
            }
        }
    }
}