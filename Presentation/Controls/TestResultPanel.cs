using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using CodeForge_Desktop.Business.Services;

namespace CodeForge_Desktop.Presentation.Controls
{
    public class TestResultPanel : Panel
    {
        private TestCaseResult _testResult;
        private int _testNumber;
        private Label lblTestNumber;
        private Label lblStatus;
        private Label lblTime;
        private Label lblMemory;
        private Panel pnlExpanded;
        private Button btnExpand;
        private bool _isExpanded = false;

        public TestResultPanel(TestCaseResult testResult, int testNumber)
        {
            _testResult = testResult ?? throw new ArgumentNullException(nameof(testResult));
            _testNumber = testNumber;
            
            this.BackColor = Color.White;
            this.BorderStyle = BorderStyle.FixedSingle;
            this.Height = 60;
            this.Width = 650;
            this.Margin = new Padding(0, 5, 0, 5);

            InitializeComponents();
        }

        private void InitializeComponents()
        {
            // Header Panel
            Panel pnlHeader = new Panel();
            pnlHeader.BackColor = _testResult.Passed ? Color.FromArgb(245, 250, 245) : Color.FromArgb(255, 245, 245);
            pnlHeader.Height = 60;
            pnlHeader.Dock = DockStyle.Top;
            this.Controls.Add(pnlHeader);

            // Status Icon + Number
            lblStatus = new Label();
            lblStatus.Text = _testResult.Passed ? "✓" : "✗";
            lblStatus.ForeColor = _testResult.Passed ? Color.FromArgb(70, 140, 70) : Color.FromArgb(200, 50, 50);
            lblStatus.Font = new Font("Arial", 14, FontStyle.Bold);
            lblStatus.AutoSize = true;
            lblStatus.Location = new Point(12, 18);
            pnlHeader.Controls.Add(lblStatus);

            // Test Number
            lblTestNumber = new Label();
            lblTestNumber.Text = $"Bài kiểm tra #{_testNumber}";
            lblTestNumber.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            lblTestNumber.ForeColor = Color.FromArgb(50, 50, 50);
            lblTestNumber.AutoSize = true;
            lblTestNumber.Location = new Point(40, 12);
            pnlHeader.Controls.Add(lblTestNumber);

            // Status Text
            Label lblStatusText = new Label();
            lblStatusText.Text = _testResult.Passed ? "Vượt qua" : "Thất bại";
            lblStatusText.Font = new Font("Segoe UI", 9);
            lblStatusText.ForeColor = _testResult.Passed ? Color.FromArgb(70, 140, 70) : Color.FromArgb(200, 50, 50);
            lblStatusText.AutoSize = true;
            lblStatusText.Location = new Point(40, 30);
            pnlHeader.Controls.Add(lblStatusText);

            // Time Info
            lblTime = new Label();
            lblTime.Text = $"Thời gian: {_testResult.Time ?? "0"}s";
            lblTime.Font = new Font("Segoe UI", 9);
            lblTime.ForeColor = Color.FromArgb(120, 120, 120);
            lblTime.AutoSize = true;
            lblTime.Location = new Point(220, 18);
            pnlHeader.Controls.Add(lblTime);

            // Memory Info
            lblMemory = new Label();
            int memoryValue = _testResult.Memory ?? 0;
            lblMemory.Text = $"Bộ nhớ: {memoryValue / 1024.0:F1} MB";
            lblMemory.Font = new Font("Segoe UI", 9);
            lblMemory.ForeColor = Color.FromArgb(120, 120, 120);
            lblMemory.AutoSize = true;
            lblMemory.Location = new Point(220, 38);
            pnlHeader.Controls.Add(lblMemory);

            // Expand Button
            btnExpand = new Button();
            btnExpand.Text = "▼";
            btnExpand.Width = 30;
            btnExpand.Height = 30;
            btnExpand.FlatStyle = FlatStyle.Flat;
            btnExpand.FlatAppearance.BorderSize = 0;
            btnExpand.BackColor = Color.Transparent;
            btnExpand.ForeColor = Color.FromArgb(150, 150, 150);
            btnExpand.Font = new Font("Arial", 9);
            btnExpand.Location = new Point(615, 15);
            btnExpand.Cursor = Cursors.Hand;
            btnExpand.Click += BtnExpand_Click;
            pnlHeader.Controls.Add(btnExpand);

            // Expanded Content Panel
            pnlExpanded = new Panel();
            pnlExpanded.BackColor = Color.FromArgb(250, 250, 250);
            pnlExpanded.Dock = DockStyle.Fill;
            pnlExpanded.Visible = false;
            pnlExpanded.Padding = new Padding(15);
            this.Controls.Add(pnlExpanded);

            CreateExpandedContent();
        }

        private void CreateExpandedContent()
        {
            int yPos = 0;

            if (!_testResult.Passed)
            {
                // Expected Output
                Label lblExpectedLabel = new Label();
                lblExpectedLabel.Text = "Kết quả mong đợi:";
                lblExpectedLabel.Font = new Font("Segoe UI", 9, FontStyle.Bold);
                lblExpectedLabel.ForeColor = Color.FromArgb(50, 50, 50);
                lblExpectedLabel.AutoSize = true;
                lblExpectedLabel.Location = new Point(0, yPos);
                pnlExpanded.Controls.Add(lblExpectedLabel);
                yPos += 22;

                TextBox txtExpected = new TextBox();
                string expectedOutput = FormatOutput(_testResult.ExpectedOutput);
                txtExpected.Text = expectedOutput;
                txtExpected.Multiline = true;
                txtExpected.ReadOnly = true;
                txtExpected.BackColor = Color.FromArgb(252, 252, 252);
                txtExpected.BorderStyle = BorderStyle.FixedSingle;
                txtExpected.Font = new Font("Consolas", 9);
                txtExpected.Width = 600;
                txtExpected.Height = Math.Max(40, (expectedOutput.Split('\n').Length * 16) + 8);
                txtExpected.Location = new Point(0, yPos);
                txtExpected.ForeColor = Color.FromArgb(40, 100, 40);
                pnlExpanded.Controls.Add(txtExpected);
                yPos += txtExpected.Height + 12;

                // Actual Output
                Label lblActualLabel = new Label();
                lblActualLabel.Text = "Kết quả thực tế:";
                lblActualLabel.Font = new Font("Segoe UI", 9, FontStyle.Bold);
                lblActualLabel.ForeColor = Color.FromArgb(50, 50, 50);
                lblActualLabel.AutoSize = true;
                lblActualLabel.Location = new Point(0, yPos);
                pnlExpanded.Controls.Add(lblActualLabel);
                yPos += 22;

                TextBox txtActual = new TextBox();
                string actualOutput = FormatOutput(_testResult.Stdout);
                txtActual.Text = actualOutput;
                txtActual.Multiline = true;
                txtActual.ReadOnly = true;
                txtActual.BackColor = Color.FromArgb(252, 252, 252);
                txtActual.BorderStyle = BorderStyle.FixedSingle;
                txtActual.Font = new Font("Consolas", 9);
                txtActual.Width = 600;
                txtActual.Height = Math.Max(40, (actualOutput.Split('\n').Length * 16) + 8);
                txtActual.Location = new Point(0, yPos);
                txtActual.ForeColor = Color.FromArgb(180, 40, 40);
                pnlExpanded.Controls.Add(txtActual);
                yPos += txtActual.Height + 12;

                // Error Output (if any)
                if (!string.IsNullOrEmpty(_testResult.Stderr))
                {
                    Label lblErrorLabel = new Label();
                    lblErrorLabel.Text = "Thông báo lỗi:";
                    lblErrorLabel.Font = new Font("Segoe UI", 9, FontStyle.Bold);
                    lblErrorLabel.ForeColor = Color.FromArgb(200, 50, 50);
                    lblErrorLabel.AutoSize = true;
                    lblErrorLabel.Location = new Point(0, yPos);
                    pnlExpanded.Controls.Add(lblErrorLabel);
                    yPos += 22;

                    TextBox txtError = new TextBox();
                    txtError.Text = _testResult.Stderr;
                    txtError.Multiline = true;
                    txtError.ReadOnly = true;
                    txtError.BackColor = Color.FromArgb(252, 252, 252);
                    txtError.BorderStyle = BorderStyle.FixedSingle;
                    txtError.Font = new Font("Consolas", 9);
                    txtError.ForeColor = Color.FromArgb(200, 50, 50);
                    txtError.Width = 600;
                    txtError.Height = Math.Max(40, (_testResult.Stderr.Split('\n').Length * 16) + 8);
                    txtError.Location = new Point(0, yPos);
                    pnlExpanded.Controls.Add(txtError);
                }
            }
            else
            {
                // Output for passed tests
                Label lblOutputLabel = new Label();
                lblOutputLabel.Text = "Kết quả:";
                lblOutputLabel.Font = new Font("Segoe UI", 9, FontStyle.Bold);
                lblOutputLabel.ForeColor = Color.FromArgb(50, 50, 50);
                lblOutputLabel.AutoSize = true;
                lblOutputLabel.Location = new Point(0, yPos);
                pnlExpanded.Controls.Add(lblOutputLabel);
                yPos += 22;

                TextBox txtOutput = new TextBox();
                string output = FormatOutput(_testResult.Stdout);
                txtOutput.Text = output;
                txtOutput.Multiline = true;
                txtOutput.ReadOnly = true;
                txtOutput.BackColor = Color.FromArgb(252, 252, 252);
                txtOutput.BorderStyle = BorderStyle.FixedSingle;
                txtOutput.Font = new Font("Consolas", 9);
                txtOutput.Width = 600;
                txtOutput.Height = Math.Max(40, (output.Split('\n').Length * 16) + 8);
                txtOutput.Location = new Point(0, yPos);
                txtOutput.ForeColor = Color.FromArgb(40, 100, 40);
                pnlExpanded.Controls.Add(txtOutput);
            }
        }

        private string FormatOutput(string output)
        {
            if (string.IsNullOrEmpty(output))
                return "(trống)";

            output = output.TrimEnd('\n', '\r');
            return output;
        }

        private void BtnExpand_Click(object sender, EventArgs e)
        {
            _isExpanded = !_isExpanded;
            pnlExpanded.Visible = _isExpanded;
            btnExpand.Text = _isExpanded ? "▲" : "▼";
            this.Height = _isExpanded ? 300 : 60;
            pnlExpanded.Height = _isExpanded ? this.Height - 60 : 0;
        }
    }
}