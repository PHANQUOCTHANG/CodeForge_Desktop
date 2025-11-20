using CodeForge_Desktop.Config;
using CodeForge_Desktop.DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.Json;

namespace CodeForge_Desktop.Presentation.Forms.Student.UserControls
{
    public partial class ucCourseLearning : UserControl
    {
        private readonly Guid _courseId;
        private readonly CourseRepository _courseRepository;
        private List<ModuleDto> _modules; // ordered modules + lessons
        private LessonDto _currentLesson;
        private HashSet<Guid> _completedLessonIds = new HashSet<Guid>();
        private bool _isQuizLoaded = false; // FIX 2: Track if quiz content is loaded

        // runtime WebView2 instance if available (created via reflection)
        private object _webView2Instance;
        private Type _webView2Type;

        public ucCourseLearning(Guid courseId) : this(courseId, new CourseRepository())
        {
        }

        public ucCourseLearning(Guid courseId, CourseRepository courseRepository)
        {
            _courseId = courseId;
            _courseRepository = courseRepository ?? new CourseRepository();
            InitializeComponent();
            WireEvents();
            InitTreeImageList();
        }

        private void WireEvents()
        {
            this.Load += UcCourseLearning_Load;
            btnBack.Click += (s, e) => MainFormStudent.Instance?.GoBack();
            btnPrev.Click += BtnPrev_Click;
            btnNext.Click += BtnNext_Click;
            btnMarkCompleted.Click += BtnMarkCompleted_Click;
            tvModulesLessons.AfterSelect += TvModulesLessons_AfterSelect;
            btnSubmitQuiz.Click += BtnSubmitQuiz_Click;
            flpQuizQuestions.AutoScroll = true;
            lvCodingProblems.DoubleClick += LvCodingProblems_DoubleClick;
        }

        private void InitTreeImageList()
        {
            try
            {
                var il = new ImageList { ImageSize = new Size(20, 20) };
                il.Images.Add("module", GetModuleIcon(20));
                il.Images.Add("video", GetVideoIcon(20));
                il.Images.Add("text", GetTextIcon(20));
                il.Images.Add("quiz", GetQuizIcon(20));
                il.Images.Add("code", GetCodeIcon(20));
                il.Images.Add("completed", GetCheckIcon(20, Color.FromArgb(76, 175, 80)));
                tvModulesLessons.ImageList = il;
            }
            catch { /* ignore image issues */ }
        }

        // FIX 3: Improved icon designs
        private Image GetModuleIcon(int size)
        {
            var bmp = new Bitmap(size, size);
            using (var g = Graphics.FromImage(bmp))
            using (var brush = new SolidBrush(Color.FromArgb(63, 81, 181))) // Indigo
            using (var borderPen = new Pen(Color.FromArgb(33, 150, 243), 2)) // Blue
            {
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                var rect = new Rectangle(2, 2, size - 4, size - 4);
                g.FillRectangle(brush, rect);
                g.DrawRectangle(borderPen, rect);
            }
            return bmp;
        }

        private Image GetVideoIcon(int size)
        {
            var bmp = new Bitmap(size, size);
            using (var g = Graphics.FromImage(bmp))
            using (var brush = new SolidBrush(Color.FromArgb(244, 67, 54))) // Red
            {
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                // Draw rounded rectangle background
                var rect = new Rectangle(1, 2, size - 2, size - 4);
                var path = new System.Drawing.Drawing2D.GraphicsPath();
                int radius = 3;
                path.AddArc(rect.Left, rect.Top, radius, radius, 180, 90);
                path.AddArc(rect.Right - radius, rect.Top, radius, radius, 270, 90);
                path.AddArc(rect.Right - radius, rect.Bottom - radius, radius, radius, 0, 90);
                path.AddArc(rect.Left, rect.Bottom - radius, radius, radius, 90, 90);
                path.CloseFigure();
                g.FillPath(brush, path);
                
                // Draw play triangle
                var pts = new[] {
                    new PointF(size * 0.35f, size * 0.3f),
                    new PointF(size * 0.35f, size * 0.7f),
                    new PointF(size * 0.65f, size * 0.5f)
                };
                g.FillPolygon(Brushes.White, pts);
            }
            return bmp;
        }

        private Image GetTextIcon(int size)
        {
            var bmp = new Bitmap(size, size);
            using (var g = Graphics.FromImage(bmp))
            using (var brush = new SolidBrush(Color.FromArgb(0, 150, 136))) // Teal
            using (var linePen = new Pen(Color.White, 1.5f))
            {
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                var rect = new Rectangle(2, 2, size - 4, size - 4);
                g.FillRectangle(brush, rect);
                
                // Draw lines representing text
                int lineY = 6;
                for (int i = 0; i < 3; i++)
                {
                    g.DrawLine(linePen, 5, lineY, size - 5, lineY);
                    lineY += 4;
                }
            }
            return bmp;
        }

        private Image GetQuizIcon(int size)
        {
            var bmp = new Bitmap(size, size);
            using (var g = Graphics.FromImage(bmp))
            using (var brush = new SolidBrush(Color.FromArgb(255, 152, 0))) // Amber
            using (var qPen = new Pen(Color.White, 2))
            {
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                var rect = new Rectangle(2, 2, size - 4, size - 4);
                g.FillEllipse(brush, rect);
                
                // Draw question mark
                g.DrawString("?", new Font("Segoe UI", size * 0.6f, FontStyle.Bold), Brushes.White, 
                    size * 0.15f, size * 0.15f);
            }
            return bmp;
        }

        private Image GetCodeIcon(int size)
        {
            var bmp = new Bitmap(size, size);
            using (var g = Graphics.FromImage(bmp))
            using (var brush = new SolidBrush(Color.FromArgb(76, 175, 80))) // Green
            using (var linePen = new Pen(Color.White, 1.5f))
            {
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                var rect = new Rectangle(2, 2, size - 4, size - 4);
                g.FillRectangle(brush, rect);
                
                // Draw angle brackets < >
                g.DrawLine(linePen, size * 0.25f, size * 0.35f, size * 0.4f, size * 0.5f);
                g.DrawLine(linePen, size * 0.4f, size * 0.5f, size * 0.25f, size * 0.65f);
                g.DrawLine(linePen, size * 0.6f, size * 0.35f, size * 0.75f, size * 0.5f);
                g.DrawLine(linePen, size * 0.75f, size * 0.5f, size * 0.6f, size * 0.65f);
            }
            return bmp;
        }

        private Image GetCheckIcon(int size, Color color)
        {
            var bmp = new Bitmap(size, size);
            using (var g = Graphics.FromImage(bmp))
            using (var b = new SolidBrush(color))
            using (var pen = new Pen(Color.White, 2.5f))
            {
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                g.FillEllipse(b, 0, 0, size - 1, size - 1);
                // draw check
                var p1 = new PointF(size * 0.25f, size * 0.55f);
                var p2 = new PointF(size * 0.45f, size * 0.75f);
                var p3 = new PointF(size * 0.78f, size * 0.28f);
                g.DrawLines(pen, new[] { p1, p2, p3 });
            }
            return bmp;
        }

        private async void UcCourseLearning_Load(object sender, EventArgs e)
        {
            await LoadModulesAndLessonsAsync();
            await LoadCompletedSetAsync();
            PopulateTree(); // re-populate with completed info
            var first = FindFirstLessonNode();
            if (first != null) tvModulesLessons.SelectedNode = first;
            UpdateCourseLessonInfo();
            TryPrepareWebView2Type(); // preload WebView2 type if available
        }

        private async Task LoadModulesAndLessonsAsync()
        {
            try
            {
                _modules = await Task.Run(() => FetchModulesLessons(_courseId));
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải chương trình học: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private List<ModuleDto> FetchModulesLessons(Guid courseId)
        {
            var modules = new List<ModuleDto>();
            try
            {
                string sqlModules = @"
                    SELECT ModuleID, Title, OrderIndex
                    FROM Modules
                    WHERE CourseID = @CourseId AND IsDeleted = 0
                    ORDER BY OrderIndex";
                var dtModules = DbContext.Query(sqlModules, new SqlParameter("@CourseId", courseId));
                if (dtModules == null) return modules;

                foreach (DataRow mr in dtModules.Rows)
                {
                    if (mr["ModuleID"] == DBNull.Value) continue;
                    var mod = new ModuleDto
                    {
                        ModuleId = (Guid)mr["ModuleID"],
                        Title = mr["Title"]?.ToString() ?? "Module"
                    };

                    string sqlLessons = @"
                        SELECT LessonID, Title, LessonType, Duration, OrderIndex
                        FROM Lessons
                        WHERE ModuleID = @ModuleId AND IsDeleted = 0
                        ORDER BY OrderIndex";
                    var dtLessons = DbContext.Query(sqlLessons, new SqlParameter("@ModuleId", mod.ModuleId));
                    if (dtLessons != null)
                    {
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
                            mod.Lessons.Add(lesson);
                        }
                    }

                    modules.Add(mod);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("FetchModulesLessons: " + ex.Message);
            }
            return modules;
        }

        private void PopulateTree()
        {
            if (tvModulesLessons.InvokeRequired)
            {
                tvModulesLessons.Invoke(new Action(PopulateTree));
                return;
            }

            tvModulesLessons.BeginUpdate();
            tvModulesLessons.Nodes.Clear();

            if (_modules == null || _modules.Count == 0)
            {
                tvModulesLessons.Nodes.Add(new TreeNode("Chưa có nội dung"));
                tvModulesLessons.EndUpdate();
                return;
            }

            foreach (var m in _modules)
            {
                var mn = new TreeNode($"{m.Title}") { Tag = m, ImageKey = "module", SelectedImageKey = "module", NodeFont = new Font(tvModulesLessons.Font, FontStyle.Bold) };
                foreach (var l in m.Lessons)
                {
                    var key = "text";
                    if (!string.IsNullOrEmpty(l.LessonType))
                    {
                        switch (l.LessonType.ToLowerInvariant())
                        {
                            case "video": key = "video"; break;
                            case "quiz": key = "quiz"; break;
                            case "coding": key = "code"; break;
                        }
                    }
                    var ln = new TreeNode(l.Title) { Tag = l, ImageKey = _completedLessonIds.Contains(l.LessonId) ? "completed" : key, SelectedImageKey = _completedLessonIds.Contains(l.LessonId) ? "completed" : key };
                    mn.Nodes.Add(ln);
                }
                tvModulesLessons.Nodes.Add(mn);
            }

            tvModulesLessons.EndUpdate();
        }

        private async Task LoadCompletedSetAsync()
        {
            try
            {
                var set = await Task.Run(() => FetchCompletedLessonIds(_courseId));
                _completedLessonIds = set ?? new HashSet<Guid>();
            }
            catch { _completedLessonIds = new HashSet<Guid>(); }
        }

        private HashSet<Guid> FetchCompletedLessonIds(Guid courseId)
        {
            try
            {
                var set = new HashSet<Guid>();
                var dt = DbContext.Query("SELECT LessonID FROM Progress WHERE CourseID = @C AND IsCompleted = 1", new SqlParameter("@C", courseId));
                if (dt != null)
                {
                    foreach (DataRow r in dt.Rows)
                    {
                        if (r["LessonID"] != DBNull.Value) set.Add((Guid)r["LessonID"]);
                    }
                }
                return set;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("FetchCompletedLessonIds: " + ex.Message);
                return new HashSet<Guid>();
            }
        }

        private async void TvModulesLessons_AfterSelect(object sender, TreeViewEventArgs e)
        {
            var tag = e.Node?.Tag as LessonDto;
            if (tag == null)
            {
                // module node selected
                return;
            }
            await DisplayLessonAsync(tag);
        }

        private async Task DisplayLessonAsync(LessonDto lesson)
        {
            _isQuizLoaded = false; // FIX 2: Reset quiz loaded flag
            
            try
            {
                _currentLesson = lesson;
                lblLessonTitle.Text = lesson.Title ?? "Lesson";
                // hide all content panels first
                rtbLessonText.Visible = false;
                wbVideo.Visible = false;
                pnlQuiz.Visible = false;
                lvCodingProblems.Visible = false;
                flpQuizQuestions.Controls.Clear();

                switch ((lesson.LessonType ?? "text").ToLowerInvariant())
                {
                    case "video":
                        await HandleVideoLessonAsync(lesson);
                        break;

                    case "quiz":
                        pnlQuiz.Visible = true;
                        var quiz = await Task.Run(() => FetchQuizQuestions(lesson.LessonId));
                        if (quiz != null && quiz.Rows.Count > 0)
                        {
                            BuildQuizUi(quiz);
                            _isQuizLoaded = true; // FIX 2: Mark quiz as loaded
                        }
                        else
                        {
                            flpQuizQuestions.Controls.Add(new Label { Text = "Không có câu hỏi.", AutoSize = true });
                            _isQuizLoaded = true;
                        }
                        break;

                    case "coding":
                        var probs = await Task.Run(() => FetchCodingProblemsForLesson(lesson.LessonId));
                        lvCodingProblems.Items.Clear();
                        if (probs != null && probs.Rows.Count > 0)
                        {
                            foreach (DataRow pr in probs.Rows)
                            {
                                var title = pr.Table.Columns.Contains("Title") && pr["Title"] != DBNull.Value ? pr["Title"].ToString() : "(untitled)";
                                var item = new ListViewItem(title) { Tag = pr };
                                lvCodingProblems.Items.Add(item);
                            }
                            lvCodingProblems.Visible = true;
                        }
                        else
                        {
                            rtbLessonText.Visible = true;
                            rtbLessonText.Text = "Coding lesson. No problems linked.";
                        }
                        break;

                    default:
                        var text = await Task.Run(() => FetchLessonText(lesson.LessonId));
                        rtbLessonText.Visible = true;
                        // Render HTML content
                        if (!string.IsNullOrEmpty(text) && text.Contains("<"))
                        {
                            RenderHtmlContent(text);
                        }
                        else
                        {
                            rtbLessonText.Text = string.IsNullOrEmpty(text) ? "(No content)" : text;
                        }
                        break;
                }

                UpdateCourseLessonInfo();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi hiển thị bài học: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // FIX 1: Improved video handling
        private async Task HandleVideoLessonAsync(LessonDto lesson)
        {
            var vid = await Task.Run(() => FetchLessonVideoUrl(lesson.LessonId));
            
            if (string.IsNullOrEmpty(vid))
            {
                rtbLessonText.Visible = true;
                rtbLessonText.Text = "Video lesson. URL not available.";
                return;
            }

            // Try WebView2 first if URL is from a known provider
            if (IsVideoUrl(vid) && TryUseWebView2Navigate(vid))
            {
                wbVideo.Visible = false;
                return;
            }

            // Fallback to WebBrowser with HTML embed
            wbVideo.Visible = true;
            try
            {
                if (IsEmbeddableVideoUrl(vid))
                {
                    string html = GenerateVideoHtml(vid);
                    wbVideo.DocumentText = html;
                }
                else
                {
                    wbVideo.Navigate(vid);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Video navigate failed: " + ex.Message);
                rtbLessonText.Visible = true;
                rtbLessonText.Text = $"Video URL: {System.Net.WebUtility.HtmlEncode(vid)}\n\nCouldn't load video. Open link in browser manually.";
            }
        }

        // FIX 1: Helper methods for video handling
        private bool IsVideoUrl(string url)
        {
            if (string.IsNullOrEmpty(url)) return false;
            url = url.ToLowerInvariant();
            return url.Contains("youtube") || url.Contains("vimeo") || url.Contains("mp4") || 
                   url.Contains("webm") || url.Contains("video") || url.StartsWith("http");
        }

        private bool IsEmbeddableVideoUrl(string url)
        {
            url = url.ToLowerInvariant();
            return url.Contains("youtube") || url.Contains("vimeo");
        }

        private string GenerateVideoHtml(string videoUrl)
        {
            string embedHtml = "";
            
            // YouTube embed
            if (videoUrl.Contains("youtube.com") || videoUrl.Contains("youtu.be"))
            {
                string videoId = ExtractYoutubeId(videoUrl);
                if (!string.IsNullOrEmpty(videoId))
                {
                    embedHtml = $@"<iframe width='100%' height='420' src='https://www.youtube.com/embed/{System.Net.WebUtility.UrlEncode(videoId)}' 
                        frameborder='0' allow='accelerometer; autoplay; clipboard-write; encrypted-media; gyroscope; picture-in-picture' 
                        allowfullscreen></iframe>";
                }
                else
                {
                    embedHtml = @"<p>Invalid YouTube URL format</p>";
                }
            }
            // Vimeo embed
            else if (videoUrl.Contains("vimeo.com"))
            {
                string videoId = ExtractVimeoId(videoUrl);
                if (!string.IsNullOrEmpty(videoId))
                {
                    embedHtml = $@"<iframe src='https://player.vimeo.com/video/{System.Net.WebUtility.UrlEncode(videoId)}' width='100%' height='420' 
                        frameborder='0' allow='autoplay; fullscreen; picture-in-picture' allowfullscreen></iframe>";
                }
                else
                {
                    embedHtml = @"<p>Invalid Vimeo URL format</p>";
                }
            }
            else
            {
                embedHtml = $@"<video width='100%' height='420' controls>
                    <source src='{System.Net.WebUtility.HtmlEncode(videoUrl)}' type='video/mp4'>
                    Your browser does not support the video tag.
                </video>";  
            }

            return $@"<!DOCTYPE html>
            <html>
            <head>
                <style>
                    body {{ margin: 0; padding: 10px; background: #f5f5f5; font-family: Segoe UI, sans-serif; }}
                    .video-container {{ width: 100%; max-width: 100%; }}
                </style>
            </head>
            <body>
                <div class='video-container'>
                    {embedHtml}
                </div>
            </body>
            </html>";
        }

        private string ExtractYoutubeId(string url)
        {
            try
            {
                if (url.Contains("youtu.be"))
                {
                    return url.Split(new[] { "youtu.be/" }, StringSplitOptions.None)[1].Split('?')[0];
                }
                if (url.Contains("youtube.com"))
                {
                    return url.Split(new[] { "v=" }, StringSplitOptions.None)[1].Split('&')[0];
                }
            }
            catch { }
            return null;
        }

        private string ExtractVimeoId(string url)
        {
            try
            {
                var parts = url.Split('/');
                return parts[parts.Length - 1].Split('?')[0];
            }
            catch { }
            return null;
        }

        private string FetchLessonVideoUrl(Guid lessonId)
        {
            try
            {
                var dt = DbContext.Query("SELECT TOP 1 VideoUrl FROM LessonVideos WHERE LessonID = @L", new SqlParameter("@L", lessonId));
                if (dt != null && dt.Rows.Count > 0 && dt.Rows[0]["VideoUrl"] != DBNull.Value) 
                {
                    return dt.Rows[0]["VideoUrl"].ToString();
                }
            }
            catch (Exception ex) { System.Diagnostics.Debug.WriteLine("FetchLessonVideoUrl: " + ex.Message); }
            return null;
        }

        private DataTable FetchQuizQuestions(Guid lessonId)
        {
            try
            {
                string sql = @"
                    SELECT q.QuestionID, q.Question, q.Answers, q.CorrectIndex, q.Explanation
                    FROM QuizQuestions q
                    INNER JOIN LessonQuizzes lq ON q.LessonQuizId = lq.LessonID
                    WHERE lq.LessonID = @L
                    ORDER BY q.QuestionID";
                return DbContext.Query(sql, new SqlParameter("@L", lessonId));
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("FetchQuizQuestions: " + ex.Message);
                return null;
            }
        }

        private void BuildQuizUi(DataTable questions)
        {
            flpQuizQuestions.Controls.Clear();
            foreach (DataRow q in questions.Rows)
            {
                var panel = new Panel { Width = flpQuizQuestions.ClientSize.Width - 25, AutoSize = true, BackColor = Color.White, Padding = new Padding(8), Margin = new Padding(6), BorderStyle = BorderStyle.FixedSingle };
                string qText = q.Table.Columns.Contains("Question") ? (q["Question"]?.ToString() ?? "") : "";
                var lbl = new Label { Text = qText, AutoSize = true, MaximumSize = new Size(panel.Width - 16, 0), Font = new Font("Segoe UI", 10F, FontStyle.Bold) };
                panel.Controls.Add(lbl);

                var optionsPanel = new FlowLayoutPanel { FlowDirection = FlowDirection.TopDown, AutoSize = true, WrapContents = false, Width = panel.Width - 16 };
                
                // Parse JSON answers array
                string answersJson = q.Table.Columns.Contains("Answers") ? (q["Answers"]?.ToString() ?? "[]") : "[]";
                int correctIndex = q.Table.Columns.Contains("CorrectIndex") && q["CorrectIndex"] != DBNull.Value ? Convert.ToInt32(q["CorrectIndex"]) : -1;
                
                var answers = ParseJsonAnswers(answersJson);
                
                for (int i = 0; i < answers.Length; i++)
                {
                    char optionLabel = (char)('A' + i);
                    var rb = new RadioButton { Text = $"{optionLabel}. {answers[i]}", AutoSize = true, Tag = i, Margin = new Padding(4) };
                    optionsPanel.Controls.Add(rb);
                }
                        
                panel.Controls.Add(optionsPanel);

                panel.Tag = q; // store DataRow
                flpQuizQuestions.Controls.Add(panel);
            }
        }

        private DataTable FetchCodingProblemsForLesson(Guid lessonId)
        {
            try
            {
                string sql = @"
                    SELECT cp.ProblemID, cp.Title, cp.Difficulty, cp.Description
                    FROM CodingProblemLessons cpl
                    INNER JOIN CodingProblems cp ON cpl.ProblemID = cp.ProblemID
                    WHERE cpl.LessonID = @L AND (cpl.IsDeleted = 0 OR cpl.IsDeleted IS NULL) AND (cp.IsDeleted = 0 OR cp.IsDeleted IS NULL)
                    ORDER BY cpl.OrderIndex";
                return DbContext.Query(sql, new SqlParameter("@L", lessonId));
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("FetchCodingProblemsForLesson: " + ex.Message);
                return null;
            }
        }

        private string FetchLessonText(Guid lessonId)
        {
            try
            {
                var dt = DbContext.Query("SELECT TOP 1 Content FROM LessonText WHERE LessonID = @L", new SqlParameter("@L", lessonId));
                if (dt != null && dt.Rows.Count > 0 && dt.Rows[0]["Content"] != DBNull.Value) return dt.Rows[0]["Content"].ToString();
            }
            catch (Exception ex) { System.Diagnostics.Debug.WriteLine("FetchLessonText: " + ex.Message); }
            return null;
        }

        private async void  BtnSubmitQuiz_Click(object sender, EventArgs e)
        {
            // FIX 2: Validate quiz is loaded before allowing submission
            if (!_isQuizLoaded)
            {
                MessageBox.Show("Vui lòng chờ nội dung bài quiz được tải xong.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                int total = 0;
                int correct = 0;

                await Task.Run(() =>
                {
                    foreach (Control panel in flpQuizQuestions.Controls)
                    {
                        if (!(panel is Panel p)) continue;
                        DataRow qRow = p.Tag as DataRow;
                        int correctIndex = qRow != null && qRow.Table.Columns.Contains("CorrectIndex") && qRow["CorrectIndex"] != DBNull.Value ? Convert.ToInt32(qRow["CorrectIndex"]) : -1;

                        int selectedIndex = -1;
                        foreach (Control c in p.Controls)
                        {
                            if (c is FlowLayoutPanel opts)
                            {
                                foreach (Control rb in opts.Controls)
                                {
                                    if (rb is RadioButton r && r.Checked)
                                    {
                                        selectedIndex = r.Tag is int idx ? idx : -1;
                                    }       
                                }
                            }
                        }

                        total++;
                        if (correctIndex >= 0 && correctIndex == selectedIndex) correct++;
                    }
                });

                MessageBox.Show($"Bạn đạt {correct}/{total} câu đúng.", "Kết quả quiz", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi chấm quiz: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LvCodingProblems_DoubleClick(object sender, EventArgs e)
        {
            if (lvCodingProblems.SelectedItems.Count == 0) return;
            var tag = lvCodingProblems.SelectedItems[0].Tag as DataRow;
            if (tag == null) return;
            var id = tag.Table.Columns.Contains("ProblemID") && tag["ProblemID"] != DBNull.Value ? (Guid)tag["ProblemID"] : Guid.Empty;
            if (id == Guid.Empty) return;

            // open a simple viewer form
            using (var f = new ProblemViewer(id))
            {
                f.ShowDialog();
            }
        }

        private TreeNode FindFirstLessonNode()
        {
            foreach (TreeNode m in tvModulesLessons.Nodes)
            {
                foreach (TreeNode l in m.Nodes) return l;
            }
            return null;
        }

        private TreeNode FindAdjacentLessonNode(int direction)
        {
            if (tvModulesLessons.SelectedNode == null) return FindFirstLessonNode();
            var lessonNodes = new List<TreeNode>();
            foreach (TreeNode m in tvModulesLessons.Nodes)
                foreach (TreeNode l in m.Nodes) lessonNodes.Add(l);
            if (lessonNodes.Count == 0) return null;
            var idx = lessonNodes.IndexOf(tvModulesLessons.SelectedNode);
            if (idx < 0) return lessonNodes.FirstOrDefault();
            var newIdx = idx + direction;
            if (newIdx >= 0 && newIdx < lessonNodes.Count) return lessonNodes[newIdx];
            return null;
        }

        private void BtnPrev_Click(object sender, EventArgs e)
        {
            var prev = FindAdjacentLessonNode(-1);
            if (prev != null) tvModulesLessons.SelectedNode = prev;
        }

        private void BtnNext_Click(object sender, EventArgs e)
        {
            var next = FindAdjacentLessonNode(1);
            if (next != null) tvModulesLessons.SelectedNode = next;
        }

        private async void BtnMarkCompleted_Click(object sender, EventArgs e)
        {
            if (_currentLesson == null) return;
            try
            {
                await Task.Run(() =>
                {
                    try
                    {
                        string sqlCheck = "SELECT COUNT(1) FROM Progress WHERE LessonID = @L AND CourseID = @C";
                        var dt = DbContext.Query(sqlCheck, new SqlParameter("@L", _currentLesson.LessonId), new SqlParameter("@C", _courseId));
                        if (dt != null && dt.Rows.Count > 0 && Convert.ToInt32(dt.Rows[0][0]) > 0)
                        {
                            DbContext.Execute("UPDATE Progress SET IsCompleted = 1, CompletedAt = @T WHERE LessonID = @L AND CourseID = @C",
                                new SqlParameter("@T", DateTime.Now), new SqlParameter("@L", _currentLesson.LessonId), new SqlParameter("@C", _courseId));
                        }
                        else
                        {
                            DbContext.Execute("INSERT INTO Progress (ProgressID, CourseID, LessonID, IsCompleted, CompletedAt) VALUES (NEWID(), @C, @L, 1, @T)",
                                new SqlParameter("@C", _courseId), new SqlParameter("@L", _currentLesson.LessonId), new SqlParameter("@T", DateTime.Now));
                        }
                    }
                    catch (Exception ex)
                    {
                        System.Diagnostics.Debug.WriteLine("MarkCompleted DB op failed: " + ex.Message);
                    }
                });

                // update local set and node icon
                _completedLessonIds.Add(_currentLesson.LessonId);
                if (tvModulesLessons.SelectedNode != null)
                {
                    tvModulesLessons.SelectedNode.ImageKey = "completed";
                    tvModulesLessons.SelectedNode.SelectedImageKey = "completed";
                    tvModulesLessons.SelectedNode.ForeColor = Color.Green;
                }

                MessageBox.Show("Đã đánh dấu hoàn thành.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi đánh dấu hoàn thành: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void UpdateCourseLessonInfo()
        {
            int lessonIndex = 0;
            int totalLessons = 0;
            var ordered = new List<LessonDto>();
            foreach (var m in _modules ?? Enumerable.Empty<ModuleDto>()) foreach (var l in m.Lessons) ordered.Add(l);
            totalLessons = ordered.Count;
            if (_currentLesson != null) lessonIndex = ordered.FindIndex(x => x.LessonId == _currentLesson.LessonId) + 1;
            lblCourseLessonInfo.Text = $"{ (lessonIndex>0 ? $"Lesson {lessonIndex}/{totalLessons}" : "") }";
        }

        // WebView2 runtime helper (reflection) - avoids hard dependency at compile time
        private void TryPrepareWebView2Type()
        {
            try
            {
                _webView2Type = Type.GetType("Microsoft.Web.WebView2.WinForms.WebView2, Microsoft.Web.WebView2.WinForms");
            }
            catch { _webView2Type = null; }
        }

        private bool TryUseWebView2Navigate(string url)
        {
            try
            {
                if (_webView2Type == null) TryPrepareWebView2Type();
                if (_webView2Type == null) return false;

                if (_webView2Instance == null)
                {
                    _webView2Instance = Activator.CreateInstance(_webView2Type);
                    var c = _webView2Instance as Control;
                    if (c != null)
                    {
                        c.Dock = DockStyle.Top;
                        c.Height = wbVideo.Height;
                        pnlLessonContent.Controls.Add(c);
                        c.BringToFront();
                        wbVideo.Visible = false;
                    }
                    var ensure = _webView2Type.GetMethod("EnsureCoreWebView2Async", new Type[] { typeof(object) });
                    if (ensure == null)
                    {
                        ensure = _webView2Type.GetMethod("EnsureCoreWebView2Async", Type.EmptyTypes);
                    }
                    if (ensure != null)
                    {
                        ensure.Invoke(_webView2Instance, null);
                    }
                }

                var nav = _webView2Type.GetMethod("Navigate", new[] { typeof(string) });
                if (nav != null)
                {
                    nav.Invoke(_webView2Instance, new object[] { url });
                    return true;
                }

                var srcProp = _webView2Type.GetProperty("Source");
                if (srcProp != null)
                {
                    srcProp.SetValue(_webView2Instance, new Uri(url));
                    return true;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("WebView2 navigate failed: " + ex.Message);
            }
            return false;
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
            public int Duration { get; set; }
            public string ExtraInfo { get; set; }
            public List<CodingProblemInfo> CodingProblems { get; } = new List<CodingProblemInfo>();
        }

        private class CodingProblemInfo
        {
            public Guid ProblemID { get; set; }
            public string Title { get; set; }
            public string Difficulty { get; set; }
        }

        private string[] ParseJsonAnswers(string answersJson)
        {
            try
            {
                return JsonSerializer.Deserialize<string[]>(answersJson) ?? Array.Empty<string>();
            }
            catch
            {
                try
                {
                    var arr = JsonSerializer.Deserialize<List<JsonElement>>(answersJson);
                    if (arr != null)
                    {
                        return arr.Select(e => e.ValueKind == JsonValueKind.String ? e.GetString() : e.ToString()).ToArray();
                    }
                }
                catch { }
                return Array.Empty<string>();
            }
        }

        private void RenderHtmlContent(string htmlContent)
        {
            try
            {
                // Wrap HTML content in a complete HTML document
                string wrappedHtml = $@"<!DOCTYPE html>
<html>
<head>
    <meta charset='UTF-8'>
    <style>
        body {{ 
            font-family: 'Segoe UI', Arial, sans-serif;
            margin: 12px;
            padding: 8px;
            background: white;
            color: #333;
            line-height: 1.6;
        }}
        h1, h2, h3, h4, h5, h6 {{ margin: 12px 0 8px 0; color: #2c3e50; }}
        p {{ margin: 8px 0; }}
        a {{ color: #0078d4; text-decoration: none; }}
        a:hover {{ text-decoration: underline; }}
        code {{ background: #f4f4f4; padding: 2px 6px; border-radius: 3px; font-family: 'Courier New', monospace; }}
        pre {{ background: #f4f4f4; padding: 12px; border-radius: 4px; overflow-x: auto; }}
        blockquote {{ border-left: 4px solid #0078d4; padding-left: 12px; margin-left: 0; color: #666; }}
        ul, ol {{ margin: 8px 0; padding-left: 24px; }}
        li {{ margin: 4px 0; }}
        table {{ border-collapse: collapse; width: 100%; margin: 12px 0; }}
        th, td {{ border: 1px solid #ddd; padding: 8px; text-align: left; }}
        th {{ background: #f4f4f4; font-weight: bold; }}
        img {{ max-width: 100%; height: auto; margin: 8px 0; }}
    </style>
</head>
<body>
    {htmlContent}
</body>
</html>";

                wbVideo.Visible = true;
                rtbLessonText.Visible = false;
                wbVideo.DocumentText = wrappedHtml;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("RenderHtmlContent error: " + ex.Message);
                rtbLessonText.Visible = true;
                rtbLessonText.Text = "Không thể render HTML content. Nội dung gốc:\n\n" + htmlContent;
            }
        }
    }
}