using CodeForge_Desktop.Business.Interfaces;
using System;
using System.Drawing;
using System.Windows.Forms;
using CodeForge_Desktop.Business.Services;

namespace CodeForge_Desktop.Presentation.Forms.Student
{
    public partial class ucProblemList : UserControl
    {
        private const string PlaceholderText = "Tìm kiếm bài tập...";
        private ICodingProblemService _problemService;
        public ucProblemList()
        {
            _problemService = new CodingProblemService();

            InitializeComponent();
            SetupPlaceholder();
            SetupSearchBox();
            SetupDataGridViewStyles();
            LoadDataFromDatabase();

            // Đăng ký sự kiện CellPainting để tô màu
            dgvProblemList.CellPainting += DgvProblemList_CellPainting;
        }

        /// <summary>
        /// Lấy dữ liệu từ database và đổ vào DataGridView
        /// </summary>
        private void LoadDataFromDatabase()
        {
            try
            {
                dgvProblemList.Rows.Clear();

                var problems = _problemService.GetAll();

                foreach (var problem in problems)
                {
                    int stt = 1;
                    dgvProblemList.Rows.Add(
                        stt++ ,
                        problem.Title,
                        problem.Difficulty,
                        problem.Tags,
                        "WAIT",
                        "-"
                    );
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Hàm tìm kiếm bài tập
        /// </summary>
        private void SearchProblems(string searchText)
        {
            try
            {
                dgvProblemList.Rows.Clear();

                var allProblems = _problemService.GetAll();
                var filteredProblems = allProblems.FindAll(p => 
                    p.Title.IndexOf(searchText, StringComparison.OrdinalIgnoreCase) >= 0 ||
                    p.Description.IndexOf(searchText, StringComparison.OrdinalIgnoreCase) >= 0
                );

                foreach (var problem in filteredProblems)
                {
                    dgvProblemList.Rows.Add(
                        problem.ProblemID.ToString(),
                        problem.Title,
                        problem.Difficulty,
                        problem.CreatedAt.ToString("yyyy-MM-dd"),
                        problem.Status,
                        "-"
                    );
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tìm kiếm: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Tùy chỉnh giao diện DataGridView
        /// </summary>
        private void SetupDataGridViewStyles()
        {
            dgvProblemList.EnableHeadersVisualStyles = false;

            dgvProblemList.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(240, 240, 240);
            dgvProblemList.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;
            dgvProblemList.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            dgvProblemList.ColumnHeadersDefaultCellStyle.Padding = new Padding(0, 5, 0, 5);

            dgvProblemList.BorderStyle = BorderStyle.None;
            dgvProblemList.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgvProblemList.DefaultCellStyle.Font = new Font("Segoe UI", 9.75F);
            dgvProblemList.DefaultCellStyle.ForeColor = Color.FromArgb(64, 64, 64);

            dgvProblemList.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvProblemList.MultiSelect = false;

            dgvProblemList.DefaultCellStyle.SelectionBackColor = Color.FromArgb(230, 230, 230);
            dgvProblemList.DefaultCellStyle.SelectionForeColor = Color.Black;

            dgvProblemList.RowsDefaultCellStyle.BackColor = Color.White;
            dgvProblemList.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(247, 249, 252);
        }

        /// <summary>
        /// Tô màu cho các ô theo trạng thái
        /// </summary>
        private void DgvProblemList_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex < 0) return;

            // Tô màu cột "Độ khó"
            if (e.ColumnIndex == dgvProblemList.Columns["colDifficulty"].Index && e.Value != null)
            {
                string difficulty = e.Value.ToString();
                Color textColor = Color.Black;

                switch (difficulty)
                {
                    case "Dễ":
                        textColor = Color.Green;
                        break;
                    case "Trung bình":
                        textColor = Color.Orange;
                        break;
                    case "Khó":
                        textColor = Color.Red;
                        break;
                }
                PaintCell(e, textColor);
            }

            // Tô màu cột "Trạng thái"
            if (e.ColumnIndex == dgvProblemList.Columns["colStatus"].Index && e.Value != null)
            {
                string status = e.Value.ToString();
                Color textColor = Color.Black;

                switch (status)
                {
                    case "SOLVED":
                        textColor = Color.Green;
                        break;
                    case "ATTEMPTED":
                        textColor = Color.Orange;
                        break;
                    case "NOT_STARTED":
                        textColor = Color.OrangeRed;
                        break;
                }
                PaintCell(e, textColor);
            }
        }

        private void PaintCell(DataGridViewCellPaintingEventArgs e, Color foreColor)
        {
            e.Paint(e.CellBounds, DataGridViewPaintParts.All & ~DataGridViewPaintParts.ContentForeground);
            TextRenderer.DrawText(e.Graphics, e.FormattedValue.ToString(), e.CellStyle.Font, e.CellBounds, foreColor, TextFormatFlags.VerticalCenter | TextFormatFlags.Left);
            e.Handled = true;
        }

        private void SetupSearchBox()
        {
            int paddingTop = (pnlSearchContainer.Height - txtSearch.Font.Height) / 2;
            if (paddingTop > 0)
            {
                txtSearch.Padding = new Padding(0, paddingTop, 0, 0);
            }

            // Sự kiện tìm kiếm khi nhập text
            txtSearch.TextChanged += (s, e) =>
            {
                string searchText = txtSearch.Text.Trim();
                if (searchText != PlaceholderText && !string.IsNullOrWhiteSpace(searchText))
                {
                    SearchProblems(searchText);
                }
                else if (string.IsNullOrWhiteSpace(searchText))
                {
                    LoadDataFromDatabase();
                }
            };
        }

        private void SetupPlaceholder()
        {
            txtSearch.Text = PlaceholderText;
            txtSearch.ForeColor = System.Drawing.Color.Gray;

            txtSearch.GotFocus += RemovePlaceholder;
            txtSearch.LostFocus += AddPlaceholder;
        }

        private void RemovePlaceholder(object sender, EventArgs e)
        {
            if (txtSearch.Text == PlaceholderText)
            {
                txtSearch.Text = "";
                txtSearch.ForeColor = System.Drawing.Color.Black;
            }
        }

        private void AddPlaceholder(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtSearch.Text))
            {
                txtSearch.Text = PlaceholderText;
                txtSearch.ForeColor = System.Drawing.Color.Gray;
            }
        }

        private void dgvProblemList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }
    }
}