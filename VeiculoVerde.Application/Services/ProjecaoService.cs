// VeiculoVerde.Application/Services/ProjecaoService.cs
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VeiculoVerde.Application.DTOs;
using VeiculoVerde.Application.Interfaces;
using VeiculoVerde.Domain.Entities;
using VeiculoVerde.Domain.Interfaces;

namespace VeiculoVerde.Application.Services
{
    public class ProjecaoService : IProjecaoService
    {
        private readonly IVeiculoSubstituidoRepository _veiculoRepository;
        private readonly IMapper _mapper;

        public ProjecaoService(IVeiculoSubstituidoRepository veiculoRepository, IMapper mapper)
        {
            _veiculoRepository = veiculoRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ProjecaoDTO>> ProjetarSubstituicoesFuturasAsync(int anosParaFrente)
        {
            var todasSubstituicoes = await _veiculoRepository.GetAllAsync();

            if (!todasSubstituicoes.Any())
            {
                return new List<ProjecaoDTO>();
            }


            var dadosHistoricosAgrupados = todasSubstituicoes
                .GroupBy(s => new { s.DataSubstituicao.Year, s.DataSubstituicao.Month })
                .Select(g => new
                {
                    Ano = g.Key.Year,
                    Mes = g.Key.Month,
                    NumeroSubstituicoes = g.Count(),
                    ReducaoCO2Total = g.Sum(s => s.EmissaoCO2Anterior - s.EmissaoCO2Nova),
                    EconomiaCombustivelTotal = g.Sum(s =>
                    {
                        if (s.TipoVeiculoSubstituto == TipoVeiculoSubstituto.BicicletaComum ||
                            s.TipoVeiculoSubstituto == TipoVeiculoSubstituto.BicicletaEletrica ||
                            s.TipoVeiculoSubstituto == TipoVeiculoSubstituto.CarroEletrico ||
                            s.TipoVeiculoSubstituto == TipoVeiculoSubstituto.MotoEletrica)
                        {
                            const decimal KM_ANUAL_PADRAO = 15000m;
                            return (KM_ANUAL_PADRAO / s.ConsumoCombustivelAnteriorKmPorLitro);
                        }
                        return 0;
                    })
                })
                .OrderBy(g => g.Ano)
                .ThenBy(g => g.Mes)
                .ToList();

            if (!dadosHistoricosAgrupados.Any())
            {
                return new List<ProjecaoDTO>();
            }

            var projecoes = new List<ProjecaoDTO>();
            var ultimoPeriodo = dadosHistoricosAgrupados.Last();

           

            int mesesParaFrente = anosParaFrente * 12;

            for (int i = 1; i <= mesesParaFrente; i++)
            {
                DateTime dataProjecao = DateTime.UtcNow.AddMonths(i);
                ProjecaoDTO projecao = new ProjecaoDTO
                {
                    Ano = dataProjecao.Year,
                    Mes = dataProjecao.Month,
                    VeiculosProjetados = (int)(ultimoPeriodo.NumeroSubstituicoes * (1 + (double)i / 120.0)), // Crescimento de 10% ao ano
                    ReducaoCO2ProjetadaKg = ultimoPeriodo.ReducaoCO2Total * (1 + (decimal)i / 120m),
                    EconomiaCombustivelProjetadaLitros = ultimoPeriodo.EconomiaCombustivelTotal * (1 + (decimal)i / 120m)
                };
                projecoes.Add(projecao);
            }

            return projecoes;
        }
    }
}