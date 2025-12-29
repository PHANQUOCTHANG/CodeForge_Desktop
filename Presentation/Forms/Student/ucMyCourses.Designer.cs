namespace CodeForge_Desktop.Presentation.Forms.Student.UserControls
{
    partial class ucMyCourses
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.FlowLayoutPanel flpCourses;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.flpCourses = new System.Windows.Forms.FlowLayoutPanel();
            this.pnlHeader.SuspendLayout();
            this.SuspendLayout();

            // 
            // pnlHeader
            // 
            this.pnlHeader.Controls.Add(this.lblTitle);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Height = 60;
            this.pnlHeader.BackColor = System.Drawing.Color.White;

            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblTitle.Location = new System.Drawing.Point(20, 15);
            this.lblTitle.Text = "Khóa học của tôi";

            // 
            // flpCourses
            // 
            this.flpCourses.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flpCourses.AutoScroll = true;
            this.flpCourses.Padding = new System.Windows.Forms.Padding(20);
            this.flpCourses.BackColor = System.Drawing.Color.WhiteSmoke;

            // 
            // ucMyCourses
            // 
            this.Controls.Add(this.flpCourses);
            this.Controls.Add(this.pnlHeader);
            this.Size = new System.Drawing.Size(1000, 700);
            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            this.ResumeLayout(false);
        }
    }
}