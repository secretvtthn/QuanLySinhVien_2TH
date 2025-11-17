using StudentManagement.Models;

namespace StudentManagement.Helper
{
    /// <summary>
    /// Class lưu thông tin phiên đăng nhập của user
    /// </summary>
    public static class SessionUser
    {
        public static int UserID { get; set; }
        public static string Username { get; set; } = string.Empty;
        public static string FullName { get; set; } = string.Empty;
        public static string Role { get; set; } = string.Empty;
        public static string Email { get; set; } = string.Empty;
        public static bool IsActive { get; set; }
        
        // ✅ PROMPT 19: Thêm CurrentUser để truy cập thông tin đầy đủ
        public static User? CurrentUser { get; private set; }

        /// <summary>
        /// Lưu thông tin user vào session
        /// </summary>
        public static void SetUser(User user)
        {
            UserID = user.UserID;
            Username = user.Username;
            FullName = user.FullName;
            Role = user.Role;
            Email = user.Email ?? string.Empty;
            IsActive = user.IsActive;
            CurrentUser = user; // ✅ Lưu object User
        }

        /// <summary>
        /// ✅ PROMPT 19: Phương thức Login (alias cho SetUser)
        /// </summary>
        public static void Login(User user)
        {
            SetUser(user);
        }

        /// <summary>
        /// Xóa thông tin session (logout)
        /// </summary>
        public static void ClearSession()
        {
            UserID = 0;
            Username = string.Empty;
            FullName = string.Empty;
            Role = string.Empty;
            Email = string.Empty;
            IsActive = false;
            CurrentUser = null; // ✅ Clear CurrentUser
        }

        /// <summary>
        /// ✅ PROMPT 19: Phương thức Logout (alias cho ClearSession)
        /// </summary>
        public static void Logout()
        {
            ClearSession();
        }

        /// <summary>
        /// Kiểm tra đã đăng nhập chưa
        /// </summary>
        public static bool IsLoggedIn()
        {
            return UserID > 0 && !string.IsNullOrEmpty(Username);
        }
    }
}