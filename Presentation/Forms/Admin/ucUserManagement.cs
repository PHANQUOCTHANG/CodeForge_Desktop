using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;
using CodeForge_Desktop.Business.Services;
using CodeForge_Desktop.DataAccess.Entities;
using CodeForge_Desktop.DataAccess.Repositories;

namespace CodeForge_Desktop.Presentation.Forms.Admin
{
    public partial class ucUserManagement : UserControl
    {
        private readonly UserService _userService;
        private List<User> _allUsers;
        private const int ButtonWidth = 34;
        private const int ButtonHeight = 34;
        private const int ButtonSpacing = 8;
        private const int ButtonMarginX = 8;

        public ucUserManagement()
        {
            InitializeComponent();

            // Khởi tạo Service
            var userRepo = new UserRepository();
            _userService = new UserService(userRepo);

            // Khởi tạo ComboBox
            InitializeFilters();

            // Gắn Event Handlers
            AttachEventHandlers();

            // Load dữ liệu ban đầu
            LoadData();
        }

        #region Initialization

        private void InitializeFilters()
        {
            // Thiết lập ComboBox Role
            cboRole.SelectedIndex = 0; // "Tất cả"

            // Thiết lập ComboBox Status
            cboStatus.SelectedIndex = 0; // "Tất cả"
        }

        private void AttachEventHandlers()
        {
            // Button Events
            btnAdd.Click += BtnAdd_Click;
            btnEdit.Click += BtnEdit_Click;
            btnDelete.Click += BtnDelete_Click;
            btnRefresh.Click += BtnRefresh_Click;

            // DataGridView Events
            dgvUsers.CellPainting += DgvUsers_CellPainting;
            dgvUsers.CellMouseClick += DgvUsers_CellMouseClick;
            dgvUsers.CellFormatting += DgvUsers_CellFormatting;

            // Search Box Events
            SetupSearchBox();

            // Filter Events
            cboRole.SelectedIndexChanged += Filter_Changed;
            cboStatus.SelectedIndexChanged += Filter_Changed;
        }

        private void SetupSearchBox()
        {
            // Focus events cho placeholder
            txtSearch.GotFocus += (s, e) =>
            {
                if (txtSearch.Text == "Tìm kiếm theo tên hoặc email...")
                {
                    txtSearch.Text = "";
                    txtSearch.ForeColor = Color.FromArgb(52, 58, 64);
                }
                // Đổi màu border khi focus
                pnlSearch.BorderStyle = BorderStyle.FixedSingle;
                pnlSearch.BackColor = Color.FromArgb(240, 248, 255); // Light blue background
            };

            txtSearch.LostFocus += (s, e) =>
            {
                if (string.IsNullOrWhiteSpace(txtSearch.Text))
                {
                    txtSearch.Text = "Tìm kiếm theo tên hoặc email...";
                    txtSearch.ForeColor = Color.FromArgb(173, 181, 189);
                }
                // Trả lại màu border bình thường
                pnlSearch.BackColor = Color.White;
            };

            // TextChanged event cho real-time search
            txtSearch.TextChanged += (s, e) =>
            {
                if (txtSearch.Text != "Tìm kiếm theo tên hoặc email...")
                {
                    ApplyFilters();
                }
            };

            // Click vào icon cũng focus vào textbox
            lblSearchIcon.Click += (s, e) => txtSearch.Focus();
            pnlSearch.Click += (s, e) => txtSearch.Focus();
        }

        #endregion

        #region Data Loading

        private void LoadData()
        {
            try
            {
                _allUsers = _userService.GetAllUsers();
                ApplyFilters();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải dữ liệu: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ApplyFilters()
        {
            if (_allUsers == null) return;

            var filteredUsers = _allUsers.AsEnumerable();

            // Lọc theo Search
            string searchText = txtSearch.Text.Trim();
            if (!string.IsNullOrEmpty(searchText) && searchText != "Tìm kiếm theo tên hoặc email...")
            {
                filteredUsers = filteredUsers.Where(u =>
                    u.Username.ToLower().Contains(searchText.ToLower()) ||
                    u.Email.ToLower().Contains(searchText.ToLower())
                );
            }

            // Lọc theo Role
            string selectedRole = cboRole.SelectedItem?.ToString();
            if (!string.IsNullOrEmpty(selectedRole) && selectedRole != "Tất cả")
            {
                filteredUsers = filteredUsers.Where(u =>
                    u.Role.Equals(selectedRole, StringComparison.OrdinalIgnoreCase)
                );
            }

            // Lọc theo Status
            string selectedStatus = cboStatus.SelectedItem?.ToString();
            if (!string.IsNullOrEmpty(selectedStatus) && selectedStatus != "Tất cả")
            {
                string statusValue = selectedStatus == "Hoạt động" ? "Active" : "Inactive";
                filteredUsers = filteredUsers.Where(u =>
                    u.Status.Equals(statusValue, StringComparison.OrdinalIgnoreCase)
                );
            }

            // Hiển thị dữ liệu
            DisplayUsers(filteredUsers.ToList());
        }

        private void DisplayUsers(List<User> users)
        {
            dgvUsers.Rows.Clear();

            foreach (var user in users)
            {
                int rowIndex = dgvUsers.Rows.Add(
                    false,
                    user.UserID,
                    user.Username,
                    user.Email,
                    user.Role,
                    user.Status,
                    user.JoinDate.ToString("dd/MM/yyyy"),
                    ""
                );

                dgvUsers.Rows[rowIndex].Tag = user.UserID;
            }

            // Cập nhật thông tin tổng kết
            lblSummary.Text = $"Tổng số: {users.Count} người dùng | Hiển thị: {users.Count}/{_allUsers.Count}";

            dgvUsers.ClearSelection();
        }

        #endregion

        #region Button Event Handlers

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                var form = new AddEditUserForm(_userService);
                if (form.ShowDialog() == DialogResult.OK)
                {
                    LoadData();
                    MessageBox.Show("Thêm người dùng thành công!", "Thành công",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi thêm người dùng: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnEdit_Click(object sender, EventArgs e)
        {
            if (dgvUsers.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn người dùng cần chỉnh sửa!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            try
            {
                var userId = (Guid)dgvUsers.SelectedRows[0].Cells["colId"].Value;
                var form = new AddEditUserForm(_userService, userId);

                if (form.ShowDialog() == DialogResult.OK)
                {
                    LoadData();
                    MessageBox.Show("Cập nhật người dùng thành công!", "Thành công",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi chỉnh sửa người dùng: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            List<Guid> idsToDelete = new List<Guid>();

            foreach (DataGridViewRow row in dgvUsers.Rows)
            {
                if (Convert.ToBoolean(row.Cells["colCheck"].Value))
                {
                    if (row.Tag is Guid id)
                    {
                        idsToDelete.Add(id);
                    }
                }
            }

            if (idsToDelete.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn ít nhất một người dùng để xóa!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var result = MessageBox.Show(
                $"Bạn có chắc chắn muốn xóa {idsToDelete.Count} người dùng đã chọn?\n\nThao tác này không thể hoàn tác!",
                "Xác nhận xóa",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning
            );

            if (result == DialogResult.Yes)
            {
                try
                {
                    if (_userService.SoftDeleteListUsers(idsToDelete))
                    {
                        MessageBox.Show($"Đã xóa thành công {idsToDelete.Count} người dùng!", "Thành công",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadData();
                    }
                    else
                    {
                        MessageBox.Show("Xóa người dùng thất bại. Vui lòng thử lại!", "Lỗi",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi khi xóa người dùng: {ex.Message}", "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void BtnRefresh_Click(object sender, EventArgs e)
        {
            // Reset filters
            txtSearch.Text = "Tìm kiếm theo tên hoặc email...";
            txtSearch.ForeColor = Color.FromArgb(173, 181, 189);
            cboRole.SelectedIndex = 0;
            cboStatus.SelectedIndex = 0;

            // Reload data
            LoadData();

            MessageBox.Show("Dữ liệu đã được làm mới!", "Thông báo",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void Filter_Changed(object sender, EventArgs e)
        {
            ApplyFilters();
        }

        #endregion

        #region DataGridView Custom Painting

        private void DgvUsers_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0) return;

            // Alternate row colors
            if (e.RowIndex % 2 == 1)
            {
                dgvUsers.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.FromArgb(252, 252, 253);
            }
        }

        private void DgvUsers_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex < 0) return;

            // Custom painting cho cột Role
            if (e.ColumnIndex == dgvUsers.Columns["colRole"].Index && e.Value != null)
            {
                e.Paint(e.CellBounds, DataGridViewPaintParts.All & ~DataGridViewPaintParts.ContentForeground);

                string role = e.Value.ToString();
                Color bgColor, textColor;

                if (role.Equals("Admin", StringComparison.OrdinalIgnoreCase))
                {
                    bgColor = Color.FromArgb(255, 243, 243);
                    textColor = Color.FromArgb(220, 53, 69);
                }
                else
                {
                    bgColor = Color.FromArgb(232, 244, 255);
                    textColor = Color.FromArgb(13, 110, 253);
                }

                // Vẽ badge
                Rectangle badgeRect = new Rectangle(
                    e.CellBounds.X + 12,
                    e.CellBounds.Y + (e.CellBounds.Height - 24) / 2,
                    70,
                    24
                );

                using (SolidBrush bgBrush = new SolidBrush(bgColor))
                {
                    e.Graphics.FillRectangle(bgBrush, badgeRect);
                }

                using (Pen borderPen = new Pen(textColor, 1))
                {
                    e.Graphics.DrawRectangle(borderPen, badgeRect);
                }

                TextRenderer.DrawText(
                    e.Graphics,
                    role,
                    new Font("Segoe UI", 8.5F, FontStyle.Bold),
                    badgeRect,
                    textColor,
                    TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter
                );

                e.Handled = true;
            }
            // Custom painting cho cột Status
            else if (e.ColumnIndex == dgvUsers.Columns["colStatus"].Index && e.Value != null)
            {
                e.Paint(e.CellBounds, DataGridViewPaintParts.All & ~DataGridViewPaintParts.ContentForeground);

                string status = e.Value.ToString();
                string displayText;
                Color bgColor, textColor;

                if (status.Equals("Active", StringComparison.OrdinalIgnoreCase))
                {
                    displayText = "Hoạt động";
                    bgColor = Color.FromArgb(235, 248, 240);
                    textColor = Color.FromArgb(25, 135, 84);
                }
                else
                {
                    displayText = "Không hoạt động";
                    bgColor = Color.FromArgb(248, 249, 250);
                    textColor = Color.FromArgb(108, 117, 125);
                }

                // Vẽ badge
                Rectangle badgeRect = new Rectangle(
                    e.CellBounds.X + 12,
                    e.CellBounds.Y + (e.CellBounds.Height - 24) / 2,
                    100,
                    24
                );

                using (SolidBrush bgBrush = new SolidBrush(bgColor))
                {
                    e.Graphics.FillRectangle(bgBrush, badgeRect);
                }

                using (Pen borderPen = new Pen(textColor, 1))
                {
                    e.Graphics.DrawRectangle(borderPen, badgeRect);
                }

                // Vẽ dot indicator
                int dotSize = 6;
                Rectangle dotRect = new Rectangle(
                    badgeRect.X + 8,
                    badgeRect.Y + (badgeRect.Height - dotSize) / 2,
                    dotSize,
                    dotSize
                );

                using (SolidBrush dotBrush = new SolidBrush(textColor))
                {
                    e.Graphics.FillEllipse(dotBrush, dotRect);
                }

                // Vẽ text
                Rectangle textRect = new Rectangle(
                    dotRect.Right + 5,
                    badgeRect.Y,
                    badgeRect.Width - dotRect.Width - 13,
                    badgeRect.Height
                );

                TextRenderer.DrawText(
                    e.Graphics,
                    displayText,
                    new Font("Segoe UI", 8F, FontStyle.Bold),
                    textRect,
                    textColor,
                    TextFormatFlags.Left | TextFormatFlags.VerticalCenter
                );

                e.Handled = true;
            }
            // Custom painting cho cột Actions
            else if (e.ColumnIndex == dgvUsers.Columns["colActions"].Index)
            {
                e.Paint(e.CellBounds, DataGridViewPaintParts.All & ~DataGridViewPaintParts.ContentForeground);

                int centerY = e.CellBounds.Y + (e.CellBounds.Height - ButtonHeight) / 2;

                var rectEdit = new Rectangle(
                    e.CellBounds.X + ButtonMarginX,
                    centerY,
                    ButtonWidth,
                    ButtonHeight
                );

                var rectDelete = new Rectangle(
                    rectEdit.Right + ButtonSpacing,
                    centerY,
                    ButtonWidth,
                    ButtonHeight
                );

                // Vẽ button Edit
                DrawActionButton(e.Graphics, rectEdit, "✏️", Color.FromArgb(13, 110, 253),
                    Color.FromArgb(232, 244, 255));

                // Vẽ button Delete
                DrawActionButton(e.Graphics, rectDelete, "🗑️", Color.FromArgb(220, 53, 69),
                    Color.FromArgb(255, 243, 243));

                e.Handled = true;
            }
        }

        private void DrawActionButton(Graphics g, Rectangle rect, string icon, Color borderColor, Color bgColor)
        {
            // Vẽ background
            using (SolidBrush bgBrush = new SolidBrush(bgColor))
            {
                g.FillRectangle(bgBrush, rect);
            }

            // Vẽ border
            using (Pen borderPen = new Pen(borderColor, 1))
            {
                g.DrawRectangle(borderPen, rect);
            }

            // Vẽ icon
            TextRenderer.DrawText(
                g,
                icon,
                new Font("Segoe UI Emoji", 11F),
                rect,
                borderColor,
                TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter
            );
        }

        #endregion

        #region DataGridView Mouse Click Handler

        private void DgvUsers_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && e.RowIndex >= 0 &&
                e.ColumnIndex == dgvUsers.Columns["colActions"].Index)
            {
                int rowHeight = dgvUsers.Rows[e.RowIndex].Height;
                int centerY = (rowHeight - ButtonHeight) / 2;

                var rectEdit = new Rectangle(ButtonMarginX, centerY, ButtonWidth, ButtonHeight);
                var rectDelete = new Rectangle(rectEdit.Right + ButtonSpacing, centerY, ButtonWidth, ButtonHeight);

                var idValue = dgvUsers.Rows[e.RowIndex].Cells["colId"].Value;
                if (idValue == null) return;

                Guid userId = (Guid)idValue;

                if (rectEdit.Contains(e.Location))
                {
                    EditUser(userId);
                }
                else if (rectDelete.Contains(e.Location))
                {
                    DeleteUser(userId);
                }
            }
        }

        private void EditUser(Guid userId)
        {
            try
            {
                var form = new AddEditUserForm(_userService, userId);
                if (form.ShowDialog() == DialogResult.OK)
                {
                    LoadData();
                    MessageBox.Show("Cập nhật người dùng thành công!", "Thành công",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi chỉnh sửa người dùng: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DeleteUser(Guid userId)
        {
            var result = MessageBox.Show(
                "Bạn có chắc chắn muốn xóa người dùng này?\n\nThao tác này không thể hoàn tác!",
                "Xác nhận xóa",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning
            );

            if (result == DialogResult.Yes)
            {
                try
                {
                    if (_userService.SoftDeleteUser(userId))
                    {
                        LoadData();
                        MessageBox.Show("Xóa người dùng thành công!", "Thành công",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Xóa người dùng thất bại. Vui lòng thử lại!", "Lỗi",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi khi xóa người dùng: {ex.Message}", "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        #endregion
    }
}