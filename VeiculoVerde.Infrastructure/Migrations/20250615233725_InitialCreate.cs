using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VeiculoVerde.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ImpactosAmbientais",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CEP = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: false),
                    Cidade = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Estado = table.Column<string>(type: "nvarchar(2)", maxLength: 2, nullable: false),
                    ReducaoCO2TotalKg = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    EconomiaCombustivelTotalLitros = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    NumeroSubstituicoes = table.Column<int>(type: "int", nullable: false),
                    DataAnalise = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImpactosAmbientais", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "VeiculosSubstituidos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PlacaVeiculoCombustao = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    MarcaVeiculoCombustao = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ModeloVeiculoCombustao = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AnoFabricacaoVeiculoCombustao = table.Column<int>(type: "int", nullable: false),
                    EmissaoCO2Anterior = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ConsumoCombustivelAnteriorKmPorLitro = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TipoVeiculoSubstituto = table.Column<int>(type: "int", nullable: false),
                    MarcaVeiculoSubstituto = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ModeloVeiculoSubstituto = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmissaoCO2Nova = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    AutonomiaVeiculoSubstitutoKm = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DataSubstituicao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CEP = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: false),
                    Cidade = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Estado = table.Column<string>(type: "nvarchar(2)", maxLength: 2, nullable: false),
                    Pais = table.Column<string>(type: "nvarchar(max)", nullable: false)
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
