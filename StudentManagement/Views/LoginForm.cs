using StudentManagement.Helper;
using StudentManagement.Models;
using StudentManagement.Data;

namespace StudentManagement
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        private void TxtPassword_KeyPress(object? sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                BtnLogin_Click(sender, e);
            }
        }

        private void BtnLogin_Click(object? sender, EventArgs e)
        {
            TextBox? txtUsername = this.Controls.Find("txtUsername", true).FirstOrDefault() as TextBox;
            TextBox? txtPassword = this.Controls.Find("txtPassword", true).FirstOrDefault() as TextBox;
            Label? lblError = this.Controls.Find("lblError", true).FirstOrDefault() as Label;

            if (txtUsername == null || txtPassword == null || lblError == null) return;

            lblError.Text = "";

            if (string.IsNullOrWhiteSpace(txtUsername.Text))
            {
                lblError.Text = "⚠️ Vui lòng nhập tên đăng nhập!";
                txtUsername.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                lblError.Text = "⚠️ Vui lòng nhập mật khẩu!";
                txtPassword.Focus();
                return;
            }

            try
            {
                // Sử dụng LINQ với DataAccess helper
                User? user = UserDataAccess.GetUserByUsernamePassword(txtUsername.Text.Trim(), txtPassword.Text);

                if (user != null)
                {
                    if (!user.IsActive)
                    {
                        lblError.Text = "❌ Tài khoản đã bị khóa. Vui lòng liên hệ quản trị viên!";
                        return;
                    }

                    SessionUser.SetUser(user);

                    MessageBox.Show(
                        $"✅ Đăng nhập thành công!\n\n" +
                        $"Họ tên: {user.FullName}\n" +
                        $"Vai trò: {user.Role}\n" +
                        $"Email: {user.Email}",
                        "Thành công",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );

                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    lblError.Text = "❌ Tên đăng nhập hoặc mật khẩu không chính xác!";
                    txtPassword.Clear();
                    txtUsername.Focus();
                }
            }
            catch (Exception ex)
            {
                lblError.Text = "❌ Lỗi kết nối database!";
                MessageBox.Show($"Lỗi: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnExit_Click(object? sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có muốn thoát ứng dụng?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void lblHeader_Click(object sender, EventArgs e)
        {

        }
    }
}