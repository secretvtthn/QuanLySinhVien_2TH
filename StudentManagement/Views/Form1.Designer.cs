namespace StudentManagement;

partial class Form1
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
        this.SuspendLayout();

        // Form properties
        this.Text = "Student Management System - Main Form";
        this.Size = new System.Drawing.Size(600, 400);
        this.MinimumSize = new System.Drawing.Size(500, 300);
        this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Sizable;
        this.MaximizeBox = true;
        this.MinimizeBox = true;
        this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;

        // Button test kết nối
        System.Windows.Forms.Button btnTest = new System.Windows.Forms.Button();
        btnTest.Text = "Test Kết Nối Database";
        btnTest.Size = new System.Drawing.Size(200, 40);
        btnTest.Location = new System.Drawing.Point(50, 50);
        btnTest.Anchor = System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Top;
        btnTest.Click += BtnTest_Click;
        this.Controls.Add(btnTest);

        // Label kết quả
        System.Windows.Forms.Label lblResult = new System.Windows.Forms.Label();
        lblResult.Name = "lblResult";
        lblResult.Text = "Nhấn button để test kết nối...";
        lblResult.Size = new System.Drawing.Size(500, 150);
        lblResult.Location = new System.Drawing.Point(50, 100);
        lblResult.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
        lblResult.Anchor = System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right | System.Windows.Forms.AnchorStyles.Bottom;
        this.Controls.Add(lblResult);

        // Button About
        System.Windows.Forms.Button btnAbout = new System.Windows.Forms.Button();
        btnAbout.Text = "Thông tin nhóm";
        btnAbout.Size = new System.Drawing.Size(150, 40);
        btnAbout.Location = new System.Drawing.Point(400, 50);
        btnAbout.BackColor = System.Drawing.Color.LightGreen;
        btnAbout.Font = new System.Drawing.Font("Arial", 10, System.Drawing.FontStyle.Bold);
        btnAbout.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
        btnAbout.Click += BtnAbout_Click;
        this.Controls.Add(btnAbout);

        this.ResumeLayout(false);
    }

    #endregion
}
