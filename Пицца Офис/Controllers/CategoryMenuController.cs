using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Пицца_Офис.Models;
using Пицца_Офис.Models.DTO;
using Пицца_Офис.Models.ViewModels;
using Пицца_Офис.Services.GenericServiceis;
using Пицца_Офис.Services.Metods;

namespace Пицца_Офис.Controllers
{
    public class CategoryMenuController : Controller
    {
        readonly IGenericServices<CategoryMenuDTO> _genericServices;
        readonly IMetods _metods;

        public IEnumerable<CategoryMenuDTO> CategoryMenuList { get; set; }

        public CategoryMenuDTO CategoryMenu { get; set; }
        public CategoryMenuDTOViewModel viewModel { get; set; }




        public CategoryMenuController(IMetods metods, IGenericServices<CategoryMenuDTO> genericServices)
        {
            
            _metods = metods;
            _genericServices = genericServices;
            CategoryMenuList = new List<CategoryMenuDTO>();
            CategoryMenu = new CategoryMenuDTO();
            viewModel = new CategoryMenuDTOViewModel();
        }



        public async Task<IActionResult> Index()
        {
            CategoryMenuList = await _metods.GetAllAsync<CategoryMenuDTO>();
            if (CategoryMenuList != null)
            {
                return View(CategoryMenuList);
            }
            return View();
        }




        #region Create Get-Post
        [HttpGet]
        public IActionResult Create()
        {

            return View(viewModel);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CategoryMenuDTOViewModel model)
        {
            try
            {
                if (ModelState.IsValid && model.CategoryMenu.Id == 0)
                {
                   
                    var response = await _genericServices.CreateAsync<APIResponse>(model.CategoryMenu);

                    if (response != null && response.IsSuccess)
                    {
                        TempData["Success"] = "Категория добавлена в базу";
                        return RedirectToAction("Index");
                    }

                }
                TempData["Error"] = "Ошибка.Что-то пошло не так";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Ошибка.Категория не добавлена в базу!";
                return BadRequest(ex.Message);
            }
        }
        #endregion




        #region Edit Get-Post
        [HttpGet]
        public async Task<IActionResult> Edit(uint id)
        {
            CategoryMenu = await _metods.GetOneAsync<CategoryMenuDTO>(id);
            if (CategoryMenu.Id>0)
            {
                viewModel.CategoryMenu = CategoryMenu;
                return View(viewModel);
            }
            return RedirectToAction("Index");
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(CategoryMenuDTOViewModel model)
        {
            try
            {

                if (ModelState.IsValid)
                {

                    var response = await _genericServices.UpdateAsync<APIResponse>(model.CategoryMenu);

                    if (response != null && response.IsSuccess)
                    {
                        TempData["Success"] = " Категория обновлена";
                        return RedirectToAction("Index");
                    }

                }
                TempData["Error"] = "Ошибка.Что-то пошло не так";
                return RedirectToAction("Index");

            }
            catch (Exception ex)
            {
                TempData["Error"] = "Ошибка.Категория не обновлена в базу!";
                return BadRequest(ex.Message);
            }
        }
        #endregion





        #region Delete Get-Post
        [HttpGet]
        public async Task<IActionResult> Delete(uint id)
        {
            CategoryMenu = await _metods.GetOneAsync<CategoryMenuDTO>(id);
            if (CategoryMenu.Id>0)
            {
                viewModel.CategoryMenu = CategoryMenu;
                return View(viewModel);
            }
            return RedirectToAction("Index"); ;
        }


        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeletePost(CategoryMenuDTOViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var response = await _genericServices.DeleteAsync<APIResponse>(model.CategoryMenu.Id);

                    if (response != null && response.IsSuccess)
                    {

                        TempData["Success"] = "Категория удалена";
                        return RedirectToAction("Index");
                    }

                }
                TempData["Error"] = "Ошибка.Что-то пошло не так";
                return NotFound();
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Ошибка.Категория не удалена!";
                return BadRequest(ex.Message);
            }
        }
        #endregion





        
    }
}
