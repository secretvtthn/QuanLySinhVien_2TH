using StudentManagement.Helper;
using StudentManagement.DAL;
using StudentManagement.Views;

namespace StudentManagement
{
    public partial class MainForm : Form
    {
        private readonly StudentRepository studentRepository;

        public MainForm()
        {
            InitializeComponent();
            this.Load += MainForm_Load;

            studentRepository = new StudentRepository();
        }

        private void MainForm_Load(object? sender, EventArgs e)
        {
            // Kiểm tra đã login chưa
            if (!SessionUser.IsLoggedIn())
            {
                MessageBox.Show("Vui lòng đăng nhập trước!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.Close();
                return;
            }

            // Cập nhật StatusBar với thông tin user
            UpdateStatusBar();

            // Phân quyền menu theo role
            ApplyRoleBasedMenuPermissions();

            ApplyRoleBasedCardsPermissions();

            // Load thống kê sử dụng LINQ
            LoadStatistics();
        }

        private void ApplyRoleBasedCardsPermissions()
        {
            // Tìm tất cả Panel cards trong form
            var allPanels = pnlMain.Controls.OfType<Panel>()
                .SelectMany(p => p.Controls.OfType<Panel>())
                .SelectMany(p => p.Controls.OfType<Panel>())
                .Where(p => p.Size.Width == 215 && p.Size.Height == 145);

            if (SessionUser.Role == "GiaoVien")
            {
                // ✅ TEACHER: ẨN card "Quản lý người dùng" (x=900, y=110)
                var cardUser = FindCardByPosition(900, 110);
                if (cardUser != null) cardUser.Visible = false;
            }
            else if (SessionUser.Role == "SinhVien")
            {
                // ✅ STUDENT: ẨN một số cards trong QUẢN LÝ
                // GIỮ: Sinh viên (x=0), Lớp học (x=450), Điểm (x=675)
                // ẨN: Giáo viên (x=225), Người dùng (x=900)
                var cardTeacher = FindCardByPosition(225, 110);
                if (cardTeacher != null) cardTeacher.Visible = false;

                var cardUser = FindCardByPosition(900, 110);
                if (cardUser != null) cardUser.Visible = false;

                // ✅ STUDENT: ẨN một số cards trong BÁO CÁO
                // GIỮ: Báo cáo điểm (x=450)
                // ẨN: Báo cáo sinh viên (x=225), Báo cáo lớp học (x=675)
                var cardReportStudent = FindCardByPosition(225, 300);
                if (cardReportStudent != null) cardReportStudent.Visible = false;

                var cardReportClass = FindCardByPosition(675, 300);
                if (cardReportClass != null) cardReportClass.Visible = false;

                // ❌ STUDENT: ẨN TOÀN BỘ phần BIỂU ĐỒ (không cần thiết)
                HideCardsByYPosition(490); // Hàng 3
            }
        }

        private Panel? FindCardByPosition(int x, int y)
        {
            return FindAllCards().FirstOrDefault(p => p.Location.X == x && p.Location.Y == y);
        }

        private void HideCardsByYPosition(int y)
        {
            var cards = FindAllCards().Where(p => p.Location.Y == y);
            foreach (var card in cards)
            {
                card.Visible = false;
            }
        }

        private IEnumerable<Panel> FindAllCards()
        {
            var pnlMain = this.Controls.OfType<Panel>().FirstOrDefault(p => p.Dock == DockStyle.Fill);
            if (pnlMain == null) return Enumerable.Empty<Panel>();

            var pnlContainer = pnlMain.Controls.OfType<Panel>().FirstOrDefault();
            if (pnlContainer == null) return Enumerable.Empty<Panel>();

            return pnlContainer.Controls.OfType<Panel>()
                .Where(p => p.Size.Width == 215 && p.Size.Height == 145);
        }

        private void UpdateStatusBar()
        {
            var statusStrip = this.Controls.OfType<StatusStrip>().FirstOrDefault();
            if (statusStrip != null)
            {
                var lblUser = statusStrip.Items["lblUser"] as ToolStripStatusLabel;
                if (lblUser != null)
                {
                    lblUser.Text = $"Đăng nhập: {SessionUser.FullName} - Quyền: {SessionUser.Role}";
                }
            }
        }

        private void ApplyRoleBasedMenuPermissions()
        {
            var menuStrip = this.Controls.OfType<MenuStrip>().FirstOrDefault();
            if (menuStrip == null) return;

            var menuManagement = menuStrip.Items.OfType<ToolStripMenuItem>().FirstOrDefault(m => m.Text == "Quản lý");
            var menuReport = menuStrip.Items.OfType<ToolStripMenuItem>().FirstOrDefault(m => m.Text == "Báo cáo");
            var menuSystem = menuStrip.Items.OfType<ToolStripMenuItem>().FirstOrDefault(m => m.Text == "Hệ thống");
            var menuStats = menuStrip.Items.OfType<ToolStripMenuItem>().FirstOrDefault(m => m.Text == "Thống kê");
            if (SessionUser.Role == "Admin")
            {
                // Admin: Full quyền
            }
            else if (SessionUser.Role == "GiaoVien")
            {
                // ✅ TEACHER: Được xem tất cả sinh viên, giáo viên, lớp học
                // ✅ TEACHER: Được quản lý điểm
                // ❌ TEACHER: KHÔNG được quản lý người dùng
                if (menuManagement != null)
                {
                    var menuUser = menuManagement.DropDownItems.OfType<ToolStripMenuItem>().FirstOrDefault(m => m.Text?.Contains("người dùng") == true); if (menuUser != null) menuUser.Visible = false;
                }

                if (menuSystem != null)
                {
                    var menuBackup = menuSystem.DropDownItems.OfType<ToolStripMenuItem>().FirstOrDefault(m => m.Text?.Contains("Sao lưu") == true);
                    if (menuBackup != null) menuBackup.Visible = false;
                }
            }
            else if (SessionUser.Role == "SinhVien")
            {
                // ✅ STUDENT: CHỈ HIỆN một số mục trong "Quản lý"
                if (menuManagement != null)
                {
                    foreach (var item in menuManagement.DropDownItems.OfType<ToolStripMenuItem>())
                    {
                        // ẨN tất cả NGOẠI TRỪ: Sinh viên (xem thông tin mình), Điểm (xem điểm mình), Lớp học (xem thông tin lớp)
                        if (!(item.Text?.Contains("sinh viên") == true) &&
                            !(item.Text?.Contains("điểm") == true) &&
                            !(item.Text?.Contains("lớp học") == true))
                        {
                            item.Visible = false;
                        }
                    }
                }

                // ✅ STUDENT: CHỈ HIỆN "Báo cáo điểm" trong menu Báo cáo
                if (menuReport != null)
                {
                    foreach (var item in menuReport.DropDownItems.OfType<ToolStripMenuItem>())
                    {
                        // CHỈ HIỆN báo cáo điểm
                        if (!(item.Text?.Contains("điểm") == true))
                        {
                            item.Visible = false;
                        }
                    }
                }

                // ❌ STUDENT: ẨN menu Thống kê (không cần thiết)
                if (menuStats != null)
                {
                    menuStats.Visible = false;
                }

                // SinhVien: Chỉ xem điểm và biểu đồ
                // ❌ STUDENT: ẨN TOÀN BỘ menu "Quản lý"
                if (menuManagement != null)
                {
                    menuManagement.Visible = false;
                }

                // ❌ STUDENT: ẨN TOÀN BỘ menu "Báo cáo"
                if (menuReport != null)
                {
                    menuReport.Visible = false;
                }

                // ❌ STUDENT: ẨN TOÀN BỘ menu "Thống kê"
                if (menuStats != null)
                {
                    menuStats.Visible = false;
                }

                if (menuSystem != null)
                {
                    foreach (var item in menuSystem.DropDownItems.OfType<ToolStripMenuItem>())
                    {
                        if (!(item.Text?.Contains("mật khẩu") == true) &&
                            !(item.Text?.Contains("xuất") == true) &&
                            !(item.Text?.Contains("Thoát") == true))
                        {
                            item.Visible = false;
                        }
                    }
                }
            }
        }

        private void LoadStatistics()
        {
            try
            {
                var allStudents = studentRepository.GetAllStudents();

                int totalStudents = allStudents.Count(s => s.IsActive);

                var studentsByGender = allStudents
                    .Where(s => s.IsActive && !string.IsNullOrEmpty(s.Gender))
                    .GroupBy(s => s.Gender)
                    .Select(g => new { Gender = g.Key, Count = g.Count() })
                    .ToList();

                int totalClasses = 5;

                int totalTeachers = 5;

                Label? lblStudents = this.Controls.Find("lblTotalStudents", true).FirstOrDefault() as Label;
                if (lblStudents != null) lblStudents.Text = totalStudents.ToString();

                Label? lblClasses = this.Controls.Find("lblTotalClasses", true).FirstOrDefault() as Label;
                if (lblClasses != null) lblClasses.Text = totalClasses.ToString();

                Label? lblTeachers = this.Controls.Find("lblTotalTeachers", true).FirstOrDefault() as Label;
                if (lblTeachers != null) lblTeachers.Text = totalTeachers.ToString();

                if (studentsByGender.Any())
                {
                    string genderStats = string.Join(", ",
                        studentsByGender.Select(g => $"{g.Gender}: {g.Count}"));

                    Label? lblGenderStats = this.Controls.Find("lblGenderStats", true).FirstOrDefault() as Label;
                    if (lblGenderStats != null) lblGenderStats.Text = genderStats;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải thống kê: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void MenuLogout_Click(object? sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có muốn đăng xuất?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                SessionUser.ClearSession();
                this.Hide();
                LoginForm loginForm = new LoginForm();
                if (loginForm.ShowDialog() == DialogResult.OK)
                {
                    this.Show();
                    MainForm_Load(null, EventArgs.Empty);
                }
                else
                {
                    Application.Exit();
                }
            }
        }

        private void MenuExit_Click(object? sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có muốn thoát ứng dụng?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void MenuStudent_Click(object? sender, EventArgs e)
        {
            // ✅ SỬA: Mở StudentForm thay vì hiển thị MessageBox
            try
            {
                var studentForm = new Views.StudentForm();
                studentForm.ShowDialog(this);  // Mở dạng modal
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi mở form quản lý sinh viên: {ex.Message}",
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void MenuTeacher_Click(object? sender, EventArgs e)
        {
            // ✅ SỬA PROMPT 9: Mở TeacherForm
            try
            {
                var teacherForm = new Views.TeacherForm();
                teacherForm.ShowDialog(this);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi mở form quản lý giáo viên: {ex.Message}",
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void MenuClass_Click(object? sender, EventArgs e)
        {
            // ✅ SỬA PROMPT 8: Mở ClassForm
            try
            {
                var classForm = new Views.ClassForm();
                classForm.ShowDialog(this);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi mở form quản lý lớp: {ex.Message}",
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void MenuScore_Click(object? sender, EventArgs e)
        {
            // ✅ SỬA PROMPT 11: Mở ScoreForm
            try
            {
                var scoreForm = new Views.ScoreForm();
                scoreForm.ShowDialog(this);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi mở form quản lý điểm: {ex.Message}",
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void MenuUser_Click(object? sender, EventArgs e)
        {
            // ✅ SỬA PROMPT 12: Mở UserForm (chỉ Admin)
            try
            {
                var userForm = new Views.UserForm();
                userForm.ShowDialog(this);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi mở form quản lý người dùng: {ex.Message}",
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void MenuReportStudent_Click(object? sender, EventArgs e)
        {
            // ✅ SỬA PROMPT 13: Mở ReportStudentForm
            try
            {
                var reportStudentForm = new Views.ReportStudentForm();
                reportStudentForm.ShowDialog(this);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi mở báo cáo sinh viên: {ex.Message}",
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void MenuReportScore_Click(object? sender, EventArgs e)
        {
            // ✅ SỬA PROMPT 14: Mở ReportScoreForm
            try
            {
                var reportScoreForm = new Views.ReportScoreForm();
                reportScoreForm.ShowDialog(this);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi mở báo cáo điểm: {ex.Message}",
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void MenuReportClass_Click(object? sender, EventArgs e)
        {
            // ✅ SỬA PROMPT 15: Mở ReportClassForm
            try
            {
                var reportClassForm = new Views.ReportClassForm();
                reportClassForm.ShowDialog(this);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi mở báo cáo lớp: {ex.Message}",
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void MenuChart_Click(object? sender, EventArgs e)
        {
            // ✅ PROMPT 16: Mở ChartForm1 (Biểu đồ cột)
            MenuChart1_Click(sender, e);
        }

        private void MenuChart1_Click(object? sender, EventArgs e)
        {
            // ✅ PROMPT 16: Mở ChartForm1 (Biểu đồ cột)
            try
            {
                var chartForm = new Views.ChartForm1();
                chartForm.ShowDialog(this);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi mở biểu đồ: {ex.Message}",
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void MenuChart2_Click(object? sender, EventArgs e)
        {
            // ✅ PROMPT 17: Mở ChartForm2 (Biểu đồ tròn)
            try
            {
                var chartForm = new Views.ChartForm2();
                chartForm.ShowDialog(this);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi mở biểu đồ: {ex.Message}",
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void MenuBackup_Click(object? sender, EventArgs e)
        {
            // ✅ XÓA: Không còn chức năng sao lưu dữ liệu
            MessageBox.Show("Chức năng này đã được loại bỏ khỏi hệ thống.", "Thông báo",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void MenuChangePassword_Click(object? sender, EventArgs e)
        {
            // ✅ PROMPT 19: Mở ChangePasswordForm
            try
            {
                ChangePasswordForm changePasswordForm = new ChangePasswordForm();
                changePasswordForm.ShowDialog(this);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi mở form đổi mật khẩu: {ex.Message}",
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void MenuHelp_Click(object? sender, EventArgs e)
        {
            // ✅ PROMPT 20: Mở HelpForm
            try
            {
                var helpForm = new Views.HelpForm();
                helpForm.ShowDialog(this);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi mở hướng dẫn: {ex.Message}",
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void MenuHelpGuide_Click(object? sender, EventArgs e)
        {
            // ✅ PROMPT 20: Mở HelpForm (từ menu Giúp đỡ)
            MenuHelp_Click(sender, e);
        }

        private void MenuAbout_Click(object? sender, EventArgs e)
        {
            AboutForm aboutForm = new AboutForm();
            aboutForm.ShowDialog(this);
        }
    }
}