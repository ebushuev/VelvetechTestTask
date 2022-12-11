#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["TodoApiDTO.WebApi/TodoApiDTO.WebApi.csproj", "TodoApiDTO.WebApi/"]
RUN dotnet restore "TodoApiDTO.WebApi/TodoApiDTO.WebApi.csproj"
COPY . .
WORKDIR "/src/TodoApiDTO.WebApi"
RUN dotnet build "TodoApiDTO.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TodoApiDTO.WebApi.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TodoApiDTO.WebApi.dll"]