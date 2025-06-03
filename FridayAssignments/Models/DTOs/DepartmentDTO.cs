using System.ComponentModel.DataAnnotations;

namespace FridayAssignments.Models.DTOs
{
    namespace FridayAssignments.DTOs
    {
        public class DepartmentDto
        {
            public string? Dept_Id { get; set; }
            public string? Name { get; set; }
        }

        public class DepartmentPostDto
        {
            [Required(ErrorMessage = "Nama Departemen wajib diisi")]
            [MaxLength(50, ErrorMessage = "Nama maksimal 50 karakter")]
            public string? Name { get; set; }
        }

        public class DepartmentPutDto
        {
            [Required(ErrorMessage = "ID Departemen wajib diisi")]
            public string? Dept_Id { get; set; }

            [Required(ErrorMessage = "Nama Departemen wajib diisi")]
            [MaxLength(50, ErrorMessage = "Nama maksimal 50 karakter")]
            public string? Name { get; set; }
        }
    }

}
