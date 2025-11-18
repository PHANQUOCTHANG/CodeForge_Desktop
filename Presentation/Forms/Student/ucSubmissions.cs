using System;
using System.Drawing;
using System.Windows.Forms;

// Đảm bảo namespace này khớp với project của bạn
namespace CodeForge_Desktop.Presentation.Forms.Student
{
    public partial class ucSubmissions : UserControl
    {
        public ucSubmissions()
        {
            InitializeComponent();
            SetupComboBoxes();
            SetupDataGridViewStyles(); // GỌI HÀM MỚI TẠI ĐÂY

            // Đăng ký sự kiện để vẽ màu cho ô và các nút
            dgvSubmissions.CellPainting += DgvSubmissions_CellPainting;
        }

        /// <summary>
        /// Hàm mới để tùy chỉnh giao diện cho DataGridView
        /// </summary>
        private void SetupDataGridViewStyles()
        {
            // TẮT giao diện mặc định của Windows để nhận màu tùy chỉnh
            dgvSubmissions.EnableHeadersVisualStyles = false;

            // Thiết lập Font, màu nền và màu chữ cho Header
            dgvSubmissions.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(240, 240, 240); // Màu xám rất nhạt
            dgvSubmissions.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;
            dgvSubmissions.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            dgvSubmissions.ColumnHeadersDefaultCellStyle.Padding = new Padding(0, 3, 0, 3); // Thêm chút đệm

            // Tùy chỉnh chung
            dgvSubmissions.BorderStyle = BorderStyle.None;
            dgvSubmissions.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal; // Viền ngang
            dgvSubmissions.DefaultCellStyle.Font = new Font("Segoe UI", 9.75F);
            dgvSubmissions.DefaultCellStyle.ForeColor = Color.FromArgb(64, 64, 64);
        }

        private void SetupComboBoxes()
        {
            // Căn chỉnh ComboBoxes cho đẹp hơn trong pnlFilters
            int controlPadding = (pnlFilters.Height - cmbProblems.Height) / 2;
            if (controlPadding > 0)
            {
                cmbProblems.Margin = new Padding(3, controlPadding, 3, 3);
                cmbStatus.Margin = new Padding(3, controlPadding, 3, 3);
            }
        }

        private void DgvSubmissions_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            // --- Logic để vẽ màu cho cột "Trạng thái" ---
            if (e.RowIndex >= 0 && e.ColumnIndex == dgvSubmissions.Columns["colStatus"].Index)
            {
                if (e.Value != null)
                {
                    string status = e.Value.ToString();
                    Color textColor = Color.Black;

                    switch (status)
                    {
                        case "Accepted":
                            textColor = Color.Green;
                            break;
                        case "Wrong Answer":
                            textColor = Color.OrangeRed;
                            break;
                        case "Time Limit":
                        case "Time Limit Exceeded": // Thêm trường hợp
                            textColor = Color.MediumBlue;
                            break;
                        case "Runtime Error":
                            textColor = Color.Red;
                            break;
                    }

                    // Vẽ lại ô
                    e.Paint(e.CellBounds, DataGridViewPaintParts.All & ~DataGridViewPaintParts.ContentForeground);
                    TextRenderer.DrawText(e.Graphics, e.FormattedValue.ToString(), e.CellStyle.Font, e.CellBounds, textColor, TextFormatFlags.VerticalCenter | TextFormatFlags.Left);
                    e.Handled = true;
                }
            }

            // --- Logic để vẽ nút "Tải" (icon) ---
            if (e.RowIndex >= 0 && e.ColumnIndex == dgvSubmissions.Columns["colDownload"].Index)
            {
                // (Sau này bạn sẽ thay thế bằng icon "download" từ Resources)
                // e.Paint(e.CellBounds, DataGridViewPaintParts.All);
                // var icon = Properties.Resources.download_icon;
                // e.Graphics.DrawImage(icon, e.CellBounds.Left + 5, e.CellBounds.Top + 5);
                // e.Handled = true;
            }
        }
    }
}