using System;
using VeiculoVerde.Domain.Entities;

namespace VeiculoVerde.Application.DTOs
{
    // DTO otimizado para o consumo (listagem/leitura) da API
    public class VeiculoSubstituidoResponseDTO
    {
        public int Id { get; set; }

        // Dados de Localização (que já estão funcionando)
        public string CEP { get; set; }
        public string Cidade { get; set; }
        public string Estado { get; set; }
        public string Pais { get; set; }

        // Dados do Veículo de Combustão (que estavam faltando na exibição)
        public string PlacaVeiculoCombustao { get; set; }
        public string MarcaVeiculoCombustao { get; set; }
        public string ModeloVeiculoCombustao { get; set; }
        public int AnoFabricacaoVeiculoCombustao { get; set; }
        public decimal ConsumoCombustivelAnteriorKmPorLitro { get; set; }
        public decimal EmissaoCO2Anterior { get; set; }

        // Dados do Veículo Substituto
        public TipoVeiculoSubstituto TipoVeiculoSubstituto { get; set; }
        public string MarcaVeiculoSubstituto { get; set; }
        public string ModeloVeiculoSubstituto { get; set; }
        public decimal EmissaoCO2Nova { get; set; }
        public decimal AutonomiaVeiculoSubstitutoKm { get; set; }

        // Data (foco na correção do formato para o frontend)
        public DateTime DataSubstituicao { get; set; }
    }
}
