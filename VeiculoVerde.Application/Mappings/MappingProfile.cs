using AutoMapper;
using VeiculoVerde.Application.DTOs;
using VeiculoVerde.Domain.Entities;

namespace VeiculoVerde.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Mapeamento para Registro (Entrada de Dados/Input DTO)
            CreateMap<VeiculoSubstituido, VeiculoSubstituidoDTO>().ReverseMap();

            // NOVO: Mapeamento para Resposta/Listagem (Output DTO) de VeiculoSubstituido
            // Garante que todos os campos necessários para exibição estão incluídos.
            CreateMap<VeiculoSubstituido, VeiculoSubstituidoResponseDTO>().ReverseMap();

            // --- Mapeamentos de Impacto Ambiental ---

            // Mapeamento de Impacto para DTO de Entrada/Input
            CreateMap<ImpactoAmbiental, ImpactoAmbientalDTO>().ReverseMap();

            // CORREÇÃO: Mapeamento FALTANTE que causou o erro (ImpactoAmbiental -> ImpactoAmbientalResponseDTO)
            // Permite que o Controller retorne o DTO de Resposta corretamente.
            CreateMap<ImpactoAmbiental, ImpactoAmbientalResponseDTO>();
        }
    }
}
