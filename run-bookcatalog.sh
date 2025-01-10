#!/bin/bash

# Define variables
API_PROJECT="BookCatalog.API"
FRONTEND_PROJECT="BookCatalog.Frontend"

echo "Starting BookCatalog projects..."

# Start the API project in a new terminal
gnome-terminal -- bash -c "cd $API_PROJECT && dotnet run; exec bash"

# Start the Frontend project in a new terminal
gnome-terminal -- bash -c "cd $FRONTEND_PROJECT && dotnet run; exec bash"

echo "All services started. Close this terminal if you don't need it anymore."
