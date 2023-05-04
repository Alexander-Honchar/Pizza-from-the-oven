using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Пицца_Сайт.Models
{
    public class OrderHeaderDTO
    {
        
        public uint NumberOrder { get; set; }


        [Required]
        [DisplayName("Имя")]
        public string? FirstName { get; set; }


        [DisplayName("Фамилия")]
        public string? LastName { get; set; }


        [Phone]
        [Required]
        [DisplayName("Номер телефона")]
        public string? PhoneNumber { get; set; }


        [EmailAddress]
        [DisplayName("E-mail")]
        public string? Email { get; set; }


        [DisplayName("Улица")]
        public string? Street { get; set; }


        [DisplayName("Дом")]
        public string? House { get; set; }


        [DisplayName("Подъезд")]
        public string? Entrance { get; set; }


        [DisplayName("Квартира")]
        public string? Apartment { get; set; }


        [DisplayName("Этаж")]
        public string? Floor { get; set; }




        [DisplayName("Пожелания клиента")]
        public string? WishesClient { get; set; }

        

        public OrderHeaderDTO()
        {
            NumberOrder = Convert.ToUInt32(new Random().Next(10000, 50000));
        }


        


    }
}
