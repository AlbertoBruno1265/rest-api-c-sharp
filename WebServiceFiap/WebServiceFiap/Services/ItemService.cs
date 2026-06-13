using WebServiceFiap.Models;
using WebServiceFiap.Repository;

namespace WebServiceFiap.Services
{
    public class ItemService
    {
        private readonly ItemRepository _repository;

        public ItemService(ItemRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<ItemModel> GetAll()
            => _repository.GetAll();

        public ItemModel? GetById(long id)
            => _repository.GetById(id);

        public void Add(ItemModel item)
            => _repository.Add(item);

        public void Update(ItemModel item)
            => _repository.Update(item);

        public void Delete(long id)
        {
            var item = _repository.GetById(id);

            if (item == null)
                throw new Exception("Item não encontrado.");

            _repository.Delete(item);
        }
    }
}
