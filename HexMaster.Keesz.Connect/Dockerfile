FROM microsoft/dotnet:2.1.4-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 63884
EXPOSE 44338

FROM microsoft/dotnet:2.1.4-aspnetcore-runtime AS build
WORKDIR /src
COPY HexMaster.Keesz.Connect/HexMaster.Keesz.Connect.csproj HexMaster.Keesz.Connect/
RUN dotnet restore HexMaster.Keesz.Connect/HexMaster.Keesz.Connect.csproj
COPY . .
WORKDIR /src/HexMaster.Keesz.Connect
RUN dotnet build HexMaster.Keesz.Connect.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish HexMaster.Keesz.Connect.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "HexMaster.Keesz.Connect.dll"]
