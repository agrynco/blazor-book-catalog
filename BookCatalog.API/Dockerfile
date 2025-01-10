# Use the official .NET SDK image to build and publish the app
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copy and restore dependencies
COPY *.csproj ./
RUN dotnet restore

# Copy the entire project and build the app
COPY . ./
RUN dotnet publish -c Release -o /publish

# Use the .NET runtime image to run the app
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /publish .

# Expose the port the API listens on
EXPOSE 5000

# Start the app
ENTRYPOINT ["dotnet", "BookCatalog.API.dll"]
