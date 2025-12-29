using System;
using System.Drawing;
using System.Net;
using System.Windows.Forms;

namespace CodeForge_Desktop.Presentation.Forms.Admin
{
    /// <summary>
    /// Improved Course Card with action buttons at the bottom.
    /// Clean, modern design with clear separation of view and actions.
    /// </summary>
    public class CourseCard : UserControl
    {
        private PictureBox pbThumb;
        private Label lblTitle;
        private Label lblMeta;
        private Label lblPrice;
        private Label lblStatus;
        private Label lblBadge;
        private Button btnEdit;
        private Button btnDelete;
        private Button btnManage;

        public Guid CourseId { get; private set; }

        public event EventHandler<Guid> ManageClicked;
        public event EventHandler<Guid> EditClicked;
        public event EventHandler<Guid> DeleteClicked;
        public event EventHandler<Guid> CardClicked;

        public CourseCard()
        {
            InitializeComponents();
        }

        private void InitializeComponents()
        {
            this.Width = 300;
            this.Height = 420;
            this.Margin = new Padding(12);
            this.BackColor = Color.White;
            this.BorderStyle = BorderStyle.FixedSingle;
            this.Cursor = Cursors.Hand;

            // Thumbnail
            pbThumb = new PictureBox
            {
                Location = new Point(8, 8),
                Size = new Size(284, 160),
                SizeMode = PictureBoxSizeMode.Zoom,
                BackColor = Color.FromArgb(250, 250, 252)
            };

            // Status badge (top-left corner of thumbnail)
            lblBadge = new Label
            {
                Location = new Point(12, 12),
                AutoSize = true,
                Padding = new Padding(8, 4, 8, 4),
                BackColor = Color.FromArgb(255, 204, 0),
                ForeColor = Color.Black,
                Font = new Font("Segoe UI", 8F, FontStyle.Bold),
                Text = ""
            };

            // Course Title
            lblTitle = new Label
            {
                Location = new Point(12, 176),
                Size = new Size(276, 50),
                Font = new Font("Segoe UI", 12F, FontStyle.Bold),
                ForeColor = Color.FromArgb(34, 34, 34),
                AutoEllipsis = false,
                Text = ""
            };

            // Course Meta (Level, Language)
            lblMeta = new Label
            {
                Location = new Point(12, 226),
                Size = new Size(276, 30),
                Font = new Font("Segoe UI", 9F),
                ForeColor = Color.FromArgb(100, 100, 100),
                AutoEllipsis = true,
                Text = ""
            };

            // Price
            lblPrice = new Label
            {
                Location = new Point(12, 260),
                Size = new Size(276, 22),
                Font = new Font("Segoe UI", 11F, FontStyle.Bold),
                ForeColor = Color.FromArgb(34, 139, 34),
                Text = ""
            };

            // Status text
            lblStatus = new Label
            {
                Location = new Point(12, 285),
                Size = new Size(276, 16),
                Font = new Font("Segoe UI", 8F),
                ForeColor = Color.Gray,
                Text = ""
            };

            // Action Buttons (Bottom section)
            int btnWidth = 88;
            int btnHeight = 32;
            int btnY = 310;
            int spacing = 4;

            btnEdit = new Button
            {
                Text = "✎ Edit",
                Location = new Point(12, btnY),
                Size = new Size(btnWidth, btnHeight),
                BackColor = Color.FromArgb(52, 152, 219),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                Cursor = Cursors.Hand
            };

            btnDelete = new Button
            {
                Text = "✕ Delete",
                Location = new Point(12 + btnWidth + spacing, btnY),
                Size = new Size(btnWidth, btnHeight),
                BackColor = Color.FromArgb(231, 76, 60),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                Cursor = Cursors.Hand
            };

            btnManage = new Button
            {
                Text = "⚙ Manage",
                Location = new Point(12 + (btnWidth + spacing) * 2, btnY),
                Size = new Size(btnWidth, btnHeight),
                BackColor = Color.FromArgb(155, 89, 182),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
           
            // With these lines:
            btnEdit.Click += (s, e) => { EditClicked?.Invoke(this, CourseId); };
            btnDelete.Click += (s, e) => { DeleteClicked?.Invoke(this, CourseId); };
            btnManage.Click += (s, e) => { ManageClicked?.Invoke(this, CourseId); };

            // Hover effects
            this.MouseEnter += (s, e) => 
            { 
                this.BackColor = Color.FromArgb(245, 250, 255); 
                this.BorderStyle = BorderStyle.Fixed3D;
                this.Shadow();
            };
            this.MouseLeave += (s, e) => 
            { 
                this.BackColor = Color.White; 
                this.BorderStyle = BorderStyle.FixedSingle;
            };

            // Click on content area to select card
            Action<object, EventArgs> selectCard = (s, e) => CardClicked?.Invoke(this, CourseId);
            pbThumb.Click += (s, e) => selectCard(s, e);
            lblTitle.Click += (s, e) => selectCard(s, e);
            lblMeta.Click += (s, e) => selectCard(s, e);
            lblPrice.Click += (s, e) => selectCard(s, e);

            this.Controls.AddRange(new Control[] { 
                pbThumb, lblBadge, lblTitle, lblMeta, lblPrice, lblStatus,
                btnEdit, btnDelete, btnManage 
            });
        }

        /// <summary>
        /// Populate card with course data
        /// </summary>
        public void SetData(Guid courseId, string title, string level, string language, decimal price, decimal discount, string status, string thumbnailPathOrUrl = null)
        {
            CourseId = courseId;
            lblTitle.Text = title ?? "(Untitled Course)";
            lblMeta.Text = $"{level ?? "-"} • {language ?? "-"}";
            
            var finalPrice = price - (price * (discount / 100m));
            lblPrice.Text = finalPrice > 0 ? $"${finalPrice:F2}" : "Free";
            lblStatus.Text = $"Status: {status ?? "draft"}";
            
            if (discount > 0)
                lblBadge.Text = $"{discount:F0}% OFF";
            else if (finalPrice <= 0)
                lblBadge.Text = "FREE";
            else
                lblBadge.BackColor = Color.FromArgb(100, 100, 100);

            LoadThumbnail(thumbnailPathOrUrl);
        }

        private void LoadThumbnail(string pathOrUrl)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(pathOrUrl))
                {
                    if (pathOrUrl.StartsWith("http", StringComparison.OrdinalIgnoreCase))
                    {
                        using (var wc = new WebClient())
                        {
                            var data = wc.DownloadData(pathOrUrl);
                            using (var ms = new System.IO.MemoryStream(data))
                                pbThumb.Image = Image.FromStream(ms);
                        }
                    }
                    else if (System.IO.File.Exists(pathOrUrl))
                    {
                        pbThumb.Image = Image.FromFile(pathOrUrl);
                    }
                }
            }
            catch { /* Use placeholder */ }

            if (pbThumb.Image == null)
            {
                var bmp = new Bitmap(pbThumb.Width, pbThumb.Height);
                using (var g = Graphics.FromImage(bmp))
                {
                    g.Clear(Color.FromArgb(240, 240, 240));
                    using (var f = new Font("Segoe UI", 10F))
                    using (var br = new SolidBrush(Color.Gray))
                    {
                        g.DrawString("No Thumbnail", f, br, new PointF(pbThumb.Width / 2 - 50, pbThumb.Height / 2 - 8));
                    }
                }
                pbThumb.Image = bmp;
            }
        }

        private void Shadow()
        {
            // Optional: Add shadow effect with slight padding adjustment
        }
    }
}