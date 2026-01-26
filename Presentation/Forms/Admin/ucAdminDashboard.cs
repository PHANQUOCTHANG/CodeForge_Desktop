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

            // Khởi tạo services (Giữ nguyên)
            var userRepository = new UserRepository();
            _userService = new UserService(userRepository);
            _problemService = new CodingProblemService();
            _submissionService = new SubmissionService();

            // Load dữ liệu
            LoadDashboardStatistics();
            LoadRecentActivity();

            // Sự kiện click (Map lại vào nút mới)
            btnQuickUsers.Click += btnUIUser_Click;
            btnQuickAssignments.Click += btnUIAssignment_Click;
            btnQuickLogs.Click += btnUILog_Click;
        }

        private void LoadDashboardStatistics()
        {
            try
            {
                // 1. Tổng số Users
                var allUsers = _userService.GetAllUsers();
                int totalUsers = allUsers?.Count ?? 0;
                lblUserCount.Text = totalUsers.ToString(); // Đổi tên control

                // 2. Tổng số Assignments
                var allProblems = _problemService.GetAll();
                int totalAssignments = allProblems?.Count ?? 0;
                lblAssignmentCount.Text = totalAssignments.ToString(); // Đổi tên control

                // 3. Submissions hôm nay
                var allSubmissions = _submissionService.GetAllSubmissions();
                int submissionsToday = 0;
                if (allSubmissions != null)
                {
                    submissionsToday = allSubmissions.Count(s => s.SubmitTime.Date == DateTime.Today);
                }
                lblSubmissionCount.Text = submissionsToday.ToString(); // Đổi tên control

                // 4. Tỷ lệ hoàn thành
                double completionRate = 0;
                if (allSubmissions != null && allSubmissions.Count > 0)
                {
                    int acceptedCount = allSubmissions.Count(s => s.Status == "Accepted");
                    completionRate = (acceptedCount * 100.0) / allSubmissions.Count;
                }
                lblRateValue.Text = $"{completionRate:F1}%"; // Đổi tên control
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi tải thống kê: {ex.Message}");
            }
        }

        private void LoadRecentActivity()
        {
            try
            {
                dgvRecentActivity.Rows.Clear(); // Đổi tên control Grid

                var allSubmissions = _submissionService.GetAllSubmissions();
                if (allSubmissions == null || allSubmissions.Count == 0) return;

                var recentSubmissions = allSubmissions
                    .OrderByDescending(s => s.SubmitTime)
                    .Take(10)
                    .ToList();

                var allUsers = _userService.GetAllUsers() ?? new List<User>();
                var allProblems = _problemService.GetAll() ?? new List<CodingProblem>();

                int index = 1;
                foreach (var submission in recentSubmissions)
                {
                    var user = allUsers.FirstOrDefault(u => u.UserID == submission.UserID);
                    string userName = user?.Username ?? "Unknown";

                    var problem = allProblems.FirstOrDefault(p => p.ProblemID == submission.ProblemID);
                    string problemName = problem?.Title ?? "Unknown";

                    string action = "Nộp bài"; // Mặc định
                    if (submission.Status == "Accepted") action = "✓ Accepted";
                    else if (submission.Status == "Wrong Answer") action = "✗ Wrong Answer";
                    else action = submission.Status;

                    string time = submission.SubmitTime.ToString("HH:mm dd/MM");

                    // Thêm vào Grid
                    dgvRecentActivity.Rows.Add(index, userName, action, problemName, time);
                    index++;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi tải hoạt động: {ex.Message}");
            }
        }

        // Sự kiện chuyển trang (Giữ nguyên logic gọi parent form)
        private void btnUIUser_Click(object sender, EventArgs e)
        {
            // Logic chuyển trang (giống cũ)
            // Lưu ý: Cần truy cập pnlContent của MainFormAdmin
            // Cách đơn giản nhất:
            Control parent = this.Parent;
            while (parent != null && !(parent is Panel && parent.Name == "pnlContent"))
            {
                parent = parent.Parent;
            }
            if (parent != null)
            {
                parent.Controls.Clear();
                ucUserManagement uc = new ucUserManagement();
                uc.Dock = DockStyle.Fill;
                parent.Controls.Add(uc);
            }
        }

        private void btnUIAssignment_Click(object sender, EventArgs e)
        {
            Control parent = this.Parent;
            while (parent != null && !(parent is Panel && parent.Name == "pnlContent"))
            {
                parent = parent.Parent;
            }
            if (parent != null)
            {
                parent.Controls.Clear();
                ucProblemManagement uc = new ucProblemManagement();
                uc.Dock = DockStyle.Fill;
                parent.Controls.Add(uc);
            }
        }

        private void btnUILog_Click(object sender, EventArgs e)
        {
            Control parent = this.Parent;
            while (parent != null && !(parent is Panel && parent.Name == "pnlContent"))
            {
                parent = parent.Parent;
            }
            if (parent != null)
            {
                parent.Controls.Clear();
                ucSystemLogs uc = new ucSystemLogs();
                uc.Dock = DockStyle.Fill;
                parent.Controls.Add(uc);
            }
        }
    }
}