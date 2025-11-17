using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using StudentManagement.DAL;
using StudentManagement.Models;

namespace StudentManagement.Views
{
    /// <summary>
    /// ✅ PROMPT 17: Form Biểu đồ Tròn - Phân loại sinh viên theo điểm
    /// Sử dụng LINQ to Objects để tính điểm TB và phân loại
    /// </summary>
    public partial class ChartForm2 : Form
    {
        private readonly StudentRepository studentRepository;
        private readonly ScoreRepository scoreRepository;

        public ChartForm2()
        {
            InitializeComponent();
            
            studentRepository = new StudentRepository();
            scoreRepository = new ScoreRepository();
        }

        private void ChartForm2_Load(object sender, EventArgs e)
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
        /// ✅ LINQ: Load dữ liệu và vẽ biểu đồ tròn
        /// </summary>
        private void LoadChartData()
        {
            try
            {
                lblStatus.Text = "⏳ Đang tải dữ liệu...";
                this.Cursor = Cursors.WaitCursor;

                // Bước 1: Lấy tất cả điểm
                var allScores = scoreRepository.GetAllScores();

                // Bước 2: Lọc theo Học kỳ và Năm học
                var filteredScores = FilterScores(allScores);

                // Bước 3: ✅ LINQ - Tính điểm TB của từng sinh viên
                var studentAverages = CalculateStudentAverages(filteredScores);

                // Bước 4: ✅ LINQ - Phân loại sinh viên
                var classification = ClassifyStudents(studentAverages);

                // Bước 5: Vẽ biểu đồ tròn
                DrawPieChart(classification);

                // Bước 6: Hiển thị thống kê
                DisplayStatistics(classification);

                lblStatus.Text = $"✅ Phân loại {classification.TotalStudents} sinh viên";
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
        /// ✅ LINQ: Tính điểm TB của từng sinh viên (GroupBy + Average)
        /// </summary>
        private List<StudentAverageScore> CalculateStudentAverages(List<ScoreWithStudentInfo> scores)
        {
            // ✅ LINQ: GroupBy StudentID, tính Average
            var averages = scores
                .GroupBy(s => new { s.StudentID, s.StudentName })
                .Select(g => new StudentAverageScore
                {
                    StudentID = g.Key.StudentID,
                    StudentName = g.Key.StudentName,
                    AverageScore = g.Average(s => s.Score), // ✅ LINQ Average
                    SubjectCount = g.Count() // ✅ LINQ Count
                })
                .ToList();

            return averages;
        }

        /// <summary>
        /// ✅ LINQ: Phân loại sinh viên theo điểm TB (Count)
        /// </summary>
        private StudentClassification ClassifyStudents(List<StudentAverageScore> studentAverages)
        {
            var classification = new StudentClassification();

            if (!studentAverages.Any())
                return classification;

            // ✅ LINQ Count - Đếm số lượng từng loại
            classification.ExcellentCount = studentAverages.Count(s => s.AverageScore >= 8.0m);
            classification.GoodCount = studentAverages.Count(s => s.AverageScore >= 6.5m && s.AverageScore < 8.0m);
            classification.AverageCount = studentAverages.Count(s => s.AverageScore >= 5.0m && s.AverageScore < 6.5m);
            classification.WeakCount = studentAverages.Count(s => s.AverageScore < 5.0m);
            classification.TotalStudents = studentAverages.Count();

            // Tính phần trăm
            if (classification.TotalStudents > 0)
            {
                classification.ExcellentPercent = (decimal)classification.ExcellentCount / classification.TotalStudents * 100;
                classification.GoodPercent = (decimal)classification.GoodCount / classification.TotalStudents * 100;
                classification.AveragePercent = (decimal)classification.AverageCount / classification.TotalStudents * 100;
                classification.WeakPercent = (decimal)classification.WeakCount / classification.TotalStudents * 100;
            }

            return classification;
        }

        /// <summary>
        /// Vẽ biểu đồ tròn (Pie Chart)
        /// </summary>
        private void DrawPieChart(StudentClassification classification)
        {
            // Xóa dữ liệu cũ
            chart2.Series["Phân loại"].Points.Clear();

            if (classification.TotalStudents == 0)
            {
                lblStatus.Text = "⚠️ Không có dữ liệu để hiển thị";
                return;
            }

            // Thêm dữ liệu vào chart (chỉ thêm loại có sinh viên)
            if (classification.ExcellentCount > 0)
            {
                var point = chart2.Series["Phân loại"].Points.Add((double)classification.ExcellentPercent);
                point.Label = $"{classification.ExcellentPercent:N1}%";
                point.LegendText = $"Giỏi ({classification.ExcellentCount})";
                point.Color = System.Drawing.Color.FromArgb(46, 204, 113); // Xanh lá
            }

            if (classification.GoodCount > 0)
            {
                var point = chart2.Series["Phân loại"].Points.Add((double)classification.GoodPercent);
                point.Label = $"{classification.GoodPercent:N1}%";
                point.LegendText = $"Khá ({classification.GoodCount})";
                point.Color = System.Drawing.Color.FromArgb(52, 152, 219); // Xanh dương
            }

            if (classification.AverageCount > 0)
            {
                var point = chart2.Series["Phân loại"].Points.Add((double)classification.AveragePercent);
                point.Label = $"{classification.AveragePercent:N1}%";
                point.LegendText = $"Trung bình ({classification.AverageCount})";
                point.Color = System.Drawing.Color.FromArgb(241, 196, 15); // Vàng
            }

            if (classification.WeakCount > 0)
            {
                var point = chart2.Series["Phân loại"].Points.Add((double)classification.WeakPercent);
                point.Label = $"{classification.WeakPercent:N1}%";
                point.LegendText = $"Yếu ({classification.WeakCount})";
                point.Color = System.Drawing.Color.FromArgb(231, 76, 60); // Đỏ
            }

            // Tùy chỉnh Chart
            chart2.Series["Phân loại"]["PieLabelStyle"] = "Outside";
            chart2.Series["Phân loại"]["PieLineColor"] = "Black";
            chart2.Series["Phân loại"].Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
        }

        /// <summary>
        /// Hiển thị thống kê lên label
        /// </summary>
        private void DisplayStatistics(StudentClassification classification)
        {
            lblTotalStudents.Text = $"Tổng sinh viên: {classification.TotalStudents}";
            lblExcellent.Text = $"🟢 Giỏi: {classification.ExcellentCount} ({classification.ExcellentPercent:N1}%)";
            lblGood.Text = $"🔵 Khá: {classification.GoodCount} ({classification.GoodPercent:N1}%)";
            lblAverage.Text = $"🟡 Trung bình: {classification.AverageCount} ({classification.AveragePercent:N1}%)";
            lblWeak.Text = $"🔴 Yếu: {classification.WeakCount} ({classification.WeakPercent:N1}%)";
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
    /// Model: Điểm TB của sinh viên
    /// </summary>
    public class StudentAverageScore
    {
        public int StudentID { get; set; }
        public string StudentName { get; set; } = string.Empty;
        public decimal AverageScore { get; set; }
        public int SubjectCount { get; set; }
    }

    /// <summary>
    /// Model: Kết quả phân loại sinh viên
    /// </summary>
    public class StudentClassification
    {
        public int TotalStudents { get; set; }
        public int ExcellentCount { get; set; } // Giỏi: >= 8.0
        public int GoodCount { get; set; } // Khá: 6.5 - 8.0
        public int AverageCount { get; set; } // TB: 5.0 - 6.5
        public int WeakCount { get; set; } // Yếu: < 5.0

        public decimal ExcellentPercent { get; set; }
        public decimal GoodPercent { get; set; }
        public decimal AveragePercent { get; set; }
        public decimal WeakPercent { get; set; }
    }
}
