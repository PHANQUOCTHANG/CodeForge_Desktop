using System;
using System.Drawing;
using System.Windows.Forms;

namespace CodeForge_Desktop.Presentation.Forms.Student
{
    public partial class MainFormStudent : Form
    {
        public MainFormStudent()
        {
            InitializeComponent();
        }

        private void MainFormStudent_Load(object sender, EventArgs e)
        {
            // Mặc định load Dashboard
            btnTrangChu.PerformClick();
        }

        private void LoadUserControl(UserControl userControl)
        {
            // Xóa control cũ (nếu muốn tiết kiệm bộ nhớ, hoặc giữ lại nếu muốn cache)
            pnlContent.Controls.Clear();

            userControl.Dock = DockStyle.Fill;
            pnlContent.Controls.Add(userControl);
            userControl.BringToFront();
        }

        // Hàm helper để đổi màu nút đang chọn
        private void SetActiveButton(Button activeButton)
        {
            // Reset màu các nút khác
            foreach (Control ctrl in pnlMenu.Controls)
            {
                if (ctrl is Button btn)
                {
                    btn.BackColor = Color.Transparent;
                    btn.Font = new Font("Segoe UI", 10F, FontStyle.Regular);
                }
            }

            // Set màu nút active
            activeButton.BackColor = Color.FromArgb(0, 120, 215); // Màu xanh nổi bật
            activeButton.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
        }

        private void btnTrangChu_Click(object sender, EventArgs e)
        {
            SetActiveButton(btnTrangChu);
            ShowDashboard();
        }

        private void btnDanhSachBaiTap_Click(object sender, EventArgs e)
        {
            SetActiveButton(btnDanhSachBaiTap);
            ShowProblemList();
        }

        private void btnLichSuNopBai_Click(object sender, EventArgs e)
        {
            SetActiveButton(btnLichSuNopBai);
            LoadUserControl(new ucSubmissions());
        }

        private void btnCaiDat_Click(object sender, EventArgs e)
        {
            SetActiveButton(btnCaiDat);
            // Giả sử có ucStudentSettings
            LoadUserControl(new ucStudentSettings());
        }

        private void ShowDashboard()
        {
            var dashboard = new ucStudentDashboard();

            // KẾT NỐI DÂY: Khi dashboard bắn sự kiện -> Gọi click của nút Sidebar tương ứng
            // Điều này giúp tái sử dụng code chuyển trang và đổi màu nút
            dashboard.ProblemListClicked += (s, args) => btnDanhSachBaiTap.PerformClick();
            dashboard.SubmissionsClicked += (s, args) => btnLichSuNopBai.PerformClick();
            dashboard.SettingsClicked += (s, args) => btnCaiDat.PerformClick();

            LoadUserControl(dashboard);
        }

        private void ShowProblemList()
        {
            var problemList = new ucProblemList();

            // Lắng nghe sự kiện: Khi chọn bài tập -> Chuyển sang màn hình làm bài
            problemList.ProblemClicked += (s, problemName) =>
            {
                ShowProblemDetail(problemName);
            };

            LoadUserControl(problemList);
        }

        private void ShowProblemDetail(string problemName)
        {
            // Không cần đổi màu menu vì vẫn đang ở mục "Bài tập" (hoặc có thể tắt hết màu nếu muốn)

            var detailView = new ucProblemDetail();
            detailView.SetProblemTitle(problemName); // Truyền tên bài tập vào

            // Lắng nghe sự kiện: Khi bấm "Quay lại" -> Quay về danh sách bài tập
            detailView.BackButtonClicked += (s, args) =>
            {
                btnDanhSachBaiTap.PerformClick(); // Giả lập bấm nút menu để load lại danh sách
            };

            LoadUserControl(detailView);
        }

    }
}