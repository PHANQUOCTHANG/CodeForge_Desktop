using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using CodeForge_Desktop.Business.Helpers;
using CodeForge_Desktop.Business.Interfaces;
using CodeForge_Desktop.Business.Models;
using CodeForge_Desktop.Business.Services;
using CodeForge_Desktop.DataAccess.Entities;
using CodeForge_Desktop.Presentation.Controls;
using ScintillaNET;

namespace CodeForge_Desktop.Presentation.Forms.Student
{
    public partial class ucProblemDetail : UserControl
    {
        private ICodingProblemService _problemService;
        private ITestCaseService _testCaseService;
        private ISubmissionService _submissionService;
        private ProblemRunnerService _runnerService;
        public event EventHandler BackButtonClicked;
        private Guid _problemId;
        private string _currentLanguage = "C++";
        private CodingProblem _currentProblem;
        private Panel pnlResultContainer;
        private Panel _loadingPanel;
        private Label _loadingLabel;
        private int _loadingDotCount = 0;
        private Timer _loadingTimer;
        private string _loadingMessage = "";
        

        public ucProblemDetail()
        {
            _problemService = new CodingProblemService();
            _testCaseService = new TestCaseService();
            _submissionService = new SubmissionService();
            _runnerService = new ProblemRunnerService();
            InitializeComponent();

            // Khởi tạo Scintilla editor
            InitializeScintillaEditor();

            btnBack.Click += (s, e) => BackButtonClicked?.Invoke(this, EventArgs.Empty);
            btnRun.Click += (s, e) => RunCode();
            btnSave.Click += (s, e) => SaveCode();
            btnSubmit.Click += (s, e) => SubmitCode();

            // ✅ SET DEFAULT LANGUAGE TO C++
            if (cmbLanguage.Items.Count > 0)
            {
                cmbLanguage.SelectedItem = "C++";
            }

            // Khởi tạo loading timer
            InitializeLoadingTimer();
        }

        /// <summary>
        /// Khởi tạo timer cho hiệu ứng loading
        /// </summary>
        private void InitializeLoadingTimer()
        {
            _loadingTimer = new Timer();
            _loadingTimer.Interval = 300;
            _loadingTimer.Tick += (s, e) => UpdateLoadingAnimation();
        }

        /// <summary>
        /// Cập nhật animation loading (chấm chấm)
        /// </summary>
        private void UpdateLoadingAnimation()
        {
            _loadingDotCount = (_loadingDotCount + 1) % 4;
            string dots = new string('.', _loadingDotCount);
            _loadingLabel.Text = $"⏳ {_loadingMessage}{dots}";
        }

        /// <summary>
        /// Hiển thị loading panel với message tùy chỉnh
        /// </summary>
        private void ShowLoadingPanel(string message = "Đang xử lý")
        {
            if (_loadingPanel != null)
                _loadingPanel.Dispose();

            _loadingMessage = message;
            _loadingPanel = new Panel();
            _loadingPanel.BackColor = Color.FromArgb(245, 245, 245);
            _loadingPanel.Dock = DockStyle.Fill;
            _loadingPanel.BorderStyle = BorderStyle.FixedSingle;

            _loadingLabel = new Label();
            _loadingLabel.Text = $"⏳ {message}...";
            _loadingLabel.Font = new Font("Segoe UI", 12, FontStyle.Bold);
            _loadingLabel.ForeColor = Color.FromArgb(0, 120, 215);
            _loadingLabel.AutoSize = false;
            _loadingLabel.TextAlign = ContentAlignment.MiddleCenter;
            _loadingLabel.Dock = DockStyle.Fill;

            _loadingPanel.Controls.Add(_loadingLabel);
            txtConsole.Controls.Clear();
            txtConsole.Controls.Add(_loadingPanel);

            _loadingDotCount = 0;
            _loadingTimer.Start();
        }

        /// <summary>
        /// Ẩn loading panel
        /// </summary>
        private void HideLoadingPanel()
        {
            _loadingTimer.Stop();
            if (_loadingPanel != null)
            {
                _loadingPanel.Dispose();
                _loadingPanel = null;
            }
        }

        /// <summary>
        /// Khởi tạo Scintilla editor
        /// </summary>
        private void InitializeScintillaEditor()
        {
            CodeEditorHelper.InitializeEditor(scintillaEditor);
            CodeEditorHelper.SetLanguage(scintillaEditor, _currentLanguage);
        }

        public void LoadProblemById(Guid problemId)
        {
            _problemId = problemId;
            LoadProblemDetail();
        }

        private void LoadProblemDetail()
        {
            try
            {
                flowDescription.Controls.Clear();

                _currentProblem = _problemService.GetById(_problemId);

                if (_currentProblem == null)
                {
                    MessageBox.Show("Không tìm thấy bài tập!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                AddSectionTitle(_currentProblem.Title);
                AddDifficultyAndTags(_currentProblem.Difficulty, _currentProblem.Tags);

                if (!string.IsNullOrEmpty(_currentProblem.Description))
                {
                    AddLabel("Mô tả:", true);
                    AddLabel(_currentProblem.Description, false);
                }

                AddSectionTitle("Bài kiểm tra");
                LoadTestCases();

                AddSectionTitle("Ràng buộc");

                if (!string.IsNullOrEmpty(_currentProblem.Constraints))
                {
                    AddLabel(_currentProblem.Constraints, false);
                }
                else
                {
                    AddLabel("Chưa có ràng buộc", false);
                }

                AddSectionTitle("Giới hạn");
                AddLabel($"⏱️  Thời gian: {_currentProblem.TimeLimit}ms", false);
                AddLabel($"💾 Bộ nhớ: {_currentProblem.MemoryLimit}MB", false);
                AddLabel("", false);

                UpdateEditorForLanguage(_currentLanguage);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải chi tiết bài tập: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Chạy code
        /// </summary>
        private async void RunCode()
        {
            if (_currentProblem == null)
            {
                MessageBox.Show("Vui lòng tải bài tập trước!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                btnRun.Enabled = false;
                ShowLoadingPanel("Đang chạy code");

                var testCases = _testCaseService.GetVisibleByProblemId(_problemId);
                var testCaseIds = new List<Guid>();
                foreach (var tc in testCases)
                {
                    testCaseIds.Add(tc.TestCaseID);
                }

                var runRequest = new RunProblem
                {
                    UserId = GetCurrentUserId(),
                    ProblemId = _problemId,
                    Code = scintillaEditor.Text,
                    Language = _currentLanguage.ToLower(),
                    FunctionName = _currentProblem.FunctionName,
                    TestCases = testCaseIds
                };

                var result = await _runnerService.RunProblemAsync(runRequest);
                HideLoadingPanel();
                DisplayRunResult(result);
            }
            catch (Exception ex)
            {
                HideLoadingPanel();
                txtConsole.Text = $"❌ Lỗi: {ex.Message}";
                MessageBox.Show($"Lỗi khi chạy code: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnRun.Enabled = true;
            }
        }

        /// <summary>
        /// Hiển thị kết quả chạy code - Phiên bản đơn giản, chuyên nghiệp
        /// </summary>
        private void DisplayRunResult(RunResultResponse result)
        {
            txtConsole.Text = "";
            txtConsole.Controls.Clear();

            if (!result.IsSuccess)
            {
                Label lblError = new Label();
                lblError.Text = $"❌ Lỗi: {result.Message}";
                lblError.Font = new Font("Segoe UI", 10, FontStyle.Regular);
                lblError.ForeColor = Color.FromArgb(200, 50, 50);
                lblError.AutoSize = true;
                lblError.Location = new Point(20, 20);
                txtConsole.Controls.Add(lblError);
                return;
            }

            if (result.Data == null || result.Data.Count == 0)
            {
                Label lblEmpty = new Label();
                lblEmpty.Text = "Không có kết quả bài kiểm tra";
                lblEmpty.Font = new Font("Segoe UI", 10);
                lblEmpty.ForeColor = Color.FromArgb(100, 100, 100);
                lblEmpty.AutoSize = true;
                lblEmpty.Location = new Point(20, 20);
                txtConsole.Controls.Add(lblEmpty);
                return;
            }

            int passedCount = 0;
            int failedCount = 0;
            long totalMemory = 0;
            double totalTime = 0;

            foreach (var testResult in result.Data)
            {
                if (testResult.Passed)
                    passedCount++;
                else
                    failedCount++;

                totalMemory += testResult.Memory ?? 0;

                if (double.TryParse(testResult.Time ?? "0", out double timeValue))
                    totalTime += timeValue;
            }

            Panel pnlContainer = new Panel();
            pnlContainer.AutoScroll = true;
            pnlContainer.Dock = DockStyle.Fill;
            pnlContainer.BackColor = Color.White;
            txtConsole.Controls.Add(pnlContainer);

            int yPos = 15;

            // Header Section - Status
            bool allPassed = passedCount == result.Data.Count;
            Panel pnlHeader = new Panel();
            pnlHeader.BackColor = Color.FromArgb(248, 249, 250);
            pnlHeader.BorderStyle = BorderStyle.None;
            pnlHeader.Width = 650;
            pnlHeader.Height = 70;
            pnlHeader.Location = new Point(15, yPos);

            Label lblStatus = new Label();
            lblStatus.Text = allPassed ? "✓ Tất cả bài kiểm tra vượt qua" : "✗ Một số bài kiểm tra thất bại";
            lblStatus.Font = new Font("Segoe UI", 12, FontStyle.Bold);
            lblStatus.ForeColor = allPassed ? Color.FromArgb(70, 140, 70) : Color.FromArgb(200, 50, 50);
            lblStatus.AutoSize = true;
            lblStatus.Location = new Point(15, 10);
            pnlHeader.Controls.Add(lblStatus);

            Label lblStats = new Label();
            lblStats.Text = $"Vượt qua: {passedCount}/{result.Data.Count}  |  Thời gian: {totalTime:F2}s  |  Bộ nhớ: {totalMemory / 1024.0:F1} MB";
            lblStats.Font = new Font("Segoe UI", 9);
            lblStats.ForeColor = Color.FromArgb(120, 120, 120);
            lblStats.AutoSize = true;
            lblStats.Location = new Point(15, 38);
            pnlHeader.Controls.Add(lblStats);

            pnlContainer.Controls.Add(pnlHeader);
            yPos += 85;

            for (int i = 0; i < result.Data.Count; i++)
            {
                TestResultPanel testPanel = new TestResultPanel(result.Data[i], i + 1);
                testPanel.Location = new Point(15, yPos);
                pnlContainer.Controls.Add(testPanel);
                yPos += testPanel.Height + 8;
            }
        }

        /// <summary>
        /// Lưu code
        /// </summary>
        private void SaveCode()
        {
            if (_currentProblem == null)
            {
                MessageBox.Show("Vui lòng tải bài tập trước!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                if (string.IsNullOrWhiteSpace(scintillaEditor.Text))
                {
                    MessageBox.Show("Code không được để trống!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var submission = new Submission
                {
                    SubmissionID = Guid.NewGuid(),
                    UserID = GetCurrentUserId(),
                    ProblemID = _problemId,
                    Code = scintillaEditor.Text,
                    Language = _currentLanguage.ToLower(),
                    Status = "Saved",
                    SubmitTime = DateTime.Now
                };

                bool result = _submissionService.SaveSubmission(submission);

                if (result)
                {
                    MessageBox.Show("✓ Lưu code thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("❌ Lưu code thất bại. Vui lòng thử lại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"❌ Lỗi khi lưu code: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Submit code
        /// </summary>
        private async void SubmitCode()
        {
            if (_currentProblem == null)
            {
                MessageBox.Show("Vui lòng tải bài tập trước!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                btnSubmit.Enabled = false;
                ShowLoadingPanel("Đang submit code");

                var testCases = _testCaseService.GetByProblemId(_problemId);
                var testCaseIds = new List<Guid>();
                foreach (var tc in testCases)
                {
                    testCaseIds.Add(tc.TestCaseID);
                }

                var submitRequest = new RunProblem
                {
                    UserId = GetCurrentUserId(),
                    ProblemId = _problemId,
                    Code = scintillaEditor.Text,
                    Language = _currentLanguage.ToLower(),
                    FunctionName = _currentProblem.FunctionName,
                    TestCases = testCaseIds
                };

                var result = await _runnerService.SubmitProblemAsync(submitRequest);
                HideLoadingPanel();
                DisplaySubmitResult(result);

                if (result.IsSuccess && result.Data != null)
                {
                    SaveSubmissionAfterSubmit(result.Data);
                    UpdateProblemStatusAfterSubmit(result.Data);
                }
            }
            catch (Exception ex)
            {
                HideLoadingPanel();
                txtConsole.Text = $"❌ Lỗi: {ex.Message}";
                MessageBox.Show($"Lỗi khi submit code: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnSubmit.Enabled = true;
            }
        }

        /// <summary>
        /// Hiển thị kết quả submit - Phiên bản đơn giản, chuyên nghiệp
        /// </summary>
        private void DisplaySubmitResult(SubmitResultResponse result)
        {
            txtConsole.Text = "";
            txtConsole.Controls.Clear();

            if (!result.IsSuccess)
            {
                Label lblError = new Label();
                lblError.Text = $"❌ Lỗi: {result.Message}";
                lblError.Font = new Font("Segoe UI", 10, FontStyle.Regular);
                lblError.ForeColor = Color.FromArgb(200, 50, 50);
                lblError.AutoSize = true;
                lblError.Location = new Point(20, 20);
                txtConsole.Controls.Add(lblError);
                return;
            }

            if (result.Data == null)
            {
                Label lblEmpty = new Label();
                lblEmpty.Text = "Không có kết quả submit";
                lblEmpty.Font = new Font("Segoe UI", 10);
                lblEmpty.ForeColor = Color.FromArgb(100, 100, 100);
                lblEmpty.AutoSize = true;
                lblEmpty.Location = new Point(20, 20);
                txtConsole.Controls.Add(lblEmpty);
                return;
            }

            Panel pnlContainer = new Panel();
            pnlContainer.AutoScroll = true;
            pnlContainer.Dock = DockStyle.Fill;
            pnlContainer.BackColor = Color.White;
            txtConsole.Controls.Add(pnlContainer);

            int yPos = 15;

            bool isAccepted = result.Data.Status == "Accepted";
            Panel pnlHeader = new Panel();
            pnlHeader.BackColor = Color.FromArgb(248, 249, 250);
            pnlHeader.BorderStyle = BorderStyle.None;
            pnlHeader.Width = 650;
            pnlHeader.Height = 80;
            pnlHeader.Location = new Point(15, yPos);

            Label lblMainStatus = new Label();
            lblMainStatus.Text = isAccepted ? "✓ Chấp nhận" : "✗ " + result.Data.Status;
            lblMainStatus.Font = new Font("Segoe UI", 13, FontStyle.Bold);
            lblMainStatus.ForeColor = isAccepted ? Color.FromArgb(70, 140, 70) : Color.FromArgb(200, 50, 50);
            lblMainStatus.AutoSize = true;
            lblMainStatus.Location = new Point(15, 10);
            pnlHeader.Controls.Add(lblMainStatus);

            Label lblTestCase = new Label();
            lblTestCase.Text = $"Bài kiểm tra: {result.Data.TestCasePass}/{result.Data.TotalTestCase}";
            lblTestCase.Font = new Font("Segoe UI", 10);
            lblTestCase.ForeColor = Color.FromArgb(80, 80, 80);
            lblTestCase.AutoSize = true;
            lblTestCase.Location = new Point(15, 38);
            pnlHeader.Controls.Add(lblTestCase);

            Label lblStats = new Label();
            double displayTime = result.Data.Time ?? 0;
            int displayMemory = result.Data.Memory ?? 0;
            lblStats.Text = $"Thời gian: {displayTime:F2}s  |  Bộ nhớ: {displayMemory / 1024.0:F1} MB";
            lblStats.Font = new Font("Segoe UI", 9);
            lblStats.ForeColor = Color.FromArgb(120, 120, 120);
            lblStats.AutoSize = true;
            lblStats.Location = new Point(15, 60);
            pnlHeader.Controls.Add(lblStats);

            pnlContainer.Controls.Add(pnlHeader);
            yPos += 95;

            if (!string.IsNullOrEmpty(result.Data.Message))
            {
                Label lblMessage = new Label();
                lblMessage.Text = result.Data.Message;
                lblMessage.Font = new Font("Segoe UI", 9);
                lblMessage.ForeColor = Color.FromArgb(100, 100, 100);
                lblMessage.AutoSize = true;
                lblMessage.Location = new Point(15, yPos);
                pnlContainer.Controls.Add(lblMessage);
                yPos += 30;
            }
        }

        /// <summary>
        /// Cập nhật status của problem sau khi submit
        /// SOLVED: nếu tất cả test case pass
        /// ATTEMPTED: nếu có test case fail
        /// </summary>
        private void UpdateProblemStatusAfterSubmit(SubmitData submitData)
        {
            try
            {
                if (_currentProblem.Status != "SOLVED")
                {
                    string newStatus = (submitData.TestCasePass == submitData.TotalTestCase) ? "SOLVED" : "ATTEMPTED";
                    
                    if (_currentProblem.Status != newStatus)
                    {
                        _currentProblem.Status = newStatus;
                        _currentProblem.UpdatedAt = DateTime.Now;
                        
                        bool updateResult = _problemService.Update(_currentProblem);
                        
                        if (updateResult)
                        {
                            System.Diagnostics.Debug.WriteLine($"✓ Status problem được cập nhật thành: {newStatus}");
                        }
                        else
                        {
                            System.Diagnostics.Debug.WriteLine($"❌ Không thể cập nhật status problem");
                        }
                    }
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine("ℹ️ Problem đã có status SOLVED, không cần cập nhật");
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Lỗi khi cập nhật status problem: {ex.Message}");
            }
        }

        /// <summary>
        /// Lưu submission sau khi submit thành công
        /// </summary>
        private void SaveSubmissionAfterSubmit(SubmitData submitData)
        {
            try
            {
                int executionTime = submitData.Time.HasValue 
                    ? (int)Math.Round(submitData.Time.Value * 1000) 
                    : 0;
                
                int memoryUsed = submitData.Memory.HasValue 
                    ? (int)Math.Round(submitData.Memory.Value / 1024.0) 
                    : 0;

                var submission = new Submission
                {
                    SubmissionID = Guid.NewGuid(),
                    UserID = GetCurrentUserId(),
                    ProblemID = _problemId,
                    Code = scintillaEditor.Text,
                    Language = _currentLanguage.ToLower(),
                    Status = submitData.Status,
                    SubmitTime = DateTime.Now,
                    ExecutionTime = executionTime,
                    MemoryUsed = memoryUsed,
                    QuantityTestPassed = submitData.TestCasePass,
                    QuantityTest = submitData.TotalTestCase
                };

                bool saveResult = _submissionService.SaveSubmission(submission);

                if (saveResult)
                {
                    System.Diagnostics.Debug.WriteLine("✓ Submission đã được lưu thành công!");
                }
                else
                {
                    MessageBox.Show("⚠️ Không thể lưu submission. Vui lòng thử lại!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"❌ Lỗi khi lưu submission: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                System.Diagnostics.Debug.WriteLine($"Error saving submission: {ex.Message}");
            }
        }

        /// <summary>
        /// Lấy ID người dùng hiện tại
        /// </summary>
        private Guid GetCurrentUserId()
        {
            return GlobalStore.user.UserID;
        }

        /// <summary>
        /// Xử lý thay đổi ngôn ngữ lập trình
        /// </summary>
        private void cmbLanguage_SelectedIndexChanged(object sender, EventArgs e)
        {
            _currentLanguage = cmbLanguage.SelectedItem.ToString();
            UpdateEditorForLanguage(_currentLanguage);
        }

        /// <summary>
        /// Cập nhật editor khi thay đổi ngôn ngữ
        /// </summary>
        private void UpdateEditorForLanguage(string language)
        {
            if (_currentProblem == null)
                return;

            string functionName = string.IsNullOrEmpty(_currentProblem.FunctionName) ? "solution" : _currentProblem.FunctionName;
            string parameters = string.IsNullOrEmpty(_currentProblem.Parameters) ? "" : _currentProblem.Parameters;
            string returnType = string.IsNullOrEmpty(_currentProblem.ReturnType) ? "void" : _currentProblem.ReturnType;

            string convertedParameters = LanguageConverter.ParseAndConvertParameters(parameters, language);
            string convertedReturnType = LanguageConverter.ConvertReturnType(returnType, language);

            string template = "";

            switch (language)
            {
                case "C++":
                    lblFileName.Text = "main.cpp";
                    template = GenerateCppTemplate(functionName, convertedParameters, convertedReturnType);
                    break;

                case "Python":
                    lblFileName.Text = "main.py";
                    template = GeneratePythonTemplate(functionName, convertedParameters);
                    break;

                case "JavaScript":
                    lblFileName.Text = "main.js";
                    template = GenerateJavaScriptTemplate(functionName, convertedParameters);
                    break;
            }

            scintillaEditor.Text = template;
            CodeEditorHelper.SetLanguage(scintillaEditor, language);
        }

        private string GenerateCppTemplate(string functionName, string parameters, string returnType)
        {
            return $"{returnType} {functionName}({parameters}) {{\n\n}}";
        }

        private string GeneratePythonTemplate(string functionName, string parameters)
        {
            return $"def {functionName}({parameters}):\n";
        }

        private string GenerateJavaScriptTemplate(string functionName, string parameters)
        {
            return $"function {functionName}({parameters}) {{\n\n}}";
        }

        private void LoadTestCases()
        {
            try
            {
                List<TestCase> testCases = _testCaseService.GetVisibleByProblemId(_problemId);

                if (testCases.Count == 0)
                {
                    AddLabel("Chưa có ví dụ bài kiểm tra", false);
                    return;
                }

                for (int i = 0; i < testCases.Count; i++)
                {
                    AddLabel($"Ví dụ {i + 1}:", true);

                    AddLabel("Đầu vào:", true);
                    string formattedInput = JsonConverter.JsonToVariableFormat(testCases[i].Input ?? "");
                    AddCodeBox(string.IsNullOrWhiteSpace(formattedInput) ? testCases[i].Input ?? "" : formattedInput);

                    AddLabel("Đầu ra:", true);
                    AddCodeBox(testCases[i].ExpectedOutput ?? "");

                    if (!string.IsNullOrEmpty(testCases[i].Explain))
                    {
                        AddLabel("Giải thích:", true);
                        AddLabel(testCases[i].Explain, false);
                    }

                    AddLabel("", false);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải bài kiểm tra: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AddDifficultyAndTags(string difficulty, string tags)
        {
            Panel pnl = new Panel();
            pnl.AutoSize = true;
            pnl.Margin = new Padding(0, 0, 0, 15);

            Label lblDiff = new Label();
            lblDiff.Text = $"Độ khó: {difficulty}";
            lblDiff.ForeColor = GetDifficultyColor(difficulty);
            lblDiff.Font = new Font("Segoe UI", 9, FontStyle.Bold);
            lblDiff.AutoSize = true;
            lblDiff.Location = new Point(0, 0);
            pnl.Controls.Add(lblDiff);

            if (!string.IsNullOrEmpty(tags))
            {
                Label lblTags = new Label();
                lblTags.Text = $" | Tags: {tags}";
                lblTags.ForeColor = Color.FromArgb(100, 100, 100);
                lblTags.Font = new Font("Segoe UI", 9);
                lblTags.AutoSize = true;
                lblTags.Location = new Point(lblDiff.Width, 0);
                pnl.Controls.Add(lblTags);
            }

            flowDescription.Controls.Add(pnl);
        }

        private Color GetDifficultyColor(string difficulty)
        {
            switch (difficulty)
            {
                case "Dễ":
                    return Color.Green;
                case "Trung bình":
                    return Color.Orange;
                case "Khó":
                    return Color.Red;
                default:
                    return Color.Black;
            }
        }

        private void AddLabel(string text, bool isBold)
        {
            Label lbl = new Label();
            lbl.Text = text;
            lbl.AutoSize = true;
            lbl.MaximumSize = new Size(flowDescription.Width - 25, 0);
            lbl.Font = new Font("Segoe UI", 9, isBold ? FontStyle.Bold : FontStyle.Regular);
            lbl.ForeColor = isBold ? Color.Black : Color.FromArgb(64, 64, 64);
            lbl.Margin = new Padding(0, 0, 0, isBold ? 8 : 10);
            flowDescription.Controls.Add(lbl);
        }

        private void AddSectionTitle(string text)
        {
            Label lbl = new Label();
            lbl.Text = $"  {text}";
            lbl.AutoSize = false;
            lbl.Size = new Size(flowDescription.Width - 10, 35);
            lbl.TextAlign = ContentAlignment.MiddleLeft;
            lbl.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            lbl.BackColor = Color.FromArgb(240, 240, 240);
            lbl.ForeColor = Color.FromArgb(50, 50, 50);
            lbl.Margin = new Padding(0, 15, 0, 10);
            flowDescription.Controls.Add(lbl);
        }

        private void AddCodeBox(string text)
        {
            TextBox txt = new TextBox();
            txt.Multiline = true;
            txt.ReadOnly = true;
            txt.Text = text;
            txt.BackColor = Color.FromArgb(250, 250, 250);
            txt.ForeColor = Color.FromArgb(30, 30, 30);
            txt.BorderStyle = BorderStyle.FixedSingle;
            txt.Font = new Font("Consolas", 9);
            txt.Width = flowDescription.Width - 10;

            int lines = text.Split('\n').Length;
            txt.Height = Math.Max(80, (lines * 18) + 10);

            txt.Margin = new Padding(0, 0, 0, 15);
            txt.ScrollBars = ScrollBars.Vertical;
            flowDescription.Controls.Add(txt);
        }
    }
}                               