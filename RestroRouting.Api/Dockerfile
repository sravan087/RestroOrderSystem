#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["RestroRouting.Api/RestroRouting.Api.csproj", "RestroRouting.Api/"]
COPY ["RestroRouting.Service/RestroRouting.Service.csproj", "RestroRouting.Service/"]
COPY ["RestroRouting.Domain/RestroRouting.Domain.csproj", "RestroRouting.Domain/"]
COPY ["RestroRouting.Data/RestroRouting.Data.csproj", "RestroRouting.Data/"]
RUN dotnet restore "RestroRouting.Api/RestroRouting.Api.csproj"
COPY . .
WORKDIR "/src/RestroRouting.Api"
RUN dotnet build "RestroRouting.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "RestroRouting.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "RestroRouting.Api.dll"]