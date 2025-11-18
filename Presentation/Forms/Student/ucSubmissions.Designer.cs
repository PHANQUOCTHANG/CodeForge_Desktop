// Đảm bảo namespace này khớp với project của bạn
namespace CodeForge_Desktop.Presentation.Forms.Student
{
    partial class ucSubmissions
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
            this.lblSummary = new System.Windows.Forms.Label();
            this.dgvSubmissions = new System.Windows.Forms.DataGridView();
            this.colHash = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colProblemName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTimestamp = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colStatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colScore = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTestCases = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colView = new System.Windows.Forms.DataGridViewButtonColumn();
            this.pnlStatsContainer = new System.Windows.Forms.Panel();
            this.tblStatsLayout = new System.Windows.Forms.TableLayoutPanel();
            this.pnlStatError = new System.Windows.Forms.Panel();
            this.lblStatErrorValue = new System.Windows.Forms.Label();
            this.lblStatErrorTitle = new System.Windows.Forms.Label();
            this.pnlStatWA = new System.Windows.Forms.Panel();
            this.lblStatWAValue = new System.Windows.Forms.Label();
            this.lblStatWATitle = new System.Windows.Forms.Label();
            this.pnlStatAccepted = new System.Windows.Forms.Panel();
            this.lblStatAcceptedValue = new System.Windows.Forms.Label();
            this.lblStatAcceptedTitle = new System.Windows.Forms.Label();
            this.pnlStatTotal = new System.Windows.Forms.Panel();
            this.lblStatTotalValue = new System.Windows.Forms.Label();
            this.lblStatTotalTitle = new System.Windows.Forms.Label();
            this.lblStatsTitle = new System.Windows.Forms.Label();
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.btnBack = new System.Windows.Forms.Button();
            this.pnlFilters = new System.Windows.Forms.Panel();
            this.btnApplyFilter = new System.Windows.Forms.Button();
            this.cmbStatus = new System.Windows.Forms.ComboBox();
            this.cmbProblems = new System.Windows.Forms.ComboBox();
            this.lblFilter = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSubmissions)).BeginInit();
            this.pnlStatsContainer.SuspendLayout();
            this.tblStatsLayout.SuspendLayout();
            this.pnlStatError.SuspendLayout();
            this.pnlStatWA.SuspendLayout();
            this.pnlStatAccepted.SuspendLayout();
            this.pnlStatTotal.SuspendLayout();
            this.pnlHeader.SuspendLayout();
            this.pnlFilters.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblSummary
            // 
            this.lblSummary.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblSummary.BackColor = System.Drawing.SystemColors.Control;
            this.lblSummary.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblSummary.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblSummary.Location = new System.Drawing.Point(0, 478);
            this.lblSummary.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblSummary.Name = "lblSummary";
            this.lblSummary.Padding = new System.Windows.Forms.Padding(13, 0, 0, 0);
            this.lblSummary.Size = new System.Drawing.Size(1312, 30);
            this.lblSummary.TabIndex = 2;
            this.lblSummary.Text = "Tổng số lần nộp: 5 | Accepted: 2";
            this.lblSummary.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // dgvSubmissions
            // 
            this.dgvSubmissions.AllowUserToAddRows = false;
            this.dgvSubmissions.AllowUserToDeleteRows = false;
            this.dgvSubmissions.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvSubmissions.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvSubmissions.BackgroundColor = System.Drawing.Color.White;
            this.dgvSubmissions.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.dgvSubmissions.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSubmissions.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colHash,
            this.colProblemName,
            this.colTimestamp,
            this.colStatus,
            this.colScore,
            this.colTestCases,
            this.colView});
            this.dgvSubmissions.Location = new System.Drawing.Point(0, 126);
            this.dgvSubmissions.Margin = new System.Windows.Forms.Padding(4, 20, 4, 4);
            this.dgvSubmissions.Name = "dgvSubmissions";
            this.dgvSubmissions.ReadOnly = true;
            this.dgvSubmissions.RowHeadersVisible = false;
            this.dgvSubmissions.RowHeadersWidth = 51;
            this.dgvSubmissions.RowTemplate.Height = 35;
            this.dgvSubmissions.Size = new System.Drawing.Size(1312, 352);
            this.dgvSubmissions.TabIndex = 3;
            // 
            // colHash
            // 
            this.colHash.FillWeight = 5F;
            this.colHash.HeaderText = "#";
            this.colHash.MinimumWidth = 6;
            this.colHash.Name = "colHash";
            this.colHash.ReadOnly = true;
            // 
            // colProblemName
            // 
            this.colProblemName.FillWeight = 20F;
            this.colProblemName.HeaderText = "Bài tập";
            this.colProblemName.MinimumWidth = 6;
            this.colProblemName.Name = "colProblemName";
            this.colProblemName.ReadOnly = true;
            // 
            // colTimestamp
            // 
            this.colTimestamp.FillWeight = 20F;
            this.colTimestamp.HeaderText = "Thời gian nộp";
            this.colTimestamp.MinimumWidth = 6;
            this.colTimestamp.Name = "colTimestamp";
            this.colTimestamp.ReadOnly = true;
            // 
            // colStatus
            // 
            this.colStatus.FillWeight = 15F;
            this.colStatus.HeaderText = "Trạng thái";
            this.colStatus.MinimumWidth = 6;
            this.colStatus.Name = "colStatus";
            this.colStatus.ReadOnly = true;
            // 
            // colScore
            // 
            this.colScore.FillWeight = 10F;
            this.colScore.HeaderText = "Điểm";
            this.colScore.MinimumWidth = 6;
            this.colScore.Name = "colScore";
            this.colScore.ReadOnly = true;
            // 
            // colTestCases
            // 
            this.colTestCases.FillWeight = 10F;
            this.colTestCases.HeaderText = "Test";
            this.colTestCases.MinimumWidth = 6;
            this.colTestCases.Name = "colTestCases";
            this.colTestCases.ReadOnly = true;
            // 
            // colView
            // 
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.NullValue = "Xem";
            this.colView.DefaultCellStyle = dataGridViewCellStyle1;
            this.colView.FillWeight = 10F;
            this.colView.HeaderText = "Thao tác";
            this.colView.MinimumWidth = 6;
            this.colView.Name = "colView";
            this.colView.ReadOnly = true;
            this.colView.Text = "Xem";
            // 
            // pnlStatsContainer
            // 
            this.pnlStatsContainer.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlStatsContainer.BackColor = System.Drawing.Color.White;
            this.pnlStatsContainer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlStatsContainer.Controls.Add(this.tblStatsLayout);
            this.pnlStatsContainer.Controls.Add(this.lblStatsTitle);
            this.pnlStatsContainer.Location = new System.Drawing.Point(0, 530);
            this.pnlStatsContainer.Margin = new System.Windows.Forms.Padding(13, 12, 13, 12);
            this.pnlStatsContainer.Name = "pnlStatsContainer";
            this.pnlStatsContainer.Padding = new System.Windows.Forms.Padding(13, 12, 13, 12);
            this.pnlStatsContainer.Size = new System.Drawing.Size(1312, 160);
            this.pnlStatsContainer.TabIndex = 4;
            // 
            // tblStatsLayout
            // 
            this.tblStatsLayout.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tblStatsLayout.ColumnCount = 4;
            this.tblStatsLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tblStatsLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tblStatsLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tblStatsLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tblStatsLayout.Controls.Add(this.pnlStatError, 3, 0);
            this.tblStatsLayout.Controls.Add(this.pnlStatWA, 2, 0);
            this.tblStatsLayout.Controls.Add(this.pnlStatAccepted, 1, 0);
            this.tblStatsLayout.Controls.Add(this.pnlStatTotal, 0, 0);
            this.tblStatsLayout.Location = new System.Drawing.Point(13, 45);
            this.tblStatsLayout.Margin = new System.Windows.Forms.Padding(4);
            this.tblStatsLayout.Name = "tblStatsLayout";
            this.tblStatsLayout.RowCount = 1;
            this.tblStatsLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblStatsLayout.Size = new System.Drawing.Size(1284, 101);
            this.tblStatsLayout.TabIndex = 1;
            // 
            // pnlStatError
            // 
            this.pnlStatError.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlStatError.BackColor = System.Drawing.Color.WhiteSmoke;
            this.pnlStatError.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlStatError.Controls.Add(this.lblStatErrorValue);
            this.pnlStatError.Controls.Add(this.lblStatErrorTitle);
            this.pnlStatError.Location = new System.Drawing.Point(967, 4);
            this.pnlStatError.Margin = new System.Windows.Forms.Padding(4);
            this.pnlStatError.Name = "pnlStatError";
            this.pnlStatError.Padding = new System.Windows.Forms.Padding(13, 12, 13, 12);
            this.pnlStatError.Size = new System.Drawing.Size(313, 93);
            this.pnlStatError.TabIndex = 3;
            // 
            // lblStatErrorValue
            // 
            this.lblStatErrorValue.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStatErrorValue.ForeColor = System.Drawing.Color.Red;
            this.lblStatErrorValue.Location = new System.Drawing.Point(11, 36);
            this.lblStatErrorValue.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblStatErrorValue.Name = "lblStatErrorValue";
            this.lblStatErrorValue.Size = new System.Drawing.Size(289, 43);
            this.lblStatErrorValue.TabIndex = 1;
            this.lblStatErrorValue.Text = "2";
            this.lblStatErrorValue.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblStatErrorTitle
            // 
            this.lblStatErrorTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblStatErrorTitle.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStatErrorTitle.Location = new System.Drawing.Point(13, 12);
            this.lblStatErrorTitle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblStatErrorTitle.Name = "lblStatErrorTitle";
            this.lblStatErrorTitle.Size = new System.Drawing.Size(285, 28);
            this.lblStatErrorTitle.TabIndex = 0;
            this.lblStatErrorTitle.Text = "Errors";
            this.lblStatErrorTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pnlStatWA
            // 
            this.pnlStatWA.BackColor = System.Drawing.Color.WhiteSmoke;
            this.pnlStatWA.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlStatWA.Controls.Add(this.lblStatWAValue);
            this.pnlStatWA.Controls.Add(this.lblStatWATitle);
            this.pnlStatWA.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlStatWA.Location = new System.Drawing.Point(646, 4);
            this.pnlStatWA.Margin = new System.Windows.Forms.Padding(4);
            this.pnlStatWA.Name = "pnlStatWA";
            this.pnlStatWA.Padding = new System.Windows.Forms.Padding(13, 12, 13, 12);
            this.pnlStatWA.Size = new System.Drawing.Size(313, 93);
            this.pnlStatWA.TabIndex = 2;
            // 
            // lblStatWAValue
            // 
            this.lblStatWAValue.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStatWAValue.ForeColor = System.Drawing.Color.Orange;
            this.lblStatWAValue.Location = new System.Drawing.Point(11, 40);
            this.lblStatWAValue.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblStatWAValue.Name = "lblStatWAValue";
            this.lblStatWAValue.Size = new System.Drawing.Size(287, 41);
            this.lblStatWAValue.TabIndex = 1;
            this.lblStatWAValue.Text = "1";
            this.lblStatWAValue.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblStatWATitle
            // 
            this.lblStatWATitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblStatWATitle.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStatWATitle.Location = new System.Drawing.Point(13, 12);
            this.lblStatWATitle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblStatWATitle.Name = "lblStatWATitle";
            this.lblStatWATitle.Size = new System.Drawing.Size(285, 28);
            this.lblStatWATitle.TabIndex = 0;
            this.lblStatWATitle.Text = "Wrong Answer";
            this.lblStatWATitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pnlStatAccepted
            // 
            this.pnlStatAccepted.BackColor = System.Drawing.Color.WhiteSmoke;
            this.pnlStatAccepted.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlStatAccepted.Controls.Add(this.lblStatAcceptedValue);
            this.pnlStatAccepted.Controls.Add(this.lblStatAcceptedTitle);
            this.pnlStatAccepted.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlStatAccepted.Location = new System.Drawing.Point(325, 4);
            this.pnlStatAccepted.Margin = new System.Windows.Forms.Padding(4);
            this.pnlStatAccepted.Name = "pnlStatAccepted";
            this.pnlStatAccepted.Padding = new System.Windows.Forms.Padding(13, 12, 13, 12);
            this.pnlStatAccepted.Size = new System.Drawing.Size(313, 93);
            this.pnlStatAccepted.TabIndex = 1;
            // 
            // lblStatAcceptedValue
            // 
            this.lblStatAcceptedValue.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStatAcceptedValue.ForeColor = System.Drawing.Color.Green;
            this.lblStatAcceptedValue.Location = new System.Drawing.Point(11, 36);
            this.lblStatAcceptedValue.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblStatAcceptedValue.Name = "lblStatAcceptedValue";
            this.lblStatAcceptedValue.Size = new System.Drawing.Size(287, 43);
            this.lblStatAcceptedValue.TabIndex = 1;
            this.lblStatAcceptedValue.Text = "2";
            this.lblStatAcceptedValue.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblStatAcceptedTitle
            // 
            this.lblStatAcceptedTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblStatAcceptedTitle.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStatAcceptedTitle.Location = new System.Drawing.Point(13, 12);
            this.lblStatAcceptedTitle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblStatAcceptedTitle.Name = "lblStatAcceptedTitle";
            this.lblStatAcceptedTitle.Size = new System.Drawing.Size(285, 28);
            this.lblStatAcceptedTitle.TabIndex = 0;
            this.lblStatAcceptedTitle.Text = "Accepted";
            this.lblStatAcceptedTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pnlStatTotal
            // 
            this.pnlStatTotal.BackColor = System.Drawing.Color.WhiteSmoke;
            this.pnlStatTotal.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlStatTotal.Controls.Add(this.lblStatTotalValue);
            this.pnlStatTotal.Controls.Add(this.lblStatTotalTitle);
            this.pnlStatTotal.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlStatTotal.Location = new System.Drawing.Point(4, 4);
            this.pnlStatTotal.Margin = new System.Windows.Forms.Padding(4);
            this.pnlStatTotal.Name = "pnlStatTotal";
            this.pnlStatTotal.Padding = new System.Windows.Forms.Padding(13, 12, 13, 12);
            this.pnlStatTotal.Size = new System.Drawing.Size(313, 93);
            this.pnlStatTotal.TabIndex = 0;
            // 
            // lblStatTotalValue
            // 
            this.lblStatTotalValue.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStatTotalValue.ForeColor = System.Drawing.Color.RoyalBlue;
            this.lblStatTotalValue.Location = new System.Drawing.Point(17, 36);
            this.lblStatTotalValue.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblStatTotalValue.Name = "lblStatTotalValue";
            this.lblStatTotalValue.Size = new System.Drawing.Size(287, 45);
            this.lblStatTotalValue.TabIndex = 1;
            this.lblStatTotalValue.Text = "5";
            this.lblStatTotalValue.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblStatTotalTitle
            // 
            this.lblStatTotalTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblStatTotalTitle.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStatTotalTitle.Location = new System.Drawing.Point(13, 12);
            this.lblStatTotalTitle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblStatTotalTitle.Name = "lblStatTotalTitle";
            this.lblStatTotalTitle.Size = new System.Drawing.Size(285, 28);
            this.lblStatTotalTitle.TabIndex = 0;
            this.lblStatTotalTitle.Text = "Tổng số lần nộp";
            this.lblStatTotalTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblStatsTitle
            // 
            this.lblStatsTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblStatsTitle.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblStatsTitle.Location = new System.Drawing.Point(13, 12);
            this.lblStatsTitle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblStatsTitle.Name = "lblStatsTitle";
            this.lblStatsTitle.Size = new System.Drawing.Size(1284, 33);
            this.lblStatsTitle.TabIndex = 0;
            this.lblStatsTitle.Text = "Thống kê";
            // 
            // pnlHeader
            // 
            this.pnlHeader.BackColor = System.Drawing.Color.White;
            this.pnlHeader.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlHeader.Controls.Add(this.lblTitle);
            this.pnlHeader.Controls.Add(this.btnBack);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlHeader.Margin = new System.Windows.Forms.Padding(4);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Padding = new System.Windows.Forms.Padding(13, 12, 13, 12);
            this.pnlHeader.Size = new System.Drawing.Size(1312, 35);
            this.pnlHeader.TabIndex = 5;
            // 
            // lblTitle
            // 
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.Location = new System.Drawing.Point(108, 4);
            this.lblTitle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(175, 24);
            this.lblTitle.TabIndex = 1;
            this.lblTitle.Text = "Lịch sử nộp bài";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnBack
            // 
            this.btnBack.BackColor = System.Drawing.Color.Gainsboro;
            this.btnBack.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBack.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBack.Location = new System.Drawing.Point(1, 2);
            this.btnBack.Margin = new System.Windows.Forms.Padding(4);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(101, 29);
            this.btnBack.TabIndex = 0;
            this.btnBack.Text = "< Quay lại";
            this.btnBack.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnBack.UseVisualStyleBackColor = false;
            // 
            // pnlFilters
            // 
            this.pnlFilters.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlFilters.BackColor = System.Drawing.Color.White;
            this.pnlFilters.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlFilters.Controls.Add(this.btnApplyFilter);
            this.pnlFilters.Controls.Add(this.cmbStatus);
            this.pnlFilters.Controls.Add(this.cmbProblems);
            this.pnlFilters.Controls.Add(this.lblFilter);
            this.pnlFilters.Location = new System.Drawing.Point(0, 52);
            this.pnlFilters.Margin = new System.Windows.Forms.Padding(4, 4, 4, 20);
            this.pnlFilters.Name = "pnlFilters";
            this.pnlFilters.Padding = new System.Windows.Forms.Padding(13, 12, 13, 12);
            this.pnlFilters.Size = new System.Drawing.Size(1312, 57);
            this.pnlFilters.TabIndex = 6;
            // 
            // btnApplyFilter
            // 
            this.btnApplyFilter.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.btnApplyFilter.Location = new System.Drawing.Point(591, 13);
            this.btnApplyFilter.Margin = new System.Windows.Forms.Padding(13, 0, 0, 0);
            this.btnApplyFilter.Name = "btnApplyFilter";
            this.btnApplyFilter.Size = new System.Drawing.Size(100, 30);
            this.btnApplyFilter.TabIndex = 6;
            this.btnApplyFilter.Text = "Áp dụng";
            this.btnApplyFilter.UseVisualStyleBackColor = true;
            // 
            // cmbStatus
            // 
            this.cmbStatus.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbStatus.FormattingEnabled = true;
            this.cmbStatus.Location = new System.Drawing.Point(338, 14);
            this.cmbStatus.Margin = new System.Windows.Forms.Padding(4, 12, 4, 4);
            this.cmbStatus.Name = "cmbStatus";
            this.cmbStatus.Size = new System.Drawing.Size(227, 28);
            this.cmbStatus.TabIndex = 5;
            this.cmbStatus.Text = "Tất cả trạng thái";
            // 
            // cmbProblems
            // 
            this.cmbProblems.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbProblems.FormattingEnabled = true;
            this.cmbProblems.Location = new System.Drawing.Point(89, 14);
            this.cmbProblems.Margin = new System.Windows.Forms.Padding(4, 12, 4, 4);
            this.cmbProblems.Name = "cmbProblems";
            this.cmbProblems.Size = new System.Drawing.Size(227, 28);
            this.cmbProblems.TabIndex = 4;
            this.cmbProblems.Text = "Tất cả bài tập";
            // 
            // lblFilter
            // 
            this.lblFilter.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.lblFilter.Location = new System.Drawing.Point(3, 15);
            this.lblFilter.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblFilter.Name = "lblFilter";
            this.lblFilter.Size = new System.Drawing.Size(89, 21);
            this.lblFilter.TabIndex = 3;
            this.lblFilter.Text = "Lọc theo:";
            this.lblFilter.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ucSubmissions
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.Controls.Add(this.dgvSubmissions);
            this.Controls.Add(this.pnlStatsContainer);
            this.Controls.Add(this.lblSummary);
            this.Controls.Add(this.pnlFilters);
            this.Controls.Add(this.pnlHeader);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "ucSubmissions";
            this.Size = new System.Drawing.Size(1312, 801);
            ((System.ComponentModel.ISupportInitialize)(this.dgvSubmissions)).EndInit();
            this.pnlStatsContainer.ResumeLayout(false);
            this.tblStatsLayout.ResumeLayout(false);
            this.pnlStatError.ResumeLayout(false);
            this.pnlStatWA.ResumeLayout(false);
            this.pnlStatAccepted.ResumeLayout(false);
            this.pnlStatTotal.ResumeLayout(false);
            this.pnlHeader.ResumeLayout(false);
            this.pnlFilters.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label lblSummary;
        private System.Windows.Forms.DataGridView dgvSubmissions;
        private System.Windows.Forms.Panel pnlStatsContainer;
        private System.Windows.Forms.TableLayoutPanel tblStatsLayout;
        private System.Windows.Forms.Panel pnlStatError;
        private System.Windows.Forms.Label lblStatErrorValue;
        private System.Windows.Forms.Label lblStatErrorTitle;
        private System.Windows.Forms.Panel pnlStatWA;
        private System.Windows.Forms.Label lblStatWAValue;
        private System.Windows.Forms.Label lblStatWATitle;
        private System.Windows.Forms.Panel pnlStatAccepted;
        private System.Windows.Forms.Label lblStatAcceptedValue;
        private System.Windows.Forms.Label lblStatAcceptedTitle;
        private System.Windows.Forms.Panel pnlStatTotal;
        private System.Windows.Forms.Label lblStatTotalValue;
        private System.Windows.Forms.Label lblStatTotalTitle;
        private System.Windows.Forms.Label lblStatsTitle;
        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Button btnBack;
        private System.Windows.Forms.Panel pnlFilters;
        private System.Windows.Forms.Button btnApplyFilter;
        private System.Windows.Forms.ComboBox cmbStatus;
        private System.Windows.Forms.ComboBox cmbProblems;
        private System.Windows.Forms.Label lblFilter;
        private System.Windows.Forms.DataGridViewTextBoxColumn colHash;
        private System.Windows.Forms.DataGridViewTextBoxColumn colProblemName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTimestamp;
        private System.Windows.Forms.DataGridViewTextBoxColumn colStatus;
        private System.Windows.Forms.DataGridViewTextBoxColumn colScore;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTestCases;
        private System.Windows.Forms.DataGridViewButtonColumn colView;
    }
}