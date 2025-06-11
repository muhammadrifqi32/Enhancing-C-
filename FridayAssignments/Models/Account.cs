using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace FridayAssignments.Models
{
    public class Account
    {
        [Key]
        public string Email { get; set; } 
        public string Password { get; set; }

        // Relasi ke Employee melalui email
        public virtual Employee Employee { get; set; } 
    }
}
