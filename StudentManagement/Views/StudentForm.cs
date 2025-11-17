using StudentManagement.DAL;
using StudentManagement.Models;

namespace StudentManagement.Views
{
    /// <summary>
    /// Form quản lý sinh viên - PROMPT 7
    /// Chức năng: CRUD sinh viên với đầy đủ các trường (Gender, Phone, Address, IsActive)
    /// </summary>
    public partial class StudentForm : Form
    {
        private StudentRepository studentRepo;
        private ClassRepository classRepo;
        private int selectedStudentID = 0;

        public StudentForm()
        {
            InitializeComponent();
            studentRepo = new StudentRepository();
            classRepo = new ClassRepository();
        }

        #region Form Events

        /// <summary>
        /// Form Load - Khởi tạo dữ liệu ban đầu
        /// </summary>
        private void StudentForm_Load(object sender, EventArgs e)
        {
            try
            {
                LoadClasses();
                LoadAllStudents();
                SetupDataGridView();
                ClearInputs();
                UpdateStatus("Sẵn sàng");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải dữ liệu: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region Data Loading

        /// <summary>
        /// Load danh sách lớp học vào ComboBox
        /// </summary>
        private void LoadClasses()
        {
            try
            {
                var classes = classRepo.GetAllClasses();

                cmbClass.DataSource = classes;
                cmbClass.DisplayMember = "ClassName";
                cmbClass.ValueMember = "ClassID";
                cmbClass.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải danh sách lớp: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Load tất cả sinh viên vào DataGridView
        /// </summary>
        private void LoadAllStudents()
        {
            try
            {
                var students = studentRepo.GetAllStudents();
                dgvStudent.DataSource = students;

                UpdateRecordCount(students.Count);
                UpdateStatus($"Đã tải {students.Count} sinh viên");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải danh sách sinh viên: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Thiết lập cột cho DataGridView
        /// </summary>
        private void SetupDataGridView()
        {
            if (dgvStudent.Columns.Count == 0) return;

            // Ẩn StudentID
            if (dgvStudent.Columns["StudentID"] != null)
                dgvStudent.Columns["StudentID"]!.Visible = false;

            // Ẩn ClassID
            if (dgvStudent.Columns["ClassID"] != null)
                dgvStudent.Columns["ClassID"]!.Visible = false;

            // Đặt tên tiêu đề cột
            if (dgvStudent.Columns["StudentCode"] != null)
            {
                dgvStudent.Columns["StudentCode"]!.HeaderText = "Mã SV";
                dgvStudent.Columns["StudentCode"]!.Width = 100;
            }

            if (dgvStudent.Columns["StudentName"] != null)
            {
                dgvStudent.Columns["StudentName"]!.HeaderText = "Tên sinh viên";
                dgvStudent.Columns["StudentName"]!.Width = 200;
            }

            if (dgvStudent.Columns["ClassName"] != null)
            {
                dgvStudent.Columns["ClassName"]!.HeaderText = "Lớp";
                dgvStudent.Columns["ClassName"]!.Width = 120;
            }

            if (dgvStudent.Columns["DateOfBirth"] != null)
            {
                dgvStudent.Columns["DateOfBirth"]!.HeaderText = "Ngày sinh";
                dgvStudent.Columns["DateOfBirth"]!.Width = 100;
                dgvStudent.Columns["DateOfBirth"]!.DefaultCellStyle.Format = "dd/MM/yyyy";
            }

            if (dgvStudent.Columns["Gender"] != null)
            {
                dgvStudent.Columns["Gender"]!.HeaderText = "Giới tính";
                dgvStudent.Columns["Gender"]!.Width = 80;
            }

            if (dgvStudent.Columns["Email"] != null)
            {
                dgvStudent.Columns["Email"]!.HeaderText = "Email";
                dgvStudent.Columns["Email"]!.Width = 180;
            }

            if (dgvStudent.Columns["Phone"] != null)
            {
                dgvStudent.Columns["Phone"]!.HeaderText = "SĐT";
                dgvStudent.Columns["Phone"]!.Width = 100;
            }

            if (dgvStudent.Columns["Address"] != null)
            {
                dgvStudent.Columns["Address"]!.HeaderText = "Địa chỉ";
                dgvStudent.Columns["Address"]!.Width = 200;
            }

            if (dgvStudent.Columns["EnrollmentDate"] != null)
            {
                dgvStudent.Columns["EnrollmentDate"]!.HeaderText = "Ngày nhập học";
                dgvStudent.Columns["EnrollmentDate"]!.Width = 100;
                dgvStudent.Columns["EnrollmentDate"]!.DefaultCellStyle.Format = "dd/MM/yyyy";
            }

            if (dgvStudent.Columns["IsActive"] != null)
            {
                dgvStudent.Columns["IsActive"]!.HeaderText = "Đang học";
                dgvStudent.Columns["IsActive"]!.Width = 80;
            }
        }

        #endregion

        #region CRUD Operations

        /// <summary>
        /// Thêm sinh viên mới
        /// </summary>
        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                // Validate dữ liệu
                if (!ValidateInputs()) return;

                // Tạo đối tượng Student
                var student = new Student
                {
                    StudentCode = txtStudentCode.Text.Trim(),
                    StudentName = txtStudentName.Text.Trim(),
                    ClassID = cmbClass.SelectedValue != null ? (int)cmbClass.SelectedValue : 0,
                    DateOfBirth = dtpDateOfBirth.Value,
                    Gender = cmbGender.SelectedItem?.ToString(),
                    Email = txtEmail.Text.Trim(),
                    Phone = txtPhone.Text.Trim(),
                    Address = txtAddress.Text.Trim(),
                    EnrollmentDate = DateTime.Now,
                    IsActive = chkIsActive.Checked
                };

                // Gọi Repository để thêm
                bool result = studentRepo.AddStudent(student);

                if (result)
                {
                    MessageBox.Show("Thêm sinh viên thành công!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);

                    LoadAllStudents();
                    ClearInputs();
                    UpdateStatus("Đã thêm sinh viên mới");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi thêm sinh viên:\n{ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Cập nhật sinh viên
        /// </summary>
        private void btnEdit_Click(object sender, EventArgs e)
        {
            try
            {
                // Kiểm tra đã chọn sinh viên chưa
                if (selectedStudentID == 0)
                {
                    MessageBox.Show("Vui lòng chọn sinh viên cần sửa!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Validate dữ liệu
                if (!ValidateInputs()) return;

                // Tạo đối tượng Student
                var student = new Student
                {
                    StudentID = selectedStudentID,
                    StudentCode = txtStudentCode.Text.Trim(),
                    StudentName = txtStudentName.Text.Trim(),
                    ClassID = cmbClass.SelectedValue != null ? (int)cmbClass.SelectedValue : 0,
                    DateOfBirth = dtpDateOfBirth.Value,
                    Gender = cmbGender.SelectedItem?.ToString(),
                    Email = txtEmail.Text.Trim(),
                    Phone = txtPhone.Text.Trim(),
                    Address = txtAddress.Text.Trim(),
                    EnrollmentDate = DateTime.Now,
                    IsActive = chkIsActive.Checked
                };

                // Gọi Repository để cập nhật
                bool result = studentRepo.UpdateStudent(student);

                if (result)
                {
                    MessageBox.Show("Cập nhật sinh viên thành công!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);

                    LoadAllStudents();
                    ClearInputs();
                    UpdateStatus("Đã cập nhật sinh viên");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi cập nhật sinh viên:\n{ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Xóa sinh viên
        /// </summary>
        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                // Kiểm tra đã chọn sinh viên chưa
                if (selectedStudentID == 0)
                {
                    MessageBox.Show("Vui lòng chọn sinh viên cần xóa!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Xác nhận xóa
                var confirmResult = MessageBox.Show(
                    $"Bạn có chắc chắn muốn xóa sinh viên '{txtStudentName.Text}'?\n\n" +
                    "Lưu ý: Không thể xóa nếu sinh viên đã có điểm thi!",
                    "Xác nhận xóa",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (confirmResult == DialogResult.Yes)
                {
                    // Gọi Repository để xóa
                    bool result = studentRepo.DeleteStudent(selectedStudentID);

                    if (result)
                    {
                        MessageBox.Show("Xóa sinh viên thành công!", "Thông báo",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);

                        LoadAllStudents();
                        ClearInputs();
                        UpdateStatus("Đã xóa sinh viên");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi xóa sinh viên:\n{ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region Search Operations

        /// <summary>
        /// Tìm kiếm sinh viên
        /// </summary>
        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                string keyword = txtSearch.Text.Trim();

                if (string.IsNullOrWhiteSpace(keyword))
                {
                    MessageBox.Show("Vui lòng nhập từ khóa tìm kiếm!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtSearch.Focus();
                    return;
                }

                var students = studentRepo.SearchStudent(keyword);
                dgvStudent.DataSource = students;

                UpdateRecordCount(students.Count);
                UpdateStatus($"Tìm thấy {students.Count} kết quả");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tìm kiếm:\n{ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Làm mới danh sách
        /// </summary>
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            try
            {
                txtSearch.Clear();
                LoadAllStudents();
                ClearInputs();
                UpdateStatus("Đã làm mới danh sách");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi làm mới:\n{ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Xử lý Enter key trong TextBox tìm kiếm
        /// </summary>
        private void txtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnSearch_Click(sender, e);
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }

        #endregion

        #region DataGridView Events

        /// <summary>
        /// Khi chọn dòng trong DataGridView
        /// </summary>
        private void dgvStudent_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                if (dgvStudent.CurrentRow != null && dgvStudent.CurrentRow.Index >= 0)
                {
                    var row = dgvStudent.CurrentRow;

                    // Lấy StudentID từ dòng được chọn
                    selectedStudentID = Convert.ToInt32(row.Cells["StudentID"].Value);

                    // Lấy thông tin chi tiết từ Repository
                    var student = studentRepo.GetStudentById(selectedStudentID);

                    if (student != null)
                    {
                        // Hiển thị thông tin lên các control
                        txtStudentCode.Text = student.StudentCode;
                        txtStudentName.Text = student.StudentName;
                        cmbClass.SelectedValue = student.ClassID;

                        if (student.DateOfBirth.HasValue)
                            dtpDateOfBirth.Value = student.DateOfBirth.Value;
                        else
                            dtpDateOfBirth.Value = DateTime.Now.AddYears(-20);

                        // Chọn giới tính
                        if (!string.IsNullOrWhiteSpace(student.Gender))
                        {
                            int genderIndex = cmbGender.FindStringExact(student.Gender);
                            cmbGender.SelectedIndex = genderIndex >= 0 ? genderIndex : -1;
                        }
                        else
                        {
                            cmbGender.SelectedIndex = -1;
                        }

                        txtEmail.Text = student.Email ?? "";
                        txtPhone.Text = student.Phone ?? "";
                        txtAddress.Text = student.Address ?? "";
                        chkIsActive.Checked = student.IsActive;

                        UpdateStatus($"Đã chọn: {student.StudentName}");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi hiển thị thông tin:\n{ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region Helper Methods

        /// <summary>
        /// Xóa các input trên form
        /// </summary>
        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearInputs();
            UpdateStatus("Đã xóa thông tin nhập");
        }

        private void ClearInputs()
        {
            selectedStudentID = 0;
            txtStudentCode.Clear();
            txtStudentName.Clear();
            cmbClass.SelectedIndex = -1;
            dtpDateOfBirth.Value = DateTime.Now.AddYears(-20);
            cmbGender.SelectedIndex = -1;
            txtEmail.Clear();
            txtPhone.Clear();
            txtAddress.Clear();
            chkIsActive.Checked = true;

            txtStudentCode.Focus();
        }

        /// <summary>
        /// Validate dữ liệu nhập
        /// </summary>
        private bool ValidateInputs()
        {
            // Kiểm tra mã sinh viên
            if (string.IsNullOrWhiteSpace(txtStudentCode.Text))
            {
                MessageBox.Show("Vui lòng nhập mã sinh viên!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtStudentCode.Focus();
                return false;
            }

            // Kiểm tra tên sinh viên
            if (string.IsNullOrWhiteSpace(txtStudentName.Text))
            {
                MessageBox.Show("Vui lòng nhập tên sinh viên!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtStudentName.Focus();
                return false;
            }

            // Kiểm tra lớp học
            if (cmbClass.SelectedIndex == -1)
            {
                MessageBox.Show("Vui lòng chọn lớp học!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbClass.Focus();
                return false;
            }

            // Validate Email (nếu có)
            if (!string.IsNullOrWhiteSpace(txtEmail.Text))
            {
                if (!txtEmail.Text.Contains("@"))
                {
                    MessageBox.Show("Email không hợp lệ!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtEmail.Focus();
                    return false;
                }
            }

            // Validate Phone (nếu có)
            if (!string.IsNullOrWhiteSpace(txtPhone.Text))
            {
                if (txtPhone.Text.Length < 10 || txtPhone.Text.Length > 11)
                {
                    MessageBox.Show("Số điện thoại phải có 10-11 chữ số!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtPhone.Focus();
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Cập nhật trạng thái trên StatusStrip
        /// </summary>
        private void UpdateStatus(string message)
        {
            lblStatus.Text = message;
        }

        /// <summary>
        /// Cập nhật số lượng bản ghi
        /// </summary>
        private void UpdateRecordCount(int count)
        {
            lblRecordCount.Text = $"Tổng: {count} sinh viên";
        }

        #endregion
    }
}

