using Data.Abstractions;
using Microsoft.EntityFrameworkCore;
using Model;

namespace Data.Context
{
    public class SQLiteDBContext : DbContext, IDBContext
    {
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Company> Companies { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=LKDS.db");
        }
    }
}
