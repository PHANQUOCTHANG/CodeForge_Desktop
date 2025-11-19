namespace CodeForge_Desktop.Presentation.Forms.Student
{
    partial class ucProblemDetail
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

        #region Component Designer generated code

        private void InitializeComponent()
        {
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.lblDeadline = new System.Windows.Forms.Label();
            this.lblProblemTitle = new System.Windows.Forms.Label();
            this.btnBack = new System.Windows.Forms.Button();
            this.pnlToolbar = new System.Windows.Forms.Panel();
            this.lblLanguage = new System.Windows.Forms.Label();
            this.btnSubmit = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnRun = new System.Windows.Forms.Button();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.flowDescription = new System.Windows.Forms.FlowLayoutPanel();
            this.pnlEditorContainer = new System.Windows.Forms.Panel();
            this.pnlOutput = new System.Windows.Forms.Panel();
            this.txtConsole = new System.Windows.Forms.TextBox();
            this.panelTabs = new System.Windows.Forms.Panel();
            this.lblTabOutput = new System.Windows.Forms.Label();
            this.lblTabConsole = new System.Windows.Forms.Label();
            this.pnlCodeArea = new System.Windows.Forms.Panel();
            this.rtbCodeEditor = new System.Windows.Forms.RichTextBox();
            this.pnlEditorHeader = new System.Windows.Forms.Panel();
            this.lblFileName = new System.Windows.Forms.Label();
            this.lblEditorTitle = new System.Windows.Forms.Label();
            this.pnlHeader.SuspendLayout();
            this.pnlToolbar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.pnlEditorContainer.SuspendLayout();
            this.pnlOutput.SuspendLayout();
            this.panelTabs.SuspendLayout();
            this.pnlCodeArea.SuspendLayout();
            this.pnlEditorHeader.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlHeader
            // 
            this.pnlHeader.BackColor = System.Drawing.Color.White;
            this.pnlHeader.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlHeader.Controls.Add(this.lblDeadline);
            this.pnlHeader.Controls.Add(this.lblProblemTitle);
            this.pnlHeader.Controls.Add(this.btnBack);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Padding = new System.Windows.Forms.Padding(10);
            this.pnlHeader.Size = new System.Drawing.Size(1200, 50);
            this.pnlHeader.TabIndex = 0;
            // 
            // lblDeadline
            // 
            this.lblDeadline.Dock = System.Windows.Forms.DockStyle.Right;
            this.lblDeadline.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblDeadline.ForeColor = System.Drawing.Color.Gray;
            this.lblDeadline.Location = new System.Drawing.Point(988, 10);
            this.lblDeadline.Name = "lblDeadline";
            this.lblDeadline.Size = new System.Drawing.Size(200, 28);
            this.lblDeadline.TabIndex = 2;
            this.lblDeadline.Text = "Deadline: 2025-11-20";
            this.lblDeadline.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblProblemTitle
            // 
            this.lblProblemTitle.AutoSize = true;
            this.lblProblemTitle.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblProblemTitle.Location = new System.Drawing.Point(130, 14);
            this.lblProblemTitle.Name = "lblProblemTitle";
            this.lblProblemTitle.Size = new System.Drawing.Size(151, 21);
            this.lblProblemTitle.TabIndex = 1;
            this.lblProblemTitle.Text = "Bài 1: Hello World";
            // 
            // btnBack
            // 
            this.btnBack.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnBack.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBack.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnBack.Location = new System.Drawing.Point(10, 10);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(100, 28);
            this.btnBack.TabIndex = 0;
            this.btnBack.Text = "← Quay lại";
            this.btnBack.UseVisualStyleBackColor = true;
            // 
            // pnlToolbar
            // 
            this.pnlToolbar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.pnlToolbar.Controls.Add(this.lblLanguage);
            this.pnlToolbar.Controls.Add(this.btnSubmit);
            this.pnlToolbar.Controls.Add(this.btnSave);
            this.pnlToolbar.Controls.Add(this.btnRun);
            this.pnlToolbar.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlToolbar.Location = new System.Drawing.Point(0, 50);
            this.pnlToolbar.Name = "pnlToolbar";
            this.pnlToolbar.Padding = new System.Windows.Forms.Padding(10, 5, 10, 5);
            this.pnlToolbar.Size = new System.Drawing.Size(1200, 45);
            this.pnlToolbar.TabIndex = 1;
            // 
            // lblLanguage
            // 
            this.lblLanguage.AutoSize = true;
            this.lblLanguage.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblLanguage.ForeColor = System.Drawing.Color.Gray;
            this.lblLanguage.Location = new System.Drawing.Point(320, 15);
            this.lblLanguage.Name = "lblLanguage";
            this.lblLanguage.Size = new System.Drawing.Size(112, 15);
            this.lblLanguage.TabIndex = 3;
            this.lblLanguage.Text = "Language: JavaScript";
            // 
            // btnSubmit
            // 
            this.btnSubmit.BackColor = System.Drawing.Color.White;
            this.btnSubmit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSubmit.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.btnSubmit.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(140)))), ((int)(((byte)(0)))));
            this.btnSubmit.Location = new System.Drawing.Point(210, 8);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(90, 30);
            this.btnSubmit.TabIndex = 2;
            this.btnSubmit.Text = "🚀 Submit";
            this.btnSubmit.UseVisualStyleBackColor = false;
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.White;
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.btnSave.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(120)))), ((int)(((byte)(215)))));
            this.btnSave.Location = new System.Drawing.Point(110, 8);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(90, 30);
            this.btnSave.TabIndex = 1;
            this.btnSave.Text = "💾 Save";
            this.btnSave.UseVisualStyleBackColor = false;
            // 
            // btnRun
            // 
            this.btnRun.BackColor = System.Drawing.Color.White;
            this.btnRun.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRun.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.btnRun.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(167)))), ((int)(((byte)(69)))));
            this.btnRun.Location = new System.Drawing.Point(10, 8);
            this.btnRun.Name = "btnRun";
            this.btnRun.Size = new System.Drawing.Size(90, 30);
            this.btnRun.TabIndex = 0;
            this.btnRun.Text = "▶ Run";
            this.btnRun.UseVisualStyleBackColor = false;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 95);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.BackColor = System.Drawing.Color.White;
            this.splitContainer1.Panel1.Controls.Add(this.flowDescription);
            this.splitContainer1.Panel1.Padding = new System.Windows.Forms.Padding(15);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.pnlEditorContainer);
            this.splitContainer1.Size = new System.Drawing.Size(1200, 705);
            this.splitContainer1.SplitterDistance = 400;
            this.splitContainer1.TabIndex = 2;
            // 
            // flowDescription
            // 
            this.flowDescription.AutoScroll = true;
            this.flowDescription.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowDescription.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowDescription.Location = new System.Drawing.Point(15, 15);
            this.flowDescription.Name = "flowDescription";
            this.flowDescription.Size = new System.Drawing.Size(370, 675);
            this.flowDescription.TabIndex = 0;
            this.flowDescription.WrapContents = false;
            // 
            // pnlEditorContainer
            // 
            this.pnlEditorContainer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.pnlEditorContainer.Controls.Add(this.pnlOutput);
            this.pnlEditorContainer.Controls.Add(this.pnlCodeArea);
            this.pnlEditorContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlEditorContainer.Location = new System.Drawing.Point(0, 0);
            this.pnlEditorContainer.Name = "pnlEditorContainer";
            this.pnlEditorContainer.Padding = new System.Windows.Forms.Padding(0, 0, 0, 5);
            this.pnlEditorContainer.Size = new System.Drawing.Size(796, 705);
            this.pnlEditorContainer.TabIndex = 0;
            // 
            // pnlOutput
            // 
            this.pnlOutput.BackColor = System.Drawing.Color.WhiteSmoke;
            this.pnlOutput.Controls.Add(this.txtConsole);
            this.pnlOutput.Controls.Add(this.panelTabs);
            this.pnlOutput.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlOutput.Location = new System.Drawing.Point(0, 505);
            this.pnlOutput.Name = "pnlOutput";
            this.pnlOutput.Size = new System.Drawing.Size(796, 195);
            this.pnlOutput.TabIndex = 1;
            // 
            // txtConsole
            // 
            this.txtConsole.BackColor = System.Drawing.Color.White;
            this.txtConsole.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtConsole.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtConsole.Font = new System.Drawing.Font("Consolas", 10F);
            this.txtConsole.Location = new System.Drawing.Point(0, 30);
            this.txtConsole.Multiline = true;
            this.txtConsole.Name = "txtConsole";
            this.txtConsole.ReadOnly = true;
            this.txtConsole.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtConsole.Size = new System.Drawing.Size(796, 165);
            this.txtConsole.TabIndex = 1;
            this.txtConsole.Text = "Nhấn \"Run\" để chạy code...";
            // 
            // panelTabs
            // 
            this.panelTabs.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.panelTabs.Controls.Add(this.lblTabOutput);
            this.panelTabs.Controls.Add(this.lblTabConsole);
            this.panelTabs.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTabs.Location = new System.Drawing.Point(0, 0);
            this.panelTabs.Name = "panelTabs";
            this.panelTabs.Size = new System.Drawing.Size(796, 30);
            this.panelTabs.TabIndex = 0;
            // 
            // lblTabOutput
            // 
            this.lblTabOutput.BackColor = System.Drawing.Color.White;
            this.lblTabOutput.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblTabOutput.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblTabOutput.Location = new System.Drawing.Point(80, 0);
            this.lblTabOutput.Name = "lblTabOutput";
            this.lblTabOutput.Size = new System.Drawing.Size(80, 30);
            this.lblTabOutput.TabIndex = 1;
            this.lblTabOutput.Text = "Output";
            this.lblTabOutput.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblTabConsole
            // 
            this.lblTabConsole.BackColor = System.Drawing.Color.WhiteSmoke;
            this.lblTabConsole.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblTabConsole.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblTabConsole.ForeColor = System.Drawing.Color.Gray;
            this.lblTabConsole.Location = new System.Drawing.Point(0, 0);
            this.lblTabConsole.Name = "lblTabConsole";
            this.lblTabConsole.Size = new System.Drawing.Size(80, 30);
            this.lblTabConsole.TabIndex = 0;
            this.lblTabConsole.Text = "Console";
            this.lblTabConsole.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnlCodeArea
            // 
            this.pnlCodeArea.Controls.Add(this.rtbCodeEditor);
            this.pnlCodeArea.Controls.Add(this.pnlEditorHeader);
            this.pnlCodeArea.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlCodeArea.Location = new System.Drawing.Point(0, 0);
            this.pnlCodeArea.Name = "pnlCodeArea";
            this.pnlCodeArea.Size = new System.Drawing.Size(796, 700);
            this.pnlCodeArea.TabIndex = 0;
            // 
            // rtbCodeEditor
            // 
            this.rtbCodeEditor.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.rtbCodeEditor.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.rtbCodeEditor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtbCodeEditor.Font = new System.Drawing.Font("Consolas", 11F);
            this.rtbCodeEditor.ForeColor = System.Drawing.Color.White;
            this.rtbCodeEditor.Location = new System.Drawing.Point(0, 30);
            this.rtbCodeEditor.Name = "rtbCodeEditor";
            this.rtbCodeEditor.Size = new System.Drawing.Size(796, 670);
            this.rtbCodeEditor.TabIndex = 1;
            this.rtbCodeEditor.Text = "// Viết code của bạn ở đây\n\nfunction solution() {\n    // TODO: Implement your solution\n}\n\nsolution();";
            // 
            // pnlEditorHeader
            // 
            this.pnlEditorHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
            this.pnlEditorHeader.Controls.Add(this.lblFileName);
            this.pnlEditorHeader.Controls.Add(this.lblEditorTitle);
            this.pnlEditorHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlEditorHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlEditorHeader.Name = "pnlEditorHeader";
            this.pnlEditorHeader.Size = new System.Drawing.Size(796, 30);
            this.pnlEditorHeader.TabIndex = 0;
            // 
            // lblFileName
            // 
            this.lblFileName.Dock = System.Windows.Forms.DockStyle.Right;
            this.lblFileName.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Italic);
            this.lblFileName.ForeColor = System.Drawing.Color.Gray;
            this.lblFileName.Location = new System.Drawing.Point(696, 0);
            this.lblFileName.Name = "lblFileName";
            this.lblFileName.Padding = new System.Windows.Forms.Padding(0, 0, 10, 0);
            this.lblFileName.Size = new System.Drawing.Size(100, 30);
            this.lblFileName.TabIndex = 1;
            this.lblFileName.Text = "main.js";
            this.lblFileName.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblEditorTitle
            // 
            this.lblEditorTitle.AutoSize = true;
            this.lblEditorTitle.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblEditorTitle.ForeColor = System.Drawing.Color.Silver;
            this.lblEditorTitle.Location = new System.Drawing.Point(10, 8);
            this.lblEditorTitle.Name = "lblEditorTitle";
            this.lblEditorTitle.Size = new System.Drawing.Size(39, 15);
            this.lblEditorTitle.TabIndex = 0;
            this.lblEditorTitle.Text = "Editor";
            // 
            // ucProblemDetail
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.pnlToolbar);
            this.Controls.Add(this.pnlHeader);
            this.Name = "ucProblemDetail";
            this.Size = new System.Drawing.Size(1200, 800);
            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            this.pnlToolbar.ResumeLayout(false);
            this.pnlToolbar.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.pnlEditorContainer.ResumeLayout(false);
            this.pnlOutput.ResumeLayout(false);
            this.pnlOutput.PerformLayout();
            this.panelTabs.ResumeLayout(false);
            this.pnlCodeArea.ResumeLayout(false);
            this.pnlEditorHeader.ResumeLayout(false);
            this.pnlEditorHeader.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Button btnBack;
        private System.Windows.Forms.Label lblProblemTitle;
        private System.Windows.Forms.Label lblDeadline;
        private System.Windows.Forms.Panel pnlToolbar;
        private System.Windows.Forms.Button btnRun;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnSubmit;
        private System.Windows.Forms.Label lblLanguage;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.FlowLayoutPanel flowDescription;
        private System.Windows.Forms.Panel pnlEditorContainer;
        private System.Windows.Forms.Panel pnlCodeArea;
        private System.Windows.Forms.Panel pnlEditorHeader;
        private System.Windows.Forms.Label lblEditorTitle;
        private System.Windows.Forms.Label lblFileName;
        private System.Windows.Forms.RichTextBox rtbCodeEditor;
        private System.Windows.Forms.Panel pnlOutput;
        private System.Windows.Forms.Panel panelTabs;
        private System.Windows.Forms.Label lblTabOutput;
        private System.Windows.Forms.Label lblTabConsole;
        private System.Windows.Forms.TextBox txtConsole;
    }
}