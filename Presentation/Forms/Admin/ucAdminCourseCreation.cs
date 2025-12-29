using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using CodeForge_Desktop.Business.DTOs.Admin;
using CodeForge_Desktop.Business.Helpers;
using CodeForge_Desktop.Business.Services.Admin;
using CodeForge_Desktop.Config;

namespace CodeForge_Desktop.Presentation.Forms.Admin
{
    /// <summary>
    /// WinForms User Control for creating new courses with nested modules and lessons.
    /// Client-side state management: all changes held in memory until Save is clicked.
    /// No API calls during edit - only on final submission.
    /// </summary>
    public partial class ucAdminCourseCreation : UserControl
    {
        private readonly AdminCourseService _courseService;
        private bool _isEditMode = false;
        private Guid _editingCourseId = Guid.Empty;

        // Use CreateCourseDto for create mode; we use same structure with IDs for edit-mode mapping
        private CreateCourseDto _currentCourse;
        private CreateModuleDto _selectedModule;
        private CreateLessonDto _selectedLesson;

        public ucAdminCourseCreation() : this(null) { }

        // New ctor: if courseId provided, load for edit
        public ucAdminCourseCreation(Guid? courseId)
        {
            InitializeComponent();
            _courseService = new AdminCourseService(new CodeForge_Desktop.DataAccess.Repositories.CourseRepository());
            InitializeState();
            WireEvents();

            if (courseId.HasValue)
            {
                _isEditMode = true;
                _editingCourseId = courseId.Value;
                // load async
                _ = LoadCourseForEditAsync(courseId.Value);
            }
        }

        private void InitializeState()
        {
            _currentCourse = new CreateCourseDto
            {
                Status = "draft",
                Price = 0m,
                Discount = 0m,
                Level = "beginner",
                Language = "C#",
                Modules = new System.Collections.Generic.List<CreateModuleDto>()
            };
        }

        private async Task LoadCourseForEditAsync(Guid courseId)
        {
            try
            {
                var dto = await _courseService.GetCourseForEditAsync(courseId);

                // map UpdateCourseDto -> CreateCourseDto with IDs populated
                _currentCourse = new CreateCourseDto
                {
                    CourseId = dto.CourseId,
                    Title = dto.Title,
                    Description = dto.Description,
                    Overview = dto.Overview,
                    Level = dto.Level,
                    Language = dto.Language,
                    Price = dto.Price,
                    Discount = dto.Discount,
                    Status = dto.Status,
                    Modules = dto.Modules.Select(m => new CreateModuleDto
                    {
                        ModuleId = m.ModuleId,
                        Title = m.Title,
                        OrderIndex = m.OrderIndex,
                        Lessons = m.Lessons.Select(l => new CreateLessonDto
                        {
                            LessonId = l.LessonId,
                            Title = l.Title,
                            LessonType = l.LessonType,
                            Duration = l.Duration,
                            OrderIndex = l.OrderIndex,
                            Content = l.Content // may be null or partially populated
                        }).ToList()
                    }).ToList()
                };

                // populate UI fields on UI thread
                if (this.InvokeRequired) this.Invoke(new Action(RefreshAllUiFromState));
                else RefreshAllUiFromState();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to load course for edit: " + ex.Message);
            }
        }

        private void RefreshAllUiFromState()
        {
            txtTitle.Text = _currentCourse.Title;
            rtbDescription.Text = _currentCourse.Description;
            rtbOverview.Text = _currentCourse.Overview;
            cmbLevel.Text = _currentCourse.Level;
            cmbLanguage.Text = _currentCourse.Language;
            numPrice.Value = _currentCourse.Price;
            nudDiscount.Value = (decimal)_currentCourse.Discount;
            cmbStatus.Text = _currentCourse.Status;
            RefreshModuleList();
            RefreshLessonList();
        }

        private void WireEvents()
        {
            // Course info section
            btnSave.Click += BtnSave_Click;
            btnCancel.Click += (s, e) => Close();

            // Module management
            btnAddModule.Click += BtnAddModule_Click;
            btnEditModule.Click += BtnEditModule_Click;
            btnDeleteModule.Click += BtnDeleteModule_Click;
            lbxModules.SelectedIndexChanged += LbxModules_SelectedIndexChanged;

            // Lesson management
            btnAddLesson.Click += BtnAddLesson_Click;
            btnEditLesson.Click += BtnEditLesson_Click;
            btnDeleteLesson.Click += BtnDeleteLesson_Click;
            lbxLessons.SelectedIndexChanged += LbxLessons_SelectedIndexChanged;
        }

        private void BtnAddModule_Click(object sender, EventArgs e)
        {
            using (var dlg = new ModuleEditDialog())
            {
                if (dlg.ShowDialog(this) == DialogResult.OK)
                {
                    var newModule = new CreateModuleDto
                    {
                        Title = dlg.Title,
                        OrderIndex = _currentCourse.Modules.Count + 1,
                        Lessons = new List<CreateLessonDto>()
                    };
                    _currentCourse.Modules.Add(newModule);
                    RefreshModuleList();
                    MessageBox.Show("Module added. You can now add lessons to it.", "Success");
                }
            }
        }

        private void BtnEditModule_Click(object sender, EventArgs e)
        {
            if (lbxModules.SelectedIndex == -1)
            {
                MessageBox.Show("Select a module to edit.", "Info");
                return;
            }

            _selectedModule = _currentCourse.Modules[lbxModules.SelectedIndex];
            using (var dlg = new ModuleEditDialog(_selectedModule))
            {
                if (dlg.ShowDialog(this) == DialogResult.OK)
                {
                    _selectedModule.Title = dlg.Title;
                    RefreshModuleList();
                    MessageBox.Show("Module updated.", "Success");
                }
            }
        }

        private void BtnDeleteModule_Click(object sender, EventArgs e)
        {
            if (lbxModules.SelectedIndex == -1)
            {
                MessageBox.Show("Select a module to delete.", "Info");
                return;
            }

            if (MessageBox.Show("Delete this module and all its lessons?", "Confirm", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                _currentCourse.Modules.RemoveAt(lbxModules.SelectedIndex);
                RefreshModuleList();
                RefreshLessonList();
                MessageBox.Show("Module deleted.", "Success");
            }
        }

        private void LbxModules_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lbxModules.SelectedIndex >= 0)
            {
                _selectedModule = _currentCourse.Modules[lbxModules.SelectedIndex];
                RefreshLessonList();
            }
        }

        private void BtnAddLesson_Click(object sender, EventArgs e)
        {
            if (_selectedModule == null)
            {
                MessageBox.Show("Select a module first.", "Info");
                return;
            }

            using (var dlg = new LessonEditDialog())
            {
                if (dlg.ShowDialog(this) == DialogResult.OK)
                {
                    var newLesson = new CreateLessonDto
                    {
                        Title = dlg.Title,
                        LessonType = dlg.LessonType,
                        Duration = dlg.Duration,
                        OrderIndex = _selectedModule.Lessons.Count + 1,
                        Content = dlg.ContentDto
                    };
                    _selectedModule.Lessons.Add(newLesson);
                    RefreshLessonList();
                    MessageBox.Show("Lesson added.", "Success");
                }
            }
        }

        private void BtnEditLesson_Click(object sender, EventArgs e)
        {
            if (lbxLessons.SelectedIndex == -1)
            {
                MessageBox.Show("Select a lesson to edit.", "Info");
                return;
            }

            _selectedLesson = _selectedModule.Lessons[lbxLessons.SelectedIndex];
            using (var dlg = new LessonEditDialog(_selectedLesson))
            {
                if (dlg.ShowDialog(this) == DialogResult.OK)
                {
                    _selectedLesson.Title = dlg.Title;
                    _selectedLesson.LessonType = dlg.LessonType;
                    _selectedLesson.Duration = dlg.Duration;
                    _selectedLesson.Content = dlg.ContentDto;
                    RefreshLessonList();
                    MessageBox.Show("Lesson updated.", "Success");
                }
            }
        }

        private void BtnDeleteLesson_Click(object sender, EventArgs e)
        {
            if (lbxLessons.SelectedIndex == -1)
            {
                MessageBox.Show("Select a lesson to delete.", "Info");
                return;
            }

            if (MessageBox.Show("Delete this lesson?", "Confirm", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                _selectedModule.Lessons.RemoveAt(lbxLessons.SelectedIndex);
                RefreshLessonList();
                MessageBox.Show("Lesson deleted.", "Success");
            }
        }

        private void LbxLessons_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lbxLessons.SelectedIndex >= 0)
            {
                _selectedLesson = _selectedModule.Lessons[lbxLessons.SelectedIndex];
            }
        }

        private async void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                // client-side validation (basic)
                if (string.IsNullOrWhiteSpace(txtTitle.Text)) { MessageBox.Show("Title required"); return; }
                _currentCourse.Title = txtTitle.Text;
                _currentCourse.Description = rtbDescription.Text;
                _currentCourse.Overview = rtbOverview.Text;
                _currentCourse.Level = cmbLevel.Text;
                _currentCourse.Language = cmbLanguage.Text;
                _currentCourse.Price = numPrice.Value;
                _currentCourse.Discount = (decimal)nudDiscount.Value;
                _currentCourse.Status = cmbStatus.Text;

                btnSave.Enabled = false;
                btnSave.Text = "Saving...";

                var currentUser = GlobalStore.user;

                if (_isEditMode && _currentCourse.CourseId.HasValue)
                {
                    // Build UpdateCourseDto from current in-memory state
                    var updateDto = new UpdateCourseDto
                    {
                        CourseId = _currentCourse.CourseId.Value,
                        Title = _currentCourse.Title,
                        Description = _currentCourse.Description,
                        Overview = _currentCourse.Overview,
                        Level = _currentCourse.Level,
                        Language = _currentCourse.Language,
                        CategoryId = _currentCourse.CategoryId,
                        Price = _currentCourse.Price,
                        Discount = _currentCourse.Discount,
                        Status = _currentCourse.Status,
                        Modules = _currentCourse.Modules.Select(m => new UpdateModuleDto
                        {
                            ModuleId = m.ModuleId,
                            Title = m.Title,
                            OrderIndex = m.OrderIndex,
                            IsDeleted = false,
                            Lessons = m.Lessons.Select(l => new UpdateLessonDto
                            {
                                LessonId = l.LessonId,
                                Title = l.Title,
                                LessonType = l.LessonType,
                                Duration = l.Duration,
                                OrderIndex = l.OrderIndex,
                                IsDeleted = false,
                                Content = l.Content
                            }).ToList()
                        }).ToList()
                    };

                    await _courseService.UpdateCourseAsync(updateDto, currentUser.UserID);
                    MessageBox.Show("Course updated successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    var courseId = await _courseService.CreateCourseAsync(_currentCourse, currentUser.UserID);
                    MessageBox.Show($"Course created successfully! ID: {courseId}", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                ClearForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving course: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnSave.Enabled = true;
                btnSave.Text = _isEditMode ? "Save Changes" : "Save Course";
            }
        }

        private void RefreshModuleList()
        {
            lbxModules.Items.Clear();
            foreach (var mod in _currentCourse.Modules)
            {
                lbxModules.Items.Add($"{mod.Title} ({mod.Lessons.Count} lessons)");
            }
        }

        private void RefreshLessonList()
        {
            lbxLessons.Items.Clear();
            if (_selectedModule != null)
            {
                foreach (var les in _selectedModule.Lessons)
                {
                    lbxLessons.Items.Add($"{les.Title} [{les.LessonType}]");
                }
            }
        }

        private void ClearForm()
        {
            InitializeState();
            txtTitle.Clear();
            rtbDescription.Clear();
            rtbOverview.Clear();
            numPrice.Value = 0;
            nudDiscount.Value = 0;
            RefreshModuleList();
            RefreshLessonList();
        }

        // Nested dialogs for module and lesson editing...
        // (Implementation simplified for brevity)

        private class ModuleEditDialog : Form
        {
            public string Title { get; private set; }
            private TextBox txtTitle;
            private Button ok, cancel;

            public ModuleEditDialog(CreateModuleDto existing = null)
            {
                Text = existing == null ? "New Module" : "Edit Module";
                Size = new Size(350, 150);
                FormBorderStyle = FormBorderStyle.FixedDialog;
                StartPosition = FormStartPosition.CenterParent;

                txtTitle = new TextBox { Location = new Point(10, 30), Width = 300 };
                ok = new Button { Text = "OK", DialogResult = DialogResult.OK, Location = new Point(170, 80) };
                cancel = new Button { Text = "Cancel", DialogResult = DialogResult.Cancel, Location = new Point(250, 80) };

                Controls.AddRange(new Control[] { new Label { Text = "Title", Location = new Point(10, 10) }, txtTitle, ok, cancel });

                if (existing != null)
                    txtTitle.Text = existing.Title;
            }

            public new DialogResult ShowDialog(IWin32Window owner)
            {
                var result = base.ShowDialog(owner);
                if (result == DialogResult.OK)
                    Title = txtTitle.Text;
                return result;
            }
        }

        private class LessonEditDialog : Form
        {
            public string Title { get; private set; }
            public string LessonType { get; private set; }
            public int Duration { get; private set; }
            public LessonContentDto ContentDto { get; private set; }

            private TextBox txtTitle;
            private ComboBox cmbType;
            private NumericUpDown nudDuration;
            private TextBox txtVideoUrl;
            private RichTextBox rtbTextContent;
            private Button ok, cancel;

            public LessonEditDialog(CreateLessonDto existing = null)
            {
                Text = existing == null ? "New Lesson" : "Edit Lesson";
                Size = new Size(500, 350);
                FormBorderStyle = FormBorderStyle.FixedDialog;
                StartPosition = FormStartPosition.CenterParent;

                txtTitle = new TextBox { Location = new Point(10, 30), Width = 450 };
                cmbType = new ComboBox { Location = new Point(10, 80), Width = 150, DropDownStyle = ComboBoxStyle.DropDownList };
                cmbType.Items.AddRange(new[] { "text", "video", "quiz", "coding" });
                nudDuration = new NumericUpDown { Location = new Point(170, 80), Maximum = 36000 };
                txtVideoUrl = new TextBox { Location = new Point(10, 130), Width = 450, Visible = false };
                rtbTextContent = new RichTextBox { Location = new Point(10, 130), Width = 450, Height = 150, Visible = false };

                ok = new Button { Text = "OK", DialogResult = DialogResult.OK, Location = new Point(340, 300) };
                cancel = new Button { Text = "Cancel", DialogResult = DialogResult.Cancel, Location = new Point(420, 300) };

                cmbType.SelectedIndexChanged += (s, e) => UpdateContentControls();

                Controls.AddRange(new Control[] {
                    new Label { Text = "Title", Location = new Point(10, 10) }, txtTitle,
                    new Label { Text = "Type", Location = new Point(10, 60) }, cmbType,
                    new Label { Text = "Duration (s)", Location = new Point(170, 60) }, nudDuration,
                    new Label { Text = "Content", Location = new Point(10, 110) }, txtVideoUrl, rtbTextContent,
                    ok, cancel
                });

                if (existing != null)
                {
                    txtTitle.Text = existing.Title;
                    cmbType.Text = existing.LessonType;
                    nudDuration.Value = existing.Duration;
                }
            }

            private void UpdateContentControls()
            {
                txtVideoUrl.Visible = cmbType.Text == "video";
                rtbTextContent.Visible = cmbType.Text == "text";
            }

            public new DialogResult ShowDialog(IWin32Window owner)
            {
                var result = base.ShowDialog(owner);
                if (result == DialogResult.OK)
                {
                    Title = txtTitle.Text;
                    LessonType = cmbType.Text;
                    Duration = (int)nudDuration.Value;

                    ContentDto = new LessonContentDto();
                    if (cmbType.Text == "video")
                        ContentDto.VideoUrl = txtVideoUrl.Text;
                    else if (cmbType.Text == "text")
                        ContentDto.TextContent = rtbTextContent.Text;
                }
                return result;
            }
        }
    }
}