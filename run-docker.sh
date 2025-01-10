#!/bin/bash

# Check if docker-compose.yml exists
if [ ! -f "docker-compose.yml" ]; then
    echo "docker-compose.yml not found. Please ensure it exists in the current directory."
    exit 1
fi

echo "Starting the application using Docker Compose..."

# Run Docker Compose to start containers
docker-compose up --build

echo "Application has been started in Docker containers."
