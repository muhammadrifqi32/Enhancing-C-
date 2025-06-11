using System.ComponentModel.DataAnnotations;

namespace FridayAssignments.Models.DTOs
{
    public class AccountsDTO
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
