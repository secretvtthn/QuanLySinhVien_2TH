@echo off
echo ========================================
echo       TEST CHANGE PASSWORD FORM
echo ========================================
echo.
echo Dang mo ChangePasswordForm...
cd StudentManagement
dotnet run --project StudentManagement.csproj StudentManagement.TestChangePasswordForm
echo.
echo ========================================
pause
