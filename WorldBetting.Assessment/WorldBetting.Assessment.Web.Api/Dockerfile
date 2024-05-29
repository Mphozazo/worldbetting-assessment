#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

#Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
#For more information, please see https://aka.ms/containercompat

FROM mcr.microsoft.com/dotnet/aspnet:8.0-nanoserver-1809 AS base
WORKDIR /app
EXPOSE 8080

FROM base AS debug
RUN tdnf install procps-ng -y

FROM mcr.microsoft.com/dotnet/sdk:8.0-nanoserver-1809 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["WorldBetting.Assessment.Web.Api/WorldBetting.Assessment.Web.Api.csproj", "WorldBetting.Assessment.Web.Api/"]
RUN dotnet restore "./WorldBetting.Assessment.Web.Api/WorldBetting.Assessment.Web.Api.csproj"
COPY . .
WORKDIR "/src/WorldBetting.Assessment.Web.Api"
RUN dotnet build "./WorldBetting.Assessment.Web.Api.csproj" -c %BUILD_CONFIGURATION% -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./WorldBetting.Assessment.Web.Api.csproj" -c %BUILD_CONFIGURATION% -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WorldBetting.Assessment.Web.Api.dll"]

# Use the official MySQL image from the Docker Hub
FROM mysql:latest

# Set environment variables for MySQL root password and database name
ENV MYSQL_ROOT_PASSWORD=worldbetting
ENV MYSQL_DATABASE=worldbetting

# Expose the MySQL port
EXPOSE 3306


# User the official Redis image from the Hub
FROM redis:latest

#Expose Redis Port
EXPOSE 6379