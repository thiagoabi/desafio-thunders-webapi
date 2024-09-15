# Use the .NET SDK image
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy the project file and restore dependencies
COPY src/Thunders.Tasks.API/Thunders.Tasks.API.csproj ./Thunders.Tasks.API.csproj
RUN dotnet restore Thunders.Tasks.API.csproj

# Copy the rest of the source code and build the application
COPY . ./
RUN dotnet build Thunders.Tasks.API.csproj -c Release -o /app/build

# Publish the application
FROM build AS publish
RUN dotnet publish Thunders.Tasks.API.csproj -c Release -o /app/publish

# Final stage with the runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=publish /app/publish ./
ENTRYPOINT ["dotnet", "Thunders.Tasks.API.dll"]
