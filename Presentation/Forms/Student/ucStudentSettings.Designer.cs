namespace CodeForge_Desktop.Presentation.Forms.Student
{
    partial class ucStudentSettings
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

        #region Component Designer generated code

        private void InitializeComponent()
        {
            this.pnlMain = new System.Windows.Forms.Panel();
            this.splitContainerSettings = new System.Windows.Forms.SplitContainer();
            this.pnlMenu = new System.Windows.Forms.Panel();
            this.btnTabNotifications = new System.Windows.Forms.Button();
            this.btnTabEditor = new System.Windows.Forms.Button();
            this.btnTabGeneral = new System.Windows.Forms.Button();
            this.lblMenuTitle = new System.Windows.Forms.Label();
            this.pnlContent = new System.Windows.Forms.Panel();
            this.pnlGeneral = new System.Windows.Forms.Panel();
            this.chkConfirmSubmit = new System.Windows.Forms.CheckBox();
            this.chkAutoSave = new System.Windows.Forms.CheckBox();
            this.cboTimezone = new System.Windows.Forms.ComboBox();
            this.lblTimezone = new System.Windows.Forms.Label();
            this.cboLanguage = new System.Windows.Forms.ComboBox();
            this.lblLanguage = new System.Windows.Forms.Label();
            this.lblGeneralHeader = new System.Windows.Forms.Label();
            this.pnlEditor = new System.Windows.Forms.Panel();
            this.chkWordWrap = new System.Windows.Forms.CheckBox();
            this.chkAutoCloseBrackets = new System.Windows.Forms.CheckBox();
            this.chkLineNumbers = new System.Windows.Forms.CheckBox();
            this.cboTabSize = new System.Windows.Forms.ComboBox();
            this.lblTabSize = new System.Windows.Forms.Label();
            this.cboFontSize = new System.Windows.Forms.ComboBox();
            this.lblFontSize = new System.Windows.Forms.Label();
            this.cboTheme = new System.Windows.Forms.ComboBox();
            this.lblTheme = new System.Windows.Forms.Label();
            this.lblEditorHeader = new System.Windows.Forms.Label();
            this.pnlNotifications = new System.Windows.Forms.Panel();
            this.chkInAppNoti = new System.Windows.Forms.CheckBox();
            this.chkNotiEmailFeedback = new System.Windows.Forms.CheckBox();
            this.chkNotiEmailDeadline = new System.Windows.Forms.CheckBox();
            this.chkNotiEmailNewProblem = new System.Windows.Forms.CheckBox();
            this.lblNotiHeader = new System.Windows.Forms.Label();
            this.pnlFooter = new System.Windows.Forms.Panel();
            this.btnReset = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.lblTitle = new System.Windows.Forms.Label();
            this.pnlMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerSettings)).BeginInit();
            this.splitContainerSettings.Panel1.SuspendLayout();
            this.splitContainerSettings.Panel2.SuspendLayout();
            this.splitContainerSettings.SuspendLayout();
            this.pnlMenu.SuspendLayout();
            this.pnlContent.SuspendLayout();
            this.pnlGeneral.SuspendLayout();
            this.pnlEditor.SuspendLayout();
            this.pnlNotifications.SuspendLayout();
            this.pnlFooter.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlMain
            // 
            this.pnlMain.Controls.Add(this.splitContainerSettings);
            this.pnlMain.Controls.Add(this.lblTitle);
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Location = new System.Drawing.Point(0, 0);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Padding = new System.Windows.Forms.Padding(30);
            this.pnlMain.Size = new System.Drawing.Size(1200, 800);
            this.pnlMain.TabIndex = 0;
            // 
            // splitContainerSettings
            // 
            this.splitContainerSettings.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerSettings.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainerSettings.IsSplitterFixed = true;
            this.splitContainerSettings.Location = new System.Drawing.Point(30, 96);
            this.splitContainerSettings.Name = "splitContainerSettings";
            // 
            // splitContainerSettings.Panel1
            // 
            this.splitContainerSettings.Panel1.Controls.Add(this.pnlMenu);
            // 
            // splitContainerSettings.Panel2
            // 
            this.splitContainerSettings.Panel2.Controls.Add(this.pnlContent);
            this.splitContainerSettings.Panel2.Controls.Add(this.pnlFooter);
            this.splitContainerSettings.Size = new System.Drawing.Size(1140, 674);
            this.splitContainerSettings.SplitterDistance = 250;
            this.splitContainerSettings.SplitterWidth = 20;
            this.splitContainerSettings.TabIndex = 1;
            // 
            // pnlMenu
            // 
            this.pnlMenu.BackColor = System.Drawing.Color.White;
            this.pnlMenu.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlMenu.Controls.Add(this.btnTabNotifications);
            this.pnlMenu.Controls.Add(this.btnTabEditor);
            this.pnlMenu.Controls.Add(this.btnTabGeneral);
            this.pnlMenu.Controls.Add(this.lblMenuTitle);
            this.pnlMenu.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMenu.Location = new System.Drawing.Point(0, 0);
            this.pnlMenu.Name = "pnlMenu";
            this.pnlMenu.Padding = new System.Windows.Forms.Padding(10);
            this.pnlMenu.Size = new System.Drawing.Size(250, 674);
            this.pnlMenu.TabIndex = 0;
            // 
            // btnTabNotifications
            // 
            this.btnTabNotifications.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnTabNotifications.FlatAppearance.BorderSize = 0;
            this.btnTabNotifications.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTabNotifications.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnTabNotifications.Location = new System.Drawing.Point(10, 150);
            this.btnTabNotifications.Name = "btnTabNotifications";
            this.btnTabNotifications.Size = new System.Drawing.Size(228, 50);
            this.btnTabNotifications.TabIndex = 3;
            this.btnTabNotifications.Text = " Thông báo";
            this.btnTabNotifications.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnTabNotifications.UseVisualStyleBackColor = true;
            // 
            // btnTabEditor
            // 
            this.btnTabEditor.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnTabEditor.FlatAppearance.BorderSize = 0;
            this.btnTabEditor.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTabEditor.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnTabEditor.Location = new System.Drawing.Point(10, 100);
            this.btnTabEditor.Name = "btnTabEditor";
            this.btnTabEditor.Size = new System.Drawing.Size(228, 50);
            this.btnTabEditor.TabIndex = 2;
            this.btnTabEditor.Text = " Trình soạn thảo";
            this.btnTabEditor.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnTabEditor.UseVisualStyleBackColor = true;
            // 
            // btnTabGeneral
            // 
            this.btnTabGeneral.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnTabGeneral.FlatAppearance.BorderSize = 0;
            this.btnTabGeneral.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTabGeneral.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnTabGeneral.Location = new System.Drawing.Point(10, 50);
            this.btnTabGeneral.Name = "btnTabGeneral";
            this.btnTabGeneral.Size = new System.Drawing.Size(228, 50);
            this.btnTabGeneral.TabIndex = 1;
            this.btnTabGeneral.Text = " Chung";
            this.btnTabGeneral.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnTabGeneral.UseVisualStyleBackColor = true;
            // 
            // lblMenuTitle
            // 
            this.lblMenuTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblMenuTitle.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblMenuTitle.ForeColor = System.Drawing.Color.Gray;
            this.lblMenuTitle.Location = new System.Drawing.Point(10, 10);
            this.lblMenuTitle.Name = "lblMenuTitle";
            this.lblMenuTitle.Size = new System.Drawing.Size(228, 40);
            this.lblMenuTitle.TabIndex = 0;
            this.lblMenuTitle.Text = "DANH MỤC";
            this.lblMenuTitle.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // pnlContent
            // 
            this.pnlContent.BackColor = System.Drawing.Color.White;
            this.pnlContent.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlContent.Controls.Add(this.pnlGeneral);
            this.pnlContent.Controls.Add(this.pnlEditor);
            this.pnlContent.Controls.Add(this.pnlNotifications);
            this.pnlContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlContent.Location = new System.Drawing.Point(0, 0);
            this.pnlContent.Name = "pnlContent";
            this.pnlContent.Padding = new System.Windows.Forms.Padding(30);
            this.pnlContent.Size = new System.Drawing.Size(870, 594);
            this.pnlContent.TabIndex = 1;
            // 
            // pnlGeneral
            // 
            this.pnlGeneral.Controls.Add(this.chkConfirmSubmit);
            this.pnlGeneral.Controls.Add(this.chkAutoSave);
            this.pnlGeneral.Controls.Add(this.cboTimezone);
            this.pnlGeneral.Controls.Add(this.lblTimezone);
            this.pnlGeneral.Controls.Add(this.cboLanguage);
            this.pnlGeneral.Controls.Add(this.lblLanguage);
            this.pnlGeneral.Controls.Add(this.lblGeneralHeader);
            this.pnlGeneral.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlGeneral.Location = new System.Drawing.Point(30, 30);
            this.pnlGeneral.Name = "pnlGeneral";
            this.pnlGeneral.Size = new System.Drawing.Size(808, 532);
            this.pnlGeneral.TabIndex = 0;
            // 
            // chkConfirmSubmit
            // 
            this.chkConfirmSubmit.AutoSize = true;
            this.chkConfirmSubmit.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.chkConfirmSubmit.Location = new System.Drawing.Point(5, 270);
            this.chkConfirmSubmit.Name = "chkConfirmSubmit";
            this.chkConfirmSubmit.Size = new System.Drawing.Size(256, 27);
            this.chkConfirmSubmit.TabIndex = 6;
            this.chkConfirmSubmit.Text = "Hiển thị xác nhận khi nộp bài";
            this.chkConfirmSubmit.UseVisualStyleBackColor = true;
            // 
            // chkAutoSave
            // 
            this.chkAutoSave.AutoSize = true;
            this.chkAutoSave.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.chkAutoSave.Location = new System.Drawing.Point(5, 230);
            this.chkAutoSave.Name = "chkAutoSave";
            this.chkAutoSave.Size = new System.Drawing.Size(272, 27);
            this.chkAutoSave.TabIndex = 5;
            this.chkAutoSave.Text = "Tự động lưu code khi chỉnh sửa";
            this.chkAutoSave.UseVisualStyleBackColor = true;
            // 
            // cboTimezone
            // 
            this.cboTimezone.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboTimezone.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cboTimezone.FormattingEnabled = true;
            this.cboTimezone.Location = new System.Drawing.Point(5, 170);
            this.cboTimezone.Name = "cboTimezone";
            this.cboTimezone.Size = new System.Drawing.Size(400, 31);
            this.cboTimezone.TabIndex = 4;
            // 
            // lblTimezone
            // 
            this.lblTimezone.AutoSize = true;
            this.lblTimezone.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblTimezone.Location = new System.Drawing.Point(0, 140);
            this.lblTimezone.Name = "lblTimezone";
            this.lblTimezone.Size = new System.Drawing.Size(68, 23);
            this.lblTimezone.TabIndex = 3;
            this.lblTimezone.Text = "Múi giờ";
            // 
            // cboLanguage
            // 
            this.cboLanguage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboLanguage.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cboLanguage.FormattingEnabled = true;
            this.cboLanguage.Location = new System.Drawing.Point(5, 90);
            this.cboLanguage.Name = "cboLanguage";
            this.cboLanguage.Size = new System.Drawing.Size(400, 31);
            this.cboLanguage.TabIndex = 2;
            // 
            // lblLanguage
            // 
            this.lblLanguage.AutoSize = true;
            this.lblLanguage.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblLanguage.Location = new System.Drawing.Point(0, 60);
            this.lblLanguage.Name = "lblLanguage";
            this.lblLanguage.Size = new System.Drawing.Size(151, 23);
            this.lblLanguage.TabIndex = 1;
            this.lblLanguage.Text = "Ngôn ngữ hiển thị";
            // 
            // lblGeneralHeader
            // 
            this.lblGeneralHeader.AutoSize = true;
            this.lblGeneralHeader.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblGeneralHeader.Location = new System.Drawing.Point(0, 0);
            this.lblGeneralHeader.Name = "lblGeneralHeader";
            this.lblGeneralHeader.Size = new System.Drawing.Size(192, 37);
            this.lblGeneralHeader.TabIndex = 0;
            this.lblGeneralHeader.Text = "Cài đặt chung";
            // 
            // pnlEditor
            // 
            this.pnlEditor.Controls.Add(this.chkWordWrap);
            this.pnlEditor.Controls.Add(this.chkAutoCloseBrackets);
            this.pnlEditor.Controls.Add(this.chkLineNumbers);
            this.pnlEditor.Controls.Add(this.cboTabSize);
            this.pnlEditor.Controls.Add(this.lblTabSize);
            this.pnlEditor.Controls.Add(this.cboFontSize);
            this.pnlEditor.Controls.Add(this.lblFontSize);
            this.pnlEditor.Controls.Add(this.cboTheme);
            this.pnlEditor.Controls.Add(this.lblTheme);
            this.pnlEditor.Controls.Add(this.lblEditorHeader);
            this.pnlEditor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlEditor.Location = new System.Drawing.Point(30, 30);
            this.pnlEditor.Name = "pnlEditor";
            this.pnlEditor.Size = new System.Drawing.Size(808, 532);
            this.pnlEditor.TabIndex = 1;
            this.pnlEditor.Visible = false;
            // 
            // chkWordWrap
            // 
            this.chkWordWrap.AutoSize = true;
            this.chkWordWrap.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.chkWordWrap.Location = new System.Drawing.Point(5, 390);
            this.chkWordWrap.Name = "chkWordWrap";
            this.chkWordWrap.Size = new System.Drawing.Size(296, 27);
            this.chkWordWrap.TabIndex = 0;
            this.chkWordWrap.Text = "Word Wrap (Tự động xuống dòng)";
            // 
            // chkAutoCloseBrackets
            // 
            this.chkAutoCloseBrackets.AutoSize = true;
            this.chkAutoCloseBrackets.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.chkAutoCloseBrackets.Location = new System.Drawing.Point(5, 350);
            this.chkAutoCloseBrackets.Name = "chkAutoCloseBrackets";
            this.chkAutoCloseBrackets.Size = new System.Drawing.Size(238, 27);
            this.chkAutoCloseBrackets.TabIndex = 1;
            this.chkAutoCloseBrackets.Text = "Tự động đóng ngoặc () [] {}";
            // 
            // chkLineNumbers
            // 
            this.chkLineNumbers.AutoSize = true;
            this.chkLineNumbers.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.chkLineNumbers.Location = new System.Drawing.Point(5, 310);
            this.chkLineNumbers.Name = "chkLineNumbers";
            this.chkLineNumbers.Size = new System.Drawing.Size(280, 27);
            this.chkLineNumbers.TabIndex = 2;
            this.chkLineNumbers.Text = "Hiển thị số dòng (Line Numbers)";
            // 
            // cboTabSize
            // 
            this.cboTabSize.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboTabSize.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cboTabSize.Location = new System.Drawing.Point(5, 250);
            this.cboTabSize.Name = "cboTabSize";
            this.cboTabSize.Size = new System.Drawing.Size(400, 31);
            this.cboTabSize.TabIndex = 3;
            // 
            // lblTabSize
            // 
            this.lblTabSize.AutoSize = true;
            this.lblTabSize.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblTabSize.Location = new System.Drawing.Point(0, 220);
            this.lblTabSize.Name = "lblTabSize";
            this.lblTabSize.Size = new System.Drawing.Size(301, 23);
            this.lblTabSize.TabIndex = 4;
            this.lblTabSize.Text = "Tab Size (Khoảng cách thụt đầu dòng)";
            // 
            // cboFontSize
            // 
            this.cboFontSize.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboFontSize.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cboFontSize.Location = new System.Drawing.Point(5, 170);
            this.cboFontSize.Name = "cboFontSize";
            this.cboFontSize.Size = new System.Drawing.Size(400, 31);
            this.cboFontSize.TabIndex = 5;
            // 
            // lblFontSize
            // 
            this.lblFontSize.AutoSize = true;
            this.lblFontSize.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblFontSize.Location = new System.Drawing.Point(0, 140);
            this.lblFontSize.Name = "lblFontSize";
            this.lblFontSize.Size = new System.Drawing.Size(79, 23);
            this.lblFontSize.TabIndex = 6;
            this.lblFontSize.Text = "Font Size";
            // 
            // cboTheme
            // 
            this.cboTheme.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboTheme.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cboTheme.Location = new System.Drawing.Point(5, 90);
            this.cboTheme.Name = "cboTheme";
            this.cboTheme.Size = new System.Drawing.Size(400, 31);
            this.cboTheme.TabIndex = 7;
            // 
            // lblTheme
            // 
            this.lblTheme.AutoSize = true;
            this.lblTheme.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblTheme.Location = new System.Drawing.Point(0, 60);
            this.lblTheme.Name = "lblTheme";
            this.lblTheme.Size = new System.Drawing.Size(192, 23);
            this.lblTheme.TabIndex = 8;
            this.lblTheme.Text = "Theme (Giao diện code)";
            // 
            // lblEditorHeader
            // 
            this.lblEditorHeader.AutoSize = true;
            this.lblEditorHeader.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblEditorHeader.Location = new System.Drawing.Point(0, 0);
            this.lblEditorHeader.Name = "lblEditorHeader";
            this.lblEditorHeader.Size = new System.Drawing.Size(192, 37);
            this.lblEditorHeader.TabIndex = 9;
            this.lblEditorHeader.Text = "Cài đặt Editor";
            // 
            // pnlNotifications
            // 
            this.pnlNotifications.Controls.Add(this.chkInAppNoti);
            this.pnlNotifications.Controls.Add(this.chkNotiEmailFeedback);
            this.pnlNotifications.Controls.Add(this.chkNotiEmailDeadline);
            this.pnlNotifications.Controls.Add(this.chkNotiEmailNewProblem);
            this.pnlNotifications.Controls.Add(this.lblNotiHeader);
            this.pnlNotifications.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlNotifications.Location = new System.Drawing.Point(30, 30);
            this.pnlNotifications.Name = "pnlNotifications";
            this.pnlNotifications.Size = new System.Drawing.Size(808, 532);
            this.pnlNotifications.TabIndex = 2;
            this.pnlNotifications.Visible = false;
            // 
            // chkInAppNoti
            // 
            this.chkInAppNoti.AutoSize = true;
            this.chkInAppNoti.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.chkInAppNoti.Location = new System.Drawing.Point(5, 180);
            this.chkInAppNoti.Name = "chkInAppNoti";
            this.chkInAppNoti.Size = new System.Drawing.Size(308, 27);
            this.chkInAppNoti.TabIndex = 0;
            this.chkInAppNoti.Text = "Thông báo trong ứng dụng (In-app)";
            // 
            // chkNotiEmailFeedback
            // 
            this.chkNotiEmailFeedback.AutoSize = true;
            this.chkNotiEmailFeedback.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.chkNotiEmailFeedback.Location = new System.Drawing.Point(5, 140);
            this.chkNotiEmailFeedback.Name = "chkNotiEmailFeedback";
            this.chkNotiEmailFeedback.Size = new System.Drawing.Size(301, 27);
            this.chkNotiEmailFeedback.TabIndex = 1;
            this.chkNotiEmailFeedback.Text = "Email khi có phản hồi từ giảng viên";
            // 
            // chkNotiEmailDeadline
            // 
            this.chkNotiEmailDeadline.AutoSize = true;
            this.chkNotiEmailDeadline.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.chkNotiEmailDeadline.Location = new System.Drawing.Point(5, 100);
            this.chkNotiEmailDeadline.Name = "chkNotiEmailDeadline";
            this.chkNotiEmailDeadline.Size = new System.Drawing.Size(222, 27);
            this.chkNotiEmailDeadline.TabIndex = 2;
            this.chkNotiEmailDeadline.Text = "Email nhắc nhở Deadline";
            // 
            // chkNotiEmailNewProblem
            // 
            this.chkNotiEmailNewProblem.AutoSize = true;
            this.chkNotiEmailNewProblem.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.chkNotiEmailNewProblem.Location = new System.Drawing.Point(5, 60);
            this.chkNotiEmailNewProblem.Name = "chkNotiEmailNewProblem";
            this.chkNotiEmailNewProblem.Size = new System.Drawing.Size(215, 27);
            this.chkNotiEmailNewProblem.TabIndex = 3;
            this.chkNotiEmailNewProblem.Text = "Email khi có bài tập mới";
            // 
            // lblNotiHeader
            // 
            this.lblNotiHeader.AutoSize = true;
            this.lblNotiHeader.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblNotiHeader.Location = new System.Drawing.Point(0, 0);
            this.lblNotiHeader.Name = "lblNotiHeader";
            this.lblNotiHeader.Size = new System.Drawing.Size(252, 37);
            this.lblNotiHeader.TabIndex = 4;
            this.lblNotiHeader.Text = "Cài đặt Thông báo";
            // 
            // pnlFooter
            // 
            this.pnlFooter.BackColor = System.Drawing.Color.White;
            this.pnlFooter.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlFooter.Controls.Add(this.btnReset);
            this.pnlFooter.Controls.Add(this.btnSave);
            this.pnlFooter.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlFooter.Location = new System.Drawing.Point(0, 594);
            this.pnlFooter.Name = "pnlFooter";
            this.pnlFooter.Padding = new System.Windows.Forms.Padding(20);
            this.pnlFooter.Size = new System.Drawing.Size(870, 80);
            this.pnlFooter.TabIndex = 2;
            // 
            // btnReset
            // 
            this.btnReset.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btnReset.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnReset.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.btnReset.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReset.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnReset.Location = new System.Drawing.Point(190, 20);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(150, 40);
            this.btnReset.TabIndex = 1;
            this.btnReset.Text = "Reset mặc định";
            this.btnReset.UseVisualStyleBackColor = false;
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(120)))), ((int)(((byte)(215)))));
            this.btnSave.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSave.FlatAppearance.BorderSize = 0;
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.Location = new System.Drawing.Point(20, 20);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(150, 40);
            this.btnSave.TabIndex = 0;
            this.btnSave.Text = "Lưu cài đặt";
            this.btnSave.UseVisualStyleBackColor = false;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(33)))), ((int)(((byte)(33)))));
            this.lblTitle.Location = new System.Drawing.Point(30, 30);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Padding = new System.Windows.Forms.Padding(0, 0, 0, 20);
            this.lblTitle.Size = new System.Drawing.Size(131, 66);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Cài đặt";
            // 
            // ucStudentSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(250)))), ((int)(((byte)(250)))));
            this.Controls.Add(this.pnlMain);
            this.Name = "ucStudentSettings";
            this.Size = new System.Drawing.Size(1200, 800);
            this.pnlMain.ResumeLayout(false);
            this.pnlMain.PerformLayout();
            this.splitContainerSettings.Panel1.ResumeLayout(false);
            this.splitContainerSettings.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerSettings)).EndInit();
            this.splitContainerSettings.ResumeLayout(false);
            this.pnlMenu.ResumeLayout(false);
            this.pnlContent.ResumeLayout(false);
            this.pnlGeneral.ResumeLayout(false);
            this.pnlGeneral.PerformLayout();
            this.pnlEditor.ResumeLayout(false);
            this.pnlEditor.PerformLayout();
            this.pnlNotifications.ResumeLayout(false);
            this.pnlNotifications.PerformLayout();
            this.pnlFooter.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlMain;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.SplitContainer splitContainerSettings;
        private System.Windows.Forms.Panel pnlMenu;
        private System.Windows.Forms.Button btnTabGeneral;
        private System.Windows.Forms.Button btnTabEditor;
        private System.Windows.Forms.Button btnTabNotifications;
        private System.Windows.Forms.Label lblMenuTitle;
        private System.Windows.Forms.Panel pnlContent;

        // General
        private System.Windows.Forms.Panel pnlGeneral;
        private System.Windows.Forms.Label lblGeneralHeader;
        private System.Windows.Forms.Label lblLanguage;
        private System.Windows.Forms.ComboBox cboLanguage;
        private System.Windows.Forms.Label lblTimezone;
        private System.Windows.Forms.ComboBox cboTimezone;
        private System.Windows.Forms.CheckBox chkAutoSave;
        private System.Windows.Forms.CheckBox chkConfirmSubmit;

        // Editor
        private System.Windows.Forms.Panel pnlEditor;
        private System.Windows.Forms.Label lblEditorHeader;
        private System.Windows.Forms.Label lblTheme;
        private System.Windows.Forms.ComboBox cboTheme;
        private System.Windows.Forms.Label lblFontSize;
        private System.Windows.Forms.ComboBox cboFontSize;
        private System.Windows.Forms.Label lblTabSize;
        private System.Windows.Forms.ComboBox cboTabSize;
        private System.Windows.Forms.CheckBox chkLineNumbers;
        private System.Windows.Forms.CheckBox chkAutoCloseBrackets;
        private System.Windows.Forms.CheckBox chkWordWrap;

        // Notifications
        private System.Windows.Forms.Panel pnlNotifications;
        private System.Windows.Forms.Label lblNotiHeader;
        private System.Windows.Forms.CheckBox chkNotiEmailNewProblem;
        private System.Windows.Forms.CheckBox chkNotiEmailDeadline;
        private System.Windows.Forms.CheckBox chkNotiEmailFeedback;
        private System.Windows.Forms.CheckBox chkInAppNoti;

        // Footer
        private System.Windows.Forms.Panel pnlFooter;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnReset;
    }
}