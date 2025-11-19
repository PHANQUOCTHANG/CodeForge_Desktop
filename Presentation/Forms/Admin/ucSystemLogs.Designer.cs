namespace CodeForge_Desktop.Presentation.Forms.Admin
{
    partial class ucSystemLogs
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.pnlTop = new System.Windows.Forms.Panel();
            this.btnApply = new System.Windows.Forms.Button();
            this.cmbSource = new System.Windows.Forms.ComboBox();
            this.cmbLevel = new System.Windows.Forms.ComboBox();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.pnlStats = new System.Windows.Forms.Panel();
            this.tblStats = new System.Windows.Forms.TableLayoutPanel();
            this.pnlTotal = new System.Windows.Forms.Panel();
            this.lblValTotal = new System.Windows.Forms.Label();
            this.lblTitleTotal = new System.Windows.Forms.Label();
            this.pnlError = new System.Windows.Forms.Panel();
            this.lblValError = new System.Windows.Forms.Label();
            this.lblTitleError = new System.Windows.Forms.Label();
            this.pnlWarning = new System.Windows.Forms.Panel();
            this.lblValWarning = new System.Windows.Forms.Label();
            this.lblTitleWarning = new System.Windows.Forms.Label();
            this.pnlInfo = new System.Windows.Forms.Panel();
            this.lblValInfo = new System.Windows.Forms.Label();
            this.lblTitleInfo = new System.Windows.Forms.Label();
            this.pnlGridContainer = new System.Windows.Forms.Panel();
            this.dgvLogs = new System.Windows.Forms.DataGridView();
            this.colTimestamp = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colLevel = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSource = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colUser = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colMessage = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pnlFooter = new System.Windows.Forms.Panel();
            this.lblFooterStatus = new System.Windows.Forms.Label();
            this.btnExport = new System.Windows.Forms.Button();
            this.pnlTop.SuspendLayout();
            this.pnlStats.SuspendLayout();
            this.tblStats.SuspendLayout();
            this.pnlTotal.SuspendLayout();
            this.pnlError.SuspendLayout();
            this.pnlWarning.SuspendLayout();
            this.pnlInfo.SuspendLayout();
            this.pnlGridContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLogs)).BeginInit();
            this.pnlFooter.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlTop
            // 
            this.pnlTop.BackColor = System.Drawing.Color.WhiteSmoke;
            this.pnlTop.Controls.Add(this.btnApply);
            this.pnlTop.Controls.Add(this.cmbSource);
            this.pnlTop.Controls.Add(this.cmbLevel);
            this.pnlTop.Controls.Add(this.btnExport);
            this.pnlTop.Controls.Add(this.btnRefresh);
            this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTop.Location = new System.Drawing.Point(0, 0);
            this.pnlTop.Name = "pnlTop";
            this.pnlTop.Size = new System.Drawing.Size(1100, 50);
            this.pnlTop.TabIndex = 0;
            // 
            // btnApply
            // 
            this.btnApply.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnApply.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.btnApply.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnApply.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnApply.Location = new System.Drawing.Point(345, 10);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new System.Drawing.Size(75, 30);
            this.btnApply.TabIndex = 4;
            this.btnApply.Text = "Lọc";
            this.btnApply.UseVisualStyleBackColor = false;
            // 
            // cmbSource
            // 
            this.cmbSource.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.cmbSource.FormattingEnabled = true;
            this.cmbSource.Items.AddRange(new object[] {
            "All Sources",
            "Auth",
            "System",
            "Database"});
            this.cmbSource.Location = new System.Drawing.Point(235, 11);
            this.cmbSource.Name = "cmbSource";
            this.cmbSource.Size = new System.Drawing.Size(100, 28);
            this.cmbSource.TabIndex = 3;
            this.cmbSource.Text = "All Sources";
            // 
            // cmbLevel
            // 
            this.cmbLevel.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.cmbLevel.FormattingEnabled = true;
            this.cmbLevel.Items.AddRange(new object[] {
            "All Levels",
            "INFO",
            "WARNING",
            "ERROR"});
            this.cmbLevel.Location = new System.Drawing.Point(125, 11);
            this.cmbLevel.Name = "cmbLevel";
            this.cmbLevel.Size = new System.Drawing.Size(100, 28);
            this.cmbLevel.TabIndex = 2;
            this.cmbLevel.Text = "All Levels";
            // 
            // btnRefresh
            // 
            this.btnRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRefresh.BackColor = System.Drawing.Color.White;
            this.btnRefresh.FlatAppearance.BorderColor = System.Drawing.Color.DodgerBlue;
            this.btnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRefresh.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnRefresh.ForeColor = System.Drawing.Color.DodgerBlue;
            this.btnRefresh.Location = new System.Drawing.Point(1005, 10);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(80, 30);
            this.btnRefresh.TabIndex = 0;
            this.btnRefresh.Text = "↻ Refresh";
            this.btnRefresh.UseVisualStyleBackColor = false;
            // 
            // pnlStats
            // 
            this.pnlStats.Controls.Add(this.tblStats);
            this.pnlStats.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlStats.Location = new System.Drawing.Point(0, 50);
            this.pnlStats.Name = "pnlStats";
            this.pnlStats.Padding = new System.Windows.Forms.Padding(10);
            this.pnlStats.Size = new System.Drawing.Size(1100, 100);
            this.pnlStats.TabIndex = 1;
            // 
            // tblStats
            // 
            this.tblStats.ColumnCount = 4;
            this.tblStats.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tblStats.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tblStats.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tblStats.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tblStats.Controls.Add(this.pnlTotal, 3, 0);
            this.tblStats.Controls.Add(this.pnlError, 2, 0);
            this.tblStats.Controls.Add(this.pnlWarning, 1, 0);
            this.tblStats.Controls.Add(this.pnlInfo, 0, 0);
            this.tblStats.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblStats.Location = new System.Drawing.Point(10, 10);
            this.tblStats.Name = "tblStats";
            this.tblStats.RowCount = 1;
            this.tblStats.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblStats.Size = new System.Drawing.Size(1080, 80);
            this.tblStats.TabIndex = 0;
            // 
            // pnlTotal
            // 
            this.pnlTotal.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.pnlTotal.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlTotal.Controls.Add(this.lblValTotal);
            this.pnlTotal.Controls.Add(this.lblTitleTotal);
            this.pnlTotal.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlTotal.Location = new System.Drawing.Point(813, 3);
            this.pnlTotal.Name = "pnlTotal";
            this.pnlTotal.Size = new System.Drawing.Size(264, 74);
            this.pnlTotal.TabIndex = 3;
            // 
            // lblValTotal
            // 
            this.lblValTotal.AutoSize = true;
            this.lblValTotal.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblValTotal.ForeColor = System.Drawing.Color.DimGray;
            this.lblValTotal.Location = new System.Drawing.Point(10, 30);
            this.lblValTotal.Name = "lblValTotal";
            this.lblValTotal.Size = new System.Drawing.Size(49, 37);
            this.lblValTotal.TabIndex = 1;
            this.lblValTotal.Text = "10";
            // 
            // lblTitleTotal
            // 
            this.lblTitleTotal.AutoSize = true;
            this.lblTitleTotal.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblTitleTotal.ForeColor = System.Drawing.Color.Gray;
            this.lblTitleTotal.Location = new System.Drawing.Point(10, 5);
            this.lblTitleTotal.Name = "lblTitleTotal";
            this.lblTitleTotal.Size = new System.Drawing.Size(42, 20);
            this.lblTitleTotal.TabIndex = 0;
            this.lblTitleTotal.Text = "Total";
            // 
            // pnlError
            // 
            this.pnlError.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.pnlError.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlError.Controls.Add(this.lblValError);
            this.pnlError.Controls.Add(this.lblTitleError);
            this.pnlError.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlError.Location = new System.Drawing.Point(543, 3);
            this.pnlError.Name = "pnlError";
            this.pnlError.Size = new System.Drawing.Size(264, 74);
            this.pnlError.TabIndex = 2;
            // 
            // lblValError
            // 
            this.lblValError.AutoSize = true;
            this.lblValError.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblValError.ForeColor = System.Drawing.Color.Crimson;
            this.lblValError.Location = new System.Drawing.Point(10, 30);
            this.lblValError.Name = "lblValError";
            this.lblValError.Size = new System.Drawing.Size(33, 37);
            this.lblValError.TabIndex = 1;
            this.lblValError.Text = "2";
            // 
            // lblTitleError
            // 
            this.lblTitleError.AutoSize = true;
            this.lblTitleError.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblTitleError.ForeColor = System.Drawing.Color.Gray;
            this.lblTitleError.Location = new System.Drawing.Point(10, 5);
            this.lblTitleError.Name = "lblTitleError";
            this.lblTitleError.Size = new System.Drawing.Size(55, 20);
            this.lblTitleError.TabIndex = 0;
            this.lblTitleError.Text = "ERROR";
            // 
            // pnlWarning
            // 
            this.pnlWarning.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(250)))), ((int)(((byte)(240)))));
            this.pnlWarning.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlWarning.Controls.Add(this.lblValWarning);
            this.pnlWarning.Controls.Add(this.lblTitleWarning);
            this.pnlWarning.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlWarning.Location = new System.Drawing.Point(273, 3);
            this.pnlWarning.Name = "pnlWarning";
            this.pnlWarning.Size = new System.Drawing.Size(264, 74);
            this.pnlWarning.TabIndex = 1;
            // 
            // lblValWarning
            // 
            this.lblValWarning.AutoSize = true;
            this.lblValWarning.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblValWarning.ForeColor = System.Drawing.Color.Orange;
            this.lblValWarning.Location = new System.Drawing.Point(10, 30);
            this.lblValWarning.Name = "lblValWarning";
            this.lblValWarning.Size = new System.Drawing.Size(33, 37);
            this.lblValWarning.TabIndex = 1;
            this.lblValWarning.Text = "2";
            // 
            // lblTitleWarning
            // 
            this.lblTitleWarning.AutoSize = true;
            this.lblTitleWarning.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblTitleWarning.ForeColor = System.Drawing.Color.Gray;
            this.lblTitleWarning.Location = new System.Drawing.Point(10, 5);
            this.lblTitleWarning.Name = "lblTitleWarning";
            this.lblTitleWarning.Size = new System.Drawing.Size(77, 20);
            this.lblTitleWarning.TabIndex = 0;
            this.lblTitleWarning.Text = "WARNING";
            // 
            // pnlInfo
            // 
            this.pnlInfo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.pnlInfo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlInfo.Controls.Add(this.lblValInfo);
            this.pnlInfo.Controls.Add(this.lblTitleInfo);
            this.pnlInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlInfo.Location = new System.Drawing.Point(3, 3);
            this.pnlInfo.Name = "pnlInfo";
            this.pnlInfo.Size = new System.Drawing.Size(264, 74);
            this.pnlInfo.TabIndex = 0;
            // 
            // lblValInfo
            // 
            this.lblValInfo.AutoSize = true;
            this.lblValInfo.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblValInfo.ForeColor = System.Drawing.Color.DodgerBlue;
            this.lblValInfo.Location = new System.Drawing.Point(10, 30);
            this.lblValInfo.Name = "lblValInfo";
            this.lblValInfo.Size = new System.Drawing.Size(33, 37);
            this.lblValInfo.TabIndex = 1;
            this.lblValInfo.Text = "6";
            // 
            // lblTitleInfo
            // 
            this.lblTitleInfo.AutoSize = true;
            this.lblTitleInfo.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblTitleInfo.ForeColor = System.Drawing.Color.Gray;
            this.lblTitleInfo.Location = new System.Drawing.Point(10, 5);
            this.lblTitleInfo.Name = "lblTitleInfo";
            this.lblTitleInfo.Size = new System.Drawing.Size(42, 20);
            this.lblTitleInfo.TabIndex = 0;
            this.lblTitleInfo.Text = "INFO";
            // 
            // pnlGridContainer
            // 
            this.pnlGridContainer.Controls.Add(this.dgvLogs);
            this.pnlGridContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlGridContainer.Location = new System.Drawing.Point(0, 150);
            this.pnlGridContainer.Name = "pnlGridContainer";
            this.pnlGridContainer.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
            this.pnlGridContainer.Size = new System.Drawing.Size(1100, 510);
            this.pnlGridContainer.TabIndex = 2;
            // 
            // dgvLogs
            // 
            this.dgvLogs.AllowUserToAddRows = false;
            this.dgvLogs.AllowUserToDeleteRows = false;
            this.dgvLogs.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvLogs.BackgroundColor = System.Drawing.Color.White;
            this.dgvLogs.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvLogs.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgvLogs.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvLogs.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dgvLogs.ColumnHeadersHeight = 35;
            this.dgvLogs.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvLogs.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colTimestamp,
            this.colLevel,
            this.colSource,
            this.colUser,
            this.colMessage});
            this.dgvLogs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvLogs.EnableHeadersVisualStyles = false;
            this.dgvLogs.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.dgvLogs.Location = new System.Drawing.Point(10, 0);
            this.dgvLogs.Name = "dgvLogs";
            this.dgvLogs.ReadOnly = true;
            this.dgvLogs.RowHeadersVisible = false;
            this.dgvLogs.RowHeadersWidth = 51;
            this.dgvLogs.RowTemplate.Height = 30;
            this.dgvLogs.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvLogs.Size = new System.Drawing.Size(1080, 510);
            this.dgvLogs.TabIndex = 0;
            // 
            // colTimestamp
            // 
            this.colTimestamp.FillWeight = 20F;
            this.colTimestamp.HeaderText = "Timestamp";
            this.colTimestamp.MinimumWidth = 6;
            this.colTimestamp.Name = "colTimestamp";
            this.colTimestamp.ReadOnly = true;
            // 
            // colLevel
            // 
            this.colLevel.FillWeight = 10F;
            this.colLevel.HeaderText = "Level";
            this.colLevel.MinimumWidth = 6;
            this.colLevel.Name = "colLevel";
            this.colLevel.ReadOnly = true;
            // 
            // colSource
            // 
            this.colSource.FillWeight = 15F;
            this.colSource.HeaderText = "Source";
            this.colSource.MinimumWidth = 6;
            this.colSource.Name = "colSource";
            this.colSource.ReadOnly = true;
            // 
            // colUser
            // 
            this.colUser.FillWeight = 15F;
            this.colUser.HeaderText = "User";
            this.colUser.MinimumWidth = 6;
            this.colUser.Name = "colUser";
            this.colUser.ReadOnly = true;
            // 
            // colMessage
            // 
            this.colMessage.FillWeight = 40F;
            this.colMessage.HeaderText = "Message";
            this.colMessage.MinimumWidth = 6;
            this.colMessage.Name = "colMessage";
            this.colMessage.ReadOnly = true;
            // 
            // pnlFooter
            // 
            this.pnlFooter.BackColor = System.Drawing.Color.WhiteSmoke;
            this.pnlFooter.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlFooter.Controls.Add(this.lblFooterStatus);
            this.pnlFooter.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlFooter.Location = new System.Drawing.Point(0, 660);
            this.pnlFooter.Name = "pnlFooter";
            this.pnlFooter.Size = new System.Drawing.Size(1100, 40);
            this.pnlFooter.TabIndex = 3;
            // 
            // lblFooterStatus
            // 
            this.lblFooterStatus.AutoSize = true;
            this.lblFooterStatus.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblFooterStatus.ForeColor = System.Drawing.Color.Gray;
            this.lblFooterStatus.Location = new System.Drawing.Point(10, 10);
            this.lblFooterStatus.Name = "lblFooterStatus";
            this.lblFooterStatus.Size = new System.Drawing.Size(227, 20);
            this.lblFooterStatus.TabIndex = 0;
            this.lblFooterStatus.Text = "Hiển thị 10 logs | Last updated: ...";
            // 
            // btnExport
            // 
            this.btnExport.BackColor = System.Drawing.Color.White;
            this.btnExport.FlatAppearance.BorderColor = System.Drawing.Color.SeaGreen;
            this.btnExport.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExport.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnExport.ForeColor = System.Drawing.Color.SeaGreen;
            this.btnExport.Image = global::CodeForge_Desktop.Properties.Resources.export;
            this.btnExport.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnExport.Location = new System.Drawing.Point(15, 10);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(88, 30);
            this.btnExport.TabIndex = 1;
            this.btnExport.Text = "   Export";
            this.btnExport.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnExport.UseVisualStyleBackColor = false;
            // 
            // ucSystemLogs
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.pnlGridContainer);
            this.Controls.Add(this.pnlFooter);
            this.Controls.Add(this.pnlStats);
            this.Controls.Add(this.pnlTop);
            this.Name = "ucSystemLogs";
            this.Size = new System.Drawing.Size(1100, 700);
            this.pnlTop.ResumeLayout(false);
            this.pnlStats.ResumeLayout(false);
            this.tblStats.ResumeLayout(false);
            this.pnlTotal.ResumeLayout(false);
            this.pnlTotal.PerformLayout();
            this.pnlError.ResumeLayout(false);
            this.pnlError.PerformLayout();
            this.pnlWarning.ResumeLayout(false);
            this.pnlWarning.PerformLayout();
            this.pnlInfo.ResumeLayout(false);
            this.pnlInfo.PerformLayout();
            this.pnlGridContainer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvLogs)).EndInit();
            this.pnlFooter.ResumeLayout(false);
            this.pnlFooter.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlTop;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Button btnExport;
        private System.Windows.Forms.ComboBox cmbLevel;
        private System.Windows.Forms.ComboBox cmbSource;
        private System.Windows.Forms.Button btnApply;
        private System.Windows.Forms.Panel pnlStats;
        private System.Windows.Forms.TableLayoutPanel tblStats;
        private System.Windows.Forms.Panel pnlTotal;
        private System.Windows.Forms.Label lblValTotal;
        private System.Windows.Forms.Label lblTitleTotal;
        private System.Windows.Forms.Panel pnlError;
        private System.Windows.Forms.Label lblValError;
        private System.Windows.Forms.Label lblTitleError;
        private System.Windows.Forms.Panel pnlWarning;
        private System.Windows.Forms.Label lblValWarning;
        private System.Windows.Forms.Label lblTitleWarning;
        private System.Windows.Forms.Panel pnlInfo;
        private System.Windows.Forms.Label lblValInfo;
        private System.Windows.Forms.Label lblTitleInfo;
        private System.Windows.Forms.Panel pnlGridContainer;
        private System.Windows.Forms.DataGridView dgvLogs;
        private System.Windows.Forms.Panel pnlFooter;
        private System.Windows.Forms.Label lblFooterStatus;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTimestamp;
        private System.Windows.Forms.DataGridViewTextBoxColumn colLevel;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSource;
        private System.Windows.Forms.DataGridViewTextBoxColumn colUser;
        private System.Windows.Forms.DataGridViewTextBoxColumn colMessage;
    }
}