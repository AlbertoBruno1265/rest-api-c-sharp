using WebServiceFiap.Data.Contexts;

namespace WebServiceFiap.Repository.AbstractRepo
{
    public abstract class RepositoryBase<TEntity>
        where TEntity : class
    {
        protected readonly ApplicationDbContext _context;

        protected RepositoryBase(ApplicationDbContext context)
        {
            _context = context;
        }

        public virtual IEnumerable<TEntity> GetAll()
        {
            return _context.Set<TEntity>().ToList();
        }

        public virtual IEnumerable<TEntity> GetPaged(int page, int pageSize)
        {
            var safePage = Math.Max(page, 1);
            var safePageSize = Math.Clamp(pageSize, 1, 100);

            return _context.Set<TEntity>()
                .Skip((safePage - 1) * safePageSize)
                .Take(safePageSize)
                .ToList();
        }

        public virtual int Count()
        {
            return _context.Set<TEntity>().Count();
        }

        public virtual TEntity? GetById(long id)
        {
            return _context.Set<TEntity>().Find(id);
        }

        public virtual void Add(TEntity entity)
        {
            _context.Set<TEntity>().Add(entity);
            _context.SaveChanges();
        }

        public virtual void Update(TEntity entity)
        {
            _context.Set<TEntity>().Update(entity);
            _context.SaveChanges();
        }

        public virtual void Delete(TEntity entity)
        {
            _context.Set<TEntity>().Remove(entity);
            _context.SaveChanges();
        }
    }
}
