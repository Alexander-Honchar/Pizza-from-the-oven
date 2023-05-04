using System.ComponentModel.DataAnnotations;

namespace Пицца_Офис.Models.Authorization
{
    public class LoginRequestDTO
    {
        [Required]
        public string? Username { get; set; }
        [Required]
        public string? Password { get; set; }
    }
}
