// VeiculoVerde.Tests/Controllers/VeiculoSubstituicaoControllerTests.cs
using Xunit;
using Moq;
using VeiculoVerde.Api.Controllers;
using VeiculoVerde.Application.Interfaces;
using VeiculoVerde.Application.DTOs;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;
using System.Collections.Generic;
using System;

namespace VeiculoVerde.Tests.Controllers
{
    public class VeiculoSubstituicaoControllerTests
    {
        private readonly Mock<IVeiculoSubstituidoService> _mockVeiculoService;
        private readonly VeiculoSubstituicaoController _controller;

        public VeiculoSubstituicaoControllerTests()
        {
            _mockVeiculoService = new Mock<IVeiculoSubstituidoService>();
            _controller = new VeiculoSubstituicaoController(_mockVeiculoService.Object);
        }

        [Fact]
        public async Task Post_ValidDto_ReturnsCreated201()
        {
            // Arrange
            var veiculoDto = new VeiculoSubstituidoDTO
            {
                PlacaVeiculoCombustao = "ABC1234",
                MarcaVeiculoCombustao = "Ford",
                ModeloVeiculoCombustao = "Ka",
                AnoFabricacaoVeiculoCombustao = 2015,
                EmissaoCO2Anterior = 150,
                ConsumoCombustivelAnteriorKmPorLitro = 12,
                TipoVeiculoSubstituto = Domain.Entities.TipoVeiculoSubstituto.CarroEletrico,
                MarcaVeiculoSubstituto = "Tesla",
                ModeloVeiculoSubstituto = "Model 3",
                EmissaoCO2Nova = 0,
                AutonomiaVeiculoSubstitutoKm = 400,
                DataSubstituicao = DateTime.Now,
                CEP = "01000000",
                Cidade = "São Paulo",
                Estado = "SP",
                Pais = "Brasil"
            };

            _mockVeiculoService.Setup(s => s.RegistrarSubstituicaoAsync(It.IsAny<VeiculoSubstituidoDTO>()))
                               .ReturnsAsync(veiculoDto); // Simula o retorno de um DTO registrado

            // Act
            var result = await _controller.Post(veiculoDto);

            // Assert
            var statusCodeResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal((int)HttpStatusCode.Created, statusCodeResult.StatusCode);
            Assert.IsType<VeiculoSubstituidoDTO>(statusCodeResult.Value);
        }

        [Fact]
        public async Task GetPaginated_ReturnsOk200_WithPaginatedResult()
        {
            // Arrange
            var pageNumber = 1;
            var pageSize = 10;
            var mockItems = new List<VeiculoSubstituidoDTO>
            {
                new VeiculoSubstituidoDTO { Id = 1, PlacaVeiculoCombustao = "ABC1234", TipoVeiculoSubstituto = Domain.Entities.TipoVeiculoSubstituto.CarroEletrico, DataSubstituicao = DateTime.Now, CEP = "01000000", Cidade = "São Paulo", Estado = "SP" },
                new VeiculoSubstituidoDTO { Id = 2, PlacaVeiculoCombustao = "DEF5678", TipoVeiculoSubstituto = Domain.Entities.TipoVeiculoSubstituto.BicicletaEletrica, DataSubstituicao = DateTime.Now, CEP = "02000000", Cidade = "Rio de Janeiro", Estado = "RJ" }
            };
            var paginatedResult = new PaginatedResultDTO<VeiculoSubstituidoDTO>
            {
                Items = mockItems,
                TotalCount = 2,
                PageNumber = pageNumber,
                PageSize = pageSize
            };

            _mockVeiculoService.Setup(s => s.GetVeiculosSubstituidosPaginatedAsync(pageNumber, pageSize))
                               .ReturnsAsync(paginatedResult);

            // Act
            var result = await _controller.GetPaginated(pageNumber, pageSize);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal((int)HttpStatusCode.OK, okResult.StatusCode);
            var returnedValue = Assert.IsType<PaginatedResultDTO<VeiculoSubstituidoDTO>>(okResult.Value);
            Assert.Equal(2, returnedValue.TotalCount);
            Assert.Equal(2, returnedValue.Items.Count());
        }

        [Fact]
        public async Task GetById_ExistingId_ReturnsOk200()
        {
            // Arrange
            var veiculoId = 1;
            var veiculoDto = new VeiculoSubstituidoDTO { Id = veiculoId, PlacaVeiculoCombustao = "XYZ9876", TipoVeiculoSubstituto = Domain.Entities.TipoVeiculoSubstituto.CarroEletrico, DataSubstituicao = DateTime.Now, CEP = "01000000", Cidade = "São Paulo", Estado = "SP" };

            _mockVeiculoService.Setup(s => s.GetVeiculoSubstituidoByIdAsync(veiculoId))
                               .ReturnsAsync(veiculoDto);

            // Act
            var result = await _controller.GetById(veiculoId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal((int)HttpStatusCode.OK, okResult.StatusCode);
            var returnedValue = Assert.IsType<VeiculoSubstituidoDTO>(okResult.Value);
            Assert.Equal(veiculoId, returnedValue.Id);
        }

        [Fact]
        public async Task GetById_NonExistingId_ReturnsNotFound404()
        {
            // Arrange
            var veiculoId = 999;
            _mockVeiculoService.Setup(s => s.GetVeiculoSubstituidoByIdAsync(veiculoId))
                               .ReturnsAsync((VeiculoSubstituidoDTO)null);

            // Act
            var result = await _controller.GetById(veiculoId);

            // Assert
            Assert.IsType<NotFoundResult>(result);
            var notFoundResult = (NotFoundResult)result;
            Assert.Equal((int)HttpStatusCode.NotFound, notFoundResult.StatusCode);
        }
    }
}