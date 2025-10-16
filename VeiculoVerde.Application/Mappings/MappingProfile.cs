// VeiculoVerde.Application/Mappings/MappingProfile.cs
using AutoMapper;
using VeiculoVerde.Application.DTOs;
using VeiculoVerde.Domain.Entities;

namespace VeiculoVerde.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
           
            CreateMap<VeiculoSubstituido, VeiculoSubstituidoDTO>().ReverseMap(); 
            CreateMap<ImpactoAmbiental, ImpactoAmbientalDTO>().ReverseMap();
        }
    }
}