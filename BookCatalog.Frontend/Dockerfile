FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

WORKDIR /app

COPY BookCatalog.sln ./
COPY ./BookCatalog.API/BookCatalog.API.csproj ./BookCatalog.API/
COPY ./BookCatalog.Frontend/BookCatalog.Frontend.csproj ./BookCatalog.Frontend/

RUN dotnet restore
COPY . ./
RUN dotnet publish -c Release -o out

FROM nginx:latest
WORKDIR /app
EXPOSE 8080
COPY ./BookCatalog.Frontend/nginx.conf /etc/nginx/nginx.conf
COPY --from=build /app/out/wwwroot /usr/share/nginx/html