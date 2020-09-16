#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

# Para build e actions no GitHub, utilizar o seguinte link;
# https://github.com/renatogroffe/ASPNETCore3.1-REST_API-xUnit-FluentAssertions-Swagger-Docker_JurosCompostos/blob/master/APIFinancas/Dockerfile

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY API/API.csproj API/
RUN dotnet restore "API/API.csproj"

RUN pwsh -Command Write-Host "API: Gerando uma imagem Docker DotNet Core e testando o PowerShell Core"


COPY . .
WORKDIR "/src/API"
RUN dotnet build "API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "API.dll"]
