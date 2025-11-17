@echo off
echo ========================================
echo       TEST LOGIN FORM
echo ========================================
echo.
echo 1. Test Login Form (UI)
echo 2. Test Login Cases (Console - LINQ)
echo 3. Thoat
echo.
set /p choice="Chon tuy chon (1-3): "

if "%choice%"=="1" (
    echo.
    echo Dang mo LoginForm...
    cd StudentManagement
    dotnet run login
) else if "%choice%"=="2" (
    echo.
    echo Dang chay test login cases...
    cd StudentManagement
    dotnet run testlogin
    pause
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