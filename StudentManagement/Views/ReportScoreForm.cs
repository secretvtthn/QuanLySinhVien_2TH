using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using StudentManagement.DAL;
using StudentManagement.Models;
using ClosedXML.Excel;

namespace StudentManagement.Views
{
    /// <summary>
    /// ✅ PROMPT 14: Form Báo cáo Điểm - Lọc theo Lớp/Môn/Khoảng điểm/Học kỳ/Năm học
    /// </summary>
    public partial class ReportScoreForm : Form
    {
        private ScoreRepository scoreRepository;
        private ClassRepository classRepository;
        private List<ScoreReportItem> currentReportData;

        public ReportScoreForm()
        {
            InitializeComponent();
            scoreRepository = new ScoreRepository();
            classRepository = new ClassRepository();
            currentReportData = new List<ScoreReportItem>();
        }

        private void ReportScoreForm_Load(object sender, EventArgs e)
        {
            InitializeForm();
            LoadFilters();
            LoadAllScores();
        }

        private void InitializeForm()
        {
            // Cấu hình DataGridView
            dgvReport.AutoGenerateColumns = false;
            dgvReport.AllowUserToAddRows = false;
            dgvReport.AllowUserToDeleteRows = false;
            dgvReport.ReadOnly = true;
            dgvReport.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvReport.MultiSelect = false;

            // Thêm các cột
            dgvReport.Columns.Clear();
            dgvReport.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "STT",
                HeaderText = "STT",
                Width = 50,
                DataPropertyName = "STT"
            });
            dgvReport.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "StudentCode",
                HeaderText = "Mã SV",
                Width = 100,
                DataPropertyName = "StudentCode"
            });
            dgvReport.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "StudentName",
                HeaderText = "Tên Sinh Viên",
                Width = 200,
                DataPropertyName = "StudentName"
            });
            dgvReport.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "ClassName",
                HeaderText = "Lớp",
                Width = 100,
                DataPropertyName = "ClassName"
            });
            dgvReport.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "SubjectName",
                HeaderText = "Môn Học",
                Width = 150,
                DataPropertyName = "SubjectName"
            });
            dgvReport.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Score",
                HeaderText = "Điểm",
                Width = 80,
                DataPropertyName = "Score",
                DefaultCellStyle = new DataGridViewCellStyle { Format = "0.00" }
            });
            dgvReport.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Semester",
                HeaderText = "Học Kỳ",
                Width = 80,
                DataPropertyName = "Semester"
            });
            dgvReport.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "AcademicYear",
                HeaderText = "Năm Học",
                Width = 100,
                DataPropertyName = "AcademicYear"
            });
            dgvReport.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Grade",
                HeaderText = "Xếp Loại",
                Width = 100,
                DataPropertyName = "Grade"
            });

            // Cấu hình ComboBox Semester
            cmbSemester.Items.Clear();
            cmbSemester.Items.Add("Tất cả");
            cmbSemester.Items.Add("Học kỳ 1");
            cmbSemester.Items.Add("Học kỳ 2");
            cmbSemester.SelectedIndex = 0;

            // Set năm học mặc định
            txtAcademicYear.Text = "2024-2025";

            // Set khoảng điểm mặc định
            txtScoreFrom.Text = "0";
            txtScoreTo.Text = "10";
        }

        private void LoadFilters()
        {
            try
            {
                // Load danh sách lớp
                var classes = classRepository.GetAllClasses();
                cmbClass.Items.Clear();
                cmbClass.Items.Add("Tất cả");
                foreach (var cls in classes)
                {
                    cmbClass.Items.Add(cls.ClassName);
                }
                cmbClass.SelectedIndex = 0;

                // ✅ LINQ: Lấy danh sách môn học duy nhất
                var allScores = scoreRepository.GetAllScores();
                var subjects = allScores
                    .Select(s => s.SubjectName)
                    .Distinct()
                    .OrderBy(s => s)
                    .ToList();

                cmbSubject.Items.Clear();
                cmbSubject.Items.Add("Tất cả");
                foreach (var subject in subjects)
                {
                    cmbSubject.Items.Add(subject);
                }
                cmbSubject.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi load bộ lọc: {ex.Message}",
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadAllScores()
        {
            try
            {
                var allScores = scoreRepository.GetAllScores();
                var students = new StudentRepository().GetAllStudents();
                var classes = classRepository.GetAllClasses();

                // ✅ LINQ: Join để lấy thông tin đầy đủ
                var reportData = (from score in allScores
                                  join student in students on score.StudentID equals student.StudentID
                                  join cls in classes on student.ClassID equals cls.ClassID
                                  select new ScoreReportItem
                                  {
                                      StudentID = student.StudentID,
                                      StudentCode = student.StudentCode,
                                      StudentName = student.StudentName,
                                      ClassName = cls.ClassName,
                                      SubjectName = score.SubjectName,
                                      Score = score.Score,
                                      Semester = score.Semester,
                                      AcademicYear = score.AcademicYear,
                                      Grade = GetGrade(score.Score)
                                  }).ToList();

                currentReportData = reportData;
                DisplayReport(reportData);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi load dữ liệu: {ex.Message}",
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnFilter_Click(object sender, EventArgs e)
        {
            try
            {
                // Lấy tất cả dữ liệu
                var allScores = scoreRepository.GetAllScores();
                var students = new StudentRepository().GetAllStudents();
                var classes = classRepository.GetAllClasses();

                // ✅ LINQ: Join để lấy thông tin đầy đủ
                var reportData = (from score in allScores
                                  join student in students on score.StudentID equals student.StudentID
                                  join cls in classes on student.ClassID equals cls.ClassID
                                  select new ScoreReportItem
                                  {
                                      StudentID = student.StudentID,
                                      StudentCode = student.StudentCode,
                                      StudentName = student.StudentName,
                                      ClassName = cls.ClassName,
                                      SubjectName = score.SubjectName,
                                      Score = score.Score,
                                      Semester = score.Semester,
                                      AcademicYear = score.AcademicYear,
                                      Grade = GetGrade(score.Score)
                                  }).AsQueryable();

                // ✅ LINQ: Lọc theo lớp
                if (cmbClass.SelectedIndex > 0 && cmbClass.SelectedItem != null)
                {
                    string selectedClass = cmbClass.SelectedItem.ToString() ?? string.Empty;
                    reportData = reportData.Where(r => r.ClassName == selectedClass);
                }

                // ✅ LINQ: Lọc theo môn học
                if (cmbSubject.SelectedIndex > 0 && cmbSubject.SelectedItem != null)
                {
                    string selectedSubject = cmbSubject.SelectedItem.ToString() ?? string.Empty;
                    reportData = reportData.Where(r => r.SubjectName == selectedSubject);
                }

                // ✅ LINQ: Lọc theo khoảng điểm
                if (decimal.TryParse(txtScoreFrom.Text, out decimal scoreFrom))
                {
                    reportData = reportData.Where(r => r.Score >= scoreFrom);
                }
                if (decimal.TryParse(txtScoreTo.Text, out decimal scoreTo))
                {
                    reportData = reportData.Where(r => r.Score <= scoreTo);
                }

                // ✅ LINQ: Lọc theo học kỳ
                if (cmbSemester.SelectedIndex > 0)
                {
                    int semester = cmbSemester.SelectedIndex; // 1 hoặc 2
                    reportData = reportData.Where(r => r.Semester == semester);
                }

                // ✅ LINQ: Lọc theo năm học
                if (!string.IsNullOrWhiteSpace(txtAcademicYear.Text))
                {
                    string academicYear = txtAcademicYear.Text.Trim();
                    reportData = reportData.Where(r => r.AcademicYear == academicYear);
                }

                // ✅ LINQ: Sắp xếp
                var finalData = reportData
                    .OrderBy(r => r.ClassName)
                    .ThenBy(r => r.StudentName)
                    .ThenBy(r => r.SubjectName)
                    .ToList();

                currentReportData = finalData;
                DisplayReport(finalData);

                // Hiển thị thông báo
                lblStatus.Text = $"✅ Tìm thấy {finalData.Count} kết quả";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi lọc dữ liệu: {ex.Message}",
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DisplayReport(List<ScoreReportItem> reportData)
        {
            try
            {
                // Hiển thị dữ liệu
                dgvReport.DataSource = null;
                
                // Thêm STT
                for (int i = 0; i < reportData.Count; i++)
                {
                    reportData[i].STT = i + 1;
                }

                dgvReport.DataSource = reportData;

                // ✅ LINQ: Tính thống kê
                if (reportData.Count > 0)
                {
                    var stats = CalculateStatistics(reportData);
                    DisplayStatistics(stats);
                }
                else
                {
                    ClearStatistics();
                }

                lblTotalRecords.Text = $"Tổng số: {reportData.Count} bản ghi";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi hiển thị báo cáo: {ex.Message}",
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// ✅ LINQ: Tính toán thống kê chi tiết
        /// </summary>
        private ScoreStatistics CalculateStatistics(List<ScoreReportItem> data)
        {
            var stats = new ScoreStatistics();

            if (data.Count == 0)
                return stats;

            // ✅ LINQ: Average, Max, Min
            stats.AverageScore = data.Average(s => s.Score);
            stats.MaxScore = data.Max(s => s.Score);
            stats.MinScore = data.Min(s => s.Score);
            stats.TotalRecords = data.Count;

            // ✅ LINQ: Count theo xếp loại
            stats.ExcellentCount = data.Count(s => s.Score >= 9.0m);
            stats.GoodCount = data.Count(s => s.Score >= 8.0m && s.Score < 9.0m);
            stats.FairCount = data.Count(s => s.Score >= 6.5m && s.Score < 8.0m);
            stats.AverageCount = data.Count(s => s.Score >= 5.0m && s.Score < 6.5m);
            stats.PoorCount = data.Count(s => s.Score < 5.0m);

            return stats;
        }

        private void DisplayStatistics(ScoreStatistics stats)
        {
            lblAverage.Text = $"Điểm TB: {stats.AverageScore:0.00}";
            lblMax.Text = $"Cao nhất: {stats.MaxScore:0.00}";
            lblMin.Text = $"Thấp nhất: {stats.MinScore:0.00}";
            
            lblExcellent.Text = $"Giỏi (≥9.0): {stats.ExcellentCount}";
            lblGood.Text = $"Khá (8.0-8.9): {stats.GoodCount}";
            lblFair.Text = $"TB (6.5-7.9): {stats.FairCount}";
            lblAvgLevel.Text = $"TB Yếu (5.0-6.4): {stats.AverageCount}";
            lblPoor.Text = $"Yếu (<5.0): {stats.PoorCount}";

            // Tính tỷ lệ phần trăm
            if (stats.TotalRecords > 0)
            {
                lblExcellentPercent.Text = $"({(stats.ExcellentCount * 100.0 / stats.TotalRecords):0.0}%)";
                lblGoodPercent.Text = $"({(stats.GoodCount * 100.0 / stats.TotalRecords):0.0}%)";
                lblFairPercent.Text = $"({(stats.FairCount * 100.0 / stats.TotalRecords):0.0}%)";
                lblAvgPercent.Text = $"({(stats.AverageCount * 100.0 / stats.TotalRecords):0.0}%)";
                lblPoorPercent.Text = $"({(stats.PoorCount * 100.0 / stats.TotalRecords):0.0}%)";
            }
        }

        private void ClearStatistics()
        {
            lblAverage.Text = "Điểm TB: --";
            lblMax.Text = "Cao nhất: --";
            lblMin.Text = "Thấp nhất: --";
            lblExcellent.Text = "Giỏi: 0";
            lblGood.Text = "Khá: 0";
            lblFair.Text = "TB: 0";
            lblAvgLevel.Text = "TB Yếu: 0";
            lblPoor.Text = "Yếu: 0";
            lblExcellentPercent.Text = "(0%)";
            lblGoodPercent.Text = "(0%)";
            lblFairPercent.Text = "(0%)";
            lblAvgPercent.Text = "(0%)";
            lblPoorPercent.Text = "(0%)";
        }

        private string GetGrade(decimal score)
        {
            if (score >= 9.0m) return "Xuất sắc";
            if (score >= 8.0m) return "Giỏi";
            if (score >= 6.5m) return "Khá";
            if (score >= 5.0m) return "Trung bình";
            if (score >= 3.5m) return "Yếu";
            return "Kém";
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            try
            {
                if (currentReportData.Count == 0)
                {
                    MessageBox.Show("Không có dữ liệu để xuất!",
                        "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                using (SaveFileDialog sfd = new SaveFileDialog())
                {
                    sfd.Filter = "Excel Files|*.xlsx";
                    sfd.Title = "Xuất báo cáo điểm";
                    sfd.FileName = $"BaoCaoDiem_{DateTime.Now:yyyyMMdd_HHmmss}.xlsx";

                    if (sfd.ShowDialog() == DialogResult.OK)
                    {
                        ExportToExcel(sfd.FileName);
                        MessageBox.Show($"✅ Xuất báo cáo thành công!\nFile: {sfd.FileName}",
                            "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        // Mở file
                        System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
                        {
                            FileName = sfd.FileName,
                            UseShellExecute = true
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi xuất Excel: {ex.Message}",
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ExportToExcel(string filePath)
        {
            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Báo Cáo Điểm");

                // Tiêu đề
                worksheet.Cell(1, 1).Value = "BÁO CÁO ĐIỂM SINH VIÊN";
                worksheet.Range(1, 1, 1, 9).Merge();
                worksheet.Cell(1, 1).Style.Font.Bold = true;
                worksheet.Cell(1, 1).Style.Font.FontSize = 16;
                worksheet.Cell(1, 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                // Thông tin lọc
                int currentRow = 2;
                worksheet.Cell(currentRow, 1).Value = $"Ngày xuất: {DateTime.Now:dd/MM/yyyy HH:mm:ss}";
                currentRow++;

                if (cmbClass.SelectedIndex > 0)
                    worksheet.Cell(currentRow++, 1).Value = $"Lớp: {cmbClass.SelectedItem}";
                if (cmbSubject.SelectedIndex > 0)
                    worksheet.Cell(currentRow++, 1).Value = $"Môn: {cmbSubject.SelectedItem}";
                if (cmbSemester.SelectedIndex > 0)
                    worksheet.Cell(currentRow++, 1).Value = $"Học kỳ: {cmbSemester.SelectedItem}";
                if (!string.IsNullOrWhiteSpace(txtAcademicYear.Text))
                    worksheet.Cell(currentRow++, 1).Value = $"Năm học: {txtAcademicYear.Text}";

                currentRow++;

                // Header
                var headerRow = currentRow;
                worksheet.Cell(headerRow, 1).Value = "STT";
                worksheet.Cell(headerRow, 2).Value = "Mã SV";
                worksheet.Cell(headerRow, 3).Value = "Họ Tên";
                worksheet.Cell(headerRow, 4).Value = "Lớp";
                worksheet.Cell(headerRow, 5).Value = "Môn Học";
                worksheet.Cell(headerRow, 6).Value = "Điểm";
                worksheet.Cell(headerRow, 7).Value = "Học Kỳ";
                worksheet.Cell(headerRow, 8).Value = "Năm Học";
                worksheet.Cell(headerRow, 9).Value = "Xếp Loại";

                // Style header
                var headerRange = worksheet.Range(headerRow, 1, headerRow, 9);
                headerRange.Style.Font.Bold = true;
                headerRange.Style.Fill.BackgroundColor = XLColor.LightBlue;
                headerRange.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                // Dữ liệu
                currentRow++;
                for (int i = 0; i < currentReportData.Count; i++)
                {
                    var item = currentReportData[i];
                    worksheet.Cell(currentRow, 1).Value = i + 1;
                    worksheet.Cell(currentRow, 2).Value = item.StudentCode;
                    worksheet.Cell(currentRow, 3).Value = item.StudentName;
                    worksheet.Cell(currentRow, 4).Value = item.ClassName;
                    worksheet.Cell(currentRow, 5).Value = item.SubjectName;
                    worksheet.Cell(currentRow, 6).Value = item.Score;
                    worksheet.Cell(currentRow, 7).Value = item.Semester;
                    worksheet.Cell(currentRow, 8).Value = item.AcademicYear;
                    worksheet.Cell(currentRow, 9).Value = item.Grade;
                    currentRow++;
                }

                // Thống kê
                currentRow++;
                var stats = CalculateStatistics(currentReportData);
                worksheet.Cell(currentRow, 1).Value = "THỐNG KÊ TỔNG KẾT";
                worksheet.Cell(currentRow, 1).Style.Font.Bold = true;
                currentRow++;

                worksheet.Cell(currentRow, 1).Value = $"Điểm trung bình: {stats.AverageScore:0.00}";
                currentRow++;
                worksheet.Cell(currentRow, 1).Value = $"Điểm cao nhất: {stats.MaxScore:0.00}";
                currentRow++;
                worksheet.Cell(currentRow, 1).Value = $"Điểm thấp nhất: {stats.MinScore:0.00}";
                currentRow++;
                worksheet.Cell(currentRow, 1).Value = $"Giỏi (≥9.0): {stats.ExcellentCount} ({(stats.ExcellentCount * 100.0 / stats.TotalRecords):0.0}%)";
                currentRow++;
                worksheet.Cell(currentRow, 1).Value = $"Khá (8.0-8.9): {stats.GoodCount} ({(stats.GoodCount * 100.0 / stats.TotalRecords):0.0}%)";
                currentRow++;
                worksheet.Cell(currentRow, 1).Value = $"Trung bình (6.5-7.9): {stats.FairCount} ({(stats.FairCount * 100.0 / stats.TotalRecords):0.0}%)";
                currentRow++;
                worksheet.Cell(currentRow, 1).Value = $"TB Yếu (5.0-6.4): {stats.AverageCount} ({(stats.AverageCount * 100.0 / stats.TotalRecords):0.0}%)";
                currentRow++;
                worksheet.Cell(currentRow, 1).Value = $"Yếu (<5.0): {stats.PoorCount} ({(stats.PoorCount * 100.0 / stats.TotalRecords):0.0}%)";

                // Auto-fit columns
                worksheet.Columns().AdjustToContents();

                workbook.SaveAs(filePath);
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Chức năng in đang được phát triển!",
                "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            cmbClass.SelectedIndex = 0;
            cmbSubject.SelectedIndex = 0;
            cmbSemester.SelectedIndex = 0;
            txtAcademicYear.Text = "2024-2025";
            txtScoreFrom.Text = "0";
            txtScoreTo.Text = "10";
            lblStatus.Text = "";
            LoadAllScores();
        }
    }

    #region Helper Classes

    /// <summary>
    /// Model cho báo cáo điểm
    /// </summary>
    public class ScoreReportItem
    {
        public int STT { get; set; }
        public int StudentID { get; set; }
        public string StudentCode { get; set; } = string.Empty;
        public string StudentName { get; set; } = string.Empty;
        public string ClassName { get; set; } = string.Empty;
        public string SubjectName { get; set; } = string.Empty;
        public decimal Score { get; set; }
        public int Semester { get; set; }
        public string AcademicYear { get; set; } = string.Empty;
        public string Grade { get; set; } = string.Empty;
    }

    /// <summary>
    /// Model cho thống kê
    /// </summary>
    public class ScoreStatistics
    {
        public decimal AverageScore { get; set; }
        public decimal MaxScore { get; set; }
        public decimal MinScore { get; set; }
        public int TotalRecords { get; set; }
        public int ExcellentCount { get; set; }
        public int GoodCount { get; set; }
        public int FairCount { get; set; }
        public int AverageCount { get; set; }
        public int PoorCount { get; set; }
    }

    #endregion
}
