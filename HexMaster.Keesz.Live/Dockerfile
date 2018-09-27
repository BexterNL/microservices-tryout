FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 59629
EXPOSE 44370

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY ["HexMaster.Keesz.Live/HexMaster.Keesz.Live.csproj", "HexMaster.Keesz.Live/"]
RUN dotnet restore "HexMaster.Keesz.Live/HexMaster.Keesz.Live.csproj"
COPY . .
WORKDIR "/src/HexMaster.Keesz.Live"
RUN dotnet build "HexMaster.Keesz.Live.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "HexMaster.Keesz.Live.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "HexMaster.Keesz.Live.dll"]