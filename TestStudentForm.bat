@echo off
chcp 65001 > nul
title TEST STUDENT FORM - PROMPT 7

echo.
echo ╔══════════════════════════════════════════════════════╗
echo ║        CHẠY TEST STUDENT FORM (PROMPT 7)             ║
echo ║     Form quản lý sinh viên với CRUD đầy đủ          ║
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
echo ĐANG MỞ STUDENT FORM...
echo ─────────────────────────────────────────────────────
echo.
echo HƯỚNG DẪN TEST:
echo 1. THÊM sinh viên mới
echo    - Nhập đầy đủ: Mã SV, Tên, Lớp, Ngày sinh, Giới tính
echo    - Có thể bỏ trống: Email, Phone, Address
echo    - Click "Thêm"
echo.
echo 2. SỬA sinh viên
echo    - Click chọn dòng trong DataGridView
echo    - Sửa thông tin
echo    - Click "Sửa"
echo.
echo 3. XÓA sinh viên
echo    - Click chọn dòng
echo    - Click "Xóa"
echo    - Xác nhận
echo.
echo 4. TÌM KIẾM
echo    - Nhập từ khóa (Tên/Mã SV/Email/Phone)
echo    - Click "Tìm kiếm"
echo.
echo ─────────────────────────────────────────────────────
echo.

dotnet run --project StudentManagement.csproj studentform

echo.
echo ─────────────────────────────────────────────────────
echo KẾT THÚC TEST
echo ─────────────────────────────────────────────────────
echo.
pause
