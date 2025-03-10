# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app

# Create a directory for the app and the data protection keys, and ensure the app user has the right permissions
RUN mkdir -p /app/dataprotection && chown -R app:app /app/dataprotection

EXPOSE 8080
EXPOSE 8081


# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["WestoniaAPI/WestoniaAPI.csproj", "WestoniaAPI/"]
COPY ["WestoniaAPI.DataLogic/WestoniaAPI.DataLogic.csproj", "WestoniaAPI.DataLogic/"]
COPY ["WestoniaAPI.DataAccess/WestoniaAPI.DataAccess.csproj", "WestoniaAPI.DataAccess/"]
COPY ["WestoniaAPI.Core/WestoniaAPI.Core.csproj", "WestoniaAPI.Core/"]
COPY ["WestoniaAPI.DataLayer/WestoniaAPI.DataLayer.csproj", "WestoniaAPI.DataLayer/"]
RUN dotnet restore "./WestoniaAPI/WestoniaAPI.csproj"
COPY . .
WORKDIR "/src/WestoniaAPI"
RUN dotnet build "./WestoniaAPI.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./WestoniaAPI.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WestoniaAPI.dll"]