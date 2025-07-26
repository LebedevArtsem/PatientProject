FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["PatientProject.Api/PatientProject.Api.csproj", "PatientProject.Api/"]
COPY ["PatientProject.Infrastructure/PatientProject.Infrastructure.csproj", "PatientProject.Infrastructure/"]
COPY ["PatientProject.Application/PatientProject.Application.csproj", "PatientProject.Application/"]
COPY ["PatientProject.Domain/PatientProject.Domain.csproj", "PatientProject.Domain/"]
RUN dotnet restore "PatientProject.Api/PatientProject.Api.csproj"
COPY . .
WORKDIR "/PatientProject.Api"
RUN dotnet build "./PatientProject.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./PatientProject.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PatientProject.Api.dll"]