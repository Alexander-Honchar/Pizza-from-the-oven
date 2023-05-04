using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pizza_WebAPI.Models
{
	public class CategoryMenu
	{
		public uint Id { get; set; }


       public string? NameCategory { get; set; }
    }
}
