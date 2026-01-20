namespace CodeForge_Desktop.Presentation.Forms.Student.UserControls
{
    partial class ucCourseDetails
    {
        private System.ComponentModel.IContainer components = null;

        // Header
        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblMeta;
        private System.Windows.Forms.Button btnBack;

        // Body
        private System.Windows.Forms.Panel pnlBody;
        private System.Windows.Forms.Panel pnlMainContent;
        private System.Windows.Forms.Panel pnlSidebar;

        // Tabs
        private System.Windows.Forms.TabControl tabContent;
        private System.Windows.Forms.TabPage tabOverview;
        private System.Windows.Forms.TabPage tabCurriculum;
        private System.Windows.Forms.TabPage tabReviews;

        // Content Controls
        private System.Windows.Forms.WebBrowser wbOverview;
        private System.Windows.Forms.FlowLayoutPanel flpCurriculum;

        // Sidebar Widgets
        private System.Windows.Forms.Panel pnlPriceCard;
        private System.Windows.Forms.PictureBox pbThumbnail;
        private System.Windows.Forms.Label lblPrice;
        private System.Windows.Forms.Button btnEnroll;
        private System.Windows.Forms.Label lblIncludes;
        private System.Windows.Forms.ProgressBar pbProgress;

        // Reviews UI
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
            this.btnBack = new System.Windows.Forms.Button();
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblMeta = new System.Windows.Forms.Label();
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
            this.lblWriteReview = new System.Windows.Forms.Label();
            this.cmbRating = new System.Windows.Forms.ComboBox();
            this.txtReviewComment = new System.Windows.Forms.TextBox();
            this.btnSubmitReview = new System.Windows.Forms.Button();
            this.pnlSidebar = new System.Windows.Forms.Panel();
            this.pnlPriceCard = new System.Windows.Forms.Panel();
            this.pbThumbnail = new System.Windows.Forms.PictureBox();
            this.lblPrice = new System.Windows.Forms.Label();
            this.btnEnroll = new System.Windows.Forms.Button();
            this.pbProgress = new System.Windows.Forms.ProgressBar();
            this.lblIncludes = new System.Windows.Forms.Label();
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

            // pnlHeader
            this.pnlHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(29)))), ((int)(((byte)(31)))));
            this.pnlHeader.Controls.Add(this.lblMeta);
            this.pnlHeader.Controls.Add(this.lblTitle);
            this.pnlHeader.Controls.Add(this.btnBack);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Height = 180;
            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Padding = new System.Windows.Forms.Padding(40, 20, 360, 20);
            this.pnlHeader.Size = new System.Drawing.Size(1200, 180);
            this.pnlHeader.TabIndex = 0;

            // btnBack
            this.btnBack.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnBack.FlatAppearance.BorderSize = 0;
            this.btnBack.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBack.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnBack.ForeColor = System.Drawing.Color.White;
            this.btnBack.Location = new System.Drawing.Point(20, 15);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(100, 30);
            this.btnBack.TabIndex = 0;
            this.btnBack.Text = "← Quay lại";
            this.btnBack.UseVisualStyleBackColor = true;

            // lblTitle
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 22F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(35, 60);
            this.lblTitle.MaximumSize = new System.Drawing.Size(600, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(253, 41);
            this.lblTitle.TabIndex = 1;
            this.lblTitle.Text = "Course Title Here";

            // lblMeta
            this.lblMeta.AutoSize = true;
            this.lblMeta.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblMeta.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(193)))), ((int)(((byte)(75)))));
            this.lblMeta.Location = new System.Drawing.Point(40, 120);
            this.lblMeta.Name = "lblMeta";
            this.lblMeta.Size = new System.Drawing.Size(325, 19);
            this.lblMeta.TabIndex = 2;
            this.lblMeta.Text = "⭐ 4.8  •  1,200 học viên  •  Cập nhật tháng 10/2023";

            // pnlBody
            this.pnlBody.Controls.Add(this.pnlMainContent);
            this.pnlBody.Controls.Add(this.pnlSidebar);
            this.pnlBody.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlBody.Location = new System.Drawing.Point(0, 180);
            this.pnlBody.Name = "pnlBody";
            this.pnlBody.Size = new System.Drawing.Size(1200, 620);
            this.pnlBody.TabIndex = 1;

            // pnlMainContent
            this.pnlMainContent.Controls.Add(this.tabContent);
            this.pnlMainContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMainContent.Location = new System.Drawing.Point(0, 0);
            this.pnlMainContent.Name = "pnlMainContent";
            this.pnlMainContent.Padding = new System.Windows.Forms.Padding(40, 20, 20, 20);
            this.pnlMainContent.Size = new System.Drawing.Size(850, 620);
            this.pnlMainContent.TabIndex = 1;

            // tabContent
            this.tabContent.Controls.Add(this.tabOverview);
            this.tabContent.Controls.Add(this.tabCurriculum);
            this.tabContent.Controls.Add(this.tabReviews);
            this.tabContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabContent.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.tabContent.ItemSize = new System.Drawing.Size(120, 40);
            this.tabContent.Location = new System.Drawing.Point(40, 20);
            this.tabContent.Name = "tabContent";
            this.tabContent.SelectedIndex = 0;
            this.tabContent.Size = new System.Drawing.Size(790, 580);
            this.tabContent.TabIndex = 0;

            // tabOverview
            this.tabOverview.BackColor = System.Drawing.Color.White;
            this.tabOverview.Controls.Add(this.wbOverview);
            this.tabOverview.Location = new System.Drawing.Point(4, 44);
            this.tabOverview.Name = "tabOverview";
            this.tabOverview.Padding = new System.Windows.Forms.Padding(10);
            this.tabOverview.Size = new System.Drawing.Size(782, 532);
            this.tabOverview.TabIndex = 0;
            this.tabOverview.Text = "Giới thiệu";

            // wbOverview
            this.wbOverview.Dock = System.Windows.Forms.DockStyle.Fill;
            this.wbOverview.Location = new System.Drawing.Point(10, 10);
            this.wbOverview.MinimumSize = new System.Drawing.Size(20, 20);
            this.wbOverview.Name = "wbOverview";
            this.wbOverview.Size = new System.Drawing.Size(762, 512);
            this.wbOverview.TabIndex = 0;

            // tabCurriculum
            this.tabCurriculum.BackColor = System.Drawing.Color.White;
            this.tabCurriculum.Controls.Add(this.flpCurriculum);
            this.tabCurriculum.Location = new System.Drawing.Point(4, 44);
            this.tabCurriculum.Name = "tabCurriculum";
            this.tabCurriculum.Size = new System.Drawing.Size(782, 532);
            this.tabCurriculum.TabIndex = 1;
            this.tabCurriculum.Text = "Nội dung";

            // flpCurriculum
            this.flpCurriculum.AutoScroll = true;
            this.flpCurriculum.BackColor = System.Drawing.Color.White;
            this.flpCurriculum.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flpCurriculum.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flpCurriculum.Location = new System.Drawing.Point(0, 0);
            this.flpCurriculum.Name = "flpCurriculum";
            this.flpCurriculum.Padding = new System.Windows.Forms.Padding(0, 10, 0, 20);
            this.flpCurriculum.Size = new System.Drawing.Size(782, 532);
            this.flpCurriculum.TabIndex = 0;
            this.flpCurriculum.WrapContents = false;

            // tabReviews
            this.tabReviews.BackColor = System.Drawing.Color.White;
            this.tabReviews.Controls.Add(this.dgvReviews);
            this.tabReviews.Controls.Add(this.pnlReviewInput);
            this.tabReviews.Location = new System.Drawing.Point(4, 44);
            this.tabReviews.Name = "tabReviews";
            this.tabReviews.Size = new System.Drawing.Size(782, 532);
            this.tabReviews.TabIndex = 2;
            this.tabReviews.Text = "Đánh giá";

            // dgvReviews
            this.dgvReviews.AllowUserToAddRows = false;
            this.dgvReviews.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvReviews.BackgroundColor = System.Drawing.Color.White;
            this.dgvReviews.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvReviews.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvReviews.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvReviews.Location = new System.Drawing.Point(0, 150);
            this.dgvReviews.Name = "dgvReviews";
            this.dgvReviews.RowHeadersVisible = false;
            this.dgvReviews.Size = new System.Drawing.Size(782, 382);
            this.dgvReviews.TabIndex = 1;

            // pnlReviewInput
            this.pnlReviewInput.BackColor = System.Drawing.Color.WhiteSmoke;
            this.pnlReviewInput.Controls.Add(this.btnSubmitReview);
            this.pnlReviewInput.Controls.Add(this.txtReviewComment);
            this.pnlReviewInput.Controls.Add(this.cmbRating);
            this.pnlReviewInput.Controls.Add(this.lblWriteReview);
            this.pnlReviewInput.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlReviewInput.Height = 150;
            this.pnlReviewInput.Location = new System.Drawing.Point(0, 0);
            this.pnlReviewInput.Name = "pnlReviewInput";
            this.pnlReviewInput.Size = new System.Drawing.Size(782, 150);
            this.pnlReviewInput.TabIndex = 0;

            // lblWriteReview
            this.lblWriteReview.AutoSize = true;
            this.lblWriteReview.Location = new System.Drawing.Point(15, 10);
            this.lblWriteReview.Name = "lblWriteReview";
            this.lblWriteReview.Size = new System.Drawing.Size(124, 19);
            this.lblWriteReview.TabIndex = 0;
            this.lblWriteReview.Text = "Đánh giá của bạn";

            // cmbRating
            this.cmbRating.FormattingEnabled = true;
            this.cmbRating.Items.AddRange(new object[] { "5 Sao", "4 Sao", "3 Sao", "2 Sao", "1 Sao" });
            this.cmbRating.Location = new System.Drawing.Point(20, 35);
            this.cmbRating.Name = "cmbRating";
            this.cmbRating.Size = new System.Drawing.Size(100, 25);
            this.cmbRating.TabIndex = 1;

            // txtReviewComment
            this.txtReviewComment.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtReviewComment.Location = new System.Drawing.Point(140, 35);
            this.txtReviewComment.Multiline = true;
            this.txtReviewComment.Name = "txtReviewComment";
            this.txtReviewComment.Size = new System.Drawing.Size(400, 70);
            this.txtReviewComment.TabIndex = 2;

            // btnSubmitReview
            this.btnSubmitReview.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.btnSubmitReview.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSubmitReview.ForeColor = System.Drawing.Color.White;
            this.btnSubmitReview.Location = new System.Drawing.Point(20, 80);
            this.btnSubmitReview.Name = "btnSubmitReview";
            this.btnSubmitReview.Size = new System.Drawing.Size(100, 35);
            this.btnSubmitReview.TabIndex = 3;
            this.btnSubmitReview.Text = "Gửi";
            this.btnSubmitReview.UseVisualStyleBackColor = false;

            // pnlSidebar
            this.pnlSidebar.Controls.Add(this.pnlPriceCard);
            this.pnlSidebar.Dock = System.Windows.Forms.DockStyle.Right;
            this.pnlSidebar.Location = new System.Drawing.Point(850, 0);
            this.pnlSidebar.Name = "pnlSidebar";
            this.pnlSidebar.Padding = new System.Windows.Forms.Padding(10, 0, 20, 20);
            this.pnlSidebar.Size = new System.Drawing.Size(350, 620);
            this.pnlSidebar.TabIndex = 0;

            // pnlPriceCard
            this.pnlPriceCard.BackColor = System.Drawing.Color.White;
            this.pnlPriceCard.Controls.Add(this.lblIncludes);
            this.pnlPriceCard.Controls.Add(this.pbProgress);
            this.pnlPriceCard.Controls.Add(this.btnEnroll);
            this.pnlPriceCard.Controls.Add(this.lblPrice);
            this.pnlPriceCard.Controls.Add(this.pbThumbnail);
            this.pnlPriceCard.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlPriceCard.Height = 500;
            this.pnlPriceCard.Location = new System.Drawing.Point(10, 0);
            this.pnlPriceCard.Name = "pnlPriceCard";
            this.pnlPriceCard.Padding = new System.Windows.Forms.Padding(2);
            this.pnlPriceCard.Size = new System.Drawing.Size(320, 500);
            this.pnlPriceCard.TabIndex = 0;
            // FIX LỖI DESIGNER Ở ĐÂY: Dùng delegate thay vì lambda

            // pbThumbnail
            this.pbThumbnail.BackColor = System.Drawing.Color.Black;
            this.pbThumbnail.Dock = System.Windows.Forms.DockStyle.Top;
            this.pbThumbnail.Location = new System.Drawing.Point(2, 2);
            this.pbThumbnail.Name = "pbThumbnail";
            this.pbThumbnail.Size = new System.Drawing.Size(316, 190);
            this.pbThumbnail.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbThumbnail.TabIndex = 0;
            this.pbThumbnail.TabStop = false;

            // lblPrice
            this.lblPrice.AutoSize = true;
            this.lblPrice.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold);
            this.lblPrice.Location = new System.Drawing.Point(20, 210);
            this.lblPrice.Name = "lblPrice";
            this.lblPrice.Size = new System.Drawing.Size(168, 37);
            this.lblPrice.TabIndex = 1;
            this.lblPrice.Text = "1,299,000 ₫";

            // btnEnroll
            this.btnEnroll.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(164)))), ((int)(((byte)(53)))), ((int)(((byte)(240)))));
            this.btnEnroll.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnEnroll.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEnroll.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnEnroll.ForeColor = System.Drawing.Color.White;
            this.btnEnroll.Location = new System.Drawing.Point(20, 260);
            this.btnEnroll.Name = "btnEnroll";
            this.btnEnroll.Size = new System.Drawing.Size(290, 50);
            this.btnEnroll.TabIndex = 2;
            this.btnEnroll.Text = "Mua ngay";
            this.btnEnroll.UseVisualStyleBackColor = false;

            // pbProgress
            this.pbProgress.Location = new System.Drawing.Point(20, 320);
            this.pbProgress.Name = "pbProgress";
            this.pbProgress.Size = new System.Drawing.Size(290, 8);
            this.pbProgress.TabIndex = 3;
            this.pbProgress.Visible = false;

            // lblIncludes
            this.lblIncludes.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblIncludes.Location = new System.Drawing.Point(20, 340);
            this.lblIncludes.Name = "lblIncludes";
            this.lblIncludes.Size = new System.Drawing.Size(290, 120);
            this.lblIncludes.TabIndex = 4;
            this.lblIncludes.Text = "Khóa học bao gồm:\n✔ Truy cập trọn đời\n✔ Bài tập Coding thực tế\n✔ Chứng chỉ hoàn thành\n✔ Hỗ trợ Q&A";

            // ucCourseDetails
            this.BackColor = System.Drawing.Color.White;
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