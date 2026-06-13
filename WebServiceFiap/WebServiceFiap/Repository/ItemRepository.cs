using WebServiceFiap.Data.Contexts;
using WebServiceFiap.Models;
using WebServiceFiap.Repository.AbstractRepo;

namespace WebServiceFiap.Repository
{
    public class ItemRepository : RepositoryBase<ItemModel>
    {
        public ItemRepository(ApplicationDbContext context)
            : base(context)
        {
        }
    }
}
