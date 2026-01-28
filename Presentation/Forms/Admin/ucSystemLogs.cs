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
        private int _pageSize = 15;

        // Dictionary để map giữa tên hiển thị và giá trị thực
        private Dictionary<string, string> _sourceMapping = new Dictionary<string, string>
        {
            { "Tất cả", "" },
            { "Xác thực", "Auth" },
            { "Hệ thống", "System" },
            { "Cơ sở dữ liệu", "Database" },
            { "Bài tập", "Submission" },
            { "Quản trị", "Admin" }
        };

        private Dictionary<string, string> _levelMapping = new Dictionary<string, string>
        {
            { "Tất cả", "" },
            { "INFO", "INFO" },
            { "WARNING", "WARNING" },
            { "ERROR", "ERROR" }
        };

        public ucSystemLogs()
        {
            InitializeComponent();
            SetupControls();
            SetupDataGridView();
            LoadMockData();
            SetupEvents();
        }

        private void SetupControls()
        {
            // Thiết lập giá trị mặc định
            cmbLevel.SelectedIndex = 0;
            cmbSource.SelectedIndex = 0;
            cmbPageSize.SelectedIndex = 1; // 15 items

            // Thiết lập DateTimePicker
            dtpFromDate.Value = DateTime.Now.AddDays(-30);
            dtpToDate.Value = DateTime.Now;

            // Thiết lập placeholder cho textbox
            txtSearchKeyword.GotFocus += TxtSearchKeyword_GotFocus;
            txtSearchKeyword.LostFocus += TxtSearchKeyword_LostFocus;
        }

        private void SetupDataGridView()
        {
            dgvLogs.Columns["colLevel"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvLogs.Columns["colUser"].DefaultCellStyle.ForeColor = Color.FromArgb(13, 110, 253);
            dgvLogs.Columns["colUser"].DefaultCellStyle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        }

        private void SetupEvents()
        {
            // DataGridView Events
            dgvLogs.CellPainting += DgvLogs_CellPainting;

            // Toolbar Events
            btnRefresh.Click += BtnRefresh_Click;
            btnExport.Click += BtnExport_Click;
            btnFilter.Click += BtnFilter_Click;
            btnClearFilter.Click += BtnClearFilter_Click;

            // Pagination Events
            btnPrev.Click += (s, e) => ChangePage(-1);
            btnNext.Click += (s, e) => ChangePage(1);
            cmbPageSize.SelectedIndexChanged += CmbPageSize_SelectedIndexChanged;

            // Enter key để tìm kiếm
            txtSearchKeyword.KeyDown += TxtSearchKeyword_KeyDown;
        }

        private void TxtSearchKeyword_GotFocus(object sender, EventArgs e)
        {
            if (txtSearchKeyword.Text == "Nhập từ khóa..." && txtSearchKeyword.ForeColor == Color.Gray)
            {
                txtSearchKeyword.Text = "";
                txtSearchKeyword.ForeColor = Color.FromArgb(40, 40, 40);
            }
        }

        private void TxtSearchKeyword_LostFocus(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtSearchKeyword.Text))
            {
                txtSearchKeyword.Text = "Nhập từ khóa...";
                txtSearchKeyword.ForeColor = Color.Gray;
            }
        }

        private void TxtSearchKeyword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ApplyFilter();
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }

        private void CmbPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (int.TryParse(cmbPageSize.Text, out int newPageSize))
            {
                _pageSize = newPageSize;
                _currentPage = 1;
                RenderGrid();
            }
        }

        private void LoadMockData()
        {
            _allLogs.Clear();
            var rand = new Random();
            string[] levels = { "INFO", "INFO", "INFO", "WARNING", "ERROR" };
            string[] sources = { "Auth", "System", "Database", "Submission", "Admin" };
            string[] users = { "sinhvien_01", "sinhvien_02", "quantri_01", "hethong", "khach" };
            string[] messages = {
                "Đăng nhập thành công từ địa chỉ IP 192.168.1.100",
                "Cập nhật cấu hình hệ thống",
                "Kết nối cơ sở dữ liệu thành công",
                "Nộp bài tập lập trình C++ thành công",
                "Xóa bản ghi cũ trong bảng logs",
                "Backup dữ liệu hoàn tất",
                "Cảnh báo: Dung lượng đĩa sắp đầy",
                "Lỗi: Không thể kết nối đến máy chủ email",
                "Người dùng thay đổi mật khẩu",
                "Tạo tài khoản mới cho sinh viên"
            };

            // Tạo 100 logs với dữ liệu mô phỏng
            for (int i = 0; i < 100; i++)
            {
                _allLogs.Add(new LogItem
                {
                    Timestamp = DateTime.Now.AddMinutes(-i * 15).AddSeconds(rand.Next(-300, 300)),
                    Level = levels[rand.Next(levels.Length)],
                    Source = sources[rand.Next(sources.Length)],
                    User = users[rand.Next(users.Length)],
                    Message = messages[rand.Next(messages.Length)]
                });
            }

            // Sắp xếp theo thời gian mới nhất
            _allLogs = _allLogs.OrderByDescending(x => x.Timestamp).ToList();

            // Mặc định hiển thị tất cả
            ApplyFilter();
        }

        private void ApplyFilter()
        {
            // Lấy giá trị lọc
            string selectedLevel = cmbLevel.Text;
            string selectedSource = cmbSource.Text;
            DateTime fromDate = dtpFromDate.Value.Date;
            DateTime toDate = dtpToDate.Value.Date.AddDays(1).AddSeconds(-1); // Cuối ngày
            string searchText = txtSearchKeyword.Text == "Nhập từ khóa..." ? "" : txtSearchKeyword.Text.Trim();

            // Map về giá trị thực
            string levelValue = _levelMapping.ContainsKey(selectedLevel) ? _levelMapping[selectedLevel] : "";
            string sourceValue = _sourceMapping.ContainsKey(selectedSource) ? _sourceMapping[selectedSource] : "";

            // Áp dụng bộ lọc
            _filteredLogs = _allLogs.Where(log =>
            {
                // Lọc theo mức độ
                bool levelMatch = string.IsNullOrEmpty(levelValue) || log.Level == levelValue;

                // Lọc theo nguồn
                bool sourceMatch = string.IsNullOrEmpty(sourceValue) || log.Source == sourceValue;

                // Lọc theo khoảng thời gian
                bool dateMatch = log.Timestamp >= fromDate && log.Timestamp <= toDate;

                // Lọc theo từ khóa (tìm trong Message hoặc User)
                bool searchMatch = string.IsNullOrEmpty(searchText) ||
                                   log.Message.ToLower().Contains(searchText.ToLower()) ||
                                   log.User.ToLower().Contains(searchText.ToLower());

                return levelMatch && sourceMatch && dateMatch && searchMatch;
            }).ToList();

            // Reset về trang 1 và render
            _currentPage = 1;
            RenderGrid();
            UpdateStats();

            // Hiệu ứng animation nhẹ
            AnimateFilterApplied();
        }

        private void AnimateFilterApplied()
        {
            // Hiệu ứng đơn giản - flash màu nền
            var originalColor = pnlFilters.BackColor;
            pnlFilters.BackColor = Color.FromArgb(220, 240, 255);

            var timer = new Timer { Interval = 150 };
            timer.Tick += (s, e) =>
            {
                pnlFilters.BackColor = originalColor;
                timer.Stop();
                timer.Dispose();
            };
            timer.Start();
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
            var pageData = _filteredLogs
                .Skip((_currentPage - 1) * _pageSize)
                .Take(_pageSize)
                .ToList();

            foreach (var log in pageData)
            {
                dgvLogs.Rows.Add(
                    log.Timestamp.ToString("dd/MM/yyyy HH:mm:ss"),
                    log.Level,
                    GetVietnameseSource(log.Source),
                    log.Message,
                    log.User
                );
            }

            dgvLogs.ClearSelection();

            // Cập nhật UI phân trang
            lblPageInfo.Text = $"Trang {_currentPage} / {totalPages}";
            btnPrev.Enabled = _currentPage > 1;
            btnNext.Enabled = _currentPage < totalPages;
        }

        private string GetVietnameseSource(string englishSource)
        {
            var entry = _sourceMapping.FirstOrDefault(x => x.Value == englishSource);
            return entry.Key != null ? entry.Key : englishSource;
        }

        private void ChangePage(int delta)
        {
            _currentPage += delta;
            RenderGrid();
        }

        private void BtnRefresh_Click(object sender, EventArgs e)
        {
            LoadMockData();
            ShowNotification("Đã làm mới dữ liệu!", Color.FromArgb(25, 135, 84));
        }

        private void BtnFilter_Click(object sender, EventArgs e)
        {
            ApplyFilter();
            ShowNotification("Đã áp dụng bộ lọc!", Color.FromArgb(111, 66, 193));
        }

        private void BtnClearFilter_Click(object sender, EventArgs e)
        {
            // Reset tất cả bộ lọc về mặc định
            cmbLevel.SelectedIndex = 0;
            cmbSource.SelectedIndex = 0;
            dtpFromDate.Value = DateTime.Now.AddDays(-30);
            dtpToDate.Value = DateTime.Now;
            txtSearchKeyword.Text = "Nhập từ khóa...";
            txtSearchKeyword.ForeColor = Color.Gray;

            ApplyFilter();
            ShowNotification("Đã xóa bộ lọc!", Color.FromArgb(108, 117, 125));
        }

        private void UpdateStats()
        {
            // Tính toán trên dữ liệu đã lọc
            int info = _filteredLogs.Count(x => x.Level == "INFO");
            int warning = _filteredLogs.Count(x => x.Level == "WARNING");
            int error = _filteredLogs.Count(x => x.Level == "ERROR");
            int total = _filteredLogs.Count;

            lblInfoCount.Text = info.ToString();
            lblWarningCount.Text = warning.ToString();
            lblErrorCount.Text = error.ToString();
            lblTotalLogs.Text = $"📊 Tổng số: {total} bản ghi";
        }

        private void BtnExport_Click(object sender, EventArgs e)
        {
            if (_filteredLogs.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu để xuất!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.Filter = "CSV File|*.csv";
                sfd.FileName = $"NhatKyHeThong_{DateTime.Now:yyyyMMdd_HHmmss}.csv";

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        using (StreamWriter sw = new StreamWriter(sfd.FileName, false, System.Text.Encoding.UTF8))
                        {
                            // Header với BOM để Excel hiển thị đúng tiếng Việt
                            sw.WriteLine("Thời gian,Mức độ,Nguồn,Người dùng,Nội dung");

                            foreach (var log in _filteredLogs)
                            {
                                string vietnameseSource = GetVietnameseSource(log.Source);
                                sw.WriteLine($"\"{log.Timestamp:dd/MM/yyyy HH:mm:ss}\",{log.Level},{vietnameseSource},{log.User},\"{log.Message}\"");
                            }
                        }

                        MessageBox.Show($"Xuất thành công {_filteredLogs.Count} bản ghi!", "Thành công",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);

                        // Hỏi có muốn mở file không
                        if (MessageBox.Show("Bạn có muốn mở file vừa xuất?", "Xác nhận",
                            MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            System.Diagnostics.Process.Start(sfd.FileName);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Lỗi khi xuất file: {ex.Message}", "Lỗi",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void ShowNotification(string message, Color color)
        {
            // Tạo label thông báo tạm thời
            var notification = new Label
            {
                Text = message,
                AutoSize = true,
                BackColor = color,
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                Padding = new Padding(15, 8, 15, 8),
                Location = new Point((Width - 200) / 2, 10)
            };

            Controls.Add(notification);
            notification.BringToFront();

            // Auto hide sau 2 giây
            var timer = new Timer { Interval = 2000 };
            timer.Tick += (s, e) =>
            {
                Controls.Remove(notification);
                notification.Dispose();
                timer.Stop();
                timer.Dispose();
            };
            timer.Start();
        }

        // Custom Painting
        private void DgvLogs_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex < 0) return;

            // Vẽ badge cho cột Level
            if (e.ColumnIndex == dgvLogs.Columns["colLevel"].Index && e.Value != null)
            {
                e.Paint(e.CellBounds, DataGridViewPaintParts.All & ~DataGridViewPaintParts.ContentForeground);

                string level = e.Value.ToString();
                Color backColor = Color.Gray;
                Color foreColor = Color.White;
                string displayText = level;

                switch (level)
                {
                    case "INFO":
                        backColor = Color.FromArgb(225, 245, 254);
                        foreColor = Color.FromArgb(3, 169, 244);
                        displayText = "ℹ️ INFO";
                        break;
                    case "WARNING":
                        backColor = Color.FromArgb(255, 248, 225);
                        foreColor = Color.FromArgb(255, 152, 0);
                        displayText = "⚠️ CẢNH BÁO";
                        break;
                    case "ERROR":
                        backColor = Color.FromArgb(255, 235, 238);
                        foreColor = Color.FromArgb(233, 30, 99);
                        displayText = "🚨 LỖI";
                        break;
                }

                var rect = new Rectangle(
                    e.CellBounds.X + 15,
                    e.CellBounds.Y + 10,
                    e.CellBounds.Width - 30,
                    e.CellBounds.Height - 20
                );

                // Vẽ nền với bo góc
                using (var path = GetRoundedRectPath(rect, 6))
                using (Brush brush = new SolidBrush(backColor))
                {
                    e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                    e.Graphics.FillPath(brush, path);
                }

                // Vẽ text
                TextRenderer.DrawText(
                    e.Graphics,
                    displayText,
                    new Font("Segoe UI", 8F, FontStyle.Bold),
                    rect,
                    foreColor,
                    TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter
                );

                e.Handled = true;
            }

            // Vẽ màu cho cột Source
            if (e.ColumnIndex == dgvLogs.Columns["colSource"].Index && e.Value != null)
            {
                e.Paint(e.CellBounds, DataGridViewPaintParts.All & ~DataGridViewPaintParts.ContentForeground);

                TextRenderer.DrawText(
                    e.Graphics,
                    e.Value.ToString(),
                    e.CellStyle.Font,
                    e.CellBounds,
                    Color.FromArgb(111, 66, 193),
                    TextFormatFlags.VerticalCenter | TextFormatFlags.Left
                );

                e.Handled = true;
            }
        }

        private System.Drawing.Drawing2D.GraphicsPath GetRoundedRectPath(Rectangle rect, int radius)
        {
            var path = new System.Drawing.Drawing2D.GraphicsPath();

            path.AddArc(rect.X, rect.Y, radius, radius, 180, 90);
            path.AddArc(rect.Right - radius, rect.Y, radius, radius, 270, 90);
            path.AddArc(rect.Right - radius, rect.Bottom - radius, radius, radius, 0, 90);
            path.AddArc(rect.X, rect.Bottom - radius, radius, radius, 90, 90);
            path.CloseFigure();

            return path;
        }
    }
}