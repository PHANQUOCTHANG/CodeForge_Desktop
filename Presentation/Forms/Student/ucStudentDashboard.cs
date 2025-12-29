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

            // Giảm giật lag khi vẽ lại giao diện
            this.DoubleBuffered = true;

            // Khởi tạo services
            _submissionService = new SubmissionService();
            _problemService = new CodingProblemService();

            // Load tất cả dữ liệu
            LoadDashboardStatistics();
            LoadRecentSubmissions();

            // Đăng ký sự kiện tô màu
            dgvRecent.CellPainting += DgvRecent_CellPainting;

            // Đăng ký sự kiện nút
            if (btnActionList != null)
                btnActionList.Click += (s, e) => ProblemListClicked?.Invoke(this, EventArgs.Empty);

            if (btnActionHistory != null)
                btnActionHistory.Click += (s, e) => SubmissionsClicked?.Invoke(this, EventArgs.Empty);

            if (btnActionSettings != null)
                btnActionSettings.Click += (s, e) => SettingsClicked?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        /// Load thống kê dashboard và cập nhật các card statistics
        /// </summary>
        private void LoadDashboardStatistics()
        {
            try
            {
                Guid userId = GlobalStore.user.UserID;

                // Lấy dữ liệu submissions
                var allSubmissions = _submissionService.GetUserSubmissions(userId);
                var allProblems = _problemService.GetAll();

                if (allSubmissions == null) allSubmissions = new List<Submission>();
                if (allProblems == null) allProblems = new List<CodingProblem>();

                // ======== TÍNH TOÁN THỐNG KÊ ========
                
                // 1. Tổng số bài tập
                int totalProblems = allProblems.Count;

                // 2. Bài tập đã hoàn thành (Status = "Accepted")
                var solvedProblemIds = allSubmissions
                    .Where(s => s.Status == "Accepted")
                    .Select(s => s.ProblemID)
                    .Distinct()
                    .ToList();
                int solvedProblems = solvedProblemIds.Count;

                // 3. Bài tập đang làm (có submission nhưng chưa Accepted)
                var attemptedProblemIds = allSubmissions
                    .Select(s => s.ProblemID)
                    .Distinct()
                    .ToList();
                int inProgressProblems = attemptedProblemIds.Count - solvedProblems;

                // 4. Tính điểm trung bình
                double averageScore = 0;
                if (allSubmissions.Count > 0)
                {
                    var successfulSubmissions = allSubmissions
                        .Where(s => s.QuantityTest.HasValue && s.QuantityTest.Value > 0)
                        .ToList();

                    if (successfulSubmissions.Count > 0)
                    {
                        double totalPercentage = 0;
                        foreach (var submission in successfulSubmissions)
                        {
                            int passed = submission.QuantityTestPassed.GetValueOrDefault(0);
                            int total = submission.QuantityTest.Value;
                            totalPercentage += (passed * 100.0) / total;
                        }
                        averageScore = totalPercentage / successfulSubmissions.Count;
                    }
                }

                // ======== CẬP NHẬT CÁC LABEL ========

                // Card 1: Tổng số bài tập
                lblValTotal.Text = totalProblems.ToString();
                lblDescTotal.Text = "Tổng số bài tập";
                lblIconTotal.Text = "📄";

                // Card 2: Bài tập đã hoàn thành
                lblValComp.Text = solvedProblems.ToString();
                lblDescComp.Text = "Đã hoàn thành";
                lblIconComp.Text = "✓";
                lblIconComp.ForeColor = Color.FromArgb(76, 175, 80); // Xanh

                // Card 3: Bài tập đang làm
                lblValProg.Text = inProgressProblems.ToString();
                lblDescProg.Text = "Đang làm";
                lblIconProg.Text = "⏰";
                lblIconProg.ForeColor = Color.FromArgb(255, 152, 0); // Cam

                // Card 4: Điểm trung bình
                lblValAvg.Text = $"{averageScore:F0}%";
                lblDescAvg.Text = "Điểm trung bình";
                lblIconAvg.Text = "📈";
                lblIconAvg.ForeColor = Color.FromArgb(156, 39, 176); // Tím

                System.Diagnostics.Debug.WriteLine($"✓ Dashboard Statistics Loaded:");
                System.Diagnostics.Debug.WriteLine($"  - Total Problems: {totalProblems}");
                System.Diagnostics.Debug.WriteLine($"  - Solved: {solvedProblems}");
                System.Diagnostics.Debug.WriteLine($"  - In Progress: {inProgressProblems}");
                System.Diagnostics.Debug.WriteLine($"  - Average Score: {averageScore:F2}%");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Lỗi khi load thống kê: {ex.Message}")
;
            }
        }

        /// <summary>
        /// Load các bài tập được làm gần nhất (tối đa 6 bài)
        /// </summary>
        private void LoadRecentSubmissions()
        {
            try
            {
                dgvRecent.Rows.Clear();

                // Lấy ID user hiện tại
                Guid userId = GlobalStore.user.UserID;

                // Lấy các submission của user, sắp xếp theo thời gian submit gần nhất
                var allSubmissions = _submissionService.GetUserSubmissions(userId);
                
                if (allSubmissions == null || allSubmissions.Count == 0)
                {
                    dgvRecent.Rows.Add("", "Chưa có bài tập nào được nộp", "", "Chưa nộp");
                    return;
                }

                var recentSubmissions = allSubmissions
                    .OrderByDescending(s => s.SubmitTime)
                    .Take(6)
                    .ToList();

                // Lấy danh sách tất cả problems để map ID với tên
                var allProblems = _problemService.GetAll();
                if (allProblems == null) allProblems = new List<CodingProblem>();

                int index = 1;
                foreach (var submission in recentSubmissions)
                {
                    // Tìm tên bài tập từ ProblemID
                    var problem = allProblems.FirstOrDefault(p => p.ProblemID == submission.ProblemID);
                    string problemName = problem?.Title ?? "Bài tập không xác định";

                    // Xác định trạng thái hiển thị
                    string statusDisplay = submission.Status ?? "Chưa nộp";
                    string statusLower = statusDisplay.ToLower();

                    if (statusLower == "accepted")
                        statusDisplay = "✓ Đã chấp nhận";
                    else if (statusLower == "failed" || statusLower == "wrong answer")
                        statusDisplay = "✗ Sai đáp án";
                    else if (statusLower.Contains("runtime"))
                        statusDisplay = "⚠️ Lỗi runtime";
                    else if (statusLower.Contains("time limit"))
                        statusDisplay = "⏱️ Vượt quá thời gian";
                    else if (statusLower == "saved")
                        statusDisplay = "⏳ Đã lưu";
                    else
                        statusDisplay = "⏳ " + statusDisplay;

                    // Tính phần trăm test case đã pass
                    string scorePercentage = "-";
                    if (submission.QuantityTest.HasValue && submission.QuantityTest.Value > 0)
                    {
                        int percentage = (submission.QuantityTestPassed.GetValueOrDefault(0) * 100) / submission.QuantityTest.Value;
                        scorePercentage = $"{percentage}%";
                    }

                    // Thêm vào DataGridView
                    dgvRecent.Rows.Add(
                        index,
                        problemName,
                        statusDisplay,
                        scorePercentage
                    );

                    index++;
                }

                System.Diagnostics.Debug.WriteLine($"✓ Loaded {recentSubmissions.Count} recent submissions");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Lỗi khi tải recent submissions: {ex.Message}");
                MessageBox.Show($"Lỗi khi tải dữ liệu: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                dgvRecent.Rows.Add("", "Lỗi khi tải dữ liệu", "", "Lỗi");
            }
        }

        /// <summary>
        /// Tô màu cho các cell trong DataGridView
        /// </summary>
        private void DgvRecent_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            try
            {
                // Tô màu cột Status (Index = 2)
                if (e.RowIndex >= 0 && e.ColumnIndex == 2 && e.Value != null)
                {
                    string status = e.Value.ToString();
                    Color textColor = Color.FromArgb(100, 100, 100);
                    Color backgroundColor = Color.White;

                    // Phân loại theo trạng thái
                    if (status.Contains("✓"))
                    {
                        textColor = Color.FromArgb(76, 175, 80); // Xanh lá - Accepted
                        backgroundColor = Color.FromArgb(232, 245, 233); // Xanh nhạt
                    }
                    else if (status.Contains("✗"))
                    {
                        textColor = Color.FromArgb(244, 67, 54); // Đỏ - Wrong Answer
                        backgroundColor = Color.FromArgb(255, 235, 238); // Đỏ nhạt
                    }
                    else if (status.Contains("⚠️") || status.Contains("Runtime"))
                    {
                        textColor = Color.FromArgb(255, 152, 0); // Cam - Runtime Error
                        backgroundColor = Color.FromArgb(255, 243, 224); // Cam nhạt
                    }
                    else if (status.Contains("⏱️"))
                    {
                        textColor = Color.FromArgb(52, 152, 219); // Xanh dương - Time Limit
                        backgroundColor = Color.FromArgb(230, 242, 255); // Xanh dương nhạt
                    }
                    else
                    {
                        textColor = Color.FromArgb(155, 89, 182); // Tím - Đang chờ
                        backgroundColor = Color.FromArgb(243, 228, 250); // Tím nhạt
                    }

                    // Vẽ nền
                    e.Graphics.FillRectangle(new SolidBrush(backgroundColor), e.CellBounds);

                    // Vẽ text
                    TextRenderer.DrawText(
                        e.Graphics,
                        status,
                        new Font("Segoe UI", 9, FontStyle.Bold),
                        e.CellBounds,
                        textColor,
                        TextFormatFlags.VerticalCenter | TextFormatFlags.Left | TextFormatFlags.NoPadding
                    );

                    e.Handled = true;
                }

                // Tô màu cột Score (Index = 3)
                if (e.RowIndex >= 0 && e.ColumnIndex == 3 && e.Value != null)
                {
                    string score = e.Value.ToString();
                    Color textColor = Color.FromArgb(100, 100, 100);

                    if (score != "-")
                    {
                        if (int.TryParse(score.Replace("%", ""), out int percentage))
                        {
                            if (percentage == 100)
                                textColor = Color.FromArgb(76, 175, 80); // Xanh lá - 100%
                            else if (percentage >= 50)
                                textColor = Color.FromArgb(255, 152, 0); // Cam - >= 50%
                            else
                                textColor = Color.FromArgb(244, 67, 54); // Đỏ - < 50%
                        }
                    }

                    e.Paint(e.CellBounds, DataGridViewPaintParts.All & ~DataGridViewPaintParts.ContentForeground);

                    TextRenderer.DrawText(
                        e.Graphics,
                        score,
                        new Font("Segoe UI", 10, FontStyle.Bold),
                        e.CellBounds,
                        textColor,
                        TextFormatFlags.VerticalCenter | TextFormatFlags.Left
                    );

                    e.Handled = true;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error in CellPainting: {ex.Message}");
            }
        }

        /// <summary>
        /// Refresh dữ liệu khi form được focus lại
        /// </summary>
        public void RefreshData()
        {
            System.Diagnostics.Debug.WriteLine("🔄 Refreshing dashboard data...");
            LoadDashboardStatistics();
            LoadRecentSubmissions();
        }
    }
}