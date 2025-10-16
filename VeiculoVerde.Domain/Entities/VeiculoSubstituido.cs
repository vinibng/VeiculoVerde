using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VeiculoVerde.Domain.Entities
{
    public enum TipoVeiculoSubstituto
    {
        CarroEletrico,
        MotoEletrica,
        BicicletaEletrica,
        BicicletaComum
    }

    public class VeiculoSubstituido
    {
        public int Id { get; set; }
        public string PlacaVeiculoCombustao { get; set; }
        public string MarcaVeiculoCombustao { get; set; }
        public string ModeloVeiculoCombustao { get; set; }
        public int AnoFabricacaoVeiculoCombustao { get; set; }
        public decimal EmissaoCO2Anterior { get; set; } 
        public decimal ConsumoCombustivelAnteriorKmPorLitro { get; set; } 

        public TipoVeiculoSubstituto TipoVeiculoSubstituto { get; set; }
        public string MarcaVeiculoSubstituto { get; set; }
        public string ModeloVeiculoSubstituto { get; set; }
        public decimal EmissaoCO2Nova { get; set; } 
        public decimal AutonomiaVeiculoSubstitutoKm { get; set; } 

        public DateTime DataSubstituicao { get; set; }

      
        public string CEP { get; set; }
        public string Cidade { get; set; }
        public string Estado { get; set; }
        public string Pais { get; set; }
    }
}
