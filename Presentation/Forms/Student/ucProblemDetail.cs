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
            _runnerService = new ProblemRunnerService();
            InitializeComponent();

            // Khởi tạo Scintilla editor
            InitializeScintillaEditor();

            btnBack.Click += (s, e) => BackButtonClicked?.Invoke(this, EventArgs.Empty);
            btnRun.Click += (s, e) => RunCode();
            btnSave.Click += (s, e) => SaveCode();
            btnSubmit.Click += (s, e) => SubmitCode();

            // Khởi tạo loading timer
            InitializeLoadingTimer();
        }

        /// <summary>
        /// Khởi tạo timer cho hiệu ứng loading
        /// </summary>
        private void InitializeLoadingTimer()
        {
            _loadingTimer = new Timer();
            _loadingTimer.Interval = 300; // Cập nhật mỗi 300ms
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

            // Clear txtConsole và thêm loading panel
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

                // 1. TIÊU ĐỀ: Mô tả bài toán
                AddSectionTitle(_currentProblem.Title);

                // 2. Độ khó + Tags trên 1 dòng
                AddDifficultyAndTags(_currentProblem.Difficulty, _currentProblem.Tags);

                // 3. Mô tả bài toán
                if (!string.IsNullOrEmpty(_currentProblem.Description))
                {
                    AddLabel("Mô tả:", true);
                    AddLabel(_currentProblem.Description, false);
                }

                // 4. TIÊU ĐỀ: Ví dụ Test Case
                AddSectionTitle("Test Case");
                LoadTestCases();

                // 5. TIÊU ĐỀ: Ràng buộc
                AddSectionTitle("Ràng buộc");

                if (!string.IsNullOrEmpty(_currentProblem.Constraints))
                {
                    AddLabel(_currentProblem.Constraints, false);
                }
                else
                {
                    AddLabel("Chưa có ràng buộc", false);
                }

                // 6. TIÊU ĐỀ: Giới hạn
                AddSectionTitle("Giới hạn");

                AddLabel($"⏱️  Thời gian: {_currentProblem.TimeLimit}ms", false);
                AddLabel($"💾 Bộ nhớ: {_currentProblem.MemoryLimit}MB", false);

                // 7. Thêm khoảng trắng ở cuối
                AddLabel("", false);

                // 8. Khởi tạo editor với template function
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

                // Lấy các visible test cases
                var testCases = _testCaseService.GetVisibleByProblemId(_problemId);
                var testCaseIds = new List<Guid>();
                foreach (var tc in testCases)
                {
                    testCaseIds.Add(tc.TestCaseID);
                }

                // Tạo request
                var runRequest = new RunProblem
                {
                    UserId = GetCurrentUserId(),
                    ProblemId = _problemId,
                    Code = scintillaEditor.Text,
                    Language = _currentLanguage.ToLower(),
                    FunctionName = _currentProblem.FunctionName,
                    TestCases = testCaseIds
                };

                // Gửi request
                var result = await _runnerService.RunProblemAsync(runRequest);

                // Hiển thị kết quả
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
        /// Hiển thị kết quả chạy code giống LeetCode - Version 2
        /// </summary>
        private void DisplayRunResult(RunResultResponse result)
        {
            txtConsole.Text = "";
            txtConsole.Controls.Clear();

            if (!result.IsSuccess)
            {
                Label lblError = new Label();
                lblError.Text = $"❌ Lỗi: {result.Message}";
                lblError.Font = new Font("Segoe UI", 11, FontStyle.Bold);
                lblError.ForeColor = Color.FromArgb(244, 67, 54);
                lblError.AutoSize = true;
                lblError.Location = new Point(15, 15);
                txtConsole.Controls.Add(lblError);
                return;
            }

            if (result.Data == null || result.Data.Count == 0)
            {
                Label lblEmpty = new Label();
                lblEmpty.Text = "⚠️ Không có kết quả test case";
                lblEmpty.Font = new Font("Segoe UI", 11);
                lblEmpty.ForeColor = Color.FromArgb(255, 152, 0);
                lblEmpty.AutoSize = true;
                lblEmpty.Location = new Point(15, 15);
                txtConsole.Controls.Add(lblEmpty);
                return;
            }

            // Tính toán thống kê
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

                totalMemory += testResult.Memory;

                if (double.TryParse(testResult.Time, out double timeValue))
                    totalTime += timeValue;
            }

            // Create scrollable container
            Panel pnlContainer = new Panel();
            pnlContainer.AutoScroll = true;
            pnlContainer.Dock = DockStyle.Fill;
            pnlContainer.BackColor = Color.FromArgb(245, 245, 245);
            txtConsole.Controls.Add(pnlContainer);

            int yPos = 15;

            // Header Section
            Panel pnlHeader = new Panel();
            pnlHeader.BackColor = passedCount == result.Data.Count ? Color.FromArgb(232, 245, 233) : Color.FromArgb(255, 235, 238);
            pnlHeader.BorderStyle = BorderStyle.FixedSingle;
            pnlHeader.Width = 650;
            pnlHeader.Height = 80;
            pnlHeader.Location = new Point(15, yPos);

            // Status
            Label lblMainStatus = new Label();
            lblMainStatus.Text = passedCount == result.Data.Count ? "✓ All Tests Passed" : "✗ Some Tests Failed";
            lblMainStatus.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            lblMainStatus.ForeColor = passedCount == result.Data.Count ? Color.FromArgb(76, 175, 80) : Color.FromArgb(244, 67, 54);
            lblMainStatus.Location = new Point(15, 10);
            pnlHeader.Controls.Add(lblMainStatus);

            // Stats
            Label lblStats = new Label();
            lblStats.Text = $"{passedCount} Accepted, {failedCount} Failed | ⏱️ {totalTime:F3}s | 💾 {totalMemory / 1024.0:F2} MB";
            lblStats.Font = new Font("Segoe UI", 10);
            lblStats.ForeColor = Color.FromArgb(100, 100, 100);
            lblStats.Location = new Point(15, 35);
            pnlHeader.Controls.Add(lblStats);

            Label lblMessage = new Label();
            lblMessage.Text = result.Message;
            lblMessage.Font = new Font("Segoe UI", 9);
            lblMessage.ForeColor = Color.FromArgb(150, 150, 150);
            lblMessage.Location = new Point(15, 55);
            pnlHeader.Controls.Add(lblMessage);

            pnlContainer.Controls.Add(pnlHeader);
            yPos += 95;

            // Test Results
            for (int i = 0; i < result.Data.Count; i++)
            {
                TestResultPanel testPanel = new TestResultPanel(result.Data[i], i + 1);
                testPanel.Location = new Point(15, yPos);
                pnlContainer.Controls.Add(testPanel);
                yPos += testPanel.Height + 10;
            }

            // Summary Footer
            Panel pnlFooter = new Panel();
            pnlFooter.BackColor = Color.FromArgb(250, 250, 250);
            pnlFooter.BorderStyle = BorderStyle.FixedSingle;
            pnlFooter.Width = 650;
            pnlFooter.Height = 50;
            pnlFooter.Location = new Point(15, yPos);

            Label lblFooter = new Label();
            lblFooter.Text = passedCount == result.Data.Count ? 
                "🎉 Perfect! All test cases passed successfully!" : 
                $"⚠️ {failedCount} test case(s) failed. Please review your solution.";
            lblFooter.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            lblFooter.ForeColor = passedCount == result.Data.Count ? 
                Color.FromArgb(76, 175, 80) : 
                Color.FromArgb(244, 67, 54);
            lblFooter.Location = new Point(15, 15);
            lblFooter.AutoSize = true;
            pnlFooter.Controls.Add(lblFooter);

            pnlContainer.Controls.Add(pnlFooter);
        }

        /// <summary>
        /// Lưu code
        /// </summary>
        private void SaveCode()
        {
            MessageBox.Show("Chức năng lưu sẽ được triển khai sau!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

                // Lấy tất cả test cases (không chỉ visible)
                //var testCases = _testCaseService.GetByProblemId(_problemId);
                //var testCaseIds = new List<Guid>();
                //foreach (var tc in testCases)
                //{
                //    testCaseIds.Add(tc.TestCaseID);
                //}

                // Tạo request
                var submitRequest = new RunProblem
                {
                    UserId = GetCurrentUserId(),
                    ProblemId = _problemId,
                    Code = scintillaEditor.Text,
                    Language = _currentLanguage.ToLower(),
                    FunctionName = _currentProblem.FunctionName   
                };

                // Gửi request
                var result = await _runnerService.SubmitProblemAsync(submitRequest);

                // Hiển thị kết quả
                HideLoadingPanel();
                DisplaySubmitResult(result);
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
        /// Hiển thị kết quả submit
        /// </summary>
        private void DisplaySubmitResult(SubmitResultResponse result)
        {
            txtConsole.Text = "";
            txtConsole.Controls.Clear();

            if (!result.IsSuccess)
            {
                Label lblError = new Label();
                lblError.Text = $"❌ Lỗi: {result.Message}";
                lblError.Font = new Font("Segoe UI", 11, FontStyle.Bold);
                lblError.ForeColor = Color.FromArgb(244, 67, 54);
                lblError.AutoSize = true;
                lblError.Location = new Point(15, 15);
                txtConsole.Controls.Add(lblError);
                return;
            }

            if (result.Data == null)
            {
                Label lblEmpty = new Label();
                lblEmpty.Text = "⚠️ Không có kết quả submit";
                lblEmpty.Font = new Font("Segoe UI", 11);
                lblEmpty.ForeColor = Color.FromArgb(255, 152, 0);
                lblEmpty.AutoSize = true;
                lblEmpty.Location = new Point(15, 15);
                txtConsole.Controls.Add(lblEmpty);
                return;
            }

            // Create scrollable container
            Panel pnlContainer = new Panel();
            pnlContainer.AutoScroll = true;
            pnlContainer.Dock = DockStyle.Fill;
            pnlContainer.BackColor = Color.FromArgb(245, 245, 245);
            txtConsole.Controls.Add(pnlContainer);

            int yPos = 15;

            // Header Section
            Panel pnlHeader = new Panel();
            bool isAccepted = result.Data.Status == "Accepted";
            pnlHeader.BackColor = isAccepted ? Color.FromArgb(232, 245, 233) : Color.FromArgb(255, 235, 238);
            pnlHeader.BorderStyle = BorderStyle.FixedSingle;
            pnlHeader.Width = 650;
            pnlHeader.Height = 120;
            pnlHeader.Location = new Point(15, yPos);

            // Status
            Label lblMainStatus = new Label();
            lblMainStatus.Text = isAccepted ? "✓ Accepted" : $"✗ {result.Data.Status}";
            lblMainStatus.Font = new Font("Segoe UI", 16, FontStyle.Bold);
            lblMainStatus.ForeColor = isAccepted ? Color.FromArgb(76, 175, 80) : Color.FromArgb(244, 67, 54);
            lblMainStatus.Location = new Point(15, 10);
            lblMainStatus.AutoSize = true;
            pnlHeader.Controls.Add(lblMainStatus);

            // Test case results
            Label lblTestCase = new Label();
            lblTestCase.Text = $"Test Cases: {result.Data.TestCasePass}/{result.Data.TotalTestCase}";
            lblTestCase.Font = new Font("Segoe UI", 11, FontStyle.Bold);
            lblTestCase.ForeColor = Color.FromArgb(50, 50, 50);
            lblTestCase.Location = new Point(15, 40);
            lblTestCase.AutoSize = true;
            pnlHeader.Controls.Add(lblTestCase);

            // Performance stats
            Label lblStats = new Label();
            lblStats.Text = $"⏱️  Time: {result.Data.Time:F3}s | 💾 Memory: {result.Data.Memory / 1024.0:F2} MB";
            lblStats.Font = new Font("Segoe UI", 10);
            lblStats.ForeColor = Color.FromArgb(100, 100, 100);
            lblStats.Location = new Point(15, 65);
            lblStats.AutoSize = true;
            pnlHeader.Controls.Add(lblStats);

            // Message
            Label lblMessage = new Label();
            lblMessage.Text = string.IsNullOrEmpty(result.Data.Message) ? result.Message : result.Data.Message;
            lblMessage.Font = new Font("Segoe UI", 9);
            lblMessage.ForeColor = Color.FromArgb(150, 150, 150);
            lblMessage.Location = new Point(15, 90);
            lblMessage.AutoSize = true;
            pnlHeader.Controls.Add(lblMessage);

            pnlContainer.Controls.Add(pnlHeader);
            yPos += 135;

            // Summary Footer
            Panel pnlFooter = new Panel();
            pnlFooter.BackColor = Color.FromArgb(250, 250, 250);
            pnlFooter.BorderStyle = BorderStyle.FixedSingle;
            pnlFooter.Width = 650;
            pnlFooter.Height = 50;
            pnlFooter.Location = new Point(15, yPos);

            Label lblFooter = new Label();
            if (isAccepted)
            {
                lblFooter.Text = "🎉 Congratulations! Your solution was accepted!";
                lblFooter.ForeColor = Color.FromArgb(76, 175, 80);
            }
            else
            {
                lblFooter.Text = $"⚠️ {result.Data.TotalTestCase - result.Data.TestCasePass} test case(s) failed.";
                lblFooter.ForeColor = Color.FromArgb(244, 67, 54);
            }
            lblFooter.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            lblFooter.Location = new Point(15, 15);
            lblFooter.AutoSize = true;
            pnlFooter.Controls.Add(lblFooter);

            pnlContainer.Controls.Add(pnlFooter);
        }

        /// <summary>
        /// Lấy ID người dùng hiện tại
        /// </summary>
        private Guid GetCurrentUserId()
        {
            return UserStore.user.UserID;
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

        private string GetSampleCall(string parameters)
        {
            if (string.IsNullOrWhiteSpace(parameters))
                return "";

            try
            {
                var paramList = parameters.Split(',');
                var sampleParams = new List<string>();

                foreach (var param in paramList)
                {
                    var trimmed = param.Trim();
                    if (string.IsNullOrWhiteSpace(trimmed))
                        continue;

                    string[] parts = trimmed.Split(new[] { ' ', '*', '&' }, System.StringSplitOptions.RemoveEmptyEntries);
                    if (parts.Length > 0)
                    {
                        sampleParams.Add(parts[parts.Length - 1]);
                    }
                }

                return string.Join(", ", sampleParams);
            }
            catch
            {
                return "";
            }
        }

        private void LoadTestCases()
        {
            try
            {
                List<TestCase> testCases = _testCaseService.GetVisibleByProblemId(_problemId);

                if (testCases.Count == 0)
                {
                    AddLabel("Chưa có ví dụ test case", false);
                    return;
                }

                for (int i = 0; i < testCases.Count; i++)
                {
                    AddLabel($"Ví dụ {i + 1}:", true);

                    AddLabel("Input:", true);
                    string formattedInput = JsonConverter.JsonToVariableFormat(testCases[i].Input ?? "");
                    AddCodeBox(string.IsNullOrWhiteSpace(formattedInput) ? testCases[i].Input ?? "" : formattedInput);

                    AddLabel("Output:", true);
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
                MessageBox.Show("Lỗi khi tải test case: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        /// <summary>
        /// Xử lý sự kiện kéo thanh Splitter
        /// </summary>
        // ĐỔI TÊN HÀM NẾU BẠN GẮN VÀO SplitterMoved
        //private void SplitContainerVertical_SplitterMoved(object sender, System.Windows.Forms.SplitterEventArgs e)
        //{
        //    // Lấy đối tượng SplitContainer
        //    SplitContainer splitContainer = (SplitContainer)sender;

        //    // Giới hạn dưới (Tối thiểu 200px cho editor - Panel 1)
        //    if (splitContainer.SplitterDistance < 200)
        //    {
        //        splitContainer.SplitterDistance = 200;
        //    }
        //    // Giới hạn trên (Tối đa là Chiều cao - 100px cho output - Panel 2)
        //    else if (splitContainer.SplitterDistance > splitContainer.Height - 100)
        //    {
        //        splitContainer.SplitterDistance = splitContainer.Height - 100;
        //    }
        //}
    }
}