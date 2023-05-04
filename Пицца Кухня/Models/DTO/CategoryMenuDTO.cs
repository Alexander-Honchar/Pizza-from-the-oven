using System.ComponentModel;

namespace Пицца_Кухня.Models.DTO
{
    public class CategoryMenuDTO
    {
        public uint Id { get; set; }

        [DisplayName("Категория")]
        public string? NameCategory { get; set; }
    }
}
