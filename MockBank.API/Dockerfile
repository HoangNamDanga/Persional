FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src
# Copy file csproj và restore trước để tận dụng cache
COPY ["MockBank.API.csproj", "./"]
RUN dotnet restore "./MockBank.API.csproj"

# Copy toàn bộ code còn lại
COPY . .
# Build dự án
RUN dotnet build "MockBank.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MockBank.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MockBank.API.dll"]