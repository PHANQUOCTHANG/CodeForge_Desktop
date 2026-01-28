using System;
using System.Drawing;
using System.Windows.Forms;
using CodeForge_Desktop.Business.Helpers;
using CodeForge_Desktop.Business.Services;
using CodeForge_Desktop.DataAccess.Entities;

namespace CodeForge_Desktop.Presentation.Forms.Admin
{
    public partial class AddEditUserForm : Form
    {
        private readonly UserService _userService;
        private Guid? _userId; // Nếu null -> Chế độ Thêm, có giá trị -> Chế độ Sửa
        private bool _isPasswordVisible = false;

        // Ánh xạ giữa text hiển thị và giá trị trong database
        private readonly string[] _roleDisplayTexts = { "Sinh viên", "Quản trị viên" };
        private readonly string[] _roleValues = { "student", "admin" };
        private readonly string[] _statusDisplayTexts = { "Đang hoạt động", "Tạm khóa" };
        private readonly string[] _statusValues = { "Active", "Inactive" };

        #region Constructors

        // Constructor cho chế độ THÊM MỚI
        public AddEditUserForm(UserService userService)
        {
            InitializeComponent();
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
            _userId = null;
        }

        // Constructor cho chế độ CHỈNH SỬA
        public AddEditUserForm(UserService userService, Guid userId) : this(userService)
        {
            if (userId == Guid.Empty)
                throw new ArgumentException("ID người dùng không hợp lệ", nameof(userId));

            _userId = userId;
        }

        #endregion

        #region Form Events

        private void AddEditUserForm_Load(object sender, EventArgs e)
        {
            try
            {
                if (_userId == null)
                {
                    InitializeFormForAdd();
                }
                else
                {
                    InitializeFormForEdit();
                    LoadUserData(_userId.Value);
                }

                SetupEventHandlers();
                ApplyModernStyling();
            }
            catch (Exception ex)
            {
                ShowNotification($"Lỗi khởi tạo form: {ex.Message}", MessageBoxIcon.Error);
                this.Close();
            }
        }

        #endregion

        #region Initialization Methods

        private void InitializeFormForAdd()
        {
            lblTitle.Text = "Thêm người dùng mới";
            lblSubtitle.Text = "Vui lòng điền đầy đủ thông tin bên dưới";
            lblHeaderIcon.Text = "➕";
            lblPasswordHint.Visible = false;

            // Giá trị mặc định
            cboRole.SelectedIndex = 0; // Sinh viên
            cboStatus.SelectedIndex = 0; // Đang hoạt động

            // Focus vào trường đầu tiên
            txtUsername.Focus();
        }

        private void InitializeFormForEdit()
        {
            lblTitle.Text = "Cập nhật thông tin người dùng";
            lblSubtitle.Text = "Chỉnh sửa thông tin người dùng";
            lblHeaderIcon.Text = "✏️";
            lblPasswordHint.Visible = true;

            // Focus vào email khi chỉnh sửa (vì username bị khóa)
            txtEmail.Focus();
        }

        private void SetupEventHandlers()
        {
            // Xác thực khi nhập liệu
            txtUsername.TextChanged += (s, e) => ValidateUsernameField();
            txtEmail.TextChanged += (s, e) => ValidateEmailField();
            txtPassword.TextChanged += (s, e) => ValidatePasswordField();

            // Phím tắt
            this.KeyPreview = true;
            this.KeyDown += Form_KeyDown;

            // Hiệu ứng hover cho nút
            SetupButtonHoverEffects();
        }

        private void Form_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.S) // Ctrl+S để lưu
            {
                e.Handled = true;
                e.SuppressKeyPress = true;
                btnSave.PerformClick();
            }
            else if (e.KeyCode == Keys.Escape) // ESC để hủy
            {
                e.Handled = true;
                e.SuppressKeyPress = true;
                btnCancel.PerformClick();
            }
        }

        private void SetupButtonHoverEffects()
        {
            // Nút Lưu
            btnSave.MouseEnter += (s, e) => btnSave.BackColor = Color.FromArgb(20, 115, 70);
            btnSave.MouseLeave += (s, e) => btnSave.BackColor = Color.FromArgb(25, 135, 84);

            // Nút Hủy
            btnCancel.MouseEnter += (s, e) => btnCancel.BackColor = Color.FromArgb(245, 245, 245);
            btnCancel.MouseLeave += (s, e) => btnCancel.BackColor = Color.White;

            // Nút hiện/ẩn mật khẩu
            btnTogglePassword.MouseEnter += (s, e) => btnTogglePassword.BackColor = Color.FromArgb(240, 240, 240);
            btnTogglePassword.MouseLeave += (s, e) => btnTogglePassword.BackColor = Color.Transparent;
        }

        private void ApplyModernStyling()
        {
            // Bo góc cho panel icon header
            pnlHeaderIcon.Paint += PnlHeaderIcon_Paint;

            // Hiệu ứng hover cho các trường nhập liệu
            ApplyTextBoxHoverEffects();
        }

        private void PnlHeaderIcon_Paint(object sender, PaintEventArgs e)
        {
            var rect = pnlHeaderIcon.ClientRectangle;
            using (var path = GetRoundedRectPath(rect, 12))
            using (var brush = new SolidBrush(pnlHeaderIcon.BackColor))
            {
                e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                e.Graphics.FillPath(brush, path);
            }
        }

        private void ApplyTextBoxHoverEffects()
        {
            foreach (Control panel in pnlFormFields.Controls)
            {
                if (panel is Panel fieldPanel)
                {
                    foreach (Control control in fieldPanel.Controls)
                    {
                        if (control is TextBox textBox)
                        {
                            textBox.MouseEnter += (s, e) =>
                            {
                                if (textBox.Enabled)
                                    textBox.BackColor = Color.FromArgb(250, 252, 255);
                            };
                            textBox.MouseLeave += (s, e) =>
                            {
                                if (textBox.Enabled)
                                    textBox.BackColor = Color.White;
                                else
                                    textBox.BackColor = Color.FromArgb(245, 245, 245);
                            };
                        }
                        else if (control is ComboBox comboBox)
                        {
                            comboBox.MouseEnter += (s, e) => comboBox.BackColor = Color.FromArgb(250, 252, 255);
                            comboBox.MouseLeave += (s, e) => comboBox.BackColor = Color.White;
                        }
                    }
                }
            }
        }

        private System.Drawing.Drawing2D.GraphicsPath GetRoundedRectPath(Rectangle rect, int radius)
        {
            var path = new System.Drawing.Drawing2D.GraphicsPath();
            int diameter = radius * 2;

            path.AddArc(rect.X, rect.Y, diameter, diameter, 180, 90);
            path.AddArc(rect.Right - diameter, rect.Y, diameter, diameter, 270, 90);
            path.AddArc(rect.Right - diameter, rect.Bottom - diameter, diameter, diameter, 0, 90);
            path.AddArc(rect.X, rect.Bottom - diameter, diameter, diameter, 90, 90);
            path.CloseFigure();

            return path;
        }

        #endregion

        #region Data Loading

        private void LoadUserData(Guid userId)
        {
            try
            {
                var user = _userService.GetUserById(userId);
                if (user == null)
                {
                    ShowNotification("Không tìm thấy thông tin người dùng!", MessageBoxIcon.Error);
                    this.DialogResult = DialogResult.Cancel;
                    this.Close();
                    return;
                }

                // Điền dữ liệu vào form
                txtUsername.Text = user.Username;
                txtUsername.Enabled = false; // Không cho phép sửa tên đăng nhập
                txtUsername.BackColor = Color.FromArgb(245, 245, 245);

                txtEmail.Text = user.Email;

                // Ánh xạ vai trò
                int roleIndex = Array.IndexOf(_roleValues, user.Role?.ToLower() ?? "student");
                cboRole.SelectedIndex = roleIndex >= 0 ? roleIndex : 0;

                // Ánh xạ trạng thái
                int statusIndex = Array.IndexOf(_statusValues, user.Status ?? "Active");
                cboStatus.SelectedIndex = statusIndex >= 0 ? statusIndex : 0;
            }
            catch (Exception ex)
            {
                ShowNotification($"Lỗi khi tải dữ liệu: {ex.Message}", MessageBoxIcon.Error);
                this.DialogResult = DialogResult.Cancel;
                this.Close();
            }
        }

        #endregion

        #region Button Click Events

        private void btnTogglePassword_Click(object sender, EventArgs e)
        {
            _isPasswordVisible = !_isPasswordVisible;

            if (_isPasswordVisible)
            {
                txtPassword.PasswordChar = '\0';
                btnTogglePassword.Text = "🙈";
            }
            else
            {
                txtPassword.PasswordChar = '●';
                btnTogglePassword.Text = "👁";
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!ValidateAllFields())
            {
                return;
            }

            try
            {
                // Vô hiệu hóa nút để tránh nhấn nhiều lần
                btnSave.Enabled = false;
                btnCancel.Enabled = false;
                this.Cursor = Cursors.WaitCursor;

                bool success = false;
                string roleValue = _roleValues[cboRole.SelectedIndex];
                string statusValue = _statusValues[cboStatus.SelectedIndex];

                if (_userId == null) // Chế độ THÊM MỚI
                {
                    success = CreateNewUser(roleValue, statusValue);
                }
                else // Chế độ CHỈNH SỬA
                {
                    success = UpdateExistingUser(roleValue, statusValue);
                }

                if (success)
                {
                    ShowSuccessAnimation();
                    ShowNotification("Lưu thông tin thành công!", MessageBoxIcon.Information);
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    ShowNotification("Lưu thất bại. Tên đăng nhập hoặc email có thể đã tồn tại.", MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                ShowNotification($"Đã xảy ra lỗi: {ex.Message}", MessageBoxIcon.Error);
            }
            finally
            {
                // Kích hoạt lại các nút
                btnSave.Enabled = true;
                btnCancel.Enabled = true;
                this.Cursor = Cursors.Default;
            }
        }

        private bool CreateNewUser(string roleValue, string statusValue)
        {
            var newUser = new User
            {
                Username = txtUsername.Text.Trim(),
                Email = txtEmail.Text.Trim(),
                PasswordHash = PasswordHasher.Hash(txtPassword.Text.Trim()),
                Role = roleValue,
                Status = statusValue,
                JoinDate = DateTime.Now
            };

            return _userService.CreateUser(newUser);
        }

        private bool UpdateExistingUser(string roleValue, string statusValue)
        {
            var userToUpdate = _userService.GetUserById(_userId.Value);
            if (userToUpdate == null)
            {
                ShowNotification("Không tìm thấy người dùng cần cập nhật!", MessageBoxIcon.Error);
                return false;
            }

            userToUpdate.Email = txtEmail.Text.Trim();
            userToUpdate.Role = roleValue;
            userToUpdate.Status = statusValue;

            // Chỉ cập nhật mật khẩu nếu người dùng nhập mật khẩu mới
            if (!string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                userToUpdate.PasswordHash = PasswordHasher.Hash(txtPassword.Text.Trim());
            }

            return _userService.UpdateUser(userToUpdate);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (HasUnsavedChanges())
            {
                var result = MessageBox.Show(
                    "Bạn có thay đổi chưa được lưu. Bạn có chắc muốn thoát không?",
                    "Xác nhận thoát",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question
                );

                if (result == DialogResult.No)
                {
                    return;
                }
            }

            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        #endregion

        #region Validation Methods

        private bool ValidateAllFields()
        {
            ResetFieldColors();

            // Xác thực Tên đăng nhập
            if (string.IsNullOrWhiteSpace(txtUsername.Text))
            {
                HighlightInvalidField(txtUsername, lblUsername);
                ShowNotification("Vui lòng nhập tên đăng nhập!", MessageBoxIcon.Warning);
                txtUsername.Focus();
                return false;
            }

            if (txtUsername.Text.Trim().Length < 3)
            {
                HighlightInvalidField(txtUsername, lblUsername);
                ShowNotification("Tên đăng nhập phải có ít nhất 3 ký tự!", MessageBoxIcon.Warning);
                txtUsername.Focus();
                return false;
            }

            // Xác thực Email
            if (string.IsNullOrWhiteSpace(txtEmail.Text))
            {
                HighlightInvalidField(txtEmail, lblEmail);
                ShowNotification("Vui lòng nhập địa chỉ email!", MessageBoxIcon.Warning);
                txtEmail.Focus();
                return false;
            }

            if (!IsValidEmail(txtEmail.Text.Trim()))
            {
                HighlightInvalidField(txtEmail, lblEmail);
                ShowNotification("Địa chỉ email không đúng định dạng!", MessageBoxIcon.Warning);
                txtEmail.Focus();
                return false;
            }

            // Xác thực Mật khẩu (chỉ bắt buộc khi thêm mới)
            if (_userId == null && string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                HighlightInvalidField(txtPassword, lblPassword);
                ShowNotification("Vui lòng nhập mật khẩu!", MessageBoxIcon.Warning);
                txtPassword.Focus();
                return false;
            }

            if (!string.IsNullOrWhiteSpace(txtPassword.Text) && txtPassword.Text.Length < 6)
            {
                HighlightInvalidField(txtPassword, lblPassword);
                ShowNotification("Mật khẩu phải có ít nhất 6 ký tự!", MessageBoxIcon.Warning);
                txtPassword.Focus();
                return false;
            }

            // Kiểm tra trùng lặp username/email
            return ValidateUniqueFields();
        }

        private bool ValidateUniqueFields()
        {
            if (_userId == null) // Chế độ THÊM MỚI
            {
                if (_userService.IsUsernameOrEmailExist(txtUsername.Text.Trim(), txtEmail.Text.Trim()))
                {
                    ShowNotification("Tên đăng nhập hoặc email đã tồn tại trong hệ thống!", MessageBoxIcon.Warning);
                    return false;
                }
            }
            else // Chế độ CHỈNH SỬA
            {
                var existingUser = _userService.GetUserById(_userId.Value);
                if (existingUser != null)
                {
                    string newEmail = txtEmail.Text.Trim();

                    // Kiểm tra email mới nếu có thay đổi
                    if (!newEmail.Equals(existingUser.Email, StringComparison.OrdinalIgnoreCase))
                    {
                        var emailCheck = _userService.GetByEmail(newEmail);
                        if (emailCheck != null)
                        {
                            HighlightInvalidField(txtEmail, lblEmail);
                            ShowNotification("Email này đã được sử dụng bởi người dùng khác!", MessageBoxIcon.Warning);
                            return false;
                        }
                    }
                }
            }

            return true;
        }

        private void ValidateUsernameField()
        {
            if (string.IsNullOrWhiteSpace(txtUsername.Text))
            {
                lblUsername.ForeColor = Color.FromArgb(244, 67, 54); // Đỏ
            }
            else if (txtUsername.Text.Trim().Length < 3)
            {
                lblUsername.ForeColor = Color.FromArgb(255, 152, 0); // Cam
            }
            else
            {
                lblUsername.ForeColor = Color.FromArgb(76, 175, 80); // Xanh lá
            }
        }

        private void ValidateEmailField()
        {
            if (string.IsNullOrWhiteSpace(txtEmail.Text))
            {
                lblEmail.ForeColor = Color.FromArgb(244, 67, 54); // Đỏ
            }
            else if (!IsValidEmail(txtEmail.Text.Trim()))
            {
                lblEmail.ForeColor = Color.FromArgb(255, 152, 0); // Cam
            }
            else
            {
                lblEmail.ForeColor = Color.FromArgb(76, 175, 80); // Xanh lá
            }
        }

        private void ValidatePasswordField()
        {
            if (_userId == null) // Chỉ validate khi thêm mới
            {
                if (string.IsNullOrWhiteSpace(txtPassword.Text))
                {
                    lblPassword.ForeColor = Color.FromArgb(244, 67, 54); // Đỏ
                }
                else if (txtPassword.Text.Length < 6)
                {
                    lblPassword.ForeColor = Color.FromArgb(255, 152, 0); // Cam
                }
                else
                {
                    lblPassword.ForeColor = Color.FromArgb(76, 175, 80); // Xanh lá
                }
            }
        }

        private bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email && email.Contains("@") && email.Contains(".");
            }
            catch
            {
                return false;
            }
        }

        #endregion

        #region UI Helper Methods

        private void HighlightInvalidField(TextBox textBox, Label label)
        {
            textBox.BackColor = Color.FromArgb(255, 235, 238);
            label.ForeColor = Color.FromArgb(244, 67, 54);

            // Tự động reset sau 2 giây
            var timer = new Timer { Interval = 2000 };
            timer.Tick += (s, e) =>
            {
                if (textBox.Enabled)
                    textBox.BackColor = Color.White;
                else
                    textBox.BackColor = Color.FromArgb(245, 245, 245);

                label.ForeColor = Color.FromArgb(60, 60, 60);
                timer.Stop();
                timer.Dispose();
            };
            timer.Start();
        }

        private void ResetFieldColors()
        {
            // Reset màu nền
            txtUsername.BackColor = _userId == null ? Color.White : Color.FromArgb(245, 245, 245);
            txtEmail.BackColor = Color.White;
            txtPassword.BackColor = Color.White;

            // Reset màu chữ label
            lblUsername.ForeColor = Color.FromArgb(60, 60, 60);
            lblEmail.ForeColor = Color.FromArgb(60, 60, 60);
            lblPassword.ForeColor = Color.FromArgb(60, 60, 60);
        }

        private void ShowNotification(string message, MessageBoxIcon icon)
        {
            string title = icon switch
            {
                MessageBoxIcon.Information => "Thông báo",
                MessageBoxIcon.Warning => "Cảnh báo",
                MessageBoxIcon.Error => "Lỗi",
                MessageBoxIcon.Question => "Xác nhận",
                _ => "Thông báo"
            };

            MessageBox.Show(message, title, MessageBoxButtons.OK, icon);
        }

        private void ShowSuccessAnimation()
        {
            var originalColor = pnlHeader.BackColor;
            pnlHeader.BackColor = Color.FromArgb(225, 245, 254);

            var timer = new Timer { Interval = 300 };
            timer.Tick += (s, e) =>
            {
                pnlHeader.BackColor = originalColor;
                timer.Stop();
                timer.Dispose();
            };
            timer.Start();
        }

        private bool HasUnsavedChanges()
        {
            if (_userId == null) // Chế độ thêm mới
            {
                return !string.IsNullOrWhiteSpace(txtUsername.Text) ||
                       !string.IsNullOrWhiteSpace(txtEmail.Text) ||
                       !string.IsNullOrWhiteSpace(txtPassword.Text);
            }
            else // Chế độ chỉnh sửa
            {
                var originalUser = _userService.GetUserById(_userId.Value);
                if (originalUser != null)
                {
                    string currentRole = _roleValues[cboRole.SelectedIndex];
                    string currentStatus = _statusValues[cboStatus.SelectedIndex];

                    return txtEmail.Text.Trim() != originalUser.Email ||
                           currentRole != originalUser.Role?.ToLower() ||
                           currentStatus != originalUser.Status ||
                           !string.IsNullOrWhiteSpace(txtPassword.Text);
                }
            }

            return false;
        }

        #endregion
    }
}