namespace Pizza_WebAPI.Models
{
    public class StatusOrder
    {
        public uint NumberOrder{ get; set; }
        public string? Name { get; set; }

        public bool IsStatus { get; set; }

        public string? OrderStatus { get; set; }

    }
}
