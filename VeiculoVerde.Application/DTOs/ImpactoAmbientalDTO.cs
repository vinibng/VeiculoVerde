// VeiculoVerde.Application/DTOs/ImpactoAmbientalDTO.cs
using System;

namespace VeiculoVerde.Application.DTOs
{
    public class ImpactoAmbientalDTO
    {
        public string CEP { get; set; }
        public string Cidade { get; set; }
        public string Estado { get; set; }
        public decimal ReducaoCO2TotalKg { get; set; }
        public decimal EconomiaCombustivelTotalLitros { get; set; }
        public int NumeroSubstituicoes { get; set; }
        public DateTime DataAnalise { get; set; }
    }
}