using System;
using System.Windows.Forms;
using CodeForge_Desktop.Business.Services;
using CodeForge_Desktop.DataAccess.Entities;

namespace CodeForge_Desktop.Presentation.Forms.Admin
{
    public partial class AddEditUserForm : Form
    {
        private readonly UserService _userService;
        private Guid? _userId; // Nếu null -> Mode Add, có giá trị -> Mode Edit

        // Constructor cho chế độ ADD
        public AddEditUserForm(UserService userService)
        {
            InitializeComponent();
            _userService = userService;
            _userId = null;

            // Setup UI cho chế độ Add
            lblTitle.Text = "Thêm User Mới";
            cboRole.SelectedIndex = 0; // Mặc định Student

            // Ẩn Status khi thêm mới (mặc định Active) hoặc để hiển thị tùy ý
            // Ở đây tôi để hiển thị và mặc định Active
            cboStatus.SelectedIndex = 0; // Active
        }

        // Constructor cho chế độ EDIT
        public AddEditUserForm(UserService userService, Guid userId) : this(userService)
        {
            _userId = userId;
            lblTitle.Text = "Cập nhật Thông tin User";

            // Load dữ liệu cũ
            LoadUserData(userId);
        }

        private void LoadUserData(Guid userId)
        {
            var user = _userService.GetUserById(userId);
            if (user != null)
            {
                txtUsername.Text = user.Username;
                txtUsername.Enabled = false; // Không cho sửa Username (tùy nghiệp vụ)
                txtEmail.Text = user.Email;

                // Không load password hash lên ô password để bảo mật
                // Nếu người dùng nhập vào ô này -> Đổi pass. Nếu để trống -> Giữ nguyên.
                //txtPassword.PlaceholderText = "(Để trống nếu không đổi mật khẩu)";

                cboRole.SelectedItem = user.Role; // Cần đảm bảo item trong combobox khớp với DB (Student, Admin)

                // Map Status (giả sử trong DB lưu "Active"/"Inactive")
                cboStatus.SelectedItem = user.Status;
            }
            else
            {
                MessageBox.Show("Không tìm thấy user!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (ValidateInput())
            {
                bool success = false;

                if (_userId == null) // ADD MODE
                {
                    var newUser = new User
                    {
                        Username = txtUsername.Text.Trim(),
                        Email = txtEmail.Text.Trim(),
                        PasswordHash = txtPassword.Text, // TODO: Cần Hash password trước khi lưu (thêm logic hash ở Service hoặc Helper)
                        Role = cboRole.SelectedItem.ToString(),
                        Status = cboStatus.SelectedItem.ToString(),
                        JoinDate = DateTime.Now
                    };

                    // Lưu ý: Hàm CreateUser trong Service của bạn cần xử lý Hash Password
                    // Hiện tại truyền plain text, bạn nhớ cập nhật Service để Hash nhé.
                    success = _userService.CreateUser(newUser);
                }
                else // EDIT MODE
                {
                    var userToUpdate = _userService.GetUserById(_userId.Value);
                    if (userToUpdate != null)
                    {
                        userToUpdate.Email = txtEmail.Text.Trim();
                        userToUpdate.Role = cboRole.SelectedItem.ToString();
                        userToUpdate.Status = cboStatus.SelectedItem.ToString();

                        // Chỉ cập nhật password nếu người dùng có nhập mới
                        if (!string.IsNullOrWhiteSpace(txtPassword.Text))
                        {
                            userToUpdate.PasswordHash = txtPassword.Text; // Nhớ Hash!
                        }

                        success = _userService.UpdateUser(userToUpdate);
                    }
                }

                if (success)
                {
                    MessageBox.Show("Lưu thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.DialogResult = DialogResult.OK; // Trả về OK để form cha biết mà reload
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Lưu thất bại. Có thể Username hoặc Email đã tồn tại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private bool ValidateInput()
        {
            if (string.IsNullOrWhiteSpace(txtUsername.Text))
            {
                MessageBox.Show("Vui lòng nhập Username.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtEmail.Text))
            {
                MessageBox.Show("Vui lòng nhập Email.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            // Nếu Add mode thì bắt buộc nhập pass
            if (_userId == null && string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                MessageBox.Show("Vui lòng nhập Mật khẩu.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}