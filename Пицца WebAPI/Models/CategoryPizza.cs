using System.ComponentModel.DataAnnotations;

namespace Pizza_WebAPI.Models
{
    public class CategoryPizza
    {
        [Key]
        public uint Id { get; set; }
        [Required]
        public string? Name { get; set; }
    }
}
