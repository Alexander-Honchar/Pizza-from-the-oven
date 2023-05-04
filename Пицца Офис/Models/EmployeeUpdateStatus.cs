using System.ComponentModel.DataAnnotations;

namespace Пицца_Офис.Models
{
    public class EmployeeUpdateStatus
    {
        [Required]
        public string? Id { get; set; }
        [Required]
        public string? JobTitle{ get; set; }
    }
}
