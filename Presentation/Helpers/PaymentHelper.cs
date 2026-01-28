using CodeForge_Desktop.Business.DTOs;
using CodeForge_Desktop.Business.Helpers;
using CodeForge_Desktop.Presentation.Forms.Student;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Specialized;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CodeForge_Desktop.Presentation.Helpers
{
    public static class PaymentHelper
    {
        private static readonly HttpClient _http = new HttpClient();
        private static readonly string BackendBase = GetBackendBase();

        public static async Task<bool> StartVietQrPaymentAsync(Guid courseId, decimal amount)
        {
            var user = GlobalStore.user;
            if (user == null)
            {
                MessageBox.Show("Vui lòng đăng nhập.", "Yêu cầu đăng nhập", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }

            // When backend no longer requires Authorization, include UserId in payload
            var reqObj = new { UserId = user.UserID, CourseId = courseId, Amount = amount };
            var reqJson = Newtonsoft.Json.JsonConvert.SerializeObject(reqObj);
            var resp = await _http.PostAsync($"{BackendBase}/api/enrollments/enroll",
                new StringContent(reqJson, Encoding.UTF8, "application/json"));

            if (!resp.IsSuccessStatusCode)
            {
                var txt = await resp.Content.ReadAsStringAsync();
                MessageBox.Show($"Không thể khởi tạo thanh toán: {resp.StatusCode}\n{txt}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            var content = await resp.Content.ReadAsStringAsync();
            var j = JObject.Parse(content);

            // Try to find payment info (ApiResponse wrapper: data.paymentInfo or data)
            var paymentInfo = j["data"]?["paymentInfo"] ?? j["data"];
            if (paymentInfo == null)
            {
                // free enrollment or unexpected shape
                MessageBox.Show("Đăng ký thành công (miễn phí) hoặc backend trả dữ liệu bất thường.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return true;
            }

            string orderId = paymentInfo["orderId"]?.ToString();
            string qrPayload = paymentInfo["qrPayload"]?.ToString() ?? paymentInfo["paymentUrl"]?.ToString();

            if (string.IsNullOrEmpty(orderId) || string.IsNullOrEmpty(qrPayload))
            {
                MessageBox.Show("Backend trả thông tin thanh toán không hợp lệ.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            // pass null for bearerToken since Authorization disabled
            using (var f = new frmPaymentQr(orderId, qrPayload, BackendBase, null))
            {
                var dr = f.ShowDialog();
                return dr == DialogResult.OK;
            }
        }

        // New: Local simulation helper (visual-only VietQR simulation)
        // Use this for testing/demo when you don't have a backend or want to simulate a realistic QR flow.
        public static async Task<bool> StartLocalVietQrSimulationAsync(Guid courseId, decimal amount)
        {
            // lightweight async wrapper so caller can await same signature as real start
            return await Task.Run(() =>
            {
                var user = GlobalStore.user;
                if (user == null)
                {
                    MessageBox.Show("Vui lòng đăng nhập.", "Yêu cầu đăng nhập", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return false;
                }

                // Create a simulated order id and a realistic-looking payload (for UI only)
                string orderId = $"SIM-{Guid.NewGuid():N}";
                string payer = string.IsNullOrWhiteSpace(user?.Username) ? "Khach" : user.Username;
                string payload = BuildFakeVietQrPayload(orderId, amount, payer);

                using (var f = new frmPaymentQr(orderId, payload, BackendBase, null))
                {
                    var dr = f.ShowDialog();
                    return dr == DialogResult.OK;
                }
            });
        }

        // Build a realistic-looking VietQR payload string for UI/testing.
        // NOTE: This payload is for simulation / demonstration only and is NOT a valid signed VietQR for actual bank apps.
        private static string BuildFakeVietQrPayload(string orderId, decimal amount, string payerName)
        {
            // A simple but plausible deep-link / URL payload that looks like a VietQR transfer request.
            // Real VietQR uses EMVCo tags + CRC and merchant signature; computing that requires bank keys.
            // For UI simulation we produce a URL with order, amount and descriptive fields so the generated QR looks "real".
            var sb = new StringBuilder();
            sb.Append("https://mock-vietqr.example/pay?");
            sb.Append("order=").Append(Uri.EscapeDataString(orderId));
            sb.Append("&amount=").Append(Uri.EscapeDataString(amount.ToString("F0"))); // integer VND-like
            sb.Append("&currency=VND");
            sb.Append("&merchant=").Append(Uri.EscapeDataString("CodeForge"));
            sb.Append("&note=").Append(Uri.EscapeDataString($"Học phí khoá học - {payerName}"));
            // Add a visual tag so testers can recognise simulation mode
            sb.Append("&sim=true");

            return sb.ToString();
        }

        // Read backend base URL:
        // 1) Environment variable PAYMENT_BACKEND_BASE (preferred for deploy)
        // 2) AppSettings "PaymentBackendBase" via reflection (works even if System.Configuration not referenced)
        // 3) fallback default
        private static string GetBackendBase()
        {
            // 1) env var
            var env = Environment.GetEnvironmentVariable("PAYMENT_BACKEND_BASE");
            if (!string.IsNullOrWhiteSpace(env)) return env.TrimEnd('/');

            // 2) try reading System.Configuration.ConfigurationManager.AppSettings via reflection
            try
            {
                var cmType = Type.GetType("System.Configuration.ConfigurationManager, System.Configuration");
                if (cmType != null)
                {
                    var appSettingsProp = cmType.GetProperty("AppSettings", BindingFlags.Static | BindingFlags.Public);
                    if (appSettingsProp != null)
                    {
                        var appSettings = appSettingsProp.GetValue(null) as NameValueCollection;
                        var val = appSettings?["PaymentBackendBase"];
                        if (!string.IsNullOrWhiteSpace(val)) return val.TrimEnd('/');
                    }
                }
            }
            catch
            {
                // ignore reflection/read errors
            }

            // 3) fallback
            return "https://your-backend.example.com";
        }
    }
}