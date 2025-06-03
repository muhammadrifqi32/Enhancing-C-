using FridayAssignments.Context;
using FridayAssignments.Models;
using FridayAssignments.Models.Dump;
using FridayAssignments.Models.Helper;
using FridayAssignments.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace FridayAssignments.Repositories
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly MyContext _myContext;

        public DepartmentRepository(MyContext myContext)
        {
            _myContext = myContext;
        }

        public async Task<List<Department>> GetAsync()
        {
            return await _myContext.Departments.ToListAsync();
        }

        public async Task<Department?> GetAsync(string deptId)
        {
            return await _myContext.Departments.FindAsync(deptId);
        }

        public async Task<bool> InsertAsync(Department department)
        {
            string nextDepartmentId = await GenerateDepartmentIDAsync();
            department.Dept_Id = nextDepartmentId;

            await _myContext.Departments.AddAsync(department);
            return await _myContext.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateAsync(Department department)
        {
            var existing = await _myContext.Departments.FindAsync(department.Dept_Id);
            if (existing == null) return false;

            existing.Name = department.Name;
            return await _myContext.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteAsync(string deptId)
        {
            var entity = await _myContext.Departments.FindAsync(deptId);
            if (entity == null) return false;

            _myContext.Departments.Remove(entity);
            return await _myContext.SaveChangesAsync() > 0;
        }

        private async Task<string> GenerateDepartmentIDAsync()
        {
            var lastId = await _myContext.Departments
                .OrderByDescending(d => d.Dept_Id)
                .Select(d => d.Dept_Id)
                .FirstOrDefaultAsync();

            int lastNumber = 0;
            if (!string.IsNullOrEmpty(lastId) && lastId.Length > 1)
                int.TryParse(lastId.Substring(1), out lastNumber);

            return $"D{lastNumber + 1:D3}";
        }

        public async Task<PagedResult<Department>> GetPagedAsync(JqueryDatatableParam param)
        {
            var query = _myContext.Departments.AsQueryable();

            if (!string.IsNullOrEmpty(param.Search?.Value))
            {
                var keyword = param.Search.Value.ToLower();
                query = query.Where(d =>
                    d.Dept_Id != null && d.Dept_Id.ToLower().Contains(keyword) ||
                    d.Name != null && d.Name.ToLower().Contains(keyword));
            }

            var recordsTotal = await _myContext.Departments.CountAsync();
            var recordsFiltered = await query.CountAsync();

            // Sorting
            query = param.Order.column switch
            {
                0 => param.Order.dir == "asc" ? query.OrderBy(d => d.Name) : query.OrderByDescending(d => d.Name),
                1 => param.Order.dir == "asc" ? query.OrderBy(d => d.Dept_Id) : query.OrderByDescending(d => d.Dept_Id),
                _ => query.OrderBy(d => d.Dept_Id)
            };

            var data = await query.Skip(param.start).Take(param.length).ToListAsync();

            return new PagedResult<Department>
            {
                RecordsTotal = recordsTotal,
                RecordsFiltered = recordsFiltered,
                Data = data
            };
        }
    }
}
