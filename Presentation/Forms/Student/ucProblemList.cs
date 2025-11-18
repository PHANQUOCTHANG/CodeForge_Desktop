using System;
using System.Drawing;
using System.Windows.Forms;

// Đảm bảo namespace này khớp với project của bạn
namespace CodeForge_Desktop.Presentation.Forms.Student
{
    public partial class ucProblemList : UserControl
    {
        private const string PlaceholderText = "Tìm kiếm bài tập...";

        public ucProblemList()
        {
            InitializeComponent();
            SetupPlaceholder();
            SetupSearchBox();
            SetupDataGridViewStyles(); // Thêm hàm style
            LoadMockData(); // Thêm hàm load data

            // Đăng ký sự kiện CellPainting để tô màu
            dgvProblemList.CellPainting += DgvProblemList_CellPainting;
        }

        /// <summary>
        /// Thêm dữ liệu mẫu vào DataGridView
        /// </summary>
        private void LoadMockData()
        {
            dgvProblemList.Rows.Add("1", "Bài 1: Hello World", "Dễ", "2025-11-20", "Đã nộp", "100%");
            dgvProblemList.Rows.Add("2", "Bài 2: Vòng lặp For", "Dễ", "2025-11-22", "Đã nộp", "95%");
            dgvProblemList.Rows.Add("3", "Bài 3: Mảng (Array)", "Trung bình", "2025-11-25", "Chưa nộp", "-");
            dgvProblemList.Rows.Add("4", "Bài 4: Hàm (Function)", "Trung bình", "2025-11-28", "Chưa nộp", "-");
            dgvProblemList.Rows.Add("5", "Bài 5: Đệ quy", "Khó", "2025-12-01", "Chưa nộp", "-");
            dgvProblemList.Rows.Add("6", "Bài 6: Tìm kiếm", "Trung bình", "2025-12-05", "Chưa nộp", "-");
            dgvProblemList.Rows.Add("7", "Bài 7: Sắp xếp", "Khó", "2025-12-08", "Chưa nộp", "-");
            dgvProblemList.Rows.Add("8", "Bài 8: Cấu trúc dữ liệu", "Khó", "2025-12-12", "Chưa nộp", "-");
        }

        /// <summary>
        /// Hàm mới để tùy chỉnh giao diện cho DataGridView (giống ucSubmissions)
        /// </summary>
        private void SetupDataGridViewStyles()
        {
            // TẮT giao diện mặc định của Windows để nhận màu tùy chỉnh
            dgvProblemList.EnableHeadersVisualStyles = false;

            // Thiết lập Font, màu nền và màu chữ cho Header
            dgvProblemList.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(240, 240, 240); // Màu xám rất nhạt
            dgvProblemList.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;
            dgvProblemList.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            dgvProblemList.ColumnHeadersDefaultCellStyle.Padding = new Padding(0, 5, 0, 5); // Thêm chút đệm

            // Tùy chỉnh chung
            dgvProblemList.BorderStyle = BorderStyle.None;
            dgvProblemList.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal; // Viền ngang
            dgvProblemList.DefaultCellStyle.Font = new Font("Segoe UI", 9.75F);
            dgvProblemList.DefaultCellStyle.ForeColor = Color.FromArgb(64, 64, 64);

            // --- CẤU HÌNH CHỌN DÒNG (ROW SELECTION) ---
            // 1. Chế độ chọn: Chọn toàn bộ hàng thay vì từng ô
            dgvProblemList.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            // 2. Tắt khả năng chọn nhiều dòng (nếu muốn chỉ chọn 1 dòng tại 1 thời điểm)
            dgvProblemList.MultiSelect = false;

            // 3. Màu nền khi được chọn: Màu xám nhạt (thay vì xanh dương mặc định)
            dgvProblemList.DefaultCellStyle.SelectionBackColor = Color.FromArgb(230, 230, 230);

            // 4. Màu chữ khi được chọn: Màu đen (để dễ đọc trên nền xám)
            dgvProblemList.DefaultCellStyle.SelectionForeColor = Color.Black;

            // Màu xen kẽ cho các hàng (giống Figma)
            dgvProblemList.RowsDefaultCellStyle.BackColor = Color.White;
            dgvProblemList.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(247, 249, 252); // Màu xanh nhạt
        }

        /// <summary>
        /// Sự kiện để tô màu cho các ô Trạng thái và Độ khó
        /// </summary>
        private void DgvProblemList_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex < 0) return; // Bỏ qua header

            // --- Logic cho cột "Độ khó" ---
            if (e.ColumnIndex == dgvProblemList.Columns["colDifficulty"].Index && e.Value != null)
            {
                string difficulty = e.Value.ToString();
                Color textColor = Color.Black;

                switch (difficulty)
                {
                    case "Dễ":
                        textColor = Color.Green;
                        break;
                    case "Trung bình":
                        textColor = Color.Orange;
                        break;
                    case "Khó":
                        textColor = Color.Red;
                        break;
                }
                PaintCell(e, textColor);
            }

            // --- Logic cho cột "Trạng thái" ---
            if (e.ColumnIndex == dgvProblemList.Columns["colStatus"].Index && e.Value != null)
            {
                string status = e.Value.ToString();
                Color textColor = Color.Black;

                switch (status)
                {
                    case "Đã nộp":
                        textColor = Color.Green;
                        break;
                    case "Chưa nộp":
                        textColor = Color.OrangeRed;
                        break;
                }
                PaintCell(e, textColor);
            }
        }

        // Hàm helper để vẽ lại ô với màu chữ tùy chỉnh
        private void PaintCell(DataGridViewCellPaintingEventArgs e, Color foreColor)
        {
            e.Paint(e.CellBounds, DataGridViewPaintParts.All & ~DataGridViewPaintParts.ContentForeground);
            TextRenderer.DrawText(e.Graphics, e.FormattedValue.ToString(), e.CellStyle.Font, e.CellBounds, foreColor, TextFormatFlags.VerticalCenter | TextFormatFlags.Left);
            e.Handled = true;
        }

        /// <summary>
        /// Căn chỉnh lại TextBox bên trong Panel container
        /// </summary>
        private void SetupSearchBox()
        {
            // Căn giữa TextBox theo chiều dọc
            // Bằng cách điều chỉnh Padding của TextBox
            int paddingTop = (pnlSearchContainer.Height - txtSearch.Font.Height) / 2;
            if (paddingTop > 0)
            {
                txtSearch.Padding = new Padding(0, paddingTop, 0, 0);
            }
        }

        /// <summary>
        /// Thiết lập văn bản giữ chỗ (placeholder) cho ô tìm kiếm
        /// </summary>
        private void SetupPlaceholder()
        {
            // Gán giá trị ban đầu
            txtSearch.Text = PlaceholderText;
            txtSearch.ForeColor = System.Drawing.Color.Gray;

            // Đăng ký sự kiện
            txtSearch.GotFocus += RemovePlaceholder;
            txtSearch.LostFocus += AddPlaceholder;
        }

        private void RemovePlaceholder(object sender, EventArgs e)
        {
            if (txtSearch.Text == PlaceholderText)
            {
                txtSearch.Text = "";
                txtSearch.ForeColor = System.Drawing.Color.Black;
            }
        }

        private void AddPlaceholder(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtSearch.Text))
            {
                txtSearch.Text = PlaceholderText;
                txtSearch.ForeColor = System.Drawing.Color.Gray;
            }
        }
    }
}