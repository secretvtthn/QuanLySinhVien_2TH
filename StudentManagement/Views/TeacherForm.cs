using System;
using System.Linq;
using System.Windows.Forms;
using StudentManagement.DAL;
using StudentManagement.Models;

namespace StudentManagement.Views
{
    /// <summary>
    /// Form quản lý giáo viên - CRUD với LINQ
    /// ✅ PROMPT 9: TeacherForm với kiểm tra lớp phụ trách trước khi xóa
    /// </summary>
    public partial class TeacherForm : Form
    {
        private readonly TeacherRepository teacherRepository;
        private int selectedTeacherId = 0;

        public TeacherForm()
        {
            InitializeComponent();
            teacherRepository = new TeacherRepository();
            this.Load += TeacherForm_Load;
        }

        #region Form Events

        private void TeacherForm_Load(object? sender, EventArgs e)
        {
            try
            {
                LoadTeachers();
                ClearInputs();
                lblStatus.Text = "Sẵn sàng";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải dữ liệu: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region Load Data Methods (LINQ)

        /// <summary>
        /// ✅ LINQ: Load Teachers vào DataGridView
        /// </summary>
        private void LoadTeachers()
        {
            try
            {
                var teachers = teacherRepository.GetAllTeachers();

                // ✅ LINQ: Select với ClassCount
                var displayData = teachers
                    .OrderBy(t => t.TeacherName)
                    .Select(t => new
                    {
                        t.TeacherID,
                        TênGiáoViên = t.TeacherName,
                        Email = t.Email ?? "",
                        BộMôn = t.Department ?? "",
                        SốĐT = t.Phone ?? "",
                        ĐịaChỉ = t.Address ?? "",
                        NgàyVàoLàm = t.HireDate.ToString("dd/MM/yyyy"),
                        TrạngThái = t.IsActive ? "Hoạt động" : "Ngừng",
                        SốLớpPhụTrách = teacherRepository.GetClassCountByTeacher(t.TeacherID)
                    })
                    .ToList();

                dgvTeacher.DataSource = displayData;

                // Ẩn cột TeacherID
                var colTeacherId = dgvTeacher.Columns["TeacherID"];
                if (colTeacherId != null)
                {
                    colTeacherId.Visible = false;
                }

                // Cập nhật số lượng
                lblRecordCount.Text = $"Tổng: {displayData.Count} giáo viên";
                lblStatus.Text = $"Đã tải {displayData.Count} giáo viên";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải danh sách giáo viên: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region Button Events

        /// <summary>
        /// Nút THÊM - Thêm giáo viên mới
        /// </summary>
        private void btnAdd_Click(object? sender, EventArgs e)
        {
            try
            {
                if (!ValidateInputs())
                    return;

                var teacher = new Teacher
                {
                    TeacherName = txtTeacherName.Text.Trim(),
                    Email = string.IsNullOrWhiteSpace(txtEmail.Text) ? null : txtEmail.Text.Trim(),
                    Department = string.IsNullOrWhiteSpace(txtDepartment.Text) ? null : txtDepartment.Text.Trim(),
                    Phone = string.IsNullOrWhiteSpace(txtPhone.Text) ? null : txtPhone.Text.Trim(),
                    Address = string.IsNullOrWhiteSpace(txtAddress.Text) ? null : txtAddress.Text.Trim(),
                    HireDate = dtpHireDate.Value,
                    IsActive = chkIsActive.Checked
                };

                bool result = teacherRepository.AddTeacher(teacher);

                if (result)
                {
                    MessageBox.Show("Thêm giáo viên thành công!", "Thành công",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);

                    LoadTeachers();
                    ClearInputs();
                    lblStatus.Text = "Đã thêm giáo viên thành công";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi thêm giáo viên: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                lblStatus.Text = "Lỗi khi thêm giáo viên";
            }
        }

        /// <summary>
        /// Nút SỬA - Cập nhật thông tin giáo viên
        /// </summary>
        private void btnEdit_Click(object? sender, EventArgs e)
        {
            try
            {
                if (selectedTeacherId == 0)
                {
                    MessageBox.Show("Vui lòng chọn giáo viên cần sửa!", "Cảnh báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (!ValidateInputs())
                    return;

                var teacher = teacherRepository.GetTeacherById(selectedTeacherId);
                if (teacher == null)
                {
                    MessageBox.Show("Không tìm thấy giáo viên!", "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                teacher.TeacherName = txtTeacherName.Text.Trim();
                teacher.Email = string.IsNullOrWhiteSpace(txtEmail.Text) ? null : txtEmail.Text.Trim();
                teacher.Department = string.IsNullOrWhiteSpace(txtDepartment.Text) ? null : txtDepartment.Text.Trim();
                teacher.Phone = string.IsNullOrWhiteSpace(txtPhone.Text) ? null : txtPhone.Text.Trim();
                teacher.Address = string.IsNullOrWhiteSpace(txtAddress.Text) ? null : txtAddress.Text.Trim();
                teacher.HireDate = dtpHireDate.Value;
                teacher.IsActive = chkIsActive.Checked;

                bool result = teacherRepository.UpdateTeacher(teacher);

                if (result)
                {
                    MessageBox.Show("Cập nhật giáo viên thành công!", "Thành công",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);

                    LoadTeachers();
                    ClearInputs();
                    lblStatus.Text = "Đã cập nhật giáo viên thành công";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi cập nhật giáo viên: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                lblStatus.Text = "Lỗi khi cập nhật giáo viên";
            }
        }

        /// <summary>
        /// Nút XÓA - Xóa giáo viên (kiểm tra lớp phụ trách)
        /// ✅ LINQ: Kiểm tra giáo viên có đang phụ trách lớp không trước khi xóa
        /// </summary>
        private void btnDelete_Click(object? sender, EventArgs e)
        {
            try
            {
                if (selectedTeacherId == 0)
                {
                    MessageBox.Show("Vui lòng chọn giáo viên cần xóa!", "Cảnh báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Kiểm tra số lớp đang phụ trách
                int classCount = teacherRepository.GetClassCountByTeacher(selectedTeacherId);

                if (classCount > 0)
                {
                    MessageBox.Show(
                        $"Không thể xóa giáo viên '{txtTeacherName.Text}' vì đang phụ trách {classCount} lớp!\n\n" +
                        "Vui lòng chuyển lớp cho giáo viên khác trước khi xóa.",
                        "Cảnh báo",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    return;
                }

                var confirmResult = MessageBox.Show(
                    $"Bạn có chắc chắn muốn xóa giáo viên '{txtTeacherName.Text}'?",
                    "Xác nhận xóa",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (confirmResult == DialogResult.Yes)
                {
                    bool result = teacherRepository.DeleteTeacher(selectedTeacherId);

                    if (result)
                    {
                        MessageBox.Show("Xóa giáo viên thành công!", "Thành công",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);

                        LoadTeachers();
                        ClearInputs();
                        lblStatus.Text = "Đã xóa giáo viên thành công";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi xóa giáo viên: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                lblStatus.Text = "Lỗi khi xóa giáo viên";
            }
        }

        /// <summary>
        /// Nút TÌM KIẾM
        /// ✅ LINQ: Dùng SearchTeacher() với LINQ Where
        /// </summary>
        private void btnSearch_Click(object? sender, EventArgs e)
        {
            try
            {
                string keyword = txtSearch.Text.Trim();

                if (string.IsNullOrEmpty(keyword))
                {
                    MessageBox.Show("Vui lòng nhập từ khóa tìm kiếm!", "Cảnh báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var teachers = teacherRepository.SearchTeacher(keyword);

                var displayData = teachers
                    .Select(t => new
                    {
                        t.TeacherID,
                        TênGiáoViên = t.TeacherName,
                        Email = t.Email ?? "",
                        BộMôn = t.Department ?? "",
                        SốĐT = t.Phone ?? "",
                        ĐịaChỉ = t.Address ?? "",
                        NgàyVàoLàm = t.HireDate.ToString("dd/MM/yyyy"),
                        TrạngThái = t.IsActive ? "Hoạt động" : "Ngừng",
                        SốLớpPhụTrách = teacherRepository.GetClassCountByTeacher(t.TeacherID)
                    })
                    .ToList();

                dgvTeacher.DataSource = displayData;

                var colTeacherId = dgvTeacher.Columns["TeacherID"];
                if (colTeacherId != null)
                {
                    colTeacherId.Visible = false;
                }

                lblRecordCount.Text = $"Tìm thấy: {displayData.Count} giáo viên";
                lblStatus.Text = $"Tìm kiếm '{keyword}': {displayData.Count} kết quả";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tìm kiếm: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Nút LÀM MỚI
        /// </summary>
        private void btnRefresh_Click(object? sender, EventArgs e)
        {
            LoadTeachers();
            txtSearch.Clear();
            lblStatus.Text = "Đã làm mới danh sách";
        }

        /// <summary>
        /// Nút CLEAR
        /// </summary>
        private void btnClear_Click(object? sender, EventArgs e)
        {
            ClearInputs();
            lblStatus.Text = "Đã xóa dữ liệu nhập liệu";
        }

        #endregion

        #region DataGridView Events

        /// <summary>
        /// Sự kiện chọn dòng trong DataGridView
        /// </summary>
        private void dgvTeacher_SelectionChanged(object? sender, EventArgs e)
        {
            try
            {
                if (dgvTeacher.CurrentRow != null && dgvTeacher.CurrentRow.Index >= 0)
                {
                    var row = dgvTeacher.CurrentRow;

                    if (row.Cells["TeacherID"].Value != null)
                    {
                        selectedTeacherId = Convert.ToInt32(row.Cells["TeacherID"].Value);

                        var teacher = teacherRepository.GetTeacherById(selectedTeacherId);

                        if (teacher != null)
                        {
                            txtTeacherName.Text = teacher.TeacherName;
                            txtEmail.Text = teacher.Email ?? "";
                            txtDepartment.Text = teacher.Department ?? "";
                            txtPhone.Text = teacher.Phone ?? "";
                            txtAddress.Text = teacher.Address ?? "";
                            dtpHireDate.Value = teacher.HireDate;
                            chkIsActive.Checked = teacher.IsActive;

                            // ✅ LINQ: Hiển thị số lớp đang phụ trách
                            int classCount = teacherRepository.GetClassCountByTeacher(selectedTeacherId);
                            lblClassCountValue.Text = classCount.ToString();
                            lblClassCountValue.ForeColor = classCount > 0
                                ? System.Drawing.Color.FromArgb(142, 68, 173)
                                : System.Drawing.Color.Gray;

                            lblStatus.Text = $"Đã chọn: {teacher.TeacherName} ({classCount} lớp)";
                        }
                    }
                }
            }
            catch
            {
                // Không hiển thị lỗi khi chọn dòng
            }
        }

        #endregion

        #region TextBox Events

        /// <summary>
        /// Sự kiện nhấn Enter trong TextBox tìm kiếm
        /// </summary>
        private void txtSearch_KeyDown(object? sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnSearch_Click(sender, e);
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }

        #endregion

        #region Helper Methods

        /// <summary>
        /// Clear all input controls
        /// </summary>
        private void ClearInputs()
        {
            txtTeacherName.Clear();
            txtEmail.Clear();
            txtDepartment.Clear();
            txtPhone.Clear();
            txtAddress.Clear();
            dtpHireDate.Value = DateTime.Now;
            chkIsActive.Checked = true;
            lblClassCountValue.Text = "0";

            selectedTeacherId = 0;
            txtTeacherName.Focus();
        }

        /// <summary>
        /// Validate input data
        /// </summary>
        private bool ValidateInputs()
        {
            if (string.IsNullOrWhiteSpace(txtTeacherName.Text))
            {
                MessageBox.Show("Vui lòng nhập tên giáo viên!", "Cảnh báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTeacherName.Focus();
                return false;
            }

            // Validate Email (nếu có)
            if (!string.IsNullOrWhiteSpace(txtEmail.Text))
            {
                if (!txtEmail.Text.Contains("@"))
                {
                    MessageBox.Show("Email không hợp lệ! Vui lòng nhập đúng định dạng (vd: abc@xyz.com)", "Cảnh báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtEmail.Focus();
                    return false;
                }
            }

            // Validate Phone (nếu có)
            if (!string.IsNullOrWhiteSpace(txtPhone.Text))
            {
                string phone = txtPhone.Text.Trim();
                if (phone.Length < 10 || phone.Length > 11)
                {
                    MessageBox.Show("Số điện thoại phải có 10-11 chữ số!", "Cảnh báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtPhone.Focus();
                    return false;
                }
            }

            return true;
        }

        #endregion
    }
}
