#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

WORKDIR /build
COPY src/ .

RUN dotnet publish "PlaceHolder.API.csproj" -c Release -o dist

FROM base AS run
WORKDIR /app
COPY --from=build /build/dist .

ENTRYPOINT ["dotnet", "PlaceHolder.API.dll"]