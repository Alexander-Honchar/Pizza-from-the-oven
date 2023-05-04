

using Microsoft.IdentityModel.Tokens;
using Пицца_Офис.Models;
using Пицца_Офис.Models.Authorization;
using Пицца_Офис.Models.DTO;
using Пицца_Офис.Models.ViewModels;
using static Пицца_Офис.Utillity.StaticDetails;

namespace Пицца_Офис.Services.GenericServiceis
{
    public class GenericServices<T> : BaseServices, IGenericServices<T> where T : class
    {
        readonly IConfiguration _configuration;
        readonly string? _apiUrl;


        MenuItemDTO modelMenuItem;
        OrderViewModels modelOrderViewModels;
        PizzaDTO modelPizza;
        CategoryMenuDTO modelCategoryMenu;
        CategoryPizzaDTO modelCategoryPizza;
        PizzaKingSizeDTO modelPizzaKingSize;
        SizePizzaDTO modelSizePizza;
        LoginRequestDTO modelLogin;



        public GenericServices(IHttpClientFactory httpClient, IConfiguration configuration, IHttpContextAccessor httContext) : base(httpClient, httContext)
        {
            _configuration = configuration;
            _apiUrl = _configuration.GetValue<string>("ServiceUrls:PizzaApi");

            modelMenuItem = new();
            modelOrderViewModels = new();
            modelPizza = new();
            modelCategoryMenu = new();
            modelCategoryPizza= new();
            modelPizzaKingSize= new();
            modelSizePizza= new();
            modelLogin=new ();
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
            else if (modelCategoryMenu.GetType() == typeof(T))
            {
                return _apiUrl + "/api/CategoryMenu/";
            }
            else if (modelCategoryPizza.GetType() == typeof(T))
            {
                return _apiUrl + "/api/CategoryPizza/";
            }
            else if (modelPizzaKingSize.GetType() == typeof(T))
            {
                return _apiUrl + "/api/PizzaKingSize/";
            }
            else if (modelSizePizza.GetType() == typeof(T))
            {
                return _apiUrl + "/api/SizePizza/";
            }
            else if (modelLogin.GetType() == typeof(T))
            {
                return _apiUrl + "/api/Authorization/login";
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




        public async  Task<K> DeleteAsync<K>(uint id)
        {
            return await SendAsync<K>(new APIRequest()
            {
                APIType = Utillity.StaticDetails.APIType.DELETE,
                Url = GetApiUrl() + "id?id=" + id,
            });
        }




        public async  Task<K> UpdateAsync<K>(T entity)
        {
            return await SendAsync<K>(new APIRequest()
            {
                APIType = Utillity.StaticDetails.APIType.PUT,
                Data = entity,
                Url = GetApiUrl(),
            });
        }

		public async Task<K> UpdateOrderDetailsAsync<K>(uint id,uint count)
		{
			return await SendAsync<K>(new APIRequest()
			{
				APIType = Utillity.StaticDetails.APIType.PUT,
				Url = GetApiUrl() + "id?id=" + id + "&&count="+count,
			});
		}


        public async Task<K> UpdateStatusAsync<K>(string status, uint id, string? idEmployee)
        {
            return await SendAsync<K>(new Models.APIRequest()
            {
                APIType = APIType.PUT,
                Url = GetApiUrl() + "status?status=" + status + "&&id=" + id + "&&idEmployee=" + idEmployee,
            });
        }


        public async Task<K> LoginAsync<K>(T entity)
        {

            return await SendAsync<K>(new APIRequest()
            {
                APIType = APIType.POST,
                Data = entity,
                Url = GetApiUrl(),
            });
        }
    }
}
