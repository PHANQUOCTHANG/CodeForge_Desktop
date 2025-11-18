using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
            LoadUserControl(new ucAdminDashboard());
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
            LoadUserControl(new ucAdminDashboard());
        }

        private void btnQuanLyUsers_Click(object sender, EventArgs e)
        {
            LoadUserControl(new ucUserManagement());
        }

        private void btnQuanLyAssignments_Click(object sender, EventArgs e)
        {
            LoadUserControl(new ucProblemManagement());
        }

        private void btnSystemLogs_Click(object sender, EventArgs e)
        {
            LoadUserControl(new ucSystemLogs());
        }

        private void btnCaiDat_Click(object sender, EventArgs e)
        {
            LoadUserControl(new ucAdminSettings());
        }
    }
}
