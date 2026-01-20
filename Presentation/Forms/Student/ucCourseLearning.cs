using CodeForge_Desktop.Business.DTOs;
using CodeForge_Desktop.Business.Helpers;
using CodeForge_Desktop.Business.Services;
using CodeForge_Desktop.DataAccess.Entities;
using CodeForge_Desktop.DataAccess.Repositories;
using Microsoft.Web.WebView2.WinForms;
using Microsoft.Web.WebView2.Core;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace CodeForge_Desktop.Presentation.Forms.Student.UserControls
{
    public partial class ucCourseLearning : UserControl
    {
        private readonly Guid _courseID;
        private readonly CourseService _courseService;
        private readonly ProgressService _progressService;
        private CourseDetailDto _courseData;
        private LessonDto _currentLesson;
        private List<LessonDto> _flatLessonList = new List<LessonDto>();
        private WebView2 _currentWebView;

        // Quiz state for current loaded quiz
        private Dictionary<Guid, int> _currentQuizSelections = new Dictionary<Guid, int>();
        private List<GroupBox> _currentQuizQuestionBoxes = new List<GroupBox>();
        private Panel _quizPanelContainer;

        public ucCourseLearning(Guid courseId)
        {
            InitializeComponent();
            _courseID = courseId;

            var repo = new CourseRepository();
            var progRepo = new ProgressRepository();
            _courseService = new CourseService(repo);
            _progressService = new ProgressService(progRepo);

            // Set sidebar styling
            pnlRightContainer.BackColor = Color.FromArgb(248, 249, 250);
            flpCurriculum.BackColor = Color.FromArgb(248, 249, 250);
            pnlSidebarHeader.BackColor = Color.FromArgb(233, 236, 239);

            this.Load += async (s, e) => await LoadDataAsync();
            btnBack.Click += (s, e) => MainFormStudent.Instance?.GoBack();
            btnNext.Click += (s, e) => NavigateLesson(1);
            btnPrev.Click += (s, e) => NavigateLesson(-1);
            btnMarkCompleted.Click += async (s, e) => await MarkLessonCompleted();

            splitMain.Panel2.SizeChanged += (s, e) => ResizeCurriculumItems();
        }

        private async Task LoadDataAsync()
        {
            try
            {
                _courseData = await _courseService.GetCourseDetailAsync(_courseID);
                if (_courseData == null)
                {
                    MessageBox.Show("Không thể tải dữ liệu khóa học.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                lblCourseTitle.Text = _courseData.Title;

                var user = GlobalStore.user;
                List<Guid> completedLessonIds = new List<Guid>();
                if (user != null)
                {
                    completedLessonIds = await _progressService.GetCompletedLessonsAsync(user.UserID, _courseID);
                }

                foreach (var mod in _courseData.Modules)
                {
                    foreach (var les in mod.Lessons)
                    {
                        if (completedLessonIds.Contains(les.LessonID))
                            les.IsCompleted = true;
                    }
                }

                _flatLessonList.Clear();
                RenderSidebar(_courseData.Modules);
                ResizeCurriculumItems();            // ensure item widths are calculated after rendering
                flpCurriculum.PerformLayout();      // force layout update
                UpdateProgressBar();

                var nextLesson = _flatLessonList.FirstOrDefault(l => !l.IsCompleted) ?? _flatLessonList.FirstOrDefault();
                if (nextLesson != null)
                {
                    await LoadLessonContentAsync(nextLesson);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // =========================================================
        // RENDER SIDEBAR
        // =========================================================
        private void RenderSidebar(List<ModuleDto> modules)
        {
            flpCurriculum.SuspendLayout();
            flpCurriculum.Controls.Clear();

            foreach (var mod in modules)
            {
                flpCurriculum.Controls.Add(CreateModuleWidget(mod));
            }

            flpCurriculum.ResumeLayout();
        }

        // Replace CreateModuleWidget implementation with this improved version
        private Control CreateModuleWidget(ModuleDto mod)
        {
            int width = flpCurriculum.ClientSize.Width - SystemInformation.VerticalScrollBarWidth - 10;

            var pnlContainer = new Panel
            {
                Width = width,
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink,
                BackColor = Color.Red,
                Margin = new Padding(0, 0, 0, 2)
            };

            // Module header
            var btnHeader = new Button
            {
                Text = $"▼  {mod.Title} ({(mod.Lessons?.Count ?? 0)} bài)",
                TextAlign = ContentAlignment.MiddleLeft,
                Dock = DockStyle.Top,
                Height = 48,
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.Red,
                ForeColor = Color.FromArgb(33, 37, 41),
                Font = new Font("Segoe UI", 10f, FontStyle.Bold),
                Cursor = Cursors.Hand,
                Padding = new Padding(15, 0, 0, 0)
            };
            btnHeader.FlatAppearance.BorderSize = 0;
            btnHeader.FlatAppearance.MouseOverBackColor = Color.FromArgb(233, 236, 239);

            // Lessons panel (FlowLayoutPanel)
            var pnlLessons = new FlowLayoutPanel
            {
                Dock = DockStyle.Top,
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink,
                FlowDirection = FlowDirection.TopDown,
                WrapContents = false,
                Visible = true,
                Padding = new Padding(0),
                Width = width,
                BackColor = Color.White
            };

            if (mod.Lessons != null && mod.Lessons.Count > 0)
            {
                foreach (var les in mod.Lessons)
                {
                    _flatLessonList.Add(les);
                    pnlLessons.Controls.Add(CreateLessonItem(les, width));
                }
            }

            // Add lessons first then header so header docks at top and lessons appear under it
            pnlContainer.Controls.Add(pnlLessons);
            pnlContainer.Controls.Add(btnHeader);

            btnHeader.Click += (s, e) =>
            {
                pnlLessons.Visible = !pnlLessons.Visible;
                btnHeader.Text = (pnlLessons.Visible ? "▼" : "▶") + $"  {mod.Title} ({(mod.Lessons?.Count ?? 0)} bài)";
            };

            return pnlContainer;
        }

        private Control CreateLessonItem(LessonDto les, int width)
        {
            string icon = les.IsCompleted ? "✓" : (les.LessonType?.ToLower() == "video" ? "▶" : "📄");

            var btn = new Button
            {
                Text = $"   {icon}   {les.Title}",
                TextAlign = ContentAlignment.MiddleLeft,
                Width = width,
                Height = 40,
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.White,
                ForeColor = les.IsCompleted ? Color.FromArgb(40, 167, 69) : Color.FromArgb(73, 80, 87),
                Font = new Font("Segoe UI", 9.25f, FontStyle.Regular),
                Cursor = Cursors.Hand,
                Padding = new Padding(45, 0, 10, 0),
                Tag = les,
                Margin = new Padding(0)
            };
            btn.FlatAppearance.BorderSize = 0;
            btn.FlatAppearance.MouseOverBackColor = Color.FromArgb(233, 236, 239);
            btn.Click += async (s, e) => await LoadLessonContentAsync(les);

            btn.MouseEnter += (s, e) => {
                if (_currentLesson != les)
                    btn.BackColor = Color.FromArgb(233, 236, 239);
            };
            btn.MouseLeave += (s, e) => {
                if (_currentLesson != les)
                    btn.BackColor = Color.White;
            };

            return btn;
        }

        private void ResizeCurriculumItems()
        {
            int w = flpCurriculum.ClientSize.Width - SystemInformation.VerticalScrollBarWidth - 10;
            foreach (Control c in flpCurriculum.Controls)
            {
                c.Width = w;
                if (c is Panel pnlMod)
                {
                    foreach (Control child in pnlMod.Controls)
                    {
                        child.Width = w;
                        if (child is FlowLayoutPanel flp)
                        {
                            foreach (Control btn in flp.Controls)
                            {
                                btn.Width = w;
                            }
                        }
                    }
                }
            }
        }

        // =========================================================
        // LOAD LESSON CONTENT
        // =========================================================
        private async Task LoadLessonContentAsync(LessonDto lesson)
        {
            _currentLesson = lesson;
            HighlightCurrentLesson(lesson);
            pnlVideoArea.Controls.Clear();
            _currentWebView = null;
            ClearQuizArea();

            string type = (lesson.LessonType ?? "").ToLower();

            if (type == "video" && lesson.VideoContent != null)
            {
                await LoadVideoContent(lesson.VideoContent.VideoUrl);
            }
            else if (type == "text" && lesson.TextContent != null)
            {
                await LoadTextContent(lesson.TextContent.Content);
            }
            else if (type == "quiz" && lesson.QuizContent != null)
            {
                LoadQuizContent(lesson.QuizContent);
            }
            else if (type == "coding" && lesson.CodingProblem != null)
            {
                // Optional: load coding problem UI (not implemented here)
                ShowCodingPlaceholder();
            }
            else
            {
                LoadEmptyContent();
            }

            UpdateDescription(lesson);
            UpdateNavButtons();
        }

        private void ShowCodingPlaceholder()
        {
            pnlVideoArea.BackColor = Color.FromArgb(248, 249, 250);
            var lbl = new Label
            {
                Text = "🧩 Bài tập lập trình (coding) — tính năng tương tác đang được phát triển.",
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleCenter,
                ForeColor = Color.FromArgb(108, 117, 125),
                Font = new Font("Segoe UI", 13f, FontStyle.Regular)
            };
            pnlVideoArea.Controls.Add(lbl);
        }

        private async Task LoadVideoContent(string url)
        {
            pnlVideoArea.BackColor = Color.Black;

            if (_currentWebView == null)
            {
                _currentWebView = new WebView2 { Dock = DockStyle.Fill };
                pnlVideoArea.Controls.Add(_currentWebView);

                try
                {
                    await _currentWebView.EnsureCoreWebView2Async(null);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi khởi tạo WebView2: {ex.Message}\n\nVui lòng cài đặt Microsoft Edge WebView2 Runtime.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            else
            {
                if (!pnlVideoArea.Controls.Contains(_currentWebView))
                    pnlVideoArea.Controls.Add(_currentWebView);
            }

            try
            {
                if (url.Contains("youtube.com") || url.Contains("youtu.be"))
                {
                    string videoId = ExtractYouTubeVideoId(url);
                    if (!string.IsNullOrEmpty(videoId))
                    {
                        string embedUrl = $"https://www.youtube.com/embed/{videoId}?autoplay=1&modestbranding=1&rel=0";
                        _currentWebView.CoreWebView2.Navigate(embedUrl);
                    }
                    else
                    {
                        ShowErrorMessage("Không thể phát video YouTube. URL không hợp lệ.");
                    }
                }
                else
                {
                    string html = $@"
                        <!DOCTYPE html>
                        <html>
                        <head>
                            <style>
                                * {{ margin:0; padding:0; }}
                                body {{ background:#000; display:flex; align-items:center; justify-content:center; height:100vh; }}
                                video {{ max-width:100%; max-height:100%; }}
                            </style>
                        </head>
                        <body>
                            <video controls autoplay>
                                <source src='{url}' type='video/mp4'>
                                Trình duyệt không hỗ trợ định dạng video này.
                            </video>
                        </body>
                        </html>";
                    _currentWebView.CoreWebView2.NavigateToString(html);
                }
            }
            catch (Exception ex)
            {
                ShowErrorMessage($"Không thể tải video: {ex.Message}");
            }
        }

        private async Task LoadTextContent(string content)
        {
            pnlVideoArea.BackColor = Color.White;

            if (_currentWebView == null)
            {
                _currentWebView = new WebView2 { Dock = DockStyle.Fill };
                pnlVideoArea.Controls.Add(_currentWebView);
                await _currentWebView.EnsureCoreWebView2Async(null);
            }
            else
            {
                if (!pnlVideoArea.Controls.Contains(_currentWebView))
                    pnlVideoArea.Controls.Add(_currentWebView);
            }

            string html = $@"
                <!DOCTYPE html>
                <html>
                <head>
                    <meta charset='utf-8'>
                    <meta name='viewport' content='width=device-width, initial-scale=1'>
                    <style>
                        * {{ box-sizing: border-box; }}
                        body {{ 
                            font-family: 'Segoe UI', system-ui, sans-serif; 
                            padding: 40px 60px; 
                            font-size: 16px; 
                            color: #212529; 
                            line-height: 1.75; 
                            background: #fff;
                            max-width: 900px;
                            margin: 0 auto;
                        }}
                        h1 {{ color: #1a1a1a; font-size: 2em; margin: 1.5em 0 0.5em; font-weight: 700; }}
                        h2 {{ color: #333; font-size: 1.5em; margin: 1.25em 0 0.5em; font-weight: 600; }}
                        h3 {{ color: #444; font-size: 1.25em; margin: 1em 0 0.5em; font-weight: 600; }}
                        p {{ margin: 0 0 1.25em; }}
                        pre {{ 
                            background: #f8f9fa; 
                            padding: 1em; 
                            border-radius: 6px; 
                            border-left: 3px solid #0d6efd; 
                            overflow-x: auto;
                            font-size: 14px;
                            line-height: 1.5;
                        }}
                        code {{ 
                            font-family: 'Consolas', monospace; 
                            background: #f1f3f5;
                            padding: 2px 6px;
                            border-radius: 3px;
                            color: #d63384;
                            font-size: 0.9em;
                        }}
                        pre code {{ background: transparent; padding: 0; color: inherit; }}
                        img {{ 
                            max-width: 100%; 
                            height: auto; 
                            display: block; 
                            margin: 1.5em auto;
                            border-radius: 8px;
                        }}
                        ul, ol {{ margin: 0 0 1.25em 1.5em; padding: 0; }}
                        li {{ margin-bottom: 0.5em; }}
                        blockquote {{
                            border-left: 4px solid #dee2e6;
                            padding-left: 1em;
                            margin: 1.5em 0;
                            color: #6c757d;
                        }}
                        a {{ color: #0d6efd; text-decoration: none; }}
                        a:hover {{ text-decoration: underline; }}
                    </style>
                </head>
                <body>{content}</body>
                </html>";

            _currentWebView.CoreWebView2.NavigateToString(html);
        }

        private void LoadEmptyContent()
        {
            _currentWebView = null;
            pnlVideoArea.BackColor = Color.FromArgb(248, 249, 250);
            var lbl = new Label
            {
                Text = "📝 Nội dung đang được cập nhật...",
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleCenter,
                ForeColor = Color.FromArgb(108, 117, 125),
                Font = new Font("Segoe UI", 13f, FontStyle.Regular)
            };
            pnlVideoArea.Controls.Add(lbl);
        }

        private void ShowErrorMessage(string message)
        {
            pnlVideoArea.Controls.Clear();
            pnlVideoArea.BackColor = Color.FromArgb(248, 249, 250);
            var lbl = new Label
            {
                Text = $"⚠️ {message}",
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleCenter,
                ForeColor = Color.FromArgb(220, 53, 69),
                Font = new Font("Segoe UI", 12f, FontStyle.Regular)
            };
            pnlVideoArea.Controls.Add(lbl);
        }

        // =========================
        // QUIZ RENDER & EVALUATION
        // =========================

        private void ClearQuizArea()
        {
            _currentQuizSelections.Clear();
            _currentQuizQuestionBoxes.Clear();
            if (_quizPanelContainer != null)
            {
                if (pnlVideoArea.Controls.Contains(_quizPanelContainer))
                    pnlVideoArea.Controls.Remove(_quizPanelContainer);
                _quizPanelContainer.Dispose();
                _quizPanelContainer = null;
            }
        }

        private void LoadQuizContent(LessonQuizDto quiz)
        {
            // Prepare container
            _quizPanelContainer = new Panel
            {
                Dock = DockStyle.Fill,
                AutoScroll = true,
                BackColor = Color.White
            };
            pnlVideoArea.Controls.Add(_quizPanelContainer);

            int margin = 12;
            int y = margin;

            // Title
            var lblTitle = new Label
            {
                Text = quiz.Title ?? "Quiz",
                Font = new Font("Segoe UI", 14F, FontStyle.Bold),
                ForeColor = Color.FromArgb(33, 37, 41),
                AutoSize = true,
                Location = new Point(margin, y)
            };
            _quizPanelContainer.Controls.Add(lblTitle);
            y += lblTitle.Height + 6;

            if (!string.IsNullOrWhiteSpace(quiz.Description))
            {
                var lblDesc = new Label
                {
                    Text = quiz.Description,
                    Font = new Font("Segoe UI", 10F),
                    ForeColor = Color.FromArgb(90, 90, 90),
                    AutoSize = false,
                    Size = new Size(_quizPanelContainer.ClientSize.Width - margin * 2, 0),
                    MaximumSize = new Size(_quizPanelContainer.ClientSize.Width - margin * 2, 200),
                    Location = new Point(margin, y)
                };
                lblDesc.AutoSize = true;
                _quizPanelContainer.Controls.Add(lblDesc);
                y += lblDesc.Height + 12;
            }

            // Questions
            _currentQuizQuestionBoxes.Clear();
            _currentQuizSelections.Clear();

            int qIndex = 0;
            foreach (var q in quiz.Questions ?? new List<QuizQuestionDto>())
            {
                var grp = new GroupBox
                {
                    Text = $"Câu {qIndex + 1}",
                    Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                    Location = new Point(margin, y),
                    Size = new Size(Math.Max(600, _quizPanelContainer.ClientSize.Width - margin * 2), 140),
                    Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right,
                    BackColor = Color.White
                };

                var lblQ = new Label
                {
                    Text = q.Question,
                    Font = new Font("Segoe UI", 10F),
                    Location = new Point(8, 24),
                    AutoSize = true,
                    MaximumSize = new Size(grp.Width - 20, 0)
                };
                grp.Controls.Add(lblQ);

                int rbY = lblQ.Bottom + 8;
                int answerIndex = 0;
                var answers = q.Answers ?? Array.Empty<string>();

                foreach (var ans in answers)
                {
                    var rb = new RadioButton
                    {
                        Text = ans,
                        Tag = new { QuestionId = q.QuestionID, AnswerIndex = answerIndex },
                        Location = new Point(12, rbY),
                        AutoSize = true,
                        Font = new Font("Segoe UI", 9F),
                        ForeColor = Color.FromArgb(73, 80, 87)
                    };
                    rb.CheckedChanged += QuizAnswer_CheckedChanged;
                    grp.Controls.Add(rb);
                    rbY += rb.Height + 6;
                    answerIndex++;
                }

                // Placeholder explanation label (hidden initially) so we can expand cleanly later
                var lblExpPlaceholder = new Label
                {
                    Name = "lblExp",
                    Text = "",
                    Font = new Font("Segoe UI", 9F, FontStyle.Italic),
                    ForeColor = Color.FromArgb(90, 90, 90),
                    AutoSize = true,
                    Location = new Point(8, rbY + 6),
                    MaximumSize = new Size(grp.ClientSize.Width - 16, 0),
                    Visible = false,
                    Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right
                };
                grp.Controls.Add(lblExpPlaceholder);

                // Reserve a small space so explanation won't overlap
                grp.Height = rbY + 12 + 6;
                _quizPanelContainer.Controls.Add(grp);
                _currentQuizQuestionBoxes.Add(grp);

                y += grp.Height + 8;
                qIndex++;
            }

            // Submit button
            var btnSubmit = new Button
            {
                Text = "Nộp bài",
                BackColor = Color.FromArgb(13, 110, 253),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                Size = new Size(120, 40),
                Location = new Point(margin, y)
            };
            btnSubmit.FlatAppearance.BorderSize = 0;
            btnSubmit.Click += (s, e) => EvaluateQuiz(quiz);
            _quizPanelContainer.Controls.Add(btnSubmit);

            // Ensure layout updates
            _quizPanelContainer.PerformLayout();
        }

        private void QuizAnswer_CheckedChanged(object sender, EventArgs e)
        {
            if (!(sender is RadioButton rb)) return;
            if (!rb.Checked) return;

            // Tag contains QuestionId and AnswerIndex
            dynamic tag = rb.Tag;
            Guid qid = tag.QuestionId;
            int ansIndex = tag.AnswerIndex;

            if (_currentQuizSelections.ContainsKey(qid))
                _currentQuizSelections[qid] = ansIndex;
            else
                _currentQuizSelections.Add(qid, ansIndex);
        }

        // Replace EvaluateQuiz implementation with this improved version
        private void EvaluateQuiz(LessonQuizDto quiz)
        {
            if (quiz?.Questions == null || quiz.Questions.Count == 0)
            {
                MessageBox.Show("Không có câu hỏi để đánh giá.", "Thông tin", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            int total = quiz.Questions.Count;
            int correct = 0;

            // 1. Bảng màu chuẩn (Style giống Bootstrap/Web hiện đại)
            var colorCorrectText = Color.FromArgb(25, 135, 84);   // Xanh lá đậm
            var colorWrongText = Color.FromArgb(220, 53, 69);     // Đỏ đậm
            var colorMutedText = Color.FromArgb(108, 117, 125);   // Xám

            var colorCorrectBg = Color.FromArgb(209, 231, 221);   // Nền xanh nhạt
            var colorWrongBg = Color.FromArgb(248, 215, 218);     // Nền đỏ nhạt
            var colorSkippedBg = Color.FromArgb(255, 243, 205);   // Nền vàng nhạt (cho câu chưa làm)

            GroupBox firstWrongQuestion = null; // Để scroll tới câu sai đầu tiên

            foreach (var q in quiz.Questions)
            {
                // Lấy đáp án người dùng chọn (-1 là chưa chọn)
                int selected = _currentQuizSelections.TryGetValue(q.QuestionID, out var s) ? s : -1;
                bool isCorrect = selected == q.CorrectIndex;
                bool isSkipped = selected == -1;

                if (isCorrect) correct++;

                // Tìm GroupBox tương ứng trên giao diện
                var grp = _currentQuizQuestionBoxes.FirstOrDefault(g => g.Controls.OfType<Label>().Any(l => l.Text == q.Question));
                if (grp == null) continue;

                // Lưu câu sai đầu tiên để tí nữa scroll tới
                if (!isCorrect && firstWrongQuestion == null) firstWrongQuestion = grp;

                // 2. Cập nhật Status Label (Góc trên phải)
                var lblStatus = grp.Controls.OfType<Label>().FirstOrDefault(l => l.Name == "lblStatus");
                if (lblStatus == null)
                {
                    lblStatus = new Label
                    {
                        Name = "lblStatus",
                        AutoSize = true,
                        Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                        Anchor = AnchorStyles.Top | AnchorStyles.Right
                    };
                    grp.Controls.Add(lblStatus);
                }

                if (isSkipped)
                {
                    lblStatus.Text = "● Chưa làm";
                    lblStatus.ForeColor = Color.FromArgb(102, 77, 3); // Màu nâu vàng
                    grp.BackColor = colorSkippedBg;
                }
                else if (isCorrect)
                {
                    lblStatus.Text = "✓ Đúng";
                    lblStatus.ForeColor = colorCorrectText;
                    grp.BackColor = colorCorrectBg;
                }
                else
                {
                    lblStatus.Text = "✖ Sai";
                    lblStatus.ForeColor = colorWrongText;
                    grp.BackColor = colorWrongBg;
                }

                // Căn chỉnh vị trí label status
                lblStatus.Left = Math.Max(8, grp.ClientSize.Width - lblStatus.PreferredWidth - 12);
                lblStatus.Top = 6;

                // 3. Tô màu các đáp án (RadioButtons)
                foreach (var rb in grp.Controls.OfType<RadioButton>())
                {
                    dynamic tag = rb.Tag;
                    int answerIndex = tag.AnswerIndex; // Index của dòng này

                    // Khóa không cho chọn lại
                    rb.Enabled = false;

                    // Giữ nguyên trạng thái đã check
                    rb.Checked = (answerIndex == selected);

                    // Logic tô màu chi tiết:
                    if (answerIndex == q.CorrectIndex)
                    {
                        // Đây là ĐÁP ÁN ĐÚNG -> Luôn tô xanh đậm + in đậm (để user biết đâu là đúng)
                        rb.ForeColor = colorCorrectText;
                        rb.Font = new Font(rb.Font, FontStyle.Bold);

                        // Nếu user chọn sai, thêm hậu tố "(Đáp án đúng)" để nhấn mạnh
                        if (!isCorrect) rb.Text += "  (Đáp án đúng)";
                    }
                    else if (answerIndex == selected && !isCorrect)
                    {
                        // Đây là ĐÁP ÁN SAI mà user ĐÃ CHỌN -> Tô đỏ + in đậm
                        rb.ForeColor = colorWrongText;
                        rb.Font = new Font(rb.Font, FontStyle.Bold);
                    }
                    else
                    {
                        // Các đáp án khác -> Màu xám mờ
                        rb.ForeColor = colorMutedText;
                        rb.Font = new Font(rb.Font, FontStyle.Regular);
                    }
                }

                // 4. Hiển thị giải thích (Explanation)
                var lblExp = grp.Controls.OfType<Label>().FirstOrDefault(l => l.Name == "lblExp");
                if (lblExp != null)
                {
                    // Set nội dung và hiển thị
                    lblExp.Text = string.IsNullOrWhiteSpace(q.Explanation)
                        ? "(Không có giải thích chi tiết)"
                        : $"💡 Giải thích: {q.Explanation}";

                    lblExp.Visible = true;
                    lblExp.AutoSize = true;

                    // Tính toán vị trí: Nằm dưới cùng của list đáp án
                    int bottomOfAnswers = 0;
                    foreach (Control c in grp.Controls)
                    {
                        if (c is RadioButton) bottomOfAnswers = Math.Max(bottomOfAnswers, c.Bottom);
                    }

                    lblExp.Location = new Point(8, bottomOfAnswers + 10);
                    lblExp.MaximumSize = new Size(grp.ClientSize.Width - 16, 0); // Word wrap

                    // Resize GroupBox để chứa đủ phần giải thích
                    grp.Height = lblExp.Bottom + 15;
                }
            }

            // 5. Scroll tới câu sai đầu tiên (UX Improvement)
            if (firstWrongQuestion != null)
            {
                _quizPanelContainer.ScrollControlIntoView(firstWrongQuestion);
            }
            else
            {
                // Nếu đúng hết thì scroll xuống cuối (chỗ nút nộp bài)
                _quizPanelContainer.AutoScrollPosition = new Point(0, _quizPanelContainer.VerticalScroll.Maximum);
            }

            // 6. Tổng kết và lưu tiến độ
            MessageBox.Show($"Bạn đúng {correct} / {total} câu.", "Kết quả Quiz", MessageBoxButtons.OK, MessageBoxIcon.Information);

            if (correct == total && _currentLesson != null)
            {
                var user = GlobalStore.user;
                if (user != null)
                {
                    _ = Task.Run(async () =>
                    {
                        try
                        {
                            await _progressService.MarkLessonCompletedAsync(user.UserID, _currentLesson.LessonID);
                        }
                        catch { /* ignore */ }
                    });
                }
            }
        }

        private void UpdateDescription(LessonDto lesson)
        {
            string type = (lesson.LessonType ?? "").ToLower();
            string typeText = type == "video" ? "Video bài học" :
                             type == "text" ? "Nội dung văn bản" :
                             type == "quiz" ? "Bài kiểm tra" :
                             "Bài học";

            wbDescription.DocumentText = $@"
                <html>
                <head>
                    <meta charset='utf-8'>
                    <style>
                        body {{ font-family:'Segoe UI'; padding:12px; color:#212529; margin:0; }}
                        h3 {{ margin:0 0 8px 0; font-size:16px; }}
                        p {{ color:#6c757d; margin:0; font-size:13px; }}
                    </style>
                </head>
                <body>
                    <h3>{lesson.Title}</h3>
                    <p>{typeText}</p>
                </body>
                </html>";
        }

        private string ExtractYouTubeVideoId(string url)
        {
            try
            {
                // youtu.be format
                if (url.Contains("youtu.be/"))
                {
                    var match = Regex.Match(url, @"youtu\.be/([a-zA-Z0-9_-]{11})");
                    if (match.Success) return match.Groups[1].Value;
                }

                // youtube.com/watch format
                if (url.Contains("youtube.com/watch"))
                {
                    var match = Regex.Match(url, @"[?&]v=([a-zA-Z0-9_-]{11})");
                    if (match.Success) return match.Groups[1].Value;
                }

                // youtube.com/embed format
                if (url.Contains("youtube.com/embed/"))
                {
                    var match = Regex.Match(url, @"embed/([a-zA-Z0-9_-]{11})");
                    if (match.Success) return match.Groups[1].Value;
                }
            }
            catch { }

            return null;
        }

        // =========================================================
        // PROGRESS TRACKING
        // =========================================================
        private async Task MarkLessonCompleted()
        {
            if (_currentLesson == null) return;

            var user = GlobalStore.user;
            if (user == null)
            {
                MessageBox.Show("Vui lòng đăng nhập để lưu tiến độ học tập.", "Yêu cầu đăng nhập", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            try
            {
                bool success = await _progressService.MarkLessonCompletedAsync(user.UserID, _currentLesson.LessonID);

                if (success)
                {
                    _currentLesson.IsCompleted = true;
                    UpdateNavButtons();
                    RefreshSidebar();
                    UpdateProgressBar();

                    int idx = _flatLessonList.IndexOf(_currentLesson);
                    if (idx < _flatLessonList.Count - 1)
                    {
                        await Task.Delay(500);
                        await LoadLessonContentAsync(_flatLessonList[idx + 1]);
                    }
                    else
                    {
                        MessageBox.Show("🎉 Chúc mừng! Bạn đã hoàn thành khóa học này!", "Hoàn thành", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    MessageBox.Show("Không thể cập nhật tiến độ. Vui lòng thử lại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void UpdateProgressBar()
        {
            if (_flatLessonList.Count == 0) return;

            int completedCount = _flatLessonList.Count(l => l.IsCompleted);
            int percent = (completedCount * 100) / _flatLessonList.Count;

            pbProgress.Value = Math.Min(percent, 100);
            lblProgress.Text = $"{completedCount}/{_flatLessonList.Count}";
        }

        private void RefreshSidebar()
        {
            foreach (Control c in flpCurriculum.Controls)
            {
                if (c is Panel pnlMod)
                {
                    foreach (Control child in pnlMod.Controls)
                    {
                        if (child is FlowLayoutPanel flp)
                        {
                            foreach (Control btn in flp.Controls)
                            {
                                if (btn is Button button && button.Tag is LessonDto les)
                                {
                                    string icon = les.IsCompleted ? "✓" : (les.LessonType?.ToLower() == "video" ? "▶" : "📄");
                                    button.Text = $"   {icon}   {les.Title}";
                                    button.ForeColor = les.IsCompleted ? Color.FromArgb(40, 167, 69) : Color.FromArgb(73, 80, 87);
                                }
                            }
                        }
                    }
                }
            }
        }

        private void UpdateNavButtons()
        {
            if (_currentLesson == null) return;

            int idx = _flatLessonList.IndexOf(_currentLesson);
            btnPrev.Enabled = idx > 0;
            btnNext.Enabled = idx < _flatLessonList.Count - 1;

            if (_currentLesson.IsCompleted)
            {
                btnMarkCompleted.Text = "✓ Đã hoàn thành";
                btnMarkCompleted.BackColor = Color.FromArgb(108, 117, 125);
                btnMarkCompleted.Enabled = false;
            }
            else
            {
                btnMarkCompleted.Text = "✓ Hoàn thành bài";
                btnMarkCompleted.BackColor = Color.FromArgb(40, 167, 69);
                btnMarkCompleted.Enabled = true;
            }
        }

        private void HighlightCurrentLesson(LessonDto current)
        {
            foreach (Control c in flpCurriculum.Controls)
            {
                if (c is Panel pnlMod)
                {
                    foreach (Control child in pnlMod.Controls)
                    {
                        if (child is FlowLayoutPanel flp)
                        {
                            bool foundInModule = false;

                            foreach (Control btn in flp.Controls)
                            {
                                if (btn is Button button)
                                {
                                    if (button.Tag == current)
                                    {
                                        button.BackColor = Color.FromArgb(207, 226, 255);
                                        button.Font = new Font("Segoe UI", 9.25f, FontStyle.Bold);
                                        if (!current.IsCompleted)
                                            button.ForeColor = Color.FromArgb(13, 110, 253);
                                        foundInModule = true;
                                    }
                                    else if (button.Tag is LessonDto les)
                                    {
                                        button.BackColor = Color.White;
                                        button.Font = new Font("Segoe UI", 9.25f, FontStyle.Regular);
                                        button.ForeColor = les.IsCompleted ? Color.FromArgb(40, 167, 69) : Color.FromArgb(73, 80, 87);
                                    }
                                }
                            }

                            if (foundInModule && !flp.Visible)
                            {
                                flp.Visible = true;
                                foreach (Control headerCtrl in pnlMod.Controls)
                                {
                                    if (headerCtrl is Button headerBtn && headerCtrl.Dock == DockStyle.Top)
                                    {
                                        string title = headerBtn.Text.Replace("▶", "").Replace("▼", "").Trim();
                                        headerBtn.Text = "▼  " + title;
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        private async void NavigateLesson(int direction)
        {
            if (_currentLesson == null) return;

            int idx = _flatLessonList.IndexOf(_currentLesson);
            int newIdx = idx + direction;

            if (newIdx >= 0 && newIdx < _flatLessonList.Count)
            {
                await LoadLessonContentAsync(_flatLessonList[newIdx]);
            }
        }
    }
}