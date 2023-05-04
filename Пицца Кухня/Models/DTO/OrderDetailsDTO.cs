using System.ComponentModel.DataAnnotations.Schema;

namespace Пицца_Кухня.Models.DTO
{
    public class OrderDetailsDTO
    {
        public uint Id { get; set; }


        public uint OrderHeaderId { get; set; }
        [ForeignKey("OrderHeaderId")]
        public OrderHeaderDTO? OrderHeader { get; set; }


        public uint MenuId { get; set; }
        [ForeignKey("MenuId")]
        public MenuItemDTO? MenuItem { get; set; }


        public string? MenuName { get; set; }

        public uint ProductId { get; set; }

        public string? NameCategory { get; set; }

        public uint Count { get; set; }

        public double Price { get; set; }
    }
}
