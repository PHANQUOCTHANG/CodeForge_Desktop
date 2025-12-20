using CodeForge_Desktop.Business.Interfaces;
using CodeForge_Desktop.Config;
using System;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using CodeForge_Desktop.DataAccess.Entities;
using CodeForge_Desktop.Presentation.Forms.Admin;
using CodeForge_Desktop.Presentation.Forms.Student;
using CodeForge_Desktop.Business.Models;
using CodeForge_Desktop.Business.Helpers;
using CodeForge_Desktop.Business.Services;

namespace CodeForge_Desktop.Presentation.Forms
{
    public partial class Login : Form
    {
        private IAuthService _authService;
        
        public Login()
        {
            _authService = new AuthService();
            InitializeComponent();

            // Gán sự kiện cho liên kết Đăng ký
            //this.lblRegisterLink.Click += lblRegisterLink_Click;
        }

        /// <summary>
        /// Xử lý sự kiện khi người dùng nhấn liên kết Đăng ký.
        /// </summary>
        private void lblRegisterLink_Click(object sender, EventArgs e)
        {
            this.Hide();
            Registration registrationForm = new Registration();
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

        /// <summary>
        /// Kiểm tra tính hợp lệ của tên đăng nhập
        /// </summary>
        private bool ValidateUsername(string username)
        {
            if (string.IsNullOrWhiteSpace(username))
            {
                MessageBox.Show("Vui lòng nhập tên đăng nhập!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (username.Length < 3)
            {
                MessageBox.Show("Tên đăng nhập phải có ít nhất 3 ký tự!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (username.Length > 30)
            {
                MessageBox.Show("Tên đăng nhập không được vượt quá 30 ký tự!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }

        /// <summary>
        /// Kiểm tra tính hợp lệ của mật khẩu
        /// - Tối thiểu 8 ký tự
        /// - Tối đa 16 ký tự
        /// - Phải chứa cả chữ (a-z, A-Z) và số (0-9)
        /// </summary>
        private bool ValidatePassword(string password)
        {
            if (string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Vui lòng nhập mật khẩu!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            // Kiểm tra độ dài
            if (password.Length < 8)
            {
                MessageBox.Show("Mật khẩu phải có ít nhất 8 ký tự!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (password.Length > 16)
            {
                MessageBox.Show("Mật khẩu không được vượt quá 16 ký tự!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            // Kiểm tra có chữ (a-z hoặc A-Z)
            if (!Regex.IsMatch(password, @"[a-zA-Z]"))
            {
                MessageBox.Show("Mật khẩu phải chứa ít nhất một chữ cái (a-z hoặc A-Z)!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            // Kiểm tra có số (0-9)
            if (!Regex.IsMatch(password, @"[0-9]"))
            {
                MessageBox.Show("Mật khẩu phải chứa ít nhất một số (0-9)!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text.Trim();

            // Kiểm tra validation
            if (!ValidateUsername(username))
                return;

            if (!ValidatePassword(password))
                return;

            try
            {
                Response<User> response = _authService.Login(username, password);
                
                if (response.Code == 1)
                {
                    User user = response.Data;
                    GlobalStore.user = user;

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

                    MessageBox.Show(response.Message,
                                    "Đăng nhập Thành công",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Information);

                    this.Hide();
                }
                else
                {
                    MessageBox.Show(response.Message,
                                    "Đăng nhập Thất bại",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Error);

                    // Xóa mật khẩu để bảo mật
                    txtPassword.Clear();
                    txtUsername.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi đăng nhập: {ex.Message}",
                                "Lỗi",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        // ... các phương thức khác ...
    }
}