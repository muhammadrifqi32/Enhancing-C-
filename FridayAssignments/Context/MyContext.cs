using FridayAssignments.Models;
using Microsoft.EntityFrameworkCore;

namespace FridayAssignments.Context
{
    public class MyContext : DbContext
    {
        public MyContext(DbContextOptions<MyContext> options) : base(options)
        {

        }

        // DbSet for each of your models
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Department> Departments { get; set; }
    }
}
