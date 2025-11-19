using CodeForge_Desktop.Business.Interfaces;
using CodeForge_Desktop.Config;
using System;
using System.Windows.Forms;
using CodeForge_Desktop.DataAccess.Entities;
using CodeForge_Desktop.Presentation.Forms.Admin;
using CodeForge_Desktop.Presentation.Forms.Student;
using CodeForge_Desktop.Business.Models;

namespace CodeForge_Desktop.Presentation.Forms
{
    public partial class Login : Form
    {
        private readonly IAuthService _authService;
        public Login(IAuthService authService)
        {
            _authService = authService;
            InitializeComponent();

            // Gán sự kiện cho liên kết Đăng ký
            //this.lblRegisterLink.Click += lblRegisterLink_Click;
        }
   
        /// Xử lý sự kiện khi người dùng nhấn liên kết Đăng ký.
        private void lblRegisterLink_Click(object sender, EventArgs e)
        {

            this.Hide();
            Registration registrationForm = new Registration(_authService);
            registrationForm.Show();
        }

        private void Login_Load(object sender, EventArgs e)
        {
            //string msg;
            //if (DbContext.TestConnection(out msg))
            //    MessageBox.Show(msg, "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //else
            //    MessageBox.Show(msg, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text.Trim();
            Response<User> response = _authService.Login(username, password);
            if (response.Code == 1)
            {
                User user = response.Data;
                if (user.Role == "admin")
                {
                    MainFormAdmin mainFormAdmin = new MainFormAdmin();
                    mainFormAdmin.Show();
                   
                }
                else
                {
                    MainFormStudent mainFormStudent = new MainFormStudent();
                    mainFormStudent.Show();
                   
                }

                //this.Close();

                MessageBox.Show(response.Message,
                                       "Đăng ký Thành công",
                                       MessageBoxButtons.OK,
                                       MessageBoxIcon.Information);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        // ... các phương thức khác ...
    }
}