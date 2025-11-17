namespace StudentManagement
{
    partial class AboutForm
    {
        private System.ComponentModel.IContainer components = null;

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
            pnlHeader = new Panel();
            lblIcon = new Label();
            lblTitle = new Label();
            lblVersion = new Label();
            pnlContent = new Panel();
            lblGroupTitle = new Label();
            lblMemberIcon1 = new Label();
            lblMemberName1 = new Label();
            lblMemberCode1 = new Label();
            lblMemberIcon2 = new Label();
            lblMemberName2 = new Label();
            lblMemberCode2 = new Label();
            lblMemberIcon3 = new Label();
            lblMemberName3 = new Label();
            lblMemberCode3 = new Label();
            lblMemberIcon4 = new Label();
            lblMemberName4 = new Label();
            lblMemberCode4 = new Label();
            lblMemberIcon5 = new Label();
            lblMemberName5 = new Label();
            lblMemberCode5 = new Label();
            lblSchool = new Label();
            btnClose = new Button();
            pnlHeader.SuspendLayout();
            pnlContent.SuspendLayout();
            SuspendLayout();
            // 
            // pnlHeader
            // 
            pnlHeader.BackColor = Color.FromArgb(70, 130, 180);
            pnlHeader.Controls.Add(lblIcon);
            pnlHeader.Controls.Add(lblTitle);
            pnlHeader.Controls.Add(lblVersion);
            pnlHeader.Dock = DockStyle.Top;
            pnlHeader.Location = new Point(0, 0);
            pnlHeader.Name = "pnlHeader";
            pnlHeader.Size = new Size(738, 80);
            pnlHeader.TabIndex = 0;
            // 
            // lblIcon
            // 
            lblIcon.Font = new Font("Segoe UI", 24F);
            lblIcon.ForeColor = Color.White;
            lblIcon.Location = new Point(20, 15);
            lblIcon.Name = "lblIcon";
            lblIcon.Size = new Size(50, 50);
            lblIcon.TabIndex = 0;
            lblIcon.Text = "📚";
            // 
            // lblTitle
            // 
            lblTitle.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            lblTitle.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            lblTitle.ForeColor = Color.White;
            lblTitle.Location = new Point(80, 20);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(938, 30);
            lblTitle.TabIndex = 1;
            lblTitle.Text = "HỆ THỐNG QUẢN LÝ SINH VIÊN";
            // 
            // lblVersion
            // 
            lblVersion.Font = new Font("Segoe UI", 9F, FontStyle.Italic);
            lblVersion.ForeColor = Color.LightGray;
            lblVersion.Location = new Point(80, 50);
            lblVersion.Name = "lblVersion";
            lblVersion.Size = new Size(100, 20);
            lblVersion.TabIndex = 2;
            lblVersion.Text = "Version 1.0.0";
            // 
            // pnlContent
            // 
            pnlContent.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            pnlContent.AutoScroll = true;
            pnlContent.BackColor = Color.White;
            pnlContent.BorderStyle = BorderStyle.FixedSingle;
            pnlContent.Controls.Add(lblGroupTitle);
            pnlContent.Controls.Add(lblMemberIcon1);
            pnlContent.Controls.Add(lblMemberName1);
            pnlContent.Controls.Add(lblMemberCode1);
            pnlContent.Controls.Add(lblMemberIcon2);
            pnlContent.Controls.Add(lblMemberName2);
            pnlContent.Controls.Add(lblMemberCode2);
            pnlContent.Controls.Add(lblMemberIcon3);
            pnlContent.Controls.Add(lblMemberName3);
            pnlContent.Controls.Add(lblMemberCode3);
            pnlContent.Controls.Add(lblMemberIcon4);
            pnlContent.Controls.Add(lblMemberName4);
            pnlContent.Controls.Add(lblMemberCode4);
            pnlContent.Controls.Add(lblMemberIcon5);
            pnlContent.Controls.Add(lblMemberName5);
            pnlContent.Controls.Add(lblMemberCode5);
            pnlContent.Controls.Add(lblSchool);
            pnlContent.Location = new Point(25, 100);
            pnlContent.Name = "pnlContent";
            pnlContent.Size = new Size(696, 400);
            pnlContent.TabIndex = 1;
            // 
            // lblGroupTitle
            // 
            lblGroupTitle.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblGroupTitle.ForeColor = Color.FromArgb(70, 130, 180);
            lblGroupTitle.Location = new Point(20, 20);
            lblGroupTitle.Name = "lblGroupTitle";
            lblGroupTitle.Size = new Size(200, 25);
            lblGroupTitle.TabIndex = 0;
            lblGroupTitle.Text = "👥 NHÓM 2TH";
            // 
            // lblMemberIcon1
            // 
            lblMemberIcon1.Font = new Font("Segoe UI", 14F);
            lblMemberIcon1.Location = new Point(20, 55);
            lblMemberIcon1.Name = "lblMemberIcon1";
            lblMemberIcon1.Size = new Size(30, 30);
            lblMemberIcon1.TabIndex = 1;
            lblMemberIcon1.Text = "👨‍💼";
            // 
            // lblMemberName1
            // 
            lblMemberName1.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblMemberName1.ForeColor = Color.FromArgb(51, 51, 51);
            lblMemberName1.Location = new Point(60, 55);
            lblMemberName1.Name = "lblMemberName1";
            lblMemberName1.Size = new Size(400, 22);
            lblMemberName1.TabIndex = 2;
            lblMemberName1.Text = "Phạm Nguyễn Trường Huy";
            // 
            // lblMemberCode1
            // 
            lblMemberCode1.Font = new Font("Segoe UI", 9F);
            lblMemberCode1.ForeColor = Color.Gray;
            lblMemberCode1.Location = new Point(60, 75);
            lblMemberCode1.Name = "lblMemberCode1";
            lblMemberCode1.Size = new Size(200, 18);
            lblMemberCode1.TabIndex = 3;
            lblMemberCode1.Text = "MSSV: 50.01.104.063";
            // 
            // lblMemberIcon2
            // 
            lblMemberIcon2.Font = new Font("Segoe UI", 14F);
            lblMemberIcon2.Location = new Point(20, 105);
            lblMemberIcon2.Name = "lblMemberIcon2";
            lblMemberIcon2.Size = new Size(30, 30);
            lblMemberIcon2.TabIndex = 4;
            lblMemberIcon2.Text = "👩‍💻";
            // 
            // lblMemberName2
            // 
            lblMemberName2.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblMemberName2.ForeColor = Color.FromArgb(51, 51, 51);
            lblMemberName2.Location = new Point(60, 105);
            lblMemberName2.Name = "lblMemberName2";
            lblMemberName2.Size = new Size(250, 22);
            lblMemberName2.TabIndex = 5;
            lblMemberName2.Text = "Trần Tâm";
            // 
            // lblMemberCode2
            // 
            lblMemberCode2.Font = new Font("Segoe UI", 9F);
            lblMemberCode2.ForeColor = Color.Gray;
            lblMemberCode2.Location = new Point(60, 125);
            lblMemberCode2.Name = "lblMemberCode2";
            lblMemberCode2.Size = new Size(200, 18);
            lblMemberCode2.TabIndex = 6;
            lblMemberCode2.Text = "MSSV: 50.01.104.142";
            // 
            // lblMemberIcon3
            // 
            lblMemberIcon3.Font = new Font("Segoe UI", 14F);
            lblMemberIcon3.Location = new Point(20, 155);
            lblMemberIcon3.Name = "lblMemberIcon3";
            lblMemberIcon3.Size = new Size(30, 30);
            lblMemberIcon3.TabIndex = 7;
            lblMemberIcon3.Text = "👨‍💻";
            // 
            // lblMemberName3
            // 
            lblMemberName3.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblMemberName3.ForeColor = Color.FromArgb(51, 51, 51);
            lblMemberName3.Location = new Point(60, 155);
            lblMemberName3.Name = "lblMemberName3";
            lblMemberName3.Size = new Size(250, 22);
            lblMemberName3.TabIndex = 8;
            lblMemberName3.Text = "Lê Hoàng Huy";
            // 
            // lblMemberCode3
            // 
            lblMemberCode3.Font = new Font("Segoe UI", 9F);
            lblMemberCode3.ForeColor = Color.Gray;
            lblMemberCode3.Location = new Point(60, 175);
            lblMemberCode3.Name = "lblMemberCode3";
            lblMemberCode3.Size = new Size(200, 18);
            lblMemberCode3.TabIndex = 9;
            lblMemberCode3.Text = "MSSV: 50.01.104.058";
            // 
            // lblMemberIcon4
            // 
            lblMemberIcon4.Font = new Font("Segoe UI", 14F);
            lblMemberIcon4.Location = new Point(20, 205);
            lblMemberIcon4.Name = "lblMemberIcon4";
            lblMemberIcon4.Size = new Size(30, 30);
            lblMemberIcon4.TabIndex = 10;
            lblMemberIcon4.Text = "👨‍💻";
            // 
            // lblMemberName4
            // 
            lblMemberName4.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblMemberName4.ForeColor = Color.FromArgb(51, 51, 51);
            lblMemberName4.Location = new Point(60, 205);
            lblMemberName4.Name = "lblMemberName4";
            lblMemberName4.Size = new Size(250, 22);
            lblMemberName4.TabIndex = 11;
            lblMemberName4.Text = "Võ Tấn Thiện";
            // 
            // lblMemberCode4
            // 
            lblMemberCode4.Font = new Font("Segoe UI", 9F);
            lblMemberCode4.ForeColor = Color.Gray;
            lblMemberCode4.Location = new Point(60, 225);
            lblMemberCode4.Name = "lblMemberCode4";
            lblMemberCode4.Size = new Size(200, 18);
            lblMemberCode4.TabIndex = 12;
            lblMemberCode4.Text = "MSSV: 50.01.104.153";
            // 
            // lblMemberIcon5
            // 
            lblMemberIcon5.Font = new Font("Segoe UI", 14F);
            lblMemberIcon5.Location = new Point(20, 255);
            lblMemberIcon5.Name = "lblMemberIcon5";
            lblMemberIcon5.Size = new Size(30, 30);
            lblMemberIcon5.TabIndex = 13;
            lblMemberIcon5.Text = "👨‍💻";
            // 
            // lblMemberName5
            // 
            lblMemberName5.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblMemberName5.ForeColor = Color.FromArgb(51, 51, 51);
            lblMemberName5.Location = new Point(60, 255);
            lblMemberName5.Name = "lblMemberName5";
            lblMemberName5.Size = new Size(250, 22);
            lblMemberName5.TabIndex = 14;
            lblMemberName5.Text = "Phạm Văn Phi";
            // 
            // lblMemberCode5
            // 
            lblMemberCode5.Font = new Font("Segoe UI", 9F);
            lblMemberCode5.ForeColor = Color.Gray;
            lblMemberCode5.Location = new Point(60, 275);
            lblMemberCode5.Name = "lblMemberCode5";
            lblMemberCode5.Size = new Size(200, 18);
            lblMemberCode5.TabIndex = 15;
            lblMemberCode5.Text = "MSSV: 50.01.104.115";
            // 
            // lblSchool
            // 
            lblSchool.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            lblSchool.Font = new Font("Segoe UI", 9F, FontStyle.Italic);
            lblSchool.ForeColor = Color.Gray;
            lblSchool.Location = new Point(20, 338);
            lblSchool.Name = "lblSchool";
            lblSchool.Size = new Size(604, 20);
            lblSchool.TabIndex = 16;
            lblSchool.Text = "🏫 Đại học Sư Phạm Thành Phố Hồ Chí Minh - Năm học 2024-2025";
            lblSchool.Click += lblSchool_Click;
            // 
            // btnClose
            // 
            btnClose.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnClose.BackColor = Color.FromArgb(70, 130, 180);
            btnClose.Cursor = Cursors.Hand;
            btnClose.FlatAppearance.BorderSize = 0;
            btnClose.FlatStyle = FlatStyle.Flat;
            btnClose.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnClose.ForeColor = Color.White;
            btnClose.Location = new Point(646, 520);
            btnClose.Name = "btnClose";
            btnClose.Size = new Size(80, 35);
            btnClose.TabIndex = 2;
            btnClose.Text = "Đóng";
            btnClose.UseVisualStyleBackColor = false;
            btnClose.Click += BtnClose_Click;
            // 
            // AboutForm
            // 
            BackColor = Color.FromArgb(240, 248, 255);
            ClientSize = new Size(738, 573);
            Controls.Add(pnlHeader);
            Controls.Add(pnlContent);
            Controls.Add(btnClose);
            MinimumSize = new Size(550, 580);
            Name = "AboutForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Thông tin nhóm 2TH";
            pnlHeader.ResumeLayout(false);
            pnlContent.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Panel pnlHeader;
        private Label lblIcon;
        private Label lblTitle;
        private Label lblVersion;
        private Panel pnlContent;
        private Label lblGroupTitle;
        private Label lblMemberIcon1;
        private Label lblMemberName1;
        private Label lblMemberCode1;
        private Label lblMemberIcon2;
        private Label lblMemberName2;
        private Label lblMemberCode2;
        private Label lblMemberIcon3;
        private Label lblMemberName3;
        private Label lblMemberCode3;
        private Label lblMemberIcon4;
        private Label lblMemberName4;
        private Label lblMemberCode4;
        private Label lblMemberIcon5;
        private Label lblMemberName5;
        private Label lblMemberCode5;
        private Label lblSchool;
        private Button btnClose;
    }
}