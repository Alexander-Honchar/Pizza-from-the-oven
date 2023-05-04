using System.ComponentModel.DataAnnotations;

namespace Pizza_WebAPI.Models.DTO
{
    public class ClientDTO
    {
        public uint Id { get; set; }

        [Required]
        public string? FirstName { get; set; }



        public string? LastName { get; set; }


        [Required]
        public string? PhoneNumber { get; set; }
    }
}
