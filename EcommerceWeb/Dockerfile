FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

COPY ["EcommerceWeb/EcommerceWeb.csproj", "EcommerceWeb/"]
COPY ["Ecommerce_BE.Repositories/Ecommerce_BE.Repositories.csproj", "Ecommerce_BE.Repositories/"]
COPY ["Ecommerce_BE.Contract.Repositories/Ecommerce_BE.Contract.Repositories.csproj", "Ecommerce_BE.Contract.Repositories/"]
COPY ["Ecommerce_BE.Contract.Services/Ecommerce_BE.Contract.Services.csproj", "Ecommerce_BE.Contract.Services/"]
COPY ["Ecommerce_BE.Services/Ecommerce_BE.Services.csproj", "Ecommerce_BE.Services/"]
COPY ["Ecommerce_BE.Middleware/Ecommerce_BE.Middleware.csproj", "Ecommerce_BE.Middleware/"]
COPY ["Ecommerce_BE.Core/Ecommerce_BE.Core.csproj", "Ecommerce_BE.Core/"]

RUN dotnet restore "EcommerceWeb/EcommerceWeb.csproj"

COPY . .

WORKDIR /src/EcommerceWeb
RUN dotnet build "EcommerceWeb.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "EcommerceWeb.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "EcommerceWeb.dll"]
