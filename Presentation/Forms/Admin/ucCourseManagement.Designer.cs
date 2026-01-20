using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace CodeForge_Desktop.Presentation.Forms.Admin
{
    partial class ucCourseManagement
    {
        private IContainer components = null;

        private FlowLayoutPanel topPanel;
        private Button btnCreateNew;
        private Button btnRefresh;
        private Button btnPrevPage;
        private Button btnNextPage;
        private Label lblPageInfo;

        // Card panel (replaces DataGridView)
        private FlowLayoutPanel flpCourses;

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
            this.components = new Container();

            this.topPanel = new FlowLayoutPanel();
            this.btnCreateNew = new Button();
            this.btnRefresh = new Button();
            this.btnPrevPage = new Button();
            this.btnNextPage = new Button();
            this.lblPageInfo = new Label();
            this.flpCourses = new FlowLayoutPanel();

            // topPanel - simplified toolbar
            this.topPanel.Dock = DockStyle.Top;
            this.topPanel.Height = 56;
            this.topPanel.Padding = new Padding(12, 8, 12, 8);
            this.topPanel.FlowDirection = FlowDirection.LeftToRight;
            this.topPanel.WrapContents = false;
            this.topPanel.BackColor = Color.FromArgb(41, 128, 185);
            this.topPanel.AutoSize = false;

            // Create New button (primary action)
            this.btnCreateNew.Text = "+ Create New Course";
            this.btnCreateNew.AutoSize = true;
            this.btnCreateNew.BackColor = Color.FromArgb(39, 174, 96);
            this.btnCreateNew.ForeColor = Color.White;
            this.btnCreateNew.FlatStyle = FlatStyle.Flat;
            this.btnCreateNew.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.btnCreateNew.Padding = new Padding(12, 6, 12, 6);

            // Refresh button
            this.btnRefresh.Text = "🔄 Refresh";
            this.btnRefresh.AutoSize = true;
            this.btnRefresh.BackColor = Color.FromArgb(52, 152, 219);
            this.btnRefresh.ForeColor = Color.White;
            this.btnRefresh.FlatStyle = FlatStyle.Flat;
            this.btnRefresh.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.btnRefresh.Margin = new Padding(12, 0, 0, 0);

            this.topPanel.Controls.AddRange(new Control[] { this.btnCreateNew, this.btnRefresh });

            // Paging controls (right-aligned)
            this.btnPrevPage = new Button 
            { 
                Text = "← Previous",
                AutoSize = true,
                BackColor = Color.FromArgb(155, 89, 182),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9F),
                Margin = new Padding(12, 0, 0, 0)
            };
            
            this.btnNextPage = new Button 
            { 
                Text = "Next →",
                AutoSize = true,
                BackColor = Color.FromArgb(155, 89, 182),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9F),
                Margin = new Padding(12, 0, 0, 0)
            };
            
            this.lblPageInfo = new Label 
            { 
                Text = "Page 1",
                AutoSize = true,
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 9F),
                Margin = new Padding(12, 0, 0, 0)
            };

            this.topPanel.Controls.AddRange(new Control[] { btnPrevPage, btnNextPage, lblPageInfo });

            // flpCourses - card container
            this.flpCourses.Dock = DockStyle.Fill;
            this.flpCourses.AutoScroll = true;
            this.flpCourses.FlowDirection = FlowDirection.LeftToRight;
            this.flpCourses.WrapContents = true;
            this.flpCourses.Padding = new Padding(12);
            this.flpCourses.BackColor = Color.FromArgb(236, 240, 241);

            // ucCourseManagement layout
            this.BackColor = Color.FromArgb(236, 240, 241);
            this.Controls.Add(this.flpCourses);
            this.Controls.Add(this.topPanel);
            this.Size = new Size(1200, 700);
        }
    }
}