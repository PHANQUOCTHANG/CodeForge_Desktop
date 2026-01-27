namespace CodeForge_Desktop.Presentation.Forms.Student
{
    partial class ucSubmissions
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.pnlMain = new System.Windows.Forms.Panel();
            this.pnlGridContainer = new System.Windows.Forms.Panel();
            this.dgvSubmissions = new System.Windows.Forms.DataGridView();
            this.pnlFilters = new System.Windows.Forms.Panel();
            this.btnApplyFilter = new System.Windows.Forms.Button();
            this.cmbStatus = new System.Windows.Forms.ComboBox();
            this.cmbProblems = new System.Windows.Forms.ComboBox();
            this.lblFilter = new System.Windows.Forms.Label();
            this.pnlStatsContainer = new System.Windows.Forms.Panel();
            this.tblStatsLayout = new System.Windows.Forms.TableLayoutPanel();
            this.pnlStatTotal = new System.Windows.Forms.Panel();
            this.stripTotal = new System.Windows.Forms.Panel();
            this.lblIconTotal = new System.Windows.Forms.Label();
            this.lblStatTotalValue = new System.Windows.Forms.Label();
            this.lblStatTotalTitle = new System.Windows.Forms.Label();
            this.pnlStatAccepted = new System.Windows.Forms.Panel();
            this.stripAccepted = new System.Windows.Forms.Panel();
            this.lblIconAccepted = new System.Windows.Forms.Label();
            this.lblStatAcceptedValue = new System.Windows.Forms.Label();
            this.lblStatAcceptedTitle = new System.Windows.Forms.Label();
            this.pnlStatWA = new System.Windows.Forms.Panel();
            this.stripWA = new System.Windows.Forms.Panel();
            this.lblIconWA = new System.Windows.Forms.Label();
            this.lblStatWAValue = new System.Windows.Forms.Label();
            this.lblStatWATitle = new System.Windows.Forms.Label();
            this.pnlStatError = new System.Windows.Forms.Panel();
            this.stripError = new System.Windows.Forms.Panel();
            this.lblIconError = new System.Windows.Forms.Label();
            this.lblStatErrorValue = new System.Windows.Forms.Label();
            this.lblStatErrorTitle = new System.Windows.Forms.Label();
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblSummary = new System.Windows.Forms.Label();
            this.colHash = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colProblemName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTimestamp = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colStatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colScore = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colView = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pnlMain.SuspendLayout();
            this.pnlGridContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSubmissions)).BeginInit();
            this.pnlFilters.SuspendLayout();
            this.pnlStatsContainer.SuspendLayout();
            this.tblStatsLayout.SuspendLayout();
            this.pnlStatTotal.SuspendLayout();
            this.pnlStatAccepted.SuspendLayout();
            this.pnlStatWA.SuspendLayout();
            this.pnlStatError.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlMain
            // 
            this.pnlMain.AutoScroll = true;
            this.pnlMain.Controls.Add(this.pnlGridContainer);
            this.pnlMain.Controls.Add(this.pnlFilters);
            this.pnlMain.Controls.Add(this.pnlStatsContainer);
            this.pnlMain.Controls.Add(this.lblTitle);
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Location = new System.Drawing.Point(0, 0);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Padding = new System.Windows.Forms.Padding(20);
            this.pnlMain.Size = new System.Drawing.Size(1200, 800);
            this.pnlMain.TabIndex = 0;
            // 
            // pnlGridContainer
            // 
            this.pnlGridContainer.BackColor = System.Drawing.Color.Transparent;
            this.pnlGridContainer.Controls.Add(this.dgvSubmissions);
            this.pnlGridContainer.Controls.Add(this.lblSummary);
            this.pnlGridContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlGridContainer.Location = new System.Drawing.Point(20, 262);
            this.pnlGridContainer.Name = "pnlGridContainer";
            this.pnlGridContainer.Size = new System.Drawing.Size(1160, 518);
            this.pnlGridContainer.TabIndex = 3;
            // 
            // dgvSubmissions
            // 
            this.dgvSubmissions.AllowUserToAddRows = false;
            this.dgvSubmissions.AllowUserToDeleteRows = false;
            this.dgvSubmissions.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvSubmissions.BackgroundColor = System.Drawing.Color.White;
            this.dgvSubmissions.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvSubmissions.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgvSubmissions.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(248)))), ((int)(((byte)(248)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            dataGridViewCellStyle1.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.dgvSubmissions.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvSubmissions.ColumnHeadersHeight = 45;
            this.dgvSubmissions.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvSubmissions.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colHash,
            this.colProblemName,
            this.colTimestamp,
            this.colStatus,
            this.colScore,
            this.colView});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(33)))), ((int)(((byte)(41)))));
            dataGridViewCellStyle2.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(245)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvSubmissions.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvSubmissions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvSubmissions.EnableHeadersVisualStyles = false;
            this.dgvSubmissions.GridColor = System.Drawing.Color.White;
            this.dgvSubmissions.Location = new System.Drawing.Point(0, 0);
            this.dgvSubmissions.Name = "dgvSubmissions";
            this.dgvSubmissions.ReadOnly = true;
            this.dgvSubmissions.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dgvSubmissions.RowHeadersVisible = false;
            this.dgvSubmissions.RowHeadersWidth = 51;
            this.dgvSubmissions.RowTemplate.Height = 45;
            this.dgvSubmissions.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvSubmissions.Size = new System.Drawing.Size(1160, 494);
            this.dgvSubmissions.TabIndex = 0;
            // 
            // pnlFilters
            // 
            this.pnlFilters.BackColor = System.Drawing.Color.Transparent;
            this.pnlFilters.Controls.Add(this.btnApplyFilter);
            this.pnlFilters.Controls.Add(this.cmbStatus);
            this.pnlFilters.Controls.Add(this.cmbProblems);
            this.pnlFilters.Controls.Add(this.lblFilter);
            this.pnlFilters.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlFilters.Location = new System.Drawing.Point(20, 202);
            this.pnlFilters.Name = "pnlFilters";
            this.pnlFilters.Padding = new System.Windows.Forms.Padding(0, 15, 0, 0);
            this.pnlFilters.Size = new System.Drawing.Size(1160, 60);
            this.pnlFilters.TabIndex = 2;
            // 
            // btnApplyFilter
            // 
            this.btnApplyFilter.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(120)))), ((int)(((byte)(215)))));
            this.btnApplyFilter.FlatAppearance.BorderSize = 0;
            this.btnApplyFilter.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnApplyFilter.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnApplyFilter.ForeColor = System.Drawing.Color.White;
            this.btnApplyFilter.Location = new System.Drawing.Point(494, 18);
            this.btnApplyFilter.Name = "btnApplyFilter";
            this.btnApplyFilter.Size = new System.Drawing.Size(100, 32);
            this.btnApplyFilter.TabIndex = 2;
            this.btnApplyFilter.Text = "Áp dụng";
            this.btnApplyFilter.UseVisualStyleBackColor = false;
            // 
            // cmbStatus
            // 
            this.cmbStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbStatus.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cmbStatus.FormattingEnabled = true;
            this.cmbStatus.Items.AddRange(new object[] {
            "Tất cả trạng thái",
            "Accepted",
            "Wrong Answer",
            "Runtime Error"});
            this.cmbStatus.Location = new System.Drawing.Point(298, 19);
            this.cmbStatus.Name = "cmbStatus";
            this.cmbStatus.Size = new System.Drawing.Size(183, 31);
            this.cmbStatus.TabIndex = 1;
            // 
            // cmbProblems
            // 
            this.cmbProblems.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbProblems.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cmbProblems.FormattingEnabled = true;
            this.cmbProblems.Location = new System.Drawing.Point(85, 19);
            this.cmbProblems.Name = "cmbProblems";
            this.cmbProblems.Size = new System.Drawing.Size(200, 31);
            this.cmbProblems.TabIndex = 0;
            // 
            // lblFilter
            // 
            this.lblFilter.AutoSize = true;
            this.lblFilter.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblFilter.Location = new System.Drawing.Point(0, 22);
            this.lblFilter.Name = "lblFilter";
            this.lblFilter.Size = new System.Drawing.Size(80, 23);
            this.lblFilter.TabIndex = 3;
            this.lblFilter.Text = "Lọc theo:";
            // 
            // pnlStatsContainer
            // 
            this.pnlStatsContainer.BackColor = System.Drawing.Color.Transparent;
            this.pnlStatsContainer.Controls.Add(this.tblStatsLayout);
            this.pnlStatsContainer.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlStatsContainer.Location = new System.Drawing.Point(20, 78);
            this.pnlStatsContainer.Name = "pnlStatsContainer";
            this.pnlStatsContainer.Size = new System.Drawing.Size(1160, 124);
            this.pnlStatsContainer.TabIndex = 1;
            // 
            // tblStatsLayout
            // 
            this.tblStatsLayout.BackColor = System.Drawing.Color.Transparent;
            this.tblStatsLayout.ColumnCount = 4;
            this.tblStatsLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tblStatsLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tblStatsLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tblStatsLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tblStatsLayout.Controls.Add(this.pnlStatTotal, 0, 0);
            this.tblStatsLayout.Controls.Add(this.pnlStatAccepted, 1, 0);
            this.tblStatsLayout.Controls.Add(this.pnlStatWA, 2, 0);
            this.tblStatsLayout.Controls.Add(this.pnlStatError, 3, 0);
            this.tblStatsLayout.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tblStatsLayout.Location = new System.Drawing.Point(0, 3);
            this.tblStatsLayout.Name = "tblStatsLayout";
            this.tblStatsLayout.Padding = new System.Windows.Forms.Padding(15, 0, 15, 15);
            this.tblStatsLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tblStatsLayout.Size = new System.Drawing.Size(1160, 121);
            this.tblStatsLayout.TabIndex = 0;
            // 
            // pnlStatTotal
            // 
            this.pnlStatTotal.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(245)))), ((int)(((byte)(255)))));
            this.pnlStatTotal.Controls.Add(this.stripTotal);
            this.pnlStatTotal.Controls.Add(this.lblIconTotal);
            this.pnlStatTotal.Controls.Add(this.lblStatTotalValue);
            this.pnlStatTotal.Controls.Add(this.lblStatTotalTitle);
            this.pnlStatTotal.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlStatTotal.Location = new System.Drawing.Point(15, 0);
            this.pnlStatTotal.Margin = new System.Windows.Forms.Padding(0, 0, 15, 0);
            this.pnlStatTotal.Name = "pnlStatTotal";
            this.pnlStatTotal.Size = new System.Drawing.Size(267, 106);
            this.pnlStatTotal.TabIndex = 0;
            // 
            // stripTotal
            // 
            this.stripTotal.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(120)))), ((int)(((byte)(215)))));
            this.stripTotal.Dock = System.Windows.Forms.DockStyle.Left;
            this.stripTotal.Location = new System.Drawing.Point(0, 0);
            this.stripTotal.Name = "stripTotal";
            this.stripTotal.Size = new System.Drawing.Size(4, 81);
            this.stripTotal.TabIndex = 0;
            // 
            // lblIconTotal
            // 
            this.lblIconTotal.AutoSize = true;
            this.lblIconTotal.Font = new System.Drawing.Font("Segoe UI Emoji", 20F);
            this.lblIconTotal.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(120)))), ((int)(((byte)(215)))));
            this.lblIconTotal.Location = new System.Drawing.Point(2, 23);
            this.lblIconTotal.Name = "lblIconTotal";
            this.lblIconTotal.Size = new System.Drawing.Size(60, 46);
            this.lblIconTotal.TabIndex = 1;
            this.lblIconTotal.Text = "📄";
            // 
            // lblStatTotalValue
            // 
            this.lblStatTotalValue.AutoSize = true;
            this.lblStatTotalValue.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold);
            this.lblStatTotalValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(120)))), ((int)(((byte)(215)))));
            this.lblStatTotalValue.Location = new System.Drawing.Point(94, 25);
            this.lblStatTotalValue.Name = "lblStatTotalValue";
            this.lblStatTotalValue.Size = new System.Drawing.Size(40, 46);
            this.lblStatTotalValue.TabIndex = 2;
            this.lblStatTotalValue.Text = "0";
            // 
            // lblStatTotalTitle
            // 
            this.lblStatTotalTitle.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblStatTotalTitle.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblStatTotalTitle.ForeColor = System.Drawing.Color.Gray;
            this.lblStatTotalTitle.Location = new System.Drawing.Point(0, 81);
            this.lblStatTotalTitle.Name = "lblStatTotalTitle";
            this.lblStatTotalTitle.Padding = new System.Windows.Forms.Padding(15, 0, 0, 10);
            this.lblStatTotalTitle.Size = new System.Drawing.Size(267, 25);
            this.lblStatTotalTitle.TabIndex = 3;
            this.lblStatTotalTitle.Text = "Tổng số lần nộp";
            // 
            // pnlStatAccepted
            // 
            this.pnlStatAccepted.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(245)))), ((int)(((byte)(233)))));
            this.pnlStatAccepted.Controls.Add(this.stripAccepted);
            this.pnlStatAccepted.Controls.Add(this.lblIconAccepted);
            this.pnlStatAccepted.Controls.Add(this.lblStatAcceptedValue);
            this.pnlStatAccepted.Controls.Add(this.lblStatAcceptedTitle);
            this.pnlStatAccepted.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlStatAccepted.Location = new System.Drawing.Point(297, 0);
            this.pnlStatAccepted.Margin = new System.Windows.Forms.Padding(0, 0, 15, 0);
            this.pnlStatAccepted.Name = "pnlStatAccepted";
            this.pnlStatAccepted.Size = new System.Drawing.Size(267, 106);
            this.pnlStatAccepted.TabIndex = 1;
            // 
            // stripAccepted
            // 
            this.stripAccepted.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(76)))), ((int)(((byte)(175)))), ((int)(((byte)(80)))));
            this.stripAccepted.Dock = System.Windows.Forms.DockStyle.Left;
            this.stripAccepted.Location = new System.Drawing.Point(0, 0);
            this.stripAccepted.Name = "stripAccepted";
            this.stripAccepted.Size = new System.Drawing.Size(4, 81);
            this.stripAccepted.TabIndex = 0;
            // 
            // lblIconAccepted
            // 
            this.lblIconAccepted.AutoSize = true;
            this.lblIconAccepted.Font = new System.Drawing.Font("Segoe UI Emoji", 20F);
            this.lblIconAccepted.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(76)))), ((int)(((byte)(175)))), ((int)(((byte)(80)))));
            this.lblIconAccepted.Location = new System.Drawing.Point(2, 23);
            this.lblIconAccepted.Name = "lblIconAccepted";
            this.lblIconAccepted.Size = new System.Drawing.Size(45, 46);
            this.lblIconAccepted.TabIndex = 1;
            this.lblIconAccepted.Text = "✓";
            // 
            // lblStatAcceptedValue
            // 
            this.lblStatAcceptedValue.AutoSize = true;
            this.lblStatAcceptedValue.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold);
            this.lblStatAcceptedValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(76)))), ((int)(((byte)(175)))), ((int)(((byte)(80)))));
            this.lblStatAcceptedValue.Location = new System.Drawing.Point(94, 25);
            this.lblStatAcceptedValue.Name = "lblStatAcceptedValue";
            this.lblStatAcceptedValue.Size = new System.Drawing.Size(40, 46);
            this.lblStatAcceptedValue.TabIndex = 2;
            this.lblStatAcceptedValue.Text = "0";
            // 
            // lblStatAcceptedTitle
            // 
            this.lblStatAcceptedTitle.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblStatAcceptedTitle.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblStatAcceptedTitle.ForeColor = System.Drawing.Color.Gray;
            this.lblStatAcceptedTitle.Location = new System.Drawing.Point(0, 81);
            this.lblStatAcceptedTitle.Name = "lblStatAcceptedTitle";
            this.lblStatAcceptedTitle.Padding = new System.Windows.Forms.Padding(15, 0, 0, 10);
            this.lblStatAcceptedTitle.Size = new System.Drawing.Size(267, 25);
            this.lblStatAcceptedTitle.TabIndex = 3;
            this.lblStatAcceptedTitle.Text = "Accepted";
            // 
            // pnlStatWA
            // 
            this.pnlStatWA.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(250)))), ((int)(((byte)(235)))));
            this.pnlStatWA.Controls.Add(this.stripWA);
            this.pnlStatWA.Controls.Add(this.lblIconWA);
            this.pnlStatWA.Controls.Add(this.lblStatWAValue);
            this.pnlStatWA.Controls.Add(this.lblStatWATitle);
            this.pnlStatWA.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlStatWA.Location = new System.Drawing.Point(579, 0);
            this.pnlStatWA.Margin = new System.Windows.Forms.Padding(0, 0, 15, 0);
            this.pnlStatWA.Name = "pnlStatWA";
            this.pnlStatWA.Size = new System.Drawing.Size(267, 106);
            this.pnlStatWA.TabIndex = 2;
            // 
            // stripWA
            // 
            this.stripWA.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(152)))), ((int)(((byte)(0)))));
            this.stripWA.Dock = System.Windows.Forms.DockStyle.Left;
            this.stripWA.Location = new System.Drawing.Point(0, 0);
            this.stripWA.Name = "stripWA";
            this.stripWA.Size = new System.Drawing.Size(4, 81);
            this.stripWA.TabIndex = 0;
            // 
            // lblIconWA
            // 
            this.lblIconWA.AutoSize = true;
            this.lblIconWA.Font = new System.Drawing.Font("Segoe UI Emoji", 20F);
            this.lblIconWA.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(152)))), ((int)(((byte)(0)))));
            this.lblIconWA.Location = new System.Drawing.Point(2, 23);
            this.lblIconWA.Name = "lblIconWA";
            this.lblIconWA.Size = new System.Drawing.Size(48, 46);
            this.lblIconWA.TabIndex = 1;
            this.lblIconWA.Text = "✗";
            // 
            // lblStatWAValue
            // 
            this.lblStatWAValue.AutoSize = true;
            this.lblStatWAValue.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold);
            this.lblStatWAValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(152)))), ((int)(((byte)(0)))));
            this.lblStatWAValue.Location = new System.Drawing.Point(94, 25);
            this.lblStatWAValue.Name = "lblStatWAValue";
            this.lblStatWAValue.Size = new System.Drawing.Size(40, 46);
            this.lblStatWAValue.TabIndex = 2;
            this.lblStatWAValue.Text = "0";
            // 
            // lblStatWATitle
            // 
            this.lblStatWATitle.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblStatWATitle.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblStatWATitle.ForeColor = System.Drawing.Color.Gray;
            this.lblStatWATitle.Location = new System.Drawing.Point(0, 81);
            this.lblStatWATitle.Name = "lblStatWATitle";
            this.lblStatWATitle.Padding = new System.Windows.Forms.Padding(15, 0, 0, 10);
            this.lblStatWATitle.Size = new System.Drawing.Size(267, 25);
            this.lblStatWATitle.TabIndex = 3;
            this.lblStatWATitle.Text = "Wrong Answer";
            // 
            // pnlStatError
            // 
            this.pnlStatError.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(235)))), ((int)(((byte)(238)))));
            this.pnlStatError.Controls.Add(this.stripError);
            this.pnlStatError.Controls.Add(this.lblIconError);
            this.pnlStatError.Controls.Add(this.lblStatErrorValue);
            this.pnlStatError.Controls.Add(this.lblStatErrorTitle);
            this.pnlStatError.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlStatError.Location = new System.Drawing.Point(861, 0);
            this.pnlStatError.Margin = new System.Windows.Forms.Padding(0);
            this.pnlStatError.Name = "pnlStatError";
            this.pnlStatError.Size = new System.Drawing.Size(284, 106);
            this.pnlStatError.TabIndex = 3;
            // 
            // stripError
            // 
            this.stripError.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(67)))), ((int)(((byte)(54)))));
            this.stripError.Dock = System.Windows.Forms.DockStyle.Left;
            this.stripError.Location = new System.Drawing.Point(0, 0);
            this.stripError.Name = "stripError";
            this.stripError.Size = new System.Drawing.Size(4, 81);
            this.stripError.TabIndex = 0;
            // 
            // lblIconError
            // 
            this.lblIconError.AutoSize = true;
            this.lblIconError.Font = new System.Drawing.Font("Segoe UI Emoji", 20F);
            this.lblIconError.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(67)))), ((int)(((byte)(54)))));
            this.lblIconError.Location = new System.Drawing.Point(2, 23);
            this.lblIconError.Name = "lblIconError";
            this.lblIconError.Size = new System.Drawing.Size(67, 46);
            this.lblIconError.TabIndex = 1;
            this.lblIconError.Text = "⚠️";
            // 
            // lblStatErrorValue
            // 
            this.lblStatErrorValue.AutoSize = true;
            this.lblStatErrorValue.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold);
            this.lblStatErrorValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(67)))), ((int)(((byte)(54)))));
            this.lblStatErrorValue.Location = new System.Drawing.Point(94, 25);
            this.lblStatErrorValue.Name = "lblStatErrorValue";
            this.lblStatErrorValue.Size = new System.Drawing.Size(40, 46);
            this.lblStatErrorValue.TabIndex = 2;
            this.lblStatErrorValue.Text = "0";
            // 
            // lblStatErrorTitle
            // 
            this.lblStatErrorTitle.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblStatErrorTitle.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblStatErrorTitle.ForeColor = System.Drawing.Color.Gray;
            this.lblStatErrorTitle.Location = new System.Drawing.Point(0, 81);
            this.lblStatErrorTitle.Name = "lblStatErrorTitle";
            this.lblStatErrorTitle.Padding = new System.Windows.Forms.Padding(15, 0, 0, 10);
            this.lblStatErrorTitle.Size = new System.Drawing.Size(284, 25);
            this.lblStatErrorTitle.TabIndex = 3;
            this.lblStatErrorTitle.Text = "Lỗi Runtime/Compile";
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(33)))), ((int)(((byte)(33)))));
            this.lblTitle.Location = new System.Drawing.Point(20, 20);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Padding = new System.Windows.Forms.Padding(0, 0, 0, 20);
            this.lblTitle.Size = new System.Drawing.Size(215, 58);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Lịch sử nộp bài";
            // 
            // lblSummary
            // 
            this.lblSummary.BackColor = System.Drawing.Color.Transparent;
            this.lblSummary.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblSummary.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSummary.ForeColor = System.Drawing.Color.Gray;
            this.lblSummary.Location = new System.Drawing.Point(0, 494);
            this.lblSummary.Name = "lblSummary";
            this.lblSummary.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.lblSummary.Size = new System.Drawing.Size(1160, 24);
            this.lblSummary.TabIndex = 1;
            this.lblSummary.Text = "Tổng số lần nộp: 0";
            this.lblSummary.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // colHash
            // 
            this.colHash.FillWeight = 32.08556F;
            this.colHash.HeaderText = "#";
            this.colHash.MinimumWidth = 6;
            this.colHash.Name = "colHash";
            this.colHash.ReadOnly = true;
            // 
            // colProblemName
            // 
            this.colProblemName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colProblemName.FillWeight = 113.5829F;
            this.colProblemName.HeaderText = "Bài tập";
            this.colProblemName.MinimumWidth = 6;
            this.colProblemName.Name = "colProblemName";
            this.colProblemName.ReadOnly = true;
            // 
            // colTimestamp
            // 
            this.colTimestamp.FillWeight = 113.5829F;
            this.colTimestamp.HeaderText = "Thời gian nộp";
            this.colTimestamp.MinimumWidth = 6;
            this.colTimestamp.Name = "colTimestamp";
            this.colTimestamp.ReadOnly = true;
            // 
            // colStatus
            // 
            this.colStatus.FillWeight = 113.5829F;
            this.colStatus.HeaderText = "Trạng thái";
            this.colStatus.MinimumWidth = 6;
            this.colStatus.Name = "colStatus";
            this.colStatus.ReadOnly = true;
            // 
            // colScore
            // 
            this.colScore.FillWeight = 113.5829F;
            this.colScore.HeaderText = "Điểm";
            this.colScore.MinimumWidth = 6;
            this.colScore.Name = "colScore";
            this.colScore.ReadOnly = true;
            // 
            // colView
            // 
            this.colView.FillWeight = 113.5829F;
            this.colView.HeaderText = "Thao tác";
            this.colView.MinimumWidth = 6;
            this.colView.Name = "colView";
            this.colView.ReadOnly = true;
            // 
            // ucSubmissions
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(250)))), ((int)(((byte)(250)))));
            this.Controls.Add(this.pnlMain);
            this.Name = "ucSubmissions";
            this.Size = new System.Drawing.Size(1200, 800);
            this.pnlMain.ResumeLayout(false);
            this.pnlMain.PerformLayout();
            this.pnlGridContainer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvSubmissions)).EndInit();
            this.pnlFilters.ResumeLayout(false);
            this.pnlFilters.PerformLayout();
            this.pnlStatsContainer.ResumeLayout(false);
            this.tblStatsLayout.ResumeLayout(false);
            this.pnlStatTotal.ResumeLayout(false);
            this.pnlStatTotal.PerformLayout();
            this.pnlStatAccepted.ResumeLayout(false);
            this.pnlStatAccepted.PerformLayout();
            this.pnlStatWA.ResumeLayout(false);
            this.pnlStatWA.PerformLayout();
            this.pnlStatError.ResumeLayout(false);
            this.pnlStatError.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        // Controls
        private System.Windows.Forms.Panel pnlMain;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Panel pnlStatsContainer;
        private System.Windows.Forms.TableLayoutPanel tblStatsLayout;

        // Stats Cards
        private System.Windows.Forms.Panel pnlStatTotal;
        private System.Windows.Forms.Label lblStatTotalValue;
        private System.Windows.Forms.Label lblStatTotalTitle;
        private System.Windows.Forms.Label lblIconTotal;
        private System.Windows.Forms.Panel stripTotal;

        private System.Windows.Forms.Panel pnlStatAccepted;
        private System.Windows.Forms.Label lblStatAcceptedValue;
        private System.Windows.Forms.Label lblStatAcceptedTitle;
        private System.Windows.Forms.Label lblIconAccepted;
        private System.Windows.Forms.Panel stripAccepted;

        private System.Windows.Forms.Panel pnlStatWA;
        private System.Windows.Forms.Label lblStatWAValue;
        private System.Windows.Forms.Label lblStatWATitle;
        private System.Windows.Forms.Label lblIconWA;
        private System.Windows.Forms.Panel stripWA;

        private System.Windows.Forms.Panel pnlStatError;
        private System.Windows.Forms.Label lblStatErrorValue;
        private System.Windows.Forms.Label lblStatErrorTitle;
        private System.Windows.Forms.Label lblIconError;
        private System.Windows.Forms.Panel stripError;

        // Filters
        private System.Windows.Forms.Panel pnlFilters;
        private System.Windows.Forms.Button btnApplyFilter;
        private System.Windows.Forms.ComboBox cmbStatus;
        private System.Windows.Forms.ComboBox cmbProblems;
        private System.Windows.Forms.Label lblFilter;

        // Grid
        private System.Windows.Forms.Panel pnlGridContainer;
        private System.Windows.Forms.DataGridView dgvSubmissions;
        private System.Windows.Forms.Label lblSummary;
        private System.Windows.Forms.DataGridViewTextBoxColumn colHash;
        private System.Windows.Forms.DataGridViewTextBoxColumn colProblemName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTimestamp;
        private System.Windows.Forms.DataGridViewTextBoxColumn colStatus;
        private System.Windows.Forms.DataGridViewTextBoxColumn colScore;
        private System.Windows.Forms.DataGridViewTextBoxColumn colView;
    }
}