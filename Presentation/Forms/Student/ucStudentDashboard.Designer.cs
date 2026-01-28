namespace CodeForge_Desktop.Presentation.Forms.Student
{
    partial class ucStudentDashboard
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
            this.pnlActionsContainer = new System.Windows.Forms.Panel();
            this.flowLayoutPanelActions = new System.Windows.Forms.FlowLayoutPanel();
            this.btnActionList = new System.Windows.Forms.Button();
            this.btnActionHistory = new System.Windows.Forms.Button();
            this.btnActionSettings = new System.Windows.Forms.Button();
            this.lblActionsTitle = new System.Windows.Forms.Label();
            this.pnlRecentContainer = new System.Windows.Forms.Panel();
            this.dgvRecent = new System.Windows.Forms.DataGridView();
            this.colIndex = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colProblem = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDeadline = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colStatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colScore = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnViewAll = new System.Windows.Forms.Button();
            this.spacer1 = new System.Windows.Forms.Panel();
            this.lblRecentTitle = new System.Windows.Forms.Label();
            this.pnlStatsContainer = new System.Windows.Forms.Panel();
            this.tblStats = new System.Windows.Forms.TableLayoutPanel();
            this.pnlCardTotal = new System.Windows.Forms.Panel();
            this.stripTotal = new System.Windows.Forms.Panel();
            this.lblIconTotal = new System.Windows.Forms.Label();
            this.lblValTotal = new System.Windows.Forms.Label();
            this.lblDescTotal = new System.Windows.Forms.Label();
            this.pnlCardComp = new System.Windows.Forms.Panel();
            this.stripComp = new System.Windows.Forms.Panel();
            this.lblIconComp = new System.Windows.Forms.Label();
            this.lblValComp = new System.Windows.Forms.Label();
            this.lblDescComp = new System.Windows.Forms.Label();
            this.pnlCardProg = new System.Windows.Forms.Panel();
            this.stripProg = new System.Windows.Forms.Panel();
            this.lblIconProg = new System.Windows.Forms.Label();
            this.lblValProg = new System.Windows.Forms.Label();
            this.lblDescProg = new System.Windows.Forms.Label();
            this.pnlCardAvg = new System.Windows.Forms.Panel();
            this.stripAvg = new System.Windows.Forms.Panel();
            this.lblIconAvg = new System.Windows.Forms.Label();
            this.lblValAvg = new System.Windows.Forms.Label();
            this.lblDescAvg = new System.Windows.Forms.Label();
            this.lblGreeting = new System.Windows.Forms.Label();
            this.lblDashboardTitle = new System.Windows.Forms.Label();
            this.pnlMain.SuspendLayout();
            this.pnlActionsContainer.SuspendLayout();
            this.flowLayoutPanelActions.SuspendLayout();
            this.pnlRecentContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRecent)).BeginInit();
            this.spacer1.SuspendLayout();
            this.pnlStatsContainer.SuspendLayout();
            this.tblStats.SuspendLayout();
            this.pnlCardTotal.SuspendLayout();
            this.pnlCardComp.SuspendLayout();
            this.pnlCardProg.SuspendLayout();
            this.pnlCardAvg.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlMain
            // 
            this.pnlMain.AutoScroll = true;
            this.pnlMain.Controls.Add(this.pnlActionsContainer);
            this.pnlMain.Controls.Add(this.pnlRecentContainer);
            this.pnlMain.Controls.Add(this.spacer1);
            this.pnlMain.Controls.Add(this.pnlStatsContainer);
            this.pnlMain.Controls.Add(this.lblGreeting);
            this.pnlMain.Controls.Add(this.lblDashboardTitle);
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Location = new System.Drawing.Point(0, 0);
            this.pnlMain.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Padding = new System.Windows.Forms.Padding(15, 16, 15, 16);
            this.pnlMain.Size = new System.Drawing.Size(900, 650);
            this.pnlMain.TabIndex = 0;
            // 
            // pnlActionsContainer
            // 
            this.pnlActionsContainer.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlActionsContainer.BackColor = System.Drawing.Color.Transparent;
            this.pnlActionsContainer.Controls.Add(this.flowLayoutPanelActions);
            this.pnlActionsContainer.Controls.Add(this.lblActionsTitle);
            this.pnlActionsContainer.Location = new System.Drawing.Point(15, 535);
            this.pnlActionsContainer.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.pnlActionsContainer.Name = "pnlActionsContainer";
            this.pnlActionsContainer.Size = new System.Drawing.Size(870, 89);
            this.pnlActionsContainer.TabIndex = 0;
            // 
            // flowLayoutPanelActions
            // 
            this.flowLayoutPanelActions.Controls.Add(this.btnActionList);
            this.flowLayoutPanelActions.Controls.Add(this.btnActionHistory);
            this.flowLayoutPanelActions.Controls.Add(this.btnActionSettings);
            this.flowLayoutPanelActions.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.flowLayoutPanelActions.Location = new System.Drawing.Point(0, 40);
            this.flowLayoutPanelActions.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.flowLayoutPanelActions.Name = "flowLayoutPanelActions";
            this.flowLayoutPanelActions.Padding = new System.Windows.Forms.Padding(11, 4, 0, 0);
            this.flowLayoutPanelActions.Size = new System.Drawing.Size(870, 49);
            this.flowLayoutPanelActions.TabIndex = 0;
            // 
            // btnActionList
            // 
            this.btnActionList.BackColor = System.Drawing.Color.White;
            this.btnActionList.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnActionList.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(120)))), ((int)(((byte)(215)))));
            this.btnActionList.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnActionList.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.btnActionList.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(120)))), ((int)(((byte)(215)))));
            this.btnActionList.Location = new System.Drawing.Point(11, 4);
            this.btnActionList.Margin = new System.Windows.Forms.Padding(0, 0, 11, 0);
            this.btnActionList.Name = "btnActionList";
            this.btnActionList.Size = new System.Drawing.Size(135, 32);
            this.btnActionList.TabIndex = 0;
            this.btnActionList.Text = "Xem danh sách bài tập";
            this.btnActionList.UseVisualStyleBackColor = false;
            // 
            // btnActionHistory
            // 
            this.btnActionHistory.BackColor = System.Drawing.Color.White;
            this.btnActionHistory.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnActionHistory.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(120)))), ((int)(((byte)(215)))));
            this.btnActionHistory.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnActionHistory.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.btnActionHistory.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(120)))), ((int)(((byte)(215)))));
            this.btnActionHistory.Location = new System.Drawing.Point(157, 4);
            this.btnActionHistory.Margin = new System.Windows.Forms.Padding(0, 0, 11, 0);
            this.btnActionHistory.Name = "btnActionHistory";
            this.btnActionHistory.Size = new System.Drawing.Size(135, 32);
            this.btnActionHistory.TabIndex = 1;
            this.btnActionHistory.Text = "Xem lịch sử nộp bài";
            this.btnActionHistory.UseVisualStyleBackColor = false;
            // 
            // btnActionSettings
            // 
            this.btnActionSettings.BackColor = System.Drawing.Color.White;
            this.btnActionSettings.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnActionSettings.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(120)))), ((int)(((byte)(215)))));
            this.btnActionSettings.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnActionSettings.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.btnActionSettings.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(120)))), ((int)(((byte)(215)))));
            this.btnActionSettings.Location = new System.Drawing.Point(303, 4);
            this.btnActionSettings.Margin = new System.Windows.Forms.Padding(0, 0, 11, 0);
            this.btnActionSettings.Name = "btnActionSettings";
            this.btnActionSettings.Size = new System.Drawing.Size(135, 32);
            this.btnActionSettings.TabIndex = 2;
            this.btnActionSettings.Text = "Cài đặt";
            this.btnActionSettings.UseVisualStyleBackColor = false;
            // 
            // lblActionsTitle
            // 
            this.lblActionsTitle.AutoSize = true;
            this.lblActionsTitle.Font = new System.Drawing.Font("Segoe UI Semibold", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblActionsTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblActionsTitle.Location = new System.Drawing.Point(11, 12);
            this.lblActionsTitle.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblActionsTitle.Name = "lblActionsTitle";
            this.lblActionsTitle.Size = new System.Drawing.Size(115, 20);
            this.lblActionsTitle.TabIndex = 1;
            this.lblActionsTitle.Text = "Thao tác nhanh";
            // 
            // pnlRecentContainer
            // 
            this.pnlRecentContainer.BackColor = System.Drawing.Color.Transparent;
            this.pnlRecentContainer.Controls.Add(this.dgvRecent);
            this.pnlRecentContainer.Controls.Add(this.btnViewAll);
            this.pnlRecentContainer.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlRecentContainer.Location = new System.Drawing.Point(15, 196);
            this.pnlRecentContainer.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.pnlRecentContainer.Name = "pnlRecentContainer";
            this.pnlRecentContainer.Size = new System.Drawing.Size(870, 434);
            this.pnlRecentContainer.TabIndex = 1;
            // 
            // dgvRecent
            // 
            this.dgvRecent.AllowUserToAddRows = false;
            this.dgvRecent.AllowUserToDeleteRows = false;
            this.dgvRecent.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvRecent.BackgroundColor = System.Drawing.Color.White;
            this.dgvRecent.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvRecent.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgvRecent.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(248)))), ((int)(((byte)(248)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(73)))), ((int)(((byte)(80)))), ((int)(((byte)(87)))));
            dataGridViewCellStyle1.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.dgvRecent.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvRecent.ColumnHeadersHeight = 40;
            this.dgvRecent.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colIndex,
            this.colProblem,
            this.colDeadline,
            this.colStatus,
            this.colScore});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(37)))), ((int)(((byte)(41)))));
            dataGridViewCellStyle2.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(245)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvRecent.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvRecent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvRecent.EnableHeadersVisualStyles = false;
            this.dgvRecent.Location = new System.Drawing.Point(0, 0);
            this.dgvRecent.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.dgvRecent.Name = "dgvRecent";
            this.dgvRecent.ReadOnly = true;
            this.dgvRecent.RowHeadersVisible = false;
            this.dgvRecent.RowHeadersWidth = 51;
            this.dgvRecent.RowTemplate.Height = 45;
            this.dgvRecent.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvRecent.Size = new System.Drawing.Size(870, 434);
            this.dgvRecent.TabIndex = 0;
            // 
            // colIndex
            // 
            this.colIndex.HeaderText = "#";
            this.colIndex.MinimumWidth = 6;
            this.colIndex.Name = "colIndex";
            this.colIndex.ReadOnly = true;
            // 
            // colProblem
            // 
            this.colProblem.HeaderText = "Tên bài tập";
            this.colProblem.MinimumWidth = 6;
            this.colProblem.Name = "colProblem";
            this.colProblem.ReadOnly = true;
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
            this.btnViewAll.Location = new System.Drawing.Point(1504, 8);
            this.btnViewAll.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnViewAll.Name = "btnViewAll";
            this.btnViewAll.Size = new System.Drawing.Size(71, 24);
            this.btnViewAll.TabIndex = 1;
            this.btnViewAll.Text = "Xem tất cả";
            this.btnViewAll.UseVisualStyleBackColor = false;
            // 
            // spacer1
            // 
            this.spacer1.BackColor = System.Drawing.Color.Transparent;
            this.spacer1.Controls.Add(this.lblRecentTitle);
            this.spacer1.Dock = System.Windows.Forms.DockStyle.Top;
            this.spacer1.Location = new System.Drawing.Point(15, 172);
            this.spacer1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.spacer1.Name = "spacer1";
            this.spacer1.Size = new System.Drawing.Size(870, 24);
            this.spacer1.TabIndex = 3;
            // 
            // lblRecentTitle
            // 
            this.lblRecentTitle.AutoSize = true;
            this.lblRecentTitle.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblRecentTitle.Font = new System.Drawing.Font("Segoe UI Semibold", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRecentTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblRecentTitle.Location = new System.Drawing.Point(0, 0);
            this.lblRecentTitle.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblRecentTitle.Name = "lblRecentTitle";
            this.lblRecentTitle.Size = new System.Drawing.Size(115, 20);
            this.lblRecentTitle.TabIndex = 3;
            this.lblRecentTitle.Text = "Bài tập gần đây";
            // 
            // pnlStatsContainer
            // 
            this.pnlStatsContainer.BackColor = System.Drawing.Color.Transparent;
            this.pnlStatsContainer.Controls.Add(this.tblStats);
            this.pnlStatsContainer.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlStatsContainer.Location = new System.Drawing.Point(15, 62);
            this.pnlStatsContainer.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.pnlStatsContainer.Name = "pnlStatsContainer";
            this.pnlStatsContainer.Size = new System.Drawing.Size(870, 110);
            this.pnlStatsContainer.TabIndex = 2;
            // 
            // tblStats
            // 
            this.tblStats.ColumnCount = 4;
            this.tblStats.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tblStats.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tblStats.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tblStats.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tblStats.Controls.Add(this.pnlCardTotal, 0, 0);
            this.tblStats.Controls.Add(this.pnlCardComp, 1, 0);
            this.tblStats.Controls.Add(this.pnlCardProg, 2, 0);
            this.tblStats.Controls.Add(this.pnlCardAvg, 3, 0);
            this.tblStats.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tblStats.Location = new System.Drawing.Point(0, -20);
            this.tblStats.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tblStats.Name = "tblStats";
            this.tblStats.Padding = new System.Windows.Forms.Padding(11, 0, 11, 12);
            this.tblStats.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 118F));
            this.tblStats.Size = new System.Drawing.Size(870, 130);
            this.tblStats.TabIndex = 0;
            // 
            // pnlCardTotal
            // 
            this.pnlCardTotal.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(245)))), ((int)(((byte)(255)))));
            this.pnlCardTotal.Controls.Add(this.stripTotal);
            this.pnlCardTotal.Controls.Add(this.lblIconTotal);
            this.pnlCardTotal.Controls.Add(this.lblValTotal);
            this.pnlCardTotal.Controls.Add(this.lblDescTotal);
            this.pnlCardTotal.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlCardTotal.Location = new System.Drawing.Point(11, 0);
            this.pnlCardTotal.Margin = new System.Windows.Forms.Padding(0, 0, 11, 0);
            this.pnlCardTotal.Name = "pnlCardTotal";
            this.pnlCardTotal.Size = new System.Drawing.Size(201, 118);
            this.pnlCardTotal.TabIndex = 0;
            // 
            // stripTotal
            // 
            this.stripTotal.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(120)))), ((int)(((byte)(215)))));
            this.stripTotal.Dock = System.Windows.Forms.DockStyle.Left;
            this.stripTotal.Location = new System.Drawing.Point(0, 0);
            this.stripTotal.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.stripTotal.Name = "stripTotal";
            this.stripTotal.Size = new System.Drawing.Size(3, 94);
            this.stripTotal.TabIndex = 0;
            // 
            // lblIconTotal
            // 
            this.lblIconTotal.AutoSize = true;
            this.lblIconTotal.Font = new System.Drawing.Font("Segoe UI Emoji", 20F);
            this.lblIconTotal.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(120)))), ((int)(((byte)(215)))));
            this.lblIconTotal.Location = new System.Drawing.Point(4, 39);
            this.lblIconTotal.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblIconTotal.Name = "lblIconTotal";
            this.lblIconTotal.Size = new System.Drawing.Size(52, 36);
            this.lblIconTotal.TabIndex = 1;
            this.lblIconTotal.Text = "📄";
            // 
            // lblValTotal
            // 
            this.lblValTotal.AutoSize = true;
            this.lblValTotal.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Bold);
            this.lblValTotal.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(120)))), ((int)(((byte)(215)))));
            this.lblValTotal.Location = new System.Drawing.Point(52, 37);
            this.lblValTotal.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblValTotal.Name = "lblValTotal";
            this.lblValTotal.Size = new System.Drawing.Size(38, 45);
            this.lblValTotal.TabIndex = 2;
            this.lblValTotal.Text = "0";
            // 
            // lblDescTotal
            // 
            this.lblDescTotal.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblDescTotal.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblDescTotal.ForeColor = System.Drawing.Color.Gray;
            this.lblDescTotal.Location = new System.Drawing.Point(0, 94);
            this.lblDescTotal.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblDescTotal.Name = "lblDescTotal";
            this.lblDescTotal.Padding = new System.Windows.Forms.Padding(11, 0, 0, 8);
            this.lblDescTotal.Size = new System.Drawing.Size(201, 24);
            this.lblDescTotal.TabIndex = 3;
            this.lblDescTotal.Text = "Tổng số bài tập";
            // 
            // pnlCardComp
            // 
            this.pnlCardComp.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(245)))), ((int)(((byte)(233)))));
            this.pnlCardComp.Controls.Add(this.stripComp);
            this.pnlCardComp.Controls.Add(this.lblIconComp);
            this.pnlCardComp.Controls.Add(this.lblValComp);
            this.pnlCardComp.Controls.Add(this.lblDescComp);
            this.pnlCardComp.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlCardComp.Location = new System.Drawing.Point(223, 0);
            this.pnlCardComp.Margin = new System.Windows.Forms.Padding(0, 0, 11, 0);
            this.pnlCardComp.Name = "pnlCardComp";
            this.pnlCardComp.Size = new System.Drawing.Size(201, 118);
            this.pnlCardComp.TabIndex = 1;
            // 
            // stripComp
            // 
            this.stripComp.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(76)))), ((int)(((byte)(175)))), ((int)(((byte)(80)))));
            this.stripComp.Dock = System.Windows.Forms.DockStyle.Left;
            this.stripComp.Location = new System.Drawing.Point(0, 0);
            this.stripComp.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.stripComp.Name = "stripComp";
            this.stripComp.Size = new System.Drawing.Size(3, 94);
            this.stripComp.TabIndex = 0;
            // 
            // lblIconComp
            // 
            this.lblIconComp.AutoSize = true;
            this.lblIconComp.Font = new System.Drawing.Font("Segoe UI Emoji", 20F);
            this.lblIconComp.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(76)))), ((int)(((byte)(175)))), ((int)(((byte)(80)))));
            this.lblIconComp.Location = new System.Drawing.Point(4, 39);
            this.lblIconComp.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblIconComp.Name = "lblIconComp";
            this.lblIconComp.Size = new System.Drawing.Size(35, 36);
            this.lblIconComp.TabIndex = 1;
            this.lblIconComp.Text = "✓";
            // 
            // lblValComp
            // 
            this.lblValComp.AutoSize = true;
            this.lblValComp.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Bold);
            this.lblValComp.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(76)))), ((int)(((byte)(175)))), ((int)(((byte)(80)))));
            this.lblValComp.Location = new System.Drawing.Point(52, 37);
            this.lblValComp.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblValComp.Name = "lblValComp";
            this.lblValComp.Size = new System.Drawing.Size(38, 45);
            this.lblValComp.TabIndex = 2;
            this.lblValComp.Text = "0";
            // 
            // lblDescComp
            // 
            this.lblDescComp.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblDescComp.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblDescComp.ForeColor = System.Drawing.Color.Gray;
            this.lblDescComp.Location = new System.Drawing.Point(0, 94);
            this.lblDescComp.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblDescComp.Name = "lblDescComp";
            this.lblDescComp.Padding = new System.Windows.Forms.Padding(11, 0, 0, 8);
            this.lblDescComp.Size = new System.Drawing.Size(201, 24);
            this.lblDescComp.TabIndex = 3;
            this.lblDescComp.Text = "Đã hoàn thành";
            // 
            // pnlCardProg
            // 
            this.pnlCardProg.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(250)))), ((int)(((byte)(235)))));
            this.pnlCardProg.Controls.Add(this.stripProg);
            this.pnlCardProg.Controls.Add(this.lblIconProg);
            this.pnlCardProg.Controls.Add(this.lblValProg);
            this.pnlCardProg.Controls.Add(this.lblDescProg);
            this.pnlCardProg.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlCardProg.Location = new System.Drawing.Point(435, 0);
            this.pnlCardProg.Margin = new System.Windows.Forms.Padding(0, 0, 11, 0);
            this.pnlCardProg.Name = "pnlCardProg";
            this.pnlCardProg.Size = new System.Drawing.Size(201, 118);
            this.pnlCardProg.TabIndex = 2;
            // 
            // stripProg
            // 
            this.stripProg.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(152)))), ((int)(((byte)(0)))));
            this.stripProg.Dock = System.Windows.Forms.DockStyle.Left;
            this.stripProg.Location = new System.Drawing.Point(0, 0);
            this.stripProg.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.stripProg.Name = "stripProg";
            this.stripProg.Size = new System.Drawing.Size(3, 94);
            this.stripProg.TabIndex = 0;
            // 
            // lblIconProg
            // 
            this.lblIconProg.AutoSize = true;
            this.lblIconProg.Font = new System.Drawing.Font("Segoe UI Emoji", 20F);
            this.lblIconProg.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(152)))), ((int)(((byte)(0)))));
            this.lblIconProg.Location = new System.Drawing.Point(4, 39);
            this.lblIconProg.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblIconProg.Name = "lblIconProg";
            this.lblIconProg.Size = new System.Drawing.Size(52, 36);
            this.lblIconProg.TabIndex = 1;
            this.lblIconProg.Text = "🕒";
            // 
            // lblValProg
            // 
            this.lblValProg.AutoSize = true;
            this.lblValProg.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Bold);
            this.lblValProg.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(152)))), ((int)(((byte)(0)))));
            this.lblValProg.Location = new System.Drawing.Point(52, 36);
            this.lblValProg.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblValProg.Name = "lblValProg";
            this.lblValProg.Size = new System.Drawing.Size(38, 45);
            this.lblValProg.TabIndex = 2;
            this.lblValProg.Text = "0";
            // 
            // lblDescProg
            // 
            this.lblDescProg.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblDescProg.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblDescProg.ForeColor = System.Drawing.Color.Gray;
            this.lblDescProg.Location = new System.Drawing.Point(0, 94);
            this.lblDescProg.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblDescProg.Name = "lblDescProg";
            this.lblDescProg.Padding = new System.Windows.Forms.Padding(11, 0, 0, 8);
            this.lblDescProg.Size = new System.Drawing.Size(201, 24);
            this.lblDescProg.TabIndex = 3;
            this.lblDescProg.Text = "Đang làm";
            // 
            // pnlCardAvg
            // 
            this.pnlCardAvg.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(235)))), ((int)(((byte)(255)))));
            this.pnlCardAvg.Controls.Add(this.stripAvg);
            this.pnlCardAvg.Controls.Add(this.lblIconAvg);
            this.pnlCardAvg.Controls.Add(this.lblValAvg);
            this.pnlCardAvg.Controls.Add(this.lblDescAvg);
            this.pnlCardAvg.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlCardAvg.Location = new System.Drawing.Point(647, 0);
            this.pnlCardAvg.Margin = new System.Windows.Forms.Padding(0);
            this.pnlCardAvg.Name = "pnlCardAvg";
            this.pnlCardAvg.Size = new System.Drawing.Size(212, 118);
            this.pnlCardAvg.TabIndex = 3;
            // 
            // stripAvg
            // 
            this.stripAvg.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(156)))), ((int)(((byte)(39)))), ((int)(((byte)(176)))));
            this.stripAvg.Dock = System.Windows.Forms.DockStyle.Left;
            this.stripAvg.Location = new System.Drawing.Point(0, 0);
            this.stripAvg.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.stripAvg.Name = "stripAvg";
            this.stripAvg.Size = new System.Drawing.Size(3, 94);
            this.stripAvg.TabIndex = 0;
            // 
            // lblIconAvg
            // 
            this.lblIconAvg.AutoSize = true;
            this.lblIconAvg.Font = new System.Drawing.Font("Segoe UI Emoji", 20F);
            this.lblIconAvg.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(156)))), ((int)(((byte)(39)))), ((int)(((byte)(176)))));
            this.lblIconAvg.Location = new System.Drawing.Point(4, 39);
            this.lblIconAvg.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblIconAvg.Name = "lblIconAvg";
            this.lblIconAvg.Size = new System.Drawing.Size(52, 36);
            this.lblIconAvg.TabIndex = 1;
            this.lblIconAvg.Text = "📈";
            // 
            // lblValAvg
            // 
            this.lblValAvg.AutoSize = true;
            this.lblValAvg.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Bold);
            this.lblValAvg.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(156)))), ((int)(((byte)(39)))), ((int)(((byte)(176)))));
            this.lblValAvg.Location = new System.Drawing.Point(52, 37);
            this.lblValAvg.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblValAvg.Name = "lblValAvg";
            this.lblValAvg.Size = new System.Drawing.Size(66, 45);
            this.lblValAvg.TabIndex = 2;
            this.lblValAvg.Text = "0%";
            // 
            // lblDescAvg
            // 
            this.lblDescAvg.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblDescAvg.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblDescAvg.ForeColor = System.Drawing.Color.Gray;
            this.lblDescAvg.Location = new System.Drawing.Point(0, 94);
            this.lblDescAvg.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblDescAvg.Name = "lblDescAvg";
            this.lblDescAvg.Padding = new System.Windows.Forms.Padding(11, 0, 0, 8);
            this.lblDescAvg.Size = new System.Drawing.Size(212, 24);
            this.lblDescAvg.TabIndex = 3;
            this.lblDescAvg.Text = "Điểm trung bình";
            // 
            // lblGreeting
            // 
            this.lblGreeting.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblGreeting.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblGreeting.ForeColor = System.Drawing.Color.Gray;
            this.lblGreeting.Location = new System.Drawing.Point(652, 28);
            this.lblGreeting.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblGreeting.Name = "lblGreeting";
            this.lblGreeting.Size = new System.Drawing.Size(219, 19);
            this.lblGreeting.TabIndex = 1;
            this.lblGreeting.Text = "Xin chào, User";
            this.lblGreeting.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblDashboardTitle
            // 
            this.lblDashboardTitle.AutoSize = true;
            this.lblDashboardTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblDashboardTitle.Font = new System.Drawing.Font("Segoe UI", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDashboardTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(33)))), ((int)(((byte)(33)))));
            this.lblDashboardTitle.Location = new System.Drawing.Point(15, 16);
            this.lblDashboardTitle.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblDashboardTitle.Name = "lblDashboardTitle";
            this.lblDashboardTitle.Padding = new System.Windows.Forms.Padding(0, 0, 0, 16);
            this.lblDashboardTitle.Size = new System.Drawing.Size(109, 46);
            this.lblDashboardTitle.TabIndex = 0;
            this.lblDashboardTitle.Text = "Thống kê";
            // 
            // ucStudentDashboard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.Controls.Add(this.pnlMain);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "ucStudentDashboard";
            this.Size = new System.Drawing.Size(900, 650);
            this.pnlMain.ResumeLayout(false);
            this.pnlMain.PerformLayout();
            this.pnlActionsContainer.ResumeLayout(false);
            this.pnlActionsContainer.PerformLayout();
            this.flowLayoutPanelActions.ResumeLayout(false);
            this.pnlRecentContainer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvRecent)).EndInit();
            this.spacer1.ResumeLayout(false);
            this.spacer1.PerformLayout();
            this.pnlStatsContainer.ResumeLayout(false);
            this.tblStats.ResumeLayout(false);
            this.pnlCardTotal.ResumeLayout(false);
            this.pnlCardTotal.PerformLayout();
            this.pnlCardComp.ResumeLayout(false);
            this.pnlCardComp.PerformLayout();
            this.pnlCardProg.ResumeLayout(false);
            this.pnlCardProg.PerformLayout();
            this.pnlCardAvg.ResumeLayout(false);
            this.pnlCardAvg.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        // Controls
        private System.Windows.Forms.Panel pnlMain;
        private System.Windows.Forms.Label lblDashboardTitle;
        private System.Windows.Forms.Label lblGreeting;

        // Recent
        private System.Windows.Forms.Panel pnlRecentContainer;
        private System.Windows.Forms.DataGridView dgvRecent;
        private System.Windows.Forms.Button btnViewAll;
        private System.Windows.Forms.DataGridViewTextBoxColumn colIndex;
        private System.Windows.Forms.DataGridViewTextBoxColumn colProblem;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDeadline;
        private System.Windows.Forms.DataGridViewTextBoxColumn colStatus;
        private System.Windows.Forms.DataGridViewTextBoxColumn colScore;

        // Actions
        private System.Windows.Forms.Panel pnlActionsContainer;
        private System.Windows.Forms.Label lblActionsTitle;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelActions;
        private System.Windows.Forms.Button btnActionList;
        private System.Windows.Forms.Button btnActionHistory;
        private System.Windows.Forms.Button btnActionSettings;
        private System.Windows.Forms.Panel spacer1;
        private System.Windows.Forms.Panel pnlStatsContainer;
        private System.Windows.Forms.TableLayoutPanel tblStats;
        private System.Windows.Forms.Panel pnlCardTotal;
        private System.Windows.Forms.Panel stripTotal;
        private System.Windows.Forms.Label lblIconTotal;
        private System.Windows.Forms.Label lblValTotal;
        private System.Windows.Forms.Label lblDescTotal;
        private System.Windows.Forms.Panel pnlCardComp;
        private System.Windows.Forms.Panel stripComp;
        private System.Windows.Forms.Label lblIconComp;
        private System.Windows.Forms.Label lblValComp;
        private System.Windows.Forms.Label lblDescComp;
        private System.Windows.Forms.Panel pnlCardProg;
        private System.Windows.Forms.Panel stripProg;
        private System.Windows.Forms.Label lblIconProg;
        private System.Windows.Forms.Label lblValProg;
        private System.Windows.Forms.Label lblDescProg;
        private System.Windows.Forms.Panel pnlCardAvg;
        private System.Windows.Forms.Panel stripAvg;
        private System.Windows.Forms.Label lblIconAvg;
        private System.Windows.Forms.Label lblValAvg;
        private System.Windows.Forms.Label lblDescAvg;
        private System.Windows.Forms.Label lblRecentTitle;
    }
}