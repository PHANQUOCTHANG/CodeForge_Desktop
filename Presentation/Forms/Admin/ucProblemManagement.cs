        using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using CodeForge_Desktop.Business.Interfaces;
using CodeForge_Desktop.Business.Services;
using CodeForge_Desktop.DataAccess.Entities;
using CodeForge_Desktop.DataAccess.Repositories;
using CodeForge_Desktop.DataAccess.Interfaces;

namespace CodeForge_Desktop.Presentation.Forms.Admin
{
    public partial class ucProblemManagement : UserControl
    {
        private ICodingProblemService _problemService;
        private ITestCaseService _testCaseService;
        private ICodingProblemRepository _problemRepository;
        private ITestCaseRepository _testCaseRepository;
        private WordImportService _wordImportService;

        private const int ButtonWidth = 30;
        private const int ButtonHeight = 30;
        private const int ButtonSpacing = 10;
        private const int ButtonMarginX = 5;

        public ucProblemManagement()
        {
            InitializeComponent();

            // Khởi tạo repositories
            _problemRepository = new CodingProblemRepository();
            _testCaseRepository = new TestCaseRepository();

            // Khởi tạo services
            _problemService = new CodingProblemService();
            _testCaseService = new TestCaseService();
            _wordImportService = new WordImportService(_problemRepository, _testCaseRepository);

            SetupDataGridView();
            LoadData();

            dgvAssignments.CellPainting += DgvAssignments_CellPainting;
            dgvAssignments.CellMouseClick += DgvAssignments_CellMouseClick;

            btnAdd.Click += BtnAdd_Click;
            btnEdit.Click += BtnEdit_Click;
            btnDelete.Click += BtnDelete_Click;
            btnImportWord.Click += BtnImportWord_Click; // ✅ Thêm event cho nút Import

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
            dgvAssignments.DefaultCellStyle.SelectionBackColor = Color.FromArgb(240, 245, 255);
            dgvAssignments.DefaultCellStyle.SelectionForeColor = Color.Black;
            dgvAssignments.DefaultCellStyle.Font = new Font("Segoe UI", 9.5F);

            if (dgvAssignments.Columns["colDeadline"] != null)
            {
                dgvAssignments.Columns["colDeadline"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            }

            if (dgvAssignments.Columns["colStatus"] != null)
            {
                dgvAssignments.Columns["colStatus"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            }

            if (dgvAssignments.Columns["colActions"] != null)
            {
                dgvAssignments.Columns["colActions"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                dgvAssignments.Columns["colActions"].Width = 100;
            }

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
                list = _problemService.GetAll();
            }
            else
            {
                var all = _problemService.GetAll();
                list = all.FindAll(p => p.Title.ToLower().Contains(keyword.ToLower()));
            }

            foreach (var p in list)
            {
                int rowIndex = dgvAssignments.Rows.Add(
                    false,
                    p.Title,
                    p.Difficulty,
                    p.Tags, 
                    "",
                    0
                );

                dgvAssignments.Rows[rowIndex].Tag = p.ProblemID;
            }

            lblSummary.Text = $"Tổng số: {list.Count} assignments";
            dgvAssignments.ClearSelection();
        }

        /// <summary>
        /// ✅ Event handler cho nút Import Word
        /// </summary>
        private void BtnImportWord_Click(object sender, EventArgs e)
        {
            // Mở dialog chọn file
            using (var ofd = new OpenFileDialog())
            {
                ofd.Filter = "Word Documents (*.docx)|*.docx|All Files (*.*)|*.*";
                ofd.Title = "Chọn file Word chứa bài lập trình";

                ImportWordFile(ofd.FileName);

                //if (ofd.ShowDialog() == DialogResult.OK)
                //{
                //    ImportWordFile(ofd.FileName);
                //}
            }
        }

        /// <summary>
        /// ✅ Thực hiện import file Word
        /// </summary>
        private void ImportWordFile(string filePath)
        {
            try
            {
                // Hiển thị form import với log chi tiết
                using (var importForm = new ImportWordForm(_wordImportService))
                {
                    //bool isConfirmed = importForm.ShowDialog() == DialogResult.OK;
                    if (importForm.ShowDialog() == DialogResult.OK)
                    {
                        // Reload dữ liệu sau khi import thành công
                        LoadData();
                        MessageBox.Show("Import hoàn tất! Danh sách bài tập đã được cập nhật.", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi import: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            var form = new AddEditProblemForm(_problemService, _testCaseService);
            if (form.ShowDialog() == DialogResult.OK) LoadData();
        }

        private void BtnEdit_Click(object sender, EventArgs e)
        {
            if (dgvAssignments.SelectedRows.Count > 0)
            {
                Guid id = (Guid)dgvAssignments.SelectedRows[0].Tag;
                var form = new AddEditProblemForm(_problemService, _testCaseService, id);
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
                    var form = new AddEditProblemForm(_problemService, _testCaseService, problemId);
                    if (form.ShowDialog() == DialogResult.OK) LoadData();
                }
                else if (rectDel.Contains(e.Location))
                {
                    if (MessageBox.Show($"Xóa bài tập: {title}?", "Xác nhận", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        if (_problemService.Delete(problemId))
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