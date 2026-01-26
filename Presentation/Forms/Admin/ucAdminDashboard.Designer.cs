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
            this.pnlMain = new System.Windows.Forms.Panel();
            this.pnlQuickAccess = new System.Windows.Forms.Panel();
            this.tblQuickAccess = new System.Windows.Forms.TableLayoutPanel();
            this.btnQuickLogs = new System.Windows.Forms.Button();
            this.btnQuickAssignments = new System.Windows.Forms.Button();
            this.btnQuickUsers = new System.Windows.Forms.Button();
            this.lblQuickAccessTitle = new System.Windows.Forms.Label();
            this.pnlRecentActivity = new System.Windows.Forms.Panel();
            this.dgvRecentActivity = new System.Windows.Forms.DataGridView();
            this.colIndex = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colUser = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colAction = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDetail = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lblRecentActivityTitle = new System.Windows.Forms.Label();
            this.pnlStats = new System.Windows.Forms.Panel();
            this.tblStats = new System.Windows.Forms.TableLayoutPanel();
            this.pnlCardRate = new System.Windows.Forms.Panel();
            this.panelRateStrip = new System.Windows.Forms.Panel();
            this.lblRateTitle = new System.Windows.Forms.Label();
            this.lblRateValue = new System.Windows.Forms.Label();
            this.lblIconRate = new System.Windows.Forms.Label();
            this.pnlCardSubmissions = new System.Windows.Forms.Panel();
            this.panelSubmissionStrip = new System.Windows.Forms.Panel();
            this.lblSubmissionTitle = new System.Windows.Forms.Label();
            this.lblSubmissionCount = new System.Windows.Forms.Label();
            this.lblIconSubmission = new System.Windows.Forms.Label();
            this.pnlCardAssignments = new System.Windows.Forms.Panel();
            this.panelAssignmentStrip = new System.Windows.Forms.Panel();
            this.lblAssignmentTitle = new System.Windows.Forms.Label();
            this.lblAssignmentCount = new System.Windows.Forms.Label();
            this.lblIconAssignment = new System.Windows.Forms.Label();
            this.pnlCardUsers = new System.Windows.Forms.Panel();
            this.panelUserStrip = new System.Windows.Forms.Panel();
            this.lblUserTitle = new System.Windows.Forms.Label();
            this.lblUserCount = new System.Windows.Forms.Label();
            this.lblIconUser = new System.Windows.Forms.Label();
            this.lblStatsTitle = new System.Windows.Forms.Label();
            this.lblDashboardTitle = new System.Windows.Forms.Label();
            this.pnlMain.SuspendLayout();
            this.pnlQuickAccess.SuspendLayout();
            this.tblQuickAccess.SuspendLayout();
            this.pnlRecentActivity.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRecentActivity)).BeginInit();
            this.pnlStats.SuspendLayout();
            this.tblStats.SuspendLayout();
            this.pnlCardRate.SuspendLayout();
            this.pnlCardSubmissions.SuspendLayout();
            this.pnlCardAssignments.SuspendLayout();
            this.pnlCardUsers.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlMain
            // 
            this.pnlMain.AutoScroll = true;
            this.pnlMain.Controls.Add(this.pnlQuickAccess);
            this.pnlMain.Controls.Add(this.pnlRecentActivity);
            this.pnlMain.Controls.Add(this.pnlStats);
            this.pnlMain.Controls.Add(this.lblDashboardTitle);
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Location = new System.Drawing.Point(0, 0);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Padding = new System.Windows.Forms.Padding(20);
            this.pnlMain.Size = new System.Drawing.Size(1200, 800);
            this.pnlMain.TabIndex = 0;
            // 
            // pnlQuickAccess
            // 
            this.pnlQuickAccess.Controls.Add(this.tblQuickAccess);
            this.pnlQuickAccess.Controls.Add(this.lblQuickAccessTitle);
            this.pnlQuickAccess.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlQuickAccess.Location = new System.Drawing.Point(20, 590);
            this.pnlQuickAccess.Name = "pnlQuickAccess";
            this.pnlQuickAccess.Padding = new System.Windows.Forms.Padding(0, 20, 0, 0);
            this.pnlQuickAccess.Size = new System.Drawing.Size(1160, 150);
            this.pnlQuickAccess.TabIndex = 3;
            // 
            // tblQuickAccess
            // 
            this.tblQuickAccess.ColumnCount = 3;
            this.tblQuickAccess.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33F));
            this.tblQuickAccess.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33F));
            this.tblQuickAccess.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33F));
            this.tblQuickAccess.Controls.Add(this.btnQuickLogs, 2, 0);
            this.tblQuickAccess.Controls.Add(this.btnQuickAssignments, 1, 0);
            this.tblQuickAccess.Controls.Add(this.btnQuickUsers, 0, 0);
            this.tblQuickAccess.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tblQuickAccess.Location = new System.Drawing.Point(0, 50);
            this.tblQuickAccess.Name = "tblQuickAccess";
            this.tblQuickAccess.RowCount = 1;
            this.tblQuickAccess.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblQuickAccess.Size = new System.Drawing.Size(1160, 100);
            this.tblQuickAccess.TabIndex = 1;
            // 
            // btnQuickLogs
            // 
            this.btnQuickLogs.BackColor = System.Drawing.Color.White;
            this.btnQuickLogs.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnQuickLogs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnQuickLogs.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(152)))), ((int)(((byte)(0)))));
            this.btnQuickLogs.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnQuickLogs.Font = new System.Drawing.Font("Segoe UI Semibold", 10F);
            this.btnQuickLogs.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(152)))), ((int)(((byte)(0)))));
            this.btnQuickLogs.Location = new System.Drawing.Point(777, 5);
            this.btnQuickLogs.Margin = new System.Windows.Forms.Padding(5);
            this.btnQuickLogs.Name = "btnQuickLogs";
            this.btnQuickLogs.Padding = new System.Windows.Forms.Padding(15);
            this.btnQuickLogs.Size = new System.Drawing.Size(378, 90);
            this.btnQuickLogs.TabIndex = 2;
            this.btnQuickLogs.Text = "System Logs\n\nXem nhật ký hệ thống";
            this.btnQuickLogs.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.btnQuickLogs.UseVisualStyleBackColor = false;
            // 
            // btnQuickAssignments
            // 
            this.btnQuickAssignments.BackColor = System.Drawing.Color.White;
            this.btnQuickAssignments.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnQuickAssignments.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnQuickAssignments.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(167)))), ((int)(((byte)(69)))));
            this.btnQuickAssignments.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnQuickAssignments.Font = new System.Drawing.Font("Segoe UI Semibold", 10F);
            this.btnQuickAssignments.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(167)))), ((int)(((byte)(69)))));
            this.btnQuickAssignments.Location = new System.Drawing.Point(391, 5);
            this.btnQuickAssignments.Margin = new System.Windows.Forms.Padding(5);
            this.btnQuickAssignments.Name = "btnQuickAssignments";
            this.btnQuickAssignments.Padding = new System.Windows.Forms.Padding(15);
            this.btnQuickAssignments.Size = new System.Drawing.Size(376, 90);
            this.btnQuickAssignments.TabIndex = 1;
            this.btnQuickAssignments.Text = "Quản lý Assignments\n\nTạo và quản lý bài tập";
            this.btnQuickAssignments.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.btnQuickAssignments.UseVisualStyleBackColor = false;
            // 
            // btnQuickUsers
            // 
            this.btnQuickUsers.BackColor = System.Drawing.Color.White;
            this.btnQuickUsers.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnQuickUsers.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnQuickUsers.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(120)))), ((int)(((byte)(215)))));
            this.btnQuickUsers.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnQuickUsers.Font = new System.Drawing.Font("Segoe UI Semibold", 10F);
            this.btnQuickUsers.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(120)))), ((int)(((byte)(215)))));
            this.btnQuickUsers.Location = new System.Drawing.Point(5, 5);
            this.btnQuickUsers.Margin = new System.Windows.Forms.Padding(5);
            this.btnQuickUsers.Name = "btnQuickUsers";
            this.btnQuickUsers.Padding = new System.Windows.Forms.Padding(15);
            this.btnQuickUsers.Size = new System.Drawing.Size(376, 90);
            this.btnQuickUsers.TabIndex = 0;
            this.btnQuickUsers.Text = "Quản lý Users\n\nThêm, sửa, xóa người dùng";
            this.btnQuickUsers.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.btnQuickUsers.UseVisualStyleBackColor = false;
            // 
            // lblQuickAccessTitle
            // 
            this.lblQuickAccessTitle.AutoSize = true;
            this.lblQuickAccessTitle.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblQuickAccessTitle.Location = new System.Drawing.Point(5, 20);
            this.lblQuickAccessTitle.Name = "lblQuickAccessTitle";
            this.lblQuickAccessTitle.Size = new System.Drawing.Size(68, 21);
            this.lblQuickAccessTitle.TabIndex = 0;
            this.lblQuickAccessTitle.Text = "Quản lý";
            // 
            // pnlRecentActivity
            // 
            this.pnlRecentActivity.Controls.Add(this.dgvRecentActivity);
            this.pnlRecentActivity.Controls.Add(this.lblRecentActivityTitle);
            this.pnlRecentActivity.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlRecentActivity.Location = new System.Drawing.Point(20, 250);
            this.pnlRecentActivity.Name = "pnlRecentActivity";
            this.pnlRecentActivity.Padding = new System.Windows.Forms.Padding(0, 20, 0, 0);
            this.pnlRecentActivity.Size = new System.Drawing.Size(1160, 340);
            this.pnlRecentActivity.TabIndex = 2;
            // 
            // dgvRecentActivity
            // 
            this.dgvRecentActivity.AllowUserToAddRows = false;
            this.dgvRecentActivity.AllowUserToDeleteRows = false;
            this.dgvRecentActivity.BackgroundColor = System.Drawing.Color.White;
            this.dgvRecentActivity.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvRecentActivity.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgvRecentActivity.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.WhiteSmoke;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Gray;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.WhiteSmoke;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.Gray;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvRecentActivity.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvRecentActivity.ColumnHeadersHeight = 40;
            this.dgvRecentActivity.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colIndex,
            this.colUser,
            this.colAction,
            this.colDetail,
            this.colTime});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 9F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(245)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvRecentActivity.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvRecentActivity.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.dgvRecentActivity.EnableHeadersVisualStyles = false;
            this.dgvRecentActivity.GridColor = System.Drawing.Color.WhiteSmoke;
            this.dgvRecentActivity.Location = new System.Drawing.Point(0, 50);
            this.dgvRecentActivity.Name = "dgvRecentActivity";
            this.dgvRecentActivity.ReadOnly = true;
            this.dgvRecentActivity.RowHeadersVisible = false;
            this.dgvRecentActivity.RowTemplate.Height = 40;
            this.dgvRecentActivity.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvRecentActivity.Size = new System.Drawing.Size(1160, 290);
            this.dgvRecentActivity.TabIndex = 1;
            // 
            // colIndex
            // 
            this.colIndex.HeaderText = "#";
            this.colIndex.Name = "colIndex";
            this.colIndex.ReadOnly = true;
            this.colIndex.Width = 50;
            // 
            // colUser
            // 
            this.colUser.HeaderText = "User";
            this.colUser.Name = "colUser";
            this.colUser.ReadOnly = true;
            this.colUser.Width = 150;
            // 
            // colAction
            // 
            this.colAction.HeaderText = "Hành động";
            this.colAction.Name = "colAction";
            this.colAction.ReadOnly = true;
            this.colAction.Width = 150;
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
            this.colTime.Width = 150;
            // 
            // lblRecentActivityTitle
            // 
            this.lblRecentActivityTitle.AutoSize = true;
            this.lblRecentActivityTitle.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblRecentActivityTitle.Location = new System.Drawing.Point(5, 20);
            this.lblRecentActivityTitle.Name = "lblRecentActivityTitle";
            this.lblRecentActivityTitle.Size = new System.Drawing.Size(155, 21);
            this.lblRecentActivityTitle.TabIndex = 0;
            this.lblRecentActivityTitle.Text = "Hoạt động gần đây";
            // 
            // pnlStats
            // 
            this.pnlStats.Controls.Add(this.tblStats);
            this.pnlStats.Controls.Add(this.lblStatsTitle);
            this.pnlStats.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlStats.Location = new System.Drawing.Point(20, 70);
            this.pnlStats.Name = "pnlStats";
            this.pnlStats.Size = new System.Drawing.Size(1160, 180);
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
            this.tblStats.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tblStats.Location = new System.Drawing.Point(0, 40);
            this.tblStats.Name = "tblStats";
            this.tblStats.RowCount = 1;
            this.tblStats.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblStats.Size = new System.Drawing.Size(1160, 140);
            this.tblStats.TabIndex = 1;
            // 
            // pnlCardRate
            // 
            this.pnlCardRate.BackColor = System.Drawing.Color.White;
            this.pnlCardRate.Controls.Add(this.panelRateStrip);
            this.pnlCardRate.Controls.Add(this.lblRateTitle);
            this.pnlCardRate.Controls.Add(this.lblRateValue);
            this.pnlCardRate.Controls.Add(this.lblIconRate);
            this.pnlCardRate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlCardRate.Location = new System.Drawing.Point(875, 5);
            this.pnlCardRate.Margin = new System.Windows.Forms.Padding(5);
            this.pnlCardRate.Name = "pnlCardRate";
            this.pnlCardRate.Size = new System.Drawing.Size(280, 130);
            this.pnlCardRate.TabIndex = 3;
            // 
            // panelRateStrip
            // 
            this.panelRateStrip.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(51)))), ((int)(((byte)(153)))));
            this.panelRateStrip.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelRateStrip.Location = new System.Drawing.Point(0, 0);
            this.panelRateStrip.Name = "panelRateStrip";
            this.panelRateStrip.Size = new System.Drawing.Size(5, 130);
            this.panelRateStrip.TabIndex = 3;
            // 
            // lblRateTitle
            // 
            this.lblRateTitle.AutoSize = true;
            this.lblRateTitle.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblRateTitle.ForeColor = System.Drawing.Color.Gray;
            this.lblRateTitle.Location = new System.Drawing.Point(20, 100);
            this.lblRateTitle.Name = "lblRateTitle";
            this.lblRateTitle.Size = new System.Drawing.Size(97, 15);
            this.lblRateTitle.TabIndex = 2;
            this.lblRateTitle.Text = "Tỷ lệ hoàn thành";
            // 
            // lblRateValue
            // 
            this.lblRateValue.AutoSize = true;
            this.lblRateValue.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Bold);
            this.lblRateValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(51)))), ((int)(((byte)(153)))));
            this.lblRateValue.Location = new System.Drawing.Point(15, 55);
            this.lblRateValue.Name = "lblRateValue";
            this.lblRateValue.Size = new System.Drawing.Size(64, 45);
            this.lblRateValue.TabIndex = 1;
            this.lblRateValue.Text = "0%";
            // 
            // lblIconRate
            // 
            this.lblIconRate.AutoSize = true;
            this.lblIconRate.Font = new System.Drawing.Font("Segoe UI Emoji", 20F);
            this.lblIconRate.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(51)))), ((int)(((byte)(153)))));
            this.lblIconRate.Location = new System.Drawing.Point(20, 15);
            this.lblIconRate.Name = "lblIconRate";
            this.lblIconRate.Size = new System.Drawing.Size(43, 37);
            this.lblIconRate.TabIndex = 0;
            this.lblIconRate.Text = "📈";
            // 
            // pnlCardSubmissions
            // 
            this.pnlCardSubmissions.BackColor = System.Drawing.Color.White;
            this.pnlCardSubmissions.Controls.Add(this.panelSubmissionStrip);
            this.pnlCardSubmissions.Controls.Add(this.lblSubmissionTitle);
            this.pnlCardSubmissions.Controls.Add(this.lblSubmissionCount);
            this.pnlCardSubmissions.Controls.Add(this.lblIconSubmission);
            this.pnlCardSubmissions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlCardSubmissions.Location = new System.Drawing.Point(585, 5);
            this.pnlCardSubmissions.Margin = new System.Windows.Forms.Padding(5);
            this.pnlCardSubmissions.Name = "pnlCardSubmissions";
            this.pnlCardSubmissions.Size = new System.Drawing.Size(280, 130);
            this.pnlCardSubmissions.TabIndex = 2;
            // 
            // panelSubmissionStrip
            // 
            this.panelSubmissionStrip.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(152)))), ((int)(((byte)(0)))));
            this.panelSubmissionStrip.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelSubmissionStrip.Location = new System.Drawing.Point(0, 0);
            this.panelSubmissionStrip.Name = "panelSubmissionStrip";
            this.panelSubmissionStrip.Size = new System.Drawing.Size(5, 130);
            this.panelSubmissionStrip.TabIndex = 3;
            // 
            // lblSubmissionTitle
            // 
            this.lblSubmissionTitle.AutoSize = true;
            this.lblSubmissionTitle.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblSubmissionTitle.ForeColor = System.Drawing.Color.Gray;
            this.lblSubmissionTitle.Location = new System.Drawing.Point(20, 100);
            this.lblSubmissionTitle.Name = "lblSubmissionTitle";
            this.lblSubmissionTitle.Size = new System.Drawing.Size(123, 15);
            this.lblSubmissionTitle.TabIndex = 2;
            this.lblSubmissionTitle.Text = "Submissions hôm nay";
            // 
            // lblSubmissionCount
            // 
            this.lblSubmissionCount.AutoSize = true;
            this.lblSubmissionCount.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Bold);
            this.lblSubmissionCount.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(152)))), ((int)(((byte)(0)))));
            this.lblSubmissionCount.Location = new System.Drawing.Point(15, 55);
            this.lblSubmissionCount.Name = "lblSubmissionCount";
            this.lblSubmissionCount.Size = new System.Drawing.Size(38, 45);
            this.lblSubmissionCount.TabIndex = 1;
            this.lblSubmissionCount.Text = "0";
            // 
            // lblIconSubmission
            // 
            this.lblIconSubmission.AutoSize = true;
            this.lblIconSubmission.Font = new System.Drawing.Font("Segoe UI Emoji", 20F);
            this.lblIconSubmission.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(152)))), ((int)(((byte)(0)))));
            this.lblIconSubmission.Location = new System.Drawing.Point(20, 15);
            this.lblIconSubmission.Name = "lblIconSubmission";
            this.lblIconSubmission.Size = new System.Drawing.Size(43, 37);
            this.lblIconSubmission.TabIndex = 0;
            this.lblIconSubmission.Text = "📉";
            // 
            // pnlCardAssignments
            // 
            this.pnlCardAssignments.BackColor = System.Drawing.Color.White;
            this.pnlCardAssignments.Controls.Add(this.panelAssignmentStrip);
            this.pnlCardAssignments.Controls.Add(this.lblAssignmentTitle);
            this.pnlCardAssignments.Controls.Add(this.lblAssignmentCount);
            this.pnlCardAssignments.Controls.Add(this.lblIconAssignment);
            this.pnlCardAssignments.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlCardAssignments.Location = new System.Drawing.Point(295, 5);
            this.pnlCardAssignments.Margin = new System.Windows.Forms.Padding(5);
            this.pnlCardAssignments.Name = "pnlCardAssignments";
            this.pnlCardAssignments.Size = new System.Drawing.Size(280, 130);
            this.pnlCardAssignments.TabIndex = 1;
            // 
            // panelAssignmentStrip
            // 
            this.panelAssignmentStrip.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(167)))), ((int)(((byte)(69)))));
            this.panelAssignmentStrip.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelAssignmentStrip.Location = new System.Drawing.Point(0, 0);
            this.panelAssignmentStrip.Name = "panelAssignmentStrip";
            this.panelAssignmentStrip.Size = new System.Drawing.Size(5, 130);
            this.panelAssignmentStrip.TabIndex = 3;
            // 
            // lblAssignmentTitle
            // 
            this.lblAssignmentTitle.AutoSize = true;
            this.lblAssignmentTitle.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblAssignmentTitle.ForeColor = System.Drawing.Color.Gray;
            this.lblAssignmentTitle.Location = new System.Drawing.Point(20, 100);
            this.lblAssignmentTitle.Name = "lblAssignmentTitle";
            this.lblAssignmentTitle.Size = new System.Drawing.Size(91, 15);
            this.lblAssignmentTitle.TabIndex = 2;
            this.lblAssignmentTitle.Text = "Tổng số Bài tập";
            // 
            // lblAssignmentCount
            // 
            this.lblAssignmentCount.AutoSize = true;
            this.lblAssignmentCount.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Bold);
            this.lblAssignmentCount.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(167)))), ((int)(((byte)(69)))));
            this.lblAssignmentCount.Location = new System.Drawing.Point(15, 55);
            this.lblAssignmentCount.Name = "lblAssignmentCount";
            this.lblAssignmentCount.Size = new System.Drawing.Size(38, 45);
            this.lblAssignmentCount.TabIndex = 1;
            this.lblAssignmentCount.Text = "0";
            // 
            // lblIconAssignment
            // 
            this.lblIconAssignment.AutoSize = true;
            this.lblIconAssignment.Font = new System.Drawing.Font("Segoe UI Emoji", 20F);
            this.lblIconAssignment.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(167)))), ((int)(((byte)(69)))));
            this.lblIconAssignment.Location = new System.Drawing.Point(20, 15);
            this.lblIconAssignment.Name = "lblIconAssignment";
            this.lblIconAssignment.Size = new System.Drawing.Size(43, 37);
            this.lblIconAssignment.TabIndex = 0;
            this.lblIconAssignment.Text = "📋";
            // 
            // pnlCardUsers
            // 
            this.pnlCardUsers.BackColor = System.Drawing.Color.White;
            this.pnlCardUsers.Controls.Add(this.panelUserStrip);
            this.pnlCardUsers.Controls.Add(this.lblUserTitle);
            this.pnlCardUsers.Controls.Add(this.lblUserCount);
            this.pnlCardUsers.Controls.Add(this.lblIconUser);
            this.pnlCardUsers.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlCardUsers.Location = new System.Drawing.Point(5, 5);
            this.pnlCardUsers.Margin = new System.Windows.Forms.Padding(5);
            this.pnlCardUsers.Name = "pnlCardUsers";
            this.pnlCardUsers.Size = new System.Drawing.Size(280, 130);
            this.pnlCardUsers.TabIndex = 0;
            // 
            // panelUserStrip
            // 
            this.panelUserStrip.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(120)))), ((int)(((byte)(215)))));
            this.panelUserStrip.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelUserStrip.Location = new System.Drawing.Point(0, 0);
            this.panelUserStrip.Name = "panelUserStrip";
            this.panelUserStrip.Size = new System.Drawing.Size(5, 130);
            this.panelUserStrip.TabIndex = 3;
            // 
            // lblUserTitle
            // 
            this.lblUserTitle.AutoSize = true;
            this.lblUserTitle.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblUserTitle.ForeColor = System.Drawing.Color.Gray;
            this.lblUserTitle.Location = new System.Drawing.Point(20, 100);
            this.lblUserTitle.Name = "lblUserTitle";
            this.lblUserTitle.Size = new System.Drawing.Size(83, 15);
            this.lblUserTitle.TabIndex = 2;
            this.lblUserTitle.Text = "Tổng số Users";
            // 
            // lblUserCount
            // 
            this.lblUserCount.AutoSize = true;
            this.lblUserCount.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Bold);
            this.lblUserCount.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(120)))), ((int)(((byte)(215)))));
            this.lblUserCount.Location = new System.Drawing.Point(15, 55);
            this.lblUserCount.Name = "lblUserCount";
            this.lblUserCount.Size = new System.Drawing.Size(38, 45);
            this.lblUserCount.TabIndex = 1;
            this.lblUserCount.Text = "0";
            // 
            // lblIconUser
            // 
            this.lblIconUser.AutoSize = true;
            this.lblIconUser.Font = new System.Drawing.Font("Segoe UI Emoji", 20F);
            this.lblIconUser.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(120)))), ((int)(((byte)(215)))));
            this.lblIconUser.Location = new System.Drawing.Point(20, 15);
            this.lblIconUser.Name = "lblIconUser";
            this.lblIconUser.Size = new System.Drawing.Size(43, 37);
            this.lblIconUser.TabIndex = 0;
            this.lblIconUser.Text = "👥";
            // 
            // lblStatsTitle
            // 
            this.lblStatsTitle.AutoSize = true;
            this.lblStatsTitle.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblStatsTitle.Location = new System.Drawing.Point(5, 0);
            this.lblStatsTitle.Name = "lblStatsTitle";
            this.lblStatsTitle.Size = new System.Drawing.Size(163, 21);
            this.lblStatsTitle.TabIndex = 0;
            this.lblStatsTitle.Text = "Thống kê tổng quan";
            // 
            // lblDashboardTitle
            // 
            this.lblDashboardTitle.AutoSize = true;
            this.lblDashboardTitle.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold);
            this.lblDashboardTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(33)))), ((int)(((byte)(33)))));
            this.lblDashboardTitle.Location = new System.Drawing.Point(20, 20);
            this.lblDashboardTitle.Name = "lblDashboardTitle";
            this.lblDashboardTitle.Size = new System.Drawing.Size(248, 37);
            this.lblDashboardTitle.TabIndex = 0;
            this.lblDashboardTitle.Text = "Admin Dashboard";
            // 
            // ucAdminDashboard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.Controls.Add(this.pnlMain);
            this.Name = "ucAdminDashboard";
            this.Size = new System.Drawing.Size(1200, 800);
            this.pnlMain.ResumeLayout(false);
            this.pnlMain.PerformLayout();
            this.pnlQuickAccess.ResumeLayout(false);
            this.pnlQuickAccess.PerformLayout();
            this.tblQuickAccess.ResumeLayout(false);
            this.pnlRecentActivity.ResumeLayout(false);
            this.pnlRecentActivity.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRecentActivity)).EndInit();
            this.pnlStats.ResumeLayout(false);
            this.pnlStats.PerformLayout();
            this.tblStats.ResumeLayout(false);
            this.pnlCardRate.ResumeLayout(false);
            this.pnlCardRate.PerformLayout();
            this.pnlCardSubmissions.ResumeLayout(false);
            this.pnlCardSubmissions.PerformLayout();
            this.pnlCardAssignments.ResumeLayout(false);
            this.pnlCardAssignments.PerformLayout();
            this.pnlCardUsers.ResumeLayout(false);
            this.pnlCardUsers.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlMain;
        private System.Windows.Forms.Label lblDashboardTitle;
        private System.Windows.Forms.Panel pnlStats;
        private System.Windows.Forms.Label lblStatsTitle;
        private System.Windows.Forms.TableLayoutPanel tblStats;
        private System.Windows.Forms.Panel pnlCardUsers;
        private System.Windows.Forms.Label lblIconUser;
        private System.Windows.Forms.Label lblUserCount;
        private System.Windows.Forms.Label lblUserTitle;
        private System.Windows.Forms.Panel pnlCardAssignments;
        private System.Windows.Forms.Label lblIconAssignment;
        private System.Windows.Forms.Label lblAssignmentCount;
        private System.Windows.Forms.Label lblAssignmentTitle;
        private System.Windows.Forms.Panel pnlCardSubmissions;
        private System.Windows.Forms.Label lblIconSubmission;
        private System.Windows.Forms.Label lblSubmissionCount;
        private System.Windows.Forms.Label lblSubmissionTitle;
        private System.Windows.Forms.Panel pnlCardRate;
        private System.Windows.Forms.Label lblIconRate;
        private System.Windows.Forms.Label lblRateValue;
        private System.Windows.Forms.Label lblRateTitle;
        private System.Windows.Forms.Panel pnlRecentActivity;
        private System.Windows.Forms.Label lblRecentActivityTitle;
        private System.Windows.Forms.DataGridView dgvRecentActivity;
        private System.Windows.Forms.DataGridViewTextBoxColumn colIndex;
        private System.Windows.Forms.DataGridViewTextBoxColumn colUser;
        private System.Windows.Forms.DataGridViewTextBoxColumn colAction;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDetail;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTime;
        private System.Windows.Forms.Panel pnlQuickAccess;
        private System.Windows.Forms.Label lblQuickAccessTitle;
        private System.Windows.Forms.TableLayoutPanel tblQuickAccess;
        private System.Windows.Forms.Button btnQuickUsers;
        private System.Windows.Forms.Button btnQuickAssignments;
        private System.Windows.Forms.Button btnQuickLogs;
        private System.Windows.Forms.Panel panelUserStrip;
        private System.Windows.Forms.Panel panelRateStrip;
        private System.Windows.Forms.Panel panelSubmissionStrip;
        private System.Windows.Forms.Panel panelAssignmentStrip;
    }
}