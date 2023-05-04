using System.ComponentModel.DataAnnotations;

namespace Pizza_WebAPI.Models
{
    public class EmployeeUpdateStatus
    {
        [Required]
        public string? Id { get; set; }
        [Required]
        public string? JobTitle{ get; set; }
    }
}
