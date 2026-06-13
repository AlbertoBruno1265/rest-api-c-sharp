using Microsoft.EntityFrameworkCore;
using WebServiceFiap.Models;

namespace WebServiceFiap.Data.Contexts
{
    public class ApplicationDbContext : DbContext
    {

        public DbSet<UsuarioModel> Usuarios { get; set; }
        public DbSet<CatadorItemModel> CatadoresItens { get; set; }
        public DbSet<CatadorModel> Catadores { get; set; }
        public DbSet<CentroColetaModel> CentrosColeta { get; set; }
        public DbSet<ColetaItemModel> ColetasItens { get; set; }
        public DbSet<ColetaModel> Coletas { get; set; }
        public DbSet<DescartadorModel> Descartadores { get; set; }
        public DbSet<ItemModel> Itens { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> opt) : base(opt)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Faz a conversão de BOOL para INT pois o Oracle Não suporta BOOL em Coluna
            modelBuilder.Entity<ColetaModel>()
                .Property(x => x.FoiFinalizada)
                .HasConversion<int>();

            modelBuilder.Entity<CatadorItemModel>()
                .Property(x => x.FoiEntregue)
                .HasConversion<int>();

            // Cria chaves primárias compostas
            modelBuilder.Entity<ColetaItemModel>()
                .HasKey(x => new
                {
                    x.IdColeta,
                    x.IdItem
                });

            modelBuilder.Entity<CatadorItemModel>()
                .HasKey(x => new
                {
                    x.IdCatador,
                    x.IdItem
                });

            base.OnModelCreating(modelBuilder);
        }

    }
}
