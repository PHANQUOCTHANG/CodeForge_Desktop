namespace CodeForge_Desktop.Presentation.Forms.Admin
{
    partial class ucAdminDashboard
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.pnlMain = new System.Windows.Forms.Panel();
            this.pnlQuickAccess = new System.Windows.Forms.Panel();
            this.tblQuickAccess = new System.Windows.Forms.TableLayoutPanel();
            this.pnlQuickLogs = new System.Windows.Forms.Panel();
            this.lblQuickLogsDesc = new System.Windows.Forms.Label();
            this.lblQuickLogsTitle = new System.Windows.Forms.Label();
            this.lblQuickLogsIcon = new System.Windows.Forms.Label();
            this.pnlLogsAccent = new System.Windows.Forms.Panel();
            this.pnlQuickAssignments = new System.Windows.Forms.Panel();
            this.lblQuickAssignDesc = new System.Windows.Forms.Label();
            this.lblQuickAssignTitle = new System.Windows.Forms.Label();
            this.lblQuickAssignIcon = new System.Windows.Forms.Label();
            this.pnlAssignAccent = new System.Windows.Forms.Panel();
            this.pnlQuickUsers = new System.Windows.Forms.Panel();
            this.lblQuickUsersDesc = new System.Windows.Forms.Label();
            this.lblQuickUsersTitle = new System.Windows.Forms.Label();
            this.lblQuickUsersIcon = new System.Windows.Forms.Label();
            this.pnlUsersAccent = new System.Windows.Forms.Panel();
            this.lblQuickAccessTitle = new System.Windows.Forms.Label();
            this.pnlRecentActivity = new System.Windows.Forms.Panel();
            this.dgvRecentActivity = new System.Windows.Forms.DataGridView();
            this.colIndex = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colUser = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colAction = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDetail = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pnlActivityHeader = new System.Windows.Forms.Panel();
            this.lblActivitySubtitle = new System.Windows.Forms.Label();
            this.lblRecentActivityTitle = new System.Windows.Forms.Label();
            this.pnlStats = new System.Windows.Forms.Panel();
            this.tblStats = new System.Windows.Forms.TableLayoutPanel();
            this.pnlCardRate = new System.Windows.Forms.Panel();
            this.lblRateChange = new System.Windows.Forms.Label();
            this.pnlRateHeader = new System.Windows.Forms.Panel();
            this.lblRateTitle = new System.Windows.Forms.Label();
            this.lblIconRate = new System.Windows.Forms.Label();
            this.lblRateValue = new System.Windows.Forms.Label();
            this.pnlCardSubmissions = new System.Windows.Forms.Panel();
            this.lblSubmissionChange = new System.Windows.Forms.Label();
            this.pnlSubmissionHeader = new System.Windows.Forms.Panel();
            this.lblSubmissionTitle = new System.Windows.Forms.Label();
            this.lblIconSubmission = new System.Windows.Forms.Label();
            this.lblSubmissionCount = new System.Windows.Forms.Label();
            this.pnlCardAssignments = new System.Windows.Forms.Panel();
            this.lblAssignmentChange = new System.Windows.Forms.Label();
            this.pnlAssignmentHeader = new System.Windows.Forms.Panel();
            this.lblAssignmentTitle = new System.Windows.Forms.Label();
            this.lblIconAssignment = new System.Windows.Forms.Label();
            this.lblAssignmentCount = new System.Windows.Forms.Label();
            this.pnlCardUsers = new System.Windows.Forms.Panel();
            this.lblUserChange = new System.Windows.Forms.Label();
            this.pnlUserHeader = new System.Windows.Forms.Panel();
            this.lblUserTitle = new System.Windows.Forms.Label();
            this.lblIconUser = new System.Windows.Forms.Label();
            this.lblUserCount = new System.Windows.Forms.Label();
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.lblWelcomeTime = new System.Windows.Forms.Label();
            this.lblDashboardTitle = new System.Windows.Forms.Label();
            this.pnlMain.SuspendLayout();
            this.pnlQuickAccess.SuspendLayout();
            this.tblQuickAccess.SuspendLayout();
            this.pnlQuickLogs.SuspendLayout();
            this.pnlQuickAssignments.SuspendLayout();
            this.pnlQuickUsers.SuspendLayout();
            this.pnlRecentActivity.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRecentActivity)).BeginInit();
            this.pnlActivityHeader.SuspendLayout();
            this.pnlStats.SuspendLayout();
            this.tblStats.SuspendLayout();
            this.pnlCardRate.SuspendLayout();
            this.pnlRateHeader.SuspendLayout();
            this.pnlCardSubmissions.SuspendLayout();
            this.pnlSubmissionHeader.SuspendLayout();
            this.pnlCardAssignments.SuspendLayout();
            this.pnlAssignmentHeader.SuspendLayout();
            this.pnlCardUsers.SuspendLayout();
            this.pnlUserHeader.SuspendLayout();
            this.pnlHeader.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlMain
            // 
            this.pnlMain.AutoScroll = true;
            this.pnlMain.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(249)))), ((int)(((byte)(250)))));
            this.pnlMain.Controls.Add(this.pnlQuickAccess);
            this.pnlMain.Controls.Add(this.pnlRecentActivity);
            this.pnlMain.Controls.Add(this.pnlStats);
            this.pnlMain.Controls.Add(this.pnlHeader);
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Location = new System.Drawing.Point(0, 0);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Padding = new System.Windows.Forms.Padding(25, 20, 25, 20);
            this.pnlMain.Size = new System.Drawing.Size(1400, 900);
            this.pnlMain.TabIndex = 0;
            // 
            // pnlQuickAccess
            // 
            this.pnlQuickAccess.BackColor = System.Drawing.Color.Transparent;
            this.pnlQuickAccess.Controls.Add(this.tblQuickAccess);
            this.pnlQuickAccess.Controls.Add(this.lblQuickAccessTitle);
            this.pnlQuickAccess.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlQuickAccess.Location = new System.Drawing.Point(25, 690);
            this.pnlQuickAccess.Name = "pnlQuickAccess";
            this.pnlQuickAccess.Padding = new System.Windows.Forms.Padding(0, 0, 0, 20);
            this.pnlQuickAccess.Size = new System.Drawing.Size(1350, 200);
            this.pnlQuickAccess.TabIndex = 3;
            // 
            // tblQuickAccess
            // 
            this.tblQuickAccess.ColumnCount = 3;
            this.tblQuickAccess.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33F));
            this.tblQuickAccess.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33F));
            this.tblQuickAccess.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.34F));
            this.tblQuickAccess.Controls.Add(this.pnlQuickLogs, 2, 0);
            this.tblQuickAccess.Controls.Add(this.pnlQuickAssignments, 1, 0);
            this.tblQuickAccess.Controls.Add(this.pnlQuickUsers, 0, 0);
            this.tblQuickAccess.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblQuickAccess.Location = new System.Drawing.Point(0, 40);
            this.tblQuickAccess.Name = "tblQuickAccess";
            this.tblQuickAccess.RowCount = 1;
            this.tblQuickAccess.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblQuickAccess.Size = new System.Drawing.Size(1350, 140);
            this.tblQuickAccess.TabIndex = 1;
            // 
            // pnlQuickLogs
            // 
            this.pnlQuickLogs.BackColor = System.Drawing.Color.White;
            this.pnlQuickLogs.Controls.Add(this.lblQuickLogsDesc);
            this.pnlQuickLogs.Controls.Add(this.lblQuickLogsTitle);
            this.pnlQuickLogs.Controls.Add(this.lblQuickLogsIcon);
            this.pnlQuickLogs.Controls.Add(this.pnlLogsAccent);
            this.pnlQuickLogs.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pnlQuickLogs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlQuickLogs.Location = new System.Drawing.Point(901, 3);
            this.pnlQuickLogs.Margin = new System.Windows.Forms.Padding(3, 3, 0, 3);
            this.pnlQuickLogs.Name = "pnlQuickLogs";
            this.pnlQuickLogs.Padding = new System.Windows.Forms.Padding(4, 0, 0, 0);
            this.pnlQuickLogs.Size = new System.Drawing.Size(449, 134);
            this.pnlQuickLogs.TabIndex = 2;
            // 
            // lblQuickLogsDesc
            // 
            this.lblQuickLogsDesc.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblQuickLogsDesc.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblQuickLogsDesc.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(117)))), ((int)(((byte)(125)))));
            this.lblQuickLogsDesc.Location = new System.Drawing.Point(20, 100);
            this.lblQuickLogsDesc.Name = "lblQuickLogsDesc";
            this.lblQuickLogsDesc.Size = new System.Drawing.Size(412, 20);
            this.lblQuickLogsDesc.TabIndex = 3;
            this.lblQuickLogsDesc.Text = "Xem chi tiết hoạt động và lịch sử hệ thống";
            // 
            // lblQuickLogsTitle
            // 
            this.lblQuickLogsTitle.AutoSize = true;
            this.lblQuickLogsTitle.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblQuickLogsTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(37)))), ((int)(((byte)(41)))));
            this.lblQuickLogsTitle.Location = new System.Drawing.Point(20, 65);
            this.lblQuickLogsTitle.Name = "lblQuickLogsTitle";
            this.lblQuickLogsTitle.Size = new System.Drawing.Size(143, 21);
            this.lblQuickLogsTitle.TabIndex = 2;
            this.lblQuickLogsTitle.Text = "Nhật ký hệ thống";
            // 
            // lblQuickLogsIcon
            // 
            this.lblQuickLogsIcon.AutoSize = true;
            this.lblQuickLogsIcon.Font = new System.Drawing.Font("Segoe UI Emoji", 24F);
            this.lblQuickLogsIcon.Location = new System.Drawing.Point(18, 15);
            this.lblQuickLogsIcon.Name = "lblQuickLogsIcon";
            this.lblQuickLogsIcon.Size = new System.Drawing.Size(63, 43);
            this.lblQuickLogsIcon.TabIndex = 1;
            this.lblQuickLogsIcon.Text = "📊";
            // 
            // pnlLogsAccent
            // 
            this.pnlLogsAccent.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(126)))), ((int)(((byte)(20)))));
            this.pnlLogsAccent.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlLogsAccent.Location = new System.Drawing.Point(4, 0);
            this.pnlLogsAccent.Name = "pnlLogsAccent";
            this.pnlLogsAccent.Size = new System.Drawing.Size(4, 134);
            this.pnlLogsAccent.TabIndex = 0;
            // 
            // pnlQuickAssignments
            // 
            this.pnlQuickAssignments.BackColor = System.Drawing.Color.White;
            this.pnlQuickAssignments.Controls.Add(this.lblQuickAssignDesc);
            this.pnlQuickAssignments.Controls.Add(this.lblQuickAssignTitle);
            this.pnlQuickAssignments.Controls.Add(this.lblQuickAssignIcon);
            this.pnlQuickAssignments.Controls.Add(this.pnlAssignAccent);
            this.pnlQuickAssignments.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pnlQuickAssignments.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlQuickAssignments.Location = new System.Drawing.Point(452, 3);
            this.pnlQuickAssignments.Name = "pnlQuickAssignments";
            this.pnlQuickAssignments.Padding = new System.Windows.Forms.Padding(4, 0, 0, 0);
            this.pnlQuickAssignments.Size = new System.Drawing.Size(443, 134);
            this.pnlQuickAssignments.TabIndex = 1;
            // 
            // lblQuickAssignDesc
            // 
            this.lblQuickAssignDesc.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblQuickAssignDesc.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblQuickAssignDesc.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(117)))), ((int)(((byte)(125)))));
            this.lblQuickAssignDesc.Location = new System.Drawing.Point(20, 100);
            this.lblQuickAssignDesc.Name = "lblQuickAssignDesc";
            this.lblQuickAssignDesc.Size = new System.Drawing.Size(409, 20);
            this.lblQuickAssignDesc.TabIndex = 3;
            this.lblQuickAssignDesc.Text = "Tạo mới và quản lý bài tập lập trình";
            // 
            // lblQuickAssignTitle
            // 
            this.lblQuickAssignTitle.AutoSize = true;
            this.lblQuickAssignTitle.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblQuickAssignTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(37)))), ((int)(((byte)(41)))));
            this.lblQuickAssignTitle.Location = new System.Drawing.Point(20, 65);
            this.lblQuickAssignTitle.Name = "lblQuickAssignTitle";
            this.lblQuickAssignTitle.Size = new System.Drawing.Size(170, 21);
            this.lblQuickAssignTitle.TabIndex = 2;
            this.lblQuickAssignTitle.Text = "Quản lý Assignments";
            // 
            // lblQuickAssignIcon
            // 
            this.lblQuickAssignIcon.AutoSize = true;
            this.lblQuickAssignIcon.Font = new System.Drawing.Font("Segoe UI Emoji", 24F);
            this.lblQuickAssignIcon.Location = new System.Drawing.Point(18, 15);
            this.lblQuickAssignIcon.Name = "lblQuickAssignIcon";
            this.lblQuickAssignIcon.Size = new System.Drawing.Size(63, 43);
            this.lblQuickAssignIcon.TabIndex = 1;
            this.lblQuickAssignIcon.Text = "📝";
            // 
            // pnlAssignAccent
            // 
            this.pnlAssignAccent.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(135)))), ((int)(((byte)(84)))));
            this.pnlAssignAccent.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlAssignAccent.Location = new System.Drawing.Point(4, 0);
            this.pnlAssignAccent.Name = "pnlAssignAccent";
            this.pnlAssignAccent.Size = new System.Drawing.Size(4, 134);
            this.pnlAssignAccent.TabIndex = 0;
            // 
            // pnlQuickUsers
            // 
            this.pnlQuickUsers.BackColor = System.Drawing.Color.White;
            this.pnlQuickUsers.Controls.Add(this.lblQuickUsersDesc);
            this.pnlQuickUsers.Controls.Add(this.lblQuickUsersTitle);
            this.pnlQuickUsers.Controls.Add(this.lblQuickUsersIcon);
            this.pnlQuickUsers.Controls.Add(this.pnlUsersAccent);
            this.pnlQuickUsers.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pnlQuickUsers.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlQuickUsers.Location = new System.Drawing.Point(0, 3);
            this.pnlQuickUsers.Margin = new System.Windows.Forms.Padding(0, 3, 3, 3);
            this.pnlQuickUsers.Name = "pnlQuickUsers";
            this.pnlQuickUsers.Padding = new System.Windows.Forms.Padding(4, 0, 0, 0);
            this.pnlQuickUsers.Size = new System.Drawing.Size(446, 134);
            this.pnlQuickUsers.TabIndex = 0;
            // 
            // lblQuickUsersDesc
            // 
            this.lblQuickUsersDesc.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblQuickUsersDesc.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblQuickUsersDesc.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(117)))), ((int)(((byte)(125)))));
            this.lblQuickUsersDesc.Location = new System.Drawing.Point(20, 100);
            this.lblQuickUsersDesc.Name = "lblQuickUsersDesc";
            this.lblQuickUsersDesc.Size = new System.Drawing.Size(409, 20);
            this.lblQuickUsersDesc.TabIndex = 3;
            this.lblQuickUsersDesc.Text = "Thêm, sửa, xóa và quản lý người dùng";
            // 
            // lblQuickUsersTitle
            // 
            this.lblQuickUsersTitle.AutoSize = true;
            this.lblQuickUsersTitle.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblQuickUsersTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(37)))), ((int)(((byte)(41)))));
            this.lblQuickUsersTitle.Location = new System.Drawing.Point(20, 65);
            this.lblQuickUsersTitle.Name = "lblQuickUsersTitle";
            this.lblQuickUsersTitle.Size = new System.Drawing.Size(163, 21);
            this.lblQuickUsersTitle.TabIndex = 2;
            this.lblQuickUsersTitle.Text = "Quản lý người dùng";
            // 
            // lblQuickUsersIcon
            // 
            this.lblQuickUsersIcon.AutoSize = true;
            this.lblQuickUsersIcon.Font = new System.Drawing.Font("Segoe UI Emoji", 24F);
            this.lblQuickUsersIcon.Location = new System.Drawing.Point(18, 15);
            this.lblQuickUsersIcon.Name = "lblQuickUsersIcon";
            this.lblQuickUsersIcon.Size = new System.Drawing.Size(63, 43);
            this.lblQuickUsersIcon.TabIndex = 1;
            this.lblQuickUsersIcon.Text = "👥";
            // 
            // pnlUsersAccent
            // 
            this.pnlUsersAccent.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(13)))), ((int)(((byte)(110)))), ((int)(((byte)(253)))));
            this.pnlUsersAccent.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlUsersAccent.Location = new System.Drawing.Point(4, 0);
            this.pnlUsersAccent.Name = "pnlUsersAccent";
            this.pnlUsersAccent.Size = new System.Drawing.Size(4, 134);
            this.pnlUsersAccent.TabIndex = 0;
            // 
            // lblQuickAccessTitle
            // 
            this.lblQuickAccessTitle.AutoSize = true;
            this.lblQuickAccessTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblQuickAccessTitle.Font = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Bold);
            this.lblQuickAccessTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(37)))), ((int)(((byte)(41)))));
            this.lblQuickAccessTitle.Location = new System.Drawing.Point(0, 0);
            this.lblQuickAccessTitle.Margin = new System.Windows.Forms.Padding(0, 0, 0, 15);
            this.lblQuickAccessTitle.Name = "lblQuickAccessTitle";
            this.lblQuickAccessTitle.Padding = new System.Windows.Forms.Padding(0, 0, 0, 15);
            this.lblQuickAccessTitle.Size = new System.Drawing.Size(144, 40);
            this.lblQuickAccessTitle.TabIndex = 0;
            this.lblQuickAccessTitle.Text = "Truy cập nhanh";
            // 
            // pnlRecentActivity
            // 
            this.pnlRecentActivity.BackColor = System.Drawing.Color.White;
            this.pnlRecentActivity.Controls.Add(this.dgvRecentActivity);
            this.pnlRecentActivity.Controls.Add(this.pnlActivityHeader);
            this.pnlRecentActivity.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlRecentActivity.Location = new System.Drawing.Point(25, 290);
            this.pnlRecentActivity.Name = "pnlRecentActivity";
            this.pnlRecentActivity.Padding = new System.Windows.Forms.Padding(25, 20, 25, 20);
            this.pnlRecentActivity.Size = new System.Drawing.Size(1350, 400);
            this.pnlRecentActivity.TabIndex = 2;
            // 
            // dgvRecentActivity
            // 
            this.dgvRecentActivity.AllowUserToAddRows = false;
            this.dgvRecentActivity.AllowUserToDeleteRows = false;
            this.dgvRecentActivity.AllowUserToResizeRows = false;
            this.dgvRecentActivity.BackgroundColor = System.Drawing.Color.White;
            this.dgvRecentActivity.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvRecentActivity.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(249)))), ((int)(((byte)(250)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(73)))), ((int)(((byte)(80)))), ((int)(((byte)(87)))));
            dataGridViewCellStyle1.Padding = new System.Windows.Forms.Padding(12, 8, 12, 8);
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(249)))), ((int)(((byte)(250)))));
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(73)))), ((int)(((byte)(80)))), ((int)(((byte)(87)))));
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvRecentActivity.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvRecentActivity.ColumnHeadersHeight = 45;
            this.dgvRecentActivity.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvRecentActivity.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colIndex,
            this.colUser,
            this.colAction,
            this.colDetail,
            this.colTime});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 9F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(58)))), ((int)(((byte)(64)))));
            dataGridViewCellStyle2.Padding = new System.Windows.Forms.Padding(12, 8, 12, 8);
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(244)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(13)))), ((int)(((byte)(110)))), ((int)(((byte)(253)))));
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvRecentActivity.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvRecentActivity.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvRecentActivity.EnableHeadersVisualStyles = false;
            this.dgvRecentActivity.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(226)))), ((int)(((byte)(230)))));
            this.dgvRecentActivity.Location = new System.Drawing.Point(25, 75);
            this.dgvRecentActivity.MultiSelect = false;
            this.dgvRecentActivity.Name = "dgvRecentActivity";
            this.dgvRecentActivity.ReadOnly = true;
            this.dgvRecentActivity.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI", 9F);
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(244)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(13)))), ((int)(((byte)(110)))), ((int)(((byte)(253)))));
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvRecentActivity.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dgvRecentActivity.RowHeadersVisible = false;
            this.dgvRecentActivity.RowTemplate.Height = 50;
            this.dgvRecentActivity.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvRecentActivity.Size = new System.Drawing.Size(1300, 305);
            this.dgvRecentActivity.TabIndex = 1;
            // 
            // colIndex
            // 
            this.colIndex.HeaderText = "#";
            this.colIndex.Name = "colIndex";
            this.colIndex.ReadOnly = true;
            this.colIndex.Width = 60;
            // 
            // colUser
            // 
            this.colUser.HeaderText = "Người dùng";
            this.colUser.Name = "colUser";
            this.colUser.ReadOnly = true;
            this.colUser.Width = 180;
            // 
            // colAction
            // 
            this.colAction.HeaderText = "Hành động";
            this.colAction.Name = "colAction";
            this.colAction.ReadOnly = true;
            this.colAction.Width = 200;
            // 
            // colDetail
            // 
            this.colDetail.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colDetail.HeaderText = "Chi tiết";
            this.colDetail.Name = "colDetail";
            this.colDetail.ReadOnly = true;
            // 
            // colTime
            // 
            this.colTime.HeaderText = "Thời gian";
            this.colTime.Name = "colTime";
            this.colTime.ReadOnly = true;
            this.colTime.Width = 180;
            // 
            // pnlActivityHeader
            // 
            this.pnlActivityHeader.Controls.Add(this.lblActivitySubtitle);
            this.pnlActivityHeader.Controls.Add(this.lblRecentActivityTitle);
            this.pnlActivityHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlActivityHeader.Location = new System.Drawing.Point(25, 20);
            this.pnlActivityHeader.Name = "pnlActivityHeader";
            this.pnlActivityHeader.Size = new System.Drawing.Size(1300, 55);
            this.pnlActivityHeader.TabIndex = 0;
            // 
            // lblActivitySubtitle
            // 
            this.lblActivitySubtitle.AutoSize = true;
            this.lblActivitySubtitle.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblActivitySubtitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(117)))), ((int)(((byte)(125)))));
            this.lblActivitySubtitle.Location = new System.Drawing.Point(0, 30);
            this.lblActivitySubtitle.Name = "lblActivitySubtitle";
            this.lblActivitySubtitle.Size = new System.Drawing.Size(267, 15);
            this.lblActivitySubtitle.TabIndex = 1;
            this.lblActivitySubtitle.Text = "Theo dõi các hoạt động mới nhất trong hệ thống";
            // 
            // lblRecentActivityTitle
            // 
            this.lblRecentActivityTitle.AutoSize = true;
            this.lblRecentActivityTitle.Font = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Bold);
            this.lblRecentActivityTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(37)))), ((int)(((byte)(41)))));
            this.lblRecentActivityTitle.Location = new System.Drawing.Point(0, 5);
            this.lblRecentActivityTitle.Name = "lblRecentActivityTitle";
            this.lblRecentActivityTitle.Size = new System.Drawing.Size(176, 25);
            this.lblRecentActivityTitle.TabIndex = 0;
            this.lblRecentActivityTitle.Text = "Hoạt động gần đây";
            // 
            // pnlStats
            // 
            this.pnlStats.BackColor = System.Drawing.Color.Transparent;
            this.pnlStats.Controls.Add(this.tblStats);
            this.pnlStats.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlStats.Location = new System.Drawing.Point(25, 120);
            this.pnlStats.Name = "pnlStats";
            this.pnlStats.Padding = new System.Windows.Forms.Padding(0, 0, 0, 20);
            this.pnlStats.Size = new System.Drawing.Size(1350, 170);
            this.pnlStats.TabIndex = 1;
            // 
            // tblStats
            // 
            this.tblStats.ColumnCount = 4;
            this.tblStats.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tblStats.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tblStats.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tblStats.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tblStats.Controls.Add(this.pnlCardRate, 3, 0);
            this.tblStats.Controls.Add(this.pnlCardSubmissions, 2, 0);
            this.tblStats.Controls.Add(this.pnlCardAssignments, 1, 0);
            this.tblStats.Controls.Add(this.pnlCardUsers, 0, 0);
            this.tblStats.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblStats.Location = new System.Drawing.Point(0, 0);
            this.tblStats.Name = "tblStats";
            this.tblStats.RowCount = 1;
            this.tblStats.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblStats.Size = new System.Drawing.Size(1350, 150);
            this.tblStats.TabIndex = 0;
            // 
            // pnlCardRate
            // 
            this.pnlCardRate.BackColor = System.Drawing.Color.White;
            this.pnlCardRate.Controls.Add(this.lblRateChange);
            this.pnlCardRate.Controls.Add(this.pnlRateHeader);
            this.pnlCardRate.Controls.Add(this.lblRateValue);
            this.pnlCardRate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlCardRate.Location = new System.Drawing.Point(1014, 3);
            this.pnlCardRate.Margin = new System.Windows.Forms.Padding(3, 3, 0, 3);
            this.pnlCardRate.Name = "pnlCardRate";
            this.pnlCardRate.Padding = new System.Windows.Forms.Padding(20, 18, 20, 18);
            this.pnlCardRate.Size = new System.Drawing.Size(336, 144);
            this.pnlCardRate.TabIndex = 3;
            // 
            // lblRateChange
            // 
            this.lblRateChange.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblRateChange.AutoSize = true;
            this.lblRateChange.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.lblRateChange.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(135)))), ((int)(((byte)(84)))));
            this.lblRateChange.Location = new System.Drawing.Point(20, 115);
            this.lblRateChange.Name = "lblRateChange";
            this.lblRateChange.Size = new System.Drawing.Size(124, 15);
            this.lblRateChange.TabIndex = 2;
            this.lblRateChange.Text = "↗ +2.5% từ tuần trước";
            // 
            // pnlRateHeader
            // 
            this.pnlRateHeader.Controls.Add(this.lblRateTitle);
            this.pnlRateHeader.Controls.Add(this.lblIconRate);
            this.pnlRateHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlRateHeader.Location = new System.Drawing.Point(20, 18);
            this.pnlRateHeader.Name = "pnlRateHeader";
            this.pnlRateHeader.Size = new System.Drawing.Size(296, 35);
            this.pnlRateHeader.TabIndex = 1;
            // 
            // lblRateTitle
            // 
            this.lblRateTitle.AutoSize = true;
            this.lblRateTitle.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblRateTitle.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblRateTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(117)))), ((int)(((byte)(125)))));
            this.lblRateTitle.Location = new System.Drawing.Point(51, 0);
            this.lblRateTitle.Name = "lblRateTitle";
            this.lblRateTitle.Padding = new System.Windows.Forms.Padding(0, 8, 0, 0);
            this.lblRateTitle.Size = new System.Drawing.Size(104, 25);
            this.lblRateTitle.TabIndex = 1;
            this.lblRateTitle.Text = "Tỷ lệ hoàn thành";
            // 
            // lblIconRate
            // 
            this.lblIconRate.AutoSize = true;
            this.lblIconRate.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblIconRate.Font = new System.Drawing.Font("Segoe UI Emoji", 16F);
            this.lblIconRate.Location = new System.Drawing.Point(0, 0);
            this.lblIconRate.Name = "lblIconRate";
            this.lblIconRate.Padding = new System.Windows.Forms.Padding(0, 3, 8, 0);
            this.lblIconRate.Size = new System.Drawing.Size(51, 33);
            this.lblIconRate.TabIndex = 0;
            this.lblIconRate.Text = "📈";
            // 
            // lblRateValue
            // 
            this.lblRateValue.AutoSize = true;
            this.lblRateValue.Font = new System.Drawing.Font("Segoe UI", 28F, System.Drawing.FontStyle.Bold);
            this.lblRateValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(13)))), ((int)(((byte)(110)))), ((int)(((byte)(253)))));
            this.lblRateValue.Location = new System.Drawing.Point(13, 55);
            this.lblRateValue.Name = "lblRateValue";
            this.lblRateValue.Size = new System.Drawing.Size(77, 51);
            this.lblRateValue.TabIndex = 0;
            this.lblRateValue.Text = "0%";
            // 
            // pnlCardSubmissions
            // 
            this.pnlCardSubmissions.BackColor = System.Drawing.Color.White;
            this.pnlCardSubmissions.Controls.Add(this.lblSubmissionChange);
            this.pnlCardSubmissions.Controls.Add(this.pnlSubmissionHeader);
            this.pnlCardSubmissions.Controls.Add(this.lblSubmissionCount);
            this.pnlCardSubmissions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlCardSubmissions.Location = new System.Drawing.Point(677, 3);
            this.pnlCardSubmissions.Name = "pnlCardSubmissions";
            this.pnlCardSubmissions.Padding = new System.Windows.Forms.Padding(20, 18, 20, 18);
            this.pnlCardSubmissions.Size = new System.Drawing.Size(331, 144);
            this.pnlCardSubmissions.TabIndex = 2;
            // 
            // lblSubmissionChange
            // 
            this.lblSubmissionChange.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblSubmissionChange.AutoSize = true;
            this.lblSubmissionChange.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.lblSubmissionChange.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(135)))), ((int)(((byte)(84)))));
            this.lblSubmissionChange.Location = new System.Drawing.Point(20, 115);
            this.lblSubmissionChange.Name = "lblSubmissionChange";
            this.lblSubmissionChange.Size = new System.Drawing.Size(104, 15);
            this.lblSubmissionChange.TabIndex = 2;
            this.lblSubmissionChange.Text = "↗ +12 từ hôm qua";
            // 
            // pnlSubmissionHeader
            // 
            this.pnlSubmissionHeader.Controls.Add(this.lblSubmissionTitle);
            this.pnlSubmissionHeader.Controls.Add(this.lblIconSubmission);
            this.pnlSubmissionHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlSubmissionHeader.Location = new System.Drawing.Point(20, 18);
            this.pnlSubmissionHeader.Name = "pnlSubmissionHeader";
            this.pnlSubmissionHeader.Size = new System.Drawing.Size(291, 35);
            this.pnlSubmissionHeader.TabIndex = 1;
            // 
            // lblSubmissionTitle
            // 
            this.lblSubmissionTitle.AutoSize = true;
            this.lblSubmissionTitle.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblSubmissionTitle.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblSubmissionTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(117)))), ((int)(((byte)(125)))));
            this.lblSubmissionTitle.Location = new System.Drawing.Point(51, 0);
            this.lblSubmissionTitle.Name = "lblSubmissionTitle";
            this.lblSubmissionTitle.Padding = new System.Windows.Forms.Padding(0, 8, 0, 0);
            this.lblSubmissionTitle.Size = new System.Drawing.Size(134, 25);
            this.lblSubmissionTitle.TabIndex = 1;
            this.lblSubmissionTitle.Text = "Submissions hôm nay";
            // 
            // lblIconSubmission
            // 
            this.lblIconSubmission.AutoSize = true;
            this.lblIconSubmission.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblIconSubmission.Font = new System.Drawing.Font("Segoe UI Emoji", 16F);
            this.lblIconSubmission.Location = new System.Drawing.Point(0, 0);
            this.lblIconSubmission.Name = "lblIconSubmission";
            this.lblIconSubmission.Padding = new System.Windows.Forms.Padding(0, 3, 8, 0);
            this.lblIconSubmission.Size = new System.Drawing.Size(51, 33);
            this.lblIconSubmission.TabIndex = 0;
            this.lblIconSubmission.Text = "📄";
            // 
            // lblSubmissionCount
            // 
            this.lblSubmissionCount.AutoSize = true;
            this.lblSubmissionCount.Font = new System.Drawing.Font("Segoe UI", 28F, System.Drawing.FontStyle.Bold);
            this.lblSubmissionCount.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(126)))), ((int)(((byte)(20)))));
            this.lblSubmissionCount.Location = new System.Drawing.Point(13, 55);
            this.lblSubmissionCount.Name = "lblSubmissionCount";
            this.lblSubmissionCount.Size = new System.Drawing.Size(44, 51);
            this.lblSubmissionCount.TabIndex = 0;
            this.lblSubmissionCount.Text = "0";
            // 
            // pnlCardAssignments
            // 
            this.pnlCardAssignments.BackColor = System.Drawing.Color.White;
            this.pnlCardAssignments.Controls.Add(this.lblAssignmentChange);
            this.pnlCardAssignments.Controls.Add(this.pnlAssignmentHeader);
            this.pnlCardAssignments.Controls.Add(this.lblAssignmentCount);
            this.pnlCardAssignments.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlCardAssignments.Location = new System.Drawing.Point(340, 3);
            this.pnlCardAssignments.Name = "pnlCardAssignments";
            this.pnlCardAssignments.Padding = new System.Windows.Forms.Padding(20, 18, 20, 18);
            this.pnlCardAssignments.Size = new System.Drawing.Size(331, 144);
            this.pnlCardAssignments.TabIndex = 1;
            // 
            // lblAssignmentChange
            // 
            this.lblAssignmentChange.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblAssignmentChange.AutoSize = true;
            this.lblAssignmentChange.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.lblAssignmentChange.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(135)))), ((int)(((byte)(84)))));
            this.lblAssignmentChange.Location = new System.Drawing.Point(20, 115);
            this.lblAssignmentChange.Name = "lblAssignmentChange";
            this.lblAssignmentChange.Size = new System.Drawing.Size(112, 15);
            this.lblAssignmentChange.TabIndex = 2;
            this.lblAssignmentChange.Text = "↗ +3 từ tháng trước";
            // 
            // pnlAssignmentHeader
            // 
            this.pnlAssignmentHeader.Controls.Add(this.lblAssignmentTitle);
            this.pnlAssignmentHeader.Controls.Add(this.lblIconAssignment);
            this.pnlAssignmentHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlAssignmentHeader.Location = new System.Drawing.Point(20, 18);
            this.pnlAssignmentHeader.Name = "pnlAssignmentHeader";
            this.pnlAssignmentHeader.Size = new System.Drawing.Size(291, 35);
            this.pnlAssignmentHeader.TabIndex = 1;
            // 
            // lblAssignmentTitle
            // 
            this.lblAssignmentTitle.AutoSize = true;
            this.lblAssignmentTitle.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblAssignmentTitle.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblAssignmentTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(117)))), ((int)(((byte)(125)))));
            this.lblAssignmentTitle.Location = new System.Drawing.Point(51, 0);
            this.lblAssignmentTitle.Name = "lblAssignmentTitle";
            this.lblAssignmentTitle.Padding = new System.Windows.Forms.Padding(0, 8, 0, 0);
            this.lblAssignmentTitle.Size = new System.Drawing.Size(101, 25);
            this.lblAssignmentTitle.TabIndex = 1;
            this.lblAssignmentTitle.Text = "Tổng số bài tập";
            // 
            // lblIconAssignment
            // 
            this.lblIconAssignment.AutoSize = true;
            this.lblIconAssignment.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblIconAssignment.Font = new System.Drawing.Font("Segoe UI Emoji", 16F);
            this.lblIconAssignment.Location = new System.Drawing.Point(0, 0);
            this.lblIconAssignment.Name = "lblIconAssignment";
            this.lblIconAssignment.Padding = new System.Windows.Forms.Padding(0, 3, 8, 0);
            this.lblIconAssignment.Size = new System.Drawing.Size(51, 33);
            this.lblIconAssignment.TabIndex = 0;
            this.lblIconAssignment.Text = "📝";
            // 
            // lblAssignmentCount
            // 
            this.lblAssignmentCount.AutoSize = true;
            this.lblAssignmentCount.Font = new System.Drawing.Font("Segoe UI", 28F, System.Drawing.FontStyle.Bold);
            this.lblAssignmentCount.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(135)))), ((int)(((byte)(84)))));
            this.lblAssignmentCount.Location = new System.Drawing.Point(13, 55);
            this.lblAssignmentCount.Name = "lblAssignmentCount";
            this.lblAssignmentCount.Size = new System.Drawing.Size(44, 51);
            this.lblAssignmentCount.TabIndex = 0;
            this.lblAssignmentCount.Text = "0";
            // 
            // pnlCardUsers
            // 
            this.pnlCardUsers.BackColor = System.Drawing.Color.White;
            this.pnlCardUsers.Controls.Add(this.lblUserChange);
            this.pnlCardUsers.Controls.Add(this.pnlUserHeader);
            this.pnlCardUsers.Controls.Add(this.lblUserCount);
            this.pnlCardUsers.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlCardUsers.Location = new System.Drawing.Point(0, 3);
            this.pnlCardUsers.Margin = new System.Windows.Forms.Padding(0, 3, 3, 3);
            this.pnlCardUsers.Name = "pnlCardUsers";
            this.pnlCardUsers.Padding = new System.Windows.Forms.Padding(20, 18, 20, 18);
            this.pnlCardUsers.Size = new System.Drawing.Size(334, 144);
            this.pnlCardUsers.TabIndex = 0;
            // 
            // lblUserChange
            // 
            this.lblUserChange.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblUserChange.AutoSize = true;
            this.lblUserChange.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.lblUserChange.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(135)))), ((int)(((byte)(84)))));
            this.lblUserChange.Location = new System.Drawing.Point(20, 115);
            this.lblUserChange.Name = "lblUserChange";
            this.lblUserChange.Size = new System.Drawing.Size(112, 15);
            this.lblUserChange.TabIndex = 2;
            this.lblUserChange.Text = "↗ +5 từ tháng trước";
            // 
            // pnlUserHeader
            // 
            this.pnlUserHeader.Controls.Add(this.lblUserTitle);
            this.pnlUserHeader.Controls.Add(this.lblIconUser);
            this.pnlUserHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlUserHeader.Location = new System.Drawing.Point(20, 18);
            this.pnlUserHeader.Name = "pnlUserHeader";
            this.pnlUserHeader.Size = new System.Drawing.Size(294, 35);
            this.pnlUserHeader.TabIndex = 1;
            // 
            // lblUserTitle
            // 
            this.lblUserTitle.AutoSize = true;
            this.lblUserTitle.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblUserTitle.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblUserTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(117)))), ((int)(((byte)(125)))));
            this.lblUserTitle.Location = new System.Drawing.Point(51, 0);
            this.lblUserTitle.Name = "lblUserTitle";
            this.lblUserTitle.Padding = new System.Windows.Forms.Padding(0, 8, 0, 0);
            this.lblUserTitle.Size = new System.Drawing.Size(128, 25);
            this.lblUserTitle.TabIndex = 1;
            this.lblUserTitle.Text = "Tổng số người dùng";
            // 
            // lblIconUser
            // 
            this.lblIconUser.AutoSize = true;
            this.lblIconUser.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblIconUser.Font = new System.Drawing.Font("Segoe UI Emoji", 16F);
            this.lblIconUser.Location = new System.Drawing.Point(0, 0);
            this.lblIconUser.Name = "lblIconUser";
            this.lblIconUser.Padding = new System.Windows.Forms.Padding(0, 3, 8, 0);
            this.lblIconUser.Size = new System.Drawing.Size(51, 33);
            this.lblIconUser.TabIndex = 0;
            this.lblIconUser.Text = "👥";
            // 
            // lblUserCount
            // 
            this.lblUserCount.AutoSize = true;
            this.lblUserCount.Font = new System.Drawing.Font("Segoe UI", 28F, System.Drawing.FontStyle.Bold);
            this.lblUserCount.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(13)))), ((int)(((byte)(110)))), ((int)(((byte)(253)))));
            this.lblUserCount.Location = new System.Drawing.Point(13, 55);
            this.lblUserCount.Name = "lblUserCount";
            this.lblUserCount.Size = new System.Drawing.Size(44, 51);
            this.lblUserCount.TabIndex = 0;
            this.lblUserCount.Text = "0";
            // 
            // pnlHeader
            // 
            this.pnlHeader.BackColor = System.Drawing.Color.Transparent;
            this.pnlHeader.Controls.Add(this.lblWelcomeTime);
            this.pnlHeader.Controls.Add(this.lblDashboardTitle);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Location = new System.Drawing.Point(25, 20);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Padding = new System.Windows.Forms.Padding(0, 0, 0, 20);
            this.pnlHeader.Size = new System.Drawing.Size(1350, 100);
            this.pnlHeader.TabIndex = 0;
            // 
            // lblWelcomeTime
            // 
            this.lblWelcomeTime.AutoSize = true;
            this.lblWelcomeTime.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblWelcomeTime.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(117)))), ((int)(((byte)(125)))));
            this.lblWelcomeTime.Location = new System.Drawing.Point(0, 50);
            this.lblWelcomeTime.Name = "lblWelcomeTime";
            this.lblWelcomeTime.Size = new System.Drawing.Size(344, 19);
            this.lblWelcomeTime.TabIndex = 1;
            this.lblWelcomeTime.Text = "Chào mừng bạn trở lại! Hôm nay là Thứ Tư, 28/1/2026";
            // 
            // lblDashboardTitle
            // 
            this.lblDashboardTitle.AutoSize = true;
            this.lblDashboardTitle.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Bold);
            this.lblDashboardTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(37)))), ((int)(((byte)(41)))));
            this.lblDashboardTitle.Location = new System.Drawing.Point(0, 5);
            this.lblDashboardTitle.Name = "lblDashboardTitle";
            this.lblDashboardTitle.Size = new System.Drawing.Size(184, 45);
            this.lblDashboardTitle.TabIndex = 0;
            this.lblDashboardTitle.Text = "Dashboard";
            // 
            // ucAdminDashboard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(249)))), ((int)(((byte)(250)))));
            this.Controls.Add(this.pnlMain);
            this.Name = "ucAdminDashboard";
            this.Size = new System.Drawing.Size(1400, 900);
            this.pnlMain.ResumeLayout(false);
            this.pnlQuickAccess.ResumeLayout(false);
            this.pnlQuickAccess.PerformLayout();
            this.tblQuickAccess.ResumeLayout(false);
            this.pnlQuickLogs.ResumeLayout(false);
            this.pnlQuickLogs.PerformLayout();
            this.pnlQuickAssignments.ResumeLayout(false);
            this.pnlQuickAssignments.PerformLayout();
            this.pnlQuickUsers.ResumeLayout(false);
            this.pnlQuickUsers.PerformLayout();
            this.pnlRecentActivity.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvRecentActivity)).EndInit();
            this.pnlActivityHeader.ResumeLayout(false);
            this.pnlActivityHeader.PerformLayout();
            this.pnlStats.ResumeLayout(false);
            this.tblStats.ResumeLayout(false);
            this.pnlCardRate.ResumeLayout(false);
            this.pnlCardRate.PerformLayout();
            this.pnlRateHeader.ResumeLayout(false);
            this.pnlRateHeader.PerformLayout();
            this.pnlCardSubmissions.ResumeLayout(false);
            this.pnlCardSubmissions.PerformLayout();
            this.pnlSubmissionHeader.ResumeLayout(false);
            this.pnlSubmissionHeader.PerformLayout();
            this.pnlCardAssignments.ResumeLayout(false);
            this.pnlCardAssignments.PerformLayout();
            this.pnlAssignmentHeader.ResumeLayout(false);
            this.pnlAssignmentHeader.PerformLayout();
            this.pnlCardUsers.ResumeLayout(false);
            this.pnlCardUsers.PerformLayout();
            this.pnlUserHeader.ResumeLayout(false);
            this.pnlUserHeader.PerformLayout();
            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlMain;
        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label lblDashboardTitle;
        private System.Windows.Forms.Label lblWelcomeTime;
        private System.Windows.Forms.Panel pnlStats;
        private System.Windows.Forms.TableLayoutPanel tblStats;
        private System.Windows.Forms.Panel pnlCardUsers;
        private System.Windows.Forms.Label lblUserCount;
        private System.Windows.Forms.Panel pnlUserHeader;
        private System.Windows.Forms.Label lblUserTitle;
        private System.Windows.Forms.Label lblIconUser;
        private System.Windows.Forms.Label lblUserChange;
        private System.Windows.Forms.Panel pnlCardAssignments;
        private System.Windows.Forms.Label lblAssignmentChange;
        private System.Windows.Forms.Panel pnlAssignmentHeader;
        private System.Windows.Forms.Label lblAssignmentTitle;
        private System.Windows.Forms.Label lblIconAssignment;
        private System.Windows.Forms.Label lblAssignmentCount;
        private System.Windows.Forms.Panel pnlCardSubmissions;
        private System.Windows.Forms.Label lblSubmissionChange;
        private System.Windows.Forms.Panel pnlSubmissionHeader;
        private System.Windows.Forms.Label lblSubmissionTitle;
        private System.Windows.Forms.Label lblIconSubmission;
        private System.Windows.Forms.Label lblSubmissionCount;
        private System.Windows.Forms.Panel pnlCardRate;
        private System.Windows.Forms.Label lblRateChange;
        private System.Windows.Forms.Panel pnlRateHeader;
        private System.Windows.Forms.Label lblRateTitle;
        private System.Windows.Forms.Label lblIconRate;
        private System.Windows.Forms.Label lblRateValue;
        private System.Windows.Forms.Panel pnlRecentActivity;
        private System.Windows.Forms.DataGridView dgvRecentActivity;
        private System.Windows.Forms.Panel pnlActivityHeader;
        private System.Windows.Forms.Label lblActivitySubtitle;
        private System.Windows.Forms.Label lblRecentActivityTitle;
        private System.Windows.Forms.DataGridViewTextBoxColumn colIndex;
        private System.Windows.Forms.DataGridViewTextBoxColumn colUser;
        private System.Windows.Forms.DataGridViewTextBoxColumn colAction;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDetail;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTime;
        private System.Windows.Forms.Panel pnlQuickAccess;
        private System.Windows.Forms.TableLayoutPanel tblQuickAccess;
        private System.Windows.Forms.Panel pnlQuickLogs;
        private System.Windows.Forms.Label lblQuickLogsDesc;
        private System.Windows.Forms.Label lblQuickLogsTitle;
        private System.Windows.Forms.Label lblQuickLogsIcon;
        private System.Windows.Forms.Panel pnlLogsAccent;
        private System.Windows.Forms.Panel pnlQuickAssignments;
        private System.Windows.Forms.Label lblQuickAssignDesc;
        private System.Windows.Forms.Label lblQuickAssignTitle;
        private System.Windows.Forms.Label lblQuickAssignIcon;
        private System.Windows.Forms.Panel pnlAssignAccent;
        private System.Windows.Forms.Panel pnlQuickUsers;
        private System.Windows.Forms.Label lblQuickUsersDesc;
        private System.Windows.Forms.Label lblQuickUsersTitle;
        private System.Windows.Forms.Label lblQuickUsersIcon;
        private System.Windows.Forms.Panel pnlUsersAccent;
        private System.Windows.Forms.Label lblQuickAccessTitle;
    }
}