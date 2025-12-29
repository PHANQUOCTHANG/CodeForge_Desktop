namespace CodeForge_Desktop.Presentation.Forms.Admin
{
    partial class ImportWordForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.pnlContent = new System.Windows.Forms.Panel();
            this.pnlFileSelection = new System.Windows.Forms.Panel();
            this.txtLessonId = new System.Windows.Forms.TextBox();
            this.lblLessonId = new System.Windows.Forms.Label();
            this.lblSelectedFile = new System.Windows.Forms.Label();
            this.btnSelectFile = new System.Windows.Forms.Button();
            this.lblFileLabel = new System.Windows.Forms.Label();
            this.rtbLog = new System.Windows.Forms.RichTextBox();
            this.lblLogLabel = new System.Windows.Forms.Label();
            this.pnlFooter = new System.Windows.Forms.Panel();
            this.btnClearLog = new System.Windows.Forms.Button();
            this.btnImport = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.pnlHeader.SuspendLayout();
            this.pnlContent.SuspendLayout();
            this.pnlFileSelection.SuspendLayout();
            this.pnlFooter.SuspendLayout();
            this.SuspendLayout();

            // pnlHeader
            this.pnlHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(120)))), ((int)(((byte)(215)))));
            this.pnlHeader.Controls.Add(this.lblTitle);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size = new System.Drawing.Size(900, 60);
            this.pnlHeader.TabIndex = 0;

            // lblTitle
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(20, 15);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(380, 30);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "📥 Import Bài Lập Trình từ Word";

            // pnlContent
            this.pnlContent.BackColor = System.Drawing.Color.White;
            this.pnlContent.Controls.Add(this.rtbLog);
            this.pnlContent.Controls.Add(this.lblLogLabel);
            this.pnlContent.Controls.Add(this.pnlFileSelection);
            this.pnlContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlContent.Location = new System.Drawing.Point(0, 60);
            this.pnlContent.Name = "pnlContent";
            this.pnlContent.Padding = new System.Windows.Forms.Padding(15);
            this.pnlContent.Size = new System.Drawing.Size(900, 530);
            this.pnlContent.TabIndex = 1;

            // pnlFileSelection
            this.pnlFileSelection.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlFileSelection.Controls.Add(this.lblFileLabel);
            this.pnlFileSelection.Controls.Add(this.btnSelectFile);
            this.pnlFileSelection.Controls.Add(this.lblSelectedFile);
            this.pnlFileSelection.Controls.Add(this.lblLessonId);
            this.pnlFileSelection.Controls.Add(this.txtLessonId);
            this.pnlFileSelection.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlFileSelection.Location = new System.Drawing.Point(15, 15);
            this.pnlFileSelection.Name = "pnlFileSelection";
            this.pnlFileSelection.Padding = new System.Windows.Forms.Padding(15);
            this.pnlFileSelection.Size = new System.Drawing.Size(870, 110);
            this.pnlFileSelection.TabIndex = 0;

            // lblFileLabel
            this.lblFileLabel.AutoSize = true;
            this.lblFileLabel.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.lblFileLabel.Location = new System.Drawing.Point(15, 15);
            this.lblFileLabel.Name = "lblFileLabel";
            this.lblFileLabel.Size = new System.Drawing.Size(68, 19);
            this.lblFileLabel.TabIndex = 0;
            this.lblFileLabel.Text = "File Word:";

            // btnSelectFile
            this.btnSelectFile.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(120)))), ((int)(((byte)(215)))));
            this.btnSelectFile.FlatAppearance.BorderSize = 0;
            this.btnSelectFile.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSelectFile.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnSelectFile.ForeColor = System.Drawing.Color.White;
            this.btnSelectFile.Location = new System.Drawing.Point(15, 37);
            this.btnSelectFile.Name = "btnSelectFile";
            this.btnSelectFile.Size = new System.Drawing.Size(100, 35);
            this.btnSelectFile.TabIndex = 1;
            this.btnSelectFile.Text = "📁 Chọn File";
            this.btnSelectFile.UseVisualStyleBackColor = false;

            // lblSelectedFile
            this.lblSelectedFile.AutoSize = true;
            this.lblSelectedFile.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblSelectedFile.ForeColor = System.Drawing.Color.Gray;
            this.lblSelectedFile.Location = new System.Drawing.Point(125, 47);
            this.lblSelectedFile.Name = "lblSelectedFile";
            this.lblSelectedFile.Size = new System.Drawing.Size(70, 15);
            this.lblSelectedFile.TabIndex = 2;
            this.lblSelectedFile.Text = "Chưa chọn file";

            // lblLessonId
            this.lblLessonId.AutoSize = true;
            this.lblLessonId.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.lblLessonId.Location = new System.Drawing.Point(450, 15);
            this.lblLessonId.Name = "lblLessonId";
            this.lblLessonId.Size = new System.Drawing.Size(80, 19);
            this.lblLessonId.TabIndex = 3;
            this.lblLessonId.Text = "Lesson ID:";

            // txtLessonId
            this.txtLessonId.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtLessonId.Location = new System.Drawing.Point(450, 37);
            this.txtLessonId.Name = "txtLessonId";
            this.txtLessonId.Size = new System.Drawing.Size(350, 25);
            this.txtLessonId.TabIndex = 4;
            this.txtLessonId.Text = "";

            // lblLogLabel
            this.lblLogLabel.AutoSize = true;
            this.lblLogLabel.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.lblLogLabel.Location = new System.Drawing.Point(15, 135);
            this.lblLogLabel.Name = "lblLogLabel";
            this.lblLogLabel.Size = new System.Drawing.Size(38, 19);
            this.lblLogLabel.TabIndex = 1;
            this.lblLogLabel.Text = "Log:";

            // rtbLog
            this.rtbLog.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))));
            this.rtbLog.Font = new System.Drawing.Font("Courier New", 9.75F);
            this.rtbLog.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.rtbLog.Location = new System.Drawing.Point(15, 160);
            this.rtbLog.Name = "rtbLog";
            this.rtbLog.ReadOnly = true;
            this.rtbLog.Size = new System.Drawing.Size(870, 330);
            this.rtbLog.TabIndex = 2;
            this.rtbLog.Text = "";

            // pnlFooter
            this.pnlFooter.BackColor = System.Drawing.Color.WhiteSmoke;
            this.pnlFooter.Controls.Add(this.btnClose);
            this.pnlFooter.Controls.Add(this.btnImport);
            this.pnlFooter.Controls.Add(this.btnClearLog);
            this.pnlFooter.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlFooter.Location = new System.Drawing.Point(0, 590);
            this.pnlFooter.Name = "pnlFooter";
            this.pnlFooter.Size = new System.Drawing.Size(900, 60);
            this.pnlFooter.TabIndex = 2;

            // btnClearLog
            this.btnClearLog.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClearLog.BackColor = System.Drawing.Color.Orange;
            this.btnClearLog.FlatAppearance.BorderSize = 0;
            this.btnClearLog.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClearLog.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnClearLog.ForeColor = System.Drawing.Color.White;
            this.btnClearLog.Location = new System.Drawing.Point(620, 12);
            this.btnClearLog.Name = "btnClearLog";
            this.btnClearLog.Size = new System.Drawing.Size(100, 35);
            this.btnClearLog.TabIndex = 1;
            this.btnClearLog.Text = "🗑 Xóa Log";
            this.btnClearLog.UseVisualStyleBackColor = false;

            // btnImport
            this.btnImport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnImport.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(167)))), ((int)(((byte)(69)))));
            this.btnImport.Enabled = false;
            this.btnImport.FlatAppearance.BorderSize = 0;
            this.btnImport.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnImport.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnImport.ForeColor = System.Drawing.Color.White;
            this.btnImport.Location = new System.Drawing.Point(726, 12);
            this.btnImport.Name = "btnImport";
            this.btnImport.Size = new System.Drawing.Size(100, 35);
            this.btnImport.TabIndex = 0;
            this.btnImport.Text = "📥 Import";
            this.btnImport.UseVisualStyleBackColor = false;

            // btnClose
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.BackColor = System.Drawing.Color.Gray;
            this.btnClose.FlatAppearance.BorderSize = 0;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnClose.ForeColor = System.Drawing.Color.White;
            this.btnClose.Location = new System.Drawing.Point(832, 12);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(56, 35);
            this.btnClose.TabIndex = 2;
            this.btnClose.Text = "✕";
            this.btnClose.UseVisualStyleBackColor = false;

            // ImportWordForm
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(900, 650);
            this.Controls.Add(this.pnlContent);
            this.Controls.Add(this.pnlFooter);
            this.Controls.Add(this.pnlHeader);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ImportWordForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Import Word";
            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            this.pnlContent.ResumeLayout(false);
            this.pnlContent.PerformLayout();
            this.pnlFileSelection.ResumeLayout(false);
            this.pnlFileSelection.PerformLayout();
            this.pnlFooter.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Panel pnlContent;
        private System.Windows.Forms.Panel pnlFileSelection;
        private System.Windows.Forms.Label lblFileLabel;
        private System.Windows.Forms.Button btnSelectFile;
        private System.Windows.Forms.Label lblSelectedFile;
        private System.Windows.Forms.Label lblLessonId;
        private System.Windows.Forms.TextBox txtLessonId;
        private System.Windows.Forms.Label lblLogLabel;
        private System.Windows.Forms.RichTextBox rtbLog;
        private System.Windows.Forms.Panel pnlFooter;
        private System.Windows.Forms.Button btnClearLog;
        private System.Windows.Forms.Button btnImport;
        private System.Windows.Forms.Button btnClose;
    }
}