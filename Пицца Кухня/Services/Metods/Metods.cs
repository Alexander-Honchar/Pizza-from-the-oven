using Newtonsoft.Json;
using Пицца_Кухня.Models;
using Пицца_Кухня.Models.Authorization;
using Пицца_Кухня.Models.ViewModels;
using Пицца_Кухня.Services.GenericServiceis;

namespace Пицца_Кухня.Services.Metods
{
    public class Metods : IMetods
    {
        readonly IGenericServices<OrderViewModels> _orderViewModelsServices;
        readonly IGenericServices<LoginRequestDTO> _loginRequestServices;


        public OrderViewModels modelOrderViewModels;

        public Metods(IGenericServices<OrderViewModels> orderViewModelsServices, IGenericServices<LoginRequestDTO> loginRequestServices)
        {
            _orderViewModelsServices = orderViewModelsServices;
            _loginRequestServices = loginRequestServices;


            modelOrderViewModels = new();
            
        }




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









        public async  Task<List<T>> GetAllAsync<T>() where T : class
        {
            #region return OrderViewModel From Database
            if (modelOrderViewModels.GetType() == typeof(T))
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

            return null;
        }





        public async  Task<T> GetOneAsync<T>(uint id) where T : class
        {
            #region return Object OrderViewModel From Database
            if (modelOrderViewModels.GetType() == typeof(T))
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

            return null;
        }
    }
}
