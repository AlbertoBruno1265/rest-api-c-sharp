using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using WebServiceFiap.Data.Contexts;
using WebServiceFiap.Models;

namespace WebServiceFiap.Tests;

public class ApiFactory : WebApplicationFactory<Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureLogging(logging =>
        {
            logging.ClearProviders();
        });

        builder.ConfigureServices(services =>
        {
            services.RemoveAll<DbContextOptions<ApplicationDbContext>>();

            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseInMemoryDatabase("WebServiceFiapTests");
            });

            var serviceProvider = services.BuildServiceProvider();

            using var scope = serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            context.Usuarios.Add(new UsuarioModel
            {
                Id = 1,
                Nome = "Usuario Teste",
                Email = "teste@fiap.com.br",
                Senha = "123456",
                Funcao = "Admin"
            });

            context.SaveChanges();
        });
    }
}
