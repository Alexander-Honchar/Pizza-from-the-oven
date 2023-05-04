using System.ComponentModel;

namespace Пицца_Кухня.Models.DTO
{
    public class PizzaKingSizeDTO
    {
        public uint Id { get; set; }

        [DisplayName("Название")]
        public string? Name { get; set; }

        [DisplayName("Цена")]
        public double Price { get; set; }

        [DisplayName("Описание")]
        public string? Description { get; set; }

        [DisplayName("Фото")]
        public string? ImagePath { get; set; }
    }
}
