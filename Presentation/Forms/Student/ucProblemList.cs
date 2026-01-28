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
        public event EventHandler<Guid> ProblemClicked;
        private ICodingProblemService _problemService;
        private List<CodeForge_Desktop.DataAccess.Entities.CodingProblem> _allProblems;
        private List<CodeForge_Desktop.DataAccess.Entities.CodingProblem> _displayedProblems;
        private string _currentSearchText = "";
        private string _currentDifficultyFilter = "Tất cả";
        private int _lastHoveredRow = -1;
        private Font _headerFont;
        private Font _cellFont;
        private Font _cellBoldFont;

        // Color Palette - Slate Theme
        private readonly Color _slateHeader = Color.FromArgb(51, 65, 85);
        private readonly Color _slateText = Color.FromArgb(51, 65, 85);
        private readonly Color _slateSecondary = Color.FromArgb(71, 85, 105);
        private readonly Color _slateTertiary = Color.FromArgb(100, 116, 139);
        private readonly Color _slateLight = Color.FromArgb(148, 163, 184);
        private readonly Color _slateBorder = Color.FromArgb(226, 232, 240);
        private readonly Color _slateHover = Color.FromArgb(248, 250, 252);
        private readonly Color _linkBlue = Color.FromArgb(59, 130, 246);

        public ucProblemList()
        {
            _problemService = new CodingProblemService();
            _displayedProblems = new List<CodeForge_Desktop.DataAccess.Entities.CodingProblem>();
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

        /// <summary>
        /// Thiết lập style tùy chỉnh cho các controls
        /// </summary>
        private void SetupCustomStyles()
        {
            // Rounded corners for search box
            pnlSearchContainer.Paint += (s, e) =>
            {
                DrawRoundedPanel(e.Graphics, pnlSearchContainer.ClientRectangle,
                    Color.FromArgb(248, 250, 252), 6);
            };

            // Rounded corners for stat badges
            lblTotal.Paint += (s, e) => DrawStatBadge(e.Graphics, lblTotal);
            lblSolved.Paint += (s, e) => DrawStatBadge(e.Graphics, lblSolved);
            lblAttempted.Paint += (s, e) => DrawStatBadge(e.Graphics, lblAttempted);
            lblNotStarted.Paint += (s, e) => DrawStatBadge(e.Graphics, lblNotStarted);
        }

        /// <summary>
        /// Vẽ panel bo tròn góc
        /// </summary>
        private void DrawRoundedPanel(Graphics g, Rectangle bounds, Color color, int radius)
        {
            g.SmoothingMode = SmoothingMode.AntiAlias;
            using (GraphicsPath path = GetRoundedRectPath(bounds, radius))
            using (SolidBrush brush = new SolidBrush(color))
            {
                g.FillPath(brush, path);
            }
        }

        /// <summary>
        /// Vẽ stat badge với góc bo tròn
        /// </summary>
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

        /// <summary>
        /// Tạo path cho hình chữ nhật bo góc
        /// </summary>
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

            // Top left arc
            path.AddArc(arc, 180, 90);

            // Top right arc
            arc.X = bounds.Right - diameter;
            path.AddArc(arc, 270, 90);

            // Bottom right arc
            arc.Y = bounds.Bottom - diameter;
            path.AddArc(arc, 0, 90);

            // Bottom left arc
            arc.X = bounds.Left;
            path.AddArc(arc, 90, 90);

            path.CloseFigure();
            return path;
        }

        /// <summary>
        /// Xử lý hover effect
        /// </summary>
        private void DgvProblemList_MouseMove(object sender, MouseEventArgs e)
        {
            DataGridView.HitTestInfo hit = dgvProblemList.HitTest(e.X, e.Y);

            if (hit.RowIndex != _lastHoveredRow)
            {
                // Restore previous row
                if (_lastHoveredRow >= 0 && _lastHoveredRow < dgvProblemList.Rows.Count)
                {
                    dgvProblemList.Rows[_lastHoveredRow].DefaultCellStyle.BackColor = Color.White;
                }

                _lastHoveredRow = hit.RowIndex;

                // Highlight current row
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
            ApplyCombinedFilters();
        }

        /// <summary>
        /// Áp dụng filter kết hợp
        /// </summary>
        private void ApplyCombinedFilters()
        {
            try
            {
                if (_allProblems == null) return;

                dgvProblemList.SuspendLayout();
                dgvProblemList.Rows.Clear();

                var filteredProblems = _allProblems.Where(p =>
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

                _displayedProblems = filteredProblems;

                foreach (var problem in filteredProblems)
                {
                    dgvProblemList.Rows.Add(
                        problem.ProblemID.ToString(),
                        problem.Title,
                        problem.Difficulty,
                        problem.Tags ?? "",
                        problem.Status
                    );
                }

                dgvProblemList.ResumeLayout();
                UpdateStatistics(filteredProblems);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi lọc dữ liệu: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Cập nhật thống kê
        /// </summary>
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

        /// <summary>
        /// Load dữ liệu từ database
        /// </summary>
        private void LoadDataFromDatabase()
        {
            try
            {
                dgvProblemList.SuspendLayout();
                dgvProblemList.Rows.Clear();

                _allProblems = _problemService.GetAll();

                if (_allProblems == null)
                {
                    _allProblems = new List<CodeForge_Desktop.DataAccess.Entities.CodingProblem>();
                }

                _displayedProblems = new List<CodeForge_Desktop.DataAccess.Entities.CodingProblem>(_allProblems);

                foreach (var problem in _allProblems)
                {
                    dgvProblemList.Rows.Add(
                        problem.ProblemID.ToString(),
                        problem.Title,
                        problem.Difficulty,
                        problem.Tags ?? "",
                        problem.Status
                    );
                }

                dgvProblemList.ResumeLayout();
                UpdateStatistics(_allProblems);
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

        /// <summary>
        /// Custom paint cho cells
        /// </summary>
        private void dgvProblemList_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex < 0 || e.Value == null) return;

            try
            {
                // Status column - Beautiful badges
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

                    // Draw rounded badge
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
                // Problem name - Link style
                else if (e.ColumnIndex == dgvProblemList.Columns["colProblemName"].Index)
                {
                    e.Paint(e.CellBounds, DataGridViewPaintParts.All & ~DataGridViewPaintParts.ContentForeground);

                    TextRenderer.DrawText(e.Graphics, e.Value.ToString(), _cellFont,
                        e.CellBounds, _linkBlue,
                        TextFormatFlags.VerticalCenter | TextFormatFlags.Left);

                    e.Handled = true;
                }
                // Difficulty - Color coded
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
                // Tags - Subtle color
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

        /// <summary>
        /// Placeholder handling
        /// </summary>
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

        /// <summary>
        /// Search với debouncing
        /// </summary>
        private void SetupSearchBox()
        {
            System.Windows.Forms.Timer searchTimer = new System.Windows.Forms.Timer();
            searchTimer.Interval = 300;
            searchTimer.Tick += (s, e) =>
            {
                searchTimer.Stop();
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