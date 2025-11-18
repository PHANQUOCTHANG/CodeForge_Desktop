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
    public partial class ucUserManagement : UserControl
    {
        public ucUserManagement()
        {
            InitializeComponent();
        }

        private void ucUserManagement_Load(object sender, EventArgs e)
        {
            txtSearch.Text = "Tìm kiếm user...";
            txtSearch.ForeColor = Color.Gray;
        }

        private void txtSearch_Enter(object sender, EventArgs e)
        {
            if (txtSearch.Text == "Tìm kiếm user...")
            {
                txtSearch.Text = "";
                txtSearch.ForeColor = Color.Black;
            }
        }

        private void txtSearch_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtSearch.Text))
            {
                txtSearch.Text = "Tìm kiếm user...";
                txtSearch.ForeColor = Color.Gray;
            }
        }
    }
}
