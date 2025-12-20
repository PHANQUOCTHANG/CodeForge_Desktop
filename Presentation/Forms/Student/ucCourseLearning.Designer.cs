using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace CodeForge_Desktop.Presentation.Forms.Student.UserControls
{
    partial class ucCourseLearning
    {
        private IContainer components = null;

        private Panel pnlTop;
        private Button btnBack;
        private Button btnPrev;
        private Button btnNext;
        private Button btnMarkCompleted;
        private Label lblCourseLessonInfo;

        private SplitContainer splitMain;
        private TreeView tvModulesLessons;
        private Panel pnlLessonContent;

        private Label lblLessonTitle;
        private RichTextBox rtbLessonText;
        private Panel pnlQuiz;
        private Button btnSubmitQuiz;
        private FlowLayoutPanel flpQuizQuestions;

        private ListView lvCodingProblems;
        private WebBrowser wbVideo; // used for video url preview

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
            this.pnlTop = new System.Windows.Forms.Panel();
            this.btnBack = new System.Windows.Forms.Button();
            this.btnPrev = new System.Windows.Forms.Button();
            this.btnNext = new System.Windows.Forms.Button();
            this.btnMarkCompleted = new System.Windows.Forms.Button();
            this.lblCourseLessonInfo = new System.Windows.Forms.Label();
            this.splitMain = new System.Windows.Forms.SplitContainer();
            this.tvModulesLessons = new System.Windows.Forms.TreeView();
            this.pnlLessonContent = new System.Windows.Forms.Panel();
            this.lblLessonTitle = new System.Windows.Forms.Label();
            this.pnlQuiz = new System.Windows.Forms.Panel();
            this.flpQuizQuestions = new System.Windows.Forms.FlowLayoutPanel();
            this.wbVideo = new System.Windows.Forms.WebBrowser();
            this.rtbLessonText = new System.Windows.Forms.RichTextBox();
            this.btnSubmitQuiz = new System.Windows.Forms.Button();
            this.lvCodingProblems = new System.Windows.Forms.ListView();
            this.pnlTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitMain)).BeginInit();
            this.splitMain.Panel1.SuspendLayout();
            this.splitMain.Panel2.SuspendLayout();
            this.splitMain.SuspendLayout();
            this.pnlLessonContent.SuspendLayout();
            this.pnlQuiz.SuspendLayout();
            this.flpQuizQuestions.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlTop
            // 
            this.pnlTop.BackColor = System.Drawing.Color.WhiteSmoke;
            this.pnlTop.Controls.Add(this.btnBack);
            this.pnlTop.Controls.Add(this.btnPrev);
            this.pnlTop.Controls.Add(this.btnNext);
            this.pnlTop.Controls.Add(this.btnMarkCompleted);
            this.pnlTop.Controls.Add(this.lblCourseLessonInfo);
            this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTop.Location = new System.Drawing.Point(0, 0);
            this.pnlTop.Name = "pnlTop";
            this.pnlTop.Padding = new System.Windows.Forms.Padding(8);
            this.pnlTop.Size = new System.Drawing.Size(980, 48);
            this.pnlTop.TabIndex = 1;
            // 
            // btnBack
            // 
            this.btnBack.Location = new System.Drawing.Point(8, 8);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(92, 23);
            this.btnBack.TabIndex = 0;
            this.btnBack.Text = "← Quay lại ";
            // 
            // btnPrev
            // 
            this.btnPrev.Location = new System.Drawing.Point(112, 8);
            this.btnPrev.Name = "btnPrev";
            this.btnPrev.Size = new System.Drawing.Size(92, 23);
            this.btnPrev.TabIndex = 1;
            this.btnPrev.Text = "◀ Trước";
            // 
            // btnNext
            // 
            this.btnNext.Location = new System.Drawing.Point(212, 8);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(92, 23);
            this.btnNext.TabIndex = 2;
            this.btnNext.Text = "Sau ▶";
            // 
            // btnMarkCompleted
            // 
            this.btnMarkCompleted.BackColor = System.Drawing.Color.White;
            this.btnMarkCompleted.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMarkCompleted.Location = new System.Drawing.Point(320, 8);
            this.btnMarkCompleted.Name = "btnMarkCompleted";
            this.btnMarkCompleted.Size = new System.Drawing.Size(140, 23);
            this.btnMarkCompleted.TabIndex = 3;
            this.btnMarkCompleted.Text = "✔ Mark Completed";
            this.btnMarkCompleted.UseVisualStyleBackColor = false;
            // 
            // lblCourseLessonInfo
            // 
            this.lblCourseLessonInfo.AutoSize = true;
            this.lblCourseLessonInfo.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblCourseLessonInfo.Location = new System.Drawing.Point(480, 14);
            this.lblCourseLessonInfo.Name = "lblCourseLessonInfo";
            this.lblCourseLessonInfo.Size = new System.Drawing.Size(0, 15);
            this.lblCourseLessonInfo.TabIndex = 4;
            // 
            // splitMain
            // 
            this.splitMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitMain.Location = new System.Drawing.Point(0, 48);
            this.splitMain.Name = "splitMain";
            // 
            // splitMain.Panel1
            // 
            this.splitMain.Panel1.Controls.Add(this.tvModulesLessons);
            this.splitMain.Panel1.Padding = new System.Windows.Forms.Padding(8);
            // 
            // splitMain.Panel2
            // 
            this.splitMain.Panel2.Controls.Add(this.pnlLessonContent);
            this.splitMain.Panel2.Padding = new System.Windows.Forms.Padding(12);
            this.splitMain.Size = new System.Drawing.Size(980, 572);
            this.splitMain.SplitterDistance = 497;
            this.splitMain.TabIndex = 0;
            this.splitMain.SplitterMoved += new System.Windows.Forms.SplitterEventHandler(this.splitMain_SplitterMoved);
            // 
            // tvModulesLessons
            // 
            this.tvModulesLessons.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvModulesLessons.FullRowSelect = true;
            this.tvModulesLessons.HideSelection = false;
            this.tvModulesLessons.Location = new System.Drawing.Point(8, 8);
            this.tvModulesLessons.Name = "tvModulesLessons";
            this.tvModulesLessons.Size = new System.Drawing.Size(481, 556);
            this.tvModulesLessons.TabIndex = 0;
            // 
            // pnlLessonContent
            // 
            this.pnlLessonContent.BackColor = System.Drawing.Color.White;
            this.pnlLessonContent.Controls.Add(this.lblLessonTitle);
            this.pnlLessonContent.Controls.Add(this.pnlQuiz);
            this.pnlLessonContent.Controls.Add(this.lvCodingProblems);
            this.pnlLessonContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlLessonContent.Location = new System.Drawing.Point(12, 12);
            this.pnlLessonContent.Name = "pnlLessonContent";
            this.pnlLessonContent.Size = new System.Drawing.Size(455, 548);
            this.pnlLessonContent.TabIndex = 0;
            // 
            // lblLessonTitle
            // 
            this.lblLessonTitle.AutoSize = true;
            this.lblLessonTitle.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblLessonTitle.Location = new System.Drawing.Point(8, 8);
            this.lblLessonTitle.Name = "lblLessonTitle";
            this.lblLessonTitle.Size = new System.Drawing.Size(96, 21);
            this.lblLessonTitle.TabIndex = 0;
            this.lblLessonTitle.Text = "Lesson title";
            // 
            // pnlQuiz
            // 
            this.pnlQuiz.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlQuiz.Controls.Add(this.flpQuizQuestions);
            this.pnlQuiz.Controls.Add(this.btnSubmitQuiz);
            this.pnlQuiz.Location = new System.Drawing.Point(8, 44);
            this.pnlQuiz.Name = "pnlQuiz";
            this.pnlQuiz.Size = new System.Drawing.Size(447, 868);
            this.pnlQuiz.TabIndex = 3;
            // 
            // flpQuizQuestions
            // 
            this.flpQuizQuestions.AutoScroll = true;
            this.flpQuizQuestions.Controls.Add(this.wbVideo);
            this.flpQuizQuestions.Controls.Add(this.rtbLessonText);
            this.flpQuizQuestions.Dock = System.Windows.Forms.DockStyle.Top;
            this.flpQuizQuestions.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flpQuizQuestions.Location = new System.Drawing.Point(0, 0);
            this.flpQuizQuestions.Name = "flpQuizQuestions";
            this.flpQuizQuestions.Size = new System.Drawing.Size(447, 400);
            this.flpQuizQuestions.TabIndex = 0;
            this.flpQuizQuestions.WrapContents = false;
            // 
            // wbVideo
            // 
            this.wbVideo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.wbVideo.Location = new System.Drawing.Point(3, 3);
            this.wbVideo.Name = "wbVideo";
            this.wbVideo.Size = new System.Drawing.Size(0, 420);
            this.wbVideo.TabIndex = 2;
            this.wbVideo.Visible = false;
            // 
            // rtbLessonText
            // 
            this.rtbLessonText.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.rtbLessonText.BackColor = System.Drawing.Color.WhiteSmoke;
            this.rtbLessonText.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.rtbLessonText.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.rtbLessonText.Location = new System.Drawing.Point(3, 429);
            this.rtbLessonText.Name = "rtbLessonText";
            this.rtbLessonText.ReadOnly = true;
            this.rtbLessonText.Size = new System.Drawing.Size(0, 868);
            this.rtbLessonText.TabIndex = 1;
            this.rtbLessonText.Text = "";
            // 
            // btnSubmitQuiz
            // 
            this.btnSubmitQuiz.Location = new System.Drawing.Point(164, 429);
            this.btnSubmitQuiz.Name = "btnSubmitQuiz";
            this.btnSubmitQuiz.Size = new System.Drawing.Size(120, 36);
            this.btnSubmitQuiz.TabIndex = 1;
            this.btnSubmitQuiz.Text = "Nộp bài";
            // 
            // lvCodingProblems
            // 
            this.lvCodingProblems.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lvCodingProblems.HideSelection = false;
            this.lvCodingProblems.Location = new System.Drawing.Point(8, 44);
            this.lvCodingProblems.Name = "lvCodingProblems";
            this.lvCodingProblems.Size = new System.Drawing.Size(875, 868);
            this.lvCodingProblems.TabIndex = 4;
            this.lvCodingProblems.UseCompatibleStateImageBehavior = false;
            this.lvCodingProblems.View = System.Windows.Forms.View.List;
            this.lvCodingProblems.Visible = false;
            // 
            // ucCourseLearning
            // 
            this.BackColor = System.Drawing.SystemColors.Control;
            this.Controls.Add(this.splitMain);
            this.Controls.Add(this.pnlTop);
            this.Name = "ucCourseLearning";
            this.Size = new System.Drawing.Size(980, 620);
            this.pnlTop.ResumeLayout(false);
            this.pnlTop.PerformLayout();
            this.splitMain.Panel1.ResumeLayout(false);
            this.splitMain.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitMain)).EndInit();
            this.splitMain.ResumeLayout(false);
            this.pnlLessonContent.ResumeLayout(false);
            this.pnlLessonContent.PerformLayout();
            this.pnlQuiz.ResumeLayout(false);
            this.flpQuizQuestions.ResumeLayout(false);
            this.ResumeLayout(false);

        }
        #endregion
    }
}
