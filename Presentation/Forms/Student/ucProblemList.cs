using CodeForge_Desktop.Business.Interfaces;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;
using CodeForge_Desktop.Business.Services;

namespace CodeForge_Desktop.Presentation.Forms.Student
{
    public partial class ucProblemList : UserControl
    {
        private const string PlaceholderText = "Tìm kiếm bài tập...";
        private const int ItemsPerPage = 5;
        private const int MaxVisiblePageButtons = 5;

        public event EventHandler<Guid> ProblemClicked;
        private ICodingProblemService _problemService;
        private List<CodeForge_Desktop.DataAccess.Entities.CodingProblem> _allProblems;
        private List<CodeForge_Desktop.DataAccess.Entities.CodingProblem> _filteredProblems;
        private string _currentSearchText = "";
        private string _currentDifficultyFilter = "Tất cả";
        private int _lastHoveredRow = -1;

        // Pagination
        private int _currentPage = 1;
        private int _totalPages = 1;
        private List<Button> _pageButtons = new List<Button>();

        // Fonts
        private Font _headerFont;
        private Font _cellFont;
        private Font _cellBoldFont;

        // Color Palette
        private readonly Color _slateHeader = Color.FromArgb(51, 65, 85);
        private readonly Color _slateText = Color.FromArgb(51, 65, 85);
        private readonly Color _slateSecondary = Color.FromArgb(71, 85, 105);
        private readonly Color _slateTertiary = Color.FromArgb(100, 116, 139);
        private readonly Color _slateLight = Color.FromArgb(148, 163, 184);
        private readonly Color _slateBorder = Color.FromArgb(203, 213, 225);
        private readonly Color _slateHover = Color.FromArgb(248, 250, 252);
        private readonly Color _linkBlue = Color.FromArgb(59, 130, 246);

        public ucProblemList()
        {
            _problemService = new CodingProblemService();
            _filteredProblems = new List<CodeForge_Desktop.DataAccess.Entities.CodingProblem>();
            _allProblems = new List<CodeForge_Desktop.DataAccess.Entities.CodingProblem>();

            _headerFont = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            _cellFont = new Font("Segoe UI", 9.5F);
            _cellBoldFont = new Font("Segoe UI Semibold", 9.5F, FontStyle.Bold);

            InitializeComponent();
            SetupCustomStyles();
            SetupPlaceholder();
            SetupSearchBox();
            LoadDataFromDatabase();

            // Enable double buffering
            typeof(DataGridView).InvokeMember("DoubleBuffered",
                System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.SetProperty,
                null, dgvProblemList, new object[] { true });

            dgvProblemList.CellClick += DgvProblemList_CellClick;
            dgvProblemList.MouseMove += DgvProblemList_MouseMove;
            dgvProblemList.MouseLeave += DgvProblemList_MouseLeave;
            cmbDifficulty.SelectedIndexChanged += CmbDifficulty_SelectedIndexChanged;
        }

        private void SetupCustomStyles()
        {
            // Rounded corners for search box
            pnlSearchContainer.Paint += (s, e) =>
            {
                DrawRoundedPanel(e.Graphics, pnlSearchContainer.ClientRectangle,
                    Color.FromArgb(248, 250, 252), 6);
            };

            // Rounded corners for stat badges - với text hiển thị rõ ràng
            lblTotal.Paint += (s, e) => DrawStatBadgeWithText(e.Graphics, lblTotal);
            lblSolved.Paint += (s, e) => DrawStatBadgeWithText(e.Graphics, lblSolved);
            lblAttempted.Paint += (s, e) => DrawStatBadgeWithText(e.Graphics, lblAttempted);
            lblNotStarted.Paint += (s, e) => DrawStatBadgeWithText(e.Graphics, lblNotStarted);
        }

        private void DrawRoundedPanel(Graphics g, Rectangle bounds, Color color, int radius)
        {
            g.SmoothingMode = SmoothingMode.AntiAlias;
            using (GraphicsPath path = GetRoundedRectPath(bounds, radius))
            using (SolidBrush brush = new SolidBrush(color))
            {
                g.FillPath(brush, path);
            }
        }

        private void DrawStatBadge(Graphics g, Label label)
        {
            g.SmoothingMode = SmoothingMode.AntiAlias;
            Rectangle bounds = new Rectangle(0, 0, label.Width, label.Height);
            using (GraphicsPath path = GetRoundedRectPath(bounds, 6))
            using (SolidBrush brush = new SolidBrush(label.BackColor))
            {
                g.FillPath(brush, path);
            }
        }

        private void DrawStatBadgeWithText(Graphics g, Label label)
        {
            g.SmoothingMode = SmoothingMode.AntiAlias;
            Rectangle bounds = new Rectangle(0, 0, label.Width, label.Height);

            // Vẽ background bo góc
            using (GraphicsPath path = GetRoundedRectPath(bounds, 6))
            using (SolidBrush bgBrush = new SolidBrush(label.BackColor))
            {
                g.FillPath(bgBrush, path);
            }

            // Vẽ text lên trên
            using (SolidBrush textBrush = new SolidBrush(label.ForeColor))
            {
                StringFormat sf = new StringFormat
                {
                    Alignment = StringAlignment.Center,
                    LineAlignment = StringAlignment.Center
                };
                g.DrawString(label.Text, label.Font, textBrush, bounds, sf);
            }
        }

        private GraphicsPath GetRoundedRectPath(Rectangle bounds, int radius)
        {
            int diameter = radius * 2;
            Size size = new Size(diameter, diameter);
            Rectangle arc = new Rectangle(bounds.Location, size);
            GraphicsPath path = new GraphicsPath();

            if (radius == 0)
            {
                path.AddRectangle(bounds);
                return path;
            }

            path.AddArc(arc, 180, 90);
            arc.X = bounds.Right - diameter;
            path.AddArc(arc, 270, 90);
            arc.Y = bounds.Bottom - diameter;
            path.AddArc(arc, 0, 90);
            arc.X = bounds.Left;
            path.AddArc(arc, 90, 90);
            path.CloseFigure();
            return path;
        }

        private void DgvProblemList_MouseMove(object sender, MouseEventArgs e)
        {
            DataGridView.HitTestInfo hit = dgvProblemList.HitTest(e.X, e.Y);

            if (hit.RowIndex != _lastHoveredRow)
            {
                if (_lastHoveredRow >= 0 && _lastHoveredRow < dgvProblemList.Rows.Count)
                {
                    dgvProblemList.Rows[_lastHoveredRow].DefaultCellStyle.BackColor = Color.White;
                }

                _lastHoveredRow = hit.RowIndex;

                if (hit.RowIndex >= 0 && hit.RowIndex < dgvProblemList.Rows.Count)
                {
                    dgvProblemList.Rows[hit.RowIndex].DefaultCellStyle.BackColor = _slateHover;
                    dgvProblemList.Cursor = Cursors.Hand;
                }
                else
                {
                    dgvProblemList.Cursor = Cursors.Default;
                }
            }
        }

        private void DgvProblemList_MouseLeave(object sender, EventArgs e)
        {
            if (_lastHoveredRow >= 0 && _lastHoveredRow < dgvProblemList.Rows.Count)
            {
                dgvProblemList.Rows[_lastHoveredRow].DefaultCellStyle.BackColor = Color.White;
                _lastHoveredRow = -1;
            }
            dgvProblemList.Cursor = Cursors.Default;
        }

        private void CmbDifficulty_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedDifficulty = cmbDifficulty.SelectedItem?.ToString();
            _currentDifficultyFilter = selectedDifficulty ?? "Tất cả";
            _currentPage = 1; // Reset to first page
            ApplyCombinedFilters();
        }

        /// <summary>
        /// Áp dụng filter và phân trang
        /// </summary>
        private void ApplyCombinedFilters()
        {
            try
            {
                if (_allProblems == null) return;

                // Apply filters
                var filtered = _allProblems.Where(p =>
                {
                    if (_currentDifficultyFilter != "Tất cả" && p.Difficulty != _currentDifficultyFilter)
                        return false;

                    if (!string.IsNullOrWhiteSpace(_currentSearchText))
                    {
                        return p.Title.IndexOf(_currentSearchText, StringComparison.OrdinalIgnoreCase) >= 0 ||
                               (p.Tags?.IndexOf(_currentSearchText, StringComparison.OrdinalIgnoreCase) >= 0);
                    }

                    return true;
                }).ToList();

                _filteredProblems = filtered;

                // Calculate pagination
                _totalPages = (int)Math.Ceiling((double)_filteredProblems.Count / ItemsPerPage);
                if (_totalPages == 0) _totalPages = 1;
                if (_currentPage > _totalPages) _currentPage = _totalPages;

                // Update display
                DisplayCurrentPage();
                UpdateStatistics(_filteredProblems);
                UpdatePaginationControls();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi lọc dữ liệu: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Hiển thị dữ liệu trang hiện tại
        /// </summary>
        private void DisplayCurrentPage()
        {
            dgvProblemList.SuspendLayout();
            dgvProblemList.Rows.Clear();

            int startIndex = (_currentPage - 1) * ItemsPerPage;
            int endIndex = Math.Min(startIndex + ItemsPerPage, _filteredProblems.Count);

            for (int i = startIndex; i < endIndex; i++)
            {
                var problem = _filteredProblems[i];
                dgvProblemList.Rows.Add(
                    problem.ProblemID.ToString(),
                    problem.Title,
                    problem.Difficulty,
                    problem.Tags ?? "",
                    problem.Status
                );
            }

            dgvProblemList.ResumeLayout();
        }

        /// <summary>
        /// Tạo các nút phân trang động
        /// </summary>
        private void CreatePageButtons()
        {
            // Xóa các nút cũ
            foreach (var btn in _pageButtons)
            {
                pnlPagination.Controls.Remove(btn);
                btn.Dispose();
            }
            _pageButtons.Clear();

            // Tính toán các trang cần hiển thị
            int startPage, endPage;

            if (_totalPages <= MaxVisiblePageButtons)
            {
                startPage = 1;
                endPage = _totalPages;
            }
            else
            {
                // Hiển thị 5 trang: current page ở giữa khi có thể
                int pagesBeforeCurrent = MaxVisiblePageButtons / 2;
                int pagesAfterCurrent = MaxVisiblePageButtons - pagesBeforeCurrent - 1;

                startPage = Math.Max(1, _currentPage - pagesBeforeCurrent);
                endPage = Math.Min(_totalPages, _currentPage + pagesAfterCurrent);

                // Điều chỉnh nếu chưa đủ 5 trang
                if (endPage - startPage + 1 < MaxVisiblePageButtons)
                {
                    if (startPage == 1)
                    {
                        endPage = Math.Min(_totalPages, startPage + MaxVisiblePageButtons - 1);
                    }
                    else if (endPage == _totalPages)
                    {
                        startPage = Math.Max(1, endPage - MaxVisiblePageButtons + 1);
                    }
                }
            }

            // Tính số lượng nút cần tạo
            int totalPageButtons = endPage - startPage + 1;

            // Vị trí của nút Last (cố định ở bên phải)
            int lastBtnX = pnlPagination.Width - 20 - 42; // 20 padding + 42 width

            // Đặt lại vị trí cho btnLastPage và btnNextPage
            btnLastPage.Location = new Point(lastBtnX, 14);
            btnNextPage.Location = new Point(lastBtnX - 48, 14); // 48 = 42 + 6

            // Vị trí bắt đầu của các nút số trang (tính từ bên phải btnNextPage)
            int startX = btnNextPage.Location.X - (totalPageButtons * 48);

            // Đặt lại vị trí cho btnPrevPage và btnFirstPage
            btnPrevPage.Location = new Point(startX - 48, 14);
            btnFirstPage.Location = new Point(startX - 96, 14); // 96 = 48 * 2

            // Tạo các nút số trang từ trái sang phải
            int buttonX = startX;
            for (int i = startPage; i <= endPage; i++)
            {
                Button pageBtn = CreatePageButton(i);
                pageBtn.Location = new Point(buttonX, 14);
                pageBtn.Anchor = AnchorStyles.Top | AnchorStyles.Right;
                pnlPagination.Controls.Add(pageBtn);
                _pageButtons.Add(pageBtn);
                buttonX += 48;
            }
        }

        /// <summary>
        /// Tạo một nút số trang
        /// </summary>
        private Button CreatePageButton(int pageNumber)
        {
            Button btn = new Button
            {
                Size = new Size(42, 32),
                Text = pageNumber.ToString(),
                Cursor = Cursors.Hand,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold),
                Tag = pageNumber
            };

            // Style dựa theo trang hiện tại
            if (pageNumber == _currentPage)
            {
                btn.BackColor = _linkBlue;
                btn.ForeColor = Color.White;
                btn.FlatAppearance.BorderColor = _linkBlue;
            }
            else
            {
                btn.BackColor = Color.White;
                btn.ForeColor = _slateSecondary;
                btn.FlatAppearance.BorderColor = _slateBorder;
            }

            // Event handler
            btn.Click += (s, e) =>
            {
                _currentPage = pageNumber;
                DisplayCurrentPage();
                UpdatePaginationControls();
            };

            // Hover effect cho nút không phải current page
            if (pageNumber != _currentPage)
            {
                btn.MouseEnter += (s, e) =>
                {
                    btn.BackColor = _slateHover;
                };
                btn.MouseLeave += (s, e) =>
                {
                    btn.BackColor = Color.White;
                };
            }

            return btn;
        }

        /// <summary>
        /// Cập nhật controls phân trang
        /// </summary>
        private void UpdatePaginationControls()
        {
            // Tạo lại các nút số trang
            CreatePageButtons();

            // Cập nhật thông tin hiển thị
            int startItem = _filteredProblems.Count == 0 ? 0 : ((_currentPage - 1) * ItemsPerPage + 1);
            int endItem = Math.Min(_currentPage * ItemsPerPage, _filteredProblems.Count);

            lblPageInfo.Text = $"Hiển thị {startItem}-{endItem} trong {_filteredProblems.Count} bài";

            // Enable/disable buttons
            bool canGoPrev = _currentPage > 1;
            bool canGoNext = _currentPage < _totalPages;

            btnFirstPage.Enabled = canGoPrev;
            btnPrevPage.Enabled = canGoPrev;
            btnNextPage.Enabled = canGoNext;
            btnLastPage.Enabled = canGoNext;

            // Update button styles
            UpdateButtonStyle(btnFirstPage, canGoPrev);
            UpdateButtonStyle(btnPrevPage, canGoPrev);
            UpdateButtonStyle(btnNextPage, canGoNext);
            UpdateButtonStyle(btnLastPage, canGoNext);

            // Thay đổi cursor
            btnFirstPage.Cursor = canGoPrev ? Cursors.Hand : Cursors.Default;
            btnPrevPage.Cursor = canGoPrev ? Cursors.Hand : Cursors.Default;
            btnNextPage.Cursor = canGoNext ? Cursors.Hand : Cursors.Default;
            btnLastPage.Cursor = canGoNext ? Cursors.Hand : Cursors.Default;
        }

        private void UpdateButtonStyle(Button btn, bool enabled)
        {
            if (enabled)
            {
                btn.ForeColor = _slateSecondary;
                btn.FlatAppearance.BorderColor = _slateBorder;
                btn.BackColor = Color.White;
            }
            else
            {
                btn.ForeColor = _slateLight;
                btn.FlatAppearance.BorderColor = Color.FromArgb(226, 232, 240);
                btn.BackColor = Color.FromArgb(249, 250, 251);
            }
        }

        /// <summary>
        /// Pagination button handlers
        /// </summary>
        private void btnFirstPage_Click(object sender, EventArgs e)
        {
            if (_currentPage > 1)
            {
                _currentPage = 1;
                DisplayCurrentPage();
                UpdatePaginationControls();
            }
        }

        private void btnPrevPage_Click(object sender, EventArgs e)
        {
            if (_currentPage > 1)
            {
                _currentPage--;
                DisplayCurrentPage();
                UpdatePaginationControls();
            }
        }

        private void btnNextPage_Click(object sender, EventArgs e)
        {
            if (_currentPage < _totalPages)
            {
                _currentPage++;
                DisplayCurrentPage();
                UpdatePaginationControls();
            }
        }

        private void btnLastPage_Click(object sender, EventArgs e)
        {
            if (_currentPage < _totalPages)
            {
                _currentPage = _totalPages;
                DisplayCurrentPage();
                UpdatePaginationControls();
            }
        }

        private void UpdateStatistics(List<CodeForge_Desktop.DataAccess.Entities.CodingProblem> problems)
        {
            if (problems == null || problems.Count == 0)
            {
                lblTotal.Text = "Tổng: 0";
                lblSolved.Text = "Xong: 0";
                lblAttempted.Text = "Đang: 0";
                lblNotStarted.Text = "Chưa: 0";
                return;
            }

            int total = problems.Count;
            int solved = problems.Count(p => p.Status == "SOLVED");
            int attempted = problems.Count(p => p.Status == "ATTEMPTED");
            int notStarted = problems.Count(p => p.Status == "NOT_STARTED");

            lblTotal.Text = $"Tổng: {total}";
            lblSolved.Text = $"Xong: {solved}";
            lblAttempted.Text = $"Đang: {attempted}";
            lblNotStarted.Text = $"Chưa: {notStarted}";
        }

        private void LoadDataFromDatabase()
        {
            try
            {
                _allProblems = _problemService.GetAll();

                if (_allProblems == null)
                {
                    _allProblems = new List<CodeForge_Desktop.DataAccess.Entities.CodingProblem>();
                }

                _filteredProblems = new List<CodeForge_Desktop.DataAccess.Entities.CodingProblem>(_allProblems);
                _currentPage = 1;

                // Calculate pagination
                _totalPages = (int)Math.Ceiling((double)_filteredProblems.Count / ItemsPerPage);
                if (_totalPages == 0) _totalPages = 1;

                DisplayCurrentPage();
                UpdateStatistics(_allProblems);
                UpdatePaginationControls();

                cmbDifficulty.SelectedIndex = 0;
                _currentDifficultyFilter = "Tất cả";
                _currentSearchText = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải dữ liệu: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvProblemList_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex < 0 || e.Value == null) return;

            try
            {
                // Status column
                if (e.ColumnIndex == dgvProblemList.Columns["colStatus"].Index)
                {
                    e.Paint(e.CellBounds, DataGridViewPaintParts.All & ~DataGridViewPaintParts.ContentForeground);

                    string status = e.Value.ToString();
                    Color bgColor, fgColor;
                    string displayText;

                    switch (status)
                    {
                        case "SOLVED":
                            bgColor = Color.FromArgb(209, 250, 229);
                            fgColor = Color.FromArgb(6, 95, 70);
                            displayText = "✓ Đã làm";
                            break;
                        case "ATTEMPTED":
                            bgColor = Color.FromArgb(254, 243, 199);
                            fgColor = Color.FromArgb(180, 83, 9);
                            displayText = "◐ Đang làm";
                            break;
                        default:
                            bgColor = Color.FromArgb(226, 232, 240);
                            fgColor = Color.FromArgb(71, 85, 105);
                            displayText = "○ Chưa làm";
                            break;
                    }

                    Rectangle badgeRect = new Rectangle(
                        e.CellBounds.X + 16,
                        e.CellBounds.Y + (e.CellBounds.Height - 28) / 2,
                        110,
                        28
                    );

                    e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                    using (GraphicsPath path = GetRoundedRectPath(badgeRect, 6))
                    using (SolidBrush brush = new SolidBrush(bgColor))
                    {
                        e.Graphics.FillPath(brush, path);
                    }

                    TextRenderer.DrawText(e.Graphics, displayText, _cellFont, badgeRect,
                        fgColor, TextFormatFlags.VerticalCenter | TextFormatFlags.HorizontalCenter);

                    e.Handled = true;
                }
                // Problem name
                else if (e.ColumnIndex == dgvProblemList.Columns["colProblemName"].Index)
                {
                    e.Paint(e.CellBounds, DataGridViewPaintParts.All & ~DataGridViewPaintParts.ContentForeground);

                    TextRenderer.DrawText(e.Graphics, e.Value.ToString(), _cellFont,
                        e.CellBounds, _linkBlue,
                        TextFormatFlags.VerticalCenter | TextFormatFlags.Left);

                    e.Handled = true;
                }
                // Difficulty
                else if (e.ColumnIndex == dgvProblemList.Columns["colDifficulty"].Index)
                {
                    e.Paint(e.CellBounds, DataGridViewPaintParts.All & ~DataGridViewPaintParts.ContentForeground);

                    string difficulty = e.Value.ToString();
                    Color diffColor = difficulty switch
                    {
                        "Dễ" => Color.FromArgb(34, 197, 94),
                        "Trung bình" => Color.FromArgb(251, 146, 60),
                        "Khó" => Color.FromArgb(239, 68, 68),
                        _ => _slateSecondary
                    };

                    TextRenderer.DrawText(e.Graphics, difficulty, _cellBoldFont,
                        e.CellBounds, diffColor,
                        TextFormatFlags.VerticalCenter | TextFormatFlags.Left);

                    e.Handled = true;
                }
                // Tags
                else if (e.ColumnIndex == dgvProblemList.Columns["TagProblem"].Index)
                {
                    e.Paint(e.CellBounds, DataGridViewPaintParts.All & ~DataGridViewPaintParts.ContentForeground);

                    TextRenderer.DrawText(e.Graphics, e.Value.ToString(), _cellFont,
                        e.CellBounds, _slateTertiary,
                        TextFormatFlags.VerticalCenter | TextFormatFlags.Left);

                    e.Handled = true;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error in CellPainting: {ex.Message}");
            }
        }

        private void SetupPlaceholder()
        {
            txtSearch.Text = PlaceholderText;
            txtSearch.ForeColor = _slateLight;
            txtSearch.GotFocus += RemovePlaceholder;
            txtSearch.LostFocus += AddPlaceholder;
        }

        private void RemovePlaceholder(object sender, EventArgs e)
        {
            if (txtSearch.Text == PlaceholderText)
            {
                txtSearch.Text = "";
                txtSearch.ForeColor = _slateSecondary;
            }
        }

        private void AddPlaceholder(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtSearch.Text))
            {
                txtSearch.Text = PlaceholderText;
                txtSearch.ForeColor = _slateLight;
            }
        }

        private void SetupSearchBox()
        {
            System.Windows.Forms.Timer searchTimer = new System.Windows.Forms.Timer();
            searchTimer.Interval = 300;
            searchTimer.Tick += (s, e) =>
            {
                searchTimer.Stop();
                _currentPage = 1; // Reset to first page on search
                ApplyCombinedFilters();
            };

            txtSearch.TextChanged += (s, e) =>
            {
                string searchText = txtSearch.Text.Trim();
                _currentSearchText = (searchText == PlaceholderText || string.IsNullOrWhiteSpace(searchText))
                    ? "" : searchText;

                searchTimer.Stop();
                searchTimer.Start();
            };
        }

        private void DgvProblemList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                var cellValue = dgvProblemList.Rows[e.RowIndex].Cells["colHash"].Value;

                if (cellValue != null && Guid.TryParse(cellValue.ToString(), out Guid problemId))
                {
                    ProblemClicked?.Invoke(this, problemId);
                }
            }
        }

        private void dgvProblemList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            DgvProblemList_CellClick(sender, e);
        }
    }
}