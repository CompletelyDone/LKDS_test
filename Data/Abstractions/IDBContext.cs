using Microsoft.EntityFrameworkCore;
using Model;

namespace Data.Abstractions
{
    public interface IDBContext
    {
        DbSet<Company> Companies { get; set; }
        DbSet<Employee> Employees { get; set; }
    }
}