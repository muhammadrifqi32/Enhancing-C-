using FridayAssignments.Models;
using Microsoft.EntityFrameworkCore;

namespace FridayAssignments.Context
{
    public class MyContext : DbContext
    {
        public MyContext(DbContextOptions<MyContext> options) : base(options)
        {

        }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Account> Accounts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //one-to-one Employee && Account
            modelBuilder.Entity<Employee>()
                .HasOne(e => e.Account)
                .WithOne(a => a.Employee)
                .HasForeignKey<Employee>(e => e.Email)           
                .HasPrincipalKey<Account>(a => a.Email)          
                .OnDelete(DeleteBehavior.Cascade);               

            modelBuilder.Entity<Employee>()
                .Property(e => e.Email)
                .IsRequired(); // Email harus ada

            modelBuilder.Entity<Account>()
                .Property(a => a.Email)
                .IsRequired(); // Email harus ada
        }
    }
}
