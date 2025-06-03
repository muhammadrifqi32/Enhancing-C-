using System.ComponentModel.DataAnnotations;

namespace FridayAssignments.Models.DTOs
{
    public class EmployeeDto
    {
        public string? NIK { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Address { get; set; }
        public string? DepartmentName { get; set; }
        public bool? isActive { get; set; }
    }

    public class EmployeePostDto
    {
        [Required(ErrorMessage = "Nama depan wajib diisi")]
        [MaxLength(50, ErrorMessage = "Nama depan maksimal 50 karakter")]
        public string? FirstName { get; set; }

        [Required(ErrorMessage = "Nama belakang wajib diisi")]
        [MaxLength(50, ErrorMessage = "Nama belakang maksimal 50 karakter")]
        public string? LastName { get; set; }

        [Required(ErrorMessage = "Nomor telepon wajib diisi")]
        [Phone(ErrorMessage = "Format nomor telepon tidak valid")]
        [MaxLength(15, ErrorMessage = "Nomor telepon maksimal 15 karakter")]
        public string? PhoneNumber { get; set; }

        [Required(ErrorMessage = "Alamat wajib diisi")]
        [MaxLength(200, ErrorMessage = "Alamat maksimal 200 karakter")]
        public string? Address { get; set; }

        [Required(ErrorMessage = "Departemen wajib dipilih")]
        public string? Dept_Id { get; set; }
    }

    public class EmployeePutDto
    {
        [Required(ErrorMessage = "NIK wajib diisi")]
        public string? NIK { get; set; }

        [Required(ErrorMessage = "Nama depan wajib diisi")]
        [MaxLength(50, ErrorMessage = "Nama depan maksimal 50 karakter")]
        public string? FirstName { get; set; }

        [Required(ErrorMessage = "Nama belakang wajib diisi")]
        [MaxLength(50, ErrorMessage = "Nama belakang maksimal 50 karakter")]
        public string? LastName { get; set; }

        [Required(ErrorMessage = "Nomor telepon wajib diisi")]
        [Phone(ErrorMessage = "Format nomor telepon tidak valid")]
        [MaxLength(15, ErrorMessage = "Nomor telepon maksimal 15 karakter")]
        public string? PhoneNumber { get; set; }

        [Required(ErrorMessage = "Alamat wajib diisi")]
        [MaxLength(200, ErrorMessage = "Alamat maksimal 200 karakter")]
        public string? Address { get; set; }

        [Required(ErrorMessage = "Departemen wajib dipilih")]
        public string? Dept_Id { get; set; }
    }

}
