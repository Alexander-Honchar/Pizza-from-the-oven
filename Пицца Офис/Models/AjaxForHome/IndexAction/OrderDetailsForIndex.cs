using System.ComponentModel;

namespace Пицца_Офис.Models.AjaxForHome.IndexAction
{
    public class OrderDetailsForIndex
    {
        public uint IdOrderHeader { get; set; }
        public uint NumberOrder { get; set; }
        public string? OrderStatus { get; set; }
        public string? DateCreatedOrder { get; set; }

        public List<string>? MenuName { get; set; }
        
        public string? FirstName { get; set; }


       
        public string? LastName { get; set; }


        
        public string? PhoneNumber { get; set; }



        public string? Street { get; set; }

        
        public string? House { get; set; }

        public string? Entrance { get; set; }

        
        public string? Apartment { get; set; }

        
        public string? Floor { get; set; }


        public OrderDetailsForIndex()
        {
            MenuName= new List<string>();
        }
    }
}
