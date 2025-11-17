using System;
using System.Windows.Forms;
using StudentManagement.Helper;

namespace StudentManagement
{
    /// <summary>
    /// Test MainForm với các role khác nhau
    /// Mục đích: Kiểm tra phân quyền menu theo Role
    /// </summary>
    public class TestMainFormRoles
    {
        // Đổi tên Main() thành RunTests() để tránh conflict
        public static void RunTests()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Console.WriteLine("=== TEST MAINFORM VỚI CÁC ROLE KHÁC NHAU ===\n");

            // Test 1: Admin - Full quyền
            Console.WriteLine("TEST 1: Đăng nhập với quyền ADMIN");
            Console.WriteLine("------------------------------------");
            TestWithRole("admin", "admin123", "Admin Nguyễn Văn A", "Admin");
            Console.WriteLine();

            // Test 2: GiaoVien - Ẩn User và Backup
            Console.WriteLine("TEST 2: Đăng nhập với quyền GIÁO VIÊN");
            Console.WriteLine("----------------------------------------");
            TestWithRole("teacher", "teacher123", "Giáo viên Trần Thị B", "GiaoVien");
            Console.WriteLine();

            // Test 3: SinhVien - Chỉ xem điểm và biểu đồ
            Console.WriteLine("TEST 3: Đăng nhập với quyền SINH VIÊN");
            Console.WriteLine("---------------------------------------");
            TestWithRole("student", "student123", "Sinh viên Lê Văn C", "SinhVien");
            Console.WriteLine();

            Console.WriteLine("\n=== KẾT THÚC TEST ===");
            Console.WriteLine("\nHƯỚNG DẪN KIỂM TRA:");
            Console.WriteLine("1. Quan sát menu hiển thị khác nhau cho từng role");
            Console.WriteLine("2. Admin: Hiển thị tất cả menu");
            Console.WriteLine("3. GiaoVien: Ẩn 'Quản lý người dùng' và 'Sao lưu dữ liệu'");
            Console.WriteLine("4. SinhVien: Ẩn menu 'Quản lý', chỉ hiển thị 'Báo cáo điểm' và 'Biểu đồ'");
            Console.WriteLine("5. StatusBar: Hiển thị 'Đăng nhập: [FullName] - Quyền: [Role]'");
            Console.WriteLine("6. Thống kê: Hiển thị số lượng sinh viên, lớp học, giáo viên");
            
            Console.WriteLine("\nNhấn Enter để thoát...");
            Console.ReadLine();
        }

        private static void TestWithRole(string username, string password, string fullName, string role)
        {
            try
            {
                // Giả lập session user với role
                SessionUser.UserID = role == "Admin" ? 1 : role == "GiaoVien" ? 2 : 3;
                SessionUser.Username = username;
                SessionUser.FullName = fullName;
                SessionUser.Role = role;

                Console.WriteLine($"✓ Session thiết lập: {fullName} ({role})");
                Console.WriteLine($"  - UserID: {SessionUser.UserID}");
                Console.WriteLine($"  - Username: {SessionUser.Username}");
                Console.WriteLine($"  - IsLoggedIn: {SessionUser.IsLoggedIn()}");

                // Mở MainForm
                Console.WriteLine($"✓ Mở MainForm với role: {role}");
                Console.WriteLine("  → Kiểm tra menu được hiển thị/ẩn theo quyền");
                Console.WriteLine("  → Kiểm tra StatusBar hiển thị thông tin user");
                Console.WriteLine("  → Kiểm tra thống kê LINQ đếm dữ liệu");

                MainForm mainForm = new MainForm();
                
                // Hiển thị thông tin menu sẽ được apply
                Console.WriteLine("\nQuyền truy cập:");
                if (role == "Admin")
                {
                    Console.WriteLine("  ✓ Hệ thống: Đổi mật khẩu, Sao lưu, Đăng xuất, Thoát");
                    Console.WriteLine("  ✓ Quản lý: Sinh viên, Giáo viên, Lớp học, Điểm, Người dùng");
                    Console.WriteLine("  ✓ Báo cáo: Báo cáo sinh viên, Báo cáo điểm");
                    Console.WriteLine("  ✓ Thống kê: Biểu đồ thống kê");
                    Console.WriteLine("  ✓ Giúp đỡ: Hướng dẫn, Thông tin nhóm");
                }
                else if (role == "GiaoVien")
                {
                    Console.WriteLine("  ✓ Hệ thống: Đổi mật khẩu, Đăng xuất, Thoát");
                    Console.WriteLine("  ✗ Hệ thống: Sao lưu dữ liệu (ẨN)");
                    Console.WriteLine("  ✓ Quản lý: Sinh viên, Giáo viên, Lớp học, Điểm");
                    Console.WriteLine("  ✗ Quản lý: Người dùng (ẨN)");
                    Console.WriteLine("  ✓ Báo cáo: Báo cáo sinh viên, Báo cáo điểm");
                    Console.WriteLine("  ✓ Thống kê: Biểu đồ thống kê");
                    Console.WriteLine("  ✓ Giúp đỡ: Hướng dẫn, Thông tin nhóm");
                }
                else if (role == "SinhVien")
                {
                    Console.WriteLine("  ✓ Hệ thống: Đổi mật khẩu, Đăng xuất, Thoát");
                    Console.WriteLine("  ✗ Hệ thống: Sao lưu dữ liệu (ẨN)");
                    Console.WriteLine("  ✗ Quản lý: Tất cả (ẨN)");
                    Console.WriteLine("  ✓ Báo cáo: Báo cáo điểm");
                    Console.WriteLine("  ✗ Báo cáo: Báo cáo sinh viên (ẨN)");
                    Console.WriteLine("  ✓ Thống kê: Biểu đồ thống kê");
                    Console.WriteLine("  ✓ Giúp đỡ: Hướng dẫn, Thông tin nhóm");
                }

                Application.Run(mainForm);

                Console.WriteLine($"✓ MainForm đã đóng");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"✗ Lỗi: {ex.Message}");
                Console.WriteLine($"  Stack: {ex.StackTrace}");
            }
            finally
            {
                // Clear session sau khi test
                SessionUser.ClearSession();
                Console.WriteLine("✓ Session đã xóa");
            }
        }
    }
}
