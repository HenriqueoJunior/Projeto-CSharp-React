using ControleGastos.API.Data;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Configura CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

// Configura JSON serialization para ignorar referências circulares
// Isso previne o erro "object cycle detected" ao serializar entidades com relacionamentos bidirecionais
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
        options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
    });

// Registra o AppDbContext usando SQLite
// O arquivo do banco será criado automaticamente na raiz do projeto
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite("Data Source=controle_gastos.db"));

// Registra os services 
builder.Services.AddScoped<ControleGastos.API.Services.PessoaService.IPessoaService, ControleGastos.API.Services.PessoaService.PessoaService>();
builder.Services.AddScoped<ControleGastos.API.Services.CategoriaService.ICategoriaService, ControleGastos.API.Services.CategoriaService.CategoriaService>();
builder.Services.AddScoped<ControleGastos.API.Services.TransacaoService.ITransacaoService, ControleGastos.API.Services.TransacaoService.TransacaoService>();
builder.Services.AddScoped<ControleGastos.API.Services.RelatorioService.IRelatorioService, ControleGastos.API.Services.RelatorioService.RelatorioService>();

var app = builder.Build();

// Garante que o banco e as tabelas sejam criados ao iniciar a aplicação
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.EnsureCreated();
}

app.UseCors("AllowAll");

app.UseHttpsRedirection();
app.MapControllers();
app.Run();
