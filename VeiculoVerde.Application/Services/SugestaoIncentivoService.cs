// VeiculoVerde.Application/Services/SugestaoIncentivoService.cs
using VeiculoVerde.Application.DTOs;
using VeiculoVerde.Application.Interfaces;
using VeiculoVerde.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace VeiculoVerde.Application.Services
{
    public class SugestaoIncentivoService : ISugestaoIncentivoService
    {
       
        private readonly List<SugestaoIncentivoDTO> _incentivosDisponiveis = new List<SugestaoIncentivoDTO>
        {
            new SugestaoIncentivoDTO
            {
                TipoIncentivo = "Desconto IPVA",
                Descricao = "Redução ou isenção do IPVA para veículos elétricos.",
                LinkMaisInformacoes = "https://example.com/ipva-eletrico",
                Elegibilidade = "Veículos elétricos, varia por estado.",
                ValorEstimadoBeneficio = 500.00m 
            },
            new SugestaoIncentivoDTO
            {
                TipoIncentivo = "Isenção de Rodízio",
                Descricao = "Veículos elétricos geralmente são isentos de rodízio em grandes cidades.",
                LinkMaisInformacoes = "https://example.com/rodizio-eletrico",
                Elegibilidade = "Veículos elétricos, válido em algumas cidades (ex: São Paulo).",
                ValorEstimadoBeneficio = 0m 
            },
            new SugestaoIncentivoDTO
            {
                TipoIncentivo = "Linhas de Crédito Especiais",
                Descricao = "Bancos oferecem linhas de crédito com juros menores para a compra de veículos elétricos e bicicletas.",
                LinkMaisInformacoes = "https://example.com/credito-verde",
                Elegibilidade = "Pessoas físicas e jurídicas que compram veículos elétricos/bicicletas.",
                ValorEstimadoBeneficio = 0m 
            },
            new SugestaoIncentivoDTO
            {
                TipoIncentivo = "Subsídio para Bicicletas Elétricas",
                Descricao = "Alguns municípios oferecem subsídios para a compra de bicicletas elétricas.",
                LinkMaisInformacoes = "https://example.com/subsidio-bike",
                Elegibilidade = "Moradores de cidades específicas.",
                ValorEstimadoBeneficio = 1000.00m
            },
            new SugestaoIncentivoDTO
            {
                TipoIncentivo = "Redução IPI",
                Descricao = "Redução do Imposto sobre Produtos Industrializados para veículos elétricos/híbridos.",
                LinkMaisInformacoes = "https://example.com/reducao-ipi",
                Elegibilidade = "Veículos elétricos/híbridos, nível federal.",
                ValorEstimadoBeneficio = 2000.00m
            }
        };

        public Task<IEnumerable<SugestaoIncentivoDTO>> SugerirIncentivosAsync(string tipoVeiculoSubstituto, string estado)
        {
            var incentivosFiltrados = _incentivosDisponiveis.AsEnumerable();

            if (!string.IsNullOrEmpty(tipoVeiculoSubstituto))
            {
                if (tipoVeiculoSubstituto.Contains("Eletrico") || tipoVeiculoSubstituto.Contains("CarroEletrico") || tipoVeiculoSubstituto.Contains("MotoEletrica"))
                {
                    incentivosFiltrados = incentivosFiltrados.Where(i => i.Elegibilidade.Contains("elétricos") || i.Elegibilidade.Contains("híbridos"));
                }
                else if (tipoVeiculoSubstituto.Contains("Bicicleta"))
                {
                    incentivosFiltrados = incentivosFiltrados.Where(i => i.Elegibilidade.Contains("bicicletas"));
                }
            }

            if (!string.IsNullOrEmpty(estado))
            {
               
                incentivosFiltrados = incentivosFiltrados.Where(i =>
                    !i.Elegibilidade.Contains("varia por estado") || i.Elegibilidade.Contains(estado) || i.Elegibilidade.Contains("geralmente")
                );
            }

            return Task.FromResult(incentivosFiltrados);
        }
    }
}
