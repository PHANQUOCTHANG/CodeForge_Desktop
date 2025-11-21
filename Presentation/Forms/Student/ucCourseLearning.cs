using CodeForge_Desktop.Config;
using CodeForge_Desktop.DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.Json;
using CodeForge_Desktop.Business.Helpers;
using CodeForge_Desktop.Business.Services;

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
            ApplyCompletedFlags();
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
                    // prefer lesson.IsCompleted flag (set from progress) to determine icon
                    var ln = new TreeNode(l.Title) { Tag = l, ImageKey = l.IsCompleted ? "completed" : key, SelectedImageKey = l.IsCompleted ? "completed" : key };
                    mn.Nodes.Add(ln);
                }
                tvModulesLessons.Nodes.Add(mn);
            }

            // Replace previous EndUpdate with calls that also refresh icons
            tvModulesLessons.EndUpdate();
            RefreshTreeNodeCompletionIcons();
        }

        // Add this helper near PopulateTree / ApplyCompletedFlags (inside the class)
        private void RefreshTreeNodeCompletionIcons()
        {
            if (tvModulesLessons.InvokeRequired)
            {
                tvModulesLessons.Invoke(new Action(RefreshTreeNodeCompletionIcons));
                return;
            }

            try
            {
                tvModulesLessons.BeginUpdate();
                foreach (TreeNode m in tvModulesLessons.Nodes)
                {
                    foreach (TreeNode lnode in m.Nodes)
                    {
                        var lesson = lnode.Tag as LessonDto;
                        if (lesson == null) continue;

                        string key = "text";
                        if (!string.IsNullOrEmpty(lesson.LessonType))
                        {
                            switch (lesson.LessonType.ToLowerInvariant())
                            {
                                case "video": key = "video"; break;
                                case "quiz": key = "quiz"; break;
                                case "coding": key = "code"; break;
                            }
                        }

                        var imageKey = lesson.IsCompleted ? "completed" : key;
                        lnode.ImageKey = imageKey;
                        lnode.SelectedImageKey = imageKey;
                    }
                }
            }
            finally
            {
                tvModulesLessons.EndUpdate();
                tvModulesLessons.Refresh();
            }
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
                var currentUser = GlobalStore.user;
                if (currentUser == null || currentUser.UserID == Guid.Empty) return set;

                // Progress table in your schema does not have CourseID or IsCompleted columns.
                // Join Progress -> Lessons -> Modules to filter by CourseID and check Status = 'completed'.
                string sql = @"
            SELECT DISTINCT p.LessonID
            FROM Progress p
            INNER JOIN Lessons l ON p.LessonID = l.LessonID
            INNER JOIN Modules m ON l.ModuleID = m.ModuleID
            WHERE m.CourseID = @CourseId
              AND p.UserID = @UserId
              AND ISNULL(p.Status,'') = 'completed'";

                var dt = DbContext.Query(sql,
                    new SqlParameter("@CourseId", courseId),
                    new SqlParameter("@UserId", currentUser.UserID));

                if (dt != null)
                {
                    foreach (DataRow r in dt.Rows)
                    {
                        if (r["LessonID"] != DBNull.Value)
                            set.Add((Guid)r["LessonID"]);
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

        private string FetchLessonText(Guid lessonId)
        {
            try
            {
                // Table name in your DB is LessonTexts (plural)
                var dt = DbContext.Query("SELECT TOP 1 Content FROM LessonTexts WHERE LessonID = @L", new SqlParameter("@L", lessonId));
                if (dt != null && dt.Rows.Count > 0 && dt.Rows[0]["Content"] != DBNull.Value)
                    return dt.Rows[0]["Content"].ToString();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("FetchLessonText: " + ex.Message);
            }
            return null;
        }

        // apply _completedLessonIds into lesson DTOs so UI can rely on lesson.IsCompleted
        private void ApplyCompletedFlags()
        {
            if (_modules == null || _completedLessonIds == null) return;
            foreach (var m in _modules)
            {
                foreach (var l in m.Lessons)
                {
                    l.IsCompleted = _completedLessonIds.Contains(l.LessonId);
                }
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

                        // debug: log returned text length/preview
                        System.Diagnostics.Debug.WriteLine($"FetchLessonText: len={(text?.Length ?? 0)}, preview={(text?.Substring(0, Math.Min(200, text?.Length ?? 0)) ?? "<null>")}");

                        // Render HTML content (await it so errors are not swallowed)
                        if (!string.IsNullOrEmpty(text) && text.Contains("<"))
                        {
                            await RenderHtmlContent(text); // now returns Task and is awaited
                        }
                        else
                        {
                            rtbLessonText.Text = string.IsNullOrEmpty(text) ? "(No content)" : text;
                        }

                        // If rendering produced no visible browser, show raw HTML so user sees data
                        bool webViewVisible = (_webView2Instance is Control wc && wc.Visible) || (wbVideo != null && wbVideo.Visible);
                        if (!webViewVisible && !string.IsNullOrEmpty(text) && text.Contains("<"))
                        {
                            // show the raw HTML as a fallback so it is not completely blank
                            rtbLessonText.Visible = true;
                            rtbLessonText.Text = text;
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
            string vid = await Task.Run(() => FetchLessonVideoUrl(lesson.LessonId));

            if (string.IsNullOrWhiteSpace(vid))
            {
                ShowErrorInPanel("Video URL not found or empty.");
                return;
            }

            vid = vid.Trim();

            if (!Uri.IsWellFormedUriString(vid, UriKind.Absolute) && File.Exists(vid))
                vid = new Uri(Path.GetFullPath(vid)).AbsoluteUri;

            if (!IsVideoUrl(vid))
            {
                ShowErrorInPanel("Video URL is not a supported video link.");
                return;
            }

            if (IsEmbeddableVideoUrl(vid) && _webView2Type == null)
            {
                var openExtern = MessageBox.Show("This video may not play inside the app. Open in your browser instead?", "Open video", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (openExtern == DialogResult.Yes)
                {
                    try { Process.Start(new ProcessStartInfo { FileName = vid, UseShellExecute = true }); }
                    catch (Exception ex) { ShowErrorInPanel("Unable to open external browser: " + ex.Message); }
                }
                else
                {
                    ShowErrorInPanel("Embedded playback requires WebView2. Please install WebView2 runtime or open externally.");
                }
                return;
            }

            string htmlContent = GenerateEmbedVideoHtml(vid);

            // 1) Prefer WebView2 and ensure CoreWebView2 is initialized before NavigateToString
            if (await TryUseWebView2NavigateAsync(htmlContent))
            {
                wbVideo.Visible = false;
                rtbLessonText.Visible = false;
                return;
            }

            // 2) Fallback: check URL accessibility and offer external open
            bool accessible = await CheckUrlAccessibleAsync(vid);

            if (!accessible)
            {
                var open = MessageBox.Show("Video link is inaccessible. Open in browser?", "Video inaccessible", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (open == DialogResult.Yes)
                {
                    try { Process.Start(new ProcessStartInfo { FileName = vid, UseShellExecute = true }); }
                    catch (Exception ex) { ShowErrorInPanel("Unable to open external browser: " + ex.Message); }
                }
                else
                {
                    ShowErrorInPanel("Cannot play video in embedded player.");
                }
                return;
            }

            // 3) Final fallback: use WebBrowser control to render the embed HTML (may still fail for YouTube)
            try
            {
                wbVideo.Visible = true;
                rtbLessonText.Visible = false;
                wbVideo.DocumentText = htmlContent;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("WebBrowser render failed: " + ex.Message);
                var ask = MessageBox.Show("Embedded player failed. Open in external browser?", "Playback error", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (ask == DialogResult.Yes)
                {
                    try { Process.Start(new ProcessStartInfo { FileName = vid, UseShellExecute = true }); }
                    catch { ShowErrorInPanel("Cannot open external browser."); }
                }
                else
                {
                    ShowErrorInPanel("Cannot play video in embedded player.");
                }
            }
        }

        // New helper: do a fast HEAD request to check URL accessibility
        private async Task<bool> CheckUrlAccessibleAsync(string url)
        {
            try
            {
                using (var http = new HttpClient())
                {
                    http.Timeout = TimeSpan.FromSeconds(6);
                    // Some servers don't accept HEAD; fall back to GET but request only headers
                    var req = new HttpRequestMessage(HttpMethod.Head, url);
                    var resp = await http.SendAsync(req);
                    if (resp.IsSuccessStatusCode) return true;

                    // fallback: try GET but do not download content fully
                    req = new HttpRequestMessage(HttpMethod.Get, url);
                    req.Headers.Range = new System.Net.Http.Headers.RangeHeaderValue(0, 0);
                    resp = await http.SendAsync(req);
                    return resp.IsSuccessStatusCode;
                }
            }
            catch
            {
                return false;
            }
        }

        private async Task SubmitQuizAsync()
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

        private async Task RenderHtmlContentAsync(string htmlContent)
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

            try
            {
                // 1) If a WebView2 instance exists, try NavigateToString on it (reflection)
                if (_webView2Instance != null)
                {
                    try
                    {
                        var instType = _webView2Instance.GetType();
                        var nav = instType.GetMethod("NavigateToString", new[] { typeof(string) });
                        if (nav != null)
                        {
                            // Invoke on UI thread
                            if (this.InvokeRequired)
                                this.Invoke(new Action(() => nav.Invoke(_webView2Instance, new object[] { wrappedHtml })));
                            else
                                nav.Invoke(_webView2Instance, new object[] { wrappedHtml });

                            // ensure WebView2 control visible if it's a Control
                            if (_webView2Instance is Control c)
                            {
                                if (this.InvokeRequired) this.Invoke(new Action(() => { c.Visible = true; c.BringToFront(); }));
                                else { c.Visible = true; c.BringToFront(); }
                            }

                            rtbLessonText.Visible = false;
                            return;
                        }
                    }
                    catch (Exception ex)
                    {
                        System.Diagnostics.Debug.WriteLine("RenderHtmlContentAsync: WebView2 navigate failed: " + ex.Message);
                    }
                }

                // 2) Try to create/use WebView2 via helper (async)
                if (_webView2Type != null)
                {
                    try
                    {
                        var used = await TryUseWebView2NavigateAsync(wrappedHtml);
                        if (used)
                        {
                            rtbLessonText.Visible = false;
                            return;
                        }
                    }
                    catch (Exception ex)
                    {
                        System.Diagnostics.Debug.WriteLine("RenderHtmlContentAsync: TryUseWebView2NavigateAsync failed: " + ex.Message);
                    }
                }

                // 3) Fallback to legacy WebBrowser control (wbVideo)
                Action setDoc = () =>
                {
                    try
                    {
                        // if wbVideo was removed from the visual tree earlier, re-add it to this control
                        if (wbVideo.Parent == null)
                        {
                            // place it into the same container as rtbLessonText if available, otherwise onto this control
                            var container = rtbLessonText.Parent ?? (Control)this;
                            container.Controls.Add(wbVideo);
                            wbVideo.Dock = DockStyle.Fill;
                        }

                        wbVideo.Visible = true;
                        rtbLessonText.Visible = false;
                        wbVideo.DocumentText = wrappedHtml;
                        wbVideo.BringToFront();
                    }
                    catch (Exception ex)
                    {
                        System.Diagnostics.Debug.WriteLine("RenderHtmlContentAsync (WebBrowser) error: " + ex.Message);
                        rtbLessonText.Visible = true;
                        rtbLessonText.Text = "Cannot render HTML content. Raw:\n\n" + htmlContent;
                    }
                };

                if (this.InvokeRequired) this.Invoke(setDoc);
                else setDoc();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("RenderHtmlContentAsync error: " + ex.Message);
                if (this.InvokeRequired)
                    this.Invoke(new Action(() => { rtbLessonText.Visible = true; rtbLessonText.Text = "Cannot render HTML content. Raw:\n\n" + htmlContent; }));
                else
                {
                    rtbLessonText.Visible = true;
                    rtbLessonText.Text = "Cannot render HTML content. Raw:\n\n" + htmlContent;
                }
            }
        }

        private Task RenderHtmlContent(string htmlContent)
        {
            // forward to the existing async renderer so callers can await it and catch errors
            return RenderHtmlContentAsync(htmlContent);
        }

        private void ShowErrorInPanel(string message)
        {
            rtbLessonText.Visible = true;
            wbVideo.Visible = false;
            pnlQuiz.Visible = false;
            lvCodingProblems.Visible = false;
            rtbLessonText.Text = message;
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

            // completion flag applied from Progress data
            public bool IsCompleted { get; set; } = false;
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

        private void BtnPrev_Click(object sender, EventArgs e)
        {
            // TODO: Implement logic to navigate to the previous lesson.
            MessageBox.Show("Previous button clicked. Implement navigation logic here.");
        }

        private void BtnNext_Click(object sender, EventArgs e)
        {
            // Navigate to next lesson node if available
            var sel = tvModulesLessons.SelectedNode;
            if (sel == null)
            {
                MessageBox.Show("No lesson selected.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // if a module node was selected, try to pick its first child
            if (sel.Tag is ModuleDto)
            {
                if (sel.Nodes.Count > 0) { tvModulesLessons.SelectedNode = sel.Nodes[0]; return; }
                return;
            }

            // lesson node -> move to next sibling, or next module's first child
            var parent = sel.Parent;
            if (parent == null) return;
            int idx = parent.Nodes.IndexOf(sel);
            if (idx >= 0 && idx + 1 < parent.Nodes.Count)
            {
                tvModulesLessons.SelectedNode = parent.Nodes[idx + 1];
                return;
            }

            // move to next module's first lesson
            var moduleIdx = tvModulesLessons.Nodes.IndexOf(parent);
            if (moduleIdx >= 0 && moduleIdx + 1 < tvModulesLessons.Nodes.Count)
            {
                var nextModule = tvModulesLessons.Nodes[moduleIdx + 1];
                if (nextModule.Nodes.Count > 0) tvModulesLessons.SelectedNode = nextModule.Nodes[0];
            }
        }

        private void BtnMarkCompleted_Click(object sender, EventArgs e)
        {
            if (_currentLesson == null)
            {
                MessageBox.Show("No lesson selected.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // toggle local completion state; ideally you would persist via ProgressRepository/Service
            _currentLesson.IsCompleted = !_currentLesson.IsCompleted;
            if (_currentLesson.IsCompleted) _completedLessonIds.Add(_currentLesson.LessonId);
            else _completedLessonIds.Remove(_currentLesson.LessonId);

            RefreshTreeNodeCompletionIcons();
            UpdateCourseLessonInfo();

            // Optionally notify user; persistence to DB omitted here (call repository/service in real code)
            MessageBox.Show(_currentLesson.IsCompleted ? "Marked completed." : "Marked not completed.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private async void BtnSubmitQuiz_Click(object sender, EventArgs e)
        {
            await SubmitQuizAsync();
        }

        private void LvCodingProblems_DoubleClick(object sender, EventArgs e)
        {
            if (lvCodingProblems.SelectedItems.Count == 0) return;
            var item = lvCodingProblems.SelectedItems[0];
            var row = item.Tag as DataRow;
            if (row != null)
            {
                var id = row.Table.Columns.Contains("ProblemID") && row["ProblemID"] != DBNull.Value ? row["ProblemID"].ToString() : "(unknown)";
                var title = row.Table.Columns.Contains("Title") && row["Title"] != DBNull.Value ? row["Title"].ToString() : item.Text;
                MessageBox.Show($"Open coding problem:\n{title}\nID: {id}", "Coding problem", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private TreeNode FindFirstLessonNode()
        {
            foreach (TreeNode module in tvModulesLessons.Nodes)
            {
                foreach (TreeNode ln in module.Nodes)
                {
                    if (ln.Tag is LessonDto) return ln;
                }
            }
            return null;
        }

        private void UpdateCourseLessonInfo()
        {
            // enable/disable nav buttons and update mark-completed caption
            var sel = tvModulesLessons.SelectedNode;
            bool hasPrev = false, hasNext = false;
            if (sel != null && sel.Parent != null)
            {
                var parent = sel.Parent;
                int idx = parent.Nodes.IndexOf(sel);
                hasPrev = idx > 0 || tvModulesLessons.Nodes.IndexOf(parent) > 0;
                hasNext = idx < parent.Nodes.Count - 1 || tvModulesLessons.Nodes.IndexOf(parent) < tvModulesLessons.Nodes.Count - 1;
            }
            btnPrev.Enabled = hasPrev;
            btnNext.Enabled = hasNext;

            if (_currentLesson != null)
            {
                btnMarkCompleted.Text = _currentLesson.IsCompleted ? "Unmark completed" : "Mark completed";
            }
            else
            {
                btnMarkCompleted.Text = "Mark completed";
            }
        }

        private void TryPrepareWebView2Type()
        {
            try
            {
                // Attempt to find WebView2 WinForms type by full type name
                _webView2Type = Type.GetType("Microsoft.Web.WebView2.WinForms.WebView2, Microsoft.Web.WebView2.WinForms");
            }
            catch
            {
                _webView2Type = null;
            }
        }

        // Data access helpers (lightweight fallbacks that try to read from DB; adjust SQL to your schema)
        private DataTable FetchQuizQuestions(Guid lessonId)
        {
            try
            {
                // Schema: QuizQuestions references LessonQuizzes via LessonQuizId and stores Answers (JSON).
                string sql = @"
            SELECT QuestionID,
                   Question AS QuestionText,
                   Answers AS OptionsJson,
                   CorrectIndex
            FROM QuizQuestions
            WHERE LessonQuizId = @L
            ORDER BY QuestionID";
                var dt = DbContext.Query(sql, new SqlParameter("@L", lessonId));
                return dt ?? new DataTable();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("FetchQuizQuestions: " + ex.Message);
                return new DataTable();
            }
        }

        private string FetchLessonVideoUrl(Guid lessonId)
        {
            try
            {
                // Video URL is stored in LessonVideos table per your DDL.
                var dt = DbContext.Query("SELECT TOP 1 VideoUrl FROM LessonVideos WHERE LessonID = @L", new SqlParameter("@L", lessonId));
                if (dt != null && dt.Rows.Count > 0 && dt.Rows[0]["VideoUrl"] != DBNull.Value)
                    return dt.Rows[0]["VideoUrl"].ToString();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("FetchLessonVideoUrl: " + ex.Message);
            }
            return null;
        }

        private DataTable FetchCodingProblemsForLesson(Guid lessonId)
        {
            try
            {
                // In your schema CodingProblems has LessonID directly (no join table).
                string sql = @"
            SELECT ProblemID, Title, Difficulty
            FROM CodingProblems
            WHERE LessonID = @L AND IsDeleted = 0
            ORDER BY Title";
                var dt = DbContext.Query(sql, new SqlParameter("@L", lessonId));
                return dt ?? new DataTable();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("FetchCodingProblemsForLesson: " + ex.Message);
                return new DataTable();
            }
        }

        private void BuildQuizUi(DataTable quiz)
        {
            if (flpQuizQuestions.InvokeRequired)
            {
                flpQuizQuestions.Invoke(new Action<DataTable>(BuildQuizUi), quiz);
                return;
            }

            flpQuizQuestions.Controls.Clear();
            int qIndex = 0;
            foreach (DataRow r in quiz.Rows)
            {
                var panel = new Panel { AutoSize = true, Tag = r, Padding = new Padding(6), Margin = new Padding(6) };
                string qText = r.Table.Columns.Contains("QuestionText") && r["QuestionText"] != DBNull.Value
                    ? r["QuestionText"].ToString()
                    : r.Table.Columns.Contains("Question") && r["Question"] != DBNull.Value
                        ? r["Question"].ToString()
                        : "(Question)";

                var lbl = new Label { AutoSize = true, Text = $"{++qIndex}. {qText}", MaximumSize = new Size(Math.Max(100, flpQuizQuestions.Width - 40), 0) };
                panel.Controls.Add(lbl);

                var optsPanel = new FlowLayoutPanel { FlowDirection = FlowDirection.TopDown, AutoSize = true, WrapContents = false, Margin = new Padding(4) };
                string optsJson = r.Table.Columns.Contains("OptionsJson") && r["OptionsJson"] != DBNull.Value
                    ? r["OptionsJson"].ToString()
                    : r.Table.Columns.Contains("Answers") && r["Answers"] != DBNull.Value
                        ? r["Answers"].ToString()
                        : null;

                var options = ParseJsonAnswers(optsJson);
                for (int i = 0; i < options.Length; i++)
                {
                    var rb = new RadioButton { AutoSize = true, Text = options[i], Tag = i, Margin = new Padding(2) };
                    optsPanel.Controls.Add(rb);
                }

                panel.Controls.Add(optsPanel);
                flpQuizQuestions.Controls.Add(panel);
            }

            _isQuizLoaded = true;
        }

        private bool IsVideoUrl(string url)
        {
            if (string.IsNullOrWhiteSpace(url)) return false;
            if (Uri.TryCreate(url, UriKind.Absolute, out var u))
            {
                var ext = Path.GetExtension(u.AbsolutePath)?.ToLowerInvariant();
                if (!string.IsNullOrEmpty(ext) && (ext == ".mp4" || ext == ".webm" || ext == ".ogg")) return true;
                var host = u.Host.ToLowerInvariant();
                if (host.Contains("youtube.com") || host.Contains("youtu.be") || host.Contains("vimeo.com")) return true;
                // treat other absolute http(s) urls as playable externally
                return u.Scheme.StartsWith("http");
            }
            // allow local file path
            return File.Exists(url);
        }

        private bool IsEmbeddableVideoUrl(string url)
        {
            if (!Uri.TryCreate(url, UriKind.Absolute, out var u)) return false;
            var host = u.Host.ToLowerInvariant();
            return host.Contains("youtube.com") || host.Contains("youtu.be") || host.Contains("vimeo.com");
        }

        private string GenerateEmbedVideoHtml(string url)
        {
            try
            {
                if (url.Contains("youtube.com") || url.Contains("youtu.be"))
                {
                    string id = null;
                    try
                    {
                        var uri = new Uri(url);
                        if (uri.Host.Contains("youtu.be"))
                        {
                            id = uri.AbsolutePath.Trim('/');
                        }
                        else
                        {
                            var q = System.Web.HttpUtility.ParseQueryString(uri.Query);
                            id = q["v"];
                        }
                    }
                    catch { /* ignore parse errors */ }

                    if (string.IsNullOrEmpty(id))
                        return $"<iframe width='100%' height='480' src='{url}' frameborder='0' allow='autoplay; encrypted-media' allowfullscreen></iframe>";

                    var src = $"https://www.youtube-nocookie.com/embed/{id}?rel=0&modestbranding=1&enablejsapi=1";
                    return $"<!DOCTYPE html><html><head><meta charset='utf-8'></head><body style='margin:0'><iframe src='{src}' width='100%' height='480' frameborder='0' allow='autoplay; encrypted-media' allowfullscreen></iframe></body></html>";
                }

                if (url.Contains("vimeo.com"))
                {
                    return $"<!DOCTYPE html><html><head><meta charset='utf-8'></head><body style='margin:0'><iframe src='{url}' width='100%' height='480' frameborder='0' allow='autoplay; fullscreen' allowfullscreen></iframe></body></html>";
                }

                // generic video tag fallback
                return $"<!DOCTYPE html><html><head><meta charset='utf-8'></head><body style='margin:0'><video width='100%' height='480' controls><source src='{url}' type='video/mp4'>Your browser does not support the video tag.</video></body></html>";
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("GenerateEmbedVideoHtml: " + ex.Message);
                return $"<p>Unable to generate embed for {url}</p>";
            }
        }

        private async Task<bool> TryUseWebView2NavigateAsync(string html)
        {
            if (_webView2Type == null) return false;

            try
            {
                var instance = Activator.CreateInstance(_webView2Type);
                if (instance == null) return false;

                if (!(instance is Control createdControl)) return false;

                // call EnsureCoreWebView2Async if available (await so NavigateToString works)
                var ensureMethod = _webView2Type.GetMethod("EnsureCoreWebView2Async", Type.EmptyTypes)
                                   ?? _webView2Type.GetMethod("EnsureCoreWebView2Async", new[] { typeof(object) });

                if (ensureMethod != null)
                {
                    var taskObj = ensureMethod.GetParameters().Length == 0
                        ? ensureMethod.Invoke(instance, null)
                        : ensureMethod.Invoke(instance, new object[] { null });

                    if (taskObj is Task t) await t;
                    else
                    {
                        try { await (dynamic)taskObj; } catch { /* best-effort */ }
                    }
                }

                var parent = wbVideo?.Parent;
                if (parent != null)
                {
                    // insert WebView2 control and remove legacy WebBrowser
                    createdControl.Dock = DockStyle.Fill;
                    parent.Controls.Add(createdControl);
                    parent.Controls.Remove(wbVideo);
                }

                var nav = _webView2Type.GetMethod("NavigateToString", new[] { typeof(string) });
                if (nav != null)
                {
                    nav.Invoke(instance, new object[] { html });
                    _webView2Instance = instance;
                    return true;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("TryUseWebView2NavigateAsync: " + ex.Message);
            }

            return false;
        }

        private void splitMain_SplitterMoved(object sender, SplitterEventArgs e)
        {

        }
    }
}