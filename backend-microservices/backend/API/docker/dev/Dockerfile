# Dockerfile for Backend
# Location: backend-microservices/backend/docker/dev/Dockerfile

# Build arguments
ARG DOTNET_RUNTIME=mcr.microsoft.com/dotnet/aspnet:8.0
ARG DOTNET_SDK=mcr.microsoft.com/dotnet/sdk:8.0

# Stage 1: base
FROM ${DOTNET_RUNTIME} AS base
WORKDIR /app
EXPOSE 8080

# Install FFmpeg
RUN apt-get update \
    && apt-get install -y ffmpeg \
    && apt-get clean \
    && rm -rf /var/lib/apt/lists/*

# Stage 2: build
FROM ${DOTNET_SDK} AS build
WORKDIR /app

# Copy project files
COPY Backend.sln ./
COPY Core/Core.csproj ./Core/
COPY Infrastructure/Infrastructure.csproj ./Infrastructure/
COPY API/API.csproj ./API/

# Restore dependencies
RUN dotnet restore "./API/API.csproj"

# Copy all source code
COPY  . .

# Build the application
RUN dotnet build "./API/API.csproj" -c Debug -o /app/build

# Stage 3: final
FROM base AS final
WORKDIR /app

# Copy the build output to the final image
COPY --from=build /app/build .

# Copy the entrypoint script
COPY API/entrypoint.sh ./

# Grant permissions to the entrypoint script
RUN chmod +x ./entrypoint.sh

ENTRYPOINT ["./entrypoint.sh"]
