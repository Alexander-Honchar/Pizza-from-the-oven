using Hanssens.Net;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Пицца_Сайт.Models;
using Пицца_Сайт.Models.ViewModels;
using Пицца_Сайт.Services.GenericServiceis;
using Пицца_Сайт.Services.Metods;

using Пицца_Сайт.Utillity;

namespace Пицца_Сайт.Controllers
{
    public class OrderController : Controller
    {
        readonly IMetods _metods;
        readonly IGenericServices<OrderViewModels> _orderViewModelsServices;
        

        private ChangesCount changesCount;
        private StatusOrder modelStatusOrder;

        public ShoppingCart ShoppingCart;
        public OrderViewModels modelOrderViewModels;



        public OrderController(IMetods metods, IGenericServices<OrderViewModels> orderViewModelsServices)
        {
            _metods= metods;
            _orderViewModelsServices = orderViewModelsServices;
            modelOrderViewModels = new OrderViewModels();
            ShoppingCart= new ShoppingCart();
            changesCount= new ChangesCount();
            modelStatusOrder= new StatusOrder();
            
        }



        #region Index Get-Post

        [HttpGet]
        public IActionResult Index( ChangesCount changesCount)
        {
            if (!changesCount.IsChangesCount)
            {
                modelOrderViewModels = CreateViewModelsFromShoppingCart(_metods.GetShoppingCart());


                SaveOrderViewModel(modelOrderViewModels);    
                return View(modelOrderViewModels);
            }
            //GetOrderViewModels();                           
            return View(GetOrderViewModels());
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task< IActionResult> Index(OrderViewModels model)
        {
            var response = await _orderViewModelsServices.CreateAsync<APIResponse>(SaveAllDataForOrder(model));
            if (response!=null && response.IsSuccess)
            {
                modelStatusOrder = JsonConvert.DeserializeObject<StatusOrder>(Convert.ToString(response.Result));
            }


            return RedirectToAction("OrderResult", modelStatusOrder);
        }
        #endregion




        [HttpGet]
        public IActionResult OrderResult(StatusOrder model)
        {
            if (model.IsStatus)
            {
                modelOrderViewModels = GetOrderViewModels();
                if (modelOrderViewModels != null)
                {
                    foreach (var item in modelOrderViewModels.OrderDetailsList)
                    {
                        _metods.ChangesCountInCart(item.MenuId, 0);
                    }
                    
                }
                SaveOrderViewModel(modelOrderViewModels);
            }

            return View(model);
        }





        public IActionResult Remove(uint id)
        {
            modelOrderViewModels = GetOrderViewModels();
            if (modelOrderViewModels !=null)
            {
                modelOrderViewModels =_metods.RemoveOrderInOrderView(modelOrderViewModels,id);
            }
            SaveOrderViewModel(modelOrderViewModels);
            changesCount.IsChangesCount = true;
            return RedirectToAction("Index", routeValues: changesCount);
        }

        






        #region Metods

        /// <summary>
        /// Создаем экземпляр OrderViewModels из ShoppingCart
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        OrderViewModels CreateViewModelsFromShoppingCart(ShoppingCart model)
        {
            OrderViewModels newModel= new OrderViewModels();

            foreach (var item in model.CartsList)
            {
                newModel.OrderDetailsList.Add(new OrderDetailsDTO()
                {
                    MenuId = item.MenuId,
                    NameCategory = item.MenuNameCategory,
                    MenuName = item.Name,
                    ProductId = item.ProductId,
                    Price = item.Price,
                    Count = item.Count,
                });
            }

            newModel.TotalSumma = model.TotalSumma;

            return newModel;
        }




        /// <summary>
        /// сохраняем в  сессии OrderViewModels
        /// </summary>
        /// <param name="model"></param>
        void SaveOrderViewModel(OrderViewModels model)
        {

            var serializingOrderViewModel = JsonConvert.SerializeObject(model);
            HttpContext.Session.SetString(StaticDetails.OrderViewModelInSession, serializingOrderViewModel);

        }





        /// <summary>
        /// достаем из сессии OrderViewModels
        /// </summary>
        /// <returns></returns>
        OrderViewModels GetOrderViewModels()
        {
            OrderViewModels viewModels;
            var checkOrderViewModels = HttpContext.Session.GetString(StaticDetails.OrderViewModelInSession);
            if (checkOrderViewModels != null)
            {
                viewModels = JsonConvert.DeserializeObject<OrderViewModels>(checkOrderViewModels);
                return viewModels;
            }
            return null;
        }







        /// <summary>
        /// Соединяем заказ с пользовательскими данными
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        private OrderViewModels SaveAllDataForOrder(OrderViewModels model)
        {
            modelOrderViewModels = GetOrderViewModels();
            var NumberOrder = modelOrderViewModels.OrderHeader.NumberOrder;
            modelOrderViewModels.OrderHeader=model.OrderHeader;
            modelOrderViewModels.OrderHeader.NumberOrder=NumberOrder;

            return modelOrderViewModels;
        }


        #endregion

    }
}
