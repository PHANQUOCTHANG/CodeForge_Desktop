namespace CodeForge_Desktop.Presentation.Forms.Student
{
    partial class ucStudentDashboard
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.lblDashboardTitle = new System.Windows.Forms.Label();
            this.pnlStats = new System.Windows.Forms.Panel();
            this.tblStats = new System.Windows.Forms.TableLayoutPanel();
            this.cardAverage = new System.Windows.Forms.Panel();
            this.lblIconAvg = new System.Windows.Forms.Label();
            this.lblValAvg = new System.Windows.Forms.Label();
            this.lblDescAvg = new System.Windows.Forms.Label();
            this.cardInProgress = new System.Windows.Forms.Panel();
            this.lblIconProg = new System.Windows.Forms.Label();
            this.lblValProg = new System.Windows.Forms.Label();
            this.lblDescProg = new System.Windows.Forms.Label();
            this.cardCompleted = new System.Windows.Forms.Panel();
            this.lblIconComp = new System.Windows.Forms.Label();
            this.lblValComp = new System.Windows.Forms.Label();
            this.lblDescComp = new System.Windows.Forms.Label();
            this.cardTotal = new System.Windows.Forms.Panel();
            this.lblIconTotal = new System.Windows.Forms.Label();
            this.lblValTotal = new System.Windows.Forms.Label();
            this.lblDescTotal = new System.Windows.Forms.Label();
            this.lblStatsTitle = new System.Windows.Forms.Label();
            this.pnlRecent = new System.Windows.Forms.Panel();
            this.dgvRecent = new System.Windows.Forms.DataGridView();
            this.colHash = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDeadline = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colStatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colScore = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnViewAll = new System.Windows.Forms.Button();
            this.lblRecentTitle = new System.Windows.Forms.Label();
            this.pnlActions = new System.Windows.Forms.Panel();
            this.btnActionSettings = new System.Windows.Forms.Button();
            this.btnActionHistory = new System.Windows.Forms.Button();
            this.btnActionList = new System.Windows.Forms.Button();
            this.lblActionsTitle = new System.Windows.Forms.Label();
            this.pnlStats.SuspendLayout();
            this.tblStats.SuspendLayout();
            this.cardAverage.SuspendLayout();
            this.cardInProgress.SuspendLayout();
            this.cardCompleted.SuspendLayout();
            this.cardTotal.SuspendLayout();
            this.pnlRecent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRecent)).BeginInit();
            this.pnlActions.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblDashboardTitle
            // 
            this.lblDashboardTitle.AutoSize = true;
            this.lblDashboardTitle.Font = new System.Drawing.Font("Segoe UI", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDashboardTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(33)))), ((int)(((byte)(33)))));
            this.lblDashboardTitle.Location = new System.Drawing.Point(16, 0);
            this.lblDashboardTitle.Name = "lblDashboardTitle";
            this.lblDashboardTitle.Size = new System.Drawing.Size(220, 31);
            this.lblDashboardTitle.TabIndex = 0;
            this.lblDashboardTitle.Text = "Student Dashboard";
            // 
            // pnlStats
            // 
            this.pnlStats.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlStats.BackColor = System.Drawing.Color.White;
            this.pnlStats.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlStats.Controls.Add(this.tblStats);
            this.pnlStats.Controls.Add(this.lblStatsTitle);
            this.pnlStats.Location = new System.Drawing.Point(25, 34);
            this.pnlStats.Name = "pnlStats";
            this.pnlStats.Size = new System.Drawing.Size(1050, 193);
            this.pnlStats.TabIndex = 1;
            // 
            // tblStats
            // 
            this.tblStats.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tblStats.ColumnCount = 4;
            this.tblStats.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tblStats.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tblStats.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tblStats.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tblStats.Controls.Add(this.cardAverage, 3, 0);
            this.tblStats.Controls.Add(this.cardInProgress, 2, 0);
            this.tblStats.Controls.Add(this.cardCompleted, 1, 0);
            this.tblStats.Controls.Add(this.cardTotal, 0, 0);
            this.tblStats.Location = new System.Drawing.Point(15, 50);
            this.tblStats.Name = "tblStats";
            this.tblStats.RowCount = 1;
            this.tblStats.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblStats.Size = new System.Drawing.Size(1020, 120);
            this.tblStats.TabIndex = 1;
            // 
            // cardAverage
            // 
            this.cardAverage.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(250)))), ((int)(((byte)(250)))));
            this.cardAverage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.cardAverage.Controls.Add(this.lblIconAvg);
            this.cardAverage.Controls.Add(this.lblValAvg);
            this.cardAverage.Controls.Add(this.lblDescAvg);
            this.cardAverage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cardAverage.Location = new System.Drawing.Point(768, 3);
            this.cardAverage.Margin = new System.Windows.Forms.Padding(3, 3, 15, 3);
            this.cardAverage.Name = "cardAverage";
            this.cardAverage.Size = new System.Drawing.Size(237, 114);
            this.cardAverage.TabIndex = 3;
            // 
            // lblIconAvg
            // 
            this.lblIconAvg.AutoSize = true;
            this.lblIconAvg.Font = new System.Drawing.Font("Segoe UI Emoji", 19.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblIconAvg.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(156)))), ((int)(((byte)(39)))), ((int)(((byte)(176)))));
            this.lblIconAvg.Location = new System.Drawing.Point(3, 0);
            this.lblIconAvg.Name = "lblIconAvg";
            this.lblIconAvg.Size = new System.Drawing.Size(64, 46);
            this.lblIconAvg.TabIndex = 2;
            this.lblIconAvg.Text = "📈";
            // 
            // lblValAvg
            // 
            this.lblValAvg.AutoSize = true;
            this.lblValAvg.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblValAvg.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(33)))), ((int)(((byte)(33)))));
            this.lblValAvg.Location = new System.Drawing.Point(10, 45);
            this.lblValAvg.Name = "lblValAvg";
            this.lblValAvg.Size = new System.Drawing.Size(78, 41);
            this.lblValAvg.TabIndex = 1;
            this.lblValAvg.Text = "85%";
            // 
            // lblDescAvg
            // 
            this.lblDescAvg.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblDescAvg.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblDescAvg.ForeColor = System.Drawing.Color.Gray;
            this.lblDescAvg.Location = new System.Drawing.Point(0, 87);
            this.lblDescAvg.Name = "lblDescAvg";
            this.lblDescAvg.Padding = new System.Windows.Forms.Padding(15, 0, 0, 5);
            this.lblDescAvg.Size = new System.Drawing.Size(235, 25);
            this.lblDescAvg.TabIndex = 0;
            this.lblDescAvg.Text = "Điểm trung bình";
            this.lblDescAvg.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cardInProgress
            // 
            this.cardInProgress.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(250)))), ((int)(((byte)(250)))));
            this.cardInProgress.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.cardInProgress.Controls.Add(this.lblIconProg);
            this.cardInProgress.Controls.Add(this.lblValProg);
            this.cardInProgress.Controls.Add(this.lblDescProg);
            this.cardInProgress.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cardInProgress.Location = new System.Drawing.Point(513, 3);
            this.cardInProgress.Margin = new System.Windows.Forms.Padding(3, 3, 15, 3);
            this.cardInProgress.Name = "cardInProgress";
            this.cardInProgress.Size = new System.Drawing.Size(237, 114);
            this.cardInProgress.TabIndex = 2;
            // 
            // lblIconProg
            // 
            this.lblIconProg.AutoSize = true;
            this.lblIconProg.Font = new System.Drawing.Font("Segoe UI Emoji", 19.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblIconProg.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(152)))), ((int)(((byte)(0)))));
            this.lblIconProg.Location = new System.Drawing.Point(3, -1);
            this.lblIconProg.Name = "lblIconProg";
            this.lblIconProg.Size = new System.Drawing.Size(67, 46);
            this.lblIconProg.TabIndex = 2;
            this.lblIconProg.Text = "⏰";
            // 
            // lblValProg
            // 
            this.lblValProg.AutoSize = true;
            this.lblValProg.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblValProg.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(33)))), ((int)(((byte)(33)))));
            this.lblValProg.Location = new System.Drawing.Point(11, 46);
            this.lblValProg.Name = "lblValProg";
            this.lblValProg.Size = new System.Drawing.Size(35, 41);
            this.lblValProg.TabIndex = 1;
            this.lblValProg.Text = "3";
            // 
            // lblDescProg
            // 
            this.lblDescProg.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblDescProg.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblDescProg.ForeColor = System.Drawing.Color.Gray;
            this.lblDescProg.Location = new System.Drawing.Point(0, 87);
            this.lblDescProg.Name = "lblDescProg";
            this.lblDescProg.Padding = new System.Windows.Forms.Padding(15, 0, 0, 5);
            this.lblDescProg.Size = new System.Drawing.Size(235, 25);
            this.lblDescProg.TabIndex = 0;
            this.lblDescProg.Text = "Đang làm";
            this.lblDescProg.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cardCompleted
            // 
            this.cardCompleted.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(250)))), ((int)(((byte)(250)))));
            this.cardCompleted.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.cardCompleted.Controls.Add(this.lblIconComp);
            this.cardCompleted.Controls.Add(this.lblValComp);
            this.cardCompleted.Controls.Add(this.lblDescComp);
            this.cardCompleted.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cardCompleted.Location = new System.Drawing.Point(258, 3);
            this.cardCompleted.Margin = new System.Windows.Forms.Padding(3, 3, 15, 3);
            this.cardCompleted.Name = "cardCompleted";
            this.cardCompleted.Size = new System.Drawing.Size(237, 114);
            this.cardCompleted.TabIndex = 1;
            // 
            // lblIconComp
            // 
            this.lblIconComp.AutoSize = true;
            this.lblIconComp.Font = new System.Drawing.Font("Segoe UI Emoji", 19.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblIconComp.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(76)))), ((int)(((byte)(175)))), ((int)(((byte)(80)))));
            this.lblIconComp.Location = new System.Drawing.Point(9, -4);
            this.lblIconComp.Name = "lblIconComp";
            this.lblIconComp.Size = new System.Drawing.Size(45, 46);
            this.lblIconComp.TabIndex = 2;
            this.lblIconComp.Text = "✓";
            // 
            // lblValComp
            // 
            this.lblValComp.AutoSize = true;
            this.lblValComp.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblValComp.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(33)))), ((int)(((byte)(33)))));
            this.lblValComp.Location = new System.Drawing.Point(10, 45);
            this.lblValComp.Name = "lblValComp";
            this.lblValComp.Size = new System.Drawing.Size(52, 41);
            this.lblValComp.TabIndex = 1;
            this.lblValComp.Text = "18";
            // 
            // lblDescComp
            // 
            this.lblDescComp.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblDescComp.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblDescComp.ForeColor = System.Drawing.Color.Gray;
            this.lblDescComp.Location = new System.Drawing.Point(0, 87);
            this.lblDescComp.Name = "lblDescComp";
            this.lblDescComp.Padding = new System.Windows.Forms.Padding(15, 0, 0, 5);
            this.lblDescComp.Size = new System.Drawing.Size(235, 25);
            this.lblDescComp.TabIndex = 0;
            this.lblDescComp.Text = "Đã hoàn thành";
            this.lblDescComp.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cardTotal
            // 
            this.cardTotal.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(250)))), ((int)(((byte)(250)))));
            this.cardTotal.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.cardTotal.Controls.Add(this.lblIconTotal);
            this.cardTotal.Controls.Add(this.lblValTotal);
            this.cardTotal.Controls.Add(this.lblDescTotal);
            this.cardTotal.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cardTotal.Location = new System.Drawing.Point(3, 3);
            this.cardTotal.Margin = new System.Windows.Forms.Padding(3, 3, 15, 3);
            this.cardTotal.Name = "cardTotal";
            this.cardTotal.Size = new System.Drawing.Size(237, 114);
            this.cardTotal.TabIndex = 0;
            // 
            // lblIconTotal
            // 
            this.lblIconTotal.AutoSize = true;
            this.lblIconTotal.Font = new System.Drawing.Font("Segoe UI Emoji", 19.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblIconTotal.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(120)))), ((int)(((byte)(215)))));
            this.lblIconTotal.Location = new System.Drawing.Point(10, 0);
            this.lblIconTotal.Name = "lblIconTotal";
            this.lblIconTotal.Size = new System.Drawing.Size(60, 46);
            this.lblIconTotal.TabIndex = 2;
            this.lblIconTotal.Text = "📄";
            // 
            // lblValTotal
            // 
            this.lblValTotal.AutoSize = true;
            this.lblValTotal.Font = new System.Drawing.Font("Segoe UI", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblValTotal.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(33)))), ((int)(((byte)(33)))));
            this.lblValTotal.Location = new System.Drawing.Point(11, 47);
            this.lblValTotal.Name = "lblValTotal";
            this.lblValTotal.Size = new System.Drawing.Size(49, 38);
            this.lblValTotal.TabIndex = 1;
            this.lblValTotal.Text = "24";
            // 
            // lblDescTotal
            // 
            this.lblDescTotal.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblDescTotal.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblDescTotal.ForeColor = System.Drawing.Color.Gray;
            this.lblDescTotal.Location = new System.Drawing.Point(0, 87);
            this.lblDescTotal.Name = "lblDescTotal";
            this.lblDescTotal.Padding = new System.Windows.Forms.Padding(15, 0, 0, 5);
            this.lblDescTotal.Size = new System.Drawing.Size(235, 25);
            this.lblDescTotal.TabIndex = 0;
            this.lblDescTotal.Text = "Tổng số bài tập";
            this.lblDescTotal.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblStatsTitle
            // 
            this.lblStatsTitle.AutoSize = true;
            this.lblStatsTitle.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblStatsTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(33)))), ((int)(((byte)(33)))));
            this.lblStatsTitle.Location = new System.Drawing.Point(12, 0);
            this.lblStatsTitle.Name = "lblStatsTitle";
            this.lblStatsTitle.Size = new System.Drawing.Size(120, 32);
            this.lblStatsTitle.TabIndex = 0;
            this.lblStatsTitle.Text = "Thống kê";
            // 
            // pnlRecent
            // 
            this.pnlRecent.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlRecent.BackColor = System.Drawing.Color.White;
            this.pnlRecent.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlRecent.Controls.Add(this.dgvRecent);
            this.pnlRecent.Controls.Add(this.btnViewAll);
            this.pnlRecent.Controls.Add(this.lblRecentTitle);
            this.pnlRecent.Location = new System.Drawing.Point(25, 260);
            this.pnlRecent.Name = "pnlRecent";
            this.pnlRecent.Size = new System.Drawing.Size(1050, 288);
            this.pnlRecent.TabIndex = 2;
            // 
            // dgvRecent
            // 
            this.dgvRecent.AllowUserToAddRows = false;
            this.dgvRecent.AllowUserToDeleteRows = false;
            this.dgvRecent.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvRecent.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvRecent.BackgroundColor = System.Drawing.Color.White;
            this.dgvRecent.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvRecent.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgvRecent.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(248)))), ((int)(((byte)(248)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvRecent.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvRecent.ColumnHeadersHeight = 40;
            this.dgvRecent.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colHash,
            this.colName,
            this.colDeadline,
            this.colStatus,
            this.colScore});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 9F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.Padding = new System.Windows.Forms.Padding(8, 3, 8, 3);
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(240)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvRecent.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvRecent.EnableHeadersVisualStyles = false;
            this.dgvRecent.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.dgvRecent.Location = new System.Drawing.Point(15, 55);
            this.dgvRecent.Name = "dgvRecent";
            this.dgvRecent.ReadOnly = true;
            this.dgvRecent.RowHeadersVisible = false;
            this.dgvRecent.RowHeadersWidth = 51;
            this.dgvRecent.RowTemplate.Height = 40;
            this.dgvRecent.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvRecent.Size = new System.Drawing.Size(1020, 228);
            this.dgvRecent.TabIndex = 2;
            // 
            // colHash
            // 
            this.colHash.FillWeight = 50F;
            this.colHash.HeaderText = "#";
            this.colHash.MinimumWidth = 6;
            this.colHash.Name = "colHash";
            this.colHash.ReadOnly = true;
            // 
            // colName
            // 
            this.colName.HeaderText = "Tên bài tập";
            this.colName.MinimumWidth = 6;
            this.colName.Name = "colName";
            this.colName.ReadOnly = true;
            // 
            // colDeadline
            // 
            this.colDeadline.HeaderText = "Deadline";
            this.colDeadline.MinimumWidth = 6;
            this.colDeadline.Name = "colDeadline";
            this.colDeadline.ReadOnly = true;
            // 
            // colStatus
            // 
            this.colStatus.HeaderText = "Trạng thái";
            this.colStatus.MinimumWidth = 6;
            this.colStatus.Name = "colStatus";
            this.colStatus.ReadOnly = true;
            // 
            // colScore
            // 
            this.colScore.HeaderText = "Điểm";
            this.colScore.MinimumWidth = 6;
            this.colScore.Name = "colScore";
            this.colScore.ReadOnly = true;
            // 
            // btnViewAll
            // 
            this.btnViewAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnViewAll.BackColor = System.Drawing.Color.White;
            this.btnViewAll.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnViewAll.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(120)))), ((int)(((byte)(215)))));
            this.btnViewAll.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnViewAll.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnViewAll.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(120)))), ((int)(((byte)(215)))));
            this.btnViewAll.Location = new System.Drawing.Point(940, 12);
            this.btnViewAll.Name = "btnViewAll";
            this.btnViewAll.Size = new System.Drawing.Size(95, 30);
            this.btnViewAll.TabIndex = 1;
            this.btnViewAll.Text = "Xem tất cả";
            this.btnViewAll.UseVisualStyleBackColor = false;
            // 
            // lblRecentTitle
            // 
            this.lblRecentTitle.AutoSize = true;
            this.lblRecentTitle.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblRecentTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(33)))), ((int)(((byte)(33)))));
            this.lblRecentTitle.Location = new System.Drawing.Point(15, 15);
            this.lblRecentTitle.Name = "lblRecentTitle";
            this.lblRecentTitle.Size = new System.Drawing.Size(191, 32);
            this.lblRecentTitle.TabIndex = 0;
            this.lblRecentTitle.Text = "Bài tập gần đây";
            // 
            // pnlActions
            // 
            this.pnlActions.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlActions.BackColor = System.Drawing.Color.White;
            this.pnlActions.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlActions.Controls.Add(this.btnActionSettings);
            this.pnlActions.Controls.Add(this.btnActionHistory);
            this.pnlActions.Controls.Add(this.btnActionList);
            this.pnlActions.Controls.Add(this.lblActionsTitle);
            this.pnlActions.Location = new System.Drawing.Point(25, 574);
            this.pnlActions.Name = "pnlActions";
            this.pnlActions.Size = new System.Drawing.Size(1050, 110);
            this.pnlActions.TabIndex = 3;
            // 
            // btnActionSettings
            // 
            this.btnActionSettings.BackColor = System.Drawing.Color.White;
            this.btnActionSettings.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnActionSettings.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(120)))), ((int)(((byte)(215)))));
            this.btnActionSettings.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnActionSettings.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnActionSettings.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(120)))), ((int)(((byte)(215)))));
            this.btnActionSettings.Location = new System.Drawing.Point(395, 55);
            this.btnActionSettings.Name = "btnActionSettings";
            this.btnActionSettings.Size = new System.Drawing.Size(180, 35);
            this.btnActionSettings.TabIndex = 3;
            this.btnActionSettings.Text = "Cài đặt";
            this.btnActionSettings.UseVisualStyleBackColor = false;
            // 
            // btnActionHistory
            // 
            this.btnActionHistory.BackColor = System.Drawing.Color.White;
            this.btnActionHistory.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnActionHistory.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(120)))), ((int)(((byte)(215)))));
            this.btnActionHistory.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnActionHistory.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnActionHistory.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(120)))), ((int)(((byte)(215)))));
            this.btnActionHistory.Location = new System.Drawing.Point(205, 55);
            this.btnActionHistory.Name = "btnActionHistory";
            this.btnActionHistory.Size = new System.Drawing.Size(180, 35);
            this.btnActionHistory.TabIndex = 2;
            this.btnActionHistory.Text = "Xem lịch sử nộp bài";
            this.btnActionHistory.UseVisualStyleBackColor = false;
            // 
            // btnActionList
            // 
            this.btnActionList.BackColor = System.Drawing.Color.White;
            this.btnActionList.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnActionList.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(120)))), ((int)(((byte)(215)))));
            this.btnActionList.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnActionList.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnActionList.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(120)))), ((int)(((byte)(215)))));
            this.btnActionList.Location = new System.Drawing.Point(15, 55);
            this.btnActionList.Name = "btnActionList";
            this.btnActionList.Size = new System.Drawing.Size(180, 35);
            this.btnActionList.TabIndex = 1;
            this.btnActionList.Text = "Xem danh sách bài tập";
            this.btnActionList.UseVisualStyleBackColor = false;
            // 
            // lblActionsTitle
            // 
            this.lblActionsTitle.AutoSize = true;
            this.lblActionsTitle.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblActionsTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(33)))), ((int)(((byte)(33)))));
            this.lblActionsTitle.Location = new System.Drawing.Point(15, 15);
            this.lblActionsTitle.Name = "lblActionsTitle";
            this.lblActionsTitle.Size = new System.Drawing.Size(189, 32);
            this.lblActionsTitle.TabIndex = 0;
            this.lblActionsTitle.Text = "Thao tác nhanh";
            // 
            // ucStudentDashboard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.Controls.Add(this.pnlActions);
            this.Controls.Add(this.pnlRecent);
            this.Controls.Add(this.pnlStats);
            this.Controls.Add(this.lblDashboardTitle);
            this.Name = "ucStudentDashboard";
            this.Padding = new System.Windows.Forms.Padding(25);
            this.Size = new System.Drawing.Size(1100, 800);
            this.pnlStats.ResumeLayout(false);
            this.pnlStats.PerformLayout();
            this.tblStats.ResumeLayout(false);
            this.cardAverage.ResumeLayout(false);
            this.cardAverage.PerformLayout();
            this.cardInProgress.ResumeLayout(false);
            this.cardInProgress.PerformLayout();
            this.cardCompleted.ResumeLayout(false);
            this.cardCompleted.PerformLayout();
            this.cardTotal.ResumeLayout(false);
            this.cardTotal.PerformLayout();
            this.pnlRecent.ResumeLayout(false);
            this.pnlRecent.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRecent)).EndInit();
            this.pnlActions.ResumeLayout(false);
            this.pnlActions.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblDashboardTitle;
        private System.Windows.Forms.Panel pnlStats;
        private System.Windows.Forms.Label lblStatsTitle;
        private System.Windows.Forms.TableLayoutPanel tblStats;
        private System.Windows.Forms.Panel cardAverage;
        private System.Windows.Forms.Label lblIconAvg;
        private System.Windows.Forms.Label lblValAvg;
        private System.Windows.Forms.Label lblDescAvg;
        private System.Windows.Forms.Panel cardInProgress;
        private System.Windows.Forms.Label lblIconProg;
        private System.Windows.Forms.Label lblValProg;
        private System.Windows.Forms.Label lblDescProg;
        private System.Windows.Forms.Panel cardCompleted;
        private System.Windows.Forms.Label lblIconComp;
        private System.Windows.Forms.Label lblValComp;
        private System.Windows.Forms.Label lblDescComp;
        private System.Windows.Forms.Panel cardTotal;
        private System.Windows.Forms.Label lblIconTotal;
        private System.Windows.Forms.Label lblValTotal;
        private System.Windows.Forms.Label lblDescTotal;
        private System.Windows.Forms.Panel pnlRecent;
        private System.Windows.Forms.DataGridView dgvRecent;
        private System.Windows.Forms.Button btnViewAll;
        private System.Windows.Forms.Label lblRecentTitle;
        private System.Windows.Forms.DataGridViewTextBoxColumn colHash;
        private System.Windows.Forms.DataGridViewTextBoxColumn colName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDeadline;
        private System.Windows.Forms.DataGridViewTextBoxColumn colStatus;
        private System.Windows.Forms.DataGridViewTextBoxColumn colScore;
        private System.Windows.Forms.Panel pnlActions;
        private System.Windows.Forms.Label lblActionsTitle;
        private System.Windows.Forms.Button btnActionList;
        private System.Windows.Forms.Button btnActionHistory;
        private System.Windows.Forms.Button btnActionSettings;
    }
}