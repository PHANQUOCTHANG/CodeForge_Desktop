using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace CodeForge_Desktop.Presentation.Forms.Student.UserControls
{
    partial class ucCourseList
    {
        private IContainer components = null;

        // Controls
        private Panel pnlHeader;
        private Button btnBack;
        private Label lblHeader;

        private Panel pnlSearch;
        private PictureBox pbSearchIcon;
        private TextBox txtSearch;
        private ComboBox cmbFilterLevel;

        private SplitContainer splitContainerMain;
        private DataGridView dgvCourses;

        private Panel pnlCoursePreview;
        private PictureBox pbThumbnail;
        private Label lblTitle; 
        private Label lblMeta;
        private TextBox txtShortOverview;
        private Panel pnlInfoStudents;
        private Label lblInfoStudentsTitle;
        private Label lblStudents;
        private Panel pnlInfoDuration;
        private Label lblInfoDurationTitle;
        private Label lblDuration;
        private Button btnViewDetails;
        private Button btnEnrollContinue;
        private ProgressBar pbCourseProgress; // NEW: small progress bar in preview panel

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
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.btnBack = new System.Windows.Forms.Button();
            this.lblHeader = new System.Windows.Forms.Label();
            this.pnlSearch = new System.Windows.Forms.Panel();
            this.pbSearchIcon = new System.Windows.Forms.PictureBox();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.cmbFilterLevel = new System.Windows.Forms.ComboBox();
            this.splitContainerMain = new System.Windows.Forms.SplitContainer();
            this.dgvCourses = new System.Windows.Forms.DataGridView();
            this.pnlCoursePreview = new System.Windows.Forms.Panel();
            this.pbThumbnail = new System.Windows.Forms.PictureBox();
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblMeta = new System.Windows.Forms.Label();
            this.txtShortOverview = new System.Windows.Forms.TextBox();
            this.pnlInfoStudents = new System.Windows.Forms.Panel();
            this.lblInfoStudentsTitle = new System.Windows.Forms.Label();
            this.lblStudents = new System.Windows.Forms.Label();
            this.pnlInfoDuration = new System.Windows.Forms.Panel();
            this.lblInfoDurationTitle = new System.Windows.Forms.Label();
            this.lblDuration = new System.Windows.Forms.Label();
            this.btnViewDetails = new System.Windows.Forms.Button();
            this.btnEnrollContinue = new System.Windows.Forms.Button();
            this.pbCourseProgress = new System.Windows.Forms.ProgressBar();
            this.pnlHeader.SuspendLayout();
            this.pnlSearch.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbSearchIcon)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerMain)).BeginInit();
            this.splitContainerMain.Panel1.SuspendLayout();
            this.splitContainerMain.Panel2.SuspendLayout();
            this.splitContainerMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCourses)).BeginInit();
            this.pnlCoursePreview.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbThumbnail)).BeginInit();
            this.pnlInfoStudents.SuspendLayout();
            this.pnlInfoDuration.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlHeader
            // 
            this.pnlHeader.BackColor = System.Drawing.SystemColors.ControlLight;
            this.pnlHeader.Controls.Add(this.btnBack);
            this.pnlHeader.Controls.Add(this.lblHeader);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Padding = new System.Windows.Forms.Padding(8);
            this.pnlHeader.Size = new System.Drawing.Size(980, 48);
            this.pnlHeader.TabIndex = 2;
            // 
            // btnBack
            // 
            this.btnBack.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnBack.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnBack.Location = new System.Drawing.Point(8, 8);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(92, 32);
            this.btnBack.TabIndex = 0;
            this.btnBack.Text = "← Quay lại";
            // 
            // lblHeader
            // 
            this.lblHeader.AutoSize = true;
            this.lblHeader.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.lblHeader.Location = new System.Drawing.Point(112, 12);
            this.lblHeader.Name = "lblHeader";
            this.lblHeader.Size = new System.Drawing.Size(182, 25);
            this.lblHeader.TabIndex = 1;
            this.lblHeader.Text = "Danh sách khóa học";
            // 
            // pnlSearch
            // 
            this.pnlSearch.BackColor = System.Drawing.Color.Transparent;
            this.pnlSearch.Controls.Add(this.pbSearchIcon);
            this.pnlSearch.Controls.Add(this.txtSearch);
            this.pnlSearch.Controls.Add(this.cmbFilterLevel);
            this.pnlSearch.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlSearch.Location = new System.Drawing.Point(0, 48);
            this.pnlSearch.Name = "pnlSearch";
            this.pnlSearch.Padding = new System.Windows.Forms.Padding(12, 8, 12, 8);
            this.pnlSearch.Size = new System.Drawing.Size(980, 48);
            this.pnlSearch.TabIndex = 1;
            // 
            // pbSearchIcon
            // 
            this.pbSearchIcon.BackColor = System.Drawing.Color.Transparent;
            this.pbSearchIcon.Location = new System.Drawing.Point(12, 14);
            this.pbSearchIcon.Name = "pbSearchIcon";
            this.pbSearchIcon.Size = new System.Drawing.Size(20, 20);
            this.pbSearchIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pbSearchIcon.TabIndex = 0;
            this.pbSearchIcon.TabStop = false;
            // 
            // txtSearch
            // 
            this.txtSearch.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtSearch.Location = new System.Drawing.Point(40, 12);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(680, 27);
            this.txtSearch.TabIndex = 1;
            // 
            // cmbFilterLevel
            // 
            this.cmbFilterLevel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFilterLevel.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.cmbFilterLevel.Items.AddRange(new object[] {
            "Tất cả level",
            "Beginner",
            "Intermediate",
            "Advanced"});
            this.cmbFilterLevel.Location = new System.Drawing.Point(740, 11);
            this.cmbFilterLevel.Name = "cmbFilterLevel";
            this.cmbFilterLevel.Size = new System.Drawing.Size(160, 28);
            this.cmbFilterLevel.TabIndex = 2;
            // 
            // splitContainerMain
            // 
            this.splitContainerMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerMain.Location = new System.Drawing.Point(0, 96);
            this.splitContainerMain.Name = "splitContainerMain";
            // 
            // splitContainerMain.Panel1
            // 
            this.splitContainerMain.Panel1.BackColor = System.Drawing.Color.White;
            this.splitContainerMain.Panel1.Controls.Add(this.dgvCourses);
            this.splitContainerMain.Panel1.Padding = new System.Windows.Forms.Padding(12);
            // 
            // splitContainerMain.Panel2
            // 
            this.splitContainerMain.Panel2.BackColor = System.Drawing.Color.WhiteSmoke;
            this.splitContainerMain.Panel2.Controls.Add(this.pnlCoursePreview);
            this.splitContainerMain.Panel2.Padding = new System.Windows.Forms.Padding(12);
            this.splitContainerMain.Size = new System.Drawing.Size(980, 524);
            this.splitContainerMain.SplitterDistance = 632;
            this.splitContainerMain.TabIndex = 0;
            // 
            // dgvCourses
            // 
            this.dgvCourses.AllowUserToAddRows = false;
            this.dgvCourses.AllowUserToDeleteRows = false;
            this.dgvCourses.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvCourses.BackgroundColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 9F);
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvCourses.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvCourses.ColumnHeadersHeight = 29;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 9F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvCourses.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvCourses.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvCourses.Location = new System.Drawing.Point(12, 12);
            this.dgvCourses.MultiSelect = false;
            this.dgvCourses.Name = "dgvCourses";
            this.dgvCourses.ReadOnly = true;
            this.dgvCourses.RowHeadersVisible = false;
            this.dgvCourses.RowHeadersWidth = 51;
            this.dgvCourses.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvCourses.Size = new System.Drawing.Size(608, 500);
            this.dgvCourses.TabIndex = 0;
            // 
            // pnlCoursePreview
            // 
            this.pnlCoursePreview.BackColor = System.Drawing.Color.White;
            this.pnlCoursePreview.Controls.Add(this.pbThumbnail);
            this.pnlCoursePreview.Controls.Add(this.lblTitle);
            this.pnlCoursePreview.Controls.Add(this.lblMeta);
            this.pnlCoursePreview.Controls.Add(this.txtShortOverview);
            this.pnlCoursePreview.Controls.Add(this.pnlInfoStudents);
            this.pnlCoursePreview.Controls.Add(this.pnlInfoDuration);
            this.pnlCoursePreview.Controls.Add(this.btnViewDetails);
            this.pnlCoursePreview.Controls.Add(this.btnEnrollContinue);
            this.pnlCoursePreview.Controls.Add(this.pbCourseProgress);
            this.pnlCoursePreview.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlCoursePreview.Location = new System.Drawing.Point(12, 12);
            this.pnlCoursePreview.Name = "pnlCoursePreview";
            this.pnlCoursePreview.Padding = new System.Windows.Forms.Padding(18);
            this.pnlCoursePreview.Size = new System.Drawing.Size(320, 500);
            this.pnlCoursePreview.TabIndex = 0;
            // 
            // pbThumbnail
            // 
            this.pbThumbnail.BackColor = System.Drawing.Color.Transparent;
            this.pbThumbnail.Location = new System.Drawing.Point(21, 38);
            this.pbThumbnail.Name = "pbThumbnail";
            this.pbThumbnail.Size = new System.Drawing.Size(81, 73);
            this.pbThumbnail.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbThumbnail.TabIndex = 0;
            this.pbThumbnail.TabStop = false;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblTitle.Location = new System.Drawing.Point(114, 38);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(196, 28);
            this.lblTitle.TabIndex = 1;
            this.lblTitle.Text = "Chọn một khóa học";
            // 
            // lblMeta
            // 
            this.lblMeta.AutoSize = true;
            this.lblMeta.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblMeta.ForeColor = System.Drawing.Color.Gray;
            this.lblMeta.Location = new System.Drawing.Point(115, 77);
            this.lblMeta.Name = "lblMeta";
            this.lblMeta.Size = new System.Drawing.Size(194, 20);
            this.lblMeta.TabIndex = 2;
            this.lblMeta.Text = "Beginner | JavaScript | ⭐ 0.0";
            // 
            // txtShortOverview
            // 
            this.txtShortOverview.BackColor = System.Drawing.Color.WhiteSmoke;
            this.txtShortOverview.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtShortOverview.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtShortOverview.Location = new System.Drawing.Point(20, 150);
            this.txtShortOverview.Multiline = true;
            this.txtShortOverview.Name = "txtShortOverview";
            this.txtShortOverview.ReadOnly = true;
            this.txtShortOverview.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtShortOverview.Size = new System.Drawing.Size(290, 110);
            this.txtShortOverview.TabIndex = 3;
            // 
            // pnlInfoStudents
            // 
            this.pnlInfoStudents.BackColor = System.Drawing.Color.WhiteSmoke;
            this.pnlInfoStudents.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlInfoStudents.Controls.Add(this.lblInfoStudentsTitle);
            this.pnlInfoStudents.Controls.Add(this.lblStudents);
            this.pnlInfoStudents.Location = new System.Drawing.Point(20, 276);
            this.pnlInfoStudents.Name = "pnlInfoStudents";
            this.pnlInfoStudents.Padding = new System.Windows.Forms.Padding(8);
            this.pnlInfoStudents.Size = new System.Drawing.Size(130, 60);
            this.pnlInfoStudents.TabIndex = 4;
            // 
            // lblInfoStudentsTitle
            // 
            this.lblInfoStudentsTitle.AutoSize = true;
            this.lblInfoStudentsTitle.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.lblInfoStudentsTitle.ForeColor = System.Drawing.Color.Gray;
            this.lblInfoStudentsTitle.Location = new System.Drawing.Point(8, 6);
            this.lblInfoStudentsTitle.Name = "lblInfoStudentsTitle";
            this.lblInfoStudentsTitle.Size = new System.Drawing.Size(62, 19);
            this.lblInfoStudentsTitle.TabIndex = 0;
            this.lblInfoStudentsTitle.Text = "Học viên";
            // 
            // lblStudents
            // 
            this.lblStudents.AutoSize = true;
            this.lblStudents.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblStudents.Location = new System.Drawing.Point(8, 28);
            this.lblStudents.Name = "lblStudents";
            this.lblStudents.Size = new System.Drawing.Size(73, 23);
            this.lblStudents.TabIndex = 1;
            this.lblStudents.Text = "0 người";
            // 
            // pnlInfoDuration
            // 
            this.pnlInfoDuration.BackColor = System.Drawing.Color.WhiteSmoke;
            this.pnlInfoDuration.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlInfoDuration.Controls.Add(this.lblInfoDurationTitle);
            this.pnlInfoDuration.Controls.Add(this.lblDuration);
            this.pnlInfoDuration.Location = new System.Drawing.Point(164, 276);
            this.pnlInfoDuration.Name = "pnlInfoDuration";
            this.pnlInfoDuration.Padding = new System.Windows.Forms.Padding(8);
            this.pnlInfoDuration.Size = new System.Drawing.Size(130, 60);
            this.pnlInfoDuration.TabIndex = 5;
            // 
            // lblInfoDurationTitle
            // 
            this.lblInfoDurationTitle.AutoSize = true;
            this.lblInfoDurationTitle.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.lblInfoDurationTitle.ForeColor = System.Drawing.Color.Gray;
            this.lblInfoDurationTitle.Location = new System.Drawing.Point(8, 6);
            this.lblInfoDurationTitle.Name = "lblInfoDurationTitle";
            this.lblInfoDurationTitle.Size = new System.Drawing.Size(74, 19);
            this.lblInfoDurationTitle.TabIndex = 0;
            this.lblInfoDurationTitle.Text = "Thời lượng";
            // 
            // lblDuration
            // 
            this.lblDuration.AutoSize = true;
            this.lblDuration.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblDuration.Location = new System.Drawing.Point(8, 28);
            this.lblDuration.Name = "lblDuration";
            this.lblDuration.Size = new System.Drawing.Size(52, 23);
            this.lblDuration.TabIndex = 1;
            this.lblDuration.Text = "0 giờ";
            // 
            // btnViewDetails
            // 
            this.btnViewDetails.BackColor = System.Drawing.Color.White;
            this.btnViewDetails.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(120)))), ((int)(((byte)(215)))));
            this.btnViewDetails.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnViewDetails.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnViewDetails.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(120)))), ((int)(((byte)(215)))));
            this.btnViewDetails.Location = new System.Drawing.Point(20, 356);
            this.btnViewDetails.Name = "btnViewDetails";
            this.btnViewDetails.Size = new System.Drawing.Size(274, 40);
            this.btnViewDetails.TabIndex = 6;
            this.btnViewDetails.Text = "Xem chi tiết";
            this.btnViewDetails.UseVisualStyleBackColor = false;
            // 
            // btnEnrollContinue
            // 
            this.btnEnrollContinue.BackColor = System.Drawing.Color.White;
            this.btnEnrollContinue.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(177)))), ((int)(((byte)(64)))));
            this.btnEnrollContinue.FlatAppearance.BorderSize = 2;
            this.btnEnrollContinue.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEnrollContinue.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnEnrollContinue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(177)))), ((int)(((byte)(64)))));
            this.btnEnrollContinue.Location = new System.Drawing.Point(20, 408);
            this.btnEnrollContinue.Name = "btnEnrollContinue";
            this.btnEnrollContinue.Size = new System.Drawing.Size(274, 56);
            this.btnEnrollContinue.TabIndex = 7;
            this.btnEnrollContinue.Text = "Đăng ký / Tiếp tục";
            this.btnEnrollContinue.UseVisualStyleBackColor = false;
            // 
            // pbCourseProgress
            // 
            this.pbCourseProgress.Location = new System.Drawing.Point(20, 384);
            this.pbCourseProgress.Name = "pbCourseProgress";
            this.pbCourseProgress.Size = new System.Drawing.Size(274, 12);
            this.pbCourseProgress.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.pbCourseProgress.Value = 0;
            this.pbCourseProgress.Visible = false;
            // 
            // ucCourseList
            // 
            this.BackColor = System.Drawing.SystemColors.Control;
            this.Controls.Add(this.splitContainerMain);
            this.Controls.Add(this.pnlSearch);
            this.Controls.Add(this.pnlHeader);
            this.Name = "ucCourseList";
            this.Size = new System.Drawing.Size(980, 620);
            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            this.pnlSearch.ResumeLayout(false);
            this.pnlSearch.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbSearchIcon)).EndInit();
            this.splitContainerMain.Panel1.ResumeLayout(false);
            this.splitContainerMain.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerMain)).EndInit();
            this.splitContainerMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvCourses)).EndInit();
            this.pnlCoursePreview.ResumeLayout(false);
            this.pnlCoursePreview.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbThumbnail)).EndInit();
            this.pnlInfoStudents.ResumeLayout(false);
            this.pnlInfoStudents.PerformLayout();
            this.pnlInfoDuration.ResumeLayout(false);
            this.pnlInfoDuration.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
    }
}