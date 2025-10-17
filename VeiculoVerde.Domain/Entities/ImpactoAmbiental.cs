// <copyright file="ImpactoAmbiental.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace VeiculoVerde.Domain.Entities
{
    /// <summary>
    /// Represents the environmental impact resulting from replacing combustion vehicles with more sustainable alternatives.
    /// </summary>
    public class ImpactoAmbiental
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ImpactoAmbiental"/> class.
        /// </summary>
        public ImpactoAmbiental()
        {
        }

        /// <summary>
        /// Gets or sets the unique identifier of the environmental impact.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the ZIP code related to the environmental impact.
        /// </summary>
        required public string CEP { get; set; }

        /// <summary>
        /// Gets or sets the city related to the environmental impact.
        /// </summary>
        required public string Cidade { get; set; }

        /// <summary>
        /// Gets or sets the state related to the environmental impact.
        /// </summary>
        required public string Estado { get; set; }

        /// <summary>
        /// Gets or sets the total CO2 reduction in kilograms.
        /// </summary>
        public decimal ReducaoCO2TotalKg { get; set; }

        /// <summary>
        /// Gets or sets the total fuel savings in liters.
        /// </summary>
        public decimal EconomiaCombustivelTotalLitros { get; set; } = 0;

        /// <summary>
        /// Gets or sets the number of substitutions performed.
        /// </summary>
        public int NumeroSubstituicoes { get; set; }

        /// <summary>
        /// Gets or sets the date of the environmental impact analysis.
        /// </summary>
        public DateTime DataAnalise { get; set; }
    }
}