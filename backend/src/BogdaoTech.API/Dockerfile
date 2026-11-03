# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081


# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/BogdaoTech.API/BogdaoTech.API.csproj", "src/BogdaoTech.API/"]
COPY ["src/BogdaoTech.Application/BogdaoTech.Application.csproj", "src/BogdaoTech.Application/"]
COPY ["src/BogdaoTech.Domain/BogdaoTech.Domain.csproj", "src/BogdaoTech.Domain/"]
COPY ["src/BogdaoTech.Infrastructure/BogdaoTech.Infrastructure.csproj", "src/BogdaoTech.Infrastructure/"]
RUN dotnet restore "./src/BogdaoTech.API/BogdaoTech.API.csproj"
COPY . .
WORKDIR "/src/src/BogdaoTech.API"
RUN dotnet build "./BogdaoTech.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./BogdaoTech.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BogdaoTech.API.dll"]