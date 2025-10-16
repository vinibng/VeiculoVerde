// VeiculoVerde.Api/Controllers/IncentivosController.cs
using Microsoft.AspNetCore.Mvc;
using VeiculoVerde.Application.DTOs;
using VeiculoVerde.Application.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net;

namespace VeiculoVerde.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class IncentivosController : ControllerBase
    {
        private readonly ISugestaoIncentivoService _sugestaoService;

        public IncentivosController(ISugestaoIncentivoService sugestaoService)
        {
            _sugestaoService = sugestaoService;
        }

        /// <summary>
        /// Sugere incentivos com base no tipo de veículo substituto e no estado.
        /// </summary>
        /// <param name="tipoVeiculoSubstituto">Tipo do veículo substituto (opcional, ex: CarroEletrico, BicicletaEletrica).</param>
        /// <param name="estado">Estado para o qual buscar incentivos (opcional, ex: SP, RJ).</param>
        /// <returns>Lista de incentivos sugeridos.</returns>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<SugestaoIncentivoDTO>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetSugestoes([FromQuery] string? tipoVeiculoSubstituto, [FromQuery] string? estado)
        {
            var sugestoes = await _sugestaoService.SugerirIncentivosAsync(tipoVeiculoSubstituto, estado);
            return Ok(sugestoes);
        }
    }
}