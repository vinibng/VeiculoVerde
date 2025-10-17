using Microsoft.EntityFrameworkCore;
using VeiculoVerde.Domain.Entities;
using VeiculoVerde.Infrastructure.Data.Configurations;

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

            modelBuilder.ApplyConfiguration(new VeiculoSubstituidoConfiguration());
            modelBuilder.ApplyConfiguration(new ImpactoAmbientalConfiguration());
        }
    }
}
