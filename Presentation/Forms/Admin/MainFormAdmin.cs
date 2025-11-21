using System;
using System.Drawing;
using System.Windows.Forms;
// Đảm bảo bạn đã có các UserControl này, nếu chưa có hãy comment lại hoặc tạo tạm
// using CodeForge_Desktop.Presentation.Forms.Admin.UserControls; 

namespace CodeForge_Desktop.Presentation.Forms.Admin
{
    public partial class MainFormAdmin : Form
    {
        public MainFormAdmin()
        {
            InitializeComponent();
        }

        private void MainFormAdmin_Load(object sender, EventArgs e)
        {
            // Mặc định load Dashboard khi mở form
            btnTrangChu.PerformClick();
        }

        // Hàm chung để tải UserControl vào Panel nội dung
        private void LoadUserControl(UserControl userControl)
        {
            pnlContent.Controls.Clear();
            userControl.Dock = DockStyle.Fill;
            pnlContent.Controls.Add(userControl);
            userControl.BringToFront();
        }

        // Hàm để đổi màu nút (Highlight nút đang chọn)
        private void SetActiveButton(Button activeButton)
        {
            // Reset tất cả các nút về mặc định
            foreach (Control ctrl in pnlMenu.Controls)
            {
                if (ctrl is Button btn)
                {
                    btn.BackColor = Color.Transparent;
                    btn.Font = new Font("Segoe UI", 10F, FontStyle.Regular);
                }
            }

            // Set màu cho nút đang chọn (Màu xanh dương đậm đà hơn)
            activeButton.BackColor = Color.FromArgb(0, 120, 215);
            activeButton.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
        }

        // --- CÁC SỰ KIỆN CLICK MENU ---

        private void btnTrangChu_Click(object sender, EventArgs e)
        {
            SetActiveButton(btnTrangChu);
            LoadUserControl(new ucAdminDashboard()); // Bỏ comment khi đã tạo UC này
        }

        private void btnQuanLyUsers_Click(object sender, EventArgs e)
        {
            SetActiveButton(btnQuanLyUsers);
            LoadUserControl(new ucUserManagement());
        }

        private void btnQuanLyAssignments_Click(object sender, EventArgs e)
        {
            SetActiveButton(btnQuanLyAssignments);
            LoadUserControl(new ucProblemManagement());
        }

        private void btnSystemLogs_Click(object sender, EventArgs e)
        {
            SetActiveButton(btnSystemLogs);
            LoadUserControl(new ucSystemLogs());
        }

        private void btnCaiDat_Click(object sender, EventArgs e)
        {
            SetActiveButton(btnCaiDat);
            LoadUserControl(new ucAdminSettings());
        }

        private void btnDangXuat_Click(object sender, EventArgs e)
        {
            // Xử lý đăng xuất, ví dụ:
            this.Close();
            // new LoginForm().Show(); 
        }

        private void btnCourseManagement_Click(object sender, EventArgs e)
        {
            SetActiveButton(btnCourseManagement);
            LoadUserControl(new ucCourseManagement());
        }
    }
}
