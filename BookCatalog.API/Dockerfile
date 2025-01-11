FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

COPY *.sln ./

COPY BookCatalog.API/*.csproj ./BookCatalog.API/

RUN dotnet restore ./BookCatalog.API/BookCatalog.API.csproj

COPY BookCatalog.API/. ./BookCatalog.API/

WORKDIR /app/BookCatalog.API

RUN dotnet publish -c Release -o /publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app

COPY --from=build /publish .

EXPOSE 5000

ENTRYPOINT ["dotnet", "BookCatalog.API.dll"]