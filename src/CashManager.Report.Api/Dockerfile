FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 5080
EXPOSE 80

ENV ASPNETCORE_URLS=http://+:5080

USER app
FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG configuration=Release
WORKDIR /src
COPY ["src/CashManager.Report.Api/CashManager.Report.Api.csproj", "src/CashManager.Report.Api/"]
RUN dotnet restore "src/CashManager.Report.Api/CashManager.Report.Api.csproj"
COPY . .
WORKDIR "/src/src/CashManager.Report.Api"
RUN dotnet build "CashManager.Report.Api.csproj" -c $configuration -o /app/build

FROM build AS publish
ARG configuration=Release
RUN dotnet publish "CashManager.Report.Api.csproj" -c $configuration -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CashManager.Report.Api.dll"]
