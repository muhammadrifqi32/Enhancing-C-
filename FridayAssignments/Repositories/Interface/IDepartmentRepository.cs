using FridayAssignments.Models;

namespace FridayAssignments.Repositories.Interface
{
    public interface IDepartmentRepository
    {
        public IEnumerable<Department> Get();
        //public PagedData<Department> Get(Parameters parameters);
        public Department Get(string Dept_Id);
        public int Insert(Department department);
        public int Update(Department department);
        public int Delete(string Dept_Id);

    }
}
