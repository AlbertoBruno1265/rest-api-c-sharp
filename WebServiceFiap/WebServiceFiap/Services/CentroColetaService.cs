using WebServiceFiap.Models;
using WebServiceFiap.Repository;

namespace WebServiceFiap.Services
{
    public class CentroColetaService
    {
        private readonly CentroColetaRepository _repository;

        public CentroColetaService(CentroColetaRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<CentroColetaModel> GetAll()
            => _repository.GetAll();

        public IEnumerable<CentroColetaModel> GetPaged(int page, int pageSize)
            => _repository.GetPaged(page, pageSize);

        public int Count()
            => _repository.Count();

        public CentroColetaModel? GetById(long id)
            => _repository.GetById(id);

        public void Add(CentroColetaModel centro)
            => _repository.Add(centro);

        public void Update(CentroColetaModel centro)
            => _repository.Update(centro);

        public void Delete(long id)
        {
            var centro = _repository.GetById(id);

            if (centro == null)
                throw new Exception("Centro de coleta não encontrado.");

            _repository.Delete(centro);
        }
    }
    
}
