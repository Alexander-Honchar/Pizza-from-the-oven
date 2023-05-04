using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pizza_WebAPI.Models
{
    public class Pizza
    {
        [Key]
        public uint Id { get; set; }
        [Required]
        public string? Name { get; set; }

        [Required]
        public double? Price { get; set; }

        [Required]
        public uint SizePizzaId { get; set; }
        [ForeignKey("SizePizzaId")]
        public SizePizza? SizePizza { get; set; }



        [Required]
        public uint CategoryPizzaId { get; set; }
        [ForeignKey("CategoryPizzaId")]
        public CategoryPizza? CategoryPizza { get; set; }


    }
}
