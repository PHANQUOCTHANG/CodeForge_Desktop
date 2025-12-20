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
        /// Load thống kê dashboard (nếu có các control thống kê)
        /// </summary>
        private void LoadDashboardStatistics()
        {
            try
            {
                Guid userId = GlobalStore.user.UserID;

                // Lấy dữ liệu submissions
                var allSubmissions = _submissionService.GetUserSubmissions(userId);
                var allProblems = _problemService.GetAll();

                // Tính toán thống kê
                int totalProblems = allProblems.Count;
                int solvedProblems = allSubmissions.Where(s => s.Status == "Accepted").Select(s => s.ProblemID).Distinct().Count();
                int attemptedProblems = allSubmissions.Select(s => s.ProblemID).Distinct().Count();
                int totalSubmissions = allSubmissions.Count;

                // Cập nhật các label thống kê nếu tồn tại
                // Ví dụ: lblTotalProblems.Text = $"Tổng bài: {totalProblems}";
                // Ví dụ: lblSolvedProblems.Text = $"Đã giải: {solvedProblems}";
                // Ví dụ: lblAttemptedProblems.Text = $"Đã làm: {attemptedProblems}";
                // Ví dụ: lblTotalSubmissions.Text = $"Tổng nộp: {totalSubmissions}";

                // Nếu bạn có các control này trong Designer, hãy uncomment và điều chỉnh tên
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Lỗi khi load thống kê: {ex.Message}");
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
                var recentSubmissions = allSubmissions
                    .OrderByDescending(s => s.SubmitTime)
                    .Take(6)
                    .ToList();

                if (recentSubmissions.Count == 0)
                {
                    dgvRecent.Rows.Add("", "Chưa có bài tập nào được nộp", "", "Chưa nộp", "-");
                    return;
                }

                // Lấy danh sách tất cả problems để map ID với tên
                var allProblems = _problemService.GetAll();

                int index = 1;
                foreach (var submission in recentSubmissions)
                {
                    // Tìm tên bài tập từ ProblemID
                    var problem = allProblems.FirstOrDefault(p => p.ProblemID == submission.ProblemID);
                    string problemName = problem?.Title ?? "Bài tập không xác định";

                    // Xác định trạng thái
                    string status = submission.Status ?? "Chưa nộp";
                    if (status == "Accepted")
                        status = "✓ Đã chấp nhận";
                    else if (status == "Wrong Answer")
                        status = "✗ Sai đáp án";
                    else if (status == "Runtime Error")
                        status = "⚠️ Lỗi runtime";
                    else if (status == "Time Limit Exceeded")
                        status = "⏱️ Vượt quá thời gian";
                    else
                        status = "⏳ " + status;

                    // Tính phần trăm test case đã pass
                    string scorePercentage = "-";
                    if (submission.QuantityTest.HasValue && submission.QuantityTest.Value > 0)
                    {
                        int percentage = (submission.QuantityTestPassed.GetValueOrDefault(0) * 100) / submission.QuantityTest.Value;
                        scorePercentage = $"{percentage}";
                    }   

                    // Format thời gian submit
                    string submitTime = submission.SubmitTime.ToString("HH:mm dd/MM/yyyy");

                    // Thêm vào DataGridView (Bỏ cột Deadline)
                    // Cột: #, Tên bài tập, Thời gian nộp, Trạng thái, Điểm
                    dgvRecent.Rows.Add(
                        index,
                        problemName,
                        status,
                        scorePercentage
                    );

                    index++;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải dữ liệu: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                dgvRecent.Rows.Add("", "Lỗi khi tải dữ liệu", "", "Lỗi", "-");
            }
        }

        /// <summary>
        /// Tô màu cột Trạng thái
        /// </summary>
        private void DgvRecent_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            // Tô màu cột Trạng thái (Index = 3)
            if (e.RowIndex >= 0 && e.ColumnIndex == 3 && e.Value != null)
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

            // Tô màu cột Score (Index = 4) - Căn lề trái
            if (e.RowIndex >= 0 && e.ColumnIndex == 4 && e.Value != null)
            {
                string score = e.Value.ToString();
                Color textColor = Color.FromArgb(100, 100, 100);

                if (score != "-")
                {
                    if (int.TryParse(score.Replace("%", ""), out int percentage))
                    {
                        if (percentage == 100)
                            textColor = Color.FromArgb(76, 175, 80); // Xanh lá
                        else if (percentage >= 50)
                            textColor = Color.FromArgb(255, 152, 0); // Cam
                        else
                            textColor = Color.FromArgb(244, 67, 54); // Đỏ
                    }
                }

                e.Paint(e.CellBounds, DataGridViewPaintParts.All & ~DataGridViewPaintParts.ContentForeground);
                
                // Căn lề trái thay vì căn giữa
                TextRenderer.DrawText(
                    e.Graphics,
                    score,
                    new Font("Segoe UI", 10, FontStyle.Bold),
                    e.CellBounds,
                    textColor,
                    TextFormatFlags.VerticalCenter | TextFormatFlags.Left // Đổi từ HorizontalCenter sang Left
                );

                e.Handled = true;
            }
        }

        /// <summary>
        /// Refresh dữ liệu khi form được focus lại
        /// </summary>
        public void RefreshData()
        {
            LoadDashboardStatistics();
            LoadRecentSubmissions();
        }
    }
}