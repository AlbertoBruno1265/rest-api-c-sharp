using WebServiceFiap.Data.Contexts;
using WebServiceFiap.Models;
using WebServiceFiap.Repository.AbstractRepo;

namespace WebServiceFiap.Repository
{
    public class ColetaRepository : RepositoryBase<ColetaModel>
    {
        public ColetaRepository(ApplicationDbContext context)
            : base(context)
        {
        }
    }
}
