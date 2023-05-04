using Microsoft.AspNetCore.Identity;

namespace Pizza_WebAPI.Models.DTO.Authorization
{
    public class RegistrationRequestDTo
    {
        public string? UserName { get; set; }
        public string? Password { get; set; }
        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public string? RoleId { get; set; }


    }
}
