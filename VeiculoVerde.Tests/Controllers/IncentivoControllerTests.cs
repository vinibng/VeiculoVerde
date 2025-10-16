// VeiculoVerde.Tests/Controllers/IncentivosControllerTests.cs
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
    public class IncentivosControllerTests
    {
        private readonly Mock<ISugestaoIncentivoService> _mockSugestaoService;
        private readonly IncentivosController _controller;

        public IncentivosControllerTests()
        {
            _mockSugestaoService = new Mock<ISugestaoIncentivoService>();
            _controller = new IncentivosController(_mockSugestaoService.Object);
        }

        [Fact]
        public async Task GetSugestoes_ReturnsOk200_WithIncentives()
        {
            // Arrange
            var mockIncentivos = new List<SugestaoIncentivoDTO>
            {
                new SugestaoIncentivoDTO { TipoIncentivo = "Desconto IPVA", Descricao = "...", LinkMaisInformacoes = "...", Elegibilidade = "...", ValorEstimadoBeneficio = 500 },
                new SugestaoIncentivoDTO { TipoIncentivo = "Isenção de Rodízio", Descricao = "...", LinkMaisInformacoes = "...", Elegibilidade = "...", ValorEstimadoBeneficio = 0 }
            };

            _mockSugestaoService.Setup(s => s.SugerirIncentivosAsync(It.IsAny<string>(), It.IsAny<string>()))
                               .ReturnsAsync(mockIncentivos);

            // Act
            var result = await _controller.GetSugestoes("CarroEletrico", "SP");

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal((int)HttpStatusCode.OK, okResult.StatusCode);
            var returnedValue = Assert.IsType<List<SugestaoIncentivoDTO>>(okResult.Value);
            Assert.Equal(mockIncentivos.Count, returnedValue.Count);
        }

        [Fact]
        public async Task GetSugestoes_NoMatchingIncentives_ReturnsOk200_EmptyList()
        {
            // Arrange
            var mockIncentivos = new List<SugestaoIncentivoDTO>(); // Lista vazia

            _mockSugestaoService.Setup(s => s.SugerirIncentivosAsync(It.IsAny<string>(), It.IsAny<string>()))
                               .ReturnsAsync(mockIncentivos);

            // Act
            var result = await _controller.GetSugestoes("VeiculoInexistente", "XX");

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal((int)HttpStatusCode.OK, okResult.StatusCode);
            var returnedValue = Assert.IsType<List<SugestaoIncentivoDTO>>(okResult.Value);
            Assert.Empty(returnedValue);
        }
    }
}