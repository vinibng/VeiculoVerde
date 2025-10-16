// VeiculoVerde.Application/Interfaces/IProjecaoService.cs
using VeiculoVerde.Application.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace VeiculoVerde.Application.Interfaces
{
    public interface IProjecaoService
    {
        Task<IEnumerable<ProjecaoDTO>> ProjetarSubstituicoesFuturasAsync(int anosParaFrente);
    }
}
