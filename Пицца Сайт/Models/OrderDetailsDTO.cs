using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Пицца_Сайт.Models.DTO;

namespace Пицца_Сайт.Models
{
    public class OrderDetailsDTO
    {

        public uint MenuId { get; set; }
        public string? MenuName { get; set; }

        public uint ProductId { get; set; }

        public string? NameCategory{ get; set; }

		public uint Count { get; set; }

        public double Price { get; set; }

        

    }
}
