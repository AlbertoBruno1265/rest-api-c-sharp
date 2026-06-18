using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Configuration;
using System.Text;
using WebServiceFiap.Data.Contexts;
using WebServiceFiap.Repository;
using WebServiceFiap.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// Autenticação JWT
var jwtIssuer = builder.Configuration["Jwt:Issuer"];
var jwtAudience = builder.Configuration["Jwt:Audience"];
var jwtSecretKey = builder.Configuration["Jwt:SecretKey"];

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtIssuer,
            ValidAudience = jwtAudience,
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(jwtSecretKey ?? string.Empty))
        };
    });

builder.Services.AddAuthorization();

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

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();

public partial class Program
{
}
