FROM microsoft/dotnet:2.1.4-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 63172
EXPOSE 44398

FROM microsoft/dotnet:2.1.4-aspnetcore-runtime AS build
WORKDIR /src
COPY HexMaster.Keesz.Api/HexMaster.Keesz.Api.csproj HexMaster.Keesz.Api/
RUN dotnet restore HexMaster.Keesz.Api/HexMaster.Keesz.Api.csproj
COPY . .
WORKDIR /src/HexMaster.Keesz.Api
RUN dotnet build HexMaster.Keesz.Api.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish HexMaster.Keesz.Api.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "HexMaster.Keesz.Api.dll"]
