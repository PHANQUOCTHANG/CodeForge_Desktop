using CodeForge_Desktop.Business.Interfaces;
using CodeForge_Desktop.DataAccess.Entities;
using System;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using CodeForge_Desktop.Business.Models;
using CodeForge_Desktop.Business.Services;

namespace CodeForge_Desktop.Presentation.Forms
{
    public partial class Registration : Form
    {
        private IAuthService _authService;

        // Cập nhật Constructor để nhận IAuthService qua Dependency Injection
        public Registration()
        {
            InitializeComponent();
            _authService = new AuthService();
        }

        /// <summary>
        /// Xử lý sự kiện khi người dùng nhấn nút liên kết Đăng nhập.
        /// </summary>
        private void lblBackToLogin_Click(object sender, EventArgs e)
        {
            // Khi quay lại Login, cần truyền IAuthService
            Login login = new Login();
            login.Show();
            this.Close();
        }

        /// <summary>
        /// Kiểm tra tính hợp lệ của tên đăng nhập
        /// - Tối thiểu 3 ký tự
        /// - Tối đa 30 ký tự
        /// - Chỉ chứa chữ, số, và dấu gạch dưới
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

            // Kiểm tra định dạng: chỉ chứa chữ, số, dấu gạch dưới
            if (!Regex.IsMatch(username, @"^[a-zA-Z0-9_]+$"))
            {
                MessageBox.Show("Tên đăng nhập chỉ được chứa chữ, số và dấu gạch dưới (_)!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }

        /// <summary>
        /// Kiểm tra tính hợp lệ của email
        /// - Định dạng email chuẩn
        /// </summary>
        private bool ValidateEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                MessageBox.Show("Vui lòng nhập địa chỉ Email!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            // Regex kiểm tra email chuẩn
            string emailPattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            if (!Regex.IsMatch(email, emailPattern))
            {
                MessageBox.Show("Địa chỉ Email không hợp lệ. Vui lòng nhập đúng định dạng (ví dụ: user@example.com)!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (email.Length > 100)
            {
                MessageBox.Show("Địa chỉ Email không được vượt quá 100 ký tự!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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

        /// <summary>
        /// Kiểm tra xác nhận mật khẩu
        /// </summary>
        private bool ValidateConfirmPassword(string password, string confirmPassword)
        {
            if (string.IsNullOrWhiteSpace(confirmPassword))
            {
                MessageBox.Show("Vui lòng xác nhận mật khẩu!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (password != confirmPassword)
            {
                MessageBox.Show("Mật khẩu và Xác nhận mật khẩu không khớp!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPassword.Clear();
                txtConfirmPassword.Clear();
                txtPassword.Focus();
                return false;
            }

            return true;
        }

        /// <summary>
        /// Xử lý sự kiện khi người dùng nhấn nút Đăng ký.
        /// </summary>
        private void btnRegister_Click(object sender, EventArgs e)
        {
            // 1. Lấy dữ liệu từ các trường nhập
            string username = txtUsername.Text.Trim();
            string email = txtEmail.Text.Trim();
            string password = txtPassword.Text;
            string confirmPassword = txtConfirmPassword.Text;

            // 2. Xác thực dữ liệu
            if (!ValidateUsername(username))
                return;

            if (!ValidateEmail(email))
                return;

            if (!ValidatePassword(password))
                return;

            if (!ValidateConfirmPassword(password, confirmPassword))
                return;

            // --- 3. Thực hiện Đăng ký bằng AuthService ---
            try
            {
                // Khởi tạo đối tượng User mới
                User newUser = new User
                {
                    Username = username,
                    Email = email,
                    Role = "student"
                };

                // Gọi AuthService để đăng ký người dùng
                Response<User> registerResponse = _authService.Register(newUser, password);

                if (registerResponse.Code == 1 && registerResponse.Data != null)
                {
                    // Đăng ký thành công
                    User registeredUser = registerResponse.Data;
                    string registrationInfo = $"Đăng ký thành công tài khoản:\n" +
                                              $"- Tên đăng nhập: {registeredUser.Username}\n" +
                                              $"- Email: {registeredUser.Email}\n" +
                                              $"- Vai trò: {registeredUser.Role}\n\n" +
                                              $"Bạn có thể đăng nhập ngay bây giờ.";

                    MessageBox.Show(registrationInfo,
                                     "Đăng ký Thành công",
                                     MessageBoxButtons.OK,
                                     MessageBoxIcon.Information);

                    // 4. Điều hướng: Đóng form đăng ký và quay lại form đăng nhập
                    lblBackToLogin_Click(sender, e);
                }
                else
                {
                    // Đăng ký thất bại
                    string errorMessage = registerResponse.Message ?? "Đăng ký thất bại. Vui lòng thử lại.";
                    MessageBox.Show(errorMessage,
                                     "Lỗi Đăng ký",
                                     MessageBoxButtons.OK,
                                     MessageBoxIcon.Error);

                    // Xóa mật khẩu để bảo mật
                    txtPassword.Clear();
                    txtConfirmPassword.Clear();
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