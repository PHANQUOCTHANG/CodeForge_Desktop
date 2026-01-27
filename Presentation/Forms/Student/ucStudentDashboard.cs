using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using CodeForge_Desktop.Business.Helpers;
using CodeForge_Desktop.Business.Services;
using CodeForge_Desktop.DataAccess.Entities;
using CodeForge_Desktop.Business.Interfaces;

namespace CodeForge_Desktop.Presentation.Forms.Student
{
    public partial class ucStudentDashboard : UserControl
    {
        public event EventHandler ProblemListClicked;
        public event EventHandler SubmissionsClicked;
        public event EventHandler SettingsClicked;

        private ISubmissionService _submissionService;
        private ICodingProblemService _problemService;

        public ucStudentDashboard()
        {
            InitializeComponent();

            // Giảm giật lag
            this.DoubleBuffered = true;

            // Khởi tạo Services
            _submissionService = new SubmissionService();
            _problemService = new CodingProblemService();

            // Setup Greeting
            try
            {
                var username = GlobalStore.user?.Username ?? "Student";
                lblGreeting.Text = $"Xin chào, {username}";
            }
            catch { lblGreeting.Text = "Xin chào"; }

            LoadDashboardStatistics();
            LoadRecentSubmissions();

            // Gắn sự kiện
            dgvRecent.CellPainting += DgvRecent_CellPainting;

            btnActionList.Click += (s, e) => ProblemListClicked?.Invoke(this, EventArgs.Empty);
            btnActionHistory.Click += (s, e) => SubmissionsClicked?.Invoke(this, EventArgs.Empty);
            btnActionSettings.Click += (s, e) => SettingsClicked?.Invoke(this, EventArgs.Empty);

            // btnViewAll click cũng chuyển sang danh sách bài tập
            btnViewAll.Click += (s, e) => ProblemListClicked?.Invoke(this, EventArgs.Empty);
        }

        private void LoadDashboardStatistics()
        {
            try
            {
                Guid userId = GlobalStore.user.UserID;
                var allSubmissions = _submissionService.GetUserSubmissions(userId) ?? new List<Submission>();
                var allProblems = _problemService.GetAll() ?? new List<CodingProblem>();

                int totalProblems = allProblems.Count;

                var solvedProblemIds = allSubmissions.Where(s => s.Status == "Accepted").Select(s => s.ProblemID).Distinct().ToList();
                int solvedProblems = solvedProblemIds.Count;

                var attemptedProblemIds = allSubmissions.Select(s => s.ProblemID).Distinct().ToList();
                int inProgressProblems = attemptedProblemIds.Count - solvedProblems;

                double averageScore = 0;
                if (allSubmissions.Count > 0)
                {
                    var validSubs = allSubmissions.Where(s => s.QuantityTest.HasValue && s.QuantityTest.Value > 0).ToList();
                    if (validSubs.Count > 0)
                    {
                        double totalPercent = validSubs.Sum(s => (s.QuantityTestPassed.GetValueOrDefault(0) * 100.0) / s.QuantityTest.Value);
                        averageScore = totalPercent / validSubs.Count;
                    }
                }

                // Cập nhật UI
                lblValTotal.Text = totalProblems.ToString();
                lblValComp.Text = solvedProblems.ToString();
                lblValProg.Text = inProgressProblems.ToString();
                lblValAvg.Text = $"{averageScore:F0}%";
            }
            catch (Exception ex)
            {
                // System.Diagnostics.Debug.WriteLine(ex.Message);
            }
        }

        private void LoadRecentSubmissions()
        {
            try
            {
                dgvRecent.Rows.Clear();
                Guid userId = GlobalStore.user.UserID;
                var allSubmissions = _submissionService.GetUserSubmissions(userId);

                if (allSubmissions == null || allSubmissions.Count == 0) return;

                var recent = allSubmissions.OrderByDescending(s => s.SubmitTime).Take(4).ToList();
                var allProblems = _problemService.GetAll();

                int idx = 1;
                foreach (var sub in recent)
                {
                    var prob = allProblems.FirstOrDefault(p => p.ProblemID == sub.ProblemID);
                    string name = prob?.Title ?? "Unknown";
                    string status = sub.Status ?? "Chưa nộp";

                    string score = "-";
                    if (sub.QuantityTest.HasValue && sub.QuantityTest > 0)
                    {
                        int p = (sub.QuantityTestPassed.GetValueOrDefault(0) * 100) / sub.QuantityTest.Value;
                        score = $"{p}%";
                    }

                    // Deadline: Lấy từ problem nếu có, hoặc để trống (vì DB cũ có thể chưa có cột Deadline)
                    // Ở đây tôi dùng CreatedAt làm giả deadline để hiển thị cho đẹp, bạn sửa lại sau.
                    string deadline = prob?.CreatedAt.AddDays(7).ToString("yyyy-MM-dd") ?? "-";

                    dgvRecent.Rows.Add(idx++, name, deadline, status, score);
                }
                dgvRecent.ClearSelection();
            }
            catch { }
        }

        private void DgvRecent_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex < 0) return;

            // Tô màu cột Trạng thái (Index = 3)
            if (e.ColumnIndex == 3 && e.Value != null)
            {
                string status = e.Value.ToString();
                Color color = Color.Gray;

                if (status == "Accepted" || status == "Đã nộp") color = Color.FromArgb(76, 175, 80); // Green
                else if (status == "Wrong Answer" || status == "Chưa nộp") color = Color.FromArgb(255, 152, 0); // Orange
                else if (status.Contains("Time") || status.Contains("Runtime")) color = Color.Red;

                e.Paint(e.CellBounds, DataGridViewPaintParts.All & ~DataGridViewPaintParts.ContentForeground);
                TextRenderer.DrawText(e.Graphics, status, e.CellStyle.Font, e.CellBounds, color, TextFormatFlags.VerticalCenter | TextFormatFlags.Left);
                e.Handled = true;
            }
        }
    }
}