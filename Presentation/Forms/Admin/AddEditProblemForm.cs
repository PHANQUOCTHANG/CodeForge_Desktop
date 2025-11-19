using System;
using System.Windows.Forms;
using CodeForge_Desktop.Business.Services;
using CodeForge_Desktop.DataAccess.Entities;

namespace CodeForge_Desktop.Presentation.Forms.Admin
{
    public partial class AddEditProblemForm : Form
    {
        private readonly CodingProblemService _service;
        private Guid? _problemId;

        public AddEditProblemForm(CodingProblemService service, Guid? id = null)
        {
            InitializeComponent();
            _service = service;
            _problemId = id;

            // Setup Defaults
            cboDifficulty.SelectedIndex = 0;
            cboStatus.SelectedIndex = 0;
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

            btnSave.Click += BtnSave_Click;
            btnCancel.Click += (s, e) => this.Close();
        }

        private void LoadData()
        {
            var p = _service.GetById(_problemId.Value);
            if (p != null)
            {
                txtTitle.Text = p.Title;
                cboDifficulty.SelectedItem = p.Difficulty;
                cboCategory.Text = p.Tags; // Category map vào Tags
                cboStatus.SelectedItem = p.Status;
                txtDescription.Text = p.Description;

                // Load các thông tin cấu hình code
                txtFunctionName.Text = p.FunctionName;
                txtParameters.Text = p.Parameters;
                txtReturnType.Text = p.ReturnType;
                numTimeLimit.Value = p.TimeLimit > 0 ? p.TimeLimit : 1000;
                numMemoryLimit.Value = p.MemoryLimit > 0 ? p.MemoryLimit : 256;
            }
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTitle.Text))
            {
                MessageBox.Show("Vui lòng nhập tên bài tập.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            bool success = false;
            if (_problemId == null) // Add
            {
                var p = new CodingProblem
                {
                    Title = txtTitle.Text.Trim(),
                    Slug = txtTitle.Text.Trim().ToLower().Replace(" ", "-"),
                    Difficulty = cboDifficulty.Text,
                    Tags = cboCategory.Text,
                    Status = cboStatus.Text,
                    Description = txtDescription.Text,
                    FunctionName = txtFunctionName.Text,
                    Parameters = txtParameters.Text,
                    ReturnType = txtReturnType.Text,
                    TimeLimit = (int)numTimeLimit.Value,
                    MemoryLimit = (int)numMemoryLimit.Value,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                };
                success = _service.Create(p);
            }
            else // Edit
            {
                var p = _service.GetById(_problemId.Value);
                if (p != null)
                {
                    p.Title = txtTitle.Text.Trim();
                    p.Difficulty = cboDifficulty.Text;
                    p.Tags = cboCategory.Text;
                    p.Status = cboStatus.Text;
                    p.Description = txtDescription.Text;
                    p.FunctionName = txtFunctionName.Text;
                    p.Parameters = txtParameters.Text;
                    p.ReturnType = txtReturnType.Text;
                    p.TimeLimit = (int)numTimeLimit.Value;
                    p.MemoryLimit = (int)numMemoryLimit.Value;
                    p.UpdatedAt = DateTime.Now;

                    success = _service.Update(p);
                }
            }

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
    }
}