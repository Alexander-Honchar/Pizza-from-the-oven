using System.ComponentModel.DataAnnotations;
using Пицца_Сайт.Models.DTO;

namespace Пицца_Сайт.Models
{
    public class Cart
    {
        public uint MenuId { get; set; }
        public uint ProductId { get; set; }
        public uint Count { get; set; }
        public string? MenuNameCategory { get; set; }
        public string? Name { get; set; }
        public double Price { get; set; }
        public string? ImagePatch { get; set; }

        public double? CartTotal { get; set; }

        public PizzaDTO? Pizza { get; set; }
		public PizzaKingSizeDTO? PizzaKingSize { get; set; }
	}
}
