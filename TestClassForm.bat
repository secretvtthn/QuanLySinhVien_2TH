@echo off
chcp 65001 > nul
title TEST CLASS FORM - PROMPT 8

echo.
echo ╔══════════════════════════════════════════════════════╗
echo ║         CHẠY TEST CLASS FORM (PROMPT 8)              ║
echo ║   Form quản lý lớp học với kiểm tra sinh viên       ║
echo ╚══════════════════════════════════════════════════════╝
echo.

cd /d "%~dp0StudentManagement"

echo [INFO] Build project...
dotnet build StudentManagement.csproj -c Debug > nul 2>&1

if %ERRORLEVEL% NEQ 0 (
    echo [ERROR] Build failed! Đang compile lại...
    dotnet build StudentManagement.csproj -c Debug
    if %ERRORLEVEL% NEQ 0 (
        echo [ERROR] Build thất bại! Kiểm tra lỗi compiler.
        pause
        exit /b 1
    )
)

echo [OK] Build thành công!
echo.
echo ─────────────────────────────────────────────────────
echo ĐANG MỞ CLASS FORM...
echo ─────────────────────────────────────────────────────
echo.
echo HƯỚNG DẪN TEST:
echo.
echo 1. THÊM LỚP MỚI
echo    - Nhập Tên lớp: "Lớp 10A3"
echo    - Chọn Giáo viên từ ComboBox (hoặc "Chưa có GVCN")
echo    - Nhập Sĩ số: 35
echo    - Nhập Phòng học: "A101"
echo    - Click "Thêm"
echo.
echo 2. SỬA LỚP
echo    - Click chọn dòng trong DataGridView
echo    - Sửa thông tin (Tên lớp, GV, Sĩ số, Phòng)
echo    - Click "Sửa"
echo.
echo 3. XÓA LỚP (TEST QUAN TRỌNG!)
echo    - Thử xóa lớp CÓ SINH VIÊN → Báo lỗi (LINQ kiểm tra)
echo    - Xóa lớp KHÔNG CÓ SINH VIÊN → Xóa thành công
echo.
echo 4. TÌM KIẾM
echo    - Nhập từ khóa (Tên lớp/Phòng học)
echo    - Click "Tìm kiếm"
echo.
echo 5. KIỂM TRA SỐ SINH VIÊN
echo    - Chọn lớp → Xem "Số SV hiện tại" (LINQ Count)
echo    - Nếu vượt sĩ số → Hiển thị màu đỏ
echo.
echo ─────────────────────────────────────────────────────
echo.

dotnet run --project StudentManagement.csproj classform

echo.
echo ─────────────────────────────────────────────────────
echo KẾT THÚC TEST
echo ─────────────────────────────────────────────────────
echo.
pause
