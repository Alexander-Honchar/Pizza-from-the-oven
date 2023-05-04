using Microsoft.AspNetCore.Identity;

namespace Pizza_WebAPI.Models
{
    public class Workers:IdentityUser
    {
        public string? FirstName { get; set; }

        public string? LastName { get; set; }
    }
}
