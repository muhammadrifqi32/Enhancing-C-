using FridayAssignments.Models;
using FridayAssignments.Models.Dump;
using FridayAssignments.Models.Helper;

namespace FridayAssignments.Repositories.Interface
{
    public interface IDepartmentRepository
    {
        Task<List<Department>> GetAsync();
        Task<Department?> GetAsync(string deptId);
        Task<bool> InsertAsync(Department department);
        Task<bool> UpdateAsync(Department department);
        Task<bool> DeleteAsync(string deptId);
        Task<PagedResult<Department>> GetPagedAsync(JqueryDatatableParam param);

    }
}
