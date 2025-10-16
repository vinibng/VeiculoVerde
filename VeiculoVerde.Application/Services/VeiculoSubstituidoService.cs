// VeiculoVerde.Application/Services/VeiculoSubstituidoService.cs
using AutoMapper;
using VeiculoVerde.Application.DTOs;
using VeiculoVerde.Application.Interfaces;
using VeiculoVerde.Domain.Entities;
using VeiculoVerde.Domain.Interfaces;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

namespace VeiculoVerde.Application.Services
{
    public class VeiculoSubstituidoService : IVeiculoSubstituidoService
    {
        private readonly IVeiculoSubstituidoRepository _veiculoRepository;
        private readonly IAnaliseImpactoService _analiseImpactoService; // Para integração
        private readonly IMapper _mapper; // Usaremos AutoMapper para simplificar o mapeamento

        public VeiculoSubstituidoService(IVeiculoSubstituidoRepository veiculoRepository, IAnaliseImpactoService analiseImpactoService, IMapper mapper)
        {
            _veiculoRepository = veiculoRepository;
            _analiseImpactoService = analiseImpactoService;
            _mapper = mapper;
        }

        public async Task<VeiculoSubstituidoDTO> RegistrarSubstituicaoAsync(VeiculoSubstituidoDTO dto)
        {
            

            var veiculo = _mapper.Map<VeiculoSubstituido>(dto);
            await _veiculoRepository.AddAsync(veiculo);

           
            await _analiseImpactoService.CalcularImpactoAmbientalPorRegiaoAsync(veiculo.CEP, veiculo.Cidade, veiculo.Estado);

            return _mapper.Map<VeiculoSubstituidoDTO>(veiculo);
        }

        public async Task<VeiculoSubstituidoDTO> GetVeiculoSubstituidoByIdAsync(int id)
        {
            var veiculo = await _veiculoRepository.GetByIdAsync(id);
            return _mapper.Map<VeiculoSubstituidoDTO>(veiculo);
        }

        public async Task<PaginatedResultDTO<VeiculoSubstituidoDTO>> GetVeiculosSubstituidosPaginatedAsync(int pageNumber, int pageSize)
        {
            var (items, totalCount) = await _veiculoRepository.GetPaginatedAsync(pageNumber, pageSize);
            var dtos = _mapper.Map<IEnumerable<VeiculoSubstituidoDTO>>(items);

            return new PaginatedResultDTO<VeiculoSubstituidoDTO>
            {
                Items = dtos,
                TotalCount = totalCount,
                PageNumber = pageNumber,
                PageSize = pageSize
            };
        }
    }
}