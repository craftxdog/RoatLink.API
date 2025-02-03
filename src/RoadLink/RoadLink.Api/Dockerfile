FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src
COPY ["src/RoadLink/RoadLink.Api/RoatLink.Api.csproj", "src/RoadLink/RoadLink.Api/"]
COPY ["src/RoadLink/RoadLink.Infrastructure/RoadLink.Infrastructure.csproj", "src/RoadLink/RoadLink.Infrastructure/"]
COPY ["src/RoadLink/RoadLink.Application/RoadLink.Application.csproj", "src/RoadLink/RoadLink.Application/"]
COPY ["src/RoadLink/RoadLink.Domain/RoadLink.Domain.csproj", "src/RoadLink/RoadLink.Domain/"]
RUN dotnet restore "src/RoadLink/RoadLink.Api/RoatLink.Api.csproj"
COPY . .
WORKDIR "/src/src/RoadLink/RoadLink.Api"
RUN dotnet build "RoatLink.Api.csproj" -c Debug -o /app/build

FROM build AS publish
RUN dotnet publish "RoatLink.Api.csproj" -c Debug -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "RoatLink.Api.dll"]
