using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace CodeForge_Desktop.Presentation.Forms.Admin
{
    partial class ucAdminCourseCreation
    {
        private IContainer components = null;

        // Course info controls
        private Label lblTitle;
        private TextBox txtTitle;
        private Label lblDescription;
        private RichTextBox rtbDescription;
        private Label lblOverview;
        private RichTextBox rtbOverview;
        private Label lblLevel;
        private ComboBox cmbLevel;
        private Label lblLanguage;
        private ComboBox cmbLanguage;
        private Label lblPrice;
        private NumericUpDown numPrice;
        private Label lblDiscount;
        private NumericUpDown nudDiscount;
        private Label lblStatus;
        private ComboBox cmbStatus;

        // Module / Lesson controls
        private GroupBox grpModules;
        private ListBox lbxModules;
        private Button btnAddModule;
        private Button btnEditModule;
        private Button btnDeleteModule;

        private GroupBox grpLessons;
        private ListBox lbxLessons;
        private Button btnAddLesson;
        private Button btnEditLesson;
        private Button btnDeleteLesson;

        // Actions
        private Button btnSave;
        private Button btnCancel;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
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

            // Basic layout sizes
            this.BackColor = SystemColors.Control;
            this.Size = new Size(980, 620);

            // Title
            lblTitle = new Label { Text = "Title", Location = new Point(12, 12), AutoSize = true };
            txtTitle = new TextBox { Location = new Point(12, 34), Width = 720 };

            // Description
            lblDescription = new Label { Text = "Description (HTML)", Location = new Point(12, 68), AutoSize = true };
            rtbDescription = new RichTextBox { Location = new Point(12, 90), Width = 720, Height = 120 };

            // Overview
            lblOverview = new Label { Text = "Overview (HTML)", Location = new Point(12, 220), AutoSize = true };
            rtbOverview = new RichTextBox { Location = new Point(12, 242), Width = 720, Height = 100 };

            // Level / Language / Price / Discount / Status
            lblLevel = new Label { Text = "Level", Location = new Point(12, 356), AutoSize = true };
            cmbLevel = new ComboBox { Location = new Point(12, 378), Width = 120, DropDownStyle = ComboBoxStyle.DropDownList };
            cmbLevel.Items.AddRange(new[] { "beginner", "intermediate", "advanced" });
            cmbLevel.Text = "beginner";

            lblLanguage = new Label { Text = "Language", Location = new Point(150, 356), AutoSize = true };
            cmbLanguage = new ComboBox { Location = new Point(150, 378), Width = 120, DropDownStyle = ComboBoxStyle.DropDownList };
            cmbLanguage.Items.AddRange(new[] { "C#", "Java", "Python", "JavaScript" });
            cmbLanguage.Text = "C#";

            lblPrice = new Label { Text = "Price", Location = new Point(290, 356), AutoSize = true };
            numPrice = new NumericUpDown { Location = new Point(290, 378), Width = 100, DecimalPlaces = 2, Maximum = 1000000, Minimum = 0 };

            lblDiscount = new Label { Text = "Discount (%)", Location = new Point(410, 356), AutoSize = true };
            nudDiscount = new NumericUpDown { Location = new Point(410, 378), Width = 80, DecimalPlaces = 0, Maximum = 100, Minimum = 0 };

            lblStatus = new Label { Text = "Status", Location = new Point(510, 356), AutoSize = true };
            cmbStatus = new ComboBox { Location = new Point(510, 378), Width = 120, DropDownStyle = ComboBoxStyle.DropDownList };
            cmbStatus.Items.AddRange(new[] { "draft", "active" });
            cmbStatus.Text = "draft";

            // Modules group
            grpModules = new GroupBox { Text = "Modules", Location = new Point(12, 420), Size = new Size(450, 170) };
            lbxModules = new ListBox { Location = new Point(12, 22), Size = new Size(300, 130) };
            btnAddModule = new Button { Text = "Add Module", Location = new Point(320, 22), AutoSize = true };
            btnEditModule = new Button { Text = "Edit Module", Location = new Point(320, 58), AutoSize = true };
            btnDeleteModule = new Button { Text = "Delete Module", Location = new Point(320, 94), AutoSize = true };
            grpModules.Controls.AddRange(new Control[] { lbxModules, btnAddModule, btnEditModule, btnDeleteModule });

            // Lessons group
            grpLessons = new GroupBox { Text = "Lessons", Location = new Point(480, 420), Size = new Size(470, 170) };
            lbxLessons = new ListBox { Location = new Point(12, 22), Size = new Size(320, 130) };
            btnAddLesson = new Button { Text = "Add Lesson", Location = new Point(340, 22), AutoSize = true };
            btnEditLesson = new Button { Text = "Edit Lesson", Location = new Point(340, 58), AutoSize = true };
            btnDeleteLesson = new Button { Text = "Delete Lesson", Location = new Point(340, 94), AutoSize = true };
            grpLessons.Controls.AddRange(new Control[] { lbxLessons, btnAddLesson, btnEditLesson, btnDeleteLesson });

            // Save / Cancel
            btnSave = new Button { Text = "Save Course", Location = new Point(12, 600), Size = new Size(120, 30) };
            btnCancel = new Button { Text = "Cancel", Location = new Point(140, 600), Size = new Size(120, 30) };

            // Add to control
            this.Controls.AddRange(new Control[] {
                lblTitle, txtTitle,
                lblDescription, rtbDescription,
                lblOverview, rtbOverview,
                lblLevel, cmbLevel,
                lblLanguage, cmbLanguage,
                lblPrice, numPrice,
                lblDiscount, nudDiscount,
                lblStatus, cmbStatus,
                grpModules, grpLessons,
                btnSave, btnCancel
            });

            // NOTE: Designer does not wire events because ucAdminCourseCreation.cs wires them in WireEvents()
        }
    }
}