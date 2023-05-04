using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Пицца_Сайт.Models;
using Пицца_Сайт.Models.DTO;
using Пицца_Сайт.Models.ViewModels;
using Пицца_Сайт.Services.GenericServiceis;

using Пицца_Сайт.Utillity;

namespace Пицца_Сайт.Services.Metods
{
    public class Metods:IMetods
    {
        readonly IHttpContextAccessor? _httpContext;

        readonly IGenericServices<MenuItemDTO> _menuItenServices;
        readonly IGenericServices<PizzaDTO> _pizzaServices;
        readonly IGenericServices<OrderViewModels> _OrderViewModelsServices;

        List<MenuItemDTO>? menuItemsList;
        MenuItemDTO MenuItemDTO;
        PizzaDTO PizzaDTO;
        OrderViewModels OrderViewModels;


        public Metods(IHttpContextAccessor httpContext, IGenericServices<OrderViewModels> orderViewModelsServices,
                                                        IGenericServices<MenuItemDTO> menuItenServices,
                                                        IGenericServices<PizzaDTO> pizzaServices)
        {
            _httpContext = httpContext;
            MenuItemDTO = new MenuItemDTO();
            PizzaDTO = new PizzaDTO();
            OrderViewModels = new();
            menuItemsList = new List<MenuItemDTO>();
            _OrderViewModelsServices = orderViewModelsServices;
            _menuItenServices = menuItenServices;
            _pizzaServices = pizzaServices;
        }



        #region Metods


        #region Plus-Minus
        public void Plus(uint id, uint count)
        {
            _httpContext.HttpContext.Session.SetInt32("idForCountCart", Convert.ToInt32(id));
            _httpContext.HttpContext.Session.SetInt32("countForCart", Convert.ToInt32(count + 1));

        }




        public void Minus(uint id, uint count)
        {

            _httpContext.HttpContext.Session.SetInt32("idForCountCart", Convert.ToInt32(id));
            _httpContext.HttpContext.Session.SetInt32("countForCart", Convert.ToInt32(count - 1));

        }
        #endregion




        #region SaveMenuItemInSession



        /// <summary>
        /// сохраняем меню для корзины в сессию
        /// </summary>
        /// <param name="model"></param>
        public void SaveCartInSession(MenuItemDTO model)
        {
            List<MenuItemDTO> menuForCart = new();
            var checkListOrdersMenu = _httpContext.HttpContext.Session.GetString(StaticDetails.ListMenuInSession);
            if (checkListOrdersMenu != null)
            {
                menuForCart = JsonConvert.DeserializeObject<List<MenuItemDTO>>(checkListOrdersMenu);

                menuForCart = CheckMenuOnDublicate(menuForCart, model);
                string listMenuForCart = JsonConvert.SerializeObject(menuForCart);
                _httpContext.HttpContext.Session.SetString(StaticDetails.ListMenuInSession, listMenuForCart);
            }
            else
            {
                menuForCart.Add(model);
                string listMenuForCart = JsonConvert.SerializeObject(menuForCart);
                _httpContext.HttpContext.Session.SetString(StaticDetails.ListMenuInSession, listMenuForCart);
            }


        }




        /// <summary>
        /// проверяем меню на дубликаты перед сохраненме в сессии
        /// </summary>
        /// <param name="menuForCart"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        List<MenuItemDTO> CheckMenuOnDublicate(List<MenuItemDTO> menuForCart, MenuItemDTO model)
        {
            bool IsResult = false;
            foreach (var item in menuForCart)
            {
                if (item.Id == model.Id && item.ProductId == model.ProductId)
                {
                    item.Count += model.Count;
                    IsResult = true;
                }
            }
            if (IsResult)
            {
                return menuForCart;
            }
            menuForCart.Add(model);
            return menuForCart;
        }



        #region SaveMenuItemInCookies

        //void SaveMenuItemInCookies(MenuItemDTO model)
        //{
        //	List<MenuItemDTO> menuForCart = new();
        //	if (HttpContext.Request.Cookies.ContainsKey("ListOrdersMenuCookies"))
        //	{
        //		var listOrdersMenuFromCookies = HttpContext.Request.Cookies["ListOrdersMenuCookies"];
        //		menuForCart = JsonConvert.DeserializeObject<List<MenuItemDTO>>(listOrdersMenuFromCookies);

        //		menuForCart = CheckMenuOnDublicate(menuForCart, model);
        //		string listMenuForCart = JsonConvert.SerializeObject(menuForCart);
        //		HttpContext.Session.SetString("ListOrdersMenu", listMenuForCart);
        //	}
        //	else
        //	{
        //		menuForCart.Add(model);
        //		string listMenuForCart = JsonConvert.SerializeObject(menuForCart);
        //		HttpContext.Response.Cookies.Append("ListOrdersMenuCookies", listMenuForCart);
        //	}


        //}

        //List<MenuItemDTO> CheckMenuOnDublicate(List<MenuItemDTO> menuForCart, MenuItemDTO model)
        //{
        //	bool IsResult = false;
        //	foreach (var item in menuForCart)
        //	{
        //		if (item.Id == model.Id && item.ProductId == model.ProductId)
        //		{
        //			item.Count += model.Count;
        //			IsResult = true;
        //		}
        //	}
        //	if (IsResult)
        //	{
        //		return menuForCart;
        //	}
        //	menuForCart.Add(model);
        //	return menuForCart;
        //}
        #endregion

        #endregion


        /// <summary>
        /// достаем меню для корзины из сессии
        /// </summary>
        /// <returns></returns>
        public List<MenuItemDTO> GetCartFromSession()
        {
            var checkListOrdersMenu = _httpContext.HttpContext.Session.GetString(StaticDetails.ListMenuInSession);
            if (checkListOrdersMenu != null)
            {
                menuItemsList = JsonConvert.DeserializeObject<List<MenuItemDTO>>(checkListOrdersMenu);
                return menuItemsList;
            }
            return null;
        }



        /// <summary>
        /// достаем из сессии корзину с товарами
        /// </summary>
        /// <returns></returns>
        public ShoppingCart GetShoppingCart()
        {
            ShoppingCart cart = new ShoppingCart();
            var checkListCarts = _httpContext.HttpContext.Session.GetString(StaticDetails.ListCartInSession);
            if (checkListCarts != null)
            {
                cart = JsonConvert.DeserializeObject<ShoppingCart>(checkListCarts);
                return cart;
            }
            return null;
        }





        /// <summary>
        /// получаем список меню по названию категории из Db
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="nameCategory"></param>
        /// <returns></returns>
        public async Task<List<T>> GetListNameCategoryMenuAsync<T>(string nameCategory) where T : class
        {
            var responseOnlyNameCategory = await _menuItenServices.GetAllNameCategoryAsync<APIResponse>(nameCategory);
            if (responseOnlyNameCategory != null)
            {
                var ListFromDb = JsonConvert.DeserializeObject<List<T>>(Convert.ToString(responseOnlyNameCategory.Result));
                return ListFromDb;
            }
            return null;
        }



       



        /// <summary>
        /// возвращает ID меню из сессии счетчика 
        /// </summary>
        /// <returns></returns>
        public uint GetIdMenuForPlusMinusInCart()
        {
            return Convert.ToUInt32(_httpContext.HttpContext.Session.GetInt32("idForCountCart"));
        }



        /// <summary>
        /// возвращает Count товара меню из сессии  
        /// </summary>
        /// <returns></returns>
        public uint GetCountMenuForPlusMinusInCart()
        {
            
            return Convert.ToUInt32(_httpContext.HttpContext.Session.GetInt32("countForCart"));
        }



        /// <summary>
        ///  получаем список Pizza из Db по названию пиццы
        /// </summary>
        /// <param name="namePizza"></param>
        /// <returns></returns>
        public async  Task<List<PizzaDTO>> GetListPizzaByName(string namePizza)
        {
            var response = await _pizzaServices.GetAllNameCategoryAsync<APIResponse>(namePizza);
            if (response!=null)
            {
                var pizzaDTOList = JsonConvert.DeserializeObject<List<PizzaDTO>>(Convert.ToString(response.Result));
                return pizzaDTOList;
            }
            return null;
        }




        /// <summary>
        /// изменяем в сессии  CartViewComponent , if count=0  удаляется меню из CartViewComponent
        /// </summary>
        /// <param name="id"> ID MenuItem</param>
        /// <param name="count"> Новое количество меню</param>
        /// <param name="symbol"></param>
        public void ChangesCountInCart(uint id, uint count)
        {
            var menuFromSession = GetCartFromSession();
            if (menuFromSession != null)
            {
                for (int i = 0; i < menuFromSession.Count; i++)
                {
                    if (menuFromSession[i].Id == id)
                    {
                        menuFromSession[i].Count = count;
                        if (menuFromSession[i].Count == 0)
                        {
                            menuFromSession.RemoveAt(i);

                        }
                    }
                }
            }
            string listMenuForCart = JsonConvert.SerializeObject(menuFromSession);
            _httpContext.HttpContext.Session.SetString(StaticDetails.ListMenuInSession, listMenuForCart);

        }





        /// <summary>
        ///  удаляем одну позицию в заказе
        /// </summary>
        /// <param name="orderViewModels"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public OrderViewModels RemoveOrderInOrderView(OrderViewModels orderViewModels, uint id)
        {

            var newTotalSumma = 0.0;
            for (int i = 0; i < orderViewModels.OrderDetailsList.Count; i++)
            {

                if (orderViewModels.OrderDetailsList[i].MenuId == id)
                {
                    orderViewModels.OrderDetailsList.RemoveAt(i);
                    ChangesCountInCart(id, 0);

                }
            }

            for (int j = 0; j < orderViewModels.OrderDetailsList.Count; j++)
            {
                newTotalSumma += orderViewModels.OrderDetailsList[j].Price * orderViewModels.OrderDetailsList[j].Count;
            }

            orderViewModels.TotalSumma = newTotalSumma;

            return orderViewModels;
        }



        #endregion



        #region Работа с DataBase



        /// <summary>
        /// универсальный метод возвращает список товаров из Db заданого класса
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public async Task<List<T>> GetAllAsync<T>() where T : class
        {

            #region return List MenuItem From Database
            if (MenuItemDTO.GetType() == typeof(T))
            {
                var response = await _menuItenServices.GetAllAsync<APIResponse>();
                if (response != null)
                {
                    var ListFromDb = JsonConvert.DeserializeObject<List<T>>(Convert.ToString(response.Result));
                    return ListFromDb;
                }
                return null;
            }
            #endregion 


            #region return List Pizza From Database
            if (PizzaDTO.GetType() == typeof(T))
            {
                var response = await _pizzaServices.GetAllAsync<APIResponse>();
                if (response != null)
                {
                    var ListFromDb = JsonConvert.DeserializeObject<List<T>>(Convert.ToString(response.Result));
                    return ListFromDb;
                }
                return null;
            }
            #endregion 


            return null;
        }



        /// <summary>
        /// универсальный метод возвращает товар из Db заданого класса
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<T> GetOneAsync<T>(uint id) where T : class
        {

            #region return Object MenuItem From Database
            if (MenuItemDTO.GetType() == typeof(T))
            {
                var response = await _menuItenServices.GetOneAsync<APIResponse>(id);
                if (response != null)
                {
                    var EntityFromDb = JsonConvert.DeserializeObject<T>(Convert.ToString(response.Result));
                    return EntityFromDb;
                }
                return null;
            }
            #endregion

            #region return Object Pizza From Database
            if (PizzaDTO.GetType() == typeof(T))
            {
                var response = await _pizzaServices.GetOneAsync<APIResponse>(id);
                if (response != null)
                {
                    var EntityFromDb = JsonConvert.DeserializeObject<T>(Convert.ToString(response.Result));
                    return EntityFromDb;
                }
                return null;
            }
            #endregion


            return null;
        }


        /// <summary>
        /// универсальный метод создает товар в Db заданого класса
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="model"></param>
        /// <returns></returns>
        //public async Task<T> CrateAsync<T>(T model) where T : class
        //{
        //    #region return Object OrderViewModels For Database
        //    if (OrderViewModels.GetType() == typeof(T))
        //    {
        //        var response = await _OrderViewModelsServices.CreateAsync<APIResponse>(model as OrderViewModels);
        //        if (response != null)
        //        {
        //            var EntityFromDb = JsonConvert.DeserializeObject<T>(Convert.ToString(response.Result));
        //            return EntityFromDb;
        //        }
        //        return null;
        //    }
        //    #endregion


        //    return null;
        //}


        #endregion



    }
}
