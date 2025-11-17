using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Data.SqlClient;
using StudentManagement.Helper;
using StudentManagement.Models;

namespace StudentManagement.DAL
{
    /// <summary>
    /// Repository cho Teacher - Sử dụng LINQ to Objects
    /// ✅ PROMPT 9: Quản lý giáo viên với kiểm tra lớp phụ trách
    /// </summary>
    public class TeacherRepository
    {
        private readonly string? connectionString;

        public TeacherRepository()
        {
            connectionString = DatabaseHelper.ConnectionString;
        }

        #region GET Methods (LINQ to Objects)

        /// <summary>
        /// ✅ LINQ: Lấy tất cả giáo viên
        /// </summary>
        public List<Teacher> GetAllTeachers()
        {
            try
            {
                var teachers = LoadTeachersFromDatabase();
                
                // ✅ LINQ: OrderBy
                return teachers
                    .OrderBy(t => t.TeacherName)
                    .ToList();
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi lấy danh sách giáo viên: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// ✅ LINQ: Lấy giáo viên theo ID
        /// </summary>
        public Teacher? GetTeacherById(int teacherId)
        {
            try
            {
                var teachers = LoadTeachersFromDatabase();
                
                // ✅ LINQ: FirstOrDefault
                return teachers.FirstOrDefault(t => t.TeacherID == teacherId);
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi lấy giáo viên ID {teacherId}: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// ✅ LINQ: Lấy giáo viên theo lớp
        /// </summary>
        public Teacher? GetTeacherByClass(int classId)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = @"
                        SELECT t.TeacherID, t.TeacherName, t.Email, t.Department, 
                               t.Phone, t.Address, t.HireDate, t.IsActive
                        FROM Teachers t
                        INNER JOIN Classes c ON t.TeacherID = c.TeacherID
                        WHERE c.ClassID = @ClassID";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@ClassID", classId);
                        
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                return MapReaderToTeacher(reader);
                            }
                        }
                    }
                }
                
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi lấy giáo viên của lớp {classId}: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// ✅ LINQ: Lấy giáo viên đang hoạt động
        /// </summary>
        public List<Teacher> GetActiveTeachers()
        {
            try
            {
                var teachers = LoadTeachersFromDatabase();
                
                // ✅ LINQ: Where + OrderBy
                return teachers
                    .Where(t => t.IsActive)
                    .OrderBy(t => t.TeacherName)
                    .ToList();
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi lấy giáo viên đang hoạt động: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// ✅ LINQ: Tìm kiếm giáo viên
        /// </summary>
        public List<Teacher> SearchTeacher(string keyword)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(keyword))
                {
                    return GetAllTeachers();
                }

                var teachers = LoadTeachersFromDatabase();

                // ✅ LINQ: Where với Contains
                return teachers
                    .Where(t =>
                        t.TeacherName.Contains(keyword, StringComparison.OrdinalIgnoreCase) ||
                        (t.Email != null && t.Email.Contains(keyword, StringComparison.OrdinalIgnoreCase)) ||
                        (t.Department != null && t.Department.Contains(keyword, StringComparison.OrdinalIgnoreCase)) ||
                        (t.Phone != null && t.Phone.Contains(keyword, StringComparison.OrdinalIgnoreCase))
                    )
                    .OrderBy(t => t.TeacherName)
                    .ToList();
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi tìm kiếm giáo viên: {ex.Message}", ex);
            }
        }

        #endregion

        #region CUD Methods (Create, Update, Delete)

        /// <summary>
        /// Thêm giáo viên mới
        /// </summary>
        public bool AddTeacher(Teacher teacher)
        {
            try
            {
                ValidateTeacher(teacher);

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = @"
                        INSERT INTO Teachers (TeacherName, Email, Department, Phone, Address, HireDate, IsActive)
                        VALUES (@TeacherName, @Email, @Department, @Phone, @Address, @HireDate, @IsActive)";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@TeacherName", teacher.TeacherName);
                        cmd.Parameters.AddWithValue("@Email", 
                            string.IsNullOrWhiteSpace(teacher.Email) ? DBNull.Value : teacher.Email);
                        cmd.Parameters.AddWithValue("@Department", 
                            string.IsNullOrWhiteSpace(teacher.Department) ? DBNull.Value : teacher.Department);
                        cmd.Parameters.AddWithValue("@Phone", 
                            string.IsNullOrWhiteSpace(teacher.Phone) ? DBNull.Value : teacher.Phone);
                        cmd.Parameters.AddWithValue("@Address", 
                            string.IsNullOrWhiteSpace(teacher.Address) ? DBNull.Value : teacher.Address);
                        cmd.Parameters.AddWithValue("@HireDate", teacher.HireDate);
                        cmd.Parameters.AddWithValue("@IsActive", teacher.IsActive);

                        int rowsAffected = cmd.ExecuteNonQuery();
                        return rowsAffected > 0;
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                throw new Exception($"Lỗi SQL khi thêm giáo viên: {sqlEx.Message}", sqlEx);
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi thêm giáo viên: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Cập nhật thông tin giáo viên
        /// </summary>
        public bool UpdateTeacher(Teacher teacher)
        {
            try
            {
                ValidateTeacher(teacher);

                // Kiểm tra giáo viên có tồn tại không
                var existingTeacher = GetTeacherById(teacher.TeacherID);
                if (existingTeacher == null)
                {
                    throw new Exception($"Không tìm thấy giáo viên ID {teacher.TeacherID}!");
                }

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = @"
                        UPDATE Teachers
                        SET TeacherName = @TeacherName,
                            Email = @Email,
                            Department = @Department,
                            Phone = @Phone,
                            Address = @Address,
                            HireDate = @HireDate,
                            IsActive = @IsActive
                        WHERE TeacherID = @TeacherID";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@TeacherID", teacher.TeacherID);
                        cmd.Parameters.AddWithValue("@TeacherName", teacher.TeacherName);
                        cmd.Parameters.AddWithValue("@Email",
                            string.IsNullOrWhiteSpace(teacher.Email) ? DBNull.Value : teacher.Email);
                        cmd.Parameters.AddWithValue("@Department",
                            string.IsNullOrWhiteSpace(teacher.Department) ? DBNull.Value : teacher.Department);
                        cmd.Parameters.AddWithValue("@Phone",
                            string.IsNullOrWhiteSpace(teacher.Phone) ? DBNull.Value : teacher.Phone);
                        cmd.Parameters.AddWithValue("@Address",
                            string.IsNullOrWhiteSpace(teacher.Address) ? DBNull.Value : teacher.Address);
                        cmd.Parameters.AddWithValue("@HireDate", teacher.HireDate);
                        cmd.Parameters.AddWithValue("@IsActive", teacher.IsActive);

                        int rowsAffected = cmd.ExecuteNonQuery();
                        return rowsAffected > 0;
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                throw new Exception($"Lỗi SQL khi cập nhật giáo viên: {sqlEx.Message}", sqlEx);
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi cập nhật giáo viên: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// ✅ LINQ: Xóa giáo viên (kiểm tra lớp phụ trách)
        /// </summary>
        public bool DeleteTeacher(int teacherId)
        {
            try
            {
                // Kiểm tra giáo viên có tồn tại không
                var existingTeacher = GetTeacherById(teacherId);
                if (existingTeacher == null)
                {
                    throw new Exception($"Không tìm thấy giáo viên ID {teacherId}!");
                }

                // ✅ LINQ: Kiểm tra giáo viên có đang phụ trách lớp không
                int classCount = GetClassCountByTeacher(teacherId);
                if (classCount > 0)
                {
                    throw new Exception($"Không thể xóa giáo viên '{existingTeacher.TeacherName}' vì đang phụ trách {classCount} lớp!\nVui lòng chuyển lớp cho giáo viên khác trước khi xóa.");
                }

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "DELETE FROM Teachers WHERE TeacherID = @TeacherID";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@TeacherID", teacherId);
                        int rowsAffected = cmd.ExecuteNonQuery();
                        return rowsAffected > 0;
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                throw new Exception($"Lỗi SQL khi xóa giáo viên: {sqlEx.Message}", sqlEx);
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi xóa giáo viên: {ex.Message}", ex);
            }
        }

        #endregion

        #region Statistics Methods (LINQ)

        /// <summary>
        /// ✅ LINQ: Đếm số lớp giáo viên đang phụ trách
        /// </summary>
        public int GetClassCountByTeacher(int teacherId)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "SELECT COUNT(*) FROM Classes WHERE TeacherID = @TeacherID";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@TeacherID", teacherId);
                        int count = (int)cmd.ExecuteScalar();
                        return count;
                    }
                }
            }
            catch
            {
                return 0;
            }
        }

        /// <summary>
        /// ✅ LINQ: Đếm tổng số giáo viên
        /// </summary>
        public int GetTotalTeachers()
        {
            try
            {
                var teachers = LoadTeachersFromDatabase();
                return teachers.Count();  // ✅ LINQ Count
            }
            catch
            {
                return 0;
            }
        }

        /// <summary>
        /// ✅ LINQ: Đếm số giáo viên đang hoạt động
        /// </summary>
        public int GetActiveTeacherCount()
        {
            try
            {
                var teachers = LoadTeachersFromDatabase();
                return teachers.Count(t => t.IsActive);  // ✅ LINQ Count với điều kiện
            }
            catch
            {
                return 0;
            }
        }

        #endregion

        #region Helper Methods

        /// <summary>
        /// Load dữ liệu từ database
        /// </summary>
        private List<Teacher> LoadTeachersFromDatabase()
        {
            List<Teacher> teachers = new List<Teacher>();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = @"
                        SELECT TeacherID, TeacherName, Email, Department, 
                               Phone, Address, HireDate, IsActive
                        FROM Teachers";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            teachers.Add(MapReaderToTeacher(reader));
                        }
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                throw new Exception($"Lỗi SQL: {sqlEx.Message}", sqlEx);
            }

            return teachers;
        }

        /// <summary>
        /// Map SqlDataReader sang Teacher object
        /// </summary>
        private Teacher MapReaderToTeacher(SqlDataReader reader)
        {
            return new Teacher
            {
                TeacherID = reader.GetInt32(reader.GetOrdinal("TeacherID")),
                TeacherName = reader.GetString(reader.GetOrdinal("TeacherName")),
                Email = reader.IsDBNull(reader.GetOrdinal("Email"))
                    ? null
                    : reader.GetString(reader.GetOrdinal("Email")),
                Department = reader.IsDBNull(reader.GetOrdinal("Department"))
                    ? null
                    : reader.GetString(reader.GetOrdinal("Department")),
                Phone = reader.IsDBNull(reader.GetOrdinal("Phone"))
                    ? null
                    : reader.GetString(reader.GetOrdinal("Phone")),
                Address = reader.IsDBNull(reader.GetOrdinal("Address"))
                    ? null
                    : reader.GetString(reader.GetOrdinal("Address")),
                HireDate = reader.GetDateTime(reader.GetOrdinal("HireDate")),
                IsActive = reader.GetBoolean(reader.GetOrdinal("IsActive"))
            };
        }

        /// <summary>
        /// Validate Teacher object
        /// </summary>
        private void ValidateTeacher(Teacher teacher)
        {
            if (teacher == null)
                throw new ArgumentNullException(nameof(teacher), "Teacher object không được null!");

            if (string.IsNullOrWhiteSpace(teacher.TeacherName))
                throw new ArgumentException("Tên giáo viên không được để trống!");

            // Validate Email format (nếu có)
            if (!string.IsNullOrWhiteSpace(teacher.Email))
            {
                if (!teacher.Email.Contains("@"))
                    throw new ArgumentException("Email không hợp lệ!");
            }

            // Validate Phone format (nếu có)
            if (!string.IsNullOrWhiteSpace(teacher.Phone))
            {
                if (teacher.Phone.Length < 10 || teacher.Phone.Length > 11)
                    throw new ArgumentException("Số điện thoại phải có 10-11 chữ số!");
            }
        }

        #endregion
    }
}
