FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Thunders.Tasks.API/Thunders.Tasks.API.csproj", "Thunders.Tasks.API/"]
RUN dotnet restore "./Thunders.Tasks.API/Thunders.Tasks.API.csproj"
COPY . .
WORKDIR "/src/Thunders.Tasks.API"
RUN dotnet build "./Thunders.Tasks.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Thunders.Tasks.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Thunders.Tasks.API.dll"]