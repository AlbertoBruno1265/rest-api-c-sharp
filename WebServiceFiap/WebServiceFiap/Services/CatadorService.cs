using WebServiceFiap.Models;
using WebServiceFiap.Repository;

namespace WebServiceFiap.Services
{
    public class CatadorService
    {
        private readonly CatadorRepository _repository;

        public CatadorService(CatadorRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<CatadorModel> GetAll()
            => _repository.GetAll();

        public IEnumerable<CatadorModel> GetPaged(int page, int pageSize)
            => _repository.GetPaged(page, pageSize);

        public int Count()
            => _repository.Count();

        public CatadorModel? GetById(long id)
            => _repository.GetById(id);

        public void Add(CatadorModel catador)
            => _repository.Add(catador);

        public void Update(CatadorModel catador)
            => _repository.Update(catador);

        public void Delete(long id)
        {
            var catador = _repository.GetById(id);

            if (catador == null)
                throw new Exception("Catador não encontrado.");

            _repository.Delete(catador);
        }
    }
}
