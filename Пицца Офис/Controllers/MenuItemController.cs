using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using Пицца_Офис.Models;
using Пицца_Офис.Models.DTO;
using Пицца_Офис.Models.ViewModels;
using Пицца_Офис.Services.GenericServiceis;
using Пицца_Офис.Services.Metods;

namespace Пицца_Офис.Controllers
{
    public class MenuItemController : Controller
    {
        //readonly IGenericServices<CategoryMenuDTO> _genericServices;
        readonly IMetods _metods;



        readonly IWebHostEnvironment _webHostEnvironment;
        //readonly IGenericServices<CategoryMenuDTO> _pizzaServices;
        readonly IGenericServices<MenuItemDTO> _menuItemServices;
        //readonly IGenericServices<PizzaKingSizeDTO> _kingSizeServices;
        readonly IGenericServices<CategoryMenuDTO> _categoryMenuServices;


        public IEnumerable<MenuItemDTO> MenuItemList { get; set; }
        public MenuItemDTO MenuItem { get; set; }
        public PizzaDTO Pizza{ get; set; }
        public MenuItemDTOViewModel viewModel { get; set; }



        public MenuItemController( IMetods metods, IGenericServices<MenuItemDTO> menuItemServices, IGenericServices<CategoryMenuDTO> categoryMenuServices, IWebHostEnvironment webHostEnvironment /*IPizzaServices pizzaServices,  IPizzaKingSizeServices kingSizeServices,*/
                                   /*IMenuItemServices menuItemServices, ICategoryMenuServices categoryMenuServices*/)
        {
            _webHostEnvironment = webHostEnvironment;
            //_pizzaServices = pizzaServices;
            //_kingSizeServices= kingSizeServices;
            //_categoryMenuServices= categoryMenuServices;
            _metods = metods;
            _categoryMenuServices = categoryMenuServices; 
            //_kingSizeServices = kingSizeServices;

            viewModel = new MenuItemDTOViewModel();

            _menuItemServices = menuItemServices;
            MenuItemList = new List<MenuItemDTO>();
            MenuItem= new MenuItemDTO();
            Pizza= new PizzaDTO();
        }
       



        public async Task<IActionResult> Index()
        {
            MenuItemList = await _metods.GetAllAsync<MenuItemDTO>();
            if (MenuItemList != null)
            {
                return View(MenuItemList);
            }
            return View();
        }




        #region Create Get-Post
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var result = await GetMenuItemViewModelAsync();

            return View(result);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(MenuItemDTOViewModel model,IFormFile? file)
        {
            try
            {
                if (ModelState.IsValid && model.MenuItem.Id == 0)
                {
                    if (file!=null)
                    {
                        model.MenuItem.ImagePatch = AddNewImage(file);
                        if (model.MenuItem.ImagePatch == null)
                        {
                            TempData["Error"] = "Ошибка.Такое фото уже есть";
                            return RedirectToAction("Index");
                        }
                    }

                    var updatingModel = await UpdateMenuItem(model);
                    
                    var response = await _menuItemServices.CreateAsync<APIResponse>(updatingModel.MenuItem);
                    if (response != null && response.ErrorsMessages.Count > 0 && response.ErrorsMessages[0].ToString() ==
                                                                                        "MenuItem not created. Name MenuItem not uniqum")
                    {
                        TempData["Error"] = "Ошибка.Название не уникальное";
                        return View(model);
                    }
                    if (response != null && response.IsSuccess)
                    {
                        TempData["Success"] = "Меню добавлено в базу";

                        return RedirectToAction("Index");
                    }

                }
                TempData["Error"] = "Ошибка.Что-то пошло не так";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Ошибка.Меню не добавлено в базу!";
                return BadRequest(ex.Message);
            }
        }
        #endregion




        #region Edit Get-Post
        [HttpGet]
        public async Task<IActionResult> Edit(uint id)
        {
            var viewModel = await GetViewModelWithDbAsync(id);
            if (viewModel != null)
            {
                return View(viewModel);
            }
            return RedirectToAction("Index");
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(MenuItemDTOViewModel model,IFormFile? file)
        {
            try
            {
                if (file!=null)
                {
                    DeleteFile(model.MenuItem.ImagePatch);
                    model.MenuItem.ImagePatch = AddNewImage(file);
                    if (model.MenuItem.ImagePatch ==null)
                    {
                        TempData["Error"] = "Ошибка.Такое фото уже есть";
                        return RedirectToAction("Index");
                    }
                }

                var updatingModel = await UpdateMenuItem(model);

                if (ModelState.IsValid)
                {
                    var response = await _menuItemServices.UpdateAsync<APIResponse>(updatingModel.MenuItem);

                    if (response != null && response.ErrorsMessages.Count > 0 && response.ErrorsMessages[0].ToString()
                                                                                 == "MenuItem not update. Name MenuItem not uniqum")
                    {
                        TempData["Error"] = "Ошибка.Название не уникальное";
                        var viewModel = await GetViewModelWithDbAsync(model.MenuItem.Id);
                        if (viewModel != null)
                        {
                            return View(viewModel);
                        }

                    }

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
            var viewModel = await GetViewModelWithDbAsync(id);
            if (viewModel != null)
            {
                return View(viewModel);
            }
            return RedirectToAction("Index"); ;
        }


        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeletePost( MenuItemDTOViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var response = await _menuItemServices.DeleteAsync<APIResponse>(model.MenuItem.Id);

                    if (response != null && response.IsSuccess)
                    {
                        if (model.MenuItem.ImagePatch != null)
                        {
                            DeleteFile(model.MenuItem.ImagePatch);
                        }
                        
                        TempData["Success"] = "Пицца удалена";
                        return RedirectToAction("Index");
                    }

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
        /// Возврашает MenuItemDTOViewModel с пустым MenuItemDTO и всеми Categories 
        /// </summary>
        /// <returns></returns>
        private async Task<MenuItemDTOViewModel> GetMenuItemViewModelAsync()
        {
            IEnumerable<SelectListItem> result = new List<SelectListItem>();
            viewModel.MenuItem = new MenuItemDTO();


            #region get all Pizza
            //var responseFromPizza = await _pizzaServices.GetAllAsync<APIResponse>();
            //if (responseFromPizza != null)
            //{
            //    var listFromDb = JsonConvert.DeserializeObject<List<PizzaDTO>>(Convert.ToString(responseFromPizza.Result));
            //    result = listFromDb.Select(u => new SelectListItem()
            //    {
            //        Text = u.Name,
            //        Value = u.Id.ToString(),
            //    });
            //    viewModel.PizzaList = result;
            //}
            #endregion 


            #region get all PizzaKingSize
            var listPizzaKingFromDb = await _metods.GetAllAsync<PizzaKingSizeDTO>();
            if (listPizzaKingFromDb != null)
            {
                
                result = listPizzaKingFromDb.Select(u => new SelectListItem()
                {
                    Text = u.Name,
                    Value = u.Id.ToString(),
                });
                
                viewModel.PizzaKingSizeList = result;
            }
            #endregion 


            #region get all CaegoryMenu
            var listCategoryMenuFromDb = await _metods.GetAllAsync<CategoryMenuDTO>();
            if (listCategoryMenuFromDb != null)
            {
                
                result = listCategoryMenuFromDb.Select(u => new SelectListItem()
                {
                    Text = u.NameCategory,
                    Value = u.Id.ToString(),
                });

                viewModel.CategoryMenuList = result;
            }
            #endregion 


            return viewModel;
        }





        /// <summary>
        ///  Возврашает MenuItemDTO  по Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private async Task<MenuItemDTO> GetMenuItemWithDbAsync(uint id)
        {
            MenuItem = await _metods.GetOneAsync<MenuItemDTO>(id);
            if (MenuItem.Id>0)
            {
                 return MenuItem;
            }
            return null;
        }






        /// <summary>
        /// Возврашает MenuItemDTOViewModel   по  MenuItemDTO.Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private async Task<MenuItemDTOViewModel> GetViewModelWithDbAsync(uint id)
        {
            if (id > 0)
            {
                var MenuItemWithDb = await GetMenuItemWithDbAsync(id);
                if (MenuItemWithDb != null)
                {
                    var viewModelWithDb = await GetMenuItemViewModelAsync();
                    viewModelWithDb.MenuItem = MenuItemWithDb;
                    return viewModelWithDb;
                }
                return null;
            }
            return null;

        }




        void DeleteFile(string pathImage)
        {
            if (pathImage != null)
            {
                
                var oldImagePath = Path.Combine(_webHostEnvironment.WebRootPath, pathImage.TrimStart('\\'));
                if (System.IO.File.Exists(oldImagePath))
                {
                    System.IO.File.Delete(oldImagePath);
                }
                
            }

        }

        string AddNewImage(IFormFile file)
        {

            var wwwrootPath = _webHostEnvironment.WebRootPath;

            var upload = Path.Combine(wwwrootPath, @"images\menuItem\pizzaKingSize\");

            if (System.IO.File.Exists(upload + file.FileName))
            {
                return null;
            }

            #region Создаем поток чтобы сохранить файл
            using (var fileStreams = new FileStream(Path.Combine(upload, file.FileName), FileMode.Create))
            {
                file.CopyTo(fileStreams);
            }
            #endregion

            return @"\images\menuItem\pizzaKingSize\" + file.FileName;
        }



        async Task<MenuItemDTOViewModel> UpdateMenuItem(MenuItemDTOViewModel model)
        {

            #region  Переносим данные с KingSize в MenuItem
            if (model.MenuItem.PizzaKingSizeId>0)
            {
                PizzaKingSizeDTO modelkingSize = new();

                modelkingSize = await _metods.GetOneAsync<PizzaKingSizeDTO>(model.MenuItem.PizzaKingSizeId);
                
                if (modelkingSize.Name != "Пусто")
                {
                    model.MenuItem.Name = modelkingSize.Name;
                    model.MenuItem.Price = modelkingSize.Price;
                    model.MenuItem.Description = modelkingSize.Description;
                    model.MenuItem.ImagePatch = modelkingSize.ImagePath;
                }
            }
            #endregion

            #region  Переносим данные с CategoryMenu в MenuItem
            if (model.MenuItem.CategoryMenuId > 0)
            {
                CategoryMenuDTO modelCategoryMenu = new();

                modelCategoryMenu = await _categoryMenuServices.GetOneAsync<CategoryMenuDTO>(model.MenuItem.CategoryMenuId);
                
                if (modelCategoryMenu.NameCategory != "Пусто")
                {
                    model.MenuItem.NameCategory = modelCategoryMenu.NameCategory;
                }
            }
            #endregion


            return model;
            

        }



        #endregion


    }




}
