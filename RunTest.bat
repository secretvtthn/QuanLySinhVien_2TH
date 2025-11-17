@echo off
echo ========================================
echo       TEST KET NOI DATABASE
echo ========================================
echo.
echo Compiling Console Test...
cd StudentManagement
csc /reference:bin\Debug\net9.0-windows\Microsoft.Data.SqlClient.dll /reference:bin\Debug\net9.0-windows\System.Configuration.ConfigurationManager.dll ConsoleTest.cs Helper\DatabaseHelper.cs /out:TestConnection.exe

echo.
echo Running Test...
TestConnection.exe

echo.
echo ========================================
pause