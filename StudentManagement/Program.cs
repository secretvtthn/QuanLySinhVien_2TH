namespace StudentManagement;

static class Program
{
    /// <summary>
    ///  The main entry point for the application.
    /// </summary>
    [STAThread]
    static void Main(string[] args)
    {
        // Kiểm tra tham số command line
        if (args.Length > 0)
        {
            switch (args[0].ToLower())
            {
                case "test":
                    // Test kết nối database
                    AllocConsole();
                    SimpleTest.RunTest();
                    Console.WriteLine("\nNhấn Enter để thoát...");
                    Console.ReadLine();
                    return;
                    
                case "testlogin":
                    // Test login với LINQ
                    AllocConsole();
                    TestLogin.TestAllLoginCases();
                    Console.WriteLine("\nNhấn Enter để thoát...");
                    Console.ReadLine();
                    return;

                case "teststudentrepository":
                    // Test StudentRepository (PROMPT 6)
                    AllocConsole();
                    var studentTest = new TestStudentRepository();
                    studentTest.RunAllTests();
                    Console.WriteLine("\nNhấn Enter để thoát...");
                    Console.ReadLine();
                    return;

                case "testscorerepository":
                    // Test ScoreRepository (PROMPT 10)
                    AllocConsole();
                    var scoreTest = new TestScoreRepository();
                    scoreTest.RunAllTests();
                    Console.WriteLine("\nNhấn Enter để thoát...");
                    Console.ReadLine();
                    return;

                case "testmainformroles":
                    // Test MainForm roles
                    TestMainFormRoles.RunTests();
                    return;
                    
                case "about":
                    // Test AboutForm từ Form1
                    Application.EnableVisualStyles();
                    Application.SetCompatibleTextRenderingDefault(false);
                    TestAboutForm.TestAboutFromForm1();
                    return;
                    
                case "aboutonly":
                    // Chỉ mở AboutForm
                    Application.EnableVisualStyles();
                    Application.SetCompatibleTextRenderingDefault(false);
                    AboutForm aboutForm = new AboutForm();
                    Application.Run(aboutForm);
                    return;
                    
                case "main":
                    // Test MainForm
                    Application.EnableVisualStyles();
                    Application.SetCompatibleTextRenderingDefault(false);
                    MainForm mainForm = new MainForm();
                    Application.Run(mainForm);
                    return;
                    
                case "login":
                    // Test LoginForm
                    Application.EnableVisualStyles();
                    Application.SetCompatibleTextRenderingDefault(false);
                    LoginForm loginForm = new LoginForm();
                    Application.Run(loginForm);
                    return;

                case "studentform":
                    // Test StudentForm (PROMPT 7)
                    Application.EnableVisualStyles();
                    Application.SetCompatibleTextRenderingDefault(false);
                    Application.Run(new StudentManagement.Views.StudentForm());
                    return;

                case "classform":
                    // Test ClassForm (PROMPT 8)
                    Application.EnableVisualStyles();
                    Application.SetCompatibleTextRenderingDefault(false);
                    Application.Run(new StudentManagement.Views.ClassForm());
                    return;

                case "teacherform":
                    // Test TeacherForm (PROMPT 9)
                    Application.EnableVisualStyles();
                    Application.SetCompatibleTextRenderingDefault(false);
                    Application.Run(new StudentManagement.Views.TeacherForm());
                    return;

                case "scoreform":
                    // Test ScoreForm (PROMPT 11)
                    Application.EnableVisualStyles();
                    Application.SetCompatibleTextRenderingDefault(false);
                    Application.Run(new StudentManagement.Views.ScoreForm());
                    return;

                case "reportstudentform":
                case "testreportstudentform":
                    // Test ReportStudentForm (PROMPT 12)
                    Application.EnableVisualStyles();
                    Application.SetCompatibleTextRenderingDefault(false);
                    Application.Run(new StudentManagement.Views.ReportStudentForm());
                    return;

                case "reportscoreform":
                case "testreportscoreform":
                    // Test ReportScoreForm (PROMPT 14)
                    Application.EnableVisualStyles();
                    Application.SetCompatibleTextRenderingDefault(false);
                    Application.Run(new StudentManagement.Views.ReportScoreForm());
                    return;

                case "reportclassform":
                case "testreportclassform":
                    // Test ReportClassForm (PROMPT 15)
                    Application.EnableVisualStyles();
                    Application.SetCompatibleTextRenderingDefault(false);
                    Application.Run(new StudentManagement.Views.ReportClassForm());
                    return;

                case "chartform1":
                case "testchartform1":
                    // Test ChartForm1 (PROMPT 16)
                    Application.EnableVisualStyles();
                    Application.SetCompatibleTextRenderingDefault(false);
                    Application.Run(new StudentManagement.Views.ChartForm1());
                    return;

                case "chartform2":
                case "testchartform2":
                    // Test ChartForm2 (PROMPT 17)
                    Application.EnableVisualStyles();
                    Application.SetCompatibleTextRenderingDefault(false);
                    Application.Run(new StudentManagement.Views.ChartForm2());
                    return;

                case "helpform":
                case "testhelpform":
                    // Test HelpForm (PROMPT 20)
                    Application.EnableVisualStyles();
                    Application.SetCompatibleTextRenderingDefault(false);
                    Application.Run(new StudentManagement.Views.HelpForm());
                    return;
            }
        }

        // Chạy ứng dụng thực tế: Login -> MainForm
        ApplicationConfiguration.Initialize();
        
        LoginForm loginForm1 = new LoginForm();
        if (loginForm1.ShowDialog() == DialogResult.OK)
        {
            // Đăng nhập thành công -> Mở MainForm
            Application.Run(new MainForm());
        }
    }

    [System.Runtime.InteropServices.DllImport("kernel32.dll")]
    private static extern bool AllocConsole();
}