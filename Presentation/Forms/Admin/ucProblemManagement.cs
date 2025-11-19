using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using CodeForge_Desktop.Business.Services;
using CodeForge_Desktop.DataAccess.Entities;
using CodeForge_Desktop.DataAccess.Repositories; // Cần thêm dòng này để new Repository

namespace CodeForge_Desktop.Presentation.Forms.Admin
{
    public partial class ucProblemManagement : UserControl
    {
        private readonly CodingProblemService _problemService;

        private const int ButtonWidth = 30;
        private const int ButtonHeight = 30;
        private const int ButtonSpacing = 10;
        private const int ButtonMarginX = 5;

        public ucProblemManagement()
        {
            InitializeComponent();

            // --- SỬA LỖI KHỞI TẠO SERVICE ---
            // Vì Service của bạn cần Repository, ta phải tạo Repository trước
            var repo = new CodingProblemRepository();
            _problemService = new CodingProblemService(repo);

            SetupDataGridView();
            LoadData();

            dgvAssignments.CellPainting += DgvAssignments_CellPainting;
            dgvAssignments.CellMouseClick += DgvAssignments_CellMouseClick;

            btnAdd.Click += BtnAdd_Click;
            btnEdit.Click += BtnEdit_Click;
            btnDelete.Click += BtnDelete_Click;

            SetupSearchBox();
        }

        private void SetupSearchBox()
        {
            txtSearch.GotFocus += (s, e) => { if (txtSearch.Text == "Tìm kiếm assignment...") { txtSearch.Text = ""; txtSearch.ForeColor = Color.Black; } };
            txtSearch.LostFocus += (s, e) => { if (string.IsNullOrWhiteSpace(txtSearch.Text)) { txtSearch.Text = "Tìm kiếm assignment..."; txtSearch.ForeColor = Color.Gray; } };
            txtSearch.TextChanged += (s, e) => {
                if (txtSearch.Text != "Tìm kiếm assignment...") LoadData(txtSearch.Text);
            };
        }

        private void SetupDataGridView()
        {
            // 1. Cấu hình chung cho toàn bộ bảng
            dgvAssignments.DefaultCellStyle.SelectionBackColor = Color.FromArgb(240, 245, 255);
            dgvAssignments.DefaultCellStyle.SelectionForeColor = Color.Black;
            dgvAssignments.DefaultCellStyle.Font = new Font("Segoe UI", 9.5F);

            // 2. Cấu hình an toàn cho từng cột (Check null trước khi truy cập)

            // Cột Deadline
            if (dgvAssignments.Columns["colDeadline"] != null)
            {
                dgvAssignments.Columns["colDeadline"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            }

            // Cột Status
            if (dgvAssignments.Columns["colStatus"] != null)
            {
                dgvAssignments.Columns["colStatus"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            }

            // Cột Actions (Quan trọng để vẽ nút)
            if (dgvAssignments.Columns["colActions"] != null)
            {
                dgvAssignments.Columns["colActions"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                dgvAssignments.Columns["colActions"].Width = 100;
            }

            // Cột Submissions (nếu có)
            if (dgvAssignments.Columns.Contains("colSubmissions") && dgvAssignments.Columns["colSubmissions"] != null)
            {
                dgvAssignments.Columns["colSubmissions"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
        }

        private void LoadData(string keyword = "")
        {
            dgvAssignments.Rows.Clear();
            List<CodingProblem> list;

            if (string.IsNullOrWhiteSpace(keyword) || keyword == "Tìm kiếm assignment...")
            {
                // SỬA LỖI TÊN HÀM: Dùng GetAll() thay vì GetAllProblems()
                list = _problemService.GetAll();
            }
            else
            {
                // SỬA LỖI TÌM KIẾM: Nếu Service chưa có SearchProblems, ta gọi GetAll rồi lọc thủ công tại đây
                var all = _problemService.GetAll();
                list = all.FindAll(p => p.Title.ToLower().Contains(keyword.ToLower()));
            }

            foreach (var p in list)
            {
                // Map dữ liệu
                int rowIndex = dgvAssignments.Rows.Add(
                    false,
                    p.Title,
                    p.Difficulty,
                    p.Tags, // Dùng Tags làm Category
                    "", // Deadline chưa có thì để trống
                    0, // Submissions chưa có thì để 0
                    p.Status,
                    ""
                );

                dgvAssignments.Rows[rowIndex].Tag = p.ProblemID;
            }

            lblSummary.Text = $"Tổng số: {list.Count} assignments";
            dgvAssignments.ClearSelection();
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            // Đảm bảo AddEditProblemForm có constructor nhận service
            var form = new AddEditProblemForm(_problemService);
            if (form.ShowDialog() == DialogResult.OK) LoadData();
        }

        private void BtnEdit_Click(object sender, EventArgs e)
        {
            if (dgvAssignments.SelectedRows.Count > 0)
            {
                Guid id = (Guid)dgvAssignments.SelectedRows[0].Tag;
                // Đảm bảo AddEditProblemForm có constructor nhận service và ID
                var form = new AddEditProblemForm(_problemService, id);
                if (form.ShowDialog() == DialogResult.OK) LoadData();
            }
            else
            {
                MessageBox.Show("Vui lòng chọn bài tập cần sửa.", "Thông báo");
            }
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            List<Guid> idsToDelete = new List<Guid>();
            foreach (DataGridViewRow row in dgvAssignments.Rows)
            {
                if (Convert.ToBoolean(row.Cells["colCheck"].Value))
                {
                    if (row.Tag is Guid id) idsToDelete.Add(id);
                }
            }

            if (idsToDelete.Count == 0)
            {
                if (dgvAssignments.SelectedRows.Count > 0 && dgvAssignments.SelectedRows[0].Tag is Guid id)
                {
                    idsToDelete.Add(id);
                }
                else
                {
                    MessageBox.Show("Vui lòng chọn ít nhất một bài tập để xóa.", "Thông báo");
                    return;
                }
            }

            if (MessageBox.Show($"Bạn có chắc muốn xóa {idsToDelete.Count} bài tập đã chọn?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                // SỬA LỖI GỌI HÀM XÓA: Bạn cần tự viết vòng lặp gọi Delete
                bool success = true;
                foreach (var id in idsToDelete)
                {
                    if (!_problemService.Delete(id)) success = false;
                }

                if (success)
                {
                    MessageBox.Show("Đã xóa thành công.", "Thông báo");
                    LoadData();
                }
                else
                {
                    MessageBox.Show("Có lỗi xảy ra khi xóa một số bài tập.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    LoadData();
                }
            }
        }

        // --- Giữ nguyên phần Painting và Click ---
        private void DgvAssignments_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex < 0) return;

            if (e.ColumnIndex == dgvAssignments.Columns["colDifficulty"].Index && e.Value != null)
            {
                string diff = e.Value.ToString();
                Color color = Color.Black;
                if (diff == "Dễ") color = Color.Green;
                else if (diff == "Trung bình") color = Color.Orange;
                else if (diff == "Khó") color = Color.Red;

                e.Paint(e.CellBounds, DataGridViewPaintParts.All & ~DataGridViewPaintParts.ContentForeground);
                TextRenderer.DrawText(e.Graphics, diff, e.CellStyle.Font, e.CellBounds, color, TextFormatFlags.VerticalCenter | TextFormatFlags.Left);
                e.Handled = true;
            }
            else if (e.ColumnIndex == dgvAssignments.Columns["colStatus"].Index && e.Value != null)
            {
                string status = e.Value.ToString();
                Color color = status == "Active" ? Color.Green : Color.Gray;

                e.Paint(e.CellBounds, DataGridViewPaintParts.All & ~DataGridViewPaintParts.ContentForeground);
                TextRenderer.DrawText(e.Graphics, status, e.CellStyle.Font, e.CellBounds, color, TextFormatFlags.VerticalCenter | TextFormatFlags.Left);
                e.Handled = true;
            }
            else if (e.ColumnIndex == dgvAssignments.Columns["colActions"].Index)
            {
                e.Paint(e.CellBounds, DataGridViewPaintParts.All & ~DataGridViewPaintParts.ContentForeground);
                int centerY = e.CellBounds.Y + (e.CellBounds.Height - ButtonHeight) / 2;
                var rectEdit = new Rectangle(e.CellBounds.X + ButtonMarginX, centerY, ButtonWidth, ButtonHeight);
                var rectDel = new Rectangle(rectEdit.Right + ButtonSpacing, centerY, ButtonWidth, ButtonHeight);

                using (Pen p = new Pen(Color.DodgerBlue)) e.Graphics.DrawRectangle(p, rectEdit);
                TextRenderer.DrawText(e.Graphics, "📝", new Font("Segoe UI Emoji", 10), rectEdit, Color.DodgerBlue, TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);

                using (Pen p = new Pen(Color.Red)) e.Graphics.DrawRectangle(p, rectDel);
                TextRenderer.DrawText(e.Graphics, "🗑", new Font("Segoe UI Emoji", 10), rectDel, Color.Red, TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);

                e.Handled = true;
            }
        }

        private void DgvAssignments_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && e.RowIndex >= 0 && e.ColumnIndex == dgvAssignments.Columns["colActions"].Index)
            {
                int rowHeight = dgvAssignments.Rows[e.RowIndex].Height;
                int centerY = (rowHeight - ButtonHeight) / 2;

                var rectEdit = new Rectangle(ButtonMarginX, centerY, ButtonWidth, ButtonHeight);
                var rectDel = new Rectangle(rectEdit.Right + ButtonSpacing, centerY, ButtonWidth, ButtonHeight);

                Guid problemId = (Guid)dgvAssignments.Rows[e.RowIndex].Tag;
                string title = dgvAssignments.Rows[e.RowIndex].Cells["colName"].Value.ToString();

                if (rectEdit.Contains(e.Location))
                {
                    var form = new AddEditProblemForm(_problemService, problemId);
                    if (form.ShowDialog() == DialogResult.OK) LoadData();
                }
                else if (rectDel.Contains(e.Location))
                {
                    if (MessageBox.Show($"Xóa bài tập: {title}?", "Xác nhận", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        if (_problemService.Delete(problemId)) // Dùng hàm Delete của bạn
                        {
                            LoadData();
                        }
                        else
                        {
                            MessageBox.Show("Xóa thất bại.");
                        }
                    }
                }
            }
        }
    }
}