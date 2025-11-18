using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            LoadUserControl(new ucStudentDashboard());
        }
        private void LoadUserControl(UserControl userControl)
        {
            pnlContent.Controls.Clear();

            pnlContent.Controls.Add(userControl);

            userControl.Dock = DockStyle.Fill;

            // Hiển thị nó
            userControl.BringToFront();
        }

        private void btnTrangChu_Click(object sender, EventArgs e)
        {
            LoadUserControl(new ucStudentDashboard());
        }

        private void btnDanhSachBaiTap_Click(object sender, EventArgs e)
        {
            LoadUserControl(new ucProblemList());
        }

        private void btnLichSuNopBai_Click(object sender, EventArgs e)
        {
            LoadUserControl(new ucSubmissions());
        }

        private void btnCaiDat_Click(object sender, EventArgs e)
        {
            LoadUserControl(new ucStudentSettings());
        }
    }
}
