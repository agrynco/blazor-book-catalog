# Use the official .NET SDK image to build the app
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copy and restore dependencies
COPY *.csproj ./
RUN dotnet restore

# Copy the entire project and build the app
COPY . ./
RUN dotnet publish -c Release -o /publish

# Use a lightweight web server (nginx) to serve the app
FROM nginx:alpine
WORKDIR /usr/share/nginx/html
COPY --from=build /publish/wwwroot .
COPY nginx.conf /etc/nginx/conf.d/default.conf
EXPOSE 80

# Expose the default nginx port
EXPOSE 80
