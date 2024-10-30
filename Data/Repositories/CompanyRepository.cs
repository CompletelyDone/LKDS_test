using Data.Abstractions;
using Data.Context;
using Microsoft.EntityFrameworkCore;
using Model;

namespace Data.Repositories
{
    public class CompanyRepository : IRepositoryBase<Company>
    {
        private readonly SQLiteDBContext context;
        public CompanyRepository(SQLiteDBContext context)
        {
            this.context = context;
        }
        public async Task<Company?> GetById(Guid id)
        {
            return await context.Companies
                .Include(c => c.Employees)
                .FirstOrDefaultAsync(c => c.Id == id);
        }
        public async Task<List<Company>> GetAllAsync()
        {
            return await context.Companies
                .AsNoTracking()
                .ToListAsync();
        }
        public async Task Create(Company company)
        {
            await context.Companies.AddAsync(company);
            await context.SaveChangesAsync();
        }
        public async Task Update(Company company)
        {
            await context.Companies
                .Where(c => c.Id == company.Id)
                .ExecuteUpdateAsync(s => s
                    .SetProperty(c => c.Title, company.Title));
        }
        public async Task Delete(Guid id)
        {
            await context.Companies
                .Where(c => c.Id == id)
                .ExecuteDeleteAsync();
        }
    }
}
