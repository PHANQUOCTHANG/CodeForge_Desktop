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
    public partial class ucSubmissions : UserControl
    {
        private ISubmissionService _submissionService;
        private ICodingProblemService _problemService;
        private List<Submission> _allSubmissions;
        private List<CodingProblem> _allProblems;

        public event EventHandler BackButtonClicked;

        public ucSubmissions()
        {
            InitializeComponent();
            _submissionService = new SubmissionService();
            _problemService = new CodingProblemService();
            
            SetupComboBoxes();
            SetupDataGridViewStyles();

            // Đăng ký sự kiện
            dgvSubmissions.CellPainting += DgvSubmissions_CellPainting;
            dgvSubmissions.CellClick += DgvSubmissions_CellClick;
           
            btnApplyFilter.Click += (s, e) => ApplyFilters();

            // Load dữ liệu khi control được load
            this.Load += (s, e) => LoadSubmissions();
        }

        /// <summary>
        /// Xử lý sự kiện click vào DataGridView
        /// </summary>
        private void DgvSubmissions_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex == dgvSubmissions.Columns["colView"].Index)
            {
                try
                {
                    int rowIndex = e.RowIndex;
                    if (rowIndex < _allSubmissions.Count)
                    {
                        Submission submission = _allSubmissions[rowIndex];
                        ShowSubmissionCode(submission);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi khi mở code: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        /// <summary>
        /// Hiển thị dialog với code của submission
        /// </summary>
        private void ShowSubmissionCode(Submission submission)
        {
            var problem = _allProblems?.FirstOrDefault(p => p.ProblemID == submission.ProblemID);
            string problemTitle = problem?.Title ?? "Không xác định";

            Form codeForm = new Form
            {
                Text = $"Code Submission - {problemTitle}",
                Width = 1000,
                Height = 700,
                StartPosition = FormStartPosition.CenterParent,
                Icon = this.FindForm()?.Icon,
                BackColor = Color.FromArgb(245, 245, 245)
            };

            // ===== HEADER =====
            Panel pnlHeader = new Panel
            {
                Dock = DockStyle.Top,
                Height = 70,
                BackColor = Color.FromArgb(41, 128, 185),
                BorderStyle = BorderStyle.None
            };

            Label lblTitle = new Label
            {
                Text = $"📄 {problemTitle}",
                Dock = DockStyle.Top,
                Height = 35,
                TextAlign = ContentAlignment.MiddleLeft,
                Padding = new Padding(20, 0, 0, 0),
                Font = new Font("Segoe UI", 14, FontStyle.Bold),
                ForeColor = Color.White
            };
            pnlHeader.Controls.Add(lblTitle);

            Label lblInfo = new Label
            {
                Text = $"🗣️ {submission.Language.ToUpper()} | ✓ {submission.Status} | ⏰ {submission.SubmitTime:dd/MM/yyyy HH:mm:ss} | 💾 {submission.MemoryUsed ?? 0}MB | ⏱️ {submission.ExecutionTime ?? 0}ms",
                Dock = DockStyle.Bottom,
                Height = 35,
                TextAlign = ContentAlignment.MiddleLeft,
                Padding = new Padding(20, 0, 0, 0),
                Font = new Font("Segoe UI", 9),
                ForeColor = Color.FromArgb(230, 230, 230)
            };
            pnlHeader.Controls.Add(lblInfo);

            // ===== CODE AREA =====
            Panel pnlCodeContainer = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.FromArgb(245, 245, 245),
                Padding = new Padding(15)
            };

            RichTextBox codeTextBox = new RichTextBox
            {
                Dock = DockStyle.Fill,
                Text = submission.Code,
                ReadOnly = true,
                Font = new Font("Consolas", 11),
                BackColor = Color.FromArgb(30, 30, 30),
                ForeColor = Color.FromArgb(200, 200, 200),
                BorderStyle = BorderStyle.FixedSingle,
                Margin = new Padding(0)
            };

            pnlCodeContainer.Controls.Add(codeTextBox);

            // ===== FOOTER =====
            Panel pnlFooter = new Panel
            {
                Dock = DockStyle.Bottom,
                Height = 60,
                BackColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle,
                Padding = new Padding(15)
            };

            // Nút Sao chép
            Button btnCopy = new Button
            {
                Text = "📋 Sao chép Code",
                Location = new Point(15, 15),
                Width = 140,
                Height = 30,
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.FromArgb(52, 152, 219),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            btnCopy.FlatAppearance.BorderSize = 0;
            btnCopy.Click += (s, e) =>
            {
                Clipboard.SetText(submission.Code);
                MessageBox.Show("✓ Code đã được sao chép vào clipboard!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
            };
            pnlFooter.Controls.Add(btnCopy);

            // Nút Tải xuống
            Button btnDownload = new Button
            {
                Text = "⬇️ Tải xuống",
                Location = new Point(165, 15),
                Width = 140,
                Height = 30,
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.FromArgb(46, 204, 113),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            btnDownload.FlatAppearance.BorderSize = 0;
            btnDownload.Click += (s, e) => DownloadSubmissionCode(submission, problemTitle);
            pnlFooter.Controls.Add(btnDownload);

            // Nút Đóng
            Button btnClose = new Button
            {
                Text = "✕ Đóng",
                Location = new Point(codeForm.Width - 145, 15),
                Width = 130,
                Height = 30,
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.FromArgb(149, 165, 166),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                Cursor = Cursors.Hand,
                DialogResult = DialogResult.OK
            };
            btnClose.FlatAppearance.BorderSize = 0;
            pnlFooter.Controls.Add(btnClose);

            codeForm.Controls.Add(pnlCodeContainer);
            codeForm.Controls.Add(pnlHeader);
            codeForm.Controls.Add(pnlFooter);

            codeForm.ShowDialog(this.FindForm());
        }

        /// <summary>
        /// Tải xuống code submission
        /// </summary>
        private void DownloadSubmissionCode(Submission submission, string problemTitle)
        {
            try
            {
                SaveFileDialog saveDialog = new SaveFileDialog
                {
                    FileName = $"{problemTitle}_{submission.SubmitTime:yyyyMMdd_HHmmss}.{GetFileExtension(submission.Language)}",
                    Filter = $"{submission.Language.ToUpper()} Files (*.{GetFileExtension(submission.Language)})|*.{GetFileExtension(submission.Language)}|All Files (*.*)|*.*",
                    DefaultExt = GetFileExtension(submission.Language)
                };

                if (saveDialog.ShowDialog() == DialogResult.OK)
                {
                    System.IO.File.WriteAllText(saveDialog.FileName, submission.Code);
                    MessageBox.Show($"✓ Code đã được tải xuống thành công!\n📁 {saveDialog.FileName}", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải xuống: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Lấy đuôi file theo ngôn ngữ
        /// </summary>
        private string GetFileExtension(string language)
        {
            return language?.ToLower() switch
            {
                "c++" => "cpp",
                "python" => "py",
                "javascript" => "js",
                _ => "txt"
            };
        }

        /// <summary>
        /// Load tất cả submissions của user hiện tại
        /// </summary>
        private void LoadSubmissions()
        {
            try
            {
                Guid userId = GlobalStore.user.UserID;
                _allSubmissions = _submissionService.GetUserSubmissions(userId);
                _allProblems = _problemService.GetAll();

                PopulateProblemComboBox();
                PopulateStatusComboBox();
                BindSubmissionsToGrid(_allSubmissions);
                UpdateStatistics(_allSubmissions);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải submissions: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Populate ComboBox với danh sách bài tập
        /// </summary>
        private void PopulateProblemComboBox()
        {
            cmbProblems.Items.Clear();
            cmbProblems.Items.Add("Tất cả bài tập");

            if (_allProblems != null)
            {
                foreach (var problem in _allProblems)
                {
                    cmbProblems.Items.Add(problem.Title);
                }
            }

            cmbProblems.SelectedIndex = 0;
        }

        /// <summary>
        /// Populate ComboBox với danh sách trạng thái
        /// </summary>
        private void PopulateStatusComboBox()
        {
            cmbStatus.Items.Clear();
            cmbStatus.Items.Add("Tất cả trạng thái");
            cmbStatus.Items.Add("Accepted");
            cmbStatus.Items.Add("Wrong Answer");
            cmbStatus.Items.Add("Runtime Error");
            cmbStatus.Items.Add("Time Limit Exceeded");

            cmbStatus.SelectedIndex = 0;
        }

        /// <summary>
        /// Bind submissions vào DataGridView
        /// </summary>
        private void BindSubmissionsToGrid(List<Submission> submissions)
        {
            dgvSubmissions.Rows.Clear();

            if (submissions == null || submissions.Count == 0)
            {
                lblSummary.Text = "Chưa có bài nộp nào";
                return;
            }

            int rowIndex = 1;
            foreach (var submission in submissions)
            {
                try
                {
                    var problem = _allProblems?.FirstOrDefault(p => p.ProblemID == submission.ProblemID);
                    string problemName = problem?.Title ?? "Không xác định";

                    string submitTime = submission.SubmitTime.ToString("dd/MM/yyyy HH:mm:ss");
                    string status = submission.Status ?? "Unknown";

                    int score = 0;
                    if (status == "Accepted" && submission.QuantityTestPassed.HasValue && submission.QuantityTest.HasValue)
                    {
                        score = (submission.QuantityTestPassed.Value * 100) / submission.QuantityTest.Value;
                    }

                    string testCases = $"{submission.QuantityTestPassed ?? 0}/{submission.QuantityTest ?? 0}";

                    dgvSubmissions.Rows.Add(
                        rowIndex,
                        problemName,
                        submitTime,
                        status,
                        score,
                        testCases,
                        "Xem"
                    );

                    rowIndex++;
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"Lỗi khi thêm row: {ex.Message}");
                }
            }

            UpdateSummary(submissions);
        }

        /// <summary>
        /// Áp dụng bộ lọc
        /// </summary>
        private void ApplyFilters()
        {
            try
            {
                var filtered = _allSubmissions;

                if (cmbProblems.SelectedIndex > 0)
                {
                    string selectedProblem = cmbProblems.SelectedItem.ToString();
                    var problem = _allProblems.FirstOrDefault(p => p.Title == selectedProblem);
                    if (problem != null)
                    {
                        filtered = filtered.Where(s => s.ProblemID == problem.ProblemID).ToList();
                    }
                }

                if (cmbStatus.SelectedIndex > 0)
                {
                    string selectedStatus = cmbStatus.SelectedItem.ToString();
                    filtered = filtered.Where(s => s.Status == selectedStatus).ToList();
                }

                BindSubmissionsToGrid(filtered);
                UpdateStatistics(filtered);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi áp dụng lọc: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Cập nhật summary
        /// </summary>
        private void UpdateSummary(List<Submission> submissions)
        {
            int total = submissions.Count;
            int accepted = submissions.Count(s => s.Status == "Accepted");

            lblSummary.Text = $"📊 Tổng: {total} | ✓ Accepted: {accepted} | Tỉ lệ: {(total > 0 ? (accepted * 100 / total) : 0)}%";
        }

        /// <summary>
        /// Cập nhật thống kê
        /// </summary>
        private void UpdateStatistics(List<Submission> submissions)
        {
            int total = submissions.Count;
            int accepted = submissions.Count(s => s.Status == "Accepted");
            int wrongAnswer = submissions.Count(s => s.Status == "Wrong Answer");
            int errors = submissions.Count(s => s.Status != "Accepted" && s.Status != "Wrong Answer");

            lblStatTotalValue.Text = total.ToString();
            lblStatAcceptedValue.Text = accepted.ToString();
            lblStatWAValue.Text = wrongAnswer.ToString();
            lblStatErrorValue.Text = errors.ToString();
        }

        /// <summary>
        /// Hàm tùy chỉnh giao diện cho DataGridView
        /// </summary>
        private void SetupDataGridViewStyles()
        {
            dgvSubmissions.EnableHeadersVisualStyles = false;
            dgvSubmissions.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(52, 73, 94);
            dgvSubmissions.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvSubmissions.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 11, FontStyle.Bold);
            dgvSubmissions.ColumnHeadersDefaultCellStyle.Padding = new Padding(0, 5, 0, 5);
            dgvSubmissions.ColumnHeadersHeight = 40;

            dgvSubmissions.BorderStyle = BorderStyle.None;
            dgvSubmissions.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgvSubmissions.DefaultCellStyle.Font = new Font("Segoe UI", 10);
            dgvSubmissions.DefaultCellStyle.ForeColor = Color.FromArgb(64, 64, 64);
            dgvSubmissions.DefaultCellStyle.SelectionBackColor = Color.FromArgb(52, 152, 219);
            dgvSubmissions.DefaultCellStyle.SelectionForeColor = Color.White;
            dgvSubmissions.RowTemplate.Height = 35;
            dgvSubmissions.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(249, 249, 249);
        }

        private void SetupComboBoxes()
        {
            int controlPadding = (pnlFilters.Height - cmbProblems.Height) / 2;
            if (controlPadding > 0)
            {
                cmbProblems.Margin = new Padding(3, controlPadding, 3, 3);
                cmbStatus.Margin = new Padding(3, controlPadding, 3, 3);
            }
        }

        private void DgvSubmissions_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex == dgvSubmissions.Columns["colStatus"].Index)
            {
                if (e.Value != null)
                {
                    string status = e.Value.ToString();
                    Color textColor = Color.Black;
                    string icon = "";

                    switch (status)
                    {
                        case "Accepted":
                            textColor = Color.FromArgb(46, 204, 113);
                            icon = "✓ ";
                            break;
                        case "Wrong Answer":
                            textColor = Color.FromArgb(231, 76, 60);
                            icon = "✗ ";
                            break;
                        case "Time Limit Exceeded":
                            textColor = Color.FromArgb(52, 152, 219);
                            icon = "⏱️ ";
                            break;
                        case "Runtime Error":
                            textColor = Color.FromArgb(230, 126, 34);
                            icon = "⚠️ ";
                            break;
                    }

                    e.Paint(e.CellBounds, DataGridViewPaintParts.All & ~DataGridViewPaintParts.ContentForeground);
                    TextRenderer.DrawText(e.Graphics, icon + status, new Font("Segoe UI", 10, FontStyle.Bold), e.CellBounds, textColor, TextFormatFlags.VerticalCenter | TextFormatFlags.Left | TextFormatFlags.NoPadding);
                    e.Handled = true;
                }
            }

            // Vẽ nút "Xem" với styling
            if (e.RowIndex >= 0 && e.ColumnIndex == dgvSubmissions.Columns["colView"].Index)
            {
                e.Paint(e.CellBounds, DataGridViewPaintParts.All);
                var buttonBounds = new Rectangle(e.CellBounds.X + 5, e.CellBounds.Y + 5, e.CellBounds.Width - 10, e.CellBounds.Height - 10);
                e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(52, 152, 219)), buttonBounds);
                TextRenderer.DrawText(e.Graphics, "👁️ Xem", new Font("Segoe UI", 9, FontStyle.Bold), buttonBounds, Color.White, TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
                e.Handled = true;
            }
        }
    }
}