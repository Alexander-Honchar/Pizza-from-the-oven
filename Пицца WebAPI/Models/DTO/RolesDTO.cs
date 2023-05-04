using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pizza_WebAPI.Models.DTO
{
    public class RolesDTO
    {
        public string? RoleId { get; set; }
        
        public string? Role { get; set; }
        
        public IEnumerable<SelectListItem>? RoleList { get; set; }
    }
}
