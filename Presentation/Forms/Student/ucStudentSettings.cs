using System;
using System.Drawing;
using System.Windows.Forms;

namespace CodeForge_Desktop.Presentation.Forms.Student
{
    public partial class ucStudentSettings : UserControl
    {
        public ucStudentSettings()
        {
            InitializeComponent();

            // Init Data
            InitData();

            // Set Default Active Tab
            SwitchTab(pnlGeneral, btnTabGeneral);

            // Events
            btnTabGeneral.Click += (s, e) => SwitchTab(pnlGeneral, btnTabGeneral);
            btnTabEditor.Click += (s, e) => SwitchTab(pnlEditor, btnTabEditor);
            btnTabNotifications.Click += (s, e) => SwitchTab(pnlNotifications, btnTabNotifications);

            btnSave.Click += BtnSave_Click;
            btnReset.Click += BtnReset_Click;
        }

        private void InitData()
        {
            // General
            cboLanguage.Items.AddRange(new object[] { "Tiếng Việt", "English", "日本語" });
            cboLanguage.SelectedIndex = 0;

            cboTimezone.Items.AddRange(new object[] { "GMT+7 (Vietnam)", "GMT+8 (Singapore)", "GMT+0 (UTC)" });
            cboTimezone.SelectedIndex = 0;

            // Editor
            cboTheme.Items.AddRange(new object[] { "Dark (Mặc định)", "Light", "Monokai" });
            cboTheme.SelectedIndex = 0;

            for (int i = 10; i <= 24; i += 2) cboFontSize.Items.Add(i.ToString());
            cboFontSize.SelectedIndex = 2; // 14

            cboTabSize.Items.AddRange(new object[] { "2 spaces", "4 spaces", "8 spaces" });
            cboTabSize.SelectedIndex = 1;

            // Checkbox defaults
            chkAutoSave.Checked = true;
            chkConfirmSubmit.Checked = true;
            chkLineNumbers.Checked = true;
            chkAutoCloseBrackets.Checked = true;
            chkNotiEmailNewProblem.Checked = true;
            chkInAppNoti.Checked = true;
        }

        private void SwitchTab(Panel activePanel, Button activeButton)
        {
            // Reset styles
            ResetButtonStyle(btnTabGeneral);
            ResetButtonStyle(btnTabEditor);
            ResetButtonStyle(btnTabNotifications);

            // Hide all panels
            pnlGeneral.Visible = false;
            pnlEditor.Visible = false;
            pnlNotifications.Visible = false;

            // Activate new tab
            activePanel.Visible = true;

            // Highlight Button (Blue background, White text)
            activeButton.BackColor = Color.FromArgb(235, 245, 255); // Xanh rất nhạt
            activeButton.ForeColor = Color.FromArgb(0, 120, 215);   // Chữ xanh đậm
            activeButton.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            // Thêm vạch màu bên trái để chỉ định active (nếu muốn cầu kỳ hơn, nhưng đổi màu nền là đủ đẹp)
        }

        private void ResetButtonStyle(Button btn)
        {
            btn.BackColor = Color.White;
            btn.ForeColor = Color.Black;
            btn.Font = new Font("Segoe UI", 10F, FontStyle.Regular);
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            // Logic lưu cài đặt xuống file config hoặc database
            MessageBox.Show("Đã lưu cài đặt thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void BtnReset_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc muốn khôi phục mặc định?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                InitData(); // Reload default values
            }
        }
    }
}