namespace StudentManagement
{
    partial class LoginForm
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
            pnlMain = new Panel();
            lblHeader = new Label();
            lblUsername = new Label();
            txtUsername = new TextBox();
            lblPassword = new Label();
            txtPassword = new TextBox();
            lblError = new Label();
            pnlButtons = new Panel();
            btnLogin = new Button();
            btnExit = new Button();
            pnlMain.SuspendLayout();
            pnlButtons.SuspendLayout();
            SuspendLayout();
            // 
            // pnlMain
            // 
            pnlMain.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            pnlMain.BackColor = Color.White;
            pnlMain.BorderStyle = BorderStyle.FixedSingle;
            pnlMain.Controls.Add(lblHeader);
            pnlMain.Controls.Add(lblUsername);
            pnlMain.Controls.Add(txtUsername);
            pnlMain.Controls.Add(lblPassword);
            pnlMain.Controls.Add(txtPassword);
            pnlMain.Controls.Add(lblError);
            pnlMain.Controls.Add(pnlButtons);
            pnlMain.Location = new Point(20, 20);
            pnlMain.Name = "pnlMain";
            pnlMain.Size = new Size(390, 310);
            pnlMain.TabIndex = 0;
            // 
            // lblHeader
            // 
            lblHeader.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            lblHeader.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblHeader.ForeColor = Color.FromArgb(0, 0, 192);
            lblHeader.Location = new Point(55, 14);
            lblHeader.Name = "lblHeader";
            lblHeader.Size = new Size(288, 41);
            lblHeader.TabIndex = 0;
            lblHeader.Text = "🔐 ĐĂNG NHẬP HỆ THỐNG";
            lblHeader.TextAlign = ContentAlignment.MiddleCenter;
            lblHeader.Click += lblHeader_Click;
            // 
            // lblUsername
            // 
            lblUsername.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblUsername.Location = new Point(20, 87);
            lblUsername.Name = "lblUsername";
            lblUsername.Size = new Size(137, 33);
            lblUsername.TabIndex = 1;
            lblUsername.Text = "Tên đăng nhập:";
            // 
            // txtUsername
            // 
            txtUsername.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtUsername.Font = new Font("Segoe UI", 10F);
            txtUsername.Location = new Point(170, 84);
            txtUsername.Name = "txtUsername";
            txtUsername.Size = new Size(198, 30);
            txtUsername.TabIndex = 2;
            // 
            // lblPassword
            // 
            lblPassword.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblPassword.Location = new Point(20, 120);
            lblPassword.Name = "lblPassword";
            lblPassword.Size = new Size(120, 25);
            lblPassword.TabIndex = 3;
            lblPassword.Text = "Mật khẩu:";
            // 
            // txtPassword
            // 
            txtPassword.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtPassword.Font = new Font("Segoe UI", 10F);
            txtPassword.Location = new Point(170, 120);
            txtPassword.Name = "txtPassword";
            txtPassword.Size = new Size(198, 30);
            txtPassword.TabIndex = 4;
            txtPassword.UseSystemPasswordChar = true;
            txtPassword.KeyPress += TxtPassword_KeyPress;
            // 
            // lblError
            // 
            lblError.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            lblError.Font = new Font("Segoe UI", 9F, FontStyle.Italic);
            lblError.ForeColor = Color.Red;
            lblError.Location = new Point(30, 160);
            lblError.Name = "lblError";
            lblError.Size = new Size(328, 40);
            lblError.TabIndex = 5;
            lblError.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // pnlButtons
            // 
            pnlButtons.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            pnlButtons.Controls.Add(btnLogin);
            pnlButtons.Controls.Add(btnExit);
            pnlButtons.Location = new Point(20, 238);
            pnlButtons.Name = "pnlButtons";
            pnlButtons.Size = new Size(348, 50);
            pnlButtons.TabIndex = 6;
            // 
            // btnLogin
            // 
            btnLogin.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnLogin.BackColor = Color.FromArgb(70, 130, 180);
            btnLogin.FlatStyle = FlatStyle.Flat;
            btnLogin.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnLogin.ForeColor = Color.White;
            btnLogin.Location = new Point(50, 10);
            btnLogin.Name = "btnLogin";
            btnLogin.Size = new Size(120, 35);
            btnLogin.TabIndex = 0;
            btnLogin.Text = "Đăng nhập";
            btnLogin.UseVisualStyleBackColor = false;
            btnLogin.Click += BtnLogin_Click;
            // 
            // btnExit
            // 
            btnExit.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnExit.BackColor = Color.Gray;
            btnExit.FlatStyle = FlatStyle.Flat;
            btnExit.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnExit.ForeColor = Color.Red;
            btnExit.Location = new Point(198, 10);
            btnExit.Name = "btnExit";
            btnExit.Size = new Size(100, 35);
            btnExit.TabIndex = 1;
            btnExit.Text = "Thoát";
            btnExit.UseVisualStyleBackColor = false;
            btnExit.Click += BtnExit_Click;
            // 
            // LoginForm
            // 
            BackColor = Color.FromArgb(245, 245, 245);
            ClientSize = new Size(432, 333);
            Controls.Add(pnlMain);
            MinimumSize = new Size(400, 330);
            Name = "LoginForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Đăng nhập hệ thống";
            pnlMain.ResumeLayout(false);
            pnlMain.PerformLayout();
            pnlButtons.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Panel pnlMain;
        private Label lblHeader;
        private Label lblUsername;
        private TextBox txtUsername;
        private Label lblPassword;
        private TextBox txtPassword;
        private Label lblError;
        private Panel pnlButtons;
        private Button btnLogin;
        private Button btnExit;
    }
}