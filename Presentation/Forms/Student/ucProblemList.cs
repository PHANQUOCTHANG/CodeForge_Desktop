using CodeForge_Desktop.Business.Interfaces;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using CodeForge_Desktop.Business.Services;

namespace CodeForge_Desktop.Presentation.Forms.Student
{
    public partial class ucProblemList : UserControl
    {
        private const string PlaceholderText = "Tìm kiếm bài tập...";
        public event EventHandler<Guid> ProblemClicked;
        private ICodingProblemService _problemService;
        private List<CodeForge_Desktop.DataAccess.Entities.CodingProblem> _allProblems;
        private List<CodeForge_Desktop.DataAccess.Entities.CodingProblem> _displayedProblems;
        private string _currentSearchText = "";
        private string _currentDifficultyFilter = "Tất cả";

        public ucProblemList()
        {
            _problemService = new CodingProblemService();
            _displayedProblems = new List<CodeForge_Desktop.DataAccess.Entities.CodingProblem>();

            InitializeComponent();
            SetupPlaceholder();
            SetupSearchBox();
            SetupDataGridViewStyles();
            LoadDataFromDatabase();

            typeof(DataGridView).InvokeMember("DoubleBuffered",
                System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.SetProperty,
                null, dgvProblemList, new object[] { true });

            dgvProblemList.CellPainting += DgvProblemList_CellPainting;
            dgvProblemList.CellClick += DgvProblemList_CellClick;
            dgvProblemList.MouseMove += DgvProblemList_MouseMove;
            cmbDifficulty.SelectedIndexChanged += CmbDifficulty_SelectedIndexChanged;
        }

        private int _lastHoveredRow = -1;

        /// <summary>
        /// Xử lý hover effect cho rows
        /// </summary>
        private void DgvProblemList_MouseMove(object sender, MouseEventArgs e)
        {
            DataGridView.HitTestInfo hit = dgvProblemList.HitTest(e.X, e.Y);
            if (hit.RowIndex != _lastHoveredRow)
            {
                if (_lastHoveredRow >= 0 && _lastHoveredRow < dgvProblemList.Rows.Count)
                {
                    dgvProblemList.Rows[_lastHoveredRow].DefaultCellStyle.BackColor = 
                        _lastHoveredRow % 2 == 0 ? Color.White : Color.FromArgb(247, 249, 252);
                }

                _lastHoveredRow = hit.RowIndex;

                if (hit.RowIndex >= 0 && hit.RowIndex < dgvProblemList.Rows.Count)
                {
                    dgvProblemList.Rows[hit.RowIndex].DefaultCellStyle.BackColor = Color.FromArgb(230, 242, 255);
                    dgvProblemList.Cursor = Cursors.Hand;
                }
                else
                {
                    dgvProblemList.Cursor = Cursors.Default;
                }
            }
        }

        /// <summary>
        /// Xử lý thay đổi filter độ khó
        /// </summary>
        private void CmbDifficulty_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedDifficulty = cmbDifficulty.SelectedItem?.ToString();
            
            _currentDifficultyFilter = selectedDifficulty ?? "Tất cả";
            
            // Áp dụng cả search và filter
            ApplyCombinedFilters();
        }

        /// <summary>
        /// Áp dụng kết hợp cả tìm kiếm và filter độ khó
        /// </summary>
        private void ApplyCombinedFilters()
        {
            try
            {
                dgvProblemList.Rows.Clear();

                var filteredProblems = _allProblems.Where(p =>
                {
                    // Filter độ khó
                    if (_currentDifficultyFilter != "Tất cả" && p.Difficulty != _currentDifficultyFilter)
                        return false;

                    // Filter tìm kiếm - chỉ cần xuất hiện trong tên (case-insensitive)
                    if (!string.IsNullOrWhiteSpace(_currentSearchText))
                    {
                        return p.Title.IndexOf(_currentSearchText, StringComparison.OrdinalIgnoreCase) >= 0;
                    }

                    return true;
                }).ToList();

                _displayedProblems = filteredProblems;

                foreach (var problem in filteredProblems)
                {
                    dgvProblemList.Rows.Add(
                        problem.ProblemID.ToString(),
                        problem.Title,
                        problem.Difficulty,
                        problem.Tags,
                        "WAIT"
                    );
                }

                // Cập nhật summary text
                string summary = $"📊 Hiển thị {filteredProblems.Count} bài tập";
                
                if (!string.IsNullOrWhiteSpace(_currentSearchText))
                {
                    summary += $" (Tìm: '{_currentSearchText}')";
                }
                
                if (_currentDifficultyFilter != "Tất cả")
                {
                    summary += $" (Độ khó: {_currentDifficultyFilter})";
                }

                lblSummary.Text = summary;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi áp dụng filter: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Lấy dữ liệu từ database và đổ vào DataGridView
        /// </summary>
        private void LoadDataFromDatabase()
        {
            try
            {
                dgvProblemList.Rows.Clear();
                _allProblems = _problemService.GetAll();
                _displayedProblems = new List<CodeForge_Desktop.DataAccess.Entities.CodingProblem>(_allProblems);

                foreach (var problem in _allProblems)
                {
                    dgvProblemList.Rows.Add(
                        problem.ProblemID.ToString(),
                        problem.Title,
                        problem.Difficulty,
                        problem.Tags,
                        "WAIT"
                    );
                }

                lblSummary.Text = $"📊 Tổng cộng {_allProblems.Count} bài tập";
                cmbDifficulty.SelectedIndex = 0;
                _currentDifficultyFilter = "Tất cả";
                _currentSearchText = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Tùy chỉnh giao diện DataGridView
        /// </summary>
        private void SetupDataGridViewStyles()
        {
            dgvProblemList.EnableHeadersVisualStyles = false;
            dgvProblemList.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(52, 73, 94);
            dgvProblemList.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvProblemList.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            dgvProblemList.ColumnHeadersDefaultCellStyle.Padding = new Padding(0, 8, 0, 8);
            dgvProblemList.ColumnHeadersHeight = 40;

            dgvProblemList.BorderStyle = BorderStyle.None;
            dgvProblemList.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgvProblemList.GridColor = Color.FromArgb(220, 220, 220);
            dgvProblemList.DefaultCellStyle.Font = new Font("Segoe UI", 10F);
            dgvProblemList.DefaultCellStyle.ForeColor = Color.FromArgb(64, 64, 64);
            dgvProblemList.DefaultCellStyle.Padding = new Padding(5, 3, 5, 3);

            dgvProblemList.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvProblemList.MultiSelect = false;

            dgvProblemList.DefaultCellStyle.SelectionBackColor = Color.White;
            dgvProblemList.DefaultCellStyle.SelectionForeColor = Color.FromArgb(64, 64, 64);

            dgvProblemList.RowsDefaultCellStyle.BackColor = Color.White;
            dgvProblemList.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(247, 249, 252);
            dgvProblemList.RowTemplate.Height = 40;
        }

        /// <summary>
        /// Tô màu cho các ô theo trạng thái
        /// </summary>
        private void DgvProblemList_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex < 0) return;

            if (e.ColumnIndex == dgvProblemList.Columns["colDifficulty"].Index && e.Value != null)
            {
                string difficulty = e.Value.ToString();
                e.Paint(e.CellBounds, DataGridViewPaintParts.All & ~DataGridViewPaintParts.ContentForeground);
                TextRenderer.DrawText(e.Graphics, difficulty, new Font("Segoe UI", 10), e.CellBounds, Color.FromArgb(64, 64, 64), TextFormatFlags.VerticalCenter | TextFormatFlags.Left | TextFormatFlags.NoPadding);
                e.Handled = true;
            }

            if (e.ColumnIndex == dgvProblemList.Columns["colProblemName"].Index && e.Value != null)
            {
                string problemName = e.Value.ToString();
                e.Paint(e.CellBounds, DataGridViewPaintParts.All & ~DataGridViewPaintParts.ContentForeground);
                var textFont = new Font("Segoe UI", 10, FontStyle.Regular);
                TextRenderer.DrawText(e.Graphics, problemName, textFont, e.CellBounds, Color.FromArgb(41, 128, 185), TextFormatFlags.VerticalCenter | TextFormatFlags.Left | TextFormatFlags.NoPadding);
                e.Handled = true;
            }

            if (e.ColumnIndex == dgvProblemList.Columns["colStatus"].Index && e.Value != null)
            {
                string status = e.Value.ToString();
                Color textColor = Color.FromArgb(155, 89, 182);
                string icon = "⏳ ";

                switch (status)
                {
                    case "SOLVED":
                        textColor = Color.FromArgb(46, 204, 113);
                        icon = "✓ ";
                        break;
                    case "ATTEMPTED":
                        textColor = Color.FromArgb(241, 196, 15);
                        icon = "◐ ";
                        break;
                    case "WAIT":
                    default:
                        textColor = Color.FromArgb(149, 165, 166);
                        break;
                }

                e.Paint(e.CellBounds, DataGridViewPaintParts.All & ~DataGridViewPaintParts.ContentForeground);
                TextRenderer.DrawText(e.Graphics, icon + status, new Font("Segoe UI", 9), e.CellBounds, textColor, TextFormatFlags.VerticalCenter | TextFormatFlags.Left);
                e.Handled = true;
            }
        }

        private void SetupSearchBox()
        {
            int paddingTop = (pnlSearchContainer.Height - txtSearch.Font.Height) / 2;
            if (paddingTop > 0)
            {
                txtSearch.Padding = new Padding(0, paddingTop, 0, 0);
            }

            txtSearch.TextChanged += (s, e) =>
            {
                string searchText = txtSearch.Text.Trim();
                
                // Nếu text là placeholder hoặc rỗng
                if (searchText == PlaceholderText || string.IsNullOrWhiteSpace(searchText))
                {
                    _currentSearchText = "";
                }
                else
                {
                    _currentSearchText = searchText;
                }

                // Áp dụng kết hợp search + filter
                ApplyCombinedFilters();
            };
        }

        private void SetupPlaceholder()
        {
            txtSearch.Text = PlaceholderText;
            txtSearch.ForeColor = System.Drawing.Color.Gray;

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

        private void DgvProblemList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                string problemIdStr = dgvProblemList.Rows[e.RowIndex].Cells["colHash"].Value?.ToString();
                
                if (Guid.TryParse(problemIdStr, out Guid problemId))
                {
                    ProblemClicked?.Invoke(this, problemId);
                }
                else
                {
                    MessageBox.Show("Lỗi: Không tìm thấy ID bài tập", "Lỗi");
                }
            }
        }
    }
}