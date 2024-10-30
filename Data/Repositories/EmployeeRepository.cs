using Data.Abstractions;
using Data.Context;
using Microsoft.EntityFrameworkCore;
using Model;

namespace Data.Repositories
{
    public class EmployeeRepository : IRepositoryBase<Employee>
    {
        private readonly SQLiteDBContext context;
        public EmployeeRepository(SQLiteDBContext context)
        {
            this.context = context;
        }

        public async Task Create(Employee param)
        {
            await context.Employees.AddAsync(param);
            await context.SaveChangesAsync();
        }

        public async Task Delete(Guid id)
        {
            await context.Employees
                .Where(emp=>emp.Id == id)
                .ExecuteDeleteAsync();
        }

        public async Task<List<Employee>> GetAllAsync()
        {
            return await context.Employees
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Employee?> GetById(Guid id)
        {
            return await context.Employees
                .Where (emp=>emp.Id == id)
                .FirstOrDefaultAsync();
        }

        public async Task Update(Employee param)
        {
            await context.Employees
                .ExecuteUpdateAsync(s => s
                    .SetProperty(emp => emp.FirstName, param.FirstName)
                    .SetProperty(emp => emp.LastName, param.LastName)
                    .SetProperty(emp => emp.Patronymic, param.Patronymic)
                    .SetProperty(emp => emp.PhotoPath, param.PhotoPath)
                    .SetProperty(emp => emp.Company, param.Company));
        }
    }
}
