using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.Reflection;
using Пицца_Офис.Models;
using Пицца_Офис.Models.DTO;
using Пицца_Офис.Models.ViewModels;
using Пицца_Офис.Services.GenericServiceis;
using Пицца_Офис.Services.Metods;


namespace Пицца_Офис.Controllers
{
	public class PizzaController : Controller
	{

        readonly IMetods _metods;
		readonly IGenericServices<PizzaDTO> _pizzaServices;
		//      readonly ICategoryPizzaServices _categoryPizza;
		//      readonly ISizePizzaServices _sizePizza;

		public IEnumerable<PizzaDTO> PizzaList { get; set; }

		public PizzaDTO Pizza { get; set; }
		public PizzaDTOViewModel viewModel { get; set; }
		
		


		public PizzaController( IMetods metods, IGenericServices<PizzaDTO> pizzaServices/*IPizzaServices pizzaServices, ICategoryPizzaServices CategoryPizza, ISizePizzaServices SizePizza*/)
		{
			_pizzaServices = pizzaServices;
			_metods = metods;
			PizzaList = new List<PizzaDTO>();
			Pizza = new PizzaDTO();
			viewModel=new PizzaDTOViewModel();
			//_sizePizza = SizePizza;
			//_categoryPizza= CategoryPizza;
		}



		public async Task<IActionResult> Index()
		{
            PizzaList = await _metods.GetAllAsync<PizzaDTO>();
			if (PizzaList != null)
			{
				return View(PizzaList);
			}
			return View();
		}




		#region Create Get-Post
		[HttpGet]
		public async Task<IActionResult> Create()
		{
			var result =await GetPizzaViewModelAsync();

            return View(result);
		}


		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create(PizzaDTOViewModel model)
		{
			try
			{
				if (ModelState.IsValid && model.PizzaDTO.Id==0)
				{
					var response = await _pizzaServices.CreateAsync<APIResponse>(model.PizzaDTO);
					
					if (response != null && response.IsSuccess)
					{
						TempData["Success"] = "Пицца добавлена в базу";
						return RedirectToAction("Index");
					}

				}
				TempData["Error"] = "Ошибка.Что-то пошло не так";
				return RedirectToAction("Index");
            }
			catch (Exception ex)
			{
				TempData["Error"] = "Ошибка.Пицца не добавлена в базу!";
				return BadRequest(ex.Message);
			}
		}
		#endregion




		#region Edit Get-Post
		[HttpGet]
		public async Task<IActionResult> Edit(uint id)
		{
			var viewModel= await GetViewModelFromEditOrDelitAsync(id);
			if (viewModel!=null)
			{
                return View(viewModel);
			}
			return RedirectToAction("Index");
        }


		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(PizzaDTOViewModel model)
		{
			try
			{
				if (ModelState.IsValid)
				{
					var response = await _pizzaServices.UpdateAsync<APIResponse>(model.PizzaDTO);


					if (response != null && response.IsSuccess)
					{
						TempData["Success"] = "Пицца обновлена";
						return RedirectToAction("Index");
					}

				}
				TempData["Error"] = "Ошибка.Что-то пошло не так";
                return RedirectToAction("Index");

            }
			catch (Exception ex)
			{
				TempData["Error"] = "Ошибка.Пицца не обновлена в базу!";
				return BadRequest(ex.Message);
			}
		}
		#endregion





		#region Delete Get-Post
		[HttpGet]
		public async Task<IActionResult> Delete(uint id)
		{
            var viewModel = await GetViewModelFromEditOrDelitAsync(id);
            if (viewModel != null)
            {
                return View(viewModel);
            }
            return RedirectToAction("Index"); ;
		}


		[HttpPost]
		[ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeletePost(uint id)
		{
			try
			{
				var response = await _pizzaServices.DeleteAsync<APIResponse>(id);

				if (response != null && response.IsSuccess)
				{
					TempData["Success"] = "Пицца удалена";
					return RedirectToAction("Index");
				}

				
				TempData["Error"] = "Ошибка.Что-то пошло не так";
				return NotFound();
			}
			catch (Exception ex)
			{
				TempData["Error"] = "Ошибка.Пицца не удалена!";
				return BadRequest(ex.Message);
			}
		}
        #endregion





        #region Metods
        /// <summary>
        /// возвращает PizzaDTOViewModel с пустым PizzaDTO
        /// </summary>
        /// <returns></returns>
        private async Task<PizzaDTOViewModel> GetPizzaViewModelAsync()
		{
			IEnumerable<SelectListItem> result = new List<SelectListItem>();
			viewModel.PizzaDTO=new PizzaDTO();

            var listCategoryPizzaFromDb = await _metods.GetAllAsync<CategoryPizzaDTO>();
            if (listCategoryPizzaFromDb != null)
            {
				result = listCategoryPizzaFromDb.Select(u => new SelectListItem()
				{
					Text= u.Name,
					Value=u.Id.ToString(),
				});
				viewModel.CategorySelectList=result;
            }
            var listSizePizzaFromDb = await _metods.GetAllAsync<SizePizzaDTO>();
            if (listSizePizzaFromDb != null)
            {
                result = listSizePizzaFromDb.Select(u => new SelectListItem()
                {
                    Text = u.Size,
                    Value = u.Id.ToString(),
                });
                viewModel.SizeSelectList = result;
            }

			return viewModel;
        }



        


        /// <summary>
        /// возвращает PizzaDTOViewModel с PizzaDTO по Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private async Task<PizzaDTOViewModel> GetViewModelFromEditOrDelitAsync(uint id)
		{
			if (id>0)
			{
                var PizzaFromDb = await _metods.GetOneAsync<PizzaDTO>(id);
                if (PizzaFromDb != null)
                {
                    var viewModelFromEdit = await GetPizzaViewModelAsync();
                    viewModelFromEdit.PizzaDTO = PizzaFromDb;
                    return viewModelFromEdit;
                }
				return null;
            }
            return null;

        }

        #endregion

    }
}
