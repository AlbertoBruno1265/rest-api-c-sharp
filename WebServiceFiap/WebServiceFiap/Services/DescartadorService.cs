using WebServiceFiap.Models;
using WebServiceFiap.Repository;

namespace WebServiceFiap.Services
{
    public class DescartadorService
    {
        private readonly DescartadorRepository _repository;

        public DescartadorService(DescartadorRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<DescartadorModel> GetAll()
            => _repository.GetAll();

        public DescartadorModel? GetById(long id)
            => _repository.GetById(id);

        public void Add(DescartadorModel descartador)
            => _repository.Add(descartador);

        public void Update(DescartadorModel descartador)
            => _repository.Update(descartador);

        public void Delete(long id)
        {
            var descartador = _repository.GetById(id);

            if (descartador == null)
                throw new Exception("Descartador não encontrado.");

            _repository.Delete(descartador);
        }
    }
}
