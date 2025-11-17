using System;

namespace StudentManagement.Models
{
    public class Class
    {
        public int ClassID { get; set; }
        
        public string ClassName { get; set; } = string.Empty;
        
        public int? TeacherID { get; set; }
        
        public int Capacity { get; set; }
        
        public string? Room { get; set; }
        
        public DateTime CreatedDate { get; set; }
        
        public DateTime UpdatedDate { get; set; }

        public Class()
        {
            Capacity = 30;
            CreatedDate = DateTime.Now;
            UpdatedDate = DateTime.Now;
        }

        public Class(string className, int? teacherID, string? room)
        {
            ClassName = className;
            TeacherID = teacherID;
            Room = room;
            Capacity = 30;
            CreatedDate = DateTime.Now;
            UpdatedDate = DateTime.Now;
        }
    }
}