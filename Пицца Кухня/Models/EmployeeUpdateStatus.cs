using System.ComponentModel.DataAnnotations;

namespace Пицца_Кухня.Models
{
    public class EmployeeUpdateStatus
    {
        [Required]
        public string? Id { get; set; }
        [Required]
        public string? JobTitle{ get; set; }
    }
}
