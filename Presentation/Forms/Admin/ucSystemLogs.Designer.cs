namespace CodeForge_Desktop.Presentation.Forms.Admin
{
    partial class ucSystemLogs
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.pnlTop = new System.Windows.Forms.Panel();
            this.pnlStats = new System.Windows.Forms.Panel();
            this.lblTotalLogs = new System.Windows.Forms.Label();
            this.flowBadges = new System.Windows.Forms.FlowLayoutPanel();
            this.pnlInfoBadge = new System.Windows.Forms.Panel();
            this.lblInfoLabel = new System.Windows.Forms.Label();
            this.lblInfoCount = new System.Windows.Forms.Label();
            this.pnlWarningBadge = new System.Windows.Forms.Panel();
            this.lblWarningLabel = new System.Windows.Forms.Label();
            this.lblWarningCount = new System.Windows.Forms.Label();
            this.pnlErrorBadge = new System.Windows.Forms.Panel();
            this.lblErrorLabel = new System.Windows.Forms.Label();
            this.lblErrorCount = new System.Windows.Forms.Label();
            this.pnlActions = new System.Windows.Forms.Panel();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.btnExport = new System.Windows.Forms.Button();
            this.btnFilter = new System.Windows.Forms.Button();
            this.btnClearFilter = new System.Windows.Forms.Button();
            this.pnlFilters = new System.Windows.Forms.Panel();
            this.pnlFilterControls = new System.Windows.Forms.Panel();
            this.txtSearchKeyword = new System.Windows.Forms.TextBox();
            this.lblSearchLabel = new System.Windows.Forms.Label();
            this.dtpToDate = new System.Windows.Forms.DateTimePicker();
            this.lblDateSeparator = new System.Windows.Forms.Label();
            this.dtpFromDate = new System.Windows.Forms.DateTimePicker();
            this.lblDateLabel = new System.Windows.Forms.Label();
            this.cmbSource = new System.Windows.Forms.ComboBox();
            this.lblSourceLabel = new System.Windows.Forms.Label();
            this.cmbLevel = new System.Windows.Forms.ComboBox();
            this.lblLevelLabel = new System.Windows.Forms.Label();
            this.lblFilterTitle = new System.Windows.Forms.Label();
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.lblSubtitle = new System.Windows.Forms.Label();
            this.lblTitle = new System.Windows.Forms.Label();
            this.dgvLogs = new System.Windows.Forms.DataGridView();
            this.colTimestamp = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colLevel = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSource = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colMessage = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colUser = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pnlBottom = new System.Windows.Forms.Panel();
            this.pnlPagination = new System.Windows.Forms.Panel();
            this.lblPageSizeLabel = new System.Windows.Forms.Label();
            this.cmbPageSize = new System.Windows.Forms.ComboBox();
            this.btnNext = new System.Windows.Forms.Button();
            this.lblPageInfo = new System.Windows.Forms.Label();
            this.btnPrev = new System.Windows.Forms.Button();
            this.pnlTop.SuspendLayout();
            this.pnlStats.SuspendLayout();
            this.flowBadges.SuspendLayout();
            this.pnlInfoBadge.SuspendLayout();
            this.pnlWarningBadge.SuspendLayout();
            this.pnlErrorBadge.SuspendLayout();
            this.pnlActions.SuspendLayout();
            this.pnlFilters.SuspendLayout();
            this.pnlFilterControls.SuspendLayout();
            this.pnlHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLogs)).BeginInit();
            this.pnlBottom.SuspendLayout();
            this.pnlPagination.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlTop
            // 
            this.pnlTop.BackColor = System.Drawing.Color.White;
            this.pnlTop.Controls.Add(this.pnlStats);
            this.pnlTop.Controls.Add(this.pnlActions);
            this.pnlTop.Controls.Add(this.pnlFilters);
            this.pnlTop.Controls.Add(this.pnlHeader);
            this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTop.Location = new System.Drawing.Point(0, 0);
            this.pnlTop.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.pnlTop.Name = "pnlTop";
            this.pnlTop.Padding = new System.Windows.Forms.Padding(15, 16, 15, 16);
            this.pnlTop.Size = new System.Drawing.Size(900, 249);
            this.pnlTop.TabIndex = 0;
            // 
            // pnlStats
            // 
            this.pnlStats.Controls.Add(this.lblTotalLogs);
            this.pnlStats.Controls.Add(this.flowBadges);
            this.pnlStats.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlStats.Location = new System.Drawing.Point(15, 171);
            this.pnlStats.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.pnlStats.Name = "pnlStats";
            this.pnlStats.Padding = new System.Windows.Forms.Padding(0, 8, 0, 0);
            this.pnlStats.Size = new System.Drawing.Size(870, 62);
            this.pnlStats.TabIndex = 3;
            // 
            // lblTotalLogs
            // 
            this.lblTotalLogs.Dock = System.Windows.Forms.DockStyle.Right;
            this.lblTotalLogs.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblTotalLogs.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.lblTotalLogs.Location = new System.Drawing.Point(685, 8);
            this.lblTotalLogs.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblTotalLogs.Name = "lblTotalLogs";
            this.lblTotalLogs.Size = new System.Drawing.Size(185, 54);
            this.lblTotalLogs.TabIndex = 1;
            this.lblTotalLogs.Text = "📊 Tổng số: 0 bản ghi";
            this.lblTotalLogs.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // flowBadges
            // 
            this.flowBadges.Controls.Add(this.pnlInfoBadge);
            this.flowBadges.Controls.Add(this.pnlWarningBadge);
            this.flowBadges.Controls.Add(this.pnlErrorBadge);
            this.flowBadges.Dock = System.Windows.Forms.DockStyle.Left;
            this.flowBadges.Location = new System.Drawing.Point(0, 8);
            this.flowBadges.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.flowBadges.Name = "flowBadges";
            this.flowBadges.Size = new System.Drawing.Size(525, 54);
            this.flowBadges.TabIndex = 0;
            this.flowBadges.WrapContents = false;
            // 
            // pnlInfoBadge
            // 
            this.pnlInfoBadge.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(245)))), ((int)(((byte)(254)))));
            this.pnlInfoBadge.Controls.Add(this.lblInfoLabel);
            this.pnlInfoBadge.Controls.Add(this.lblInfoCount);
            this.pnlInfoBadge.Location = new System.Drawing.Point(0, 2);
            this.pnlInfoBadge.Margin = new System.Windows.Forms.Padding(0, 2, 11, 2);
            this.pnlInfoBadge.Name = "pnlInfoBadge";
            this.pnlInfoBadge.Padding = new System.Windows.Forms.Padding(11, 4, 11, 4);
            this.pnlInfoBadge.Size = new System.Drawing.Size(135, 28);
            this.pnlInfoBadge.TabIndex = 0;
            // 
            // lblInfoLabel
            // 
            this.lblInfoLabel.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblInfoLabel.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblInfoLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(3)))), ((int)(((byte)(169)))), ((int)(((byte)(244)))));
            this.lblInfoLabel.Location = new System.Drawing.Point(11, 4);
            this.lblInfoLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblInfoLabel.Name = "lblInfoLabel";
            this.lblInfoLabel.Size = new System.Drawing.Size(68, 20);
            this.lblInfoLabel.TabIndex = 0;
            this.lblInfoLabel.Text = "ℹ️ Thông tin";
            this.lblInfoLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblInfoCount
            // 
            this.lblInfoCount.Dock = System.Windows.Forms.DockStyle.Right;
            this.lblInfoCount.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblInfoCount.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(3)))), ((int)(((byte)(169)))), ((int)(((byte)(244)))));
            this.lblInfoCount.Location = new System.Drawing.Point(102, 4);
            this.lblInfoCount.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblInfoCount.Name = "lblInfoCount";
            this.lblInfoCount.Size = new System.Drawing.Size(22, 20);
            this.lblInfoCount.TabIndex = 1;
            this.lblInfoCount.Text = "0";
            this.lblInfoCount.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // pnlWarningBadge
            // 
            this.pnlWarningBadge.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(248)))), ((int)(((byte)(225)))));
            this.pnlWarningBadge.Controls.Add(this.lblWarningLabel);
            this.pnlWarningBadge.Controls.Add(this.lblWarningCount);
            this.pnlWarningBadge.Location = new System.Drawing.Point(146, 2);
            this.pnlWarningBadge.Margin = new System.Windows.Forms.Padding(0, 2, 11, 2);
            this.pnlWarningBadge.Name = "pnlWarningBadge";
            this.pnlWarningBadge.Padding = new System.Windows.Forms.Padding(11, 4, 11, 4);
            this.pnlWarningBadge.Size = new System.Drawing.Size(135, 28);
            this.pnlWarningBadge.TabIndex = 1;
            // 
            // lblWarningLabel
            // 
            this.lblWarningLabel.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblWarningLabel.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblWarningLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(152)))), ((int)(((byte)(0)))));
            this.lblWarningLabel.Location = new System.Drawing.Point(11, 4);
            this.lblWarningLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblWarningLabel.Name = "lblWarningLabel";
            this.lblWarningLabel.Size = new System.Drawing.Size(68, 20);
            this.lblWarningLabel.TabIndex = 0;
            this.lblWarningLabel.Text = "⚠️ Cảnh báo";
            this.lblWarningLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblWarningCount
            // 
            this.lblWarningCount.Dock = System.Windows.Forms.DockStyle.Right;
            this.lblWarningCount.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblWarningCount.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(152)))), ((int)(((byte)(0)))));
            this.lblWarningCount.Location = new System.Drawing.Point(102, 4);
            this.lblWarningCount.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblWarningCount.Name = "lblWarningCount";
            this.lblWarningCount.Size = new System.Drawing.Size(22, 20);
            this.lblWarningCount.TabIndex = 1;
            this.lblWarningCount.Text = "0";
            this.lblWarningCount.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // pnlErrorBadge
            // 
            this.pnlErrorBadge.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(235)))), ((int)(((byte)(238)))));
            this.pnlErrorBadge.Controls.Add(this.lblErrorLabel);
            this.pnlErrorBadge.Controls.Add(this.lblErrorCount);
            this.pnlErrorBadge.Location = new System.Drawing.Point(292, 2);
            this.pnlErrorBadge.Margin = new System.Windows.Forms.Padding(0, 2, 11, 2);
            this.pnlErrorBadge.Name = "pnlErrorBadge";
            this.pnlErrorBadge.Padding = new System.Windows.Forms.Padding(11, 4, 11, 4);
            this.pnlErrorBadge.Size = new System.Drawing.Size(135, 28);
            this.pnlErrorBadge.TabIndex = 2;
            // 
            // lblErrorLabel
            // 
            this.lblErrorLabel.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblErrorLabel.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblErrorLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(233)))), ((int)(((byte)(30)))), ((int)(((byte)(99)))));
            this.lblErrorLabel.Location = new System.Drawing.Point(11, 4);
            this.lblErrorLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblErrorLabel.Name = "lblErrorLabel";
            this.lblErrorLabel.Size = new System.Drawing.Size(68, 20);
            this.lblErrorLabel.TabIndex = 0;
            this.lblErrorLabel.Text = "🚨 Lỗi";
            this.lblErrorLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblErrorCount
            // 
            this.lblErrorCount.Dock = System.Windows.Forms.DockStyle.Right;
            this.lblErrorCount.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblErrorCount.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(233)))), ((int)(((byte)(30)))), ((int)(((byte)(99)))));
            this.lblErrorCount.Location = new System.Drawing.Point(102, 4);
            this.lblErrorCount.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblErrorCount.Name = "lblErrorCount";
            this.lblErrorCount.Size = new System.Drawing.Size(22, 20);
            this.lblErrorCount.TabIndex = 1;
            this.lblErrorCount.Text = "0";
            this.lblErrorCount.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // pnlActions
            // 
            this.pnlActions.Controls.Add(this.btnRefresh);
            this.pnlActions.Controls.Add(this.btnExport);
            this.pnlActions.Controls.Add(this.btnFilter);
            this.pnlActions.Controls.Add(this.btnClearFilter);
            this.pnlActions.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlActions.Location = new System.Drawing.Point(15, 130);
            this.pnlActions.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.pnlActions.Name = "pnlActions";
            this.pnlActions.Padding = new System.Windows.Forms.Padding(0, 12, 0, 0);
            this.pnlActions.Size = new System.Drawing.Size(870, 41);
            this.pnlActions.TabIndex = 2;
            // 
            // btnRefresh
            // 
            this.btnRefresh.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(13)))), ((int)(((byte)(110)))), ((int)(((byte)(253)))));
            this.btnRefresh.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRefresh.FlatAppearance.BorderSize = 0;
            this.btnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRefresh.Font = new System.Drawing.Font("Segoe UI Semibold", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnRefresh.ForeColor = System.Drawing.Color.White;
            this.btnRefresh.Location = new System.Drawing.Point(0, 12);
            this.btnRefresh.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(98, 28);
            this.btnRefresh.TabIndex = 0;
            this.btnRefresh.Text = "🔄 Làm mới";
            this.btnRefresh.UseVisualStyleBackColor = false;
            // 
            // btnExport
            // 
            this.btnExport.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(135)))), ((int)(((byte)(84)))));
            this.btnExport.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnExport.FlatAppearance.BorderSize = 0;
            this.btnExport.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExport.Font = new System.Drawing.Font("Segoe UI Semibold", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnExport.ForeColor = System.Drawing.Color.White;
            this.btnExport.Location = new System.Drawing.Point(105, 12);
            this.btnExport.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(98, 28);
            this.btnExport.TabIndex = 1;
            this.btnExport.Text = "📥 Xuất CSV";
            this.btnExport.UseVisualStyleBackColor = false;
            // 
            // btnFilter
            // 
            this.btnFilter.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(111)))), ((int)(((byte)(66)))), ((int)(((byte)(193)))));
            this.btnFilter.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnFilter.FlatAppearance.BorderSize = 0;
            this.btnFilter.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFilter.Font = new System.Drawing.Font("Segoe UI Semibold", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnFilter.ForeColor = System.Drawing.Color.White;
            this.btnFilter.Location = new System.Drawing.Point(210, 12);
            this.btnFilter.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnFilter.Name = "btnFilter";
            this.btnFilter.Size = new System.Drawing.Size(98, 28);
            this.btnFilter.TabIndex = 2;
            this.btnFilter.Text = "🔍 Áp dụng lọc";
            this.btnFilter.UseVisualStyleBackColor = false;
            // 
            // btnClearFilter
            // 
            this.btnClearFilter.BackColor = System.Drawing.Color.White;
            this.btnClearFilter.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnClearFilter.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.btnClearFilter.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClearFilter.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnClearFilter.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.btnClearFilter.Location = new System.Drawing.Point(315, 12);
            this.btnClearFilter.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnClearFilter.Name = "btnClearFilter";
            this.btnClearFilter.Size = new System.Drawing.Size(98, 28);
            this.btnClearFilter.TabIndex = 3;
            this.btnClearFilter.Text = "✖ Xóa bộ lọc";
            this.btnClearFilter.UseVisualStyleBackColor = false;
            // 
            // pnlFilters
            // 
            this.pnlFilters.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(249)))), ((int)(((byte)(250)))));
            this.pnlFilters.Controls.Add(this.pnlFilterControls);
            this.pnlFilters.Controls.Add(this.lblFilterTitle);
            this.pnlFilters.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlFilters.Location = new System.Drawing.Point(15, 57);
            this.pnlFilters.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.pnlFilters.Name = "pnlFilters";
            this.pnlFilters.Padding = new System.Windows.Forms.Padding(11, 8, 11, 8);
            this.pnlFilters.Size = new System.Drawing.Size(870, 73);
            this.pnlFilters.TabIndex = 1;
            // 
            // pnlFilterControls
            // 
            this.pnlFilterControls.Controls.Add(this.txtSearchKeyword);
            this.pnlFilterControls.Controls.Add(this.lblSearchLabel);
            this.pnlFilterControls.Controls.Add(this.dtpToDate);
            this.pnlFilterControls.Controls.Add(this.lblDateSeparator);
            this.pnlFilterControls.Controls.Add(this.dtpFromDate);
            this.pnlFilterControls.Controls.Add(this.lblDateLabel);
            this.pnlFilterControls.Controls.Add(this.cmbSource);
            this.pnlFilterControls.Controls.Add(this.lblSourceLabel);
            this.pnlFilterControls.Controls.Add(this.cmbLevel);
            this.pnlFilterControls.Controls.Add(this.lblLevelLabel);
            this.pnlFilterControls.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlFilterControls.Location = new System.Drawing.Point(11, 8);
            this.pnlFilterControls.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.pnlFilterControls.Name = "pnlFilterControls";
            this.pnlFilterControls.Size = new System.Drawing.Size(848, 57);
            this.pnlFilterControls.TabIndex = 1;
            // 
            // txtSearchKeyword
            // 
            this.txtSearchKeyword.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtSearchKeyword.ForeColor = System.Drawing.Color.Gray;
            this.txtSearchKeyword.Location = new System.Drawing.Point(674, 4);
            this.txtSearchKeyword.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtSearchKeyword.Name = "txtSearchKeyword";
            this.txtSearchKeyword.Size = new System.Drawing.Size(166, 23);
            this.txtSearchKeyword.TabIndex = 9;
            this.txtSearchKeyword.Text = "Nhập từ khóa...";
            // 
            // lblSearchLabel
            // 
            this.lblSearchLabel.AutoSize = true;
            this.lblSearchLabel.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblSearchLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.lblSearchLabel.Location = new System.Drawing.Point(611, 6);
            this.lblSearchLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblSearchLabel.Name = "lblSearchLabel";
            this.lblSearchLabel.Size = new System.Drawing.Size(60, 15);
            this.lblSearchLabel.TabIndex = 8;
            this.lblSearchLabel.Text = "Tìm kiếm:";
            // 
            // dtpToDate
            // 
            this.dtpToDate.CalendarFont = new System.Drawing.Font("Segoe UI", 9F);
            this.dtpToDate.CustomFormat = "dd/MM/yyyy";
            this.dtpToDate.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.dtpToDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpToDate.Location = new System.Drawing.Point(514, 4);
            this.dtpToDate.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.dtpToDate.Name = "dtpToDate";
            this.dtpToDate.Size = new System.Drawing.Size(84, 23);
            this.dtpToDate.TabIndex = 7;
            // 
            // lblDateSeparator
            // 
            this.lblDateSeparator.AutoSize = true;
            this.lblDateSeparator.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblDateSeparator.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.lblDateSeparator.Location = new System.Drawing.Point(492, 6);
            this.lblDateSeparator.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblDateSeparator.Name = "lblDateSeparator";
            this.lblDateSeparator.Size = new System.Drawing.Size(17, 15);
            this.lblDateSeparator.TabIndex = 6;
            this.lblDateSeparator.Text = "→";
            // 
            // dtpFromDate
            // 
            this.dtpFromDate.CalendarFont = new System.Drawing.Font("Segoe UI", 9F);
            this.dtpFromDate.CustomFormat = "dd/MM/yyyy";
            this.dtpFromDate.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.dtpFromDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFromDate.Location = new System.Drawing.Point(405, 4);
            this.dtpFromDate.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.dtpFromDate.Name = "dtpFromDate";
            this.dtpFromDate.Size = new System.Drawing.Size(84, 23);
            this.dtpFromDate.TabIndex = 5;
            // 
            // lblDateLabel
            // 
            this.lblDateLabel.AutoSize = true;
            this.lblDateLabel.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblDateLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.lblDateLabel.Location = new System.Drawing.Point(340, 6);
            this.lblDateLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblDateLabel.Name = "lblDateLabel";
            this.lblDateLabel.Size = new System.Drawing.Size(60, 15);
            this.lblDateLabel.TabIndex = 4;
            this.lblDateLabel.Text = "Thời gian:";
            // 
            // cmbSource
            // 
            this.cmbSource.BackColor = System.Drawing.Color.White;
            this.cmbSource.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSource.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.cmbSource.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.cmbSource.FormattingEnabled = true;
            this.cmbSource.Items.AddRange(new object[] {
            "Tất cả",
            "Xác thực",
            "Hệ thống",
            "Cơ sở dữ liệu",
            "Bài tập",
            "Quản trị"});
            this.cmbSource.Location = new System.Drawing.Point(220, 4);
            this.cmbSource.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.cmbSource.Name = "cmbSource";
            this.cmbSource.Size = new System.Drawing.Size(106, 23);
            this.cmbSource.TabIndex = 3;
            // 
            // lblSourceLabel
            // 
            this.lblSourceLabel.AutoSize = true;
            this.lblSourceLabel.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblSourceLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.lblSourceLabel.Location = new System.Drawing.Point(165, 6);
            this.lblSourceLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblSourceLabel.Name = "lblSourceLabel";
            this.lblSourceLabel.Size = new System.Drawing.Size(47, 15);
            this.lblSourceLabel.TabIndex = 2;
            this.lblSourceLabel.Text = "Nguồn:";
            // 
            // cmbLevel
            // 
            this.cmbLevel.BackColor = System.Drawing.Color.White;
            this.cmbLevel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbLevel.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.cmbLevel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.cmbLevel.FormattingEnabled = true;
            this.cmbLevel.Items.AddRange(new object[] {
            "Tất cả",
            "INFO",
            "WARNING",
            "ERROR"});
            this.cmbLevel.Location = new System.Drawing.Point(60, 4);
            this.cmbLevel.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.cmbLevel.Name = "cmbLevel";
            this.cmbLevel.Size = new System.Drawing.Size(91, 23);
            this.cmbLevel.TabIndex = 1;
            // 
            // lblLevelLabel
            // 
            this.lblLevelLabel.AutoSize = true;
            this.lblLevelLabel.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblLevelLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.lblLevelLabel.Location = new System.Drawing.Point(4, 6);
            this.lblLevelLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblLevelLabel.Name = "lblLevelLabel";
            this.lblLevelLabel.Size = new System.Drawing.Size(51, 15);
            this.lblLevelLabel.TabIndex = 0;
            this.lblLevelLabel.Text = "Mức độ:";
            // 
            // lblFilterTitle
            // 
            this.lblFilterTitle.AutoSize = true;
            this.lblFilterTitle.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblFilterTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.lblFilterTitle.Location = new System.Drawing.Point(11, 8);
            this.lblFilterTitle.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblFilterTitle.Name = "lblFilterTitle";
            this.lblFilterTitle.Size = new System.Drawing.Size(64, 15);
            this.lblFilterTitle.TabIndex = 0;
            this.lblFilterTitle.Text = "🔍 BỘ LỌC";
            // 
            // pnlHeader
            // 
            this.pnlHeader.Controls.Add(this.lblSubtitle);
            this.pnlHeader.Controls.Add(this.lblTitle);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Location = new System.Drawing.Point(15, 16);
            this.pnlHeader.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size = new System.Drawing.Size(870, 41);
            this.pnlHeader.TabIndex = 0;
            // 
            // lblSubtitle
            // 
            this.lblSubtitle.AutoSize = true;
            this.lblSubtitle.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblSubtitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(120)))), ((int)(((byte)(120)))));
            this.lblSubtitle.Location = new System.Drawing.Point(2, 28);
            this.lblSubtitle.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblSubtitle.Name = "lblSubtitle";
            this.lblSubtitle.Size = new System.Drawing.Size(263, 15);
            this.lblSubtitle.TabIndex = 1;
            this.lblSubtitle.Text = "Theo dõi và quản lý các hoạt động của hệ thống";
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.lblTitle.Location = new System.Drawing.Point(0, 0);
            this.lblTitle.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(239, 30);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "📋 Nhật Ký Hệ Thống";
            // 
            // dgvLogs
            // 
            this.dgvLogs.AllowUserToAddRows = false;
            this.dgvLogs.AllowUserToDeleteRows = false;
            this.dgvLogs.BackgroundColor = System.Drawing.Color.White;
            this.dgvLogs.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvLogs.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgvLogs.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(249)))), ((int)(((byte)(250)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            dataGridViewCellStyle1.Padding = new System.Windows.Forms.Padding(15, 0, 0, 0);
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(249)))), ((int)(((byte)(250)))));
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvLogs.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvLogs.ColumnHeadersHeight = 50;
            this.dgvLogs.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvLogs.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colTimestamp,
            this.colLevel,
            this.colSource,
            this.colMessage,
            this.colUser});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            dataGridViewCellStyle2.Padding = new System.Windows.Forms.Padding(15, 0, 0, 0);
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(240)))), ((int)(((byte)(254)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvLogs.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvLogs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvLogs.EnableHeadersVisualStyles = false;
            this.dgvLogs.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(235)))), ((int)(((byte)(235)))));
            this.dgvLogs.Location = new System.Drawing.Point(0, 249);
            this.dgvLogs.Margin = new System.Windows.Forms.Padding(15, 0, 15, 0);
            this.dgvLogs.Name = "dgvLogs";
            this.dgvLogs.ReadOnly = true;
            this.dgvLogs.RowHeadersVisible = false;
            this.dgvLogs.RowHeadersWidth = 51;
            this.dgvLogs.RowTemplate.Height = 50;
            this.dgvLogs.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvLogs.Size = new System.Drawing.Size(900, 303);
            this.dgvLogs.TabIndex = 1;
            // 
            // colTimestamp
            // 
            this.colTimestamp.HeaderText = "Thời gian";
            this.colTimestamp.MinimumWidth = 6;
            this.colTimestamp.Name = "colTimestamp";
            this.colTimestamp.ReadOnly = true;
            this.colTimestamp.Width = 180;
            // 
            // colLevel
            // 
            this.colLevel.HeaderText = "Mức độ";
            this.colLevel.MinimumWidth = 6;
            this.colLevel.Name = "colLevel";
            this.colLevel.ReadOnly = true;
            this.colLevel.Width = 130;
            // 
            // colSource
            // 
            this.colSource.HeaderText = "Nguồn";
            this.colSource.MinimumWidth = 6;
            this.colSource.Name = "colSource";
            this.colSource.ReadOnly = true;
            this.colSource.Width = 150;
            // 
            // colMessage
            // 
            this.colMessage.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colMessage.HeaderText = "Nội dung";
            this.colMessage.MinimumWidth = 6;
            this.colMessage.Name = "colMessage";
            this.colMessage.ReadOnly = true;
            // 
            // colUser
            // 
            this.colUser.HeaderText = "Người dùng";
            this.colUser.MinimumWidth = 6;
            this.colUser.Name = "colUser";
            this.colUser.ReadOnly = true;
            this.colUser.Width = 150;
            // 
            // pnlBottom
            // 
            this.pnlBottom.BackColor = System.Drawing.Color.White;
            this.pnlBottom.Controls.Add(this.pnlPagination);
            this.pnlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlBottom.Location = new System.Drawing.Point(0, 552);
            this.pnlBottom.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.pnlBottom.Name = "pnlBottom";
            this.pnlBottom.Padding = new System.Windows.Forms.Padding(15, 10, 15, 10);
            this.pnlBottom.Size = new System.Drawing.Size(900, 57);
            this.pnlBottom.TabIndex = 2;
            // 
            // pnlPagination
            // 
            this.pnlPagination.Controls.Add(this.lblPageSizeLabel);
            this.pnlPagination.Controls.Add(this.cmbPageSize);
            this.pnlPagination.Controls.Add(this.btnNext);
            this.pnlPagination.Controls.Add(this.lblPageInfo);
            this.pnlPagination.Controls.Add(this.btnPrev);
            this.pnlPagination.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlPagination.Location = new System.Drawing.Point(15, 10);
            this.pnlPagination.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.pnlPagination.Name = "pnlPagination";
            this.pnlPagination.Size = new System.Drawing.Size(870, 37);
            this.pnlPagination.TabIndex = 0;
            // 
            // lblPageSizeLabel
            // 
            this.lblPageSizeLabel.AutoSize = true;
            this.lblPageSizeLabel.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblPageSizeLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.lblPageSizeLabel.Location = new System.Drawing.Point(4, 12);
            this.lblPageSizeLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblPageSizeLabel.Name = "lblPageSizeLabel";
            this.lblPageSizeLabel.Size = new System.Drawing.Size(105, 15);
            this.lblPageSizeLabel.TabIndex = 3;
            this.lblPageSizeLabel.Text = "Số bản ghi / trang:";
            // 
            // cmbPageSize
            // 
            this.cmbPageSize.BackColor = System.Drawing.Color.White;
            this.cmbPageSize.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPageSize.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.cmbPageSize.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.cmbPageSize.FormattingEnabled = true;
            this.cmbPageSize.Items.AddRange(new object[] {
            "10",
            "15",
            "25",
            "50",
            "100"});
            this.cmbPageSize.Location = new System.Drawing.Point(106, 8);
            this.cmbPageSize.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.cmbPageSize.Name = "cmbPageSize";
            this.cmbPageSize.Size = new System.Drawing.Size(61, 23);
            this.cmbPageSize.TabIndex = 4;
            // 
            // btnNext
            // 
            this.btnNext.BackColor = System.Drawing.Color.White;
            this.btnNext.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnNext.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.btnNext.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNext.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnNext.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.btnNext.Location = new System.Drawing.Point(802, 6);
            this.btnNext.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(68, 26);
            this.btnNext.TabIndex = 2;
            this.btnNext.Text = "Sau ▶";
            this.btnNext.UseVisualStyleBackColor = false;
            // 
            // lblPageInfo
            // 
            this.lblPageInfo.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblPageInfo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.lblPageInfo.Location = new System.Drawing.Point(712, 6);
            this.lblPageInfo.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblPageInfo.Name = "lblPageInfo";
            this.lblPageInfo.Size = new System.Drawing.Size(82, 26);
            this.lblPageInfo.TabIndex = 1;
            this.lblPageInfo.Text = "Trang 1 / 1";
            this.lblPageInfo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnPrev
            // 
            this.btnPrev.BackColor = System.Drawing.Color.White;
            this.btnPrev.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPrev.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.btnPrev.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPrev.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnPrev.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.btnPrev.Location = new System.Drawing.Point(638, 6);
            this.btnPrev.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnPrev.Name = "btnPrev";
            this.btnPrev.Size = new System.Drawing.Size(68, 26);
            this.btnPrev.TabIndex = 0;
            this.btnPrev.Text = "◀ Trước";
            this.btnPrev.UseVisualStyleBackColor = false;
            // 
            // ucSystemLogs
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.Controls.Add(this.dgvLogs);
            this.Controls.Add(this.pnlBottom);
            this.Controls.Add(this.pnlTop);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "ucSystemLogs";
            this.Size = new System.Drawing.Size(900, 609);
            this.pnlTop.ResumeLayout(false);
            this.pnlStats.ResumeLayout(false);
            this.flowBadges.ResumeLayout(false);
            this.pnlInfoBadge.ResumeLayout(false);
            this.pnlWarningBadge.ResumeLayout(false);
            this.pnlErrorBadge.ResumeLayout(false);
            this.pnlActions.ResumeLayout(false);
            this.pnlFilters.ResumeLayout(false);
            this.pnlFilters.PerformLayout();
            this.pnlFilterControls.ResumeLayout(false);
            this.pnlFilterControls.PerformLayout();
            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLogs)).EndInit();
            this.pnlBottom.ResumeLayout(false);
            this.pnlPagination.ResumeLayout(false);
            this.pnlPagination.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlTop;
        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblSubtitle;

        private System.Windows.Forms.Panel pnlFilters;
        private System.Windows.Forms.Label lblFilterTitle;
        private System.Windows.Forms.Panel pnlFilterControls;
        private System.Windows.Forms.Label lblLevelLabel;
        private System.Windows.Forms.ComboBox cmbLevel;
        private System.Windows.Forms.Label lblSourceLabel;
        private System.Windows.Forms.ComboBox cmbSource;
        private System.Windows.Forms.Label lblDateLabel;
        private System.Windows.Forms.DateTimePicker dtpFromDate;
        private System.Windows.Forms.Label lblDateSeparator;
        private System.Windows.Forms.DateTimePicker dtpToDate;
        private System.Windows.Forms.Label lblSearchLabel;
        private System.Windows.Forms.TextBox txtSearchKeyword;

        private System.Windows.Forms.Panel pnlActions;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Button btnExport;
        private System.Windows.Forms.Button btnFilter;
        private System.Windows.Forms.Button btnClearFilter;

        private System.Windows.Forms.Panel pnlStats;
        private System.Windows.Forms.FlowLayoutPanel flowBadges;
        private System.Windows.Forms.Panel pnlInfoBadge;
        private System.Windows.Forms.Label lblInfoLabel;
        private System.Windows.Forms.Label lblInfoCount;
        private System.Windows.Forms.Panel pnlWarningBadge;
        private System.Windows.Forms.Label lblWarningLabel;
        private System.Windows.Forms.Label lblWarningCount;
        private System.Windows.Forms.Panel pnlErrorBadge;
        private System.Windows.Forms.Label lblErrorLabel;
        private System.Windows.Forms.Label lblErrorCount;
        private System.Windows.Forms.Label lblTotalLogs;

        private System.Windows.Forms.DataGridView dgvLogs;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTimestamp;
        private System.Windows.Forms.DataGridViewTextBoxColumn colLevel;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSource;
        private System.Windows.Forms.DataGridViewTextBoxColumn colMessage;
        private System.Windows.Forms.DataGridViewTextBoxColumn colUser;

        private System.Windows.Forms.Panel pnlBottom;
        private System.Windows.Forms.Panel pnlPagination;
        private System.Windows.Forms.Button btnPrev;
        private System.Windows.Forms.Label lblPageInfo;
        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.Label lblPageSizeLabel;
        private System.Windows.Forms.ComboBox cmbPageSize;
    }
}