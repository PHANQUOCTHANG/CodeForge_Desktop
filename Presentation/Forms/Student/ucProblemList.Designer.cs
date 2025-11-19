// Đảm bảo namespace này khớp với project của bạn
namespace CodeForge_Desktop.Presentation.Forms.Student
{
    partial class ucProblemList
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.btnBack = new System.Windows.Forms.Button();
            this.lblSummary = new System.Windows.Forms.Label();
            this.dgvProblemList = new System.Windows.Forms.DataGridView();
            this.colHash = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colProblemName = new System.Windows.Forms.DataGridViewLinkColumn();
            this.colDifficulty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDeadline = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colStatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colScore = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pnlFilters = new System.Windows.Forms.Panel();
            this.pnlSearchContainer = new System.Windows.Forms.Panel();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.picSearchIcon = new System.Windows.Forms.PictureBox();
            this.btnFilter = new System.Windows.Forms.Button();
            this.pnlHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvProblemList)).BeginInit();
            this.pnlFilters.SuspendLayout();
            this.pnlSearchContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picSearchIcon)).BeginInit();
            this.SuspendLayout();
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
            this.pnlHeader.Size = new System.Drawing.Size(1117, 44);
            this.pnlHeader.TabIndex = 0;
            // 
            // lblTitle
            // 
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.Location = new System.Drawing.Point(123, 3);
            this.lblTitle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(171, 34);
            this.lblTitle.TabIndex = 1;
            this.lblTitle.Text = "Danh sách bài tập";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnBack
            // 
            this.btnBack.BackColor = System.Drawing.Color.Gainsboro;
            this.btnBack.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBack.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.btnBack.Location = new System.Drawing.Point(3, 3);
            this.btnBack.Margin = new System.Windows.Forms.Padding(4);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(112, 34);
            this.btnBack.TabIndex = 0;
            this.btnBack.Text = "< Quay lại";
            this.btnBack.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnBack.UseVisualStyleBackColor = false;
            // 
            // lblSummary
            // 
            this.lblSummary.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblSummary.BackColor = System.Drawing.Color.WhiteSmoke;
            this.lblSummary.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblSummary.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblSummary.Location = new System.Drawing.Point(12, 534);
            this.lblSummary.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblSummary.Name = "lblSummary";
            this.lblSummary.Padding = new System.Windows.Forms.Padding(13, 0, 0, 0);
            this.lblSummary.Size = new System.Drawing.Size(1092, 30);
            this.lblSummary.TabIndex = 1;
            this.lblSummary.Text = "Tổng số 8 bài tập | Đã nộp: 2 | Chưa nộp: 6";
            this.lblSummary.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // dgvProblemList
            // 
            this.dgvProblemList.AllowUserToAddRows = false;
            this.dgvProblemList.AllowUserToDeleteRows = false;
            this.dgvProblemList.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvProblemList.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvProblemList.BackgroundColor = System.Drawing.Color.White;
            this.dgvProblemList.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dgvProblemList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvProblemList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colHash,
            this.colProblemName,
            this.colDifficulty,
            this.colDeadline,
            this.colStatus,
            this.colScore});
            this.dgvProblemList.Location = new System.Drawing.Point(12, 130);
            this.dgvProblemList.Margin = new System.Windows.Forms.Padding(4);
            this.dgvProblemList.Name = "dgvProblemList";
            this.dgvProblemList.ReadOnly = true;
            this.dgvProblemList.RowHeadersVisible = false;
            this.dgvProblemList.RowHeadersWidth = 51;
            this.dgvProblemList.RowTemplate.Height = 35;
            this.dgvProblemList.Size = new System.Drawing.Size(1092, 400);
            this.dgvProblemList.TabIndex = 2;
            this.dgvProblemList.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DgvProblemList_CellClick);
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
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.colProblemName.DefaultCellStyle = dataGridViewCellStyle5;
            this.colProblemName.FillWeight = 35F;
            this.colProblemName.HeaderText = "Tên bài tập";
            this.colProblemName.MinimumWidth = 6;
            this.colProblemName.Name = "colProblemName";
            this.colProblemName.ReadOnly = true;
            this.colProblemName.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.colProblemName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // colDifficulty
            // 
            this.colDifficulty.FillWeight = 15F;
            this.colDifficulty.HeaderText = "Độ khó";
            this.colDifficulty.MinimumWidth = 6;
            this.colDifficulty.Name = "colDifficulty";
            this.colDifficulty.ReadOnly = true;
            // 
            // colDeadline
            // 
            this.colDeadline.FillWeight = 15F;
            this.colDeadline.HeaderText = "Deadline";
            this.colDeadline.MinimumWidth = 6;
            this.colDeadline.Name = "colDeadline";
            this.colDeadline.ReadOnly = true;
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
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.colScore.DefaultCellStyle = dataGridViewCellStyle6;
            this.colScore.FillWeight = 10F;
            this.colScore.HeaderText = "Điểm";
            this.colScore.MinimumWidth = 6;
            this.colScore.Name = "colScore";
            this.colScore.ReadOnly = true;
            // 
            // pnlFilters
            // 
            this.pnlFilters.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlFilters.Controls.Add(this.pnlSearchContainer);
            this.pnlFilters.Controls.Add(this.btnFilter);
            this.pnlFilters.Location = new System.Drawing.Point(0, 52);
            this.pnlFilters.Margin = new System.Windows.Forms.Padding(4);
            this.pnlFilters.Name = "pnlFilters";
            this.pnlFilters.Padding = new System.Windows.Forms.Padding(13, 12, 13, 12);
            this.pnlFilters.Size = new System.Drawing.Size(1117, 59);
            this.pnlFilters.TabIndex = 3;
            // 
            // pnlSearchContainer
            // 
            this.pnlSearchContainer.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlSearchContainer.BackColor = System.Drawing.Color.White;
            this.pnlSearchContainer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlSearchContainer.Controls.Add(this.txtSearch);
            this.pnlSearchContainer.Controls.Add(this.picSearchIcon);
            this.pnlSearchContainer.Location = new System.Drawing.Point(12, 13);
            this.pnlSearchContainer.Margin = new System.Windows.Forms.Padding(4);
            this.pnlSearchContainer.Name = "pnlSearchContainer";
            this.pnlSearchContainer.Padding = new System.Windows.Forms.Padding(7, 0, 0, 0);
            this.pnlSearchContainer.Size = new System.Drawing.Size(971, 33);
            this.pnlSearchContainer.TabIndex = 4;
            // 
            // txtSearch
            // 
            this.txtSearch.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtSearch.Font = new System.Drawing.Font("Segoe UI", 11.25F);
            this.txtSearch.Location = new System.Drawing.Point(36, 3);
            this.txtSearch.Margin = new System.Windows.Forms.Padding(4);
            this.txtSearch.Multiline = true;
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(937, 25);
            this.txtSearch.TabIndex = 0;
            // 
            // picSearchIcon
            // 
            this.picSearchIcon.Dock = System.Windows.Forms.DockStyle.Left;
            this.picSearchIcon.Image = global::CodeForge_Desktop.Properties.Resources.magnifying_glass;
            this.picSearchIcon.Location = new System.Drawing.Point(7, 0);
            this.picSearchIcon.Margin = new System.Windows.Forms.Padding(4);
            this.picSearchIcon.Name = "picSearchIcon";
            this.picSearchIcon.Size = new System.Drawing.Size(27, 31);
            this.picSearchIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picSearchIcon.TabIndex = 1;
            this.picSearchIcon.TabStop = false;
            // 
            // btnFilter
            // 
            this.btnFilter.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnFilter.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.btnFilter.Location = new System.Drawing.Point(984, 12);
            this.btnFilter.Margin = new System.Windows.Forms.Padding(13, 0, 0, 0);
            this.btnFilter.Name = "btnFilter";
            this.btnFilter.Size = new System.Drawing.Size(120, 35);
            this.btnFilter.TabIndex = 2;
            this.btnFilter.Text = "Lọc";
            this.btnFilter.UseVisualStyleBackColor = true;
            // 
            // ucProblemList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.Controls.Add(this.dgvProblemList);
            this.Controls.Add(this.lblSummary);
            this.Controls.Add(this.pnlFilters);
            this.Controls.Add(this.pnlHeader);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "ucProblemList";
            this.Size = new System.Drawing.Size(1117, 697);
            this.pnlHeader.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvProblemList)).EndInit();
            this.pnlFilters.ResumeLayout(false);
            this.pnlSearchContainer.ResumeLayout(false);
            this.pnlSearchContainer.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picSearchIcon)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Button btnBack;
        private System.Windows.Forms.Label lblSummary;
        private System.Windows.Forms.DataGridView dgvProblemList;
        private System.Windows.Forms.DataGridViewTextBoxColumn colHash;
        private System.Windows.Forms.DataGridViewLinkColumn colProblemName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDifficulty;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDeadline;
        private System.Windows.Forms.DataGridViewTextBoxColumn colStatus;
        private System.Windows.Forms.DataGridViewTextBoxColumn colScore;
        private System.Windows.Forms.Panel pnlFilters;
        private System.Windows.Forms.Panel pnlSearchContainer;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.PictureBox picSearchIcon;
        private System.Windows.Forms.Button btnFilter;
    }
}