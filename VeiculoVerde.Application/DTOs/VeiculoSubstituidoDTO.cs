
using System.ComponentModel.DataAnnotations;
using VeiculoVerde.Domain.Entities;

namespace VeiculoVerde.Application.DTOs
{
    public class VeiculoSubstituidoDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "A placa do veículo a combustão é obrigatória.")]
        [StringLength(10, ErrorMessage = "A placa deve ter no máximo 10 caracteres.")]
        public string PlacaVeiculoCombustao { get; set; }

        [Required(ErrorMessage = "A marca do veículo a combustão é obrigatória.")]
        public string MarcaVeiculoCombustao { get; set; }

        [Required(ErrorMessage = "O modelo do veículo a combustão é obrigatório.")]
        public string ModeloVeiculoCombustao { get; set; }

        [Range(1900, 2025, ErrorMessage = "O ano de fabricação deve estar entre 1900 e 2025.")]
        public int AnoFabricacaoVeiculoCombustao { get; set; }

        [Range(0, 1000, ErrorMessage = "A emissão de CO2 anterior deve ser um valor positivo.")]
        public decimal EmissaoCO2Anterior { get; set; }

        [Range(0, 100, ErrorMessage = "O consumo de combustível anterior deve ser um valor positivo.")]
        public decimal ConsumoCombustivelAnteriorKmPorLitro { get; set; }

        [Required(ErrorMessage = "O tipo do veículo substituto é obrigatório.")]
        public TipoVeiculoSubstituto TipoVeiculoSubstituto { get; set; }

        [Required(ErrorMessage = "A marca do veículo substituto é obrigatória.")]
        public string MarcaVeiculoSubstituto { get; set; }

        [Required(ErrorMessage = "O modelo do veículo substituto é obrigatório.")]
        public string ModeloVeiculoSubstituto { get; set; }

        [Range(0, 1000, ErrorMessage = "A emissão de CO2 nova deve ser um valor positivo.")]
        public decimal EmissaoCO2Nova { get; set; }

        [Range(0, 10000, ErrorMessage = "A autonomia do veículo substituto deve ser um valor positivo.")]
        public decimal AutonomiaVeiculoSubstitutoKm { get; set; }

        [Required(ErrorMessage = "A data de substituição é obrigatória.")]
        public DateTime DataSubstituicao { get; set; }

        [Required(ErrorMessage = "O CEP é obrigatório.")]
        [StringLength(8, MinimumLength = 8, ErrorMessage = "O CEP deve ter 8 dígitos.")]
        public string CEP { get; set; }

        [Required(ErrorMessage = "A cidade é obrigatória.")]
        public string Cidade { get; set; }

        [Required(ErrorMessage = "O estado é obrigatório.")]
        [StringLength(2, MinimumLength = 2, ErrorMessage = "O estado deve ter 2 caracteres (UF).")]
        public string Estado { get; set; }

        public string Pais { get; set; } = "Brasil";
    }
}