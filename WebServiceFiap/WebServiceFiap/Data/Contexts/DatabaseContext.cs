using Microsoft.EntityFrameworkCore;

namespace WebServiceFiap.Data.Contexts
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions options) : base(options)
        {
        }

        protected DatabaseContext()
        {

        }

    }
}
