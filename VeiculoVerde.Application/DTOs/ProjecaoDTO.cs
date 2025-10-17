using System;
using System.ComponentModel.DataAnnotations;

namespace VeiculoVerde.Application.DTOs
{
    public class ProjecaoDTO
    {
        [Range(1900, 2100)]
        public int Ano { get; set; }

        [Range(1, 12)]
        public int Mes { get; set; }

        [Range(0, int.MaxValue)]
        public int VeiculosProjetados { get; set; }

        [Range(0, double.MaxValue)]
        public decimal ReducaoCO2ProjetadaKg { get; set; }

        [Range(0, double.MaxValue)]
        public decimal EconomiaCombustivelProjetadaLitros { get; set; }
    }
}
