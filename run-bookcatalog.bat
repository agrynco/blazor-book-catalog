@echo off
setlocal

REM Define variables
set "API_PROJECT=BookCatalog.API"
set "FRONTEND_PROJECT=BookCatalog.Frontend"

echo Starting BookCatalog projects...

REM Check if the API project directory exists
if not exist "%API_PROJECT%" (
    echo Error: API project directory "%API_PROJECT%" not found.
    pause
    exit /b 1
)

REM Check if the Frontend project directory exists
if not exist "%FRONTEND_PROJECT%" (
    echo Error: Frontend project directory "%FRONTEND_PROJECT%" not found.
    pause
    exit /b 1
)

REM Start the API project in a new window
echo Starting the API project...
start "API Console" cmd /k "cd %API_PROJECT% && dotnet run"
start http://localhost:5000/swagger

REM Start the Frontend project in a new window
echo Starting the Frontend project...
set ASPNETCORE_URLS=http://localhost:5163
start "Frontend Console" cmd /k "cd %FRONTEND_PROJECT% && dotnet run"
start http://localhost:5163

echo All services started successfully. Close this window if you don't need it anymore.
pause
