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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.pnlFilters = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.cmbDifficulty = new System.Windows.Forms.ComboBox();
            this.lblDifficultyFilter = new System.Windows.Forms.Label();
            this.pnlSearchContainer = new System.Windows.Forms.Panel();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.picSearchIcon = new System.Windows.Forms.PictureBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.dgvProblemList = new System.Windows.Forms.DataGridView();
            this.colHash = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colProblemName = new System.Windows.Forms.DataGridViewLinkColumn();
            this.colDifficulty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TagProblem = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colStatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lblSummary = new System.Windows.Forms.Label();
            this.pnlFilters.SuspendLayout();
            this.pnlSearchContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picSearchIcon)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvProblemList)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlFilters
            // 
            this.pnlFilters.BackColor = System.Drawing.Color.Transparent;
            this.pnlFilters.Controls.Add(this.lblTitle);
            this.pnlFilters.Controls.Add(this.cmbDifficulty);
            this.pnlFilters.Controls.Add(this.lblDifficultyFilter);
            this.pnlFilters.Controls.Add(this.pnlSearchContainer);
            this.pnlFilters.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlFilters.Location = new System.Drawing.Point(0, 0);
            this.pnlFilters.Margin = new System.Windows.Forms.Padding(4);
            this.pnlFilters.Name = "pnlFilters";
            this.pnlFilters.Padding = new System.Windows.Forms.Padding(13, 10, 13, 10);
            this.pnlFilters.Size = new System.Drawing.Size(1117, 98);
            this.pnlFilters.TabIndex = 3;
            // 
            // lblTitle
            // 
            this.lblTitle.BackColor = System.Drawing.Color.Transparent;
            this.lblTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI Semibold", 14F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.Black;
            this.lblTitle.Location = new System.Drawing.Point(13, 10);
            this.lblTitle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(1091, 37);
            this.lblTitle.TabIndex = 6;
            this.lblTitle.Text = "Danh sách bài tập";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cmbDifficulty
            // 
            this.cmbDifficulty.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbDifficulty.BackColor = System.Drawing.Color.White;
            this.cmbDifficulty.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDifficulty.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbDifficulty.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cmbDifficulty.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.cmbDifficulty.FormattingEnabled = true;
            this.cmbDifficulty.Items.AddRange(new object[] {
            "Tất cả",
            "Dễ",
            "Trung bình",
            "Khó"});
            this.cmbDifficulty.Location = new System.Drawing.Point(881, 56);
            this.cmbDifficulty.Margin = new System.Windows.Forms.Padding(4);
            this.cmbDifficulty.Name = "cmbDifficulty";
            this.cmbDifficulty.Size = new System.Drawing.Size(219, 31);
            this.cmbDifficulty.TabIndex = 3;
            // 
            // lblDifficultyFilter
            // 
            this.lblDifficultyFilter.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblDifficultyFilter.AutoSize = true;
            this.lblDifficultyFilter.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblDifficultyFilter.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblDifficultyFilter.Location = new System.Drawing.Point(764, 60);
            this.lblDifficultyFilter.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblDifficultyFilter.Name = "lblDifficultyFilter";
            this.lblDifficultyFilter.Size = new System.Drawing.Size(98, 23);
            this.lblDifficultyFilter.TabIndex = 2;
            this.lblDifficultyFilter.Text = "Lọc độ khó:";
            // 
            // pnlSearchContainer
            // 
            this.pnlSearchContainer.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlSearchContainer.BackColor = System.Drawing.Color.White;
            this.pnlSearchContainer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlSearchContainer.Controls.Add(this.txtSearch);
            this.pnlSearchContainer.Controls.Add(this.picSearchIcon);
            this.pnlSearchContainer.Location = new System.Drawing.Point(15, 55);
            this.pnlSearchContainer.Margin = new System.Windows.Forms.Padding(4);
            this.pnlSearchContainer.Name = "pnlSearchContainer";
            this.pnlSearchContainer.Padding = new System.Windows.Forms.Padding(7, 0, 0, 0);
            this.pnlSearchContainer.Size = new System.Drawing.Size(587, 33);
            this.pnlSearchContainer.TabIndex = 4;
            // 
            // txtSearch
            // 
            this.txtSearch.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtSearch.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtSearch.Font = new System.Drawing.Font("Segoe UI", 11.25F);
            this.txtSearch.Location = new System.Drawing.Point(34, 0);
            this.txtSearch.Margin = new System.Windows.Forms.Padding(4);
            this.txtSearch.Multiline = true;
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(551, 31);
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
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.Controls.Add(this.dgvProblemList);
            this.panel1.Controls.Add(this.lblSummary);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 98);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1117, 599);
            this.panel1.TabIndex = 4;
            // 
            // dgvProblemList
            // 
            this.dgvProblemList.AllowUserToAddRows = false;
            this.dgvProblemList.AllowUserToDeleteRows = false;
            this.dgvProblemList.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvProblemList.BackgroundColor = System.Drawing.Color.White;
            this.dgvProblemList.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dgvProblemList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvProblemList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colHash,
            this.colProblemName,
            this.colDifficulty,
            this.TagProblem,
            this.colStatus});
            this.dgvProblemList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvProblemList.Location = new System.Drawing.Point(0, 0);
            this.dgvProblemList.Margin = new System.Windows.Forms.Padding(4);
            this.dgvProblemList.Name = "dgvProblemList";
            this.dgvProblemList.ReadOnly = true;
            this.dgvProblemList.RowHeadersVisible = false;
            this.dgvProblemList.RowHeadersWidth = 51;
            this.dgvProblemList.RowTemplate.Height = 40;
            this.dgvProblemList.Size = new System.Drawing.Size(1117, 569);
            this.dgvProblemList.TabIndex = 4;
            // 
            // colHash
            // 
            this.colHash.FillWeight = 11.02F;
            this.colHash.HeaderText = "#";
            this.colHash.MinimumWidth = 6;
            this.colHash.Name = "colHash";
            this.colHash.ReadOnly = true;
            this.colHash.Visible = false;
            // 
            // colProblemName
            // 
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.colProblemName.DefaultCellStyle = dataGridViewCellStyle1;
            this.colProblemName.FillWeight = 53.78963F;
            this.colProblemName.HeaderText = "Tên bài tập";
            this.colProblemName.MinimumWidth = 6;
            this.colProblemName.Name = "colProblemName";
            this.colProblemName.ReadOnly = true;
            this.colProblemName.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.colProblemName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // colDifficulty
            // 
            this.colDifficulty.FillWeight = 35.84239F;
            this.colDifficulty.HeaderText = "Độ khó";
            this.colDifficulty.MinimumWidth = 6;
            this.colDifficulty.Name = "colDifficulty";
            this.colDifficulty.ReadOnly = true;
            // 
            // TagProblem
            // 
            this.TagProblem.FillWeight = 51.77662F;
            this.TagProblem.HeaderText = "Dạng bài";
            this.TagProblem.MinimumWidth = 6;
            this.TagProblem.Name = "TagProblem";
            this.TagProblem.ReadOnly = true;
            // 
            // colStatus
            // 
            this.colStatus.FillWeight = 17.57126F;
            this.colStatus.HeaderText = "Trạng thái";
            this.colStatus.MinimumWidth = 6;
            this.colStatus.Name = "colStatus";
            this.colStatus.ReadOnly = true;
            // 
            // lblSummary
            // 
            this.lblSummary.BackColor = System.Drawing.Color.WhiteSmoke;
            this.lblSummary.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblSummary.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblSummary.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSummary.Location = new System.Drawing.Point(0, 569);
            this.lblSummary.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblSummary.Name = "lblSummary";
            this.lblSummary.Padding = new System.Windows.Forms.Padding(13, 0, 0, 0);
            this.lblSummary.Size = new System.Drawing.Size(1117, 30);
            this.lblSummary.TabIndex = 3;
            this.lblSummary.Text = "Tổng số 8 bài tập | Đã nộp: 2 | Chưa nộp: 6";
            this.lblSummary.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ucProblemList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.pnlFilters);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "ucProblemList";
            this.Size = new System.Drawing.Size(1117, 697);
            this.pnlFilters.ResumeLayout(false);
            this.pnlFilters.PerformLayout();
            this.pnlSearchContainer.ResumeLayout(false);
            this.pnlSearchContainer.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picSearchIcon)).EndInit();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvProblemList)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel pnlFilters;
        private System.Windows.Forms.ComboBox cmbDifficulty;
        private System.Windows.Forms.Label lblDifficultyFilter;
        private System.Windows.Forms.Panel pnlSearchContainer;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.PictureBox picSearchIcon;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridView dgvProblemList;
        private System.Windows.Forms.DataGridViewTextBoxColumn colHash;
        private System.Windows.Forms.DataGridViewLinkColumn colProblemName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDifficulty;
        private System.Windows.Forms.DataGridViewTextBoxColumn TagProblem;
        private System.Windows.Forms.DataGridViewTextBoxColumn colStatus;
        private System.Windows.Forms.Label lblSummary;
        private System.Windows.Forms.Label lblTitle;
    }
}