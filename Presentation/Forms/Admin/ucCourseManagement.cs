using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using CodeForge_Desktop.Config;

namespace CodeForge_Desktop.Presentation.Forms.Admin
{
    // Simple course management control: list courses, create/edit/soft-delete,
    // open module/lesson manager for selected course. Designer partial holds UI layout.
    public partial class ucCourseManagement : UserControl
    {
        public ucCourseManagement()
        {
            InitializeComponent();
            WireEvents();
            this.Load += (s, e) => LoadCourses();
        }

        private void WireEvents()
        {
            btnRefresh.Click += (s, e) => LoadCourses();
            btnNew.Click += (s, e) => CreateOrEditCourse();
            btnEdit.Click += (s, e) => CreateOrEditCourse(GetSelectedCourseId());
            btnDelete.Click += (s, e) => SoftDeleteCourse();
            btnManageModules.Click += (s, e) => OpenModuleManagerForSelectedCourse();
            dgvCourses.DoubleClick += (s, e) => CreateOrEditCourse(GetSelectedCourseId());
        }

        private Guid? GetSelectedCourseId()
        {
            if (dgvCourses.SelectedRows.Count == 0) return null;
            var drv = dgvCourses.SelectedRows[0].DataBoundItem as DataRowView;
            if (drv == null) return null;
            if (!drv.Row.Table.Columns.Contains("CourseID")) return null;
            if (drv.Row["CourseID"] == DBNull.Value) return null;
            return (Guid)drv.Row["CourseID"];
        }

        private void LoadCourses()
        {
            try
            {
                var dt = DbContext.Query(@"SELECT CourseID, Title, Level, Language, Price, Status, CreatedAt, ISNULL(IsDeleted,0) AS IsDeleted
                                           FROM Courses
                                           ORDER BY CreatedAt DESC");
                dgvCourses.DataSource = dt;
                if (dgvCourses.Columns.Contains("CourseID")) dgvCourses.Columns["CourseID"].Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("LoadCourses failed: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CreateOrEditCourse(Guid? courseId = null)
        {
            DataRow row = null;
            if (courseId != null)
            {
                var dt = DbContext.Query("SELECT * FROM Courses WHERE CourseID = @id", new SqlParameter("@id", courseId));
                if (dt == null || dt.Rows.Count == 0) { MessageBox.Show("Course not found."); return; }
                row = dt.Rows[0];
            }

            using (var dlg = new CourseDialog(row))
            {
                if (dlg.ShowDialog() != DialogResult.OK) return;

                if (courseId == null)
                {
                    DbContext.ExecuteNonQuery(
                        @"INSERT INTO Courses (CourseID, Title, Description, Overview, Level, Language, CreatedBy, Status, Price, CategoryID, CreatedAt)
                          VALUES (NEWID(), @Title, @Description, @Overview, @Level, @Language, @CreatedBy, @Status, @Price, @CategoryID, SYSUTCDATETIME())",
                        new SqlParameter("@Title", dlg.Title ?? (object)DBNull.Value),
                        new SqlParameter("@Description", dlg.Description ?? (object)DBNull.Value),
                        new SqlParameter("@Overview", dlg.Overview ?? (object)DBNull.Value),
                        new SqlParameter("@Level", dlg.Level ?? (object)DBNull.Value),
                        new SqlParameter("@Language", dlg.Language ?? (object)DBNull.Value),
                        new SqlParameter("@CreatedBy", Guid.Empty),
                        new SqlParameter("@Status", dlg.Status ?? (object)DBNull.Value),
                        new SqlParameter("@Price", dlg.Price),
                        new SqlParameter("@CategoryID", DBNull.Value)
                    );
                }
                else
                {
                    DbContext.ExecuteNonQuery(
                        @"UPDATE Courses SET Title=@Title, Description=@Description, Overview=@Overview, Level=@Level, Language=@Language, Status=@Status, Price=@Price WHERE CourseID=@id",
                        new SqlParameter("@Title", dlg.Title ?? (object)DBNull.Value),
                        new SqlParameter("@Description", dlg.Description ?? (object)DBNull.Value),
                        new SqlParameter("@Overview", dlg.Overview ?? (object)DBNull.Value),
                        new SqlParameter("@Level", dlg.Level ?? (object)DBNull.Value),
                        new SqlParameter("@Language", dlg.Language ?? (object)DBNull.Value),
                        new SqlParameter("@Status", dlg.Status ?? (object)DBNull.Value),
                        new SqlParameter("@Price", dlg.Price),
                        new SqlParameter("@id", courseId)
                    );
                }
                LoadCourses();
            }
        }

        private void SoftDeleteCourse()
        {
            var id = GetSelectedCourseId();
            if (id == null) { MessageBox.Show("Select a course first."); return; }
            if (MessageBox.Show("Mark selected course as deleted?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;
            DbContext.ExecuteNonQuery("UPDATE Courses SET IsDeleted = 1 WHERE CourseID = @id", new SqlParameter("@id", id));
            LoadCourses();
        }

        private void OpenModuleManagerForSelectedCourse()
        {
            var courseId = GetSelectedCourseId();
            if (courseId == null) { MessageBox.Show("Select a course first."); return; }
            using (var dlg = new ModuleLessonManager(courseId.Value))
            {
                dlg.ShowDialog();
                // refresh courses/mods after close
                LoadCourses();
            }
        }

        // ---------- Modal editors ----------

        private class CourseDialog : Form
        {
            public string Title => txtTitle.Text;
            public string Description => txtDesc.Text;
            public string Overview => txtOverview.Text;
            public string Level => txtLevel.Text;
            public string Language => txtLanguage.Text;
            public decimal Price => numPrice.Value;
            public string Status => txtStatus.Text;

            private TextBox txtTitle, txtDesc, txtOverview, txtLevel, txtLanguage, txtStatus;
            private NumericUpDown numPrice;
            private Button ok, cancel;

            public CourseDialog(DataRow row)
            {
                Text = row == null ? "New Course" : "Edit Course";
                Size = new Size(700, 520);
                FormBorderStyle = FormBorderStyle.FixedDialog;
                StartPosition = FormStartPosition.CenterParent;

                var y = 8;
                var lblTitle = new Label { Text = "Title", Location = new Point(10, y) };
                txtTitle = new TextBox { Location = new Point(10, y + 20), Width = 660 };

                y += 60;
                var lblDesc = new Label { Text = "Description (HTML)", Location = new Point(10, y) };
                txtDesc = new TextBox { Location = new Point(10, y + 20), Width = 660, Height = 120, Multiline = true, ScrollBars = ScrollBars.Vertical };

                y += 150;
                var lblOverview = new Label { Text = "Overview (HTML)", Location = new Point(10, y) };
                txtOverview = new TextBox { Location = new Point(10, y + 20), Width = 660, Height = 100, Multiline = true, ScrollBars = ScrollBars.Vertical };

                y += 130;
                var lblLevel = new Label { Text = "Level", Location = new Point(10, y) };
                txtLevel = new TextBox { Location = new Point(10, y + 20), Width = 120 };

                var lblLang = new Label { Text = "Language", Location = new Point(150, y) };
                txtLanguage = new TextBox { Location = new Point(150, y + 20), Width = 120 };

                var lblPrice = new Label { Text = "Price", Location = new Point(290, y) };
                numPrice = new NumericUpDown { Location = new Point(290, y + 20), DecimalPlaces = 2, Maximum = 1000000 };

                var lblStatus = new Label { Text = "Status", Location = new Point(430, y) };
                txtStatus = new TextBox { Location = new Point(430, y + 20), Width = 120 };

                ok = new Button { Text = "OK", DialogResult = DialogResult.OK, Location = new Point(520, y + 60) };
                cancel = new Button { Text = "Cancel", DialogResult = DialogResult.Cancel, Location = new Point(600, y + 60) };

                Controls.AddRange(new Control[] { lblTitle, txtTitle, lblDesc, txtDesc, lblOverview, txtOverview, lblLevel, txtLevel, lblLang, txtLanguage, lblPrice, numPrice, lblStatus, txtStatus, ok, cancel });

                if (row != null)
                {
                    txtTitle.Text = row.Table.Columns.Contains("Title") && row["Title"] != DBNull.Value ? row["Title"].ToString() : "";
                    txtDesc.Text = row.Table.Columns.Contains("Description") && row["Description"] != DBNull.Value ? row["Description"].ToString() : "";
                    txtOverview.Text = row.Table.Columns.Contains("Overview") && row["Overview"] != DBNull.Value ? row["Overview"].ToString() : "";
                    txtLevel.Text = row.Table.Columns.Contains("Level") && row["Level"] != DBNull.Value ? row["Level"].ToString() : "";
                    txtLanguage.Text = row.Table.Columns.Contains("Language") && row["Language"] != DBNull.Value ? row["Language"].ToString() : "";
                    numPrice.Value = row.Table.Columns.Contains("Price") && row["Price"] != DBNull.Value ? Convert.ToDecimal(row["Price"]) : 0m;
                    txtStatus.Text = row.Table.Columns.Contains("Status") && row["Status"] != DBNull.Value ? row["Status"].ToString() : "";
                }
            }
        }

        // Very small module/lesson manager dialog to create/edit modules and lessons
        private class ModuleLessonManager : Form
        {
            private Guid _courseId;
            private DataGridView dgvModules, dgvLessons;
            private Button btnNewModule, btnEditModule, btnDelModule;
            private Button btnNewLesson, btnEditLesson, btnDelLesson, btnClose;

            public ModuleLessonManager(Guid courseId)
            {
                _courseId = courseId;
                Text = "Modules & Lessons";
                Size = new Size(900, 600);
                StartPosition = FormStartPosition.CenterParent;

                dgvModules = new DataGridView { Dock = DockStyle.Left, Width = 320, ReadOnly = true, SelectionMode = DataGridViewSelectionMode.FullRowSelect, AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill };
                dgvLessons = new DataGridView { Dock = DockStyle.Fill, ReadOnly = true, SelectionMode = DataGridViewSelectionMode.FullRowSelect, AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill };

                var pnlLeft = new Panel { Dock = DockStyle.Left, Width = 320 };
                var pnlLeftButtons = new FlowLayoutPanel { Dock = DockStyle.Top, Height = 40 };
                btnNewModule = new Button { Text = "New Module" }; btnEditModule = new Button { Text = "Edit Module" }; btnDelModule = new Button { Text = "Delete Module" };
                pnlLeftButtons.Controls.AddRange(new Control[] { btnNewModule, btnEditModule, btnDelModule });
                pnlLeft.Controls.Add(dgvModules); pnlLeft.Controls.Add(pnlLeftButtons);

                var pnlRightTop = new FlowLayoutPanel { Dock = DockStyle.Top, Height = 40 };
                btnNewLesson = new Button { Text = "New Lesson" }; btnEditLesson = new Button { Text = "Edit Lesson" }; btnDelLesson = new Button { Text = "Delete Lesson" }; btnClose = new Button { Text = "Close" };
                pnlRightTop.Controls.AddRange(new Control[] { btnNewLesson, btnEditLesson, btnDelLesson, btnClose });

                Controls.Add(dgvLessons);
                Controls.Add(pnlLeft);
                Controls.Add(pnlRightTop);

                Load += (s, e) => LoadModules();
                dgvModules.SelectionChanged += (s, e) => LoadLessonsForSelectedModule();

                btnNewModule.Click += (s, e) => EditModule(null);
                btnEditModule.Click += (s, e) => { var id = GetSelectedModuleId(); if (id != null) EditModule(id); };
                btnDelModule.Click += (s, e) => DeleteModule();

                btnNewLesson.Click += (s, e) => { var mid = GetSelectedModuleId(); if (mid != null) EditLesson(mid, null); else MessageBox.Show("Select module first."); };
                btnEditLesson.Click += (s, e) => { var mid = GetSelectedModuleId(); var lid = GetSelectedLessonId(); if (mid != null && lid != null) EditLesson(mid, lid); };
                btnDelLesson.Click += (s, e) => DeleteLesson();
                btnClose.Click += (s, e) => Close();
            }

            private void LoadModules()
            {
                var dt = DbContext.Query("SELECT ModuleID, Title, OrderIndex FROM Modules WHERE CourseID=@c AND ISNULL(IsDeleted,0)=0 ORDER BY OrderIndex", new SqlParameter("@c", _courseId));
                dgvModules.DataSource = dt;
            }

            private Guid? GetSelectedModuleId()
            {
                if (dgvModules.SelectedRows.Count == 0) return null;
                var dv = dgvModules.SelectedRows[0].DataBoundItem as DataRowView;
                if (dv == null) return null;
                if (dv.Row["ModuleID"] == DBNull.Value) return null;
                return (Guid)dv.Row["ModuleID"];
            }

            private void LoadLessonsForSelectedModule()
            {
                var mid = GetSelectedModuleId();
                if (mid == null) { dgvLessons.DataSource = null; return; }
                var dt = DbContext.Query("SELECT LessonID, Title, LessonType, Duration, OrderIndex FROM Lessons WHERE ModuleID=@m AND ISNULL(IsDeleted,0)=0 ORDER BY OrderIndex", new SqlParameter("@m", mid));
                dgvLessons.DataSource = dt;
            }

            private Guid? GetSelectedLessonId()
            {
                if (dgvLessons.SelectedRows.Count == 0) return null;
                var dv = dgvLessons.SelectedRows[0].DataBoundItem as DataRowView;
                if (dv == null) return null;
                if (dv.Row["LessonID"] == DBNull.Value) return null;
                return (Guid)dv.Row["LessonID"];
            }

            private void EditModule(Guid? moduleId)
            {
                DataRow row = null;
                if (moduleId != null)
                {
                    var dt = DbContext.Query("SELECT * FROM Modules WHERE ModuleID=@id", new SqlParameter("@id", moduleId));
                    if (dt != null && dt.Rows.Count > 0) row = dt.Rows[0];
                }
                using (var dlg = new SimpleTextEditor(row, "Module", "Title", "OrderIndex")) // re-using small editor
                {
                    if (dlg.ShowDialog() != DialogResult.OK) return;
                    if (moduleId == null)
                    {
                        DbContext.ExecuteNonQuery("INSERT INTO Modules (ModuleID, CourseID, Title, OrderIndex) VALUES (NEWID(), @c, @t, @o)",
                            new SqlParameter("@c", _courseId), new SqlParameter("@t", dlg.ValueText), new SqlParameter("@o", dlg.Order));
                    }
                    else
                    {
                        DbContext.ExecuteNonQuery("UPDATE Modules SET Title=@t, OrderIndex=@o WHERE ModuleID=@id",
                            new SqlParameter("@t", dlg.ValueText), new SqlParameter("@o", dlg.Order), new SqlParameter("@id", moduleId));
                    }
                    LoadModules();
                }
            }

            private void DeleteModule()
            {
                var id = GetSelectedModuleId();
                if (id == null) { MessageBox.Show("Select module"); return; }
                if (MessageBox.Show("Delete module and mark its lessons deleted?", "Confirm", MessageBoxButtons.YesNo) != DialogResult.Yes) return;
                DbContext.ExecuteNonQuery("UPDATE Lessons SET IsDeleted=1 WHERE ModuleID=@id", new SqlParameter("@id", id));
                DbContext.ExecuteNonQuery("UPDATE Modules SET IsDeleted=1 WHERE ModuleID=@id", new SqlParameter("@id", id));
                LoadModules();
            }

            private void EditLesson(Guid? moduleId, Guid? lessonId)
            {
                DataRow row = null;
                if (lessonId != null)
                {
                    var dt = DbContext.Query("SELECT * FROM Lessons WHERE LessonID=@id", new SqlParameter("@id", lessonId));
                    if (dt != null && dt.Rows.Count > 0) row = dt.Rows[0];
                }
                using (var dlg = new LessonDialog(row))
                {
                    if (dlg.ShowDialog() != DialogResult.OK) return;
                    if (lessonId == null)
                    {
                        DbContext.ExecuteNonQuery("INSERT INTO Lessons (LessonID, ModuleID, Title, LessonType, OrderIndex, Duration) VALUES (NEWID(), @m, @t, @type, @o, @d)",
                            new SqlParameter("@m", moduleId), new SqlParameter("@t", dlg.Title), new SqlParameter("@type", dlg.LessonType), new SqlParameter("@o", dlg.OrderIndex), new SqlParameter("@d", dlg.Duration));
                    }
                    else
                    {
                        DbContext.ExecuteNonQuery("UPDATE Lessons SET Title=@t, LessonType=@type, OrderIndex=@o, Duration=@d WHERE LessonID=@id",
                            new SqlParameter("@t", dlg.Title), new SqlParameter("@type", dlg.LessonType), new SqlParameter("@o", dlg.OrderIndex), new SqlParameter("@d", dlg.Duration), new SqlParameter("@id", lessonId));
                    }
                    LoadLessonsForSelectedModule();
                }
            }

            private void DeleteLesson()
            {
                var id = GetSelectedLessonId();
                if (id == null) { MessageBox.Show("Select lesson"); return; }
                if (MessageBox.Show("Delete lesson and related content?", "Confirm", MessageBoxButtons.YesNo) != DialogResult.Yes) return;
                DbContext.ExecuteNonQuery("UPDATE Lessons SET IsDeleted=1 WHERE LessonID=@id", new SqlParameter("@id", id));
                DbContext.ExecuteNonQuery("DELETE FROM LessonTexts WHERE LessonID=@id", new SqlParameter("@id", id));
                DbContext.ExecuteNonQuery("DELETE FROM LessonVideos WHERE LessonID=@id", new SqlParameter("@id", id));
                DbContext.ExecuteNonQuery("DELETE FROM LessonQuizzes WHERE LessonID=@id", new SqlParameter("@id", id));
                DbContext.ExecuteNonQuery("DELETE FROM QuizQuestions WHERE LessonQuizId=@id", new SqlParameter("@id", id));
                LoadLessonsForSelectedModule();
            }

            // small reusable editors:
            private class SimpleTextEditor : Form
            {
                public string ValueText => txt.Text;
                public int Order => (int)nudOrder.Value;
                private TextBox txt;
                private NumericUpDown nudOrder;
                public SimpleTextEditor(DataRow row, string title, string textColumn, string orderColumn)
                {
                    Text = row == null ? $"New {title}" : $"Edit {title}";
                    Size = new Size(420, 160);
                    StartPosition = FormStartPosition.CenterParent;
                    txt = new TextBox { Location = new Point(10, 30), Width = 380 };
                    nudOrder = new NumericUpDown { Location = new Point(10, 70), Maximum = 1000 };
                    var ok = new Button { Text = "OK", DialogResult = DialogResult.OK, Location = new Point(220, 90) };
                    var cancel = new Button { Text = "Cancel", DialogResult = DialogResult.Cancel, Location = new Point(300, 90) };
                    Controls.AddRange(new Control[] { new Label { Text = title, Location = new Point(10, 10) }, txt, new Label { Text = "Order", Location = new Point(10, 50) }, nudOrder, ok, cancel });
                    if (row != null)
                    {
                        txt.Text = row.Table.Columns.Contains(textColumn) && row[textColumn] != DBNull.Value ? row[textColumn].ToString() : "";
                        nudOrder.Value = row.Table.Columns.Contains(orderColumn) && row[orderColumn] != DBNull.Value ? Convert.ToDecimal(row[orderColumn]) : 0;
                    }
                }
            }

            private class LessonDialog : Form
            {
                public string Title => txtTitle.Text;
                public string LessonType => cmbType.Text;
                public int OrderIndex => (int)nudOrder.Value;
                public int Duration => (int)nudDuration.Value;

                private TextBox txtTitle;
                private ComboBox cmbType;
                private NumericUpDown nudOrder, nudDuration;
                private Button ok, cancel;

                public LessonDialog(DataRow row)
                {
                    Text = row == null ? "New Lesson" : "Edit Lesson";
                    Size = new Size(480, 220);
                    StartPosition = FormStartPosition.CenterParent;
                    txtTitle = new TextBox { Location = new Point(10, 30), Width = 440 };
                    cmbType = new ComboBox { Location = new Point(10, 80), Width = 200, DropDownStyle = ComboBoxStyle.DropDownList };
                    cmbType.Items.AddRange(new[] { "text", "video", "quiz", "coding" });
                    nudOrder = new NumericUpDown { Location = new Point(230, 80), Maximum = 1000 };
                    nudDuration = new NumericUpDown { Location = new Point(350, 80), Maximum = 36000 };
                    ok = new Button { Text = "OK", DialogResult = DialogResult.OK, Location = new Point(300, 140) };
                    cancel = new Button { Text = "Cancel", DialogResult = DialogResult.Cancel, Location = new Point(380, 140) };
                    Controls.AddRange(new Control[] { new Label { Text = "Title", Location = new Point(10, 10) }, txtTitle,
                        new Label { Text = "Type", Location = new Point(10, 60) }, cmbType,
                        new Label { Text = "Order", Location = new Point(230, 60) }, nudOrder,
                        new Label { Text = "Duration (s)", Location = new Point(350, 60) }, nudDuration, ok, cancel });

                    if (row != null)
                    {
                        txtTitle.Text = row["Title"] != DBNull.Value ? row["Title"].ToString() : "";
                        cmbType.Text = row.Table.Columns.Contains("LessonType") && row["LessonType"] != DBNull.Value ? row["LessonType"].ToString() : "text";
                        nudOrder.Value = row.Table.Columns.Contains("OrderIndex") && row["OrderIndex"] != DBNull.Value ? Convert.ToDecimal(row["OrderIndex"]) : 0;
                        nudDuration.Value = row.Table.Columns.Contains("Duration") && row["Duration"] != DBNull.Value ? Convert.ToDecimal(row["Duration"]) : 0;
                    }
                }
            }
        }
    }

    // DbContext adapter
    internal static class DbContext
    {
        public static DataTable Query(string sql, params SqlParameter[] ps) => CodeForge_Desktop.Config.DbContext.Query(sql, ps);
        public static int ExecuteNonQuery(string sql, params SqlParameter[] ps) => CodeForge_Desktop.Config.DbContext.Execute(sql, ps);
    }
}