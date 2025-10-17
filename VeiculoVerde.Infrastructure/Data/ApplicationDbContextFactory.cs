using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using VeiculoVerde.Infrastructure.Data;

public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
{
    public ApplicationDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
        // Use a connection string hardcoded or via environment variables aqui
        optionsBuilder.UseNpgsql("Host=db;Port=5432;Database=esg_cidades;Username=esguser;Password=esgpassword");

        return new ApplicationDbContext(optionsBuilder.Options);
    }
}
