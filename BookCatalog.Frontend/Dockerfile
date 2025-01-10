# Use .NET 8 SDK for building the application
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copy project files
COPY . ./

# Restore dependencies
RUN dotnet restore

# Build the Blazor application
RUN dotnet publish -c Release -o /publish

# Use Nginx to serve the Blazor app
FROM nginx:alpine AS final
WORKDIR /usr/share/nginx/html

# Copy the published Blazor files
COPY --from=build /publish/wwwroot .

# Expose port 80
EXPOSE 80

# Start Nginx
CMD ["nginx", "-g", "daemon off;"]
