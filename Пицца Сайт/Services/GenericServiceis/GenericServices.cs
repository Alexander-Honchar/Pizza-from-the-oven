using Пицца_Сайт.Models;
using Пицца_Сайт.Models.DTO;
using Пицца_Сайт.Models.ViewModels;

namespace Пицца_Сайт.Services.GenericServiceis
{
    public class GenericServices<T> : BaseServices, IGenericServices<T> where T : class
    {
        readonly IConfiguration _configuration;
        readonly string? _apiUrl;


        MenuItemDTO modelMenuItem;
        OrderViewModels modelOrderViewModels;
        PizzaDTO modelPizza;



        public GenericServices(IHttpClientFactory httpClient, IConfiguration configuration) : base(httpClient)
        {
            _configuration = configuration;
            _apiUrl = _configuration.GetValue<string>("ServiceUrls:PizzaApi");

            modelMenuItem = new();
            modelOrderViewModels = new();
            modelPizza = new();
        }

        
        private string GetApiUrl() 
        {

            if (modelMenuItem.GetType()==typeof(T))
            {
                return _apiUrl + "/api/MenuItem/";
            }
            else if (modelOrderViewModels.GetType() == typeof(T))
            {
                return _apiUrl + "/api/Order/";
            }
            else if (modelPizza.GetType() == typeof(T))
            {
                return _apiUrl + "/api/Pizza/";
            }

            return "";
        } 
        



    public async Task<K> GetAllAsync<K>()
        {
            return await SendAsync<K>(new APIRequest()
            {
                APIType = Utillity.StaticDetails.APIType.GET,
                Url = GetApiUrl(),
            });
        }

        public async  Task<K> GetAllNameCategoryAsync<K>(string nameCategory)
        {
            return await SendAsync<K>(new APIRequest()
            {
                APIType = Utillity.StaticDetails.APIType.GET,
                Url = GetApiUrl() + "nameCategory?nameCategory=" + nameCategory,
            });
        }



        public async  Task<K> GetOneAsync<K>(uint id)
        {
            return await SendAsync<K>(new APIRequest()
            {
                APIType = Utillity.StaticDetails.APIType.GET,
                Url = GetApiUrl() + "id?id=" + id,
            });
        }

        public async  Task<K> CreateAsync<K>(T entity)
        {
 
            return await SendAsync<K>(new APIRequest()
            {
                APIType = Utillity.StaticDetails.APIType.POST,
                Data = entity,
                Url = GetApiUrl(),
            });
        }

    }
}
