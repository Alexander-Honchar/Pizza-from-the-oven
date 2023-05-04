using System.IO;
using Пицца_Кухня.Models;
using Пицца_Кухня.Models.Authorization;
using Пицца_Кухня.Models.ViewModels;
using Пицца_Кухня.Utility;

namespace Пицца_Кухня.Services.GenericServiceis
{
    public class GenericServices<T> :BaseServices,  IGenericServices<T> where T : class
    {

        readonly IConfiguration _configuration;
        readonly string? _apiUrl;

        OrderViewModels modelOrderViewModels;
        LoginRequestDTO modelLogin;


        public GenericServices(IConfiguration configuration,IHttpClientFactory httpClient, IHttpContextAccessor httContext) :base(httpClient, httContext) 
        {
            _configuration= configuration;
            _apiUrl = _configuration.GetValue<string>("ServiceUrls:PizzaApi");


            modelLogin= new();
            modelOrderViewModels = new();
        }



        string GetApiUrl()
        {
            if (modelOrderViewModels.GetType() == typeof(T))
            {
                return _apiUrl + "/api/Order/";
            }
            else if (modelLogin.GetType() == typeof(T))
            {
                return _apiUrl + "/api/Authorization/login";
            }
            return "";
        }



        public async  Task<K> GetAllAsync<K>()
        {
            return await SendRequstAsync<K>(new Models.APIRequest()
            {
                APIType=Utility.APIType.Get,
                Url= GetApiUrl(),
            });
        }

        public async Task<K> GetOneAsync<K>(uint id)
        {
            return await SendRequstAsync<K>(new Models.APIRequest()
            {
                APIType=Utility.APIType.Get,
                Url= GetApiUrl()+ "id?id=" + id,
            });
        }

        public async Task<K> UpdateStatusAsync<K>(string status, uint id, string? idEmployee)
        {
            return await SendRequstAsync<K>(new Models.APIRequest()
            {
                APIType=Utility.APIType.Put,
                Url=GetApiUrl() + "status?status=" + status + "&&id=" + id + "&&idEmployee=" + idEmployee,
            });
        }


        public async Task<K> LoginAsync<K>(T entity)
        {

            return await SendRequstAsync<K>(new APIRequest()
            {
                APIType = APIType.Post,
                Data = entity,
                Url = GetApiUrl(),
            });
        }

    }
}
