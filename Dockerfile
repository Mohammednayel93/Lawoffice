# Use official .NET SDK to build the app
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /app

# Copy solution and restore
COPY *.sln .
COPY Lawoffice/*.csproj ./Lawoffice/
COPY Lawoffice.Models/*.csproj ./Lawoffice.Models/
COPY Lawoffice.DTOs/*.csproj ./Lawoffice.DTOs/
COPY Lawoffice.Services/*.csproj ./Lawoffice.Services/

RUN dotnet restore

# Copy everything and build
COPY . .
WORKDIR /app/Lawoffice
RUN dotnet publish -c Release -o /app/publish

# Use runtime-only image
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "Lawoffice.dll"]
