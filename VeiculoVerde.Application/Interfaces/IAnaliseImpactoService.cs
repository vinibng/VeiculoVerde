// VeiculoVerde.Application/Interfaces/IAnaliseImpactoService.cs
using VeiculoVerde.Application.DTOs;
using System.Threading.Tasks;

namespace VeiculoVerde.Application.Interfaces
{
    public interface IAnaliseImpactoService
    {
        Task<ImpactoAmbientalDTO> CalcularImpactoAmbientalPorRegiaoAsync(string cep, string cidade, string estado);
        Task<PaginatedResultDTO<ImpactoAmbientalDTO>> GetImpactosAmbientaisPaginatedAsync(int pageNumber, int pageSize, string? cep = null, string? cidade = null, string? estado = null);
    }
}
