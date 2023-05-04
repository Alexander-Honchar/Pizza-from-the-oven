using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Pizza_WebAPI.Models.DTO
{
    public class MenuItemDTO
    {
        public uint Id { get; set; }
        
        public string? Name { get; set; }

        
        public double? Price { get; set; }

        
        public string? ImagePatch { get; set; }


        public string? Description { get; set; }

        public string? NameCategory { get; set; }




    }
}
