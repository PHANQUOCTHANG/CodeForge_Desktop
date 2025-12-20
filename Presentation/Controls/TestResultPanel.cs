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
            _testResult = testResult;
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
            pnlHeader.BackColor = _testResult.Passed ? Color.FromArgb(232, 245, 233) : Color.FromArgb(255, 235, 238);
            pnlHeader.Height = 60;
            pnlHeader.Dock = DockStyle.Top;
            this.Controls.Add(pnlHeader);

            // Status Icon + Number
            lblStatus = new Label();
            lblStatus.Text = _testResult.Passed ? "✓" : "✗";
            lblStatus.ForeColor = _testResult.Passed ? Color.FromArgb(76, 175, 80) : Color.FromArgb(244, 67, 54);
            lblStatus.Font = new Font("Arial", 16, FontStyle.Bold);
            lblStatus.AutoSize = true;
            lblStatus.Location = new Point(15, 18);
            pnlHeader.Controls.Add(lblStatus);

            // Test Number
            lblTestNumber = new Label();
            lblTestNumber.Text = $"Test Case #{_testNumber}";
            lblTestNumber.Font = new Font("Segoe UI", 11, FontStyle.Bold);
            lblTestNumber.ForeColor = Color.FromArgb(50, 50, 50);
            lblTestNumber.AutoSize = true;
            lblTestNumber.Location = new Point(45, 10);
            pnlHeader.Controls.Add(lblTestNumber);

            // Status Text
            Label lblStatusText = new Label();
            lblStatusText.Text = _testResult.Passed ? "Accepted" : "Wrong Answer";
            lblStatusText.Font = new Font("Segoe UI", 9);
            lblStatusText.ForeColor = _testResult.Passed ? Color.FromArgb(76, 175, 80) : Color.FromArgb(244, 67, 54);
            lblStatusText.AutoSize = true;
            lblStatusText.Location = new Point(45, 28);
            pnlHeader.Controls.Add(lblStatusText);

            // Time Info
            lblTime = new Label();
            lblTime.Text = $"⏱️  {_testResult.Time}s";
            lblTime.Font = new Font("Segoe UI", 9);
            lblTime.ForeColor = Color.FromArgb(100, 100, 100);
            lblTime.AutoSize = true;
            lblTime.Location = new Point(200, 18);
            pnlHeader.Controls.Add(lblTime);

            // Memory Info
            lblMemory = new Label();
            lblMemory.Text = $"💾 {_testResult.Memory / 1024.0:F2} MB";
            lblMemory.Font = new Font("Segoe UI", 9);
            lblMemory.ForeColor = Color.FromArgb(100, 100, 100);
            lblMemory.AutoSize = true;
            lblMemory.Location = new Point(310, 18);
            pnlHeader.Controls.Add(lblMemory);

            // Expand Button
            btnExpand = new Button();
            btnExpand.Text = "▼";
            btnExpand.Width = 35;
            btnExpand.Height = 35;
            btnExpand.FlatStyle = FlatStyle.Flat;
            btnExpand.FlatAppearance.BorderSize = 0;
            btnExpand.BackColor = Color.Transparent;
            btnExpand.ForeColor = Color.FromArgb(150, 150, 150);
            btnExpand.Font = new Font("Arial", 10);
            btnExpand.Location = new Point(610, 12);
            btnExpand.Click += BtnExpand_Click;
            pnlHeader.Controls.Add(btnExpand);

            // Expanded Content Panel
            pnlExpanded = new Panel();
            pnlExpanded.BackColor = Color.FromArgb(248, 248, 248);
            pnlExpanded.Dock = DockStyle.Fill;
            pnlExpanded.Visible = false;
            pnlExpanded.Padding = new Padding(15);
            this.Controls.Add(pnlExpanded);

            CreateExpandedContent();
        }

        private void CreateExpandedContent()
        {
            int yPos = 0;

            // Status Info
            if (!_testResult.Passed)
            {
                Label lblExpectedLabel = new Label();
                lblExpectedLabel.Text = "Expected Output:";
                lblExpectedLabel.Font = new Font("Segoe UI", 10, FontStyle.Bold);
                lblExpectedLabel.ForeColor = Color.FromArgb(50, 50, 50);
                lblExpectedLabel.AutoSize = true;
                lblExpectedLabel.Location = new Point(0, yPos);
                pnlExpanded.Controls.Add(lblExpectedLabel);
                yPos += 25;

                TextBox txtExpected = new TextBox();
                txtExpected.Text = FormatOutput(_testResult.ExpectedOutput);
                txtExpected.Multiline = true;
                txtExpected.ReadOnly = true;
                txtExpected.BackColor = Color.FromArgb(240, 255, 240);
                txtExpected.BorderStyle = BorderStyle.FixedSingle;
                txtExpected.Font = new Font("Consolas", 9);
                txtExpected.Width = 600;
                txtExpected.Height = Math.Max(40, _testResult.ExpectedOutput.Split('\n').Length * 18);
                txtExpected.Location = new Point(0, yPos);
                pnlExpanded.Controls.Add(txtExpected);
                yPos += txtExpected.Height + 15;

                Label lblActualLabel = new Label();
                lblActualLabel.Text = "Actual Output:";
                lblActualLabel.Font = new Font("Segoe UI", 10, FontStyle.Bold);
                lblActualLabel.ForeColor = Color.FromArgb(50, 50, 50);
                lblActualLabel.AutoSize = true;
                lblActualLabel.Location = new Point(0, yPos);
                pnlExpanded.Controls.Add(lblActualLabel);
                yPos += 25;

                TextBox txtActual = new TextBox();
                txtActual.Text = FormatOutput(_testResult.Stdout);
                txtActual.Multiline = true;
                txtActual.ReadOnly = true;
                txtActual.BackColor = Color.FromArgb(255, 240, 240);
                txtActual.BorderStyle = BorderStyle.FixedSingle;
                txtActual.Font = new Font("Consolas", 9);
                txtActual.Width = 600;
                txtActual.Height = Math.Max(40, _testResult.Stdout.Split('\n').Length * 18);
                txtActual.Location = new Point(0, yPos);
                pnlExpanded.Controls.Add(txtActual);
                yPos += txtActual.Height + 15;

                if (!string.IsNullOrEmpty(_testResult.Stderr))
                {
                    Label lblErrorLabel = new Label();
                    lblErrorLabel.Text = "Error Output:";
                    lblErrorLabel.Font = new Font("Segoe UI", 10, FontStyle.Bold);
                    lblErrorLabel.ForeColor = Color.FromArgb(211, 47, 47);
                    lblErrorLabel.AutoSize = true;
                    lblErrorLabel.Location = new Point(0, yPos);
                    pnlExpanded.Controls.Add(lblErrorLabel);
                    yPos += 25;

                    TextBox txtError = new TextBox();
                    txtError.Text = _testResult.Stderr;
                    txtError.Multiline = true;
                    txtError.ReadOnly = true;
                    txtError.BackColor = Color.FromArgb(255, 235, 238);
                    txtError.BorderStyle = BorderStyle.FixedSingle;
                    txtError.Font = new Font("Consolas", 9);
                    txtError.ForeColor = Color.FromArgb(211, 47, 47);
                    txtError.Width = 600;
                    txtError.Height = Math.Max(40, _testResult.Stderr.Split('\n').Length * 18);
                    txtError.Location = new Point(0, yPos);
                    pnlExpanded.Controls.Add(txtError);
                    yPos += txtError.Height + 15;
                }
            }
            else
            {
                Label lblOutputLabel = new Label();
                lblOutputLabel.Text = "Output:";
                lblOutputLabel.Font = new Font("Segoe UI", 10, FontStyle.Bold);
                lblOutputLabel.ForeColor = Color.FromArgb(50, 50, 50);
                lblOutputLabel.AutoSize = true;
                lblOutputLabel.Location = new Point(0, yPos);
                pnlExpanded.Controls.Add(lblOutputLabel);
                yPos += 25;

                TextBox txtOutput = new TextBox();
                txtOutput.Text = FormatOutput(_testResult.Stdout);
                txtOutput.Multiline = true;
                txtOutput.ReadOnly = true;
                txtOutput.BackColor = Color.FromArgb(240, 255, 240);
                txtOutput.BorderStyle = BorderStyle.FixedSingle;
                txtOutput.Font = new Font("Consolas", 9);
                txtOutput.Width = 600;
                txtOutput.Height = Math.Max(40, _testResult.Stdout.Split('\n').Length * 18);
                txtOutput.Location = new Point(0, yPos);
                pnlExpanded.Controls.Add(txtOutput);
            }
        }

        private string FormatOutput(string output)
        {
            if (string.IsNullOrEmpty(output))
                return "(empty)";

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