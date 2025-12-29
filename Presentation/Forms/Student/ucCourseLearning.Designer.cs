using System.Drawing;

namespace CodeForge_Desktop.Presentation.Forms.Student.UserControls
{
    partial class ucCourseLearning
    {
        private System.ComponentModel.IContainer components = null;

        // Top Bar
        private System.Windows.Forms.Panel pnlTopBar;
        private System.Windows.Forms.Button btnBack;
        private System.Windows.Forms.Label lblCourseTitle;
        private System.Windows.Forms.Label lblProgress;
        private System.Windows.Forms.ProgressBar pbProgress;

        // Main Layout
        private System.Windows.Forms.SplitContainer splitMain;

        // Left Side (Content)
        private System.Windows.Forms.Panel pnlLeftContainer;
        private System.Windows.Forms.Panel pnlVideoArea;
        private System.Windows.Forms.Panel pnlNavButtons;
        private System.Windows.Forms.TabControl tabInfo;
        private System.Windows.Forms.TabPage tabDescription;
        private System.Windows.Forms.TabPage tabQnA;
        private System.Windows.Forms.WebBrowser wbDescription;

        // Nav Buttons
        private System.Windows.Forms.Button btnPrev;
        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.Button btnMarkCompleted; // Nút hoàn thành

        // Right Side (Curriculum)
        private System.Windows.Forms.Panel pnlRightContainer;
        private System.Windows.Forms.Panel pnlSidebarHeader;
        private System.Windows.Forms.Label lblCurriculumHeader;
        private System.Windows.Forms.FlowLayoutPanel flpCurriculum;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.pnlTopBar = new System.Windows.Forms.Panel();
            this.lblProgress = new System.Windows.Forms.Label();
            this.pbProgress = new System.Windows.Forms.ProgressBar();
            this.lblCourseTitle = new System.Windows.Forms.Label();
            this.btnBack = new System.Windows.Forms.Button();
            this.splitMain = new System.Windows.Forms.SplitContainer();
            this.pnlLeftContainer = new System.Windows.Forms.Panel();
            this.pnlVideoArea = new System.Windows.Forms.Panel();
            this.pnlNavButtons = new System.Windows.Forms.Panel();
            this.btnMarkCompleted = new System.Windows.Forms.Button();
            this.btnNext = new System.Windows.Forms.Button();
            this.btnPrev = new System.Windows.Forms.Button();
            this.tabInfo = new System.Windows.Forms.TabControl();
            this.tabDescription = new System.Windows.Forms.TabPage();
            this.wbDescription = new System.Windows.Forms.WebBrowser();
            this.tabQnA = new System.Windows.Forms.TabPage();
            this.pnlRightContainer = new System.Windows.Forms.Panel();
            this.flpCurriculum = new System.Windows.Forms.FlowLayoutPanel();
            this.pnlSidebarHeader = new System.Windows.Forms.Panel();
            this.lblCurriculumHeader = new System.Windows.Forms.Label();
            this.pnlTopBar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitMain)).BeginInit();
            this.splitMain.Panel1.SuspendLayout();
            this.splitMain.Panel2.SuspendLayout();
            this.splitMain.SuspendLayout();
            this.pnlLeftContainer.SuspendLayout();
            this.pnlNavButtons.SuspendLayout();
            this.tabInfo.SuspendLayout();
            this.tabDescription.SuspendLayout();
            this.pnlRightContainer.SuspendLayout();
            this.pnlSidebarHeader.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlTopBar
            // 
            this.pnlTopBar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))));
            this.pnlTopBar.Controls.Add(this.lblProgress);
            this.pnlTopBar.Controls.Add(this.pbProgress);
            this.pnlTopBar.Controls.Add(this.lblCourseTitle);
            this.pnlTopBar.Controls.Add(this.btnBack);
            this.pnlTopBar.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTopBar.Location = new System.Drawing.Point(0, 0);
            this.pnlTopBar.Name = "pnlTopBar";
            this.pnlTopBar.Padding = new System.Windows.Forms.Padding(10);
            this.pnlTopBar.Size = new System.Drawing.Size(1200, 60);
            this.pnlTopBar.TabIndex = 0;
            // 
            // lblProgress
            // 
            this.lblProgress.Dock = System.Windows.Forms.DockStyle.Right;
            this.lblProgress.ForeColor = System.Drawing.Color.LightGray;
            this.lblProgress.Location = new System.Drawing.Point(880, 10);
            this.lblProgress.Name = "lblProgress";
            this.lblProgress.Size = new System.Drawing.Size(60, 40);
            this.lblProgress.TabIndex = 3;
            this.lblProgress.Text = "0%";
            this.lblProgress.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // pbProgress
            // 
            this.pbProgress.Dock = System.Windows.Forms.DockStyle.Right;
            this.pbProgress.Location = new System.Drawing.Point(940, 10);
            this.pbProgress.Name = "pbProgress";
            this.pbProgress.Size = new System.Drawing.Size(250, 40);
            this.pbProgress.TabIndex = 2;
            // 
            // lblCourseTitle
            // 
            this.lblCourseTitle.AutoSize = true;
            this.lblCourseTitle.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblCourseTitle.ForeColor = System.Drawing.Color.White;
            this.lblCourseTitle.Location = new System.Drawing.Point(120, 18);
            this.lblCourseTitle.Name = "lblCourseTitle";
            this.lblCourseTitle.Size = new System.Drawing.Size(180, 28);
            this.lblCourseTitle.TabIndex = 1;
            this.lblCourseTitle.Text = "Đang tải dữ liệu...";
            // 
            // btnBack
            // 
            this.btnBack.BackColor = System.Drawing.Color.Transparent;
            this.btnBack.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnBack.FlatAppearance.BorderSize = 0;
            this.btnBack.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBack.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnBack.ForeColor = System.Drawing.Color.White;
            this.btnBack.Location = new System.Drawing.Point(10, 12);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(90, 36);
            this.btnBack.TabIndex = 0;
            this.btnBack.Text = "❮ Quay lại";
            this.btnBack.UseVisualStyleBackColor = false;
            // 
            // splitMain
            // 
            this.splitMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitMain.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitMain.Location = new System.Drawing.Point(0, 60);
            this.splitMain.Name = "splitMain";
            // 
            // splitMain.Panel1
            // 
            this.splitMain.Panel1.Controls.Add(this.pnlLeftContainer);
            // 
            // splitMain.Panel2
            // 
            this.splitMain.Panel2.Controls.Add(this.pnlRightContainer);
            this.splitMain.Size = new System.Drawing.Size(1200, 660);
            this.splitMain.SplitterDistance = 850;
            this.splitMain.TabIndex = 1;
            // 
            // pnlLeftContainer
            // 
            this.pnlLeftContainer.Controls.Add(this.pnlVideoArea);
            this.pnlLeftContainer.Controls.Add(this.pnlNavButtons);
            this.pnlLeftContainer.Controls.Add(this.tabInfo);
            this.pnlLeftContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlLeftContainer.Location = new System.Drawing.Point(0, 0);
            this.pnlLeftContainer.Name = "pnlLeftContainer";
            this.pnlLeftContainer.Size = new System.Drawing.Size(850, 660);
            this.pnlLeftContainer.TabIndex = 0;
            // 
            // pnlVideoArea
            // 
            this.pnlVideoArea.BackColor = System.Drawing.Color.Black;
            this.pnlVideoArea.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlVideoArea.Location = new System.Drawing.Point(0, 0);
            this.pnlVideoArea.Name = "pnlVideoArea";
            this.pnlVideoArea.Size = new System.Drawing.Size(850, 482);
            this.pnlVideoArea.TabIndex = 2;
            // 
            // pnlNavButtons
            // 
            this.pnlNavButtons.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.pnlNavButtons.Controls.Add(this.btnMarkCompleted);
            this.pnlNavButtons.Controls.Add(this.btnNext);
            this.pnlNavButtons.Controls.Add(this.btnPrev);
            this.pnlNavButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlNavButtons.Location = new System.Drawing.Point(0, 482);
            this.pnlNavButtons.Name = "pnlNavButtons";
            this.pnlNavButtons.Size = new System.Drawing.Size(850, 60);
            this.pnlNavButtons.TabIndex = 1;
            // 
            // btnMarkCompleted
            // 
            this.btnMarkCompleted.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnMarkCompleted.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(76)))), ((int)(((byte)(175)))), ((int)(((byte)(80)))));
            this.btnMarkCompleted.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnMarkCompleted.FlatAppearance.BorderSize = 0;
            this.btnMarkCompleted.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMarkCompleted.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnMarkCompleted.ForeColor = System.Drawing.Color.White;
            this.btnMarkCompleted.Location = new System.Drawing.Point(550, 10);
            this.btnMarkCompleted.Name = "btnMarkCompleted";
            this.btnMarkCompleted.Size = new System.Drawing.Size(160, 40);
            this.btnMarkCompleted.TabIndex = 2;
            this.btnMarkCompleted.Text = "✔ Hoàn thành";
            this.btnMarkCompleted.UseVisualStyleBackColor = false;
            // 
            // btnNext
            // 
            this.btnNext.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnNext.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(120)))), ((int)(((byte)(215)))));
            this.btnNext.FlatAppearance.BorderSize = 0;
            this.btnNext.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNext.ForeColor = System.Drawing.Color.White;
            this.btnNext.Location = new System.Drawing.Point(738, 10);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(100, 40);
            this.btnNext.TabIndex = 1;
            this.btnNext.Text = "Bài tiếp ❯";
            this.btnNext.UseVisualStyleBackColor = false;
            // 
            // btnPrev
            // 
            this.btnPrev.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.btnPrev.FlatAppearance.BorderSize = 0;
            this.btnPrev.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPrev.ForeColor = System.Drawing.Color.White;
            this.btnPrev.Location = new System.Drawing.Point(12, 10);
            this.btnPrev.Name = "btnPrev";
            this.btnPrev.Size = new System.Drawing.Size(100, 40);
            this.btnPrev.TabIndex = 0;
            this.btnPrev.Text = "❮ Bài trước";
            this.btnPrev.UseVisualStyleBackColor = false;
            // 
            // tabInfo
            // 
            this.tabInfo.Controls.Add(this.tabDescription);
            this.tabInfo.Controls.Add(this.tabQnA);
            this.tabInfo.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tabInfo.Location = new System.Drawing.Point(0, 542);
            this.tabInfo.Name = "tabInfo";
            this.tabInfo.SelectedIndex = 0;
            this.tabInfo.Size = new System.Drawing.Size(850, 118);
            this.tabInfo.TabIndex = 0;
            // 
            // tabDescription
            // 
            this.tabDescription.Controls.Add(this.wbDescription);
            this.tabDescription.Location = new System.Drawing.Point(4, 29);
            this.tabDescription.Name = "tabDescription";
            this.tabDescription.Padding = new System.Windows.Forms.Padding(3);
            this.tabDescription.Size = new System.Drawing.Size(842, 85);
            this.tabDescription.TabIndex = 0;
            this.tabDescription.Text = "Tổng quan bài học";
            this.tabDescription.UseVisualStyleBackColor = true;
            // 
            // wbDescription
            // 
            this.wbDescription.Dock = System.Windows.Forms.DockStyle.Fill;
            this.wbDescription.Location = new System.Drawing.Point(3, 3);
            this.wbDescription.MinimumSize = new System.Drawing.Size(20, 20);
            this.wbDescription.Name = "wbDescription";
            this.wbDescription.Size = new System.Drawing.Size(836, 79);
            this.wbDescription.TabIndex = 0;
            // 
            // tabQnA
            // 
            this.tabQnA.Location = new System.Drawing.Point(4, 25);
            this.tabQnA.Name = "tabQnA";
            this.tabQnA.Padding = new System.Windows.Forms.Padding(3);
            this.tabQnA.Size = new System.Drawing.Size(842, 221);
            this.tabQnA.TabIndex = 1;
            this.tabQnA.Text = "Hỏi đáp";
            this.tabQnA.UseVisualStyleBackColor = true;
            // 
            // pnlRightContainer
            // 
            this.pnlRightContainer.BackColor = Color.FromArgb(248, 249, 250);
            this.pnlRightContainer.Controls.Add(this.flpCurriculum);
            this.pnlRightContainer.Controls.Add(this.pnlSidebarHeader);
            this.pnlRightContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlRightContainer.Location = new System.Drawing.Point(0, 0);
            this.pnlRightContainer.Name = "pnlRightContainer";
            this.pnlRightContainer.Size = new System.Drawing.Size(346, 660);
            this.pnlRightContainer.TabIndex = 0;
            // 
            // flpCurriculum
            // 
            this.flpCurriculum.BackColor = Color.FromArgb(248, 249, 250);
            this.flpCurriculum.AutoScroll = true;
            this.flpCurriculum.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flpCurriculum.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flpCurriculum.Location = new System.Drawing.Point(0, 40);
            this.flpCurriculum.Name = "flpCurriculum";
            this.flpCurriculum.Padding = new System.Windows.Forms.Padding(0, 5, 0, 0);
            this.flpCurriculum.Size = new System.Drawing.Size(346, 620);
            this.flpCurriculum.TabIndex = 1;
            this.flpCurriculum.WrapContents = false;
            // 
            // pnlSidebarHeader
            // 
            this.pnlSidebarHeader.BackColor = System.Drawing.Color.WhiteSmoke;
            this.pnlSidebarHeader.Controls.Add(this.lblCurriculumHeader);
            this.pnlSidebarHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlSidebarHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlSidebarHeader.Name = "pnlSidebarHeader";
            this.pnlSidebarHeader.Size = new System.Drawing.Size(346, 40);
            this.pnlSidebarHeader.TabIndex = 0;
            // 
            // lblCurriculumHeader
            // 
            this.lblCurriculumHeader.AutoSize = true;
            this.lblCurriculumHeader.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblCurriculumHeader.Location = new System.Drawing.Point(10, 10);
            this.lblCurriculumHeader.Name = "lblCurriculumHeader";
            this.lblCurriculumHeader.Size = new System.Drawing.Size(162, 23);
            this.lblCurriculumHeader.TabIndex = 0;
            this.lblCurriculumHeader.Text = "Nội dung khóa học";
            // 
            // ucCourseLearning
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitMain);
            this.Controls.Add(this.pnlTopBar);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Name = "ucCourseLearning";
            this.Size = new System.Drawing.Size(1200, 720);
            this.pnlTopBar.ResumeLayout(false);
            this.pnlTopBar.PerformLayout();
            this.splitMain.Panel1.ResumeLayout(false);
            this.splitMain.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitMain)).EndInit();
            this.splitMain.ResumeLayout(false);
            this.pnlLeftContainer.ResumeLayout(false);
            this.pnlNavButtons.ResumeLayout(false);
            this.tabInfo.ResumeLayout(false);
            this.tabDescription.ResumeLayout(false);
            this.pnlRightContainer.ResumeLayout(false);
            this.pnlSidebarHeader.ResumeLayout(false);
            this.pnlSidebarHeader.PerformLayout();
            this.ResumeLayout(false);

        }
    }
}