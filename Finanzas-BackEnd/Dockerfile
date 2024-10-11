FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Finanzas-BackEnd/Finanzas-BackEnd.csproj", "Finanzas-BackEnd/"]
RUN dotnet restore "Finanzas-BackEnd/Finanzas-BackEnd.csproj"
COPY . .
WORKDIR "/src/Finanzas-BackEnd"
RUN dotnet build "Finanzas-BackEnd.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Finanzas-BackEnd.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Finanzas-BackEnd.dll"]
