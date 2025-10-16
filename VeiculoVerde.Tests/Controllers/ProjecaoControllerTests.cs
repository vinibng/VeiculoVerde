// VeiculoVerde.Tests/Controllers/ProjecaoControllerTests.cs
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
    public class ProjecaoControllerTests
    {
        private readonly Mock<IProjecaoService> _mockProjecaoService;
        private readonly ProjecaoController _controller;

        public ProjecaoControllerTests()
        {
            _mockProjecaoService = new Mock<IProjecaoService>();
            _controller = new ProjecaoController(_mockProjecaoService.Object);
        }

        [Fact]
        public async Task GetProjecao_ValidYears_ReturnsOk200()
        {
            // Arrange
            var anosParaFrente = 2;
            var mockProjecoes = new List<ProjecaoDTO>
            {
                new ProjecaoDTO { Ano = 2026, Mes = 1, VeiculosProjetados = 10, ReducaoCO2ProjetadaKg = 500, EconomiaCombustivelProjetadaLitros = 200 },
                new ProjecaoDTO { Ano = 2026, Mes = 2, VeiculosProjetados = 12, ReducaoCO2ProjetadaKg = 600, EconomiaCombustivelProjetadaLitros = 250 }
            };

            _mockProjecaoService.Setup(s => s.ProjetarSubstituicoesFuturasAsync(anosParaFrente))
                               .ReturnsAsync(mockProjecoes);

            // Act
            var result = await _controller.GetProjecao(anosParaFrente);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal((int)HttpStatusCode.OK, okResult.StatusCode);
            var returnedValue = Assert.IsType<List<ProjecaoDTO>>(okResult.Value);
            Assert.Equal(mockProjecoes.Count, returnedValue.Count);
        }

        [Fact]
        public async Task GetProjecao_InvalidYears_ReturnsBadRequest400()
        {
            // Arrange
            var anosParaFrente = 0; // Cenário inválido

            // Act
            var result = await _controller.GetProjecao(anosParaFrente);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal((int)HttpStatusCode.BadRequest, badRequestResult.StatusCode);
            Assert.Equal("O número de anos para a projeção deve ser maior que zero.", badRequestResult.Value);
        }
    }
}