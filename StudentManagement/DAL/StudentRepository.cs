using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Data.SqlClient;
using StudentManagement.Helper;
using StudentManagement.Models;

namespace StudentManagement.DAL
{
    /// <summary>
    /// Repository cho Student - Sử dụng LINQ to Objects
    /// ✅ PROMPT 6: Quản lý sinh viên với LINQ
    /// </summary>
    public class StudentRepository
    {
        private readonly string? connectionString;

        public StudentRepository()
        {
            connectionString = DatabaseHelper.ConnectionString;
        }

        #region GET Methods (LINQ to Objects)

        /// <summary>
        /// ✅ LINQ: Lấy tất cả sinh viên (kèm thông tin lớp)
        /// </summary>
        public List<dynamic> GetAllStudents()
        {
            try
            {
                var students = new List<dynamic>();

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = @"
                        SELECT s.StudentID, s.StudentCode, s.StudentName, s.ClassID, 
                               c.ClassName, s.DateOfBirth, s.Gender, s.Email, s.Phone, 
                               s.Address, s.EnrollmentDate, s.IsActive
                        FROM Students s
                        INNER JOIN Classes c ON s.ClassID = c.ClassID";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            students.Add(new
                            {
                                StudentID = reader.GetInt32(reader.GetOrdinal("StudentID")),
                                StudentCode = reader.GetString(reader.GetOrdinal("StudentCode")),
                                StudentName = reader.GetString(reader.GetOrdinal("StudentName")),
                                ClassID = reader.GetInt32(reader.GetOrdinal("ClassID")),
                                ClassName = reader.GetString(reader.GetOrdinal("ClassName")),
                                DateOfBirth = reader.IsDBNull(reader.GetOrdinal("DateOfBirth")) 
                                    ? (DateTime?)null 
                                    : reader.GetDateTime(reader.GetOrdinal("DateOfBirth")),
                                Gender = reader.IsDBNull(reader.GetOrdinal("Gender")) 
                                    ? null 
                                    : reader.GetString(reader.GetOrdinal("Gender")),
                                Email = reader.IsDBNull(reader.GetOrdinal("Email")) 
                                    ? null 
                                    : reader.GetString(reader.GetOrdinal("Email")),
                                Phone = reader.IsDBNull(reader.GetOrdinal("Phone")) 
                                    ? null 
                                    : reader.GetString(reader.GetOrdinal("Phone")),
                                Address = reader.IsDBNull(reader.GetOrdinal("Address")) 
                                    ? null 
                                    : reader.GetString(reader.GetOrdinal("Address")),
                                EnrollmentDate = reader.GetDateTime(reader.GetOrdinal("EnrollmentDate")),
                                IsActive = reader.GetBoolean(reader.GetOrdinal("IsActive"))
                            });
                        }
                    }
                }

                // ✅ LINQ: OrderByDescending
                return students.OrderByDescending(s => s.StudentID).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi lấy danh sách sinh viên: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// ✅ LINQ: Lấy sinh viên theo ID
        /// </summary>
        public Student? GetStudentById(int studentID)
        {
            try
            {
                var students = LoadStudentsFromDatabase();
                
                // ✅ LINQ: FirstOrDefault
                return students.FirstOrDefault(s => s.StudentID == studentID);
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi lấy sinh viên ID {studentID}: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// ✅ LINQ: Lấy sinh viên theo lớp
        /// </summary>
        public List<dynamic> GetStudentsByClass(int classID)
        {
            try
            {
                var allStudents = GetAllStudents();
                
                // ✅ LINQ: Where + OrderBy
                return allStudents
                    .Where(s => s.ClassID == classID)
                    .OrderBy(s => s.StudentName)
                    .ToList();
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi lấy sinh viên lớp {classID}: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// ✅ SỬA PROMPT 11: Lấy sinh viên theo lớp (trả về List<Student> thay vì List<dynamic>)
        /// </summary>
        public List<Student> GetStudentsObjectByClass(int classID)
        {
            try
            {
                var students = LoadStudentsFromDatabase();
                
                // ✅ LINQ: Where + OrderBy
                return students
                    .Where(s => s.ClassID == classID && s.IsActive)
                    .OrderBy(s => s.StudentName)
                    .ToList();
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi lấy sinh viên lớp {classID}: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// ✅ LINQ: Tìm kiếm sinh viên theo từ khóa (StudentName, StudentCode, Email, Phone)
        /// </summary>
        public List<dynamic> SearchStudent(string keyword)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(keyword))
                {
                    return GetAllStudents();
                }

                var allStudents = GetAllStudents();
                keyword = keyword.Trim().ToLower();
                
                // ✅ LINQ: Where với Contains
                return allStudents
                    .Where(s =>
                        s.StudentName.ToString().ToLower().Contains(keyword) ||
                        s.StudentCode.ToString().ToLower().Contains(keyword) ||
                        (s.Email != null && s.Email.ToString().ToLower().Contains(keyword)) ||
                        (s.Phone != null && s.Phone.ToString().Contains(keyword))
                    )
                    .OrderByDescending(s => s.StudentID)
                    .ToList();
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi tìm kiếm sinh viên: {ex.Message}", ex);
            }
        }

        #endregion

        #region CUD Methods (Create, Update, Delete)

        /// <summary>
        /// Thêm sinh viên mới (với đầy đủ thuộc tính)
        /// </summary>
        public bool AddStudent(Student student)
        {
            try
            {
                // Validate dữ liệu
                ValidateStudent(student);

                // Kiểm tra trùng mã sinh viên
                if (IsStudentCodeExist(student.StudentCode))
                {
                    throw new InvalidOperationException($"Mã sinh viên '{student.StudentCode}' đã tồn tại!");
                }

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = @"
                        INSERT INTO Students (StudentCode, StudentName, ClassID, DateOfBirth, 
                                            Gender, Email, Phone, Address, EnrollmentDate, IsActive)
                        VALUES (@StudentCode, @StudentName, @ClassID, @DateOfBirth, 
                                @Gender, @Email, @Phone, @Address, @EnrollmentDate, @IsActive)";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@StudentCode", student.StudentCode);
                        cmd.Parameters.AddWithValue("@StudentName", student.StudentName);
                        cmd.Parameters.AddWithValue("@ClassID", student.ClassID);
                        cmd.Parameters.AddWithValue("@DateOfBirth", 
                            student.DateOfBirth.HasValue ? (object)student.DateOfBirth.Value : DBNull.Value);
                        cmd.Parameters.AddWithValue("@Gender", 
                            string.IsNullOrWhiteSpace(student.Gender) ? DBNull.Value : student.Gender);
                        cmd.Parameters.AddWithValue("@Email", 
                            string.IsNullOrWhiteSpace(student.Email) ? DBNull.Value : student.Email);
                        cmd.Parameters.AddWithValue("@Phone", 
                            string.IsNullOrWhiteSpace(student.Phone) ? DBNull.Value : student.Phone);
                        cmd.Parameters.AddWithValue("@Address", 
                            string.IsNullOrWhiteSpace(student.Address) ? DBNull.Value : student.Address);
                        cmd.Parameters.AddWithValue("@EnrollmentDate", student.EnrollmentDate);
                        cmd.Parameters.AddWithValue("@IsActive", student.IsActive);

                        int rowsAffected = cmd.ExecuteNonQuery();
                        return rowsAffected > 0;
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                throw new Exception($"Lỗi SQL khi thêm sinh viên: {sqlEx.Message}", sqlEx);
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi thêm sinh viên: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Cập nhật thông tin sinh viên (đầy đủ thuộc tính)
        /// </summary>
        public bool UpdateStudent(Student student)
        {
            try
            {
                // Validate dữ liệu
                ValidateStudent(student);

                // Kiểm tra sinh viên có tồn tại không
                var existingStudent = GetStudentById(student.StudentID);
                if (existingStudent == null)
                {
                    throw new InvalidOperationException($"Không tìm thấy sinh viên ID {student.StudentID}!");
                }

                // Kiểm tra trùng mã sinh viên (ngoại trừ chính nó)
                if (IsStudentCodeExist(student.StudentCode, student.StudentID))
                {
                    throw new InvalidOperationException($"Mã sinh viên '{student.StudentCode}' đã được sử dụng!");
                }

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = @"
                        UPDATE Students
                        SET StudentCode = @StudentCode,
                            StudentName = @StudentName,
                            ClassID = @ClassID,
                            DateOfBirth = @DateOfBirth,
                            Gender = @Gender,
                            Email = @Email,
                            Phone = @Phone,
                            Address = @Address,
                            EnrollmentDate = @EnrollmentDate,
                            IsActive = @IsActive
                        WHERE StudentID = @StudentID";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@StudentID", student.StudentID);
                        cmd.Parameters.AddWithValue("@StudentCode", student.StudentCode);
                        cmd.Parameters.AddWithValue("@StudentName", student.StudentName);
                        cmd.Parameters.AddWithValue("@ClassID", student.ClassID);
                        cmd.Parameters.AddWithValue("@DateOfBirth",
                            student.DateOfBirth.HasValue ? (object)student.DateOfBirth.Value : DBNull.Value);
                        cmd.Parameters.AddWithValue("@Gender",
                            string.IsNullOrWhiteSpace(student.Gender) ? DBNull.Value : student.Gender);
                        cmd.Parameters.AddWithValue("@Email",
                            string.IsNullOrWhiteSpace(student.Email) ? DBNull.Value : student.Email);
                        cmd.Parameters.AddWithValue("@Phone",
                            string.IsNullOrWhiteSpace(student.Phone) ? DBNull.Value : student.Phone);
                        cmd.Parameters.AddWithValue("@Address",
                            string.IsNullOrWhiteSpace(student.Address) ? DBNull.Value : student.Address);
                        cmd.Parameters.AddWithValue("@EnrollmentDate", student.EnrollmentDate);
                        cmd.Parameters.AddWithValue("@IsActive", student.IsActive);

                        int rowsAffected = cmd.ExecuteNonQuery();
                        return rowsAffected > 0;
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                throw new Exception($"Lỗi SQL khi cập nhật sinh viên: {sqlEx.Message}", sqlEx);
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi cập nhật sinh viên: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// ✅ LINQ: Xóa sinh viên (kiểm tra điểm thi)
        /// </summary>
        public bool DeleteStudent(int studentID)
        {
            try
            {
                if (studentID <= 0)
                {
                    throw new ArgumentException("StudentID không hợp lệ!");
                }

                // Kiểm tra sinh viên có tồn tại không
                var existingStudent = GetStudentById(studentID);
                if (existingStudent == null)
                {
                    throw new InvalidOperationException($"Không tìm thấy sinh viên ID {studentID}!");
                }

                // Kiểm tra có điểm thi không (nếu có thì không cho xóa)
                if (HasScores(studentID))
                {
                    throw new InvalidOperationException($"Không thể xóa sinh viên '{existingStudent.StudentName}' vì đã có điểm thi. Hãy chuyển sang trạng thái IsActive = false thay vì xóa!");
                }

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "DELETE FROM Students WHERE StudentID = @StudentID";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@StudentID", studentID);
                        int rowsAffected = cmd.ExecuteNonQuery();
                        return rowsAffected > 0;
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                throw new Exception($"Lỗi SQL khi xóa sinh viên: {sqlEx.Message}", sqlEx);
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi xóa sinh viên: {ex.Message}", ex);
            }
        }

        #endregion

        #region Statistics Methods (LINQ)

        /// <summary>
        /// ✅ LINQ: Kiểm tra mã sinh viên đã tồn tại chưa
        /// </summary>
        public bool IsStudentCodeExist(string studentCode, int excludeStudentID = 0)
        {
            try
            {
                var students = LoadStudentsFromDatabase();
                
                // ✅ LINQ: Any
                return students.Any(s => 
                    s.StudentCode.Equals(studentCode, StringComparison.OrdinalIgnoreCase) && 
                    s.StudentID != excludeStudentID);
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi kiểm tra mã sinh viên: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// ✅ LINQ: Đếm số lượng sinh viên trong lớp
        /// </summary>
        public int CountStudentsByClass(int classID)
        {
            try
            {
                var students = LoadStudentsFromDatabase();
                
                // ✅ LINQ: Count với điều kiện
                return students.Count(s => s.ClassID == classID && s.IsActive);
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi đếm sinh viên: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// ✅ LINQ: Đếm tổng số sinh viên
        /// </summary>
        public int GetTotalStudents()
        {
            try
            {
                var students = LoadStudentsFromDatabase();
                return students.Count();  // ✅ LINQ Count
            }
            catch
            {
                return 0;
            }
        }

        /// <summary>
        /// ✅ LINQ: Đếm số sinh viên đang hoạt động
        /// </summary>
        public int GetActiveStudentCount()
        {
            try
            {
                var students = LoadStudentsFromDatabase();
                return students.Count(s => s.IsActive);  // ✅ LINQ Count với điều kiện
            }
            catch
            {
                return 0;
            }
        }

        #endregion

        #region Helper Methods

        /// <summary>
        /// Load dữ liệu sinh viên từ database
        /// </summary>
        private List<Student> LoadStudentsFromDatabase()
        {
            List<Student> students = new List<Student>();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = @"
                        SELECT StudentID, StudentCode, StudentName, ClassID, DateOfBirth, 
                               Gender, Email, Phone, Address, EnrollmentDate, IsActive
                        FROM Students";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            students.Add(MapReaderToStudent(reader));
                        }
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                throw new Exception($"Lỗi SQL: {sqlEx.Message}", sqlEx);
            }

            return students;
        }

        /// <summary>
        /// Map SqlDataReader sang Student object
        /// </summary>
        private Student MapReaderToStudent(SqlDataReader reader)
        {
            return new Student
            {
                StudentID = reader.GetInt32(reader.GetOrdinal("StudentID")),
                StudentCode = reader.GetString(reader.GetOrdinal("StudentCode")),
                StudentName = reader.GetString(reader.GetOrdinal("StudentName")),
                ClassID = reader.GetInt32(reader.GetOrdinal("ClassID")),
                DateOfBirth = reader.IsDBNull(reader.GetOrdinal("DateOfBirth"))
                    ? null
                    : reader.GetDateTime(reader.GetOrdinal("DateOfBirth")),
                Gender = reader.IsDBNull(reader.GetOrdinal("Gender"))
                    ? null
                    : reader.GetString(reader.GetOrdinal("Gender")),
                Email = reader.IsDBNull(reader.GetOrdinal("Email"))
                    ? null
                    : reader.GetString(reader.GetOrdinal("Email")),
                Phone = reader.IsDBNull(reader.GetOrdinal("Phone"))
                    ? null
                    : reader.GetString(reader.GetOrdinal("Phone")),
                Address = reader.IsDBNull(reader.GetOrdinal("Address"))
                    ? null
                    : reader.GetString(reader.GetOrdinal("Address")),
                EnrollmentDate = reader.GetDateTime(reader.GetOrdinal("EnrollmentDate")),
                IsActive = reader.GetBoolean(reader.GetOrdinal("IsActive"))
            };
        }

        /// <summary>
        /// Validate Student object
        /// </summary>
        private void ValidateStudent(Student student)
        {
            if (student == null)
                throw new ArgumentNullException(nameof(student), "Student object không được null!");

            if (string.IsNullOrWhiteSpace(student.StudentCode))
                throw new ArgumentException("Mã sinh viên không được để trống!");

            if (string.IsNullOrWhiteSpace(student.StudentName))
                throw new ArgumentException("Tên sinh viên không được để trống!");

            if (student.ClassID <= 0)
                throw new ArgumentException("Phải chọn lớp học!");

            // Validate Email format (nếu có)
            if (!string.IsNullOrWhiteSpace(student.Email))
            {
                if (!student.Email.Contains("@"))
                    throw new ArgumentException("Email không hợp lệ!");
            }

            // Validate Phone format (nếu có)
            if (!string.IsNullOrWhiteSpace(student.Phone))
            {
                if (student.Phone.Length < 10 || student.Phone.Length > 11)
                    throw new ArgumentException("Số điện thoại phải có 10-11 chữ số!");
            }
        }

        /// <summary>
        /// Kiểm tra sinh viên có điểm thi không
        /// </summary>
        private bool HasScores(int studentID)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "SELECT COUNT(*) FROM Scores WHERE StudentID = @StudentID";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@StudentID", studentID);
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

        #endregion
    }
}
