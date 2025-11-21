using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace CodeForge_Desktop.Presentation.Forms.Admin
{
    partial class ucCourseManagement
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private IContainer components = null;

        private DataGridView dgvCourses;
        private FlowLayoutPanel topPanel;
        private Button btnNew;
        private Button btnEdit;
        private Button btnDelete;
        private Button btnManageModules;
        private Button btnRefresh;

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

        #region Component Designer generated code

        private void InitializeComponent()
        {
            this.components = new Container();
            this.dgvCourses = new DataGridView();
            this.topPanel = new FlowLayoutPanel();
            this.btnNew = new Button();
            this.btnEdit = new Button();
            this.btnDelete = new Button();
            this.btnManageModules = new Button();
            this.btnRefresh = new Button();

            // 
            // topPanel
            // 
            this.topPanel.Dock = DockStyle.Top;
            this.topPanel.Height = 44;
            this.topPanel.Padding = new Padding(6);
            this.topPanel.FlowDirection = FlowDirection.LeftToRight;
            this.topPanel.WrapContents = false;

            // 
            // Buttons
            // 
            this.btnNew.Text = "New Course";
            this.btnNew.AutoSize = true;
            this.btnEdit.Text = "Edit Course";
            this.btnEdit.AutoSize = true;
            this.btnDelete.Text = "Delete (soft)";
            this.btnDelete.AutoSize = true;
            this.btnManageModules.Text = "Manage Modules";
            this.btnManageModules.AutoSize = true;
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.AutoSize = true;

            this.topPanel.Controls.AddRange(new Control[] { this.btnNew, this.btnEdit, this.btnDelete, this.btnManageModules, this.btnRefresh });

            // 
            // dgvCourses
            // 
            this.dgvCourses.Dock = DockStyle.Fill;
            this.dgvCourses.ReadOnly = true;
            this.dgvCourses.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.dgvCourses.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvCourses.AllowUserToAddRows = false;
            this.dgvCourses.AllowUserToDeleteRows = false;
            this.dgvCourses.MultiSelect = false;

            // 
            // ucCourseManagement
            // 
            this.BackColor = SystemColors.Control;
            this.Controls.Add(this.dgvCourses);
            this.Controls.Add(this.topPanel);
            this.Size = new Size(980, 620);
        }

        #endregion
    }
}