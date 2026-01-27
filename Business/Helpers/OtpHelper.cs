using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace CodeForge_Desktop.Business.Helpers
{
    /// <summary>
    /// Helper class để quản lý OTP (One-Time Password)
    /// Demo version: lưu OTP tạm trong memory
    /// </summary>
    public static class OtpHelper
    {
        // Dictionary lưu OTP tạm thời: key = email, value = (otp, expirationTime)
        private static Dictionary<string, (string otp, DateTime expirationTime)> _otpStorage =
            new Dictionary<string, (string, DateTime)>();

        private const int OTP_LENGTH = 6;
        private const int OTP_EXPIRATION_MINUTES = 5; // OTP hết hạn sau 5 phút

        // ✅ Demo emails để test
        private static readonly string[] DEMO_EMAILS = new[]
        {
            "admin@codeforge.com",
            "user@codeforge.com",
            "student@codeforge.com",
            "teacher@codeforge.com"
        };

        /// <summary>
        /// Tạo OTP 6 chữ số
        /// </summary>
        public static string GenerateOtp()
        {
            Random random = new Random();
            string otp = "";
            for (int i = 0; i < OTP_LENGTH; i++)
            {
                otp += random.Next(0, 10).ToString();
            }
            return otp;
        }

        /// <summary>
        /// Lưu OTP cho email
        /// </summary>
        public static void SaveOtp(string email, string otp)
        {
            DateTime expirationTime = DateTime.Now.AddMinutes(OTP_EXPIRATION_MINUTES);

            if (_otpStorage.ContainsKey(email))
            {
                _otpStorage[email] = (otp, expirationTime);
            }
            else
            {
                _otpStorage.Add(email, (otp, expirationTime));
            }

            Debug.WriteLine($"✓ OTP Generated for {email}: {otp} (Expires at {expirationTime:HH:mm:ss})");
        }

        /// <summary>
        /// Gửi OTP qua email (DEMO): hiển thị popup form nhỏ, rõ ràng, dễ đọc và hỗ trợ sao chép.
        /// </summary>
        public static bool SendOtpEmail(string email, string otp)
        {
            try
            {
                using (Form popup = new Form())
                {
                    popup.FormBorderStyle = FormBorderStyle.FixedDialog;
                    popup.StartPosition = FormStartPosition.CenterParent;
                    popup.ClientSize = new Size(360, 180);
                    popup.Text = "Mã OTP";
                    popup.MaximizeBox = false;
                    popup.MinimizeBox = false;
                    popup.ShowIcon = false;
                    popup.ShowInTaskbar = false;
                    popup.Font = new Font("Segoe UI", 9F, FontStyle.Regular);

                    // Email info
                    Label lblEmail = new Label
                    {
                        AutoSize = false,
                        Location = new Point(12, 12),
                        Size = new Size(336, 20),
                        Text = $"Đã gửi tới: {email}",
                        ForeColor = Color.FromArgb(60, 60, 60)
                    };

                    // OTP label - large and centered
                    Label lblOtp = new Label
                    {
                        AutoSize = false,
                        Location = new Point(12, 40),
                        Size = new Size(336, 60),
                        Text = otp,
                        TextAlign = ContentAlignment.MiddleCenter,
                        Font = new Font("Segoe UI", 28F, FontStyle.Bold),
                        ForeColor = Color.FromArgb(20, 90, 170)
                    };

                    // Expiration info
                    Label lblExpire = new Label
                    {
                        AutoSize = false,
                        Location = new Point(12, 105),
                        Size = new Size(336, 16),
                        Text = $"⏱ Hết hạn sau: {OTP_EXPIRATION_MINUTES} phút",
                        ForeColor = Color.Gray,
                        TextAlign = ContentAlignment.MiddleCenter,
                        Font = new Font("Segoe UI", 8.5F, FontStyle.Regular)
                    };

                    // Copy button
                    Button btnCopy = new Button
                    {
                        Text = "Sao chép",
                        Location = new Point(56, 128),
                        Size = new Size(100, 30)
                    };

                    // Close button
                    Button btnClose = new Button
                    {
                        Text = "Đóng",
                        Location = new Point(204, 128),
                        Size = new Size(100, 30),
                        DialogResult = DialogResult.OK
                    };

                    // Copy click handler
                    btnCopy.Click += (s, e) =>
                    {
                        try
                        {
                            Clipboard.SetText(otp);
                            // Provide gentle feedback by temporarily changing button text
                            string old = btnCopy.Text;
                            btnCopy.Text = "Đã sao chép";
                            btnCopy.Enabled = false;
                            var t = new Timer { Interval = 1200 };
                            t.Tick += (ts, te) =>
                            {
                                t.Stop();
                                t.Dispose();
                                btnCopy.Text = old;
                                btnCopy.Enabled = true;
                            };
                            t.Start();
                        }
                        catch
                        {
                            MessageBox.Show("Không thể sao chép vào clipboard.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    };

                    popup.Controls.Add(lblEmail);
                    popup.Controls.Add(lblOtp);
                    popup.Controls.Add(lblExpire);
                    popup.Controls.Add(btnCopy);
                    popup.Controls.Add(btnClose);

                    popup.AcceptButton = btnClose;
                    popup.ShowDialog();
                }

                Debug.WriteLine($"OTP for {email}: {otp}");
                return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error showing OTP UI: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Xác thực OTP
        /// </summary>
        public static bool VerifyOtp(string email, string inputOtp)
        {
            if (!_otpStorage.ContainsKey(email))
            {
                return false;
            }

            var (storedOtp, expirationTime) = _otpStorage[email];

            // Kiểm tra OTP hết hạn
            if (DateTime.Now > expirationTime)
            {
                _otpStorage.Remove(email);
                return false;
            }

            // Kiểm tra OTP khớp
            if (storedOtp == inputOtp)
            {
                _otpStorage.Remove(email);
                return true;
            }

            return false;
        }

        /// <summary>
        /// Xóa OTP của email
        /// </summary>
        public static void ClearOtp(string email)
        {
            if (_otpStorage.ContainsKey(email))
            {
                _otpStorage.Remove(email);
            }
        }

        /// <summary>
        /// Lấy danh sách email demo để test
        /// </summary>
        public static string[] GetDemoEmails()
        {
            return DEMO_EMAILS;
        }

        /// <summary>
        /// Kiểm tra email có phải demo không
        /// </summary>
        public static bool IsDemoEmail(string email)
        {
            foreach (var demoEmail in DEMO_EMAILS)
            {
                if (demoEmail.Equals(email, StringComparison.OrdinalIgnoreCase))
                    return true;
            }
            return false;
        }
    }
}