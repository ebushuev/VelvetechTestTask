# backend - Asp.Net Core
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /src/backend/app
EXPOSE 5000
ENV ASPNETCORE_URLS=http://*:5000

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src/backend/src
COPY ["Todo.DataAccess/Todo.DataAccess.csproj", "Todo.DataAccess/"]
COPY ["Todo.Domain/Todo.Domain.csproj", "Todo.Domain/"]
COPY ["Todo.Services/Todo.Services.csproj", "Todo.Services/"]
COPY ["Todo.WebApi/Todo.WebApi.csproj", "Todo.WebApi/"]
RUN dotnet restore "Todo.WebApi/Todo.WebApi.csproj"
COPY . .
WORKDIR "/src/backend/src/Todo.WebApi"
RUN dotnet build "Todo.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Todo.WebApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /src/backend/app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Todo.WebApi.dll"]