#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["RestaurantApi/RestaurantApi.csproj", "RestaurantApi/"]
COPY ["BuisnessLogic/BuisnessLogic.csproj", "BuisnessLogic/"]
COPY ["Models/Models.csproj", "Models/"]
COPY ["Entities/Entities.csproj", "Entities/"]
RUN dotnet restore "RestaurantApi/RestaurantApi.csproj"
COPY . .
WORKDIR "/src/RestaurantApi"
RUN dotnet build "RestaurantApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "RestaurantApi.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "RestaurantApi.dll"]
