FROM microsoft/dotnet:2.1.4-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 63839
EXPOSE 44314

FROM microsoft/dotnet:2.1.4-aspnetcore-runtime AS build
WORKDIR /src
COPY HexMaster.Keesz.Client/HexMaster.Keesz.Client.csproj HexMaster.Keesz.Client/
RUN dotnet restore HexMaster.Keesz.Client/HexMaster.Keesz.Client.csproj
COPY . .
WORKDIR /src/HexMaster.Keesz.Client
RUN dotnet build HexMaster.Keesz.Client.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish HexMaster.Keesz.Client.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "HexMaster.Keesz.Client.dll"]
