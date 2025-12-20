using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;


namespace CodeForge_Desktop.Presentation.Forms.Student.UserControls
{
    partial class ucCourseDetails
    {
        private IContainer components = null;

        private Panel pnlHeader;
        private Button btnBack;
        private PictureBox pbThumb;
        private Label lblCourseTitle;
        private Label lblInstructor;
        private Label lblMetaSmall; // rating | students | duration
        private Button btnEnrollStart;
        private ProgressBar pbCourseProgress;

        private TabControl tabCourse;
        private TabPage tabOverview;
        private TabPage tabCurriculum;
        private TabPage tabReviews;

        private RichTextBox rtbOverview;
        private TreeView tvCurriculum;
        private DataGridView dgvReviews;

        // REVIEW input controls (added)
        private Panel pnlReviewInput;
        private Label lblYourRating;
        private ComboBox cbRating;
        private Label lblYourComment;
        private TextBox txtComment;
        private Button btnSubmitReview;
        private Label lblReviewHint;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Designer generated
        private void InitializeComponent()
        {
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.btnBack = new System.Windows.Forms.Button();
            this.pbThumb = new System.Windows.Forms.PictureBox();
            this.lblCourseTitle = new System.Windows.Forms.Label();
            this.lblInstructor = new System.Windows.Forms.Label();
            this.lblMetaSmall = new System.Windows.Forms.Label();
            this.btnEnrollStart = new System.Windows.Forms.Button();
            this.pbCourseProgress = new System.Windows.Forms.ProgressBar();
            this.tabCourse = new System.Windows.Forms.TabControl();
            this.tabOverview = new System.Windows.Forms.TabPage();
            this.rtbOverview = new System.Windows.Forms.RichTextBox();
            this.tabCurriculum = new System.Windows.Forms.TabPage();
            this.tvCurriculum = new System.Windows.Forms.TreeView();
            this.tabReviews = new System.Windows.Forms.TabPage();
            this.pnlReviewInput = new System.Windows.Forms.Panel();
            this.lblYourRating = new System.Windows.Forms.Label();
            this.cbRating = new System.Windows.Forms.ComboBox();
            this.lblYourComment = new System.Windows.Forms.Label();
            this.txtComment = new System.Windows.Forms.TextBox();
            this.btnSubmitReview = new System.Windows.Forms.Button();
            this.lblReviewHint = new System.Windows.Forms.Label();
            this.dgvReviews = new System.Windows.Forms.DataGridView();
            this.pnlHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbThumb)).BeginInit();
            this.tabCourse.SuspendLayout();
            this.tabOverview.SuspendLayout();
            this.tabCurriculum.SuspendLayout();
            this.tabReviews.SuspendLayout();
            this.pnlReviewInput.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvReviews)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlHeader
            // 
            this.pnlHeader.BackColor = System.Drawing.Color.WhiteSmoke;
            this.pnlHeader.Controls.Add(this.btnBack);
            this.pnlHeader.Controls.Add(this.pbThumb);
            this.pnlHeader.Controls.Add(this.lblCourseTitle);
            this.pnlHeader.Controls.Add(this.lblInstructor);
            this.pnlHeader.Controls.Add(this.lblMetaSmall);
            this.pnlHeader.Controls.Add(this.pbCourseProgress);
            this.pnlHeader.Controls.Add(this.btnEnrollStart);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Padding = new System.Windows.Forms.Padding(12);
            this.pnlHeader.Size = new System.Drawing.Size(980, 225);
            this.pnlHeader.TabIndex = 1;
            // 
            // btnBack
            // 
            this.btnBack.Location = new System.Drawing.Point(12, 12);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(88, 32);
            this.btnBack.TabIndex = 0;
            this.btnBack.Text = "← Quay lại";
            // 
            // pbThumb
            // 
            this.pbThumb.BackColor = System.Drawing.Color.Transparent;
            this.pbThumb.Location = new System.Drawing.Point(61, 50);
            this.pbThumb.Name = "pbThumb";
            this.pbThumb.Size = new System.Drawing.Size(345, 146);
            this.pbThumb.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbThumb.TabIndex = 1;
            this.pbThumb.TabStop = false;
            // 
            // lblCourseTitle
            // 
            this.lblCourseTitle.AutoSize = true;
            this.lblCourseTitle.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblCourseTitle.Location = new System.Drawing.Point(615, 42);
            this.lblCourseTitle.Name = "lblCourseTitle";
            this.lblCourseTitle.Size = new System.Drawing.Size(121, 28);
            this.lblCourseTitle.TabIndex = 2;
            this.lblCourseTitle.Text = "Course title";
            // 
            // lblInstructor
            // 
            this.lblInstructor.AutoSize = true;
            this.lblInstructor.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblInstructor.ForeColor = System.Drawing.Color.Gray;
            this.lblInstructor.Location = new System.Drawing.Point(616, 84);
            this.lblInstructor.Name = "lblInstructor";
            this.lblInstructor.Size = new System.Drawing.Size(92, 20);
            this.lblInstructor.TabIndex = 3;
            this.lblInstructor.Text = "Giảng viên: -";
            // 
            // lblMetaSmall
            // 
            this.lblMetaSmall.AutoSize = true;
            this.lblMetaSmall.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblMetaSmall.ForeColor = System.Drawing.Color.Black;
            this.lblMetaSmall.Location = new System.Drawing.Point(616, 120);
            this.lblMetaSmall.Name = "lblMetaSmall";
            this.lblMetaSmall.Size = new System.Drawing.Size(206, 20);
            this.lblMetaSmall.TabIndex = 4;
            this.lblMetaSmall.Text = "⭐ 0.0   •   0 học viên   •   0 giờ";
            // 
            // pbCourseProgress
            // 
            this.pbCourseProgress.Location = new System.Drawing.Point(620, 136);
            this.pbCourseProgress.Name = "pbCourseProgress";
            this.pbCourseProgress.Size = new System.Drawing.Size(260, 12);
            this.pbCourseProgress.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.pbCourseProgress.TabIndex = 6;
            this.pbCourseProgress.Visible = false;
            // 
            // btnEnrollStart
            // 
            this.btnEnrollStart.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEnrollStart.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnEnrollStart.Location = new System.Drawing.Point(620, 156);
            this.btnEnrollStart.Name = "btnEnrollStart";
            this.btnEnrollStart.Size = new System.Drawing.Size(260, 40);
            this.btnEnrollStart.TabIndex = 5;
            this.btnEnrollStart.Text = "Đăng ký / Bắt đầu học";
            // 
            // tabCourse
            // 
            this.tabCourse.Controls.Add(this.tabOverview);
            this.tabCourse.Controls.Add(this.tabCurriculum);
            this.tabCourse.Controls.Add(this.tabReviews);
            this.tabCourse.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabCourse.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.tabCourse.Location = new System.Drawing.Point(0, 225);
            this.tabCourse.Name = "tabCourse";
            this.tabCourse.SelectedIndex = 0;
            this.tabCourse.Size = new System.Drawing.Size(980, 395);
            this.tabCourse.TabIndex = 0;
            // 
            // tabOverview
            // 
            this.tabOverview.Controls.Add(this.rtbOverview);
            this.tabOverview.Location = new System.Drawing.Point(4, 29);
            this.tabOverview.Name = "tabOverview";
            this.tabOverview.Padding = new System.Windows.Forms.Padding(12);
            this.tabOverview.Size = new System.Drawing.Size(972, 362);
            this.tabOverview.TabIndex = 0;
            this.tabOverview.Text = "Overview";
            // 
            // rtbOverview
            // 
            this.rtbOverview.BackColor = System.Drawing.Color.WhiteSmoke;
            this.rtbOverview.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.rtbOverview.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtbOverview.Location = new System.Drawing.Point(12, 12);
            this.rtbOverview.Name = "rtbOverview";
            this.rtbOverview.ReadOnly = true;
            this.rtbOverview.Size = new System.Drawing.Size(948, 338);
            this.rtbOverview.TabIndex = 0;
            this.rtbOverview.Text = "";
            // 
            // tabCurriculum
            // 
            this.tabCurriculum.Controls.Add(this.tvCurriculum);
            this.tabCurriculum.Location = new System.Drawing.Point(4, 29);
            this.tabCurriculum.Name = "tabCurriculum";
            this.tabCurriculum.Padding = new System.Windows.Forms.Padding(12);
            this.tabCurriculum.Size = new System.Drawing.Size(972, 362);
            this.tabCurriculum.TabIndex = 1;
            this.tabCurriculum.Text = "Curriculum";
            // 
            // tvCurriculum
            // 
            this.tvCurriculum.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tvCurriculum.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvCurriculum.Location = new System.Drawing.Point(12, 12);
            this.tvCurriculum.Name = "tvCurriculum";
            this.tvCurriculum.Size = new System.Drawing.Size(948, 338);
            this.tvCurriculum.TabIndex = 0;
            // 
            // tabReviews
            // 
            this.tabReviews.Controls.Add(this.dgvReviews);
            // Add the review input panel on top of the reviews grid
            this.tabReviews.Controls.Add(this.pnlReviewInput);
            this.tabReviews.Location = new System.Drawing.Point(4, 29);
            this.tabReviews.Name = "tabReviews";
            this.tabReviews.Padding = new System.Windows.Forms.Padding(12);
            this.tabReviews.Size = new System.Drawing.Size(972, 362);
            this.tabReviews.TabIndex = 2;
            this.tabReviews.Text = "Reviews";
            // 
            // pnlReviewInput
            // 
            this.pnlReviewInput.BackColor = System.Drawing.Color.Transparent;
            this.pnlReviewInput.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlReviewInput.Location = new System.Drawing.Point(12, 12);
            this.pnlReviewInput.Name = "pnlReviewInput";
            this.pnlReviewInput.Size = new System.Drawing.Size(948, 120);
            this.pnlReviewInput.TabIndex = 1;
            // 
            // lblYourRating
            // 
            this.lblYourRating.AutoSize = true;
            this.lblYourRating.Location = new System.Drawing.Point(6, 8);
            this.lblYourRating.Name = "lblYourRating";
            this.lblYourRating.Size = new System.Drawing.Size(90, 20);
            this.lblYourRating.TabIndex = 0;
            this.lblYourRating.Text = "Đánh giá của bạn:";
            // 
            // cbRating
            // 
            this.cbRating.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbRating.FormattingEnabled = true;
            this.cbRating.Items.AddRange(new object[] {
            "5",
            "4",
            "3",
            "2",
            "1"});
            this.cbRating.Location = new System.Drawing.Point(12, 32);
            this.cbRating.Name = "cbRating";
            this.cbRating.Size = new System.Drawing.Size(80, 28);
            this.cbRating.TabIndex = 1;
            // 
            // lblYourComment
            // 
            this.lblYourComment.AutoSize = true;
            this.lblYourComment.Location = new System.Drawing.Point(110, 8);
            this.lblYourComment.Name = "lblYourComment";
            this.lblYourComment.Size = new System.Drawing.Size(66, 20);
            this.lblYourComment.TabIndex = 2;
            this.lblYourComment.Text = "Bình luận:";
            // 
            // txtComment
            // 
            this.txtComment.Location = new System.Drawing.Point(114, 32);
            this.txtComment.Multiline = true;
            this.txtComment.Name = "txtComment";
            this.txtComment.Size = new System.Drawing.Size(580, 72);
            this.txtComment.TabIndex = 3;
            // 
            // btnSubmitReview
            // 
            this.btnSubmitReview.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSubmitReview.Location = new System.Drawing.Point(708, 32);
            this.btnSubmitReview.Name = "btnSubmitReview";
            this.btnSubmitReview.Size = new System.Drawing.Size(120, 36);
            this.btnSubmitReview.TabIndex = 4;
            this.btnSubmitReview.Text = "Gửi đánh giá";
            // 
            // lblReviewHint
            // 
            this.lblReviewHint.AutoSize = true;
            this.lblReviewHint.ForeColor = System.Drawing.Color.Gray;
            this.lblReviewHint.Location = new System.Drawing.Point(708, 76);
            this.lblReviewHint.Name = "lblReviewHint";
            this.lblReviewHint.Size = new System.Drawing.Size(132, 20);
            this.lblReviewHint.TabIndex = 5;
            this.lblReviewHint.Text = "Bạn phải đăng ký để đánh giá.";
            // 
            // add review controls into panel
            // 
            this.pnlReviewInput.Controls.Add(this.lblYourRating);
            this.pnlReviewInput.Controls.Add(this.cbRating);
            this.pnlReviewInput.Controls.Add(this.lblYourComment);
            this.pnlReviewInput.Controls.Add(this.txtComment);
            this.pnlReviewInput.Controls.Add(this.btnSubmitReview);
            this.pnlReviewInput.Controls.Add(this.lblReviewHint);
            // 
            // dgvReviews
            // 
            this.dgvReviews.AllowUserToAddRows = false;
            this.dgvReviews.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvReviews.ColumnHeadersHeight = 29;
            this.dgvReviews.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvReviews.Location = new System.Drawing.Point(12, 132);
            this.dgvReviews.Name = "dgvReviews";
            this.dgvReviews.ReadOnly = true;
            this.dgvReviews.RowHeadersVisible = false;
            this.dgvReviews.RowHeadersWidth = 51;
            this.dgvReviews.Size = new System.Drawing.Size(948, 218);
            this.dgvReviews.TabIndex = 0;
            // 
            // ucCourseDetails
            // 
            this.BackColor = System.Drawing.SystemColors.Control;
            this.Controls.Add(this.tabCourse);
            this.Controls.Add(this.pnlHeader);
            this.Name = "ucCourseDetails";
            this.Size = new System.Drawing.Size(980, 620);
            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbThumb)).EndInit();
            this.tabCourse.ResumeLayout(false);
            this.tabOverview.ResumeLayout(false);
            this.tabCurriculum.ResumeLayout(false);
            this.tabReviews.ResumeLayout(false);
            this.pnlReviewInput.ResumeLayout(false);
            this.pnlReviewInput.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvReviews)).EndInit();
            this.ResumeLayout(false);

        }
        #endregion
    }
}