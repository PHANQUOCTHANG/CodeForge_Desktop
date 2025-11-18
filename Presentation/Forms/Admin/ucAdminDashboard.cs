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
    public partial class ucAdminDashboard : UserControl
    {
        public ucAdminDashboard()
        {
            InitializeComponent();
        }

        private void btnUIUser_Click(object sender, EventArgs e)
        {
            pnlContent.Controls.Clear();
            ucUserManagement ucUserManagement = new ucUserManagement();
            ucUserManagement.Dock = DockStyle.Fill;
            pnlContent.Controls.Add(ucUserManagement);

        }

        private void btnUIAssignment_Click(object sender, EventArgs e)
        {
            pnlContent.Controls.Clear();
            ucProblemManagement ucProblemManagement = new ucProblemManagement();
            ucProblemManagement.Dock = DockStyle.Fill;
            pnlContent.Controls.Add(ucProblemManagement);
        }

        private void btnUILog_Click(object sender, EventArgs e)
        {
            pnlContent.Controls.Clear();
            ucSystemLogs ucSystemLogs = new ucSystemLogs();
            ucSystemLogs.Dock = DockStyle.Fill;
            pnlContent.Controls.Add(ucSystemLogs);
        }
    }
}
