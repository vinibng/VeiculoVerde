using Microsoft.EntityFrameworkCore;
using VeiculoVerde.Infrastructure.Data;
using VeiculoVerde.Domain.Interfaces;
using VeiculoVerde.Infrastructure.Repositories;
using VeiculoVerde.Application.Services;
using VeiculoVerde.Application.Interfaces;
using Microsoft.Extensions.Logging; // Necessário para configurar o LogLevel

var builder = WebApplication.CreateBuilder(args);

// [CORREÇÃO: Aumenta o nível de log para Debug para diagnosticar o erro de startup]
builder.Host.ConfigureLogging(logging =>
{
    logging.ClearProviders();
    logging.AddConsole();
    logging.SetMinimumLevel(LogLevel.Debug);
});


// 1. Configurar CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

// CORREÇÃO ESSENCIAL PARA DATAS/POSTGRESQL (Npgsql)
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
Console.WriteLine($"Connection string: '{connectionString}'");

if (string.IsNullOrEmpty(connectionString))
{
    throw new Exception("Connection string is null or empty! Verifique variáveis de ambiente e appsettings.json.");
}

// Configura o DbContext com retry automático na conexão
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(connectionString, npgsqlOptions =>
        npgsqlOptions.EnableRetryOnFailure(
            maxRetryCount: 5,
            maxRetryDelay: TimeSpan.FromSeconds(10),
            errorCodesToAdd: null)));

builder.Services.AddScoped<IVeiculoSubstituidoRepository, VeiculoSubstituidoRepository>();
builder.Services.AddScoped<IImpactoAmbientalRepository, ImpactoAmbientalRepository>();
builder.Services.AddScoped<IVeiculoSubstituidoService, VeiculoSubstituidoService>();
builder.Services.AddScoped<IAnaliseImpactoService, AnaliseImpactoService>();
builder.Services.AddScoped<IProjecaoService, ProjecaoService>();
builder.Services.AddScoped<ISugestaoIncentivoService, SugestaoIncentivoService>();

builder.Services.AddAutoMapper(typeof(VeiculoVerde.Application.Mappings.MappingProfile).Assembly);


var app = builder.Build();

// MIGRAÇÃO DO BANCO COM RETENTATIVAS
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

    var retries = 10;
    var delay = TimeSpan.FromSeconds(5);
    var connected = false;
    Exception? lastException = null;

    for (int i = 0; i < retries; i++)
    {
        try
        {
            Console.WriteLine($"Tentativa {i + 1}/{retries}: Tentando aplicar migrações...");
            dbContext.Database.Migrate();
            connected = true;
            Console.WriteLine("Migrações aplicadas com sucesso!");
            break;
        }
        catch (Exception ex)
        {
            lastException = ex;
            Console.WriteLine($"Tentativa {i + 1} falhou. Aguardando {delay.TotalSeconds}s. Erro: {ex.Message}");
            Thread.Sleep(delay);
        }
    }

    if (!connected)
    {
        // Continuamos sem o 'throw' para evitar o crash forçado.
        Console.WriteLine("---------------------------------------------------------------------");
        Console.WriteLine("AVISO CRÍTICO: Não foi possível conectar ao banco após múltiplas tentativas.");
        Console.WriteLine("O aplicativo irá iniciar, mas o acesso ao DB falhará.");
        Console.WriteLine($"Detalhes do último erro: {lastException?.Message}");
        Console.WriteLine("---------------------------------------------------------------------");
    }
}

// Somente redireciona HTTPS fora do ambiente de desenvolvimento
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "VeiculoVerde API V1");
    });
}

// O UseRouting é necessário para o FallbackToPage.
app.UseRouting();

// 2. Usar o Middleware CORS 
app.UseCors("AllowAll");

app.UseStaticFiles();
app.UseAuthorization();

// As APIs devem ser mapeadas primeiro para não serem interceptadas pelo fallback
app.MapControllers();

// ADICIONADO: Configura o Fallback. 
app.MapFallbackToFile("index.html");

app.Run();
