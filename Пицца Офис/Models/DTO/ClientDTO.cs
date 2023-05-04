using System.ComponentModel.DataAnnotations;

namespace Пицца_Офис.Models.DTO
{
    public class ClientDTO
    {
        public uint Id { get; set; }

        
        public string? FirstName { get; set; }



        public string? LastName { get; set; }


        
        public string? PhoneNumber { get; set; }
    }
}
