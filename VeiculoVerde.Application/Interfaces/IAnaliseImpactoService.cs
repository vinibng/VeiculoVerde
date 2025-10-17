using VeiculoVerde.Application.DTOs;
using System.Threading.Tasks;

namespace VeiculoVerde.Application.Interfaces
{
    public interface IAnaliseImpactoService
    {
        // CORRIGIDO: O tipo de retorno agora é ImpactoAmbientalResponseDTO
        Task<ImpactoAmbientalResponseDTO> CalcularImpactoAmbientalPorRegiaoAsync(string cep, string cidade, string estado);

        // Mantido o DTO original para paginação, se for o caso
        Task<PaginatedResultDTO<ImpactoAmbientalDTO>> GetImpactosAmbientaisPaginatedAsync(int pageNumber, int pageSize, string? cep = null, string? cidade = null, string? estado = null);
    }
}
