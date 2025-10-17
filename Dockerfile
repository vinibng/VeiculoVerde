# -------------------------------------------------------------------------
# STAGE 1: BUILD (SDK) - Prepara e compila os projetos
# -------------------------------------------------------------------------
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copia o arquivo da solu��o e os projetos essenciais para o cache do Docker.
# Ajustamos o COPY para ser mais expl�cito.
COPY VeiculoVerde.sln . 
COPY VeiculoVerde.Api/*.csproj ./VeiculoVerde.Api/
COPY VeiculoVerde.Application/*.csproj ./VeiculoVerde.Application/
COPY VeiculoVerde.Domain/*.csproj ./VeiculoVerde.Domain/
COPY VeiculoVerde.Infrastructure/*.csproj ./VeiculoVerde.Infrastructure/
COPY VeiculoVerde.Tests/*.csproj ./VeiculoVerde.Tests/

# CORRE��O MSB1011: Restaura depend�ncias, ESPECIFICANDO o arquivo de solu��o.
# Isso resolve o erro de ambiguidade e restaura as depend�ncias para todos os projetos.
RUN dotnet restore VeiculoVerde.sln

# Copia todo o c�digo-fonte restante
COPY . .

# -------------------------------------------------------------------------
# STAGE 2: TEST - Executa os testes unit�rios (SEUS REQUISITOS)
# -------------------------------------------------------------------------
FROM build AS test
WORKDIR /src
# A pasta do projeto de testes � VeiculoVerde.Tests
# Executa os testes. Se falhar, o build para aqui, como desejado.
RUN dotnet test VeiculoVerde.Tests/VeiculoVerde.Tests.csproj --no-restore --verbosity normal

# -------------------------------------------------------------------------
# STAGE 3: PUBLISH (BUILD) - Cria os artefatos de deploy
# -------------------------------------------------------------------------
FROM build AS publish
WORKDIR /src
# Publica apenas o projeto da API
RUN dotnet publish VeiculoVerde.Api/VeiculoVerde.Api.csproj -c Release -o /app/publish

# -------------------------------------------------------------------------
# STAGE 4: RUNTIME (FINAL) - Imagem final, mais leve
# -------------------------------------------------------------------------
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

# Copia o resultado da publica��o da etapa 'publish'
COPY --from=publish /app/publish .

# Define a porta de exposi��o no cont�iner
ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080

# Comando de entrada
ENTRYPOINT ["dotnet", "VeiculoVerde.Api.dll"]
