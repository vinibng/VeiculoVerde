using AutoMapper;
using VeiculoVerde.Application.DTOs; // Assumindo o DTO de requisição
using VeiculoVerde.Domain.Entities; // Assumindo a Entidade de domínio
using System.Globalization;
using System;

namespace VeiculoVerde.Application.Mappings
{
    // Este perfil define as regras de mapeamento do AutoMapper.
    public class VeiculoSubstituicaoProfile : Profile
    {
        public VeiculoSubstituicaoProfile()
        {
            // Mapeamento CORRETO: do DTO de Requisição (VeiculoSubstituidoDTO) para a Entidade de Domínio.
            // GARANTINDO O NOME CORRETO: VeiculoSubstituicao (com 'o' no final)
            CreateMap<VeiculoSubstituidoDTO, VeiculoSubstituido>()
                // Removemos o mapeamento explícito de DataSubstituicao, pois ela já é DateTime 
                // no DTO de origem e será mapeada automaticamente.

                // Mapeamento da Entidade para o DTO de Resposta (Output).
                .ReverseMap();
        }
    }
}
