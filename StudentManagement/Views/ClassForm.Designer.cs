using System.Windows.Forms;
using System.Drawing;

namespace StudentManagement.Views
{
    partial class ClassForm
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
            this.components = new System.ComponentModel.Container();
            
            // Form
            this.SuspendLayout();
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new Size(1100, 650);
            this.MinimumSize = new Size(900, 500);
            this.Text = "Quản lý Lớp học";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Font = new Font("Segoe UI", 10F);

            // Panel Header
            this.pnlHeader = new Panel();
            this.pnlHeader.BackColor = Color.FromArgb(142, 68, 173);
            this.pnlHeader.Dock = DockStyle.Top;
            this.pnlHeader.Height = 60;

            this.lblTitle = new Label();
            this.lblTitle.Text = "QUẢN LÝ LỚP HỌC";
            this.lblTitle.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            this.lblTitle.ForeColor = Color.White;
            this.lblTitle.AutoSize = true;
            this.lblTitle.Location = new Point(20, 15);

            this.pnlHeader.Controls.Add(this.lblTitle);

            // GroupBox Thông tin
            this.grpInfo = new GroupBox();
            this.grpInfo.Text = "Thông tin lớp học";
            this.grpInfo.Location = new Point(12, 75);
            this.grpInfo.Size = new Size(380, 280);
            this.grpInfo.Anchor = AnchorStyles.Top | AnchorStyles.Left;

            // Labels
            this.lblClassName = new Label();
            this.lblClassName.Text = "Tên lớp:";
            this.lblClassName.Location = new Point(15, 30);
            this.lblClassName.AutoSize = true;

            this.lblTeacher = new Label();
            this.lblTeacher.Text = "Giáo viên:";
            this.lblTeacher.Location = new Point(15, 70);
            this.lblTeacher.AutoSize = true;

            this.lblCapacity = new Label();
            this.lblCapacity.Text = "Sĩ số:";
            this.lblCapacity.Location = new Point(15, 110);
            this.lblCapacity.AutoSize = true;

            this.lblRoom = new Label();
            this.lblRoom.Text = "Phòng học:";
            this.lblRoom.Location = new Point(15, 150);
            this.lblRoom.AutoSize = true;

            this.lblStudentCount = new Label();
            this.lblStudentCount.Text = "Số SV hiện tại:";
            this.lblStudentCount.Location = new Point(15, 190);
            this.lblStudentCount.AutoSize = true;

            // TextBox
            this.txtClassName = new TextBox();
            this.txtClassName.Location = new Point(130, 27);
            this.txtClassName.Size = new Size(230, 25);
            this.txtClassName.MaxLength = 50;

            this.txtRoom = new TextBox();
            this.txtRoom.Location = new Point(130, 147);
            this.txtRoom.Size = new Size(230, 25);
            this.txtRoom.MaxLength = 20;

            // ComboBox Teacher
            this.cmbTeacher = new ComboBox();
            this.cmbTeacher.Location = new Point(130, 67);
            this.cmbTeacher.Size = new Size(230, 25);
            this.cmbTeacher.DropDownStyle = ComboBoxStyle.DropDownList;

            // NumericUpDown Capacity
            this.numCapacity = new NumericUpDown();
            this.numCapacity.Location = new Point(130, 107);
            this.numCapacity.Size = new Size(230, 25);
            this.numCapacity.Minimum = 1;
            this.numCapacity.Maximum = 100;
            this.numCapacity.Value = 30;

            // Label Student Count (readonly)
            this.lblCurrentStudentCount = new Label();
            this.lblCurrentStudentCount.Text = "0";
            this.lblCurrentStudentCount.Location = new Point(130, 190);
            this.lblCurrentStudentCount.AutoSize = true;
            this.lblCurrentStudentCount.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.lblCurrentStudentCount.ForeColor = Color.FromArgb(41, 128, 185);

            // Add controls to GroupBox
            this.grpInfo.Controls.Add(this.lblClassName);
            this.grpInfo.Controls.Add(this.txtClassName);
            this.grpInfo.Controls.Add(this.lblTeacher);
            this.grpInfo.Controls.Add(this.cmbTeacher);
            this.grpInfo.Controls.Add(this.lblCapacity);
            this.grpInfo.Controls.Add(this.numCapacity);
            this.grpInfo.Controls.Add(this.lblRoom);
            this.grpInfo.Controls.Add(this.txtRoom);
            this.grpInfo.Controls.Add(this.lblStudentCount);
            this.grpInfo.Controls.Add(this.lblCurrentStudentCount);

            // Panel Buttons
            this.pnlButtons = new Panel();
            this.pnlButtons.Location = new Point(12, 365);
            this.pnlButtons.Size = new Size(380, 50);

            this.btnAdd = new Button();
            this.btnAdd.Text = "Thêm";
            this.btnAdd.Location = new Point(0, 10);
            this.btnAdd.Size = new Size(85, 35);
            this.btnAdd.BackColor = Color.FromArgb(46, 204, 113);
            this.btnAdd.ForeColor = Color.White;
            this.btnAdd.FlatStyle = FlatStyle.Flat;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);

            this.btnEdit = new Button();
            this.btnEdit.Text = "Sửa";
            this.btnEdit.Location = new Point(95, 10);
            this.btnEdit.Size = new Size(85, 35);
            this.btnEdit.BackColor = Color.FromArgb(52, 152, 219);
            this.btnEdit.ForeColor = Color.White;
            this.btnEdit.FlatStyle = FlatStyle.Flat;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);

            this.btnDelete = new Button();
            this.btnDelete.Text = "Xóa";
            this.btnDelete.Location = new Point(190, 10);
            this.btnDelete.Size = new Size(85, 35);
            this.btnDelete.BackColor = Color.FromArgb(231, 76, 60);
            this.btnDelete.ForeColor = Color.White;
            this.btnDelete.FlatStyle = FlatStyle.Flat;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);

            this.btnClear = new Button();
            this.btnClear.Text = "Làm mới";
            this.btnClear.Location = new Point(285, 10);
            this.btnClear.Size = new Size(85, 35);
            this.btnClear.BackColor = Color.FromArgb(149, 165, 166);
            this.btnClear.ForeColor = Color.White;
            this.btnClear.FlatStyle = FlatStyle.Flat;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);

            this.pnlButtons.Controls.Add(this.btnAdd);
            this.pnlButtons.Controls.Add(this.btnEdit);
            this.pnlButtons.Controls.Add(this.btnDelete);
            this.pnlButtons.Controls.Add(this.btnClear);

            // GroupBox Search
            this.grpSearch = new GroupBox();
            this.grpSearch.Text = "Tìm kiếm";
            this.grpSearch.Location = new Point(405, 75);
            this.grpSearch.Size = new Size(670, 70);
            this.grpSearch.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;

            this.lblSearch = new Label();
            this.lblSearch.Text = "Từ khóa:";
            this.lblSearch.Location = new Point(15, 30);
            this.lblSearch.AutoSize = true;

            this.txtSearch = new TextBox();
            this.txtSearch.Location = new Point(90, 27);
            this.txtSearch.Size = new Size(380, 25);
            this.txtSearch.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;

            this.btnSearch = new Button();
            this.btnSearch.Text = "Tìm kiếm";
            this.btnSearch.Location = new Point(485, 25);
            this.btnSearch.Size = new Size(85, 30);
            this.btnSearch.BackColor = Color.FromArgb(142, 68, 173);
            this.btnSearch.ForeColor = Color.White;
            this.btnSearch.FlatStyle = FlatStyle.Flat;
            this.btnSearch.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);

            this.btnRefresh = new Button();
            this.btnRefresh.Text = "Làm mới";
            this.btnRefresh.Location = new Point(580, 25);
            this.btnRefresh.Size = new Size(80, 30);
            this.btnRefresh.BackColor = Color.FromArgb(52, 73, 94);
            this.btnRefresh.ForeColor = Color.White;
            this.btnRefresh.FlatStyle = FlatStyle.Flat;
            this.btnRefresh.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);

            this.grpSearch.Controls.Add(this.lblSearch);
            this.grpSearch.Controls.Add(this.txtSearch);
            this.grpSearch.Controls.Add(this.btnSearch);
            this.grpSearch.Controls.Add(this.btnRefresh);

            // DataGridView
            this.dgvClass = new DataGridView();
            this.dgvClass.Location = new Point(405, 155);
            this.dgvClass.Size = new Size(670, 435);
            this.dgvClass.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            this.dgvClass.AllowUserToAddRows = false;
            this.dgvClass.AllowUserToDeleteRows = false;
            this.dgvClass.ReadOnly = true;
            this.dgvClass.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.dgvClass.MultiSelect = false;
            this.dgvClass.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvClass.BackgroundColor = Color.White;
            this.dgvClass.RowHeadersVisible = false;
            this.dgvClass.SelectionChanged += new System.EventHandler(this.dgvClass_SelectionChanged);

            // Status Strip
            this.statusStrip = new StatusStrip();
            this.lblStatus = new ToolStripStatusLabel();
            this.lblStatus.Text = "Sẵn sàng";
            this.statusStrip.Items.Add(this.lblStatus);

            this.lblRecordCount = new ToolStripStatusLabel();
            this.lblRecordCount.Text = "Tổng: 0 lớp";
            this.lblRecordCount.Spring = true;
            this.lblRecordCount.TextAlign = ContentAlignment.MiddleRight;
            this.statusStrip.Items.Add(this.lblRecordCount);

            // Add controls to Form
            this.Controls.Add(this.pnlHeader);
            this.Controls.Add(this.grpInfo);
            this.Controls.Add(this.pnlButtons);
            this.Controls.Add(this.grpSearch);
            this.Controls.Add(this.dgvClass);
            this.Controls.Add(this.statusStrip);

            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private Panel pnlHeader;
        private Label lblTitle;
        private GroupBox grpInfo;
        private Label lblClassName;
        private Label lblTeacher;
        private Label lblCapacity;
        private Label lblRoom;
        private Label lblStudentCount;
        private Label lblCurrentStudentCount;
        private TextBox txtClassName;
        private TextBox txtRoom;
        private ComboBox cmbTeacher;
        private NumericUpDown numCapacity;
        private Panel pnlButtons;
        private Button btnAdd;
        private Button btnEdit;
        private Button btnDelete;
        private Button btnClear;
        private GroupBox grpSearch;
        private Label lblSearch;
        private TextBox txtSearch;
        private Button btnSearch;
        private Button btnRefresh;
        private DataGridView dgvClass;
        private StatusStrip statusStrip;
        private ToolStripStatusLabel lblStatus;
        private ToolStripStatusLabel lblRecordCount;
    }
}
