
using Пицца_Сайт.Models.DTO;

namespace Пицца_Сайт.Models.ViewModels
{
	public class AllPizzaViewModels
	{

		public MenuItemDTO? MenuItem { get; set; }

		public IEnumerable<MenuItemDTO>? MenuItemList { get; set; }

		public IEnumerable<PizzaDTO>? PizzaList { get; set; }
		


	}
}
