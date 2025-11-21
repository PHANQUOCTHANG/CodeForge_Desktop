namespace CodeForge_Desktop.Presentation.Forms.Admin
{
    partial class MainFormAdmin
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.pnlSidebar = new System.Windows.Forms.Panel();
            this.pnlMenu = new System.Windows.Forms.Panel();
            this.btnDangXuat = new System.Windows.Forms.Button();
            this.btnCaiDat = new System.Windows.Forms.Button();
            this.btnSystemLogs = new System.Windows.Forms.Button();
            this.btnQuanLyAssignments = new System.Windows.Forms.Button();
            this.btnQuanLyUsers = new System.Windows.Forms.Button();
            this.btnTrangChu = new System.Windows.Forms.Button();
            this.pnlLogo = new System.Windows.Forms.Panel();
            this.lblAppName = new System.Windows.Forms.Label();
            this.lblLogo = new System.Windows.Forms.Label();
            this.pnlContent = new System.Windows.Forms.Panel();
            this.btnCourseManagement = new System.Windows.Forms.Button();
            this.pnlSidebar.SuspendLayout();
            this.pnlMenu.SuspendLayout();
            this.pnlLogo.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlSidebar
            // 
            this.pnlSidebar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.pnlSidebar.Controls.Add(this.pnlMenu);
            this.pnlSidebar.Controls.Add(this.pnlLogo);
            this.pnlSidebar.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlSidebar.Location = new System.Drawing.Point(0, 0);
            this.pnlSidebar.Name = "pnlSidebar";
            this.pnlSidebar.Size = new System.Drawing.Size(250, 803);
            this.pnlSidebar.TabIndex = 0;
            // 
            // pnlMenu
            // 
            this.pnlMenu.Controls.Add(this.btnCourseManagement);
            this.pnlMenu.Controls.Add(this.btnDangXuat);
            this.pnlMenu.Controls.Add(this.btnCaiDat);
            this.pnlMenu.Controls.Add(this.btnSystemLogs);
            this.pnlMenu.Controls.Add(this.btnQuanLyAssignments);
            this.pnlMenu.Controls.Add(this.btnQuanLyUsers);
            this.pnlMenu.Controls.Add(this.btnTrangChu);
            this.pnlMenu.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMenu.Location = new System.Drawing.Point(0, 74);
            this.pnlMenu.Name = "pnlMenu";
            this.pnlMenu.Padding = new System.Windows.Forms.Padding(0, 10, 0, 0);
            this.pnlMenu.Size = new System.Drawing.Size(250, 729);
            this.pnlMenu.TabIndex = 2;
            // 
            // btnDangXuat
            // 
            this.btnDangXuat.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.btnDangXuat.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDangXuat.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnDangXuat.FlatAppearance.BorderSize = 0;
            this.btnDangXuat.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnDangXuat.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDangXuat.Font = new System.Drawing.Font("Segoe UI Semibold", 10.2F, System.Drawing.FontStyle.Bold);
            this.btnDangXuat.ForeColor = System.Drawing.Color.White;
            this.btnDangXuat.Location = new System.Drawing.Point(0, 674);
            this.btnDangXuat.Name = "btnDangXuat";
            this.btnDangXuat.Padding = new System.Windows.Forms.Padding(20, 0, 0, 0);
            this.btnDangXuat.Size = new System.Drawing.Size(250, 55);
            this.btnDangXuat.TabIndex = 6;
            this.btnDangXuat.Text = "Đăng xuất";
            this.btnDangXuat.UseVisualStyleBackColor = false;
            // 
            // btnCaiDat
            // 
            this.btnCaiDat.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCaiDat.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnCaiDat.FlatAppearance.BorderSize = 0;
            this.btnCaiDat.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(70)))), ((int)(((byte)(70)))));
            this.btnCaiDat.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCaiDat.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnCaiDat.ForeColor = System.Drawing.Color.White;
            this.btnCaiDat.Location = new System.Drawing.Point(0, 190);
            this.btnCaiDat.Name = "btnCaiDat";
            this.btnCaiDat.Padding = new System.Windows.Forms.Padding(5, 0, 0, 0);
            this.btnCaiDat.Size = new System.Drawing.Size(250, 45);
            this.btnCaiDat.TabIndex = 5;
            this.btnCaiDat.Text = "⚙  Cài đặt";
            this.btnCaiDat.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCaiDat.UseVisualStyleBackColor = true;
            this.btnCaiDat.Click += new System.EventHandler(this.btnCaiDat_Click);
            // 
            // btnSystemLogs
            // 
            this.btnSystemLogs.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSystemLogs.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnSystemLogs.FlatAppearance.BorderSize = 0;
            this.btnSystemLogs.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(70)))), ((int)(((byte)(70)))));
            this.btnSystemLogs.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSystemLogs.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnSystemLogs.ForeColor = System.Drawing.Color.White;
            this.btnSystemLogs.Location = new System.Drawing.Point(0, 145);
            this.btnSystemLogs.Name = "btnSystemLogs";
            this.btnSystemLogs.Padding = new System.Windows.Forms.Padding(5, 0, 0, 0);
            this.btnSystemLogs.Size = new System.Drawing.Size(250, 45);
            this.btnSystemLogs.TabIndex = 4;
            this.btnSystemLogs.Text = "📉  System Logs";
            this.btnSystemLogs.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSystemLogs.UseVisualStyleBackColor = true;
            this.btnSystemLogs.Click += new System.EventHandler(this.btnSystemLogs_Click);
            // 
            // btnQuanLyAssignments
            // 
            this.btnQuanLyAssignments.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnQuanLyAssignments.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnQuanLyAssignments.FlatAppearance.BorderSize = 0;
            this.btnQuanLyAssignments.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(70)))), ((int)(((byte)(70)))));
            this.btnQuanLyAssignments.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnQuanLyAssignments.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnQuanLyAssignments.ForeColor = System.Drawing.Color.White;
            this.btnQuanLyAssignments.Location = new System.Drawing.Point(0, 100);
            this.btnQuanLyAssignments.Name = "btnQuanLyAssignments";
            this.btnQuanLyAssignments.Padding = new System.Windows.Forms.Padding(5, 0, 0, 0);
            this.btnQuanLyAssignments.Size = new System.Drawing.Size(250, 45);
            this.btnQuanLyAssignments.TabIndex = 3;
            this.btnQuanLyAssignments.Text = "📋  Quản lý Assignments";
            this.btnQuanLyAssignments.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnQuanLyAssignments.UseVisualStyleBackColor = true;
            this.btnQuanLyAssignments.Click += new System.EventHandler(this.btnQuanLyAssignments_Click);
            // 
            // btnQuanLyUsers
            // 
            this.btnQuanLyUsers.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnQuanLyUsers.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnQuanLyUsers.FlatAppearance.BorderSize = 0;
            this.btnQuanLyUsers.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(70)))), ((int)(((byte)(70)))));
            this.btnQuanLyUsers.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnQuanLyUsers.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnQuanLyUsers.ForeColor = System.Drawing.Color.White;
            this.btnQuanLyUsers.Location = new System.Drawing.Point(0, 55);
            this.btnQuanLyUsers.Name = "btnQuanLyUsers";
            this.btnQuanLyUsers.Padding = new System.Windows.Forms.Padding(5, 0, 0, 0);
            this.btnQuanLyUsers.Size = new System.Drawing.Size(250, 45);
            this.btnQuanLyUsers.TabIndex = 2;
            this.btnQuanLyUsers.Text = "👥  Quản lý Users";
            this.btnQuanLyUsers.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnQuanLyUsers.UseVisualStyleBackColor = true;
            this.btnQuanLyUsers.Click += new System.EventHandler(this.btnQuanLyUsers_Click);
            // 
            // btnTrangChu
            // 
            this.btnTrangChu.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnTrangChu.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnTrangChu.FlatAppearance.BorderSize = 0;
            this.btnTrangChu.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(70)))), ((int)(((byte)(70)))));
            this.btnTrangChu.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTrangChu.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnTrangChu.ForeColor = System.Drawing.Color.White;
            this.btnTrangChu.Location = new System.Drawing.Point(0, 10);
            this.btnTrangChu.Name = "btnTrangChu";
            this.btnTrangChu.Padding = new System.Windows.Forms.Padding(5, 0, 0, 0);
            this.btnTrangChu.Size = new System.Drawing.Size(250, 45);
            this.btnTrangChu.TabIndex = 1;
            this.btnTrangChu.Text = "🏠  Trang chủ";
            this.btnTrangChu.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnTrangChu.UseVisualStyleBackColor = true;
            this.btnTrangChu.Click += new System.EventHandler(this.btnTrangChu_Click);
            // 
            // pnlLogo
            // 
            this.pnlLogo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(120)))), ((int)(((byte)(215)))));
            this.pnlLogo.Controls.Add(this.lblAppName);
            this.pnlLogo.Controls.Add(this.lblLogo);
            this.pnlLogo.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlLogo.Location = new System.Drawing.Point(0, 0);
            this.pnlLogo.Name = "pnlLogo";
            this.pnlLogo.Size = new System.Drawing.Size(250, 74);
            this.pnlLogo.TabIndex = 0;
            // 
            // lblAppName
            // 
            this.lblAppName.AutoSize = true;
            this.lblAppName.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblAppName.ForeColor = System.Drawing.Color.White;
            this.lblAppName.Location = new System.Drawing.Point(70, 20);
            this.lblAppName.Name = "lblAppName";
            this.lblAppName.Size = new System.Drawing.Size(110, 46);
            this.lblAppName.TabIndex = 1;
            this.lblAppName.Text = "CodePractice\r\nAdmin";
            // 
            // lblLogo
            // 
            this.lblLogo.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.lblLogo.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblLogo.ForeColor = System.Drawing.Color.White;
            this.lblLogo.Location = new System.Drawing.Point(15, 18);
            this.lblLogo.Name = "lblLogo";
            this.lblLogo.Size = new System.Drawing.Size(45, 45);
            this.lblLogo.TabIndex = 0;
            this.lblLogo.Text = "CP";
            this.lblLogo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnlContent
            // 
            this.pnlContent.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.pnlContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlContent.Location = new System.Drawing.Point(250, 0);
            this.pnlContent.Name = "pnlContent";
            this.pnlContent.Size = new System.Drawing.Size(1532, 803);
            this.pnlContent.TabIndex = 1;
            // 
            // btnCourseManagement
            // 
            this.btnCourseManagement.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCourseManagement.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnCourseManagement.FlatAppearance.BorderSize = 0;
            this.btnCourseManagement.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(70)))), ((int)(((byte)(70)))));
            this.btnCourseManagement.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCourseManagement.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnCourseManagement.ForeColor = System.Drawing.Color.White;
            this.btnCourseManagement.Location = new System.Drawing.Point(0, 235);
            this.btnCourseManagement.Name = "btnCourseManagement";
            this.btnCourseManagement.Padding = new System.Windows.Forms.Padding(5, 0, 0, 0);
            this.btnCourseManagement.Size = new System.Drawing.Size(250, 45);
            this.btnCourseManagement.TabIndex = 7;
            this.btnCourseManagement.Text = "📋  Quản lý Khóa học";
            this.btnCourseManagement.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCourseManagement.UseVisualStyleBackColor = true;
            this.btnCourseManagement.Click += new System.EventHandler(this.btnCourseManagement_Click);
            // 
            // MainFormAdmin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1782, 803);
            this.Controls.Add(this.pnlContent);
            this.Controls.Add(this.pnlSidebar);
            this.Name = "MainFormAdmin";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "CodePractice - Admin";
            this.Load += new System.EventHandler(this.MainFormAdmin_Load);
            this.pnlSidebar.ResumeLayout(false);
            this.pnlMenu.ResumeLayout(false);
            this.pnlLogo.ResumeLayout(false);
            this.pnlLogo.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlSidebar;
        private System.Windows.Forms.Panel pnlLogo;
        private System.Windows.Forms.Label lblLogo;
        private System.Windows.Forms.Label lblAppName;
        private System.Windows.Forms.Panel pnlMenu;
        private System.Windows.Forms.Button btnTrangChu;
        private System.Windows.Forms.Button btnQuanLyUsers;
        private System.Windows.Forms.Button btnQuanLyAssignments;
        private System.Windows.Forms.Button btnCaiDat;
        private System.Windows.Forms.Panel pnlContent;
        private System.Windows.Forms.Button btnSystemLogs;
        private System.Windows.Forms.Button btnDangXuat;
        private System.Windows.Forms.Button btnCourseManagement;
    }
}