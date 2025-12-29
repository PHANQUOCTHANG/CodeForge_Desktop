using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CodeForge_Desktop.Business.Interfaces;
using CodeForge_Desktop.Business.Services;
using CodeForge_Desktop.DataAccess.Entities;
using CodeForge_Desktop.DataAccess.Repositories;

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

            // ✅ Khởi tạo services với dependencies
            var userRepository = new UserRepository();
            _userService = new UserService(userRepository);
            _problemService = new CodingProblemService();
            _submissionService = new SubmissionService();

            // Load dữ liệu dashboard
            LoadDashboardStatistics();
            LoadRecentActivity();

            // Ẩn row headers
            dataGridView1.RowHeadersVisible = false;
        }

        /// <summary>
        /// Load thống kê tổng quan cho dashboard
        /// </summary>
        private void LoadDashboardStatistics()
        {
            try
            {
                // ✅ 1. Tổng số Users - Sử dụng GetAllUsers()
                var allUsers = _userService.GetAllUsers();
                int totalUsers = allUsers?.Count ?? 0;
                lblUserNumber.Text = totalUsers.ToString();

                // 2. Tổng số Assignments (Bài tập)
                var allProblems = _problemService.GetAll();
                int totalAssignments = allProblems?.Count ?? 0;
                lnlAssignmentNumber.Text = totalAssignments.ToString();

                // 3. Submissions hôm nay
                var allSubmissions = _submissionService.GetAllSubmissions();
                int submissionsToday = 0;

                if (allSubmissions != null)
                {
                    var today = DateTime.Today;
                    submissionsToday = allSubmissions
                        .Where(s => s.SubmitTime.Date == today)
                        .Count();
                }

                lblSubmissionNumber.Text = submissionsToday.ToString();

                // 4. Tỷ lệ hoàn thành (Accepted submissions / Total submissions)
                double completionRate = 0;

                if (allSubmissions != null && allSubmissions.Count > 0)
                {
                    int acceptedCount = allSubmissions
                        .Where(s => s.Status == "Accepted")
                        .Count();

                    completionRate = (acceptedCount * 100.0) / allSubmissions.Count;
                }

                lblRateNumber.Text = $"{completionRate:F1}%";

                System.Diagnostics.Debug.WriteLine($"✓ Dashboard Statistics Loaded:");
                System.Diagnostics.Debug.WriteLine($"  - Total Users: {totalUsers}");
                System.Diagnostics.Debug.WriteLine($"  - Total Assignments: {totalAssignments}");
                System.Diagnostics.Debug.WriteLine($"  - Submissions Today: {submissionsToday}");
                System.Diagnostics.Debug.WriteLine($"  - Completion Rate: {completionRate:F1}%");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Lỗi khi load thống kê dashboard: {ex.Message}");
                MessageBox.Show($"Lỗi khi tải thống kê: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Load hoạt động gần đây (Recent Activity)
        /// </summary>
        private void LoadRecentActivity()
        {
            try
            {
                dataGridView1.Rows.Clear();

                // Lấy tất cả submissions sắp xếp theo thời gian gần đây nhất
                var allSubmissions = _submissionService.GetAllSubmissions();

                if (allSubmissions == null || allSubmissions.Count == 0)
                {
                    dataGridView1.Rows.Add("", "Không có hoạt động", "", "", "");
                    return;
                }

                // Lấy 10 submission gần đây nhất
                var recentSubmissions = allSubmissions
                    .OrderByDescending(s => s.SubmitTime)
                    .Take(10)
                    .ToList();

                // ✅ Lấy danh sách users - Sử dụng GetAllUsers()
                var allUsers = _userService.GetAllUsers() ?? new List<User>();
                var allProblems = _problemService.GetAll() ?? new List<CodingProblem>();

                int index = 1;
                foreach (var submission in recentSubmissions)
                {
                    // Tìm user name
                    var user = allUsers.FirstOrDefault(u => u.UserID == submission.UserID);
                    string userName = user?.Username ?? "Unknown User";

                    // Tìm problem name
                    var problem = allProblems.FirstOrDefault(p => p.ProblemID == submission.ProblemID);
                    string problemName = problem?.Title ?? "Unknown Problem";

                    // Xác định hành động
                    string action = submission.Status ?? "Unknown";
                    if (action.Equals("Accepted", StringComparison.OrdinalIgnoreCase))
                        action = "✓ Accepted";
                    else if (action.Equals("Failed", StringComparison.OrdinalIgnoreCase))
                        action = "✗ Failed";
                    else if (action.Equals("Saved", StringComparison.OrdinalIgnoreCase))
                        action = "💾 Saved";

                    // Chi tiết (test cases passed/total)
                    string details = "-";
                    if (submission.QuantityTest.HasValue && submission.QuantityTest.Value > 0)
                    {
                        int passed = submission.QuantityTestPassed.GetValueOrDefault(0);
                        int total = submission.QuantityTest.Value;
                        details = $"{passed}/{total}";
                    }

                    // Thời gian submit
                    string submitTime = submission.SubmitTime.ToString("HH:mm:ss dd/MM/yyyy");

                    // Thêm vào DataGridView
                    dataGridView1.Rows.Add(
                        index,
                        userName,
                        action,
                        $"{problemName} ({details})",
                        submitTime
                    );

                    index++;
                }

                System.Diagnostics.Debug.WriteLine($"✓ Loaded {recentSubmissions.Count} recent activities");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Lỗi khi tải recent activity: {ex.Message}");
                MessageBox.Show($"Lỗi khi tải hoạt động gần đây: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Refresh dữ liệu dashboard
        /// </summary>
        public void RefreshDashboard()
        {
            System.Diagnostics.Debug.WriteLine("🔄 Refreshing admin dashboard data...");
            LoadDashboardStatistics();
            LoadRecentActivity();
        }

        private void btnUIUser_Click(object sender, EventArgs e)
        {
            pnlContent.Controls.Clear();
            ucUserManagement ucUserManagement = new ucUserManagement();
            ucUserManagement.Dock = DockStyle.Fill;
            pnlContent.Controls.Add(ucUserManagement);
        }

        private void btnUIAssignment_Click(object sender, EventArgs e)
        {
            pnlContent.Controls.Clear();
            ucProblemManagement ucProblemManagement = new ucProblemManagement();
            ucProblemManagement.Dock = DockStyle.Fill;
            pnlContent.Controls.Add(ucProblemManagement);
        }

        private void btnUILog_Click(object sender, EventArgs e)
        {
            pnlContent.Controls.Clear();
            ucSystemLogs ucSystemLogs = new ucSystemLogs();
            ucSystemLogs.Dock = DockStyle.Fill;
            pnlContent.Controls.Add(ucSystemLogs);
        }
    }
}
