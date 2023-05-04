using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Pizza_WebAPI.Models.DTO;

namespace Pizza_WebAPI.Models
{
    public class MenuItem
    {
        [Key]
        public uint Id { get; set; }


        //[Required]
        //[DisplayName("Название")]
        public string? Name { get; set; }

        //[Required]
        //[DisplayName("Цена")]
        public double? Price { get; set; }

        //[Required]
        //[DisplayName("Фото")]
        public string? ImagePatch { get; set; }

        //[DisplayName("Описание")]
        public string? Description { get; set; }


        //public uint ProductId { get; set; }


        public string? NameCategory { get; set; }




    }
}
