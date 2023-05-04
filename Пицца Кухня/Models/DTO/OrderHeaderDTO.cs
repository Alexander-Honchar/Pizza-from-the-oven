using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;

namespace Пицца_Кухня.Models.DTO
{
    public class OrderHeaderDTO
    {
        public uint Id { get; set; }




        public string? ManagerId { get; set; }
        [ForeignKey("ManagerId")]
        public ApplicationUser? Manager { get; set; }



        public string? СookId { get; set; }
        [ForeignKey("СookId")]
        public ApplicationUser? Сook { get; set; }



        //public uint ClientId { get; set; }
        //[ForeignKey("ClientId")]
        //public ClientDTO? Client { get; set; }



        public uint NumberOrder { get; set; }
        public string? OrderStatus { get; set; }


        [DisplayName("Имя")]
        public string? FirstName { get; set; }


        [DisplayName("Фамилмя")]
        public string? LastName { get; set; }


        [DisplayName("Телефон")]
        public string? PhoneNumber { get; set; }


        [DisplayName("Почта")]
        public string? Email { get; set; }

        [DisplayName("Улица")]
        public string? Street { get; set; }

        [DisplayName("Номер дома")]
        public string? House { get; set; }

        [DisplayName("Подъезд")]
        public string? Entrance { get; set; }

        [DisplayName("Квартира")]
        public string? Apartment { get; set; }

        [DisplayName("Этаж")]
        public string? Floor { get; set; }



        [DisplayName("Пожелания клиента")]
        public string? WishesClient { get; set; }

        public string? DateClosedOrder { get; set; }

    }
}
