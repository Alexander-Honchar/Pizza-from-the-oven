namespace Пицца_Сайт.Models
{
    public class StatusOrder
    {
        public int NumberOrder{ get; set; }
        public string? Name { get; set; }

        public bool IsStatus { get; set; }
        public string? OrderStatus { get; set; }
    }
}
