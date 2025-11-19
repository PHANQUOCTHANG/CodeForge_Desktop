namespace CodeForge_Desktop.Presentation.Forms.Admin
{
    partial class AddEditProblemForm
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
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.pnlFooter = new System.Windows.Forms.Panel();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.pnlContent = new System.Windows.Forms.Panel();
            this.tabProblemInfo = new System.Windows.Forms.TabControl();
            this.tabGeneral = new System.Windows.Forms.TabPage();
            this.txtTitle = new System.Windows.Forms.TextBox();
            this.lblTitleLabel = new System.Windows.Forms.Label();
            this.cboDifficulty = new System.Windows.Forms.ComboBox();
            this.lblDifficulty = new System.Windows.Forms.Label();
            this.cboCategory = new System.Windows.Forms.ComboBox();
            this.lblCategory = new System.Windows.Forms.Label();
            this.cboStatus = new System.Windows.Forms.ComboBox();
            this.lblStatus = new System.Windows.Forms.Label();
            this.txtDescription = new System.Windows.Forms.TextBox(); // Changed to TextBox for multiline support
            this.lblDesc = new System.Windows.Forms.Label();
            this.tabCodeSettings = new System.Windows.Forms.TabPage();
            this.txtFunctionName = new System.Windows.Forms.TextBox();
            this.lblFunctionName = new System.Windows.Forms.Label();
            this.txtParameters = new System.Windows.Forms.TextBox();
            this.lblParameters = new System.Windows.Forms.Label();
            this.txtReturnType = new System.Windows.Forms.TextBox();
            this.lblReturnType = new System.Windows.Forms.Label();
            this.numTimeLimit = new System.Windows.Forms.NumericUpDown();
            this.lblTimeLimit = new System.Windows.Forms.Label();
            this.numMemoryLimit = new System.Windows.Forms.NumericUpDown();
            this.lblMemoryLimit = new System.Windows.Forms.Label();
            this.pnlHeader.SuspendLayout();
            this.pnlFooter.SuspendLayout();
            this.pnlContent.SuspendLayout();
            this.tabProblemInfo.SuspendLayout();
            this.tabGeneral.SuspendLayout();
            this.tabCodeSettings.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numTimeLimit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMemoryLimit)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlHeader
            // 
            this.pnlHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(120)))), ((int)(((byte)(215)))));
            this.pnlHeader.Controls.Add(this.lblTitle);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size = new System.Drawing.Size(600, 60);
            this.pnlHeader.TabIndex = 0;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(20, 15);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(203, 30);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Thêm Bài Tập Mới";
            // 
            // pnlFooter
            // 
            this.pnlFooter.BackColor = System.Drawing.Color.WhiteSmoke;
            this.pnlFooter.Controls.Add(this.btnCancel);
            this.pnlFooter.Controls.Add(this.btnSave);
            this.pnlFooter.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlFooter.Location = new System.Drawing.Point(0, 540);
            this.pnlFooter.Name = "pnlFooter";
            this.pnlFooter.Size = new System.Drawing.Size(600, 60);
            this.pnlFooter.TabIndex = 2;
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.BackColor = System.Drawing.Color.Gray;
            this.btnCancel.FlatAppearance.BorderSize = 0;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnCancel.ForeColor = System.Drawing.Color.White;
            this.btnCancel.Location = new System.Drawing.Point(480, 12);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(100, 35);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "Hủy";
            this.btnCancel.UseVisualStyleBackColor = false;
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(167)))), ((int)(((byte)(69)))));
            this.btnSave.FlatAppearance.BorderSize = 0;
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.Location = new System.Drawing.Point(370, 12);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(100, 35);
            this.btnSave.TabIndex = 0;
            this.btnSave.Text = "Lưu";
            this.btnSave.UseVisualStyleBackColor = false;
            // 
            // pnlContent
            // 
            this.pnlContent.BackColor = System.Drawing.Color.White;
            this.pnlContent.Controls.Add(this.tabProblemInfo);
            this.pnlContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlContent.Location = new System.Drawing.Point(0, 60);
            this.pnlContent.Name = "pnlContent";
            this.pnlContent.Padding = new System.Windows.Forms.Padding(10);
            this.pnlContent.Size = new System.Drawing.Size(600, 480);
            this.pnlContent.TabIndex = 1;
            // 
            // tabProblemInfo
            // 
            this.tabProblemInfo.Controls.Add(this.tabGeneral);
            this.tabProblemInfo.Controls.Add(this.tabCodeSettings);
            this.tabProblemInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabProblemInfo.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.tabProblemInfo.Location = new System.Drawing.Point(10, 10);
            this.tabProblemInfo.Name = "tabProblemInfo";
            this.tabProblemInfo.SelectedIndex = 0;
            this.tabProblemInfo.Size = new System.Drawing.Size(580, 460);
            this.tabProblemInfo.TabIndex = 0;
            // 
            // tabGeneral
            // 
            this.tabGeneral.Controls.Add(this.txtDescription);
            this.tabGeneral.Controls.Add(this.lblDesc);
            this.tabGeneral.Controls.Add(this.cboStatus);
            this.tabGeneral.Controls.Add(this.lblStatus);
            this.tabGeneral.Controls.Add(this.cboCategory);
            this.tabGeneral.Controls.Add(this.lblCategory);
            this.tabGeneral.Controls.Add(this.cboDifficulty);
            this.tabGeneral.Controls.Add(this.lblDifficulty);
            this.tabGeneral.Controls.Add(this.txtTitle);
            this.tabGeneral.Controls.Add(this.lblTitleLabel);
            this.tabGeneral.Location = new System.Drawing.Point(4, 26);
            this.tabGeneral.Name = "tabGeneral";
            this.tabGeneral.Padding = new System.Windows.Forms.Padding(10);
            this.tabGeneral.Size = new System.Drawing.Size(572, 430);
            this.tabGeneral.TabIndex = 0;
            this.tabGeneral.Text = "Thông tin chung";
            this.tabGeneral.UseVisualStyleBackColor = true;
            // 
            // lblTitleLabel
            // 
            this.lblTitleLabel.AutoSize = true;
            this.lblTitleLabel.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.lblTitleLabel.Location = new System.Drawing.Point(13, 15);
            this.lblTitleLabel.Name = "lblTitleLabel";
            this.lblTitleLabel.Size = new System.Drawing.Size(82, 19);
            this.lblTitleLabel.TabIndex = 0;
            this.lblTitleLabel.Text = "Tên bài tập:";
            // 
            // txtTitle
            // 
            this.txtTitle.Location = new System.Drawing.Point(17, 37);
            this.txtTitle.Name = "txtTitle";
            this.txtTitle.Size = new System.Drawing.Size(538, 25);
            this.txtTitle.TabIndex = 1;
            // 
            // lblDifficulty
            // 
            this.lblDifficulty.AutoSize = true;
            this.lblDifficulty.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.lblDifficulty.Location = new System.Drawing.Point(13, 75);
            this.lblDifficulty.Name = "lblDifficulty";
            this.lblDifficulty.Size = new System.Drawing.Size(57, 19);
            this.lblDifficulty.TabIndex = 2;
            this.lblDifficulty.Text = "Độ khó:";
            // 
            // cboDifficulty
            // 
            this.cboDifficulty.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboDifficulty.FormattingEnabled = true;
            this.cboDifficulty.Items.AddRange(new object[] {
            "Dễ",
            "Trung bình",
            "Khó"});
            this.cboDifficulty.Location = new System.Drawing.Point(17, 97);
            this.cboDifficulty.Name = "cboDifficulty";
            this.cboDifficulty.Size = new System.Drawing.Size(170, 25);
            this.cboDifficulty.TabIndex = 3;
            // 
            // lblCategory
            // 
            this.lblCategory.AutoSize = true;
            this.lblCategory.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.lblCategory.Location = new System.Drawing.Point(200, 75);
            this.lblCategory.Name = "lblCategory";
            this.lblCategory.Size = new System.Drawing.Size(76, 19);
            this.lblCategory.TabIndex = 4;
            this.lblCategory.Text = "Danh mục:";
            // 
            // cboCategory
            // 
            this.cboCategory.FormattingEnabled = true;
            this.cboCategory.Items.AddRange(new object[] {
            "Cơ bản",
            "Thuật toán",
            "Cấu trúc dữ liệu"});
            this.cboCategory.Location = new System.Drawing.Point(204, 97);
            this.cboCategory.Name = "cboCategory";
            this.cboCategory.Size = new System.Drawing.Size(170, 25);
            this.cboCategory.TabIndex = 5;
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.lblStatus.Location = new System.Drawing.Point(385, 75);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(75, 19);
            this.lblStatus.TabIndex = 6;
            this.lblStatus.Text = "Trạng thái:";
            // 
            // cboStatus
            // 
            this.cboStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboStatus.FormattingEnabled = true;
            this.cboStatus.Items.AddRange(new object[] {
            "SOLVED",
            "ATTEMPTED",
            "NOT_STARTED"});
            this.cboStatus.Location = new System.Drawing.Point(389, 97);
            this.cboStatus.Name = "cboStatus";
            this.cboStatus.Size = new System.Drawing.Size(166, 25);
            this.cboStatus.TabIndex = 7;
            // 
            // lblDesc
            // 
            this.lblDesc.AutoSize = true;
            this.lblDesc.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.lblDesc.Location = new System.Drawing.Point(13, 135);
            this.lblDesc.Name = "lblDesc";
            this.lblDesc.Size = new System.Drawing.Size(99, 19);
            this.lblDesc.TabIndex = 8;
            this.lblDesc.Text = "Mô tả bài tập:";
            // 
            // txtDescription
            // 
            this.txtDescription.Location = new System.Drawing.Point(17, 157);
            this.txtDescription.Multiline = true;
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtDescription.Size = new System.Drawing.Size(538, 256);
            this.txtDescription.TabIndex = 9;
            // 
            // tabCodeSettings
            // 
            this.tabCodeSettings.Controls.Add(this.lblMemoryLimit);
            this.tabCodeSettings.Controls.Add(this.numMemoryLimit);
            this.tabCodeSettings.Controls.Add(this.lblTimeLimit);
            this.tabCodeSettings.Controls.Add(this.numTimeLimit);
            this.tabCodeSettings.Controls.Add(this.txtReturnType);
            this.tabCodeSettings.Controls.Add(this.lblReturnType);
            this.tabCodeSettings.Controls.Add(this.txtParameters);
            this.tabCodeSettings.Controls.Add(this.lblParameters);
            this.tabCodeSettings.Controls.Add(this.txtFunctionName);
            this.tabCodeSettings.Controls.Add(this.lblFunctionName);
            this.tabCodeSettings.Location = new System.Drawing.Point(4, 26);
            this.tabCodeSettings.Name = "tabCodeSettings";
            this.tabCodeSettings.Padding = new System.Windows.Forms.Padding(10);
            this.tabCodeSettings.Size = new System.Drawing.Size(572, 430);
            this.tabCodeSettings.TabIndex = 1;
            this.tabCodeSettings.Text = "Cấu hình Code";
            this.tabCodeSettings.UseVisualStyleBackColor = true;
            // 
            // lblFunctionName
            // 
            this.lblFunctionName.AutoSize = true;
            this.lblFunctionName.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.lblFunctionName.Location = new System.Drawing.Point(13, 15);
            this.lblFunctionName.Name = "lblFunctionName";
            this.lblFunctionName.Size = new System.Drawing.Size(103, 19);
            this.lblFunctionName.TabIndex = 0;
            this.lblFunctionName.Text = "Tên hàm (VD: solution):";
            // 
            // txtFunctionName
            // 
            this.txtFunctionName.Location = new System.Drawing.Point(17, 37);
            this.txtFunctionName.Name = "txtFunctionName";
            this.txtFunctionName.Size = new System.Drawing.Size(538, 25);
            this.txtFunctionName.TabIndex = 1;
            // 
            // lblParameters
            // 
            this.lblParameters.AutoSize = true;
            this.lblParameters.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.lblParameters.Location = new System.Drawing.Point(13, 75);
            this.lblParameters.Name = "lblParameters";
            this.lblParameters.Size = new System.Drawing.Size(236, 19);
            this.lblParameters.TabIndex = 2;
            this.lblParameters.Text = "Tham số (VD: int a, int b):";
            // 
            // txtParameters
            // 
            this.txtParameters.Location = new System.Drawing.Point(17, 97);
            this.txtParameters.Name = "txtParameters";
            this.txtParameters.Size = new System.Drawing.Size(538, 25);
            this.txtParameters.TabIndex = 3;
            // 
            // lblReturnType
            // 
            this.lblReturnType.AutoSize = true;
            this.lblReturnType.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.lblReturnType.Location = new System.Drawing.Point(13, 135);
            this.lblReturnType.Name = "lblReturnType";
            this.lblReturnType.Size = new System.Drawing.Size(156, 19);
            this.lblReturnType.TabIndex = 4;
            this.lblReturnType.Text = "Kiểu trả về (VD: int, string):";
            // 
            // txtReturnType
            // 
            this.txtReturnType.Location = new System.Drawing.Point(17, 157);
            this.txtReturnType.Name = "txtReturnType";
            this.txtReturnType.Size = new System.Drawing.Size(538, 25);
            this.txtReturnType.TabIndex = 5;
            // 
            // lblTimeLimit
            // 
            this.lblTimeLimit.AutoSize = true;
            this.lblTimeLimit.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.lblTimeLimit.Location = new System.Drawing.Point(13, 195);
            this.lblTimeLimit.Name = "lblTimeLimit";
            this.lblTimeLimit.Size = new System.Drawing.Size(148, 19);
            this.lblTimeLimit.TabIndex = 6;
            this.lblTimeLimit.Text = "Giới hạn thời gian (ms):";
            // 
            // numTimeLimit
            // 
            this.numTimeLimit.Location = new System.Drawing.Point(17, 217);
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
            this.numTimeLimit.Size = new System.Drawing.Size(200, 25);
            this.numTimeLimit.TabIndex = 7;
            this.numTimeLimit.Value = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            // 
            // lblMemoryLimit
            // 
            this.lblMemoryLimit.AutoSize = true;
            this.lblMemoryLimit.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.lblMemoryLimit.Location = new System.Drawing.Point(250, 195);
            this.lblMemoryLimit.Name = "lblMemoryLimit";
            this.lblMemoryLimit.Size = new System.Drawing.Size(146, 19);
            this.lblMemoryLimit.TabIndex = 8;
            this.lblMemoryLimit.Text = "Giới hạn bộ nhớ (MB):";
            // 
            // numMemoryLimit
            // 
            this.numMemoryLimit.Location = new System.Drawing.Point(254, 217);
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
            this.numMemoryLimit.Size = new System.Drawing.Size(200, 25);
            this.numMemoryLimit.TabIndex = 9;
            this.numMemoryLimit.Value = new decimal(new int[] {
            256,
            0,
            0,
            0});
            // 
            // AddEditProblemForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(600, 600);
            this.Controls.Add(this.pnlContent);
            this.Controls.Add(this.pnlFooter);
            this.Controls.Add(this.pnlHeader);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AddEditProblemForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Quản lý Bài tập";
            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            this.pnlFooter.ResumeLayout(false);
            this.pnlContent.ResumeLayout(false);
            this.tabProblemInfo.ResumeLayout(false);
            this.tabGeneral.ResumeLayout(false);
            this.tabGeneral.PerformLayout();
            this.tabCodeSettings.ResumeLayout(false);
            this.tabCodeSettings.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numTimeLimit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMemoryLimit)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Panel pnlFooter;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Panel pnlContent;
        private System.Windows.Forms.TabControl tabProblemInfo;
        private System.Windows.Forms.TabPage tabGeneral;
        private System.Windows.Forms.TextBox txtDescription;
        private System.Windows.Forms.Label lblDesc;
        private System.Windows.Forms.ComboBox cboStatus;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.ComboBox cboCategory;
        private System.Windows.Forms.Label lblCategory;
        private System.Windows.Forms.ComboBox cboDifficulty;
        private System.Windows.Forms.Label lblDifficulty;
        private System.Windows.Forms.TextBox txtTitle;
        private System.Windows.Forms.Label lblTitleLabel;
        private System.Windows.Forms.TabPage tabCodeSettings;
        private System.Windows.Forms.TextBox txtFunctionName;
        private System.Windows.Forms.Label lblFunctionName;
        private System.Windows.Forms.TextBox txtParameters;
        private System.Windows.Forms.Label lblParameters;
        private System.Windows.Forms.TextBox txtReturnType;
        private System.Windows.Forms.Label lblReturnType;
        private System.Windows.Forms.NumericUpDown numTimeLimit;
        private System.Windows.Forms.Label lblTimeLimit;
        private System.Windows.Forms.NumericUpDown numMemoryLimit;
        private System.Windows.Forms.Label lblMemoryLimit;
    }
}