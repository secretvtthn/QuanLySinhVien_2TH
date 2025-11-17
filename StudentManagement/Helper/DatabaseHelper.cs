using System;
using System.Configuration;
using Microsoft.Data.SqlClient;

namespace StudentManagement.Helper
{
    public class DatabaseHelper
    {
        private static string connectionString = ConfigurationManager.ConnectionStrings["StudentManagementDB"].ConnectionString;

        // Sửa: Expose ConnectionString như property public
        public static string? ConnectionString 
        { 
            get => connectionString;
            set => connectionString = value ?? connectionString;
        }

        /// <summary>
        /// Lấy connection đến SQL Server database
        /// </summary>
        /// <returns>SqlConnection object</returns>
        public static SqlConnection GetConnection()
        {
            try
            {
                SqlConnection connection = new SqlConnection(connectionString);
                return connection;
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi kết nối database: {ex.Message}");
            }
        }

        /// <summary>
        /// Test kết nối database
        /// </summary>
        /// <returns>True nếu kết nối thành công</returns>
        public static bool TestConnection()
        {
            try
            {
                using (SqlConnection connection = GetConnection())
                {
                    connection.Open();
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }
    }
}