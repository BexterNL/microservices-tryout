FROM microsoft/dotnet:2.1.4-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 54673
EXPOSE 44306

FROM microsoft/dotnet:2.1.4-aspnetcore-runtime AS build
WORKDIR /src
COPY HexMaster.Keesz.Identity/HexMaster.Keesz.Identity.csproj HexMaster.Keesz.Identity/
RUN dotnet restore HexMaster.Keesz.Identity/HexMaster.Keesz.Identity.csproj
COPY . .
WORKDIR /src/HexMaster.Keesz.Identity
RUN dotnet build HexMaster.Keesz.Identity.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish HexMaster.Keesz.Identity.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "HexMaster.Keesz.Identity.dll"]
