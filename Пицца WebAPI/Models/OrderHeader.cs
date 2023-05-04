using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pizza_WebAPI.Models
{
    public class OrderHeader
    {
        [Key]
        public uint Id { get; set; }




        public string? ManagerId { get; set; }
        [ForeignKey("ManagerId")]
        public Workers? Manager { get; set; }



        public string? СookId { get; set; }
        [ForeignKey("СookId")]
        public Workers? Сook { get; set; }




        public uint ClientId { get; set; }
        [ForeignKey("ClientId")]
        public Client? Client { get; set; }



        public uint NumberOrder { get; set; }

        public string? OrderStatus { get; set; }


        public string? FirstName { get; set; }


        public string? LastName { get; set; }


         
        public string? PhoneNumber { get; set; }


         
        public string? Email { get; set; }


        public string? Street { get; set; }


        public string? House { get; set; }


        public string? Entrance { get; set; }


        public string? Apartment { get; set; }


        public string? Floor { get; set; }


        public string? WishesClient { get; set; }


        public double TotalSumma { get; set; }
        public string? DateCreatedOrder { get; set; }
        public string? DateClosedOrder { get; set; }

    }
}
