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
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnDangXuat = new Guna.UI2.WinForms.Guna2Button();
            this.btnCaiDat = new Guna.UI2.WinForms.Guna2Button();
            this.btnSystemLogs = new Guna.UI2.WinForms.Guna2Button();
            this.btnQuanLyAssignments = new Guna.UI2.WinForms.Guna2Button();
            this.btnQuanLyUsers = new Guna.UI2.WinForms.Guna2Button();
            this.btnTrangChu = new Guna.UI2.WinForms.Guna2Button();
            this.pnlContent = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.DarkSlateGray;
            this.panel1.Controls.Add(this.btnDangXuat);
            this.panel1.Controls.Add(this.btnCaiDat);
            this.panel1.Controls.Add(this.btnSystemLogs);
            this.panel1.Controls.Add(this.btnQuanLyAssignments);
            this.panel1.Controls.Add(this.btnQuanLyUsers);
            this.panel1.Controls.Add(this.btnTrangChu);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(290, 803);
            this.panel1.TabIndex = 0;
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(290, 66);
            this.panel2.TabIndex = 0;
            // 
            // btnDangXuat
            // 
            this.btnDangXuat.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnDangXuat.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnDangXuat.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnDangXuat.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnDangXuat.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnDangXuat.FillColor = System.Drawing.Color.Transparent;
            this.btnDangXuat.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDangXuat.ForeColor = System.Drawing.Color.White;
            this.btnDangXuat.Image = global::CodeForge_Desktop.Properties.Resources.logout__1_;
            this.btnDangXuat.Location = new System.Drawing.Point(0, 750);
            this.btnDangXuat.Name = "btnDangXuat";
            this.btnDangXuat.Size = new System.Drawing.Size(290, 53);
            this.btnDangXuat.TabIndex = 6;
            this.btnDangXuat.Text = "Đăng xuất";
            // 
            // btnCaiDat
            // 
            this.btnCaiDat.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnCaiDat.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnCaiDat.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnCaiDat.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnCaiDat.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnCaiDat.FillColor = System.Drawing.Color.Transparent;
            this.btnCaiDat.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCaiDat.ForeColor = System.Drawing.Color.White;
            this.btnCaiDat.Image = global::CodeForge_Desktop.Properties.Resources.setting;
            this.btnCaiDat.ImageAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.btnCaiDat.Location = new System.Drawing.Point(0, 278);
            this.btnCaiDat.Name = "btnCaiDat";
            this.btnCaiDat.Size = new System.Drawing.Size(290, 53);
            this.btnCaiDat.TabIndex = 5;
            this.btnCaiDat.Text = "Cài đặt";
            this.btnCaiDat.Click += new System.EventHandler(this.btnCaiDat_Click);
            // 
            // btnSystemLogs
            // 
            this.btnSystemLogs.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnSystemLogs.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnSystemLogs.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnSystemLogs.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnSystemLogs.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnSystemLogs.FillColor = System.Drawing.Color.Transparent;
            this.btnSystemLogs.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSystemLogs.ForeColor = System.Drawing.Color.White;
            this.btnSystemLogs.Image = global::CodeForge_Desktop.Properties.Resources.trend__2_;
            this.btnSystemLogs.ImageAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.btnSystemLogs.Location = new System.Drawing.Point(0, 225);
            this.btnSystemLogs.Name = "btnSystemLogs";
            this.btnSystemLogs.Size = new System.Drawing.Size(290, 53);
            this.btnSystemLogs.TabIndex = 4;
            this.btnSystemLogs.Text = "Nhật kí hệ thống";
            this.btnSystemLogs.Click += new System.EventHandler(this.btnSystemLogs_Click);
            // 
            // btnQuanLyAssignments
            // 
            this.btnQuanLyAssignments.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnQuanLyAssignments.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnQuanLyAssignments.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnQuanLyAssignments.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnQuanLyAssignments.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnQuanLyAssignments.FillColor = System.Drawing.Color.Transparent;
            this.btnQuanLyAssignments.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnQuanLyAssignments.ForeColor = System.Drawing.Color.White;
            this.btnQuanLyAssignments.Image = global::CodeForge_Desktop.Properties.Resources.paper;
            this.btnQuanLyAssignments.ImageAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.btnQuanLyAssignments.Location = new System.Drawing.Point(0, 172);
            this.btnQuanLyAssignments.Name = "btnQuanLyAssignments";
            this.btnQuanLyAssignments.Size = new System.Drawing.Size(290, 53);
            this.btnQuanLyAssignments.TabIndex = 3;
            this.btnQuanLyAssignments.Text = "Quản lí bài tập";
            this.btnQuanLyAssignments.Click += new System.EventHandler(this.btnQuanLyAssignments_Click);
            // 
            // btnQuanLyUsers
            // 
            this.btnQuanLyUsers.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnQuanLyUsers.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnQuanLyUsers.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnQuanLyUsers.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnQuanLyUsers.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnQuanLyUsers.FillColor = System.Drawing.Color.Transparent;
            this.btnQuanLyUsers.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnQuanLyUsers.ForeColor = System.Drawing.Color.White;
            this.btnQuanLyUsers.Image = global::CodeForge_Desktop.Properties.Resources.group;
            this.btnQuanLyUsers.ImageAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.btnQuanLyUsers.Location = new System.Drawing.Point(0, 119);
            this.btnQuanLyUsers.Name = "btnQuanLyUsers";
            this.btnQuanLyUsers.Size = new System.Drawing.Size(290, 53);
            this.btnQuanLyUsers.TabIndex = 2;
            this.btnQuanLyUsers.Text = "Quản lí tài khoản";
            this.btnQuanLyUsers.Click += new System.EventHandler(this.btnQuanLyUsers_Click);
            // 
            // btnTrangChu
            // 
            this.btnTrangChu.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnTrangChu.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnTrangChu.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnTrangChu.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnTrangChu.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnTrangChu.FillColor = System.Drawing.Color.Transparent;
            this.btnTrangChu.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTrangChu.ForeColor = System.Drawing.Color.White;
            this.btnTrangChu.Image = global::CodeForge_Desktop.Properties.Resources.home;
            this.btnTrangChu.ImageAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.btnTrangChu.Location = new System.Drawing.Point(0, 66);
            this.btnTrangChu.Name = "btnTrangChu";
            this.btnTrangChu.Size = new System.Drawing.Size(290, 53);
            this.btnTrangChu.TabIndex = 1;
            this.btnTrangChu.Text = "Trang chủ";
            this.btnTrangChu.Click += new System.EventHandler(this.btnTrangChu_Click);
            // 
            // pnlContent
            // 
            this.pnlContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlContent.Location = new System.Drawing.Point(290, 0);
            this.pnlContent.Name = "pnlContent";
            this.pnlContent.Size = new System.Drawing.Size(1442, 803);
            this.pnlContent.TabIndex = 3;
            // 
            // MainFormAdmin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1732, 803);
            this.Controls.Add(this.pnlContent);
            this.Controls.Add(this.panel1);
            this.Name = "MainFormAdmin";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MainFormAdmin";
            this.Load += new System.EventHandler(this.MainFormAdmin_Load);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private Guna.UI2.WinForms.Guna2Button btnTrangChu;
        private Guna.UI2.WinForms.Guna2Button btnDangXuat;
        private Guna.UI2.WinForms.Guna2Button btnCaiDat;
        private Guna.UI2.WinForms.Guna2Button btnSystemLogs;
        private Guna.UI2.WinForms.Guna2Button btnQuanLyAssignments;
        private Guna.UI2.WinForms.Guna2Button btnQuanLyUsers;
        private System.Windows.Forms.Panel pnlContent;
    }
}