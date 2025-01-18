FROM mcr.microsoft.com/dotnet/sdk:8.0.101 AS build-env
WORKDIR /app

# Build
COPY . ./
RUN dotnet restore
RUN dotnet publish -c Release -o out

# Build image
FROM mcr.microsoft.com/dotnet/aspnet:8.0.1
WORKDIR /app
COPY --from=build-env /app/out .
ENV ASPNETCORE_ENVIRONMENT=Development
ENTRYPOINT ["dotnet", "RavanhaniMovies.Web.dll"]