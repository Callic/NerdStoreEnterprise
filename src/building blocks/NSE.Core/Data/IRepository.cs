using NSE.Core.DomainObjects;

namespace NSE.Core.Data
{
    public interface IRepository<T> where T : Entity
    {
        Task CreateAsync(T entity);
        void Update(T entity);
        void Delete(T entity);
        Task<T?> GetAsync(Guid id);
        Task<List<T>> GetAllAsync();
    }
}
