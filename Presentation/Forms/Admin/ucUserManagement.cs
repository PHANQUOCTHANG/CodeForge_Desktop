using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
using CodeForge_Desktop.Business.Services;
using CodeForge_Desktop.DataAccess.Entities;
using CodeForge_Desktop.DataAccess.Repositories; // Cần để khởi tạo Repository

namespace CodeForge_Desktop.Presentation.Forms.Admin
{
    public partial class ucUserManagement : UserControl
    {
        // Service để xử lý logic
        private readonly UserService _userService;

        // Định nghĩa kích thước nút để dùng chung cho Vẽ và Click
        private const int ButtonWidth = 30;
        private const int ButtonHeight = 30;
        private const int ButtonSpacing = 10;
        private const int ButtonMarginX = 5;

        public ucUserManagement()
        {
            InitializeComponent();

            // --- KHỞI TẠO SERVICE ---
            // Trong thực tế nên dùng Dependency Injection, nhưng ở đây ta new trực tiếp
            // UserService cần IUserRepository -> ta truyền UserRepository vào
            var userRepo = new UserRepository();
            _userService = new UserService(userRepo);

            SetupDataGridView();
            SetupSearchBox();

            // Gắn các sự kiện
            dgvUsers.CellPainting += DgvUsers_CellPainting;
            dgvUsers.CellMouseClick += DgvUsers_CellMouseClick;

            btnAdd.Click += BtnAdd_Click;
            btnEdit.Click += BtnEdit_Click;
            btnDelete.Click += BtnDelete_Click;
            txtSearch.TextChanged += TxtSearch_TextChanged; // Tìm kiếm realtime

            // Load dữ liệu ban đầu
            LoadData();
        }

        // --- HÀM LOAD DỮ LIỆU TỪ DB ---
        private void LoadData(string keyword = "")
        {
            dgvUsers.Rows.Clear();
            List<User> users;

            // Nếu không có từ khóa tìm kiếm -> Lấy tất cả
            if (string.IsNullOrWhiteSpace(keyword) || keyword == "Tìm kiếm user...")
            {
                users = _userService.GetAllUsers();
            }
            else
            {
                // Nếu có từ khóa -> Gọi hàm tìm kiếm (Cần bổ sung hàm này vào UserService sau)
                // Tạm thời lọc thủ công ở đây nếu UserService chưa có Search
                var allUsers = _userService.GetAllUsers();
                users = allUsers.FindAll(u =>
                    u.Username.ToLower().Contains(keyword.ToLower()) ||
                    u.Email.ToLower().Contains(keyword.ToLower())
                );
            }

            foreach (var user in users)
            {
                // Map từ User Entity sang DataGridView Row
                // Lưu ý thứ tự cột phải khớp với Designer: Check, ID, Username, Email, Role, Status, Created, Actions
                dgvUsers.Rows.Add(
                    false, // Checkbox
                    user.UserID, // ID (Guid)
                    user.Username,
                    user.Email,
                    user.Role, // Student/Admin
                    user.Status, // Active/Inactive
                    user.JoinDate.ToString("yyyy-MM-dd"),
                    "" // Cột Actions (để trống để tự vẽ)
                );
            }

            UpdateSummary(users.Count);
            dgvUsers.ClearSelection();
        }

        private void UpdateSummary(int totalCount)
        {
            // Cập nhật label tổng số (có thể cải tiến để đếm Active/Inactive sau)
            lblSummary.Text = $"Tổng số: {totalCount} users";
        }

        // --- XỬ LÝ SỰ KIỆN BUTTONS ---

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            var form = new AddEditUserForm(_userService);
            if (form.ShowDialog() == DialogResult.OK)
            {
                LoadData(); // Reload lại danh sách sau khi thêm thành công
            }
        }

        private void BtnEdit_Click(object sender, EventArgs e)
        {
            // Logic sửa từ nút Toolbar (cần chọn row trước)
            if (dgvUsers.SelectedRows.Count > 0)
            {
                // Lấy ID thật (Guid)
                var userId = (Guid)dgvUsers.SelectedRows[0].Cells["colId"].Value;

                // Mở form Edit (truyền Service và ID)
                var form = new AddEditUserForm(_userService, userId);
                if (form.ShowDialog() == DialogResult.OK)
                {
                    LoadData(); // Reload lại danh sách sau khi sửa
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn user cần sửa!", "Thông báo");
            }
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            // 1. Tạo danh sách chứa các ID cần xóa
            List<Guid> idsToDelete = new List<Guid>();

            // 2. Duyệt qua từng dòng trong Grid
            foreach (DataGridViewRow row in dgvUsers.Rows)
            {
                // Bỏ qua dòng mới (nếu có)
                if (row.IsNewRow) continue;

                // Lấy giá trị của ô Checkbox (Cột đầu tiên - colCheck)
                // Lưu ý: Cần ép kiểu an toàn vì nó có thể null
                bool isChecked = Convert.ToBoolean(row.Cells["colCheck"].Value ?? false);

                if (isChecked)
                {
                    // Lấy ID của user
                    var idVal = row.Cells["colId"].Value;
                    if (idVal != null && Guid.TryParse(idVal.ToString(), out Guid userId))
                    {
                        idsToDelete.Add(userId);
                    }
                }
            }

            // 3. Kiểm tra xem có chọn ai không
            if (idsToDelete.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn ít nhất một user để xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // 4. Hỏi xác nhận
            if (MessageBox.Show($"Bạn có chắc chắn muốn xóa {idsToDelete.Count} user đã chọn không?", "Xác nhận xóa nhiều", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                // 5. Gọi Service để xóa
                bool result = _userService.SoftDeleteListUsers(idsToDelete);

                if (result)
                {
                    MessageBox.Show("Đã xóa thành công các user đã chọn.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadData(); // Tải lại danh sách
                }
                else
                {
                    // Trường hợp xóa có lỗi (ví dụ xóa 5 được 4)
                    MessageBox.Show("Đã thực hiện lệnh xóa, nhưng có thể một số user chưa được xóa thành công.", "Lưu ý", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    LoadData();
                }
            }
        }

        private void TxtSearch_TextChanged(object sender, EventArgs e)
        {
            // Gọi tìm kiếm mỗi khi gõ phím
            LoadData(txtSearch.Text);
        }

        // --- CÁC HÀM GIAO DIỆN (GIỮ NGUYÊN) ---

        private void SetupSearchBox()
        {
            txtSearch.GotFocus += (s, e) => { if (txtSearch.Text == "Tìm kiếm user...") { txtSearch.Text = ""; txtSearch.ForeColor = Color.Black; } };
            txtSearch.LostFocus += (s, e) => { if (string.IsNullOrWhiteSpace(txtSearch.Text)) { txtSearch.Text = "Tìm kiếm user..."; txtSearch.ForeColor = Color.Gray; } };
        }

        private void SetupDataGridView()
        {
            dgvUsers.DefaultCellStyle.SelectionBackColor = Color.FromArgb(240, 245, 255);
            dgvUsers.DefaultCellStyle.SelectionForeColor = Color.Black;
            dgvUsers.DefaultCellStyle.Font = new Font("Segoe UI", 9.5F);

            // QUAN TRỌNG: Xóa hết cột cũ để tránh xung đột với Designer
            dgvUsers.Columns.Clear();

            // 1. Cột Checkbox
            var colCheck = new DataGridViewCheckBoxColumn();
            colCheck.Name = "colCheck";
            colCheck.HeaderText = "";
            colCheck.Width = 30;
            colCheck.FalseValue = false;
            colCheck.TrueValue = true;
            dgvUsers.Columns.Add(colCheck);

            // 2. Cột ID (Ẩn hoặc hiện)
            var colId = new DataGridViewTextBoxColumn();
            colId.Name = "colId";
            colId.HeaderText = "ID";
            colId.Width = 40;
            colId.Visible = true; // Hoặc false nếu muốn ẩn
            dgvUsers.Columns.Add(colId);

            // 3. Username
            var colUsername = new DataGridViewTextBoxColumn();
            colUsername.Name = "colUsername";
            colUsername.HeaderText = "Username";
            colUsername.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill; // Tự dãn
            dgvUsers.Columns.Add(colUsername);

            // 4. Email
            var colEmail = new DataGridViewTextBoxColumn();
            colEmail.Name = "colEmail";
            colEmail.HeaderText = "Email";
            colEmail.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvUsers.Columns.Add(colEmail);

            // 5. Role
            var colRole = new DataGridViewTextBoxColumn();
            colRole.Name = "colRole";
            colRole.HeaderText = "Role";
            colRole.Width = 80;
            dgvUsers.Columns.Add(colRole);

            // 6. Status
            var colStatus = new DataGridViewTextBoxColumn();
            colStatus.Name = "colStatus";
            colStatus.HeaderText = "Status";
            colStatus.Width = 80;
            dgvUsers.Columns.Add(colStatus);

            // 7. Created
            var colCreated = new DataGridViewTextBoxColumn();
            colCreated.Name = "colCreated";
            colCreated.HeaderText = "Created";
            colCreated.Width = 100;
            dgvUsers.Columns.Add(colCreated);

            // 8. Actions (Cột này dùng ImageColumn để vẽ nút, nhưng ta để trống cũng được vì ta tự vẽ)
            // Tuy nhiên, để an toàn và đúng chuẩn "Actions", ta nên dùng DataGridViewImageColumn
            // Hoặc đơn giản là TextBoxColumn và ta vẽ đè lên.
            // Ở code cũ bạn dùng DataGridViewImageColumn cho colActions, nhưng lại không truyền Image vào Rows.Add -> Lỗi

            // SỬA LỖI: Đổi thành TextBoxColumn cho dễ xử lý (vì ta tự vẽ nút bằng CellPainting)
            var colActions = new DataGridViewTextBoxColumn();
            colActions.Name = "colActions";
            colActions.HeaderText = "Actions";
            colActions.Width = 100;
            dgvUsers.Columns.Add(colActions);
        }

        // ... (Giữ nguyên DgvUsers_CellPainting) ...
        private void DgvUsers_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex < 0) return;

            if (e.ColumnIndex == dgvUsers.Columns["colRole"].Index && e.Value != null)
            {
                string role = e.Value.ToString();
                Color color = (role.ToLower() == "admin") ? Color.FromArgb(220, 53, 69) : Color.FromArgb(13, 110, 253);
                e.Paint(e.CellBounds, DataGridViewPaintParts.All & ~DataGridViewPaintParts.ContentForeground);
                TextRenderer.DrawText(e.Graphics, role, e.CellStyle.Font, e.CellBounds, color, TextFormatFlags.VerticalCenter | TextFormatFlags.Left);
                e.Handled = true;
            }
            else if (e.ColumnIndex == dgvUsers.Columns["colStatus"].Index && e.Value != null)
            {
                string status = e.Value.ToString();
                Color color = (status.ToLower() == "active") ? Color.FromArgb(40, 167, 69) : Color.Gray;
                e.Paint(e.CellBounds, DataGridViewPaintParts.All & ~DataGridViewPaintParts.ContentForeground);
                TextRenderer.DrawText(e.Graphics, status, e.CellStyle.Font, e.CellBounds, color, TextFormatFlags.VerticalCenter | TextFormatFlags.Left);
                e.Handled = true;
            }
            else if (e.ColumnIndex == dgvUsers.Columns["colActions"].Index)
            {
                e.Paint(e.CellBounds, DataGridViewPaintParts.All & ~DataGridViewPaintParts.ContentForeground);
                int centerY = e.CellBounds.Y + (e.CellBounds.Height - ButtonHeight) / 2;
                var rectEdit = new Rectangle(e.CellBounds.X + ButtonMarginX, centerY, ButtonWidth, ButtonHeight);
                var rectDel = new Rectangle(rectEdit.Right + ButtonSpacing, centerY, ButtonWidth, ButtonHeight);

                using (Pen p = new Pen(Color.FromArgb(13, 110, 253))) e.Graphics.DrawRectangle(p, rectEdit);
                e.Graphics.FillRectangle(Brushes.White, rectEdit.X + 1, rectEdit.Y + 1, rectEdit.Width - 2, rectEdit.Height - 2);
                TextRenderer.DrawText(e.Graphics, "📝", new Font("Segoe UI Emoji", 12), rectEdit, Color.FromArgb(13, 110, 253), TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);

                using (Pen p = new Pen(Color.FromArgb(220, 53, 69))) e.Graphics.DrawRectangle(p, rectDel);
                e.Graphics.FillRectangle(Brushes.White, rectDel.X + 1, rectDel.Y + 1, rectDel.Width - 2, rectDel.Height - 2);
                TextRenderer.DrawText(e.Graphics, "🗑", new Font("Segoe UI Emoji", 12), rectDel, Color.FromArgb(220, 53, 69), TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
                e.Handled = true;
            }
        }

        // --- XỬ LÝ CLICK TRỰC TIẾP TRÊN GRID ---
        private void DgvUsers_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && e.RowIndex >= 0 && e.ColumnIndex == dgvUsers.Columns["colActions"].Index)
            {
                int rowHeight = dgvUsers.Rows[e.RowIndex].Height;
                int centerY = (rowHeight - ButtonHeight) / 2;
                var rectEdit = new Rectangle(ButtonMarginX, centerY, ButtonWidth, ButtonHeight);
                var rectDel = new Rectangle(rectEdit.Right + ButtonSpacing, centerY, ButtonWidth, ButtonHeight);

                var userIdVal = dgvUsers.Rows[e.RowIndex].Cells["colId"].Value;
                if (userIdVal == null) return;

                Guid userId = (Guid)userIdVal;
                string username = dgvUsers.Rows[e.RowIndex].Cells["colUsername"].Value.ToString();

                // Kiểm tra tọa độ click
                if (rectEdit.Contains(e.Location))
                {
                    // Mở form Edit từ nút icon trên lưới
                    var form = new AddEditUserForm(_userService, userId);
                    if (form.ShowDialog() == DialogResult.OK)
                    {
                        LoadData();
                    }
                }
                else if (rectDel.Contains(e.Location))
                {
                    if (MessageBox.Show($"Bạn có chắc chắn muốn xóa user: {username}?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                    {
                        bool result = _userService.SoftDeleteUser(userId);

                        if (result)
                        {
                            MessageBox.Show("Đã xóa thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LoadData(); // Reload lại danh sách từ DB
                        }
                        else
                        {
                            MessageBox.Show("Xóa thất bại! Có lỗi xảy ra.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
        }
    }
}