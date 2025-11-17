using System;
using StudentManagement.Helper;

namespace StudentManagement
{
    public class TestConnection
    {
        /// <summary>
        /// Test method để kiểm tra kết nối database
        /// </summary>
        public static void TestDatabaseConnection()
        {
            try
            {
                bool isConnected = DatabaseHelper.TestConnection();
                
                if (isConnected)
                {
                    Console.WriteLine("✅ Kết nối database thành công!");
                    Console.WriteLine("Database: StudentManagement");
                    Console.WriteLine("Server: localhost\\SQLEXPRESS");
                }
                else
                {
                    Console.WriteLine("❌ Kết nối database thất bại!");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Lỗi kết nối database: {ex.Message}");
            }
        }

        /// <summary>
        /// Test method với thông tin chi tiết
        /// </summary>
        public static void TestConnectionWithDetails()
        {
            Console.WriteLine("=== TEST KẾT NỐI DATABASE ===");
            
            try
            {
                using (var connection = DatabaseHelper.GetConnection())
                {
                    connection.Open();
                    Console.WriteLine("✅ Kết nối thành công!");
                    Console.WriteLine($"Database: {connection.Database}");
                    Console.WriteLine($"Server: {connection.DataSource}");
                    Console.WriteLine($"Connection State: {connection.State}");
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Lỗi: {ex.Message}");
            }
            
            Console.WriteLine("================================");
        }
    }
}