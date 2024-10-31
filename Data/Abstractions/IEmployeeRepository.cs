using Model;

namespace Data.Abstractions
{
    public interface IEmployeeRepository : IRepositoryBase<Employee>
    {
        Task<List<Employee>> SearchEmployeeByFieldAsync(string field);
    }
}