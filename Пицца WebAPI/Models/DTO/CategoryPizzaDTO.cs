using System.ComponentModel.DataAnnotations;

namespace Pizza_WebAPI.Models.DTO
{
    public class CategoryPizzaDTO
    {
        public uint Id { get; set; }
        public string? Name { get; set; }
    }
}
