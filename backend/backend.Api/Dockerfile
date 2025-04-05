# ---------------------------------------------------------------------------
# BASE IMAGE
# ---------------------------------------------------------------------------
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base

RUN apt-get update && apt-get install -y tzdata

# Instalar dependencias necesarias
RUN apt-get update && \
    apt-get install -y apt-utils && \
    apt-get install -y libgdiplus && \
    apt-get install -y libc6-dev && \
    ln -s /usr/lib/libgdiplus.so /usr/lib/gdiplus.dll

RUN mkdir -p /app/wwwroot && \
    chmod 755 /app/wwwroot
WORKDIR /app
EXPOSE 8080
ENV TZ=America/Costa_Rica

# ---------------------------------------------------------------------------
# BUILD IMAGE
# ---------------------------------------------------------------------------
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Copiar archivos de proyecto
COPY ["backend.Api/backend.Api.csproj", "backend.Api/"]
COPY ["backend.Application/backend.Application.csproj", "backend.Application/"]
COPY ["backend.Domain/backend.Domain.csproj", "backend.Domain/"]
COPY ["backend.Utilities/backend.Utilities.csproj", "backend.Utilities/"]
COPY ["backend.Infrastructure/backend.Infrastructure.csproj", "backend.Infrastructure/"]

# Restaurar paquetes
RUN dotnet restore "./backend.Api/backend.Api.csproj"

# Copiar el resto del código fuente
COPY . .

WORKDIR "/src/backend.Api"
RUN dotnet build "./backend.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

# ---------------------------------------------------------------------------
# PUBLISH IMAGE
# ---------------------------------------------------------------------------
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./backend.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# ---------------------------------------------------------------------------
# FINAL IMAGE
# ---------------------------------------------------------------------------
FROM base AS final
WORKDIR /app

# Copiar la app publicada
COPY --from=publish /app/publish .

# Establecer permisos
RUN chmod 755 /app

EXPOSE 8080

# Variables de entorno (si usás HashiCorp Vault u otros secretos)
ARG VAULT_TOKEN
ENV VAULT_TOKEN=$VAULT_TOKEN

ENV ASPNETCORE_ENVIRONMENT=Production

ENTRYPOINT ["dotnet", "backend.Api.dll"]
