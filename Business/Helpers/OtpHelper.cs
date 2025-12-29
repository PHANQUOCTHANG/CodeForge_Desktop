using System;
using System.Collections.Generic;
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

            System.Diagnostics.Debug.WriteLine($"✓ OTP Generated for {email}: {otp} (Expires at {expirationTime:HH:mm:ss})");
        }

        /// <summary>
        /// Gửi OTP qua email (DEMO: hiển thị MessageBox + Console)
        /// </summary>
        public static bool SendOtpEmail(string email, string otp)
        {
            try
            {
                // ✅ DEMO: Hiển thị OTP trong MessageBox
                string demoMessage = $@"
╔════════════════════════════════╗
║   📧 MÃ OTP ĐÃ ĐƯỢC GỬI        ║
╠════════════════════════════════╣
║                                ║
║  Email: {email,-23} ║
║                                ║
║  Mã OTP (6 chữ số):            ║
║                                ║
║  ┌────────────────────────┐    ║
║  │     {otp}          │    ║
║  └────────────────────────┘    ║
║                                ║
║  ⏱️ Hết hạn sau: 5 phút        ║
║                                ║
╚════════════════════════════════╝
";

                MessageBox.Show(demoMessage, "✉️ OTP Demo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // ✅ In ra console để debug
                Console.WriteLine(demoMessage);
                Console.WriteLine("💡 Đây là chế độ DEMO - OTP được hiển thị bên trên");
                Console.WriteLine("===============================================\n");

                return true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error sending OTP: {ex.Message}");
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