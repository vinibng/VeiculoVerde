// VeiculoVerde.Application/Interfaces/IVeiculoSubstituidoService.cs
using VeiculoVerde.Application.DTOs;
using System.Threading.Tasks;

namespace VeiculoVerde.Application.Interfaces
{
    public interface IVeiculoSubstituidoService
    {
        Task<VeiculoSubstituidoDTO> RegistrarSubstituicaoAsync(VeiculoSubstituidoDTO dto);
        Task<VeiculoSubstituidoDTO> GetVeiculoSubstituidoByIdAsync(int id);
        Task<PaginatedResultDTO<VeiculoSubstituidoDTO>> GetVeiculosSubstituidosPaginatedAsync(int pageNumber, int pageSize);
    }
}
