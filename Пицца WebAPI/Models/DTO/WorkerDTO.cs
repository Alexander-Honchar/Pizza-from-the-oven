using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pizza_WebAPI.Models.DTO
{
    public class WorkerDTO
    {
        public string? Id { get; set; }
        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public string? UserName { get; set; }

        public string? RoleId { get; set; }
        
        public string? Role { get; set; }
    }
}
