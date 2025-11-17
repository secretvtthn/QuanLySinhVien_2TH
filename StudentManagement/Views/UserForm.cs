using StudentManagement.DAL;
using StudentManagement.Helper;
using StudentManagement.Models;

namespace StudentManagement.Views
{
    /// <summary>
    /// ✅ PROMPT 12: Quản lý Người dùng - ADMIN ONLY
    /// </summary>
    public partial class UserForm : Form
    {
        private readonly UserRepository userRepo;
        private int currentUserID = 0;

        public UserForm()
        {
            InitializeComponent();
            userRepo = new UserRepository();
        }

        #region Form Events

        /// <summary>
        /// ✅ PROMPT 12: Form chỉ hiển thị khi SessionUser.Role == "Admin"
        /// </summary>
        private void UserForm_Load(object sender, EventArgs e)
        {
            try
            {
                // ✅ Kiểm tra quyền Admin
                if (SessionUser.Role != "Admin")
                {
                    MessageBox.Show("Bạn không có quyền truy cập chức năng này!\nChỉ Admin mới được phép.",
                        "Từ chối truy cập", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Close();
                    return;
                }

                LoadAllUsers();
                ClearForm();
                UpdateStatus("Sẵn sàng");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải form: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Khi chọn dòng trong DataGridView
        /// </summary>
        private void dgvUser_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                if (dgvUser.SelectedRows.Count > 0)
                {
                    var row = dgvUser.SelectedRows[0];
                    currentUserID = Convert.ToInt32(row.Cells["UserID"].Value);

                    // ✅ Load thông tin lên form
                    txtUsername.Text = row.Cells["Username"].Value?.ToString() ?? "";
                    txtFullName.Text = row.Cells["FullName"].Value?.ToString() ?? "";
                    cmbRole.Text = row.Cells["Role"].Value?.ToString() ?? "SinhVien";
                    txtEmail.Text = row.Cells["Email"].Value?.ToString() ?? "";
                    txtPhone.Text = row.Cells["Phone"].Value?.ToString() ?? "";
                    chkIsActive.Checked = Convert.ToBoolean(row.Cells["IsActive"].Value);

                    // ✅ KHÔNG hiển thị Password
                    txtPassword.Clear();
                    txtPassword.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi chọn người dùng: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region Button Events

        /// <summary>
        /// ✅ Thêm người dùng mới (check Username UNIQUE)
        /// </summary>
        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                // Validate dữ liệu
                if (string.IsNullOrWhiteSpace(txtUsername.Text))
                {
                    MessageBox.Show("Vui lòng nhập Username!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtUsername.Focus();
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtPassword.Text))
                {
                    MessageBox.Show("Vui lòng nhập Password khi thêm mới!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtPassword.Focus();
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtFullName.Text))
                {
                    MessageBox.Show("Vui lòng nhập Họ và tên!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtFullName.Focus();
                    return;
                }

                // Tạo User object
                var user = new User
                {
                    Username = txtUsername.Text.Trim(),
                    Password = txtPassword.Text.Trim(),
                    FullName = txtFullName.Text.Trim(),
                    Role = cmbRole.Text,
                    Email = txtEmail.Text.Trim(),
                    Phone = txtPhone.Text.Trim(),
                    IsActive = chkIsActive.Checked
                };

                // Thêm vào database
                bool success = userRepo.AddUser(user);

                if (success)
                {
                    MessageBox.Show("✅ Thêm người dùng thành công!", "Thành công",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadAllUsers();
                    ClearForm();
                    UpdateStatus($"Đã thêm người dùng: {user.Username}");
                }
                else
                {
                    MessageBox.Show("❌ Thêm người dùng thất bại!", "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi thêm người dùng: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// ✅ Sửa thông tin người dùng (KHÔNG sửa Password)
        /// </summary>
        private void btnEdit_Click(object sender, EventArgs e)
        {
            try
            {
                if (currentUserID == 0)
                {
                    MessageBox.Show("Vui lòng chọn người dùng cần sửa!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Validate dữ liệu
                if (string.IsNullOrWhiteSpace(txtUsername.Text))
                {
                    MessageBox.Show("Vui lòng nhập Username!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtUsername.Focus();
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtFullName.Text))
                {
                    MessageBox.Show("Vui lòng nhập Họ và tên!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtFullName.Focus();
                    return;
                }

                // Lấy thông tin hiện tại
                var existingUser = userRepo.GetUserById(currentUserID);
                if (existingUser == null)
                {
                    MessageBox.Show("Không tìm thấy người dùng!", "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Cập nhật thông tin (GIỮ NGUYÊN Password)
                existingUser.Username = txtUsername.Text.Trim();
                existingUser.FullName = txtFullName.Text.Trim();
                existingUser.Role = cmbRole.Text;
                existingUser.Email = txtEmail.Text.Trim();
                existingUser.Phone = txtPhone.Text.Trim();
                existingUser.IsActive = chkIsActive.Checked;
                // ✅ Password KHÔNG được update ở đây

                // Cập nhật vào database
                bool success = userRepo.UpdateUser(existingUser);

                if (success)
                {
                    MessageBox.Show("✅ Cập nhật thông tin thành công!", "Thành công",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadAllUsers();
                    UpdateStatus($"Đã cập nhật: {existingUser.Username}");
                }
                else
                {
                    MessageBox.Show("❌ Cập nhật thất bại!", "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi cập nhật: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// ✅ Xóa người dùng (không xóa được tài khoản đang đăng nhập)
        /// </summary>
        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (currentUserID == 0)
                {
                    MessageBox.Show("Vui lòng chọn người dùng cần xóa!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // ✅ Kiểm tra không xóa được tài khoản đang đăng nhập
                if (currentUserID == SessionUser.UserID)
                {
                    MessageBox.Show("❌ Không thể xóa tài khoản đang đăng nhập!", "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                var user = userRepo.GetUserById(currentUserID);
                if (user == null)
                {
                    MessageBox.Show("Không tìm thấy người dùng!", "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Xác nhận xóa
                var result = MessageBox.Show(
                    $"Bạn có chắc chắn muốn xóa người dùng:\n\n" +
                    $"Username: {user.Username}\n" +
                    $"Họ tên: {user.FullName}\n" +
                    $"Role: {user.Role}\n\n" +
                    $"⚠️ Hành động này không thể hoàn tác!",
                    "Xác nhận xóa",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);

                if (result == DialogResult.Yes)
                {
                    bool success = userRepo.DeleteUser(currentUserID);

                    if (success)
                    {
                        MessageBox.Show("✅ Xóa người dùng thành công!", "Thành công",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadAllUsers();
                        ClearForm();
                        UpdateStatus($"Đã xóa: {user.Username}");
                    }
                    else
                    {
                        MessageBox.Show("❌ Xóa người dùng thất bại!", "Lỗi",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi xóa người dùng: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// ✅ Đổi mật khẩu (yêu cầu nhập mật khẩu cũ)
        /// </summary>
        private void btnChangePassword_Click(object sender, EventArgs e)
        {
            try
            {
                if (currentUserID == 0)
                {
                    MessageBox.Show("Vui lòng chọn người dùng cần đổi mật khẩu!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var user = userRepo.GetUserById(currentUserID);
                if (user == null)
                {
                    MessageBox.Show("Không tìm thấy người dùng!", "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Hiển thị form ChangePassword
                using (var form = new Form())
                {
                    form.Text = $"🔑 Đổi mật khẩu - {user.Username}";
                    form.Size = new System.Drawing.Size(450, 300);
                    form.StartPosition = FormStartPosition.CenterParent;
                    form.FormBorderStyle = FormBorderStyle.FixedDialog;
                    form.MaximizeBox = false;
                    form.MinimizeBox = false;

                    var lblOldPassword = new Label
                    {
                        Text = "Mật khẩu cũ (*)",
                        Location = new System.Drawing.Point(20, 20),
                        AutoSize = true
                    };

                    var txtOldPassword = new TextBox
                    {
                        Location = new System.Drawing.Point(20, 45),
                        Size = new System.Drawing.Size(380, 23),
                        UseSystemPasswordChar = true
                    };

                    var lblNewPassword = new Label
                    {
                        Text = "Mật khẩu mới (*)",
                        Location = new System.Drawing.Point(20, 80),
                        AutoSize = true
                    };

                    var txtNewPassword = new TextBox
                    {
                        Location = new System.Drawing.Point(20, 105),
                        Size = new System.Drawing.Size(380, 23),
                        UseSystemPasswordChar = true
                    };

                    var lblConfirmPassword = new Label
                    {
                        Text = "Xác nhận mật khẩu mới (*)",
                        Location = new System.Drawing.Point(20, 140),
                        AutoSize = true
                    };

                    var txtConfirmPassword = new TextBox
                    {
                        Location = new System.Drawing.Point(20, 165),
                        Size = new System.Drawing.Size(380, 23),
                        UseSystemPasswordChar = true
                    };

                    var btnOK = new Button
                    {
                        Text = "✅ Đổi mật khẩu",
                        Location = new System.Drawing.Point(150, 210),
                        Size = new System.Drawing.Size(120, 35),
                        DialogResult = DialogResult.OK,
                        BackColor = System.Drawing.Color.FromArgb(46, 204, 113),
                        ForeColor = System.Drawing.Color.White,
                        FlatStyle = FlatStyle.Flat
                    };

                    var btnCancel = new Button
                    {
                        Text = "❌ Hủy",
                        Location = new System.Drawing.Point(280, 210),
                        Size = new System.Drawing.Size(100, 35),
                        DialogResult = DialogResult.Cancel,
                        BackColor = System.Drawing.Color.FromArgb(149, 165, 166),
                        ForeColor = System.Drawing.Color.White,
                        FlatStyle = FlatStyle.Flat
                    };

                    form.Controls.Add(lblOldPassword);
                    form.Controls.Add(txtOldPassword);
                    form.Controls.Add(lblNewPassword);
                    form.Controls.Add(txtNewPassword);
                    form.Controls.Add(lblConfirmPassword);
                    form.Controls.Add(txtConfirmPassword);
                    form.Controls.Add(btnOK);
                    form.Controls.Add(btnCancel);

                    if (form.ShowDialog() == DialogResult.OK)
                    {
                        // Validate
                        if (string.IsNullOrWhiteSpace(txtOldPassword.Text))
                        {
                            MessageBox.Show("Vui lòng nhập mật khẩu cũ!", "Thông báo",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }

                        if (string.IsNullOrWhiteSpace(txtNewPassword.Text))
                        {
                            MessageBox.Show("Vui lòng nhập mật khẩu mới!", "Thông báo",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }

                        if (txtNewPassword.Text != txtConfirmPassword.Text)
                        {
                            MessageBox.Show("Xác nhận mật khẩu không khớp!", "Lỗi",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }

                        // Đổi mật khẩu
                        bool success = userRepo.ChangePassword(
                            currentUserID,
                            txtOldPassword.Text,
                            txtNewPassword.Text);

                        if (success)
                        {
                            MessageBox.Show("✅ Đổi mật khẩu thành công!", "Thành công",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                            UpdateStatus($"Đã đổi mật khẩu: {user.Username}");
                        }
                        else
                        {
                            MessageBox.Show("❌ Đổi mật khẩu thất bại!", "Lỗi",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi đổi mật khẩu: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Làm mới danh sách
        /// </summary>
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            try
            {
                LoadAllUsers();
                ClearForm();
                UpdateStatus("Đã làm mới danh sách");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi làm mới: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Clear form
        /// </summary>
        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearForm();
        }

        #endregion

        #region Helper Methods

        /// <summary>
        /// Load tất cả người dùng
        /// </summary>
        private void LoadAllUsers()
        {
            try
            {
                var users = userRepo.GetAllUsers();
                dgvUser.DataSource = null;
                dgvUser.DataSource = users;

                // Thiết lập DataGridView
                SetupDataGridView();
                UpdateRecordCount(users.Count);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải danh sách: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Thiết lập DataGridView
        /// </summary>
        private void SetupDataGridView()
        {
            try
            {
                if (dgvUser.Columns.Count == 0) return;

                // Ẩn Password
                if (dgvUser.Columns.Contains("Password"))
                    dgvUser.Columns["Password"]!.Visible = false;

                // Ẩn CreatedDate, UpdatedDate
                if (dgvUser.Columns.Contains("CreatedDate"))
                    dgvUser.Columns["CreatedDate"]!.Visible = false;

                if (dgvUser.Columns.Contains("UpdatedDate"))
                    dgvUser.Columns["UpdatedDate"]!.Visible = false;

                // Đặt tên tiêu đề cột
                if (dgvUser.Columns.Contains("UserID"))
                {
                    dgvUser.Columns["UserID"]!.HeaderText = "ID";
                    dgvUser.Columns["UserID"]!.Width = 60;
                }

                if (dgvUser.Columns.Contains("Username"))
                {
                    dgvUser.Columns["Username"]!.HeaderText = "Username";
                    dgvUser.Columns["Username"]!.Width = 150;
                }

                if (dgvUser.Columns.Contains("FullName"))
                {
                    dgvUser.Columns["FullName"]!.HeaderText = "Họ và tên";
                    dgvUser.Columns["FullName"]!.Width = 200;
                }

                if (dgvUser.Columns.Contains("Role"))
                {
                    dgvUser.Columns["Role"]!.HeaderText = "Phân quyền";
                    dgvUser.Columns["Role"]!.Width = 100;
                }

                if (dgvUser.Columns.Contains("Email"))
                {
                    dgvUser.Columns["Email"]!.HeaderText = "Email";
                    dgvUser.Columns["Email"]!.Width = 200;
                }

                if (dgvUser.Columns.Contains("Phone"))
                {
                    dgvUser.Columns["Phone"]!.HeaderText = "Số điện thoại";
                    dgvUser.Columns["Phone"]!.Width = 120;
                }

                if (dgvUser.Columns.Contains("IsActive"))
                {
                    dgvUser.Columns["IsActive"]!.HeaderText = "Hoạt động";
                    dgvUser.Columns["IsActive"]!.Width = 80;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi thiết lập DataGridView: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Clear form
        /// </summary>
        private void ClearForm()
        {
            currentUserID = 0;
            txtUsername.Clear();
            txtPassword.Clear();
            txtPassword.Enabled = true; // Bật lại để thêm mới
            txtFullName.Clear();
            cmbRole.SelectedIndex = 2; // SinhVien
            txtEmail.Clear();
            txtPhone.Clear();
            chkIsActive.Checked = true;
            txtUsername.Focus();
        }

        /// <summary>
        /// Cập nhật trạng thái
        /// </summary>
        private void UpdateStatus(string message)
        {
            lblStatus.Text = message;
        }

        /// <summary>
        /// Cập nhật số lượng record
        /// </summary>
        private void UpdateRecordCount(int count)
        {
            lblRecordCount.Text = $"Tổng: {count} người dùng";
        }

        #endregion
    }
}
