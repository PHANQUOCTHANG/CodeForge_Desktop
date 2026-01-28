namespace CodeForge_Desktop.Presentation.Forms.Student.UserControls
{
    partial class ucCourseDetails
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblMeta;
        private System.Windows.Forms.Button btnBack;

        private System.Windows.Forms.Panel pnlBody;
        private System.Windows.Forms.Panel pnlMainContent;
        private System.Windows.Forms.Panel pnlSidebar;

        private System.Windows.Forms.TabControl tabContent;
        private System.Windows.Forms.TabPage tabOverview;
        private System.Windows.Forms.TabPage tabCurriculum;
        private System.Windows.Forms.TabPage tabReviews;

        private System.Windows.Forms.WebBrowser wbOverview;
        private System.Windows.Forms.FlowLayoutPanel flpCurriculum;

        private System.Windows.Forms.Panel pnlPriceCard;
        private System.Windows.Forms.PictureBox pbThumbnail;
        private System.Windows.Forms.Label lblPrice;
        private System.Windows.Forms.Button btnEnroll;
        private System.Windows.Forms.Label lblIncludes;
        private System.Windows.Forms.ProgressBar pbProgress;

        private System.Windows.Forms.DataGridView dgvReviews;
        private System.Windows.Forms.Panel pnlReviewInput;
        private System.Windows.Forms.TextBox txtReviewComment;
        private System.Windows.Forms.Button btnSubmitReview;
        private System.Windows.Forms.ComboBox cmbRating;
        private System.Windows.Forms.Label lblWriteReview;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        private void InitializeComponent()
        {
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.lblMeta = new System.Windows.Forms.Label();
            this.lblTitle = new System.Windows.Forms.Label();
            this.btnBack = new System.Windows.Forms.Button();
            this.pnlBody = new System.Windows.Forms.Panel();
            this.pnlMainContent = new System.Windows.Forms.Panel();
            this.tabContent = new System.Windows.Forms.TabControl();
            this.tabOverview = new System.Windows.Forms.TabPage();
            this.wbOverview = new System.Windows.Forms.WebBrowser();
            this.tabCurriculum = new System.Windows.Forms.TabPage();
            this.flpCurriculum = new System.Windows.Forms.FlowLayoutPanel();
            this.tabReviews = new System.Windows.Forms.TabPage();
            this.dgvReviews = new System.Windows.Forms.DataGridView();
            this.pnlReviewInput = new System.Windows.Forms.Panel();
            this.btnSubmitReview = new System.Windows.Forms.Button();
            this.txtReviewComment = new System.Windows.Forms.TextBox();
            this.cmbRating = new System.Windows.Forms.ComboBox();
            this.lblWriteReview = new System.Windows.Forms.Label();
            this.pnlSidebar = new System.Windows.Forms.Panel();
            this.pnlPriceCard = new System.Windows.Forms.Panel();
            this.lblIncludes = new System.Windows.Forms.Label();
            this.pbProgress = new System.Windows.Forms.ProgressBar();
            this.btnEnroll = new System.Windows.Forms.Button();
            this.lblPrice = new System.Windows.Forms.Label();
            this.pbThumbnail = new System.Windows.Forms.PictureBox();
            this.pnlHeader.SuspendLayout();
            this.pnlBody.SuspendLayout();
            this.pnlMainContent.SuspendLayout();
            this.tabContent.SuspendLayout();
            this.tabOverview.SuspendLayout();
            this.tabCurriculum.SuspendLayout();
            this.tabReviews.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvReviews)).BeginInit();
            this.pnlReviewInput.SuspendLayout();
            this.pnlSidebar.SuspendLayout();
            this.pnlPriceCard.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbThumbnail)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlHeader
            // 
            this.pnlHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(24)))), ((int)(((byte)(39)))));
            this.pnlHeader.Controls.Add(this.lblMeta);
            this.pnlHeader.Controls.Add(this.lblTitle);
            this.pnlHeader.Controls.Add(this.btnBack);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Padding = new System.Windows.Forms.Padding(48, 24, 380, 24);
            this.pnlHeader.Size = new System.Drawing.Size(1200, 200);
            this.pnlHeader.TabIndex = 0;
            // 
            // lblMeta
            // 
            this.lblMeta.AutoSize = true;
            this.lblMeta.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular);
            this.lblMeta.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(156)))), ((int)(((byte)(163)))), ((int)(((byte)(175)))));
            this.lblMeta.Location = new System.Drawing.Point(48, 145);
            this.lblMeta.Name = "lblMeta";
            this.lblMeta.Size = new System.Drawing.Size(423, 25);
            this.lblMeta.TabIndex = 2;
            this.lblMeta.Text = "⭐ 4.8  •  1,200 học viên  •  Cập nhật tháng 10/2023";
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 26F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(42, 75);
            this.lblTitle.MaximumSize = new System.Drawing.Size(700, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(383, 60);
            this.lblTitle.TabIndex = 1;
            this.lblTitle.Text = "Course Title Here";
            // 
            // btnBack
            // 
            this.btnBack.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(55)))), ((int)(((byte)(65)))));
            this.btnBack.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnBack.FlatAppearance.BorderSize = 0;
            this.btnBack.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBack.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular);
            this.btnBack.ForeColor = System.Drawing.Color.White;
            this.btnBack.Location = new System.Drawing.Point(32, 18);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(120, 38);
            this.btnBack.TabIndex = 0;
            this.btnBack.Text = "← Quay lại";
            this.btnBack.UseVisualStyleBackColor = false;
            // 
            // pnlBody
            // 
            this.pnlBody.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(244)))), ((int)(((byte)(246)))));
            this.pnlBody.Controls.Add(this.pnlMainContent);
            this.pnlBody.Controls.Add(this.pnlSidebar);
            this.pnlBody.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlBody.Location = new System.Drawing.Point(0, 200);
            this.pnlBody.Name = "pnlBody";
            this.pnlBody.Size = new System.Drawing.Size(1200, 600);
            this.pnlBody.TabIndex = 1;
            // 
            // pnlMainContent
            // 
            this.pnlMainContent.BackColor = System.Drawing.Color.Transparent;
            this.pnlMainContent.Controls.Add(this.tabContent);
            this.pnlMainContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMainContent.Location = new System.Drawing.Point(0, 0);
            this.pnlMainContent.Name = "pnlMainContent";
            this.pnlMainContent.Padding = new System.Windows.Forms.Padding(48, 32, 24, 32);
            this.pnlMainContent.Size = new System.Drawing.Size(820, 600);
            this.pnlMainContent.TabIndex = 1;
            // 
            // tabContent
            // 
            this.tabContent.Controls.Add(this.tabOverview);
            this.tabContent.Controls.Add(this.tabCurriculum);
            this.tabContent.Controls.Add(this.tabReviews);
            this.tabContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabContent.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular);
            this.tabContent.ItemSize = new System.Drawing.Size(140, 48);
            this.tabContent.Location = new System.Drawing.Point(48, 32);
            this.tabContent.Name = "tabContent";
            this.tabContent.Padding = new System.Drawing.Point(20, 0);
            this.tabContent.SelectedIndex = 0;
            this.tabContent.Size = new System.Drawing.Size(748, 536);
            this.tabContent.TabIndex = 0;
            // 
            // tabOverview
            // 
            this.tabOverview.BackColor = System.Drawing.Color.White;
            this.tabOverview.Controls.Add(this.wbOverview);
            this.tabOverview.Location = new System.Drawing.Point(4, 52);
            this.tabOverview.Name = "tabOverview";
            this.tabOverview.Padding = new System.Windows.Forms.Padding(16);
            this.tabOverview.Size = new System.Drawing.Size(740, 480);
            this.tabOverview.TabIndex = 0;
            this.tabOverview.Text = "📖 Giới thiệu";
            // 
            // wbOverview
            // 
            this.wbOverview.Dock = System.Windows.Forms.DockStyle.Fill;
            this.wbOverview.Location = new System.Drawing.Point(16, 16);
            this.wbOverview.MinimumSize = new System.Drawing.Size(20, 20);
            this.wbOverview.Name = "wbOverview";
            this.wbOverview.Size = new System.Drawing.Size(708, 448);
            this.wbOverview.TabIndex = 0;
            // 
            // tabCurriculum
            // 
            this.tabCurriculum.BackColor = System.Drawing.Color.White;
            this.tabCurriculum.Controls.Add(this.flpCurriculum);
            this.tabCurriculum.Location = new System.Drawing.Point(4, 52);
            this.tabCurriculum.Name = "tabCurriculum";
            this.tabCurriculum.Padding = new System.Windows.Forms.Padding(16);
            this.tabCurriculum.Size = new System.Drawing.Size(740, 480);
            this.tabCurriculum.TabIndex = 1;
            this.tabCurriculum.Text = "📚 Nội dung";
            // 
            // flpCurriculum
            // 
            this.flpCurriculum.AutoScroll = true;
            this.flpCurriculum.BackColor = System.Drawing.Color.White;
            this.flpCurriculum.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flpCurriculum.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flpCurriculum.Location = new System.Drawing.Point(16, 16);
            this.flpCurriculum.Name = "flpCurriculum";
            this.flpCurriculum.Padding = new System.Windows.Forms.Padding(8, 16, 8, 24);
            this.flpCurriculum.Size = new System.Drawing.Size(708, 448);
            this.flpCurriculum.TabIndex = 0;
            this.flpCurriculum.WrapContents = false;
            // 
            // tabReviews
            // 
            this.tabReviews.BackColor = System.Drawing.Color.White;
            this.tabReviews.Controls.Add(this.dgvReviews);
            this.tabReviews.Controls.Add(this.pnlReviewInput);
            this.tabReviews.Location = new System.Drawing.Point(4, 52);
            this.tabReviews.Name = "tabReviews";
            this.tabReviews.Padding = new System.Windows.Forms.Padding(16);
            this.tabReviews.Size = new System.Drawing.Size(740, 480);
            this.tabReviews.TabIndex = 2;
            this.tabReviews.Text = "⭐ Đánh giá";
            // 
            // dgvReviews
            // 
            this.dgvReviews.AllowUserToAddRows = false;
            this.dgvReviews.AllowUserToDeleteRows = false;
            this.dgvReviews.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvReviews.BackgroundColor = System.Drawing.Color.White;
            this.dgvReviews.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvReviews.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvReviews.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvReviews.Location = new System.Drawing.Point(16, 200);
            this.dgvReviews.Name = "dgvReviews";
            this.dgvReviews.ReadOnly = true;
            this.dgvReviews.RowHeadersVisible = false;
            this.dgvReviews.RowHeadersWidth = 51;
            this.dgvReviews.Size = new System.Drawing.Size(708, 264);
            this.dgvReviews.TabIndex = 1;
            // 
            // pnlReviewInput
            // 
            this.pnlReviewInput.BackColor = System.Drawing.Color.White;
            this.pnlReviewInput.Controls.Add(this.btnSubmitReview);
            this.pnlReviewInput.Controls.Add(this.txtReviewComment);
            this.pnlReviewInput.Controls.Add(this.cmbRating);
            this.pnlReviewInput.Controls.Add(this.lblWriteReview);
            this.pnlReviewInput.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlReviewInput.Location = new System.Drawing.Point(16, 16);
            this.pnlReviewInput.Name = "pnlReviewInput";
            this.pnlReviewInput.Padding = new System.Windows.Forms.Padding(24);
            this.pnlReviewInput.Size = new System.Drawing.Size(708, 184);
            this.pnlReviewInput.TabIndex = 0;
            // 
            // btnSubmitReview
            // 
            this.btnSubmitReview.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(99)))), ((int)(((byte)(102)))), ((int)(((byte)(241)))));
            this.btnSubmitReview.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSubmitReview.FlatAppearance.BorderSize = 0;
            this.btnSubmitReview.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSubmitReview.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnSubmitReview.ForeColor = System.Drawing.Color.White;
            this.btnSubmitReview.Location = new System.Drawing.Point(24, 125);
            this.btnSubmitReview.Name = "btnSubmitReview";
            this.btnSubmitReview.Size = new System.Drawing.Size(140, 44);
            this.btnSubmitReview.TabIndex = 3;
            this.btnSubmitReview.Text = "Gửi đánh giá";
            this.btnSubmitReview.UseVisualStyleBackColor = false;
            // 
            // txtReviewComment
            // 
            this.txtReviewComment.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtReviewComment.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.txtReviewComment.Location = new System.Drawing.Point(195, 48);
            this.txtReviewComment.Multiline = true;
            this.txtReviewComment.Name = "txtReviewComment";
            this.txtReviewComment.Padding = new System.Windows.Forms.Padding(12);
            this.txtReviewComment.Size = new System.Drawing.Size(480, 90);
            this.txtReviewComment.TabIndex = 2;
            // 
            // cmbRating
            // 
            this.cmbRating.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbRating.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbRating.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.cmbRating.FormattingEnabled = true;
            this.cmbRating.Items.AddRange(new object[] {
            "⭐⭐⭐⭐⭐ Tuyệt vời",
            "⭐⭐⭐⭐ Rất tốt",
            "⭐⭐⭐ Tốt",
            "⭐⭐ Trung bình",
            "⭐ Kém"});
            this.cmbRating.Location = new System.Drawing.Point(24, 48);
            this.cmbRating.Name = "cmbRating";
            this.cmbRating.Size = new System.Drawing.Size(155, 33);
            this.cmbRating.TabIndex = 1;
            // 
            // lblWriteReview
            // 
            this.lblWriteReview.AutoSize = true;
            this.lblWriteReview.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblWriteReview.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(24)))), ((int)(((byte)(39)))));
            this.lblWriteReview.Location = new System.Drawing.Point(20, 12);
            this.lblWriteReview.Name = "lblWriteReview";
            this.lblWriteReview.Size = new System.Drawing.Size(188, 28);
            this.lblWriteReview.TabIndex = 0;
            this.lblWriteReview.Text = "Viết đánh giá của bạn";
            // 
            // pnlSidebar
            // 
            this.pnlSidebar.BackColor = System.Drawing.Color.Transparent;
            this.pnlSidebar.Controls.Add(this.pnlPriceCard);
            this.pnlSidebar.Dock = System.Windows.Forms.DockStyle.Right;
            this.pnlSidebar.Location = new System.Drawing.Point(820, 0);
            this.pnlSidebar.Name = "pnlSidebar";
            this.pnlSidebar.Padding = new System.Windows.Forms.Padding(16, 32, 32, 32);
            this.pnlSidebar.Size = new System.Drawing.Size(380, 600);
            this.pnlSidebar.TabIndex = 0;
            // 
            // pnlPriceCard
            // 
            this.pnlPriceCard.BackColor = System.Drawing.Color.White;
            this.pnlPriceCard.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.pnlPriceCard.Controls.Add(this.lblIncludes);
            this.pnlPriceCard.Controls.Add(this.pbProgress);
            this.pnlPriceCard.Controls.Add(this.btnEnroll);
            this.pnlPriceCard.Controls.Add(this.lblPrice);
            this.pnlPriceCard.Controls.Add(this.pbThumbnail);
            this.pnlPriceCard.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlPriceCard.Location = new System.Drawing.Point(16, 32);
            this.pnlPriceCard.Name = "pnlPriceCard";
            this.pnlPriceCard.Padding = new System.Windows.Forms.Padding(0);
            this.pnlPriceCard.Size = new System.Drawing.Size(332, 540);
            this.pnlPriceCard.TabIndex = 0;
            // 
            // lblIncludes
            // 
            this.lblIncludes.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblIncludes.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(75)))), ((int)(((byte)(85)))), ((int)(((byte)(99)))));
            this.lblIncludes.Location = new System.Drawing.Point(24, 380);
            this.lblIncludes.Name = "lblIncludes";
            this.lblIncludes.Size = new System.Drawing.Size(290, 140);
            this.lblIncludes.TabIndex = 4;
            this.lblIncludes.Text = "📌 Khóa học bao gồm:\r\n\r\n✓ Truy cập trọn đời\r\n✓ Bài tập thực tế\r\n✓ Chứng chỉ hoàn" +
    " thành\r\n✓ Hỗ trợ Q&A 24/7";
            // 
            // pbProgress
            // 
            this.pbProgress.Location = new System.Drawing.Point(24, 350);
            this.pbProgress.Name = "pbProgress";
            this.pbProgress.Size = new System.Drawing.Size(290, 10);
            this.pbProgress.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.pbProgress.TabIndex = 3;
            this.pbProgress.Visible = false;
            // 
            // btnEnroll
            // 
            this.btnEnroll.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(99)))), ((int)(((byte)(102)))), ((int)(((byte)(241)))));
            this.btnEnroll.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnEnroll.FlatAppearance.BorderSize = 0;
            this.btnEnroll.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEnroll.Font = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Bold);
            this.btnEnroll.ForeColor = System.Drawing.Color.White;
            this.btnEnroll.Location = new System.Drawing.Point(24, 280);
            this.btnEnroll.Name = "btnEnroll";
            this.btnEnroll.Size = new System.Drawing.Size(290, 56);
            this.btnEnroll.TabIndex = 2;
            this.btnEnroll.Text = "Mua ngay";
            this.btnEnroll.UseVisualStyleBackColor = false;
            // 
            // lblPrice
            // 
            this.lblPrice.AutoSize = true;
            this.lblPrice.Font = new System.Drawing.Font("Segoe UI", 28F, System.Drawing.FontStyle.Bold);
            this.lblPrice.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(24)))), ((int)(((byte)(39)))));
            this.lblPrice.Location = new System.Drawing.Point(16, 220);
            this.lblPrice.Name = "lblPrice";
            this.lblPrice.Size = new System.Drawing.Size(311, 62);
            this.lblPrice.TabIndex = 1;
            this.lblPrice.Text = "1,299,000 ₫";
            // 
            // pbThumbnail
            // 
            this.pbThumbnail.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(244)))), ((int)(((byte)(246)))));
            this.pbThumbnail.Dock = System.Windows.Forms.DockStyle.Top;
            this.pbThumbnail.Location = new System.Drawing.Point(0, 0);
            this.pbThumbnail.Name = "pbThumbnail";
            this.pbThumbnail.Size = new System.Drawing.Size(332, 210);
            this.pbThumbnail.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbThumbnail.TabIndex = 0;
            this.pbThumbnail.TabStop = false;
            // 
            // ucCourseDetails
            // 
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(244)))), ((int)(((byte)(246)))));
            this.Controls.Add(this.pnlBody);
            this.Controls.Add(this.pnlHeader);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Name = "ucCourseDetails";
            this.Size = new System.Drawing.Size(1200, 800);
            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            this.pnlBody.ResumeLayout(false);
            this.pnlMainContent.ResumeLayout(false);
            this.tabContent.ResumeLayout(false);
            this.tabOverview.ResumeLayout(false);
            this.tabCurriculum.ResumeLayout(false);
            this.tabReviews.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvReviews)).EndInit();
            this.pnlReviewInput.ResumeLayout(false);
            this.pnlReviewInput.PerformLayout();
            this.pnlSidebar.ResumeLayout(false);
            this.pnlPriceCard.ResumeLayout(false);
            this.pnlPriceCard.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbThumbnail)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
    }
}