# Build arguments
ARG DOTNET_RUNTIME=mcr.microsoft.com/dotnet/aspnet:8.0
ARG DOTNET_SDK=mcr.microsoft.com/dotnet/sdk:8.0

ENV ASPNETCORE_ENVIRONMENT Test

# Stage 1: base
FROM ${DOTNET_RUNTIME} AS base
WORKDIR /app
EXPOSE 8080

# Stage 2: build
FROM ${DOTNET_SDK} AS build
WORKDIR /app

# Copy projects files
COPY backend/Backend.sln ./
COPY backend/Core/Core.csproj ./backend/Core/
COPY backend/Infrastructure/Infrastructure.csproj ./backend/Infrastructure/
COPY backend/API/API.csproj ./backend/API/
COPY backend/backendTests/backendTests.csproj ./backend/backendTests/

# Restore dependencies
RUN dotnet restore "./backend/API/API.csproj"

# Copy all source code
COPY backend/ ./backend/

# Build the application
RUN dotnet build "./backend/API/API.csproj" -c Debug -o /app/build

CMD ["dotnet", "test", "./backend/backendTests/backendTests.csproj"]
