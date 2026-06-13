using WebServiceFiap.Models;
using WebServiceFiap.Repository;

namespace WebServiceFiap.Services
{
    public class UsuarioService
    {
        private readonly UsuarioRepository _repository;

        public UsuarioService(UsuarioRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<UsuarioModel> GetAll()
        {
            return _repository.GetAll();
        }

        public UsuarioModel? GetById(long id)
        {
            return _repository.GetById(id);
        }

        public void Add(UsuarioModel usuario)
        {
            _repository.Add(usuario);
        }

        public void Update(UsuarioModel usuario)
        {
            _repository.Update(usuario);
        }

        public void Delete(long id)
        {
            var usuario = _repository.GetById(id);

            if (usuario == null)
                throw new Exception("Usuário não encontrado.");

            _repository.Delete(usuario);
        }
    }
}
