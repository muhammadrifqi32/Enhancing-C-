using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace FridayAssignments.Models
{
    public class ActiveEmployeeVM
    {
        public string NIK { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string DepartmentName { get; set; }
    }
    public class DeactiveEmployeeVM
    {
        public string NIK { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string DepartmentName { get; set; }
    }
    public class GroupByCountDepartment
    {
        public string DepartmentName { get; set; }
        public int CountEmployee { get; set; }
    }
}
