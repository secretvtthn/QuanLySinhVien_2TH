using System;

namespace StudentManagement.Models
{
    public class User
    {
        public int UserID { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public DateTime? CreatedDate { get; set; } // ✅ SỬA: Nullable DateTime
        public DateTime? UpdatedDate { get; set; } // ✅ SỬA: Nullable DateTime
        public bool IsActive { get; set; }

        public User()
        {
            CreatedDate = DateTime.Now;
            UpdatedDate = DateTime.Now;
            IsActive = true;
        }

        public User(string username, string password, string fullName, string role)
        {
            Username = username;
            Password = password;
            FullName = fullName;
            Role = role;
            CreatedDate = DateTime.Now;
            UpdatedDate = DateTime.Now;
            IsActive = true;
        }
    }
}