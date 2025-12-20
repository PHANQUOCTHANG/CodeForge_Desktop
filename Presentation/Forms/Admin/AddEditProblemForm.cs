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

            // ✅ Setup DataGridView TRƯỚC khi làm gì khác
            SetupDataGridView();

            // Setup Defaults
            cboDifficulty.SelectedIndex = 0;
            cboCategory.SelectedIndex = 0;

            if (_problemId.HasValue)
            {
                lblTitle.Text = "Cập nhật Bài tập";
                LoadData();
            }
            else
            {
                lblTitle.Text = "Thêm Bài tập Mới";
            }

            // Event Handlers
            btnSave.Click += BtnSave_Click;
            btnCancel.Click += (s, e) => this.Close();
            btnAddTestCase.Click += BtnAddTestCase_Click;
            btnDeleteTestCase.Click += BtnDeleteTestCase_Click;
            dgvTestCases.SelectionChanged += DgvTestCases_SelectionChanged;  // ✅ THÊM: Đăng ký event
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
            dgvTestCases.Columns[1].Width = 100;
            dgvTestCases.Columns[2].Width = 120;
            dgvTestCases.Columns[3].Width = 100;
            dgvTestCases.Columns[4].Width = 50;
        }

        private void RefreshTestCaseGrid()
        {
            try
            {
                dgvTestCases.Rows.Clear();
                
                // ✅ Đảm bảo columns tồn tại trước khi add rows
                if (dgvTestCases.Columns.Count == 0)
                {
                    SetupDataGridView();
                }

                // ✅ Loop qua tất cả test cases và add vào grid
                foreach (var tc in _testCases)
                {
                    dgvTestCases.Rows.Add(
                        tc.TestCaseID,
                        tc.Input?.Length > 50 ? tc.Input.Substring(0, 50) + "..." : tc.Input,
                        tc.ExpectedOutput?.Length > 50 ? tc.ExpectedOutput.Substring(0, 50) + "..." : tc.ExpectedOutput,
                        tc.Explain?.Length > 50 ? tc.Explain.Substring(0, 50) + "..." : tc.Explain,
                        tc.IsHidden ? "Có" : "Không"
                    );
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi refresh grid: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DgvTestCases_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                if (dgvTestCases.SelectedRows.Count > 0)
                {
                    var selectedRow = dgvTestCases.SelectedRows[0];
                    
                    // ✅ Kiểm tra row có dữ liệu không
                    if (selectedRow.Cells[0].Value == null)
                        return;

                    var testCaseId = (Guid)selectedRow.Cells[0].Value;
                    _selectedTestCase = _testCases.Find(tc => tc.TestCaseID == testCaseId);

                    if (_selectedTestCase != null)
                    {
                        // ✅ Hiển thị dữ liệu JSON gốc (không bị cắt ngắn)
                        txtTestCaseInput.Text = _selectedTestCase.Input;
                        txtTestCaseOutput.Text = _selectedTestCase.ExpectedOutput;
                        txtTestCaseExplain.Text = _selectedTestCase.Explain ?? "";
                        chkIsHidden.Checked = _selectedTestCase.IsHidden;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi chọn test case: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnAddTestCase_Click(object sender, EventArgs e)
        {
            if (!ValidateTestCase())
                return;

            if (_selectedTestCase != null)
            {
                // ===== CẬP NHẬT TEST CASE CÓ SẴN (chỉ trong memory) =====
                string jsonInput = InputParser.ParseInputToJson(txtTestCaseInput.Text.Trim());
                
                _selectedTestCase.Input = jsonInput;
                _selectedTestCase.ExpectedOutput = txtTestCaseOutput.Text.Trim();
                _selectedTestCase.Explain = txtTestCaseExplain.Text.Trim();
                _selectedTestCase.IsHidden = chkIsHidden.Checked;
                
                MessageBox.Show("Cập nhật test case thành công! (Click Lưu để lưu vào database)", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                // ===== THÊM TEST CASE MỚI (chỉ trong memory) =====
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
                
                _testCases.Add(newTestCase);  // ✅ Thêm vào list (memory)
                MessageBox.Show("Thêm test case thành công! (Click Lưu để lưu vào database)", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            ClearTestCaseInput();
            RefreshTestCaseGrid();  // ✅ Refresh lại grid để hiển thị tất cả
        }

        private void BtnDeleteTestCase_Click(object sender, EventArgs e)
        {
            if (_selectedTestCase == null)
            {
                MessageBox.Show("Vui lòng chọn test case để xóa.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show("Bạn có chắc muốn xóa test case này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                _testCases.Remove(_selectedTestCase);
                _selectedTestCase = null;
                ClearTestCaseInput();
                RefreshTestCaseGrid();
                MessageBox.Show("Xóa test case thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void ClearTestCaseInput()
        {
            txtTestCaseInput.Clear();
            txtTestCaseOutput.Clear();
            txtTestCaseExplain.Clear();
            chkIsHidden.Checked = false;
            _selectedTestCase = null;
        }

        private bool ValidateTestCase()
        {
            if (string.IsNullOrWhiteSpace(txtTestCaseInput.Text))
            {
                MessageBox.Show("Vui lòng nhập Input cho test case.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTestCaseInput.Focus();
                return false;
            }

            // Validate Input format: variable=value,variable2=value,...
            string validationError = ValidateInputFormat(txtTestCaseInput.Text.Trim());
            if (!string.IsNullOrEmpty(validationError))
            {
                MessageBox.Show($"Định dạng Input không hợp lệ.\r\n\r\n{validationError}", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTestCaseInput.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtTestCaseOutput.Text))
            {
                MessageBox.Show("Vui lòng nhập Expected Output cho test case.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTestCaseOutput.Focus();
                return false;
            }

            return true;
        }

        /// <summary>
        /// Validate input format: variable=value,variable2=value,...
        /// Returns error message if invalid, or null/empty if valid
        /// </summary>
        private string ValidateInputFormat(string input)
        {
            if (string.IsNullOrEmpty(input))
                return "Input không được để trống.";

            try
            {
                // Parse to JSON to validate format
                string jsonResult = InputParser.ParseInputToJson(input);
                
                // Check if result is empty {}
                if (jsonResult.Equals("{}"))
                    return "Input phải chứa ít nhất một cặp biến=giá_trị.\r\nVí dụ: a=5,b=10";

                // Valid format
                return null;
            }
            catch (FormatException ex)
            {
                return $"Lỗi: {ex.Message}\r\nĐịnh dạng đúng: biến=giá_trị,biến2=giá_trị,...\r\nVí dụ: a=5,b=10,name=\"John\",arr=[1,2,3]";
            }
            catch (Exception ex)
            {
                return $"Lỗi: {ex.Message}";
            }
        }

        /// <summary>
        /// Check if format is valid (quick check without detailed error)
        /// </summary>
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
                    cboCategory.Text = p.Tags;
                    txtDescription.Text = p.Description;

                    txtFunctionName.Text = p.FunctionName;
                    txtParameters.Text = p.Parameters;
                    txtReturnType.Text = p.ReturnType;
                    numTimeLimit.Value = p.TimeLimit > 0 ? p.TimeLimit : 1000;
                    numMemoryLimit.Value = p.MemoryLimit > 0 ? p.MemoryLimit : 256;
                    txtTags.Text = p.Tags;
                    txtConstraints.Text = p.Constraints;
                    txtNotes.Text = p.Notes;

                    // ✅ Clear old test cases trước
                    _testCases.Clear();
                    
                    // ✅ Load tất cả test cases của bài này
                    List<TestCase> loadedTestCases = _testCaseService.GetByProblemId(p.ProblemID);
                    
                    if (loadedTestCases != null && loadedTestCases.Count > 0)
                    {
                        _testCases.AddRange(loadedTestCases);
                    }
                    
                    // ✅ DEBUG
                    System.Diagnostics.Debug.WriteLine($"✅ Loaded {_testCases.Count} test cases");
                    
                    // ✅ Refresh grid để hiển thị tất cả
                    RefreshTestCaseGrid();
                }
                else
                {
                    MessageBox.Show("Không tìm thấy bài tập!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi load dữ liệu: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (!ValidateInput())
            {
                return;
            }

            bool success = false;

            // ===== TẠO MỚI =====
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
                    ReturnType = txtReturnType.Text.Trim(),
                    Constraints = txtConstraints.Text.Trim(),
                    Notes = txtNotes.Text.Trim(),
                    TimeLimit = (int)numTimeLimit.Value,
                    MemoryLimit = (int)numMemoryLimit.Value,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                    IsDeleted = false
                };

                // 1️⃣ Lưu Problem trước
                success = _problemService.Create(p);

                if (success)
                {
                    _problemId = p.ProblemID;

                    // 2️⃣ Sau đó lưu Test Cases
                    foreach (var tc in _testCases)
                    {
                        tc.ProblemID = _problemId.Value;
                        bool tcSuccess = _testCaseService.Create(tc);
                        if (!tcSuccess)
                        {
                            MessageBox.Show($"Lỗi khi lưu test case!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            success = false;
                            break;
                        }
                    }
                }
            }

            // ===== CẬP NHẬT =====
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
                    p.ReturnType = txtReturnType.Text.Trim();
                    p.Constraints = txtConstraints.Text.Trim();
                    p.Notes = txtNotes.Text.Trim();
                    p.TimeLimit = (int)numTimeLimit.Value;
                    p.MemoryLimit = (int)numMemoryLimit.Value;
                    p.UpdatedAt = DateTime.Now;

                    // 1️⃣ Cập nhật Problem trước
                    success = _problemService.Update(p);

                    if (success)
                    {
                        // 2️⃣ Sau đó xử lý Test Cases
                        HashSet<Guid> currentIds = new HashSet<Guid>(_testCases.Select(tc => tc.TestCaseID).Where(id => id != Guid.Empty));

                        foreach (var tc in _testCases)
                        {
                            tc.ProblemID = _problemId.Value;

                            bool tcSuccess;
                            
                            // ✅ Kiểm tra: nếu ID rỗng → Tạo mới
                            if (tc.TestCaseID == Guid.Empty)
                            {
                                tc.TestCaseID = Guid.NewGuid();
                                tcSuccess = _testCaseService.Create(tc);
                            }
                            else
                            {
                                // ✅ Nếu ID tồn tại → Cập nhật
                                tcSuccess = _testCaseService.Update(tc);
                            }

                            if (!tcSuccess)
                            {
                                MessageBox.Show($"Lỗi khi lưu test case!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                success = false;
                                break;
                            }
                        }

                        // ✅ LOẠI BỎ: Không xóa test case cũ nữa
                        // Giữ lại tất cả test case trong DB
                    }
                }
            }

            // ===== KẾT QUẢ =====
            if (success)
            {
                MessageBox.Show("Lưu thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show("Lưu thất bại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool ValidateInput()
        {
            if (string.IsNullOrWhiteSpace(txtTitle.Text))
            {
                MessageBox.Show("Vui lòng nhập tên bài tập.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tabProblemInfo.SelectedIndex = 0;
                txtTitle.Focus();
                return false;
            }

            if (txtTitle.Text.Trim().Length < 8)
            {
                MessageBox.Show("Tên bài tập phải có ít nhất 8 ký tự.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tabProblemInfo.SelectedIndex = 0;
                txtTitle.Focus();
                return false;
            }

            if (txtTitle.Text.Trim().Length > 200)
            {
                MessageBox.Show("Tên bài tập không được vượt quá 200 ký tự.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tabProblemInfo.SelectedIndex = 0;
                txtTitle.Focus();
                return false;
            }

            if (cboDifficulty.SelectedIndex == -1 || string.IsNullOrWhiteSpace(cboDifficulty.Text))
            {
                MessageBox.Show("Vui lòng chọn độ khó.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tabProblemInfo.SelectedIndex = 0;
                cboDifficulty.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtDescription.Text))
            {
                MessageBox.Show("Vui lòng nhập mô tả bài tập.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tabProblemInfo.SelectedIndex = 0;
                txtDescription.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtFunctionName.Text))
            {
                MessageBox.Show("Vui lòng nhập tên hàm.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tabProblemInfo.SelectedIndex = 1;
                txtFunctionName.Focus();
                return false;
            }

            if (!IsValidFunctionName(txtFunctionName.Text.Trim()))
            {
                MessageBox.Show("Tên hàm không hợp lệ. Tên hàm phải bắt đầu bằng chữ cái hoặc gạch dưới, chỉ chứa chữ cái, số, gạch dưới.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tabProblemInfo.SelectedIndex = 1;
                txtFunctionName.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtParameters.Text))
            {
                MessageBox.Show("Vui lòng nhập tham số.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tabProblemInfo.SelectedIndex = 1;
                txtParameters.Focus();
                return false;
            }

            if (!IsValidParameters(txtParameters.Text.Trim()))
            {
                MessageBox.Show("Định dạng tham số không hợp lệ. Ví dụ: int a, string b, int[] arr", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tabProblemInfo.SelectedIndex = 1;
                txtParameters.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtReturnType.Text))
            {
                MessageBox.Show("Vui lòng nhập kiểu trả về.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tabProblemInfo.SelectedIndex = 1;
                txtReturnType.Focus();
                return false;
            }

            if (!IsValidReturnType(txtReturnType.Text.Trim()))
            {
                MessageBox.Show("Kiểu trả về không hợp lệ. Ví dụ: int, string, bool, int[]", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tabProblemInfo.SelectedIndex = 1;
                txtReturnType.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtTags.Text))
            {
                MessageBox.Show("Vui lòng nhập Tags. Ví dụ: Array, String, Loop", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tabProblemInfo.SelectedIndex = 1;
                txtTags.Focus();
                return false;
            }

            if (!IsValidTags(txtTags.Text.Trim()))
            {
                MessageBox.Show("Định dạng Tags không hợp lệ. Tags phải phân cách bằng dấu phẩy, mỗi tag chỉ chứa chữ cái, số, dấu gạch ngang. Ví dụ: Array, String, Loop", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tabProblemInfo.SelectedIndex = 1;
                txtTags.Focus();
                return false;
            }

            if (_testCases.Count == 0)
            {
                MessageBox.Show("Vui lòng thêm ít nhất 1 test case.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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

        private bool IsValidReturnType(string returnType)
        {
            if (string.IsNullOrEmpty(returnType))
                return false;
            return Regex.IsMatch(returnType, @"^[a-zA-Z_][a-zA-Z0-9_\[\]]*$");
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

                if (!Regex.IsMatch(trimmedTag, @"^[a-zA-Z0-9_\-]+$"))
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