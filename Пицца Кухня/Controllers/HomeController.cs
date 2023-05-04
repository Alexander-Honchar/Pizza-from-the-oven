using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using Пицца_Кухня.Models;
using Пицца_Кухня.Models.AjaxViewModels;
using Пицца_Кухня.Models.DTO;
using Пицца_Кухня.Models.ViewModels;
using Пицца_Кухня.Services.GenericServiceis;
using Пицца_Кухня.Services.Metods;
using Пицца_Кухня.Utility;

namespace Пицца_Кухня.Controllers
{
    public class HomeController : Controller
    {
        readonly IMetods _metods;
        readonly IGenericServices<OrderViewModels> _orderViewModelsServices;
        readonly IHttpContextAccessor _httpContextAccessor;

        OrderViewModels modelOrderViewModels;
        List<OrderViewModels> listOrderViewModels;




        public HomeController(IMetods metods, IGenericServices<OrderViewModels> orderViewModelsServices,
                                                            IHttpContextAccessor httpContextAccessor)
        {
           _metods= metods;
           _orderViewModelsServices = orderViewModelsServices;
            _httpContextAccessor = httpContextAccessor;

            modelOrderViewModels = new OrderViewModels();
            listOrderViewModels= new List<OrderViewModels>();

        }



        #region Index

        public async Task< IActionResult> Index()
        {
            listOrderViewModels = await _metods.GetAllAsync<OrderViewModels>();

            if (listOrderViewModels!=null)
            {
                
                return View(GetViewModelForIndex(listOrderViewModels));
            }

            return View();
        }


        [HttpGet]
        public async Task<IActionResult> IndexAjax()
        {
            listOrderViewModels = await _metods.GetAllAsync<OrderViewModels>();

            if (listOrderViewModels != null)
            {
                return Json(GetViewModelForIndex(listOrderViewModels));
            }

            return Json(null);
        }


        #endregion



        #region InWork

        [Authorize(Roles =StaticDetails.CookRole)]
        [HttpGet]
        public async Task<IActionResult> InWork(uint id)
        {
            modelOrderViewModels = await _metods.GetOneAsync<OrderViewModels>(id);

            if (modelOrderViewModels.OrderHeader.Id > 0)
            {
                
                return View(GetViewModelForOrder(modelOrderViewModels));
            }

            return View();
        }


        [HttpGet]
        public async Task<IActionResult> InWorkAjax(uint id,string status)
        {
            if (id>0)
            {
                //
                var employee = new EmployeeUpdateStatus()
                {
                    Id=_httpContextAccessor.HttpContext.User.Claims.ToList()[4].Value,
                    JobTitle= _httpContextAccessor.HttpContext.User.Claims.ToList()[3].Value,
                };
                var idEmployee=JsonConvert.SerializeObject(employee);


                var newIsStatus = new IsStatus();
                var response = await _orderViewModelsServices.UpdateStatusAsync<APIResponse>(status, id, idEmployee);
                if (response != null && response.IsSuccess)
                {
                    newIsStatus.IsSuccess = response.IsSuccess;
                    newIsStatus.Status = status;
                    return Json(newIsStatus);
                }
                return Json(null);
            }

            return Json(null);
        }


        #endregion











        #region Metods


        ViewModelForOrder GetViewModelForOrder(OrderViewModels model)
        {
            ViewModelForOrder newModel= new ViewModelForOrder();
            if (model.OrderHeader.Id>0)
            {
                newModel.Id = model.OrderHeader.Id;
                newModel.NumberOrder = model.OrderHeader.NumberOrder;
                newModel.OrderStatus= model.OrderHeader.OrderStatus;
                newModel.DateCreatedOrder = EditStringDateCreate(model.DateCreatedOrder);
                newModel.WishesClient = model.OrderHeader.WishesClient;
                newModel.MenuList = GetListMenu(model.OrderDetailsList);
            }

            return newModel;
        }




        List<ViewModelForIndex> GetViewModelForIndex(List<OrderViewModels> models )
        {
            List<ViewModelForIndex> newlist= new List<ViewModelForIndex>();

            if (models.Count>0)
            {
                foreach (var item in models)
                {
                    if (item.OrderHeader.OrderStatus==StaticDetails.StatusAccepted || item.OrderHeader.OrderStatus == StaticDetails.StatusInWork)
                    {
                        var newModel = new ViewModelForIndex()
                        {
                            IdOrderHeader = item.OrderHeader.Id,
                            DateCreatedOrder = EditStringDateCreate(item.DateCreatedOrder),
                            MenuList = GetListMenu(item.OrderDetailsList),
                            OrderStatus = item.OrderHeader.OrderStatus,
                        };
                        newlist.Add(newModel);
                    }
                    
                }
            }
            return newlist;
        }



        private string EditStringDateCreate(string date)
        {
            var newString = "";
            if (date != null)
            {
                var part_1 = date.Substring(11);
                var part_2 = date.Substring(8, 2);
                var part_3 = date.Substring(5, 2);
                var part_4 = date.Substring(0, 4);
                newString = part_1 + " " + part_2 + "-" + part_3 + "-" + part_4;
            }



            return newString;
        }


        List<OrderCount> GetListMenu(List<OrderDetailsDTO> models)
        {
            List<OrderCount> newlist= new List<OrderCount>();
            if (models!=null)
            {
                for (int i = 0; i < models.Count; i++)
                {
                    var newModel = new OrderCount()
                    {
                        MenuName=models[i].NameCategory+" "+ models[i].MenuName,
                        Count = models[i].Count,
                    }; 
                    newlist.Add(newModel);
                }
            }
            return newlist;
        }




        #endregion
    }
}