using StudentManagement.DAL;
using StudentManagement.Models;

namespace StudentManagement.Views
{
    /// <summary>
    /// ✅ PROMPT 16: Form Biểu đồ Cột - Điểm trung bình theo lớp
    /// Sử dụng LINQ to Objects để tính điểm TB và hiển thị Chart
    /// </summary>
    public partial class ChartForm1 : Form
    {
        private readonly ClassRepository classRepository;
        private readonly StudentRepository studentRepository;
        private readonly ScoreRepository scoreRepository;

        public ChartForm1()
        {
            InitializeComponent();
            
            classRepository = new ClassRepository();
            studentRepository = new StudentRepository();
            scoreRepository = new ScoreRepository();
        }

        private void ChartForm1_Load(object sender, EventArgs e)
        {
            try
            {
                // Load ComboBox Học kỳ
                LoadSemesterComboBox();
                
                // Load dữ liệu ban đầu
                LoadChartData();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi khởi tạo form: {ex.Message}",
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Load dữ liệu cho ComboBox Học kỳ
        /// </summary>
        private void LoadSemesterComboBox()
        {
            cmbSemester.Items.Clear();
            cmbSemester.Items.Add("Tất cả");
            cmbSemester.Items.Add("Học kỳ 1");
            cmbSemester.Items.Add("Học kỳ 2");
            cmbSemester.SelectedIndex = 0; // Mặc định: Tất cả
        }

        /// <summary>
        /// ✅ LINQ: Load dữ liệu và vẽ biểu đồ
        /// </summary>
        private void LoadChartData()
        {
            try
            {
                lblStatus.Text = "⏳ Đang tải dữ liệu...";
                this.Cursor = Cursors.WaitCursor;

                // Bước 1: Lấy dữ liệu từ Repository
                var allClasses = classRepository.GetAllClasses();
                var allStudents = new List<Student>();
                
                // Load tất cả sinh viên
                foreach (var cls in allClasses)
                {
                    var studentsInClass = studentRepository.GetStudentsObjectByClass(cls.ClassID);
                    allStudents.AddRange(studentsInClass);
                }
                
                var allScores = scoreRepository.GetAllScores();

                // Bước 2: Lọc theo Học kỳ và Năm học
                var filteredScores = FilterScores(allScores);

                // Bước 3: ✅ LINQ - Tính điểm TB theo lớp
                var chartData = CalculateAverageScoreByClass(allClasses, allStudents, filteredScores);

                // Bước 4: Vẽ biểu đồ
                DrawChart(chartData);

                lblStatus.Text = $"✅ Hiển thị {chartData.Count} lớp học";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải dữ liệu: {ex.Message}",
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                lblStatus.Text = "❌ Lỗi khi tải dữ liệu";
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        /// <summary>
        /// ✅ LINQ: Lọc điểm theo Học kỳ và Năm học
        /// </summary>
        private List<ScoreWithStudentInfo> FilterScores(List<ScoreWithStudentInfo> scores)
        {
            var filtered = scores.AsQueryable();

            // ✅ LINQ Where - Lọc theo học kỳ
            if (cmbSemester.SelectedIndex > 0) // Không phải "Tất cả"
            {
                int semester = cmbSemester.SelectedIndex; // 1 hoặc 2
                filtered = filtered.Where(s => s.Semester == semester);
            }

            // ✅ LINQ Where - Lọc theo năm học
            if (!string.IsNullOrWhiteSpace(txtAcademicYear.Text))
            {
                string academicYear = txtAcademicYear.Text.Trim();
                filtered = filtered.Where(s => s.AcademicYear == academicYear);
            }

            return filtered.ToList();
        }

        /// <summary>
        /// ✅ LINQ: Tính điểm TB theo lớp (GroupBy + Average)
        /// </summary>
        private List<ClassChartData> CalculateAverageScoreByClass(
            List<Class> classes, 
            List<Student> students, 
            List<ScoreWithStudentInfo> scores)
        {
            // ✅ LINQ: Join Classes với Students
            var classStudents = from cls in classes
                                join student in students on cls.ClassID equals student.ClassID
                                where student.IsActive
                                select new
                                {
                                    ClassID = cls.ClassID,
                                    ClassName = cls.ClassName,
                                    StudentID = student.StudentID
                                };

            // ✅ LINQ: Join với Scores và tính điểm TB
            var result = (from cs in classStudents
                          join score in scores on cs.StudentID equals score.StudentID
                          group score by new { cs.ClassID, cs.ClassName } into g
                          select new ClassChartData
                          {
                              ClassName = g.Key.ClassName,
                              AverageScore = g.Average(s => s.Score), // ✅ LINQ Average
                              StudentCount = g.Select(s => s.StudentID).Distinct().Count(), // ✅ LINQ Distinct + Count
                              TotalScores = g.Count() // ✅ LINQ Count
                          })
                          .OrderBy(c => c.ClassName) // ✅ LINQ OrderBy
                          .ToList();

            return result;
        }

        /// <summary>
        /// Vẽ biểu đồ cột
        /// </summary>
        private void DrawChart(List<ClassChartData> data)
        {
            // Xóa dữ liệu cũ
            chart1.Series["Điểm TB"].Points.Clear();

            if (data == null || !data.Any())
            {
                lblStatus.Text = "⚠️ Không có dữ liệu để hiển thị";
                return;
            }

            // Thêm dữ liệu vào chart
            foreach (var item in data)
            {
                var point = chart1.Series["Điểm TB"].Points.AddXY(item.ClassName, (double)item.AverageScore);
                
                // ✅ Tô màu theo điểm TB
                if (item.AverageScore >= 8.0m)
                    chart1.Series["Điểm TB"].Points[point].Color = System.Drawing.Color.FromArgb(46, 204, 113); // Xanh lá - Giỏi
                else if (item.AverageScore >= 6.5m)
                    chart1.Series["Điểm TB"].Points[point].Color = System.Drawing.Color.FromArgb(52, 152, 219); // Xanh dương - Khá
                else if (item.AverageScore >= 5.0m)
                    chart1.Series["Điểm TB"].Points[point].Color = System.Drawing.Color.FromArgb(241, 196, 15); // Vàng - TB
                else
                    chart1.Series["Điểm TB"].Points[point].Color = System.Drawing.Color.FromArgb(231, 76, 60); // Đỏ - Yếu/Kém

                // Tooltip hiển thị chi tiết
                chart1.Series["Điểm TB"].Points[point].ToolTip = 
                    $"Lớp: {item.ClassName}\n" +
                    $"Điểm TB: {item.AverageScore:N2}\n" +
                    $"Số SV: {item.StudentCount}\n" +
                    $"Tổng điểm: {item.TotalScores}";
            }

            // Tùy chỉnh Chart
            chart1.ChartAreas[0].AxisX.LabelStyle.Angle = -45; // Xoay label 45 độ
            chart1.ChartAreas[0].AxisX.Interval = 1;
            chart1.ChartAreas[0].AxisY.Minimum = 0;
            chart1.ChartAreas[0].AxisY.Maximum = 10;
            chart1.ChartAreas[0].AxisY.Interval = 1;

            // Hiển thị giá trị trên cột
            chart1.Series["Điểm TB"].IsValueShownAsLabel = true;
            chart1.Series["Điểm TB"].LabelFormat = "N2";
            chart1.Series["Điểm TB"].Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
        }

        /// <summary>
        /// Nút Làm mới
        /// </summary>
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadChartData();
        }

        /// <summary>
        /// Nút Đóng
        /// </summary>
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }

    /// <summary>
    /// Model cho dữ liệu biểu đồ
    /// </summary>
    public class ClassChartData
    {
        public string ClassName { get; set; } = string.Empty;
        public decimal AverageScore { get; set; }
        public int StudentCount { get; set; }
        public int TotalScores { get; set; }
    }
}
