using System;
using System.Linq;
using System.Windows.Forms;
using StudentManagement.DAL;
using StudentManagement.Helper;

namespace StudentManagement.Views
{
    /// <summary>
    /// Form đổi mật khẩu - PROMPT 19
    /// Chức năng: Người dùng đổi mật khẩu của chính mình
    /// Logic: Kiểm tra OldPassword (dùng SessionUser.UserID và LINQ), 
    ///        kiểm tra NewPassword == ConfirmPassword, update DB
    /// </summary>
    public partial class ChangePasswordForm : Form
    {
        private UserRepository userRepo;

        public ChangePasswordForm()
        {
            InitializeComponent();
            userRepo = new UserRepository();
        }

        #region Form Events

        private void ChangePasswordForm_Load(object sender, EventArgs e)
        {
            // Hiển thị thông tin user hiện tại
            if (SessionUser.CurrentUser != null)
            {
                lblTitle.Text = $"🔐 ĐỔI MẬT KHẨU - {SessionUser.CurrentUser.FullName}";
            }

            txtOldPassword.Focus();
        }

        #endregion

        #region Button Events

        /// <summary>
        /// Xử lý đổi mật khẩu
        /// </summary>
        private void btnChange_Click(object sender, EventArgs e)
        {
            try
            {
                // Validate inputs
                if (!ValidateInputs())
                    return;

                // Kiểm tra SessionUser
                if (SessionUser.CurrentUser == null || SessionUser.UserID == 0)
                {
                    MessageBox.Show("Phiên đăng nhập không hợp lệ!\nVui lòng đăng nhập lại.", "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.DialogResult = DialogResult.Cancel;
                    this.Close();
                    return;
                }

                // ✅ PROMPT 19: Kiểm tra mật khẩu cũ bằng LINQ
                string oldPassword = txtOldPassword.Text.Trim();
                string newPassword = txtNewPassword.Text.Trim();

                // Lấy user hiện tại từ DB và kiểm tra mật khẩu cũ
                var currentUser = userRepo.GetUserById(SessionUser.UserID);

                if (currentUser == null)
                {
                    MessageBox.Show("Không tìm thấy thông tin người dùng!", "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // ✅ LINQ: Kiểm tra mật khẩu cũ
                if (currentUser.Password != oldPassword)
                {
                    MessageBox.Show("Mật khẩu hiện tại không đúng!\nVui lòng kiểm tra lại.", "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtOldPassword.Focus();
                    txtOldPassword.SelectAll();
                    UpdateStatus("❌ Mật khẩu hiện tại không đúng");
                    return;
                }

                // Kiểm tra mật khẩu mới không trùng mật khẩu cũ
                if (oldPassword == newPassword)
                {
                    MessageBox.Show("Mật khẩu mới phải khác mật khẩu hiện tại!", "Cảnh báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtNewPassword.Focus();
                    UpdateStatus("⚠️ Mật khẩu mới phải khác mật khẩu cũ");
                    return;
                }

                // Xác nhận đổi mật khẩu
                var confirmResult = MessageBox.Show(
                    "Bạn có chắc chắn muốn đổi mật khẩu?\n\n" +
                    "Sau khi đổi, bạn sẽ cần sử dụng mật khẩu mới để đăng nhập.",
                    "Xác nhận đổi mật khẩu",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (confirmResult == DialogResult.Yes)
                {
                    // ✅ PROMPT 19: Update password trong DB
                    bool result = userRepo.UpdatePassword(SessionUser.UserID, newPassword);

                    if (result)
                    {
                        MessageBox.Show(
                            "✅ Đổi mật khẩu thành công!\n\n" +
                            "Mật khẩu mới đã được lưu vào hệ thống.\n" +
                            "Vui lòng sử dụng mật khẩu mới cho lần đăng nhập tiếp theo.",
                            "Thành công",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);

                        // Cập nhật mật khẩu trong SessionUser (nếu cần)
                        if (SessionUser.CurrentUser != null)
                        {
                            SessionUser.CurrentUser.Password = newPassword;
                        }

                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Không thể cập nhật mật khẩu!\nVui lòng thử lại.", "Lỗi",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        UpdateStatus("❌ Lỗi khi cập nhật mật khẩu");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi đổi mật khẩu:\n{ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                UpdateStatus($"❌ Lỗi: {ex.Message}");
            }
        }

        /// <summary>
        /// Hủy và đóng form
        /// </summary>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        /// <summary>
        /// Hiển thị/ẩn mật khẩu
        /// </summary>
        private void chkShowPassword_CheckedChanged(object sender, EventArgs e)
        {
            if (chkShowPassword.Checked)
            {
                txtOldPassword.PasswordChar = '\0';
                txtNewPassword.PasswordChar = '\0';
                txtConfirmPassword.PasswordChar = '\0';
            }
            else
            {
                txtOldPassword.PasswordChar = '●';
                txtNewPassword.PasswordChar = '●';
                txtConfirmPassword.PasswordChar = '●';
            }
        }

        #endregion

        #region Validation

        /// <summary>
        /// Validate các input
        /// </summary>
        private bool ValidateInputs()
        {
            // Kiểm tra mật khẩu hiện tại
            if (string.IsNullOrWhiteSpace(txtOldPassword.Text))
            {
                MessageBox.Show("Vui lòng nhập mật khẩu hiện tại!", "Cảnh báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtOldPassword.Focus();
                UpdateStatus("⚠️ Chưa nhập mật khẩu hiện tại");
                return false;
            }

            // Kiểm tra mật khẩu mới
            if (string.IsNullOrWhiteSpace(txtNewPassword.Text))
            {
                MessageBox.Show("Vui lòng nhập mật khẩu mới!", "Cảnh báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNewPassword.Focus();
                UpdateStatus("⚠️ Chưa nhập mật khẩu mới");
                return false;
            }

            // Kiểm tra độ dài mật khẩu mới
            if (txtNewPassword.Text.Trim().Length < 6)
            {
                MessageBox.Show("Mật khẩu mới phải có ít nhất 6 ký tự!", "Cảnh báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNewPassword.Focus();
                UpdateStatus("⚠️ Mật khẩu quá ngắn (tối thiểu 6 ký tự)");
                return false;
            }

            // Kiểm tra xác nhận mật khẩu
            if (string.IsNullOrWhiteSpace(txtConfirmPassword.Text))
            {
                MessageBox.Show("Vui lòng xác nhận mật khẩu mới!", "Cảnh báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtConfirmPassword.Focus();
                UpdateStatus("⚠️ Chưa xác nhận mật khẩu mới");
                return false;
            }

            // ✅ PROMPT 19: Kiểm tra NewPassword == ConfirmPassword
            if (txtNewPassword.Text.Trim() != txtConfirmPassword.Text.Trim())
            {
                MessageBox.Show(
                    "Mật khẩu mới và xác nhận mật khẩu không khớp!\n\n" +
                    "Vui lòng kiểm tra và nhập lại.",
                    "Cảnh báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                txtConfirmPassword.Focus();
                txtConfirmPassword.SelectAll();
                UpdateStatus("⚠️ Mật khẩu xác nhận không khớp");
                return false;
            }

            UpdateStatus("✅ Thông tin hợp lệ");
            return true;
        }

        #endregion

        #region Helper Methods

        /// <summary>
        /// Cập nhật status
        /// </summary>
        private void UpdateStatus(string message)
        {
            lblStatus.Text = message;
        }

        #endregion
    }
}
