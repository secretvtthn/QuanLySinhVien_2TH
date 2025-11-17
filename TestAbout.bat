@echo off
echo ========================================
echo       TEST ABOUT FORM
echo ========================================
echo.
echo 1. Test AboutForm rieng le
echo 2. Test AboutForm tu Form1
echo 3. Thoat
echo.
set /p choice="Chon tuy chon (1-3): "

if "%choice%"=="1" (
    echo.
    echo Dang mo AboutForm...
    cd StudentManagement
    dotnet run aboutonly
) else if "%choice%"=="2" (
    echo.
    echo Dang mo Form1 voi button About...
    cd StudentManagement
    dotnet run about
) else if "%choice%"=="3" (
    echo Thoat...
    exit /b
) else (
    echo Tuy chon khong hop le!
    pause
    goto start
)

echo.
echo ========================================
pause