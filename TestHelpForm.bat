@echo off
echo ========================================
echo       TEST HELP FORM
echo ========================================
echo.
echo Dang mo HelpForm...
cd StudentManagement
dotnet run --project StudentManagement.csproj StudentManagement.TestHelpForm
echo.
echo ========================================
pause
