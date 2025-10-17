using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace VeiculoVerde.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialSetup : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ImpactosAmbientais",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CEP = table.Column<string>(type: "varchar(8)", nullable: false),
                    Cidade = table.Column<string>(type: "varchar(100)", nullable: false),
                    Estado = table.Column<string>(type: "varchar(2)", nullable: false),
                    ReducaoCO2TotalKg = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    EconomiaCombustivelTotalLitros = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    NumeroSubstituicoes = table.Column<int>(type: "integer", nullable: false),
                    DataAnalise = table.Column<DateTime>(type: "timestamp", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImpactosAmbientais", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "VeiculosSubstituidos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PlacaVeiculoCombustao = table.Column<string>(type: "varchar(10)", nullable: false),
                    MarcaVeiculoCombustao = table.Column<string>(type: "text", nullable: false),
                    ModeloVeiculoCombustao = table.Column<string>(type: "text", nullable: false),
                    AnoFabricacaoVeiculoCombustao = table.Column<int>(type: "integer", nullable: false),
                    EmissaoCO2Anterior = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    ConsumoCombustivelAnteriorKmPorLitro = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    TipoVeiculoSubstituto = table.Column<int>(type: "integer", nullable: false),
                    MarcaVeiculoSubstituto = table.Column<string>(type: "text", nullable: false),
                    ModeloVeiculoSubstituto = table.Column<string>(type: "text", nullable: false),
                    EmissaoCO2Nova = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    AutonomiaVeiculoSubstitutoKm = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    DataSubstituicao = table.Column<DateTime>(type: "timestamp", nullable: false),
                    CEP = table.Column<string>(type: "varchar(8)", nullable: false),
                    Cidade = table.Column<string>(type: "varchar(100)", nullable: false),
                    Estado = table.Column<string>(type: "varchar(2)", nullable: false),
                    Pais = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VeiculosSubstituidos", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ImpactosAmbientais");

            migrationBuilder.DropTable(
                name: "VeiculosSubstituidos");
        }
    }
}
