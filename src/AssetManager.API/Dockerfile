FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

COPY ../AssetManager.Domain/AssetManager.Domain.csproj AssetManager.Domain/
COPY ../AssetManager.Application/AssetManager.Application.csproj AssetManager.Application/
COPY ../AssetManager.Infrastructure/AssetManager.Infrastructure.csproj AssetManager.Infrastructure/
COPY AssetManager.API.csproj AssetManager.API/
RUN dotnet restore AssetManager.API/AssetManager.API.csproj

COPY ../AssetManager.Domain/ AssetManager.Domain/
COPY ../AssetManager.Application/ AssetManager.Application/
COPY ../AssetManager.Infrastructure/ AssetManager.Infrastructure/
COPY . AssetManager.API/
RUN dotnet publish AssetManager.API/AssetManager.API.csproj -c Release -o /app

FROM mcr.microsoft.com/dotnet/aspnet:10.0
WORKDIR /app
COPY --from=build /app .
EXPOSE 8080
ENTRYPOINT ["dotnet", "AssetManager.API.dll"]
