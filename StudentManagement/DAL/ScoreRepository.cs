using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Data.SqlClient;
using StudentManagement.Helper;
using StudentManagement.Models;

namespace StudentManagement.DAL
{
    /// <summary>
    /// Repository cho Score - Sử dụng LINQ to Objects
    /// ✅ PROMPT 10: Quản lý điểm với LINQ, filter theo Semester/AcademicYear
    /// </summary>
    public class ScoreRepository
    {
        private readonly string? connectionString;

        public ScoreRepository()
        {
            connectionString = DatabaseHelper.ConnectionString;
        }

        #region GET Methods (LINQ to Objects)

        /// <summary>
        /// ✅ LINQ: Lấy tất cả điểm (kèm thông tin sinh viên)
        /// </summary>
        public List<ScoreWithStudentInfo> GetAllScores()
        {
            try
            {
                var scores = new List<ScoreWithStudentInfo>();

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    // ✅ SỬA: Dùng "Score" thay vì "ScoreValue" (theo CreateDatabase.sql)
                    string query = @"
                        SELECT sc.ScoreID, sc.StudentID, s.StudentName, sc.SubjectName, 
                               sc.Score, sc.ScoreDate, sc.Semester, sc.AcademicYear, sc.Notes
                        FROM Scores sc
                        INNER JOIN Students s ON sc.StudentID = s.StudentID
                        ORDER BY sc.ScoreID DESC";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            scores.Add(new ScoreWithStudentInfo
                            {
                                ScoreID = reader.GetInt32(reader.GetOrdinal("ScoreID")),
                                StudentID = reader.GetInt32(reader.GetOrdinal("StudentID")),
                                StudentName = reader.GetString(reader.GetOrdinal("StudentName")),
                                SubjectName = reader.GetString(reader.GetOrdinal("SubjectName")),
                                Score = reader.GetDecimal(reader.GetOrdinal("Score")), // ✅ SỬA: "Score"
                                ScoreDate = reader.IsDBNull(reader.GetOrdinal("ScoreDate")) 
                                    ? null 
                                    : reader.GetDateTime(reader.GetOrdinal("ScoreDate")),
                                Semester = reader.GetInt32(reader.GetOrdinal("Semester")),
                                AcademicYear = reader.GetString(reader.GetOrdinal("AcademicYear")),
                                Notes = reader.IsDBNull(reader.GetOrdinal("Notes")) 
                                    ? "" 
                                    : reader.GetString(reader.GetOrdinal("Notes"))
                            });
                        }
                    }
                }

                return scores;
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi lấy danh sách điểm: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// ✅ LINQ: Lấy điểm của 1 sinh viên (tất cả học kỳ)
        /// </summary>
        public List<ScoreWithStudentInfo> GetScoresByStudent(int studentID)
        {
            try
            {
                var allScores = GetAllScores();
                
                // ✅ LINQ: Where + OrderBy
                return allScores
                    .Where(sc => sc.StudentID == studentID)
                    .OrderBy(sc => sc.AcademicYear)
                    .ThenBy(sc => sc.Semester)
                    .ThenBy(sc => sc.SubjectName)
                    .ToList();
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi lấy điểm sinh viên ID {studentID}: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// ✅ LINQ: Lấy điểm của 1 sinh viên theo học kỳ và năm học
        /// </summary>
        public List<ScoreWithStudentInfo> GetScoresByStudent(int studentID, int semester, string academicYear)
        {
            try
            {
                var allScores = GetAllScores();
                
                // ✅ LINQ: Where với nhiều điều kiện
                return allScores
                    .Where(sc => 
                        sc.StudentID == studentID && 
                        sc.Semester == semester && 
                        sc.AcademicYear == academicYear)
                    .OrderBy(sc => sc.SubjectName)
                    .ToList();
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi lấy điểm sinh viên: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// ✅ LINQ: Lấy điểm của cả lớp
        /// </summary>
        public List<ScoreWithStudentInfo> GetScoresByClass(int classID)
        {
            try
            {
                var scores = new List<ScoreWithStudentInfo>();

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    // ✅ SỬA: Dùng "Score" thay vì "ScoreValue"
                    string query = @"
                        SELECT sc.ScoreID, sc.StudentID, s.StudentName, sc.SubjectName, 
                               sc.Score, sc.ScoreDate, sc.Semester, sc.AcademicYear, sc.Notes
                        FROM Scores sc
                        INNER JOIN Students s ON sc.StudentID = s.StudentID
                        WHERE s.ClassID = @ClassID
                        ORDER BY s.StudentName, sc.SubjectName";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@ClassID", classID);
                        
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                scores.Add(MapReaderToScoreWithStudentInfo(reader));
                            }
                        }
                    }
                }

                return scores;
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi lấy điểm lớp {classID}: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// ✅ LINQ: Lọc điểm theo môn học
        /// </summary>
        public List<ScoreWithStudentInfo> GetScoresBySubject(string subjectName)
        {
            try
            {
                var allScores = GetAllScores();
                
                // ✅ LINQ: Where với Contains (không phân biệt hoa thường)
                return allScores
                    .Where(sc => sc.SubjectName.Contains(subjectName, StringComparison.OrdinalIgnoreCase))
                    .OrderBy(sc => sc.StudentName)
                    .ThenByDescending(sc => sc.Score)
                    .ToList();
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi lấy điểm môn {subjectName}: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// ✅ LINQ: Tính điểm trung bình chung của sinh viên
        /// </summary>
        public decimal GetAverageScoreByStudent(int studentID)
        {
            try
            {
                var scores = LoadScoresFromDatabase()
                    .Where(sc => sc.StudentID == studentID)
                    .ToList();

                if (scores.Count == 0)
                    return 0;

                // ✅ LINQ: Average
                return scores.Average(sc => sc.ScoreValue);
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi tính điểm TB: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// ✅ LINQ: Tính điểm TB theo học kỳ và năm học
        /// </summary>
        public decimal GetAverageScoreByStudent(int studentID, int semester, string academicYear)
        {
            try
            {
                var scores = LoadScoresFromDatabase()
                    .Where(sc => 
                        sc.StudentID == studentID && 
                        sc.Semester == semester && 
                        sc.AcademicYear == academicYear)
                    .ToList();

                if (scores.Count == 0)
                    return 0;

                // ✅ LINQ: Average với điều kiện
                return scores.Average(sc => sc.ScoreValue);
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi tính điểm TB: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// ✅ LINQ: Lấy điểm trong khoảng min-max
        /// </summary>
        public List<ScoreWithStudentInfo> GetScoresInRange(decimal minScore, decimal maxScore)
        {
            try
            {
                var allScores = GetAllScores();
                
                // ✅ LINQ: Where với điều kiện khoảng
                return allScores
                    .Where(sc => sc.Score >= minScore && sc.Score <= maxScore)
                    .OrderByDescending(sc => sc.Score)
                    .ThenBy(sc => sc.StudentName)
                    .ToList();
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi lọc điểm: {ex.Message}", ex);
            }
        }

        #endregion

        #region CUD Methods (Create, Update, Delete)

        /// <summary>
        /// Thêm điểm mới (có kiểm tra ràng buộc)
        /// </summary>
        public bool AddScore(Score score)
        {
            try
            {
                // Validate dữ liệu
                ValidateScore(score);

                // Kiểm tra StudentID có tồn tại không
                if (!IsStudentExists(score.StudentID))
                {
                    throw new InvalidOperationException($"Sinh viên ID {score.StudentID} không tồn tại!");
                }

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    // ✅ SỬA: Dùng "Score" thay vì "ScoreValue" trong INSERT
                    string query = @"
                        INSERT INTO Scores (StudentID, SubjectName, Score, ScoreDate, Semester, AcademicYear, Notes)
                        VALUES (@StudentID, @SubjectName, @Score, @ScoreDate, @Semester, @AcademicYear, @Notes)";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@StudentID", score.StudentID);
                        cmd.Parameters.AddWithValue("@SubjectName", score.SubjectName);
                        cmd.Parameters.AddWithValue("@Score", score.ScoreValue); // ✅ Tên parameter: @Score
                        cmd.Parameters.AddWithValue("@ScoreDate", 
                            score.ScoreDate.HasValue ? (object)score.ScoreDate.Value : DBNull.Value);
                        cmd.Parameters.AddWithValue("@Semester", score.Semester);
                        cmd.Parameters.AddWithValue("@AcademicYear", score.AcademicYear);
                        cmd.Parameters.AddWithValue("@Notes", 
                            string.IsNullOrWhiteSpace(score.Notes) ? DBNull.Value : score.Notes);

                        int rowsAffected = cmd.ExecuteNonQuery();
                        return rowsAffected > 0;
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                throw new Exception($"Lỗi SQL khi thêm điểm: {sqlEx.Message}", sqlEx);
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi thêm điểm: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Cập nhật điểm
        /// </summary>
        public bool UpdateScore(Score score)
        {
            try
            {
                // Validate dữ liệu
                ValidateScore(score);

                // Kiểm tra điểm có tồn tại không
                var existingScore = GetScoreById(score.ScoreID);
                if (existingScore == null)
                {
                    throw new InvalidOperationException($"Không tìm thấy điểm ID {score.ScoreID}!");
                }

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    // ✅ SỬA: Dùng "Score" thay vì "ScoreValue" trong UPDATE
                    string query = @"
                        UPDATE Scores
                        SET StudentID = @StudentID,
                            SubjectName = @SubjectName,
                            Score = @Score,
                            ScoreDate = @ScoreDate,
                            Semester = @Semester,
                            AcademicYear = @AcademicYear,
                            Notes = @Notes
                        WHERE ScoreID = @ScoreID";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@ScoreID", score.ScoreID);
                        cmd.Parameters.AddWithValue("@StudentID", score.StudentID);
                        cmd.Parameters.AddWithValue("@SubjectName", score.SubjectName);
                        cmd.Parameters.AddWithValue("@Score", score.ScoreValue); // ✅ Tên parameter: @Score
                        cmd.Parameters.AddWithValue("@ScoreDate",
                            score.ScoreDate.HasValue ? (object)score.ScoreDate.Value : DBNull.Value);
                        cmd.Parameters.AddWithValue("@Semester", score.Semester);
                        cmd.Parameters.AddWithValue("@AcademicYear", score.AcademicYear);
                        cmd.Parameters.AddWithValue("@Notes",
                            string.IsNullOrWhiteSpace(score.Notes) ? DBNull.Value : score.Notes);

                        int rowsAffected = cmd.ExecuteNonQuery();
                        return rowsAffected > 0;
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                throw new Exception($"Lỗi SQL khi cập nhật điểm: {sqlEx.Message}", sqlEx);
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi cập nhật điểm: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Xóa điểm
        /// </summary>
        public bool DeleteScore(int scoreID)
        {
            try
            {
                if (scoreID <= 0)
                {
                    throw new ArgumentException("ScoreID không hợp lệ!");
                }

                // Kiểm tra điểm có tồn tại không
                var existingScore = GetScoreById(scoreID);
                if (existingScore == null)
                {
                    throw new InvalidOperationException($"Không tìm thấy điểm ID {scoreID}!");
                }

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "DELETE FROM Scores WHERE ScoreID = @ScoreID";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@ScoreID", scoreID);
                        int rowsAffected = cmd.ExecuteNonQuery();
                        return rowsAffected > 0;
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                throw new Exception($"Lỗi SQL khi xóa điểm: {sqlEx.Message}", sqlEx);
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi xóa điểm: {ex.Message}", ex);
            }
        }

        #endregion

        #region Statistics Methods (LINQ)

        /// <summary>
        /// ✅ LINQ: Lấy top N điểm cao nhất theo môn
        /// </summary>
        public List<ScoreWithStudentInfo> GetTopScoresBySubject(string subjectName, int topN = 10)
        {
            try
            {
                var allScores = GetAllScores();
                
                // ✅ LINQ: Where + OrderByDescending + Take
                return allScores
                    .Where(sc => sc.SubjectName.Equals(subjectName, StringComparison.OrdinalIgnoreCase))
                    .OrderByDescending(sc => sc.Score)
                    .Take(topN)
                    .ToList();
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi lấy top điểm: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// ✅ LINQ: Đếm số môn của sinh viên
        /// </summary>
        public int CountSubjectsByStudent(int studentID)
        {
            try
            {
                var scores = LoadScoresFromDatabase()
                    .Where(sc => sc.StudentID == studentID)
                    .ToList();

                // ✅ LINQ: Select + Distinct + Count
                return scores
                    .Select(sc => sc.SubjectName)
                    .Distinct()
                    .Count();
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi đếm môn học: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// ✅ LINQ: Lấy danh sách tất cả môn học (Distinct)
        /// </summary>
        public List<string> GetAllSubjects()
        {
            try
            {
                var scores = LoadScoresFromDatabase();
                
                // ✅ LINQ: Select + Distinct + OrderBy
                return scores
                    .Select(sc => sc.SubjectName)
                    .Distinct()
                    .OrderBy(name => name)
                    .ToList();
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi lấy danh sách môn: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// ✅ LINQ: Tính điểm TB của tất cả sinh viên theo học kỳ
        /// </summary>
        public List<dynamic> GetAverageScoresBySemester(int semester, string academicYear)
        {
            try
            {
                var scores = LoadScoresFromDatabase()
                    .Where(sc => sc.Semester == semester && sc.AcademicYear == academicYear)
                    .ToList();

                // ✅ LINQ: GroupBy + Average
                var result = scores
                    .GroupBy(sc => sc.StudentID)
                    .Select(g => new
                    {
                        StudentID = g.Key,
                        AverageScore = g.Average(sc => sc.ScoreValue),
                        SubjectCount = g.Count()
                    })
                    .OrderByDescending(x => x.AverageScore)
                    .ToList<dynamic>();

                return result;
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi tính điểm TB theo học kỳ: {ex.Message}", ex);
            }
        }

        #endregion

        #region Helper Methods

        /// <summary>
        /// Load dữ liệu điểm từ database (không JOIN)
        /// </summary>
        private List<Score> LoadScoresFromDatabase()
        {
            List<Score> scores = new List<Score>();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    // ✅ SỬA: Dùng "Score" thay vì "ScoreValue"
                    string query = @"
                        SELECT ScoreID, StudentID, SubjectName, Score, ScoreDate, 
                               Semester, AcademicYear, Notes
                        FROM Scores";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            scores.Add(MapReaderToScore(reader));
                        }
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                throw new Exception($"Lỗi SQL: {sqlEx.Message}", sqlEx);
            }

            return scores;
        }

        /// <summary>
        /// Lấy điểm theo ID
        /// </summary>
        public Score? GetScoreById(int scoreID)
        {
            try
            {
                var scores = LoadScoresFromDatabase();
                
                // ✅ LINQ: FirstOrDefault
                return scores.FirstOrDefault(sc => sc.ScoreID == scoreID);
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi lấy điểm ID {scoreID}: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Map SqlDataReader sang Score object
        /// </summary>
        private Score MapReaderToScore(SqlDataReader reader)
        {
            return new Score
            {
                ScoreID = reader.GetInt32(reader.GetOrdinal("ScoreID")),
                StudentID = reader.GetInt32(reader.GetOrdinal("StudentID")),
                SubjectName = reader.GetString(reader.GetOrdinal("SubjectName")),
                ScoreValue = reader.GetDecimal(reader.GetOrdinal("Score")), // ✅ SỬA: đọc từ cột "Score"
                ScoreDate = reader.IsDBNull(reader.GetOrdinal("ScoreDate"))
                    ? null
                    : reader.GetDateTime(reader.GetOrdinal("ScoreDate")),
                Semester = reader.GetInt32(reader.GetOrdinal("Semester")),
                AcademicYear = reader.GetString(reader.GetOrdinal("AcademicYear")),
                Notes = reader.IsDBNull(reader.GetOrdinal("Notes"))
                    ? null
                    : reader.GetString(reader.GetOrdinal("Notes"))
            };
        }

        /// <summary>
        /// Map SqlDataReader sang ScoreWithStudentInfo object
        /// </summary>
        private ScoreWithStudentInfo MapReaderToScoreWithStudentInfo(SqlDataReader reader)
        {
            return new ScoreWithStudentInfo
            {
                ScoreID = reader.GetInt32(reader.GetOrdinal("ScoreID")),
                StudentID = reader.GetInt32(reader.GetOrdinal("StudentID")),
                StudentName = reader.GetString(reader.GetOrdinal("StudentName")),
                SubjectName = reader.GetString(reader.GetOrdinal("SubjectName")),
                Score = reader.GetDecimal(reader.GetOrdinal("Score")), // ✅ SỬA: đọc từ cột "Score"
                ScoreDate = reader.IsDBNull(reader.GetOrdinal("ScoreDate"))
                    ? null
                    : reader.GetDateTime(reader.GetOrdinal("ScoreDate")),
                Semester = reader.GetInt32(reader.GetOrdinal("Semester")),
                AcademicYear = reader.GetString(reader.GetOrdinal("AcademicYear")),
                Notes = reader.IsDBNull(reader.GetOrdinal("Notes"))
                    ? ""
                    : reader.GetString(reader.GetOrdinal("Notes"))
            };
        }

        /// <summary>
        /// Validate Score object
        /// </summary>
        private void ValidateScore(Score score)
        {
            if (score == null)
                throw new ArgumentNullException(nameof(score), "Score object không được null!");

            if (score.StudentID <= 0)
                throw new ArgumentException("StudentID không hợp lệ!");

            if (string.IsNullOrWhiteSpace(score.SubjectName))
                throw new ArgumentException("Tên môn học không được để trống!");

            // ✅ Kiểm tra ràng buộc: Điểm phải từ 0-10
            if (score.ScoreValue < 0 || score.ScoreValue > 10)
                throw new ArgumentException("Điểm phải trong khoảng 0-10!");

            if (score.Semester < 1 || score.Semester > 3)
                throw new ArgumentException("Học kỳ phải từ 1-3!");

            if (string.IsNullOrWhiteSpace(score.AcademicYear))
                throw new ArgumentException("Năm học không được để trống!");
        }

        /// <summary>
        /// Kiểm tra sinh viên có tồn tại không
        /// </summary>
        private bool IsStudentExists(int studentID)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "SELECT COUNT(*) FROM Students WHERE StudentID = @StudentID";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@StudentID", studentID);
                        int count = (int)cmd.ExecuteScalar();
                        return count > 0;
                    }
                }
            }
            catch
            {
                return false;
            }
        }

        #endregion
    }
}
