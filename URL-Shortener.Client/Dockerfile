﻿FROM mcr.microsoft.com/dotnet/sdk:7.0 as build-env
WORKDIR /app

COPY *.csproj ./
RUN dotnet restore

COPY . ./
RUN dotnet publish -c release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:7.0
WORKDIR /app
COPY --from=build-env /app/publish ./
ENTRYPOINT ["dotnet", "URL-Shortener.Client.dll"]