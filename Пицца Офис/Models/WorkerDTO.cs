using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace Пицца_Офис.Models
{
    public class WorkerDTO
    {

		public string? Id { get; set; }

		[DisplayName("Имя")]
		public string? FirstName { get; set; }
		[DisplayName("Фамилмя")]
		public string? LastName { get; set; }

		[DisplayName("Ник")]
		public string? UserName { get; set; }

		
		public string? RoleId { get; set; }

		[DisplayName("Должность")]
		public string? Role { get; set; }

        public IEnumerable<SelectListItem>? RoleList { get; set; }

    }
}
