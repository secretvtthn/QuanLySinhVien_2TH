using System.Data;
using Microsoft.Data.SqlClient;
using StudentManagement.Models;

namespace StudentManagement.Data
{
    /// <summary>
    /// DataAccess class (non-static) để thực thi query
    /// </summary>
    public class DataAccess
    {
        /// <summary>
        /// Thực thi SELECT query và trả về DataTable
        /// </summary>
        public DataTable ExecuteQuery(string query, SqlParameter[]? parameters = null)
        {
            DataTable dataTable = new DataTable();

            try
            {
                using (SqlConnection connection = Helper.DatabaseHelper.GetConnection())
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        if (parameters != null)
                        {
                            command.Parameters.AddRange(parameters);
                        }

                        using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                        {
                            adapter.Fill(dataTable);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in ExecuteQuery: {ex.Message}");
                throw;
            }

            return dataTable;
        }

        /// <summary>
        /// Thực thi INSERT, UPDATE, DELETE và trả về số dòng affected
        /// </summary>
        public int ExecuteNonQuery(string query, SqlParameter[]? parameters = null)
        {
            int rowsAffected = 0;

            try
            {
                using (SqlConnection connection = Helper.DatabaseHelper.GetConnection())
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        if (parameters != null)
                        {
                            command.Parameters.AddRange(parameters);
                        }

                        rowsAffected = command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in ExecuteNonQuery: {ex.Message}");
                throw;
            }

            return rowsAffected;
        }

        /// <summary>
        /// Thực thi query và trả về giá trị đơn (COUNT, SUM, AVG, etc.)
        /// </summary>
        public object? ExecuteScalar(string query, SqlParameter[]? parameters = null)
        {
            object? result = null;

            try
            {
                using (SqlConnection connection = Helper.DatabaseHelper.GetConnection())
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        if (parameters != null)
                        {
                            command.Parameters.AddRange(parameters);
                        }

                        result = command.ExecuteScalar();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in ExecuteScalar: {ex.Message}");
                throw;
            }

            return result;
        }
    }

    /// <summary>
    /// DataAccess helper sử dụng LINQ với SqlCommand
    /// </summary>
    public static class UserDataAccess
    {
        /// <summary>
        /// Lấy tất cả users từ database và trả về IEnumerable để dùng LINQ
        /// </summary>
        public static IEnumerable<User> GetAllUsers()
        {
            var users = new List<User>();

            using (SqlConnection connection = Helper.DatabaseHelper.GetConnection())
            {
                connection.Open();
                string query = "SELECT UserID, Username, Password, FullName, Role, Email, Phone, CreatedDate, UpdatedDate, IsActive FROM Users";
                SqlCommand command = new SqlCommand(query, connection);
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    users.Add(new User
                    {
                        UserID = reader.GetInt32(0),
                        Username = reader.GetString(1),
                        Password = reader.GetString(2),
                        FullName = reader.GetString(3),
                        Role = reader.GetString(4),
                        Email = reader.IsDBNull(5) ? null : reader.GetString(5),
                        Phone = reader.IsDBNull(6) ? null : reader.GetString(6),
                        CreatedDate = reader.GetDateTime(7),
                        UpdatedDate = reader.GetDateTime(8),
                        IsActive = reader.GetBoolean(9)
                    });
                }
            }

            return users;
        }

        /// <summary>
        /// Query user bằng LINQ
        /// </summary>
        public static User? GetUserByUsernamePassword(string username, string password)
        {
            var users = GetAllUsers();

            // LINQ query
            var user = (from u in users
                       where u.Username == username && u.Password == password
                       select u).FirstOrDefault();

            return user;
        }
    }

    /// <summary>
    /// DataAccess cho Students
    /// </summary>
    public static class StudentDataAccess
    {
        public static IEnumerable<Student> GetAllStudents()
        {
            var students = new List<Student>();

            using (SqlConnection connection = Helper.DatabaseHelper.GetConnection())
            {
                connection.Open();
                string query = "SELECT StudentID, StudentCode, StudentName, ClassID, DateOfBirth, Gender, Email, Phone, Address, EnrollmentDate, IsActive FROM Students";
                SqlCommand command = new SqlCommand(query, connection);
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    students.Add(new Student
                    {
                        StudentID = reader.GetInt32(0),
                        StudentCode = reader.GetString(1),
                        StudentName = reader.GetString(2),
                        ClassID = reader.GetInt32(3),
                        DateOfBirth = reader.IsDBNull(4) ? null : reader.GetDateTime(4),
                        Gender = reader.IsDBNull(5) ? null : reader.GetString(5),
                        Email = reader.IsDBNull(6) ? null : reader.GetString(6),
                        Phone = reader.IsDBNull(7) ? null : reader.GetString(7),
                        Address = reader.IsDBNull(8) ? null : reader.GetString(8),
                        EnrollmentDate = reader.GetDateTime(9),
                        IsActive = reader.GetBoolean(10)
                    });
                }
            }

            return students;
        }

        /// <summary>
        /// Đếm số sinh viên active bằng LINQ
        /// </summary>
        public static int CountActiveStudents()
        {
            return GetAllStudents().Count(s => s.IsActive);
        }
    }

    /// <summary>
    /// DataAccess cho Teachers
    /// </summary>
    public static class TeacherDataAccess
    {
        public static IEnumerable<Teacher> GetAllTeachers()
        {
            var teachers = new List<Teacher>();

            using (SqlConnection connection = Helper.DatabaseHelper.GetConnection())
            {
                connection.Open();
                string query = "SELECT TeacherID, TeacherName, Email, Department, Phone, Address, HireDate, IsActive FROM Teachers";
                SqlCommand command = new SqlCommand(query, connection);
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    teachers.Add(new Teacher
                    {
                        TeacherID = reader.GetInt32(0),
                        TeacherName = reader.GetString(1),
                        Email = reader.IsDBNull(2) ? null : reader.GetString(2),
                        Department = reader.IsDBNull(3) ? null : reader.GetString(3),
                        Phone = reader.IsDBNull(4) ? null : reader.GetString(4),
                        Address = reader.IsDBNull(5) ? null : reader.GetString(5),
                        HireDate = reader.GetDateTime(6),
                        IsActive = reader.GetBoolean(7)
                    });
                }
            }

            return teachers;
        }

        /// <summary>
        /// Đếm số giáo viên active bằng LINQ
        /// </summary>
        public static int CountActiveTeachers()
        {
            return GetAllTeachers().Count(t => t.IsActive);
        }
    }

    /// <summary>
    /// DataAccess cho Classes
    /// </summary>
    public static class ClassDataAccess
    {
        public static IEnumerable<Class> GetAllClasses()
        {
            var classes = new List<Class>();

            using (SqlConnection connection = Helper.DatabaseHelper.GetConnection())
            {
                connection.Open();
                string query = "SELECT ClassID, ClassName, TeacherID, Capacity, Room, CreatedDate, UpdatedDate FROM Classes";
                SqlCommand command = new SqlCommand(query, connection);
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    classes.Add(new Class
                    {
                        ClassID = reader.GetInt32(0),
                        ClassName = reader.GetString(1),
                        TeacherID = reader.IsDBNull(2) ? null : reader.GetInt32(2),
                        Capacity = reader.GetInt32(3),
                        Room = reader.IsDBNull(4) ? null : reader.GetString(4),
                        CreatedDate = reader.GetDateTime(5),
                        UpdatedDate = reader.GetDateTime(6)
                    });
                }
            }

            return classes;
        }

        /// <summary>
        /// Đếm số lớp học bằng LINQ
        /// </summary>
        public static int CountClasses()
        {
            return GetAllClasses().Count();
        }
    }
}