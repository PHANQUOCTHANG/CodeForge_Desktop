namespace CodeForge_Desktop.Presentation.Forms.Admin
{
    partial class AddEditProblemForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.pnlMain = new System.Windows.Forms.Panel();
            this.pnlContent = new System.Windows.Forms.Panel();
            this.tabProblemInfo = new System.Windows.Forms.TabControl();
            this.tabGeneral = new System.Windows.Forms.TabPage();
            this.pnlGeneral = new System.Windows.Forms.Panel();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.lblDesc = new System.Windows.Forms.Label();
            this.pnlDifficultyCategory = new System.Windows.Forms.Panel();
            this.cboDifficulty = new System.Windows.Forms.ComboBox();
            this.lblDifficulty = new System.Windows.Forms.Label();
            this.txtTitle = new System.Windows.Forms.TextBox();
            this.lblTitleLabel = new System.Windows.Forms.Label();
            this.tabCodeSettings = new System.Windows.Forms.TabPage();
            this.pnlCodeSettings = new System.Windows.Forms.Panel();
            this.txtNotes = new System.Windows.Forms.TextBox();
            this.lblNotes = new System.Windows.Forms.Label();
            this.txtConstraints = new System.Windows.Forms.TextBox();
            this.lblConstraints = new System.Windows.Forms.Label();
            this.txtTags = new System.Windows.Forms.TextBox();
            this.lblTags = new System.Windows.Forms.Label();
            this.pnlLimits = new System.Windows.Forms.Panel();
            this.numMemoryLimit = new System.Windows.Forms.NumericUpDown();
            this.lblMemoryLimit = new System.Windows.Forms.Label();
            this.numTimeLimit = new System.Windows.Forms.NumericUpDown();
            this.lblTimeLimit = new System.Windows.Forms.Label();
            this.cboReturnType = new System.Windows.Forms.ComboBox();
            this.lblReturnType = new System.Windows.Forms.Label();
            this.txtParameters = new System.Windows.Forms.TextBox();
            this.lblParameters = new System.Windows.Forms.Label();
            this.txtFunctionName = new System.Windows.Forms.TextBox();
            this.lblFunctionName = new System.Windows.Forms.Label();
            this.tabTestCases = new System.Windows.Forms.TabPage();
            this.pnlTestCases = new System.Windows.Forms.Panel();
            this.dgvTestCases = new System.Windows.Forms.DataGridView();
            this.pnlTestCaseInput = new System.Windows.Forms.Panel();
            this.pnlTestCaseButtons = new System.Windows.Forms.Panel();
            this.btnDeleteTestCase = new System.Windows.Forms.Button();
            this.btnAddTestCase = new System.Windows.Forms.Button();
            this.pnlInputOutput = new System.Windows.Forms.Panel();
            this.pnlOutput = new System.Windows.Forms.Panel();
            this.txtTestCaseOutput = new System.Windows.Forms.TextBox();
            this.lblTestCaseOutput = new System.Windows.Forms.Label();
            this.pnlInput = new System.Windows.Forms.Panel();
            this.txtTestCaseInput = new System.Windows.Forms.TextBox();
            this.lblTestCaseInput = new System.Windows.Forms.Label();
            this.pnlExplain = new System.Windows.Forms.Panel();
            this.chkIsHidden = new System.Windows.Forms.CheckBox();
            this.txtTestCaseExplain = new System.Windows.Forms.TextBox();
            this.lblTestCaseExplain = new System.Windows.Forms.Label();
            this.pnlFooter = new System.Windows.Forms.Panel();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.lblSubtitle = new System.Windows.Forms.Label();
            this.lblTitle = new System.Windows.Forms.Label();
            this.pnlMain.SuspendLayout();
            this.pnlContent.SuspendLayout();
            this.tabProblemInfo.SuspendLayout();
            this.tabGeneral.SuspendLayout();
            this.pnlGeneral.SuspendLayout();
            this.pnlDifficultyCategory.SuspendLayout();
            this.tabCodeSettings.SuspendLayout();
            this.pnlCodeSettings.SuspendLayout();
            this.pnlLimits.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numMemoryLimit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numTimeLimit)).BeginInit();
            this.tabTestCases.SuspendLayout();
            this.pnlTestCases.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTestCases)).BeginInit();
            this.pnlTestCaseInput.SuspendLayout();
            this.pnlTestCaseButtons.SuspendLayout();
            this.pnlInputOutput.SuspendLayout();
            this.pnlOutput.SuspendLayout();
            this.pnlInput.SuspendLayout();
            this.pnlExplain.SuspendLayout();
            this.pnlFooter.SuspendLayout();
            this.pnlHeader.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlMain
            // 
            this.pnlMain.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(247)))), ((int)(((byte)(250)))));
            this.pnlMain.Controls.Add(this.pnlContent);
            this.pnlMain.Controls.Add(this.pnlFooter);
            this.pnlMain.Controls.Add(this.pnlHeader);
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Location = new System.Drawing.Point(0, 0);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(1000, 700);
            this.pnlMain.TabIndex = 0;
            // 
            // pnlContent
            // 
            this.pnlContent.BackColor = System.Drawing.Color.White;
            this.pnlContent.Controls.Add(this.tabProblemInfo);
            this.pnlContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlContent.Location = new System.Drawing.Point(0, 100);
            this.pnlContent.Name = "pnlContent";
            this.pnlContent.Padding = new System.Windows.Forms.Padding(20);
            this.pnlContent.Size = new System.Drawing.Size(1000, 530);
            this.pnlContent.TabIndex = 2;
            // 
            // tabProblemInfo
            // 
            this.tabProblemInfo.Controls.Add(this.tabGeneral);
            this.tabProblemInfo.Controls.Add(this.tabCodeSettings);
            this.tabProblemInfo.Controls.Add(this.tabTestCases);
            this.tabProblemInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabProblemInfo.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.tabProblemInfo.Location = new System.Drawing.Point(20, 20);
            this.tabProblemInfo.Name = "tabProblemInfo";
            this.tabProblemInfo.SelectedIndex = 0;
            this.tabProblemInfo.Size = new System.Drawing.Size(960, 490);
            this.tabProblemInfo.TabIndex = 0;
            // 
            // tabGeneral
            // 
            this.tabGeneral.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.tabGeneral.Controls.Add(this.pnlGeneral);
            this.tabGeneral.Location = new System.Drawing.Point(4, 26);
            this.tabGeneral.Name = "tabGeneral";
            this.tabGeneral.Padding = new System.Windows.Forms.Padding(20);
            this.tabGeneral.Size = new System.Drawing.Size(952, 460);
            this.tabGeneral.TabIndex = 0;
            this.tabGeneral.Text = "📋 Thông tin chung";
            // 
            // pnlGeneral
            // 
            this.pnlGeneral.BackColor = System.Drawing.Color.White;
            this.pnlGeneral.Controls.Add(this.txtDescription);
            this.pnlGeneral.Controls.Add(this.lblDesc);
            this.pnlGeneral.Controls.Add(this.pnlDifficultyCategory);
            this.pnlGeneral.Controls.Add(this.txtTitle);
            this.pnlGeneral.Controls.Add(this.lblTitleLabel);
            this.pnlGeneral.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlGeneral.Location = new System.Drawing.Point(20, 20);
            this.pnlGeneral.Name = "pnlGeneral";
            this.pnlGeneral.Padding = new System.Windows.Forms.Padding(25);
            this.pnlGeneral.Size = new System.Drawing.Size(912, 420);
            this.pnlGeneral.TabIndex = 0;
            // 
            // txtDescription
            // 
            this.txtDescription.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtDescription.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtDescription.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.txtDescription.Location = new System.Drawing.Point(25, 168);
            this.txtDescription.Multiline = true;
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtDescription.Size = new System.Drawing.Size(862, 227);
            this.txtDescription.TabIndex = 4;
            // 
            // lblDesc
            // 
            this.lblDesc.AutoSize = true;
            this.lblDesc.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblDesc.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblDesc.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.lblDesc.Location = new System.Drawing.Point(25, 144);
            this.lblDesc.Name = "lblDesc";
            this.lblDesc.Padding = new System.Windows.Forms.Padding(0, 0, 0, 5);
            this.lblDesc.Size = new System.Drawing.Size(114, 24);
            this.lblDesc.TabIndex = 3;
            this.lblDesc.Text = "Mô tả đề bài (*)";
            // 
            // pnlDifficultyCategory
            // 
            this.pnlDifficultyCategory.Controls.Add(this.cboDifficulty);
            this.pnlDifficultyCategory.Controls.Add(this.lblDifficulty);
            this.pnlDifficultyCategory.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlDifficultyCategory.Location = new System.Drawing.Point(25, 74);
            this.pnlDifficultyCategory.Name = "pnlDifficultyCategory";
            this.pnlDifficultyCategory.Padding = new System.Windows.Forms.Padding(0, 10, 0, 10);
            this.pnlDifficultyCategory.Size = new System.Drawing.Size(862, 70);
            this.pnlDifficultyCategory.TabIndex = 2;
            // 
            // cboDifficulty
            // 
            this.cboDifficulty.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboDifficulty.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cboDifficulty.FormattingEnabled = true;
            this.cboDifficulty.Items.AddRange(new object[] {
            "Dễ",
            "Trung bình",
            "Khó"});
            this.cboDifficulty.Location = new System.Drawing.Point(0, 30);
            this.cboDifficulty.Name = "cboDifficulty";
            this.cboDifficulty.Size = new System.Drawing.Size(250, 25);
            this.cboDifficulty.TabIndex = 1;
            // 
            // lblDifficulty
            // 
            this.lblDifficulty.AutoSize = true;
            this.lblDifficulty.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblDifficulty.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.lblDifficulty.Location = new System.Drawing.Point(0, 10);
            this.lblDifficulty.Name = "lblDifficulty";
            this.lblDifficulty.Size = new System.Drawing.Size(77, 19);
            this.lblDifficulty.TabIndex = 0;
            this.lblDifficulty.Text = "Độ khó (*)";
            // 
            // txtTitle
            // 
            this.txtTitle.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.txtTitle.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtTitle.Location = new System.Drawing.Point(25, 49);
            this.txtTitle.Name = "txtTitle";
            this.txtTitle.Size = new System.Drawing.Size(862, 25);
            this.txtTitle.TabIndex = 1;
            // 
            // lblTitleLabel
            // 
            this.lblTitleLabel.AutoSize = true;
            this.lblTitleLabel.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTitleLabel.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblTitleLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.lblTitleLabel.Location = new System.Drawing.Point(25, 25);
            this.lblTitleLabel.Name = "lblTitleLabel";
            this.lblTitleLabel.Padding = new System.Windows.Forms.Padding(0, 0, 0, 5);
            this.lblTitleLabel.Size = new System.Drawing.Size(103, 24);
            this.lblTitleLabel.TabIndex = 0;
            this.lblTitleLabel.Text = "Tên bài tập (*)";
            // 
            // tabCodeSettings
            // 
            this.tabCodeSettings.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.tabCodeSettings.Controls.Add(this.pnlCodeSettings);
            this.tabCodeSettings.Location = new System.Drawing.Point(4, 26);
            this.tabCodeSettings.Name = "tabCodeSettings";
            this.tabCodeSettings.Padding = new System.Windows.Forms.Padding(20);
            this.tabCodeSettings.Size = new System.Drawing.Size(952, 460);
            this.tabCodeSettings.TabIndex = 1;
            this.tabCodeSettings.Text = "⚙ Cấu hình Code";
            // 
            // pnlCodeSettings
            // 
            this.pnlCodeSettings.AutoScroll = true;
            this.pnlCodeSettings.BackColor = System.Drawing.Color.White;
            this.pnlCodeSettings.Controls.Add(this.txtNotes);
            this.pnlCodeSettings.Controls.Add(this.lblNotes);
            this.pnlCodeSettings.Controls.Add(this.txtConstraints);
            this.pnlCodeSettings.Controls.Add(this.lblConstraints);
            this.pnlCodeSettings.Controls.Add(this.txtTags);
            this.pnlCodeSettings.Controls.Add(this.lblTags);
            this.pnlCodeSettings.Controls.Add(this.pnlLimits);
            this.pnlCodeSettings.Controls.Add(this.cboReturnType);
            this.pnlCodeSettings.Controls.Add(this.lblReturnType);
            this.pnlCodeSettings.Controls.Add(this.txtParameters);
            this.pnlCodeSettings.Controls.Add(this.lblParameters);
            this.pnlCodeSettings.Controls.Add(this.txtFunctionName);
            this.pnlCodeSettings.Controls.Add(this.lblFunctionName);
            this.pnlCodeSettings.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlCodeSettings.Location = new System.Drawing.Point(20, 20);
            this.pnlCodeSettings.Name = "pnlCodeSettings";
            this.pnlCodeSettings.Padding = new System.Windows.Forms.Padding(25);
            this.pnlCodeSettings.Size = new System.Drawing.Size(912, 420);
            this.pnlCodeSettings.TabIndex = 0;
            // 
            // txtNotes
            // 
            this.txtNotes.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtNotes.Dock = System.Windows.Forms.DockStyle.Top;
            this.txtNotes.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.txtNotes.Location = new System.Drawing.Point(25, 562);
            this.txtNotes.Multiline = true;
            this.txtNotes.Name = "txtNotes";
            this.txtNotes.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtNotes.Size = new System.Drawing.Size(845, 80);
            this.txtNotes.TabIndex = 12;
            // 
            // lblNotes
            // 
            this.lblNotes.AutoSize = true;
            this.lblNotes.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblNotes.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblNotes.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.lblNotes.Location = new System.Drawing.Point(25, 528);
            this.lblNotes.Name = "lblNotes";
            this.lblNotes.Padding = new System.Windows.Forms.Padding(0, 10, 0, 5);
            this.lblNotes.Size = new System.Drawing.Size(129, 34);
            this.lblNotes.TabIndex = 11;
            this.lblNotes.Text = "Ghi chú (tuỳ chọn)";
            // 
            // txtConstraints
            // 
            this.txtConstraints.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtConstraints.Dock = System.Windows.Forms.DockStyle.Top;
            this.txtConstraints.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.txtConstraints.Location = new System.Drawing.Point(25, 448);
            this.txtConstraints.Multiline = true;
            this.txtConstraints.Name = "txtConstraints";
            this.txtConstraints.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtConstraints.Size = new System.Drawing.Size(845, 80);
            this.txtConstraints.TabIndex = 10;
            // 
            // lblConstraints
            // 
            this.lblConstraints.AutoSize = true;
            this.lblConstraints.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblConstraints.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblConstraints.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.lblConstraints.Location = new System.Drawing.Point(25, 414);
            this.lblConstraints.Name = "lblConstraints";
            this.lblConstraints.Padding = new System.Windows.Forms.Padding(0, 10, 0, 5);
            this.lblConstraints.Size = new System.Drawing.Size(100, 34);
            this.lblConstraints.TabIndex = 9;
            this.lblConstraints.Text = "Ràng buộc (*)";
            // 
            // txtTags
            // 
            this.txtTags.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtTags.Dock = System.Windows.Forms.DockStyle.Top;
            this.txtTags.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtTags.Location = new System.Drawing.Point(25, 389);
            this.txtTags.Name = "txtTags";
            this.txtTags.Size = new System.Drawing.Size(845, 25);
            this.txtTags.TabIndex = 8;
            // 
            // lblTags
            // 
            this.lblTags.AutoSize = true;
            this.lblTags.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTags.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblTags.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.lblTags.Location = new System.Drawing.Point(25, 355);
            this.lblTags.Name = "lblTags";
            this.lblTags.Padding = new System.Windows.Forms.Padding(0, 10, 0, 5);
            this.lblTags.Size = new System.Drawing.Size(227, 34);
            this.lblTags.TabIndex = 7;
            this.lblTags.Text = "Tags (*) - VD: Array, String, Loop";
            // 
            // pnlLimits
            // 
            this.pnlLimits.Controls.Add(this.numMemoryLimit);
            this.pnlLimits.Controls.Add(this.lblMemoryLimit);
            this.pnlLimits.Controls.Add(this.numTimeLimit);
            this.pnlLimits.Controls.Add(this.lblTimeLimit);
            this.pnlLimits.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlLimits.Location = new System.Drawing.Point(25, 275);
            this.pnlLimits.Name = "pnlLimits";
            this.pnlLimits.Padding = new System.Windows.Forms.Padding(0, 10, 0, 10);
            this.pnlLimits.Size = new System.Drawing.Size(845, 80);
            this.pnlLimits.TabIndex = 6;
            // 
            // numMemoryLimit
            // 
            this.numMemoryLimit.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.numMemoryLimit.Location = new System.Drawing.Point(430, 35);
            this.numMemoryLimit.Maximum = new decimal(new int[] {
            1024,
            0,
            0,
            0});
            this.numMemoryLimit.Minimum = new decimal(new int[] {
            16,
            0,
            0,
            0});
            this.numMemoryLimit.Name = "numMemoryLimit";
            this.numMemoryLimit.Size = new System.Drawing.Size(250, 25);
            this.numMemoryLimit.TabIndex = 3;
            this.numMemoryLimit.Value = new decimal(new int[] {
            256,
            0,
            0,
            0});
            // 
            // lblMemoryLimit
            // 
            this.lblMemoryLimit.AutoSize = true;
            this.lblMemoryLimit.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblMemoryLimit.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.lblMemoryLimit.Location = new System.Drawing.Point(430, 10);
            this.lblMemoryLimit.Name = "lblMemoryLimit";
            this.lblMemoryLimit.Size = new System.Drawing.Size(151, 19);
            this.lblMemoryLimit.TabIndex = 2;
            this.lblMemoryLimit.Text = "Giới hạn bộ nhớ (MB)";
            // 
            // numTimeLimit
            // 
            this.numTimeLimit.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.numTimeLimit.Location = new System.Drawing.Point(0, 35);
            this.numTimeLimit.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numTimeLimit.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.numTimeLimit.Name = "numTimeLimit";
            this.numTimeLimit.Size = new System.Drawing.Size(250, 25);
            this.numTimeLimit.TabIndex = 1;
            this.numTimeLimit.Value = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            // 
            // lblTimeLimit
            // 
            this.lblTimeLimit.AutoSize = true;
            this.lblTimeLimit.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblTimeLimit.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.lblTimeLimit.Location = new System.Drawing.Point(0, 10);
            this.lblTimeLimit.Name = "lblTimeLimit";
            this.lblTimeLimit.Size = new System.Drawing.Size(160, 19);
            this.lblTimeLimit.TabIndex = 0;
            this.lblTimeLimit.Text = "Giới hạn thời gian (ms)";
            // 
            // cboReturnType
            // 
            this.cboReturnType.Dock = System.Windows.Forms.DockStyle.Top;
            this.cboReturnType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboReturnType.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cboReturnType.FormattingEnabled = true;
            this.cboReturnType.Items.AddRange(new object[] {
            "int",
            "double",
            "float",
            "string",
            "bool",
            "long",
            "char",
            "void",
            "int[]",
            "double[]",
            "string[]",
            "List<int>",
            "List<string>"});
            this.cboReturnType.Location = new System.Drawing.Point(25, 250);
            this.cboReturnType.Name = "cboReturnType";
            this.cboReturnType.Size = new System.Drawing.Size(845, 25);
            this.cboReturnType.TabIndex = 5;
            // 
            // lblReturnType
            // 
            this.lblReturnType.AutoSize = true;
            this.lblReturnType.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblReturnType.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblReturnType.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.lblReturnType.Location = new System.Drawing.Point(25, 216);
            this.lblReturnType.Name = "lblReturnType";
            this.lblReturnType.Padding = new System.Windows.Forms.Padding(0, 10, 0, 5);
            this.lblReturnType.Size = new System.Drawing.Size(101, 34);
            this.lblReturnType.TabIndex = 4;
            this.lblReturnType.Text = "Kiểu trả về (*)";
            // 
            // txtParameters
            // 
            this.txtParameters.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtParameters.Dock = System.Windows.Forms.DockStyle.Top;
            this.txtParameters.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtParameters.Location = new System.Drawing.Point(25, 186);
            this.txtParameters.Multiline = true;
            this.txtParameters.Name = "txtParameters";
            this.txtParameters.Size = new System.Drawing.Size(845, 30);
            this.txtParameters.TabIndex = 3;
            // 
            // lblParameters
            // 
            this.lblParameters.AutoSize = true;
            this.lblParameters.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblParameters.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblParameters.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.lblParameters.Location = new System.Drawing.Point(25, 152);
            this.lblParameters.Name = "lblParameters";
            this.lblParameters.Padding = new System.Windows.Forms.Padding(0, 10, 0, 5);
            this.lblParameters.Size = new System.Drawing.Size(193, 34);
            this.lblParameters.TabIndex = 2;
            this.lblParameters.Text = "Tham số (*) - VD: int a, int b";
            // 
            // txtFunctionName
            // 
            this.txtFunctionName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtFunctionName.Dock = System.Windows.Forms.DockStyle.Top;
            this.txtFunctionName.Font = new System.Drawing.Font("Consolas", 10F);
            this.txtFunctionName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(99)))), ((int)(((byte)(102)))), ((int)(((byte)(241)))));
            this.txtFunctionName.Location = new System.Drawing.Point(25, 129);
            this.txtFunctionName.Name = "txtFunctionName";
            this.txtFunctionName.ReadOnly = true;
            this.txtFunctionName.Size = new System.Drawing.Size(845, 23);
            this.txtFunctionName.TabIndex = 1;
            // 
            // lblFunctionName
            // 
            this.lblFunctionName.AutoSize = true;
            this.lblFunctionName.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblFunctionName.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblFunctionName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.lblFunctionName.Location = new System.Drawing.Point(25, 25);
            this.lblFunctionName.Name = "lblFunctionName";
            this.lblFunctionName.Padding = new System.Windows.Forms.Padding(0, 80, 0, 5);
            this.lblFunctionName.Size = new System.Drawing.Size(230, 104);
            this.lblFunctionName.TabIndex = 0;
            this.lblFunctionName.Text = "Tên hàm (tự động sinh từ tiêu đề)";
            // 
            // tabTestCases
            // 
            this.tabTestCases.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.tabTestCases.Controls.Add(this.pnlTestCases);
            this.tabTestCases.Location = new System.Drawing.Point(4, 26);
            this.tabTestCases.Name = "tabTestCases";
            this.tabTestCases.Padding = new System.Windows.Forms.Padding(20);
            this.tabTestCases.Size = new System.Drawing.Size(952, 460);
            this.tabTestCases.TabIndex = 2;
            this.tabTestCases.Text = "✓ Test Cases";
            // 
            // pnlTestCases
            // 
            this.pnlTestCases.BackColor = System.Drawing.Color.White;
            this.pnlTestCases.Controls.Add(this.dgvTestCases);
            this.pnlTestCases.Controls.Add(this.pnlTestCaseInput);
            this.pnlTestCases.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlTestCases.Location = new System.Drawing.Point(20, 20);
            this.pnlTestCases.Name = "pnlTestCases";
            this.pnlTestCases.Padding = new System.Windows.Forms.Padding(25);
            this.pnlTestCases.Size = new System.Drawing.Size(912, 420);
            this.pnlTestCases.TabIndex = 0;
            // 
            // dgvTestCases
            // 
            this.dgvTestCases.AllowUserToAddRows = false;
            this.dgvTestCases.AllowUserToDeleteRows = false;
            this.dgvTestCases.BackgroundColor = System.Drawing.Color.White;
            this.dgvTestCases.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 10F);
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            dataGridViewCellStyle1.Padding = new System.Windows.Forms.Padding(5);
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvTestCases.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvTestCases.ColumnHeadersHeight = 40;
            this.dgvTestCases.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 10F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            dataGridViewCellStyle2.Padding = new System.Windows.Forms.Padding(5);
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(246)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(64)))), ((int)(((byte)(175)))));
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvTestCases.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvTestCases.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvTestCases.EnableHeadersVisualStyles = false;
            this.dgvTestCases.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(232)))), ((int)(((byte)(240)))));
            this.dgvTestCases.Location = new System.Drawing.Point(25, 215);
            this.dgvTestCases.MultiSelect = false;
            this.dgvTestCases.Name = "dgvTestCases";
            this.dgvTestCases.ReadOnly = true;
            this.dgvTestCases.RowHeadersVisible = false;
            this.dgvTestCases.RowHeadersWidth = 51;
            this.dgvTestCases.RowTemplate.Height = 40;
            this.dgvTestCases.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvTestCases.Size = new System.Drawing.Size(862, 180);
            this.dgvTestCases.TabIndex = 1;
            // 
            // pnlTestCaseInput
            // 
            this.pnlTestCaseInput.Controls.Add(this.pnlTestCaseButtons);
            this.pnlTestCaseInput.Controls.Add(this.pnlInputOutput);
            this.pnlTestCaseInput.Controls.Add(this.pnlExplain);
            this.pnlTestCaseInput.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTestCaseInput.Location = new System.Drawing.Point(25, 25);
            this.pnlTestCaseInput.Name = "pnlTestCaseInput";
            this.pnlTestCaseInput.Size = new System.Drawing.Size(862, 190);
            this.pnlTestCaseInput.TabIndex = 0;
            // 
            // pnlTestCaseButtons
            // 
            this.pnlTestCaseButtons.Controls.Add(this.btnDeleteTestCase);
            this.pnlTestCaseButtons.Controls.Add(this.btnAddTestCase);
            this.pnlTestCaseButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlTestCaseButtons.Location = new System.Drawing.Point(0, 150);
            this.pnlTestCaseButtons.Name = "pnlTestCaseButtons";
            this.pnlTestCaseButtons.Padding = new System.Windows.Forms.Padding(0, 5, 0, 5);
            this.pnlTestCaseButtons.Size = new System.Drawing.Size(862, 40);
            this.pnlTestCaseButtons.TabIndex = 2;
            // 
            // btnDeleteTestCase
            // 
            this.btnDeleteTestCase.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(68)))), ((int)(((byte)(68)))));
            this.btnDeleteTestCase.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDeleteTestCase.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnDeleteTestCase.FlatAppearance.BorderSize = 0;
            this.btnDeleteTestCase.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDeleteTestCase.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnDeleteTestCase.ForeColor = System.Drawing.Color.White;
            this.btnDeleteTestCase.Location = new System.Drawing.Point(632, 5);
            this.btnDeleteTestCase.Name = "btnDeleteTestCase";
            this.btnDeleteTestCase.Size = new System.Drawing.Size(110, 30);
            this.btnDeleteTestCase.TabIndex = 1;
            this.btnDeleteTestCase.Text = "🗑 Xóa";
            this.btnDeleteTestCase.UseVisualStyleBackColor = false;
            // 
            // btnAddTestCase
            // 
            this.btnAddTestCase.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(197)))), ((int)(((byte)(94)))));
            this.btnAddTestCase.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAddTestCase.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnAddTestCase.FlatAppearance.BorderSize = 0;
            this.btnAddTestCase.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddTestCase.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnAddTestCase.ForeColor = System.Drawing.Color.White;
            this.btnAddTestCase.Location = new System.Drawing.Point(742, 5);
            this.btnAddTestCase.Name = "btnAddTestCase";
            this.btnAddTestCase.Size = new System.Drawing.Size(120, 30);
            this.btnAddTestCase.TabIndex = 0;
            this.btnAddTestCase.Text = "+ Thêm/Cập nhật";
            this.btnAddTestCase.UseVisualStyleBackColor = false;
            // 
            // pnlInputOutput
            // 
            this.pnlInputOutput.Controls.Add(this.pnlOutput);
            this.pnlInputOutput.Controls.Add(this.pnlInput);
            this.pnlInputOutput.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlInputOutput.Location = new System.Drawing.Point(0, 50);
            this.pnlInputOutput.Name = "pnlInputOutput";
            this.pnlInputOutput.Size = new System.Drawing.Size(862, 90);
            this.pnlInputOutput.TabIndex = 1;
            // 
            // pnlOutput
            // 
            this.pnlOutput.Controls.Add(this.txtTestCaseOutput);
            this.pnlOutput.Controls.Add(this.lblTestCaseOutput);
            this.pnlOutput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlOutput.Location = new System.Drawing.Point(436, 0);
            this.pnlOutput.Name = "pnlOutput";
            this.pnlOutput.Padding = new System.Windows.Forms.Padding(5, 0, 0, 0);
            this.pnlOutput.Size = new System.Drawing.Size(426, 90);
            this.pnlOutput.TabIndex = 1;
            // 
            // txtTestCaseOutput
            // 
            this.txtTestCaseOutput.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtTestCaseOutput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtTestCaseOutput.Font = new System.Drawing.Font("Consolas", 9.75F);
            this.txtTestCaseOutput.Location = new System.Drawing.Point(5, 22);
            this.txtTestCaseOutput.Multiline = true;
            this.txtTestCaseOutput.Name = "txtTestCaseOutput";
            this.txtTestCaseOutput.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtTestCaseOutput.Size = new System.Drawing.Size(421, 68);
            this.txtTestCaseOutput.TabIndex = 1;
            // 
            // lblTestCaseOutput
            // 
            this.lblTestCaseOutput.AutoSize = true;
            this.lblTestCaseOutput.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTestCaseOutput.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold);
            this.lblTestCaseOutput.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.lblTestCaseOutput.Location = new System.Drawing.Point(5, 0);
            this.lblTestCaseOutput.Name = "lblTestCaseOutput";
            this.lblTestCaseOutput.Padding = new System.Windows.Forms.Padding(0, 0, 0, 5);
            this.lblTestCaseOutput.Size = new System.Drawing.Size(131, 22);
            this.lblTestCaseOutput.TabIndex = 0;
            this.lblTestCaseOutput.Text = "Expected Output (*)";
            // 
            // pnlInput
            // 
            this.pnlInput.Controls.Add(this.txtTestCaseInput);
            this.pnlInput.Controls.Add(this.lblTestCaseInput);
            this.pnlInput.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlInput.Location = new System.Drawing.Point(0, 0);
            this.pnlInput.Name = "pnlInput";
            this.pnlInput.Padding = new System.Windows.Forms.Padding(0, 0, 5, 0);
            this.pnlInput.Size = new System.Drawing.Size(436, 90);
            this.pnlInput.TabIndex = 0;
            // 
            // txtTestCaseInput
            // 
            this.txtTestCaseInput.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtTestCaseInput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtTestCaseInput.Font = new System.Drawing.Font("Consolas", 9.75F);
            this.txtTestCaseInput.Location = new System.Drawing.Point(0, 22);
            this.txtTestCaseInput.Multiline = true;
            this.txtTestCaseInput.Name = "txtTestCaseInput";
            this.txtTestCaseInput.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtTestCaseInput.Size = new System.Drawing.Size(431, 68);
            this.txtTestCaseInput.TabIndex = 1;
            // 
            // lblTestCaseInput
            // 
            this.lblTestCaseInput.AutoSize = true;
            this.lblTestCaseInput.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTestCaseInput.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold);
            this.lblTestCaseInput.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.lblTestCaseInput.Location = new System.Drawing.Point(0, 0);
            this.lblTestCaseInput.Name = "lblTestCaseInput";
            this.lblTestCaseInput.Padding = new System.Windows.Forms.Padding(0, 0, 0, 5);
            this.lblTestCaseInput.Size = new System.Drawing.Size(228, 22);
            this.lblTestCaseInput.TabIndex = 0;
            this.lblTestCaseInput.Text = "Input (*) - VD: a=5,b=10,arr=[1,2,3]";
            // 
            // pnlExplain
            // 
            this.pnlExplain.Controls.Add(this.chkIsHidden);
            this.pnlExplain.Controls.Add(this.txtTestCaseExplain);
            this.pnlExplain.Controls.Add(this.lblTestCaseExplain);
            this.pnlExplain.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlExplain.Location = new System.Drawing.Point(0, 0);
            this.pnlExplain.Name = "pnlExplain";
            this.pnlExplain.Size = new System.Drawing.Size(862, 50);
            this.pnlExplain.TabIndex = 0;
            // 
            // chkIsHidden
            // 
            this.chkIsHidden.AutoSize = true;
            this.chkIsHidden.Dock = System.Windows.Forms.DockStyle.Right;
            this.chkIsHidden.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.chkIsHidden.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.chkIsHidden.Location = new System.Drawing.Point(690, 17);
            this.chkIsHidden.Name = "chkIsHidden";
            this.chkIsHidden.Padding = new System.Windows.Forms.Padding(0, 5, 0, 5);
            this.chkIsHidden.Size = new System.Drawing.Size(172, 33);
            this.chkIsHidden.TabIndex = 2;
            this.chkIsHidden.Text = "🔒 Test case ẩn (hidden)";
            this.chkIsHidden.UseVisualStyleBackColor = true;
            // 
            // txtTestCaseExplain
            // 
            this.txtTestCaseExplain.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtTestCaseExplain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtTestCaseExplain.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.txtTestCaseExplain.Location = new System.Drawing.Point(0, 17);
            this.txtTestCaseExplain.Multiline = true;
            this.txtTestCaseExplain.Name = "txtTestCaseExplain";
            this.txtTestCaseExplain.Size = new System.Drawing.Size(862, 33);
            this.txtTestCaseExplain.TabIndex = 1;
            // 
            // lblTestCaseExplain
            // 
            this.lblTestCaseExplain.AutoSize = true;
            this.lblTestCaseExplain.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTestCaseExplain.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold);
            this.lblTestCaseExplain.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.lblTestCaseExplain.Location = new System.Drawing.Point(0, 0);
            this.lblTestCaseExplain.Name = "lblTestCaseExplain";
            this.lblTestCaseExplain.Size = new System.Drawing.Size(135, 17);
            this.lblTestCaseExplain.TabIndex = 0;
            this.lblTestCaseExplain.Text = "Giải thích (tuỳ chọn)";
            // 
            // pnlFooter
            // 
            this.pnlFooter.BackColor = System.Drawing.Color.White;
            this.pnlFooter.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlFooter.Controls.Add(this.btnCancel);
            this.pnlFooter.Controls.Add(this.btnSave);
            this.pnlFooter.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlFooter.Location = new System.Drawing.Point(0, 630);
            this.pnlFooter.Name = "pnlFooter";
            this.pnlFooter.Padding = new System.Windows.Forms.Padding(20, 15, 20, 15);
            this.pnlFooter.Size = new System.Drawing.Size(1000, 70);
            this.pnlFooter.TabIndex = 1;
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.White;
            this.btnCancel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancel.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnCancel.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(232)))), ((int)(((byte)(240)))));
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnCancel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.btnCancel.Location = new System.Drawing.Point(728, 15);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(120, 38);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "Hủy";
            this.btnCancel.UseVisualStyleBackColor = false;
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(99)))), ((int)(((byte)(235)))));
            this.btnSave.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSave.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnSave.FlatAppearance.BorderSize = 0;
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.Location = new System.Drawing.Point(848, 15);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(130, 38);
            this.btnSave.TabIndex = 0;
            this.btnSave.Text = "💾 Lưu bài tập";
            this.btnSave.UseVisualStyleBackColor = false;
            // 
            // pnlHeader
            // 
            this.pnlHeader.BackColor = System.Drawing.Color.White;
            this.pnlHeader.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlHeader.Controls.Add(this.lblSubtitle);
            this.pnlHeader.Controls.Add(this.lblTitle);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Padding = new System.Windows.Forms.Padding(30, 20, 30, 20);
            this.pnlHeader.Size = new System.Drawing.Size(1000, 100);
            this.pnlHeader.TabIndex = 0;
            // 
            // lblSubtitle
            // 
            this.lblSubtitle.AutoSize = true;
            this.lblSubtitle.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.lblSubtitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblSubtitle.Location = new System.Drawing.Point(30, 55);
            this.lblSubtitle.Name = "lblSubtitle";
            this.lblSubtitle.Size = new System.Drawing.Size(416, 17);
            this.lblSubtitle.TabIndex = 1;
            this.lblSubtitle.Text = "Điền đầy đủ thông tin bài tập, cấu hình code và test cases để hoàn tất";
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.lblTitle.Location = new System.Drawing.Point(25, 20);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(214, 32);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Thêm bài tập mới";
            // 
            // AddEditProblemForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1000, 700);
            this.Controls.Add(this.pnlMain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AddEditProblemForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Quản lý Bài tập Lập trình";
            this.pnlMain.ResumeLayout(false);
            this.pnlContent.ResumeLayout(false);
            this.tabProblemInfo.ResumeLayout(false);
            this.tabGeneral.ResumeLayout(false);
            this.pnlGeneral.ResumeLayout(false);
            this.pnlGeneral.PerformLayout();
            this.pnlDifficultyCategory.ResumeLayout(false);
            this.pnlDifficultyCategory.PerformLayout();
            this.tabCodeSettings.ResumeLayout(false);
            this.pnlCodeSettings.ResumeLayout(false);
            this.pnlCodeSettings.PerformLayout();
            this.pnlLimits.ResumeLayout(false);
            this.pnlLimits.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numMemoryLimit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numTimeLimit)).EndInit();
            this.tabTestCases.ResumeLayout(false);
            this.pnlTestCases.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvTestCases)).EndInit();
            this.pnlTestCaseInput.ResumeLayout(false);
            this.pnlTestCaseButtons.ResumeLayout(false);
            this.pnlInputOutput.ResumeLayout(false);
            this.pnlOutput.ResumeLayout(false);
            this.pnlOutput.PerformLayout();
            this.pnlInput.ResumeLayout(false);
            this.pnlInput.PerformLayout();
            this.pnlExplain.ResumeLayout(false);
            this.pnlExplain.PerformLayout();
            this.pnlFooter.ResumeLayout(false);
            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlMain;
        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblSubtitle;
        private System.Windows.Forms.Panel pnlFooter;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Panel pnlContent;
        private System.Windows.Forms.TabControl tabProblemInfo;
        private System.Windows.Forms.TabPage tabGeneral;
        private System.Windows.Forms.Panel pnlGeneral;
        private System.Windows.Forms.TextBox txtTitle;
        private System.Windows.Forms.Label lblTitleLabel;
        private System.Windows.Forms.Panel pnlDifficultyCategory;
        private System.Windows.Forms.ComboBox cboDifficulty;
        private System.Windows.Forms.Label lblDifficulty;
        private System.Windows.Forms.TextBox txtDescription;
        private System.Windows.Forms.Label lblDesc;
        private System.Windows.Forms.TabPage tabCodeSettings;
        private System.Windows.Forms.Panel pnlCodeSettings;
        private System.Windows.Forms.TextBox txtFunctionName;
        private System.Windows.Forms.Label lblFunctionName;
        private System.Windows.Forms.TextBox txtParameters;
        private System.Windows.Forms.Label lblParameters;
        private System.Windows.Forms.ComboBox cboReturnType;
        private System.Windows.Forms.Label lblReturnType;
        private System.Windows.Forms.Panel pnlLimits;
        private System.Windows.Forms.NumericUpDown numTimeLimit;
        private System.Windows.Forms.Label lblTimeLimit;
        private System.Windows.Forms.NumericUpDown numMemoryLimit;
        private System.Windows.Forms.Label lblMemoryLimit;
        private System.Windows.Forms.TextBox txtTags;
        private System.Windows.Forms.Label lblTags;
        private System.Windows.Forms.TextBox txtConstraints;
        private System.Windows.Forms.Label lblConstraints;
        private System.Windows.Forms.TextBox txtNotes;
        private System.Windows.Forms.Label lblNotes;
        private System.Windows.Forms.TabPage tabTestCases;
        private System.Windows.Forms.Panel pnlTestCases;
        private System.Windows.Forms.DataGridView dgvTestCases;
        private System.Windows.Forms.Panel pnlTestCaseInput;
        private System.Windows.Forms.Panel pnlExplain;
        private System.Windows.Forms.TextBox txtTestCaseExplain;
        private System.Windows.Forms.Label lblTestCaseExplain;
        private System.Windows.Forms.CheckBox chkIsHidden;
        private System.Windows.Forms.Panel pnlInputOutput;
        private System.Windows.Forms.Panel pnlInput;
        private System.Windows.Forms.TextBox txtTestCaseInput;
        private System.Windows.Forms.Label lblTestCaseInput;
        private System.Windows.Forms.Panel pnlOutput;
        private System.Windows.Forms.TextBox txtTestCaseOutput;
        private System.Windows.Forms.Label lblTestCaseOutput;
        private System.Windows.Forms.Panel pnlTestCaseButtons;
        private System.Windows.Forms.Button btnDeleteTestCase;
        private System.Windows.Forms.Button btnAddTestCase;
    }
}