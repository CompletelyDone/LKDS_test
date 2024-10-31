using Data.Abstractions;
using Data.Context;
using Microsoft.EntityFrameworkCore;
using Model;
using Serilog;

namespace Data.Repositories
{
    public class CompanyRepository : IRepositoryBase<Company>
    {
        private readonly SQLiteDBContext context;
        public CompanyRepository(SQLiteDBContext context)
        {
            this.context = context;
        }
        public async Task<Company?> GetByIdAsync(Guid id)
        {
            if(id == Guid.Empty) return null;
            Company? company = null;
            try
            {
                company = await context.Companies
                    .Include(c => c.Employees)
                    .FirstOrDefaultAsync(c => c.Id == id);
                Log.Information($"Успешно извлечена организация {id}.");
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Ошибка при извлечении организации.");
            }
            return company;
        }
        public async Task<List<Company>> GetAllAsync()
        {
            List<Company> companies = [];
            try
            {
                companies = await context.Companies
                    .AsNoTracking()
                    .ToListAsync();
                Log.Information($"Успешно извлечено {companies.Count} организаций.");
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Ошибка при извлечении организаций.");
            }
            return companies;
        }
        public async Task CreateAsync(Company company)
        {
            try
            {
                await context.Companies.AddAsync(company);
                await context.SaveChangesAsync();
                Log.Information($"Организация {company.Id} добавлена.");
            }
            catch (Exception ex)
            {
                Log.Error(ex, $"Ошибка при добавлении организации {company.Id}.");
            }
        }
        public async Task CreateRangeAsync(List<Company> companies)
        {
            if (companies == null || companies.Count == 0)
            {
                Log.Warning("Попытка добавить пустой список компаний.");
                return;
            }
            try
            {
                await context.Companies.AddRangeAsync(companies);
                await context.SaveChangesAsync();
                Log.Information($"Добавлено {companies.Count} компаний.");
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Ошибка при добавлении группы компаний.");
            }
        }
        public async Task UpdateAsync(Company company)
        {
            try
            {
                await context.Companies
                    .Where(c => c.Id == company.Id)
                    .ExecuteUpdateAsync(s => s
                        .SetProperty(c => c.Title, company.Title));
                Log.Information($"Успешно обновлена организация {company.Id}.");
            }
            catch (Exception ex)
            {
                Log.Error(ex, $"Ошибка при обновлении организации {company.Id}.");
            }
        }
        public async Task DeleteAsync(Guid id)
        {
            if(id == Guid.Empty) return;
            try
            {
                await context.Companies
                    .Where(c => c.Id == id)
                    .ExecuteDeleteAsync();
                Log.Information($"Успешно удалена организация {id}.");
            }
            catch (Exception ex)
            {
                Log.Error(ex, $"Ошибка при удалении организации {id}.");
            }
        }
    }
}
