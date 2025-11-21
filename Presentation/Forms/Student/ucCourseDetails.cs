using CodeForge_Desktop.Config;
using CodeForge_Desktop.DataAccess.Entities;
using CodeForge_Desktop.DataAccess.Interfaces;
using CodeForge_Desktop.DataAccess.Repositories;
using CodeForge_Desktop.Business.Interfaces;
using System.Windows.Forms;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using CodeForge_Desktop.Business.Helpers;
using CodeForge_Desktop.Business.Services;

namespace CodeForge_Desktop.Presentation.Forms.Student.UserControls
{
    public partial class ucCourseDetails : UserControl
    {
        private readonly Guid _courseId;
        private readonly ICourseRepository _courseRepository;
        private Course _course;

        private readonly ICourseReviewService _reviewService; // <-- interface field

        // Enrollment service (used to check/create enrollment)
        private readonly IEnrollmentService _enrollmentService;

        private const string NO_INSTRUCTOR = "Giảng viên: Chưa cập nhật";
        private const string NO_REVIEWS = "Chưa có đánh giá cho khóa học này.";
        private const string NO_CURRICULUM = "Chưa có nội dung cho khóa học này.";

        // default ctor used by caller that only supplies courseId
        public ucCourseDetails(Guid courseId) : this(
            courseId,
            new CourseRepository(),
            new CourseReviewService(new CourseReviewRepository(), new EnrollmentRepository()))
        {
        }

        // DI-friendly ctor
        public ucCourseDetails(Guid courseId, ICourseRepository courseRepository, ICourseReviewService reviewService)
        {
            _courseId = courseId;
            _courseRepository = courseRepository ?? new CourseRepository();
            _reviewService = reviewService ?? throw new ArgumentNullException(nameof(reviewService));
            // create enrollment service here (repository-level objects match your DB schema)
            _enrollmentService = new EnrollmentService(new EnrollmentRepository(), new ProgressRepository());

            InitializeComponent();

            WireEvents();
            ConfigureControls();
        }

        private void WireEvents()
        {
            this.Load += ucCourseDetails_Load;
            btnBack.Click += (s, e) => MainFormStudent.Instance?.GoBack();
            btnEnrollStart.Click += btnEnrollStart_Click;

            // wire review submit button if present
            if (btnSubmitReview != null)
                btnSubmitReview.Click += btnSubmitReview_Click;
        }

        private void ConfigureControls()
        {
            // thumbnail
            if (pbThumb != null) pbThumb.SizeMode = PictureBoxSizeMode.Zoom;

            // overview (plain)
            if (rtbOverview != null)
            {
                rtbOverview.ReadOnly = true;
                rtbOverview.BackColor = Color.White;
                rtbOverview.BorderStyle = BorderStyle.FixedSingle;
                rtbOverview.Font = new Font("Segoe UI", 10F);
            }

            // Reviews grid: base style
            if (dgvReviews != null)
            {
                dgvReviews.AllowUserToAddRows = false;
                dgvReviews.AllowUserToDeleteRows = false;
                dgvReviews.ReadOnly = true;
                dgvReviews.RowHeadersVisible = false;
                dgvReviews.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dgvReviews.MultiSelect = false;
                dgvReviews.EnableHeadersVisualStyles = false;
                dgvReviews.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(250, 250, 250);
                dgvReviews.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
                dgvReviews.DefaultCellStyle.Font = new Font("Segoe UI", 9F);
                dgvReviews.RowTemplate.Height = 64;
                dgvReviews.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
                dgvReviews.AutoGenerateColumns = false;
                dgvReviews.BorderStyle = BorderStyle.None;
                dgvReviews.CellFormatting += DgvReviews_CellFormatting;
                dgvReviews.CellPainting += DgvReviews_CellPainting;
                dgvReviews.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(248, 248, 248);
            }

            // Curriculum tree: ensure visible and set image list + owner draw
            if (tvCurriculum != null)
            {
                tvCurriculum.ShowPlusMinus = true;
                tvCurriculum.ShowLines = true;
                tvCurriculum.FullRowSelect = true;
                tvCurriculum.HideSelection = false;
                tvCurriculum.DrawMode = TreeViewDrawMode.OwnerDrawText;
                tvCurriculum.DrawNode += TvCurriculum_DrawNode;

                var il = new ImageList { ImageSize = new Size(16, 16) };
                il.Images.Add("module", GetRoundedRectIcon(Color.FromArgb(236, 239, 241), 16, 3));
                il.Images.Add("video", SystemIcons.Application.ToBitmap());
                il.Images.Add("text", SystemIcons.Information.ToBitmap());
                il.Images.Add("quiz", SystemIcons.Question.ToBitmap());
                il.Images.Add("code", SystemIcons.Shield.ToBitmap());
                // completed icon
                il.Images.Add("completed", GetCheckIcon(16, Color.FromArgb(76, 175, 80)));
                tvCurriculum.ImageList = il;
            }

            // Review inputs: default state
            if (cbRating != null)
            {
                cbRating.SelectedIndex = 0; // default to 5 (first item)
            }
            if (lblReviewHint != null)
            {
                lblReviewHint.Visible = false;
            }
        }

        private void DgvReviews_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            // subtle separator line between rows for cleaner look
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                e.Paint(e.CellBounds, DataGridViewPaintParts.All & ~DataGridViewPaintParts.Border);
                using (var pen = new Pen(Color.FromArgb(230, 230, 230)))
                {
                    var y = e.CellBounds.Bottom - 1;
                    e.Graphics.DrawLine(pen, e.CellBounds.Left, y, e.CellBounds.Right, y);
                }
                e.Handled = true;
            }
        }

        private void DgvReviews_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dgvReviews.Columns[e.ColumnIndex].Name == "colRating" && e.Value != null)
            {
                if (int.TryParse(e.Value.ToString(), out int rating))
                {
                    // render stars as unicode string (max 5)
                    rating = Math.Max(0, Math.Min(5, rating));
                    e.Value = new string('★', rating) + new string('☆', 5 - rating);
                    e.CellStyle.ForeColor = Color.FromArgb(255, 165, 0); // orange stars
                    e.FormattingApplied = true;
                }
            }
        }

        private void TvCurriculum_DrawNode(object sender, DrawTreeNodeEventArgs e)
        {
            var tv = sender as TreeView;
            if (e.Node == null) return;

            var g = e.Graphics;
            var bounds = e.Bounds;
            bool selected = (e.State & TreeNodeStates.Selected) == TreeNodeStates.Selected;

            if (e.Node.Level == 0)
            {
                // module header: draw light background and bold text
                var bg = selected ? Color.FromArgb(220, 235, 255) : Color.FromArgb(243, 246, 249);
                using (var b = new SolidBrush(bg))
                {
                    var rect = new Rectangle(bounds.Left - 4, bounds.Top, tv.ClientRectangle.Width - bounds.Left - 4, bounds.Height);
                    g.FillRectangle(b, rect);
                }
                var font = new Font(tv.Font.FontFamily, tv.Font.Size, FontStyle.Bold);
                TextRenderer.DrawText(g, e.Node.Text, font, new Rectangle(bounds.Left, bounds.Top, bounds.Width, bounds.Height), Color.FromArgb(40, 40, 40), TextFormatFlags.VerticalCenter | TextFormatFlags.Left);
            }
            else
            {
                // lesson row: draw icon (TreeView will draw image), then title and duration right-aligned
                // let default drawing handle image and focus rectangle for simplicity, but draw duration ourselves
                e.DrawDefault = true;
                e.DrawDefault = true;

                // draw duration at right edge
                var lesson = e.Node.Tag as LessonDto;
                if (lesson != null && lesson.Duration > 0)
                {
                    string dur = $"{lesson.Duration} phút";
                    var durRect = new Rectangle(tv.ClientRectangle.Right - 100, bounds.Top, 96, bounds.Height);
                    TextRenderer.DrawText(g, dur, tv.Font, durRect, Color.Gray, TextFormatFlags.VerticalCenter | TextFormatFlags.Right);
                }
            }
        }

        private async void ucCourseDetails_Load(object sender, EventArgs e)
        {
            try
            {
                await LoadCourseAsync();
                await LoadCurriculumAsync();
                await LoadReviewsAsync();

                // initialize review UI access and current user review
                UpdateReviewTabAccess();
                await LoadUserReviewAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải dữ liệu: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async Task LoadCourseAsync()
        {
            try
            {
                _course = await _courseRepository.GetByIdAsync(_courseId);
                if (_course == null)
                {
                    MessageBox.Show("Không tìm thấy khóa học.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                lblCourseTitle.Text = _course.Title ?? "Không có tiêu đề";
                lblInstructor.Text = NO_INSTRUCTOR;
                var hours = _course.Duration > 0 ? Math.Max(1, _course.Duration / 60) : 0;
                lblMetaSmall.Text = $"⭐ {_course.Rating:F1}   •   {_course.TotalStudents:N0} học viên   •   {hours} giờ";

                // ✅ Render HTML overview using helper
                string overview = string.IsNullOrWhiteSpace(_course.Overview) ? (_course.Description ?? "Không có mô tả") : _course.Overview;
                HtmlRenderHelper.RenderHtmlOverviewToRtb(rtbOverview, overview);

                // load thumbnail (file path or online URL)
                await LoadThumbnailAsync(_course.Thumbnail);

                // update enroll button state (based on DB)
                await UpdateEnrollButtonStateAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi load chi tiết khóa học: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async Task UpdateEnrollButtonStateAsync()
        {
            try
            {
                bool enrolled = false;
                int progress = 0;
                var currentUser = GlobalStore.user;
                if (currentUser != null && currentUser.UserID != Guid.Empty)
                {
                    enrolled = await Task.Run(() => _enrollmentService.IsUserEnrolled(currentUser.UserID, _courseId));
                    if (enrolled)
                    {
                        try
                        {
                            // get progress percentage
                            var progressSvc = new ProgressService(new ProgressRepository());
                            double pct = await Task.Run(() => progressSvc.GetProgressPercentage(currentUser.UserID, _courseId));
                            progress = (int)Math.Round(Math.Max(0.0, Math.Min(100.0, pct)));
                        }
                        catch (Exception ex)
                        {
                            AppLogger.LogException(ex, nameof(UpdateEnrollButtonStateAsync));
                            progress = 0;
                        }
                    }
                }

                if (btnEnrollStart.InvokeRequired)
                {
                    btnEnrollStart.Invoke(new Action(() =>
                    {
                        if (enrolled)
                        {
                            btnEnrollStart.Text = "▶️ Tiếp tục học";
                            btnEnrollStart.BackColor = Color.FromArgb(0, 177, 64);
                            // show progress
                            try
                            {
                                pbCourseProgress.Value = Math.Max(0, Math.Min(100, progress));
                                pbCourseProgress.Visible = true;
                            }
                            catch { pbCourseProgress.Visible = true; }
                        }
                        else
                        {
                            btnEnrollStart.Text = "💰 Đăng ký";
                            btnEnrollStart.BackColor = Color.FromArgb(0, 120, 215);
                            pbCourseProgress.Visible = false;
                        }
                        btnEnrollStart.Enabled = true;
                    }));
                }
                else
                {
                    if (enrolled)
                    {
                        btnEnrollStart.Text = "▶️ Tiếp tục học";
                        btnEnrollStart.BackColor = Color.FromArgb(0, 177, 64);
                        try
                        {
                            pbCourseProgress.Value = Math.Max(0, Math.Min(100, progress));
                            pbCourseProgress.Visible = true;
                        }
                        catch { pbCourseProgress.Visible = true; }
                    }
                    else
                    {
                        btnEnrollStart.Text = "💰 Đăng ký";
                        btnEnrollStart.BackColor = Color.FromArgb(0, 120, 215);
                        pbCourseProgress.Visible = false;
                    }
                    btnEnrollStart.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                AppLogger.LogException(ex, nameof(UpdateEnrollButtonStateAsync));
                // leave button default (disabled until user interacts)
                try { btnEnrollStart.Enabled = true; } catch { }
            }
        }

        private async Task LoadThumbnailAsync(string thumbnail)
        {
            try
            {
                if (pbThumb == null) return;
                pbThumb.Image = GetRoundedRectIcon(Color.FromArgb(255, 140, 0), 96);

                if (string.IsNullOrWhiteSpace(thumbnail)) return;

                // if it's a URL, try download; otherwise treat as local path
                if (Uri.IsWellFormedUriString(thumbnail, UriKind.Absolute))
                {
                    try
                    {
                        using (var http = new HttpClient())
                        {
                            http.Timeout = TimeSpan.FromSeconds(8);
                            var data = await http.GetByteArrayAsync(thumbnail);
                            using (var ms = new MemoryStream(data))
                            {
                                var img = Image.FromStream(ms);
                                pbThumb.Image = img;
                                return;
                            }
                        }
                    }
                    catch { }
                }

                if (File.Exists(thumbnail))
                {
                    try { pbThumb.Image = Image.FromFile(thumbnail); }
                    catch { }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("LoadThumbnailAsync: " + ex.Message);
            }
        }

        private async Task LoadCurriculumAsync()
        {
            if (tvCurriculum == null) return;

            tvCurriculum.BeginUpdate();
            tvCurriculum.Nodes.Clear();

            var modules = await Task.Run(() => FetchCurriculumStructuredSafe());

            // apply completed flags for current user
            var currentUser = GlobalStore.user;
            if (currentUser != null && currentUser.UserID != Guid.Empty)
            {
                try
                {
                    var progressRepo = new ProgressRepository();
                    var completedIds = await Task.Run(() => progressRepo.GetCompletedLessonIds(currentUser.UserID, _courseId));
                    if (completedIds != null && completedIds.Count > 0)
                    {
                        foreach (var m in modules)
                            foreach (var l in m.Lessons)
                                l.IsCompleted = completedIds.Contains(l.LessonId);
                    }
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine("Apply completed flags failed: " + ex.Message);
                }
            }

            if (modules == null || modules.Count == 0)
            {
                tvCurriculum.Nodes.Add(new TreeNode(NO_CURRICULUM));
                tvCurriculum.EndUpdate();
                return;
            }

            foreach (var m in modules)
            {
                var moduleNode = new TreeNode($"{m.Title}  •  {m.Lessons.Count} bài")
                {
                    Tag = m,
                    ImageKey = "module",
                    SelectedImageKey = "module",
                    NodeFont = new Font(tvCurriculum.Font, FontStyle.Bold)
                };
        
                foreach (var lesson in m.Lessons)
                {
                    var iconKey = "text";
                    switch ((lesson.LessonType ?? "").ToLowerInvariant())
                    {
                        case "video": iconKey = "video"; break;
                        case "quiz": iconKey = "quiz"; break;
                        case "coding": iconKey = "code"; break;
                    }

                    // if the lesson is completed use the completed icon
                    var nodeKey = lesson.IsCompleted ? "completed" : iconKey;

                    var lessonNode = new TreeNode(lesson.Title)
                    {
                        Tag = lesson,
                        ImageKey = nodeKey,
                        SelectedImageKey = nodeKey
                    };

                    moduleNode.Nodes.Add(lessonNode);
                }

                tvCurriculum.Nodes.Add(moduleNode);
            }

            if (tvCurriculum.Nodes.Count > 0) tvCurriculum.Nodes[0].Expand();
            tvCurriculum.EndUpdate();
        }

        // Use safer fetch: modules -> lessons -> optional enrichment; avoid heavy LEFT JOINs that can fail when tables absent
        private List<ModuleDto> FetchCurriculumStructuredSafe()
        {
            var modules = new List<ModuleDto>();
            try
            {
                string sqlModules = @"
                    SELECT ModuleID, Title, OrderIndex
                    FROM Modules
                    WHERE CourseID = @CourseId
                      AND (
                            NOT EXISTS(SELECT 1 FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'Modules' AND COLUMN_NAME = 'IsDeleted')
                            OR ISNULL(IsDeleted,0) = 0
                          )
                    ORDER BY OrderIndex";
                var dtModules = DbContext.Query(sqlModules, new SqlParameter("@CourseId", _courseId));
                if (dtModules == null || dtModules.Rows.Count == 0) return modules;

                foreach (DataRow mr in dtModules.Rows)
                {
                    if (mr["ModuleID"] == DBNull.Value) continue;
                    var module = new ModuleDto { ModuleId = (Guid)mr["ModuleID"], Title = mr["Title"]?.ToString() ?? "Module" };

                    string sqlLessons = @"
                        SELECT LessonID, Title, LessonType, Duration, OrderIndex
                        FROM Lessons
                        WHERE ModuleID = @ModuleId
                          AND (
                                NOT EXISTS(SELECT 1 FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'Lessons' AND COLUMN_NAME = 'IsDeleted')
                                OR ISNULL(IsDeleted,0) = 0
                              )
                        ORDER BY OrderIndex";
                    var dtLessons = DbContext.Query(sqlLessons, new SqlParameter("@ModuleId", module.ModuleId));
                    if (dtLessons == null) { modules.Add(module); continue; }

                    foreach (DataRow lr in dtLessons.Rows)
                    {
                        if (lr["LessonID"] == DBNull.Value) continue;
                        var lesson = new LessonDto
                        {
                            LessonId = (Guid)lr["LessonID"],
                            Title = lr["Title"]?.ToString() ?? "Lesson",
                            LessonType = lr.Table.Columns.Contains("LessonType") && lr["LessonType"] != DBNull.Value ? lr["LessonType"].ToString() : "text",
                            Duration = lr.Table.Columns.Contains("Duration") && lr["Duration"] != DBNull.Value ? Convert.ToInt32(lr["Duration"]) : 0
                        };

                        try
                        {
                            if (string.Equals(lesson.LessonType, "video", StringComparison.OrdinalIgnoreCase))
                            {
                                var dtV = DbContext.Query("SELECT TOP 1 VideoUrl FROM LessonVideos WHERE LessonID = @L", new SqlParameter("@L", lesson.LessonId));
                                if (dtV?.Rows.Count > 0 && dtV.Rows[0]["VideoUrl"] != DBNull.Value) lesson.ExtraInfo = dtV.Rows[0]["VideoUrl"].ToString();
                            }
                            else if (string.Equals(lesson.LessonType, "text", StringComparison.OrdinalIgnoreCase))
                            {
                                var dtT = DbContext.Query("SELECT TOP 1 Content FROM LessonTexts WHERE LessonID = @L", new SqlParameter("@L", lesson.LessonId));
                                if (dtT?.Rows.Count > 0) lesson.ExtraInfo = dtT.Rows[0]["Content"]?.ToString();
                            }
                            else if (string.Equals(lesson.LessonType, "quiz", StringComparison.OrdinalIgnoreCase))
                            {
                                var dtQ = DbContext.Query("SELECT TOP 1 Title FROM LessonQuizzes WHERE LessonID = @L", new SqlParameter("@L", lesson.LessonId));
                                if (dtQ?.Rows.Count > 0) lesson.ExtraInfo = dtQ.Rows[0]["Title"]?.ToString();
                            }
                            else if (string.Equals(lesson.LessonType, "coding", StringComparison.OrdinalIgnoreCase))
                            {
                                var dtProblems = DbContext.Query(@"
                                    SELECT cp.ProblemID, cp.Title, cp.Difficulty
                                    FROM CodingProblemLessons cpl
                                    INNER JOIN CodingProblems cp ON cpl.ProblemID = cp.ProblemID
                                    WHERE cpl.LessonID = @LessonId
                                      AND (
                                            NOT EXISTS(SELECT 1 FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'CodingProblemLessons' AND COLUMN_NAME = 'IsDeleted')
                                            OR ISNULL(cpl.IsDeleted,0) = 0
                                          )
                                      AND (
                                            NOT EXISTS(SELECT 1 FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'CodingProblems' AND COLUMN_NAME = 'IsDeleted')
                                            OR ISNULL(cp.IsDeleted,0) = 0
                                          )
                                    ORDER BY ISNULL(cpl.OrderIndex, 0)", new SqlParameter("@LessonId", lesson.LessonId));

                                if (dtProblems != null && dtProblems.Rows.Count > 0)
                                {
                                    foreach (DataRow pr in dtProblems.Rows)
                                    {
                                        var p = new CodingProblemInfo
                                        {
                                            ProblemID = pr["ProblemID"] != DBNull.Value ? (Guid)pr["ProblemID"] : Guid.Empty,
                                            Title = pr.Table.Columns.Contains("Title") && pr["Title"] != DBNull.Value ? pr["Title"].ToString() : "(untitled)",
                                            Difficulty = pr.Table.Columns.Contains("Difficulty") && pr["Difficulty"] != DBNull.Value ? pr["Difficulty"].ToString() : null
                                        };
                                        lesson.CodingProblems.Add(p);
                                    }
                                    lesson.ExtraInfo = string.Join(", ", lesson.CodingProblems.Select(x => x.Title));
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            System.Diagnostics.Debug.WriteLine("Enrich lesson failed: " + ex.Message);
                        }

                        module.Lessons.Add(lesson);
                    }

                    modules.Add(module);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("FetchCurriculumStructuredSafe: " + ex.Message);
            }
            return modules;
        }

        private async Task LoadReviewsAsync()
        {
            try
            {
                if (dgvReviews == null) return;

                var dt = await Task.Run(() => FetchReviewsData());

                dgvReviews.SuspendLayout();
                dgvReviews.DataSource = null;
                dgvReviews.Columns.Clear();

                // Build columns explicitly for nice UI
                dgvReviews.AutoGenerateColumns = false;

                dgvReviews.Columns.Add(new DataGridViewTextBoxColumn { Name = "ReviewID", DataPropertyName = "ReviewID", Visible = false });
                dgvReviews.Columns.Add(new DataGridViewTextBoxColumn { Name = "colUser", HeaderText = "User", DataPropertyName = "User", Width = 160 });
                dgvReviews.Columns.Add(new DataGridViewTextBoxColumn { Name = "colRating", HeaderText = "Rating", DataPropertyName = "Rating", Width = 90, DefaultCellStyle = { Alignment = DataGridViewContentAlignment.MiddleCenter } });
                dgvReviews.Columns.Add(new DataGridViewTextBoxColumn { Name = "colComment", HeaderText = "Comment", DataPropertyName = "Comment", AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill, DefaultCellStyle = { WrapMode = DataGridViewTriState.True } });
                dgvReviews.Columns.Add(new DataGridViewTextBoxColumn { Name = "colCreatedAt", HeaderText = "Created", DataPropertyName = "CreatedAt", Width = 110, DefaultCellStyle = { Format = "yyyy-MM-dd", Alignment = DataGridViewContentAlignment.MiddleCenter } });

                if (dt == null || dt.Rows.Count == 0)
                {
                    ShowEmptyReviewsState();
                }
                else
                {
                    dgvReviews.DataSource = dt;
                    dgvReviews.AutoResizeRows(DataGridViewAutoSizeRowsMode.AllCells);
                }

                dgvReviews.ResumeLayout();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi load reviews: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private DataTable FetchReviewsData()
        {
            try
            {
                string sql = @"
                    SELECT cr.ReviewID,
                           ISNULL(u.Username, 'Người dùng ẩn danh') as [User],
                           cr.Rating,
                           cr.Comment,
                           cr.CreatedAt
                    FROM CourseReviews cr
                    LEFT JOIN Users u ON cr.UserID = u.UserID
                    WHERE cr.CourseID = @CourseId
                    ORDER BY cr.CreatedAt DESC";

                return DbContext.Query(sql, new SqlParameter("@CourseId", _courseId));
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("FetchReviewsData: " + ex.Message);
                return null;
            }
        }

        private void ShowEmptyReviewsState()
        {
            var emptyTable = new DataTable();
            emptyTable.Columns.Add("Message");
            var row = emptyTable.NewRow();
            row["Message"] = NO_REVIEWS;
            emptyTable.Rows.Add(row);

            dgvReviews.DataSource = emptyTable;
            if (dgvReviews.Columns.Count > 0)
            {
                dgvReviews.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgvReviews.Columns[0].DefaultCellStyle.ForeColor = Color.Gray;
            }
        }

        private async void btnEnrollStart_Click(object sender, EventArgs e)
        {
            if (_course == null) return;

            var currentUser = GlobalStore.user;
            if (currentUser == null || currentUser.UserID == Guid.Empty)
            {
                MessageBox.Show("Bạn phải đăng nhập để đăng ký hoặc tiếp tục học.", "Yêu cầu đăng nhập", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // UI lock + feedback
            btnEnrollStart.Enabled = false;
            var originalText = btnEnrollStart.Text;
            var prevCursor = Cursor.Current;
            Cursor.Current = Cursors.WaitCursor;

            try
            {
                AppLogger.LogInfo($"Course details enroll clicked. User={currentUser.UserID}, Course={_courseId}", nameof(btnEnrollStart_Click));

                bool isEnrolled = false;
                try { isEnrolled = await Task.Run(() => _enrollmentService.IsUserEnrolled(currentUser.UserID, _courseId)); }
                catch (Exception chkEx) { AppLogger.LogException(chkEx, "IsUserEnrolled check"); }

                var courseRepo = _courseRepository as CourseRepository ?? new CourseRepository();

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
                            AppLogger.LogInfo($"Attempting enrollment (attempt {attempts}) User={currentUser.UserID}, Course={_courseId}", nameof(btnEnrollStart_Click));
                            enrolled = await Task.Run(() => _enrollmentService.EnrollUserToCourse(currentUser.UserID, _courseId));
                            AppLogger.LogInfo($"Enroll result: {enrolled} (attempt {attempts}) User={currentUser.UserID}, Course={_courseId}", nameof(btnEnrollStart_Click));
                        }
                        catch (SqlException sqlEx)
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
                        AppLogger.LogError($"Enrollment failed after {maxAttempts} attempts for User={currentUser.UserID}, Course={_courseId}", nameof(btnEnrollStart_Click));
                        MessageBox.Show("Đăng ký khóa học thất bại. Vui lòng thử lại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    // refresh UI so IsEnrolled / Progress show correctly
                    try { await LoadCourseAsync(); } catch (Exception refreshEx) { AppLogger.LogException(refreshEx, "LoadCourseAsync after enroll"); }

                    // navigate to learning page
                    MainFormStudent.Instance?.NavigateTo(new ucCourseLearning(_courseId, courseRepo));
                }
                else
                {
                    // already enrolled -> go straight to learning
                    MainFormStudent.Instance?.NavigateTo(new ucCourseLearning(_courseId, courseRepo));
                }
            }
            catch (SqlException sqlEx)
            {
                AppLogger.LogException(sqlEx, "Database error during enrollment from details");
                MessageBox.Show("Lỗi cơ sở dữ liệu. Vui lòng thử lại sau.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                AppLogger.LogException(ex, "Enrollment error from details");
                MessageBox.Show("Đã xảy ra lỗi. Vui lòng thử lại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                // restore UI
                btnEnrollStart.Enabled = true;
                btnEnrollStart.Text = originalText;
                Cursor.Current = prevCursor;
            }
        }

        // Review UI helpers (NEW)
        private void UpdateReviewTabAccess()
        {
            // Review panel functionality removed - controls not defined in designer
            // This method is intentionally left empty as review input controls are not part of the UI

            // Keep the comment above for history; implement access logic below.
            try
            {
                var currentUser = GlobalStore.user;
                bool canReview = false;
                if (currentUser != null && currentUser.UserID != Guid.Empty)
                {
                    try
                    {
                        canReview = _reviewService?.CanReviewCourse(currentUser.UserID, _courseId) ?? false;
                    }
                    catch (Exception ex)
                    {
                        AppLogger.LogException(ex, nameof(UpdateReviewTabAccess));
                        canReview = false;
                    }
                }

                // if controls are present, enable/disable accordingly
                if (cbRating != null) cbRating.Enabled = canReview;
                if (txtComment != null) txtComment.Enabled = canReview;
                if (btnSubmitReview != null) btnSubmitReview.Enabled = canReview;

                if (lblReviewHint != null)
                {
                    lblReviewHint.Visible = !canReview;
                    lblReviewHint.Text = canReview ? string.Empty : "Bạn phải đăng ký để đánh giá.";
                }
            }
            catch (Exception ex)
            {
                AppLogger.LogException(ex, nameof(UpdateReviewTabAccess));
            }
        }

        private async Task LoadUserReviewAsync()
        {
            // Review loading functionality removed - controls not defined in designer
            // This method is intentionally left empty as review input controls are not part of the UI

            // Keep previous comment above; implement actual load below.
            try
            {
                var currentUser = GlobalStore.user;
                if (currentUser == null || currentUser.UserID == Guid.Empty) return;
                if (_reviewService == null) return;

                CourseReview existing = null;
                try
                {
                    existing = await Task.Run(() => _reviewService.GetUserReview(currentUser.UserID, _courseId));
                }
                catch (Exception ex)
                {
                    AppLogger.LogException(ex, nameof(LoadUserReviewAsync));
                }

                if (existing != null)
                {
                    if (cbRating != null)
                    {
                        // service stores rating as 1..5; combobox items are strings "5","4",...
                        var str = existing.Rating.ToString();
                        var idx = cbRating.Items.IndexOf(str);
                        if (idx >= 0) cbRating.SelectedIndex = idx;
                    }
                    if (txtComment != null) txtComment.Text = existing.Comment ?? string.Empty;
                }
            }
            catch (Exception ex)
            {
                AppLogger.LogException(ex, nameof(LoadUserReviewAsync));
            }
        }

        private async void btnSubmitReview_Click(object sender, EventArgs e)
        {
            // Review submission functionality removed - controls not defined in designer
            // This method is intentionally left empty as review input controls are not part of the UI

            // Implement submission using ICourseReviewService (synchronous service wrapped in Task.Run)
            try
            {
                var currentUser = GlobalStore.user;
                if (currentUser == null || currentUser.UserID == Guid.Empty)
                {
                    MessageBox.Show("Bạn phải đăng nhập để gửi đánh giá.", "Yêu cầu đăng nhập", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (_reviewService == null)
                {
                    MessageBox.Show("Không thể gửi đánh giá tại thời điểm này.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (cbRating == null || cbRating.SelectedItem == null)
                {
                    MessageBox.Show("Vui lòng chọn số sao (1 - 5).", "Thiếu thông tin", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (int.TryParse(cbRating.SelectedItem.ToString(), out int rating))
                {
                    // rating accepted (1..5)
                }
                else
                {
                    MessageBox.Show("Giá trị đánh giá không hợp lệ.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                rating = int.Parse(cbRating.SelectedItem.ToString());
                var comment = txtComment?.Text?.Trim();

                // UI feedback
                btnSubmitReview.Enabled = false;
                var prevCursor = Cursor.Current;
                Cursor.Current = Cursors.WaitCursor;

                bool result = false;
                try
                {
                    // call service on background thread to avoid UI hang
                    result = await Task.Run(() => _reviewService.SubmitReview(currentUser.UserID, _courseId, rating, comment));
                }
                catch (Exception ex)
                {
                    AppLogger.LogException(ex, nameof(btnSubmitReview_Click));
                    result = false;
                }

                if (result)
                {
                    MessageBox.Show("Gửi đánh giá thành công.", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    await LoadReviewsAsync();
                    UpdateReviewTabAccess();
                }
                else
                {
                    MessageBox.Show("Không thể gửi đánh giá. Kiểm tra điều kiện (phải đăng ký khóa học) hoặc thử lại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                AppLogger.LogException(ex, nameof(btnSubmitReview_Click));
                MessageBox.Show("Đã xảy ra lỗi khi gửi đánh giá.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                try { btnSubmitReview.Enabled = true; } catch { }
                Cursor.Current = Cursors.Default;
            }
        }

        // Reuse helper for nice thumbnail placeholder
        public System.Drawing.Image GetRoundedRectIcon(System.Drawing.Color color, int size = 96, int radius = 12)
        {
            var bmp = new System.Drawing.Bitmap(size, size);
            using (var g = System.Drawing.Graphics.FromImage(bmp))
            {
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                var rect = new System.Drawing.Rectangle(0, 0, size, size);
                var diameter = radius * 2;
                using (var path = new System.Drawing.Drawing2D.GraphicsPath())
                {
                    var arc = new System.Drawing.Rectangle(rect.Location, new System.Drawing.Size(diameter, diameter));
                    path.AddArc(arc, 180, 90);
                    arc.X = rect.Right - diameter;
                    path.AddArc(arc, 270, 90);
                    arc.Y = rect.Bottom - diameter;
                    path.AddArc(arc, 0, 90);
                    arc.X = rect.Left;
                    arc.Y = rect.Bottom - diameter;
                    path.AddArc(arc, 90, 90);   
                    path.CloseFigure();
                    g.FillPath(new System.Drawing.SolidBrush(color), path);             
                }
            }
            return bmp;
        }

        // small green check icon generator (used for "completed")
        private Image GetCheckIcon(int size, Color color)
        {
            var bmp = new Bitmap(size, size);
            using (var g = Graphics.FromImage(bmp))
            using (var b = new SolidBrush(color))
            using (var pen = new Pen(Color.White, Math.Max(1, size / 8)))
            {
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                g.FillEllipse(b, 0, 0, size - 1, size - 1);
                var p1 = new PointF(size * 0.25f, size * 0.55f);
                var p2 = new PointF(size * 0.45f, size * 0.75f);
                var p3 = new PointF(size * 0.78f, size * 0.28f);
                g.DrawLines(pen, new[] { p1, p2, p3 });
            }
            return bmp;
        }

        // DTOs
        private class ModuleDto
        {
            public Guid ModuleId { get; set; }
            public string Title { get; set; }
            public List<LessonDto> Lessons { get; } = new List<LessonDto>();
        }

        private class LessonDto
        {
            public Guid LessonId { get; set; }
            public string Title { get; set; }
            public string LessonType { get; set; }
            public int Duration { get; set; } // minutes
            public string ExtraInfo { get; set; }
            public List<CodingProblemInfo> CodingProblems { get; } = new List<CodingProblemInfo>();

            // new: completion flag for current user (set by UI layer using ProgressRepository)
            public bool IsCompleted { get; set; } = false;
        }

        private class CodingProblemInfo
        {
            public Guid ProblemID { get; set; } = Guid.Empty;
            public string Title { get; set; }
            public string Difficulty { get; set; }
        }
    }
}