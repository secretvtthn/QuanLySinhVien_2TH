using System;

namespace StudentManagement.Models
{
    /// <summary>
    /// Model: Score (Điểm)
    /// Mapping với bảng Scores trong database
    /// </summary>
    public class Score
    {
        public int ScoreID { get; set; }
        
        public int StudentID { get; set; }
        
        public string SubjectName { get; set; } = string.Empty;
        
        public decimal ScoreValue { get; set; }
        
        public DateTime? ScoreDate { get; set; }
        
        public int Semester { get; set; } = 1;
        
        public string AcademicYear { get; set; } = "2024-2025";
        
        public string? Notes { get; set; }

        public Score()
        {
            ScoreDate = DateTime.Now;
            Semester = 1;
            AcademicYear = "2024-2025";
        }

        public Score(int studentID, string subjectName, decimal scoreValue, int semester, string academicYear)
        {
            StudentID = studentID;
            SubjectName = subjectName;
            ScoreValue = scoreValue;
            Semester = semester;
            AcademicYear = academicYear;
            ScoreDate = DateTime.Now;
        }
    }

    /// <summary>
    /// Model: ScoreWithStudentInfo
    /// Dùng cho việc hiển thị điểm kèm thông tin sinh viên
    /// </summary>
    public class ScoreWithStudentInfo
    {
        public int ScoreID { get; set; }
        public int StudentID { get; set; }
        public string StudentName { get; set; } = string.Empty;
        public string SubjectName { get; set; } = string.Empty;
        public decimal Score { get; set; }
        public DateTime? ScoreDate { get; set; }
        public int Semester { get; set; }
        public string AcademicYear { get; set; } = "2024-2025";
        public string Notes { get; set; } = string.Empty;
    }
}