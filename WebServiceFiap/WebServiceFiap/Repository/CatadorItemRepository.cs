using WebServiceFiap.Data.Contexts;
using WebServiceFiap.Models;
using WebServiceFiap.Repository.AbstractRepo;

namespace WebServiceFiap.Repository
{
    public class CatadorItemRepository : RepositoryBase<CatadorItemModel>
    {
        public CatadorItemRepository(ApplicationDbContext context) : base(context)
        {
        }

        public CatadorItemModel? GetById(
            long idCatador,
            long idItem)
        {
            return _context.CatadoresItens
                .FirstOrDefault(x =>
                    x.IdCatador == idCatador &&
                    x.IdItem == idItem);
        }
    }
}
