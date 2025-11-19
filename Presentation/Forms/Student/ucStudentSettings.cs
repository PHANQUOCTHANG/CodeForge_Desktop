using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CodeForge_Desktop.Presentation.Forms.Student
{
    public partial class ucStudentSettings : UserControl
    {
        public ucStudentSettings()
        {
            InitializeComponent();
            SwitchTab(pnlGeneral, btnGeneral);

            cboNgonNgu.Items.Add("Tiếng Việt");
            cboNgonNgu.Items.Add("English");
            cboNgonNgu.Items.Add("Español");
            cboNgonNgu.Items.Add("Français");
            cboNgonNgu.Items.Add("日本語");
            cboNgonNgu.SelectedIndex = 0;

            cboMuiGio.Items.Add("GMT+7 (Vietnam)");
            cboMuiGio.Items.Add("GMT+8 (Singapore, Beijing)");
            cboMuiGio.Items.Add("GMT+0 (UTC/GMT)");
            cboMuiGio.Items.Add("GMT-5 (New York)");
            cboMuiGio.SelectedIndex = 0;

            cboTheme.Items.Add("Dark");
            cboTheme.Items.Add("Light");
            cboTheme.SelectedIndex = 0;

            AddFontSizes(cboFontSize);
            cboFontSize.SelectedIndex = 12;

            cboTabSize.Items.Add("2 spaces");
            cboTabSize.Items.Add("4 spaces");
            cboTabSize.Items.Add("8 spaces");
            cboTabSize.SelectedIndex = 0;

        }

        private void SwitchTab(Panel activePanel, Button activeButton)
        {
            ResetMenuButtons(btnGeneral);
            ResetMenuButtons(btnEditor);
            ResetMenuButtons(btnNotifications);

            pnlGeneral.Visible = false;
            pnlEditor.Visible = false;
            pnlNotifications.Visible = false;

            activePanel.Visible = true;
            activeButton.BackColor = Color.DodgerBlue; 
            activeButton.ForeColor = Color.White;      
            activeButton.FlatAppearance.BorderSize = 0;
        }
        private void ResetMenuButtons(Button btn)
        {
            btn.BackColor = Color.White; 
            btn.ForeColor = Color.Black;     
                                             
        }
        private void btnGeneral_Click(object sender, EventArgs e)
        {
            SwitchTab(pnlGeneral, btnGeneral);
        }
        private void AddFontSizes(ComboBox cbo)
        {
            int minSize = 8;
            int maxSize = 72;
            int step = 2;

            for (int size = minSize; size <= 28; size += step)
            {
                cbo.Items.Add(size);
            }
            for (int size = 32; size <= 48; size += 4)
            {
                cbo.Items.Add(size);
            }
            for (int size = 60; size <= maxSize; size += 12)
            {
                cbo.Items.Add(size);
            }
        }
        private void btnEditor_Click(object sender, EventArgs e)
        {
            SwitchTab(pnlEditor, btnEditor);
        }

        private void btnNotifications_Click(object sender, EventArgs e)
        {
            SwitchTab(pnlNotifications, btnNotifications);
        }
    }
}
