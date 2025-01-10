@echo off
setlocal

REM Define variables
set "API_PROJECT=BookCatalog.API"
set "FRONTEND_PROJECT=BookCatalog.Frontend"

echo Starting BookCatalog projects...

REM Start the API project in a new window
start "API Console" cmd /k "cd %API_PROJECT% && dotnet run"
start http://localhost:5000/swagger

REM Start the Frontend project in a new window
start "Frontend Console" cmd /k "cd %FRONTEND_PROJECT% && dotnet run"
start http://localhost:5163

echo All services started. Close this window if you don't need it anymore.
pause
