using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
using CodeForge_Desktop.Business.Services;
using CodeForge_Desktop.DataAccess.Entities;
using CodeForge_Desktop.DataAccess.Repositories;

namespace CodeForge_Desktop.Presentation.Forms.Admin
{
    public partial class ucUserManagement : UserControl
    {
        private readonly UserService _userService;
        private const int ButtonWidth = 30;
        private const int ButtonHeight = 30;
        private const int ButtonSpacing = 10;
        private const int ButtonMarginX = 5;

        public ucUserManagement()
        {
            InitializeComponent();
            
            // Khởi tạo Service
            var userRepo = new UserRepository();
            _userService = new UserService(userRepo);

            // Gắn Event
            dgvUsers.CellPainting += DgvUsers_CellPainting;
            dgvUsers.CellMouseClick += DgvUsers_CellMouseClick;
            
            btnAdd.Click += BtnAdd_Click;
            btnEdit.Click += BtnEdit_Click;
            btnDelete.Click += BtnDelete_Click;
            
            SetupSearchBox();
            
            // Load Data
            LoadData();
        }

        private void SetupSearchBox()
        {
            txtSearch.GotFocus += (s, e) => { if(txtSearch.Text == "Tìm kiếm user...") { txtSearch.Text = ""; txtSearch.ForeColor = Color.Black; } };
            txtSearch.LostFocus += (s, e) => { if(string.IsNullOrWhiteSpace(txtSearch.Text)) { txtSearch.Text = "Tìm kiếm user..."; txtSearch.ForeColor = Color.Gray; } };
            txtSearch.TextChanged += (s, e) => {
                if (txtSearch.Text != "Tìm kiếm user...") LoadData(txtSearch.Text);
            };
        }

        private void LoadData(string keyword = "")
        {
            dgvUsers.Rows.Clear();
            var allUsers = _userService.GetAllUsers();
            
            // Lọc dữ liệu
            var filteredUsers = string.IsNullOrWhiteSpace(keyword) || keyword == "Tìm kiếm user..." 
                ? allUsers 
                : allUsers.FindAll(u => u.Username.ToLower().Contains(keyword.ToLower()) || u.Email.ToLower().Contains(keyword.ToLower()));

            foreach (var user in filteredUsers)
            {
                int rowIndex = dgvUsers.Rows.Add(
                    false, 
                    user.UserID, 
                    user.Username, 
                    user.Email, 
                    user.Role, 
                    user.Status, 
                    user.JoinDate.ToString("yyyy-MM-dd"), 
                    ""
                );
                // Lưu ID vào Tag để dùng sau
                dgvUsers.Rows[rowIndex].Tag = user.UserID;
            }

            lblSummary.Text = $"Tổng số: {filteredUsers.Count} users";
            dgvUsers.ClearSelection();
        }

        // --- Event Handlers (Giữ nguyên logic cũ của bạn) ---

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            var form = new AddEditUserForm(_userService);
            if (form.ShowDialog() == DialogResult.OK) LoadData();
        }

        private void BtnEdit_Click(object sender, EventArgs e)
        {
            if (dgvUsers.SelectedRows.Count > 0)
            {
                var userId = (Guid)dgvUsers.SelectedRows[0].Cells["colId"].Value;
                var form = new AddEditUserForm(_userService, userId);
                if (form.ShowDialog() == DialogResult.OK) LoadData();
            }
            else MessageBox.Show("Vui lòng chọn user cần sửa!", "Thông báo");
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            List<Guid> idsToDelete = new List<Guid>();
            foreach (DataGridViewRow row in dgvUsers.Rows)
            {
                if (Convert.ToBoolean(row.Cells["colCheck"].Value))
                {
                    if (row.Tag is Guid id) idsToDelete.Add(id);
                }
            }

            if (idsToDelete.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn ít nhất một user để xóa!", "Thông báo");
                return;
            }

            if (MessageBox.Show($"Bạn có chắc chắn muốn xóa {idsToDelete.Count} user?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                if (_userService.SoftDeleteListUsers(idsToDelete))
                {
                    MessageBox.Show("Đã xóa thành công.");
                    LoadData();
                }
                else MessageBox.Show("Xóa thất bại.");
            }
        }

        // --- Custom Painting ---

        private void DgvUsers_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex < 0) return;

            // Tô màu Role
            if (e.ColumnIndex == dgvUsers.Columns["colRole"].Index && e.Value != null)
            {
                string role = e.Value.ToString();
                Color color = (role.ToLower() == "admin") ? Color.FromArgb(220, 53, 69) : Color.FromArgb(13, 110, 253);
                
                // Vẽ nền (để xóa text cũ)
                e.Paint(e.CellBounds, DataGridViewPaintParts.All & ~DataGridViewPaintParts.ContentForeground);
                
                // Vẽ text màu
                TextRenderer.DrawText(e.Graphics, role, e.CellStyle.Font, e.CellBounds, color, TextFormatFlags.VerticalCenter | TextFormatFlags.Left);
                e.Handled = true;
            }
            // Tô màu Status
            else if (e.ColumnIndex == dgvUsers.Columns["colStatus"].Index && e.Value != null)
            {
                string status = e.Value.ToString();
                Color color = (status.ToLower() == "active") ? Color.FromArgb(40, 167, 69) : Color.Gray;
                e.Paint(e.CellBounds, DataGridViewPaintParts.All & ~DataGridViewPaintParts.ContentForeground);
                TextRenderer.DrawText(e.Graphics, status, e.CellStyle.Font, e.CellBounds, color, TextFormatFlags.VerticalCenter | TextFormatFlags.Left);
                e.Handled = true;
            }
            // Vẽ Actions
            else if (e.ColumnIndex == dgvUsers.Columns["colActions"].Index)
            {
                e.Paint(e.CellBounds, DataGridViewPaintParts.All & ~DataGridViewPaintParts.ContentForeground);
                int centerY = e.CellBounds.Y + (e.CellBounds.Height - ButtonHeight) / 2;
                var rectEdit = new Rectangle(e.CellBounds.X + ButtonMarginX, centerY, ButtonWidth, ButtonHeight);
                var rectDel = new Rectangle(rectEdit.Right + ButtonSpacing, centerY, ButtonWidth, ButtonHeight);

                // Edit
                using (Pen p = new Pen(Color.FromArgb(13, 110, 253))) e.Graphics.DrawRectangle(p, rectEdit);
                TextRenderer.DrawText(e.Graphics, "📝", new Font("Segoe UI Emoji", 10), rectEdit, Color.FromArgb(13, 110, 253), TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);

                // Delete
                using (Pen p = new Pen(Color.FromArgb(220, 53, 69))) e.Graphics.DrawRectangle(p, rectDel);
                TextRenderer.DrawText(e.Graphics, "🗑", new Font("Segoe UI Emoji", 10), rectDel, Color.FromArgb(220, 53, 69), TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);

                e.Handled = true;
            }
        }

        private void DgvUsers_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && e.RowIndex >= 0 && e.ColumnIndex == dgvUsers.Columns["colActions"].Index)
            {
                int rowHeight = dgvUsers.Rows[e.RowIndex].Height;
                int centerY = (rowHeight - ButtonHeight) / 2;
                var rectEdit = new Rectangle(ButtonMarginX, centerY, ButtonWidth, ButtonHeight);
                var rectDel = new Rectangle(rectEdit.Right + ButtonSpacing, centerY, ButtonWidth, ButtonHeight);

                var idValue = dgvUsers.Rows[e.RowIndex].Cells["colId"].Value;
                if (idValue == null) return;
                Guid userId = (Guid)idValue;

                if (rectEdit.Contains(e.Location))
                {
                    var form = new AddEditUserForm(_userService, userId);
                    if (form.ShowDialog() == DialogResult.OK) LoadData();
                }
                else if (rectDel.Contains(e.Location))
                {
                    if (MessageBox.Show("Bạn có chắc chắn muốn xóa?", "Xác nhận", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        if (_userService.SoftDeleteUser(userId)) LoadData();
                        else MessageBox.Show("Xóa thất bại.");
                    }
                }
            }
        }
    }
}