namespace Pizza_WebAPI.Models
{
    public class PizzaKingSize
    {
        public uint Id { get; set; }
        public string? Name { get; set; }
        public double Price { get; set; }
        
        public string? Description { get; set; }
        public string? ImagePath { get; set; }

    }
}
