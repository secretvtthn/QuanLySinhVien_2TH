using System;
using System.Windows.Forms;
using StudentManagement.Views;
using StudentManagement.Helper;
using StudentManagement.Models;

namespace StudentManagement
{
    /// <summary>
    /// Test ChangePasswordForm - PROMPT 19
    /// </summary>
    public class TestChangePasswordForm
    {
        [STAThread]
        public static void TestMain()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Console.WriteLine("========================================");
            Console.WriteLine("    TEST CHANGE PASSWORD FORM");
            Console.WriteLine("========================================");
            Console.WriteLine();
            Console.WriteLine("HƯỚNG DẪN KIỂM TRA:");
            Console.WriteLine("1. Form hiển thị với 3 ô nhập liệu:");
            Console.WriteLine("   - Mật khẩu hiện tại");
            Console.WriteLine("   - Mật khẩu mới");
            Console.WriteLine("   - Xác nhận mật khẩu mới");
            Console.WriteLine("2. Checkbox 'Hiển thị mật khẩu'");
            Console.WriteLine("3. Kiểm tra validation:");
            Console.WriteLine("   - Bỏ trống các trường");
            Console.WriteLine("   - Mật khẩu mới < 6 ký tự");
            Console.WriteLine("   - Mật khẩu xác nhận không khớp");
            Console.WriteLine("   - Mật khẩu hiện tại sai");
            Console.WriteLine("4. Test case thành công:");
            Console.WriteLine("   - Nhập đúng mật khẩu cũ");
            Console.WriteLine("   - Mật khẩu mới hợp lệ");
            Console.WriteLine("   - Xác nhận khớp");
            Console.WriteLine();
            Console.WriteLine("THÔNG TIN TEST:");
            Console.WriteLine("- Cần đăng nhập trước khi test");
            Console.WriteLine("- SessionUser phải được set");
            Console.WriteLine();

            try
            {
                // Giả lập đăng nhập để có SessionUser
                Console.WriteLine("Đang giả lập đăng nhập với user 'admin'...");
                var testUser = new User
                {
                    UserID = 1,
                    Username = "admin",
                    Password = "admin123",
                    FullName = "Administrator",
                    Role = "Admin",
                    Email = "admin@example.com",
                    IsActive = true
                };

                SessionUser.Login(testUser);
                Console.WriteLine($"✅ Đã đăng nhập: {SessionUser.CurrentUser?.FullName}");
                Console.WriteLine($"   UserID: {SessionUser.UserID}");
                Console.WriteLine($"   Role: {SessionUser.Role}");
                Console.WriteLine();

                Console.WriteLine("Mở ChangePasswordForm...");
                Console.WriteLine();
                Console.WriteLine("TEST CASE CẦN KIỂM TRA:");
                Console.WriteLine();
                Console.WriteLine("📌 TEST VALIDATION:");
                Console.WriteLine("   1. Bỏ trống 'Mật khẩu hiện tại' → Hiển thị cảnh báo");
                Console.WriteLine("   2. Bỏ trống 'Mật khẩu mới' → Hiển thị cảnh báo");
                Console.WriteLine("   3. Mật khẩu mới < 6 ký tự → Hiển thị cảnh báo");
                Console.WriteLine("   4. Bỏ trống 'Xác nhận mật khẩu' → Hiển thị cảnh báo");
                Console.WriteLine("   5. Mật khẩu xác nhận khác mật khẩu mới → Hiển thị cảnh báo");
                Console.WriteLine();
                Console.WriteLine("📌 TEST LOGIC:");
                Console.WriteLine("   6. Nhập sai mật khẩu hiện tại → Hiển thị lỗi");
                Console.WriteLine("   7. Mật khẩu mới trùng mật khẩu cũ → Hiển thị cảnh báo");
                Console.WriteLine();
                Console.WriteLine("📌 TEST THÀNH CÔNG:");
                Console.WriteLine("   8. Nhập đúng thông tin:");
                Console.WriteLine("      - Mật khẩu hiện tại: admin123");
                Console.WriteLine("      - Mật khẩu mới: newpass123");
                Console.WriteLine("      - Xác nhận: newpass123");
                Console.WriteLine("      → Đổi mật khẩu thành công");
                Console.WriteLine();
                Console.WriteLine("📌 TEST UI:");
                Console.WriteLine("   9. Check/Uncheck 'Hiển thị mật khẩu' → Ẩn/hiện mật khẩu");
                Console.WriteLine("   10. Nhấn 'Hủy' → Đóng form");
                Console.WriteLine();
                Console.WriteLine("TIÊU CHÍ ĐÁNH GIÁ:");
                Console.WriteLine("✅ Form hiển thị đúng tiêu đề với tên người dùng");
                Console.WriteLine("✅ Validation hoạt động đúng");
                Console.WriteLine("✅ Kiểm tra mật khẩu cũ chính xác");
                Console.WriteLine("✅ Kiểm tra mật khẩu mới và xác nhận khớp nhau");
                Console.WriteLine("✅ Cập nhật database thành công");
                Console.WriteLine("✅ Hiển thị thông báo phù hợp");
                Console.WriteLine("✅ Status bar cập nhật");
                Console.WriteLine();

                ChangePasswordForm form = new ChangePasswordForm();
                var result = form.ShowDialog();

                Console.WriteLine();
                if (result == DialogResult.OK)
                {
                    Console.WriteLine("✅ TEST HOÀN TẤT - Đổi mật khẩu thành công!");
                    Console.WriteLine("   Mật khẩu mới đã được lưu vào database.");
                }
                else
                {
                    Console.WriteLine("ℹ️  Form đã đóng (Cancel hoặc Close)");
                }

                // Logout
                SessionUser.Logout();
                Console.WriteLine("Đã đăng xuất.");
            }
            catch (Exception ex)
            {
                Console.WriteLine();
                Console.WriteLine("❌ LỖI KHI CHẠY TEST:");
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
            }

            Console.WriteLine();
            Console.WriteLine("========================================");
            Console.WriteLine("Nhấn phím bất kỳ để thoát...");
            Console.ReadKey();
        }
    }
}
