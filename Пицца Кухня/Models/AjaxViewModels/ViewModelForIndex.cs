namespace Пицца_Кухня.Models.AjaxViewModels
{
    public class ViewModelForIndex
    {
        public uint IdOrderHeader { get; set; }
        public List<OrderCount>? MenuList { get; set; }=new List<OrderCount>();
        public string? OrderStatus { get; set; }

        public string? DateCreatedOrder { get; set; }



    }
}
