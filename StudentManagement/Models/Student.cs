using System;

namespace StudentManagement.Models
{
    public class Student
    {
        public int StudentID { get; set; }
        
        public string StudentCode { get; set; } = string.Empty;
        
        public string StudentName { get; set; } = string.Empty;
        
        public int ClassID { get; set; }
        
        public DateTime? DateOfBirth { get; set; }
        
        public string? Gender { get; set; }
        
        public string? Email { get; set; }
        
        public string? Phone { get; set; }
        
        public string? Address { get; set; }
        
        public DateTime EnrollmentDate { get; set; }
        
        public bool IsActive { get; set; }

        public Student()
        {
            EnrollmentDate = DateTime.Now;
            IsActive = true;
        }

        public Student(string studentCode, string studentName, int classID)
        {
            StudentCode = studentCode;
            StudentName = studentName;
            ClassID = classID;
            EnrollmentDate = DateTime.Now;
            IsActive = true;
        }
    }
}