using System.Windows.Forms;
using System.Drawing;
using System.Windows.Forms.DataVisualization.Charting;

namespace StudentManagement.Views
{
    partial class ChartForm2
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            
            this.pnlTop = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.grpFilter = new System.Windows.Forms.GroupBox();
            this.cmbSemester = new System.Windows.Forms.ComboBox();
            this.lblSemester = new System.Windows.Forms.Label();
            this.txtAcademicYear = new System.Windows.Forms.TextBox();
            this.lblAcademicYear = new System.Windows.Forms.Label();
            this.grpSummary = new System.Windows.Forms.GroupBox();
            this.lblTotalStudents = new System.Windows.Forms.Label();
            this.lblExcellent = new System.Windows.Forms.Label();
            this.lblGood = new System.Windows.Forms.Label();
            this.lblAverage = new System.Windows.Forms.Label();
            this.lblWeak = new System.Windows.Forms.Label();
            this.chart2 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.pnlBottom = new System.Windows.Forms.Panel();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.lblStatus = new System.Windows.Forms.Label();
            
            this.pnlTop.SuspendLayout();
            this.grpFilter.SuspendLayout();
            this.grpSummary.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart2)).BeginInit();
            this.pnlBottom.SuspendLayout();
            this.SuspendLayout();
            
            // 
            // pnlTop
            // 
            this.pnlTop.BackColor = System.Drawing.Color.FromArgb(155, 89, 182);
            this.pnlTop.Controls.Add(this.lblTitle);
            this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTop.Location = new System.Drawing.Point(0, 0);
            this.pnlTop.Name = "pnlTop";
            this.pnlTop.Size = new System.Drawing.Size(1100, 60);
            this.pnlTop.TabIndex = 0;
            
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(20, 15);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(500, 32);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "📊 BIỂU ĐỒ PHÂN LOẠI SINH VIÊN THEO ĐIỂM";
            
            // 
            // grpFilter
            // 
            this.grpFilter.Controls.Add(this.cmbSemester);
            this.grpFilter.Controls.Add(this.lblSemester);
            this.grpFilter.Controls.Add(this.txtAcademicYear);
            this.grpFilter.Controls.Add(this.lblAcademicYear);
            this.grpFilter.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.grpFilter.Location = new System.Drawing.Point(20, 80);
            this.grpFilter.Name = "grpFilter";
            this.grpFilter.Size = new System.Drawing.Size(520, 80);
            this.grpFilter.TabIndex = 1;
            this.grpFilter.TabStop = false;
            this.grpFilter.Text = "🔍 Bộ lọc";
            
            // 
            // lblSemester
            // 
            this.lblSemester.AutoSize = true;
            this.lblSemester.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.lblSemester.Location = new System.Drawing.Point(20, 35);
            this.lblSemester.Name = "lblSemester";
            this.lblSemester.Size = new System.Drawing.Size(60, 17);
            this.lblSemester.TabIndex = 0;
            this.lblSemester.Text = "Học kỳ:";
            
            // 
            // cmbSemester
            // 
            this.cmbSemester.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSemester.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.cmbSemester.FormattingEnabled = true;
            this.cmbSemester.Location = new System.Drawing.Point(90, 32);
            this.cmbSemester.Name = "cmbSemester";
            this.cmbSemester.Size = new System.Drawing.Size(150, 25);
            this.cmbSemester.TabIndex = 1;
            
            // 
            // lblAcademicYear
            // 
            this.lblAcademicYear.AutoSize = true;
            this.lblAcademicYear.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.lblAcademicYear.Location = new System.Drawing.Point(270, 35);
            this.lblAcademicYear.Name = "lblAcademicYear";
            this.lblAcademicYear.Size = new System.Drawing.Size(65, 17);
            this.lblAcademicYear.TabIndex = 2;
            this.lblAcademicYear.Text = "Năm học:";
            
            // 
            // txtAcademicYear
            // 
            this.txtAcademicYear.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.txtAcademicYear.Location = new System.Drawing.Point(340, 32);
            this.txtAcademicYear.Name = "txtAcademicYear";
            this.txtAcademicYear.Size = new System.Drawing.Size(150, 25);
            this.txtAcademicYear.TabIndex = 3;
            this.txtAcademicYear.Text = "2024-2025";
            
            // 
            // grpSummary
            // 
            this.grpSummary.Controls.Add(this.lblTotalStudents);
            this.grpSummary.Controls.Add(this.lblExcellent);
            this.grpSummary.Controls.Add(this.lblGood);
            this.grpSummary.Controls.Add(this.lblAverage);
            this.grpSummary.Controls.Add(this.lblWeak);
            this.grpSummary.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.grpSummary.Location = new System.Drawing.Point(560, 80);
            this.grpSummary.Name = "grpSummary";
            this.grpSummary.Size = new System.Drawing.Size(520, 80);
            this.grpSummary.TabIndex = 2;
            this.grpSummary.TabStop = false;
            this.grpSummary.Text = "📊 Thống kê";
            
            // 
            // lblTotalStudents
            // 
            this.lblTotalStudents.AutoSize = true;
            this.lblTotalStudents.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold);
            this.lblTotalStudents.ForeColor = System.Drawing.Color.FromArgb(52, 73, 94);
            this.lblTotalStudents.Location = new System.Drawing.Point(20, 30);
            this.lblTotalStudents.Name = "lblTotalStudents";
            this.lblTotalStudents.Size = new System.Drawing.Size(120, 17);
            this.lblTotalStudents.TabIndex = 0;
            this.lblTotalStudents.Text = "Tổng sinh viên: --";
            
            // 
            // lblExcellent
            // 
            this.lblExcellent.AutoSize = true;
            this.lblExcellent.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblExcellent.ForeColor = System.Drawing.Color.FromArgb(46, 204, 113);
            this.lblExcellent.Location = new System.Drawing.Point(20, 52);
            this.lblExcellent.Name = "lblExcellent";
            this.lblExcellent.Size = new System.Drawing.Size(80, 15);
            this.lblExcellent.TabIndex = 1;
            this.lblExcellent.Text = "🟢 Giỏi: -- (--%)";
            
            // 
            // lblGood
            // 
            this.lblGood.AutoSize = true;
            this.lblGood.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblGood.ForeColor = System.Drawing.Color.FromArgb(52, 152, 219);
            this.lblGood.Location = new System.Drawing.Point(150, 52);
            this.lblGood.Name = "lblGood";
            this.lblGood.Size = new System.Drawing.Size(80, 15);
            this.lblGood.TabIndex = 2;
            this.lblGood.Text = "🔵 Khá: -- (--%)";
            
            // 
            // lblAverage
            // 
            this.lblAverage.AutoSize = true;
            this.lblAverage.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblAverage.ForeColor = System.Drawing.Color.FromArgb(241, 196, 15);
            this.lblAverage.Location = new System.Drawing.Point(280, 52);
            this.lblAverage.Name = "lblAverage";
            this.lblAverage.Size = new System.Drawing.Size(110, 15);
            this.lblAverage.TabIndex = 3;
            this.lblAverage.Text = "🟡 Trung bình: -- (--%)";
            
            // 
            // lblWeak
            // 
            this.lblWeak.AutoSize = true;
            this.lblWeak.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblWeak.ForeColor = System.Drawing.Color.FromArgb(231, 76, 60);
            this.lblWeak.Location = new System.Drawing.Point(410, 52);
            this.lblWeak.Name = "lblWeak";
            this.lblWeak.Size = new System.Drawing.Size(80, 15);
            this.lblWeak.TabIndex = 4;
            this.lblWeak.Text = "🔴 Yếu: -- (--%)";
            
            // 
            // chart2
            // 
            chartArea1.Name = "ChartArea1";
            this.chart2.ChartAreas.Add(chartArea1);
            
            legend1.Name = "Legend1";
            legend1.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            legend1.Docking = System.Windows.Forms.DataVisualization.Charting.Docking.Right;
            this.chart2.Legends.Add(legend1);
            
            this.chart2.Location = new System.Drawing.Point(20, 180);
            this.chart2.Name = "chart2";
            
            series1.ChartArea = "ChartArea1";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Pie;
            series1.Legend = "Legend1";
            series1.Name = "Phân loại";
            series1.IsValueShownAsLabel = true;
            series1.LabelFormat = "#.##'%'";
            series1["PieLabelStyle"] = "Outside";
            series1["PieLineColor"] = "Black";
            this.chart2.Series.Add(series1);
            
            this.chart2.Size = new System.Drawing.Size(1060, 450);
            this.chart2.TabIndex = 3;
            this.chart2.Text = "chart2";
            
            // 
            // pnlBottom
            // 
            this.pnlBottom.BackColor = System.Drawing.Color.FromArgb(236, 240, 241);
            this.pnlBottom.Controls.Add(this.btnRefresh);
            this.pnlBottom.Controls.Add(this.btnClose);
            this.pnlBottom.Controls.Add(this.lblStatus);
            this.pnlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlBottom.Location = new System.Drawing.Point(0, 650);
            this.pnlBottom.Name = "pnlBottom";
            this.pnlBottom.Size = new System.Drawing.Size(1100, 60);
            this.pnlBottom.TabIndex = 4;
            
            // 
            // btnRefresh
            // 
            this.btnRefresh.BackColor = System.Drawing.Color.FromArgb(46, 204, 113);
            this.btnRefresh.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRefresh.FlatAppearance.BorderSize = 0;
            this.btnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRefresh.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnRefresh.ForeColor = System.Drawing.Color.White;
            this.btnRefresh.Location = new System.Drawing.Point(780, 12);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(150, 36);
            this.btnRefresh.TabIndex = 0;
            this.btnRefresh.Text = "🔄 Làm mới";
            this.btnRefresh.UseVisualStyleBackColor = false;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.FromArgb(231, 76, 60);
            this.btnClose.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnClose.FlatAppearance.BorderSize = 0;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnClose.ForeColor = System.Drawing.Color.White;
            this.btnClose.Location = new System.Drawing.Point(940, 12);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(140, 36);
            this.btnClose.TabIndex = 1;
            this.btnClose.Text = "❌ Đóng";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.lblStatus.ForeColor = System.Drawing.Color.FromArgb(127, 140, 141);
            this.lblStatus.Location = new System.Drawing.Point(20, 20);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(200, 17);
            this.lblStatus.TabIndex = 2;
            this.lblStatus.Text = "✅ Nhấn 'Làm mới' để xem biểu đồ";
            
            // 
            // ChartForm2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1100, 710);
            this.Controls.Add(this.pnlBottom);
            this.Controls.Add(this.chart2);
            this.Controls.Add(this.grpSummary);
            this.Controls.Add(this.grpFilter);
            this.Controls.Add(this.pnlTop);
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "ChartForm2";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "📊 Biểu đồ Phân loại Sinh viên - StudentManagement";
            this.Load += new System.EventHandler(this.ChartForm2_Load);
            this.pnlTop.ResumeLayout(false);
            this.pnlTop.PerformLayout();
            this.grpFilter.ResumeLayout(false);
            this.grpFilter.PerformLayout();
            this.grpSummary.ResumeLayout(false);
            this.grpSummary.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart2)).EndInit();
            this.pnlBottom.ResumeLayout(false);
            this.pnlBottom.PerformLayout();
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Panel pnlTop;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.GroupBox grpFilter;
        private System.Windows.Forms.ComboBox cmbSemester;
        private System.Windows.Forms.Label lblSemester;
        private System.Windows.Forms.TextBox txtAcademicYear;
        private System.Windows.Forms.Label lblAcademicYear;
        private System.Windows.Forms.GroupBox grpSummary;
        private System.Windows.Forms.Label lblTotalStudents;
        private System.Windows.Forms.Label lblExcellent;
        private System.Windows.Forms.Label lblGood;
        private System.Windows.Forms.Label lblAverage;
        private System.Windows.Forms.Label lblWeak;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart2;
        private System.Windows.Forms.Panel pnlBottom;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Label lblStatus;
    }
}
