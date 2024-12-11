FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["RentalCar.Security.API/RentalCar.Security.API.csproj", "RentalCar.Security.API/"]
#COPY ["RentalCar.Security.Application/RentalCar.Security.Application.csproj", "RentalCar.Security.Application/"]
#COPY ["RentalCar.Security.Core/RentalCar.Security.Core.csproj", "RentalCar.Security.Core/"]
#COPY ["RentalCar.Security.Infrastructure/RentalCar.Security.Infrastructure.csproj", "RentalCar.Security.Infrastructure/"]
RUN dotnet restore "RentalCar.Security.API/RentalCar.Security.API.csproj"
COPY . .
WORKDIR "/src/RentalCar.Security.API"
RUN dotnet build "RentalCar.Security.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "RentalCar.Security.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "RentalCar.Security.API.dll"]
