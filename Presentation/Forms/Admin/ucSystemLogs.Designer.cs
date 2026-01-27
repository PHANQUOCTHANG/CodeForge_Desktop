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
            this.lblTotalLogs = new System.Windows.Forms.Label();
            this.cmbSource = new System.Windows.Forms.ComboBox();
            this.cmbLevel = new System.Windows.Forms.ComboBox();
            this.btnFilter = new System.Windows.Forms.Button();
            this.btnExport = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.dgvLogs = new System.Windows.Forms.DataGridView();
            this.colTimestamp = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colLevel = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSource = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colMessage = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colUser = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pnlBottom = new System.Windows.Forms.Panel();
            this.pnlPagination = new System.Windows.Forms.Panel();
            this.btnNext = new System.Windows.Forms.Button();
            this.lblPageInfo = new System.Windows.Forms.Label();
            this.btnPrev = new System.Windows.Forms.Button();
            this.flowBadges = new System.Windows.Forms.FlowLayoutPanel();
            this.lblInfoBadge = new System.Windows.Forms.Label();
            this.lblWarningBadge = new System.Windows.Forms.Label();
            this.lblErrorBadge = new System.Windows.Forms.Label();
            this.pnlTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLogs)).BeginInit();
            this.pnlBottom.SuspendLayout();
            this.pnlPagination.SuspendLayout();
            this.flowBadges.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlTop
            // 
            this.pnlTop.BackColor = System.Drawing.Color.White;
            this.pnlTop.Controls.Add(this.lblTotalLogs);
            this.pnlTop.Controls.Add(this.cmbSource);
            this.pnlTop.Controls.Add(this.cmbLevel);
            this.pnlTop.Controls.Add(this.btnFilter);
            this.pnlTop.Controls.Add(this.btnExport);
            this.pnlTop.Controls.Add(this.btnRefresh);
            this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTop.Location = new System.Drawing.Point(0, 0);
            this.pnlTop.Name = "pnlTop";
            this.pnlTop.Padding = new System.Windows.Forms.Padding(15);
            this.pnlTop.Size = new System.Drawing.Size(1200, 60);
            this.pnlTop.TabIndex = 0;
            // 
            // lblTotalLogs
            // 
            this.lblTotalLogs.Dock = System.Windows.Forms.DockStyle.Right;
            this.lblTotalLogs.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblTotalLogs.ForeColor = System.Drawing.Color.OrangeRed;
            this.lblTotalLogs.Location = new System.Drawing.Point(1000, 15);
            this.lblTotalLogs.Name = "lblTotalLogs";
            this.lblTotalLogs.Size = new System.Drawing.Size(185, 30);
            this.lblTotalLogs.TabIndex = 5;
            this.lblTotalLogs.Text = "Total Logs: 0";
            this.lblTotalLogs.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cmbSource
            // 
            this.cmbSource.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSource.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cmbSource.ForeColor = System.Drawing.Color.OrangeRed;
            this.cmbSource.FormattingEnabled = true;
            this.cmbSource.Items.AddRange(new object[] {
            "All Sources",
            "Auth",
            "System",
            "Database"});
            this.cmbSource.Location = new System.Drawing.Point(326, 14);
            this.cmbSource.Name = "cmbSource";
            this.cmbSource.Size = new System.Drawing.Size(100, 31);
            this.cmbSource.TabIndex = 3;
            // 
            // cmbLevel
            // 
            this.cmbLevel.BackColor = System.Drawing.SystemColors.Window;
            this.cmbLevel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbLevel.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cmbLevel.ForeColor = System.Drawing.Color.Tomato;
            this.cmbLevel.FormattingEnabled = true;
            this.cmbLevel.Items.AddRange(new object[] {
            "All Levels",
            "INFO",
            "WARNING",
            "ERROR"});
            this.cmbLevel.Location = new System.Drawing.Point(215, 14);
            this.cmbLevel.Name = "cmbLevel";
            this.cmbLevel.Size = new System.Drawing.Size(100, 31);
            this.cmbLevel.TabIndex = 2;
            // 
            // btnFilter
            // 
            this.btnFilter.BackColor = System.Drawing.Color.White;
            this.btnFilter.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnFilter.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnFilter.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFilter.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnFilter.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnFilter.Location = new System.Drawing.Point(440, 12);
            this.btnFilter.Name = "btnFilter";
            this.btnFilter.Size = new System.Drawing.Size(80, 36);
            this.btnFilter.TabIndex = 4;
            this.btnFilter.Text = "Filter";
            this.btnFilter.UseVisualStyleBackColor = false;
            // 
            // btnExport
            // 
            this.btnExport.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(167)))), ((int)(((byte)(69)))));
            this.btnExport.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnExport.FlatAppearance.BorderSize = 0;
            this.btnExport.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExport.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.btnExport.ForeColor = System.Drawing.Color.White;
            this.btnExport.Location = new System.Drawing.Point(115, 12);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(90, 36);
            this.btnExport.TabIndex = 1;
            this.btnExport.Text = "📥 Export";
            this.btnExport.UseVisualStyleBackColor = false;
            // 
            // btnRefresh
            // 
            this.btnRefresh.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(13)))), ((int)(((byte)(110)))), ((int)(((byte)(253)))));
            this.btnRefresh.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRefresh.FlatAppearance.BorderSize = 0;
            this.btnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRefresh.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.btnRefresh.ForeColor = System.Drawing.Color.White;
            this.btnRefresh.Location = new System.Drawing.Point(15, 12);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(90, 36);
            this.btnRefresh.TabIndex = 0;
            this.btnRefresh.Text = "↻ Refresh";
            this.btnRefresh.UseVisualStyleBackColor = false;
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
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            dataGridViewCellStyle1.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvLogs.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvLogs.ColumnHeadersHeight = 45;
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
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(37)))), ((int)(((byte)(41)))));
            dataGridViewCellStyle2.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(245)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvLogs.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvLogs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvLogs.EnableHeadersVisualStyles = false;
            this.dgvLogs.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.dgvLogs.Location = new System.Drawing.Point(0, 60);
            this.dgvLogs.Name = "dgvLogs";
            this.dgvLogs.ReadOnly = true;
            this.dgvLogs.RowHeadersVisible = false;
            this.dgvLogs.RowHeadersWidth = 51;
            this.dgvLogs.RowTemplate.Height = 45;
            this.dgvLogs.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvLogs.Size = new System.Drawing.Size(1200, 630);
            this.dgvLogs.TabIndex = 0;
            // 
            // colTimestamp
            // 
            this.colTimestamp.HeaderText = "Timestamp";
            this.colTimestamp.MinimumWidth = 6;
            this.colTimestamp.Name = "colTimestamp";
            this.colTimestamp.ReadOnly = true;
            this.colTimestamp.Width = 160;
            // 
            // colLevel
            // 
            this.colLevel.HeaderText = "Level";
            this.colLevel.MinimumWidth = 6;
            this.colLevel.Name = "colLevel";
            this.colLevel.ReadOnly = true;
            this.colLevel.Width = 125;
            // 
            // colSource
            // 
            this.colSource.HeaderText = "Source";
            this.colSource.MinimumWidth = 6;
            this.colSource.Name = "colSource";
            this.colSource.ReadOnly = true;
            this.colSource.Width = 120;
            // 
            // colMessage
            // 
            this.colMessage.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colMessage.HeaderText = "Message";
            this.colMessage.MinimumWidth = 6;
            this.colMessage.Name = "colMessage";
            this.colMessage.ReadOnly = true;
            // 
            // colUser
            // 
            this.colUser.HeaderText = "User";
            this.colUser.MinimumWidth = 6;
            this.colUser.Name = "colUser";
            this.colUser.ReadOnly = true;
            this.colUser.Width = 120;
            // 
            // pnlBottom
            // 
            this.pnlBottom.BackColor = System.Drawing.Color.White;
            this.pnlBottom.Controls.Add(this.pnlPagination);
            this.pnlBottom.Controls.Add(this.flowBadges);
            this.pnlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlBottom.Location = new System.Drawing.Point(0, 690);
            this.pnlBottom.Name = "pnlBottom";
            this.pnlBottom.Padding = new System.Windows.Forms.Padding(15, 10, 15, 10);
            this.pnlBottom.Size = new System.Drawing.Size(1200, 60);
            this.pnlBottom.TabIndex = 2;
            // 
            // pnlPagination
            // 
            this.pnlPagination.Controls.Add(this.btnNext);
            this.pnlPagination.Controls.Add(this.lblPageInfo);
            this.pnlPagination.Controls.Add(this.btnPrev);
            this.pnlPagination.Dock = System.Windows.Forms.DockStyle.Right;
            this.pnlPagination.Location = new System.Drawing.Point(885, 10);
            this.pnlPagination.Name = "pnlPagination";
            this.pnlPagination.Size = new System.Drawing.Size(300, 40);
            this.pnlPagination.TabIndex = 1;
            // 
            // btnNext
            // 
            this.btnNext.BackColor = System.Drawing.Color.Transparent;
            this.btnNext.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNext.ForeColor = System.Drawing.Color.Chartreuse;
            this.btnNext.Location = new System.Drawing.Point(205, 5);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(75, 30);
            this.btnNext.TabIndex = 0;
            this.btnNext.Text = "Next >";
            this.btnNext.UseVisualStyleBackColor = false;
            // 
            // lblPageInfo
            // 
            this.lblPageInfo.AutoSize = true;
            this.lblPageInfo.Location = new System.Drawing.Point(125, 12);
            this.lblPageInfo.Name = "lblPageInfo";
            this.lblPageInfo.Size = new System.Drawing.Size(67, 16);
            this.lblPageInfo.TabIndex = 1;
            this.lblPageInfo.Text = "Page 1 / 1";
            // 
            // btnPrev
            // 
            this.btnPrev.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPrev.ForeColor = System.Drawing.Color.Goldenrod;
            this.btnPrev.Location = new System.Drawing.Point(40, 5);
            this.btnPrev.Name = "btnPrev";
            this.btnPrev.Size = new System.Drawing.Size(75, 30);
            this.btnPrev.TabIndex = 2;
            this.btnPrev.Text = "< Prev";
            // 
            // flowBadges
            // 
            this.flowBadges.Controls.Add(this.lblInfoBadge);
            this.flowBadges.Controls.Add(this.lblWarningBadge);
            this.flowBadges.Controls.Add(this.lblErrorBadge);
            this.flowBadges.Dock = System.Windows.Forms.DockStyle.Left;
            this.flowBadges.Location = new System.Drawing.Point(15, 10);
            this.flowBadges.Name = "flowBadges";
            this.flowBadges.Size = new System.Drawing.Size(400, 40);
            this.flowBadges.TabIndex = 0;
            this.flowBadges.WrapContents = false;
            // 
            // lblInfoBadge
            // 
            this.lblInfoBadge.AutoSize = true;
            this.lblInfoBadge.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(245)))), ((int)(((byte)(254)))));
            this.lblInfoBadge.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblInfoBadge.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(3)))), ((int)(((byte)(169)))), ((int)(((byte)(244)))));
            this.lblInfoBadge.Location = new System.Drawing.Point(0, 5);
            this.lblInfoBadge.Margin = new System.Windows.Forms.Padding(0, 5, 15, 0);
            this.lblInfoBadge.Name = "lblInfoBadge";
            this.lblInfoBadge.Padding = new System.Windows.Forms.Padding(10, 5, 10, 5);
            this.lblInfoBadge.Size = new System.Drawing.Size(78, 30);
            this.lblInfoBadge.TabIndex = 0;
            this.lblInfoBadge.Text = "INFO 0";
            // 
            // lblWarningBadge
            // 
            this.lblWarningBadge.AutoSize = true;
            this.lblWarningBadge.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(248)))), ((int)(((byte)(225)))));
            this.lblWarningBadge.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblWarningBadge.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(152)))), ((int)(((byte)(0)))));
            this.lblWarningBadge.Location = new System.Drawing.Point(93, 5);
            this.lblWarningBadge.Margin = new System.Windows.Forms.Padding(0, 5, 15, 0);
            this.lblWarningBadge.Name = "lblWarningBadge";
            this.lblWarningBadge.Padding = new System.Windows.Forms.Padding(10, 5, 10, 5);
            this.lblWarningBadge.Size = new System.Drawing.Size(117, 30);
            this.lblWarningBadge.TabIndex = 1;
            this.lblWarningBadge.Text = "WARNING 0";
            // 
            // lblErrorBadge
            // 
            this.lblErrorBadge.AutoSize = true;
            this.lblErrorBadge.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(235)))), ((int)(((byte)(238)))));
            this.lblErrorBadge.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblErrorBadge.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(233)))), ((int)(((byte)(30)))), ((int)(((byte)(99)))));
            this.lblErrorBadge.Location = new System.Drawing.Point(225, 5);
            this.lblErrorBadge.Margin = new System.Windows.Forms.Padding(0, 5, 15, 0);
            this.lblErrorBadge.Name = "lblErrorBadge";
            this.lblErrorBadge.Padding = new System.Windows.Forms.Padding(10, 5, 10, 5);
            this.lblErrorBadge.Size = new System.Drawing.Size(91, 30);
            this.lblErrorBadge.TabIndex = 2;
            this.lblErrorBadge.Text = "ERROR 0";
            // 
            // ucSystemLogs
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(250)))), ((int)(((byte)(250)))));
            this.Controls.Add(this.dgvLogs);
            this.Controls.Add(this.pnlBottom);
            this.Controls.Add(this.pnlTop);
            this.Name = "ucSystemLogs";
            this.Size = new System.Drawing.Size(1200, 750);
            this.pnlTop.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvLogs)).EndInit();
            this.pnlBottom.ResumeLayout(false);
            this.pnlPagination.ResumeLayout(false);
            this.pnlPagination.PerformLayout();
            this.flowBadges.ResumeLayout(false);
            this.flowBadges.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlTop;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Button btnExport;
        private System.Windows.Forms.Button btnFilter;
        private System.Windows.Forms.ComboBox cmbLevel;
        private System.Windows.Forms.ComboBox cmbSource;
        private System.Windows.Forms.Label lblTotalLogs;

        private System.Windows.Forms.DataGridView dgvLogs;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTimestamp;
        private System.Windows.Forms.DataGridViewTextBoxColumn colLevel;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSource;
        private System.Windows.Forms.DataGridViewTextBoxColumn colMessage;
        private System.Windows.Forms.DataGridViewTextBoxColumn colUser;

        private System.Windows.Forms.Panel pnlBottom;
        private System.Windows.Forms.FlowLayoutPanel flowBadges;
        private System.Windows.Forms.Label lblInfoBadge;
        private System.Windows.Forms.Label lblWarningBadge;
        private System.Windows.Forms.Label lblErrorBadge;

        private System.Windows.Forms.Panel pnlPagination;
        private System.Windows.Forms.Button btnPrev;
        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.Label lblPageInfo;
    }
}