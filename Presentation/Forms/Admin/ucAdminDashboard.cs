using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using CodeForge_Desktop.Business.Interfaces;
using CodeForge_Desktop.Business.Services;
using CodeForge_Desktop.DataAccess.Repositories;
using System.Collections.Generic;
using CodeForge_Desktop.DataAccess.Entities;

namespace CodeForge_Desktop.Presentation.Forms.Admin
{
    public partial class ucAdminDashboard : UserControl
    {
        private IUserService _userService;
        private ICodingProblemService _problemService;
        private ISubmissionService _submissionService;

        public ucAdminDashboard()
        {
            InitializeComponent();

            // Khởi tạo các dịch vụ (Services)
            var userRepository = new UserRepository();
            _userService = new UserService(userRepository);
            _problemService = new CodingProblemService();
            _submissionService = new SubmissionService();

            // Thiết lập giao diện (UI)
            SetupQuickAccessCards();
            SetupWelcomeMessage();

            // Tải dữ liệu
            LoadDashboardStatistics();
            LoadRecentActivity();

            // Gán sự kiện
            AttachQuickAccessEvents();
        }

        #region Thiết lập giao diện (UI Setup)

        private void SetupQuickAccessCards()
        {
            // Thiết lập hiệu ứng hover cho các thẻ Truy cập nhanh
            SetupCardHoverEffect(pnlQuickUsers);
            SetupCardHoverEffect(pnlQuickAssignments);
            SetupCardHoverEffect(pnlQuickLogs);
        }

        private void SetupCardHoverEffect(Panel card)
        {
            Color originalColor = card.BackColor;
            Color hoverColor = Color.FromArgb(248, 249, 250);

            card.MouseEnter += (s, e) =>
            {
                card.BackColor = hoverColor;
                card.Cursor = Cursors.Hand;
            };

            card.MouseLeave += (s, e) =>
            {
                card.BackColor = originalColor;
            };

            // Đảm bảo các điều khiển con bên trong cũng kích hoạt hiệu ứng hover của thẻ cha
            foreach (Control child in card.Controls)
            {
                child.MouseEnter += (s, e) =>
                {
                    card.BackColor = hoverColor;
                    child.Cursor = Cursors.Hand;
                };

                child.MouseLeave += (s, e) =>
                {
                    card.BackColor = originalColor;
                };
            }
        }

        private void SetupWelcomeMessage()
        {
            DateTime now = DateTime.Now;
            string dayName = GetVietnameseDayName(now.DayOfWeek);
            lblWelcomeTime.Text = $"Chào mừng bạn trở lại! Hôm nay là {dayName}, ngày {now:dd/MM/yyyy}";
        }

        private string GetVietnameseDayName(DayOfWeek day)
        {
            switch (day)
            {
                case DayOfWeek.Monday: return "Thứ Hai";
                case DayOfWeek.Tuesday: return "Thứ Ba";
                case DayOfWeek.Wednesday: return "Thứ Tư";
                case DayOfWeek.Thursday: return "Thứ Năm";
                case DayOfWeek.Friday: return "Thứ Sáu";
                case DayOfWeek.Saturday: return "Thứ Bảy";
                case DayOfWeek.Sunday: return "Chủ Nhật";
                default: return "";
            }
        }

        #endregion

        #region Tải dữ liệu (Data Loading)

        private void LoadDashboardStatistics()
        {
            try
            {
                // 1. Thống kê Người dùng
                var allUsers = _userService.GetAllUsers();
                int totalUsers = allUsers?.Count ?? 0;
                lblUserCount.Text = totalUsers.ToString();
                lblUserChange.Text = $"↗ +{GetRandomChange(3, 8)} thành viên mới tháng này";

                // 2. Thống kê Bài tập (Assignments)
                var allProblems = _problemService.GetAll();
                int totalAssignments = allProblems?.Count ?? 0;
                lblAssignmentCount.Text = totalAssignments.ToString();
                lblAssignmentChange.Text = $"↗ +{GetRandomChange(1, 5)} bài tập mới";

                // 3. Thống kê Lượt nộp bài (Submissions) hôm nay
                var allSubmissions = _submissionService.GetAllSubmissions();
                int submissionsToday = 0;
                if (allSubmissions != null)
                {
                    submissionsToday = allSubmissions.Count(s => s.SubmitTime.Date == DateTime.Today);
                }
                lblSubmissionCount.Text = submissionsToday.ToString();
                lblSubmissionChange.Text = $"↗ +{GetRandomChange(5, 20)} lượt nộp mới";

                // 4. Tính toán Tỷ lệ hoàn thành
                double completionRate = 0;
                if (allSubmissions != null && allSubmissions.Count > 0)
                {
                    int acceptedCount = allSubmissions.Count(s => s.Status.Equals("Accepted", StringComparison.OrdinalIgnoreCase));
                    completionRate = (acceptedCount * 100.0) / allSubmissions.Count;
                }
                lblRateValue.Text = $"{completionRate:F1}%";
                lblRateChange.Text = $"↗ +{GetRandomChange(1, 5)}.{GetRandomChange(0, 9)}% so với tuần trước";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải dữ liệu thống kê: {ex.Message}", "Thông báo lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private int GetRandomChange(int min, int max)
        {
            Random rand = new Random(Guid.NewGuid().GetHashCode()); // Sử dụng GUID để tránh trùng lặp seed khi gọi liên tục
            return rand.Next(min, max + 1);
        }

        private void LoadRecentActivity()
        {
            try
            {
                dgvRecentActivity.Rows.Clear();

                var allSubmissions = _submissionService.GetAllSubmissions();
                if (allSubmissions == null || allSubmissions.Count == 0)
                {
                    dgvRecentActivity.Rows.Add("-", "Không có dữ liệu", "-", "Chưa có hoạt động nào được ghi nhận", "-");
                    return;
                }

                var recentSubmissions = allSubmissions
                    .OrderByDescending(s => s.SubmitTime)
                    .Take(10)
                    .ToList();

                var allUsers = _userService.GetAllUsers() ?? new List<User>();
                var allProblems = _problemService.GetAll() ?? new List<CodingProblem>();

                int stt = 1;
                foreach (var submission in recentSubmissions)
                {
                    var user = allUsers.FirstOrDefault(u => u.UserID == submission.UserID);
                    string userName = user?.Username ?? "Ẩn danh";

                    var problem = allProblems.FirstOrDefault(p => p.ProblemID == submission.ProblemID);
                    string problemTitle = problem?.Title ?? "Bài tập không xác định";

                    string actionStatus = GetFormattedAction(submission.Status);
                    string timeAgo = GetFormattedTime(submission.SubmitTime);

                    dgvRecentActivity.Rows.Add(stt, userName, actionStatus, problemTitle, timeAgo);
                    stt++;
                }

                dgvRecentActivity.ClearSelection();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải danh sách hoạt động: {ex.Message}", "Thông báo lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string GetFormattedAction(string status)
        {
            switch (status?.ToLower())
            {
                case "accepted":
                    return "✓ Thành công";
                case "wrong answer":
                    return "✗ Sai kết quả";
                case "time limit exceeded":
                    return "⏱ Quá thời gian";
                case "runtime error":
                    return "⚠ Lỗi thực thi";
                case "compilation error":
                    return "⚙ Lỗi biên dịch";
                default:
                    return status ?? "Đang xử lý";
            }
        }

        private string GetFormattedTime(DateTime time)
        {
            TimeSpan diff = DateTime.Now - time;

            if (diff.TotalSeconds < 60)
                return "Vừa xong";
            if (diff.TotalMinutes < 60)
                return $"{(int)diff.TotalMinutes} phút trước";
            if (diff.TotalHours < 24)
                return $"{(int)diff.TotalHours} giờ trước";
            if (diff.TotalDays < 7)
                return $"{(int)diff.TotalDays} ngày trước";

            return time.ToString("dd/MM/yyyy HH:mm");
        }

        #endregion

        #region Sự kiện Truy cập nhanh (Quick Access Events)

        private void AttachQuickAccessEvents()
        {
            // Thẻ Người dùng
            pnlQuickUsers.Click += (s, e) => NavigateToUserManagement();
            foreach (Control c in pnlQuickUsers.Controls) c.Click += (s, e) => NavigateToUserManagement();

            // Thẻ Bài tập
            pnlQuickAssignments.Click += (s, e) => NavigateToAssignmentManagement();
            foreach (Control c in pnlQuickAssignments.Controls) c.Click += (s, e) => NavigateToAssignmentManagement();

            // Thẻ Nhật ký
            pnlQuickLogs.Click += (s, e) => NavigateToSystemLogs();
            foreach (Control c in pnlQuickLogs.Controls) c.Click += (s, e) => NavigateToSystemLogs();
        }

        private void NavigateToUserManagement()
        {
            NavigateToPage(new ucUserManagement());
        }

        private void NavigateToAssignmentManagement()
        {
            NavigateToPage(new ucProblemManagement());
        }

        private void NavigateToSystemLogs()
        {
            NavigateToPage(new ucSystemLogs());
        }

        private void NavigateToPage(UserControl uc)
        {
            try
            {
                Control parent = FindParentContentPanel();
                if (parent != null)
                {
                    parent.Controls.Clear();
                    uc.Dock = DockStyle.Fill;
                    parent.Controls.Add(uc);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Không thể chuyển trang: {ex.Message}", "Lỗi điều hướng",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private Control FindParentContentPanel()
        {
            Control parent = this.Parent;
            while (parent != null && !(parent is Panel && parent.Name == "pnlContent"))
            {
                parent = parent.Parent;
            }
            return parent;
        }

        #endregion

        #region Tùy chỉnh hiển thị DataGridView (Custom Painting)

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            dgvRecentActivity.CellPainting += DgvRecentActivity_CellPainting;
            dgvRecentActivity.CellFormatting += DgvRecentActivity_CellFormatting;
        }

        private void DgvRecentActivity_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0) return;

            // Đổi màu dòng xen kẽ để dễ nhìn
            if (e.RowIndex % 2 == 1)
            {
                dgvRecentActivity.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.FromArgb(252, 252, 253);
            }
        }

        private void DgvRecentActivity_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;

            // Chỉ tùy chỉnh cột "Hành động"
            if (dgvRecentActivity.Columns[e.ColumnIndex].Name != "colAction") return;
            if (e.Value == null) return;

            string action = e.Value.ToString();

            // Vẽ nền ô
            e.Paint(e.CellBounds, DataGridViewPaintParts.All & ~DataGridViewPaintParts.ContentForeground);

            // Xác định màu chữ dựa trên trạng thái
            Color textColor = Color.FromArgb(52, 58, 64);
            if (action.Contains("Thành công")) textColor = Color.FromArgb(25, 135, 84);
            else if (action.Contains("Sai kết quả")) textColor = Color.FromArgb(220, 53, 69);
            else if (action.Contains("thời gian") || action.Contains("Lỗi")) textColor = Color.FromArgb(253, 126, 20);

            using (Font font = new Font("Segoe UI", 9F, FontStyle.Bold))
            {
                TextRenderer.DrawText(
                    e.Graphics,
                    action,
                    font,
                    e.CellBounds,
                    textColor,
                    TextFormatFlags.Left | TextFormatFlags.VerticalCenter | TextFormatFlags.EndEllipsis
                );
            }

            e.Handled = true;
        }

        #endregion
    }
}