using Microsoft.EntityFrameworkCore;
using WebServiceFiap.Data.Contexts;
using WebServiceFiap.Models;
using WebServiceFiap.Repository.AbstractRepo;

namespace WebServiceFiap.Repository
{
    public class ColetaItemRepository : RepositoryBase<ColetaItemModel>
    {
        public ColetaItemRepository(ApplicationDbContext context)
            : base(context)
        {

        }
        public ColetaItemModel? GetById(
            long idColeta,
            long idItem)
        {
            return _context.ColetasItens
                .FirstOrDefault(x =>
                    x.IdColeta == idColeta &&
                    x.IdItem == idItem);
        }
    }
}
