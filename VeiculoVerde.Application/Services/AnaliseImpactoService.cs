using AutoMapper;
using VeiculoVerde.Application.DTOs;
using VeiculoVerde.Application.Interfaces;
using VeiculoVerde.Domain.Entities;
using VeiculoVerde.Domain.Interfaces;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace VeiculoVerde.Application.Services
{
    public class AnaliseImpactoService : IAnaliseImpactoService
    {
        private readonly IVeiculoSubstituidoRepository _veiculoRepository;
        private readonly IImpactoAmbientalRepository _impactoRepository;
        private readonly IMapper _mapper;

        public AnaliseImpactoService(IVeiculoSubstituidoRepository veiculoRepository, IImpactoAmbientalRepository impactoRepository, IMapper mapper)
        {
            _veiculoRepository = veiculoRepository;
            _impactoRepository = impactoRepository;
            _mapper = mapper;
        }

        // ALTERADO O TIPO DE RETORNO para ImpactoAmbientalResponseDTO
        public async Task<ImpactoAmbientalResponseDTO> CalcularImpactoAmbientalPorRegiaoAsync(string cep, string cidade, string estado)
        {
            // Busca todos os veículos substituídos na região
            var veiculosNaRegiao = await _veiculoRepository.FindAsync(v =>
                (string.IsNullOrEmpty(cep) || v.CEP == cep) &&
                (string.IsNullOrEmpty(cidade) || v.Cidade == cidade) &&
                (string.IsNullOrEmpty(estado) || v.Estado == estado)
            );

            if (!veiculosNaRegiao.Any())
            {
                // ALTERADO: Retorna null ou um objeto vazio se não houver veículos
                return null;
            }

            // CÁLCULOS AGREGADOS
            decimal reducaoCO2TotalKg = veiculosNaRegiao.Sum(v => v.EmissaoCO2Anterior - v.EmissaoCO2Nova);
            decimal economiaCombustivelTotalLitros = veiculosNaRegiao.Sum(v =>
            {
                // Lógica de cálculo de economia de combustível
                if (v.TipoVeiculoSubstituto == TipoVeiculoSubstituto.BicicletaComum ||
                    v.TipoVeiculoSubstituto == TipoVeiculoSubstituto.BicicletaEletrica ||
                    v.TipoVeiculoSubstituto == TipoVeiculoSubstituto.CarroEletrico ||
                    v.TipoVeiculoSubstituto == TipoVeiculoSubstituto.MotoEletrica)
                {
                    const decimal KM_ANUAL_PADRAO = 15000m;
                    // Certifique-se de que ConsumoCombustivelAnteriorKmPorLitro não é zero
                    return v.ConsumoCombustivelAnteriorKmPorLitro > 0 ? (KM_ANUAL_PADRAO / v.ConsumoCombustivelAnteriorKmPorLitro) : 0m;
                }
                return 0m;
            });


            // BUSCA E ATUALIZA O REGISTRO DE IMPACTO
            var impactoExistente = (await _impactoRepository.FindAsync(i =>
                (string.IsNullOrEmpty(cep) || i.CEP == cep) &&
                (string.IsNullOrEmpty(cidade) || i.Cidade == cidade) &&
                (string.IsNullOrEmpty(estado) || i.Estado == estado)
            )).FirstOrDefault();


            if (impactoExistente == null)
            {
                impactoExistente = new ImpactoAmbiental
                {
                    CEP = cep,
                    Cidade = cidade,
                    Estado = estado,
                    ReducaoCO2TotalKg = reducaoCO2TotalKg,
                    EconomiaCombustivelTotalLitros = economiaCombustivelTotalLitros,
                    NumeroSubstituicoes = veiculosNaRegiao.Count(),
                    DataAnalise = DateTime.UtcNow
                };
                await _impactoRepository.AddAsync(impactoExistente);
            }
            else
            {
                impactoExistente.ReducaoCO2TotalKg = reducaoCO2TotalKg;
                impactoExistente.EconomiaCombustivelTotalLitros = economiaCombustivelTotalLitros;
                impactoExistente.NumeroSubstituicoes = veiculosNaRegiao.Count();
                impactoExistente.DataAnalise = DateTime.UtcNow;
                await _impactoRepository.UpdateAsync(impactoExistente);
            }

            // Mapeia o Impacto (agregado)
            var resultadoDto = _mapper.Map<ImpactoAmbientalResponseDTO>(impactoExistente);

            // NOVO: Mapeia e anexa a lista detalhada de veículos ao DTO de Resposta
            resultadoDto.VeiculosSubstituidos = _mapper.Map<IEnumerable<VeiculoSubstituidoResponseDTO>>(veiculosNaRegiao);

            return resultadoDto;
        }

        // MÉTODOS DE PAGINAÇÃO NÃO PRECISAM DE ALTERAÇÃO, MAS CERTIFIQUE-SE DE QUE ESTÃO 
        // RETORNANDO A ImpactoAmbientalResponseDTO SE FOR USADO NA API DE LISTAGEM GERAL
        public async Task<PaginatedResultDTO<ImpactoAmbientalDTO>> GetImpactosAmbientaisPaginatedAsync(int pageNumber, int pageSize, string? cep = null, string? cidade = null, string? estado = null)
        {
            var query = await _impactoRepository.GetAllAsync();

            if (!string.IsNullOrEmpty(cep))
            {
                query = query.Where(i => i.CEP == cep);
            }
            if (!string.IsNullOrEmpty(cidade))
            {
                query = query.Where(i => i.Cidade == cidade);
            }
            if (!string.IsNullOrEmpty(estado))
            {
                query = query.Where(i => i.Estado == estado);
            }

            var totalCount = query.Count();
            var items = query.Skip((pageNumber - 1) * pageSize)
                             .Take(pageSize)
                             .ToList();

            var dtos = _mapper.Map<IEnumerable<ImpactoAmbientalDTO>>(items);

            return new PaginatedResultDTO<ImpactoAmbientalDTO>
            {
                Items = dtos,
                TotalCount = totalCount,
                PageNumber = pageNumber,
                PageSize = pageSize
            };
        }
    }
}
