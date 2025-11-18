using System;
using System.Windows.Forms;

namespace CodeForge_Desktop.Presentation.Forms
{
    public partial class Registration : Form
    {
       
        public Registration()
        {
            InitializeComponent();
        }

      
        /// Xử lý sự kiện khi người dùng nhấn nút liên kết Đăng nhập.
       
        private void lblBackToLogin_Click(object sender, EventArgs e)
        {
            Login login = new Login();
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

            // Kiểm tra định dạng email cơ bản (ví dụ: chỉ cần chứa '@')
            if (!email.Contains("@") || !email.Contains("."))
            {
                MessageBox.Show("Địa chỉ Email không hợp lệ.",
                                "Lỗi Email",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
                txtEmail.Focus();
                return;
            }

            // --- 3. Thực hiện Đăng ký (Logic Mock) ---

            // Trong ứng dụng thực tế:
            // - Bạn sẽ tạo PasswordHash (ví dụ: bằng BCrypt)
            // - Gọi lớp Service/Repository để thêm người dùng vào SQL Server (UserID, JoinDate, Role, Status, IsDeleted sẽ được set tự động hoặc mặc định)
            // - Xử lý trường hợp Username/Email đã tồn tại (UNIQUE constraint)

            try
            {
                // Giả định đăng ký thành công sau khi xác thực hợp lệ
                string registrationInfo = $"Đăng ký thành công tài khoản:\n" +
                                          $"- Tên đăng nhập: {username}\n" +
                                          $"- Email: {email}\n" +
                                          $"- Vai trò mặc định: student";

                MessageBox.Show(registrationInfo,
                                "Đăng ký Thành công",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);

                // 4. Điều hướng: Đóng form đăng ký và quay lại form đăng nhập
                this.lblBackToLogin_Click(sender, e);
            }
            catch (Exception ex)
            {
                // Xử lý các lỗi có thể xảy ra trong môi trường thực (ví dụ: lỗi kết nối DB, lỗi Unique Constraint)
                MessageBox.Show($"Đã xảy ra lỗi trong quá trình đăng ký: {ex.Message}",
                                "Lỗi Hệ thống",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
            }
        }
    }
}