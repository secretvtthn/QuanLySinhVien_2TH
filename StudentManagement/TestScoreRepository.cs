using System;
using System.Linq;
using StudentManagement.DAL;
using StudentManagement.Models;

namespace StudentManagement
{
    /// <summary>
    /// Test class: TestScoreRepository
    /// ✅ PROMPT 10: Kiểm tra các method LINQ của ScoreRepository
    /// Đặc biệt test filter theo Semester/AcademicYear và validate điểm 0-10
    /// </summary>
    public class TestScoreRepository
    {
        private ScoreRepository scoreRepo = new ScoreRepository();

        public void RunAllTests()
        {
            Console.WriteLine("=== BẮT ĐẦU TEST SCORE REPOSITORY (PROMPT 10) ===\n");

            try
            {
                TestGetAllScores();
                TestGetScoresByStudent();
                TestGetScoresByStudentWithSemester();
                TestGetScoresByClass();
                TestGetScoresBySubject();
                TestGetAverageScoreByStudent();
                TestGetAverageScoreByStudentWithSemester();
                TestGetScoresInRange();
                TestAddScore();
                TestAddScoreInvalidRange(); // Test điểm > 10
                TestUpdateScore();
                TestDeleteScore();
                TestGetTopScoresBySubject();
                TestCountSubjectsByStudent();
                TestGetAllSubjects();
                TestGetAverageScoresBySemester();

                Console.WriteLine("\n=== ✅ TẤT CẢ TEST ĐÃ HOÀN THÀNH THÀNH CÔNG ===");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\n❌ LỖI NGHIÊM TRỌNG: {ex.Message}");
            }
        }

        #region Test GET Methods

        // Test 1: Lấy tất cả điểm
        private void TestGetAllScores()
        {
            Console.WriteLine("TEST 1: GetAllScores()");
            try
            {
                var scores = scoreRepo.GetAllScores();
                Console.WriteLine($"✅ Tìm thấy {scores.Count} điểm");
                
                if (scores.Count > 0)
                {
                    var firstScore = scores[0];
                    Console.WriteLine($"   Điểm đầu tiên: {firstScore.StudentName} - {firstScore.SubjectName}: {firstScore.Score}");
                    Console.WriteLine($"   Học kỳ: {firstScore.Semester}, Năm học: {firstScore.AcademicYear}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Lỗi: {ex.Message}");
            }
            Console.WriteLine();
        }

        // Test 2: Lấy điểm của 1 sinh viên (tất cả học kỳ)
        private void TestGetScoresByStudent()
        {
            Console.WriteLine("TEST 2: GetScoresByStudent(studentID)");
            try
            {
                // Lấy StudentID đầu tiên
                var allScores = scoreRepo.GetAllScores();
                if (allScores.Count > 0)
                {
                    int testStudentID = allScores[0].StudentID;
                    var scores = scoreRepo.GetScoresByStudent(testStudentID);
                    
                    Console.WriteLine($"✅ Sinh viên ID {testStudentID} có {scores.Count} điểm");
                    foreach (var s in scores.Take(5))
                    {
                        Console.WriteLine($"   {s.SubjectName}: {s.Score} (HK{s.Semester} - {s.AcademicYear})");
                    }
                }
                else
                {
                    Console.WriteLine("⚠️ Không có dữ liệu điểm để test");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Lỗi: {ex.Message}");
            }
            Console.WriteLine();
        }

        // Test 3: Lấy điểm của 1 sinh viên theo học kỳ và năm học
        private void TestGetScoresByStudentWithSemester()
        {
            Console.WriteLine("TEST 3: GetScoresByStudent(studentID, semester, academicYear) - ✅ FILTER THEO SEMESTER");
            try
            {
                var allScores = scoreRepo.GetAllScores();
                if (allScores.Count > 0)
                {
                    int testStudentID = allScores[0].StudentID;
                    int testSemester = allScores[0].Semester;
                    string testAcademicYear = allScores[0].AcademicYear;
                    
                    var scores = scoreRepo.GetScoresByStudent(testStudentID, testSemester, testAcademicYear);
                    
                    Console.WriteLine($"✅ SV ID {testStudentID} - HK{testSemester} - {testAcademicYear}: {scores.Count} điểm");
                    foreach (var s in scores.Take(5))
                    {
                        Console.WriteLine($"   {s.SubjectName}: {s.Score}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Lỗi: {ex.Message}");
            }
            Console.WriteLine();
        }

        // Test 4: Lấy điểm của cả lớp
        private void TestGetScoresByClass()
        {
            Console.WriteLine("TEST 4: GetScoresByClass(classID)");
            try
            {
                // Lấy ClassID từ sinh viên đầu tiên
                var studentRepo = new StudentRepository();
                var students = studentRepo.GetAllStudents();
                
                if (students.Count > 0)
                {
                    int testClassID = students[0].ClassID;
                    var scores = scoreRepo.GetScoresByClass(testClassID);
                    
                    Console.WriteLine($"✅ Lớp ID {testClassID} có {scores.Count} điểm");
                    foreach (var s in scores.Take(5))
                    {
                        Console.WriteLine($"   {s.StudentName} - {s.SubjectName}: {s.Score}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Lỗi: {ex.Message}");
            }
            Console.WriteLine();
        }

        // Test 5: Lọc điểm theo môn học
        private void TestGetScoresBySubject()
        {
            Console.WriteLine("TEST 5: GetScoresBySubject(subjectName) - ✅ LINQ WHERE");
            try
            {
                var allScores = scoreRepo.GetAllScores();
                if (allScores.Count > 0)
                {
                    string testSubject = allScores[0].SubjectName;
                    var scores = scoreRepo.GetScoresBySubject(testSubject);
                    
                    Console.WriteLine($"✅ Môn '{testSubject}': {scores.Count} điểm");
                    foreach (var s in scores.Take(5))
                    {
                        Console.WriteLine($"   {s.StudentName}: {s.Score}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Lỗi: {ex.Message}");
            }
            Console.WriteLine();
        }

        // Test 6: Tính điểm TB chung
        private void TestGetAverageScoreByStudent()
        {
            Console.WriteLine("TEST 6: GetAverageScoreByStudent(studentID) - ✅ LINQ AVERAGE");
            try
            {
                var allScores = scoreRepo.GetAllScores();
                if (allScores.Count > 0)
                {
                    int testStudentID = allScores[0].StudentID;
                    decimal avgScore = scoreRepo.GetAverageScoreByStudent(testStudentID);
                    
                    Console.WriteLine($"✅ Điểm TB chung của SV ID {testStudentID}: {avgScore:F2}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Lỗi: {ex.Message}");
            }
            Console.WriteLine();
        }

        // Test 7: Tính điểm TB theo học kỳ
        private void TestGetAverageScoreByStudentWithSemester()
        {
            Console.WriteLine("TEST 7: GetAverageScoreByStudent(studentID, semester, year) - ✅ FILTER + AVERAGE");
            try
            {
                var allScores = scoreRepo.GetAllScores();
                if (allScores.Count > 0)
                {
                    int testStudentID = allScores[0].StudentID;
                    int testSemester = allScores[0].Semester;
                    string testYear = allScores[0].AcademicYear;
                    
                    decimal avgScore = scoreRepo.GetAverageScoreByStudent(testStudentID, testSemester, testYear);
                    
                    Console.WriteLine($"✅ Điểm TB HK{testSemester}-{testYear} của SV ID {testStudentID}: {avgScore:F2}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Lỗi: {ex.Message}");
            }
            Console.WriteLine();
        }

        // Test 8: Lấy điểm trong khoảng
        private void TestGetScoresInRange()
        {
            Console.WriteLine("TEST 8: GetScoresInRange(min, max) - ✅ LINQ WHERE RANGE");
            try
            {
                var scores = scoreRepo.GetScoresInRange(8.0m, 10.0m);
                
                Console.WriteLine($"✅ Tìm thấy {scores.Count} điểm từ 8.0 đến 10.0");
                foreach (var s in scores.Take(5))
                {
                    Console.WriteLine($"   {s.StudentName} - {s.SubjectName}: {s.Score}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Lỗi: {ex.Message}");
            }
            Console.WriteLine();
        }

        #endregion

        #region Test CUD Methods

        // Test 9: Thêm điểm mới
        private void TestAddScore()
        {
            Console.WriteLine("TEST 9: AddScore() - ✅ VALIDATE 0-10");
            try
            {
                // Lấy StudentID từ dữ liệu có sẵn
                var studentRepo = new StudentRepository();
                var students = studentRepo.GetAllStudents();
                
                if (students.Count == 0)
                {
                    Console.WriteLine("⚠️ Không có sinh viên để test");
                    return;
                }

                int testStudentID = students[0].StudentID;

                var newScore = new Score
                {
                    StudentID = testStudentID,
                    SubjectName = "Môn Test PROMPT 10",
                    ScoreValue = 8.5m, // ✅ Điểm hợp lệ (0-10)
                    Semester = 1,
                    AcademicYear = "2024-2025",
                    ScoreDate = DateTime.Now,
                    Notes = "Test PROMPT 10"
                };

                bool result = scoreRepo.AddScore(newScore);
                if (result)
                {
                    Console.WriteLine($"✅ Thêm điểm thành công: {newScore.SubjectName} - {newScore.ScoreValue}");
                    Console.WriteLine($"   StudentID: {newScore.StudentID}, HK{newScore.Semester} - {newScore.AcademicYear}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Lỗi: {ex.Message}");
            }
            Console.WriteLine();
        }

        // Test 10: Thêm điểm KHÔNG hợp lệ (> 10)
        private void TestAddScoreInvalidRange()
        {
            Console.WriteLine("TEST 10: AddScore() với điểm > 10 - ✅ TEST VALIDATE");
            try
            {
                var studentRepo = new StudentRepository();
                var students = studentRepo.GetAllStudents();
                
                if (students.Count == 0)
                {
                    Console.WriteLine("⚠️ Không có sinh viên để test");
                    return;
                }

                int testStudentID = students[0].StudentID;

                var invalidScore = new Score
                {
                    StudentID = testStudentID,
                    SubjectName = "Test Invalid Score",
                    ScoreValue = 15.0m, // ❌ Điểm KHÔNG hợp lệ > 10
                    Semester = 1,
                    AcademicYear = "2024-2025"
                };

                bool result = scoreRepo.AddScore(invalidScore);
                Console.WriteLine($"❌ LỖI: Không nên thêm được điểm > 10!");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"✅ ĐÚNG! Đã chặn điểm không hợp lệ: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"⚠️ Lỗi khác: {ex.Message}");
            }
            Console.WriteLine();
        }

        // Test 11: Cập nhật điểm
        private void TestUpdateScore()
        {
            Console.WriteLine("TEST 11: UpdateScore()");
            try
            {
                // Tìm điểm test vừa thêm
                var allScores = scoreRepo.GetAllScores();
                var testScore = allScores.FirstOrDefault(s => s.SubjectName.Contains("Test PROMPT 10"));

                if (testScore != null)
                {
                    var score = scoreRepo.GetScoreById(testScore.ScoreID);
                    if (score != null)
                    {
                        score.ScoreValue = 9.0m;
                        score.Notes = "Updated by test";

                        bool result = scoreRepo.UpdateScore(score);
                        if (result)
                        {
                            Console.WriteLine($"✅ Cập nhật điểm thành công: ScoreID {score.ScoreID}");
                            Console.WriteLine($"   Điểm mới: {score.ScoreValue}, Notes: {score.Notes}");
                        }
                    }
                }
                else
                {
                    Console.WriteLine("⚠️ Không tìm thấy điểm test để cập nhật");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Lỗi: {ex.Message}");
            }
            Console.WriteLine();
        }

        // Test 12: Xóa điểm
        private void TestDeleteScore()
        {
            Console.WriteLine("TEST 12: DeleteScore()");
            try
            {
                // Tìm điểm test để xóa
                var allScores = scoreRepo.GetAllScores();
                var testScore = allScores.FirstOrDefault(s => s.SubjectName.Contains("Test PROMPT 10"));

                if (testScore != null)
                {
                    int scoreID = testScore.ScoreID;

                    bool result = scoreRepo.DeleteScore(scoreID);
                    if (result)
                    {
                        Console.WriteLine($"✅ Xóa điểm thành công: ScoreID {scoreID}");
                    }
                }
                else
                {
                    Console.WriteLine("⚠️ Không tìm thấy điểm test để xóa");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Lỗi: {ex.Message}");
            }
            Console.WriteLine();
        }

        #endregion

        #region Test Statistics Methods

        // Test 13: Lấy top điểm cao nhất
        private void TestGetTopScoresBySubject()
        {
            Console.WriteLine("TEST 13: GetTopScoresBySubject() - ✅ LINQ TAKE");
            try
            {
                var allScores = scoreRepo.GetAllScores();
                if (allScores.Count > 0)
                {
                    string testSubject = allScores[0].SubjectName;
                    var topScores = scoreRepo.GetTopScoresBySubject(testSubject, 5);
                    
                    Console.WriteLine($"✅ Top 5 điểm cao nhất môn '{testSubject}':");
                    foreach (var s in topScores)
                    {
                        Console.WriteLine($"   {s.StudentName}: {s.Score}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Lỗi: {ex.Message}");
            }
            Console.WriteLine();
        }

        // Test 14: Đếm số môn của sinh viên
        private void TestCountSubjectsByStudent()
        {
            Console.WriteLine("TEST 14: CountSubjectsByStudent() - ✅ LINQ DISTINCT + COUNT");
            try
            {
                var allScores = scoreRepo.GetAllScores();
                if (allScores.Count > 0)
                {
                    int testStudentID = allScores[0].StudentID;
                    int subjectCount = scoreRepo.CountSubjectsByStudent(testStudentID);
                    
                    Console.WriteLine($"✅ Sinh viên ID {testStudentID} có {subjectCount} môn học");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Lỗi: {ex.Message}");
            }
            Console.WriteLine();
        }

        // Test 15: Lấy tất cả môn học
        private void TestGetAllSubjects()
        {
            Console.WriteLine("TEST 15: GetAllSubjects() - ✅ LINQ SELECT DISTINCT");
            try
            {
                var subjects = scoreRepo.GetAllSubjects();
                
                Console.WriteLine($"✅ Tìm thấy {subjects.Count} môn học:");
                foreach (var subject in subjects.Take(10))
                {
                    Console.WriteLine($"   - {subject}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Lỗi: {ex.Message}");
            }
            Console.WriteLine();
        }

        // Test 16: Tính điểm TB theo học kỳ (GroupBy)
        private void TestGetAverageScoresBySemester()
        {
            Console.WriteLine("TEST 16: GetAverageScoresBySemester() - ✅ LINQ GROUPBY + AVERAGE");
            try
            {
                var allScores = scoreRepo.GetAllScores();
                if (allScores.Count > 0)
                {
                    int testSemester = allScores[0].Semester;
                    string testYear = allScores[0].AcademicYear;
                    
                    var avgScores = scoreRepo.GetAverageScoresBySemester(testSemester, testYear);
                    
                    Console.WriteLine($"✅ Điểm TB HK{testSemester}-{testYear} của các sinh viên:");
                    foreach (var item in avgScores.Take(5))
                    {
                        Console.WriteLine($"   SV ID {item.StudentID}: {item.AverageScore:F2} ({item.SubjectCount} môn)");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Lỗi: {ex.Message}");
            }
            Console.WriteLine();
        }

        #endregion
    }
}
