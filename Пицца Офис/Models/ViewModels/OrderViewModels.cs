using Пицца_Офис.Models.DTO;

namespace Пицца_Офис.Models.ViewModels
{
    public class OrderViewModels
    {
        public OrderHeaderDTO? OrderHeader { get; set; }

        public List<OrderDetailsDTO>? OrderDetailsList { get; set; }

        public double TotalSumma { get; set; }

        public string? DateCreatedOrder { get; set; }

        public OrderViewModels()
        {
            OrderDetailsList = new List<OrderDetailsDTO>();
            OrderHeader = new OrderHeaderDTO();
        }

    }
}
