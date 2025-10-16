# Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY *.sln ./
COPY VeiculoVerde.Api/*.csproj ./VeiculoVerde.Api/
COPY VeiculoVerde.Application/*.csproj ./VeiculoVerde.Application/
COPY VeiculoVerde.Domain/*.csproj ./VeiculoVerde.Domain/
COPY VeiculoVerde.Infrastructure/*.csproj ./VeiculoVerde.Infrastructure/
COPY VeiculoVerde.Tests/*.csproj ./VeiculoVerde.Tests/

RUN dotnet restore

COPY . .
WORKDIR /src/VeiculoVerde.Api
RUN dotnet publish -c Release -o /app/publish

# Runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .

ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080

ENTRYPOINT ["dotnet", "VeiculoVerde.Api.dll"]
