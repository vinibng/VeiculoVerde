using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VeiculoVerde.Domain.Entities
{
    public class ImpactoAmbiental
    {
        public int Id { get; set; }
        public string CEP { get; set; }
        public string Cidade { get; set; }
        public string Estado { get; set; }
        public decimal ReducaoCO2TotalKg { get; set; } 
        public decimal EconomiaCombustivelTotalLitros { get; set; }
        public int NumeroSubstituicoes { get; set; }
        public DateTime DataAnalise { get; set; }
    }
}