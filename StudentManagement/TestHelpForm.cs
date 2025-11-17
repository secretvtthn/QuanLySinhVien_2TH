using System;
using System.Windows.Forms;
using StudentManagement.Views;

namespace StudentManagement
{
    public class TestHelpForm
    {
        [STAThread]
        public static void TestMain(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Console.WriteLine("=== TEST HELP FORM ===");
            Console.WriteLine("Mở HelpForm...");
            Console.WriteLine();
            Console.WriteLine("HƯỚNG DẪN KIỂM TRA:");
            Console.WriteLine("1. Form hiển thị với TreeView bên trái và RichTextBox bên phải");
            Console.WriteLine("2. TreeView có 3 nhánh chính:");
            Console.WriteLine("   - ADMIN - Quản trị viên (7 mục con)");
            Console.WriteLine("   - TEACHER - Giáo viên (6 mục con)");
            Console.WriteLine("   - STUDENT - Sinh viên (6 mục con)");
            Console.WriteLine("3. Click vào từng mục trong TreeView");
            Console.WriteLine("4. Kiểm tra nội dung hiển thị trong RichTextBox");
            Console.WriteLine("5. Mỗi quyền có hướng dẫn khoảng 150-200 chữ");
            Console.WriteLine("6. Kiểm tra status bar ở dưới cùng");
            Console.WriteLine("7. Nhấn nút 'Đóng' để thoát");
            Console.WriteLine();
            Console.WriteLine("CÁC MỤC CẦN KIỂM TRA:");
            Console.WriteLine();
            Console.WriteLine("📌 ADMIN:");
            Console.WriteLine("   ✓ Tổng quan quyền Admin");
            Console.WriteLine("   ✓ Quản lý người dùng");
            Console.WriteLine("   ✓ Quản lý sinh viên");
            Console.WriteLine("   ✓ Quản lý giáo viên");
            Console.WriteLine("   ✓ Quản lý lớp học");
            Console.WriteLine("   ✓ Quản lý điểm số");
            Console.WriteLine("   ✓ Báo cáo và thống kê");
            Console.WriteLine();
            Console.WriteLine("📌 TEACHER:");
            Console.WriteLine("   ✓ Tổng quan quyền Teacher");
            Console.WriteLine("   ✓ Xem thông tin sinh viên");
            Console.WriteLine("   ✓ Nhập và quản lý điểm");
            Console.WriteLine("   ✓ Xem thông tin lớp học");
            Console.WriteLine("   ✓ Xem báo cáo học tập");
            Console.WriteLine("   ✓ Quản lý thông tin cá nhân");
            Console.WriteLine();
            Console.WriteLine("📌 STUDENT:");
            Console.WriteLine("   ✓ Tổng quan quyền Student");
            Console.WriteLine("   ✓ Xem thông tin cá nhân");
            Console.WriteLine("   ✓ Xem điểm số cá nhân");
            Console.WriteLine("   ✓ Xem thông tin lớp học");
            Console.WriteLine("   ✓ Xem báo cáo kết quả học tập");
            Console.WriteLine("   ✓ Hỗ trợ và liên hệ");
            Console.WriteLine();
            Console.WriteLine("TIÊU CHÍ ĐÁNH GIÁ:");
            Console.WriteLine("✅ TreeView hiển thị đầy đủ 3 quyền với các mục con");
            Console.WriteLine("✅ Click vào mục → RichTextBox hiển thị nội dung tương ứng");
            Console.WriteLine("✅ Nội dung có format đẹp (tiêu đề in đậm, màu sắc)");
            Console.WriteLine("✅ Mỗi hướng dẫn có 150-200 chữ (khoảng 10 dòng)");
            Console.WriteLine("✅ Status bar cập nhật khi chọn mục");
            Console.WriteLine("✅ Nút 'Đóng' hoạt động");
            Console.WriteLine();

            try
            {
                HelpForm form = new HelpForm();
                Application.Run(form);
                
                Console.WriteLine();
                Console.WriteLine("✅ TEST HOÀN TẤT!");
                Console.WriteLine("Form đã đóng thành công.");
            }
            catch (Exception ex)
            {
                Console.WriteLine();
                Console.WriteLine("❌ LỖI KHI CHẠY TEST:");
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
            }

            Console.WriteLine();
            Console.WriteLine("Nhấn phím bất kỳ để thoát...");
            Console.ReadKey();
        }
    }
}
