using StudentManagement.Helper;

namespace StudentManagement
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void BtnTest_Click(object? sender, EventArgs e)
        {
            Label? lblResult = this.Controls["lblResult"] as Label;
            
            if (lblResult == null) return;
            
            try
            {
                bool isConnected = DatabaseHelper.TestConnection();
                
                if (isConnected)
                {
                    lblResult.Text = "✅ Kết nối database thành công!\n" +
                                   "Database: StudentManagement\n" +
                                   "Server: localhost\\SQLEXPRESS";
                    lblResult.ForeColor = Color.Green;
                }
                else
                {
                    lblResult.Text = "❌ Kết nối database thất bại!";
                    lblResult.ForeColor = Color.Red;
                }
            }
            catch (Exception ex)
            {
                lblResult.Text = $"❌ Lỗi: {ex.Message}";
                lblResult.ForeColor = Color.Red;
            }
        }

        private void BtnAbout_Click(object? sender, EventArgs e)
        {
            AboutForm aboutForm = new AboutForm();
            aboutForm.ShowDialog(this);
        }
    }
}
