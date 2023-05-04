using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Пицца_Офис.Models;
using Пицца_Офис.Models.DTO;
using Пицца_Офис.Services.GenericServiceis;
using Пицца_Офис.Services.Metods;

namespace Пицца_Офис.Controllers
{
    public class CategoryPizzaController : Controller
    {
        readonly IGenericServices<CategoryPizzaDTO> _genericServices;
        readonly IMetods _metods;

        public IEnumerable<CategoryPizzaDTO> CategoryPizzaList;
        public CategoryPizzaDTO CategoryPizza;


        public CategoryPizzaController(IGenericServices<CategoryPizzaDTO> genericServices, IMetods metods)
        {
            _metods = metods;
            _genericServices = genericServices;
            CategoryPizzaList = new List<CategoryPizzaDTO>();
            CategoryPizza = new CategoryPizzaDTO();
            
        }



        public async Task<IActionResult> Index()
        {
            CategoryPizzaList = await _metods.GetAllAsync<CategoryPizzaDTO>();
            if (CategoryPizzaList!=null)
            {
                return View(CategoryPizzaList);
            }
            return View();
        }





        #region Create Get-Post
        [HttpGet]
        public IActionResult Create()
        {

            return View(CategoryPizza);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CategoryPizzaDTO model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var response = await _genericServices.CreateAsync<APIResponse>(model);

                    if (response != null && response.ErrorsMessages.Count > 0 && response.ErrorsMessages[0].ToString() == "CategoryPizza not created. Name CategoryPizza not uniqum")
                    {
                        TempData["Error"] = "Ошибка.Название не уникальное";
                        return View(model);
                    }
                    if (response != null && response.IsSuccess)
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
            if (id > 0)
            {
                CategoryPizza = await _metods.GetOneAsync<CategoryPizzaDTO>(id);

                if (CategoryPizza.Id>0)
                {
                    return View(CategoryPizza);
                }
                return NotFound();
            }
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(CategoryPizzaDTO model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var response = await _genericServices.UpdateAsync<APIResponse>(model);

                    if (response != null && response.ErrorsMessages.Count > 0 && response.ErrorsMessages[0].ToString() == "CategoryPizza not update. Name CategoryPizza not uniqum")
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
                CategoryPizza = await _metods.GetOneAsync<CategoryPizzaDTO>(id);
                if (CategoryPizza.Id>0)
                {
                    return View(CategoryPizza);
                }
                return NotFound();
            }
            return View();
        }


        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeletePost(uint id)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var response = await _genericServices.DeleteAsync<APIResponse>(id);

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
