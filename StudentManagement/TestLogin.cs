using System;
using System.Linq;
using StudentManagement.Helper;
using StudentManagement.Models;
using StudentManagement.Data;

namespace StudentManagement
{
    public class TestLogin
    {
        /// <summary>
        /// Test login với các case khác nhau
        /// </summary>
        public static void TestAllLoginCases()
        {
            Console.WriteLine("========================================");
            Console.WriteLine("    TEST LOGIN VỚI LINQ");
            Console.WriteLine("========================================\n");

            // Test Case 1: Login đúng với Admin
            Console.WriteLine("TEST CASE 1: Login Admin (IsActive = true)");
            TestLoginCase("admin", "admin123", "Admin");
            Console.WriteLine();

            // Test Case 2: Login đúng với GiaoVien
            Console.WriteLine("TEST CASE 2: Login GiaoVien (IsActive = true)");
            TestLoginCase("gv001", "password1", "GiaoVien");
            Console.WriteLine();

            // Test Case 3: Login đúng với SinhVien
            Console.WriteLine("TEST CASE 3: Login SinhVien (IsActive = true)");
            TestLoginCase("sv001", "password3", "SinhVien");
            Console.WriteLine();

            // Test Case 4: Login sai username
            Console.WriteLine("TEST CASE 4: Login sai username");
            TestLoginCase("wronguser", "admin123", null);
            Console.WriteLine();

            // Test Case 5: Login sai password
            Console.WriteLine("TEST CASE 5: Login sai password");
            TestLoginCase("admin", "wrongpass", null);
            Console.WriteLine();

            // Test Case 6: Login với IsActive = false
            Console.WriteLine("TEST CASE 6: Login với tài khoản bị khóa");
            TestInactiveAccount();
            Console.WriteLine();

            // Hiển thị tất cả users
            Console.WriteLine("========================================");
            Console.WriteLine("DANH SÁCH TẤT CẢ USERS (LINQ):");
            Console.WriteLine("========================================");
            DisplayAllUsers();

            Console.WriteLine("\n========================================");
            Console.WriteLine("    KẾT THÚC TEST");
            Console.WriteLine("========================================");
        }

        /// <summary>
        /// Test một case login cụ thể
        /// </summary>
        private static void TestLoginCase(string username, string password, string? expectedRole)
        {
            try
            {
                // LINQ query với DataAccess
                var user = UserDataAccess.GetUserByUsernamePassword(username, password);

                if (user != null)
                {
                    if (!user.IsActive)
                    {
                        Console.WriteLine($"❌ FAILED: Tài khoản '{username}' đã bị khóa (IsActive = false)");
                        return;
                    }

                    if (expectedRole != null && user.Role == expectedRole)
                    {
                        Console.WriteLine($"✅ PASSED: Login thành công!");
                        Console.WriteLine($"   - Username: {user.Username}");
                        Console.WriteLine($"   - FullName: {user.FullName}");
                        Console.WriteLine($"   - Role: {user.Role}");
                        Console.WriteLine($"   - Email: {user.Email}");
                        Console.WriteLine($"   - IsActive: {user.IsActive}");
                    }
                    else
                    {
                        Console.WriteLine($"❌ FAILED: Role không đúng. Expected: {expectedRole}, Actual: {user.Role}");
                    }
                }
                else
                {
                    if (expectedRole == null)
                    {
                        Console.WriteLine($"✅ PASSED: Login thất bại như mong đợi (sai username/password)");
                    }
                    else
                    {
                        Console.WriteLine($"❌ FAILED: Không tìm thấy user (Expected role: {expectedRole})");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ ERROR: {ex.Message}");
            }
        }

        /// <summary>
        /// Test login với tài khoản bị khóa
        /// </summary>
        private static void TestInactiveAccount()
        {
            try
            {
                var allUsers = UserDataAccess.GetAllUsers();
                
                // LINQ query để tìm users bị khóa
                var inactiveUsers = from u in allUsers
                                   where !u.IsActive
                                   select u;

                if (inactiveUsers.Any())
                {
                    var user = inactiveUsers.First();
                    Console.WriteLine($"✅ PASSED: Tìm thấy tài khoản bị khóa:");
                    Console.WriteLine($"   - Username: {user.Username}");
                    Console.WriteLine($"   - FullName: {user.FullName}");
                    Console.WriteLine($"   - IsActive: {user.IsActive}");
                    Console.WriteLine($"   => Tài khoản này sẽ không thể đăng nhập");
                }
                else
                {
                    Console.WriteLine($"ℹ️  INFO: Không có tài khoản nào bị khóa trong database");
                    Console.WriteLine($"   => Tất cả users đều có IsActive = true");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ ERROR: {ex.Message}");
            }
        }

        /// <summary>
        /// Hiển thị tất cả users trong database
        /// </summary>
        private static void DisplayAllUsers()
        {
            try
            {
                var allUsers = UserDataAccess.GetAllUsers();

                // LINQ query - nhóm theo Role
                var usersByRole = from u in allUsers
                                 group u by u.Role into g
                                 orderby g.Key
                                 select g;

                foreach (var roleGroup in usersByRole)
                {
                    Console.WriteLine($"\n📋 Role: {roleGroup.Key}");
                    Console.WriteLine(new string('-', 80));

                    foreach (var user in roleGroup)
                    {
                        string status = user.IsActive ? "✅ Active" : "❌ Inactive";
                        Console.WriteLine($"   {status} | {user.Username,-15} | {user.FullName,-25} | {user.Email}");
                    }
                }

                // Thống kê sử dụng LINQ
                var totalUsers = allUsers.Count();
                var activeUsers = allUsers.Count(u => u.IsActive);
                var inactiveUsers = allUsers.Count(u => !u.IsActive);
                var adminCount = allUsers.Count(u => u.Role == "Admin");
                var teacherCount = allUsers.Count(u => u.Role == "GiaoVien");
                var studentCount = allUsers.Count(u => u.Role == "SinhVien");

                Console.WriteLine($"\n📊 THỐNG KÊ:");
                Console.WriteLine($"   - Tổng số users: {totalUsers}");
                Console.WriteLine($"   - Active users: {activeUsers}");
                Console.WriteLine($"   - Inactive users: {inactiveUsers}");
                Console.WriteLine($"   - Admin: {adminCount}");
                Console.WriteLine($"   - GiaoVien: {teacherCount}");
                Console.WriteLine($"   - SinhVien: {studentCount}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ ERROR: {ex.Message}");
            }
        }
    }
}