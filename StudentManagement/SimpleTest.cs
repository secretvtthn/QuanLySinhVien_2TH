using System;
using System.Configuration;
using Microsoft.Data.SqlClient;

namespace StudentManagement
{
    public class SimpleTest
    {
        public static void RunTest()
        {
            string connectionString = "Server=localhost\\SQLEXPRESS;Database=StudentManagement;Integrated Security=true;TrustServerCertificate=true;Encrypt=false;";
            
            Console.WriteLine("🔍 ĐANG TEST KẾT NỐI DATABASE...");
            Console.WriteLine("================================");
            
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    Console.WriteLine("Đang mở kết nối...");
                    connection.Open();
                    
                    Console.WriteLine("✅ KẾT NỐI THÀNH CÔNG!");
                    Console.WriteLine($"📍 Server: {connection.DataSource}");
                    Console.WriteLine($"💾 Database: {connection.Database}");
                    Console.WriteLine($"🔗 State: {connection.State}");
                    
                    // Test query đơn giản
                    using (SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM Users", connection))
                    {
                        int userCount = (int)cmd.ExecuteScalar();
                        Console.WriteLine($"👥 Số lượng Users: {userCount}");
                    }
                    
                    connection.Close();
                    Console.WriteLine("🔒 Đã đóng kết nối");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("❌ LỖI KẾT NỐI!");
                Console.WriteLine($"📝 Chi tiết: {ex.Message}");
                
                if (ex.Message.Contains("network-related") || ex.Message.Contains("server was not found"))
                {
                    Console.WriteLine("\n💡 GỢI Ý:");
                    Console.WriteLine("- Kiểm tra SQL Server đã được khởi động");
                    Console.WriteLine("- Xác nhận tên server: localhost\\SQLEXPRESS");
                    Console.WriteLine("- Kiểm tra database 'StudentManagement' đã tồn tại");
                }
            }
            
            Console.WriteLine("================================");
        }
    }
}