using System;
using System.Windows.Forms;
using CodeForge_Desktop.Business.Services;

namespace CodeForge_Desktop.Presentation.Forms.Admin
{
    public partial class ImportWordForm : Form
    {
        private readonly WordImportService _importService;
        private string _selectedFilePath;
        private bool _importCompleted = false;

        // ✅ Constructor mới: nhận file path từ ngoài
        public ImportWordForm(WordImportService importService, string initialFilePath = "")
        {
            InitializeComponent();
            _importService = importService ?? throw new ArgumentNullException(nameof(importService));
            _selectedFilePath = initialFilePath;
            WireEvents();
        }

        private void WireEvents()
        {
            btnSelectFile.Click += BtnSelectFile_Click;
            btnImport.Click += BtnImport_Click;
            btnClearLog.Click += BtnClearLog_Click;
            btnClose.Click += BtnClose_Click;  // ✅ Thêm event cho nút Close
            this.Load += ImportWordForm_Load;
        }

        private void ImportWordForm_Load(object sender, EventArgs e)
        {
            // ✅ Nếu có file path từ ngoài, hiển thị nó
            if (!string.IsNullOrWhiteSpace(_selectedFilePath))
            {
                lblSelectedFile.Text = System.IO.Path.GetFileName(_selectedFilePath);
                btnImport.Enabled = true;
                AppendLog($"✓ File đã chọn: {_selectedFilePath}");
            }
            else
            {
                lblSelectedFile.Text = "Chưa chọn file";
                btnImport.Enabled = false;
                rtbLog.Text = "Sẵn sàng. Chọn file Word để bắt đầu.\r\n";
            }
        }

        private void BtnSelectFile_Click(object sender, EventArgs e)
        {
            using (var ofd = new OpenFileDialog())
            {
                ofd.Filter = "Word Documents (*.docx)|*.docx|All Files (*.*)|*.*";
                ofd.Title = "Chọn file Word chứa bài lập trình";
                
                // ✅ Set initial directory nếu có file path
                if (!string.IsNullOrWhiteSpace(_selectedFilePath))
                {
                    ofd.InitialDirectory = System.IO.Path.GetDirectoryName(_selectedFilePath);
                }

                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    _selectedFilePath = ofd.FileName;
                    lblSelectedFile.Text = System.IO.Path.GetFileName(_selectedFilePath);
                    btnImport.Enabled = true;
                    _importCompleted = false;  // ✅ Reset flag
                    AppendLog($"✓ File đã chọn: {_selectedFilePath}");
                }
            }
        }

        private void BtnImport_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(_selectedFilePath))
            {
                MessageBox.Show("Vui lòng chọn file Word trước.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Lấy LessonID nếu có (optional)
            Guid? lessonId = null;
            if (!string.IsNullOrWhiteSpace(txtLessonId.Text) && Guid.TryParse(txtLessonId.Text, out var lid))
            {
                lessonId = lid;
            }

            // Disable button
            btnImport.Enabled = false;
            btnSelectFile.Enabled = false;
            rtbLog.Clear();
            AppendLog("⏳ Đang import...\r\n");

            try
            {
                // Import
                var result = _importService.ImportFromWordFile(_selectedFilePath, lessonId);

                // Hiển thị kết quả
                DisplayImportResult(result);

                // ✅ Chỉ set flag, KHÔNG set DialogResult
                _importCompleted = (result.SuccessCount > 0);
            }
            catch (Exception ex)
            {
                AppendLog($"\r\n❌ Lỗi: {ex.Message}");
                MessageBox.Show($"Lỗi khi import:\n{ex.Message}\n\nChiết tiết: {ex.InnerException?.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnImport.Enabled = true;
                btnSelectFile.Enabled = true;
            }
        }

        private void DisplayImportResult(ImportResult result)
        {
            AppendLog("╔════════════════════════════════════════╗");
            AppendLog("║        KẾT QUẢ IMPORT                  ║");
            AppendLog("╠════════════════════════════════════════╣");
            AppendLog($"║ ✓ Thành công: {result.SuccessCount,33} ║");
            AppendLog($"║ ✗ Lỗi:        {result.FailureCount,33} ║");
            AppendLog($"║ Tổng:      {(result.SuccessCount + result.FailureCount),32} ║");
            AppendLog("╚════════════════════════════════════════╝");
            AppendLog("");

            // Chi tiết log
            if (result.Log != null && result.Log.Count > 0)
            {
                AppendLog("📋 CHI TIẾT:");
                foreach (var log in result.Log)
                {
                    AppendLog(log);
                }
            }

            // Summary message
            AppendLog("");
            AppendLog($"📌 {result.Message}");
        }

        private void BtnClearLog_Click(object sender, EventArgs e)
        {
            rtbLog.Clear();
            AppendLog("Log đã xóa.");
        }

        private void AppendLog(string message)
        {
            if (rtbLog.InvokeRequired)
            {
                rtbLog.Invoke(new Action<string>(AppendLog), message);
            }
            else
            {
                rtbLog.AppendText(message + Environment.NewLine);
                rtbLog.ScrollToCaret();
            }
        }

        // ✅ Event handler cho nút Close
        private void BtnClose_Click(object sender, EventArgs e)
        {
            if (_importCompleted)
            {
                this.DialogResult = DialogResult.OK;
            }
            this.Close();
        }

        /// <summary>
        /// ✅ Override FormClosing để xác nhận trước khi đóng nếu import đã hoàn tất
        /// </summary>
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (_importCompleted && e.CloseReason == CloseReason.UserClosing)
            {
                // ✅ Set DialogResult khi user đóng form (sau khi xem log)
                this.DialogResult = DialogResult.OK;
            }
            else if (e.CloseReason == CloseReason.UserClosing && !_importCompleted && !string.IsNullOrWhiteSpace(rtbLog.Text))
            {
                // Tùy chọn: hỏi user có chắc chắn muốn đóng không
                if (MessageBox.Show("Bạn có chắc muốn đóng? Import chưa hoàn tất.", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                {
                    e.Cancel = true;
                }
            }

            base.OnFormClosing(e);
        }
    }
}