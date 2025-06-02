using System.ComponentModel.DataAnnotations;

namespace FridayAssignments.Models
{
    public class Department
    {
        [Key]
        public string? Dept_Id { get; set; }
        public string? Name { get; set; }
    }
}
