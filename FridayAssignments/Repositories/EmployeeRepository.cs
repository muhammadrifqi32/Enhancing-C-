using FridayAssignments.Context;
using FridayAssignments.Models;
using FridayAssignments.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace FridayAssignments.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly MyContext myContext;

        public EmployeeRepository(MyContext myContext)
        {
            this.myContext = myContext;
        }

        public int Delete(string NIK)
        {
            var findData = myContext.Employees.Find(NIK);
            if (findData != null)
            {
                findData.isActive = false;
            }
            var save = myContext.SaveChanges();
            return save;
        }

        public IEnumerable<Employee> Get()
        {
            return myContext.Employees.ToList();
        }

        public Employee Get(string NIK)
        {
            return myContext.Employees.Find(NIK);
        }

        public int Insert(Employee employee)
        {
            employee.NIK = GeneratedNIK();
            employee.Email = GeneratedEmail(employee.FirstName, employee.LastName);
            employee.isActive = true;
            myContext.Employees.Add(employee);
            var save = myContext.SaveChanges();
            return save;
        }

        public int Update(Employee employee)
        {
            myContext.Entry(employee).State = EntityState.Modified;
            var save = myContext.SaveChanges();
            return save;
        }

        private string GeneratedNIK()
        {
            var todaysDate = DateTime.Now.ToString("ddMMyy");
            var countEmployee = myContext.Employees.Count();
            if (countEmployee == 0)
            {
                return $"{todaysDate}{"001"}";
            }
            else
            {
                return $"{todaysDate}{countEmployee + 1:D3}";
            }
        }

        private string GeneratedEmail(string FirstName, string LastName)
        {
            string baseEmail = $"{FirstName.ToLower()}.{LastName.ToLower()}";
            string generatedEmail = $"{baseEmail}{"@berca.co.id"}";
            int counter = 1;

            // Check if the generated username already exists in the database
            while (myContext.Employees.Any(u => u.Email == generatedEmail))
            {
                generatedEmail = $"{baseEmail}{counter:D3}"; // Append a three-digit number
                counter++;

                // To avoid infinite loops, you can add a maximum number of retries here.
                if (counter > 999)
                {
                    throw new Exception("Unable to generate a unique username.");
                }
            }

            return generatedEmail;
        }

        public IEnumerable<object> GetActiveEmployee()
        {
            var getActiveEmployee = myContext.Employees.Include(d => d.Department).ToList().Where(e => e.isActive == true);
            var activeEmployee = getActiveEmployee.Select(a => new ActiveEmployeeVM
            {
                NIK = a.NIK,
                FirstName = a.FirstName,
                LastName = a.LastName,
                Email = a.Email,
                PhoneNumber = a.PhoneNumber,
                Address = a.Address,
                DepartmentName = a.Department.Name
            });
            return activeEmployee;
        }

        public IEnumerable<object> GetDeactiveEmployee()
        {
            var getDeactiveEmployee = myContext.Employees.Include(d => d.Department).ToList().Where(e => e.isActive == false);
            var deactiveEmployee = getDeactiveEmployee.Select(a => new DeactiveEmployeeVM
            {
                NIK = a.NIK,
                FirstName = a.FirstName,
                LastName = a.LastName,
                Email = a.Email,
                PhoneNumber = a.PhoneNumber,
                Address = a.Address,
                DepartmentName = a.Department.Name
            });
            return deactiveEmployee;
        }
        public IEnumerable<object> GetActiveEmployeePerDepartment(string DeptId)
        {
            var checkDeptId = myContext.Departments.Find(DeptId);
            if (checkDeptId == null)
            {
                return null;
            }
            var getActiveEmployee = myContext.Employees.Include(d => d.Department).ToList().Where(e => e.isActive == true && e.Dept_Id == DeptId);
            var activeEmployee = getActiveEmployee.Select(a => new ActiveEmployeeVM
            {
                NIK = a.NIK,
                FirstName = a.FirstName,
                LastName = a.LastName,
                Email = a.Email,
                PhoneNumber = a.PhoneNumber,
                Address = a.Address,
                DepartmentName = a.Department.Name
            });
            return activeEmployee;
        }
        public IEnumerable<object> GetDeactiveEmployeePerDepartment(string DeptId)
        {
            var checkDeptId = myContext.Departments.Find(DeptId);
            if (checkDeptId == null)
            {
                return null;
            }
            var getDeactiveEmployee = myContext.Employees.Include(d => d.Department).ToList().Where(e => e.isActive == false && e.Dept_Id == DeptId);
            var deactiveEmployee = getDeactiveEmployee.Select(a => new DeactiveEmployeeVM
            {
                NIK = a.NIK,
                FirstName = a.FirstName,
                LastName = a.LastName,
                Email = a.Email,
                PhoneNumber = a.PhoneNumber,
                Address = a.Address,
                DepartmentName = a.Department.Name
            });
            return deactiveEmployee;
        }

        public IEnumerable<object> GetTotalActivePerDepartment()
        {
            var deptCount = myContext.Employees.Where(e => e.isActive == true).GroupBy(e => e.Department.Name).Select(g => new GroupByCountDepartment
            {
                DepartmentName = g.Key,
                CountEmployee = g.Count(),
            }).ToList();
            return deptCount;
        }

        public IEnumerable<object> GetTotalDeactivePerDepartment()
        {
            var deptCount = myContext.Employees.Where(e => e.isActive == false).GroupBy(e => e.Department.Name).Select(g => new GroupByCountDepartment
            {
                DepartmentName = g.Key,
                CountEmployee = g.Count(),
            }).ToList();
            return deptCount;
        }
    }
}
