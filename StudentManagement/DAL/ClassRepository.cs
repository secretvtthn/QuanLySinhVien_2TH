using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Data.SqlClient;
using StudentManagement.Helper;
using StudentManagement.Models;

namespace StudentManagement.DAL
{
    /// <summary>
    /// Repository cho Class - Sử dụng LINQ to Objects
    /// ✅ PROMPT 8: Quản lý lớp học với LINQ
    /// </summary>
    public class ClassRepository
    {
        private readonly string? connectionString;

        public ClassRepository()
        {
            connectionString = DatabaseHelper.ConnectionString;
        }

        #region GET Methods (LINQ to Objects)

        /// <summary>
        /// ✅ LINQ: Lấy tất cả lớp học
        /// </summary>
        public List<Class> GetAllClasses()
        {
            try
            {
                var classes = LoadClassesFromDatabase();
                
                // ✅ LINQ: OrderBy
                return classes
                    .OrderBy(c => c.ClassName)
                    .ToList();
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi lấy danh sách lớp: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// ✅ LINQ: Lấy lớp theo ID
        /// </summary>
        public Class? GetClassById(int classId)
        {
            try
            {
                var classes = LoadClassesFromDatabase();
                
                // ✅ LINQ: FirstOrDefault
                return classes.FirstOrDefault(c => c.ClassID == classId);
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi lấy lớp ID {classId}: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// ✅ LINQ: Lấy lớp theo tên
        /// </summary>
        public Class? GetClassByName(string className)
        {
            try
            {
                var classes = LoadClassesFromDatabase();
                
                // ✅ LINQ: FirstOrDefault với StringComparison
                return classes.FirstOrDefault(c => 
                    c.ClassName.Equals(className, StringComparison.OrdinalIgnoreCase));
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi lấy lớp '{className}': {ex.Message}", ex);
            }
        }

        /// <summary>
        /// ✅ LINQ: Lấy các lớp của giáo viên
        /// </summary>
        public List<Class> GetClassesByTeacher(int teacherId)
        {
            try
            {
                var classes = LoadClassesFromDatabase();
                
                // ✅ LINQ: Where
                return classes
                    .Where(c => c.TeacherID == teacherId)
                    .OrderBy(c => c.ClassName)
                    .ToList();
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi lấy lớp của giáo viên {teacherId}: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// ✅ LINQ: Tìm kiếm lớp
        /// </summary>
        public List<Class> SearchClass(string keyword)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(keyword))
                {
                    return GetAllClasses();
                }

                var classes = LoadClassesFromDatabase();

                // ✅ LINQ: Where với Contains
                return classes
                    .Where(c =>
                        c.ClassName.Contains(keyword, StringComparison.OrdinalIgnoreCase) ||
                        (c.Room != null && c.Room.Contains(keyword, StringComparison.OrdinalIgnoreCase))
                    )
                    .OrderBy(c => c.ClassName)
                    .ToList();
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi tìm kiếm lớp: {ex.Message}", ex);
            }
        }

        #endregion

        #region CUD Methods (Create, Update, Delete)

        /// <summary>
        /// Thêm lớp mới
        /// </summary>
        public bool AddClass(Class classObj)
        {
            try
            {
                ValidateClass(classObj);

                // Kiểm tra trùng tên lớp
                if (IsClassNameExists(classObj.ClassName))
                {
                    throw new Exception($"Tên lớp '{classObj.ClassName}' đã tồn tại!");
                }

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = @"
                        INSERT INTO Classes (ClassName, TeacherID, Capacity, Room, CreatedDate, UpdatedDate)
                        VALUES (@ClassName, @TeacherID, @Capacity, @Room, GETDATE(), GETDATE())";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@ClassName", classObj.ClassName);
                        cmd.Parameters.AddWithValue("@TeacherID", 
                            classObj.TeacherID.HasValue ? (object)classObj.TeacherID.Value : DBNull.Value);
                        cmd.Parameters.AddWithValue("@Capacity", classObj.Capacity);
                        cmd.Parameters.AddWithValue("@Room", 
                            string.IsNullOrWhiteSpace(classObj.Room) ? DBNull.Value : classObj.Room);

                        int rowsAffected = cmd.ExecuteNonQuery();
                        return rowsAffected > 0;
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                throw new Exception($"Lỗi SQL khi thêm lớp: {sqlEx.Message}", sqlEx);
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi thêm lớp: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Cập nhật thông tin lớp
        /// </summary>
        public bool UpdateClass(Class classObj)
        {
            try
            {
                ValidateClass(classObj);

                // Kiểm tra lớp có tồn tại không
                var existingClass = GetClassById(classObj.ClassID);
                if (existingClass == null)
                {
                    throw new Exception($"Không tìm thấy lớp ID {classObj.ClassID}!");
                }

                // Kiểm tra trùng tên (trừ chính nó)
                if (IsClassNameExists(classObj.ClassName, classObj.ClassID))
                {
                    throw new Exception($"Tên lớp '{classObj.ClassName}' đã tồn tại!");
                }

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = @"
                        UPDATE Classes
                        SET ClassName = @ClassName,
                            TeacherID = @TeacherID,
                            Capacity = @Capacity,
                            Room = @Room,
                            UpdatedDate = GETDATE()
                        WHERE ClassID = @ClassID";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@ClassID", classObj.ClassID);
                        cmd.Parameters.AddWithValue("@ClassName", classObj.ClassName);
                        cmd.Parameters.AddWithValue("@TeacherID",
                            classObj.TeacherID.HasValue ? (object)classObj.TeacherID.Value : DBNull.Value);
                        cmd.Parameters.AddWithValue("@Capacity", classObj.Capacity);
                        cmd.Parameters.AddWithValue("@Room",
                            string.IsNullOrWhiteSpace(classObj.Room) ? DBNull.Value : classObj.Room);

                        int rowsAffected = cmd.ExecuteNonQuery();
                        return rowsAffected > 0;
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                throw new Exception($"Lỗi SQL khi cập nhật lớp: {sqlEx.Message}", sqlEx);
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi cập nhật lớp: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// ✅ LINQ: Xóa lớp (kiểm tra sinh viên bằng LINQ)
        /// </summary>
        public bool DeleteClass(int classId)
        {
            try
            {
                // Kiểm tra lớp có tồn tại không
                var existingClass = GetClassById(classId);
                if (existingClass == null)
                {
                    throw new Exception($"Không tìm thấy lớp ID {classId}!");
                }

                // ✅ LINQ: Kiểm tra lớp có sinh viên không
                if (HasStudents(classId))
                {
                    throw new Exception($"Không thể xóa lớp '{existingClass.ClassName}' vì còn sinh viên trong lớp!\nVui lòng chuyển sinh viên sang lớp khác trước.");
                }

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "DELETE FROM Classes WHERE ClassID = @ClassID";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@ClassID", classId);
                        int rowsAffected = cmd.ExecuteNonQuery();
                        return rowsAffected > 0;
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                throw new Exception($"Lỗi SQL khi xóa lớp: {sqlEx.Message}", sqlEx);
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi xóa lớp: {ex.Message}", ex);
            }
        }

        #endregion

        #region Statistics Methods (LINQ)

        /// <summary>
        /// ✅ LINQ: Đếm số sinh viên trong lớp
        /// </summary>
        public int GetStudentCountByClass(int classId)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "SELECT StudentID, ClassID, IsActive FROM Students WHERE ClassID = @ClassID";

                    List<int> studentIds = new List<int>();
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@ClassID", classId);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                if (reader.GetBoolean(reader.GetOrdinal("IsActive")))
                                {
                                    studentIds.Add(reader.GetInt32(reader.GetOrdinal("StudentID")));
                                }
                            }
                        }
                    }

                    // ✅ LINQ: Count
                    return studentIds.Count;
                }
            }
            catch
            {
                return 0;
            }
        }

        /// <summary>
        /// ✅ LINQ: Đếm tổng số lớp
        /// </summary>
        public int GetTotalClasses()
        {
            try
            {
                var classes = LoadClassesFromDatabase();
                return classes.Count();  // ✅ LINQ Count
            }
            catch
            {
                return 0;
            }
        }

        /// <summary>
        /// ✅ LINQ: Lấy lớp có nhiều sinh viên nhất
        /// </summary>
        public Class? GetClassWithMostStudents()
        {
            try
            {
                var classes = LoadClassesFromDatabase();
                
                // ✅ LINQ: Select + OrderByDescending + FirstOrDefault
                return classes
                    .Select(c => new { Class = c, StudentCount = GetStudentCountByClass(c.ClassID) })
                    .OrderByDescending(x => x.StudentCount)
                    .Select(x => x.Class)
                    .FirstOrDefault();
            }
            catch
            {
                return null;
            }
        }

        #endregion

        #region Helper Methods

        /// <summary>
        /// Load dữ liệu từ database
        /// </summary>
        private List<Class> LoadClassesFromDatabase()
        {
            List<Class> classes = new List<Class>();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = @"
                        SELECT ClassID, ClassName, TeacherID, Capacity, Room, 
                               CreatedDate, UpdatedDate
                        FROM Classes";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            classes.Add(MapReaderToClass(reader));
                        }
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                throw new Exception($"Lỗi SQL: {sqlEx.Message}", sqlEx);
            }

            return classes;
        }

        /// <summary>
        /// Map SqlDataReader sang Class object
        /// </summary>
        private Class MapReaderToClass(SqlDataReader reader)
        {
            return new Class
            {
                ClassID = reader.GetInt32(reader.GetOrdinal("ClassID")),
                ClassName = reader.GetString(reader.GetOrdinal("ClassName")),
                TeacherID = reader.IsDBNull(reader.GetOrdinal("TeacherID"))
                    ? null
                    : reader.GetInt32(reader.GetOrdinal("TeacherID")),
                Capacity = reader.GetInt32(reader.GetOrdinal("Capacity")),
                Room = reader.IsDBNull(reader.GetOrdinal("Room"))
                    ? null
                    : reader.GetString(reader.GetOrdinal("Room")),
                CreatedDate = reader.GetDateTime(reader.GetOrdinal("CreatedDate")),
                UpdatedDate = reader.GetDateTime(reader.GetOrdinal("UpdatedDate"))
            };
        }

        /// <summary>
        /// Validate Class object
        /// </summary>
        private void ValidateClass(Class classObj)
        {
            if (classObj == null)
                throw new ArgumentNullException(nameof(classObj), "Class object không được null!");

            if (string.IsNullOrWhiteSpace(classObj.ClassName))
                throw new ArgumentException("Tên lớp không được để trống!");

            if (classObj.Capacity <= 0)
                throw new ArgumentException("Sĩ số phải lớn hơn 0!");
        }

        /// <summary>
        /// ✅ LINQ: Kiểm tra tên lớp đã tồn tại chưa
        /// </summary>
        private bool IsClassNameExists(string className, int excludeClassId = 0)
        {
            try
            {
                var classes = LoadClassesFromDatabase();
                
                // ✅ LINQ: Any
                return classes.Any(c =>
                    c.ClassName.Equals(className, StringComparison.OrdinalIgnoreCase) &&
                    c.ClassID != excludeClassId);
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// ✅ LINQ: Kiểm tra lớp có sinh viên không
        /// </summary>
        private bool HasStudents(int classId)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "SELECT COUNT(*) FROM Students WHERE ClassID = @ClassID AND IsActive = 1";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@ClassID", classId);
                        int count = (int)cmd.ExecuteScalar();
                        return count > 0;  // ✅ LINQ: Tương đương Any()
                    }
                }
            }
            catch
            {
                return false;
            }
        }

        #endregion
    }
}
