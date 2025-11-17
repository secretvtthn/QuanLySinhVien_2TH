@echo off
echo ====================================
echo TEST REPORTSTUDENTFORM
echo ====================================
echo.

cd StudentManagement
dotnet build -c Debug
if %errorlevel% neq 0 (
    echo.
    echo BUILD FAILED!
    pause
    exit /b 1
)

echo.
echo BUILD SUCCESS! Running test...
echo.

dotnet run --project StudentManagement.csproj --no-build reportstudentform

echo.
echo ====================================
echo TEST COMPLETED
echo ====================================
pause
