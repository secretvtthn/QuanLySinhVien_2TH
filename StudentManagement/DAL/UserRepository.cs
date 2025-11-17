using Microsoft.Data.SqlClient;
using StudentManagement.Helper;
using StudentManagement.Models;

namespace StudentManagement.DAL
{
    /// <summary>
    /// Repository cho User - Quản lý người dùng
    /// ✅ PROMPT 12: Quản lý tài khoản, phân quyền (Admin only)
    /// </summary>
    public class UserRepository
    {
        private readonly string? connectionString;

        public UserRepository()
        {
            connectionString = DatabaseHelper.ConnectionString;
        }

        #region GET Methods

        /// <summary>
        /// Lấy tất cả người dùng
        /// </summary>
        public List<User> GetAllUsers()
        {
            try
            {
                var users = new List<User>();

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = @"
                        SELECT UserID, Username, Password, FullName, Role, 
                               Email, Phone, CreatedDate, UpdatedDate, IsActive
                        FROM Users
                        ORDER BY UserID DESC";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            users.Add(MapReaderToUser(reader));
                        }
                    }
                }

                return users;
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi lấy danh sách người dùng: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Lấy người dùng theo ID
        /// </summary>
        public User? GetUserById(int userID)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = @"
                        SELECT UserID, Username, Password, FullName, Role, 
                               Email, Phone, CreatedDate, UpdatedDate, IsActive
                        FROM Users
                        WHERE UserID = @UserID";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@UserID", userID);
                        
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                return MapReaderToUser(reader);
                            }
                        }
                    }
                }

                return null;
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi lấy người dùng ID {userID}: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Lấy người dùng theo Username
        /// </summary>
        public User? GetUserByUsername(string username)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = @"
                        SELECT UserID, Username, Password, FullName, Role, 
                               Email, Phone, CreatedDate, UpdatedDate, IsActive
                        FROM Users
                        WHERE Username = @Username";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Username", username);
                        
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                return MapReaderToUser(reader);
                            }
                        }
                    }
                }

                return null;
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi tìm username '{username}': {ex.Message}", ex);
            }
        }

        /// <summary>
        /// LINQ: Lọc người dùng theo Role
        /// </summary>
        public List<User> GetUsersByRole(string role)
        {
            try
            {
                var allUsers = GetAllUsers();
                
                return allUsers
                    .Where(u => u.Role.Equals(role, StringComparison.OrdinalIgnoreCase))
                    .OrderBy(u => u.FullName)
                    .ToList();
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi lọc người dùng theo role: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// LINQ: Lấy người dùng đang hoạt động
        /// </summary>
        public List<User> GetActiveUsers()
        {
            try
            {
                var allUsers = GetAllUsers();
                
                return allUsers
                    .Where(u => u.IsActive)
                    .OrderBy(u => u.FullName)
                    .ToList();
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi lấy người dùng active: {ex.Message}", ex);
            }
        }

        #endregion

        #region CUD Methods (Create, Update, Delete)

        /// <summary>
        /// ✅ Thêm người dùng mới (check Username UNIQUE)
        /// </summary>
        public bool AddUser(User user)
        {
            try
            {
                // Validate dữ liệu
                ValidateUser(user);

                // ✅ Kiểm tra Username đã tồn tại chưa
                if (IsUsernameExists(user.Username))
                {
                    throw new InvalidOperationException($"Username '{user.Username}' đã tồn tại!");
                }

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = @"
                        INSERT INTO Users (Username, Password, FullName, Role, Email, Phone, CreatedDate, UpdatedDate, IsActive)
                        VALUES (@Username, @Password, @FullName, @Role, @Email, @Phone, GETDATE(), GETDATE(), @IsActive)";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Username", user.Username);
                        cmd.Parameters.AddWithValue("@Password", user.Password); // ⚠️ Nên hash password trước khi lưu
                        cmd.Parameters.AddWithValue("@FullName", user.FullName);
                        cmd.Parameters.AddWithValue("@Role", user.Role);
                        cmd.Parameters.AddWithValue("@Email", 
                            string.IsNullOrWhiteSpace(user.Email) ? DBNull.Value : user.Email);
                        cmd.Parameters.AddWithValue("@Phone", 
                            string.IsNullOrWhiteSpace(user.Phone) ? DBNull.Value : user.Phone);
                        cmd.Parameters.AddWithValue("@IsActive", user.IsActive);

                        int rowsAffected = cmd.ExecuteNonQuery();
                        return rowsAffected > 0;
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                throw new Exception($"Lỗi SQL khi thêm người dùng: {sqlEx.Message}", sqlEx);
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi thêm người dùng: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// ✅ Cập nhật thông tin người dùng (KHÔNG update Password)
        /// </summary>
        public bool UpdateUser(User user)
        {
            try
            {
                // Validate dữ liệu
                ValidateUser(user);

                // Kiểm tra người dùng có tồn tại không
                var existingUser = GetUserById(user.UserID);
                if (existingUser == null)
                {
                    throw new InvalidOperationException($"Không tìm thấy người dùng ID {user.UserID}!");
                }

                // ✅ Kiểm tra Username UNIQUE (ngoại trừ chính nó)
                var userWithSameUsername = GetUserByUsername(user.Username);
                if (userWithSameUsername != null && userWithSameUsername.UserID != user.UserID)
                {
                    throw new InvalidOperationException($"Username '{user.Username}' đã được sử dụng bởi người khác!");
                }

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    // ✅ KHÔNG update Password (dùng ChangePassword riêng)
                    string query = @"
                        UPDATE Users
                        SET Username = @Username,
                            FullName = @FullName,
                            Role = @Role,
                            Email = @Email,
                            Phone = @Phone,
                            IsActive = @IsActive,
                            UpdatedDate = GETDATE()
                        WHERE UserID = @UserID";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@UserID", user.UserID);
                        cmd.Parameters.AddWithValue("@Username", user.Username);
                        cmd.Parameters.AddWithValue("@FullName", user.FullName);
                        cmd.Parameters.AddWithValue("@Role", user.Role);
                        cmd.Parameters.AddWithValue("@Email", 
                            string.IsNullOrWhiteSpace(user.Email) ? DBNull.Value : user.Email);
                        cmd.Parameters.AddWithValue("@Phone", 
                            string.IsNullOrWhiteSpace(user.Phone) ? DBNull.Value : user.Phone);
                        cmd.Parameters.AddWithValue("@IsActive", user.IsActive);

                        int rowsAffected = cmd.ExecuteNonQuery();
                        return rowsAffected > 0;
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                throw new Exception($"Lỗi SQL khi cập nhật người dùng: {sqlEx.Message}", sqlEx);
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi cập nhật người dùng: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// ✅ Xóa người dùng (không xóa được tài khoản đang đăng nhập)
        /// </summary>
        public bool DeleteUser(int userID)
        {
            try
            {
                if (userID <= 0)
                {
                    throw new ArgumentException("UserID không hợp lệ!");
                }

                // ✅ PROMPT 12: Không xóa được tài khoản đang đăng nhập
                if (SessionUser.UserID == userID)
                {
                    throw new InvalidOperationException("Không thể xóa tài khoản đang đăng nhập!");
                }

                // Kiểm tra người dùng có tồn tại không
                var existingUser = GetUserById(userID);
                if (existingUser == null)
                {
                    throw new InvalidOperationException($"Không tìm thấy người dùng ID {userID}!");
                }

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "DELETE FROM Users WHERE UserID = @UserID";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@UserID", userID);
                        int rowsAffected = cmd.ExecuteNonQuery();
                        return rowsAffected > 0;
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                throw new Exception($"Lỗi SQL khi xóa người dùng: {sqlEx.Message}", sqlEx);
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi xóa người dùng: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// ✅ Đổi mật khẩu (yêu cầu mật khẩu cũ)
        /// </summary>
        public bool ChangePassword(int userID, string oldPassword, string newPassword)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(oldPassword))
                {
                    throw new ArgumentException("Mật khẩu cũ không được để trống!");
                }

                if (string.IsNullOrWhiteSpace(newPassword))
                {
                    throw new ArgumentException("Mật khẩu mới không được để trống!");
                }

                if (newPassword.Length < 6)
                {
                    throw new ArgumentException("Mật khẩu mới phải có ít nhất 6 ký tự!");
                }

                // Lấy thông tin người dùng
                var user = GetUserById(userID);
                if (user == null)
                {
                    throw new InvalidOperationException($"Không tìm thấy người dùng ID {userID}!");
                }

                // ✅ Kiểm tra mật khẩu cũ có đúng không
                if (user.Password != oldPassword) // ⚠️ Nên compare hash password
                {
                    throw new InvalidOperationException("Mật khẩu cũ không đúng!");
                }

                // ✅ Mật khẩu mới không được trùng mật khẩu cũ
                if (oldPassword == newPassword)
                {
                    throw new InvalidOperationException("Mật khẩu mới không được trùng mật khẩu cũ!");
                }

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = @"
                        UPDATE Users
                        SET Password = @NewPassword,
                            UpdatedDate = GETDATE()
                        WHERE UserID = @UserID";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@UserID", userID);
                        cmd.Parameters.AddWithValue("@NewPassword", newPassword); // ⚠️ Nên hash password

                        int rowsAffected = cmd.ExecuteNonQuery();
                        return rowsAffected > 0;
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                throw new Exception($"Lỗi SQL khi đổi mật khẩu: {sqlEx.Message}", sqlEx);
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi đổi mật khẩu: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// ✅ PROMPT 19: Cập nhật mật khẩu (không cần kiểm tra mật khẩu cũ - đã kiểm tra ở form)
        /// </summary>
        public bool UpdatePassword(int userID, string newPassword)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(newPassword))
                {
                    throw new ArgumentException("Mật khẩu mới không được để trống!");
                }

                if (newPassword.Length < 6)
                {
                    throw new ArgumentException("Mật khẩu mới phải có ít nhất 6 ký tự!");
                }

                // Kiểm tra người dùng có tồn tại không
                var user = GetUserById(userID);
                if (user == null)
                {
                    throw new InvalidOperationException($"Không tìm thấy người dùng ID {userID}!");
                }

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = @"
                        UPDATE Users
                        SET Password = @NewPassword,
                            UpdatedDate = GETDATE()
                        WHERE UserID = @UserID";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@UserID", userID);
                        cmd.Parameters.AddWithValue("@NewPassword", newPassword);

                        int rowsAffected = cmd.ExecuteNonQuery();
                        return rowsAffected > 0;
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                throw new Exception($"Lỗi SQL khi cập nhật mật khẩu: {sqlEx.Message}", sqlEx);
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi cập nhật mật khẩu: {ex.Message}", ex);
            }
        }

        #endregion

        #region Helper Methods

        /// <summary>
        /// Kiểm tra Username đã tồn tại chưa
        /// </summary>
        private bool IsUsernameExists(string username)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "SELECT COUNT(*) FROM Users WHERE Username = @Username";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Username", username);
                        int count = (int)cmd.ExecuteScalar();
                        return count > 0;
                    }
                }
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Map SqlDataReader sang User object
        /// </summary>
        private User MapReaderToUser(SqlDataReader reader)
        {
            return new User
            {
                UserID = reader.GetInt32(reader.GetOrdinal("UserID")),
                Username = reader.GetString(reader.GetOrdinal("Username")),
                Password = reader.GetString(reader.GetOrdinal("Password")),
                FullName = reader.GetString(reader.GetOrdinal("FullName")),
                Role = reader.GetString(reader.GetOrdinal("Role")),
                Email = reader.IsDBNull(reader.GetOrdinal("Email")) 
                    ? null 
                    : reader.GetString(reader.GetOrdinal("Email")),
                Phone = reader.IsDBNull(reader.GetOrdinal("Phone")) 
                    ? null 
                    : reader.GetString(reader.GetOrdinal("Phone")),
                CreatedDate = reader.IsDBNull(reader.GetOrdinal("CreatedDate")) 
                    ? null 
                    : reader.GetDateTime(reader.GetOrdinal("CreatedDate")),
                UpdatedDate = reader.IsDBNull(reader.GetOrdinal("UpdatedDate")) 
                    ? null 
                    : reader.GetDateTime(reader.GetOrdinal("UpdatedDate")),
                IsActive = reader.GetBoolean(reader.GetOrdinal("IsActive"))
            };
        }

        /// <summary>
        /// Validate User object
        /// </summary>
        private void ValidateUser(User user)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user), "User object không được null!");

            if (string.IsNullOrWhiteSpace(user.Username))
                throw new ArgumentException("Username không được để trống!");

            if (user.Username.Length < 3)
                throw new ArgumentException("Username phải có ít nhất 3 ký tự!");

            if (string.IsNullOrWhiteSpace(user.FullName))
                throw new ArgumentException("Họ tên không được để trống!");

            if (string.IsNullOrWhiteSpace(user.Role))
                throw new ArgumentException("Role không được để trống!");

            // ✅ Kiểm tra Role hợp lệ
            if (user.Role != "Admin" && user.Role != "GiaoVien" && user.Role != "SinhVien")
                throw new ArgumentException("Role phải là: Admin, GiaoVien hoặc SinhVien!");

            // Validate Email (nếu có)
            if (!string.IsNullOrWhiteSpace(user.Email))
            {
                if (!user.Email.Contains("@"))
                    throw new ArgumentException("Email không hợp lệ!");
            }

            // Validate Phone (nếu có)
            if (!string.IsNullOrWhiteSpace(user.Phone))
            {
                if (user.Phone.Length < 10 || user.Phone.Length > 11)
                    throw new ArgumentException("Số điện thoại phải có 10-11 chữ số!");
            }
        }

        #endregion
    }
}
