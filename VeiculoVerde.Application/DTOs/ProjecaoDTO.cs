// VeiculoVerde.Application/DTOs/ProjecaoDTO.cs
using System;

namespace VeiculoVerde.Application.DTOs
{
    public class ProjecaoDTO
    {
        public int Ano { get; set; }
        public int Mes { get; set; }
        public int VeiculosProjetados { get; set; }
        public decimal ReducaoCO2ProjetadaKg { get; set; }
        public decimal EconomiaCombustivelProjetadaLitros { get; set; }
    }
}