using System.ComponentModel;

namespace Пицца_Офис.Models.DTO
{
    public class CategoryMenuDTO
    {
        public uint Id { get; set; }

        [DisplayName("Категория")]
        public string? NameCategory { get; set; }
    }
}
