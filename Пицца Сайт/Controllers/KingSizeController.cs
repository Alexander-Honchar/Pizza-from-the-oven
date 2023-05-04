using Microsoft.AspNetCore.Mvc;
using Пицца_Сайт.Models;
using Пицца_Сайт.Models.DTO;
using Пицца_Сайт.Models.ViewModels;
using Пицца_Сайт.Services.Metods;

namespace Пицца_Сайт.Controllers
{
    public class KingSizeController : Controller
    {
        public APIResponse _response;
        readonly IMetods _metods;

        public KingSizeViewModels KingSizeView;
		//public List<MenuItemDTO> menuItemDTOList;
		//public IEnumerable<PizzaDTO> pizzaDTOList;
		//public AllPizzaViewModels allPizza;
		public MenuItemDTO menuItem;


		public KingSizeController(IMetods metods)
        {
            _metods = metods;
            _response = new APIResponse();
            KingSizeView= new KingSizeViewModels();
			//menuItemDTOList = new List<MenuItemDTO>();
			//pizzaDTOList = new List<PizzaDTO>();
			//allPizza = new AllPizzaViewModels();
			menuItem = new MenuItemDTO();


		}








        #region Index Get-Post

        public async Task<IActionResult> Index()
        {

            var listNameCategoryMenu = await _metods.GetListNameCategoryMenuAsync<MenuItemDTO>("KingSize");


            if ( listNameCategoryMenu != null)
            {
                KingSizeView.MemuItemsList= listNameCategoryMenu;
                
            }

            return View(KingSizeView);
        }

		#endregion


		#region Details Get-Post

		[HttpGet]
		public async Task<IActionResult> Details(uint id)
		{
			try
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

				return View(menuItem);
			}
			catch (Exception ex)
			{

				return BadRequest(ex.Message);
			}
		}


		[HttpPost]
		[ActionName("Details")]
		[ValidateAntiForgeryToken]
		public IActionResult DetailsPost(MenuItemDTO model)
		{
			if (!ModelState.IsValid)
			{

				HttpContext.Session.SetInt32("countForCart", Convert.ToInt32(model.Count));
				menuItem = model;


				_metods.SaveCartInSession(menuItem);

				return RedirectToAction("Index", "Cart");
			}
			return RedirectToAction("Index");
		}



		#endregion


		public IActionResult RemoveDetails()
		{
			return RedirectToAction("Index");
		}
	}
}
