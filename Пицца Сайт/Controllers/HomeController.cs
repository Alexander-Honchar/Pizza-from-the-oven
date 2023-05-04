using Hanssens.Net;

using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.JSInterop;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;

using Пицца_Сайт.Models;
using Пицца_Сайт.Models.DTO;
using Пицца_Сайт.Models.ViewModels;
using Пицца_Сайт.Services.Metods;


namespace Пицца_Сайт.Controllers
{
    public class HomeController : Controller
    {
		

		public APIResponse _response;
		readonly IMetods _metods;

		public List<MenuItemDTO> menuItemDTOList;
		public IEnumerable<PizzaDTO> pizzaDTOList;
        public AllPizzaViewModels allPizza;
		public MenuItemDTO menuItem;


		public HomeController( IMetods metods)
        {
			_metods = metods;
			_response= new APIResponse();
            menuItemDTOList= new List<MenuItemDTO>();
            pizzaDTOList= new List<PizzaDTO>();
            allPizza= new AllPizzaViewModels();
            menuItem =new MenuItemDTO();


		}


		#region Index Get-Post

		public async Task<IActionResult> Index()
        {

			var listNameCategoryMenu = await _metods.GetListNameCategoryMenuAsync<MenuItemDTO>("Pizza");

			var listForPizza = await _metods.GetAllAsync<PizzaDTO>();

			if (listForPizza!=null && listNameCategoryMenu != null)
			{
				allPizza.MenuItemList = listNameCategoryMenu;
				allPizza.PizzaList = listForPizza;
			}

            return View(allPizza);
        }



		[HttpPost]
		[ActionName("Index")]
		[ValidateAntiForgeryToken]
		public async Task< IActionResult> Index(uint productid)
		{
			var menuFromDb = await _metods.GetOneAsync<MenuItemDTO>(productid);
			menuItem.Id = menuFromDb.Id;
            menuItem.NameCategory = menuFromDb.NameCategory;

            menuItem.ProductId = Convert.ToUInt32(Request.Form["options-outlined"]);


			_metods.SaveCartInSession(menuItem);

			return RedirectToAction("Index","Cart");
		}

		#endregion







		#region Details Get-Post

		[HttpGet]
		public async Task<IActionResult> Details(uint id)
		{
			var idForCart = _metods.GetIdMenuForPlusMinusInCart();
			var countForCart = _metods.GetCountMenuForPlusMinusInCart();


			if (id > 0)
			{
				menuItem = await _metods.GetOneAsync<MenuItemDTO>(id);
			}
			if (idForCart != 0 && id == 0)
			{
				menuItem = await _metods.GetOneAsync<MenuItemDTO>(idForCart);
			}



			if (menuItem.Id != 0)
			{
				pizzaDTOList = await _metods.GetListPizzaByName(menuItem.Name);
				allPizza.MenuItem = menuItem;
				allPizza.PizzaList = pizzaDTOList;
				if (countForCart != 0 && id == 0)
				{
					allPizza.MenuItem.Count = countForCart;
					if (allPizza.MenuItem.Count == 0)
					{
						return RedirectToAction("Index");
					}
				}
				return View(allPizza);

			}

			return NotFound();
		}


		[HttpPost]
		[ActionName("Details")]
		[ValidateAntiForgeryToken]
		public IActionResult DetailsPost(AllPizzaViewModels model)
		{
			if (!ModelState.IsValid)
			{

				HttpContext.Session.SetInt32("countForCart", Convert.ToInt32(model.MenuItem.Count));
				menuItem = model.MenuItem;
				
				menuItem.Count = model.MenuItem.Count;
				

				menuItem.ProductId = Convert.ToUInt32(Request.Form["options-outlined"]);


				_metods.SaveCartInSession(menuItem);

				return RedirectToAction("Index", "Cart");
			}
			return RedirectToAction("Index");
		}



		#endregion









        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }




		


		#region DetailsAction Plus-Minus
		//public IActionResult PlusDetails(uint id, uint count)
		//{
		//	_metods.Plus(id, count);
		//	return RedirectToAction("Details");
		//}

		//public IActionResult MinusDetails(uint id, uint count)
		//{

		//	_metods.Minus(id, count);
		//	return RedirectToAction("Details");
		//}

		public IActionResult RemoveDetails()
		{
			return RedirectToAction("Index");
		}
		#endregion


	}
}