using FridayAssignments.Models;
using FridayAssignments.Models.DTOs;

namespace FridayAssignments.Repositories.Interface
{
    public interface IEmployeeRepository
    {
        Task<IEnumerable<Employee>> GetAsync();
        Task<Employee?> GetAsync(string NIK);
        Task<int> InsertAsync(Employee employee);
        Task<int> UpdateAsync(Employee employee);
        Task<int> DeleteAsync(string NIK);
        Task<List<EmployeePerDepartmentDto>> GetEmployeeCountPerDepartmentAsync();

    }
}
