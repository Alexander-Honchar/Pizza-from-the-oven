using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Пицца_Офис.Models.DTO
{
    public class PizzaDTO
    {
        public uint Id { get; set; }

		[Required]
        [DisplayName("Название пиццы")]
		public string? Name { get; set; }

        [Required]
        [DisplayName("Цена пиццы")]
        public double? Price { get; set; }


        [Required]
        [DisplayName("Диаметр пиццы")]
        public uint SizePizzaId { get; set; }
        [ForeignKey("SizePizzaId")]
        public SizePizzaDTO? SizePizza { get; set; }



        [Required]
        [DisplayName("Категория пиццы")]
        public uint CategoryPizzaId { get; set; }
        [ForeignKey("CategoryId")]
        public CategoryPizzaDTO? CategoryPizza { get; set; }
    }
}
