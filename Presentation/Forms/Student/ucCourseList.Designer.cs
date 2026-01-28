namespace CodeForge_Desktop.Presentation.Forms.Student.UserControls
{
    partial class ucCourseList
    {
        private System.ComponentModel.IContainer components = null;

        // --- CÁC CONTROL GIAO DIỆN ---
        private System.Windows.Forms.Panel pnlHero;
        private System.Windows.Forms.Label lblHeroTitle;
        private System.Windows.Forms.Label lblHeroSubtitle;
        private System.Windows.Forms.Button btnHeroAction;
        private System.Windows.Forms.PictureBox pbHeroDecoration;

        private System.Windows.Forms.Panel pnlSearchContainer;
        private System.Windows.Forms.Panel pnlSearchBox;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Label lblSearchIcon;
        private System.Windows.Forms.Panel pnlFilterBox;
        private System.Windows.Forms.ComboBox cmbFilterLevel;
        private System.Windows.Forms.Label lblFilterIcon;
        private System.Windows.Forms.Button btnClearFilters;

        private System.Windows.Forms.Panel pnlStatsBar;
        private System.Windows.Forms.Label lblTotalCourses;
        private System.Windows.Forms.Label lblFilteredResults;

        private System.Windows.Forms.FlowLayoutPanel flpCourseGrid;
        private System.Windows.Forms.Panel pnlLoading;
        private System.Windows.Forms.Label lblLoading;

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
            this.pnlHero = new System.Windows.Forms.Panel();
            this.pbHeroDecoration = new System.Windows.Forms.PictureBox();
            this.btnHeroAction = new System.Windows.Forms.Button();
            this.lblHeroSubtitle = new System.Windows.Forms.Label();
            this.lblHeroTitle = new System.Windows.Forms.Label();

            this.pnlSearchContainer = new System.Windows.Forms.Panel();
            this.btnClearFilters = new System.Windows.Forms.Button();
            this.pnlFilterBox = new System.Windows.Forms.Panel();
            this.lblFilterIcon = new System.Windows.Forms.Label();
            this.cmbFilterLevel = new System.Windows.Forms.ComboBox();
            this.pnlSearchBox = new System.Windows.Forms.Panel();
            this.lblSearchIcon = new System.Windows.Forms.Label();
            this.txtSearch = new System.Windows.Forms.TextBox();

            this.pnlStatsBar = new System.Windows.Forms.Panel();
            this.lblFilteredResults = new System.Windows.Forms.Label();
            this.lblTotalCourses = new System.Windows.Forms.Label();

            this.flpCourseGrid = new System.Windows.Forms.FlowLayoutPanel();

            this.pnlLoading = new System.Windows.Forms.Panel();
            this.lblLoading = new System.Windows.Forms.Label();

            this.pnlHero.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbHeroDecoration)).BeginInit();
            this.pnlSearchContainer.SuspendLayout();
            this.pnlFilterBox.SuspendLayout();
            this.pnlSearchBox.SuspendLayout();
            this.pnlStatsBar.SuspendLayout();
            this.pnlLoading.SuspendLayout();
            this.SuspendLayout();

            // 
            // pnlHero
            // 
            this.pnlHero.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(30)))), ((int)(((byte)(48)))));
            this.pnlHero.Controls.Add(this.pbHeroDecoration);
            this.pnlHero.Controls.Add(this.btnHeroAction);
            this.pnlHero.Controls.Add(this.lblHeroSubtitle);
            this.pnlHero.Controls.Add(this.lblHeroTitle);
            this.pnlHero.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHero.Location = new System.Drawing.Point(0, 0);
            this.pnlHero.Name = "pnlHero";
            this.pnlHero.Padding = new System.Windows.Forms.Padding(50, 40, 50, 40);
            this.pnlHero.Size = new System.Drawing.Size(1000, 200);
            this.pnlHero.TabIndex = 0;

            // 
            // pbHeroDecoration
            // 
            this.pbHeroDecoration.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pbHeroDecoration.BackColor = System.Drawing.Color.Transparent;
            this.pbHeroDecoration.Location = new System.Drawing.Point(750, 30);
            this.pbHeroDecoration.Name = "pbHeroDecoration";
            this.pbHeroDecoration.Size = new System.Drawing.Size(200, 140);
            this.pbHeroDecoration.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbHeroDecoration.TabIndex = 3;
            this.pbHeroDecoration.TabStop = false;

            // 
            // btnHeroAction
            // 
            this.btnHeroAction.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(255)))));
            this.btnHeroAction.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnHeroAction.FlatAppearance.BorderSize = 0;
            this.btnHeroAction.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnHeroAction.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnHeroAction.ForeColor = System.Drawing.Color.White;
            this.btnHeroAction.Location = new System.Drawing.Point(53, 135);
            this.btnHeroAction.Name = "btnHeroAction";
            this.btnHeroAction.Size = new System.Drawing.Size(180, 42);
            this.btnHeroAction.TabIndex = 2;
            this.btnHeroAction.Text = "🎯 Tạo lộ trình học";
            this.btnHeroAction.UseVisualStyleBackColor = false;
            this.btnHeroAction.Click += new System.EventHandler(this.btnHeroAction_Click);
            this.btnHeroAction.MouseEnter += new System.EventHandler(this.btnHeroAction_MouseEnter);
            this.btnHeroAction.MouseLeave += new System.EventHandler(this.btnHeroAction_MouseLeave);

            // 
            // lblHeroSubtitle
            // 
            this.lblHeroSubtitle.AutoSize = true;
            this.lblHeroSubtitle.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.lblHeroSubtitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(190)))), ((int)(((byte)(200)))));
            this.lblHeroSubtitle.Location = new System.Drawing.Point(50, 90);
            this.lblHeroSubtitle.MaximumSize = new System.Drawing.Size(600, 0);
            this.lblHeroSubtitle.Name = "lblHeroSubtitle";
            this.lblHeroSubtitle.Size = new System.Drawing.Size(550, 25);
            this.lblHeroSubtitle.TabIndex = 1;
            this.lblHeroSubtitle.Text = "Khám phá hàng trăm khóa học lập trình chất lượng cao từ cơ bản đến nâng cao";

            // 
            // lblHeroTitle
            // 
            this.lblHeroTitle.AutoSize = true;
            this.lblHeroTitle.Font = new System.Drawing.Font("Segoe UI", 28F, System.Drawing.FontStyle.Bold);
            this.lblHeroTitle.ForeColor = System.Drawing.Color.White;
            this.lblHeroTitle.Location = new System.Drawing.Point(45, 35);
            this.lblHeroTitle.Name = "lblHeroTitle";
            this.lblHeroTitle.Size = new System.Drawing.Size(510, 62);
            this.lblHeroTitle.TabIndex = 0;
            this.lblHeroTitle.Text = "Khám phá tri thức mới";

            // 
            // pnlSearchContainer
            // 
            this.pnlSearchContainer.BackColor = System.Drawing.Color.White;
            this.pnlSearchContainer.Controls.Add(this.btnClearFilters);
            this.pnlSearchContainer.Controls.Add(this.pnlFilterBox);
            this.pnlSearchContainer.Controls.Add(this.pnlSearchBox);
            this.pnlSearchContainer.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlSearchContainer.Location = new System.Drawing.Point(0, 200);
            this.pnlSearchContainer.Name = "pnlSearchContainer";
            this.pnlSearchContainer.Padding = new System.Windows.Forms.Padding(50, 20, 50, 20);
            this.pnlSearchContainer.Size = new System.Drawing.Size(1000, 80);
            this.pnlSearchContainer.TabIndex = 1;

            // 
            // btnClearFilters
            // 
            this.btnClearFilters.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClearFilters.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(249)))), ((int)(((byte)(250)))));
            this.btnClearFilters.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnClearFilters.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.btnClearFilters.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClearFilters.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnClearFilters.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(117)))), ((int)(((byte)(125)))));
            this.btnClearFilters.Location = new System.Drawing.Point(835, 25);
            this.btnClearFilters.Name = "btnClearFilters";
            this.btnClearFilters.Size = new System.Drawing.Size(100, 35);
            this.btnClearFilters.TabIndex = 2;
            this.btnClearFilters.Text = "🔄 Xóa lọc";
            this.btnClearFilters.UseVisualStyleBackColor = false;
            this.btnClearFilters.Click += new System.EventHandler(this.btnClearFilters_Click);

            // 
            // pnlFilterBox
            // 
            this.pnlFilterBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlFilterBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(249)))), ((int)(((byte)(250)))));
            this.pnlFilterBox.Controls.Add(this.lblFilterIcon);
            this.pnlFilterBox.Controls.Add(this.cmbFilterLevel);
            this.pnlFilterBox.Location = new System.Drawing.Point(620, 25);
            this.pnlFilterBox.Name = "pnlFilterBox";
            this.pnlFilterBox.Size = new System.Drawing.Size(200, 35);
            this.pnlFilterBox.TabIndex = 1;

            // 
            // lblFilterIcon
            // 
            this.lblFilterIcon.AutoSize = true;
            this.lblFilterIcon.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.lblFilterIcon.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(117)))), ((int)(((byte)(125)))));
            this.lblFilterIcon.Location = new System.Drawing.Point(8, 7);
            this.lblFilterIcon.Name = "lblFilterIcon";
            this.lblFilterIcon.Size = new System.Drawing.Size(28, 25);
            this.lblFilterIcon.TabIndex = 1;
            this.lblFilterIcon.Text = "🎚️";

            // 
            // cmbFilterLevel
            // 
            this.cmbFilterLevel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(249)))), ((int)(((byte)(250)))));
            this.cmbFilterLevel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFilterLevel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbFilterLevel.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cmbFilterLevel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(37)))), ((int)(((byte)(41)))));
            this.cmbFilterLevel.FormattingEnabled = true;
            this.cmbFilterLevel.Items.AddRange(new object[] {
            "Tất cả level",
            "Beginner",
            "Intermediate",
            "Advanced"});
            this.cmbFilterLevel.Location = new System.Drawing.Point(38, 5);
            this.cmbFilterLevel.Name = "cmbFilterLevel";
            this.cmbFilterLevel.Size = new System.Drawing.Size(155, 31);
            this.cmbFilterLevel.TabIndex = 0;

            // 
            // pnlSearchBox
            // 
            this.pnlSearchBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(249)))), ((int)(((byte)(250)))));
            this.pnlSearchBox.Controls.Add(this.lblSearchIcon);
            this.pnlSearchBox.Controls.Add(this.txtSearch);
            this.pnlSearchBox.Location = new System.Drawing.Point(53, 25);
            this.pnlSearchBox.Name = "pnlSearchBox";
            this.pnlSearchBox.Size = new System.Drawing.Size(450, 35);
            this.pnlSearchBox.TabIndex = 0;

            // 
            // lblSearchIcon
            // 
            this.lblSearchIcon.AutoSize = true;
            this.lblSearchIcon.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.lblSearchIcon.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(117)))), ((int)(((byte)(125)))));
            this.lblSearchIcon.Location = new System.Drawing.Point(10, 5);
            this.lblSearchIcon.Name = "lblSearchIcon";
            this.lblSearchIcon.Size = new System.Drawing.Size(39, 28);
            this.lblSearchIcon.TabIndex = 1;
            this.lblSearchIcon.Text = "🔍";

            // 
            // txtSearch
            // 
            this.txtSearch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(249)))), ((int)(((byte)(250)))));
            this.txtSearch.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtSearch.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.txtSearch.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(37)))), ((int)(((byte)(41)))));
            this.txtSearch.Location = new System.Drawing.Point(50, 8);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(390, 25);
            this.txtSearch.TabIndex = 0;
            this.txtSearch.Text = "Tìm kiếm khóa học...";
            this.txtSearch.ForeColor = System.Drawing.Color.Gray;
            this.txtSearch.Enter += new System.EventHandler(this.txtSearch_Enter);
            this.txtSearch.Leave += new System.EventHandler(this.txtSearch_Leave);

            // 
            // pnlStatsBar
            // 
            this.pnlStatsBar.BackColor = System.Drawing.Color.White;
            this.pnlStatsBar.Controls.Add(this.lblFilteredResults);
            this.pnlStatsBar.Controls.Add(this.lblTotalCourses);
            this.pnlStatsBar.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlStatsBar.Location = new System.Drawing.Point(0, 280);
            this.pnlStatsBar.Name = "pnlStatsBar";
            this.pnlStatsBar.Padding = new System.Windows.Forms.Padding(50, 12, 50, 12);
            this.pnlStatsBar.Size = new System.Drawing.Size(1000, 45);
            this.pnlStatsBar.TabIndex = 2;

            // 
            // lblFilteredResults
            // 
            this.lblFilteredResults.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblFilteredResults.AutoSize = true;
            this.lblFilteredResults.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Italic);
            this.lblFilteredResults.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(117)))), ((int)(((byte)(125)))));
            this.lblFilteredResults.Location = new System.Drawing.Point(820, 14);
            this.lblFilteredResults.Name = "lblFilteredResults";
            this.lblFilteredResults.Size = new System.Drawing.Size(0, 20);
            this.lblFilteredResults.TabIndex = 1;

            // 
            // lblTotalCourses
            // 
            this.lblTotalCourses.AutoSize = true;
            this.lblTotalCourses.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblTotalCourses.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(37)))), ((int)(((byte)(41)))));
            this.lblTotalCourses.Location = new System.Drawing.Point(50, 12);
            this.lblTotalCourses.Name = "lblTotalCourses";
            this.lblTotalCourses.Size = new System.Drawing.Size(150, 23);
            this.lblTotalCourses.TabIndex = 0;
            this.lblTotalCourses.Text = "📚 0 khóa học";

            // 
            // flpCourseGrid
            // 
            this.flpCourseGrid.AutoScroll = true;
            this.flpCourseGrid.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(249)))), ((int)(((byte)(250)))));
            this.flpCourseGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flpCourseGrid.Location = new System.Drawing.Point(0, 325);
            this.flpCourseGrid.Name = "flpCourseGrid";
            this.flpCourseGrid.Padding = new System.Windows.Forms.Padding(35, 20, 35, 30);
            this.flpCourseGrid.Size = new System.Drawing.Size(1000, 375);
            this.flpCourseGrid.TabIndex = 3;

            // 
            // pnlLoading
            // 
            this.pnlLoading.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(240)))));
            this.pnlLoading.Controls.Add(this.lblLoading);
            this.pnlLoading.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlLoading.Location = new System.Drawing.Point(0, 325);
            this.pnlLoading.Name = "pnlLoading";
            this.pnlLoading.Size = new System.Drawing.Size(1000, 375);
            this.pnlLoading.TabIndex = 4;
            this.pnlLoading.Visible = false;

            // 
            // lblLoading
            // 
            this.lblLoading.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblLoading.AutoSize = true;
            this.lblLoading.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.lblLoading.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(117)))), ((int)(((byte)(125)))));
            this.lblLoading.Location = new System.Drawing.Point(420, 175);
            this.lblLoading.Name = "lblLoading";
            this.lblLoading.Size = new System.Drawing.Size(160, 28);
            this.lblLoading.TabIndex = 0;
            this.lblLoading.Text = "⏳ Đang tải...";
            this.lblLoading.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            // 
            // ucCourseList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.pnlLoading);
            this.Controls.Add(this.flpCourseGrid);
            this.Controls.Add(this.pnlStatsBar);
            this.Controls.Add(this.pnlSearchContainer);
            this.Controls.Add(this.pnlHero);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Name = "ucCourseList";
            this.Size = new System.Drawing.Size(1000, 700);
            this.pnlHero.ResumeLayout(false);
            this.pnlHero.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbHeroDecoration)).EndInit();
            this.pnlSearchContainer.ResumeLayout(false);
            this.pnlFilterBox.ResumeLayout(false);
            this.pnlFilterBox.PerformLayout();
            this.pnlSearchBox.ResumeLayout(false);
            this.pnlSearchBox.PerformLayout();
            this.pnlStatsBar.ResumeLayout(false);
            this.pnlStatsBar.PerformLayout();
            this.pnlLoading.ResumeLayout(false);
            this.pnlLoading.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
    }
}