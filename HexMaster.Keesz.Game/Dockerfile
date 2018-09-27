FROM microsoft/dotnet:2.1.4-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 58172
EXPOSE 44348

FROM microsoft/dotnet:2.1.4-aspnetcore-runtime AS build
WORKDIR /src
COPY ["HexMaster.Keesz.Game/HexMaster.Keesz.Game.csproj", "HexMaster.Keesz.Game/"]
RUN dotnet restore "HexMaster.Keesz.Game/HexMaster.Keesz.Game.csproj"
COPY . .
WORKDIR "/src/HexMaster.Keesz.Game"
RUN dotnet build "HexMaster.Keesz.Game.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "HexMaster.Keesz.Game.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "HexMaster.Keesz.Game.dll"]
