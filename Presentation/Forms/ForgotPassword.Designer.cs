namespace CodeForge_Desktop.Presentation.Forms
{
    partial class ForgotPassword
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblSubtitle = new System.Windows.Forms.Label();
            this.pnlContent = new System.Windows.Forms.Panel();
            this.lblStepInfo = new System.Windows.Forms.Label();
            this.pnlEmailVerification = new System.Windows.Forms.Panel();
            this.lblEmailLabel = new System.Windows.Forms.Label();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.btnSendOtp = new System.Windows.Forms.Button();
            this.pnlOtpVerification = new System.Windows.Forms.Panel();
            this.lblOtpLabel = new System.Windows.Forms.Label();
            this.txtOtp = new System.Windows.Forms.TextBox();
            this.btnVerifyOtp = new System.Windows.Forms.Button();
            this.lblOtpTimer = new System.Windows.Forms.Label();
            this.btnResendOtp = new System.Windows.Forms.Button();
            this.pnlChangePassword = new System.Windows.Forms.Panel();
            this.lblNewPassword = new System.Windows.Forms.Label();
            this.txtNewPassword = new System.Windows.Forms.TextBox();
            this.btnToggleNewPassword = new System.Windows.Forms.Button();
            this.lblConfirmPassword = new System.Windows.Forms.Label();
            this.txtConfirmPassword = new System.Windows.Forms.TextBox();
            this.btnToggleConfirmPassword = new System.Windows.Forms.Button();
            this.pnlFooter = new System.Windows.Forms.Panel();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnChangePassword = new System.Windows.Forms.Button();
            this.pnlHeader.SuspendLayout();
            this.pnlContent.SuspendLayout();
            this.pnlEmailVerification.SuspendLayout();
            this.pnlOtpVerification.SuspendLayout();
            this.pnlChangePassword.SuspendLayout();
            this.pnlFooter.SuspendLayout();
            this.SuspendLayout();

            // pnlHeader
            this.pnlHeader.BackColor = System.Drawing.Color.FromArgb(0, 120, 215);
            this.pnlHeader.Controls.Add(this.lblSubtitle);
            this.pnlHeader.Controls.Add(this.lblTitle);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Padding = new System.Windows.Forms.Padding(30, 20, 30, 20);
            this.pnlHeader.Size = new System.Drawing.Size(700, 100);
            this.pnlHeader.TabIndex = 0;

            // lblTitle
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(30, 20);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(350, 37);
            this.lblTitle.Text = "🔐 Đặt Lại Mật Khẩu";

            // lblSubtitle
            this.lblSubtitle.AutoSize = true;
            this.lblSubtitle.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.lblSubtitle.ForeColor = System.Drawing.Color.FromArgb(200, 220, 240);
            this.lblSubtitle.Location = new System.Drawing.Point(30, 60);
            this.lblSubtitle.Name = "lblSubtitle";
            this.lblSubtitle.Size = new System.Drawing.Size(320, 17);
            this.lblSubtitle.Text = "Nhập email để nhận mã xác thực qua email";

            // pnlContent
            this.pnlContent.BackColor = System.Drawing.Color.FromArgb(240, 245, 250);
            this.pnlContent.Controls.Add(this.pnlChangePassword);
            this.pnlContent.Controls.Add(this.pnlOtpVerification);
            this.pnlContent.Controls.Add(this.pnlEmailVerification);
            this.pnlContent.Controls.Add(this.lblStepInfo);
            this.pnlContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlContent.Location = new System.Drawing.Point(0, 100);
            this.pnlContent.Name = "pnlContent";
            this.pnlContent.Padding = new System.Windows.Forms.Padding(40, 30, 40, 30);
            this.pnlContent.Size = new System.Drawing.Size(700, 420);
            this.pnlContent.TabIndex = 1;

            // lblStepInfo
            this.lblStepInfo.AutoSize = true;
            this.lblStepInfo.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.lblStepInfo.ForeColor = System.Drawing.Color.FromArgb(0, 120, 215);
            this.lblStepInfo.Location = new System.Drawing.Point(40, 30);
            this.lblStepInfo.Name = "lblStepInfo";
            this.lblStepInfo.Size = new System.Drawing.Size(150, 17);
            this.lblStepInfo.Text = "📝 Bước 1: Nhập Email";

            // pnlEmailVerification
            this.pnlEmailVerification.BackColor = System.Drawing.Color.White;
            this.pnlEmailVerification.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlEmailVerification.Controls.Add(this.btnSendOtp);
            this.pnlEmailVerification.Controls.Add(this.txtEmail);
            this.pnlEmailVerification.Controls.Add(this.lblEmailLabel);
            this.pnlEmailVerification.Location = new System.Drawing.Point(40, 60);
            this.pnlEmailVerification.Name = "pnlEmailVerification";
            this.pnlEmailVerification.Padding = new System.Windows.Forms.Padding(25);
            this.pnlEmailVerification.Size = new System.Drawing.Size(620, 140);
            this.pnlEmailVerification.TabIndex = 2;

            // lblEmailLabel
            this.lblEmailLabel.AutoSize = true;
            this.lblEmailLabel.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.lblEmailLabel.ForeColor = System.Drawing.Color.FromArgb(51, 51, 51);
            this.lblEmailLabel.Location = new System.Drawing.Point(25, 15);
            this.lblEmailLabel.Name = "lblEmailLabel";
            this.lblEmailLabel.Size = new System.Drawing.Size(55, 19);
            this.lblEmailLabel.Text = "Email:";

            // txtEmail
            this.txtEmail.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.txtEmail.Location = new System.Drawing.Point(25, 40);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(570, 30);
            this.txtEmail.TabIndex = 0;

            // btnSendOtp
            this.btnSendOtp.BackColor = System.Drawing.Color.FromArgb(0, 120, 215);
            this.btnSendOtp.FlatAppearance.BorderSize = 0;
            this.btnSendOtp.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSendOtp.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.btnSendOtp.ForeColor = System.Drawing.Color.White;
            this.btnSendOtp.Location = new System.Drawing.Point(470, 85);
            this.btnSendOtp.Name = "btnSendOtp";
            this.btnSendOtp.Size = new System.Drawing.Size(125, 35);
            this.btnSendOtp.TabIndex = 1;
            this.btnSendOtp.Text = "📧 Gửi OTP";
            this.btnSendOtp.UseVisualStyleBackColor = false;
            this.btnSendOtp.Click += new System.EventHandler(this.btnSendOtp_Click);

            // pnlOtpVerification
            this.pnlOtpVerification.BackColor = System.Drawing.Color.White;
            this.pnlOtpVerification.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlOtpVerification.Controls.Add(this.btnResendOtp);
            this.pnlOtpVerification.Controls.Add(this.lblOtpTimer);
            this.pnlOtpVerification.Controls.Add(this.btnVerifyOtp);
            this.pnlOtpVerification.Controls.Add(this.txtOtp);
            this.pnlOtpVerification.Controls.Add(this.lblOtpLabel);
            this.pnlOtpVerification.Location = new System.Drawing.Point(40, 60);
            this.pnlOtpVerification.Name = "pnlOtpVerification";
            this.pnlOtpVerification.Padding = new System.Windows.Forms.Padding(25);
            this.pnlOtpVerification.Size = new System.Drawing.Size(620, 160);
            this.pnlOtpVerification.TabIndex = 3;
            this.pnlOtpVerification.Visible = false;

            // lblOtpLabel
            this.lblOtpLabel.AutoSize = true;
            this.lblOtpLabel.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.lblOtpLabel.ForeColor = System.Drawing.Color.FromArgb(51, 51, 51);
            this.lblOtpLabel.Location = new System.Drawing.Point(25, 15);
            this.lblOtpLabel.Name = "lblOtpLabel";
            this.lblOtpLabel.Size = new System.Drawing.Size(140, 19);
            this.lblOtpLabel.Text = "Mã OTP (6 chữ số):";

            // txtOtp
            this.txtOtp.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.txtOtp.Location = new System.Drawing.Point(25, 40);
            this.txtOtp.MaxLength = 6;
            this.txtOtp.Name = "txtOtp";
            this.txtOtp.Size = new System.Drawing.Size(180, 36);
            this.txtOtp.TabIndex = 0;
            this.txtOtp.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;

            // btnVerifyOtp
            this.btnVerifyOtp.BackColor = System.Drawing.Color.FromArgb(76, 175, 80);
            this.btnVerifyOtp.FlatAppearance.BorderSize = 0;
            this.btnVerifyOtp.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnVerifyOtp.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnVerifyOtp.ForeColor = System.Drawing.Color.White;
            this.btnVerifyOtp.Location = new System.Drawing.Point(215, 40);
            this.btnVerifyOtp.Name = "btnVerifyOtp";
            this.btnVerifyOtp.Size = new System.Drawing.Size(100, 36);
            this.btnVerifyOtp.TabIndex = 1;
            this.btnVerifyOtp.Text = "✓ Xác nhận";
            this.btnVerifyOtp.UseVisualStyleBackColor = false;
            this.btnVerifyOtp.Click += new System.EventHandler(this.btnVerifyOtp_Click);

            // lblOtpTimer
            this.lblOtpTimer.AutoSize = true;
            this.lblOtpTimer.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.lblOtpTimer.ForeColor = System.Drawing.Color.FromArgb(244, 67, 54);
            this.lblOtpTimer.Location = new System.Drawing.Point(25, 110);
            this.lblOtpTimer.Name = "lblOtpTimer";
            this.lblOtpTimer.Size = new System.Drawing.Size(200, 17);
            this.lblOtpTimer.Text = "⏱️ Hết hạn sau: 5:00";

            // btnResendOtp
            this.btnResendOtp.BackColor = System.Drawing.Color.FromArgb(255, 152, 0);
            this.btnResendOtp.FlatAppearance.BorderSize = 0;
            this.btnResendOtp.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnResendOtp.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnResendOtp.ForeColor = System.Drawing.Color.White;
            this.btnResendOtp.Location = new System.Drawing.Point(470, 110);
            this.btnResendOtp.Name = "btnResendOtp";
            this.btnResendOtp.Size = new System.Drawing.Size(125, 35);
            this.btnResendOtp.TabIndex = 2;
            this.btnResendOtp.Text = "🔄 Gửi lại";
            this.btnResendOtp.UseVisualStyleBackColor = false;
            this.btnResendOtp.Click += new System.EventHandler(this.btnResendOtp_Click);

            // pnlChangePassword
            this.pnlChangePassword.BackColor = System.Drawing.Color.White;
            this.pnlChangePassword.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlChangePassword.Controls.Add(this.btnToggleConfirmPassword);
            this.pnlChangePassword.Controls.Add(this.txtConfirmPassword);
            this.pnlChangePassword.Controls.Add(this.lblConfirmPassword);
            this.pnlChangePassword.Controls.Add(this.btnToggleNewPassword);
            this.pnlChangePassword.Controls.Add(this.txtNewPassword);
            this.pnlChangePassword.Controls.Add(this.lblNewPassword);
            this.pnlChangePassword.Location = new System.Drawing.Point(40, 60);
            this.pnlChangePassword.Name = "pnlChangePassword";
            this.pnlChangePassword.Padding = new System.Windows.Forms.Padding(25);
            this.pnlChangePassword.Size = new System.Drawing.Size(620, 220);
            this.pnlChangePassword.TabIndex = 4;
            this.pnlChangePassword.Visible = false;

            // lblNewPassword
            this.lblNewPassword.AutoSize = true;
            this.lblNewPassword.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.lblNewPassword.ForeColor = System.Drawing.Color.FromArgb(51, 51, 51);
            this.lblNewPassword.Location = new System.Drawing.Point(25, 15);
            this.lblNewPassword.Name = "lblNewPassword";
            this.lblNewPassword.Size = new System.Drawing.Size(115, 19);
            this.lblNewPassword.Text = "Mật khẩu mới:";

            // txtNewPassword
            this.txtNewPassword.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.txtNewPassword.Location = new System.Drawing.Point(25, 40);
            this.txtNewPassword.Name = "txtNewPassword";
            this.txtNewPassword.PasswordChar = '*';
            this.txtNewPassword.Size = new System.Drawing.Size(545, 30);
            this.txtNewPassword.TabIndex = 0;

            // btnToggleNewPassword
            this.btnToggleNewPassword.BackColor = System.Drawing.Color.White;
            this.btnToggleNewPassword.FlatAppearance.BorderSize = 1;
            this.btnToggleNewPassword.FlatAppearance.BorderColor = System.Drawing.Color.LightGray;
            this.btnToggleNewPassword.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnToggleNewPassword.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnToggleNewPassword.Location = new System.Drawing.Point(570, 40);
            this.btnToggleNewPassword.Name = "btnToggleNewPassword";
            this.btnToggleNewPassword.Size = new System.Drawing.Size(25, 30);
            this.btnToggleNewPassword.TabIndex = 1;
            this.btnToggleNewPassword.Text = "👁️";
            this.btnToggleNewPassword.UseVisualStyleBackColor = false;
            this.btnToggleNewPassword.Click += new System.EventHandler(this.btnToggleNewPassword_Click);

            // lblConfirmPassword
            this.lblConfirmPassword.AutoSize = true;
            this.lblConfirmPassword.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.lblConfirmPassword.ForeColor = System.Drawing.Color.FromArgb(51, 51, 51);
            this.lblConfirmPassword.Location = new System.Drawing.Point(25, 85);
            this.lblConfirmPassword.Name = "lblConfirmPassword";
            this.lblConfirmPassword.Size = new System.Drawing.Size(160, 19);
            this.lblConfirmPassword.Text = "Xác nhận mật khẩu:";

            // txtConfirmPassword
            this.txtConfirmPassword.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.txtConfirmPassword.Location = new System.Drawing.Point(25, 110);
            this.txtConfirmPassword.Name = "txtConfirmPassword";
            this.txtConfirmPassword.PasswordChar = '*';
            this.txtConfirmPassword.Size = new System.Drawing.Size(545, 30);
            this.txtConfirmPassword.TabIndex = 2;

            // btnToggleConfirmPassword
            this.btnToggleConfirmPassword.BackColor = System.Drawing.Color.White;
            this.btnToggleConfirmPassword.FlatAppearance.BorderSize = 1;
            this.btnToggleConfirmPassword.FlatAppearance.BorderColor = System.Drawing.Color.LightGray;
            this.btnToggleConfirmPassword.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnToggleConfirmPassword.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnToggleConfirmPassword.Location = new System.Drawing.Point(570, 110);
            this.btnToggleConfirmPassword.Name = "btnToggleConfirmPassword";
            this.btnToggleConfirmPassword.Size = new System.Drawing.Size(25, 30);
            this.btnToggleConfirmPassword.TabIndex = 3;
            this.btnToggleConfirmPassword.Text = "👁️";
            this.btnToggleConfirmPassword.UseVisualStyleBackColor = false;
            this.btnToggleConfirmPassword.Click += new System.EventHandler(this.btnToggleConfirmPassword_Click);

            // pnlFooter
            this.pnlFooter.BackColor = System.Drawing.Color.FromArgb(248, 249, 250);
            this.pnlFooter.Controls.Add(this.btnChangePassword);
            this.pnlFooter.Controls.Add(this.btnCancel);
            this.pnlFooter.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlFooter.Location = new System.Drawing.Point(0, 520);
            this.pnlFooter.Name = "pnlFooter";
            this.pnlFooter.Padding = new System.Windows.Forms.Padding(40);
            this.pnlFooter.Size = new System.Drawing.Size(700, 80);
            this.pnlFooter.TabIndex = 5;

            // btnCancel
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.BackColor = System.Drawing.Color.FromArgb(158, 158, 158);
            this.btnCancel.FlatAppearance.BorderSize = 0;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.btnCancel.ForeColor = System.Drawing.Color.White;
            this.btnCancel.Location = new System.Drawing.Point(520, 20);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(120, 40);
            this.btnCancel.TabIndex = 0;
            this.btnCancel.Text = "✕ Hủy";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);

            // btnChangePassword
            this.btnChangePassword.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnChangePassword.BackColor = System.Drawing.Color.FromArgb(76, 175, 80);
            this.btnChangePassword.FlatAppearance.BorderSize = 0;
            this.btnChangePassword.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnChangePassword.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.btnChangePassword.ForeColor = System.Drawing.Color.White;
            this.btnChangePassword.Location = new System.Drawing.Point(520, 20);
            this.btnChangePassword.Name = "btnChangePassword";
            this.btnChangePassword.Size = new System.Drawing.Size(120, 40);
            this.btnChangePassword.TabIndex = 1;
            this.btnChangePassword.Text = "✓ Đổi Mật Khẩu";
            this.btnChangePassword.UseVisualStyleBackColor = false;
            this.btnChangePassword.Visible = false;
            this.btnChangePassword.Click += new System.EventHandler(this.btnChangePassword_Click);

            // ForgotPassword
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(700, 600);
            this.Controls.Add(this.pnlContent);
            this.Controls.Add(this.pnlFooter);
            this.Controls.Add(this.pnlHeader);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ForgotPassword";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Đặt Lại Mật Khẩu";
            this.Load += new System.EventHandler(this.ForgotPassword_Load);
            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            this.pnlContent.ResumeLayout(false);
            this.pnlContent.PerformLayout();
            this.pnlEmailVerification.ResumeLayout(false);
            this.pnlEmailVerification.PerformLayout();
            this.pnlOtpVerification.ResumeLayout(false);
            this.pnlOtpVerification.PerformLayout();
            this.pnlChangePassword.ResumeLayout(false);
            this.pnlChangePassword.PerformLayout();
            this.pnlFooter.ResumeLayout(false);
            this.ResumeLayout(false);
        }


        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblSubtitle;
        private System.Windows.Forms.Panel pnlContent;
        private System.Windows.Forms.Label lblStepInfo;
        private System.Windows.Forms.Panel pnlEmailVerification;
        private System.Windows.Forms.Label lblEmailLabel;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.Button btnSendOtp;
        private System.Windows.Forms.Panel pnlOtpVerification;
        private System.Windows.Forms.Label lblOtpLabel;
        private System.Windows.Forms.TextBox txtOtp;
        private System.Windows.Forms.Button btnVerifyOtp;
        private System.Windows.Forms.Label lblOtpTimer;
        private System.Windows.Forms.Button btnResendOtp;
        private System.Windows.Forms.Panel pnlChangePassword;
        private System.Windows.Forms.Label lblNewPassword;
        private System.Windows.Forms.TextBox txtNewPassword;
        private System.Windows.Forms.Button btnToggleNewPassword;
        private System.Windows.Forms.Label lblConfirmPassword;
        private System.Windows.Forms.TextBox txtConfirmPassword;
        private System.Windows.Forms.Button btnToggleConfirmPassword;
        private System.Windows.Forms.Panel pnlFooter;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnChangePassword;
    }
}