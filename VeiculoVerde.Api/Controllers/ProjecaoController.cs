// VeiculoVerde.Api/Controllers/ProjecaoController.cs
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
    public class ProjecaoController : ControllerBase
    {
        private readonly IProjecaoService _projecaoService;

        public ProjecaoController(IProjecaoService projecaoService)
        {
            _projecaoService = projecaoService;
        }

        /// <summary>
        /// Projeta o número esperado de substituições futuras e seus impactos.
        /// </summary>
        /// <param name="anosParaFrente">Número de anos para os quais a projeção deve ser feita.</param>
        /// <returns>Lista de projeções anuais/mensais.</returns>
        [HttpGet("{anosParaFrente}")]
        [ProducesResponseType(typeof(IEnumerable<ProjecaoDTO>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetProjecao(int anosParaFrente)
        {
            if (anosParaFrente <= 0)
            {
                return BadRequest("O número de anos para a projeção deve ser maior que zero.");
            }

            var projecoes = await _projecaoService.ProjetarSubstituicoesFuturasAsync(anosParaFrente);
            return Ok(projecoes);
        }
    }
}