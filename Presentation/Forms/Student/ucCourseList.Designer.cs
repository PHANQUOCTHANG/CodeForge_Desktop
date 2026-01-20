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

        private System.Windows.Forms.Panel pnlSearchContainer;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.ComboBox cmbFilterLevel;
        private System.Windows.Forms.Label lblSearchIcon;
        private System.Windows.Forms.Label lblFilterLabel;

        private System.Windows.Forms.FlowLayoutPanel flpCourseGrid;

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
            this.btnHeroAction = new System.Windows.Forms.Button();
            this.lblHeroSubtitle = new System.Windows.Forms.Label();
            this.lblHeroTitle = new System.Windows.Forms.Label();
            this.pnlSearchContainer = new System.Windows.Forms.Panel();
            this.lblFilterLabel = new System.Windows.Forms.Label();
            this.lblSearchIcon = new System.Windows.Forms.Label();
            this.cmbFilterLevel = new System.Windows.Forms.ComboBox();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.flpCourseGrid = new System.Windows.Forms.FlowLayoutPanel();
            this.pnlHero.SuspendLayout();
            this.pnlSearchContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlHero (Banner)
            // 
            this.pnlHero.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(35)))), ((int)(((byte)(50)))));
            this.pnlHero.Controls.Add(this.btnHeroAction);
            this.pnlHero.Controls.Add(this.lblHeroSubtitle);
            this.pnlHero.Controls.Add(this.lblHeroTitle);
            this.pnlHero.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHero.Location = new System.Drawing.Point(0, 0);
            this.pnlHero.Name = "pnlHero";
            this.pnlHero.Padding = new System.Windows.Forms.Padding(40);
            this.pnlHero.Size = new System.Drawing.Size(1000, 180);
            this.pnlHero.TabIndex = 0;
            // 
            // btnHeroAction
            // 
            this.btnHeroAction.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(120)))), ((int)(((byte)(215)))));
            this.btnHeroAction.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnHeroAction.FlatAppearance.BorderSize = 0;
            this.btnHeroAction.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnHeroAction.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnHeroAction.ForeColor = System.Drawing.Color.White;
            this.btnHeroAction.Location = new System.Drawing.Point(48, 120);
            this.btnHeroAction.Name = "btnHeroAction";
            this.btnHeroAction.Size = new System.Drawing.Size(160, 40);
            this.btnHeroAction.TabIndex = 2;
            this.btnHeroAction.Text = "Lộ trình học tập ➔";
            this.btnHeroAction.UseVisualStyleBackColor = false;
            // 
            // lblHeroSubtitle
            // 
            this.lblHeroSubtitle.AutoSize = true;
            this.lblHeroSubtitle.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.lblHeroSubtitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.lblHeroSubtitle.Location = new System.Drawing.Point(45, 80);
            this.lblHeroSubtitle.Name = "lblHeroSubtitle";
            this.lblHeroSubtitle.Size = new System.Drawing.Size(406, 20);
            this.lblHeroSubtitle.TabIndex = 1;
            this.lblHeroSubtitle.Text = "Hàng trăm khóa học lập trình chất lượng cao đang chờ bạn.";
            // 
            // lblHeroTitle
            // 
            this.lblHeroTitle.AutoSize = true;
            this.lblHeroTitle.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Bold);
            this.lblHeroTitle.ForeColor = System.Drawing.Color.White;
            this.lblHeroTitle.Location = new System.Drawing.Point(40, 30);
            this.lblHeroTitle.Name = "lblHeroTitle";
            this.lblHeroTitle.Size = new System.Drawing.Size(359, 45);
            this.lblHeroTitle.TabIndex = 0;
            this.lblHeroTitle.Text = "Khám phá tri thức mới";
            // 
            // pnlSearchContainer
            // 
            this.pnlSearchContainer.BackColor = System.Drawing.Color.White;
            this.pnlSearchContainer.Controls.Add(this.lblFilterLabel);
            this.pnlSearchContainer.Controls.Add(this.lblSearchIcon);
            this.pnlSearchContainer.Controls.Add(this.cmbFilterLevel);
            this.pnlSearchContainer.Controls.Add(this.txtSearch);
            this.pnlSearchContainer.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlSearchContainer.Location = new System.Drawing.Point(0, 180);
            this.pnlSearchContainer.Name = "pnlSearchContainer";
            this.pnlSearchContainer.Size = new System.Drawing.Size(1000, 70);
            this.pnlSearchContainer.TabIndex = 1;
            // 
            // lblFilterLabel
            // 
            this.lblFilterLabel.AutoSize = true;
            this.lblFilterLabel.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblFilterLabel.ForeColor = System.Drawing.Color.Gray;
            this.lblFilterLabel.Location = new System.Drawing.Point(520, 26);
            this.lblFilterLabel.Name = "lblFilterLabel";
            this.lblFilterLabel.Size = new System.Drawing.Size(56, 19);
            this.lblFilterLabel.TabIndex = 3;
            this.lblFilterLabel.Text = "Độ khó:";
            // 
            // lblSearchIcon
            // 
            this.lblSearchIcon.AutoSize = true;
            this.lblSearchIcon.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.lblSearchIcon.Location = new System.Drawing.Point(45, 23);
            this.lblSearchIcon.Name = "lblSearchIcon";
            this.lblSearchIcon.Size = new System.Drawing.Size(26, 21);
            this.lblSearchIcon.TabIndex = 2;
            this.lblSearchIcon.Text = "🔍";
            // 
            // cmbFilterLevel
            // 
            this.cmbFilterLevel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFilterLevel.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.cmbFilterLevel.FormattingEnabled = true;
            this.cmbFilterLevel.Items.AddRange(new object[] {
            "Tất cả level",
            "Beginner",
            "Intermediate",
            "Advanced"});
            this.cmbFilterLevel.Location = new System.Drawing.Point(582, 22);
            this.cmbFilterLevel.Name = "cmbFilterLevel";
            this.cmbFilterLevel.Size = new System.Drawing.Size(180, 28);
            this.cmbFilterLevel.TabIndex = 1;
            // 
            // txtSearch
            // 
            this.txtSearch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSearch.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.txtSearch.Location = new System.Drawing.Point(80, 22);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(400, 27);
            this.txtSearch.TabIndex = 0;
            // 
            // flpCourseGrid
            // 
            this.flpCourseGrid.AutoScroll = true;
            this.flpCourseGrid.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(247)))), ((int)(((byte)(250)))));
            this.flpCourseGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flpCourseGrid.Location = new System.Drawing.Point(0, 250);
            this.flpCourseGrid.Name = "flpCourseGrid";
            this.flpCourseGrid.Padding = new System.Windows.Forms.Padding(30);
            this.flpCourseGrid.Size = new System.Drawing.Size(1000, 450);
            this.flpCourseGrid.TabIndex = 2;
            // 
            // ucCourseList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.flpCourseGrid);
            this.Controls.Add(this.pnlSearchContainer);
            this.Controls.Add(this.pnlHero);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Name = "ucCourseList";
            this.Size = new System.Drawing.Size(1000, 700);
            this.pnlHero.ResumeLayout(false);
            this.pnlHero.PerformLayout();
            this.pnlSearchContainer.ResumeLayout(false);
            this.pnlSearchContainer.PerformLayout();
            this.ResumeLayout(false);
        }

        #endregion
    }
}