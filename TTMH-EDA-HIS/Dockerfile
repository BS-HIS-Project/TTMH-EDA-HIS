#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["TTMH-EDA-HIS/TTMH-EDA-HIS.csproj", "TTMH-EDA-HIS/"]
RUN dotnet restore "TTMH-EDA-HIS/TTMH-EDA-HIS.csproj"
COPY . .
WORKDIR "/src/TTMH-EDA-HIS"
RUN dotnet build "TTMH-EDA-HIS.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TTMH-EDA-HIS.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TTMH-EDA-HIS.dll"]