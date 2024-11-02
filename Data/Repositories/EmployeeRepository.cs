using Data.Abstractions;
using Data.Context;
using Microsoft.EntityFrameworkCore;
using Model;
using Serilog;

namespace Data.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly SQLiteDBContext context;
        public EmployeeRepository(SQLiteDBContext context)
        {
            this.context = context;
        }
        public async Task CreateAsync(Employee employee)
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
        public async Task CreateRangeAsync(List<Employee> employees)
        {
            if (employees == null || employees.Count == 0)
            {
                Log.Warning("Попытка добавить пустой список сотрудников.");
                return;
            }
            try
            {
                await context.Employees.AddRangeAsync(employees);
                await context.SaveChangesAsync();
                Log.Information($"Добавлено {employees.Count} сотрудников.");
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Ошибка при добавлении группы сотрудников.");
            }
        }
        public async Task DeleteAsync(Guid id)
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
                   .Include(emp => emp.Company)
                   .ToListAsync();
                Log.Information($"Успешно извлечено {employees.Count} сотрудников.");
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Ошибка при извлечении сотрудников.");
            }
            return employees;
        }
        public async Task<Employee?> GetByIdAsync(Guid id)
        {
            if (id == Guid.Empty) return null;
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
        public async Task UpdateAsync(Employee employee)
        {
            try
            {
                await context.Employees
                    .ExecuteUpdateAsync(s => s
                        .SetProperty(emp => emp.FirstName, employee.FirstName)
                        .SetProperty(emp => emp.LastName, employee.LastName)
                        .SetProperty(emp => emp.Patronymic, employee.Patronymic)
                        .SetProperty(emp => emp.PhotoPath, employee.PhotoPath)
                        .SetProperty(emp => emp.CompanyId, employee.CompanyId));
                Log.Information($"Успешно обновлен сотрудник {employee.Id}.");
            }
            catch (Exception ex)
            {
                Log.Error(ex, $"Ошибка при обновлении сотрудника {employee.Id}.");
            }
        }
        public async Task<List<Employee>> SearchEmployeeByFieldAsync(string field)
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
