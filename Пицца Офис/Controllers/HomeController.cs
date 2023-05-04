using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlTypes;
using System.Diagnostics;
using Пицца_Офис.Models;
using Пицца_Офис.Models.AjaxForHome.CountAction;
using Пицца_Офис.Models.AjaxForHome.GetAllStatusAction;
using Пицца_Офис.Models.AjaxForHome.IndexAction;
using Пицца_Офис.Models.DTO;
using Пицца_Офис.Models.ViewModels;
using Пицца_Офис.Services.GenericServiceis;
using Пицца_Офис.Services.Metods;
using Пицца_Офис.Utillity;

namespace Пицца_Офис.Controllers
{
    
    [Authorize(Roles = StaticDetails.OfficeRole)]
    public class HomeController : Controller
    {
        readonly IGenericServices<OrderViewModels> _orderViewModelsServices;
        readonly IMetods _metods;
        readonly IHttpContextAccessor _httpContext;


        public OrderViewModels modelOrderViewModels;
        public List< OrderViewModels> listOrderViewModels;



        public HomeController(IMetods metods, IGenericServices<OrderViewModels> orderViewModelsServices,
                                                                 IHttpContextAccessor httpContext)
        {
            _metods = metods;
            _orderViewModelsServices = orderViewModelsServices;
            modelOrderViewModels = new OrderViewModels();
            listOrderViewModels = new List<OrderViewModels>();
            _httpContext = httpContext;
        }




        public IActionResult Index()
        {
            return View();
        }



        public async Task<IActionResult> IndexAjax()
        {
            listOrderViewModels = await _metods.GetAllAsync<OrderViewModels>();

            if (listOrderViewModels != null)
            {
                OrderDetailsForIndex[] arrayOrderDetails = new OrderDetailsForIndex[listOrderViewModels.Count];

                for (int i = 0; i < listOrderViewModels.Count; i++)
                {
                    var model = AddAllParametrsOrder(listOrderViewModels[i].OrderHeader);

                    model.DateCreatedOrder = EditStringDateCreate(listOrderViewModels[i].DateCreatedOrder);

                    foreach (var OrderDetails in listOrderViewModels[i].OrderDetailsList)
                    {
                        model.MenuName.Add(OrderDetails.MenuName);
                    }

                    arrayOrderDetails[i] = model;

                }


                return Json(arrayOrderDetails);

            }

            return View();
        }




        #region Edit Get-Post

        [HttpGet]
        public async Task< IActionResult> Edit(uint id)
        {
            modelOrderViewModels =await _metods.GetOneAsync<OrderViewModels>(id);
            return View(modelOrderViewModels);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task< IActionResult> Edit(OrderViewModels model)
        {

            if (ModelState.IsValid)
            {
                var response = await _orderViewModelsServices.UpdateAsync<APIResponse>(model);
                if (response != null && response.IsSuccess)
                {
                    TempData["Success"] = "Изменения внесены";
                    return RedirectToAction("Index");
                }
                TempData["Error"] = "Ошибка.Сервер не сохранил";
                return View(model);
            }
            TempData["Error"] = "Ошибка.Форма заполнена некоректно";
            return View(model);
        }



        #endregion




        #region Delete Get-Post

        [HttpGet]
        public async Task<IActionResult> Delete(uint id)
        {
            modelOrderViewModels = await _metods.GetOneAsync<OrderViewModels>(id);
            return View(modelOrderViewModels);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(OrderViewModels model)
        {

            if (model.OrderHeader.Id>0)
            {
                var response = await _orderViewModelsServices.DeleteAsync<APIResponse>(model.OrderHeader.Id);
                if (response != null && response.IsSuccess)
                {
                    TempData["Success"] = "Заказ удален";
                    return RedirectToAction("Index");
                }
                TempData["Error"] = "Ошибка.Сервер не удалил";
                return View(model);
            }
            TempData["Error"] = "Ошибка.Что-то пошло не так";
            return View(model);
        }



        #endregion




        #region CountAjax Get-Post

        [HttpGet]
        public async Task<IActionResult> Count(uint id,uint count)
        {
            if (id>0 )
            {
                var response = await _orderViewModelsServices.UpdateOrderDetailsAsync<APIResponse>(id, count);
                if (response != null && response.IsSuccess)
                {
                    OrderDetailsForEditViewModel orderDetailsForEdit = new();

                    var orderViewModelsFromDb = JsonConvert.DeserializeObject<OrderViewModels>(Convert.ToString(response.Result));
                    
                    if (orderViewModelsFromDb!=null)
                    {
                        OrderDetailsForEdit[] arrayOrderDetails = new OrderDetailsForEdit[orderViewModelsFromDb.OrderDetailsList.Count];

                        for (int i = 0; i < orderViewModelsFromDb.OrderDetailsList.Count; i++)
                        {
                            arrayOrderDetails[i] = new OrderDetailsForEdit()
                            {
                                menuName = orderViewModelsFromDb.OrderDetailsList[i].MenuName,
                                nameCategory = orderViewModelsFromDb.OrderDetailsList[i].NameCategory,
                                count = orderViewModelsFromDb.OrderDetailsList[i].Count,
                                id = orderViewModelsFromDb.OrderDetailsList[i].Id,
                            };
                        }
                        orderDetailsForEdit.arrayOrderDetails = arrayOrderDetails;
                    }

                    orderDetailsForEdit.IsSuccess = response.IsSuccess;
                    
                    return Json(orderDetailsForEdit);
                }
                
            }
            return Json(null);
        }








        #endregion



        #region StatusAjax Get

        [HttpGet]
        public async Task<IActionResult> ChangeStatus(uint id, string status)
        {
            if (id > 0)
            {
                //
                var employee = new EmployeeUpdateStatus()
                {
                    Id = _httpContext.HttpContext.User.Claims.ToList()[4].Value,
                    JobTitle = _httpContext.HttpContext.User.Claims.ToList()[3].Value,
                };
                var idEmployee = JsonConvert.SerializeObject(employee);


                var response = await _orderViewModelsServices.UpdateStatusAsync<APIResponse>(status,id, idEmployee);
                if (response != null && response.IsSuccess)
                {
                    return Json(response.IsSuccess);
                }
                return Json(null);
            }
            return Json(null);
        }
        #endregion



        #region GetAllStatus Get

        [HttpGet]
        public async Task<IActionResult> GetAllStatus()
        {
            try
            {
                var listOrderViewModels = await _metods.GetAllAsync<OrderViewModels>();
                if (listOrderViewModels!=null)
                {
                    Status[] arroyAllStstus = new Status[listOrderViewModels.Count];

                    for (int i = 0; i < listOrderViewModels.Count; i++)
                    {
                        var statusForOneOrder = new Status()
                        {
                            id = listOrderViewModels[i].OrderHeader.Id,
                            status = listOrderViewModels[i].OrderHeader.OrderStatus
                        };

                        arroyAllStstus[i] = statusForOneOrder;
                    }

                    return Json(arroyAllStstus);
                }
                return Json(null);
            }
            catch (Exception ex)
            {

                return Json(ex.Message);
            }
        }
        #endregion




        #region Metods

        private string EditStringDateCreate( string date)
        {
            var newString = "";
            if (date!=null)
            {
                var part_1 = date.Substring(11);
                var part_2 = date.Substring(8,2);
                var part_3 = date.Substring(5, 2);
                var part_4 = date.Substring(0, 4);
                newString = part_1+" "+ part_2+"-"+part_3+"-"+part_4;
            }

            

            return newString;
        }



        OrderDetailsForIndex AddAllParametrsOrder(OrderHeaderDTO model)
        {
            OrderDetailsForIndex modelOrderDetails = new();

            modelOrderDetails.IdOrderHeader = model.Id;
            modelOrderDetails.NumberOrder = model.NumberOrder;
            modelOrderDetails.OrderStatus = model.OrderStatus;
            modelOrderDetails.FirstName = model.FirstName;
            modelOrderDetails.LastName = model.LastName;
            modelOrderDetails.PhoneNumber = model.PhoneNumber;
            modelOrderDetails.Street = model.Street;
            modelOrderDetails.House = model.House;
            modelOrderDetails.Entrance = model.Entrance;
            modelOrderDetails.Apartment = model.Apartment;
            modelOrderDetails.Floor = model.Floor;

            return modelOrderDetails;
        }

        #endregion 
    }
}