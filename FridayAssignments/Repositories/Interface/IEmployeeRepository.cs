using FridayAssignments.Models;

namespace FridayAssignments.Repositories.Interface
{
    public interface IEmployeeRepository
    {
        public IEnumerable<Employee> Get();
        public Employee Get(string NIK);
        public int Insert(Employee employee);
        public int Update(Employee employee);
        public int Delete(string NIK);
    }
}
