using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace CodeForge_Desktop.Presentation.Forms
{
    public partial class ForgotPassword : Form
    {
        private System.Windows.Forms.Timer _otpTimer;
        private int _otpCountdown = 300;
        private const int OTP_EXPIRATION_SECONDS = 300;

        // Front-end state for reset flow (use backend API instead of local OtpHelper/UserRepository)
        private string _emailForReset = null;
        private string _lastOtpEntered = null;
        private bool _otpVerified = false;

        // HttpClient for calling backend API
        private readonly HttpClient _httpClient;

        public ForgotPassword()
        {
            InitializeComponent();

            // Configure HttpClient to point to backend auth endpoints
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri("https://localhost:7225/api/Auth/")
            };

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
                // Reset local state (backend handles OTP lifecycle)
                _emailForReset = null;
                _otpVerified = false;
                pnlOtpVerification.Visible = false;
                pnlEmailVerification.Enabled = true;
                lblStepInfo.Text = "Bước 1: Nhập email";
            }
        }

        /// <summary>
        /// Bước 1: Gửi OTP qua email (calls backend)
        /// </summary>
        private async void btnSendOtp_Click(object sender, EventArgs e)
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
                var (success, otpForDev, message, error) = await SendForgotPasswordOtpAsync(email);
                if (!success)
                {
                    MessageBox.Show($"Không thể gửi OTP: {error ?? message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                _emailForReset = email;
                _otpVerified = false;

                if (!string.IsNullOrEmpty(otpForDev))
                {
                    // Development mode — backend returned OTP for dev
                    MessageBox.Show($"✓ Mã OTP (dev): {otpForDev}\nMã này đã được gửi tới email (dev).", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show($"✓ Mã OTP đã được yêu cầu. Nếu tài khoản tồn tại, mã sẽ được gửi tới:\n{email}", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                pnlEmailVerification.Enabled = false;
                pnlOtpVerification.Visible = true;
                lblStepInfo.Text = "🔐 Bước 2: Xác thực mã OTP";
                txtOtp.Clear();
                txtOtp.Focus();

                _otpCountdown = OTP_EXPIRATION_SECONDS;
                _otpTimer.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Bước 2: Xác thực OTP (calls backend)
        /// </summary>
        private async void btnVerifyOtp_Click(object sender, EventArgs e)
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

            if (string.IsNullOrEmpty(_emailForReset))
            {
                MessageBox.Show("Vui lòng yêu cầu mã OTP trước.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                var (verified, message, error) = await VerifyOtpAsync(_emailForReset, inputOtp);
                if (verified)
                {
                    MessageBox.Show("✓ OTP hợp lệ!\nVui lòng đặt mật khẩu mới.", "Thành công",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);

                    _otpTimer.Stop();
                    _otpVerified = true;
                    _lastOtpEntered = inputOtp;

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
                    MessageBox.Show($"❌ OTP không hợp lệ: {error ?? message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtOtp.Clear();
                    txtOtp.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Gửi lại OTP (calls backend)
        /// </summary>
        private async void btnResendOtp_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(_emailForReset))
            {
                MessageBox.Show("Vui lòng nhập email và gửi mã OTP trước.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            _otpTimer.Stop();

            try
            {
                var (success, otpForDev, message, error) = await SendForgotPasswordOtpAsync(_emailForReset);
                if (!success)
                {
                    MessageBox.Show($"Không thể gửi OTP: {error ?? message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (!string.IsNullOrEmpty(otpForDev))
                {
                    MessageBox.Show($"✓ Mã OTP mới (dev): {otpForDev}", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("✓ Mã OTP mới đã được gửi!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                _otpCountdown = OTP_EXPIRATION_SECONDS;
                _otpTimer.Start();
                txtOtp.Clear();
                txtOtp.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Bước 3: Đổi mật khẩu (calls backend reset-password)
        /// </summary>
        private async void btnChangePassword_Click(object sender, EventArgs e)
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

            if (string.IsNullOrEmpty(_emailForReset))
            {
                MessageBox.Show("Vui lòng bắt đầu quy trình quên mật khẩu trước.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!_otpVerified)
            {
                MessageBox.Show("Vui lòng xác thực OTP trước khi đổi mật khẩu.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                var (success, message, error) = await ResetPasswordAsync(_emailForReset, _lastOtpEntered, newPassword);
                if (success)
                {
                    MessageBox.Show("✓ Đổi mật khẩu thành công!\nVui lòng đăng nhập lại.", "Thành công",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
                else
                {
                    MessageBox.Show($"❌ Lỗi khi đổi mật khẩu: {error ?? message}", "Thất bại", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private void btnCancel_Click(object sender, EventArgs e)
        {
            _otpTimer?.Stop();
            // Backend manages OTP lifecycle; just reset local state
            _emailForReset = null;
            _otpVerified = false;
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

        // ---- Backend API helpers ----

        // Returns: (success, otpForDev, message, error)
        private async Task<(bool success, string otpForDev, string message, string error)> SendForgotPasswordOtpAsync(string email)
        {
            var payload = new { email };
            var content = new StringContent(JsonConvert.SerializeObject(payload), Encoding.UTF8, "application/json");
            var resp = await _httpClient.PostAsync("forgot-password", content);
            var respStr = await resp.Content.ReadAsStringAsync();

            if (!resp.IsSuccessStatusCode)
            {
                // Try to extract message if response body is JSON
                try
                {
                    var j = JObject.Parse(respStr);
                    return (false, null, j["message"]?.ToString(), j["errors"]?.ToString() ?? resp.ReasonPhrase);
                }
                catch
                {
                    return (false, null, respStr, resp.ReasonPhrase);
                }
            }

            try
            {
                var j = JObject.Parse(respStr);
                var data = j["data"]?.ToString();
                var message = j["message"]?.ToString();
                return (true, data, message, null);
            }
            catch (Exception ex)
            {
                return (true, null, respStr, null);
            }
        }

        // Returns: (verified, message, error)
        private async Task<(bool verified, string message, string error)> VerifyOtpAsync(string email, string otp)
        {
            var payload = new { email, otp };
            var content = new StringContent(JsonConvert.SerializeObject(payload), Encoding.UTF8, "application/json");
            var resp = await _httpClient.PostAsync("verify-otp", content);
            var respStr = await resp.Content.ReadAsStringAsync();

            if (!resp.IsSuccessStatusCode)
            {
                try
                {
                    var j = JObject.Parse(respStr);
                    return (false, j["message"]?.ToString(), j["errors"]?.ToString() ?? resp.ReasonPhrase);
                }
                catch
                {
                    return (false, respStr, resp.ReasonPhrase);
                }
            }

            try
            {
                var j = JObject.Parse(respStr);
                return (true, j["message"]?.ToString(), null);
            }
            catch
            {
                return (true, respStr, null);
            }
        }

        // Returns: (success, message, error)
        private async Task<(bool success, string message, string error)> ResetPasswordAsync(string email, string otp, string newPassword)
        {
            // Send email, otp and newPassword. Backend may ignore otp if VerifyOtp already validated it.
            var payload = new
            {
                email,
                otp,
                newPassword
            };
            var content = new StringContent(JsonConvert.SerializeObject(payload), Encoding.UTF8, "application/json");
            var resp = await _httpClient.PostAsync("reset-password", content);
            var respStr = await resp.Content.ReadAsStringAsync();

            if (!resp.IsSuccessStatusCode)
            {
                try
                {
                    var j = JObject.Parse(respStr);
                    return (false, j["message"]?.ToString(), j["errors"]?.ToString() ?? resp.ReasonPhrase);
                }
                catch
                {
                    return (false, respStr, resp.ReasonPhrase);
                }
            }

            try
            {
                var j = JObject.Parse(respStr);
                return (true, j["message"]?.ToString(), null);
            }
            catch
            {
                return (true, respStr, null);
            }
        }
    }
}