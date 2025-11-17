namespace StudentManagement.Views
{
    partial class ReportScoreForm
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
            
            // Panel Top
            this.pnlTop = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            
            // GroupBox Filter
            this.grpFilter = new System.Windows.Forms.GroupBox();
            this.lblClass = new System.Windows.Forms.Label();
            this.cmbClass = new System.Windows.Forms.ComboBox();
            this.lblSubject = new System.Windows.Forms.Label();
            this.cmbSubject = new System.Windows.Forms.ComboBox();
            this.lblSemester = new System.Windows.Forms.Label();
            this.cmbSemester = new System.Windows.Forms.ComboBox();
            this.lblAcademicYear = new System.Windows.Forms.Label();
            this.txtAcademicYear = new System.Windows.Forms.TextBox();
            this.lblScoreRange = new System.Windows.Forms.Label();
            this.txtScoreFrom = new System.Windows.Forms.TextBox();
            this.lblTo = new System.Windows.Forms.Label();
            this.txtScoreTo = new System.Windows.Forms.TextBox();
            this.btnFilter = new System.Windows.Forms.Button();
            this.btnReset = new System.Windows.Forms.Button();
            
            // GroupBox Statistics
            this.grpStatistics = new System.Windows.Forms.GroupBox();
            this.lblAverage = new System.Windows.Forms.Label();
            this.lblMax = new System.Windows.Forms.Label();
            this.lblMin = new System.Windows.Forms.Label();
            this.lblExcellent = new System.Windows.Forms.Label();
            this.lblExcellentPercent = new System.Windows.Forms.Label();
            this.lblGood = new System.Windows.Forms.Label();
            this.lblGoodPercent = new System.Windows.Forms.Label();
            this.lblFair = new System.Windows.Forms.Label();
            this.lblFairPercent = new System.Windows.Forms.Label();
            this.lblAvgLevel = new System.Windows.Forms.Label();
            this.lblAvgPercent = new System.Windows.Forms.Label();
            this.lblPoor = new System.Windows.Forms.Label();
            this.lblPoorPercent = new System.Windows.Forms.Label();
            
            // DataGridView
            this.dgvReport = new System.Windows.Forms.DataGridView();
            
            // Panel Bottom
            this.pnlBottom = new System.Windows.Forms.Panel();
            this.btnExport = new System.Windows.Forms.Button();
            this.btnPrint = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.lblTotalRecords = new System.Windows.Forms.Label();
            this.lblStatus = new System.Windows.Forms.Label();
            
            this.pnlTop.SuspendLayout();
            this.grpFilter.SuspendLayout();
            this.grpStatistics.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvReport)).BeginInit();
            this.pnlBottom.SuspendLayout();
            this.SuspendLayout();
            
            // 
            // pnlTop
            // 
            this.pnlTop.BackColor = System.Drawing.Color.FromArgb(41, 128, 185);
            this.pnlTop.Controls.Add(this.lblTitle);
            this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTop.Location = new System.Drawing.Point(0, 0);
            this.pnlTop.Name = "pnlTop";
            this.pnlTop.Size = new System.Drawing.Size(1400, 60);
            this.pnlTop.TabIndex = 0;
            
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(20, 15);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(280, 32);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "📊 BÁO CÁO ĐIỂM SỐ";
            
            // 
            // grpFilter
            // 
            this.grpFilter.Controls.Add(this.lblClass);
            this.grpFilter.Controls.Add(this.cmbClass);
            this.grpFilter.Controls.Add(this.lblSubject);
            this.grpFilter.Controls.Add(this.cmbSubject);
            this.grpFilter.Controls.Add(this.lblSemester);
            this.grpFilter.Controls.Add(this.cmbSemester);
            this.grpFilter.Controls.Add(this.lblAcademicYear);
            this.grpFilter.Controls.Add(this.txtAcademicYear);
            this.grpFilter.Controls.Add(this.lblScoreRange);
            this.grpFilter.Controls.Add(this.txtScoreFrom);
            this.grpFilter.Controls.Add(this.lblTo);
            this.grpFilter.Controls.Add(this.txtScoreTo);
            this.grpFilter.Controls.Add(this.btnFilter);
            this.grpFilter.Controls.Add(this.btnReset);
            this.grpFilter.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.grpFilter.Location = new System.Drawing.Point(20, 80);
            this.grpFilter.Name = "grpFilter";
            this.grpFilter.Size = new System.Drawing.Size(1000, 140);
            this.grpFilter.TabIndex = 1;
            this.grpFilter.TabStop = false;
            this.grpFilter.Text = "🔍 Bộ lọc";
            
            // 
            // lblClass
            // 
            this.lblClass.AutoSize = true;
            this.lblClass.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.lblClass.Location = new System.Drawing.Point(20, 35);
            this.lblClass.Name = "lblClass";
            this.lblClass.Size = new System.Drawing.Size(35, 17);
            this.lblClass.TabIndex = 0;
            this.lblClass.Text = "Lớp:";
            
            // 
            // cmbClass
            // 
            this.cmbClass.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbClass.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.cmbClass.FormattingEnabled = true;
            this.cmbClass.Location = new System.Drawing.Point(120, 32);
            this.cmbClass.Name = "cmbClass";
            this.cmbClass.Size = new System.Drawing.Size(200, 25);
            this.cmbClass.TabIndex = 1;
            
            // 
            // lblSubject
            // 
            this.lblSubject.AutoSize = true;
            this.lblSubject.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.lblSubject.Location = new System.Drawing.Point(340, 35);
            this.lblSubject.Name = "lblSubject";
            this.lblSubject.Size = new System.Drawing.Size(66, 17);
            this.lblSubject.TabIndex = 2;
            this.lblSubject.Text = "Môn học:";
            
            // 
            // cmbSubject
            // 
            this.cmbSubject.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSubject.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.cmbSubject.FormattingEnabled = true;
            this.cmbSubject.Location = new System.Drawing.Point(420, 32);
            this.cmbSubject.Name = "cmbSubject";
            this.cmbSubject.Size = new System.Drawing.Size(200, 25);
            this.cmbSubject.TabIndex = 3;
            
            // 
            // lblSemester
            // 
            this.lblSemester.AutoSize = true;
            this.lblSemester.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.lblSemester.Location = new System.Drawing.Point(640, 35);
            this.lblSemester.Name = "lblSemester";
            this.lblSemester.Size = new System.Drawing.Size(54, 17);
            this.lblSemester.TabIndex = 4;
            this.lblSemester.Text = "Học kỳ:";
            
            // 
            // cmbSemester
            // 
            this.cmbSemester.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSemester.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.cmbSemester.FormattingEnabled = true;
            this.cmbSemester.Location = new System.Drawing.Point(710, 32);
            this.cmbSemester.Name = "cmbSemester";
            this.cmbSemester.Size = new System.Drawing.Size(120, 25);
            this.cmbSemester.TabIndex = 5;
            
            // 
            // lblAcademicYear
            // 
            this.lblAcademicYear.AutoSize = true;
            this.lblAcademicYear.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.lblAcademicYear.Location = new System.Drawing.Point(20, 75);
            this.lblAcademicYear.Name = "lblAcademicYear";
            this.lblAcademicYear.Size = new System.Drawing.Size(67, 17);
            this.lblAcademicYear.TabIndex = 6;
            this.lblAcademicYear.Text = "Năm học:";
            
            // 
            // txtAcademicYear
            // 
            this.txtAcademicYear.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.txtAcademicYear.Location = new System.Drawing.Point(120, 72);
            this.txtAcademicYear.Name = "txtAcademicYear";
            this.txtAcademicYear.Size = new System.Drawing.Size(200, 25);
            this.txtAcademicYear.TabIndex = 7;
            this.txtAcademicYear.Text = "2024-2025";
            
            // 
            // lblScoreRange
            // 
            this.lblScoreRange.AutoSize = true;
            this.lblScoreRange.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.lblScoreRange.Location = new System.Drawing.Point(340, 75);
            this.lblScoreRange.Name = "lblScoreRange";
            this.lblScoreRange.Size = new System.Drawing.Size(93, 17);
            this.lblScoreRange.TabIndex = 8;
            this.lblScoreRange.Text = "Khoảng điểm:";
            
            // 
            // txtScoreFrom
            // 
            this.txtScoreFrom.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.txtScoreFrom.Location = new System.Drawing.Point(440, 72);
            this.txtScoreFrom.Name = "txtScoreFrom";
            this.txtScoreFrom.Size = new System.Drawing.Size(70, 25);
            this.txtScoreFrom.TabIndex = 9;
            this.txtScoreFrom.Text = "0";
            this.txtScoreFrom.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            
            // 
            // lblTo
            // 
            this.lblTo.AutoSize = true;
            this.lblTo.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.lblTo.Location = new System.Drawing.Point(520, 75);
            this.lblTo.Name = "lblTo";
            this.lblTo.Size = new System.Drawing.Size(29, 17);
            this.lblTo.TabIndex = 10;
            this.lblTo.Text = "đến";
            
            // 
            // txtScoreTo
            // 
            this.txtScoreTo.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.txtScoreTo.Location = new System.Drawing.Point(560, 72);
            this.txtScoreTo.Name = "txtScoreTo";
            this.txtScoreTo.Size = new System.Drawing.Size(70, 25);
            this.txtScoreTo.TabIndex = 11;
            this.txtScoreTo.Text = "10";
            this.txtScoreTo.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            
            // 
            // btnFilter
            // 
            this.btnFilter.BackColor = System.Drawing.Color.FromArgb(46, 204, 113);
            this.btnFilter.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnFilter.FlatAppearance.BorderSize = 0;
            this.btnFilter.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFilter.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnFilter.ForeColor = System.Drawing.Color.White;
            this.btnFilter.Location = new System.Drawing.Point(850, 30);
            this.btnFilter.Name = "btnFilter";
            this.btnFilter.Size = new System.Drawing.Size(130, 35);
            this.btnFilter.TabIndex = 12;
            this.btnFilter.Text = "🔍 Lọc";
            this.btnFilter.UseVisualStyleBackColor = false;
            this.btnFilter.Click += new System.EventHandler(this.btnFilter_Click);
            
            // 
            // btnReset
            // 
            this.btnReset.BackColor = System.Drawing.Color.FromArgb(149, 165, 166);
            this.btnReset.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnReset.FlatAppearance.BorderSize = 0;
            this.btnReset.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReset.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnReset.ForeColor = System.Drawing.Color.White;
            this.btnReset.Location = new System.Drawing.Point(850, 70);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(130, 35);
            this.btnReset.TabIndex = 13;
            this.btnReset.Text = "🔄 Đặt lại";
            this.btnReset.UseVisualStyleBackColor = false;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            
            // 
            // grpStatistics
            // 
            this.grpStatistics.Controls.Add(this.lblAverage);
            this.grpStatistics.Controls.Add(this.lblMax);
            this.grpStatistics.Controls.Add(this.lblMin);
            this.grpStatistics.Controls.Add(this.lblExcellent);
            this.grpStatistics.Controls.Add(this.lblExcellentPercent);
            this.grpStatistics.Controls.Add(this.lblGood);
            this.grpStatistics.Controls.Add(this.lblGoodPercent);
            this.grpStatistics.Controls.Add(this.lblFair);
            this.grpStatistics.Controls.Add(this.lblFairPercent);
            this.grpStatistics.Controls.Add(this.lblAvgLevel);
            this.grpStatistics.Controls.Add(this.lblAvgPercent);
            this.grpStatistics.Controls.Add(this.lblPoor);
            this.grpStatistics.Controls.Add(this.lblPoorPercent);
            this.grpStatistics.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.grpStatistics.Location = new System.Drawing.Point(1030, 80);
            this.grpStatistics.Name = "grpStatistics";
            this.grpStatistics.Size = new System.Drawing.Size(350, 140);
            this.grpStatistics.TabIndex = 2;
            this.grpStatistics.TabStop = false;
            this.grpStatistics.Text = "📈 Thống kê";
            
            // 
            // lblAverage
            // 
            this.lblAverage.AutoSize = true;
            this.lblAverage.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold);
            this.lblAverage.ForeColor = System.Drawing.Color.FromArgb(41, 128, 185);
            this.lblAverage.Location = new System.Drawing.Point(15, 30);
            this.lblAverage.Name = "lblAverage";
            this.lblAverage.Size = new System.Drawing.Size(90, 17);
            this.lblAverage.TabIndex = 0;
            this.lblAverage.Text = "Điểm TB: --";
            
            // 
            // lblMax
            // 
            this.lblMax.AutoSize = true;
            this.lblMax.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.lblMax.ForeColor = System.Drawing.Color.FromArgb(39, 174, 96);
            this.lblMax.Location = new System.Drawing.Point(120, 30);
            this.lblMax.Name = "lblMax";
            this.lblMax.Size = new System.Drawing.Size(92, 17);
            this.lblMax.TabIndex = 1;
            this.lblMax.Text = "Cao nhất: --";
            
            // 
            // lblMin
            // 
            this.lblMin.AutoSize = true;
            this.lblMin.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.lblMin.ForeColor = System.Drawing.Color.FromArgb(231, 76, 60);
            this.lblMin.Location = new System.Drawing.Point(230, 30);
            this.lblMin.Name = "lblMin";
            this.lblMin.Size = new System.Drawing.Size(98, 17);
            this.lblMin.TabIndex = 2;
            this.lblMin.Text = "Thấp nhất: --";
            
            // 
            // lblExcellent
            // 
            this.lblExcellent.AutoSize = true;
            this.lblExcellent.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblExcellent.Location = new System.Drawing.Point(15, 60);
            this.lblExcellent.Name = "lblExcellent";
            this.lblExcellent.Size = new System.Drawing.Size(90, 15);
            this.lblExcellent.TabIndex = 3;
            this.lblExcellent.Text = "Giỏi (≥9.0): 0";
            
            // 
            // lblExcellentPercent
            // 
            this.lblExcellentPercent.AutoSize = true;
            this.lblExcellentPercent.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.lblExcellentPercent.ForeColor = System.Drawing.Color.Gray;
            this.lblExcellentPercent.Location = new System.Drawing.Point(110, 61);
            this.lblExcellentPercent.Name = "lblExcellentPercent";
            this.lblExcellentPercent.Size = new System.Drawing.Size(30, 13);
            this.lblExcellentPercent.TabIndex = 4;
            this.lblExcellentPercent.Text = "(0%)";
            
            // 
            // lblGood
            // 
            this.lblGood.AutoSize = true;
            this.lblGood.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblGood.Location = new System.Drawing.Point(180, 60);
            this.lblGood.Name = "lblGood";
            this.lblGood.Size = new System.Drawing.Size(112, 15);
            this.lblGood.TabIndex = 5;
            this.lblGood.Text = "Khá (8.0-8.9): 0";
            
            // 
            // lblGoodPercent
            // 
            this.lblGoodPercent.AutoSize = true;
            this.lblGoodPercent.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.lblGoodPercent.ForeColor = System.Drawing.Color.Gray;
            this.lblGoodPercent.Location = new System.Drawing.Point(295, 61);
            this.lblGoodPercent.Name = "lblGoodPercent";
            this.lblGoodPercent.Size = new System.Drawing.Size(30, 13);
            this.lblGoodPercent.TabIndex = 6;
            this.lblGoodPercent.Text = "(0%)";
            
            // 
            // lblFair
            // 
            this.lblFair.AutoSize = true;
            this.lblFair.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblFair.Location = new System.Drawing.Point(15, 85);
            this.lblFair.Name = "lblFair";
            this.lblFair.Size = new System.Drawing.Size(105, 15);
            this.lblFair.TabIndex = 7;
            this.lblFair.Text = "TB (6.5-7.9): 0";
            
            // 
            // lblFairPercent
            // 
            this.lblFairPercent.AutoSize = true;
            this.lblFairPercent.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.lblFairPercent.ForeColor = System.Drawing.Color.Gray;
            this.lblFairPercent.Location = new System.Drawing.Point(125, 86);
            this.lblFairPercent.Name = "lblFairPercent";
            this.lblFairPercent.Size = new System.Drawing.Size(30, 13);
            this.lblFairPercent.TabIndex = 8;
            this.lblFairPercent.Text = "(0%)";
            
            // 
            // lblAvgLevel
            // 
            this.lblAvgLevel.AutoSize = true;
            this.lblAvgLevel.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblAvgLevel.Location = new System.Drawing.Point(180, 85);
            this.lblAvgLevel.Name = "lblAvgLevel";
            this.lblAvgLevel.Size = new System.Drawing.Size(135, 15);
            this.lblAvgLevel.TabIndex = 9;
            this.lblAvgLevel.Text = "TB Yếu (5.0-6.4): 0";
            
            // 
            // lblAvgPercent
            // 
            this.lblAvgPercent.AutoSize = true;
            this.lblAvgPercent.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.lblAvgPercent.ForeColor = System.Drawing.Color.Gray;
            this.lblAvgPercent.Location = new System.Drawing.Point(15, 110);
            this.lblAvgPercent.Name = "lblAvgPercent";
            this.lblAvgPercent.Size = new System.Drawing.Size(30, 13);
            this.lblAvgPercent.TabIndex = 10;
            this.lblAvgPercent.Text = "(0%)";
            
            // 
            // lblPoor
            // 
            this.lblPoor.AutoSize = true;
            this.lblPoor.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblPoor.Location = new System.Drawing.Point(15, 110);
            this.lblPoor.Name = "lblPoor";
            this.lblPoor.Size = new System.Drawing.Size(92, 15);
            this.lblPoor.TabIndex = 11;
            this.lblPoor.Text = "Yếu (<5.0): 0";
            
            // 
            // lblPoorPercent
            // 
            this.lblPoorPercent.AutoSize = true;
            this.lblPoorPercent.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.lblPoorPercent.ForeColor = System.Drawing.Color.Gray;
            this.lblPoorPercent.Location = new System.Drawing.Point(110, 111);
            this.lblPoorPercent.Name = "lblPoorPercent";
            this.lblPoorPercent.Size = new System.Drawing.Size(30, 13);
            this.lblPoorPercent.TabIndex = 12;
            this.lblPoorPercent.Text = "(0%)";
            
            // 
            // dgvReport
            // 
            this.dgvReport.AllowUserToAddRows = false;
            this.dgvReport.AllowUserToDeleteRows = false;
            this.dgvReport.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvReport.BackgroundColor = System.Drawing.Color.White;
            this.dgvReport.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvReport.Location = new System.Drawing.Point(20, 240);
            this.dgvReport.MultiSelect = false;
            this.dgvReport.Name = "dgvReport";
            this.dgvReport.ReadOnly = true;
            this.dgvReport.RowHeadersWidth = 51;
            this.dgvReport.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvReport.Size = new System.Drawing.Size(1360, 430);
            this.dgvReport.TabIndex = 3;
            
            // 
            // pnlBottom
            // 
            this.pnlBottom.BackColor = System.Drawing.Color.FromArgb(236, 240, 241);
            this.pnlBottom.Controls.Add(this.btnExport);
            this.pnlBottom.Controls.Add(this.btnPrint);
            this.pnlBottom.Controls.Add(this.btnClose);
            this.pnlBottom.Controls.Add(this.lblTotalRecords);
            this.pnlBottom.Controls.Add(this.lblStatus);
            this.pnlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlBottom.Location = new System.Drawing.Point(0, 690);
            this.pnlBottom.Name = "pnlBottom";
            this.pnlBottom.Size = new System.Drawing.Size(1400, 60);
            this.pnlBottom.TabIndex = 4;
            
            // 
            // btnExport
            // 
            this.btnExport.BackColor = System.Drawing.Color.FromArgb(39, 174, 96);
            this.btnExport.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnExport.FlatAppearance.BorderSize = 0;
            this.btnExport.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExport.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnExport.ForeColor = System.Drawing.Color.White;
            this.btnExport.Location = new System.Drawing.Point(1090, 12);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(140, 36);
            this.btnExport.TabIndex = 0;
            this.btnExport.Text = "📊 Xuất Excel";
            this.btnExport.UseVisualStyleBackColor = false;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            
            // 
            // btnPrint
            // 
            this.btnPrint.BackColor = System.Drawing.Color.FromArgb(155, 89, 182);
            this.btnPrint.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPrint.FlatAppearance.BorderSize = 0;
            this.btnPrint.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPrint.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnPrint.ForeColor = System.Drawing.Color.White;
            this.btnPrint.Location = new System.Drawing.Point(940, 12);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(140, 36);
            this.btnPrint.TabIndex = 1;
            this.btnPrint.Text = "🖨️ In";
            this.btnPrint.UseVisualStyleBackColor = false;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.FromArgb(231, 76, 60);
            this.btnClose.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnClose.FlatAppearance.BorderSize = 0;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnClose.ForeColor = System.Drawing.Color.White;
            this.btnClose.Location = new System.Drawing.Point(1240, 12);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(140, 36);
            this.btnClose.TabIndex = 2;
            this.btnClose.Text = "❌ Đóng";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            
            // 
            // lblTotalRecords
            // 
            this.lblTotalRecords.AutoSize = true;
            this.lblTotalRecords.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold);
            this.lblTotalRecords.ForeColor = System.Drawing.Color.FromArgb(52, 73, 94);
            this.lblTotalRecords.Location = new System.Drawing.Point(20, 10);
            this.lblTotalRecords.Name = "lblTotalRecords";
            this.lblTotalRecords.Size = new System.Drawing.Size(120, 17);
            this.lblTotalRecords.TabIndex = 3;
            this.lblTotalRecords.Text = "Tổng số: 0 bản ghi";
            
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblStatus.ForeColor = System.Drawing.Color.FromArgb(127, 140, 141);
            this.lblStatus.Location = new System.Drawing.Point(20, 35);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(60, 15);
            this.lblStatus.TabIndex = 4;
            this.lblStatus.Text = "Sẵn sàng";
            
            // 
            // ReportScoreForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1400, 750);
            this.Controls.Add(this.pnlBottom);
            this.Controls.Add(this.dgvReport);
            this.Controls.Add(this.grpStatistics);
            this.Controls.Add(this.grpFilter);
            this.Controls.Add(this.pnlTop);
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "ReportScoreForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "📊 Báo cáo Điểm - StudentManagement";
            this.Load += new System.EventHandler(this.ReportScoreForm_Load);
            this.pnlTop.ResumeLayout(false);
            this.pnlTop.PerformLayout();
            this.grpFilter.ResumeLayout(false);
            this.grpFilter.PerformLayout();
            this.grpStatistics.ResumeLayout(false);
            this.grpStatistics.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvReport)).EndInit();
            this.pnlBottom.ResumeLayout(false);
            this.pnlBottom.PerformLayout();
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Panel pnlTop;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.GroupBox grpFilter;
        private System.Windows.Forms.Label lblClass;
        private System.Windows.Forms.ComboBox cmbClass;
        private System.Windows.Forms.Label lblSubject;
        private System.Windows.Forms.ComboBox cmbSubject;
        private System.Windows.Forms.Label lblSemester;
        private System.Windows.Forms.ComboBox cmbSemester;
        private System.Windows.Forms.Label lblAcademicYear;
        private System.Windows.Forms.TextBox txtAcademicYear;
        private System.Windows.Forms.Label lblScoreRange;
        private System.Windows.Forms.TextBox txtScoreFrom;
        private System.Windows.Forms.Label lblTo;
        private System.Windows.Forms.TextBox txtScoreTo;
        private System.Windows.Forms.Button btnFilter;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.GroupBox grpStatistics;
        private System.Windows.Forms.Label lblAverage;
        private System.Windows.Forms.Label lblMax;
        private System.Windows.Forms.Label lblMin;
        private System.Windows.Forms.Label lblExcellent;
        private System.Windows.Forms.Label lblExcellentPercent;
        private System.Windows.Forms.Label lblGood;
        private System.Windows.Forms.Label lblGoodPercent;
        private System.Windows.Forms.Label lblFair;
        private System.Windows.Forms.Label lblFairPercent;
        private System.Windows.Forms.Label lblAvgLevel;
        private System.Windows.Forms.Label lblAvgPercent;
        private System.Windows.Forms.Label lblPoor;
        private System.Windows.Forms.Label lblPoorPercent;
        private System.Windows.Forms.DataGridView dgvReport;
        private System.Windows.Forms.Panel pnlBottom;
        private System.Windows.Forms.Button btnExport;
        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Label lblTotalRecords;
        private System.Windows.Forms.Label lblStatus;
    }
}
