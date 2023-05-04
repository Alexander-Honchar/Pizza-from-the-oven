using Newtonsoft.Json;
using Пицца_Офис.Models;
using Пицца_Офис.Models.Authorization;
using Пицца_Офис.Models.DTO;
using Пицца_Офис.Models.ViewModels;
using Пицца_Офис.Services.GenericServiceis;


namespace Пицца_Офис.Services.Metods
{
    public class Metods:IMetods
    {
        readonly IGenericServices<MenuItemDTO> _menuItenServices;
        readonly IGenericServices<PizzaDTO> _pizzaServices;
        readonly IGenericServices<CategoryMenuDTO> _categoryMenuServices;
        readonly IGenericServices<CategoryPizzaDTO> _categoryPizzaServices;
        readonly IGenericServices<OrderViewModels> _orderViewModelsServices;
        readonly IGenericServices<PizzaKingSizeDTO> _pizzaKingSizeServices;
        readonly IGenericServices<SizePizzaDTO> _sizePizzaServices;
        readonly IGenericServices<LoginRequestDTO> _loginRequestServices;


        MenuItemDTO modelMenuItem;
        PizzaDTO modelPizza;
        CategoryMenuDTO modelCategoryMenu;
        CategoryPizzaDTO modelCategoryPizza;
        OrderViewModels modelOrderViewModel;
        PizzaKingSizeDTO modelPizzaKingSize;
        SizePizzaDTO modelSizePizza;




        public Metods(IGenericServices<MenuItemDTO> menuItenServices, IGenericServices<PizzaDTO> pizzaServices,
                                                                      IGenericServices<CategoryMenuDTO> categoryMenuServices,
                                                                      IGenericServices<CategoryPizzaDTO> categoryPizzaServices,
                                                                      IGenericServices<OrderViewModels> orderViewModelsServices,
                                                                      IGenericServices<PizzaKingSizeDTO> pizzaKingSizeServices,
                                                                      IGenericServices<SizePizzaDTO> sizePizzaServices,
                                                                      IGenericServices<LoginRequestDTO> loginRequestServices)
        {
            _menuItenServices = menuItenServices;
            _pizzaServices= pizzaServices;
            _categoryMenuServices = categoryMenuServices;
            _categoryPizzaServices= categoryPizzaServices;
            _orderViewModelsServices= orderViewModelsServices;
            _pizzaKingSizeServices= pizzaKingSizeServices;
            _sizePizzaServices= sizePizzaServices;
            _loginRequestServices= loginRequestServices;

            modelMenuItem= new MenuItemDTO();
            modelPizza= new PizzaDTO();
            modelCategoryMenu= new CategoryMenuDTO();
            modelCategoryPizza= new CategoryPizzaDTO();
            modelOrderViewModel= new OrderViewModels();
            modelPizzaKingSize= new PizzaKingSizeDTO();
            modelSizePizza= new SizePizzaDTO();
        }






        #region Metods

        /// <summary>
        /// получаем токен для User
        /// </summary>
        /// <param name="loginRequest"></param>
        /// <returns></returns>
        public async Task<LoginResponseDTO> GetTokenForUserAsync(LoginRequestDTO loginRequest)
        {
            var response = await _loginRequestServices.LoginAsync<APIResponse>(loginRequest);
            if (response != null)
            {
                var loginResponse = JsonConvert.DeserializeObject<LoginResponseDTO>(Convert.ToString(response.Result));
                return loginResponse;
            }
            return null;
        }



        /// <summary>
        /// универсальный метод возвращает список товаров из Db заданого класса
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public async Task<List<T>> GetAllAsync<T>() where T : class
        {

            #region return List MenuItem From Database
            if (modelMenuItem.GetType() == typeof(T))
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
            if (modelPizza.GetType() == typeof(T))
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


            #region return CategoryMenu From Database
            if (modelCategoryMenu.GetType() == typeof(T))
            {
                var response = await _categoryMenuServices.GetAllAsync<APIResponse>();
                if (response != null)
                {
                    var ListFromDb = JsonConvert.DeserializeObject<List<T>>(Convert.ToString(response.Result));
                    return ListFromDb;
                }
                return null;
            }
            #endregion 


            #region return CategoryPizza From Database
            if (modelCategoryPizza.GetType() == typeof(T))
            {
                var response = await _categoryPizzaServices.GetAllAsync<APIResponse>();
                if (response != null)
                {
                    var ListFromDb = JsonConvert.DeserializeObject<List<T>>(Convert.ToString(response.Result));
                    return ListFromDb;
                }
                return null;
            }
            #endregion 


            #region return OrderViewModel From Database
            if (modelOrderViewModel.GetType() == typeof(T))
            {
                var response = await _orderViewModelsServices.GetAllAsync<APIResponse>();
                if (response != null)
                {
                    var ListFromDb = JsonConvert.DeserializeObject<List<T>>(Convert.ToString(response.Result));
                    return ListFromDb;
                }
                return null;
            }
            #endregion


            #region return  PizzaKingSize From Database
            if (modelPizzaKingSize.GetType() == typeof(T))
            {
                var response = await _pizzaKingSizeServices.GetAllAsync<APIResponse>();
                if (response != null)
                {
                    var ListFromDb = JsonConvert.DeserializeObject<List<T>>(Convert.ToString(response.Result));
                    return ListFromDb;
                }
                return null;
            }
            #endregion


            #region return  SizePizza From Database
            if (modelSizePizza.GetType() == typeof(T))
            {
                var response = await _sizePizzaServices.GetAllAsync<APIResponse>();
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
            if (modelMenuItem.GetType() == typeof(T))
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
            if (modelPizza.GetType() == typeof(T))
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


            #region return Object CategoryMenu From Database
            if (modelCategoryMenu.GetType() == typeof(T))
            {
                var response = await _categoryMenuServices.GetOneAsync<APIResponse>(id);
                if (response != null)
                {
                    var EntityFromDb = JsonConvert.DeserializeObject<T>(Convert.ToString(response.Result));
                    return EntityFromDb;
                }
                return null;
            }
            #endregion


            #region return Object CategoryPizza From Database
            if (modelCategoryPizza.GetType() == typeof(T))
            {
                var response = await _categoryPizzaServices.GetOneAsync<APIResponse>(id);
                if (response != null)
                {
                    var EntityFromDb = JsonConvert.DeserializeObject<T>(Convert.ToString(response.Result));
                    return EntityFromDb;
                }
                return null;
            }
            #endregion


            #region return Object OrderViewModel From Database
            if (modelOrderViewModel.GetType() == typeof(T))
            {
                var response = await _orderViewModelsServices.GetOneAsync<APIResponse>(id);
                if (response != null)
                {
                    var EntityFromDb = JsonConvert.DeserializeObject<T>(Convert.ToString(response.Result));
                    return EntityFromDb;
                }
                return null;
            }
            #endregion


            #region return Object PizzaKingSize From Database
            if (modelPizzaKingSize.GetType() == typeof(T))
            {
                var response = await _pizzaKingSizeServices.GetOneAsync<APIResponse>(id);
                if (response != null)
                {
                    var EntityFromDb = JsonConvert.DeserializeObject<T>(Convert.ToString(response.Result));
                    return EntityFromDb;
                }
                return null;
            }
            #endregion


            #region return Object CategoryPizza From Database
            if (modelCategoryPizza.GetType() == typeof(T))
            {
                var response = await _categoryPizzaServices.GetOneAsync<APIResponse>(id);
                if (response != null)
                {
                    var EntityFromDb = JsonConvert.DeserializeObject<T>(Convert.ToString(response.Result));
                    return EntityFromDb;
                }
                return null;
            }
            #endregion

            #region return Object SizePizza From Database
            if (modelSizePizza.GetType() == typeof(T))
            {
                var response = await _sizePizzaServices.GetOneAsync<APIResponse>(id);
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


        #endregion
    }
}
