using StudentManagement.DAL;
using StudentManagement.Models;

namespace StudentManagement.Views
{
    /// <summary>
    /// Form quản lý điểm - PROMPT 11
    /// Chức năng: CRUD điểm, filter theo lớp/sinh viên/học kỳ/năm học, tính điểm TB và xếp loại
    /// </summary>
    public partial class ScoreForm : Form
    {
        private ScoreRepository scoreRepo;
        private ClassRepository classRepo;
        private StudentRepository studentRepo;
        private int selectedScoreID = 0;
        private int currentStudentID = 0;

        public ScoreForm()
        {
            InitializeComponent();
            scoreRepo = new ScoreRepository();
            classRepo = new ClassRepository();
            studentRepo = new StudentRepository();
        }

        #region Form Events

        /// <summary>
        /// Form Load - Khởi tạo dữ liệu ban đầu
        /// </summary>
        private void ScoreForm_Load(object sender, EventArgs e)
        {
            try
            {
                LoadClasses();
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
        /// Load danh sách lớp vào ComboBox
        /// </summary>
        private void LoadClasses()
        {
            try
            {
                var classes = classRepo.GetAllClasses();
                
                // ✅ SỬA: Dùng List thay vì Items.Add
                var classList = new List<Class>();
                classList.Add(new Class { ClassID = 0, ClassName = "-- Chọn lớp --" });
                classList.AddRange(classes);
                
                cmbClass.DataSource = classList;
                cmbClass.DisplayMember = "ClassName";
                cmbClass.ValueMember = "ClassID";
                cmbClass.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải danh sách lớp: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Load sinh viên theo lớp
        /// </summary>
        private void cmbClass_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                // ✅ Reset danh sách sinh viên
                cmbStudent.DataSource = null;
                dgvScore.DataSource = null;
                currentStudentID = 0;
                UpdateStatistics(null);
                
                // ✅ SỬA LỖI: Kiểm tra SelectedIndex thay vì SelectedValue
                if (cmbClass.SelectedIndex <= 0)
                {
                    var emptyList = new List<Student>();
                    emptyList.Add(new Student { StudentID = 0, StudentName = "-- Chọn sinh viên --" });
                    cmbStudent.DataSource = emptyList;
                    cmbStudent.DisplayMember = "StudentName";
                    cmbStudent.ValueMember = "StudentID";
                    return;
                }

                // ✅ SỬA LỖI: Lấy ClassID từ object Class
                var selectedClass = cmbClass.SelectedItem as Class;
                if (selectedClass == null || selectedClass.ClassID == 0)
                    return;

                int classID = selectedClass.ClassID;
                
                // ✅ SỬA: Dùng GetStudentsObjectByClass thay vì GetStudentsByClass
                var students = studentRepo.GetStudentsObjectByClass(classID);
                
                // ✅ SỬA: Dùng List với item placeholder
                var studentList = new List<Student>();
                studentList.Add(new Student { StudentID = 0, StudentName = "-- Chọn sinh viên --" });
                studentList.AddRange(students);
                
                cmbStudent.DataSource = studentList;
                cmbStudent.DisplayMember = "StudentName";
                cmbStudent.ValueMember = "StudentID";
                cmbStudent.SelectedIndex = 0;
                
                UpdateStatus($"Đã tải {students.Count} sinh viên của lớp {selectedClass.ClassName}");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải sinh viên: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Load điểm của sinh viên khi chọn
        /// </summary>
        private void cmbStudent_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                // ✅ SỬA LỖI: Kiểm tra SelectedIndex thay vì SelectedValue
                if (cmbStudent.SelectedIndex <= 0)
                {
                    currentStudentID = 0;
                    dgvScore.DataSource = null;
                    UpdateStatistics(null);
                    UpdateRecordCount(0);
                    return;
                }

                // ✅ SỬA LỖI: Lấy StudentID từ object Student
                var selectedStudent = cmbStudent.SelectedItem as Student;
                if (selectedStudent == null || selectedStudent.StudentID == 0)
                    return;

                currentStudentID = selectedStudent.StudentID;
                LoadScoresByStudent(currentStudentID);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải điểm: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Load tất cả điểm của sinh viên
        /// </summary>
        private void LoadScoresByStudent(int studentID)
        {
            try
            {
                var scores = scoreRepo.GetScoresByStudent(studentID);
                
                // ✅ TRÁNH LỖI IndexOutOfRangeException: Set DataSource = null trước
                dgvScore.DataSource = null;
                dgvScore.Columns.Clear();
                
                if (scores == null || scores.Count == 0)
                {
                    UpdateStatistics(null);
                    UpdateRecordCount(0);
                    UpdateStatus("Sinh viên chưa có điểm");
                    return;
                }
                
                // Tạo DataSource mới
                dgvScore.DataSource = scores;
                
                SetupDataGridView();
                UpdateStatistics(scores);
                UpdateRecordCount(scores.Count);
                UpdateStatus($"Đã tải {scores.Count} điểm của sinh viên {cmbStudent.Text}");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải điểm sinh viên: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// ✅ Thiết lập cột DataGridView - TRÁNH LỖI Index
        /// </summary>
        private void SetupDataGridView()
        {
            try
            {
                if (dgvScore.Columns.Count == 0) return;

                // Ẩn các cột không cần thiết
                if (dgvScore.Columns.Contains("ScoreID") && dgvScore.Columns["ScoreID"] != null)
                    dgvScore.Columns["ScoreID"]!.Visible = false;
                
                if (dgvScore.Columns.Contains("StudentID") && dgvScore.Columns["StudentID"] != null)
                    dgvScore.Columns["StudentID"]!.Visible = false;
                
                if (dgvScore.Columns.Contains("StudentName") && dgvScore.Columns["StudentName"] != null)
                    dgvScore.Columns["StudentName"]!.Visible = false;

                // Đặt tên tiêu đề cột
                if (dgvScore.Columns.Contains("SubjectName") && dgvScore.Columns["SubjectName"] != null)
                {
                    var col = dgvScore.Columns["SubjectName"]!;
                    col.HeaderText = "Môn học";
                    col.Width = 200;
                    col.DisplayIndex = 0;
                }

                if (dgvScore.Columns.Contains("Score") && dgvScore.Columns["Score"] != null)
                {
                    var col = dgvScore.Columns["Score"]!;
                    col.HeaderText = "Điểm";
                    col.Width = 80;
                    col.DefaultCellStyle.Format = "0.0";
                    col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    col.DisplayIndex = 1;
                }

                if (dgvScore.Columns.Contains("ScoreDate") && dgvScore.Columns["ScoreDate"] != null)
                {
                    var col = dgvScore.Columns["ScoreDate"]!;
                    col.HeaderText = "Ngày thi";
                    col.Width = 100;
                    col.DefaultCellStyle.Format = "dd/MM/yyyy";
                    col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    col.DisplayIndex = 2;
                }

                if (dgvScore.Columns.Contains("Semester") && dgvScore.Columns["Semester"] != null)
                {
                    var col = dgvScore.Columns["Semester"]!;
                    col.HeaderText = "Học kỳ";
                    col.Width = 80;
                    col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    col.DisplayIndex = 3;
                }

                if (dgvScore.Columns.Contains("AcademicYear") && dgvScore.Columns["AcademicYear"] != null)
                {
                    var col = dgvScore.Columns["AcademicYear"]!;
                    col.HeaderText = "Năm học";
                    col.Width = 100;
                    col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    col.DisplayIndex = 4;
                }

                if (dgvScore.Columns.Contains("Notes") && dgvScore.Columns["Notes"] != null)
                {
                    var col = dgvScore.Columns["Notes"]!;
                    col.HeaderText = "Ghi chú";
                    col.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                    col.DisplayIndex = 5;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi thiết lập DataGridView: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region Filter Operations

        /// <summary>
        /// Lọc điểm theo học kỳ và năm học
        /// </summary>
        private void btnFilterBySemester_Click(object sender, EventArgs e)
        {
            try
            {
                if (currentStudentID == 0)
                {
                    MessageBox.Show("Vui lòng chọn sinh viên trước!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // ✅ SỬA LỖI: Kiểm tra null an toàn
                string selectedSemester = cmbSemester.SelectedItem?.ToString() ?? "Tất cả";
                string academicYear = txtAcademicYear.Text.Trim();

                List<ScoreWithStudentInfo> filteredScores;

                if (selectedSemester == "Tất cả" || string.IsNullOrEmpty(selectedSemester))
                {
                    // Lấy tất cả điểm của sinh viên
                    filteredScores = scoreRepo.GetScoresByStudent(currentStudentID);
                }
                else
                {
                    // Lấy số học kỳ từ text (VD: "Học kỳ 1" -> 1)
                    int semester = int.Parse(selectedSemester.Replace("Học kỳ ", ""));
                    
                    // ✅ PROMPT 11: Filter theo Semester và AcademicYear
                    filteredScores = scoreRepo.GetScoresByStudent(currentStudentID, semester, academicYear);
                }

                // ✅ TRÁNH LỖI: Clear trước khi set DataSource
                dgvScore.DataSource = null;
                dgvScore.Columns.Clear();
                dgvScore.DataSource = filteredScores;
                
                SetupDataGridView();
                UpdateStatistics(filteredScores);
                UpdateRecordCount(filteredScores.Count);
                UpdateStatus($"Đã lọc: {filteredScores.Count} điểm");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi lọc điểm: {ex.Message}", "Lỗi",
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
                if (currentStudentID > 0)
                {
                    LoadScoresByStudent(currentStudentID);
                    cmbSemester.SelectedIndex = 0;
                    txtAcademicYear.Text = "2024-2025";
                }
                
                ClearInputs();
                UpdateStatus("Đã làm mới");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi làm mới: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region CRUD Operations

        /// <summary>
        /// Thêm điểm mới
        /// </summary>
        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (currentStudentID == 0)
                {
                    MessageBox.Show("Vui lòng chọn sinh viên trước!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Validate dữ liệu
                if (!ValidateInputs()) return;

                // Lấy học kỳ từ ComboBox
                string selectedSemester = cmbSemester.SelectedItem?.ToString() ?? "Học kỳ 1";
                int semester = selectedSemester == "Tất cả" ? 1 : int.Parse(selectedSemester.Replace("Học kỳ ", ""));

                // Tạo đối tượng Score
                var score = new Score
                {
                    StudentID = currentStudentID,
                    SubjectName = txtSubject.Text.Trim(),
                    ScoreValue = numScore.Value,
                    ScoreDate = dtpScoreDate.Value,
                    Semester = semester,
                    AcademicYear = txtAcademicYear.Text.Trim(),
                    Notes = txtNotes.Text.Trim()
                };

                // Gọi Repository để thêm
                bool result = scoreRepo.AddScore(score);

                if (result)
                {
                    MessageBox.Show("Thêm điểm thành công!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    
                    LoadScoresByStudent(currentStudentID);
                    ClearInputs();
                    UpdateStatus("Đã thêm điểm mới");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi thêm điểm:\n{ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Cập nhật điểm
        /// </summary>
        private void btnEdit_Click(object sender, EventArgs e)
        {
            try
            {
                if (selectedScoreID == 0)
                {
                    MessageBox.Show("Vui lòng chọn điểm cần sửa!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Validate dữ liệu
                if (!ValidateInputs()) return;

                // Lấy học kỳ
                string selectedSemester = cmbSemester.SelectedItem?.ToString() ?? "Học kỳ 1";
                int semester = selectedSemester == "Tất cả" ? 1 : int.Parse(selectedSemester.Replace("Học kỳ ", ""));

                // Tạo đối tượng Score
                var score = new Score
                {
                    ScoreID = selectedScoreID,
                    StudentID = currentStudentID,
                    SubjectName = txtSubject.Text.Trim(),
                    ScoreValue = numScore.Value,
                    ScoreDate = dtpScoreDate.Value,
                    Semester = semester,
                    AcademicYear = txtAcademicYear.Text.Trim(),
                    Notes = txtNotes.Text.Trim()
                };

                // Gọi Repository để cập nhật
                bool result = scoreRepo.UpdateScore(score);

                if (result)
                {
                    MessageBox.Show("Cập nhật điểm thành công!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    
                    LoadScoresByStudent(currentStudentID);
                    ClearInputs();
                    UpdateStatus("Đã cập nhật điểm");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi cập nhật điểm:\n{ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Xóa điểm
        /// </summary>
        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (selectedScoreID == 0)
                {
                    MessageBox.Show("Vui lòng chọn điểm cần xóa!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Xác nhận xóa
                var confirmResult = MessageBox.Show(
                    $"Bạn có chắc chắn muốn xóa điểm môn '{txtSubject.Text}'?",
                    "Xác nhận xóa",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (confirmResult == DialogResult.Yes)
                {
                    bool result = scoreRepo.DeleteScore(selectedScoreID);

                    if (result)
                    {
                        MessageBox.Show("Xóa điểm thành công!", "Thông báo",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        
                        LoadScoresByStudent(currentStudentID);
                        ClearInputs();
                        UpdateStatus("Đã xóa điểm");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi xóa điểm:\n{ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region DataGridView Events

        /// <summary>
        /// ✅ Khi chọn dòng trong DataGridView - TRÁNH LỖI Index
        /// </summary>
        private void dgvScore_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                if (dgvScore.CurrentRow == null || dgvScore.CurrentRow.Index < 0)
                    return;

                var row = dgvScore.CurrentRow;
                
                // ✅ TRÁNH LỖI: Kiểm tra column tồn tại trước khi truy cập
                if (!dgvScore.Columns.Contains("ScoreID")) return;

                // Lấy ScoreID
                var scoreIDCell = row.Cells["ScoreID"];
                if (scoreIDCell.Value == null || scoreIDCell.Value == DBNull.Value)
                    return;

                selectedScoreID = Convert.ToInt32(scoreIDCell.Value);

                // Lấy thông tin điểm
                var score = scoreRepo.GetScoreById(selectedScoreID);

                if (score != null)
                {
                    txtSubject.Text = score.SubjectName;
                    numScore.Value = score.ScoreValue;
                    
                    if (score.ScoreDate.HasValue)
                        dtpScoreDate.Value = score.ScoreDate.Value;
                    
                    // Set học kỳ
                    int semesterIndex = score.Semester;
                    if (semesterIndex >= 1 && semesterIndex <= 3)
                        cmbSemester.SelectedIndex = semesterIndex; // 1->1, 2->2, 3->3
                    
                    txtAcademicYear.Text = score.AcademicYear;
                    txtNotes.Text = score.Notes ?? "";

                    UpdateStatus($"Đã chọn: {score.SubjectName}");
                }
            }
            catch (Exception ex)
            {
                // ✅ TRÁNH LỖI: Catch exception nhưng không hiển thị MessageBox liên tục
                Console.WriteLine($"Lỗi khi chọn dòng: {ex.Message}");
            }
        }

        #endregion

        #region Statistics

        /// <summary>
        /// ✅ PROMPT 11: Cập nhật thống kê (Điểm TB, Xếp loại, Số môn)
        /// </summary>
        private void UpdateStatistics(List<ScoreWithStudentInfo>? scores)
        {
            try
            {
                if (scores == null || scores.Count == 0)
                {
                    lblAverageScore.Text = "0.0";
                    lblGradeValue.Text = "-";
                    lblSubjectCountValue.Text = "0";
                    return;
                }

                // Tính điểm trung bình
                decimal avgScore = scores.Average(s => s.Score);
                lblAverageScore.Text = avgScore.ToString("F2");

                // ✅ PROMPT 11: Xếp loại (Giỏi/Khá/TB/Yếu)
                string grade = GetGrade(avgScore);
                lblGradeValue.Text = grade;
                
                // Đổi màu theo xếp loại
                if (avgScore >= 8.0m)
                    lblGradeValue.ForeColor = System.Drawing.Color.FromArgb(46, 204, 113); // Xanh lá
                else if (avgScore >= 6.5m)
                    lblGradeValue.ForeColor = System.Drawing.Color.FromArgb(52, 152, 219); // Xanh dương
                else if (avgScore >= 5.0m)
                    lblGradeValue.ForeColor = System.Drawing.Color.FromArgb(241, 196, 15); // Vàng
                else
                    lblGradeValue.ForeColor = System.Drawing.Color.FromArgb(231, 76, 60); // Đỏ

                // Đếm số môn
                int subjectCount = scores.Select(s => s.SubjectName).Distinct().Count();
                lblSubjectCountValue.Text = subjectCount.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi cập nhật thống kê: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// ✅ PROMPT 11: Xếp loại học lực
        /// </summary>
        private string GetGrade(decimal avgScore)
        {
            if (avgScore >= 8.0m) return "Giỏi";
            if (avgScore >= 6.5m) return "Khá";
            if (avgScore >= 5.0m) return "Trung bình";
            if (avgScore >= 3.5m) return "Yếu";
            return "Kém";
        }

        #endregion

        #region Helper Methods

        /// <summary>
        /// Clear inputs
        /// </summary>
        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearInputs();
            UpdateStatus("Đã xóa thông tin nhập");
        }

        private void ClearInputs()
        {
            selectedScoreID = 0;
            txtSubject.Clear();
            numScore.Value = 0;
            dtpScoreDate.Value = DateTime.Now;
            txtNotes.Clear();
            
            txtSubject.Focus();
        }

        /// <summary>
        /// Validate dữ liệu nhập
        /// </summary>
        private bool ValidateInputs()
        {
            if (string.IsNullOrWhiteSpace(txtSubject.Text))
            {
                MessageBox.Show("Vui lòng nhập tên môn học!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtSubject.Focus();
                return false;
            }

            if (numScore.Value < 0 || numScore.Value > 10)
            {
                MessageBox.Show("Điểm phải từ 0 đến 10!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                numScore.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtAcademicYear.Text))
            {
                MessageBox.Show("Vui lòng nhập năm học!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtAcademicYear.Focus();
                return false;
            }

            return true;
        }

        /// <summary>
        /// Cập nhật trạng thái
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
            lblRecordCount.Text = $"Tổng: {count} điểm";
        }

        #endregion
    }
}
