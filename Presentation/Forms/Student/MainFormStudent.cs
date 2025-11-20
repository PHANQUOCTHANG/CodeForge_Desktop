using CodeForge_Desktop.Presentation.Forms.Student.UserControls;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace CodeForge_Desktop.Presentation.Forms.Student
{
    public partial class MainFormStudent : Form
    {
        // Singleton instance để dễ Navigate từ các UserControl
        public static MainFormStudent Instance { get; private set; }

        // Navigation stack để hỗ trợ GoBack
        private readonly Stack<UserControl> _navigationStack = new Stack<UserControl>();

        public MainFormStudent()
        {
            InitializeComponent();
            Instance = this;
        }

        private void MainFormStudent_Load(object sender, EventArgs e)
        {
            btnTrangChu.PerformClick();
        }   

        private void LoadUserControl(UserControl userControl)
        {
            pnlContent.Controls.Clear();
            userControl.Dock = DockStyle.Fill;
            pnlContent.Controls.Add(userControl);
            userControl.BringToFront();
        }

        // Public method để chuyển trang và push màn hình hiện tại vào stack
        public void NavigateTo(UserControl userControl)
        {
            // Nếu đang có control hiện tại, push vào stack
            if (pnlContent.Controls.Count > 0)
            {
                var current = pnlContent.Controls[0] as UserControl;
                if (current != null)
                {
                    _navigationStack.Push(current);
                }
                pnlContent.Controls.Clear();
            }

            userControl.Dock = DockStyle.Fill;
            pnlContent.Controls.Add(userControl);
            userControl.BringToFront();
        }

        // Quay lại màn hình trước đó (nếu có)
        public void GoBack()
        {
            if (_navigationStack.Count == 0) return;

            var prev = _navigationStack.Pop();
            pnlContent.Controls.Clear();
            prev.Dock = DockStyle.Fill;
            pnlContent.Controls.Add(prev);
            prev.BringToFront();
        }

        // Hàm helper để đổi màu nút đang chọn
        private void SetActiveButton(Button activeButton)
        {
            foreach (Control ctrl in pnlMenu.Controls)
            {
                if (ctrl is Button btn)
                {
                    btn.BackColor = Color.Transparent;
                    btn.Font = new Font("Segoe UI", 10F, FontStyle.Regular);
                }
            }

            activeButton.BackColor = Color.FromArgb(0, 120, 215);
            activeButton.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
        }

        private void btnTrangChu_Click(object sender, EventArgs e)
        {
            SetActiveButton(btnTrangChu);
            ShowDashboard();
        }

        private void btnDanhSachBaiTap_Click(object sender, EventArgs e)
        {
            if (!pnlSidebar.Visible) pnlSidebar.Visible = true;
            SetActiveButton(btnDanhSachBaiTap);
            ShowProblemList();
        }

        private void btnLichSuNopBai_Click(object sender, EventArgs e)
        {
            SetActiveButton(btnLichSuNopBai);
            LoadUserControl(new ucSubmissions());
        }

        private void btnDanhSachKhoaHoc_Click(object sender, EventArgs e)
        {
            SetActiveButton(btnDanhSachKhoaHoc);
            LoadUserControl(new ucCourseList());
        }

        private void btnCaiDat_Click(object sender, EventArgs e)
        {
            SetActiveButton(btnCaiDat);
            LoadUserControl(new ucStudentSettings());
        }

        private void ShowDashboard()
        {
            var dashboard = new ucStudentDashboard();

            // KẾT NỐI DÂY: Khi dashboard bắn sự kiện -> Gọi click của nút Sidebar tương ứng
            dashboard.ProblemListClicked += (s, args) => btnDanhSachBaiTap.PerformClick();
            dashboard.SubmissionsClicked += (s, args) => btnLichSuNopBai.PerformClick();
            dashboard.SettingsClicked += (s, args) => btnCaiDat.PerformClick();

            LoadUserControl(dashboard);
        }

        private void ShowProblemList()
        {
            var problemList = new ucProblemList();

            // Lắng nghe sự kiện: Khi chọn bài tập -> Chuyển sang màn hình chi tiết (gửi ProblemID)
            problemList.ProblemClicked += (s, problemId) =>
            {
                ShowProblemDetail(problemId);
            };

            LoadUserControl(problemList);
        }

        private void ShowProblemDetail(Guid problemId)
        {
            pnlSidebar.Visible = false;

            var detailView = new ucProblemDetail();
            
            // Tải dữ liệu bài tập theo ID
            detailView.LoadProblemById(problemId);

            detailView.BackButtonClicked += (s, args) =>
            {
                pnlSidebar.Visible = true;
                btnDanhSachBaiTap.PerformClick();
            };

            LoadUserControl(detailView);
        }
    }
}