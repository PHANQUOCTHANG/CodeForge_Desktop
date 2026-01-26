using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace CodeForge_Desktop.Presentation.Forms.Admin
{
    public partial class ucSystemLogs : UserControl
    {
        // Class dữ liệu
        private class LogItem
        {
            public DateTime Timestamp { get; set; }
            public string Level { get; set; }
            public string Source { get; set; }
            public string User { get; set; }
            public string Message { get; set; }
        }

        private List<LogItem> _allLogs = new List<LogItem>();
        private List<LogItem> _filteredLogs = new List<LogItem>();

        // Phân trang
        private int _currentPage = 1;
        private const int _pageSize = 15;

        public ucSystemLogs()
        {
            InitializeComponent();
            SetupDataGridView();
            LoadMockData();

            // Gắn sự kiện
            dgvLogs.CellPainting += DgvLogs_CellPainting;

            // Toolbar Events
            btnRefresh.Click += (s, e) => { LoadMockData(); };
            btnExport.Click += BtnExport_Click;
            btnFilter.Click += BtnFilter_Click;

            // Pagination Events
            btnPrev.Click += (s, e) => ChangePage(-1);
            btnNext.Click += (s, e) => ChangePage(1);

            // Combobox Default
            cmbLevel.SelectedIndex = 0;
            cmbSource.SelectedIndex = 0;
        }

        private void SetupDataGridView()
        {
            dgvLogs.Columns["colLevel"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvLogs.Columns["colUser"].DefaultCellStyle.ForeColor = Color.FromArgb(13, 110, 253);
        }

        private void LoadMockData()
        {
            _allLogs.Clear();
            var rand = new Random();
            string[] levels = { "INFO", "INFO", "INFO", "WARNING", "ERROR" };
            string[] sources = { "Auth", "System", "Database", "Submission", "Admin" };
            string[] users = { "student_01", "student_02", "admin_01", "system", "unknown" };

            // Tạo giả 50 logs
            for (int i = 0; i < 50; i++)
            {
                _allLogs.Add(new LogItem
                {
                    Timestamp = DateTime.Now.AddMinutes(-i * 10),
                    Level = levels[rand.Next(levels.Length)],
                    Source = sources[rand.Next(sources.Length)],
                    User = users[rand.Next(users.Length)],
                    Message = $"System log entry generated for testing purposes. ID: {i + 1}"
                });
            }

            // Mặc định hiển thị tất cả
            ApplyFilter();
        }

        private void ApplyFilter()
        {
            string lv = cmbLevel.Text == "All Levels" ? "" : cmbLevel.Text;
            string src = cmbSource.Text == "All Sources" ? "" : cmbSource.Text;

            // Lọc dữ liệu
            _filteredLogs = _allLogs.Where(x =>
                (string.IsNullOrEmpty(lv) || x.Level == lv) &&
                (string.IsNullOrEmpty(src) || x.Source == src)
            ).ToList();

            // Reset về trang 1
            _currentPage = 1;
            RenderGrid();
            UpdateStats();
        }

        private void RenderGrid()
        {
            dgvLogs.Rows.Clear();

            int totalRecords = _filteredLogs.Count;
            int totalPages = (int)Math.Ceiling((double)totalRecords / _pageSize);
            if (totalPages < 1) totalPages = 1;

            if (_currentPage < 1) _currentPage = 1;
            if (_currentPage > totalPages) _currentPage = totalPages;

            // Cắt dữ liệu theo trang
            var pageData = _filteredLogs.Skip((_currentPage - 1) * _pageSize).Take(_pageSize).ToList();

            foreach (var log in pageData)
            {
                dgvLogs.Rows.Add(
                    log.Timestamp.ToString("yyyy-MM-dd HH:mm:ss"),
                    log.Level,
                    log.Source,
                    log.Message,
                    log.User
                );
            }
            dgvLogs.ClearSelection();

            // Cập nhật UI
            lblPageInfo.Text = $"Page {_currentPage} / {totalPages}";
            btnPrev.Enabled = _currentPage > 1;
            btnNext.Enabled = _currentPage < totalPages;
            lblTotalLogs.Text = $"Total Logs: {totalRecords}"; // Highlight Total
        }

        private void ChangePage(int delta)
        {
            _currentPage += delta;
            RenderGrid();
        }

        private void BtnFilter_Click(object sender, EventArgs e)
        {
            ApplyFilter();
        }

        private void UpdateStats()
        {
            // Tính toán trên dữ liệu đã lọc
            int info = _filteredLogs.Count(x => x.Level == "INFO");
            int warning = _filteredLogs.Count(x => x.Level == "WARNING");
            int error = _filteredLogs.Count(x => x.Level == "ERROR");

            lblInfoBadge.Text = $"INFO {info}";
            lblWarningBadge.Text = $"WARNING {warning}";
            lblErrorBadge.Text = $"ERROR {error}";
        }

        private void BtnExport_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.Filter = "CSV File|*.csv";
                sfd.FileName = $"SystemLogs_{DateTime.Now:yyyyMMdd_HHmmss}.csv";
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        using (StreamWriter sw = new StreamWriter(sfd.FileName))
                        {
                            sw.WriteLine("Timestamp,Level,Source,User,Message");
                            foreach (var log in _filteredLogs)
                            {
                                sw.WriteLine($"{log.Timestamp},{log.Level},{log.Source},{log.User},\"{log.Message}\"");
                            }
                        }
                        MessageBox.Show("Export thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Lỗi export: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        // --- Custom Painting (Giữ nguyên) ---
        private void DgvLogs_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex < 0) return;

            if (e.ColumnIndex == dgvLogs.Columns["colLevel"].Index && e.Value != null)
            {
                e.Paint(e.CellBounds, DataGridViewPaintParts.All & ~DataGridViewPaintParts.ContentForeground);
                string level = e.Value.ToString();
                Color back = Color.Gray, fore = Color.White;

                if (level == "INFO") { back = Color.FromArgb(225, 245, 254); fore = Color.FromArgb(3, 169, 244); }
                else if (level == "WARNING") { back = Color.FromArgb(255, 248, 225); fore = Color.FromArgb(255, 160, 0); }
                else if (level == "ERROR") { back = Color.FromArgb(255, 235, 238); fore = Color.FromArgb(233, 30, 99); }

                var rect = new Rectangle(e.CellBounds.X + 15, e.CellBounds.Y + 8, e.CellBounds.Width - 30, e.CellBounds.Height - 16);
                using (Brush b = new SolidBrush(back)) e.Graphics.FillRectangle(b, rect);
                TextRenderer.DrawText(e.Graphics, level, new Font("Segoe UI", 8, FontStyle.Bold), rect, fore, TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
                e.Handled = true;
            }

            if (e.ColumnIndex == dgvLogs.Columns["colSource"].Index && e.Value != null)
            {
                e.Paint(e.CellBounds, DataGridViewPaintParts.All & ~DataGridViewPaintParts.ContentForeground);
                TextRenderer.DrawText(e.Graphics, e.Value.ToString(), e.CellStyle.Font, e.CellBounds, Color.FromArgb(0, 120, 215), TextFormatFlags.VerticalCenter | TextFormatFlags.Left);
                e.Handled = true;
            }
        }
    }
}