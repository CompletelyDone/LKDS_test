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
                      .HasMaxLength(100); 
            });

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.HasKey(e => e.Id); 
                entity.Property(e => e.LastName)
                      .IsRequired() 
                      .HasMaxLength(50);
                entity.Property(e => e.FirstName)
                      .IsRequired() 
                      .HasMaxLength(50); 
                entity.Property(e => e.Patronymic)
                      .HasMaxLength(50); 
                entity.Property(e => e.PhotoPath)
                      .HasMaxLength(255); 

                entity.HasOne<Company>()
                      .WithMany(c => c.Employees)
                      .HasForeignKey(e => e.CompanyId)
                      .OnDelete(DeleteBehavior.Cascade); 
            });
        }
    }
}
