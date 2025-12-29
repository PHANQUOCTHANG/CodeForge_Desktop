using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using CodeForge_Desktop.Config;
namespace CodeForge_Desktop.Presentation.Forms.Admin
{
    /// <summary>
    /// Course Management UI with card-based layout.
    /// Simplified toolbar with only Create, Refresh, and Pagination controls.
    /// Individual cards have Edit, Delete, Manage buttons.
    /// </summary>
    public partial class ucCourseManagement : UserControl
    {
        private Guid? _selectedCourseId;
        private int _pageSize = 12;
        private int _currentPage = 1;
        private int _totalItems = 0;

        public ucCourseManagement()
        {
            InitializeComponent();
            WireEvents();
            this.Load += (s, e) => LoadCourses();
        }

        private void WireEvents()
        {
            btnRefresh.Click += (s, e) => LoadCourses();
            btnCreateNew.Click += (s, e) => OpenCreateCourse();

            // Paging
            btnPrevPage.Click += (s, e) => 
            { 
                if (_currentPage > 1) 
                { 
                    _currentPage--; 
                    LoadCourses(); 
                } 
            };
            btnNextPage.Click += (s, e) => 
            { 
                _currentPage++; 
                LoadCourses(); 
            };
        }

        private void LoadCourses()
        {
            try
            {
                flpCourses.Controls.Clear();

                var countObj = DbContext.ExecuteScalar("SELECT COUNT(1) FROM Courses WHERE ISNULL(IsDeleted,0)=0");
                _totalItems = Convert.ToInt32(countObj ?? 0);
                var offset = (_currentPage - 1) * _pageSize;

                string sql = @"
                    SELECT CourseID, Title, Level, Language, Price, Discount, Status, Thumbnail
                    FROM Courses
                    WHERE ISNULL(IsDeleted,0)=0
                    ORDER BY CreatedAt DESC
                    OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY";

                var dt = DbContext.Query(sql, new SqlParameter("@Offset", offset), new SqlParameter("@PageSize", _pageSize));

                if (dt == null || dt.Rows.Count == 0)
                {
                    var lbl = new Label 
                    { 
                        Text = "No courses available. Click 'Create New Course' to get started.", 
                        AutoSize = true, 
                        ForeColor = Color.Gray, 
                        Padding = new Padding(20),
                        Font = new Font("Segoe UI", 11F)
                    };
                    flpCourses.Controls.Add(lbl);
                }
                else
                {
                    foreach (DataRow r in dt.Rows)
                    {
                        var id = r["CourseID"] == DBNull.Value ? Guid.Empty : (Guid)r["CourseID"];
                        var title = r["Title"]?.ToString() ?? "(Untitled)";
                        var level = r["Level"]?.ToString() ?? "-";
                        var lang = r["Language"]?.ToString() ?? "-";
                        var price = r["Price"] != DBNull.Value ? Convert.ToDecimal(r["Price"]) : 0m;
                        var discount = r.Table.Columns.Contains("Discount") && r["Discount"] != DBNull.Value ? Convert.ToDecimal(r["Discount"]) : 0m;
                        var status = r["Status"]?.ToString() ?? "draft";
                        string thumb = null;
                        if (r.Table.Columns.Contains("Thumbnail") && r["Thumbnail"] != DBNull.Value)
                            thumb = r["Thumbnail"].ToString();

                        var card = new CourseCard();
                        card.SetData(id, title, level, lang, price, discount, status, thumb);
                        
                        // Card events - actions now on the card itself
                        card.EditClicked += (s, courseId) => OpenEditCourse(courseId);
                        card.DeleteClicked += (s, courseId) => SoftDeleteCourse(courseId);
                        card.ManageClicked += (s, courseId) => OpenModuleManagerForCourse(courseId);
                        card.CardClicked += (s, courseId) => SelectCard(courseId, (CourseCard)s);

                        flpCourses.Controls.Add(card);
                    }
                }

                int totalPages = Math.Max(1, (int)Math.Ceiling(_totalItems / (double)_pageSize));
                lblPageInfo.Text = $"Page {_currentPage} of {totalPages}";
                btnPrevPage.Enabled = _currentPage > 1;
                btnNextPage.Enabled = (_currentPage * _pageSize) < _totalItems;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading courses: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SelectCard(Guid courseId, CourseCard card)
        {
            foreach (Control c in flpCourses.Controls.OfType<CourseCard>())
            {
                c.BackColor = Color.White;
            }
            card.BackColor = Color.FromArgb(230, 245, 255);
            _selectedCourseId = courseId;
        }

        private void OpenCreateCourse()
        {
            var ctl = new ucAdminCourseCreation();
            var parentForm = this.FindForm();
            
            using (var dlg = new Form 
            { 
                Text = "Create New Course", 
                Size = new Size(1000, 750), 
                StartPosition = FormStartPosition.CenterParent,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                MaximizeBox = false,
                MinimizeBox = false
            })
            {
                ctl.Dock = DockStyle.Fill;
                dlg.Controls.Add(ctl);
                if (parentForm != null)
                    dlg.ShowDialog(parentForm);
                else
                    dlg.ShowDialog();
            }

            LoadCourses();
        }

        private void OpenEditCourse(Guid courseId)
        {
            var ctl = new ucAdminCourseCreation(courseId);
            var parentForm = this.FindForm();
            
            using (var dlg = new Form 
            { 
                Text = "Edit Course", 
                Size = new Size(1000, 750), 
                StartPosition = FormStartPosition.CenterParent,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                MaximizeBox = false,
                MinimizeBox = false
            })
            {
                ctl.Dock = DockStyle.Fill;
                dlg.Controls.Add(ctl);
                if (parentForm != null)
                    dlg.ShowDialog(parentForm);
                else
                    dlg.ShowDialog();
            }

            LoadCourses();
        }

        private void SoftDeleteCourse(Guid courseId)
        {
            if (courseId == Guid.Empty) return;
            
            if (MessageBox.Show("Delete this course? This action cannot be undone.", "Confirm Delete", 
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    DbContext.Execute("UPDATE Courses SET IsDeleted = 1 WHERE CourseID = @id", new SqlParameter("@id", courseId));
                    LoadCourses();
                    MessageBox.Show("Course deleted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error deleting course: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void OpenModuleManagerForCourse(Guid courseId)
        {
            var ctl = new ucAdminCourseCreation(courseId);
            var parentForm = this.FindForm();
            
            using (var dlg = new Form 
            { 
                Text = "Manage Modules & Lessons", 
                Size = new Size(1000, 750), 
                StartPosition = FormStartPosition.CenterParent,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                MaximizeBox = false,
                MinimizeBox = false
            })
            {
                ctl.Dock = DockStyle.Fill;
                dlg.Controls.Add(ctl);
                if (parentForm != null)
                    dlg.ShowDialog(parentForm);
                else
                    dlg.ShowDialog();
            }

            LoadCourses();
        }
    }
}