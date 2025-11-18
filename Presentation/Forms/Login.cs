using System;
using System.Windows.Forms;

namespace CodeForge_Desktop.Presentation.Forms
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
            // Gán sự kiện cho liên kết Đăng ký
            //this.lblRegisterLink.Click += lblRegisterLink_Click;
        }
   
        /// Xử lý sự kiện khi người dùng nhấn liên kết Đăng ký.
        private void lblRegisterLink_Click(object sender, EventArgs e)
        {

            this.Hide();
            Registration registrationForm = new Registration();
            registrationForm.Show();

        }

        // ... các phương thức khác ...
    }
}