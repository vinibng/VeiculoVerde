using Microsoft.EntityFrameworkCore;
using VeiculoVerde.Infrastructure.Data;
using VeiculoVerde.Domain.Interfaces;
using VeiculoVerde.Infrastructure.Repositories;
using VeiculoVerde.Application.Services;
using VeiculoVerde.Application.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Busca connection string da variável de ambiente "ConnectionStrings__DefaultConnection"
// ou do appsettings.json
var connectionString = Environment.GetEnvironmentVariable("ConnectionStrings__DefaultConnection")
                      ?? builder.Configuration.GetConnectionString("DefaultConnection");

// Configura o DbContext para usar PostgreSQL
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(connectionString));

// Injeção de dependências
builder.Services.AddScoped<IVeiculoSubstituidoRepository, VeiculoSubstituidoRepository>();
builder.Services.AddScoped<IImpactoAmbientalRepository, ImpactoAmbientalRepository>();
builder.Services.AddScoped<IVeiculoSubstituidoService, VeiculoSubstituidoService>();
builder.Services.AddScoped<IAnaliseImpactoService, AnaliseImpactoService>();
builder.Services.AddScoped<IProjecaoService, ProjecaoService>();
builder.Services.AddScoped<ISugestaoIncentivoService, SugestaoIncentivoService>();

builder.Services.AddAutoMapper(typeof(VeiculoVerde.Application.Mappings.MappingProfile).Assembly);

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    dbContext.Database.Migrate();
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAuthorization();
app.MapControllers();

app.Run();
