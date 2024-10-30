using System.ComponentModel.DataAnnotations;

namespace Manager.Models
{
    public class LoginAcc
    {
        [Required]
        public string? User_Name { get; set; }

        [Required]
        public string? Password { get; set; }
    }
}
