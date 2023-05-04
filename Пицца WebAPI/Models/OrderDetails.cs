using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pizza_WebAPI.Models
{
    public class OrderDetails
    {
        [Key]
        public uint Id { get; set; }



        public uint OrderHeaderId { get; set; }
        [ForeignKey("OrderHeaderId")]
        public OrderHeader? OrderHeader { get; set; }


        public uint MenuId { get; set; }
        [ForeignKey("MenuId")]
        public MenuItem? MenuItem { get; set; }



        
        public string? MenuName { get; set; }

        public uint ProductId { get; set; }

        public string? NameCategory { get; set; }

        public uint Count { get; set; }

        public double Price { get; set; }


    }
}
