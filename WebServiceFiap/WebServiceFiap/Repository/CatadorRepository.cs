using WebServiceFiap.Data.Contexts;
using WebServiceFiap.Models;
using WebServiceFiap.Repository.AbstractRepo;

namespace WebServiceFiap.Repository
{
    public class CatadorRepository : RepositoryBase<CatadorModel>
    {
        public CatadorRepository(ApplicationDbContext context)
            : base(context)
        {
        }
    }
}
