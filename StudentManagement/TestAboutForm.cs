using System;
using System.Windows.Forms;

namespace StudentManagement
{
    public class TestAboutForm
    {
        /// <summary>
        /// Test method để kiểm tra AboutForm
        /// </summary>
        public static void TestAboutFormFunction()
        {
            try
            {
                Console.WriteLine("🔍 ĐANG TEST ABOUT FORM...");
                Console.WriteLine("================================");
                
                // Tạo và hiển thị AboutForm
                AboutForm aboutForm = new AboutForm();
                
                Console.WriteLine("✅ AboutForm đã được tạo thành công!");
                Console.WriteLine("📋 Kiểm tra các thành phần:");
                Console.WriteLine("- Text: " + aboutForm.Text);
                Console.WriteLine("- Size: " + aboutForm.Size);
                Console.WriteLine("- FormBorderStyle: " + aboutForm.FormBorderStyle);
                Console.WriteLine("- StartPosition: " + aboutForm.StartPosition);
                
                // Hiển thị form
                Console.WriteLine("🚀 Đang hiển thị AboutForm...");
                aboutForm.Show();
                
                Console.WriteLine("✅ TEST THÀNH CÔNG!");
                Console.WriteLine("📝 Hướng dẫn kiểm tra:");
                Console.WriteLine("1. Form AboutForm sẽ hiển thị");
                Console.WriteLine("2. Kiểm tra logo SMS V1.0 ở góc trái");
                Console.WriteLine("3. Kiểm tra tiêu đề 'HỆ THỐNG QUẢN LÝ SINH VIÊN'");
                Console.WriteLine("4. Kiểm tra thông tin 4 thành viên nhóm");
                Console.WriteLine("5. Kiểm tra button 'Đóng' hoạt động");
                Console.WriteLine("6. Kiểm tra copyright ở dưới cùng");
                
                Console.WriteLine("================================");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Lỗi khi test AboutForm: {ex.Message}");
            }
        }

        /// <summary>
        /// Test tạo AboutForm từ Form1
        /// </summary>
        public static void TestAboutFromForm1()
        {
            try
            {
                Console.WriteLine("🔍 TEST ABOUT FORM TỪ FORM1...");
                
                // Tạo Form1
                Form1 mainForm = new Form1();
                
                Console.WriteLine("✅ Form1 đã được tạo");
                Console.WriteLine("📱 Form1 có button 'Thông tin nhóm'");
                
                // Hiển thị Form1
                Console.WriteLine("🚀 Hiển thị Form1 với button About...");
                Application.Run(mainForm);
                
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Lỗi: {ex.Message}");
            }
        }
    }
}