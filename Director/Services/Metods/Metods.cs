using Director.Models;
using Director.Models.Authorization;
using Director.Services.GenericServiceis;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;


namespace Director.Services.Metods
{
    public class Metods:IMetods
    {
        readonly IGenericServices<OrderViewForDirectorDTO> _orderViewServices;
        readonly IGenericServices<WorkerDTO> _workerServices;
        readonly IGenericServices<RolesDTO> _rolesServices;
        readonly IGenericServices<LoginRequestDTO> _loginRequestServices;


        OrderViewForDirectorDTO modelOrderView;
        WorkerDTO modelWorker;




        public Metods(IGenericServices<OrderViewForDirectorDTO> orderViewServices, IGenericServices<WorkerDTO> workerServices,
                                                                                   IGenericServices<RolesDTO> rolesServices,
                                                                                   IGenericServices<LoginRequestDTO> loginRequestServices)
        {
            _orderViewServices = orderViewServices;
            _workerServices = workerServices;
            _rolesServices = rolesServices;
            _loginRequestServices = loginRequestServices;

            modelWorker= new ();
            modelOrderView = new ();
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
        /// получаем список ролей из Db
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="nameCategory"></param>
        /// <returns></returns>
        public async Task<List<IdentityRole>> GetListRolesAsync()
        {
            var response = await _rolesServices.GetAlRolesAsync<APIResponse>();
            if (response != null)
            {
                var rolesList = JsonConvert.DeserializeObject<List<IdentityRole>>(Convert.ToString(response.Result));
                return rolesList;
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

            #region return List OrderViewForDirectorDTO From Database
            if (modelOrderView.GetType() == typeof(T))
            {
                var response = await _orderViewServices.GetAllAsync<APIResponse>();
                if (response != null)
                {
                    var ListFromDb = JsonConvert.DeserializeObject<List<T>>(Convert.ToString(response.Result));
                    return ListFromDb;
                }
                return null;
            }
            #endregion 


            #region return List Workers From Database
            if (modelWorker.GetType() == typeof(T))
            {
                var response = await _workerServices.GetAllAsync<APIResponse>();
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
        public async Task<T> GetOneAsync<T>(string id) where T : class
        {

            #region return Object OrderViewForDirectorDTO From Database
            if (modelOrderView.GetType() == typeof(T))
            {
                var response = await _orderViewServices.GetOneAsync<APIResponse>(id);
                if (response != null)
                {
                    var EntityFromDb = JsonConvert.DeserializeObject<T>(Convert.ToString(response.Result));
                    return EntityFromDb;
                }
                return null;
            }
            #endregion


            #region return Object Workers From Database
            if (modelWorker.GetType() == typeof(T))
            {
                var response = await _workerServices.GetOneAsync<APIResponse>(id);
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
