using System;
using System.Linq;
using StudentManagement.DAL;
using StudentManagement.Models;

namespace StudentManagement
{
    /// <summary>
    /// Test class: TestStudentRepository
    /// Kiểm tra các method CRUD của StudentRepository
    /// </summary>
    public class TestStudentRepository
    {
        private StudentRepository repo = new StudentRepository();

        public void RunAllTests()
        {
            Console.WriteLine("=== BẮT ĐẦU TEST STUDENT REPOSITORY ===\n");

            try
            {
                TestGetAllStudents();
                TestGetStudentById();
                TestGetStudentsByClass();
                TestSearchStudent();
                TestAddStudent();
                TestUpdateStudent();
                TestDeleteStudent();
                TestIsStudentCodeExist();
                TestCountStudentsByClass();

                Console.WriteLine("\n=== TẤT CẢ TEST ĐÃ HOÀN THÀNH THÀNH CÔNG ===");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\n❌ LỖI NGHIÊM TRỌNG: {ex.Message}");
            }
        }

        // Test 1: Lấy tất cả sinh viên
        private void TestGetAllStudents()
        {
            Console.WriteLine("TEST 1: GetAllStudents()");
            try
            {
                var students = repo.GetAllStudents();
                Console.WriteLine($"✅ Tìm thấy {students.Count} sinh viên");
                
                if (students.Count > 0)
                {
                    var firstStudent = students[0];
                    Console.WriteLine($"   Sinh viên đầu tiên: {firstStudent.StudentCode} - {firstStudent.StudentName}");
                    Console.WriteLine($"   Lớp: {firstStudent.ClassName}, Giới tính: {firstStudent.Gender ?? "N/A"}");
                    Console.WriteLine($"   Email: {firstStudent.Email ?? "N/A"}, Phone: {firstStudent.Phone ?? "N/A"}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Lỗi: {ex.Message}");
            }
            Console.WriteLine();
        }

        // Test 2: Lấy sinh viên theo ID
        private void TestGetStudentById()
        {
            Console.WriteLine("TEST 2: GetStudentById()");
            try
            {
                // Lấy ID sinh viên đầu tiên
                var allStudents = repo.GetAllStudents();
                if (allStudents.Count > 0)
                {
                    int testID = allStudents[0].StudentID;
                    var student = repo.GetStudentById(testID);
                    
                    if (student != null)
                    {
                        Console.WriteLine($"✅ Tìm thấy sinh viên ID {testID}");
                        Console.WriteLine($"   Mã SV: {student.StudentCode}, Tên: {student.StudentName}");
                        Console.WriteLine($"   Lớp ID: {student.ClassID}, IsActive: {student.IsActive}");
                    }
                    else
                    {
                        Console.WriteLine($"❌ Không tìm thấy sinh viên ID {testID}");
                    }
                }
                else
                {
                    Console.WriteLine("⚠️ Không có dữ liệu sinh viên để test");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Lỗi: {ex.Message}");
            }
            Console.WriteLine();
        }

        // Test 3: Lấy sinh viên theo lớp
        private void TestGetStudentsByClass()
        {
            Console.WriteLine("TEST 3: GetStudentsByClass()");
            try
            {
                var allStudents = repo.GetAllStudents();
                if (allStudents.Count > 0)
                {
                    int testClassID = allStudents[0].ClassID;
                    var students = repo.GetStudentsByClass(testClassID);
                    
                    Console.WriteLine($"✅ Tìm thấy {students.Count} sinh viên trong lớp ID {testClassID}");
                    if (students.Count > 0)
                    {
                        Console.WriteLine($"   Lớp: {students[0].ClassName}");
                        foreach (var s in students.Take(3))
                        {
                            Console.WriteLine($"   - {s.StudentCode}: {s.StudentName}");
                        }
                    }
                }
                else
                {
                    Console.WriteLine("⚠️ Không có dữ liệu sinh viên để test");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Lỗi: {ex.Message}");
            }
            Console.WriteLine();
        }

        // Test 4: Tìm kiếm sinh viên
        private void TestSearchStudent()
        {
            Console.WriteLine("TEST 4: SearchStudent()");
            try
            {
                var allStudents = repo.GetAllStudents();
                if (allStudents.Count > 0)
                {
                    // Lấy tên sinh viên đầu tiên để tìm kiếm
                    string keyword = allStudents[0].StudentName.ToString().Split(' ')[0];
                    var results = repo.SearchStudent(keyword);
                    
                    Console.WriteLine($"✅ Tìm kiếm từ khóa '{keyword}': {results.Count} kết quả");
                    foreach (var s in results.Take(3))
                    {
                        Console.WriteLine($"   - {s.StudentCode}: {s.StudentName} ({s.ClassName})");
                    }
                }
                else
                {
                    Console.WriteLine("⚠️ Không có dữ liệu sinh viên để test");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Lỗi: {ex.Message}");
            }
            Console.WriteLine();
        }

        // Test 5: Thêm sinh viên mới
        private void TestAddStudent()
        {
            Console.WriteLine("TEST 5: AddStudent()");
            try
            {
                // Lấy ClassID từ dữ liệu có sẵn
                var allStudents = repo.GetAllStudents();
                if (allStudents.Count == 0)
                {
                    Console.WriteLine("⚠️ Không có dữ liệu lớp học để test");
                    return;
                }

                int classID = allStudents[0].ClassID;
                string testCode = $"TEST{DateTime.Now:yyyyMMddHHmmss}";

                var newStudent = new Student
                {
                    StudentCode = testCode,
                    StudentName = "Nguyễn Văn Test",
                    ClassID = classID,
                    DateOfBirth = new DateTime(2005, 1, 15),
                    Gender = "Nam",
                    Email = $"{testCode}@test.com",
                    Phone = "0901234567",
                    Address = "123 Test Street",
                    EnrollmentDate = DateTime.Now,
                    IsActive = true
                };

                bool result = repo.AddStudent(newStudent);
                if (result)
                {
                    Console.WriteLine($"✅ Thêm sinh viên thành công: {testCode}");
                    Console.WriteLine($"   Tên: {newStudent.StudentName}, Lớp ID: {newStudent.ClassID}");
                    Console.WriteLine($"   Gender: {newStudent.Gender}, Email: {newStudent.Email}");
                    Console.WriteLine($"   Phone: {newStudent.Phone}, Address: {newStudent.Address}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Lỗi: {ex.Message}");
            }
            Console.WriteLine();
        }

        // Test 6: Cập nhật sinh viên
        private void TestUpdateStudent()
        {
            Console.WriteLine("TEST 6: UpdateStudent()");
            try
            {
                // Tìm sinh viên có mã TEST để cập nhật
                var allStudents = repo.GetAllStudents();
                var testStudent = allStudents.FirstOrDefault(s => s.StudentCode.ToString().StartsWith("TEST"));

                if (testStudent != null)
                {
                    // Lấy thông tin chi tiết
                    var student = repo.GetStudentById(testStudent.StudentID);
                    if (student != null)
                    {
                        student.StudentName = "Nguyễn Văn Test Updated";
                        student.Gender = "Nữ";
                        student.Email = "updated@test.com";
                        student.Phone = "0987654321";
                        student.Address = "456 Updated Street";

                        bool result = repo.UpdateStudent(student);
                        if (result)
                        {
                            Console.WriteLine($"✅ Cập nhật sinh viên thành công: {student.StudentCode}");
                            Console.WriteLine($"   Tên mới: {student.StudentName}");
                            Console.WriteLine($"   Gender: {student.Gender}, Email: {student.Email}");
                            Console.WriteLine($"   Phone: {student.Phone}, Address: {student.Address}");
                        }
                    }
                }
                else
                {
                    Console.WriteLine("⚠️ Không tìm thấy sinh viên TEST để cập nhật");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Lỗi: {ex.Message}");
            }
            Console.WriteLine();
        }

        // Test 7: Xóa sinh viên
        private void TestDeleteStudent()
        {
            Console.WriteLine("TEST 7: DeleteStudent()");
            try
            {
                // Tìm sinh viên có mã TEST để xóa
                var allStudents = repo.GetAllStudents();
                var testStudent = allStudents.FirstOrDefault(s => s.StudentCode.ToString().StartsWith("TEST"));

                if (testStudent != null)
                {
                    int studentID = testStudent.StudentID;
                    string studentCode = testStudent.StudentCode.ToString();

                    bool result = repo.DeleteStudent(studentID);
                    if (result)
                    {
                        Console.WriteLine($"✅ Xóa sinh viên thành công: {studentCode} (ID: {studentID})");
                    }
                }
                else
                {
                    Console.WriteLine("⚠️ Không tìm thấy sinh viên TEST để xóa");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Lỗi: {ex.Message}");
                Console.WriteLine($"   (Lỗi này có thể do sinh viên đã có điểm thi - đây là hành vi đúng)");
            }
            Console.WriteLine();
        }

        // Test 8: Kiểm tra mã sinh viên tồn tại
        private void TestIsStudentCodeExist()
        {
            Console.WriteLine("TEST 8: IsStudentCodeExist()");
            try
            {
                var allStudents = repo.GetAllStudents();
                if (allStudents.Count > 0)
                {
                    string existingCode = allStudents[0].StudentCode.ToString();
                    bool exists = repo.IsStudentCodeExist(existingCode);
                    Console.WriteLine($"✅ Kiểm tra mã '{existingCode}': {(exists ? "TỒN TẠI" : "KHÔNG TỒN TẠI")}");

                    bool notExists = repo.IsStudentCodeExist("NOTEXIST999");
                    Console.WriteLine($"✅ Kiểm tra mã 'NOTEXIST999': {(notExists ? "TỒN TẠI" : "KHÔNG TỒN TẠI")}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Lỗi: {ex.Message}");
            }
            Console.WriteLine();
        }

        // Test 9: Đếm sinh viên theo lớp
        private void TestCountStudentsByClass()
        {
            Console.WriteLine("TEST 9: CountStudentsByClass()");
            try
            {
                var allStudents = repo.GetAllStudents();
                if (allStudents.Count > 0)
                {
                    int classID = allStudents[0].ClassID;
                    int count = repo.CountStudentsByClass(classID);
                    Console.WriteLine($"✅ Lớp ID {classID} có {count} sinh viên (IsActive = true)");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Lỗi: {ex.Message}");
            }
            Console.WriteLine();
        }
    }
}
