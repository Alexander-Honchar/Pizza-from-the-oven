using System.ComponentModel.DataAnnotations;
using Pizza_WebAPI.Utillity;

namespace Pizza_WebAPI.Models.DTO
{
    public class SizePizzaDTO
    {
        public uint Id { get; set; }

        [Required]
        public string? Size { get; set; }


    }
}
