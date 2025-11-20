namespace CodeForge_Desktop.Presentation.Forms.Student
{
    partial class MainFormStudent
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
            this.btnDangXuat = new System.Windows.Forms.Button();
            this.pnlMenu = new System.Windows.Forms.Panel();
            this.btnCaiDat = new System.Windows.Forms.Button();
            this.btnLichSuNopBai = new System.Windows.Forms.Button();
            this.btnDanhSachBaiTap = new System.Windows.Forms.Button();
            this.btnTrangChu = new System.Windows.Forms.Button();
            this.pnlLogo = new System.Windows.Forms.Panel();
            this.lblAppName = new System.Windows.Forms.Label();
            this.lblLogo = new System.Windows.Forms.Label();
            this.pnlContent = new System.Windows.Forms.Panel();
            this.btnDanhSachKhoaHoc = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.pnlSidebar.SuspendLayout();
            this.pnlMenu.SuspendLayout();
            this.pnlLogo.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlSidebar
            // 
            this.pnlSidebar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.pnlSidebar.Controls.Add(this.btnDangXuat);
            this.pnlSidebar.Controls.Add(this.pnlMenu);
            this.pnlSidebar.Controls.Add(this.pnlLogo);
            this.pnlSidebar.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlSidebar.Location = new System.Drawing.Point(0, 0);
            this.pnlSidebar.Name = "pnlSidebar";
            this.pnlSidebar.Size = new System.Drawing.Size(240, 803);
            this.pnlSidebar.TabIndex = 0;
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
            this.btnDangXuat.Location = new System.Drawing.Point(0, 748);
            this.btnDangXuat.Name = "btnDangXuat";
            this.btnDangXuat.Padding = new System.Windows.Forms.Padding(20, 0, 0, 0);
            this.btnDangXuat.Size = new System.Drawing.Size(240, 55);
            this.btnDangXuat.TabIndex = 5;
            this.btnDangXuat.Text = "Đăng xuất";
            this.btnDangXuat.UseVisualStyleBackColor = false;
            // 
            // pnlMenu
            // 
            this.pnlMenu.Controls.Add(this.button2);
            this.pnlMenu.Controls.Add(this.btnDanhSachKhoaHoc);
            this.pnlMenu.Controls.Add(this.btnCaiDat);
            this.pnlMenu.Controls.Add(this.btnLichSuNopBai);
            this.pnlMenu.Controls.Add(this.btnDanhSachBaiTap);
            this.pnlMenu.Controls.Add(this.btnTrangChu);
            this.pnlMenu.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMenu.Location = new System.Drawing.Point(0, 80);
            this.pnlMenu.Name = "pnlMenu";
            this.pnlMenu.Padding = new System.Windows.Forms.Padding(0, 10, 0, 0);
            this.pnlMenu.Size = new System.Drawing.Size(240, 723);
            this.pnlMenu.TabIndex = 1;
            // 
            // btnCaiDat
            // 
            this.btnCaiDat.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCaiDat.FlatAppearance.BorderSize = 0;
            this.btnCaiDat.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(70)))), ((int)(((byte)(70)))));
            this.btnCaiDat.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCaiDat.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnCaiDat.ForeColor = System.Drawing.Color.White;
            this.btnCaiDat.Location = new System.Drawing.Point(0, 241);
            this.btnCaiDat.Name = "btnCaiDat";
            this.btnCaiDat.Padding = new System.Windows.Forms.Padding(5, 0, 0, 0);
            this.btnCaiDat.Size = new System.Drawing.Size(240, 45);
            this.btnCaiDat.TabIndex = 4;
            this.btnCaiDat.Text = "⚙  Cài đặt";
            this.btnCaiDat.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCaiDat.UseVisualStyleBackColor = true;
            this.btnCaiDat.Click += new System.EventHandler(this.btnCaiDat_Click);
            // 
            // btnLichSuNopBai
            // 
            this.btnLichSuNopBai.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnLichSuNopBai.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnLichSuNopBai.FlatAppearance.BorderSize = 0;
            this.btnLichSuNopBai.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(70)))), ((int)(((byte)(70)))));
            this.btnLichSuNopBai.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLichSuNopBai.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnLichSuNopBai.ForeColor = System.Drawing.Color.White;
            this.btnLichSuNopBai.Location = new System.Drawing.Point(0, 100);
            this.btnLichSuNopBai.Name = "btnLichSuNopBai";
            this.btnLichSuNopBai.Padding = new System.Windows.Forms.Padding(5, 0, 0, 0);
            this.btnLichSuNopBai.Size = new System.Drawing.Size(240, 45);
            this.btnLichSuNopBai.TabIndex = 3;
            this.btnLichSuNopBai.Text = "🕐  Lịch sử nộp bài";
            this.btnLichSuNopBai.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnLichSuNopBai.UseVisualStyleBackColor = true;
            this.btnLichSuNopBai.Click += new System.EventHandler(this.btnLichSuNopBai_Click);
            // 
            // btnDanhSachBaiTap
            // 
            this.btnDanhSachBaiTap.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDanhSachBaiTap.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnDanhSachBaiTap.FlatAppearance.BorderSize = 0;
            this.btnDanhSachBaiTap.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(70)))), ((int)(((byte)(70)))));
            this.btnDanhSachBaiTap.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDanhSachBaiTap.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnDanhSachBaiTap.ForeColor = System.Drawing.Color.White;
            this.btnDanhSachBaiTap.Location = new System.Drawing.Point(0, 55);
            this.btnDanhSachBaiTap.Name = "btnDanhSachBaiTap";
            this.btnDanhSachBaiTap.Padding = new System.Windows.Forms.Padding(5, 0, 0, 0);
            this.btnDanhSachBaiTap.Size = new System.Drawing.Size(240, 45);
            this.btnDanhSachBaiTap.TabIndex = 2;
            this.btnDanhSachBaiTap.Text = "📋  Danh sách bài tập";
            this.btnDanhSachBaiTap.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDanhSachBaiTap.UseVisualStyleBackColor = true;
            this.btnDanhSachBaiTap.Click += new System.EventHandler(this.btnDanhSachBaiTap_Click);
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
            this.btnTrangChu.Size = new System.Drawing.Size(240, 45);
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
            this.pnlLogo.Size = new System.Drawing.Size(240, 80);
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
            this.lblAppName.Text = "CodePractice\r\nStudent";
            // 
            // lblLogo
            // 
            this.lblLogo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(100)))), ((int)(((byte)(180)))));
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
            this.pnlContent.Location = new System.Drawing.Point(240, 0);
            this.pnlContent.Name = "pnlContent";
            this.pnlContent.Size = new System.Drawing.Size(1542, 803);
            this.pnlContent.TabIndex = 1;
            // 
            // btnDanhSachKhoaHoc
            // 
            this.btnDanhSachKhoaHoc.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDanhSachKhoaHoc.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnDanhSachKhoaHoc.FlatAppearance.BorderSize = 0;
            this.btnDanhSachKhoaHoc.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(70)))), ((int)(((byte)(70)))));
            this.btnDanhSachKhoaHoc.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDanhSachKhoaHoc.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnDanhSachKhoaHoc.ForeColor = System.Drawing.Color.White;
            this.btnDanhSachKhoaHoc.Location = new System.Drawing.Point(0, 145);
            this.btnDanhSachKhoaHoc.Name = "btnDanhSachKhoaHoc";
            this.btnDanhSachKhoaHoc.Padding = new System.Windows.Forms.Padding(5, 0, 0, 0);
            this.btnDanhSachKhoaHoc.Size = new System.Drawing.Size(240, 45);
            this.btnDanhSachKhoaHoc.TabIndex = 5;
            this.btnDanhSachKhoaHoc.Text = "📋  Khóa học";
            this.btnDanhSachKhoaHoc.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDanhSachKhoaHoc.UseVisualStyleBackColor = true;
            this.btnDanhSachKhoaHoc.Click += new System.EventHandler(this.btnDanhSachKhoaHoc_Click);
            // 
            // button2
            // 
            this.button2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button2.Dock = System.Windows.Forms.DockStyle.Top;
            this.button2.FlatAppearance.BorderSize = 0;
            this.button2.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(70)))), ((int)(((byte)(70)))));
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.button2.ForeColor = System.Drawing.Color.White;
            this.button2.Location = new System.Drawing.Point(0, 190);
            this.button2.Name = "button2";
            this.button2.Padding = new System.Windows.Forms.Padding(5, 0, 0, 0);
            this.button2.Size = new System.Drawing.Size(240, 45);
            this.button2.TabIndex = 6;
            this.button2.Text = "📋  Khóa học của tôi";
            this.button2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button2.UseVisualStyleBackColor = true;
            // 
            // MainFormStudent
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1782, 803);
            this.Controls.Add(this.pnlContent);
            this.Controls.Add(this.pnlSidebar);
            this.Name = "MainFormStudent";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "CodePractice - Student";
            this.Load += new System.EventHandler(this.MainFormStudent_Load);
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
        private System.Windows.Forms.Button btnDanhSachBaiTap;
        private System.Windows.Forms.Button btnLichSuNopBai;
        private System.Windows.Forms.Button btnCaiDat;
        private System.Windows.Forms.Button btnDangXuat;
        private System.Windows.Forms.Panel pnlContent;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button btnDanhSachKhoaHoc;
    }
}