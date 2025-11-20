using CodeForge_Desktop.Config;
using CodeForge_Desktop.DataAccess.Entities;
using CodeForge_Desktop.DataAccess.Interfaces;
using CodeForge_Desktop.DataAccess.Repositories;
using CodeForge_Desktop.Presentation.Forms.Student.UserControls;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CodeForge_Desktop.Presentation.Forms.Student.UserControls
{
    public partial class ucCourseDetails : UserControl
    {
        private readonly Guid _courseId;
        private readonly ICourseRepository _courseRepository;
        private Course _course;

        private const string NO_INSTRUCTOR = "Giảng viên: Chưa cập nhật";
        private const string NO_REVIEWS = "Chưa có đánh giá cho khóa học này.";
        private const string NO_CURRICULUM = "Chưa có nội dung cho khóa học này.";

        public ucCourseDetails(Guid courseId) : this(courseId, new CourseRepository())
        {
        }

        public ucCourseDetails(Guid courseId, ICourseRepository courseRepository)
        {
            _courseId = courseId;
            _courseRepository = courseRepository ?? new CourseRepository();
            InitializeComponent();

            WireEvents();
            ConfigureControls();
        }

        private void WireEvents()
        {
            this.Load += ucCourseDetails_Load;
            btnBack.Click += (s, e) => MainFormStudent.Instance?.GoBack();
            btnEnrollStart.Click += btnEnrollStart_Click;
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
                tvCurriculum.ImageList = il;
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

                // set overview
                rtbOverview.Text = string.IsNullOrWhiteSpace(_course.Overview) ? (_course.Description ?? "Không có mô tả") : _course.Overview;

                // load thumbnail (file path or online URL)
                await LoadThumbnailAsync(_course.Thumbnail);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi load chi tiết khóa học: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

                    var lessonNode = new TreeNode(lesson.Title)
                    {
                        Tag = lesson,
                        ImageKey = iconKey,
                        SelectedImageKey = iconKey
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
                    WHERE CourseID = @CourseId AND IsDeleted = 0
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
                        WHERE ModuleID = @ModuleId AND IsDeleted = 0
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

                        // enrich per-type safely (try/catch)
                        try
                        {
                            if (string.Equals(lesson.LessonType, "video", StringComparison.OrdinalIgnoreCase))
                            {
                                var dtV = DbContext.Query("SELECT TOP 1 VideoUrl FROM LessonVideo WHERE LessonID = @L", new SqlParameter("@L", lesson.LessonId));
                                if (dtV?.Rows.Count > 0 && dtV.Rows[0]["VideoUrl"] != DBNull.Value) lesson.ExtraInfo = dtV.Rows[0]["VideoUrl"].ToString();
                            }
                            else if (string.Equals(lesson.LessonType, "text", StringComparison.OrdinalIgnoreCase))
                            {
                                var dtT = DbContext.Query("SELECT TOP 1 Content FROM LessonText WHERE LessonID = @L", new SqlParameter("@L", lesson.LessonId));
                                if (dtT?.Rows.Count > 0) lesson.ExtraInfo = dtT.Rows[0]["Content"]?.ToString();
                            }
                            else if (string.Equals(lesson.LessonType, "quiz", StringComparison.OrdinalIgnoreCase))
                            {
                                var dtQ = DbContext.Query("SELECT TOP 1 Title FROM LessonQuiz WHERE LessonID = @L", new SqlParameter("@L", lesson.LessonId));
                                if (dtQ?.Rows.Count > 0) lesson.ExtraInfo = dtQ.Rows[0]["Title"]?.ToString();
                            }
                            else if (string.Equals(lesson.LessonType, "coding", StringComparison.OrdinalIgnoreCase))
                            {
                                var dtProblems = DbContext.Query(@"
                                    SELECT cp.ProblemID, cp.Title, cp.Difficulty
                                    FROM CodingProblemLessons cpl
                                    INNER JOIN CodingProblems cp ON cpl.ProblemID = cp.ProblemID
                                    WHERE cpl.LessonID = @LessonId AND cpl.IsDeleted = 0 AND (cp.IsDeleted = 0 OR cp.IsDeleted IS NULL)
                                    ORDER BY cpl.OrderIndex", new SqlParameter("@LessonId", lesson.LessonId));
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

        private void btnEnrollStart_Click(object sender, EventArgs e)
        {
            if (_course == null) return;
            MessageBox.Show($"Đăng ký / Bắt đầu: {_course.Title}", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
        }

        private class CodingProblemInfo
        {
            public Guid ProblemID { get; set; }
            public string Title { get; set; }
            public string Difficulty { get; set; }
        }
    }
}