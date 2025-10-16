// VeiculoVerde.Api/Controllers/VeiculoSubstituicaoController.cs
using Microsoft.AspNetCore.Mvc;
using VeiculoVerde.Application.DTOs;
using VeiculoVerde.Application.Interfaces;
using System.Threading.Tasks;
using System.Net;
using Microsoft.AspNetCore.Authorization; // Para autenticação/autorização

namespace VeiculoVerde.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VeiculoSubstituicaoController : ControllerBase
    {
        private readonly IVeiculoSubstituidoService _veiculoService;

        public VeiculoSubstituicaoController(IVeiculoSubstituidoService veiculoService)
        {
            _veiculoService = veiculoService;
        }

        /// <summary>
        /// Registra a substituição de um veículo a combustão por um elétrico ou bicicleta.
        /// </summary>
        /// <param name="dto">Dados do veículo substituído.</param>
        /// <returns>O veículo substituído registrado.</returns>
        [HttpPost]
        [ProducesResponseType(typeof(VeiculoSubstituidoDTO), (int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        // [Authorize(Roles = "Admin, Editor")] // Exemplo de autorização
        public async Task<IActionResult> Post([FromBody] VeiculoSubstituidoDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var veiculoRegistrado = await _veiculoService.RegistrarSubstituicaoAsync(dto);
                return StatusCode((int)HttpStatusCode.Created, veiculoRegistrado);
            }
            catch (System.Exception ex)
            {
                // Logar a exceção
                return StatusCode((int)HttpStatusCode.InternalServerError, "Ocorreu um erro ao registrar a substituição: " + ex.Message);
            }
        }

        /// <summary>
        /// Obtém um veículo substituído pelo seu ID.
        /// </summary>
        /// <param name="id">ID do veículo substituído.</param>
        /// <returns>O veículo substituído.</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(VeiculoSubstituidoDTO), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            var veiculo = await _veiculoService.GetVeiculoSubstituidoByIdAsync(id);
            if (veiculo == null)
            {
                return NotFound();
            }
            return Ok(veiculo);
        }

        /// <summary>
        /// Lista veículos substituídos com paginação.
        /// </summary>
        /// <param name="pageNumber">Número da página (padrão: 1).</param>
        /// <param name="pageSize">Tamanho da página (padrão: 10).</param>
        /// <returns>Lista paginada de veículos substituídos.</returns>
        [HttpGet]
        [ProducesResponseType(typeof(PaginatedResultDTO<VeiculoSubstituidoDTO>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetPaginated([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var result = await _veiculoService.GetVeiculosSubstituidosPaginatedAsync(pageNumber, pageSize);
            return Ok(result);
        }
    }
}