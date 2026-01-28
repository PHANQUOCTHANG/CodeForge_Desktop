namespace CodeForge_Desktop.Presentation.Forms.Admin
{
    partial class AddEditUserForm
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

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.pnlMain = new System.Windows.Forms.Panel();
            this.pnlContent = new System.Windows.Forms.Panel();
            this.pnlFormFields = new System.Windows.Forms.Panel();
            this.pnlStatusField = new System.Windows.Forms.Panel();
            this.cboStatus = new System.Windows.Forms.ComboBox();
            this.lblStatus = new System.Windows.Forms.Label();
            this.lblStatusIcon = new System.Windows.Forms.Label();
            this.pnlRoleField = new System.Windows.Forms.Panel();
            this.cboRole = new System.Windows.Forms.ComboBox();
            this.lblRole = new System.Windows.Forms.Label();
            this.lblRoleIcon = new System.Windows.Forms.Label();
            this.pnlPasswordField = new System.Windows.Forms.Panel();
            this.lblPasswordHint = new System.Windows.Forms.Label();
            this.btnTogglePassword = new System.Windows.Forms.Button();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.lblPassword = new System.Windows.Forms.Label();
            this.lblPasswordIcon = new System.Windows.Forms.Label();
            this.pnlEmailField = new System.Windows.Forms.Panel();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.lblEmail = new System.Windows.Forms.Label();
            this.lblEmailIcon = new System.Windows.Forms.Label();
            this.pnlUsernameField = new System.Windows.Forms.Panel();
            this.txtUsername = new System.Windows.Forms.TextBox();
            this.lblUsername = new System.Windows.Forms.Label();
            this.lblUsernameIcon = new System.Windows.Forms.Label();
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.lblSubtitle = new System.Windows.Forms.Label();
            this.lblTitle = new System.Windows.Forms.Label();
            this.pnlHeaderIcon = new System.Windows.Forms.Panel();
            this.lblHeaderIcon = new System.Windows.Forms.Label();
            this.pnlFooter = new System.Windows.Forms.Panel();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.pnlMain.SuspendLayout();
            this.pnlContent.SuspendLayout();
            this.pnlFormFields.SuspendLayout();
            this.pnlStatusField.SuspendLayout();
            this.pnlRoleField.SuspendLayout();
            this.pnlPasswordField.SuspendLayout();
            this.pnlEmailField.SuspendLayout();
            this.pnlUsernameField.SuspendLayout();
            this.pnlHeader.SuspendLayout();
            this.pnlHeaderIcon.SuspendLayout();
            this.pnlFooter.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlMain
            // 
            this.pnlMain.BackColor = System.Drawing.Color.White;
            this.pnlMain.Controls.Add(this.pnlContent);
            this.pnlMain.Controls.Add(this.pnlHeader);
            this.pnlMain.Controls.Add(this.pnlFooter);
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Location = new System.Drawing.Point(0, 0);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(600, 650);
            this.pnlMain.TabIndex = 0;
            // 
            // pnlContent
            // 
            this.pnlContent.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(250)))), ((int)(((byte)(250)))));
            this.pnlContent.Controls.Add(this.pnlFormFields);
            this.pnlContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlContent.Location = new System.Drawing.Point(0, 120);
            this.pnlContent.Name = "pnlContent";
            this.pnlContent.Padding = new System.Windows.Forms.Padding(30, 25, 30, 25);
            this.pnlContent.Size = new System.Drawing.Size(600, 450);
            this.pnlContent.TabIndex = 1;
            // 
            // pnlFormFields
            // 
            this.pnlFormFields.BackColor = System.Drawing.Color.White;
            this.pnlFormFields.Controls.Add(this.pnlStatusField);
            this.pnlFormFields.Controls.Add(this.pnlRoleField);
            this.pnlFormFields.Controls.Add(this.pnlPasswordField);
            this.pnlFormFields.Controls.Add(this.pnlEmailField);
            this.pnlFormFields.Controls.Add(this.pnlUsernameField);
            this.pnlFormFields.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlFormFields.Location = new System.Drawing.Point(30, 25);
            this.pnlFormFields.Name = "pnlFormFields";
            this.pnlFormFields.Padding = new System.Windows.Forms.Padding(25, 20, 25, 20);
            this.pnlFormFields.Size = new System.Drawing.Size(540, 400);
            this.pnlFormFields.TabIndex = 0;
            // 
            // pnlStatusField
            // 
            this.pnlStatusField.Controls.Add(this.cboStatus);
            this.pnlStatusField.Controls.Add(this.lblStatus);
            this.pnlStatusField.Controls.Add(this.lblStatusIcon);
            this.pnlStatusField.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlStatusField.Location = new System.Drawing.Point(25, 300);
            this.pnlStatusField.Name = "pnlStatusField";
            this.pnlStatusField.Padding = new System.Windows.Forms.Padding(0, 0, 0, 15);
            this.pnlStatusField.Size = new System.Drawing.Size(490, 70);
            this.pnlStatusField.TabIndex = 4;
            // 
            // cboStatus
            // 
            this.cboStatus.BackColor = System.Drawing.Color.White;
            this.cboStatus.Dock = System.Windows.Forms.DockStyle.Top;
            this.cboStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboStatus.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cboStatus.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.cboStatus.FormattingEnabled = true;
            this.cboStatus.Items.AddRange(new object[] {
            "Đang hoạt động",
            "Tạm khóa"});
            this.cboStatus.Location = new System.Drawing.Point(45, 25);
            this.cboStatus.Name = "cboStatus";
            this.cboStatus.Size = new System.Drawing.Size(445, 31);
            this.cboStatus.TabIndex = 4;
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblStatus.Font = new System.Drawing.Font("Segoe UI Semibold", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblStatus.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.lblStatus.Location = new System.Drawing.Point(45, 0);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Padding = new System.Windows.Forms.Padding(0, 0, 0, 5);
            this.lblStatus.Size = new System.Drawing.Size(84, 26);
            this.lblStatus.TabIndex = 1;
            this.lblStatus.Text = "Trạng thái";
            // 
            // lblStatusIcon
            // 
            this.lblStatusIcon.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblStatusIcon.Font = new System.Drawing.Font("Segoe UI", 14F);
            this.lblStatusIcon.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(150)))), ((int)(((byte)(136)))));
            this.lblStatusIcon.Location = new System.Drawing.Point(0, 0);
            this.lblStatusIcon.Name = "lblStatusIcon";
            this.lblStatusIcon.Size = new System.Drawing.Size(45, 55);
            this.lblStatusIcon.TabIndex = 0;
            this.lblStatusIcon.Text = "●";
            this.lblStatusIcon.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnlRoleField
            // 
            this.pnlRoleField.Controls.Add(this.cboRole);
            this.pnlRoleField.Controls.Add(this.lblRole);
            this.pnlRoleField.Controls.Add(this.lblRoleIcon);
            this.pnlRoleField.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlRoleField.Location = new System.Drawing.Point(25, 230);
            this.pnlRoleField.Name = "pnlRoleField";
            this.pnlRoleField.Padding = new System.Windows.Forms.Padding(0, 0, 0, 15);
            this.pnlRoleField.Size = new System.Drawing.Size(490, 70);
            this.pnlRoleField.TabIndex = 3;
            // 
            // cboRole
            // 
            this.cboRole.BackColor = System.Drawing.Color.White;
            this.cboRole.Dock = System.Windows.Forms.DockStyle.Top;
            this.cboRole.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboRole.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cboRole.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.cboRole.FormattingEnabled = true;
            this.cboRole.Items.AddRange(new object[] {
            "Sinh viên",
            "Quản trị viên"});
            this.cboRole.Location = new System.Drawing.Point(45, 25);
            this.cboRole.Name = "cboRole";
            this.cboRole.Size = new System.Drawing.Size(445, 31);
            this.cboRole.TabIndex = 3;
            // 
            // lblRole
            // 
            this.lblRole.AutoSize = true;
            this.lblRole.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblRole.Font = new System.Drawing.Font("Segoe UI Semibold", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblRole.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.lblRole.Location = new System.Drawing.Point(45, 0);
            this.lblRole.Name = "lblRole";
            this.lblRole.Padding = new System.Windows.Forms.Padding(0, 0, 0, 5);
            this.lblRole.Size = new System.Drawing.Size(54, 26);
            this.lblRole.TabIndex = 1;
            this.lblRole.Text = "Vai trò";
            // 
            // lblRoleIcon
            // 
            this.lblRoleIcon.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblRoleIcon.Font = new System.Drawing.Font("Segoe UI", 14F);
            this.lblRoleIcon.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(103)))), ((int)(((byte)(58)))), ((int)(((byte)(183)))));
            this.lblRoleIcon.Location = new System.Drawing.Point(0, 0);
            this.lblRoleIcon.Name = "lblRoleIcon";
            this.lblRoleIcon.Size = new System.Drawing.Size(45, 55);
            this.lblRoleIcon.TabIndex = 0;
            this.lblRoleIcon.Text = "👤";
            this.lblRoleIcon.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnlPasswordField
            // 
            this.pnlPasswordField.Controls.Add(this.lblPasswordHint);
            this.pnlPasswordField.Controls.Add(this.btnTogglePassword);
            this.pnlPasswordField.Controls.Add(this.txtPassword);
            this.pnlPasswordField.Controls.Add(this.lblPassword);
            this.pnlPasswordField.Controls.Add(this.lblPasswordIcon);
            this.pnlPasswordField.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlPasswordField.Location = new System.Drawing.Point(25, 140);
            this.pnlPasswordField.Name = "pnlPasswordField";
            this.pnlPasswordField.Padding = new System.Windows.Forms.Padding(0, 0, 0, 15);
            this.pnlPasswordField.Size = new System.Drawing.Size(490, 90);
            this.pnlPasswordField.TabIndex = 2;
            // 
            // lblPasswordHint
            // 
            this.lblPasswordHint.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblPasswordHint.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Italic);
            this.lblPasswordHint.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(120)))), ((int)(((byte)(120)))));
            this.lblPasswordHint.Location = new System.Drawing.Point(45, 55);
            this.lblPasswordHint.Name = "lblPasswordHint";
            this.lblPasswordHint.Padding = new System.Windows.Forms.Padding(0, 3, 0, 0);
            this.lblPasswordHint.Size = new System.Drawing.Size(405, 20);
            this.lblPasswordHint.TabIndex = 5;
            this.lblPasswordHint.Text = "(Để trống nếu không muốn thay đổi mật khẩu)";
            this.lblPasswordHint.Visible = false;
            // 
            // btnTogglePassword
            // 
            this.btnTogglePassword.BackColor = System.Drawing.Color.Transparent;
            this.btnTogglePassword.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnTogglePassword.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnTogglePassword.FlatAppearance.BorderSize = 0;
            this.btnTogglePassword.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTogglePassword.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnTogglePassword.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(120)))), ((int)(((byte)(120)))));
            this.btnTogglePassword.Location = new System.Drawing.Point(450, 25);
            this.btnTogglePassword.Name = "btnTogglePassword";
            this.btnTogglePassword.Size = new System.Drawing.Size(40, 31);
            this.btnTogglePassword.TabIndex = 2;
            this.btnTogglePassword.TabStop = false;
            this.btnTogglePassword.Text = "👁";
            this.btnTogglePassword.UseVisualStyleBackColor = false;
            this.btnTogglePassword.Click += new System.EventHandler(this.btnTogglePassword_Click);
            // 
            // txtPassword
            // 
            this.txtPassword.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPassword.Dock = System.Windows.Forms.DockStyle.Top;
            this.txtPassword.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtPassword.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.txtPassword.Location = new System.Drawing.Point(45, 25);
            this.txtPassword.MaxLength = 100;
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '●';
            this.txtPassword.Size = new System.Drawing.Size(445, 30);
            this.txtPassword.TabIndex = 2;
            // 
            // lblPassword
            // 
            this.lblPassword.AutoSize = true;
            this.lblPassword.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblPassword.Font = new System.Drawing.Font("Segoe UI Semibold", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblPassword.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.lblPassword.Location = new System.Drawing.Point(45, 0);
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.Padding = new System.Windows.Forms.Padding(0, 0, 0, 5);
            this.lblPassword.Size = new System.Drawing.Size(75, 26);
            this.lblPassword.TabIndex = 1;
            this.lblPassword.Text = "Mật khẩu";
            // 
            // lblPasswordIcon
            // 
            this.lblPasswordIcon.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblPasswordIcon.Font = new System.Drawing.Font("Segoe UI", 14F);
            this.lblPasswordIcon.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(67)))), ((int)(((byte)(54)))));
            this.lblPasswordIcon.Location = new System.Drawing.Point(0, 0);
            this.lblPasswordIcon.Name = "lblPasswordIcon";
            this.lblPasswordIcon.Size = new System.Drawing.Size(45, 75);
            this.lblPasswordIcon.TabIndex = 0;
            this.lblPasswordIcon.Text = "🔒";
            this.lblPasswordIcon.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // pnlEmailField
            // 
            this.pnlEmailField.Controls.Add(this.txtEmail);
            this.pnlEmailField.Controls.Add(this.lblEmail);
            this.pnlEmailField.Controls.Add(this.lblEmailIcon);
            this.pnlEmailField.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlEmailField.Location = new System.Drawing.Point(25, 70);
            this.pnlEmailField.Name = "pnlEmailField";
            this.pnlEmailField.Padding = new System.Windows.Forms.Padding(0, 0, 0, 15);
            this.pnlEmailField.Size = new System.Drawing.Size(490, 70);
            this.pnlEmailField.TabIndex = 1;
            // 
            // txtEmail
            // 
            this.txtEmail.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtEmail.Dock = System.Windows.Forms.DockStyle.Top;
            this.txtEmail.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtEmail.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.txtEmail.Location = new System.Drawing.Point(45, 25);
            this.txtEmail.MaxLength = 100;
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(445, 30);
            this.txtEmail.TabIndex = 1;
            // 
            // lblEmail
            // 
            this.lblEmail.AutoSize = true;
            this.lblEmail.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblEmail.Font = new System.Drawing.Font("Segoe UI Semibold", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblEmail.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.lblEmail.Location = new System.Drawing.Point(45, 0);
            this.lblEmail.Name = "lblEmail";
            this.lblEmail.Padding = new System.Windows.Forms.Padding(0, 0, 0, 5);
            this.lblEmail.Size = new System.Drawing.Size(49, 26);
            this.lblEmail.TabIndex = 1;
            this.lblEmail.Text = "Email";
            // 
            // lblEmailIcon
            // 
            this.lblEmailIcon.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblEmailIcon.Font = new System.Drawing.Font("Segoe UI", 14F);
            this.lblEmailIcon.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(150)))), ((int)(((byte)(243)))));
            this.lblEmailIcon.Location = new System.Drawing.Point(0, 0);
            this.lblEmailIcon.Name = "lblEmailIcon";
            this.lblEmailIcon.Size = new System.Drawing.Size(45, 55);
            this.lblEmailIcon.TabIndex = 0;
            this.lblEmailIcon.Text = "📧";
            this.lblEmailIcon.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnlUsernameField
            // 
            this.pnlUsernameField.Controls.Add(this.txtUsername);
            this.pnlUsernameField.Controls.Add(this.lblUsername);
            this.pnlUsernameField.Controls.Add(this.lblUsernameIcon);
            this.pnlUsernameField.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlUsernameField.Location = new System.Drawing.Point(25, 20);
            this.pnlUsernameField.Name = "pnlUsernameField";
            this.pnlUsernameField.Padding = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.pnlUsernameField.Size = new System.Drawing.Size(490, 50);
            this.pnlUsernameField.TabIndex = 0;
            // 
            // txtUsername
            // 
            this.txtUsername.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtUsername.Dock = System.Windows.Forms.DockStyle.Top;
            this.txtUsername.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtUsername.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.txtUsername.Location = new System.Drawing.Point(45, 25);
            this.txtUsername.MaxLength = 50;
            this.txtUsername.Name = "txtUsername";
            this.txtUsername.Size = new System.Drawing.Size(445, 30);
            this.txtUsername.TabIndex = 0;
            // 
            // lblUsername
            // 
            this.lblUsername.AutoSize = true;
            this.lblUsername.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblUsername.Font = new System.Drawing.Font("Segoe UI Semibold", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblUsername.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.lblUsername.Location = new System.Drawing.Point(45, 0);
            this.lblUsername.Name = "lblUsername";
            this.lblUsername.Padding = new System.Windows.Forms.Padding(0, 0, 0, 5);
            this.lblUsername.Size = new System.Drawing.Size(113, 26);
            this.lblUsername.TabIndex = 1;
            this.lblUsername.Text = "Tên đăng nhập";
            // 
            // lblUsernameIcon
            // 
            this.lblUsernameIcon.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblUsernameIcon.Font = new System.Drawing.Font("Segoe UI", 14F);
            this.lblUsernameIcon.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(76)))), ((int)(((byte)(175)))), ((int)(((byte)(80)))));
            this.lblUsernameIcon.Location = new System.Drawing.Point(0, 0);
            this.lblUsernameIcon.Name = "lblUsernameIcon";
            this.lblUsernameIcon.Size = new System.Drawing.Size(45, 50);
            this.lblUsernameIcon.TabIndex = 0;
            this.lblUsernameIcon.Text = "👨";
            this.lblUsernameIcon.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnlHeader
            // 
            this.pnlHeader.BackColor = System.Drawing.Color.White;
            this.pnlHeader.Controls.Add(this.lblSubtitle);
            this.pnlHeader.Controls.Add(this.lblTitle);
            this.pnlHeader.Controls.Add(this.pnlHeaderIcon);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Padding = new System.Windows.Forms.Padding(30, 20, 30, 20);
            this.pnlHeader.Size = new System.Drawing.Size(600, 120);
            this.pnlHeader.TabIndex = 0;
            // 
            // lblSubtitle
            // 
            this.lblSubtitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblSubtitle.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblSubtitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(120)))), ((int)(((byte)(120)))));
            this.lblSubtitle.Location = new System.Drawing.Point(100, 60);
            this.lblSubtitle.Name = "lblSubtitle";
            this.lblSubtitle.Size = new System.Drawing.Size(470, 40);
            this.lblSubtitle.TabIndex = 2;
            this.lblSubtitle.Text = "Vui lòng điền đầy đủ thông tin bên dưới";
            this.lblSubtitle.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            // 
            // lblTitle
            // 
            this.lblTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.lblTitle.Location = new System.Drawing.Point(100, 20);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(470, 40);
            this.lblTitle.TabIndex = 1;
            this.lblTitle.Text = "Thêm người dùng mới";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pnlHeaderIcon
            // 
            this.pnlHeaderIcon.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(240)))), ((int)(((byte)(254)))));
            this.pnlHeaderIcon.Controls.Add(this.lblHeaderIcon);
            this.pnlHeaderIcon.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlHeaderIcon.Location = new System.Drawing.Point(30, 20);
            this.pnlHeaderIcon.Name = "pnlHeaderIcon";
            this.pnlHeaderIcon.Size = new System.Drawing.Size(70, 80);
            this.pnlHeaderIcon.TabIndex = 0;
            // 
            // lblHeaderIcon
            // 
            this.lblHeaderIcon.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblHeaderIcon.Font = new System.Drawing.Font("Segoe UI", 28F);
            this.lblHeaderIcon.Location = new System.Drawing.Point(0, 0);
            this.lblHeaderIcon.Name = "lblHeaderIcon";
            this.lblHeaderIcon.Size = new System.Drawing.Size(70, 80);
            this.lblHeaderIcon.TabIndex = 0;
            this.lblHeaderIcon.Text = "👥";
            this.lblHeaderIcon.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnlFooter
            // 
            this.pnlFooter.BackColor = System.Drawing.Color.White;
            this.pnlFooter.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlFooter.Controls.Add(this.btnCancel);
            this.pnlFooter.Controls.Add(this.btnSave);
            this.pnlFooter.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlFooter.Location = new System.Drawing.Point(0, 570);
            this.pnlFooter.Name = "pnlFooter";
            this.pnlFooter.Padding = new System.Windows.Forms.Padding(30, 15, 30, 15);
            this.pnlFooter.Size = new System.Drawing.Size(600, 80);
            this.pnlFooter.TabIndex = 2;
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.White;
            this.btnCancel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnCancel.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnCancel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.btnCancel.Location = new System.Drawing.Point(428, 15);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(140, 48);
            this.btnCancel.TabIndex = 6;
            this.btnCancel.Text = "✖ Hủy bỏ";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(135)))), ((int)(((byte)(84)))));
            this.btnSave.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSave.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnSave.FlatAppearance.BorderSize = 0;
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.Location = new System.Drawing.Point(30, 15);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(180, 48);
            this.btnSave.TabIndex = 5;
            this.btnSave.Text = "✓ Lưu thông tin";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // AddEditUserForm
            // 
            this.AcceptButton = this.btnSave;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(600, 650);
            this.Controls.Add(this.pnlMain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AddEditUserForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Quản lý người dùng";
            this.Load += new System.EventHandler(this.AddEditUserForm_Load);
            this.pnlMain.ResumeLayout(false);
            this.pnlContent.ResumeLayout(false);
            this.pnlFormFields.ResumeLayout(false);
            this.pnlStatusField.ResumeLayout(false);
            this.pnlStatusField.PerformLayout();
            this.pnlRoleField.ResumeLayout(false);
            this.pnlRoleField.PerformLayout();
            this.pnlPasswordField.ResumeLayout(false);
            this.pnlPasswordField.PerformLayout();
            this.pnlEmailField.ResumeLayout(false);
            this.pnlEmailField.PerformLayout();
            this.pnlUsernameField.ResumeLayout(false);
            this.pnlUsernameField.PerformLayout();
            this.pnlHeader.ResumeLayout(false);
            this.pnlHeaderIcon.ResumeLayout(false);
            this.pnlFooter.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlMain;
        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblSubtitle;
        private System.Windows.Forms.Panel pnlHeaderIcon;
        private System.Windows.Forms.Label lblHeaderIcon;
        private System.Windows.Forms.Panel pnlContent;
        private System.Windows.Forms.Panel pnlFormFields;
        private System.Windows.Forms.Panel pnlUsernameField;
        private System.Windows.Forms.Label lblUsernameIcon;
        private System.Windows.Forms.Label lblUsername;
        private System.Windows.Forms.TextBox txtUsername;
        private System.Windows.Forms.Panel pnlEmailField;
        private System.Windows.Forms.Label lblEmailIcon;
        private System.Windows.Forms.Label lblEmail;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.Panel pnlPasswordField;
        private System.Windows.Forms.Label lblPasswordIcon;
        private System.Windows.Forms.Label lblPassword;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Button btnTogglePassword;
        private System.Windows.Forms.Label lblPasswordHint;
        private System.Windows.Forms.Panel pnlRoleField;
        private System.Windows.Forms.Label lblRoleIcon;
        private System.Windows.Forms.Label lblRole;
        private System.Windows.Forms.ComboBox cboRole;
        private System.Windows.Forms.Panel pnlStatusField;
        private System.Windows.Forms.Label lblStatusIcon;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.ComboBox cboStatus;
        private System.Windows.Forms.Panel pnlFooter;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
    }
}