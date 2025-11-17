using StudentManagement.DAL;
using StudentManagement.Models;
using ClosedXML.Excel;

namespace StudentManagement.Views
{
    /// <summary>
    /// ✅ PROMPT 15: Form Báo cáo Thống kê Lớp học
    /// Sử dụng LINQ to Objects để JOIN nhiều bảng và tính toán thống kê
    /// </summary>
    public partial class ReportClassForm : Form
    {
        private readonly ClassRepository classRepository;
        private readonly TeacherRepository teacherRepository;
        private readonly StudentRepository studentRepository;
        private readonly ScoreRepository scoreRepository;
        
        private List<ClassReportItem>? currentReportData;

        public ReportClassForm()
        {
            InitializeComponent();
            
            classRepository = new ClassRepository();
            teacherRepository = new TeacherRepository();
            studentRepository = new StudentRepository();
            scoreRepository = new ScoreRepository();
        }

        private void ReportClassForm_Load(object sender, EventArgs e)
        {
            try
            {
                // Thiết lập DataGridView
                SetupDataGridView();
                
                lblStatus.Text = "✅ Nhấn 'Tạo báo cáo' để bắt đầu";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi khởi tạo form: {ex.Message}",
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Thiết lập cột cho DataGridView
        /// </summary>
        private void SetupDataGridView()
        {
            dgvReport.AutoGenerateColumns = false;
            dgvReport.Columns.Clear();

            // Cột 1: Tên lớp
            dgvReport.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colClassName", // ✅ Thêm Name
                DataPropertyName = "ClassName",
                HeaderText = "Tên lớp",
                Width = 120,
                DefaultCellStyle = new DataGridViewCellStyle { Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold) }
            });

            // Cột 2: Giáo viên chủ nhiệm
            dgvReport.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colTeacherName", // ✅ Thêm Name
                DataPropertyName = "TeacherName",
                HeaderText = "GVCN",
                Width = 150
            });

            // Cột 3: Số lượng sinh viên
            dgvReport.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colStudentCount", // ✅ Thêm Name
                DataPropertyName = "StudentCount",
                HeaderText = "Số SV",
                Width = 80,
                DefaultCellStyle = new DataGridViewCellStyle 
                { 
                    Alignment = DataGridViewContentAlignment.MiddleCenter,
                    ForeColor = System.Drawing.Color.FromArgb(41, 128, 185)
                }
            });

            // Cột 4: Sĩ số tối đa
            dgvReport.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colCapacity", // ✅ Thêm Name
                DataPropertyName = "Capacity",
                HeaderText = "Sĩ số",
                Width = 80,
                DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleCenter }
            });

            // Cột 5: Phòng học
            dgvReport.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colRoom", // ✅ Thêm Name
                DataPropertyName = "Room",
                HeaderText = "Phòng",
                Width = 100,
                DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleCenter }
            });

            // Cột 6: Điểm trung bình
            dgvReport.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colAverageScore", // ✅ Thêm Name
                DataPropertyName = "AverageScore",
                HeaderText = "Điểm TB",
                Width = 100,
                DefaultCellStyle = new DataGridViewCellStyle 
                { 
                    Format = "N2",
                    Alignment = DataGridViewContentAlignment.MiddleCenter,
                    ForeColor = System.Drawing.Color.FromArgb(39, 174, 96)
                }
            });

            // Cột 7: Điểm cao nhất
            dgvReport.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colHighestScore", // ✅ Thêm Name
                DataPropertyName = "HighestScore",
                HeaderText = "Cao nhất",
                Width = 100,
                DefaultCellStyle = new DataGridViewCellStyle 
                { 
                    Format = "N2",
                    Alignment = DataGridViewContentAlignment.MiddleCenter,
                    ForeColor = System.Drawing.Color.FromArgb(46, 204, 113)
                }
            });

            // Cột 8: Điểm thấp nhất
            dgvReport.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colLowestScore", // ✅ Thêm Name
                DataPropertyName = "LowestScore",
                HeaderText = "Thấp nhất",
                Width = 100,
                DefaultCellStyle = new DataGridViewCellStyle 
                { 
                    Format = "N2",
                    Alignment = DataGridViewContentAlignment.MiddleCenter,
                    ForeColor = System.Drawing.Color.FromArgb(231, 76, 60)
                }
            });

            // Cột 9: Tỷ lệ lấp đầy
            dgvReport.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colFillRate", // ✅ Thêm Name để tránh lỗi
                DataPropertyName = "FillRate",
                HeaderText = "Tỷ lệ (%)",
                Width = 100,
                DefaultCellStyle = new DataGridViewCellStyle 
                { 
                    Format = "N1",
                    Alignment = DataGridViewContentAlignment.MiddleCenter
                }
            });
        }

        /// <summary>
        /// ✅ LINQ: Tạo báo cáo thống kê lớp (JOIN nhiều bảng)
        /// </summary>
        private void btnGenerateReport_Click(object sender, EventArgs e)
        {
            try
            {
                lblStatus.Text = "⏳ Đang tạo báo cáo...";
                this.Cursor = Cursors.WaitCursor;

                // Bước 1: Load dữ liệu từ các Repository
                var allClasses = classRepository.GetAllClasses();
                var allTeachers = teacherRepository.GetAllTeachers();
                
                // ✅ Sử dụng GetStudentsObjectByClass để lấy tất cả sinh viên
                var allStudentsTemp = new List<Student>();
                foreach (var cls in allClasses)
                {
                    var studentsInClass = studentRepository.GetStudentsObjectByClass(cls.ClassID);
                    allStudentsTemp.AddRange(studentsInClass);
                }
                var allStudents = allStudentsTemp; // List<Student>
                
                var allScores = scoreRepository.GetAllScores(); // List<ScoreWithStudentInfo>

                // Bước 2: ✅ LINQ JOIN - Kết hợp Classes với Teachers
                var classWithTeacher = from cls in allClasses
                                       join teacher in allTeachers 
                                       on cls.TeacherID equals teacher.TeacherID into teacherGroup
                                       from teacher in teacherGroup.DefaultIfEmpty()
                                       select new
                                       {
                                           Class = cls,
                                           TeacherName = teacher?.TeacherName ?? "Chưa phân công"
                                       };

                // Bước 3: ✅ LINQ - Tính toán thống kê cho từng lớp
                var reportData = classWithTeacher.Select(item => new ClassReportItem
                {
                    ClassName = item.Class.ClassName,
                    TeacherName = item.TeacherName,
                    Capacity = item.Class.Capacity,
                    Room = item.Class.Room ?? "Chưa có",
                    
                    // ✅ LINQ Count - Đếm số sinh viên trong lớp
                    StudentCount = allStudents
                        .Count(s => s.ClassID == item.Class.ClassID && s.IsActive),
                    
                    // ✅ LINQ Average - Tính điểm TB của lớp
                    AverageScore = CalculateClassAverageScore(item.Class.ClassID, allStudents, allScores.ToList()),
                    
                    // ✅ LINQ Max - Điểm cao nhất
                    HighestScore = CalculateClassHighestScore(item.Class.ClassID, allStudents, allScores.ToList()),
                    
                    // ✅ LINQ Min - Điểm thấp nhất
                    LowestScore = CalculateClassLowestScore(item.Class.ClassID, allStudents, allScores.ToList())
                })
                .ToList();

                // Bước 4: ✅ LINQ - Tính tỷ lệ lấp đầy
                foreach (var item in reportData)
                {
                    item.FillRate = item.Capacity > 0 
                        ? (decimal)item.StudentCount / item.Capacity * 100 
                        : 0;
                }

                // Bước 5: ✅ LINQ OrderBy - Sắp xếp theo tên lớp
                currentReportData = reportData
                    .OrderBy(r => r.ClassName)
                    .ToList();

                // Hiển thị báo cáo
                DisplayReport(currentReportData);
                
                // Tính toán thống kê tổng quan
                CalculateSummaryStatistics(currentReportData);

                lblStatus.Text = $"✅ Tạo báo cáo thành công - {currentReportData.Count} lớp";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tạo báo cáo: {ex.Message}",
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                lblStatus.Text = "❌ Lỗi khi tạo báo cáo";
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        /// <summary>
        /// ✅ LINQ Average - Tính điểm TB của lớp
        /// </summary>
        private decimal CalculateClassAverageScore(int classId, List<Student> students, List<ScoreWithStudentInfo> scores)
        {
            // Lấy danh sách StudentID của lớp
            var studentIds = students
                .Where(s => s.ClassID == classId && s.IsActive)
                .Select(s => s.StudentID)
                .ToList();

            if (!studentIds.Any())
                return 0;

            // Lấy điểm của các sinh viên trong lớp
            var classScores = scores
                .Where(sc => studentIds.Contains(sc.StudentID))
                .ToList();

            if (!classScores.Any())
                return 0;

            // ✅ LINQ Average
            return classScores.Average(sc => sc.Score);
        }

        /// <summary>
        /// ✅ LINQ Max - Điểm cao nhất của lớp
        /// </summary>
        private decimal CalculateClassHighestScore(int classId, List<Student> students, List<ScoreWithStudentInfo> scores)
        {
            var studentIds = students
                .Where(s => s.ClassID == classId && s.IsActive)
                .Select(s => s.StudentID)
                .ToList();

            if (!studentIds.Any())
                return 0;

            var classScores = scores
                .Where(sc => studentIds.Contains(sc.StudentID))
                .ToList();

            if (!classScores.Any())
                return 0;

            // ✅ LINQ Max
            return classScores.Max(sc => sc.Score);
        }

        /// <summary>
        /// ✅ LINQ Min - Điểm thấp nhất của lớp
        /// </summary>
        private decimal CalculateClassLowestScore(int classId, List<Student> students, List<ScoreWithStudentInfo> scores)
        {
            var studentIds = students
                .Where(s => s.ClassID == classId && s.IsActive)
                .Select(s => s.StudentID)
                .ToList();

            if (!studentIds.Any())
                return 0;

            var classScores = scores
                .Where(sc => studentIds.Contains(sc.StudentID))
                .ToList();

            if (!classScores.Any())
                return 0;

            // ✅ LINQ Min
            return classScores.Min(sc => sc.Score);
        }

        /// <summary>
        /// Hiển thị báo cáo lên DataGridView
        /// </summary>
        private void DisplayReport(List<ClassReportItem> data)
        {
            dgvReport.DataSource = null;
            dgvReport.DataSource = data;
            
            // Format màu sắc theo tỷ lệ lấp đầy
            foreach (DataGridViewRow row in dgvReport.Rows)
            {
                if (row.DataBoundItem is ClassReportItem item)
                {
                    // Tô màu tỷ lệ lấp đầy
                    if (item.FillRate >= 90)
                        row.Cells["colFillRate"].Style.BackColor = System.Drawing.Color.FromArgb(255, 200, 200); // Đỏ nhạt - Quá tải
                    else if (item.FillRate >= 70)
                        row.Cells["colFillRate"].Style.BackColor = System.Drawing.Color.FromArgb(200, 255, 200); // Xanh nhạt - Tốt
                    else if (item.FillRate >= 50)
                        row.Cells["colFillRate"].Style.BackColor = System.Drawing.Color.FromArgb(255, 255, 200); // Vàng nhạt - Trung bình
                    else
                        row.Cells["colFillRate"].Style.BackColor = System.Drawing.Color.FromArgb(220, 220, 220); // Xám - Thấp
                }
            }
        }

        /// <summary>
        /// ✅ LINQ Sum, Average, Count - Tính thống kê tổng quan
        /// </summary>
        private void CalculateSummaryStatistics(List<ClassReportItem> data)
        {
            if (data == null || !data.Any())
            {
                lblTotalClasses.Text = "Tổng số lớp: 0";
                lblTotalStudents.Text = "Tổng sinh viên: 0";
                lblAvgCapacity.Text = "Sĩ số TB mỗi lớp: --";
                lblAvgScore.Text = "Điểm TB chung: --";
                return;
            }

            // ✅ LINQ Count
            int totalClasses = data.Count();
            
            // ✅ LINQ Sum
            int totalStudents = data.Sum(d => d.StudentCount);
            
            // ✅ LINQ Average (cast sang decimal)
            decimal avgCapacity = (decimal)data.Average(d => d.Capacity);
            
            // ✅ LINQ Average (chỉ tính lớp có điểm)
            var classesWithScores = data.Where(d => d.AverageScore > 0).ToList();
            decimal avgScore = classesWithScores.Any() 
                ? classesWithScores.Average(d => d.AverageScore) 
                : 0;

            // Hiển thị kết quả
            lblTotalClasses.Text = $"Tổng số lớp: {totalClasses}";
            lblTotalStudents.Text = $"Tổng sinh viên: {totalStudents}";
            lblAvgCapacity.Text = $"Sĩ số TB mỗi lớp: {avgCapacity:N1}";
            lblAvgScore.Text = $"Điểm TB chung: {avgScore:N2}";
        }

        /// <summary>
        /// Xuất báo cáo ra Excel
        /// </summary>
        private void btnExport_Click(object sender, EventArgs e)
        {
            try
            {
                if (currentReportData == null || !currentReportData.Any())
                {
                    MessageBox.Show("Vui lòng tạo báo cáo trước khi xuất Excel!",
                        "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                SaveFileDialog saveDialog = new SaveFileDialog
                {
                    Filter = "Excel Files|*.xlsx",
                    FileName = $"BaoCaoLop_{DateTime.Now:yyyyMMdd_HHmmss}.xlsx",
                    Title = "Xuất báo cáo ra Excel"
                };

                if (saveDialog.ShowDialog() == DialogResult.OK)
                {
                    using (var workbook = new XLWorkbook())
                    {
                        var worksheet = workbook.Worksheets.Add("Báo cáo Lớp");

                        // Header
                        worksheet.Cell(1, 1).Value = "BÁO CÁO THỐNG KÊ LỚP HỌC";
                        worksheet.Cell(1, 1).Style.Font.Bold = true;
                        worksheet.Cell(1, 1).Style.Font.FontSize = 16;
                        worksheet.Range(1, 1, 1, 9).Merge();

                        worksheet.Cell(2, 1).Value = $"Ngày xuất: {DateTime.Now:dd/MM/yyyy HH:mm:ss}";
                        worksheet.Range(2, 1, 2, 9).Merge();

                        // Column headers
                        var headers = new[] { "Tên lớp", "GVCN", "Số SV", "Sĩ số", "Phòng", "Điểm TB", "Cao nhất", "Thấp nhất", "Tỷ lệ (%)" };
                        for (int i = 0; i < headers.Length; i++)
                        {
                            worksheet.Cell(4, i + 1).Value = headers[i];
                            worksheet.Cell(4, i + 1).Style.Font.Bold = true;
                            worksheet.Cell(4, i + 1).Style.Fill.BackgroundColor = XLColor.LightBlue;
                        }

                        // Data rows
                        int row = 5;
                        foreach (var item in currentReportData)
                        {
                            worksheet.Cell(row, 1).Value = item.ClassName;
                            worksheet.Cell(row, 2).Value = item.TeacherName;
                            worksheet.Cell(row, 3).Value = item.StudentCount;
                            worksheet.Cell(row, 4).Value = item.Capacity;
                            worksheet.Cell(row, 5).Value = item.Room;
                            worksheet.Cell(row, 6).Value = item.AverageScore;
                            worksheet.Cell(row, 7).Value = item.HighestScore;
                            worksheet.Cell(row, 8).Value = item.LowestScore;
                            worksheet.Cell(row, 9).Value = item.FillRate;
                            row++;
                        }

                        // Auto-fit columns
                        worksheet.Columns().AdjustToContents();

                        workbook.SaveAs(saveDialog.FileName);
                    }

                    MessageBox.Show($"Xuất Excel thành công!\nĐường dẫn: {saveDialog.FileName}",
                        "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    
                    lblStatus.Text = "✅ Xuất Excel thành công";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi xuất Excel: {ex.Message}",
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// In báo cáo
        /// </summary>
        private void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                if (currentReportData == null || !currentReportData.Any())
                {
                    MessageBox.Show("Vui lòng tạo báo cáo trước khi in!",
                        "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                MessageBox.Show("Chức năng in sẽ được phát triển trong phiên bản tiếp theo!",
                    "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi in: {ex.Message}",
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }

    /// <summary>
    /// Model cho báo cáo lớp học
    /// </summary>
    public class ClassReportItem
    {
        public string ClassName { get; set; } = string.Empty;
        public string TeacherName { get; set; } = string.Empty;
        public int StudentCount { get; set; }
        public int Capacity { get; set; }
        public string Room { get; set; } = string.Empty;
        public decimal AverageScore { get; set; }
        public decimal HighestScore { get; set; }
        public decimal LowestScore { get; set; }
        public decimal FillRate { get; set; } // Tỷ lệ lấp đầy (%)
    }
}
