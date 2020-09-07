# Main variables required for the Docker layers
ARG MAIN_PROJECT_NAME=InfiniteCalendar
ARG DOTNETCORE_VERSION=3.1

# Starting layer point using build image with dotnet SDK (very heavy image ~ 2GB)
FROM mcr.microsoft.com/dotnet/core/sdk:$DOTNETCORE_VERSION AS build-env
ARG MAIN_PROJECT_NAME
ARG DOTNETCORE_VERSION

WORKDIR /app

# Installs debugger for attaching processes
RUN curl -sSL https://aka.ms/getvsdbgsh | /bin/sh /dev/stdin -v latest -l /vsdbg

# Copies all files from the current directory. Commands on container will implicitly run restore.
COPY . ./