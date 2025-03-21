#!/bin/bash
set -e

# Default to production if no argument is provided
mode=${1:-production}

echo "Starting with mode: $mode"

case "$mode" in
    test)
        echo "Starting in test mode..."
        dotnet API.dll
        ;;
    development)
        echo "Starting in development mode..."
        dotnet watch run
        ;;
    production)
        echo "Starting in production mode..."
        dotnet API.dll
        ;;
    *)
        echo "Unknown mode: $mode"
        exit 1
        ;;
esac
