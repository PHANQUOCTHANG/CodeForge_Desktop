using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
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

        // Pagination properties
        private int _currentPage = 1;
        private int _pageSize = 10;
        private int _totalRecords = 0;
        private int _totalPages = 0;
        private List<CodingProblem> _allProblems = new List<CodingProblem>();
        private List<Button> _pageButtons = new List<Button>();

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

            InitializePageSize();
            SetupDataGridView();
            LoadData();

            dgvAssignments.CellPainting += DgvAssignments_CellPainting;
            dgvAssignments.CellMouseClick += DgvAssignments_CellMouseClick;

            btnAdd.Click += BtnAdd_Click;
            btnEdit.Click += BtnEdit_Click;
            btnDelete.Click += BtnDelete_Click;
            btnImportWord.Click += BtnImportWord_Click;

            // Pagination events
            btnFirstPage.Click += (s, e) => NavigateToPage(1);
            btnPrevPage.Click += (s, e) => NavigateToPage(_currentPage - 1);
            btnNextPage.Click += (s, e) => NavigateToPage(_currentPage + 1);
            btnLastPage.Click += (s, e) => NavigateToPage(_totalPages);
            cboPageSize.SelectedIndexChanged += CboPageSize_SelectedIndexChanged;

            SetupSearchBox();
        }

        private void InitializePageSize()
        {
            cboPageSize.SelectedIndex = 0; // Default: 10
            _pageSize = int.Parse(cboPageSize.SelectedItem.ToString());
        }

        private void SetupSearchBox()
        {
            txtSearch.GotFocus += (s, e) =>
            {
                if (txtSearch.Text == "Tìm kiếm bài tập...")
                {
                    txtSearch.Text = "";
                    txtSearch.ForeColor = Color.FromArgb(71, 85, 105);
                }
            };

            txtSearch.LostFocus += (s, e) =>
            {
                if (string.IsNullOrWhiteSpace(txtSearch.Text))
                {
                    txtSearch.Text = "Tìm kiếm bài tập...";
                    txtSearch.ForeColor = Color.FromArgb(100, 116, 139);
                }
            };

            txtSearch.TextChanged += (s, e) =>
            {
                if (txtSearch.Text != "Tìm kiếm bài tập...")
                {
                    _currentPage = 1;
                    LoadData(txtSearch.Text);
                }
            };
        }

        private void SetupDataGridView()
        {
            // Remove default selection
            dgvAssignments.ClearSelection();

            // Modern styling
            dgvAssignments.DefaultCellStyle.SelectionBackColor = Color.FromArgb(239, 246, 255);
            dgvAssignments.DefaultCellStyle.SelectionForeColor = Color.FromArgb(30, 64, 175);
            dgvAssignments.DefaultCellStyle.Font = new Font("Segoe UI", 9.75F);

            // Column alignment
            if (dgvAssignments.Columns["colDifficulty"] != null)
            {
                dgvAssignments.Columns["colDifficulty"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }

            if (dgvAssignments.Columns["colActions"] != null)
            {
                dgvAssignments.Columns["colActions"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
        }

        private void LoadData(string keyword = "")
        {
            // Load all data first
            if (string.IsNullOrWhiteSpace(keyword) || keyword == "Tìm kiếm bài tập...")
            {
                _allProblems = _problemService.GetAll();
            }
            else
            {
                var all = _problemService.GetAll();
                _allProblems = all.FindAll(p => p.Title.ToLower().Contains(keyword.ToLower()));
            }

            _totalRecords = _allProblems.Count;
            _totalPages = (int)Math.Ceiling((double)_totalRecords / _pageSize);

            // Ensure current page is valid
            if (_currentPage > _totalPages && _totalPages > 0)
                _currentPage = _totalPages;
            if (_currentPage < 1)
                _currentPage = 1;

            // Load paginated data
            LoadPageData();
            UpdatePaginationUI();
        }

        private void LoadPageData()
        {
            dgvAssignments.Rows.Clear();

            var pagedData = _allProblems
                .Skip((_currentPage - 1) * _pageSize)
                .Take(_pageSize)
                .ToList();

            foreach (var p in pagedData)
            {
                int rowIndex = dgvAssignments.Rows.Add(
                    false,
                    p.Title,
                    p.Difficulty,
                    p.Tags
                );

                dgvAssignments.Rows[rowIndex].Tag = p.ProblemID;
            }

            lblSummary.Text = $"Tổng số: {_totalRecords} bài tập lập trình";
            dgvAssignments.ClearSelection();
        }

        private void UpdatePaginationUI()
        {
            // Update info label
            int startRecord = _totalRecords > 0 ? (_currentPage - 1) * _pageSize + 1 : 0;
            int endRecord = Math.Min(_currentPage * _pageSize, _totalRecords);
            lblPaginationInfo.Text = $"Hiển thị {startRecord}-{endRecord} trong {_totalRecords} bản ghi";

            // Update navigation buttons
            btnFirstPage.Enabled = _currentPage > 1;
            btnPrevPage.Enabled = _currentPage > 1;
            btnNextPage.Enabled = _currentPage < _totalPages;
            btnLastPage.Enabled = _currentPage < _totalPages;

            // Update button styles
            UpdateButtonStyle(btnFirstPage, btnFirstPage.Enabled);
            UpdateButtonStyle(btnPrevPage, btnPrevPage.Enabled);
            UpdateButtonStyle(btnNextPage, btnNextPage.Enabled);
            UpdateButtonStyle(btnLastPage, btnLastPage.Enabled);

            // Update page number buttons
            CreatePageNumberButtons();
        }

        private void UpdateButtonStyle(Button btn, bool enabled)
        {
            if (enabled)
            {
                btn.BackColor = Color.White;
                btn.ForeColor = Color.FromArgb(71, 85, 105);
                btn.FlatAppearance.BorderColor = Color.FromArgb(226, 232, 240);
            }
            else
            {
                btn.BackColor = Color.FromArgb(248, 250, 252);
                btn.ForeColor = Color.FromArgb(203, 213, 225);
                btn.FlatAppearance.BorderColor = Color.FromArgb(226, 232, 240);
            }
        }

        private void CreatePageNumberButtons()
        {
            // Clear existing buttons
            foreach (var btn in _pageButtons)
            {
                pnlPageNumbers.Controls.Remove(btn);
                btn.Dispose();
            }
            _pageButtons.Clear();

            if (_totalPages == 0)
            {
                pnlPageNumbers.Width = 0;
                // Reposition next buttons
                btnNextPage.Location = new Point(btnPrevPage.Right + 5, btnPrevPage.Top);
                btnLastPage.Location = new Point(btnNextPage.Right + 5, btnNextPage.Top);
                return;
            }

            // Calculate page range to display (max 5 pages)
            int maxVisiblePages = 5;
            int startPage = Math.Max(1, _currentPage - 2);
            int endPage = Math.Min(_totalPages, startPage + maxVisiblePages - 1);

            // Adjust startPage if we're near the end
            if (endPage - startPage < maxVisiblePages - 1)
            {
                startPage = Math.Max(1, endPage - maxVisiblePages + 1);
            }

            // Calculate actual number of pages to display
            int actualPages = endPage - startPage + 1;
            int buttonWidth = 36;
            int buttonSpacing = 5;
            int totalWidth = (buttonWidth * actualPages) + (buttonSpacing * (actualPages - 1));

            // Resize panel to fit buttons exactly (no extra space)
            pnlPageNumbers.Width = totalWidth;
            pnlPageNumbers.Height = 32;

            // Reposition next/last buttons dynamically based on page numbers panel width
            btnNextPage.Location = new Point(pnlPageNumbers.Right + 5, pnlPageNumbers.Top);
            btnLastPage.Location = new Point(btnNextPage.Right + 5, btnNextPage.Top);

            int xPos = 0;
            for (int i = startPage; i <= endPage; i++)
            {
                Button btnPage = new Button();
                btnPage.Size = new Size(buttonWidth, 32);
                btnPage.Location = new Point(xPos, 0);
                btnPage.Text = i.ToString();
                btnPage.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
                btnPage.FlatStyle = FlatStyle.Flat;
                btnPage.Cursor = Cursors.Hand;
                btnPage.Tag = i;

                if (i == _currentPage)
                {
                    // Active page
                    btnPage.BackColor = Color.FromArgb(37, 99, 235);
                    btnPage.ForeColor = Color.White;
                    btnPage.FlatAppearance.BorderColor = Color.FromArgb(37, 99, 235);
                }
                else
                {
                    // Inactive page
                    btnPage.BackColor = Color.White;
                    btnPage.ForeColor = Color.FromArgb(71, 85, 105);
                    btnPage.FlatAppearance.BorderColor = Color.FromArgb(226, 232, 240);
                }

                btnPage.FlatAppearance.BorderSize = 1;
                btnPage.Click += PageButton_Click;

                pnlPageNumbers.Controls.Add(btnPage);
                _pageButtons.Add(btnPage);

                xPos += buttonWidth + buttonSpacing;
            }
        }

        private void PageButton_Click(object sender, EventArgs e)
        {
            if (sender is Button btn && btn.Tag is int pageNumber)
            {
                NavigateToPage(pageNumber);
            }
        }

        private void NavigateToPage(int pageNumber)
        {
            if (pageNumber < 1 || pageNumber > _totalPages || pageNumber == _currentPage)
                return;

            _currentPage = pageNumber;
            LoadPageData();
            UpdatePaginationUI();
        }

        private void CboPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboPageSize.SelectedItem != null)
            {
                _pageSize = int.Parse(cboPageSize.SelectedItem.ToString());
                _currentPage = 1; // Reset to first page
                LoadData(txtSearch.Text == "Tìm kiếm bài tập..." ? "" : txtSearch.Text);
            }
        }

        private void BtnImportWord_Click(object sender, EventArgs e)
        {
            using (var ofd = new OpenFileDialog())
            {
                ofd.Filter = "Word Documents (*.docx)|*.docx|All Files (*.*)|*.*";
                ofd.Title = "Chọn file Word chứa bài lập trình";

                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    ImportWordFile(ofd.FileName);
                }
            }
        }

        private void ImportWordFile(string filePath)
        {
            try
            {
                using (var importForm = new ImportWordForm(_wordImportService))
                {
                    if (importForm.ShowDialog() == DialogResult.OK)
                    {
                        LoadData();
                        MessageBox.Show("Import hoàn tất! Danh sách bài tập đã được cập nhật.",
                            "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi import: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            var form = new AddEditProblemForm(_problemService, _testCaseService);
            if (form.ShowDialog() == DialogResult.OK)
            {
                _currentPage = 1;
                LoadData();
            }
        }

        private void BtnEdit_Click(object sender, EventArgs e)
        {
            if (dgvAssignments.SelectedRows.Count > 0)
            {
                Guid id = (Guid)dgvAssignments.SelectedRows[0].Tag;
                var form = new AddEditProblemForm(_problemService, _testCaseService, id);
                if (form.ShowDialog() == DialogResult.OK)
                    LoadData();
            }
            else
            {
                MessageBox.Show("Vui lòng chọn bài tập cần sửa.", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            List<Guid> idsToDelete = new List<Guid>();

            foreach (DataGridViewRow row in dgvAssignments.Rows)
            {
                if (Convert.ToBoolean(row.Cells["colCheck"].Value))
                {
                    if (row.Tag is Guid id)
                        idsToDelete.Add(id);
                }
            }

            if (idsToDelete.Count == 0)
            {
                if (dgvAssignments.SelectedRows.Count > 0 &&
                    dgvAssignments.SelectedRows[0].Tag is Guid id)
                {
                    idsToDelete.Add(id);
                }
                else
                {
                    MessageBox.Show("Vui lòng chọn ít nhất một bài tập để xóa.",
                        "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }

            if (MessageBox.Show($"Bạn có chắc muốn xóa {idsToDelete.Count} bài tập đã chọn?",
                "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                bool success = true;
                foreach (var id in idsToDelete)
                {
                    if (!_problemService.Delete(id))
                        success = false;
                }

                if (success)
                {
                    MessageBox.Show("Đã xóa thành công.", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadData();
                }
                else
                {
                    MessageBox.Show("Có lỗi xảy ra khi xóa một số bài tập.", "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                Color bgColor = Color.White;
                Color textColor = Color.Black;
                string displayText = diff;

                if (diff == "Dễ" || diff == "Easy")
                {
                    bgColor = Color.FromArgb(220, 252, 231); // Light green
                    textColor = Color.FromArgb(21, 128, 61); // Dark green
                    displayText = "● Dễ";
                }
                else if (diff == "Trung bình" || diff == "Medium")
                {
                    bgColor = Color.FromArgb(254, 243, 199); // Light orange
                    textColor = Color.FromArgb(180, 83, 9); // Dark orange
                    displayText = "● Trung bình";
                }
                else if (diff == "Khó" || diff == "Hard")
                {
                    bgColor = Color.FromArgb(254, 226, 226); // Light red
                    textColor = Color.FromArgb(185, 28, 28); // Dark red
                    displayText = "● Khó";
                }

                e.Paint(e.CellBounds, DataGridViewPaintParts.All & ~DataGridViewPaintParts.ContentForeground);

                // Draw rounded background
                using (SolidBrush brush = new SolidBrush(bgColor))
                {
                    Rectangle rect = new Rectangle(
                        e.CellBounds.X + 12,
                        e.CellBounds.Y + (e.CellBounds.Height - 26) / 2,
                        e.CellBounds.Width - 24,
                        26
                    );
                    e.Graphics.FillRectangle(brush, rect);
                }

                // Draw text
                TextRenderer.DrawText(e.Graphics, displayText, e.CellStyle.Font, e.CellBounds,
                    textColor, TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);

                e.Handled = true;
            }
            else if (e.ColumnIndex == dgvAssignments.Columns["colActions"].Index)
            {
                e.Paint(e.CellBounds, DataGridViewPaintParts.All & ~DataGridViewPaintParts.ContentForeground);

                int centerY = e.CellBounds.Y + (e.CellBounds.Height - ButtonHeight) / 2;
                int centerX = e.CellBounds.X + (e.CellBounds.Width - (ButtonWidth * 2 + ButtonSpacing)) / 2;

                var rectEdit = new Rectangle(centerX, centerY, ButtonWidth, ButtonHeight);
                var rectDel = new Rectangle(rectEdit.Right + ButtonSpacing, centerY, ButtonWidth, ButtonHeight);

                // Edit button
                using (SolidBrush brush = new SolidBrush(Color.FromArgb(37, 99, 235)))
                {
                    e.Graphics.FillRectangle(brush, rectEdit);
                }
                TextRenderer.DrawText(e.Graphics, "✏", new Font("Segoe UI", 11), rectEdit,
                    Color.White, TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);

                // Delete button
                using (SolidBrush brush = new SolidBrush(Color.FromArgb(239, 68, 68)))
                {
                    e.Graphics.FillRectangle(brush, rectDel);
                }
                TextRenderer.DrawText(e.Graphics, "🗑", new Font("Segoe UI Emoji", 10), rectDel,
                    Color.White, TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);

                e.Handled = true;
            }
        }

        private void DgvAssignments_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && e.RowIndex >= 0 &&
                e.ColumnIndex == dgvAssignments.Columns["colActions"].Index)
            {
                int rowHeight = dgvAssignments.Rows[e.RowIndex].Height;
                int centerY = (rowHeight - ButtonHeight) / 2;
                int centerX = (dgvAssignments.Columns["colActions"].Width - (ButtonWidth * 2 + ButtonSpacing)) / 2;

                var rectEdit = new Rectangle(centerX, centerY, ButtonWidth, ButtonHeight);
                var rectDel = new Rectangle(rectEdit.Right + ButtonSpacing, centerY, ButtonWidth, ButtonHeight);

                Guid problemId = (Guid)dgvAssignments.Rows[e.RowIndex].Tag;
                string title = dgvAssignments.Rows[e.RowIndex].Cells["colName"].Value.ToString();

                if (rectEdit.Contains(e.Location))
                {
                    var form = new AddEditProblemForm(_problemService, _testCaseService, problemId);
                    if (form.ShowDialog() == DialogResult.OK)
                        LoadData();
                }
                else if (rectDel.Contains(e.Location))
                {
                    if (MessageBox.Show($"Xóa bài tập: {title}?", "Xác nhận",
                        MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                    {
                        if (_problemService.Delete(problemId))
                        {
                            LoadData();
                        }
                        else
                        {
                            MessageBox.Show("Xóa thất bại.", "Lỗi",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
        }
    }
}