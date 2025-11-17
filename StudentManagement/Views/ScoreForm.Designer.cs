using System.Drawing;
using System.Windows.Forms;

namespace StudentManagement.Views
{
    partial class ScoreForm : Form
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
            
            // ✅ Form - RESIZABLE
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1400, 800);
            this.MinimumSize = new System.Drawing.Size(1200, 700);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Sizable;
            this.MaximizeBox = true;
            this.MinimizeBox = true;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Quản lý Điểm";
            this.BackColor = System.Drawing.Color.WhiteSmoke;

            // Panel Header
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.lblHeader = new System.Windows.Forms.Label();
            
            // GroupBox Filter
            this.grpFilter = new System.Windows.Forms.GroupBox();
            this.lblClass = new System.Windows.Forms.Label();
            this.cmbClass = new System.Windows.Forms.ComboBox();
            this.lblStudent = new System.Windows.Forms.Label();
            this.cmbStudent = new System.Windows.Forms.ComboBox();
            this.lblSemester = new System.Windows.Forms.Label();
            this.cmbSemester = new System.Windows.Forms.ComboBox();
            this.lblAcademicYear = new System.Windows.Forms.Label();
            this.txtAcademicYear = new System.Windows.Forms.TextBox();
            this.btnFilterBySemester = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            
            // GroupBox Score Info
            this.grpScoreInfo = new System.Windows.Forms.GroupBox();
            this.lblSubject = new System.Windows.Forms.Label();
            this.txtSubject = new System.Windows.Forms.TextBox();
            this.lblScore = new System.Windows.Forms.Label();
            this.numScore = new System.Windows.Forms.NumericUpDown();
            this.lblScoreDate = new System.Windows.Forms.Label();
            this.dtpScoreDate = new System.Windows.Forms.DateTimePicker();
            this.lblNotes = new System.Windows.Forms.Label();
            this.txtNotes = new System.Windows.Forms.TextBox();
            
            // Buttons
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            
            // DataGridView
            this.dgvScore = new System.Windows.Forms.DataGridView();
            
            // GroupBox Statistics
            this.grpStatistics = new System.Windows.Forms.GroupBox();
            this.lblAverageScoreLabel = new System.Windows.Forms.Label();
            this.lblAverageScore = new System.Windows.Forms.Label();
            this.lblGrade = new System.Windows.Forms.Label();
            this.lblGradeValue = new System.Windows.Forms.Label();
            this.lblSubjectCount = new System.Windows.Forms.Label();
            this.lblSubjectCountValue = new System.Windows.Forms.Label();
            
            // StatusStrip
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.lblStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblRecordCount = new System.Windows.Forms.ToolStripStatusLabel();
            
            this.pnlHeader.SuspendLayout();
            this.grpFilter.SuspendLayout();
            this.grpScoreInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numScore)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvScore)).BeginInit();
            this.grpStatistics.SuspendLayout();
            this.statusStrip.SuspendLayout();
            this.SuspendLayout();

            // 
            // pnlHeader - ✅ Dock Top
            // 
            this.pnlHeader.BackColor = System.Drawing.Color.FromArgb(231, 76, 60);
            this.pnlHeader.Controls.Add(this.lblHeader);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size = new System.Drawing.Size(1400, 60);
            this.pnlHeader.TabIndex = 0;

            // 
            // lblHeader
            // 
            this.lblHeader.AutoSize = true;
            this.lblHeader.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.lblHeader.ForeColor = System.Drawing.Color.White;
            this.lblHeader.Location = new System.Drawing.Point(20, 13);
            this.lblHeader.Name = "lblHeader";
            this.lblHeader.Size = new System.Drawing.Size(250, 32);
            this.lblHeader.TabIndex = 0;
            this.lblHeader.Text = "📊 QUẢN LÝ ĐIỂM";

            // 
            // grpFilter - ✅ Anchor Top + Left + Right
            // 
            this.grpFilter.Controls.Add(this.lblClass);
            this.grpFilter.Controls.Add(this.cmbClass);
            this.grpFilter.Controls.Add(this.lblStudent);
            this.grpFilter.Controls.Add(this.cmbStudent);
            this.grpFilter.Controls.Add(this.lblSemester);
            this.grpFilter.Controls.Add(this.cmbSemester);
            this.grpFilter.Controls.Add(this.lblAcademicYear);
            this.grpFilter.Controls.Add(this.txtAcademicYear);
            this.grpFilter.Controls.Add(this.btnFilterBySemester);
            this.grpFilter.Controls.Add(this.btnRefresh);
            this.grpFilter.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.grpFilter.Location = new System.Drawing.Point(20, 75);
            this.grpFilter.Name = "grpFilter";
            this.grpFilter.Size = new System.Drawing.Size(1360, 100);
            this.grpFilter.TabIndex = 1;
            this.grpFilter.TabStop = false;
            this.grpFilter.Text = "Bộ lọc";
            this.grpFilter.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;

            // 
            // lblClass
            // 
            this.lblClass.AutoSize = true;
            this.lblClass.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblClass.Location = new System.Drawing.Point(15, 30);
            this.lblClass.Name = "lblClass";
            this.lblClass.Size = new System.Drawing.Size(50, 15);
            this.lblClass.TabIndex = 0;
            this.lblClass.Text = "Lớp học";

            // 
            // cmbClass
            // 
            this.cmbClass.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.cmbClass.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbClass.Location = new System.Drawing.Point(15, 50);
            this.cmbClass.Name = "cmbClass";
            this.cmbClass.Size = new System.Drawing.Size(200, 23);
            this.cmbClass.TabIndex = 1;
            this.cmbClass.SelectedIndexChanged += new System.EventHandler(this.cmbClass_SelectedIndexChanged);

            // 
            // lblStudent
            // 
            this.lblStudent.AutoSize = true;
            this.lblStudent.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblStudent.Location = new System.Drawing.Point(230, 30);
            this.lblStudent.Name = "lblStudent";
            this.lblStudent.Size = new System.Drawing.Size(56, 15);
            this.lblStudent.TabIndex = 2;
            this.lblStudent.Text = "Sinh viên";

            // 
            // cmbStudent
            // 
            this.cmbStudent.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.cmbStudent.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbStudent.Location = new System.Drawing.Point(230, 50);
            this.cmbStudent.Name = "cmbStudent";
            this.cmbStudent.Size = new System.Drawing.Size(300, 23);
            this.cmbStudent.TabIndex = 3;
            this.cmbStudent.SelectedIndexChanged += new System.EventHandler(this.cmbStudent_SelectedIndexChanged);

            // 
            // lblSemester
            // 
            this.lblSemester.AutoSize = true;
            this.lblSemester.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblSemester.Location = new System.Drawing.Point(550, 30);
            this.lblSemester.Name = "lblSemester";
            this.lblSemester.Size = new System.Drawing.Size(44, 15);
            this.lblSemester.TabIndex = 4;
            this.lblSemester.Text = "Học kỳ";

            // 
            // cmbSemester
            // 
            this.cmbSemester.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.cmbSemester.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSemester.Location = new System.Drawing.Point(550, 50);
            this.cmbSemester.Name = "cmbSemester";
            this.cmbSemester.Size = new System.Drawing.Size(100, 23);
            this.cmbSemester.TabIndex = 5;
            this.cmbSemester.Items.AddRange(new object[] { "Tất cả", "Học kỳ 1", "Học kỳ 2", "Học kỳ 3" });
            this.cmbSemester.SelectedIndex = 0;

            // 
            // lblAcademicYear
            // 
            this.lblAcademicYear.AutoSize = true;
            this.lblAcademicYear.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblAcademicYear.Location = new System.Drawing.Point(670, 30);
            this.lblAcademicYear.Name = "lblAcademicYear";
            this.lblAcademicYear.Size = new System.Drawing.Size(52, 15);
            this.lblAcademicYear.TabIndex = 6;
            this.lblAcademicYear.Text = "Năm học";

            // 
            // txtAcademicYear
            // 
            this.txtAcademicYear.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtAcademicYear.Location = new System.Drawing.Point(670, 50);
            this.txtAcademicYear.Name = "txtAcademicYear";
            this.txtAcademicYear.Size = new System.Drawing.Size(120, 23);
            this.txtAcademicYear.TabIndex = 7;
            this.txtAcademicYear.Text = "2024-2025";

            // 
            // btnFilterBySemester - ✅ Anchor Top + Right
            // 
            this.btnFilterBySemester.BackColor = System.Drawing.Color.FromArgb(52, 152, 219);
            this.btnFilterBySemester.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFilterBySemester.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnFilterBySemester.ForeColor = System.Drawing.Color.White;
            this.btnFilterBySemester.Location = new System.Drawing.Point(810, 47);
            this.btnFilterBySemester.Name = "btnFilterBySemester";
            this.btnFilterBySemester.Size = new System.Drawing.Size(100, 30);
            this.btnFilterBySemester.TabIndex = 8;
            this.btnFilterBySemester.Text = "🔍 Lọc";
            this.btnFilterBySemester.UseVisualStyleBackColor = false;
            this.btnFilterBySemester.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            this.btnFilterBySemester.Click += new System.EventHandler(this.btnFilterBySemester_Click);

            // 
            // btnRefresh - ✅ Anchor Top + Right
            // 
            this.btnRefresh.BackColor = System.Drawing.Color.FromArgb(95, 106, 106);
            this.btnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRefresh.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnRefresh.ForeColor = System.Drawing.Color.White;
            this.btnRefresh.Location = new System.Drawing.Point(920, 47);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(100, 30);
            this.btnRefresh.TabIndex = 9;
            this.btnRefresh.Text = "🔄 Làm mới";
            this.btnRefresh.UseVisualStyleBackColor = false;
            this.btnRefresh.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);

            // 
            // grpScoreInfo - ✅ Anchor Top + Left
            // 
            this.grpScoreInfo.Controls.Add(this.lblSubject);
            this.grpScoreInfo.Controls.Add(this.txtSubject);
            this.grpScoreInfo.Controls.Add(this.lblScore);
            this.grpScoreInfo.Controls.Add(this.numScore);
            this.grpScoreInfo.Controls.Add(this.lblScoreDate);
            this.grpScoreInfo.Controls.Add(this.dtpScoreDate);
            this.grpScoreInfo.Controls.Add(this.lblNotes);
            this.grpScoreInfo.Controls.Add(this.txtNotes);
            this.grpScoreInfo.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.grpScoreInfo.Location = new System.Drawing.Point(20, 190);
            this.grpScoreInfo.Name = "grpScoreInfo";
            this.grpScoreInfo.Size = new System.Drawing.Size(380, 280);
            this.grpScoreInfo.TabIndex = 2;
            this.grpScoreInfo.TabStop = false;
            this.grpScoreInfo.Text = "Thông tin điểm";
            this.grpScoreInfo.Anchor = AnchorStyles.Top | AnchorStyles.Left;

            // 
            // lblSubject
            // 
            this.lblSubject.AutoSize = true;
            this.lblSubject.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblSubject.Location = new System.Drawing.Point(15, 30);
            this.lblSubject.Name = "lblSubject";
            this.lblSubject.Size = new System.Drawing.Size(80, 15);
            this.lblSubject.TabIndex = 0;
            this.lblSubject.Text = "Tên môn học (*)";

            // 
            // txtSubject
            // 
            this.txtSubject.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtSubject.Location = new System.Drawing.Point(15, 50);
            this.txtSubject.Name = "txtSubject";
            this.txtSubject.Size = new System.Drawing.Size(350, 23);
            this.txtSubject.TabIndex = 1;
            this.txtSubject.MaxLength = 100;

            // 
            // lblScore
            // 
            this.lblScore.AutoSize = true;
            this.lblScore.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblScore.Location = new System.Drawing.Point(15, 85);
            this.lblScore.Name = "lblScore";
            this.lblScore.Size = new System.Drawing.Size(80, 15);
            this.lblScore.TabIndex = 2;
            this.lblScore.Text = "Điểm (0-10) (*)";

            // 
            // numScore
            // 
            this.numScore.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.numScore.DecimalPlaces = 1;
            this.numScore.Increment = new decimal(new int[] { 1, 0, 0, 65536 }); // 0.1
            this.numScore.Location = new System.Drawing.Point(15, 105);
            this.numScore.Maximum = new decimal(new int[] { 10, 0, 0, 0 });
            this.numScore.Minimum = new decimal(new int[] { 0, 0, 0, 0 });
            this.numScore.Name = "numScore";
            this.numScore.Size = new System.Drawing.Size(150, 23);
            this.numScore.TabIndex = 3;

            // 
            // lblScoreDate
            // 
            this.lblScoreDate.AutoSize = true;
            this.lblScoreDate.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblScoreDate.Location = new System.Drawing.Point(185, 85);
            this.lblScoreDate.Name = "lblScoreDate";
            this.lblScoreDate.Size = new System.Drawing.Size(59, 15);
            this.lblScoreDate.TabIndex = 4;
            this.lblScoreDate.Text = "Ngày thi";

            // 
            // dtpScoreDate
            // 
            this.dtpScoreDate.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.dtpScoreDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpScoreDate.Location = new System.Drawing.Point(185, 105);
            this.dtpScoreDate.Name = "dtpScoreDate";
            this.dtpScoreDate.Size = new System.Drawing.Size(180, 23);
            this.dtpScoreDate.TabIndex = 5;

            // 
            // lblNotes
            // 
            this.lblNotes.AutoSize = true;
            this.lblNotes.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblNotes.Location = new System.Drawing.Point(15, 140);
            this.lblNotes.Name = "lblNotes";
            this.lblNotes.Size = new System.Drawing.Size(45, 15);
            this.lblNotes.TabIndex = 6;
            this.lblNotes.Text = "Ghi chú";

            // 
            // txtNotes
            // 
            this.txtNotes.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtNotes.Location = new System.Drawing.Point(15, 160);
            this.txtNotes.Multiline = true;
            this.txtNotes.Name = "txtNotes";
            this.txtNotes.Size = new System.Drawing.Size(350, 100);
            this.txtNotes.TabIndex = 7;
            this.txtNotes.MaxLength = 200;

            // 
            // btnAdd - ✅ Anchor Bottom + Left
            // 
            this.btnAdd.BackColor = System.Drawing.Color.FromArgb(46, 204, 113);
            this.btnAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAdd.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnAdd.ForeColor = System.Drawing.Color.White;
            this.btnAdd.Location = new System.Drawing.Point(20, 485);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(90, 35);
            this.btnAdd.TabIndex = 3;
            this.btnAdd.Text = "➕ Thêm";
            this.btnAdd.UseVisualStyleBackColor = false;
            this.btnAdd.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);

            // 
            // btnEdit - ✅ Anchor Bottom + Left
            // 
            this.btnEdit.BackColor = System.Drawing.Color.FromArgb(52, 152, 219);
            this.btnEdit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEdit.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnEdit.ForeColor = System.Drawing.Color.White;
            this.btnEdit.Location = new System.Drawing.Point(120, 485);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(90, 35);
            this.btnEdit.TabIndex = 4;
            this.btnEdit.Text = "✏️ Sửa";
            this.btnEdit.UseVisualStyleBackColor = false;
            this.btnEdit.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);

            // 
            // btnDelete - ✅ Anchor Bottom + Left
            // 
            this.btnDelete.BackColor = System.Drawing.Color.FromArgb(231, 76, 60);
            this.btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDelete.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnDelete.ForeColor = System.Drawing.Color.White;
            this.btnDelete.Location = new System.Drawing.Point(220, 485);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(90, 35);
            this.btnDelete.TabIndex = 5;
            this.btnDelete.Text = "🗑️ Xóa";
            this.btnDelete.UseVisualStyleBackColor = false;
            this.btnDelete.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);

            // 
            // btnClear - ✅ Anchor Bottom + Left
            // 
            this.btnClear.BackColor = System.Drawing.Color.FromArgb(149, 165, 166);
            this.btnClear.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClear.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnClear.ForeColor = System.Drawing.Color.White;
            this.btnClear.Location = new System.Drawing.Point(320, 485);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(80, 35);
            this.btnClear.TabIndex = 6;
            this.btnClear.Text = "🔄 Clear";
            this.btnClear.UseVisualStyleBackColor = false;
            this.btnClear.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);

            // 
            // dgvScore - ✅ Anchor All Sides
            // 
            this.dgvScore.AllowUserToAddRows = false;
            this.dgvScore.AllowUserToDeleteRows = false;
            this.dgvScore.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvScore.BackgroundColor = System.Drawing.Color.White;
            this.dgvScore.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvScore.Location = new System.Drawing.Point(420, 190);
            this.dgvScore.MultiSelect = false;
            this.dgvScore.Name = "dgvScore";
            this.dgvScore.ReadOnly = true;
            this.dgvScore.RowHeadersWidth = 51;
            this.dgvScore.RowTemplate.Height = 25;
            this.dgvScore.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvScore.Size = new System.Drawing.Size(960, 460);
            this.dgvScore.TabIndex = 7;
            this.dgvScore.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            this.dgvScore.SelectionChanged += new System.EventHandler(this.dgvScore_SelectionChanged);

            // 
            // grpStatistics - ✅ Anchor Bottom + Left
            // 
            this.grpStatistics.Controls.Add(this.lblAverageScoreLabel);
            this.grpStatistics.Controls.Add(this.lblAverageScore);
            this.grpStatistics.Controls.Add(this.lblGrade);
            this.grpStatistics.Controls.Add(this.lblGradeValue);
            this.grpStatistics.Controls.Add(this.lblSubjectCount);
            this.grpStatistics.Controls.Add(this.lblSubjectCountValue);
            this.grpStatistics.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.grpStatistics.Location = new System.Drawing.Point(420, 665);
            this.grpStatistics.Name = "grpStatistics";
            this.grpStatistics.Size = new System.Drawing.Size(500, 80);
            this.grpStatistics.TabIndex = 8;
            this.grpStatistics.TabStop = false;
            this.grpStatistics.Text = "Thống kê";
            this.grpStatistics.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;

            // 
            // lblAverageScoreLabel
            // 
            this.lblAverageScoreLabel.AutoSize = true;
            this.lblAverageScoreLabel.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblAverageScoreLabel.Location = new System.Drawing.Point(15, 30);
            this.lblAverageScoreLabel.Name = "lblAverageScoreLabel";
            this.lblAverageScoreLabel.Size = new System.Drawing.Size(90, 15);
            this.lblAverageScoreLabel.TabIndex = 0;
            this.lblAverageScoreLabel.Text = "Điểm trung bình:";

            // 
            // lblAverageScore
            // 
            this.lblAverageScore.AutoSize = true;
            this.lblAverageScore.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblAverageScore.ForeColor = System.Drawing.Color.FromArgb(231, 76, 60);
            this.lblAverageScore.Location = new System.Drawing.Point(110, 25);
            this.lblAverageScore.Name = "lblAverageScore";
            this.lblAverageScore.Size = new System.Drawing.Size(35, 25);
            this.lblAverageScore.TabIndex = 1;
            this.lblAverageScore.Text = "0.0";

            // 
            // lblGrade
            // 
            this.lblGrade.AutoSize = true;
            this.lblGrade.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblGrade.Location = new System.Drawing.Point(15, 55);
            this.lblGrade.Name = "lblGrade";
            this.lblGrade.Size = new System.Drawing.Size(53, 15);
            this.lblGrade.TabIndex = 2;
            this.lblGrade.Text = "Xếp loại:";

            // 
            // lblGradeValue
            // 
            this.lblGradeValue.AutoSize = true;
            this.lblGradeValue.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblGradeValue.ForeColor = System.Drawing.Color.FromArgb(52, 152, 219);
            this.lblGradeValue.Location = new System.Drawing.Point(75, 53);
            this.lblGradeValue.Name = "lblGradeValue";
            this.lblGradeValue.Size = new System.Drawing.Size(17, 20);
            this.lblGradeValue.TabIndex = 3;
            this.lblGradeValue.Text = "-";

            // 
            // lblSubjectCount
            // 
            this.lblSubjectCount.AutoSize = true;
            this.lblSubjectCount.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblSubjectCount.Location = new System.Drawing.Point(250, 30);
            this.lblSubjectCount.Name = "lblSubjectCount";
            this.lblSubjectCount.Size = new System.Drawing.Size(50, 15);
            this.lblSubjectCount.TabIndex = 4;
            this.lblSubjectCount.Text = "Số môn:";

            // 
            // lblSubjectCountValue
            // 
            this.lblSubjectCountValue.AutoSize = true;
            this.lblSubjectCountValue.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblSubjectCountValue.ForeColor = System.Drawing.Color.FromArgb(46, 204, 113);
            this.lblSubjectCountValue.Location = new System.Drawing.Point(310, 27);
            this.lblSubjectCountValue.Name = "lblSubjectCountValue";
            this.lblSubjectCountValue.Size = new System.Drawing.Size(19, 21);
            this.lblSubjectCountValue.TabIndex = 5;
            this.lblSubjectCountValue.Text = "0";

            // 
            // statusStrip - ✅ Dock Bottom
            // 
            this.statusStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
                this.lblStatus,
                this.lblRecordCount});
            this.statusStrip.Location = new System.Drawing.Point(0, 776);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(1400, 24);
            this.statusStrip.TabIndex = 9;
            this.statusStrip.Dock = DockStyle.Bottom;

            // 
            // lblStatus
            // 
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(56, 19);
            this.lblStatus.Text = "Sẵn sàng";

            // 
            // lblRecordCount
            // 
            this.lblRecordCount.Name = "lblRecordCount";
            this.lblRecordCount.Size = new System.Drawing.Size(1329, 19);
            this.lblRecordCount.Spring = true;
            this.lblRecordCount.Text = "Tổng: 0 điểm";
            this.lblRecordCount.TextAlign = System.Drawing.ContentAlignment.MiddleRight;

            // 
            // ScoreForm
            // 
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.grpStatistics);
            this.Controls.Add(this.dgvScore);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnEdit);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.grpScoreInfo);
            this.Controls.Add(this.grpFilter);
            this.Controls.Add(this.pnlHeader);
            this.Name = "ScoreForm";
            this.Load += new System.EventHandler(this.ScoreForm_Load);
            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            this.grpFilter.ResumeLayout(false);
            this.grpFilter.PerformLayout();
            this.grpScoreInfo.ResumeLayout(false);
            this.grpScoreInfo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numScore)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvScore)).EndInit();
            this.grpStatistics.ResumeLayout(false);
            this.grpStatistics.PerformLayout();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label lblHeader;
        private System.Windows.Forms.GroupBox grpFilter;
        private System.Windows.Forms.Label lblClass;
        private System.Windows.Forms.ComboBox cmbClass;
        private System.Windows.Forms.Label lblStudent;
        private System.Windows.Forms.ComboBox cmbStudent;
        private System.Windows.Forms.Label lblSemester;
        private System.Windows.Forms.ComboBox cmbSemester;
        private System.Windows.Forms.Label lblAcademicYear;
        private System.Windows.Forms.TextBox txtAcademicYear;
        private System.Windows.Forms.Button btnFilterBySemester;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.GroupBox grpScoreInfo;
        private System.Windows.Forms.Label lblSubject;
        private System.Windows.Forms.TextBox txtSubject;
        private System.Windows.Forms.Label lblScore;
        private System.Windows.Forms.NumericUpDown numScore;
        private System.Windows.Forms.Label lblScoreDate;
        private System.Windows.Forms.DateTimePicker dtpScoreDate;
        private System.Windows.Forms.Label lblNotes;
        private System.Windows.Forms.TextBox txtNotes;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.DataGridView dgvScore;
        private System.Windows.Forms.GroupBox grpStatistics;
        private System.Windows.Forms.Label lblAverageScoreLabel;
        private System.Windows.Forms.Label lblAverageScore;
        private System.Windows.Forms.Label lblGrade;
        private System.Windows.Forms.Label lblGradeValue;
        private System.Windows.Forms.Label lblSubjectCount;
        private System.Windows.Forms.Label lblSubjectCountValue;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel lblStatus;
        private System.Windows.Forms.ToolStripStatusLabel lblRecordCount;
    }
}
