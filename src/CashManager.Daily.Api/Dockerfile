FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 5079
EXPOSE 80

ENV ASPNETCORE_URLS=http://+:5079

USER app
FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG configuration=Release
WORKDIR /src
COPY ["src/CashManager.Daily.Api/CashManager.Daily.Api.csproj", "src/CashManager.Daily.Api/"]
RUN dotnet restore "src/CashManager.Daily.Api/CashManager.Daily.Api.csproj"
COPY . .
WORKDIR "/src/src/CashManager.Daily.Api"
RUN dotnet build "CashManager.Daily.Api.csproj" -c $configuration -o /app/build

FROM build AS publish
ARG configuration=Release
RUN dotnet publish "CashManager.Daily.Api.csproj" -c $configuration -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CashManager.Daily.Api.dll"]
