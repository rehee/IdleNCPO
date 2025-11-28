FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["src/IdleNCPO.Server/IdleNCPO.Server.csproj", "src/IdleNCPO.Server/"]
COPY ["src/IdleNCPO.Data/IdleNCPO.Data.csproj", "src/IdleNCPO.Data/"]
COPY ["src/IdleNCPO.Core/IdleNCPO.Core.csproj", "src/IdleNCPO.Core/"]
COPY ["src/IdleNCPO.Abstractions/IdleNCPO.Abstractions.csproj", "src/IdleNCPO.Abstractions/"]
RUN dotnet restore "src/IdleNCPO.Server/IdleNCPO.Server.csproj"
COPY . .
WORKDIR "/src/src/IdleNCPO.Server"
RUN dotnet build "IdleNCPO.Server.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "IdleNCPO.Server.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "IdleNCPO.Server.dll"]
