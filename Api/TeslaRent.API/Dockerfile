FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["TeslaRent.API/TeslaRent.API.csproj", "TeslaRent.API/"]
RUN dotnet restore "TeslaRent.API/TeslaRent.API.csproj"
COPY . .
WORKDIR "/src/TeslaRent.API"
RUN dotnet build "TeslaRent.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TeslaRent.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TeslaRent.API.dll"]
