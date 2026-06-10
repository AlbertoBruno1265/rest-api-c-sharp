using Microsoft.EntityFrameworkCore;
using System.Configuration;
using WebServiceFiap.Data.Contexts;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

// Conexão com o Banco
var connStr = builder.Configuration.GetConnectionString("DatabaseConnection");
builder.Services.AddDbContext<DatabaseContext>(
    opt => opt.UseOracle(connStr).EnableSensitiveDataLogging(true)
);


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
