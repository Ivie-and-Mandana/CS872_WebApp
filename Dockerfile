#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443


FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["CS872_Web.csproj", "."]
RUN dotnet restore "./WebApplication5.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "CS872_Web.csproj" -c Release -o /app/build

ENV DB2_CLI_DRIVER_INSTALL_PATH="/app/bin/Debug/net5.0/clidriver/"
ENV LD_LIBRARY_PATH="/app/clidriver/lib" 
ENV PATH=$PATH:"/app/clidriver/bin:/app/clidriver/lib"
RUN apt-get update; \ apt-get install -y libxml2-dev;

FROM build AS publish
RUN dotnet publish "CS872_Web.csproj" -c Release -o /app/publish


FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CS872_Web.dll"]