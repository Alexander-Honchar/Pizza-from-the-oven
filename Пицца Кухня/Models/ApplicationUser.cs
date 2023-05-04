using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Пицца_Кухня.Models
{
    public class ApplicationUser:IdentityUser
    {
        [Required]
        public string? FirstName { get; set; }

        public string? LastName { get; set; }


        public string? Adress { get; set; }


        public string? GeoLocation { get; set; }
    }
}