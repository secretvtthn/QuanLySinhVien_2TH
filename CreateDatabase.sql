-- ============================================================
-- DATABASE: StudentManagement
-- DBMS: SQL Server 2022
-- PURPOSE: Quản lý Sinh viên, Lớp, Giáo viên, Điểm, Người dùng
-- ============================================================

-- 1. Tạo Database
CREATE DATABASE StudentManagement;
GO

-- 2. Sử dụng Database
USE StudentManagement;
GO

-- ============================================================
-- BẢNG 1: Users (Người dùng)
-- ============================================================
CREATE TABLE Users (
    UserID INT PRIMARY KEY IDENTITY(1,1),
    Username VARCHAR(50) NOT NULL UNIQUE,
    Password VARCHAR(100) NOT NULL,
    FullName NVARCHAR(100) NOT NULL,
    Role VARCHAR(20) NOT NULL CHECK (Role IN ('Admin', 'GiaoVien', 'SinhVien')),
    Email VARCHAR(100),
    Phone VARCHAR(20),
    CreatedDate DATETIME DEFAULT GETDATE(),
    UpdatedDate DATETIME DEFAULT GETDATE(),
    IsActive BIT DEFAULT 1
);

-- Tạo Index cho Username để tăng tốc độ tìm kiếm
CREATE INDEX IDX_Users_Username ON Users(Username);

-- ============================================================
-- BẢNG 2: Teachers (Giáo viên)
-- ============================================================
CREATE TABLE Teachers (
    TeacherID INT PRIMARY KEY IDENTITY(1,1),
    TeacherName NVARCHAR(100) NOT NULL,
    Email VARCHAR(100),
    Department NVARCHAR(50),
    Phone VARCHAR(20),
    Address NVARCHAR(200),
    HireDate DATETIME DEFAULT GETDATE(),
    IsActive BIT DEFAULT 1
);

-- ============================================================
-- BẢNG 3: Classes (Lớp học)
-- ============================================================
CREATE TABLE Classes (
    ClassID INT PRIMARY KEY IDENTITY(1,1),
    ClassName NVARCHAR(50) NOT NULL UNIQUE,
    TeacherID INT,
    Capacity INT DEFAULT 30,
    Room VARCHAR(20),
    CreatedDate DATETIME DEFAULT GETDATE(),
    UpdatedDate DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (TeacherID) REFERENCES Teachers(TeacherID) 
        ON DELETE SET NULL 
        ON UPDATE CASCADE
);

CREATE INDEX IDX_Classes_TeacherID ON Classes(TeacherID);

-- ============================================================
-- BẢNG 4: Students (Sinh viên)
-- ============================================================
CREATE TABLE Students (
    StudentID INT PRIMARY KEY IDENTITY(1,1),
    StudentCode VARCHAR(20) NOT NULL UNIQUE,
    StudentName NVARCHAR(100) NOT NULL,
    ClassID INT NOT NULL,
    DateOfBirth DATE,
    Gender NVARCHAR(10),
    Email VARCHAR(100),
    Phone VARCHAR(20),
    Address NVARCHAR(200),
    EnrollmentDate DATETIME DEFAULT GETDATE(),
    IsActive BIT DEFAULT 1,
    FOREIGN KEY (ClassID) REFERENCES Classes(ClassID) 
        ON DELETE CASCADE 
        ON UPDATE CASCADE
);

CREATE INDEX IDX_Students_ClassID ON Students(ClassID);
CREATE INDEX IDX_Students_StudentCode ON Students(StudentCode);

-- ============================================================
-- BẢNG 5: Scores (Điểm)
-- ============================================================
CREATE TABLE Scores (
    ScoreID INT PRIMARY KEY IDENTITY(1,1),
    StudentID INT NOT NULL,
    SubjectName NVARCHAR(50) NOT NULL,
    Score DECIMAL(5,2) NOT NULL,
    ScoreDate DATETIME DEFAULT GETDATE(),
    Semester INT DEFAULT 1 CHECK (Semester IN (1, 2)),
    AcademicYear VARCHAR(9) DEFAULT '2024-2025',
    Notes NVARCHAR(200),
    FOREIGN KEY (StudentID) REFERENCES Students(StudentID) 
        ON DELETE CASCADE 
        ON UPDATE CASCADE,
    CONSTRAINT CK_Score CHECK (Score >= 0 AND Score <= 10)
);

CREATE INDEX IDX_Scores_StudentID ON Scores(StudentID);
CREATE INDEX IDX_Scores_SubjectName ON Scores(SubjectName);

-- ============================================================
-- DỮLIỆU MẪU
-- ============================================================

-- INSERT dữ liệu vào bảng Users
INSERT INTO Users (Username, Password, FullName, Role, Email, Phone, IsActive) VALUES
('admin', 'admin123', N'Quản trị viên', 'Admin', 'admin@school.vn', '0123456789', 1),
('gv001', '123456', N'Nguyễn Văn A', 'GiangVien', 'nguyenvana@school.vn', '0987654321', 1),
('gv002', '234567', N'Trần Thị B', 'GiangVien', 'tranthib@school.vn', '0912345678', 1),
('sv001', 'sv012345', N'Hà Văn Hùng', 'SinhVien', 'havanhunga@school.vn', '0901234567', 1),
('sv002', 'sv123456', N'Hồ Thị Lan', 'SinhVien', 'hothi@school.vn', '0909876543', 1),
('sv003', 'sv234567', N'Trần Văn Huy', 'SinhVien', 'tranvan@school.vn', '0908765432', 1);

-- INSERT dữ liệu vào bảng Teachers
INSERT INTO Teachers (TeacherName, Email, Department, Phone, Address) VALUES
(N'Nguyễn Văn A', 'nguyenvana@school.vn', N'Toán', '0987654321', N'Hà Nội'),
(N'Trần Thị B', 'tranthib@school.vn', N'Lý', '0912345678', N'Hà Nội'),
(N'Phạm Văn C', 'phamvanc@school.vn', N'Hóa', '0923456789', N'TP. HCM'),
(N'Lê Thị D', 'lethi@school.vn', N'Tiếng Anh', '0934567890', N'Đà Nẵng'),
(N'Hoàng Văn E', 'hoangvan@school.vn', N'Sinh', '0945678901', N'Hải Phòng');

-- INSERT dữ liệu vào bảng Classes
INSERT INTO Classes (ClassName, TeacherID, Capacity, Room) VALUES
(N'Lớp CNTTA', 1, 30, N'A101'),
(N'Lớp CNTTB', 2, 32, N'B102'),
(N'Lớp CNTTC', 3, 28, N'C103'),
(N'Lớp CNTTD', 4, 35, N'D104'),
(N'Lớp SPTINA', 5, 30, N'C201');

-- INSERT dữ liệu vào bảng Students
INSERT INTO Students (StudentCode, StudentName, ClassID, DateOfBirth, Gender, Email, Phone, Address) VALUES
('SV001', N'Hà Văn Hùng', 1, '2008-01-15', N'Nam', 'havanhunga@school.vn', '0901234567', N'Hà Nội'),
('SV002', N'Hồ Thị Lan', 1, '2008-02-20', N'Nữ', 'hothi@school.vn', '0909876543', N'Hà Nội'),
('SV003', N'Trần Văn Huy', 1, '2008-03-10', N'Nam', 'tranvan@school.vn', '0908765432', N'TP. HCM'),
('SV004', N'Đỗ Thị Thu', 2, '2008-04-25', N'Nữ', 'dothu@school.vn', '0917654321', N'Hà Nội'),
('SV005', N'Vũ Văn Minh', 2, '2008-05-30', N'Nam', 'vuvan@school.vn', '0928765432', N'Đà Nẵng'),
('SV006', N'Phạm Thị Hương', 2, '2008-06-08', N'Nữ', 'phamthi@school.vn', '0939876543', N'Hải Phòng'),
('SV007', N'Cao Văn Sơn', 3, '2008-07-12', N'Nam', 'caovan@school.vn', '0941234567', N'TP. HCM'),
('SV008', N'Dương Thị Hà', 3, '2008-08-18', N'Nữ', 'duong@school.vn', '0952345678', N'Hà Nội'),
('SV009', N'Bùi Văn Long', 3, '2008-09-22', N'Nam', 'buivan@school.vn', '0963456789', N'Cần Thơ'),
('SV010', N'Lý Thị Mỹ', 4, '2008-10-05', N'Nữ', 'lyti@school.vn', '0974567890', N'Hà Nội'),
('SV011', N'Tô Văn Hải', 4, '2008-11-11', N'Nam', 'tovan@school.vn', '0985678901', N'TP. HCM'),
('SV012', N'Vương Thị Anh', 4, '2008-12-16', N'Nữ', 'vuong@school.vn', '0996789012', N'Đà Nẵng'),
('SV013', N'Ngô Văn Tú', 5, '2008-01-20', N'Nam', 'ngovan@school.vn', '0907890123', N'Hà Nội'),
('SV014', N'Trương Thị Bình', 5, '2008-02-28', N'Nữ', 'truong@school.vn', '0918901234', N'TP. HCM'),
('SV015', N'Diệp Văn Khoa', 5, '2008-03-05', N'Nam', 'diep@school.vn', '0929012345', N'Cần Thơ');

-- INSERT dữ liệu vào bảng Scores
-- Sinh viên SV001 - Hà Văn Hùng
INSERT INTO Scores (StudentID, SubjectName, Score, Semester, AcademicYear) VALUES
(1, N'Toán', 8.5, 1, '2024-2025'),
(1, N'Lý', 7.5, 1, '2024-2025'),
(1, N'Hóa', 8.0, 1, '2024-2025'),
(1, N'Tiếng Anh', 7.2, 1, '2024-2025'),
(1, N'Sinh', 8.8, 1, '2024-2025');

-- Sinh viên SV002 - Hồ Thị Lan
INSERT INTO Scores (StudentID, SubjectName, Score, Semester, AcademicYear) VALUES
(2, N'Toán', 9.0, 1, '2024-2025'),
(2, N'Lý', 8.5, 1, '2024-2025'),
(2, N'Hóa', 9.2, 1, '2024-2025'),
(2, N'Tiếng Anh', 8.8, 1, '2024-2025'),
(2, N'Sinh', 9.0, 1, '2024-2025');

-- Sinh viên SV003 - Trần Văn Huy
INSERT INTO Scores (StudentID, SubjectName, Score, Semester, AcademicYear) VALUES
(3, N'Toán', 6.5, 1, '2024-2025'),
(3, N'Lý', 6.0, 1, '2024-2025'),
(3, N'Hóa', 6.8, 1, '2024-2025'),
(3, N'Tiếng Anh', 5.5, 1, '2024-2025'),
(3, N'Sinh', 6.2, 1, '2024-2025');

-- Sinh viên SV004 - Đỗ Thị Thu
INSERT INTO Scores (StudentID, SubjectName, Score, Semester, AcademicYear) VALUES
(4, N'Toán', 7.8, 1, '2024-2025'),
(4, N'Lý', 7.5, 1, '2024-2025'),
(4, N'Hóa', 7.2, 1, '2024-2025'),
(4, N'Tiếng Anh', 7.0, 1, '2024-2025'),
(4, N'Sinh', 7.5, 1, '2024-2025');

-- Sinh viên SV005 - Vũ Văn Minh
INSERT INTO Scores (StudentID, SubjectName, Score, Semester, AcademicYear) VALUES
(5, N'Toán', 4.5, 1, '2024-2025'),
(5, N'Lý', 4.0, 1, '2024-2025'),
(5, N'Hóa', 3.8, 1, '2024-2025'),
(5, N'Tiếng Anh', 4.2, 1, '2024-2025'),
(5, N'Sinh', 4.0, 1, '2024-2025');

-- Sinh viên SV006 - Phạm Thị Hương
INSERT INTO Scores (StudentID, SubjectName, Score, Semester, AcademicYear) VALUES
(6, N'Toán', 8.2, 1, '2024-2025'),
(6, N'Lý', 8.0, 1, '2024-2025'),
(6, N'Hóa', 8.5, 1, '2024-2025'),
(6, N'Tiếng Anh', 8.3, 1, '2024-2025'),
(6, N'Sinh', 8.0, 1, '2024-2025');

-- Sinh viên SV007 - Cao Văn Sơn
INSERT INTO Scores (StudentID, SubjectName, Score, Semester, AcademicYear) VALUES
(7, N'Toán', 5.5, 1, '2024-2025'),
(7, N'Lý', 5.8, 1, '2024-2025'),
(7, N'Hóa', 6.0, 1, '2024-2025'),
(7, N'Tiếng Anh', 5.2, 1, '2024-2025'),
(7, N'Sinh', 5.5, 1, '2024-2025');

-- Sinh viên SV008 - Dương Thị Hà
INSERT INTO Scores (StudentID, SubjectName, Score, Semester, AcademicYear) VALUES
(8, N'Toán', 9.2, 1, '2024-2025'),
(8, N'Lý', 9.0, 1, '2024-2025'),
(8, N'Hóa', 9.5, 1, '2024-2025'),
(8, N'Tiếng Anh', 9.0, 1, '2024-2025'),
(8, N'Sinh', 9.3, 1, '2024-2025');

-- Sinh viên SV009 - Bùi Văn Long
INSERT INTO Scores (StudentID, SubjectName, Score, Semester, AcademicYear) VALUES
(9, N'Toán', 3.5, 1, '2024-2025'),
(9, N'Lý', 3.0, 1, '2024-2025'),
(9, N'Hóa', 3.2, 1, '2024-2025'),
(9, N'Tiếng Anh', 3.5, 1, '2024-2025'),
(9, N'Sinh', 3.0, 1, '2024-2025');

-- Sinh viên SV010 - Lý Thị Mỹ
INSERT INTO Scores (StudentID, SubjectName, Score, Semester, AcademicYear) VALUES
(10, N'Toán', 7.0, 1, '2024-2025'),
(10, N'Lý', 6.8, 1, '2024-2025'),
(10, N'Hóa', 7.2, 1, '2024-2025'),
(10, N'Tiếng Anh', 6.9, 1, '2024-2025'),
(10, N'Sinh', 7.1, 1, '2024-2025');

-- Sinh viên SV011 - Tô Văn Hải
INSERT INTO Scores (StudentID, SubjectName, Score, Semester, AcademicYear) VALUES
(11, N'Toán', 6.2, 1, '2024-2025'),
(11, N'Lý', 6.0, 1, '2024-2025'),
(11, N'Hóa', 6.5, 1, '2024-2025'),
(11, N'Tiếng Anh', 5.8, 1, '2024-2025'),
(11, N'Sinh', 6.3, 1, '2024-2025');

-- Sinh viên SV012 - Vương Thị Anh
INSERT INTO Scores (StudentID, SubjectName, Score, Semester, AcademicYear) VALUES
(12, N'Toán', 8.8, 1, '2024-2025'),
(12, N'Lý', 8.5, 1, '2024-2025'),
(12, N'Hóa', 8.9, 1, '2024-2025'),
(12, N'Tiếng Anh', 8.7, 1, '2024-2025'),
(12, N'Sinh', 8.6, 1, '2024-2025');

-- Sinh viên SV013 - Ngô Văn Tú
INSERT INTO Scores (StudentID, SubjectName, Score, Semester, AcademicYear) VALUES
(13, N'Toán', 5.0, 1, '2024-2025'),
(13, N'Lý', 5.2, 1, '2024-2025'),
(13, N'Hóa', 5.5, 1, '2024-2025'),
(13, N'Tiếng Anh', 4.8, 1, '2024-2025'),
(13, N'Sinh', 5.1, 1, '2024-2025');

-- Sinh viên SV014 - Trương Thị Bình
INSERT INTO Scores (StudentID, SubjectName, Score, Semester, AcademicYear) VALUES
(14, N'Toán', 7.5, 1, '2024-2025'),
(14, N'Lý', 7.3, 1, '2024-2025'),
(14, N'Hóa', 7.6, 1, '2024-2025'),
(14, N'Tiếng Anh', 7.4, 1, '2024-2025'),
(14, N'Sinh', 7.2, 1, '2024-2025');

-- Sinh viên SV015 - Diệp Văn Khoa
INSERT INTO Scores (StudentID, SubjectName, Score, Semester, AcademicYear) VALUES
(15, N'Toán', 9.5, 1, '2024-2025'),
(15, N'Lý', 9.2, 1, '2024-2025'),
(15, N'Hóa', 9.8, 1, '2024-2025'),
(15, N'Tiếng Anh', 9.0, 1, '2024-2025'),
(15, N'Sinh', 9.5, 1, '2024-2025');

-- ============================================================
-- KIỂM TRA DỮ LIỆU
-- ============================================================

-- Xem tất cả bảng đã tạo
PRINT '===== KIỂM TRA DỮ LIỆU =====';
PRINT '';

PRINT '--- Bảng Users ---';
SELECT * FROM Users;
PRINT '';

PRINT '--- Bảng Teachers ---';
SELECT * FROM Teachers;
PRINT '';

PRINT '--- Bảng Classes ---';
SELECT * FROM Classes;
PRINT '';

PRINT '--- Bảng Students ---';
SELECT * FROM Students;
PRINT '';

PRINT '--- Bảng Scores ---';
SELECT * FROM Scores;
PRINT '';

-- ============================================================
-- CÁC QUERY LINQ TƯƠNG ĐƯƠNG (Tham khảo)
-- ============================================================

-- Query 1: Lấy tất cả sinh viên
-- SELECT * FROM Students;

-- Query 2: Lấy sinh viên theo lớp
-- SELECT * FROM Students WHERE ClassID = 1;

-- Query 3: Lấy điểm trung bình của sinh viên
-- SELECT StudentID, AVG(Score) AS AverageScore 
-- FROM Scores GROUP BY StudentID;

-- Query 4: Lấy điểm và thông tin sinh viên
-- SELECT s.StudentID, s.StudentName, sc.SubjectName, sc.Score
-- FROM Students s 
-- INNER JOIN Scores sc ON s.StudentID = sc.StudentID;

-- Query 5: Thống kê sinh viên theo lớp
-- SELECT c.ClassName, COUNT(s.StudentID) AS StudentCount
-- FROM Classes c 
-- LEFT JOIN Students s ON c.ClassID = s.ClassID
-- GROUP BY c.ClassName;