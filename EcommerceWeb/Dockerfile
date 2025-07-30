# Base image for runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

COPY ["EcommerceWeb/EcommerceWeb.csproj", "EcommerceWeb/"]
COPY ["Ecommerce_BE.Repositories.csproj", "Ecommerce_BE.Repositories/"]
COPY ["Ecommerce_BE.Contract.Repositories.csproj", "Ecommerce_BE.Contract.Repositories/"]
COPY ["Ecommerce_BE.Contract.Services.csproj", "Ecommerce_BE.Contract.Services/"]
COPY ["Ecommerce_BE.Services.csproj", "Ecommerce_BE.Services/"]
COPY ["Ecommerce_BE.Middleware.csproj", "Ecommerce_BE.Middleware/"]
COPY ["Ecommerce_BE.Core.csproj", "Ecommerce_BE.Core/"]

RUN dotnet restore "EcommerceWeb/EcommerceWeb.csproj"

COPY . .

# Build
WORKDIR /src/EcommerceWeb
RUN dotnet build "EcommerceWeb.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Publish stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./EcommerceWeb.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Final image for runtime
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "EcommerceWeb.dll"]
