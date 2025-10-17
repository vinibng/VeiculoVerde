using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VeiculoVerde.Domain.Entities;

namespace VeiculoVerde.Infrastructure.Data.Configurations
{
    public class ImpactoAmbientalConfiguration : IEntityTypeConfiguration<ImpactoAmbiental>
    {
        public void Configure(EntityTypeBuilder<ImpactoAmbiental> entity)
        {
            entity.HasKey(e => e.Id);

            entity.Property(e => e.CEP)
                .HasColumnType("varchar(8)")
                .IsRequired();

            entity.Property(e => e.Cidade)
                .HasColumnType("varchar(100)")
                .IsRequired();

            entity.Property(e => e.Estado)
                .HasColumnType("varchar(2)")
                .IsRequired();

            entity.Property(e => e.ReducaoCO2TotalKg)
                .HasColumnType("numeric(18,2)");

            entity.Property(e => e.EconomiaCombustivelTotalLitros)
                .HasColumnType("numeric(18,2)");

            entity.Property(e => e.NumeroSubstituicoes)
                .IsRequired();

            entity.Property(e => e.DataAnalise)
                .HasColumnType("timestamp")
                .IsRequired();
        }
    }
}
