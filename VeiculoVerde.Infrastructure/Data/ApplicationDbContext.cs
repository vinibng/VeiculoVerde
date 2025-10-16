using Microsoft.EntityFrameworkCore;
using VeiculoVerde.Domain.Entities;

namespace VeiculoVerde.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<VeiculoSubstituido> VeiculosSubstituidos { get; set; }
        public DbSet<ImpactoAmbiental> ImpactosAmbientais { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<VeiculoSubstituido>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.PlacaVeiculoCombustao).HasMaxLength(10).IsRequired();
                entity.Property(e => e.TipoVeiculoSubstituto).IsRequired();
                entity.Property(e => e.DataSubstituicao).IsRequired();
                entity.Property(e => e.EmissaoCO2Anterior).HasColumnType("decimal(18,2)");
                entity.Property(e => e.EmissaoCO2Nova).HasColumnType("decimal(18,2)");
                entity.Property(e => e.ConsumoCombustivelAnteriorKmPorLitro).HasColumnType("decimal(18,2)");
                entity.Property(e => e.AutonomiaVeiculoSubstitutoKm).HasColumnType("decimal(18,2)");
                entity.Property(e => e.CEP).HasMaxLength(8).IsRequired();
                entity.Property(e => e.Cidade).HasMaxLength(100).IsRequired();
                entity.Property(e => e.Estado).HasMaxLength(2).IsRequired();
            });

            modelBuilder.Entity<ImpactoAmbiental>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.CEP).HasMaxLength(8);
                entity.Property(e => e.Cidade).HasMaxLength(100);
                entity.Property(e => e.Estado).HasMaxLength(2);
                entity.Property(e => e.ReducaoCO2TotalKg).HasColumnType("decimal(18,2)");
                entity.Property(e => e.EconomiaCombustivelTotalLitros).HasColumnType("decimal(18,2)");
                entity.Property(e => e.DataAnalise).IsRequired();
            });
        }
    }
}
