FROM mcr.microsoft.com/dotnet/core/aspnet:2.1-stretch-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:2.1-stretch AS build
WORKDIR /src
COPY ["src/Services/App.API/App.API.csproj", "src/Services/App.API/"]
RUN dotnet restore "src/Services/App.API/App.API.csproj"
COPY . .
WORKDIR "/src/src/Services/App.API"
RUN dotnet build "App.API.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "App.API.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "App.API.dll"]