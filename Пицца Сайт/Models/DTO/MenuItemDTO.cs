using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;


namespace Пицца_Сайт.Models.DTO
{
    public class MenuItemDTO
    {
        public uint Id { get; set; }
        [Required]
        public string? Name { get; set; }

        [Required]
        public double? Price { get; set; }

        
        public string? ImagePatch { get; set; }


        public string? Description { get; set; }



        public string? NameCategory { get; set; }


		public uint ProductId { get; set; }

        public uint Count { get; set; } = 1;

       

	}
}
