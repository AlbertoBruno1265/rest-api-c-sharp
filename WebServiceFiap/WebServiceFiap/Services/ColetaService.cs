using WebServiceFiap.Models;
using WebServiceFiap.Repository;

namespace WebServiceFiap.Services
{
    public class ColetaService
    {
        private readonly ColetaRepository _repository;

        public ColetaService(ColetaRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<ColetaModel> GetAll()
            => _repository.GetAll();

        public IEnumerable<ColetaModel> GetPaged(int page, int pageSize)
            => _repository.GetPaged(page, pageSize);

        public int Count()
            => _repository.Count();

        public ColetaModel? GetById(long id)
            => _repository.GetById(id);

        public void Add(ColetaModel coleta)
            => _repository.Add(coleta);

        public void Update(ColetaModel coleta)
            => _repository.Update(coleta);

        public void Delete(long id)
        {
            var coleta = _repository.GetById(id);

            if (coleta == null)
                throw new Exception("Coleta não encontrada.");

            _repository.Delete(coleta);
        }
    }
}
