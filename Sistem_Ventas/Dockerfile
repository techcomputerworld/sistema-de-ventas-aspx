FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 62546
EXPOSE 44390

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY ["Sistem_Ventas/Sistem_Ventas.csproj", "Sistem_Ventas/"]
RUN dotnet restore "Sistem_Ventas/Sistem_Ventas.csproj"
COPY . .
WORKDIR "/src/Sistem_Ventas"
RUN dotnet build "Sistem_Ventas.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "Sistem_Ventas.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Sistem_Ventas.dll"]