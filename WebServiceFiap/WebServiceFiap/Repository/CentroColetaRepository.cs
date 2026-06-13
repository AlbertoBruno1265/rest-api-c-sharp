using WebServiceFiap.Data.Contexts;
using WebServiceFiap.Models;
using WebServiceFiap.Repository.AbstractRepo;

namespace WebServiceFiap.Repository
{
    public class CentroColetaRepository : RepositoryBase<CentroColetaModel>
    {
        public CentroColetaRepository(ApplicationDbContext context)
            : base(context)
        {
        }
    }
}
