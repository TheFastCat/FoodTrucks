#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
RUN apt-get update AS update
RUN apt-get install -y wget AS wget
RUN apt-get install -y gnupg2 AS gnupg2
RUN wget -qO- https://deb.nodesource.com/setup_12.x | bash - AS node
RUN apt-get install -y build-essential nodejs AS nodeinstall

WORKDIR /src
COPY ["FoodTrucks/FoodTrucks.csproj", "FoodTrucks/"]
RUN dotnet restore "FoodTrucks/FoodTrucks.csproj"
COPY . .
WORKDIR "/src/FoodTrucks"
RUN dotnet build "FoodTrucks.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "FoodTrucks.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FoodTrucks.dll"]