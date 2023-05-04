namespace Director.Models
{
    public class OrderViewForDirectorDTO
    {
        public uint IdOrderHeader { get; set; }
        public uint NumberOrder { get; set; }
        public string? DateCreateOrder { get; set; }
        public List<string>? MenuName { get; set; }
        public string? Manager { get; set; }
        public string? Cook { get; set; }
        public string? OrderStatus { get; set; }
    }
}
