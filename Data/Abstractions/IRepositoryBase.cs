using Model;

namespace Data.Abstractions
{
    public interface IRepositoryBase<T> where T : class
    {
        Task CreateAsync(T param);
        Task CreateRangeAsync(List<T> param);
        Task<List<T>> GetAllAsync();
        Task<T?> GetByIdAsync(Guid id);
        Task UpdateAsync(T param);
        Task DeleteAsync(Guid id);
    }
}