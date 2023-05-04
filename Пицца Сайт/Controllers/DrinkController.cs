using Microsoft.AspNetCore.Mvc;

namespace Пицца_Сайт.Controllers
{
	public class DrinkController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
