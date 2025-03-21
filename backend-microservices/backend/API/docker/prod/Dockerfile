# Dockerfile for Backend
# Location: backend-microservices/backend/docker/prod/Dockerfile

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /app

# Copy all necessary project files
COPY Backend.sln ./
COPY Core/Core.csproj ./Core/
COPY Infrastructure/Infrastructure.csproj ./Infrastructure/
COPY API/API.csproj ./API/
COPY backendTests/backendTests.csproj ./backendTests/

# Restore dependencies
RUN dotnet restore "./API/API.csproj"

# Copy all source code
COPY . .

# Build and publish the application
RUN dotnet publish "./API/API.csproj" -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app

# Install FFmpeg
RUN apt-get update \
    && apt-get install -y ffmpeg \
    && apt-get clean \
    && rm -rf /var/lib/apt/lists/*

COPY --from=build-env /app/out .
ENTRYPOINT ["dotnet", "API.dll"]
