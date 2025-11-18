using System;
using System.Drawing;
using System.Windows.Forms;

namespace CodeForge_Desktop.Presentation.Forms.Student
{
    public partial class ucStudentDashboard : UserControl
    {
        public event EventHandler ProblemListClicked;
        public event EventHandler SubmissionsClicked;
        public event EventHandler SettingsClicked;
        public ucStudentDashboard()
        {
            InitializeComponent();

            // Giảm giật lag khi vẽ lại giao diện
            this.DoubleBuffered = true;

            LoadSampleData();

            // Đăng ký sự kiện tô màu (vì logic này nên nằm ở code-behind)
            dgvRecent.CellPainting += DgvRecent_CellPainting;

            // Khi bấm nút -> Kích hoạt sự kiện để Form cha biết
            if (btnActionList != null)
                btnActionList.Click += (s, e) => ProblemListClicked?.Invoke(this, EventArgs.Empty);

            if (btnActionHistory != null)
                btnActionHistory.Click += (s, e) => SubmissionsClicked?.Invoke(this, EventArgs.Empty);

            if (btnActionSettings != null)
                btnActionSettings.Click += (s, e) => SettingsClicked?.Invoke(this, EventArgs.Empty);

        }

        private void LoadSampleData()
        {
            // Thêm dữ liệu mẫu y hệt hình
            dgvRecent.Rows.Add("1", "Bài 1: Hello World", "2025-11-20", "Đã nộp", "100%");
            dgvRecent.Rows.Add("2", "Bài 2: Vòng lặp For", "2025-11-22", "Đã nộp", "95%");
            dgvRecent.Rows.Add("3", "Bài 3: Mảng (Array)", "2025-11-25", "Chưa nộp", "-");
            dgvRecent.Rows.Add("4", "Bài 4: Hàm (Function)", "2025-11-28", "Chưa nộp", "-");

            // Bỏ chọn mặc định dòng đầu tiên để nhìn đẹp hơn
            dgvRecent.ClearSelection();
        }

        private void DgvRecent_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            // Tô màu cột Trạng thái (Index = 3)
            if (e.RowIndex >= 0 && e.ColumnIndex == 3)
            {
                string status = e.Value?.ToString();
                Color color = Color.Black;

                if (status == "Đã nộp") color = Color.FromArgb(76, 175, 80); // Xanh lá
                else if (status == "Chưa nộp") color = Color.FromArgb(255, 152, 0); // Cam

                // Nếu cần vẽ tùy chỉnh hoàn toàn, ta sẽ dùng e.Graphics
                // Nhưng ở đây ta chỉ muốn đổi màu chữ, cách đơn giản nhất là set Style cho Cell
                // Tuy nhiên, trong sự kiện CellPainting, ta có thể can thiệp sâu hơn nếu muốn.
                // Để đơn giản và hiệu quả, ta chỉ set màu chữ:
                dgvRecent.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.ForeColor = color;
                dgvRecent.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.Font = new Font("Segoe UI", 9, FontStyle.Bold);
            }
        }
    }
}