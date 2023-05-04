namespace Пицца_Кухня.Models.AjaxViewModels
{
    public class ViewModelForOrder
    {
        public uint Id { get; set; }
        public uint NumberOrder { get; set; }
        public List<OrderCount>? MenuList { get; set; } = new List<OrderCount>();
        public string? OrderStatus { get; set; }
        public string? WishesClient { get; set; }

        public string? DateCreatedOrder { get; set; }
    }
}
