# syntax=docker/dockerfile:1
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build-env
WORKDIR /ToDoApi

# Copy csproj and restore as distinct layers
COPY *.csproj ./
RUN dotnet restore
    
# Copy everything else and build
COPY ../ToDoApi.Data ./
COPY ../ToDoApi.Services ./
RUN dotnet publish -c Release -o out
    
# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /ToDoApi
COPY --from=build-env /app/out .
ENTRYPOINT ["dotnet", "aspnetapp.dll"]