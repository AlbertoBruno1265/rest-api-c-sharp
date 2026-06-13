using WebServiceFiap.Data.Contexts;
using WebServiceFiap.Models;
using WebServiceFiap.Repository.AbstractRepo;

namespace WebServiceFiap.Repository
{
    public class DescartadorRepository : RepositoryBase<DescartadorModel>
    {
        public DescartadorRepository(ApplicationDbContext context)
            : base(context)
        {
        }
    }
}
