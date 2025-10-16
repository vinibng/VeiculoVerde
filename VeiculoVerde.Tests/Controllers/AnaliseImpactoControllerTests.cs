// VeiculoVerde.Tests/Controllers/AnaliseImpactoControllerTests.cs
using Xunit;
using Moq;
using VeiculoVerde.Api.Controllers;
using VeiculoVerde.Application.Interfaces;
using VeiculoVerde.Application.DTOs;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

namespace VeiculoVerde.Tests.Controllers
{
    public class AnaliseImpactoControllerTests
    {
        private readonly Mock<IAnaliseImpactoService> _mockAnaliseImpactoService;
        private readonly AnaliseImpactoController _controller;

        public AnaliseImpactoControllerTests()
        {
            _mockAnaliseImpactoService = new Mock<IAnaliseImpactoService>();
            _controller = new AnaliseImpactoController(_mockAnaliseImpactoService.Object);
        }

        [Fact]
        public async Task GetImpactoPorRegiao_ExistingImpact_ReturnsOk200()
        {
            // Arrange
            var cep = "01000000";
            var cidade = "São Paulo";
            var estado = "SP";
            var impactoDto = new ImpactoAmbientalDTO
            {
                CEP = cep,
                Cidade = cidade,
                Estado = estado,
                ReducaoCO2TotalKg = 1000,
                EconomiaCombustivelTotalLitros = 500,
                NumeroSubstituicoes = 10,
                DataAnalise = System.DateTime.Now
            };

            _mockAnaliseImpactoService.Setup(s => s.CalcularImpactoAmbientalPorRegiaoAsync(cep, cidade, estado))
                                      .ReturnsAsync(impactoDto);

            // Act
            var result = await _controller.GetImpactoPorRegiao(cep, cidade, estado);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal((int)HttpStatusCode.OK, okResult.StatusCode);
            var returnedValue = Assert.IsType<ImpactoAmbientalDTO>(okResult.Value);
            Assert.Equal(cep, returnedValue.CEP);
        }

        [Fact]
        public async Task GetImpactoPorRegiao_NoImpact_ReturnsNotFound404()
        {
            // Arrange
            var cep = "99999999";
            _mockAnaliseImpactoService.Setup(s => s.CalcularImpactoAmbientalPorRegiaoAsync(cep, null, null))
                                      .ReturnsAsync((ImpactoAmbientalDTO)null);

            // Act
            var result = await _controller.GetImpactoPorRegiao(cep, null, null);

            // Assert
            Assert.IsType<NotFoundObjectResult>(result);
            var notFoundResult = (NotFoundObjectResult)result;
            Assert.Equal((int)HttpStatusCode.NotFound, notFoundResult.StatusCode);
        }

        [Fact]
        public async Task GetImpactosPaginated_ReturnsOk200_WithPaginatedResult()
        {
            // Arrange
            var pageNumber = 1;
            var pageSize = 10;
            var mockItems = new List<ImpactoAmbientalDTO>
            {
                new ImpactoAmbientalDTO { CEP = "01000000", Cidade = "São Paulo", Estado = "SP", ReducaoCO2TotalKg = 100 },
                new ImpactoAmbientalDTO { CEP = "02000000", Cidade = "Rio de Janeiro", Estado = "RJ", ReducaoCO2TotalKg = 200 }
            };
            var paginatedResult = new PaginatedResultDTO<ImpactoAmbientalDTO>
            {
                Items = mockItems,
                TotalCount = 2,
                PageNumber = pageNumber,
                PageSize = pageSize
            };

            _mockAnaliseImpactoService.Setup(s => s.GetImpactosAmbientaisPaginatedAsync(pageNumber, pageSize, null, null, null))
                                      .ReturnsAsync(paginatedResult);

            // Act
            var result = await _controller.GetImpactosPaginated(pageNumber, pageSize, null, null, null);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal((int)HttpStatusCode.OK, okResult.StatusCode);
            var returnedValue = Assert.IsType<PaginatedResultDTO<ImpactoAmbientalDTO>>(okResult.Value);
            Assert.Equal(2, returnedValue.TotalCount);
            Assert.Equal(2, returnedValue.Items.Count());
        }
    }
}