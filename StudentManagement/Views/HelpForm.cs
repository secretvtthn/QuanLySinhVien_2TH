using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace StudentManagement.Views
{
    public partial class HelpForm : Form
    {
        private Dictionary<string, string> helpContent = new Dictionary<string, string>();

        public HelpForm()
        {
            InitializeComponent();
            InitializeHelpContent();
        }

        private void HelpForm_Load(object sender, EventArgs e)
        {
            LoadTreeView();
            richTextBox.Text = "📚 Chào mừng bạn đến với Hướng dẫn sử dụng Hệ thống Quản lý Sinh viên!\n\n" +
                              "👈 Vui lòng chọn một mục trong danh sách bên trái để xem hướng dẫn chi tiết.\n\n" +
                              "Hệ thống hỗ trợ 3 quyền người dùng:\n" +
                              "• Admin - Quản trị viên hệ thống\n" +
                              "• Teacher - Giáo viên\n" +
                              "• Student - Sinh viên";
        }

        private void InitializeHelpContent()
        {
            helpContent = new Dictionary<string, string>
            {
                // ADMIN ROLE
                ["admin_overview"] = @"📋 TỔNG QUAN QUYỀN ADMIN

Quyền Admin có toàn quyền quản lý hệ thống, bao gồm quản lý người dùng, sinh viên, giáo viên, lớp học và điểm số. Admin có thể thực hiện tất cả các chức năng trong hệ thống như thêm, sửa, xóa dữ liệu, xem báo cáo thống kê và biểu đồ. Admin chịu trách nhiệm duy trì tính toàn vẹn của dữ liệu và đảm bảo hệ thống hoạt động ổn định. Ngoài ra, Admin có thể quản lý tài khoản người dùng, cấp quyền truy cập và theo dõi hoạt động của tất cả người dùng trong hệ thống.",

                ["admin_user"] = @"👥 QUẢN LÝ NGƯỜI DÙNG

Admin có thể quản lý tất cả tài khoản người dùng trong hệ thống. Chức năng bao gồm: tạo tài khoản mới với các quyền khác nhau (Admin, Teacher, Student), chỉnh sửa thông tin người dùng như tên, email, số điện thoại, mật khẩu, xóa hoặc vô hiệu hóa tài khoản khi cần thiết. Admin cần đảm bảo mỗi tài khoản được cấp đúng quyền hạn theo vai trò của họ. Khi tạo tài khoản mới, cần nhập đầy đủ thông tin và chọn quyền phù hợp để đảm bảo bảo mật hệ thống.",

                ["admin_student"] = @"🎓 QUẢN LÝ SINH VIÊN

Chức năng quản lý sinh viên cho phép Admin thêm mới sinh viên vào hệ thống với đầy đủ thông tin cá nhân như mã sinh viên, họ tên, ngày sinh, địa chỉ, email, số điện thoại và lớp học. Admin có thể cập nhật thông tin sinh viên khi có thay đổi, xóa sinh viên khỏi hệ thống nếu cần thiết, tìm kiếm sinh viên theo nhiều tiêu chí khác nhau. Ngoài ra, Admin có thể xuất danh sách sinh viên ra file Excel để lưu trữ hoặc báo cáo. Cần chú ý kiểm tra kỹ thông tin trước khi lưu để tránh sai sót.",

                ["admin_teacher"] = @"👨‍🏫 QUẢN LÝ GIÁO VIÊN

Admin quản lý thông tin giáo viên trong hệ thống bao gồm thêm mới giáo viên với thông tin như mã giáo viên, họ tên, khoa, môn giảng dạy, email, số điện thoại. Có thể cập nhật thông tin giáo viên khi có thay đổi về chuyên môn hoặc thông tin liên lạc, xóa giáo viên khỏi hệ thống khi nghỉ việc. Admin cũng có thể tìm kiếm giáo viên theo tên, khoa hoặc môn học, xuất danh sách giáo viên ra file Excel. Việc quản lý chính xác thông tin giáo viên giúp dễ dàng phân công giảng dạy và liên lạc.",

                ["admin_class"] = @"🏫 QUẢN LÝ LỚP HỌC

Quản lý lớp học cho phép Admin tạo lớp học mới với thông tin như mã lớp, tên lớp, khóa học, năm học, giáo viên chủ nhiệm. Admin có thể chỉnh sửa thông tin lớp, thay đổi giáo viên chủ nhiệm khi cần thiết, xóa lớp học nếu không còn sử dụng. Có thể xem danh sách sinh viên trong từng lớp, phân công sinh viên vào lớp phù hợp. Chức năng này giúp tổ chức quản lý sinh viên theo lớp học một cách khoa học và hiệu quả, dễ dàng theo dõi và thống kê.",

                ["admin_score"] = @"📊 QUẢN LÝ ĐIỂM SỐ

Admin có toàn quyền quản lý điểm số của sinh viên bao gồm nhập điểm cho sinh viên theo từng môn học (điểm chuyên cần, điểm giữa kỳ, điểm cuối kỳ), cập nhật và sửa đổi điểm số khi cần thiết, xóa bản ghi điểm nếu có sai sót. Admin có thể xem điểm của tất cả sinh viên, tìm kiếm điểm theo sinh viên hoặc môn học, xuất bảng điểm ra file Excel để lưu trữ và báo cáo. Cần kiểm tra kỹ điểm số trước khi lưu vì điểm số ảnh hưởng trực tiếp đến kết quả học tập của sinh viên.",

                ["admin_report"] = @"📈 BÁO CÁO VÀ THỐNG KÊ

Admin có thể xem các báo cáo và thống kê tổng quan về hệ thống như báo cáo danh sách sinh viên theo lớp, khoa, khóa học, báo cáo điểm số theo môn học hoặc theo sinh viên, thống kê số lượng sinh viên, giáo viên, lớp học. Hệ thống cung cấp biểu đồ trực quan về phân bố điểm số, tỷ lệ đậu/rớt, xu hướng kết quả học tập theo thời gian. Admin có thể xuất các báo cáo này ra file Excel để trình bày hoặc lưu trữ. Các báo cáo giúp đánh giá tình hình hoạt động và đưa ra quyết định quản lý phù hợp.",

                // TEACHER ROLE
                ["teacher_overview"] = @"📚 TỔNG QUAN QUYỀN TEACHER

Quyền Teacher (Giáo viên) được thiết kế để hỗ trợ công việc giảng dạy và quản lý học sinh. Giáo viên có thể xem danh sách sinh viên trong các lớp mình phụ trách, nhập và cập nhật điểm số cho sinh viên, xem báo cáo kết quả học tập. Giáo viên không có quyền xóa dữ liệu quan trọng hoặc quản lý người dùng nhằm đảm bảo tính toàn vẹn của hệ thống. Các chức năng được tối ưu hóa để giáo viên có thể thực hiện công việc hàng ngày một cách nhanh chóng và hiệu quả, tập trung vào việc theo dõi và đánh giá kết quả học tập của sinh viên.",

                ["teacher_student"] = @"🎓 XEM THÔNG TIN SINH VIÊN

Giáo viên có thể xem thông tin chi tiết của sinh viên bao gồm thông tin cá nhân như họ tên, mã sinh viên, lớp học, ngày sinh, địa chỉ liên lạc. Có thể tìm kiếm sinh viên theo tên, mã sinh viên hoặc lớp học để tra cứu nhanh chóng. Giáo viên có thể xem lịch sử điểm số của sinh viên để theo dõi quá trình học tập, xuất danh sách sinh viên ra Excel khi cần thiết. Tuy nhiên, giáo viên không có quyền thêm, sửa, xóa thông tin sinh viên, chỉ có quyền xem để đảm bảo dữ liệu không bị thay đổi ngoài ý muốn.",

                ["teacher_score"] = @"📝 NHẬP VÀ QUẢN LÝ ĐIỂM

Giáo viên có quyền nhập điểm cho sinh viên trong các lớp mình giảng dạy. Có thể nhập điểm chuyên cần, điểm giữa kỳ, điểm cuối kỳ cho từng sinh viên theo môn học. Giáo viên có thể cập nhật và sửa đổi điểm số trong khoảng thời gian cho phép, xem bảng điểm tổng hợp của lớp để đánh giá tổng quan. Hệ thống tự động tính điểm trung bình và xếp loại học lực. Giáo viên cần nhập điểm chính xác và kịp thời theo quy định của nhà trường. Có thể xuất bảng điểm ra Excel để lưu trữ hoặc in ấn.",

                ["teacher_class"] = @"🏫 XEM THÔNG TIN LỚP HỌC

Giáo viên có thể xem thông tin các lớp học mình phụ trách bao gồm danh sách sinh viên trong lớp, thông tin giáo viên chủ nhiệm, khóa học, năm học. Có thể xem sĩ số lớp, thống kê kết quả học tập chung của lớp, theo dõi tình hình điểm danh và học tập. Giáo viên có thể tìm kiếm và lọc thông tin theo nhiều tiêu chí khác nhau để phục vụ công tác giảng dạy. Mặc dù không có quyền tạo hoặc xóa lớp học, giáo viên có đầy đủ thông tin để thực hiện công tác quản lý lớp hiệu quả.",

                ["teacher_report"] = @"📊 XEM BÁO CÁO HỌC TẬP

Giáo viên có thể truy cập các báo cáo về kết quả học tập của sinh viên trong các lớp mình giảng dạy. Bao gồm báo cáo điểm số theo môn học, báo cáo thống kê phân bố điểm (giỏi, khá, trung bình, yếu, kém), biểu đồ trực quan về kết quả học tập của lớp. Giáo viên có thể so sánh kết quả giữa các lớp, theo dõi xu hướng thay đổi điểm số qua các kỳ học. Các báo cáo giúp giáo viên đánh giá hiệu quả giảng dạy, phát hiện sinh viên cần hỗ trợ thêm và điều chỉnh phương pháp giảng dạy cho phù hợp.",

                ["teacher_profile"] = @"👤 QUẢN LÝ THÔNG TIN CÁ NHÂN

Giáo viên có thể xem và cập nhật thông tin cá nhân của mình như họ tên, email, số điện thoại, địa chỉ. Có thể thay đổi mật khẩu đăng nhập định kỳ để đảm bảo bảo mật tài khoản. Nên cập nhật thông tin liên lạc chính xác để nhà trường có thể liên hệ khi cần thiết. Giáo viên cũng có thể xem thông tin về các môn học đang giảng dạy, lịch trình công tác. Việc duy trì thông tin cá nhân cập nhật giúp hệ thống hoạt động trơn tru và thuận tiện cho việc liên lạc, phối hợp công tác.",

                // STUDENT ROLE
                ["student_overview"] = @"📖 TỔNG QUAN QUYỀN STUDENT

Quyền Student (Sinh viên) cho phép sinh viên tra cứu thông tin học tập cá nhân, xem điểm số, kết quả học tập và thông tin lớp học. Sinh viên có quyền xem nhưng không được phép thay đổi dữ liệu để đảm bảo tính chính xác của thông tin. Giao diện được thiết kế đơn giản, dễ sử dụng để sinh viên có thể nhanh chóng tra cứu các thông tin cần thiết. Sinh viên có thể theo dõi quá trình học tập của mình, xem lịch sử điểm số qua các học kỳ, xuất bảng điểm để lưu trữ. Hệ thống giúp sinh viên chủ động trong việc theo dõi kết quả học tập và lập kế hoạch học tập hiệu quả.",

                ["student_profile"] = @"👤 XEM THÔNG TIN CÁ NHÂN

Sinh viên có thể xem thông tin cá nhân của mình bao gồm mã sinh viên, họ tên, ngày sinh, giới tính, địa chỉ, email, số điện thoại, lớp học đang theo học, khóa học. Sinh viên có thể cập nhật một số thông tin cá nhân như email, số điện thophone, địa chỉ liên lạc khi có thay đổi. Có thể thay đổi mật khẩu đăng nhập để bảo mật tài khoản. Nên kiểm tra và cập nhật thông tin liên lạc thường xuyên để nhận được thông báo từ nhà trường. Thông tin cá nhân chính xác giúp nhà trường dễ dàng liên lạc và hỗ trợ sinh viên khi cần thiết.",

                ["student_score"] = @"📊 XEM ĐIỂM SỐ CÁ NHÂN

Sinh viên có thể xem điểm số của mình theo từng môn học bao gồm điểm chuyên cần, điểm giữa kỳ, điểm cuối kỳ, điểm trung bình môn học. Có thể xem điểm theo từng học kỳ hoặc xem tổng hợp điểm của tất cả các học kỳ. Hệ thống hiển thị điểm trung bình tích lũy (GPA), xếp loại học lực (Giỏi, Khá, Trung bình, Yếu, Kém). Sinh viên có thể xuất bảng điểm ra file Excel để lưu trữ hoặc in ấn. Việc theo dõi điểm số thường xuyên giúp sinh viên nắm rõ kết quả học tập và có kế hoạch cải thiện khi cần thiết.",

                ["student_class"] = @"🏫 XEM THÔNG TIN LỚP HỌC

Sinh viên có thể xem thông tin về lớp học mình đang theo học như tên lớp, mã lớp, khóa học, năm học, giáo viên chủ nhiệm. Có thể xem danh sách các bạn cùng lớp với thông tin liên lạc cơ bản, sĩ số lớp học. Sinh viên có thể xem lịch trình học tập, thời khóa biểu của lớp nếu hệ thống có cung cấp. Thông tin này giúp sinh viên dễ dàng liên lạc với bạn bè, giáo viên và nắm rõ các hoạt động của lớp. Sinh viên nên theo dõi thông tin lớp học thường xuyên để không bỏ lỡ các thông báo quan trọng.",

                ["student_report"] = @"📈 XEM BÁO CÁO KẾT QUẢ HỌC TẬP

Sinh viên có thể xem các báo cáo tổng hợp về kết quả học tập của bản thân bao gồm báo cáo điểm số theo học kỳ, năm học, biểu đồ thể hiện sự tiến bộ qua các kỳ học, thống kê số tín chỉ đã hoàn thành, số tín chỉ còn lại. Có thể xem bảng xếp hạng trong lớp (nếu được phép), so sánh kết quả của mình với điểm trung bình của lớp. Các báo cáo giúp sinh viên tự đánh giá năng lực học tập, nhận biết điểm mạnh điểm yếu để có kế hoạch học tập phù hợp và đạt kết quả tốt hơn trong các kỳ học tiếp theo.",

                ["student_support"] = @"❓ HỖ TRỢ VÀ LIÊN HỆ

Sinh viên có thể sử dụng chức năng hỗ trợ để tra cứu thông tin, hướng dẫn sử dụng hệ thống. Nếu gặp vấn đề về điểm số hoặc thông tin cá nhân, sinh viên cần liên hệ với giáo viên chủ nhiệm hoặc phòng đào tạo. Có thể gửi phản hồi, góp ý về hệ thống để cải thiện chất lượng dịch vụ. Sinh viên nên lưu lại thông tin liên lạc của giáo viên, phòng ban để dễ dàng liên hệ khi cần hỗ trợ. Hệ thống cũng cung cấp các câu hỏi thường gặp (FAQ) để giải đáp các thắc mắc phổ biến một cách nhanh chóng."
            };
        }

        private void LoadTreeView()
        {
            treeView.Nodes.Clear();

            // ADMIN Node
            TreeNode adminNode = new TreeNode("🔐 ADMIN - Quản trị viên");
            adminNode.Nodes.Add("admin_overview", "📋 Tổng quan quyền Admin");
            adminNode.Nodes.Add("admin_user", "👥 Quản lý người dùng");
            adminNode.Nodes.Add("admin_student", "🎓 Quản lý sinh viên");
            adminNode.Nodes.Add("admin_teacher", "👨‍🏫 Quản lý giáo viên");
            adminNode.Nodes.Add("admin_class", "🏫 Quản lý lớp học");
            adminNode.Nodes.Add("admin_score", "📊 Quản lý điểm số");
            adminNode.Nodes.Add("admin_report", "📈 Báo cáo và thống kê");
            
            // TEACHER Node
            TreeNode teacherNode = new TreeNode("👨‍🏫 TEACHER - Giáo viên");
            teacherNode.Nodes.Add("teacher_overview", "📚 Tổng quan quyền Teacher");
            teacherNode.Nodes.Add("teacher_student", "🎓 Xem thông tin sinh viên");
            teacherNode.Nodes.Add("teacher_score", "📝 Nhập và quản lý điểm");
            teacherNode.Nodes.Add("teacher_class", "🏫 Xem thông tin lớp học");
            teacherNode.Nodes.Add("teacher_report", "📊 Xem báo cáo học tập");
            teacherNode.Nodes.Add("teacher_profile", "👤 Quản lý thông tin cá nhân");
            
            // STUDENT Node
            TreeNode studentNode = new TreeNode("🎓 STUDENT - Sinh viên");
            studentNode.Nodes.Add("student_overview", "📖 Tổng quan quyền Student");
            studentNode.Nodes.Add("student_profile", "👤 Xem thông tin cá nhân");
            studentNode.Nodes.Add("student_score", "📊 Xem điểm số cá nhân");
            studentNode.Nodes.Add("student_class", "🏫 Xem thông tin lớp học");
            studentNode.Nodes.Add("student_report", "📈 Xem báo cáo kết quả học tập");
            studentNode.Nodes.Add("student_support", "❓ Hỗ trợ và liên hệ");

            treeView.Nodes.Add(adminNode);
            treeView.Nodes.Add(teacherNode);
            treeView.Nodes.Add(studentNode);

            // Expand all nodes
            treeView.ExpandAll();
        }

        private void treeView_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (!string.IsNullOrEmpty(e.Node?.Name))
            {
                if (helpContent.ContainsKey(e.Node.Name))
                {
                    richTextBox.Clear();
                    richTextBox.SelectionFont = new Font("Segoe UI", 11F, FontStyle.Bold);
                    richTextBox.SelectionColor = Color.FromArgb(41, 128, 185);
                    
                    // Add title with icon
                    richTextBox.AppendText(e.Node.Text + "\n");
                    richTextBox.AppendText("═══════════════════════════════════════\n\n");
                    
                    // Add content
                    richTextBox.SelectionFont = new Font("Segoe UI", 10F, FontStyle.Regular);
                    richTextBox.SelectionColor = Color.FromArgb(44, 62, 80);
                    richTextBox.AppendText(helpContent[e.Node.Name]);
                    
                    // Add footer
                    richTextBox.AppendText("\n\n═══════════════════════════════════════\n");
                    richTextBox.SelectionFont = new Font("Segoe UI", 9F, FontStyle.Italic);
                    richTextBox.SelectionColor = Color.FromArgb(127, 140, 141);
                    richTextBox.AppendText("💡 Tip: Chọn các mục khác trong danh sách để xem thêm hướng dẫn.");
                    
                    lblStatus.Text = $"✅ Đang xem: {e.Node.Text}";
                }
                else
                {
                    lblStatus.Text = "⚠️ Chưa có nội dung hướng dẫn cho mục này";
                }
            }
            else
            {
                lblStatus.Text = "ℹ️ Vui lòng chọn một mục con để xem hướng dẫn chi tiết";
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
