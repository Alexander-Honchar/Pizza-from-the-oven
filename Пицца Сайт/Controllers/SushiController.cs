using Microsoft.AspNetCore.Mvc;

namespace Пицца_Сайт.Controllers
{
	public class SushiController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
