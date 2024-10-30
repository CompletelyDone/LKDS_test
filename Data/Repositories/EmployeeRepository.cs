using Data.Abstractions;
using Data.Context;
using Microsoft.EntityFrameworkCore;
using Model;
using Serilog;

namespace Data.Repositories
{
    public class EmployeeRepository : IRepositoryBase<Employee>
    {
        private readonly SQLiteDBContext context;
        public EmployeeRepository(SQLiteDBContext context)
        {
            this.context = context;
        }
        public async Task Create(Employee employee)
        {
            try
            {
                await context.Employees.AddAsync(employee);
                await context.SaveChangesAsync();
                Log.Information($"Сотрудник {employee.Id} добавлен.");
            }
            catch (Exception ex)
            {
                Log.Error(ex, $"Ошибка при добавлении сотрудника {employee.Id}.");
            }
        }
        public async Task Delete(Guid id)
        {
            if (id == Guid.Empty) return;
            try
            {
                await context.Employees
                    .Where(emp => emp.Id == id)
                    .ExecuteDeleteAsync();
                Log.Information($"Успешно удален сотрудник {id}.");
            }
            catch (Exception ex)
            {
                Log.Error(ex, $"Ошибка при удалении сотрудника {id}.");
            }
        }
        public async Task<List<Employee>> GetAllAsync()
        {
            List<Employee> employees = [];
            try
            {
                employees = await context.Employees
                   .AsNoTracking()
                   .ToListAsync();
                Log.Information($"Успешно извлечено {employees.Count} сотрудников.");
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Ошибка при извлечении сотрудников.");
            }
            return employees;
        }
        public async Task<Employee?> GetById(Guid id)
        {
            if(id == Guid.Empty) return null;
            Employee? employee = null;
            try
            {
                employee = await context.Employees
                    .Where(emp => emp.Id == id)
                    .FirstOrDefaultAsync();
                Log.Information($"Успешно извлечен сотрудник {id}.");
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Ошибка при извлечении сотрудника.");
            }
            return employee;
        }
        public async Task Update(Employee employee)
        {
            try
            {
                await context.Employees
                    .ExecuteUpdateAsync(s => s
                        .SetProperty(emp => emp.FirstName, employee.FirstName)
                        .SetProperty(emp => emp.LastName, employee.LastName)
                        .SetProperty(emp => emp.Patronymic, employee.Patronymic)
                        .SetProperty(emp => emp.PhotoPath, employee.PhotoPath)
                        .SetProperty(emp => emp.Company, employee.Company));
                Log.Information($"Успешно обновлен сотрудник {employee.Id}.");
            }
            catch (Exception ex)
            {
                Log.Error(ex, $"Ошибка при обновлении сотрудника {employee.Id}.");
            }
        }
        public async Task<List<Employee>> SearchEmployeeByField(string field)
        {
            if (string.IsNullOrEmpty(field)) return [];
            List<Employee> possibleEmployee = [];
            try
            {
                var employees = await context.Employees
                    .Where(e => e.LastName.Contains(field) ||
                                e.FirstName.Contains(field) ||
                                (e.Patronymic != null && e.Patronymic.Contains(field)))
                    .ToListAsync();
                Log.Information("Успешно выполнен запрос по поиску сотрудников по полю.");
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Ошибка при поиске сотрудников.");
            }

            return possibleEmployee;
        }
    }
}
