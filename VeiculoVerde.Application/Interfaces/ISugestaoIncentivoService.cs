// VeiculoVerde.Application/Interfaces/ISugestaoIncentivoService.cs
using VeiculoVerde.Application.DTOs;
using VeiculoVerde.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace VeiculoVerde.Application.Interfaces
{
    public interface ISugestaoIncentivoService
    {
        Task<IEnumerable<SugestaoIncentivoDTO>> SugerirIncentivosAsync(string tipoVeiculoSubstituto, string estado);
    }
}
