using Microsoft.EntityFrameworkCore;
using System.Configuration;
using WebServiceFiap.Data.Contexts;
using WebServiceFiap.Repository;
using WebServiceFiap.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// Conexão com o Banco
var connStr = builder.Configuration.GetConnectionString("DatabaseConnection");
builder.Services.AddDbContext<ApplicationDbContext>(
    opt => opt.UseOracle(connStr).EnableSensitiveDataLogging(true)
);

// Cadastramento de Repositórios
builder.Services.AddScoped<UsuarioRepository>();
builder.Services.AddScoped<CatadorRepository>();
builder.Services.AddScoped<DescartadorRepository>();
builder.Services.AddScoped<CentroColetaRepository>();
builder.Services.AddScoped<ItemRepository>();
builder.Services.AddScoped<ColetaRepository>();
builder.Services.AddScoped<ColetaItemRepository>();
builder.Services.AddScoped<CatadorItemRepository>();

// Cadastramento das Services
builder.Services.AddScoped<UsuarioService>();
builder.Services.AddScoped<CatadorService>();
builder.Services.AddScoped<DescartadorService>();
builder.Services.AddScoped<CentroColetaService>();
builder.Services.AddScoped<ItemService>();
builder.Services.AddScoped<ColetaService>();

// Build do Projeto
var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

public partial class Program
{
}
