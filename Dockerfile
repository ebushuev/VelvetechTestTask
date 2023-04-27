FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["TodoApiDTO.csproj", "./"]
RUN dotnet restore "TodoApiDTO.csproj"
COPY . .
WORKDIR "/src/"
RUN dotnet build "TodoApiDTO.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TodoApiDTO.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TodoApiDTO.dll"]
