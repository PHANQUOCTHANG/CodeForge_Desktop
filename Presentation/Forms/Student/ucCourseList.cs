using System;
using System.Drawing;
using System.Windows.Forms;
using System.Data;
using System.Drawing.Drawing2D;
using System.IO;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using CodeForge_Desktop.DataAccess.Entities;
using CodeForge_Desktop.DataAccess.Interfaces;
using CodeForge_Desktop.DataAccess.Repositories;
using CodeForge_Desktop.Business.Helpers;
using CodeForge_Desktop.Business.Services;

namespace CodeForge_Desktop.Presentation.Forms.Student.UserControls
{
    public partial class ucCourseList : UserControl
    {
        private Guid _selectedCourseId = Guid.Empty;
        private DataTable _courseData;
        private readonly ICourseRepository _courseRepository;
        private System.Windows.Forms.Timer _searchTimer;

        // Default constructor (for Designer)
        public ucCourseList() : this(new CourseRepository())
        {
        }

        // Allow injecting repository (for tests / DI)
        public ucCourseList(ICourseRepository courseRepository)
        {
            _courseRepository = courseRepository ?? new CourseRepository();
            InitializeComponent();

            // Init search debounce timer (components provided by Designer so components != null after InitializeComponent)
            _searchTimer = new System.Windows.Forms.Timer { Interval = 400 };
            _searchTimer.Tick += async (s, e) =>
            {
                _searchTimer.Stop();
                await ApplyFilterAsync();
            };
            // Add to components container so it will be disposed with the control (components declared in Designer partial)
            try { components?.Add(_searchTimer); } catch { /* ignore if components null */ }

            this.Load += ucCourseList_Load;
        }

        private async void ucCourseList_Load(object sender, EventArgs e)
        {
            SetupCourseGrid();
            // subscribe to progress updates so the list refreshes when user completes lessons
            ProgressNotifier.ProgressUpdated += OnProgressUpdated;

            await LoadCoursesAsync();

            // Gán lại các sự kiện
            this.dgvCourses.SelectionChanged += dgvCourses_SelectionChanged;
            this.dgvCourses.CellDoubleClick += dgvCourses_CellDoubleClick;
            this.btnViewDetails.Click += btnViewDetails_Click;
            this.btnEnrollContinue.Click += btnEnrollContinue_Click;
            this.btnBack.Click += btnBack_Click;

            // Gắn sự kiện tìm kiếm / lọc
            this.txtSearch.TextChanged += txtSearch_TextChanged;
            this.cmbFilterLevel.SelectedIndexChanged += cmbFilterLevel_SelectedIndexChanged;

            // Mặc định chọn dòng đầu tiên để hiển thị preview
            if (dgvCourses.Rows.Count > 0)
            {
                dgvCourses.Rows[0].Selected = true;
                UpdateCoursePreview(dgvCourses.Rows[0]);
            }
        }

        // ensure we unsubscribe when control destroyed
        protected override void OnHandleDestroyed(EventArgs e)
        {
            try { ProgressNotifier.ProgressUpdated -= OnProgressUpdated; } catch { }
            base.OnHandleDestroyed(e);
        }

        // handler: refresh list (or update single row) when progress changes for current user
        private void OnProgressUpdated(object sender, ProgressUpdatedEventArgs e)
        {
            // only refresh if the notification is for current user (avoid unnecessary reloads)
            Guid current = Guid.Empty;
            try { current = GlobalStore.user?.UserID ?? Guid.Empty; } catch { current = Guid.Empty; }
            if (e == null || current == Guid.Empty || e.UserId != current) return;

            // call LoadCoursesAsync on UI thread (fire-and-forget)
            if (this.IsHandleCreated)
            {
                this.BeginInvoke((Action)(async () =>
                {
                    try
                    {
                        await LoadCoursesAsync();
                    }
                    catch { /* ignore UI refresh failures */ }
                }));
            }
        }

        #region Helpers for UI Rendering (Rounded Rectangles)

        // Hàm vẽ path hình vuông bo góc
        public GraphicsPath GetRoundedRect(Rectangle bounds, int radius)
        {
            int diameter = radius * 2;
            Size size = new Size(diameter, diameter);
            Rectangle arc = new Rectangle(bounds.Location, size);
            GraphicsPath path = new GraphicsPath();

            if (radius == 0)
            {
                path.AddRectangle(bounds);
                return path;
            }

            // top left arc
            path.AddArc(arc, 180, 90);

            // top right arc
            arc.X = bounds.Right - diameter;
            path.AddArc(arc, 270, 90);

            // bottom right arc
            arc.Y = bounds.Bottom - diameter;
            path.AddArc(arc, 0, 90);

            // bottom left arc
            arc.X = bounds.Left;
            arc.Y = bounds.Bottom - diameter;
            path.AddArc(arc, 90, 90);

            path.CloseFigure();
            return path;
        }

        // Tạo icon hình vuông bo góc (cho DataGridView và PictureBox)
        Image GetRoundedRectIcon(Color color, int size = 48, int radius = 8)
        {
            var bmp = new Bitmap(size, size);
            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                using (var path = GetRoundedRect(new Rectangle(0, 0, size, size), radius))
                {
                    g.FillPath(new SolidBrush(color), path);
                }
            }
            return bmp;
        }

        #endregion

        #region DataGrid Setup and Loading

        private void SetupCourseGrid()
        {
            dgvCourses.AutoGenerateColumns = false;
            dgvCourses.RowHeadersVisible = false;
            dgvCourses.EnableHeadersVisualStyles = false;
            dgvCourses.ColumnHeadersDefaultCellStyle.BackColor = Color.LightGray;
            dgvCourses.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.LightGray;

            dgvCourses.Columns.Clear();

            // Cột 1: Icon
            var imgCol = new DataGridViewImageColumn
            {
                Name = "colThumbnail",
                HeaderText = "Icon",
                DataPropertyName = "ThumbnailImage",
                ImageLayout = DataGridViewImageCellLayout.Zoom,
                Width = 60,
                DefaultCellStyle = { BackColor = Color.White }
            };
            dgvCourses.Columns.Add(imgCol);

            // Cột 2: Title (Link)
            dgvCourses.Columns.Add(new DataGridViewLinkColumn { Name = "colTitle", HeaderText = "Title", DataPropertyName = "Title", LinkColor = Color.Blue, ActiveLinkColor = Color.Blue, Width = 200 });
            // Cột 3: Level
            dgvCourses.Columns.Add(new DataGridViewTextBoxColumn { Name = "colLevel", HeaderText = "Level", DataPropertyName = "Level", Width = 100 });
            // Cột 4: Language
            dgvCourses.Columns.Add(new DataGridViewTextBoxColumn { Name = "colLanguage", HeaderText = "Language", DataPropertyName = "Language", Width = 100 });
            // Cột 5: Rating
            dgvCourses.Columns.Add(new DataGridViewTextBoxColumn { Name = "colRating", HeaderText = "Rating", DataPropertyName = "Rating", Width = 80, DefaultCellStyle = { Alignment = DataGridViewContentAlignment.MiddleCenter } });
            // Cột 6: Price
            dgvCourses.Columns.Add(new DataGridViewTextBoxColumn { Name = "colPrice", HeaderText = "Price", DataPropertyName = "Price", DefaultCellStyle = { Alignment = DataGridViewContentAlignment.MiddleRight, Format = "N0" } });

            // Cột ẩn chứa ID và Overview để thao tác
            dgvCourses.Columns.Add(new DataGridViewTextBoxColumn { Name = "CourseID", DataPropertyName = "CourseID", Visible = false });
            dgvCourses.Columns.Add(new DataGridViewTextBoxColumn { Name = "ShortOverview", DataPropertyName = "ShortOverview", Visible = false });
            dgvCourses.Columns.Add(new DataGridViewTextBoxColumn { Name = "TotalStudents", DataPropertyName = "TotalStudents", Visible = false });
            dgvCourses.Columns.Add(new DataGridViewTextBoxColumn { Name = "Duration", DataPropertyName = "Duration", Visible = false });

            // Hidden column that maps the enrollment flag so row.Cells["IsEnrolled"] is available
            dgvCourses.Columns.Add(new DataGridViewTextBoxColumn { Name = "IsEnrolled", DataPropertyName = "IsEnrolled", Visible = false });

            // Định dạng Level và Rating
            dgvCourses.Columns["colLevel"].DefaultCellStyle.ForeColor = Color.OrangeRed;
            dgvCourses.CellFormatting += (s, ev) =>
            {
                if (ev.ColumnIndex == dgvCourses.Columns["colRating"].Index && ev.Value != null)
                {
                    if (double.TryParse(ev.Value.ToString(), out double rating))
                    {
                        ev.Value = $"⭐️ {rating:N1}"; // Thêm biểu tượng ngôi sao
                        ev.FormattingApplied = true;
                    }
                }
            };
        }

        private void EnsureCourseDataTable()
        {
            if (_courseData != null) return;

            _courseData = new DataTable();
            _courseData.Columns.Add("CourseID", typeof(Guid));
            _courseData.Columns.Add("Title", typeof(string));
            _courseData.Columns.Add("Level", typeof(string));
            _courseData.Columns.Add("Language", typeof(string));
            _courseData.Columns.Add("Rating", typeof(double));
            _courseData.Columns.Add("Price", typeof(decimal));
            _courseData.Columns.Add("ShortOverview", typeof(string));
            _courseData.Columns.Add("TotalStudents", typeof(int));
            _courseData.Columns.Add("Duration", typeof(int)); // phút

            // new column to mark whether the current logged-in user is enrolled in this course
            _courseData.Columns.Add("IsEnrolled", typeof(bool));

            // Add progress column — PopulateTableFromCoursesAsync adds progress value, so the DataTable must have this column
            _courseData.Columns.Add("ProgressPercentage", typeof(int));

            _courseData.Columns.Add("ThumbnailImage", typeof(Image));
        }

        private async Task LoadCoursesAsync()
        {
            try
            {
                EnsureCourseDataTable();
                _courseData.Rows.Clear();

                Guid currentUserId = Guid.Empty;
                try { currentUserId = GlobalStore.user?.UserID ?? Guid.Empty; } catch { currentUserId = Guid.Empty; }

                List<Course> courses;
                // use repository method that includes IsEnrolled and ProgressPercentage
                if (currentUserId != Guid.Empty && _courseRepository is CourseRepository repoConcrete)
                {
                    courses = await repoConcrete.GetListHasEnrollAsync(currentUserId);
                }
                else
                {
                    // fallback to plain list when no user
                    courses = await _courseRepository.GetAllAsync();
                }

                await PopulateTableFromCoursesAsync(courses);

                dgvCourses.DataSource = _courseData;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi load khóa học: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async Task PopulateTableFromCoursesAsync(List<Course> courses)
        {
            _courseData.Rows.Clear();

            if (courses == null || courses.Count == 0) return;

            using (var http = new HttpClient { Timeout = TimeSpan.FromSeconds(8) })
            {
                foreach (var c in courses)
                {
                    double rating = c.Rating;
                    int totalStudents = c.TotalStudents;
                    Image thumb = null;

                    // thumbnail download/generation (unchanged)
                    if (!string.IsNullOrEmpty(c.Thumbnail))
                    {
                        try
                        {
                            if (Uri.IsWellFormedUriString(c.Thumbnail, UriKind.Absolute))
                            {
                                try
                                {
                                    var data = await http.GetByteArrayAsync(c.Thumbnail);
                                    using (var ms = new MemoryStream(data))
                                    {
                                        thumb = Image.FromStream(ms);
                                    }
                                }
                                catch { thumb = null; }
                            }
                            else if (File.Exists(c.Thumbnail))
                            {
                                try { thumb = Image.FromFile(c.Thumbnail); } catch { thumb = null; }
                            }
                        }
                        catch { thumb = null; }
                    }

                    if (thumb == null)
                    {
                        Color iconColor = Color.FromArgb(255, 165, 0);
                        if (string.Equals(c.Language, "Python", StringComparison.OrdinalIgnoreCase)) iconColor = Color.FromArgb(50, 205, 50);
                        else if (c.Language != null && c.Language.IndexOf("c++", StringComparison.OrdinalIgnoreCase) >= 0) iconColor = Color.FromArgb(255, 69, 0);
                        else if (string.Equals(c.Language, "Java", StringComparison.OrdinalIgnoreCase)) iconColor = Color.FromArgb(128, 0, 128);

                        thumb = GetRoundedRectIcon(iconColor);
                    }

                    bool isEnrolled = c.IsEnrolled;
                    int progress = Math.Max(0, Math.Min(100, c.ProgressPercentage));

                    // NOTE: column order must match EnsureCourseDataTable
                    _courseData.Rows.Add(
                        c.CourseId,
                        c.Title,
                        c.Level ?? "",
                        c.Language ?? "",
                        rating,
                        c.Price,
                        string.IsNullOrEmpty(c.Overview) ? "" : c.Overview,
                        totalStudents,
                        c.Duration,
                        isEnrolled,
                        progress,
                        thumb
                    );
                }
            }
        }

        private async Task ApplyFilterAsync()
        {
            try
            {
                string keyword = string.Empty;
                string selectedLevel = null;

                if (this.InvokeRequired)
                {
                    this.Invoke(new Action(() =>
                    {
                        keyword = txtSearch.Text.Trim();
                        selectedLevel = cmbFilterLevel.SelectedItem?.ToString();
                    }));
                }
                else
                {
                    keyword = txtSearch.Text.Trim();
                    selectedLevel = cmbFilterLevel.SelectedItem?.ToString();
                }

                if (!string.IsNullOrEmpty(selectedLevel) &&
                    (selectedLevel.Equals("Tất cả level", StringComparison.OrdinalIgnoreCase) ||
                     selectedLevel.Equals("All", StringComparison.OrdinalIgnoreCase)))
                {
                    selectedLevel = null;
                }

                Guid currentUserId = Guid.Empty;
                try { currentUserId = GlobalStore.user?.UserID ?? Guid.Empty; } catch { currentUserId = Guid.Empty; }

                List<Course> courses;
                if (currentUserId != Guid.Empty && _courseRepository is CourseRepository repoConcrete)
                {
                    courses = await repoConcrete.GetListHasEnrollAsync(currentUserId);
                }
                else
                {
                    courses = await _courseRepository.GetAllAsync();
                }

                // client-side filter (keeps IsEnrolled/Progress info)
                var filtered = courses.Where(c =>
                    (string.IsNullOrEmpty(keyword) || (c.Title ?? "").IndexOf(keyword, StringComparison.OrdinalIgnoreCase) >= 0) &&
                    (string.IsNullOrEmpty(selectedLevel) || string.Equals(c.Level, selectedLevel, StringComparison.OrdinalIgnoreCase))
                ).ToList();

                EnsureCourseDataTable();
                await PopulateTableFromCoursesAsync(filtered);

                dgvCourses.DataSource = _courseData;

                if (dgvCourses.Rows.Count > 0)
                {
                    dgvCourses.Rows[0].Selected = true;
                    UpdateCoursePreview(dgvCourses.Rows[0]);
                }
                else
                {
                    ClearCoursePreview();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tìm kiếm/lọc: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region Preview Logic

        private void dgvCourses_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvCourses.SelectedRows.Count > 0)
            {
                UpdateCoursePreview(dgvCourses.SelectedRows[0]);
            }
            else
            {
                ClearCoursePreview();
            }
        }

        private void UpdateCoursePreview(DataGridViewRow row)
        {
            // guard: ensure CourseID column exists and is not null
            if (!row.DataGridView.Columns.Contains("CourseID") || row.Cells["CourseID"].Value == DBNull.Value) return;

            _selectedCourseId = (Guid)row.Cells["CourseID"].Value;

            // Title column in the grid is named "colTitle" (DataPropertyName = "Title").
            // Prefer the grid column name first, then fall back to a data column named "Title".
            string title = "";
            if (row.DataGridView.Columns.Contains("colTitle"))
                title = row.Cells["colTitle"].Value?.ToString() ?? "";
            else if (row.DataGridView.Columns.Contains("Title"))
                title = row.Cells["Title"].Value?.ToString() ?? "";

            var level = row.DataGridView.Columns.Contains("colLevel")
                ? row.Cells["colLevel"]?.Value?.ToString() ?? ""
                : (row.DataGridView.Columns.Contains("Level") ? row.Cells["Level"]?.Value?.ToString() ?? "" : "");

            var language = row.DataGridView.Columns.Contains("colLanguage")
                ? row.Cells["colLanguage"]?.Value?.ToString() ?? ""
                : (row.DataGridView.Columns.Contains("Language") ? row.Cells["Language"]?.Value?.ToString() ?? "" : "");

            var rating = 0.0;
            double.TryParse(
                (row.DataGridView.Columns.Contains("colRating") ? row.Cells["colRating"]?.Value?.ToString() : null) ??
                (row.DataGridView.Columns.Contains("Rating") ? row.Cells["Rating"]?.Value?.ToString() : "0"),
                out rating);

            // ✅ Lấy HTML overview từ ShortOverview column
            var overview = row.DataGridView.Columns.Contains("ShortOverview") ? row.Cells["ShortOverview"].Value?.ToString() ?? "" : "";
            
            var students = 0;
            int.TryParse(row.DataGridView.Columns.Contains("TotalStudents") ? row.Cells["TotalStudents"].Value?.ToString() ?? "0" : "0", out students);
            var duration = 0;
            int.TryParse(row.DataGridView.Columns.Contains("Duration") ? row.Cells["Duration"].Value?.ToString() ?? "0" : "0", out duration);

            var cellImg = row.DataGridView.Columns.Contains("colThumbnail") ? row.Cells["colThumbnail"]?.Value as Image
                : (row.DataGridView.Columns.Contains("ThumbnailImage") ? row.Cells["ThumbnailImage"]?.Value as Image : null);

            if (cellImg != null) pbThumbnail.Image = cellImg;
            else pbThumbnail.Image = GetRoundedRectIcon(Color.FromArgb(255, 165, 0), 150, 15);

            lblTitle.Text = title;
            lblMeta.Text = $"{level} | {language} | ⭐️ {rating:N1}";
            
            // ✅ Render HTML overview - hỗ trợ cả RichTextBox và TextBox
            try
            {
                // Nếu txtShortOverview là RichTextBox
                if (txtShortOverview?.GetType().Name == "RichTextBox")
                {
                    dynamic rtb = txtShortOverview;
                    HtmlRenderHelper.RenderHtmlOverviewToRtb(rtb, overview);
                }
                else
                {
                    // Nếu là TextBox - strip HTML tags
                    txtShortOverview.Text = HtmlRenderHelper.StripHtmlTags(overview);
                }
            }
            catch
            {
                // Fallback: hiển thị plain text
                txtShortOverview.Text = HtmlRenderHelper.StripHtmlTags(overview);
            }
            
            lblStudents.Text = $"{students:N0} người";
            lblDuration.Text = $"{duration / 60} giờ";

            bool isEnrolled = false;
            int progress = 0;
            try
            {
                if (row.DataGridView.Columns.Contains("IsEnrolled") && row.Cells["IsEnrolled"].Value != DBNull.Value)
                    isEnrolled = Convert.ToBoolean(row.Cells["IsEnrolled"].Value);

                if (row.DataGridView.Columns.Contains("ProgressPercentage"))
                {
                    var cellValue = row.Cells["ProgressPercentage"].Value;
                    if (cellValue != null && cellValue != DBNull.Value)
                        progress = Convert.ToInt32(cellValue);
                }
            }
            catch { isEnrolled = false; progress = 0; }

            if (isEnrolled)
            {
                btnEnrollContinue.Text = "▶️ Tiếp tục học";
                //btnEnrollContinue.BackColor = Color.FromArgb(0, 177, 64);
                pbCourseProgress.Value = Math.Max(0, Math.Min(100, progress));
                pbCourseProgress.Visible = true;
            }
            else
            {
                btnEnrollContinue.Text = "💰 Đăng ký";
                //btnEnrollContinue.BackColor = Color.FromArgb(0, 120, 215);
                pbCourseProgress.Visible = false;
            }
        }

        private void ClearCoursePreview()
        {
            _selectedCourseId = Guid.Empty;
            lblTitle.Text = "Chọn một khóa học";
            lblMeta.Text = "";
            
            // ✅ Clear cả RichTextBox và TextBox
            try
            {
                if (txtShortOverview?.GetType().Name == "RichTextBox")
                {
                    dynamic rtb = txtShortOverview;
                    rtb.Clear();
                }
                else
                {
                    txtShortOverview.Text = "";
                }
            }
            catch
            {
                txtShortOverview.Text = "";
            }
            
            lblStudents.Text = "";
            lblDuration.Text = "";
            pbThumbnail.Image = null;
            btnEnrollContinue.Text = "Đăng ký";
            //btnEnrollContinue.BackColor = Color.Gray;
        }

        #endregion

        #region Event Handlers (Stubs)

        private void dgvCourses_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                // Hành động: Chuyển đến trang chi tiết
                // MainFormStudent.Instance.NavigateTo(new ucCourseDetails(_selectedCourseId));
            }
        }

        private void btnViewDetails_Click(object sender, EventArgs e)
        {
            if (_selectedCourseId != Guid.Empty)
            {
                // Hành động: Chuyển đến trang chi tiết
                MainFormStudent.Instance.NavigateTo(new ucCourseDetails(_selectedCourseId));
            }
        }

        private async void btnEnrollContinue_Click(object sender, EventArgs e)
        {
            if (_selectedCourseId == Guid.Empty) return;

            var currentUser = GlobalStore.user;
            if (currentUser == null || currentUser.UserID == Guid.Empty)
            {
                MessageBox.Show("Bạn phải đăng nhập để đăng ký hoặc tiếp tục học.", "Yêu cầu đăng nhập", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // UI lock + feedback
            btnEnrollContinue.Enabled = false;
            var originalText = btnEnrollContinue.Text;
            btnEnrollContinue.Text = "Đang xử lý...";
            var prevCursor = Cursor.Current;
            Cursor.Current = Cursors.WaitCursor;

            // create services/repositories locally (or replace with DI if available)
            var enrollmentService = new EnrollmentService(new EnrollmentRepository(), new ProgressRepository());
            var courseRepo = _courseRepository as CourseRepository ?? new CourseRepository();

            try
            {
                AppLogger.LogInfo($"Enroll button clicked. User={currentUser.UserID}, Course={_selectedCourseId}", nameof(btnEnrollContinue_Click));

                bool isEnrolled = false;
                try
                {
                    isEnrolled = enrollmentService.IsUserEnrolled(currentUser.UserID, _selectedCourseId);
                    AppLogger.LogInfo($"IsUserEnrolled returned {isEnrolled} for User={currentUser.UserID}, Course={_selectedCourseId}", nameof(btnEnrollContinue_Click));
                }
                catch (Exception checkEx)
                {
                    AppLogger.LogException(checkEx, "IsUserEnrolled check");
                    // proceed to attempt enrollment (it may still succeed)
                }

                if (!isEnrolled)
                {
                    var confirm = MessageBox.Show("Bạn có muốn đăng ký khóa học này?", "Xác nhận đăng ký", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (confirm != DialogResult.Yes) return;

                    bool enrolled = false;
                    int attempts = 0, maxAttempts = 2;
                    while (attempts < maxAttempts && !enrolled)
                    {
                        attempts++;
                        try
                        {
                            AppLogger.LogInfo($"Attempting enrollment (attempt {attempts}) User={currentUser.UserID}, Course={_selectedCourseId}", nameof(btnEnrollContinue_Click));
                            enrolled = enrollmentService.EnrollUserToCourse(currentUser.UserID, _selectedCourseId);
                            AppLogger.LogInfo($"Enroll result: {enrolled} (attempt {attempts}) User={currentUser.UserID}, Course={_selectedCourseId}", nameof(btnEnrollContinue_Click));
                        }
                        catch (System.Data.SqlClient.SqlException sqlEx)
                        {
                            AppLogger.LogException(sqlEx, $"SQL error on enroll attempt {attempts}");
                            if (attempts >= maxAttempts) throw;
                            await Task.Delay(300);
                        }
                        catch (Exception ex)
                        {
                            AppLogger.LogException(ex, $"Unexpected error on enroll attempt {attempts}");
                            throw;
                        }
                    }

                    if (!enrolled)
                    {
                        AppLogger.LogError($"Enrollment failed after {maxAttempts} attempts for User={currentUser.UserID}, Course={_selectedCourseId}", nameof(btnEnrollContinue_Click));
                        MessageBox.Show("Đăng ký khóa học thất bại. Vui lòng thử lại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    // refresh UI and navigate to learning
                    try { await LoadCoursesAsync(); } catch (Exception refreshEx) { AppLogger.LogException(refreshEx, "LoadCoursesAsync after enroll"); }
                    MainFormStudent.Instance?.NavigateTo(new ucCourseLearning(_selectedCourseId, courseRepo));
                }
                else
                {
                    AppLogger.LogInfo($"User already enrolled. Navigating to learning. User={currentUser.UserID}, Course={_selectedCourseId}", nameof(btnEnrollContinue_Click));
                    MainFormStudent.Instance?.NavigateTo(new ucCourseLearning(_selectedCourseId, courseRepo));
                }
            }
            catch (System.Data.SqlClient.SqlException sqlEx)
            {
                AppLogger.LogException(sqlEx, "Database error during enrollment");
                MessageBox.Show("Lỗi cơ sở dữ liệu. Vui lòng thử lại sau.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                AppLogger.LogException(ex, "Enrollment error");
                MessageBox.Show("Đã xảy ra lỗi. Vui lòng thử lại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                // restore UI
                btnEnrollContinue.Enabled = true;
                btnEnrollContinue.Text = originalText;
                Cursor.Current = prevCursor;
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            // Hành động: Quay lại
            // MainFormStudent.Instance.GoBack();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            // Restart debounce timer
            try
            {
                _searchTimer.Stop();
                _searchTimer.Start();
            }
            catch
            {
                // ignore if timer not ready
            }
        }

        private void cmbFilterLevel_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Apply filter immediately (could debounce similarly if desired)
            _searchTimer.Stop();
            var _ = ApplyFilterAsync();
        }

        #endregion
    }
}