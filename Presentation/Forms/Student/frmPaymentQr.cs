using QRCoder;
using System;
using System.Drawing;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json.Linq;

namespace CodeForge_Desktop.Presentation.Forms.Student
{
    public partial class frmPaymentQr : Form
    {
        private readonly string _orderId;
        private readonly string _qrPayload;
        private readonly string _backendBase;
        private readonly string _bearerToken;
        private readonly bool _isSimulation;
        private readonly CancellationTokenSource _cts = new CancellationTokenSource();
        private readonly HttpClient _http = new HttpClient();

        // Minimal InitializeComponent implementation so designer-less form compiles
        // Keeps behavior identical to BuildUi which performs the actual UI construction.
        private void InitializeComponent()
        {
            // Designer placeholder to satisfy InitializeComponent() call in constructor.
            // Do not add heavy UI logic here — BuildUi() will build runtime UI.
            this.SuspendLayout();
            this.ClientSize = new Size(420, 560);
            this.Name = "frmPaymentQr";
            this.Text = "Quét mã VietQR";
            this.ResumeLayout(false);
        }

        public frmPaymentQr(string orderId, string qrPayload, string backendBase, string bearerToken)
        {
            InitializeComponent();
            _orderId = orderId;
            _qrPayload = qrPayload;
            _backendBase = backendBase?.TrimEnd('/') ?? "";
            _bearerToken = bearerToken;

            // Detect simulation mode early and store flag
            // NOTE: .NET Framework 4.7.2 does not support StringComparison overload on string.Contains,
            // use IndexOf with StringComparison instead.
            _isSimulation = !string.IsNullOrEmpty(_qrPayload) && _qrPayload.IndexOf("sim=true", StringComparison.OrdinalIgnoreCase) >= 0;

            BuildUi();

            // For simulation mode we DO NOT start any network polling or make HTTP calls.
            if (!_isSimulation)
            {
                StartPolling();
            }
        }

        private void BuildUi()
        {
            this.Text = "Quét mã VietQR để thanh toán";
            this.Width = 420;
            this.Height = 560;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.StartPosition = FormStartPosition.CenterParent;

            var pb = new PictureBox { Dock = DockStyle.Top, Height = 360, SizeMode = PictureBoxSizeMode.CenterImage, BackColor = Color.White };
            var lbl = new Label { Text = "Mở ứng dụng ngân hàng, quét mã QR bên dưới và thanh toán.\nĐợi xác nhận tự động...", Dock = DockStyle.Top, Height = 60, TextAlign = ContentAlignment.MiddleCenter };
            var lblOrder = new Label { Text = $"Mã đơn: {_orderId}", Dock = DockStyle.Top, Height = 24, TextAlign = ContentAlignment.MiddleCenter, ForeColor = Color.Gray };
            var pnlButtons = new Panel { Dock = DockStyle.Bottom, Height = 44 };

            var btnCancel = new Button { Text = "Hủy", Dock = DockStyle.Right, Width = 100, DialogResult = DialogResult.Cancel };
            btnCancel.Click += (s, e) => { _cts.Cancel(); this.DialogResult = DialogResult.Cancel; this.Close(); };

            var btnDone = new Button { Text = "Xong", Dock = DockStyle.Right, Width = 100, BackColor = Color.FromArgb(40, 167, 69), ForeColor = Color.White, Cursor = Cursors.Hand };
            btnDone.FlatStyle = FlatStyle.Flat;
            btnDone.FlatAppearance.BorderSize = 0;

            pnlButtons.Controls.Add(btnCancel);
            pnlButtons.Controls.Add(btnDone);

            this.Controls.Add(pnlButtons);
            this.Controls.Add(lbl);
            this.Controls.Add(lblOrder);
            this.Controls.Add(pb);

            // Generate QR image (payload can be any string in simulation)
            using (var gen = new QRCodeGenerator())
            {
                var payloadToEncode = _qrPayload ?? string.Empty;
                var data = gen.CreateQrCode(payloadToEncode, QRCodeGenerator.ECCLevel.Q);
                var qrc = new QRCode(data);
                pb.Image = qrc.GetGraphic(10, Color.Black, Color.White, true);
            }

            // DONE handler: if simulation, only run local simulated check and return OK.
            // If not simulation, perform one immediate status check and let background polling continue.
            btnDone.Click += async (s, e) =>
            {
                try
                {
                    btnDone.Enabled = false;
                    btnCancel.Enabled = false;

                    if (_isSimulation)
                    {
                        // local-only flow: short delay and return success (no HTTP calls)
                        using (var wait = new Form { Width = 300, Height = 90, StartPosition = FormStartPosition.CenterParent, FormBorderStyle = FormBorderStyle.FixedToolWindow })
                        {
                            var l = new Label { Text = "Đang kiểm tra...", Dock = DockStyle.Fill, TextAlign = ContentAlignment.MiddleCenter };
                            wait.Controls.Add(l);
                            wait.Show();
                            await Task.Delay(TimeSpan.FromSeconds(2));
                            wait.Close();
                        }

                        MessageBox.Show("Thanh toán thành công (giả lập).", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        _cts.Cancel();
                        this.DialogResult = DialogResult.OK;
                        this.Close();
                        return;
                    }

                    // Non-simulation: perform immediate status check against backend API (single shot)
                    var url = $"{_backendBase}/api/payments/status/{Uri.EscapeDataString(_orderId)}";
                    var req = new HttpRequestMessage(HttpMethod.Get, url);
                    if (!string.IsNullOrEmpty(_bearerToken))
                        req.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _bearerToken);

                    var r = await _http.SendAsync(req);
                    if (r.IsSuccessStatusCode)
                    {
                        var content = await r.Content.ReadAsStringAsync();
                        try
                        {
                            var j = JObject.Parse(content);
                            var status = j["data"]?["status"]?.ToString() ?? j["status"]?.ToString();

                            if (!string.IsNullOrEmpty(status) && (status.Equals("paid", StringComparison.OrdinalIgnoreCase)
                                || status.Equals("succeeded", StringComparison.OrdinalIgnoreCase)))
                            {
                                MessageBox.Show("Thanh toán đã được xác nhận.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                _cts.Cancel();
                                this.DialogResult = DialogResult.OK;
                                this.Close();
                                return;
                            }
                        }
                        catch
                        {
                            // ignore parse errors and fallthrough to "not paid"
                        }
                    }

                    // If we reach here, status not paid yet
                    MessageBox.Show("Chưa nhận được thanh toán. Ứng dụng sẽ tiếp tục kiểm tra tự động.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btnDone.Enabled = true;
                    btnCancel.Enabled = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi kiểm tra trạng thái: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    btnDone.Enabled = true;
                }
            };
        }

        private async void StartPolling()
        {
            // Defensive: if constructor or other callers set simulation flag, don't poll.
            if (_isSimulation) return;

            try
            {
                while (!_cts.IsCancellationRequested)
                {
                    var url = $"{_backendBase}/api/payments/status/{Uri.EscapeDataString(_orderId)}";
                    var req = new HttpRequestMessage(HttpMethod.Get, url);
                    if (!string.IsNullOrEmpty(_bearerToken))
                        req.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _bearerToken);

                    try
                    {
                        var r = await _http.SendAsync(req, _cts.Token);
                        if (r.IsSuccessStatusCode)
                        {
                            var content = await r.Content.ReadAsStringAsync();
                            // Expect ApiResponse wrapper: { data: { status: "Pending" } }
                            try
                            {
                                var j = JObject.Parse(content);
                                var status = j["data"]?["status"]?.ToString() ?? j["status"]?.ToString();
                                if (!string.IsNullOrEmpty(status) && (status.Equals("paid", StringComparison.OrdinalIgnoreCase)
                                    || status.Equals("succeeded", StringComparison.OrdinalIgnoreCase)))
                                {
                                    MessageBox.Show("Thanh toán thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    this.DialogResult = DialogResult.OK;
                                    this.Close();
                                    return;
                                }
                            }
                            catch { /* ignore parse errors */ }
                        }
                    }
                    catch (HttpRequestException)
                    {
                        // Backend unreachable — swallow and retry later to avoid noisy error dialogs.
                        await Task.Delay(TimeSpan.FromSeconds(3), _cts.Token);
                        continue;
                    }

                    await Task.Delay(TimeSpan.FromSeconds(3), _cts.Token);
                }
            }
            catch (TaskCanceledException) { }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi kiểm tra trạng thái: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.DialogResult = DialogResult.Cancel;
                this.Close();
            }
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            _cts.Cancel();
            base.OnFormClosing(e);
        }
    }
}