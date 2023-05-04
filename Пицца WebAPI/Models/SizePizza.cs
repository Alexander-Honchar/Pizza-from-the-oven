using System.ComponentModel.DataAnnotations;
using Pizza_WebAPI.Utillity;

namespace Pizza_WebAPI.Models
{
    public class SizePizza
    {
        [Key]
        public uint Id { get; set; }

        [Required]
        public string? Size { get; set; }

        //    public string? Size_24 { get; set; } = StaticDetails.Size_24;
        //    public string? Size_30 { get; set; } = StaticDetails.Size_30;
        //    public string? Size_35 { get; set; } = StaticDetails.Size_35;
        //    public string? Size_40 { get; set; } = StaticDetails.Size_40;
        //    public string? Size_50 { get; set; } = StaticDetails.Size_50;
        //    public string? Size_100 { get; set; } = StaticDetails.Size_100;

    }
}
