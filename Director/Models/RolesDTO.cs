using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations.Schema;

namespace Director.Models
{
    public class RolesDTO
    {
        public string? RoleId { get; set; }
        [ForeignKey("RoleId")]
        public IdentityRole? Role { get; set; }
        
        public IEnumerable<SelectListItem>? RoleList { get; set; }
    }
}
