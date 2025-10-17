using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VeiculoVerde.Domain.Entities;

namespace VeiculoVerde.Infrastructure.Data.Configurations
{
    public class VeiculoSubstituidoConfiguration : IEntityTypeConfiguration<VeiculoSubstituido>
    {
        public void Configure(EntityTypeBuilder<VeiculoSubstituido> entity)
        {
            entity.HasKey(e => e.Id);

            entity.Property(e => e.PlacaVeiculoCombustao)
                .HasColumnType("varchar(10)")
                .IsRequired();

            entity.Property(e => e.TipoVeiculoSubstituto)
                .IsRequired();

            entity.Property(e => e.DataSubstituicao)
                .HasColumnType("timestamp")
                .IsRequired();

            entity.Property(e => e.EmissaoCO2Anterior)
                .HasColumnType("numeric(18,2)");

            entity.Property(e => e.EmissaoCO2Nova)
                .HasColumnType("numeric(18,2)");

            entity.Property(e => e.ConsumoCombustivelAnteriorKmPorLitro)
                .HasColumnType("numeric(18,2)");

            entity.Property(e => e.AutonomiaVeiculoSubstitutoKm)
                .HasColumnType("numeric(18,2)");

            entity.Property(e => e.CEP)
                .HasColumnType("varchar(8)")
                .IsRequired();

            entity.Property(e => e.Cidade)
                .HasColumnType("varchar(100)")
                .IsRequired();

            entity.Property(e => e.Estado)
                .HasColumnType("varchar(2)")
                .IsRequired();
        }
    }
}
