using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Reflection;
using Пицца_Офис.Models;
using Пицца_Офис.Models.DTO;
using Пицца_Офис.Services.GenericServiceis;
using Пицца_Офис.Services.Metods;

namespace Пицца_Офис.Controllers
{
    public class SizePizzaController : Controller
    {
        readonly IMetods _metods;
        readonly IGenericServices<SizePizzaDTO> _sizePizzaServices;

        public IEnumerable<SizePizzaDTO> sizePizzaList;
        public SizePizzaDTO SizePizza;


        public SizePizzaController(IGenericServices<SizePizzaDTO> sizePizza, IMetods metods)
        {
            _sizePizzaServices = sizePizza;
            _metods = metods;
            sizePizzaList= new List<SizePizzaDTO>();
            SizePizza = new SizePizzaDTO();
        }



        public async Task<IActionResult> Index()
        {
            sizePizzaList = await _metods.GetAllAsync<SizePizzaDTO>();
            if (sizePizzaList != null)
            {
                return View(sizePizzaList);
            }
            return View();
        }




        #region Create Get-Post
        [HttpGet]
        public IActionResult Create()
        {
            
            return View(SizePizza);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SizePizzaDTO model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var response= await _sizePizzaServices.CreateAsync<APIResponse>(model);
                    if (response!= null && response.ErrorsMessages.Count>0 && response.ErrorsMessages[0].ToString()== "SizePizza not created. Name SizePizza not uniqum")
                    {
                        TempData["Error"] = "Ошибка.Название не уникальное";
                        return View(model);
                    }
                    if (response!=null && response.IsSuccess)
                    {
                        TempData["Success"] = "Размер пиццы добавлен в базу";
                        return RedirectToAction("Index");
                    }
                    
                }
                TempData["Error"] = "Ошибка.Что-то пошло не так";
                return View(model);
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Ошибка.Размер пиццы не добавлен в базу!";
                return BadRequest(ex.Message);
            }
        }
        #endregion




        #region Edit Get-Post
        [HttpGet]
        public async Task<IActionResult> Edit(uint id)
        {
            if (id>0)
            {
                SizePizza = await _metods.GetOneAsync<SizePizzaDTO>(id);
                
                if (SizePizza.Id>0)
                {
                    return View(SizePizza);
                }
                return NotFound();
            }
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(SizePizzaDTO model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var response = await _sizePizzaServices.UpdateAsync<APIResponse>(model);

                    if (response != null && response.ErrorsMessages.Count > 0 && response.ErrorsMessages[0].ToString() == "SizePizza not update. Name SizePizza not uniqum")
                    {
                        TempData["Error"] = "Ошибка.Название не уникальное";
                        return View();
                    }

                    if (response != null && response.IsSuccess)
                    {
                        TempData["Success"] = "Размер пиццы обновлен";
                        return RedirectToAction("Index");
                    }

                }
                TempData["Error"] = "Ошибка.Что-то пошло не так";
                return View(model);
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Ошибка.Размер пиццы не обновлен в базу!";
                return BadRequest(ex.Message);
            }
        }
        #endregion




        #region Delete Get-Post
        [HttpGet]
        public async Task<IActionResult> Delete(uint id)
        {
            if (id > 0)
            {
                SizePizza = await _metods.GetOneAsync<SizePizzaDTO>(id);
                if (SizePizza.Id>0)
                {
                    return View(SizePizza);
                }
                return NotFound();
            }
            return View();
        }


        [HttpPost]
        [ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeletePost( uint id)
		{
			try
			{
				if (ModelState.IsValid)
				{
					var response = await _sizePizzaServices.DeleteAsync<APIResponse>(id);

					if (response != null && response.IsSuccess)
					{
						TempData["Success"] = "Размер пиццы удален";
						return RedirectToAction("Index");
					}

				}
				TempData["Error"] = "Ошибка.Что-то пошло не так";
				return NotFound();
			}
			catch (Exception ex)
			{
				TempData["Error"] = "Ошибка.Размер пиццы не удален!";
				return BadRequest(ex.Message);
			}
		}
		#endregion

	}
}
