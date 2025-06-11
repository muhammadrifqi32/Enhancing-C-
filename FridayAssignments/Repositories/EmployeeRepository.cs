using FridayAssignments.Context;
using FridayAssignments.Models;
using FridayAssignments.Models.DTOs;
using FridayAssignments.Repositories.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace FridayAssignments.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly MyContext _myContext;

        public EmployeeRepository(MyContext myContext)
        {
            this._myContext = myContext;
        }

        public async Task<int> DeleteAsync(string NIK)
        {
            var findData = await _myContext.Employees.FindAsync(NIK);
            if (findData == null) return 0;

            findData.isActive = false;
            return await _myContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Employee>> GetAsync()
        {
            return await _myContext.Employees.Include(e => e.Department).Where(e => e.isActive == true).ToListAsync();
        }

        public async Task<Employee?> GetAsync(string NIK)
        {
            return await _myContext.Employees.FindAsync(NIK);
        }

        private async Task<string> GeneratedNIKAsync()
        {
            var todaysDate = DateTime.Now.ToString("ddMMyy");
            var countEmployee = await _myContext.Employees.CountAsync();
            return countEmployee == 0 ? $"{todaysDate}001" : $"{todaysDate}{countEmployee + 1:D3}";
        }

        private async Task<string> GeneratedEmailAsync(string FirstName, string LastName)
        {
            // Hapus semua spasi dan ubah ke lowercase
            string cleanFirstName = FirstName.ToLower().Replace(" ", "");
            string cleanLastName = LastName.ToLower().Replace(" ", "");

            string baseEmail = $"{cleanFirstName}.{cleanLastName}";
            string generatedEmail = $"{baseEmail}@berca.co.id";
            int counter = 1;

            while (await _myContext.Accounts.AnyAsync(u => u.Email == generatedEmail))
            {
                generatedEmail = $"{baseEmail}{counter:D3}@berca.co.id";
                counter++;
                if (counter > 999)
                {
                    throw new Exception("Unable to generate a unique username.");
                }
            }

            return generatedEmail;
        }

        private string HashPassword(string password)
        {
            // Menggunakan PasswordHasher dari ASP.NET Core Identity
            var passwordHasher = new PasswordHasher<Account>();
            return passwordHasher.HashPassword(new Account(), password); // null karena tidak ada context user
        }

        public async Task<int> InsertAsync(Employee employee)
        {
            employee.NIK = await GeneratedNIKAsync();
            employee.isActive = true;

            var generatedEmail = await GeneratedEmailAsync(employee.FirstName, employee.LastName);
            employee.Email = generatedEmail;

            var account = new Account
            {
                Email = generatedEmail, // harus sama
                Password = HashPassword("Pa5sword!")
            };
            employee.Account = account;

            await _myContext.Employees.AddAsync(employee);
            return await _myContext.SaveChangesAsync();
        }

        public async Task<int> UpdateAsync(Employee employee)
        {
            _myContext.Entry(employee).State = EntityState.Modified;
            return await _myContext.SaveChangesAsync();
        }

        public async Task<List<EmployeePerDepartmentDto>> GetEmployeeCountPerDepartmentAsync()
        {
            return await _myContext.Employees
        .Where(e => e.isActive == true)
        .GroupBy(e => e.Department!.Name)
        .Select(g => new EmployeePerDepartmentDto
        {
            DepartmentName = g.Key,
            TotalEmployees = g.Count()
        }).ToListAsync();
        }
    }
}
