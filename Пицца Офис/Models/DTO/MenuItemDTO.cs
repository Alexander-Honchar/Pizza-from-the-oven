using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Пицца_Офис.Models.DTO
{
    public class MenuItemDTO
    {
        public uint Id { get; set; }
        
        [DisplayName("Название")]
        public string? Name { get; set; }

        [DisplayName("Цена")]
        public double? Price { get; set; }

        [DisplayName("Фото")]
        public string? ImagePatch { get; set; }

        [DisplayName("Описание")]
        public string? Description { get; set; }


        public string? NameCategory { get; set; }




        [DisplayName("Пицца KingSize")]
        public uint PizzaKingSizeId { get; set; }
        [ForeignKey("PizzaKingSizeId ")]
        public PizzaKingSizeDTO? PizzaKingSize { get; set; }


        [DisplayName("Категория меню")]
        public uint CategoryMenuId { get; set; }
        [ForeignKey("CategoryMenuId ")]
        public CategoryMenuDTO? CategoryMenu { get; set; }


    }
}
