using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;

namespace Director.Models.Authorization
{
    public class RegistrationRequestDTo
    {
        [Required]
        [DisplayName("Никнейм")]
        public string? UserName { get; set; }


        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [DisplayName("Пароль")]
        public string? Password { get; set; }


        [Required]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        [DisplayName("Подтвердить Пароль")]
        public string? ConfirmPassword { get; set; }


        [DisplayName("Имя")]
        public string? FirstName { get; set; }
        [DisplayName("Фамилия")]
        public string? LastName { get; set; }

        public string? RoleId { get; set; }
        [ForeignKey("RoleId")]
        public IdentityRole? Role { get; set; }

        public IEnumerable<SelectListItem>? RoleList { get; set; }

    }
}
