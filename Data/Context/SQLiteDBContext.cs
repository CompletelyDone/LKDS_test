using Data.Abstractions;
using Microsoft.EntityFrameworkCore;
using Model;

namespace Data.Context
{
    public class SQLiteDBContext : DbContext, IDBContext
    {
        public SQLiteDBContext()
        {
            Database.EnsureCreated();
        }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Company> Companies { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=LKDS.db");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Company>(entity =>
            {
                entity.HasKey(c => c.Id); 
                entity.Property(c => c.Title)
                      .IsRequired() 
                      .HasMaxLength(Company.MAX_TITLE_LENGTH); 
            });

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.HasKey(e => e.Id); 
                entity.Property(e => e.LastName)
                      .IsRequired() 
                      .HasMaxLength(Employee.MAX_FIELD_LENGTH);
                entity.Property(e => e.FirstName)
                      .IsRequired() 
                      .HasMaxLength(Employee.MAX_FIELD_LENGTH); 
                entity.Property(e => e.Patronymic)
                      .HasMaxLength(Employee.MAX_FIELD_LENGTH); 
                entity.Property(e => e.PhotoPath)
                      .HasMaxLength(255); 

                entity.HasOne(e => e.Company)
                      .WithMany(c => c.Employees)
                      .HasForeignKey(e => e.CompanyId)
                      .OnDelete(DeleteBehavior.Cascade); 
            });
        }
    }
}
