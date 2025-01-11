# Use the official .NET SDK image to build and publish the app
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copy the solution file
COPY *.sln ./

# Copy the project file for API
COPY BookCatalog.API/*.csproj ./BookCatalog.API/

# Restore dependencies for the API project
RUN dotnet restore ./BookCatalog.API/BookCatalog.API.csproj

# Copy the entire source code for the API project
COPY BookCatalog.API/. ./BookCatalog.API/

# Set the working directory to the API project folder
WORKDIR /app/BookCatalog.API

# Publish the application
RUN dotnet publish -c Release -o /publish

# Use the .NET runtime image to run the app
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app

# Copy the published application from the build stage
COPY --from=build /publish .

# Expose the port the API listens on
EXPOSE 5000

# Start the app
ENTRYPOINT ["dotnet", "BookCatalog.API.dll"]