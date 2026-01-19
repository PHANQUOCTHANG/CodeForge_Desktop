using System;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using CodeForge_Desktop.Business.Helpers;
using CodeForge_Desktop.Business.Interfaces;
using CodeForge_Desktop.Business.Models;
using CodeForge_Desktop.Business.Services;
using CodeForge_Desktop.DataAccess.Entities;
using CodeForge_Desktop.DataAccess.Repositories;

namespace CodeForge_Desktop.Presentation.Forms
{
    public partial class ForgotPassword : Form
    {
        private IUserService _userService;
        private User _currentUser = null;
        private System.Windows.Forms.Timer _otpTimer;
        private int _otpCountdown = 300;
        private const int OTP_EXPIRATION_SECONDS = 300;

        public ForgotPassword()
        {
            InitializeComponent();
            _userService = new UserService(new UserRepository());
            SetupOtpTimer();
        }

        private void ForgotPassword_Load(object sender, EventArgs e)
        {
            pnlOtpVerification.Visible = false;
            pnlChangePassword.Visible = false;
            btnChangePassword.Visible = false;
            lblStepInfo.Text = "📝 Bước 1: Nhập Email";
            
            // Setup placeholder text
            SetupPlaceholder();
            txtEmail.Focus();
        }

        /// <summary>
        /// Setup placeholder text cho TextBox (vì .NET Framework 4.7.2 không có PlaceholderText)
        /// </summary>
        private void SetupPlaceholder()
        {
            // Email placeholder
            if (string.IsNullOrWhiteSpace(txtEmail.Text))
            {
                txtEmail.Text = "Nhập địa chỉ email của bạn";
                txtEmail.ForeColor = System.Drawing.Color.Gray;
            }

            txtEmail.GotFocus += (s, e) =>
            {
                if (txtEmail.Text == "Nhập địa chỉ email của bạn")
                {
                    txtEmail.Text = "";
                    txtEmail.ForeColor = System.Drawing.Color.Black;
                }
            };

            txtEmail.LostFocus += (s, e) =>
            {
                if (string.IsNullOrWhiteSpace(txtEmail.Text))
                {
                    txtEmail.Text = "Nhập địa chỉ email của bạn";
                    txtEmail.ForeColor = System.Drawing.Color.Gray;
                }
            };
        }

        private void SetupOtpTimer()
        {
            _otpTimer = new System.Windows.Forms.Timer();
            _otpTimer.Interval = 1000;
            _otpTimer.Tick += OtpTimer_Tick;
        }

        private void OtpTimer_Tick(object sender, EventArgs e)
        {
            _otpCountdown--;
            int minutes = _otpCountdown / 60;
            int seconds = _otpCountdown % 60;
            lblOtpTimer.Text = $"⏱️ Hết hạn sau: {minutes}:{seconds:D2}";

            if (_otpCountdown <= 0)
            {
                _otpTimer.Stop();
                MessageBox.Show("Mã OTP đã hết hạn. Vui lòng yêu cầu mã mới.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                OtpHelper.ClearOtp(_currentUser.Email);
                pnlOtpVerification.Visible = false;
                pnlEmailVerification.Enabled = true;
                lblStepInfo.Text = "Bước 1: Nhập email";
            }
        }

        /// <summary>
        /// Bước 1: Gửi OTP qua email
        /// </summary>
        private void btnSendOtp_Click(object sender, EventArgs e)
        {
            string email = txtEmail.Text.Trim();

            // Xóa placeholder text khi validate
            if (email == "Nhập địa chỉ email của bạn")
                email = "";

            if (string.IsNullOrWhiteSpace(email))
            {
                MessageBox.Show("Vui lòng nhập email!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtEmail.Focus();
                return;
            }

            if (!IsValidEmail(email))
            {
                MessageBox.Show("Email không hợp lệ! (Vd: user@example.com)", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtEmail.Focus();
                return;
            }

            try
            {
                // Tìm user với email này
                var allUsers = _userService.GetAllUsers();
                if (allUsers == null || allUsers.Count == 0)
                {
                    MessageBox.Show("Email không tồn tại trong hệ thống!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                _currentUser = null;
                foreach (var user in allUsers)
                {
                    if (user.Email.Equals(email, StringComparison.OrdinalIgnoreCase))
                    {
                        _currentUser = user;
                        break;
                    }
                }

                if (_currentUser == null)
                {
                    MessageBox.Show("Email không tồn tại trong hệ thống!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Tạo và gửi OTP
                string otp = OtpHelper.GenerateOtp();
                OtpHelper.SaveOtp(_currentUser.Email, otp);
                bool emailSent = OtpHelper.SendOtpEmail(_currentUser.Email, otp);

                if (emailSent)
                {
                    // ✅ Hiển thị notification 2 giây rồi tự động tắt
                    ShowTemporaryNotification($"✓ Mã OTP đã được gửi tới:\n{_currentUser.Email}", "Thành công");
                    
                    // Chuyển sang panel OTP sau 0.5 giây
                    System.Windows.Forms.Timer transitionTimer = new System.Windows.Forms.Timer { Interval = 500 };
                    transitionTimer.Tick += (s, e) =>
                    {
                        transitionTimer.Stop();
                        pnlEmailVerification.Enabled = false;
                        pnlOtpVerification.Visible = true;
                        lblStepInfo.Text = "🔐 Bước 2: Xác thực mã OTP";
                        txtOtp.Clear();
                        txtOtp.Focus();

                        _otpCountdown = OTP_EXPIRATION_SECONDS;
                        _otpTimer.Start();
                        transitionTimer.Dispose();
                    };
                    transitionTimer.Start();
                }
                else
                {
                    MessageBox.Show("Không thể gửi email OTP. Vui lòng thử lại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Bước 2: Xác thực OTP
        /// </summary>
        private void btnVerifyOtp_Click(object sender, EventArgs e)
        {
            string inputOtp = txtOtp.Text.Trim();

            if (string.IsNullOrWhiteSpace(inputOtp))
            {
                MessageBox.Show("Vui lòng nhập mã OTP!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtOtp.Focus();
                return;
            }

            if (inputOtp.Length != 6)
            {
                MessageBox.Show("Mã OTP phải có 6 chữ số!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtOtp.Focus();
                return;
            }

            if (OtpHelper.VerifyOtp(_currentUser.Email, inputOtp))
            {
                MessageBox.Show("✓ OTP hợp lệ!\nVui lòng đặt mật khẩu mới.", "Thành công",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                _otpTimer.Stop();
                pnlOtpVerification.Visible = false;
                pnlChangePassword.Visible = true;
                btnChangePassword.Visible = true;
                lblStepInfo.Text = "Bước 3: Đặt mật khẩu mới";
                txtNewPassword.Clear();
                txtConfirmPassword.Clear();
                txtNewPassword.Focus();
            }
            else
            {
                MessageBox.Show("❌ Mã OTP không hợp lệ hoặc đã hết hạn!\nVui lòng thử lại.", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtOtp.Clear();
                txtOtp.Focus();
            }
        }

        /// <summary>
        /// Gửi lại OTP
        /// </summary>
        private void btnResendOtp_Click(object sender, EventArgs e)
        {
            _otpTimer.Stop();
            OtpHelper.ClearOtp(_currentUser.Email);
            
            string otp = OtpHelper.GenerateOtp();
            OtpHelper.SaveOtp(_currentUser.Email, otp);
            OtpHelper.SendOtpEmail(_currentUser.Email, otp);

            MessageBox.Show("✓ Mã OTP mới đã được gửi!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
            
            _otpCountdown = OTP_EXPIRATION_SECONDS;
            _otpTimer.Start();
            txtOtp.Clear();
            txtOtp.Focus();
        }

        /// <summary>
        /// Bước 3: Đổi mật khẩu
        /// </summary>
        private void btnChangePassword_Click(object sender, EventArgs e)
        {
            string newPassword = txtNewPassword.Text.Trim();
            string confirmPassword = txtConfirmPassword.Text.Trim();

            if (string.IsNullOrWhiteSpace(newPassword))
            {
                MessageBox.Show("Vui lòng nhập mật khẩu mới!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNewPassword.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(confirmPassword))
            {
                MessageBox.Show("Vui lòng xác nhận mật khẩu!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtConfirmPassword.Focus();
                return;
            }

            if (!ValidatePassword(newPassword))
                return;

            if (newPassword != confirmPassword)
            {
                MessageBox.Show("Mật khẩu xác nhận không khớp!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtConfirmPassword.Clear();
                txtConfirmPassword.Focus();
                return;
            }

            try
            {
                var response = ChangeUserPassword(_currentUser.UserID, newPassword);

                if (response.Code == 1)
                {
                    MessageBox.Show("✓ Đổi mật khẩu thành công!\nVui lòng đăng nhập lại.", "Thành công",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
                else
                {
                    MessageBox.Show($"❌ Lỗi: {response.Message}", "Thất bại", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Kiểm tra định dạng email
        /// </summary>
        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Kiểm tra tính hợp lệ của mật khẩu
        /// </summary>
        private bool ValidatePassword(string password)
        {
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

            if (!Regex.IsMatch(password, @"[a-zA-Z]"))
            {
                MessageBox.Show("Mật khẩu phải chứa ít nhất một chữ cái!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (!Regex.IsMatch(password, @"[0-9]"))
            {
                MessageBox.Show("Mật khẩu phải chứa ít nhất một số!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }

        private Response<User> ChangeUserPassword(Guid userId, string newPassword)
        {
            try
            {
                var userRepository = new UserRepository();
                var user = userRepository.GetById(userId);

                if (user == null)
                {
                    return new Response<User>
                    {
                        Code = 0,
                        Message = "Không tìm thấy người dùng!"
                    };
                }

                user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(newPassword);
                int updateResult = userRepository.Update(user);

                if (updateResult != -1)
                {
                    System.Diagnostics.Debug.WriteLine($"✓ Password changed for: {user.Username}");
                    return new Response<User>
                    {
                        Code = 1,
                        Message = "Đổi mật khẩu thành công!",
                        Data = user
                    };
                }
                else
                {
                    return new Response<User>
                    {
                        Code = 0,
                        Message = "Lỗi khi cập nhật mật khẩu!"
                    };
                }
            }
            catch (Exception ex)
            {
                return new Response<User>
                {
                    Code = 0,
                    Message = $"Lỗi: {ex.Message}"
                };
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            _otpTimer?.Stop();
            if (!string.IsNullOrWhiteSpace(_currentUser?.Email))
            {
                OtpHelper.ClearOtp(_currentUser.Email);
            }
            this.Close();
        }

        private void btnToggleNewPassword_Click(object sender, EventArgs e)
        {
            txtNewPassword.PasswordChar = txtNewPassword.PasswordChar == '*' ? '\0' : '*';
            txtNewPassword.Focus();
        }

        private void btnToggleConfirmPassword_Click(object sender, EventArgs e)
        {
            txtConfirmPassword.PasswordChar = txtConfirmPassword.PasswordChar == '*' ? '\0' : '*';
            txtConfirmPassword.Focus();
        }

        /// <summary>
        /// Hiển thị thông báo tạm thời (2 giây) rồi tự động biến mất
        /// </summary>
        private void ShowTemporaryNotification(string message, string title)
        {
            var notificationForm = new Form
            {
                Text = title,
                Width = 400,
                Height = 120,
                StartPosition = FormStartPosition.CenterScreen,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                MaximizeBox = false,
                MinimizeBox = false,
                BackColor = System.Drawing.Color.FromArgb(76, 175, 80),
                TopMost = true
            };

            var label = new Label
            {
                Text = message,
                Dock = DockStyle.Fill,
                TextAlign = System.Drawing.ContentAlignment.MiddleCenter,
                ForeColor = System.Drawing.Color.White,
                Font = new System.Drawing.Font("Segoe UI", 11, System.Drawing.FontStyle.Bold)
            };

            notificationForm.Controls.Add(label);

            var timer = new System.Windows.Forms.Timer { Interval = 2000 };
            timer.Tick += (s, e) =>
            {
                timer.Stop();
                notificationForm.Close();
                notificationForm.Dispose();
            };
            timer.Start();

            notificationForm.Show();
        }
    }
}