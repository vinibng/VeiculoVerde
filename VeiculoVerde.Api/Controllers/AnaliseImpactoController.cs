// VeiculoVerde.Api/Controllers/AnaliseImpactoController.cs
using Microsoft.AspNetCore.Mvc;
using VeiculoVerde.Application.DTOs;
using VeiculoVerde.Application.Interfaces;
using System.Threading.Tasks;
using System.Net;

namespace VeiculoVerde.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AnaliseImpactoController : ControllerBase
    {
        private readonly IAnaliseImpactoService _analiseImpactoService;

        public AnaliseImpactoController(IAnaliseImpactoService analiseImpactoService)
        {
            _analiseImpactoService = analiseImpactoService;
        }

        /// <summary>
        /// Calcula e retorna o impacto ambiental agregado por uma região específica.
        /// </summary>
        /// <param name="cep">CEP para filtragem (opcional).</param>
        /// <param name="cidade">Cidade para filtragem (opcional).</param>
        /// <param name="estado">Estado para filtragem (opcional).</param>
        /// <returns>O impacto ambiental agregado para a região.</returns>
        [HttpGet("por-regiao")]
        [ProducesResponseType(typeof(ImpactoAmbientalDTO), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetImpactoPorRegiao([FromQuery] string? cep, [FromQuery] string? cidade, [FromQuery] string? estado)
        {
            var impacto = await _analiseImpactoService.CalcularImpactoAmbientalPorRegiaoAsync(cep, cidade, estado);
            if (impacto == null)
            {
                return NotFound("Nenhum impacto encontrado para a região especificada.");
            }
            return Ok(impacto);
        }

        /// <summary>
        /// Lista os impactos ambientais registrados com paginação e filtros.
        /// </summary>
        /// <param name="pageNumber">Número da página (padrão: 1).</param>
        /// <param name="pageSize">Tamanho da página (padrão: 10).</param>
        /// <param name="cep">CEP para filtragem (opcional).</param>
        /// <param name="cidade">Cidade para filtragem (opcional).</param>
        /// <param name="estado">Estado para filtragem (opcional).</param>
        /// <returns>Lista paginada de impactos ambientais.</returns>
        [HttpGet("lista-paginada")]
        [ProducesResponseType(typeof(PaginatedResultDTO<ImpactoAmbientalDTO>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetImpactosPaginated([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10, [FromQuery] string? cep = null, [FromQuery] string? cidade = null, [FromQuery] string? estado = null)
        {
            var result = await _analiseImpactoService.GetImpactosAmbientaisPaginatedAsync(pageNumber, pageSize, cep, cidade, estado);
            return Ok(result);
        }
    }
}
