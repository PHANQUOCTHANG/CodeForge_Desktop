using CodeForge_Desktop.Config;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace CodeForge_Desktop.Presentation.Forms.Student.UserControls
{
    public class ProblemViewer : Form
    {
        private Guid _problemId;
        private Label lblTitle;
        private TextBox txtDescription;
        private Label lblDifficulty;
        private Button btnClose;

        public ProblemViewer(Guid problemId)
        {
            _problemId = problemId;
            Initialize();
            LoadProblem();
        }

        private void Initialize()
        {
            this.Text = "Problem";
            this.Size = new Size(700, 500);
            this.StartPosition = FormStartPosition.CenterParent;

            lblTitle = new Label { Font = new Font("Segoe UI", 12F, FontStyle.Bold), AutoSize = false, Height = 30, Dock = DockStyle.Top, Padding = new Padding(8) };
            lblDifficulty = new Label { Font = new Font("Segoe UI", 9F), ForeColor = Color.Gray, AutoSize = false, Height = 22, Dock = DockStyle.Top, Padding = new Padding(8) };

            txtDescription = new TextBox { Multiline = true, ReadOnly = true, ScrollBars = ScrollBars.Vertical, Dock = DockStyle.Fill, Font = new Font("Segoe UI", 10F), BackColor = Color.WhiteSmoke, BorderStyle = BorderStyle.FixedSingle, Padding = new Padding(8) };

            btnClose = new Button { Text = "Close", Width = 96, Height = 32, Anchor = AnchorStyles.Bottom | AnchorStyles.Right };
            btnClose.Click += (s, e) => this.Close();

            var pnlBottom = new Panel { Height = 48, Dock = DockStyle.Bottom };
            pnlBottom.Controls.Add(btnClose);
            btnClose.Location = new Point(pnlBottom.ClientSize.Width - btnClose.Width - 12, 8);
            btnClose.Anchor = AnchorStyles.Top | AnchorStyles.Right;

            this.Controls.Add(txtDescription);
            this.Controls.Add(lblDifficulty);
            this.Controls.Add(lblTitle);
            this.Controls.Add(pnlBottom);
        }

        private void LoadProblem()
        {
            try
            {
                var dt = DbContext.Query("SELECT Title, Description, Difficulty FROM CodingProblems WHERE ProblemID = @P", new SqlParameter("@P", _problemId));
                if (dt != null && dt.Rows.Count > 0)
                {
                    var r = dt.Rows[0];
                    lblTitle.Text = r["Title"] != DBNull.Value ? r["Title"].ToString() : "(untitled)";
                    lblDifficulty.Text = r.Table.Columns.Contains("Difficulty") && r["Difficulty"] != DBNull.Value ? $"Difficulty: {r["Difficulty"]}" : "";
                    txtDescription.Text = r.Table.Columns.Contains("Description") && r["Description"] != DBNull.Value ? r["Description"].ToString() : "(no description)";
                }
                else
                {
                    lblTitle.Text = "(Problem not found)";
                    txtDescription.Text = "";
                    lblDifficulty.Text = "";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to load problem: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}