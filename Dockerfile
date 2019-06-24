# FROM mcr.microsoft.com/dotnet/core/runtime:2.2
FROM microsoft/dotnet:2.2.0-aspnetcore-runtime AS base
# FROM mcr.microsoft.com/dotnet/core/runtime:2.2.5-alpine3.9

WORKDIR /app
COPY .publish/W /app

ENTRYPOINT ["dotnet", "MyWeb.dll"]