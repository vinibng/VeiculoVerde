using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace VeiculoVerde.Application.DTOs
{
    // DTO utilizado para retornar o resultado da análise de impacto,
    // incluindo a lista detalhada de veículos que contribuíram.
    public class ImpactoAmbientalResponseDTO
    {
        // Campos Agregados de Impacto
        public string CEP { get; set; }
        public string Cidade { get; set; }
        public string Estado { get; set; }

        [Range(0, double.MaxValue)]
        public decimal ReducaoCO2TotalKg { get; set; }

        [Range(0, double.MaxValue)]
        public decimal EconomiaCombustivelTotalLitros { get; set; }

        [Range(0, int.MaxValue)]
        public int NumeroSubstituicoes { get; set; }

        [Required]
        public DateTime DataAnalise { get; set; }

        // NOVO CAMPO: Lista de veículos que contribuíram para este impacto.
        // Usamos o DTO de Resposta do Veículo que criamos anteriormente (VeiculoSubstituidoResponseDTO).
        public IEnumerable<VeiculoSubstituidoResponseDTO> VeiculosSubstituidos { get; set; } = new List<VeiculoSubstituidoResponseDTO>();
    }
}

