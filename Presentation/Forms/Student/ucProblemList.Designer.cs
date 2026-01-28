namespace CodeForge_Desktop.Presentation.Forms.Student
{
    partial class ucProblemList
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
            this.dgvProblemList = new System.Windows.Forms.DataGridView();
            this.colHash = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colProblemName = new System.Windows.Forms.DataGridViewLinkColumn();
            this.colDifficulty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TagProblem = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colStatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pnlFilters = new System.Windows.Forms.Panel();
            this.cmbDifficulty = new System.Windows.Forms.ComboBox();
            this.lblDifficultyFilter = new System.Windows.Forms.Label();
            this.pnlSearchContainer = new System.Windows.Forms.Panel();
            this.picSearchIcon = new System.Windows.Forms.PictureBox();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.pnlStats = new System.Windows.Forms.Panel();
            this.lblNotStarted = new System.Windows.Forms.Label();
            this.lblAttempted = new System.Windows.Forms.Label();
            this.lblSolved = new System.Windows.Forms.Label();
            this.lblTotal = new System.Windows.Forms.Label();
            this.lblDescription = new System.Windows.Forms.Label();
            this.lblTitle = new System.Windows.Forms.Label();
            this.pnlMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvProblemList)).BeginInit();
            this.pnlFilters.SuspendLayout();
            this.pnlSearchContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picSearchIcon)).BeginInit();
            this.pnlHeader.SuspendLayout();
            this.pnlStats.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlMain
            // 
            this.pnlMain.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.pnlMain.Controls.Add(this.dgvProblemList);
            this.pnlMain.Controls.Add(this.pnlFilters);
            this.pnlMain.Controls.Add(this.pnlHeader);
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Location = new System.Drawing.Point(0, 0);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Padding = new System.Windows.Forms.Padding(20, 20, 20, 20);
            this.pnlMain.Size = new System.Drawing.Size(1000, 600);
            this.pnlMain.TabIndex = 0;
            // 
            // dgvProblemList
            // 
            this.dgvProblemList.AllowUserToAddRows = false;
            this.dgvProblemList.AllowUserToDeleteRows = false;
            this.dgvProblemList.AllowUserToResizeRows = false;
            this.dgvProblemList.BackgroundColor = System.Drawing.Color.White;
            this.dgvProblemList.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvProblemList.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.dgvProblemList.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dgvProblemList.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.dgvProblemList.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.dgvProblemList.ColumnHeadersDefaultCellStyle.ForeColor = System.Drawing.Color.White;
            this.dgvProblemList.ColumnHeadersDefaultCellStyle.Padding = new System.Windows.Forms.Padding(16, 12, 16, 12);
            this.dgvProblemList.ColumnHeadersDefaultCellStyle.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.dgvProblemList.ColumnHeadersDefaultCellStyle.SelectionForeColor = System.Drawing.Color.White;
            this.dgvProblemList.ColumnHeadersHeight = 50;
            this.dgvProblemList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvProblemList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colHash,
            this.colProblemName,
            this.colDifficulty,
            this.TagProblem,
            this.colStatus});
            this.dgvProblemList.DefaultCellStyle.BackColor = System.Drawing.Color.White;
            this.dgvProblemList.DefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.dgvProblemList.DefaultCellStyle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.dgvProblemList.DefaultCellStyle.Padding = new System.Windows.Forms.Padding(16, 8, 16, 8);
            this.dgvProblemList.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(244)))), ((int)(((byte)(246)))));
            this.dgvProblemList.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.dgvProblemList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvProblemList.EnableHeadersVisualStyles = false;
            this.dgvProblemList.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(232)))), ((int)(((byte)(240)))));
            this.dgvProblemList.Location = new System.Drawing.Point(20, 210);
            this.dgvProblemList.MultiSelect = false;
            this.dgvProblemList.Name = "dgvProblemList";
            this.dgvProblemList.ReadOnly = true;
            this.dgvProblemList.RowHeadersVisible = false;
            this.dgvProblemList.RowTemplate.Height = 56;
            this.dgvProblemList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvProblemList.Size = new System.Drawing.Size(960, 370);
            this.dgvProblemList.TabIndex = 2;
            this.dgvProblemList.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvProblemList_CellContentClick);
            this.dgvProblemList.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.dgvProblemList_CellPainting);
            // 
            // colHash
            // 
            this.colHash.HeaderText = "#";
            this.colHash.Name = "colHash";
            this.colHash.ReadOnly = true;
            this.colHash.Visible = false;
            // 
            // colProblemName
            // 
            this.colProblemName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colProblemName.HeaderText = "Bài tập";
            this.colProblemName.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
            this.colProblemName.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(130)))), ((int)(((byte)(246)))));
            this.colProblemName.Name = "colProblemName";
            this.colProblemName.ReadOnly = true;
            this.colProblemName.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.colProblemName.TrackVisitedState = false;
            this.colProblemName.VisitedLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(99)))), ((int)(((byte)(102)))), ((int)(((byte)(241)))));
            // 
            // colDifficulty
            // 
            this.colDifficulty.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.colDifficulty.HeaderText = "Độ khó";
            this.colDifficulty.Name = "colDifficulty";
            this.colDifficulty.ReadOnly = true;
            this.colDifficulty.Width = 150;
            // 
            // TagProblem
            // 
            this.TagProblem.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.TagProblem.HeaderText = "Chuyên đề";
            this.TagProblem.Name = "TagProblem";
            this.TagProblem.ReadOnly = true;
            this.TagProblem.Width = 220;
            // 
            // colStatus
            // 
            this.colStatus.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.colStatus.HeaderText = "Trạng thái";
            this.colStatus.Name = "colStatus";
            this.colStatus.ReadOnly = true;
            this.colStatus.Width = 150;
            // 
            // pnlFilters
            // 
            this.pnlFilters.BackColor = System.Drawing.Color.White;
            this.pnlFilters.Controls.Add(this.cmbDifficulty);
            this.pnlFilters.Controls.Add(this.lblDifficultyFilter);
            this.pnlFilters.Controls.Add(this.pnlSearchContainer);
            this.pnlFilters.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlFilters.Location = new System.Drawing.Point(20, 140);
            this.pnlFilters.Name = "pnlFilters";
            this.pnlFilters.Padding = new System.Windows.Forms.Padding(20, 18, 20, 18);
            this.pnlFilters.Size = new System.Drawing.Size(960, 70);
            this.pnlFilters.TabIndex = 1;
            // 
            // cmbDifficulty
            // 
            this.cmbDifficulty.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbDifficulty.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.cmbDifficulty.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDifficulty.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbDifficulty.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.cmbDifficulty.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.cmbDifficulty.FormattingEnabled = true;
            this.cmbDifficulty.Items.AddRange(new object[] {
            "Tất cả",
            "Dễ",
            "Trung bình",
            "Khó"});
            this.cmbDifficulty.Location = new System.Drawing.Point(790, 20);
            this.cmbDifficulty.Name = "cmbDifficulty";
            this.cmbDifficulty.Size = new System.Drawing.Size(150, 25);
            this.cmbDifficulty.TabIndex = 2;
            // 
            // lblDifficultyFilter
            // 
            this.lblDifficultyFilter.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblDifficultyFilter.AutoSize = true;
            this.lblDifficultyFilter.Font = new System.Drawing.Font("Segoe UI Semibold", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblDifficultyFilter.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.lblDifficultyFilter.Location = new System.Drawing.Point(720, 23);
            this.lblDifficultyFilter.Name = "lblDifficultyFilter";
            this.lblDifficultyFilter.Size = new System.Drawing.Size(54, 17);
            this.lblDifficultyFilter.TabIndex = 1;
            this.lblDifficultyFilter.Text = "Độ khó:";
            // 
            // pnlSearchContainer
            // 
            this.pnlSearchContainer.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlSearchContainer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.pnlSearchContainer.Controls.Add(this.picSearchIcon);
            this.pnlSearchContainer.Controls.Add(this.txtSearch);
            this.pnlSearchContainer.Location = new System.Drawing.Point(20, 16);
            this.pnlSearchContainer.Name = "pnlSearchContainer";
            this.pnlSearchContainer.Padding = new System.Windows.Forms.Padding(14, 10, 14, 10);
            this.pnlSearchContainer.Size = new System.Drawing.Size(680, 38);
            this.pnlSearchContainer.TabIndex = 0;
            // 
            // picSearchIcon
            // 
            this.picSearchIcon.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.picSearchIcon.Image = global::CodeForge_Desktop.Properties.Resources.magnifying_glass;
            this.picSearchIcon.Location = new System.Drawing.Point(14, 10);
            this.picSearchIcon.Name = "picSearchIcon";
            this.picSearchIcon.Size = new System.Drawing.Size(18, 18);
            this.picSearchIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picSearchIcon.TabIndex = 0;
            this.picSearchIcon.TabStop = false;
            // 
            // txtSearch
            // 
            this.txtSearch.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSearch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.txtSearch.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtSearch.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtSearch.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(148)))), ((int)(((byte)(163)))), ((int)(((byte)(184)))));
            this.txtSearch.Location = new System.Drawing.Point(40, 11);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(626, 18);
            this.txtSearch.TabIndex = 1;
            this.txtSearch.Text = "Tìm kiếm bài tập...";
            // 
            // pnlHeader
            // 
            this.pnlHeader.BackColor = System.Drawing.Color.White;
            this.pnlHeader.Controls.Add(this.pnlStats);
            this.pnlHeader.Controls.Add(this.lblDescription);
            this.pnlHeader.Controls.Add(this.lblTitle);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Location = new System.Drawing.Point(20, 20);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Padding = new System.Windows.Forms.Padding(24, 20, 24, 20);
            this.pnlHeader.Size = new System.Drawing.Size(960, 120);
            this.pnlHeader.TabIndex = 0;
            // 
            // pnlStats
            // 
            this.pnlStats.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlStats.Controls.Add(this.lblNotStarted);
            this.pnlStats.Controls.Add(this.lblAttempted);
            this.pnlStats.Controls.Add(this.lblSolved);
            this.pnlStats.Controls.Add(this.lblTotal);
            this.pnlStats.Location = new System.Drawing.Point(600, 20);
            this.pnlStats.Name = "pnlStats";
            this.pnlStats.Size = new System.Drawing.Size(336, 80);
            this.pnlStats.TabIndex = 2;
            // 
            // lblNotStarted
            // 
            this.lblNotStarted.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(232)))), ((int)(((byte)(240)))));
            this.lblNotStarted.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblNotStarted.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.lblNotStarted.Location = new System.Drawing.Point(252, 44);
            this.lblNotStarted.Name = "lblNotStarted";
            this.lblNotStarted.Padding = new System.Windows.Forms.Padding(12, 6, 12, 6);
            this.lblNotStarted.Size = new System.Drawing.Size(80, 30);
            this.lblNotStarted.TabIndex = 3;
            this.lblNotStarted.Text = "Chưa: 0";
            this.lblNotStarted.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblAttempted
            // 
            this.lblAttempted.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(243)))), ((int)(((byte)(199)))));
            this.lblAttempted.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblAttempted.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(83)))), ((int)(((byte)(9)))));
            this.lblAttempted.Location = new System.Drawing.Point(168, 44);
            this.lblAttempted.Name = "lblAttempted";
            this.lblAttempted.Padding = new System.Windows.Forms.Padding(12, 6, 12, 6);
            this.lblAttempted.Size = new System.Drawing.Size(80, 30);
            this.lblAttempted.TabIndex = 2;
            this.lblAttempted.Text = "Đang: 0";
            this.lblAttempted.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblSolved
            // 
            this.lblSolved.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(209)))), ((int)(((byte)(250)))), ((int)(((byte)(229)))));
            this.lblSolved.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblSolved.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(6)))), ((int)(((byte)(95)))), ((int)(((byte)(70)))));
            this.lblSolved.Location = new System.Drawing.Point(84, 44);
            this.lblSolved.Name = "lblSolved";
            this.lblSolved.Padding = new System.Windows.Forms.Padding(12, 6, 12, 6);
            this.lblSolved.Size = new System.Drawing.Size(80, 30);
            this.lblSolved.TabIndex = 1;
            this.lblSolved.Text = "Xong: 0";
            this.lblSolved.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblTotal
            // 
            this.lblTotal.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(219)))), ((int)(((byte)(234)))), ((int)(((byte)(254)))));
            this.lblTotal.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblTotal.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(64)))), ((int)(((byte)(175)))));
            this.lblTotal.Location = new System.Drawing.Point(0, 44);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Padding = new System.Windows.Forms.Padding(12, 6, 12, 6);
            this.lblTotal.Size = new System.Drawing.Size(80, 30);
            this.lblTotal.TabIndex = 0;
            this.lblTotal.Text = "Tổng: 0";
            this.lblTotal.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblDescription
            // 
            this.lblDescription.AutoSize = true;
            this.lblDescription.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblDescription.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblDescription.Location = new System.Drawing.Point(24, 56);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Size = new System.Drawing.Size(284, 19);
            this.lblDescription.TabIndex = 1;
            this.lblDescription.Text = "Giải các bài tập để nâng cao kỹ năng lập trình";
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.lblTitle.Location = new System.Drawing.Point(20, 18);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(220, 37);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Bài tập lập trình";
            // 
            // ucProblemList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.Controls.Add(this.pnlMain);
            this.Name = "ucProblemList";
            this.Size = new System.Drawing.Size(1000, 600);
            this.pnlMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvProblemList)).EndInit();
            this.pnlFilters.ResumeLayout(false);
            this.pnlFilters.PerformLayout();
            this.pnlSearchContainer.ResumeLayout(false);
            this.pnlSearchContainer.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picSearchIcon)).EndInit();
            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            this.pnlStats.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlMain;
        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblDescription;
        private System.Windows.Forms.Panel pnlStats;
        private System.Windows.Forms.Label lblTotal;
        private System.Windows.Forms.Label lblSolved;
        private System.Windows.Forms.Label lblAttempted;
        private System.Windows.Forms.Label lblNotStarted;
        private System.Windows.Forms.Panel pnlFilters;
        private System.Windows.Forms.Panel pnlSearchContainer;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.PictureBox picSearchIcon;
        private System.Windows.Forms.ComboBox cmbDifficulty;
        private System.Windows.Forms.Label lblDifficultyFilter;
        private System.Windows.Forms.DataGridView dgvProblemList;
        private System.Windows.Forms.DataGridViewTextBoxColumn colHash;
        private System.Windows.Forms.DataGridViewLinkColumn colProblemName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDifficulty;
        private System.Windows.Forms.DataGridViewTextBoxColumn TagProblem;
        private System.Windows.Forms.DataGridViewTextBoxColumn colStatus;
    }
}