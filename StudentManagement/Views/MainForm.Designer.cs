namespace StudentManagement
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Panel pnlMain;
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.SuspendLayout();

            // Form properties
            this.Text = "Hệ thống quản lý sinh viên";
            this.Size = new System.Drawing.Size(1366, 768);
            this.MinimumSize = new System.Drawing.Size(1024, 600);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Sizable;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.BackColor = System.Drawing.Color.FromArgb(240, 244, 248);

            // Menu Strip
            System.Windows.Forms.MenuStrip menuStrip = new System.Windows.Forms.MenuStrip();
            menuStrip.Dock = System.Windows.Forms.DockStyle.Top;
            menuStrip.BackColor = System.Drawing.Color.FromArgb(41, 128, 185);
            menuStrip.ForeColor = System.Drawing.Color.White;
            menuStrip.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.Controls.Add(menuStrip);

            // Menu Hệ thống
            System.Windows.Forms.ToolStripMenuItem menuSystem = new System.Windows.Forms.ToolStripMenuItem("Hệ thống");
            menuSystem.ForeColor = System.Drawing.Color.White;
            menuSystem.DropDownItems.Add("🔐 Đổi mật khẩu", null, MenuChangePassword_Click);
            menuSystem.DropDownItems.Add(new System.Windows.Forms.ToolStripSeparator());
            menuSystem.DropDownItems.Add("📖 Hướng dẫn sử dụng", null, MenuHelp_Click);
            menuSystem.DropDownItems.Add("ℹ️ Giới thiệu", null, MenuAbout_Click);
            menuSystem.DropDownItems.Add(new System.Windows.Forms.ToolStripSeparator());
            menuSystem.DropDownItems.Add("🚪 Đăng xuất", null, MenuLogout_Click);
            menuSystem.DropDownItems.Add("❌ Thoát", null, MenuExit_Click);
            menuStrip.Items.Add(menuSystem);

            // Menu Quản lý
            System.Windows.Forms.ToolStripMenuItem menuManagement = new System.Windows.Forms.ToolStripMenuItem("Quản lý");
            menuManagement.ForeColor = System.Drawing.Color.White;
            menuManagement.DropDownItems.Add("👥 Quản lý sinh viên", null, MenuStudent_Click);
            menuManagement.DropDownItems.Add("👨‍🏫 Quản lý giáo viên", null, MenuTeacher_Click);
            menuManagement.DropDownItems.Add("🏫 Quản lý lớp học", null, MenuClass_Click);
            menuManagement.DropDownItems.Add("📊 Quản lý điểm", null, MenuScore_Click);
            menuManagement.DropDownItems.Add(new System.Windows.Forms.ToolStripSeparator());
            menuManagement.DropDownItems.Add("👤 Quản lý người dùng", null, MenuUser_Click);
            menuStrip.Items.Add(menuManagement);

            // Menu Báo cáo
            System.Windows.Forms.ToolStripMenuItem menuReport = new System.Windows.Forms.ToolStripMenuItem("Báo cáo");
            menuReport.ForeColor = System.Drawing.Color.White;
            menuReport.DropDownItems.Add("📋 Báo cáo sinh viên", null, MenuReportStudent_Click);
            menuReport.DropDownItems.Add("📈 Báo cáo điểm", null, MenuReportScore_Click);
            menuReport.DropDownItems.Add("🏫 Báo cáo lớp học", null, MenuReportClass_Click);
            menuStrip.Items.Add(menuReport);

            // Menu Thống kê
            System.Windows.Forms.ToolStripMenuItem menuStats = new System.Windows.Forms.ToolStripMenuItem("Thống kê");
            menuStats.ForeColor = System.Drawing.Color.White;
            menuStats.DropDownItems.Add("📊 Biểu đồ cột - Điểm TB theo lớp", null, MenuChart1_Click);
            menuStats.DropDownItems.Add("🥧 Biểu đồ tròn - Phân loại học lực", null, MenuChart2_Click);
            menuStrip.Items.Add(menuStats);

            // Menu Trợ giúp
            System.Windows.Forms.ToolStripMenuItem menuHelp = new System.Windows.Forms.ToolStripMenuItem("Trợ giúp");
            menuHelp.ForeColor = System.Drawing.Color.White;
            menuHelp.DropDownItems.Add("📖 Hướng dẫn sử dụng", null, MenuHelpGuide_Click);
            menuHelp.DropDownItems.Add("ℹ️ Thông tin nhóm", null, MenuAbout_Click);
            menuStrip.Items.Add(menuHelp);

            // Status Strip
            System.Windows.Forms.StatusStrip statusStrip = new System.Windows.Forms.StatusStrip();
            statusStrip.Dock = System.Windows.Forms.DockStyle.Bottom;
            statusStrip.BackColor = System.Drawing.Color.FromArgb(52, 73, 94);

            System.Windows.Forms.ToolStripStatusLabel lblUser = new System.Windows.Forms.ToolStripStatusLabel();
            lblUser.Name = "lblUser";
            lblUser.Text = "Đang tải...";
            lblUser.ForeColor = System.Drawing.Color.White;
            lblUser.Spring = true;
            lblUser.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            statusStrip.Items.Add(lblUser);

            System.Windows.Forms.ToolStripStatusLabel lblTime = new System.Windows.Forms.ToolStripStatusLabel();
            lblTime.Name = "lblTime";
            lblTime.Text = System.DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
            lblTime.ForeColor = System.Drawing.Color.White;
            statusStrip.Items.Add(lblTime);

            this.Controls.Add(statusStrip);

            // Panel chính với Auto Scroll
            pnlMain = new System.Windows.Forms.Panel();
            pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            pnlMain.BackColor = System.Drawing.Color.FromArgb(236, 240, 241);
            pnlMain.AutoScroll = true;
            this.Controls.Add(pnlMain);

            // Panel container để canh giữa - ✅ THU NHỎ kích thước
            System.Windows.Forms.Panel pnlContainer = new System.Windows.Forms.Panel();
            pnlContainer.Size = new System.Drawing.Size(1300, 900);
            pnlContainer.Location = new System.Drawing.Point(50, 30);
            pnlContainer.BackColor = System.Drawing.Color.Transparent;
            pnlContainer.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left;
            pnlMain.Controls.Add(pnlContainer);

            // Header - ✅ THU NHỎ font
            System.Windows.Forms.Label lblHeader = new System.Windows.Forms.Label();
            lblHeader.Text = "🎓 HỆ THỐNG QUẢN LÝ SINH VIÊN";
            lblHeader.Font = new System.Drawing.Font("Segoe UI", 20, System.Drawing.FontStyle.Bold);
            lblHeader.ForeColor = System.Drawing.Color.FromArgb(41, 128, 185);
            lblHeader.Size = new System.Drawing.Size(1100, 40);
            lblHeader.Location = new System.Drawing.Point(0, 0);
            lblHeader.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            pnlContainer.Controls.Add(lblHeader);

            // Subtitle - ✅ THU NHỎ font
            System.Windows.Forms.Label lblSubtitle = new System.Windows.Forms.Label();
            lblSubtitle.Text = "Chọn chức năng bên dưới để bắt đầu";
            lblSubtitle.Font = new System.Drawing.Font("Segoe UI", 10, System.Drawing.FontStyle.Italic);
            lblSubtitle.ForeColor = System.Drawing.Color.FromArgb(127, 140, 141);
            lblSubtitle.Size = new System.Drawing.Size(1100, 25);
            lblSubtitle.Location = new System.Drawing.Point(0, 45);
            lblSubtitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            pnlContainer.Controls.Add(lblSubtitle);

            // ===== QUẢN LÝ (5 chức năng) =====
            CreateSectionTitle(pnlContainer, "📂 QUẢN LÝ", 0, 80);

            // Hàng 1: Quản lý - ✅ THU NHỎ spacing
            CreateFunctionCard(pnlContainer, "👥\n\nQUẢN LÝ\nSINH VIÊN", 0, 110, System.Drawing.Color.FromArgb(52, 152, 219), MenuStudent_Click);
            CreateFunctionCard(pnlContainer, "👨‍🏫\n\nQUẢN LÝ\nGIÁO VIÊN", 225, 110, System.Drawing.Color.FromArgb(46, 204, 113), MenuTeacher_Click);
            CreateFunctionCard(pnlContainer, "🏫\n\nQUẢN LÝ\nLỚP HỌC", 450, 110, System.Drawing.Color.FromArgb(155, 89, 182), MenuClass_Click);
            CreateFunctionCard(pnlContainer, "📊\n\nQUẢN LÝ\nĐIỂM SỐ", 675, 110, System.Drawing.Color.FromArgb(230, 126, 34), MenuScore_Click);
            CreateFunctionCard(pnlContainer, "👤\n\nQUẢN LÝ\nNGƯỜI DÙNG", 900, 110, System.Drawing.Color.FromArgb(231, 76, 60), MenuUser_Click);

            // ===== BÁO CÁO (3 chức năng) =====
            CreateSectionTitle(pnlContainer, "📊 BÁO CÁO", 0, 270);

            // Hàng 2: Báo cáo - ✅ CANH GIỮA 3 card
            CreateFunctionCard(pnlContainer, "📋\n\nBÁO CÁO\nSINH VIÊN", 225, 300, System.Drawing.Color.FromArgb(26, 188, 156), MenuReportStudent_Click);
            CreateFunctionCard(pnlContainer, "📈\n\nBÁO CÁO\nĐIỂM SỐ", 450, 300, System.Drawing.Color.FromArgb(52, 152, 219), MenuReportScore_Click);
            CreateFunctionCard(pnlContainer, "🏫\n\nBÁO CÁO\nLỚP HỌC", 675, 300, System.Drawing.Color.FromArgb(142, 68, 173), MenuReportClass_Click);

            // ===== BIỂU ĐỒ (2 chức năng) =====
            CreateSectionTitle(pnlContainer, "📈 BIỂU ĐỒ THỐNG KÊ", 0, 460);

            // Hàng 3: Biểu đồ - ✅ CANH GIỮA 2 card
            CreateFunctionCard(pnlContainer, "📊\n\nBIỂU ĐỒ CỘT\nĐiểm TB theo lớp", 337, 490, System.Drawing.Color.FromArgb(41, 128, 185), MenuChart1_Click);
            CreateFunctionCard(pnlContainer, "🥧\n\nBIỂU ĐỒ TRÒN\nPhân loại học lực", 562, 490, System.Drawing.Color.FromArgb(243, 156, 18), MenuChart2_Click);

            // ===== HỆ THỐNG (3 chức năng) =====
            CreateSectionTitle(pnlContainer, "⚙️ HỆ THỐNG", 0, 650);

            // Hàng 4: Hệ thống - ✅ CANH GIỮA 3 card
            CreateFunctionCard(pnlContainer, "🔐\n\nĐỔI\nMẬT KHẨU", 225, 680, System.Drawing.Color.FromArgb(52, 73, 94), MenuChangePassword_Click);
            CreateFunctionCard(pnlContainer, "📖\n\nHƯỚNG DẪN\nSỬ DỤNG", 450, 680, System.Drawing.Color.FromArgb(22, 160, 133), MenuHelp_Click);
            CreateFunctionCard(pnlContainer, "ℹ️\n\nTHÔNG TIN\nNHÓM", 675, 680, System.Drawing.Color.FromArgb(192, 57, 43), MenuAbout_Click);

            // Timer cập nhật thời gian
            System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();
            timer.Interval = 1000;
            timer.Tick += (s, e) =>
            {
                var timeLabel = statusStrip.Items["lblTime"] as System.Windows.Forms.ToolStripStatusLabel;
                if (timeLabel != null)
                    timeLabel.Text = System.DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
            };
            timer.Start();

            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private void CreateSectionTitle(System.Windows.Forms.Panel parent, string text, int x, int y)
        {
            System.Windows.Forms.Label lblSection = new System.Windows.Forms.Label();
            lblSection.Text = text;
            lblSection.Font = new System.Drawing.Font("Segoe UI", 13, System.Drawing.FontStyle.Bold);
            lblSection.ForeColor = System.Drawing.Color.FromArgb(44, 62, 80);
            lblSection.Size = new System.Drawing.Size(1100, 25);
            lblSection.Location = new System.Drawing.Point(x, y);
            lblSection.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            parent.Controls.Add(lblSection);

            // Line separator
            System.Windows.Forms.Panel line = new System.Windows.Forms.Panel();
            line.BackColor = System.Drawing.Color.FromArgb(189, 195, 199);
            line.Size = new System.Drawing.Size(1100, 2);
            line.Location = new System.Drawing.Point(x, y + 23);
            parent.Controls.Add(line);
        }

        private void CreateFunctionCard(System.Windows.Forms.Panel parent, string text, int x, int y, System.Drawing.Color color, System.EventHandler handler)
        {
            // ✅ THU NHỎ kích thước card: 215x145 (trước: 230x180)
            System.Windows.Forms.Panel card = new System.Windows.Forms.Panel();
            card.Size = new System.Drawing.Size(215, 145);
            card.Location = new System.Drawing.Point(x, y);
            card.BackColor = System.Drawing.Color.White;
            card.Cursor = System.Windows.Forms.Cursors.Hand;
            card.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;

            // Hover effect
            card.MouseEnter += (s, e) =>
            {
                card.BackColor = System.Drawing.Color.FromArgb(245, 245, 245);
            };
            card.MouseLeave += (s, e) =>
            {
                card.BackColor = System.Drawing.Color.White;
            };

            parent.Controls.Add(card);

            // Color bar - ✅ THU NHỎ chiều cao: 6px (trước: 8px)
            System.Windows.Forms.Panel colorBar = new System.Windows.Forms.Panel();
            colorBar.BackColor = color;
            colorBar.Size = new System.Drawing.Size(215, 6);
            colorBar.Location = new System.Drawing.Point(0, 0);
            card.Controls.Add(colorBar);

            // Button - ✅ THU NHỎ font: 11 (trước: 13)
            System.Windows.Forms.Button btn = new System.Windows.Forms.Button();
            btn.Text = text;
            btn.Size = new System.Drawing.Size(215, 139);
            btn.Location = new System.Drawing.Point(0, 6);
            btn.Font = new System.Drawing.Font("Segoe UI", 11, System.Drawing.FontStyle.Bold);
            btn.BackColor = System.Drawing.Color.White;
            btn.ForeColor = color;
            btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;
            btn.Cursor = System.Windows.Forms.Cursors.Hand;
            btn.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            // Button hover effect
            btn.MouseEnter += (s, e) =>
            {
                btn.BackColor = color;
                btn.ForeColor = System.Drawing.Color.White;
            };
            btn.MouseLeave += (s, e) =>
            {
                btn.BackColor = System.Drawing.Color.White;
                btn.ForeColor = color;
            };

            btn.Click += handler;
            card.Controls.Add(btn);
        }

        #endregion
    }
}