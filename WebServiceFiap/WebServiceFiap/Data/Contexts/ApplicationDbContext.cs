using Microsoft.EntityFrameworkCore;
using WebServiceFiap.Models;

namespace WebServiceFiap.Data.Contexts
{
    public class ApplicationDbContext : DbContext
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> opt) : base(opt)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
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
