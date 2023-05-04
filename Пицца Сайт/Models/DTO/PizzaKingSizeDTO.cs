namespace Пицца_Сайт.Models.DTO
{
    public class PizzaKingSizeDTO
    {
        public uint Id { get; set; }
        public string? Name { get; set; }
        public double Price { get; set; }

        public string? Description { get; set; }
        public string? ImagePath { get; set; }
    }
}
