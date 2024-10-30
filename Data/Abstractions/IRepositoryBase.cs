using Model;

namespace Data.Abstractions
{
    public interface IRepositoryBase<T> where T : class
    {
        Task Create(T param);
        Task<List<T>> GetAllAsync();
        Task<T?> GetById(Guid id);
        Task Update(T param);
        Task Delete(Guid id);
    }
}