// <copyright file="VeiculoSubstituido.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace VeiculoVerde.Domain.Entities
{
    /// <summary>
    /// Enumerates the types of substitute vehicles available.
    /// </summary>
    public enum TipoVeiculoSubstituto
    {
        /// <summary>
        /// Electric car.
        /// </summary>
        CarroEletrico,

        /// <summary>
        /// Electric motorcycle.
        /// </summary>
        MotoEletrica,

        /// <summary>
        /// Electric bicycle.
        /// </summary>
        BicicletaEletrica,

        /// <summary>
        /// Regular bicycle.
        /// </summary>
        BicicletaComum,
    }

    /// <summary>
    /// Represents a combustion vehicle replaced by a more sustainable alternative.
    /// </summary>
    public class VeiculoSubstituido
    {
        /// <summary>
        /// Gets or sets the unique identifier of the replaced vehicle.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the license plate of the combustion vehicle.
        /// </summary>
        required public string PlacaVeiculoCombustao { get; set; }

        /// <summary>
        /// Gets or sets the brand of the combustion vehicle.
        /// </summary>
        required public string MarcaVeiculoCombustao { get; set; }

        /// <summary>
        /// Gets or sets the model of the combustion vehicle.
        /// </summary>
        required public string ModeloVeiculoCombustao { get; set; }

        /// <summary>
        /// Gets or sets the manufacturing year of the combustion vehicle.
        /// </summary>
        public int AnoFabricacaoVeiculoCombustao { get; set; }

        /// <summary>
        /// Gets or sets the previous CO2 emission of the combustion vehicle.
        /// </summary>
        public decimal EmissaoCO2Anterior { get; set; }

        /// <summary>
        /// Gets or sets the previous fuel consumption in km per liter.
        /// </summary>
        public decimal ConsumoCombustivelAnteriorKmPorLitro { get; set; }

        /// <summary>
        /// Gets or sets the type of the substitute vehicle.
        /// </summary>
        public TipoVeiculoSubstituto TipoVeiculoSubstituto { get; set; }

        /// <summary>
        /// Gets or sets the brand of the substitute vehicle.
        /// </summary>
        required public string MarcaVeiculoSubstituto { get; set; }

        /// <summary>
        /// Gets or sets the model of the substitute vehicle.
        /// </summary>
        required public string ModeloVeiculoSubstituto { get; set; }

        /// <summary>
        /// Gets or sets the new CO2 emission of the substitute vehicle.
        /// </summary>
        public decimal EmissaoCO2Nova { get; set; }

        /// <summary>
        /// Gets or sets the autonomy of the substitute vehicle in kilometers.
        /// </summary>
        public decimal AutonomiaVeiculoSubstitutoKm { get; set; }

        /// <summary>
        /// Gets or sets the date when the vehicle was replaced.
        /// </summary>
        public DateTime DataSubstituicao { get; set; }

        /// <summary>
        /// Gets or sets the ZIP code related to the replaced vehicle.
        /// </summary>
        required public string CEP { get; set; }

        /// <summary>
        /// Gets or sets the city related to the replaced vehicle.
        /// </summary>
        required public string Cidade { get; set; }

        /// <summary>
        /// Gets or sets the state related to the replaced vehicle.
        /// </summary>
        required public string Estado { get; set; }

        /// <summary>
        /// Gets or sets the country related to the replaced vehicle.
        /// </summary>
        required public string Pais { get; set; }
    }
}
