using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using StudentManagement.DAL;
using StudentManagement.Models;
using ClosedXML.Excel;
using System.IO;
using System.Drawing.Printing;

namespace StudentManagement.Views
{
    public partial class ReportStudentForm : Form
    {
        private StudentRepository _studentRepo;
        private ClassRepository _classRepo;
        private List<dynamic> _reportData;

        public ReportStudentForm()
        {
            InitializeComponent();
            _studentRepo = new StudentRepository();
            _classRepo = new ClassRepository();
            _reportData = new List<dynamic>();
        }

        private void ReportStudentForm_Load(object sender, EventArgs e)
        {
            try
            {
                LoadClassComboBox();
                ConfigureDataGridView();
                LoadAllStudents();
                UpdateStatus("Đã tải danh sách sinh viên thành công", false);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải dữ liệu: {ex.Message}", "Lỗi", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ============================================================
        // CẤU HÌNH DATAGRIDVIEW
        // ============================================================
        private void ConfigureDataGridView()
        {
            dgvReport.AutoGenerateColumns = false;
            dgvReport.Columns.Clear();

            dgvReport.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "StudentCode",
                HeaderText = "Mã SV",
                DataPropertyName = "StudentCode",
                Width = 100
            });

            dgvReport.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "StudentName",
                HeaderText = "Họ tên",
                DataPropertyName = "StudentName",
                Width = 180
            });

            dgvReport.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "ClassName",
                HeaderText = "Lớp",
                DataPropertyName = "ClassName",
                Width = 120
            });

            dgvReport.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "DateOfBirth",
                HeaderText = "Ngày sinh",
                DataPropertyName = "DateOfBirth",
                Width = 110,
                DefaultCellStyle = new DataGridViewCellStyle 
                { 
                    Format = "dd/MM/yyyy" 
                }
            });

            dgvReport.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Gender",
                HeaderText = "Giới tính",
                DataPropertyName = "Gender",
                Width = 80
            });

            dgvReport.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Email",
                HeaderText = "Email",
                DataPropertyName = "Email",
                Width = 200
            });

            dgvReport.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Phone",
                HeaderText = "Số điện thoại",
                DataPropertyName = "Phone",
                Width = 120
            });

            dgvReport.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Address",
                HeaderText = "Địa chỉ",
                DataPropertyName = "Address",
                Width = 250
            });

            // Style header
            dgvReport.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(41, 128, 185);
            dgvReport.ColumnHeadersDefaultCellStyle.ForeColor = System.Drawing.Color.White;
            dgvReport.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            dgvReport.ColumnHeadersHeight = 35;
            dgvReport.EnableHeadersVisualStyles = false;

            // Alternating row colors
            dgvReport.AlternatingRowsDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(236, 240, 241);
            dgvReport.RowsDefaultCellStyle.SelectionBackColor = System.Drawing.Color.FromArgb(52, 152, 219);
        }

        // ============================================================
        // LOAD DỮ LIỆU
        // ============================================================
        private void LoadClassComboBox()
        {
            var classes = _classRepo.GetAllClasses();
            cmbClass.DataSource = classes;
            cmbClass.DisplayMember = "ClassName";
            cmbClass.ValueMember = "ClassID";
        }

        private void LoadAllStudents()
        {
            var students = _studentRepo.GetAllStudents();
            var classes = _classRepo.GetAllClasses();

            // ✅ LINQ JOIN để lấy ClassName
            _reportData = (from s in students
                          join c in classes on s.ClassID equals c.ClassID
                          select new
                          {
                              s.StudentCode,
                              s.StudentName,
                              c.ClassName,
                              s.DateOfBirth,
                              s.Gender,
                              s.Email,
                              s.Phone,
                              s.Address
                          }).ToList<dynamic>();

            dgvReport.DataSource = _reportData;
            UpdateRecordCount(_reportData.Count);
        }

        // ============================================================
        // LỌC DỮ LIỆU - LINQ QUERY
        // ============================================================
        private void btnFilter_Click(object sender, EventArgs e)
        {
            try
            {
                var students = _studentRepo.GetAllStudents();
                var classes = _classRepo.GetAllClasses();

                // Base query với JOIN
                var query = from s in students
                           join c in classes on s.ClassID equals c.ClassID
                           select new
                           {
                               s.StudentCode,
                               s.StudentName,
                               s.ClassID,
                               c.ClassName,
                               s.DateOfBirth,
                               s.Gender,
                               s.Email,
                               s.Phone,
                               s.Address
                           };

                // ✅ LỌC THEO LỚP (LINQ WHERE)
                if (radByClass.Checked && cmbClass.SelectedValue != null)
                {
                    int classID = (int)cmbClass.SelectedValue;
                    query = query.Where(s => s.ClassID == classID);
                }

                // ✅ LỌC THEO TÊN (LINQ WHERE + Contains)
                if (radSearch.Checked && !string.IsNullOrWhiteSpace(txtSearchName.Text))
                {
                    string searchText = txtSearchName.Text.Trim().ToLower();
                    query = query.Where(s => s.StudentName.ToLower().Contains(searchText) ||
                                            s.StudentCode.ToLower().Contains(searchText));
                }

                _reportData = query.ToList<dynamic>();
                dgvReport.DataSource = _reportData;
                UpdateRecordCount(_reportData.Count);
                UpdateStatus($"Đã lọc xong - Tìm thấy {_reportData.Count} sinh viên", false);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi lọc dữ liệu: {ex.Message}", "Lỗi", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            radAll.Checked = true;
            txtSearchName.Clear();
            LoadAllStudents();
            UpdateStatus("Đã đặt lại bộ lọc", false);
        }

        // ============================================================
        // XUẤT EXCEL - CLOSEDXML
        // ============================================================
        private void btnExportExcel_Click(object sender, EventArgs e)
        {
            if (_reportData.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu để xuất!", "Thông báo", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                SaveFileDialog saveDialog = new SaveFileDialog
                {
                    Filter = "Excel Files|*.xlsx",
                    Title = "Lưu báo cáo sinh viên",
                    FileName = $"BaoCaoSinhVien_{DateTime.Now:yyyyMMdd_HHmmss}.xlsx"
                };

                if (saveDialog.ShowDialog() == DialogResult.OK)
                {
                    UpdateStatus("Đang xuất Excel...", false);
                    ExportToExcel(saveDialog.FileName);
                    UpdateStatus("Xuất Excel thành công!", false);

                    var result = MessageBox.Show("Xuất Excel thành công!\nBạn có muốn mở file?", 
                        "Thành công", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

                    if (result == DialogResult.Yes)
                    {
                        System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
                        {
                            FileName = saveDialog.FileName,
                            UseShellExecute = true
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi xuất Excel: {ex.Message}", "Lỗi", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ExportToExcel(string filePath)
        {
            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Danh sách sinh viên");

                // ✅ TIÊU ĐỀ
                worksheet.Cell(1, 1).Value = "BÁO CÁO DANH SÁCH SINH VIÊN";
                worksheet.Range(1, 1, 1, 8).Merge();
                worksheet.Cell(1, 1).Style.Font.FontSize = 16;
                worksheet.Cell(1, 1).Style.Font.Bold = true;
                worksheet.Cell(1, 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                worksheet.Cell(1, 1).Style.Fill.BackgroundColor = XLColor.FromArgb(52, 152, 219);
                worksheet.Cell(1, 1).Style.Font.FontColor = XLColor.White;

                // ✅ THÔNG TIN
                worksheet.Cell(2, 1).Value = $"Ngày xuất: {DateTime.Now:dd/MM/yyyy HH:mm:ss}";
                worksheet.Cell(3, 1).Value = $"Tổng số sinh viên: {_reportData.Count}";

                // ✅ HEADER
                int headerRow = 5;
                string[] headers = { "Mã SV", "Họ tên", "Lớp", "Ngày sinh", "Giới tính", "Email", "Số điện thoại", "Địa chỉ" };
                
                for (int i = 0; i < headers.Length; i++)
                {
                    var cell = worksheet.Cell(headerRow, i + 1);
                    cell.Value = headers[i];
                    cell.Style.Font.Bold = true;
                    cell.Style.Fill.BackgroundColor = XLColor.FromArgb(41, 128, 185);
                    cell.Style.Font.FontColor = XLColor.White;
                    cell.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                    cell.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                }

                // ✅ DỮ LIỆU
                int currentRow = headerRow + 1;
                foreach (var item in _reportData)
                {
                    worksheet.Cell(currentRow, 1).Value = item.StudentCode;
                    worksheet.Cell(currentRow, 2).Value = item.StudentName;
                    worksheet.Cell(currentRow, 3).Value = item.ClassName;
                    
                    if (item.DateOfBirth != null)
                    {
                        worksheet.Cell(currentRow, 4).Value = ((DateTime)item.DateOfBirth).ToString("dd/MM/yyyy");
                    }
                    
                    worksheet.Cell(currentRow, 5).Value = item.Gender ?? "";
                    worksheet.Cell(currentRow, 6).Value = item.Email ?? "";
                    worksheet.Cell(currentRow, 7).Value = item.Phone ?? "";
                    worksheet.Cell(currentRow, 8).Value = item.Address ?? "";

                    // Border cho mỗi cell
                    for (int col = 1; col <= 8; col++)
                    {
                        worksheet.Cell(currentRow, col).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    }

                    // Alternating row color
                    if (currentRow % 2 == 0)
                    {
                        worksheet.Range(currentRow, 1, currentRow, 8).Style.Fill.BackgroundColor = XLColor.FromArgb(236, 240, 241);
                    }

                    currentRow++;
                }

                // ✅ AUTO-FIT COLUMNS
                worksheet.Columns().AdjustToContents();

                // Lưu file
                workbook.SaveAs(filePath);
            }
        }

        // ============================================================
        // IN BÁO CÁO
        // ============================================================
        private void btnPrint_Click(object sender, EventArgs e)
        {
            if (_reportData.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu để in!", "Thông báo", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                PrintDocument printDoc = new PrintDocument();
                printDoc.PrintPage += PrintDoc_PrintPage;

                PrintPreviewDialog previewDialog = new PrintPreviewDialog
                {
                    Document = printDoc,
                    Width = 800,
                    Height = 600,
                    Text = "Xem trước bản in"
                };

                previewDialog.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi in: {ex.Message}", "Lỗi", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void PrintDoc_PrintPage(object sender, PrintPageEventArgs e)
        {
            if (e.Graphics == null) return;
            
            // Simple print implementation
            var font = new System.Drawing.Font("Segoe UI", 10);
            var titleFont = new System.Drawing.Font("Segoe UI", 14, System.Drawing.FontStyle.Bold);
            var brush = System.Drawing.Brushes.Black;
            
            int y = 50;
            
            // Title
            e.Graphics.DrawString("BÁO CÁO DANH SÁCH SINH VIÊN", titleFont, brush, 250, y);
            y += 40;
            
            e.Graphics.DrawString($"Ngày: {DateTime.Now:dd/MM/yyyy}", font, brush, 50, y);
            y += 30;
            
            e.Graphics.DrawString($"Tổng: {_reportData.Count} sinh viên", font, brush, 50, y);
            y += 40;

            // Header
            e.Graphics.DrawString("Mã SV", font, brush, 50, y);
            e.Graphics.DrawString("Họ tên", font, brush, 150, y);
            e.Graphics.DrawString("Lớp", font, brush, 350, y);
            e.Graphics.DrawString("Email", font, brush, 450, y);
            y += 30;

            // Data (first 20 records for simplicity)
            int count = 0;
            foreach (var item in _reportData.Take(20))
            {
                e.Graphics.DrawString(item.StudentCode, font, brush, 50, y);
                e.Graphics.DrawString(item.StudentName, font, brush, 150, y);
                e.Graphics.DrawString(item.ClassName, font, brush, 350, y);
                e.Graphics.DrawString(item.Email ?? "", font, brush, 450, y);
                y += 25;
                count++;

                if (y > 1000) break; // Prevent overflow
            }
        }

        // ============================================================
        // RADIO BUTTON EVENTS
        // ============================================================
        private void RadioButton_CheckedChanged(object sender, EventArgs e)
        {
            cmbClass.Enabled = radByClass.Checked;
            txtSearchName.Enabled = radSearch.Checked;
        }

        private void txtSearchName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnFilter_Click(sender, e);
            }
        }

        // ============================================================
        // HELPER METHODS
        // ============================================================
        private void UpdateRecordCount(int count)
        {
            lblRecordCount.Text = $"Tổng: {count} sinh viên";
        }

        private void UpdateStatus(string message, bool isError)
        {
            lblStatus.Text = message;
            lblStatus.ForeColor = isError 
                ? System.Drawing.Color.Red 
                : System.Drawing.Color.FromArgb(39, 174, 96);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
