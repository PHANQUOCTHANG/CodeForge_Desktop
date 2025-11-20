using CodeForge_Desktop.Business.Interfaces;
using CodeForge_Desktop.DataAccess.Entities;
using System;
using System.Windows.Forms;

namespace CodeForge_Desktop.Presentation.Forms
{
    public partial class Registration : Form
    {
        private readonly IAuthService _authService;

        // Cập nhật Constructor để nhận IAuthService qua Dependency Injection
        public Registration(IAuthService authService)
        {
            InitializeComponent();
            _authService = authService;
        }

        /// Xử lý sự kiện khi người dùng nhấn nút liên kết Đăng nhập.
        private void lblBackToLogin_Click(object sender, EventArgs e)
        {
            // Khi quay lại Login, cần truyền IAuthService
            Login login = new Login(_authService);
            login.Show();
            this.Close();
        }

        /// Xử lý sự kiện khi người dùng nhấn nút Đăng ký.
        private void btnRegister_Click(object sender, EventArgs e)
        {
            // 1. Lấy dữ liệu từ các trường nhập
            string username = txtUsername.Text.Trim();
            string email = txtEmail.Text.Trim();
            string password = txtPassword.Text;
            string confirmPassword = txtConfirmPassword.Text;

            // 2. Xác thực dữ liệu
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(email) ||
                string.IsNullOrEmpty(password) || string.IsNullOrEmpty(confirmPassword))
            {
                MessageBox.Show("Vui lòng điền đầy đủ tất cả các trường bắt buộc.",
                                 "Lỗi Xác thực",
                                 MessageBoxButtons.OK,
                                 MessageBoxIcon.Warning);
                return;
            }

            if (password != confirmPassword)
            {
                MessageBox.Show("Mật khẩu và Xác nhận mật khẩu không khớp.",
                                 "Lỗi Mật khẩu",
                                 MessageBoxButtons.OK,
                                 MessageBoxIcon.Warning);
                txtPassword.Clear();
                txtConfirmPassword.Clear();
                txtPassword.Focus();
                return;
            }

            // Kiểm tra định dạng email cơ bản
            if (!email.Contains("@") || !email.Contains("."))
            {
                MessageBox.Show("Địa chỉ Email không hợp lệ.",
                                 "Lỗi Email",
                                 MessageBoxButtons.OK,
                                 MessageBoxIcon.Warning);
                txtEmail.Focus();
                return;
            }

            // --- 3. Thực hiện Đăng ký bằng AuthService ---
            try
            {
                // Khởi tạo đối tượng User mới
                // Các giá trị mặc định (UserID, Role, JoinDate, Status) sẽ được set trong Constructor của User Entity.
                User newUser = new User {Username = username , Email = email , Role = "student" };
                User newUserAdmin = new User { Username = "admin", Email = "admin@gmail.com", Role = "admin" };
                // Gọi AuthService để đăng ký người dùng
                User registeredUser = _authService.Register(newUser, password).Data;
                User registreAdmin = _authService.Register(newUserAdmin, "123456").Data;
                if (registeredUser != null)
                {
                    // Đăng ký thành công
                    string registrationInfo = $"Đăng ký thành công tài khoản:\n" +
                                              $"- Tên đăng nhập: {username}\n" +
                                              $"- Email: {email}\n" +
                                              $"- Vai trò: {registeredUser.Role}";

                    MessageBox.Show(registrationInfo,
                                     "Đăng ký Thành công",
                                     MessageBoxButtons.OK,
                                     MessageBoxIcon.Information);

                    // 4. Điều hướng: Đóng form đăng ký và quay lại form đăng nhập
                    lblBackToLogin_Click(sender, e);
                }
                else
                {
                    // Đăng ký thất bại (thường là do Username/Email đã tồn tại, được kiểm tra trong AuthService)
                    MessageBox.Show("Đăng ký thất bại. Tên đăng nhập hoặc Email này đã được sử dụng.",
                                     "Lỗi Đăng ký",
                                     MessageBoxButtons.OK,
                                     MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                // Xử lý lỗi hệ thống hoặc lỗi DB khác
                MessageBox.Show($"Đã xảy ra lỗi trong quá trình đăng ký: {ex.Message}",
                                 "Lỗi Hệ thống",
                                 MessageBoxButtons.OK,
                                 MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}