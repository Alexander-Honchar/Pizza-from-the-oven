using System.ComponentModel.DataAnnotations;

namespace Director.Models.Authorization
{
    public class LoginRequestDTO
    {
        [Required]
        public string? Username { get; set; }
        [Required]
        public string? Password { get; set; }
    }
}
