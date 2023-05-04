using Microsoft.AspNetCore.Hosting;
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
    public class PizzaKingSizeController : Controller
    {
        readonly IMetods _metods;
        readonly IGenericServices<PizzaKingSizeDTO> _kingSizeServices;
        readonly IWebHostEnvironment _webHostEnvironment;

        public IEnumerable<PizzaKingSizeDTO> PizzaKingSizeList { get; set; }

        public PizzaKingSizeDTO PizzaKingSize { get; set; }
        public PizzaKingSizeDTOViewModel viewModel { get; set; }




        public PizzaKingSizeController(IGenericServices<PizzaKingSizeDTO> kingSizeServices, IWebHostEnvironment webHostEnvironment, IMetods metods)
        {
            _kingSizeServices= kingSizeServices;
            _webHostEnvironment= webHostEnvironment;
            _metods= metods;
            PizzaKingSizeList=new List<PizzaKingSizeDTO>();
            PizzaKingSize=new PizzaKingSizeDTO();
            viewModel= new PizzaKingSizeDTOViewModel();
        }



        public async Task<IActionResult> Index()
        {
            PizzaKingSizeList = await _metods.GetAllAsync<PizzaKingSizeDTO>();
            if (PizzaKingSizeList != null)
            {
                return View(PizzaKingSizeList);
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
        public async Task<IActionResult> Create(PizzaKingSizeDTOViewModel model,IFormFile? file)
        {
            try
            {
                if (ModelState.IsValid && model.PizzaKingSize.Id == 0)
                {
                    if (file!=null)
                    {
                        model.PizzaKingSize.ImagePath = AddNewImage(file);
                        if (model.PizzaKingSize.ImagePath == null)
                        {
                            TempData["Error"] = "Ошибка.Такое фото уже есть";
                            return RedirectToAction("Index");
                        }
                    }
                    



                    var response = await _kingSizeServices.CreateAsync<APIResponse>(model.PizzaKingSize);

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
            PizzaKingSize = await _metods.GetOneAsync<PizzaKingSizeDTO>(id);
            if (PizzaKingSize.Id>0)
            {
                viewModel.PizzaKingSize = PizzaKingSize;
                return View(viewModel);
            }
            return RedirectToAction("Index");
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(PizzaKingSizeDTOViewModel model,IFormFile? file)
        {
            try
            {
                if (file != null)
                {
                    DeleteFile(model.PizzaKingSize.ImagePath);
                    model.PizzaKingSize.ImagePath = AddNewImage(file);
                    if (model.PizzaKingSize.ImagePath == null)
                    {
                        TempData["Error"] = "Ошибка.Такое фото уже есть";
                        return RedirectToAction("Index");
                    }
                }


                if (ModelState.IsValid)
                {

                    var response = await _kingSizeServices.UpdateAsync<APIResponse>(model.PizzaKingSize);

                    if (response != null && response.ErrorsMessages.Count > 0 && response.ErrorsMessages[0].ToString()
                                                                                 == "PizzaKingSize not update. Name PizzaKingSize not uniqum")
                    {
                        TempData["Error"] = "Ошибка.Название не уникальное";
                        PizzaKingSize = await _metods.GetOneAsync<PizzaKingSizeDTO>(model.PizzaKingSize.Id);
                        if (PizzaKingSize.Id>0)
                        {
                            viewModel.PizzaKingSize = PizzaKingSize;
                            return View(viewModel);
                        }

                    }


                    if (response != null && response.IsSuccess)
                    {
                        TempData["Success"] = " Пицца King Size обновлена";
                        return RedirectToAction("Index");
                    }

                }
                TempData["Error"] = "Ошибка.Что-то пошло не так";
                return RedirectToAction("Index");

            }
            catch (Exception ex)
            {
                TempData["Error"] = "Ошибка.Пицца King Size не обновлена в базу!";
                return BadRequest(ex.Message);
            }
        }
        #endregion





        #region Delete Get-Post
        [HttpGet]
        public async Task<IActionResult> Delete(uint id)
        {
            PizzaKingSize = await _metods.GetOneAsync<PizzaKingSizeDTO>(id);
            if (PizzaKingSize.Id>0)
            {
                viewModel.PizzaKingSize = PizzaKingSize;
                return View(viewModel);
            }
            return RedirectToAction("Index"); ;
        }


        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeletePost(PizzaKingSizeDTOViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var response = await _kingSizeServices.DeleteAsync<APIResponse>(model.PizzaKingSize.Id);

                    if (response != null && response.IsSuccess)
                    {
                        if (model.PizzaKingSize.ImagePath != null)
                        {
                            DeleteFile(model.PizzaKingSize.ImagePath);
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



        #endregion
    }
}
