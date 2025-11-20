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
            _courseData.Columns.Add("ThumbnailImage", typeof(Image));
        }

        private async Task LoadCoursesAsync()
        {
            try
            {
                EnsureCourseDataTable();
                _courseData.Rows.Clear();

                List<Course> courses = await _courseRepository.GetAllAsync();
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

                    // Prefer online URL if provided
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
                                catch
                                {
                                    thumb = null;
                                }
                            }
                            else if (File.Exists(c.Thumbnail))
                            {
                                try
                                {
                                    thumb = Image.FromFile(c.Thumbnail);
                                }
                                catch
                                {
                                    thumb = null;
                                }
                            }
                        }
                        catch
                        {
                            thumb = null;
                        }
                    }

                    // If still null, generate color icon based on language
                    if (thumb == null)
                    {
                        Color iconColor = Color.FromArgb(255, 165, 0); // default
                        if (string.Equals(c.Language, "Python", StringComparison.OrdinalIgnoreCase)) iconColor = Color.FromArgb(50, 205, 50);
                        else if (c.Language != null && c.Language.IndexOf("c++", StringComparison.OrdinalIgnoreCase) >= 0) iconColor = Color.FromArgb(255, 69, 0);
                        else if (string.Equals(c.Language, "Java", StringComparison.OrdinalIgnoreCase)) iconColor = Color.FromArgb(128, 0, 128);

                        thumb = GetRoundedRectIcon(iconColor);
                    }

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

                // Read UI values on UI thread
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

                // Map "Tất cả level" to null (adjust depending on exact ComboBox item text)
                if (!string.IsNullOrEmpty(selectedLevel) &&
                    (selectedLevel.Equals("Tất cả level", StringComparison.OrdinalIgnoreCase) ||
                     selectedLevel.Equals("All", StringComparison.OrdinalIgnoreCase)))
                {
                    selectedLevel = null;
                }

                // Call repository search with level -> SQL will handle filtering
                List<Course> courses = await _courseRepository.SearchAsync(keyword ?? "", selectedLevel);

                EnsureCourseDataTable();
                await PopulateTableFromCoursesAsync(courses);

                // Rebind datasource (keep selection handling)
                dgvCourses.DataSource = _courseData;

                // Select first row if available
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
                // Fail quietly but notify
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
            if (row.Cells["CourseID"].Value == DBNull.Value) return;

            // Lấy dữ liệu
            _selectedCourseId = (Guid)row.Cells["CourseID"].Value;
            var title = row.Cells["colTitle"].Value?.ToString() ?? "";
            var level = row.Cells["colLevel"].Value?.ToString() ?? "";
            var language = row.Cells["colLanguage"].Value?.ToString() ?? "";
            var rating = 0.0;
            double.TryParse(row.Cells["colRating"].Value?.ToString() ?? "0", out rating);
            var overview = row.Cells["ShortOverview"].Value?.ToString() ?? "";
            var students = 0;
            int.TryParse(row.Cells["TotalStudents"].Value?.ToString() ?? "0", out students);
            var duration = 0;
            int.TryParse(row.Cells["Duration"].Value?.ToString() ?? "0", out duration);

            // Use the image from the grid (downloaded online or generated)
            var cellImg = row.Cells["colThumbnail"].Value as Image;
            if (cellImg != null)
            {
                pbThumbnail.Image = cellImg;
            }
            else
            {
                // Xác định màu icon lớn (dựa trên Level/Language như dữ liệu mẫu)
                Color iconColor = Color.FromArgb(255, 165, 0); // Default: Cam
                if (language != null && language.IndexOf("Python", StringComparison.OrdinalIgnoreCase) >= 0) iconColor = Color.FromArgb(50, 205, 50);
                else if (language != null && language.IndexOf("C++", StringComparison.OrdinalIgnoreCase) >= 0) iconColor = Color.FromArgb(255, 69, 0);
                else if (language != null && language.IndexOf("Java", StringComparison.OrdinalIgnoreCase) >= 0) iconColor = Color.FromArgb(128, 0, 128);

                pbThumbnail.Image = GetRoundedRectIcon(iconColor, 150, 15); // Kích thước lớn hơn và bo góc
            }

            lblTitle.Text = title;
            lblMeta.Text = $"{level} | {language} | ⭐️ {rating:N1}";
            txtShortOverview.Text = overview;

            lblStudents.Text = $"{students:N0} người";
            lblDuration.Text = $"{duration / 60} giờ";

            // Logic nút Đăng ký/Tiếp tục (Mô phỏng)
            bool isEnrolled = title != "C++ Algorithms";
            if (isEnrolled)
            {
                btnEnrollContinue.Text = "▶️ Tiếp tục học";
                btnEnrollContinue.BackColor = Color.FromArgb(0, 177, 64); // Xanh lá cây cho Tiếp tục
                btnViewDetails.FlatAppearance.BorderColor = Color.FromArgb(0, 120, 215); // Xanh dương
                btnViewDetails.ForeColor = Color.FromArgb(0, 120, 215);
            }
            else
            {
                btnEnrollContinue.Text = $"💰 Đăng ký / Tiếp tục";
                btnEnrollContinue.BackColor = Color.FromArgb(0, 120, 215); // Xanh dương cho Đăng ký
                btnViewDetails.FlatAppearance.BorderColor = Color.FromArgb(0, 120, 215);
                btnViewDetails.ForeColor = Color.FromArgb(0, 120, 215);
            }
        }

        private void ClearCoursePreview()
        {
            _selectedCourseId = Guid.Empty;
            lblTitle.Text = "Chọn một khóa học";
            lblMeta.Text = "";
            txtShortOverview.Text = "";
            lblStudents.Text = "";
            lblDuration.Text = "";
            pbThumbnail.Image = null;
            btnEnrollContinue.Text = "Đăng ký";
            btnEnrollContinue.BackColor = Color.Gray;
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

        private void btnEnrollContinue_Click(object sender, EventArgs e)
        {
            if (_selectedCourseId != Guid.Empty)
            {
                // Ensure _courseRepository is a CourseRepository instance
                var repo = _courseRepository as CourseRepository ?? new CourseRepository();
                MainFormStudent.Instance?.NavigateTo(new ucCourseLearning(_selectedCourseId, repo));
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