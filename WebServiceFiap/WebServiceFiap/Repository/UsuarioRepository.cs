using WebServiceFiap.Data.Contexts;
using WebServiceFiap.Models;
using WebServiceFiap.Repository.AbstractRepo;

namespace WebServiceFiap.Repository
{
    public class UsuarioRepository : RepositoryBase<UsuarioModel>
    {
        public UsuarioRepository(ApplicationDbContext context)
            : base(context)
        {
        }

        public UsuarioModel? GetByEmail(string email)
        {
            return _context.Usuarios
                .FirstOrDefault(usuario => usuario.Email == email);
        }
    }
}
