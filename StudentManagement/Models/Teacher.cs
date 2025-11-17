using System;

namespace StudentManagement.Models
{
    public class Teacher
    {
        public int TeacherID { get; set; }
        public string TeacherCode { get; set; } = string.Empty;
        public string TeacherName { get; set; } = string.Empty;
        public string? Email { get; set; }
        public string? Department { get; set; }  // ✅ THÊM
        public string? Phone { get; set; }
        public string? Address { get; set; }  // ✅ THÊM
        public string? Subject { get; set; }
        public DateTime HireDate { get; set; }
        public bool IsActive { get; set; }

        public Teacher()
        {
            HireDate = DateTime.Now;
            IsActive = true;
        }

        public Teacher(string teacherCode, string teacherName)
        {
            TeacherCode = teacherCode;
            TeacherName = teacherName;
            HireDate = DateTime.Now;
            IsActive = true;
        }
    }
}