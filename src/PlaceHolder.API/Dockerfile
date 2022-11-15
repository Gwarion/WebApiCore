#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/PlaceHolder.API/PlaceHolder.API.csproj", "src/PlaceHolder.API/"]
COPY ["src/PlaceHolder.DependencyInjection/PlaceHolder.DependencyInjection.csproj", "src/PlaceHolder.DependencyInjection/"]
COPY ["src/PlaceHolder.Application.Services/PlaceHolder.Application.Services.csproj", "src/PlaceHolder.Application.Services/"]
COPY ["src/PlaceHolder.Utils/PlaceHolder.Utils.csproj", "src/PlaceHolder.Utils/"]
RUN dotnet restore "src/PlaceHolder.API/PlaceHolder.API.csproj"
COPY . .
WORKDIR "/src/src/PlaceHolder.API"
RUN dotnet build "PlaceHolder.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "PlaceHolder.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PlaceHolder.API.dll"]