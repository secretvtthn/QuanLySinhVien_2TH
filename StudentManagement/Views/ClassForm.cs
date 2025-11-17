using StudentManagement.DAL;
using StudentManagement.Models;

namespace StudentManagement.Views
{
    /// <summary>
    /// Form quản lý lớp học - CRUD với LINQ
    /// ✅ PROMPT 8: ClassForm với kiểm tra sinh viên trước khi xóa
    /// </summary>
    public partial class ClassForm : Form
    {
        private readonly ClassRepository classRepository;
        private readonly TeacherRepository teacherRepository;
        private int selectedClassId = 0;

        public ClassForm()
        {
            InitializeComponent();
            classRepository = new ClassRepository();
            teacherRepository = new TeacherRepository();
            this.Load += ClassForm_Load;
        }

        #region Form Events

        private void ClassForm_Load(object? sender, EventArgs e)
        {
            try
            {
                LoadTeachers();   // Load ComboBox Teachers
                LoadClasses();    // Load DataGridView
                ClearInputs();
                
                lblStatus.Text = "Sẵn sàng";
            }
            catch (Exception ex)
            {
                _ = MessageBox.Show($"Lỗi khi tải dữ liệu: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region Load Data Methods (LINQ)

        /// <summary>
        /// ✅ LINQ: Load Teachers vào ComboBox
        /// </summary>
        private void LoadTeachers()
        {
            try
            {
                var teachers = teacherRepository.GetAllTeachers();

                // ✅ LINQ: Select để tạo anonymous type
                var teacherList = teachers
                    .Select(t => new { t.TeacherID, t.TeacherName })
                    .OrderBy(t => t.TeacherName)
                    .ToList();

                // Thêm option "Chưa có GVCN"
                var teacherListWithEmpty = new[] { new { TeacherID = 0, TeacherName = "-- Chưa có GVCN --" } }
                    .Concat(teacherList)
                    .ToList();

                cmbTeacher.DataSource = teacherListWithEmpty;
                cmbTeacher.DisplayMember = "TeacherName";
                cmbTeacher.ValueMember = "TeacherID";
                cmbTeacher.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải danh sách giáo viên: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// ✅ LINQ: Load Classes vào DataGridView với StudentCount
        /// </summary>
        private void LoadClasses()
        {
            try
            {
                var classes = classRepository.GetAllClasses();

                // ✅ LINQ: Select với join Teacher + count Students
                var displayData = classes
                    .OrderBy(c => c.ClassName)
                    .Select(c => new
                    {
                        c.ClassID,
                        TênLớp = c.ClassName,
                        GiáoViên = GetTeacherName(c.TeacherID),
                        SốSVHiệnTại = classRepository.GetStudentCountByClass(c.ClassID),
                        SĩSố = c.Capacity,
                        PhòngHọc = c.Room ?? ""
                    })
                    .ToList();

                dgvClass.DataSource = displayData;

                // Ẩn cột ClassID
                var colClassId = dgvClass.Columns["ClassID"];
                if (colClassId != null)
                {
                    colClassId.Visible = false;
                }

                // Cập nhật số lượng
                lblRecordCount.Text = $"Tổng: {displayData.Count} lớp";
                lblStatus.Text = $"Đã tải {displayData.Count} lớp";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải danh sách lớp: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Helper: Lấy tên giáo viên từ TeacherID
        /// </summary>
        private string GetTeacherName(int? teacherId)
        {
            if (!teacherId.HasValue || teacherId.Value == 0)
                return "Chưa có GVCN";

            var teacher = teacherRepository.GetTeacherById(teacherId.Value);
            return teacher?.TeacherName ?? "N/A";
        }

        #endregion

        #region Button Events

        /// <summary>
        /// Nút THÊM - Thêm lớp mới
        /// </summary>
        private void btnAdd_Click(object? sender, EventArgs e)
        {
            try
            {
                if (!ValidateInputs())
                    return;

                int? teacherId = cmbTeacher.SelectedValue != null && (int)cmbTeacher.SelectedValue != 0 
                    ? (int)cmbTeacher.SelectedValue 
                    : null;

                var classObj = new Class
                {
                    ClassName = txtClassName.Text.Trim(),
                    TeacherID = teacherId,
                    Capacity = (int)numCapacity.Value,
                    Room = string.IsNullOrWhiteSpace(txtRoom.Text) ? null : txtRoom.Text.Trim()
                };

                bool result = classRepository.AddClass(classObj);

                if (result)
                {
                    MessageBox.Show("Thêm lớp thành công!", "Thành công",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    
                    LoadClasses();
                    ClearInputs();
                    lblStatus.Text = "Đã thêm lớp thành công";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi thêm lớp: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                lblStatus.Text = "Lỗi khi thêm lớp";
            }
        }

        /// <summary>
        /// Nút SỬA - Cập nhật thông tin lớp
        /// </summary>
        private void btnEdit_Click(object? sender, EventArgs e)
        {
            try
            {
                if (selectedClassId == 0)
                {
                    MessageBox.Show("Vui lòng chọn lớp cần sửa!", "Cảnh báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (!ValidateInputs())
                    return;

                var classObj = classRepository.GetClassById(selectedClassId);
                if (classObj == null)
                {
                    MessageBox.Show("Không tìm thấy lớp!", "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                int? teacherId = cmbTeacher.SelectedValue != null && (int)cmbTeacher.SelectedValue != 0 
                    ? (int)cmbTeacher.SelectedValue 
                    : null;

                classObj.ClassName = txtClassName.Text.Trim();
                classObj.TeacherID = teacherId;
                classObj.Capacity = (int)numCapacity.Value;
                classObj.Room = string.IsNullOrWhiteSpace(txtRoom.Text) ? null : txtRoom.Text.Trim();

                bool result = classRepository.UpdateClass(classObj);

                if (result)
                {
                    MessageBox.Show("Cập nhật lớp thành công!", "Thành công",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    
                    LoadClasses();
                    ClearInputs();
                    lblStatus.Text = "Đã cập nhật lớp thành công";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi cập nhật lớp: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                lblStatus.Text = "Lỗi khi cập nhật lớp";
            }
        }

        /// <summary>
        /// Nút XÓA - Xóa lớp (kiểm tra sinh viên)
        /// ✅ LINQ: Kiểm tra lớp có sinh viên không trước khi xóa
        /// </summary>
        private void btnDelete_Click(object? sender, EventArgs e)
        {
            try
            {
                if (selectedClassId == 0)
                {
                    MessageBox.Show("Vui lòng chọn lớp cần xóa!", "Cảnh báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Kiểm tra số sinh viên hiện tại
                int studentCount = classRepository.GetStudentCountByClass(selectedClassId);
                
                if (studentCount > 0)
                {
                    MessageBox.Show(
                        $"Không thể xóa lớp '{txtClassName.Text}' vì còn {studentCount} sinh viên trong lớp!\n\n" +
                        "Vui lòng chuyển sinh viên sang lớp khác trước khi xóa.",
                        "Cảnh báo",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    return;
                }

                var confirmResult = MessageBox.Show(
                    $"Bạn có chắc chắn muốn xóa lớp '{txtClassName.Text}'?",
                    "Xác nhận xóa",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (confirmResult == DialogResult.Yes)
                {
                    bool result = classRepository.DeleteClass(selectedClassId);

                    if (result)
                    {
                        MessageBox.Show("Xóa lớp thành công!", "Thành công",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        
                        LoadClasses();
                        ClearInputs();
                        lblStatus.Text = "Đã xóa lớp thành công";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi xóa lớp: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                lblStatus.Text = "Lỗi khi xóa lớp";
            }
        }

        /// <summary>
        /// Nút TÌM KIẾM
        /// ✅ LINQ: Dùng SearchClass() với LINQ Where
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

                var classes = classRepository.SearchClass(keyword);

                var displayData = classes
                    .Select(c => new
                    {
                        c.ClassID,
                        TênLớp = c.ClassName,
                        GiáoViên = GetTeacherName(c.TeacherID),
                        SốSVHiệnTại = classRepository.GetStudentCountByClass(c.ClassID),
                        SĩSố = c.Capacity,
                        PhòngHọc = c.Room ?? ""
                    })
                    .ToList();

                dgvClass.DataSource = displayData;

                var colClassId = dgvClass.Columns["ClassID"];
                if (colClassId != null)
                {
                    colClassId.Visible = false;
                }

                lblRecordCount.Text = $"Tìm thấy: {displayData.Count} lớp";
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
            LoadClasses();
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
        private void dgvClass_SelectionChanged(object? sender, EventArgs e)
        {
            try
            {
                if (dgvClass.CurrentRow != null && dgvClass.CurrentRow.Index >= 0)
                {
                    var row = dgvClass.CurrentRow;
                    
                    if (row.Cells["ClassID"].Value != null)
                    {
                        selectedClassId = Convert.ToInt32(row.Cells["ClassID"].Value);

                        var classObj = classRepository.GetClassById(selectedClassId);

                        if (classObj != null)
                        {
                            txtClassName.Text = classObj.ClassName;
                            cmbTeacher.SelectedValue = classObj.TeacherID ?? 0;
                            numCapacity.Value = classObj.Capacity;
                            txtRoom.Text = classObj.Room ?? "";

                            // ✅ LINQ: Hiển thị số sinh viên hiện tại
                            int currentStudentCount = classRepository.GetStudentCountByClass(selectedClassId);
                            lblCurrentStudentCount.Text = currentStudentCount.ToString();
                            lblCurrentStudentCount.ForeColor = currentStudentCount > classObj.Capacity 
                                ? System.Drawing.Color.Red 
                                : System.Drawing.Color.FromArgb(41, 128, 185);

                            lblStatus.Text = $"Đã chọn: {classObj.ClassName} ({currentStudentCount}/{classObj.Capacity} SV)";
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

        #region Helper Methods

        /// <summary>
        /// Clear all input controls
        /// </summary>
        private void ClearInputs()
        {
            txtClassName.Clear();
            cmbTeacher.SelectedIndex = 0;
            numCapacity.Value = 30;
            txtRoom.Clear();
            lblCurrentStudentCount.Text = "0";
            
            selectedClassId = 0;
            txtClassName.Focus();
        }

        /// <summary>
        /// Validate input data
        /// </summary>
        private bool ValidateInputs()
        {
            if (string.IsNullOrWhiteSpace(txtClassName.Text))
            {
                MessageBox.Show("Vui lòng nhập tên lớp!", "Cảnh báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtClassName.Focus();
                return false;
            }

            if (numCapacity.Value <= 0)
            {
                MessageBox.Show("Sĩ số phải lớn hơn 0!", "Cảnh báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                numCapacity.Focus();
                return false;
            }

            return true;
        }

        #endregion
    }
}
