@echo off
chcp 65001 > nul
title TEST SCORE REPOSITORY - PROMPT 10

echo.
echo ╔══════════════════════════════════════════════════════╗
echo ║         CHẠY TEST SCORE REPOSITORY (PROMPT 10)       ║
echo ║    Kiểm tra CRUD + LINQ + VALIDATION (Điểm 0-10)    ║
echo ║      + Filter theo Semester/AcademicYear             ║
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

dotnet run --project StudentManagement.csproj TestScoreRepository

echo.
echo ─────────────────────────────────────────────────────
echo KẾT THÚC TEST
echo ─────────────────────────────────────────────────────
echo.
pause
