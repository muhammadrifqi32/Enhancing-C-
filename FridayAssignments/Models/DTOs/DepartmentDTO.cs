using System.ComponentModel.DataAnnotations;

namespace FridayAssignments.Models.DTOs
{
    namespace FridayAssignments.DTOs
    {
        public class DepartmentDTO
        {
            [Required(ErrorMessage = "ID Departemen wajib diisi untuk update")]
            public string? Dept_Id { get; set; }

            [Required(ErrorMessage = "Nama Departemen wajib diisi")]
            [MaxLength(50, ErrorMessage = "Nama maksimal 50 karakter")]
            public string? Name { get; set; }
        }

        public class DepartmentCreateDTO
        {
            [Required(ErrorMessage = "Nama Departemen wajib diisi")]
            [MaxLength(50, ErrorMessage = "Nama maksimal 50 karakter")]
            public string? Name { get; set; }
        }
    }

}
