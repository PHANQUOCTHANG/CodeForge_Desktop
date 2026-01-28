using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using CodeForge_Desktop.Business.Helpers;
using CodeForge_Desktop.Business.Interfaces;
using CodeForge_Desktop.DataAccess.Entities;
using System.Linq;

namespace CodeForge_Desktop.Presentation.Forms.Admin
{
    public partial class AddEditProblemForm : Form
    {
        private ICodingProblemService _problemService;
        private ITestCaseService _testCaseService;
        private Guid? _problemId;
        private List<TestCase> _testCases = new List<TestCase>();
        private TestCase _selectedTestCase = null;

        public AddEditProblemForm(ICodingProblemService problemService, ITestCaseService testCaseService, Guid? id = null)
        {
            InitializeComponent();
            _problemService = problemService;
            _testCaseService = testCaseService;
            _problemId = id;

            SetupDataGridView();

            // Setup Defaults
            cboDifficulty.SelectedIndex = 0;
            cboReturnType.SelectedIndex = 0;

            if (_problemId.HasValue)
            {
                lblTitle.Text = "Cập nhật bài tập";
                lblSubtitle.Text = "Chỉnh sửa thông tin bài tập, cấu hình code và test cases";
                LoadData();
            }
            else
            {
                lblTitle.Text = "Thêm bài tập mới";
                lblSubtitle.Text = "Điền đầy đủ thông tin bài tập, cấu hình code và test cases để hoàn tất";
            }

            // Event Handlers
            btnSave.Click += BtnSave_Click;
            btnCancel.Click += (s, e) => this.Close();
            btnAddTestCase.Click += BtnAddTestCase_Click;
            btnDeleteTestCase.Click += BtnDeleteTestCase_Click;
            dgvTestCases.SelectionChanged += DgvTestCases_SelectionChanged;

            // Auto-generate function name from title
            txtTitle.TextChanged += TxtTitle_TextChanged;
        }

        /// <summary>
        /// Tự động sinh tên hàm từ tiêu đề bài tập
        /// </summary>
        private void TxtTitle_TextChanged(object sender, EventArgs e)
        {
            if (!_problemId.HasValue) // Chỉ tự động sinh khi thêm mới
            {
                txtFunctionName.Text = GenerateFunctionName(txtTitle.Text);
            }
        }

        /// <summary>
        /// Sinh tên hàm từ tiêu đề
        /// VD: "Tính tổng hai số" -> "tinhTongHaiSo"
        /// </summary>
        private string GenerateFunctionName(string title)
        {
            if (string.IsNullOrWhiteSpace(title))
                return "";

            // Bỏ dấu tiếng Việt
            string normalized = RemoveVietnameseTones(title.Trim());

            // Chuyển thành camelCase
            string[] words = normalized.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            if (words.Length == 0)
                return "";

            // Từ đầu tiên viết thường, các từ sau viết hoa chữ cái đầu
            string functionName = words[0].ToLower();
            for (int i = 1; i < words.Length; i++)
            {
                if (words[i].Length > 0)
                {
                    functionName += char.ToUpper(words[i][0]) + words[i].Substring(1).ToLower();
                }
            }

            // Loại bỏ ký tự không hợp lệ
            functionName = Regex.Replace(functionName, @"[^a-zA-Z0-9_]", "");

            // Đảm bảo không bắt đầu bằng số
            if (functionName.Length > 0 && char.IsDigit(functionName[0]))
            {
                functionName = "_" + functionName;
            }

            return functionName;
        }

        /// <summary>
        /// Bỏ dấu tiếng Việt
        /// </summary>
        private string RemoveVietnameseTones(string text)
        {
            string[] vietnameseChars = new string[]
            {
                "aAeEoOuUiIdDyY",
                "áàạảãâấầậẩẫăắằặẳẵ",
                "ÁÀẠẢÃÂẤẦẬẨẪĂẮẰẶẲẴ",
                "éèẹẻẽêếềệểễ",
                "ÉÈẸẺẼÊẾỀỆỂỄ",
                "óòọỏõôốồộổỗơớờợởỡ",
                "ÓÒỌỎÕÔỐỒỘỔỖƠỚỜỢỞỠ",
                "úùụủũưứừựửữ",
                "ÚÙỤỦŨƯỨỪỰỬỮ",
                "íìịỉĩ",
                "ÍÌỊỈĨ",
                "đ",
                "Đ",
                "ýỳỵỷỹ",
                "ÝỲỴỶỸ"
            };

            for (int i = 1; i < vietnameseChars.Length; i++)
            {
                for (int j = 0; j < vietnameseChars[i].Length; j++)
                {
                    text = text.Replace(vietnameseChars[i][j], vietnameseChars[0][i - 1]);
                }
            }

            return text;
        }

        private void SetupDataGridView()
        {
            dgvTestCases.Columns.Clear();
            dgvTestCases.Columns.Add("TestCaseID", "ID");
            dgvTestCases.Columns.Add("Input", "Input");
            dgvTestCases.Columns.Add("ExpectedOutput", "Expected Output");
            dgvTestCases.Columns.Add("Explain", "Giải thích");
            dgvTestCases.Columns.Add("IsHidden", "Ẩn?");

            dgvTestCases.Columns[0].Visible = false;
            dgvTestCases.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvTestCases.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvTestCases.Columns[3].Width = 150;
            dgvTestCases.Columns[4].Width = 80;
        }

        private void RefreshTestCaseGrid()
        {
            try
            {
                dgvTestCases.Rows.Clear();

                if (dgvTestCases.Columns.Count == 0)
                {
                    SetupDataGridView();
                }

                foreach (var tc in _testCases)
                {
                    dgvTestCases.Rows.Add(
                        tc.TestCaseID,
                        tc.Input?.Length > 50 ? tc.Input.Substring(0, 50) + "..." : tc.Input,
                        tc.ExpectedOutput?.Length > 50 ? tc.ExpectedOutput.Substring(0, 50) + "..." : tc.ExpectedOutput,
                        tc.Explain?.Length > 30 ? tc.Explain.Substring(0, 30) + "..." : tc.Explain,
                        tc.IsHidden ? "Có" : "Không"
                    );
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi refresh grid: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DgvTestCases_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                if (dgvTestCases.SelectedRows.Count > 0)
                {
                    var selectedRow = dgvTestCases.SelectedRows[0];

                    if (selectedRow.Cells[0].Value == null)
                        return;

                    var testCaseId = (Guid)selectedRow.Cells[0].Value;
                    _selectedTestCase = _testCases.Find(tc => tc.TestCaseID == testCaseId);

                    if (_selectedTestCase != null)
                    {
                        txtTestCaseInput.Text = _selectedTestCase.Input;
                        txtTestCaseOutput.Text = _selectedTestCase.ExpectedOutput;
                        txtTestCaseExplain.Text = _selectedTestCase.Explain ?? "";
                        chkIsHidden.Checked = _selectedTestCase.IsHidden;

                        btnAddTestCase.Text = "✓ Cập nhật";
                        btnAddTestCase.BackColor = System.Drawing.Color.FromArgb(59, 130, 246);
                    }
                }
                else
                {
                    btnAddTestCase.Text = "+ Thêm mới";
                    btnAddTestCase.BackColor = System.Drawing.Color.FromArgb(34, 197, 94);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi chọn test case: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnAddTestCase_Click(object sender, EventArgs e)
        {
            if (!ValidateTestCase())
                return;

            if (_selectedTestCase != null)
            {
                // Cập nhật test case
                string jsonInput = InputParser.ParseInputToJson(txtTestCaseInput.Text.Trim());

                _selectedTestCase.Input = jsonInput;
                _selectedTestCase.ExpectedOutput = txtTestCaseOutput.Text.Trim();
                _selectedTestCase.Explain = txtTestCaseExplain.Text.Trim();
                _selectedTestCase.IsHidden = chkIsHidden.Checked;

                MessageBox.Show("Cập nhật test case thành công!\n(Nhớ nhấn 'Lưu bài tập' để lưu vào database)",
                    "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                // Thêm test case mới
                string jsonInput = InputParser.ParseInputToJson(txtTestCaseInput.Text.Trim());

                var newTestCase = new TestCase
                {
                    TestCaseID = Guid.NewGuid(),
                    ProblemID = _problemId ?? Guid.Empty,
                    Input = jsonInput,
                    ExpectedOutput = txtTestCaseOutput.Text.Trim(),
                    Explain = txtTestCaseExplain.Text.Trim(),
                    IsHidden = chkIsHidden.Checked,
                    IsDeleted = false
                };

                _testCases.Add(newTestCase);
                MessageBox.Show("Thêm test case thành công!\n(Nhớ nhấn 'Lưu bài tập' để lưu vào database)",
                    "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            ClearTestCaseInput();
            RefreshTestCaseGrid();
        }

        private void BtnDeleteTestCase_Click(object sender, EventArgs e)
        {
            if (_selectedTestCase == null)
            {
                MessageBox.Show("Vui lòng chọn test case để xóa.", "Cảnh báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show("Bạn có chắc muốn xóa test case này?", "Xác nhận",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                _testCases.Remove(_selectedTestCase);
                _selectedTestCase = null;
                ClearTestCaseInput();
                RefreshTestCaseGrid();
                MessageBox.Show("Xóa test case thành công!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void ClearTestCaseInput()
        {
            txtTestCaseInput.Clear();
            txtTestCaseOutput.Clear();
            txtTestCaseExplain.Clear();
            chkIsHidden.Checked = false;
            _selectedTestCase = null;
            btnAddTestCase.Text = "+ Thêm mới";
            btnAddTestCase.BackColor = System.Drawing.Color.FromArgb(34, 197, 94);
            dgvTestCases.ClearSelection();
        }

        private bool ValidateTestCase()
        {
            if (string.IsNullOrWhiteSpace(txtTestCaseInput.Text))
            {
                MessageBox.Show("Vui lòng nhập Input cho test case.", "Cảnh báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTestCaseInput.Focus();
                return false;
            }

            string validationError = ValidateInputFormat(txtTestCaseInput.Text.Trim());
            if (!string.IsNullOrEmpty(validationError))
            {
                MessageBox.Show($"Định dạng Input không hợp lệ.\n\n{validationError}",
                    "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTestCaseInput.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtTestCaseOutput.Text))
            {
                MessageBox.Show("Vui lòng nhập Expected Output cho test case.", "Cảnh báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTestCaseOutput.Focus();
                return false;
            }

            return true;
        }

        private string ValidateInputFormat(string input)
        {
            if (string.IsNullOrEmpty(input))
                return "Input không được để trống.";

            try
            {
                string jsonResult = InputParser.ParseInputToJson(input);

                if (jsonResult.Equals("{}"))
                    return "Input phải chứa ít nhất một cặp biến=giá_trị.\nVí dụ: a=5,b=10";

                return null;
            }
            catch (FormatException ex)
            {
                return $"Lỗi: {ex.Message}\nĐịnh dạng đúng: biến=giá_trị,biến2=giá_trị,...\nVí dụ: a=5,b=10,name=\"John\",arr=[1,2,3]";
            }
            catch (Exception ex)
            {
                return $"Lỗi: {ex.Message}";
            }
        }

        private bool IsValidInputFormat(string input)
        {
            return string.IsNullOrEmpty(ValidateInputFormat(input));
        }

        private void LoadData()
        {
            try
            {
                var p = _problemService.GetById(_problemId.Value);
                if (p != null)
                {
                    txtTitle.Text = p.Title;
                    cboDifficulty.SelectedItem = p.Difficulty;
                    txtDescription.Text = p.Description;

                    txtFunctionName.Text = p.FunctionName;
                    txtParameters.Text = p.Parameters;
                    cboReturnType.Text = p.ReturnType;
                    numTimeLimit.Value = p.TimeLimit > 0 ? p.TimeLimit : 1000;
                    numMemoryLimit.Value = p.MemoryLimit > 0 ? p.MemoryLimit : 256;
                    txtTags.Text = p.Tags;
                    txtConstraints.Text = p.Constraints;
                    txtNotes.Text = p.Notes;

                    _testCases.Clear();

                    List<TestCase> loadedTestCases = _testCaseService.GetByProblemId(p.ProblemID);

                    if (loadedTestCases != null && loadedTestCases.Count > 0)
                    {
                        _testCases.AddRange(loadedTestCases);
                    }

                    RefreshTestCaseGrid();
                }
                else
                {
                    MessageBox.Show("Không tìm thấy bài tập!", "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi load dữ liệu: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (!ValidateInput())
            {
                return;
            }

            bool success = false;

            // Tạo mới
            if (_problemId == null)
            {
                var p = new CodingProblem
                {
                    ProblemID = Guid.NewGuid(),
                    Title = txtTitle.Text.Trim(),
                    Slug = GenerateSlug(txtTitle.Text.Trim()),
                    Difficulty = cboDifficulty.Text,
                    Status = "NOT_STARTED",
                    Tags = txtTags.Text.Trim(),
                    Description = txtDescription.Text.Trim(),
                    FunctionName = txtFunctionName.Text.Trim(),
                    Parameters = txtParameters.Text.Trim(),
                    ReturnType = cboReturnType.Text,
                    Constraints = txtConstraints.Text.Trim(),
                    Notes = txtNotes.Text.Trim(),
                    TimeLimit = (int)numTimeLimit.Value,
                    MemoryLimit = (int)numMemoryLimit.Value,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                    IsDeleted = false
                };

                success = _problemService.Create(p);

                if (success)
                {
                    _problemId = p.ProblemID;

                    foreach (var tc in _testCases)
                    {
                        tc.ProblemID = _problemId.Value;
                        bool tcSuccess = _testCaseService.Create(tc);
                        if (!tcSuccess)
                        {
                            MessageBox.Show($"Lỗi khi lưu test case!", "Lỗi",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                            success = false;
                            break;
                        }
                    }
                }
            }
            // Cập nhật
            else
            {
                var p = _problemService.GetById(_problemId.Value);
                if (p != null)
                {
                    p.Title = txtTitle.Text.Trim();
                    p.Slug = GenerateSlug(txtTitle.Text.Trim());
                    p.Difficulty = cboDifficulty.Text;
                    p.Tags = txtTags.Text.Trim();
                    p.Description = txtDescription.Text.Trim();
                    p.FunctionName = txtFunctionName.Text.Trim();
                    p.Parameters = txtParameters.Text.Trim();
                    p.ReturnType = cboReturnType.Text;
                    p.Constraints = txtConstraints.Text.Trim();
                    p.Notes = txtNotes.Text.Trim();
                    p.TimeLimit = (int)numTimeLimit.Value;
                    p.MemoryLimit = (int)numMemoryLimit.Value;
                    p.UpdatedAt = DateTime.Now;

                    success = _problemService.Update(p);

                    if (success)
                    {
                        HashSet<Guid> currentIds = new HashSet<Guid>(_testCases.Where(tc => tc.TestCaseID != Guid.Empty).Select(tc => tc.TestCaseID));

                        foreach (var tc in _testCases)
                        {
                            tc.ProblemID = _problemId.Value;

                            bool tcSuccess;

                            if (tc.TestCaseID == Guid.Empty)
                            {
                                tc.TestCaseID = Guid.NewGuid();
                                tcSuccess = _testCaseService.Create(tc);
                            }
                            else
                            {
                                tcSuccess = _testCaseService.Update(tc);
                            }

                            if (!tcSuccess)
                            {
                                MessageBox.Show($"Lỗi khi lưu test case!", "Lỗi",
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                                success = false;
                                break;
                            }
                        }
                    }
                }
            }

            if (success)
            {
                MessageBox.Show("Lưu thành công!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show("Lưu thất bại.", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool ValidateInput()
        {
            if (string.IsNullOrWhiteSpace(txtTitle.Text))
            {
                MessageBox.Show("Vui lòng nhập tên bài tập.", "Cảnh báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tabProblemInfo.SelectedIndex = 0;
                txtTitle.Focus();
                return false;
            }

            if (txtTitle.Text.Trim().Length < 8)
            {
                MessageBox.Show("Tên bài tập phải có ít nhất 8 ký tự.", "Cảnh báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tabProblemInfo.SelectedIndex = 0;
                txtTitle.Focus();
                return false;
            }

            if (txtTitle.Text.Trim().Length > 200)
            {
                MessageBox.Show("Tên bài tập không được vượt quá 200 ký tự.", "Cảnh báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tabProblemInfo.SelectedIndex = 0;
                txtTitle.Focus();
                return false;
            }

            if (cboDifficulty.SelectedIndex == -1 || string.IsNullOrWhiteSpace(cboDifficulty.Text))
            {
                MessageBox.Show("Vui lòng chọn độ khó.", "Cảnh báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tabProblemInfo.SelectedIndex = 0;
                cboDifficulty.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtDescription.Text))
            {
                MessageBox.Show("Vui lòng nhập mô tả bài tập.", "Cảnh báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tabProblemInfo.SelectedIndex = 0;
                txtDescription.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtFunctionName.Text))
            {
                MessageBox.Show("Vui lòng nhập tên hàm.", "Cảnh báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tabProblemInfo.SelectedIndex = 1;
                txtFunctionName.Focus();
                return false;
            }

            if (!IsValidFunctionName(txtFunctionName.Text.Trim()))
            {
                MessageBox.Show("Tên hàm không hợp lệ. Tên hàm phải bắt đầu bằng chữ cái hoặc gạch dưới, chỉ chứa chữ cái, số, gạch dưới.",
                    "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tabProblemInfo.SelectedIndex = 1;
                txtFunctionName.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtParameters.Text))
            {
                MessageBox.Show("Vui lòng nhập tham số.", "Cảnh báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tabProblemInfo.SelectedIndex = 1;
                txtParameters.Focus();
                return false;
            }

            if (!IsValidParameters(txtParameters.Text.Trim()))
            {
                MessageBox.Show("Định dạng tham số không hợp lệ. Ví dụ: int a, string b, int[] arr",
                    "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tabProblemInfo.SelectedIndex = 1;
                txtParameters.Focus();
                return false;
            }

            if (cboReturnType.SelectedIndex == -1 || string.IsNullOrWhiteSpace(cboReturnType.Text))
            {
                MessageBox.Show("Vui lòng chọn kiểu trả về.", "Cảnh báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tabProblemInfo.SelectedIndex = 1;
                cboReturnType.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtTags.Text))
            {
                MessageBox.Show("Vui lòng nhập Tags. Ví dụ: Array, String, Loop", "Cảnh báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tabProblemInfo.SelectedIndex = 1;
                txtTags.Focus();
                return false;
            }

            if (!IsValidTags(txtTags.Text.Trim()))
            {
                MessageBox.Show("Định dạng Tags không hợp lệ. Tags phải phân cách bằng dấu phẩy.",
                    "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tabProblemInfo.SelectedIndex = 1;
                txtTags.Focus();
                return false;
            }

            if (_testCases.Count == 0)
            {
                MessageBox.Show("Vui lòng thêm ít nhất 1 test case.", "Cảnh báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tabProblemInfo.SelectedIndex = 2;
                return false;
            }

            return true;
        }

        private bool IsValidFunctionName(string functionName)
        {
            if (string.IsNullOrEmpty(functionName))
                return false;
            return Regex.IsMatch(functionName, @"^[a-zA-Z_][a-zA-Z0-9_]*$");
        }

        private bool IsValidParameters(string parameters)
        {
            if (string.IsNullOrEmpty(parameters))
                return false;

            string[] paramList = parameters.Split(',');
            foreach (string param in paramList)
            {
                string trimmedParam = param.Trim();
                if (string.IsNullOrEmpty(trimmedParam))
                    return false;

                if (!Regex.IsMatch(trimmedParam, @"^[a-zA-Z_][a-zA-Z0-9_\[\]]*\s+[a-zA-Z_][a-zA-Z0-9_]*$"))
                    return false;
            }

            return true;
        }

        private bool IsValidTags(string tags)
        {
            if (string.IsNullOrEmpty(tags))
                return false;

            string[] tagList = tags.Split(',');

            foreach (string tag in tagList)
            {
                string trimmedTag = tag.Trim();
                if (string.IsNullOrEmpty(trimmedTag))
                    return false;
            }

            return true;
        }

        private string GenerateSlug(string title)
        {
            if (string.IsNullOrEmpty(title))
                return "";

            string slug = title.ToLower().Trim();
            slug = Regex.Replace(slug, @"\s+", "-");
            slug = Regex.Replace(slug, @"[^a-z0-9\-_]", "");
            slug = Regex.Replace(slug, @"-+", "-");
            return slug.TrimEnd('-');
        }
    }
}