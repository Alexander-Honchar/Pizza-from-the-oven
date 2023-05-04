using System.ComponentModel.DataAnnotations;

namespace Pizza_WebAPI.Models
{
    public class Client
    {
        [Key]
        public uint Id { get; set; }

        [Required]
        public string? FirstName { get; set; }



        public string? LastName { get; set; }


        [Required]
        public string? PhoneNumber { get; set; }
    }
}
