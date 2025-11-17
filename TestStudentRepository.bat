@echo off
chcp 65001 > nul
title TEST STUDENT REPOSITORY - PROMPT 6

echo.
echo ╔══════════════════════════════════════════════════════╗
echo ║        CHẠY TEST STUDENT REPOSITORY (PROMPT 6)       ║
echo ║     Kiểm tra CRUD + LINQ với ĐẦY ĐỦ CÁC TRƯỜNG     ║
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
echo BẮT ĐẦU CHẠY TEST...
echo ─────────────────────────────────────────────────────
echo.

dotnet run --project StudentManagement.csproj TestStudentRepository

echo.
echo ─────────────────────────────────────────────────────
echo KẾT THÚC TEST
echo ─────────────────────────────────────────────────────
echo.
pause
