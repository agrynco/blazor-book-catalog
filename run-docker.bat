@echo off
setlocal

REM Check if docker-compose.yml exists
if not exist "docker-compose.yml" (
    echo docker-compose.yml not found. Please ensure it exists in the current directory.
    pause
    exit /b 1
)

docker-compose down --remove-orphans

echo Starting the application using Docker Compose...

docker-compose build

start http://localhost:5000/swagger
start http://localhost:8080

docker-compose up

echo Application has been started in Docker containers.
