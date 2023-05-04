

using Director.Models;
using Director.Models.Authorization;
using Director.Services.GenericServiceis;
using Director.Utillity;

namespace Director.Services.GenericServiceis
{
    public class GenericServices<T> : BaseServices, IGenericServices<T> where T : class
    {
        readonly IConfiguration _configuration;
        readonly string? _apiUrl;


        WorkerDTO modelWorker;
        RolesDTO modelRoles;
        OrderViewForDirectorDTO modelOrderView;
        RegistrationRequestDTo modelRegistration;
        LoginRequestDTO modelLogin;



        public GenericServices(IHttpClientFactory httpClient, IConfiguration configuration, IHttpContextAccessor httContext) : base(httpClient, httContext)
        {

            _configuration = configuration;
            _apiUrl = _configuration.GetValue<string>("ServiceUrls:PizzaApi");

            modelWorker = new();
            modelRoles = new();
            modelOrderView = new();
            modelRegistration = new();
            modelLogin = new();

        }





        private string GetApiUrl() 
        {

            if (modelWorker.GetType() == typeof(T))
            {
                return _apiUrl + "/api/Workers/";
            }
            else if (modelRoles.GetType() == typeof(T))
            {
                return _apiUrl + "/api/Authorization/";
            }
            else if (modelRegistration.GetType() == typeof(T))
            {
                return _apiUrl + "/api/Authorization/register";
            }
            else if (modelLogin.GetType() == typeof(T))
            {
                return _apiUrl + "/api/Authorization/login";
            }
            else if (modelOrderView.GetType() == typeof(T))
            {
                return _apiUrl + "/api/Order/ForDirector";
            }


            return "";
        } 
        



        public async Task<K> GetAllAsync<K>()
        {
            return await SendAsync<K>(new APIRequest()
            {
                APIType = Director.Utillity.StaticDetails.APIType.GET,
                Url = GetApiUrl(),
            });
        }



        public async Task<K> GetAlRolesAsync<K>()
        {
            return await SendAsync<K>(new APIRequest()
            {
                APIType = Director.Utillity.StaticDetails.APIType.GET,
                Url = GetApiUrl() ,
            });
        }



        public async  Task<K> GetOneAsync<K>(string id)
        {
            return await SendAsync<K>(new APIRequest()
            {
                APIType = Director.Utillity.StaticDetails.APIType.GET,
                Url = GetApiUrl() + "id?id=" + id,
            });
        }




        public async  Task<K> RegisterAsync<K>(T entity)
        {
 
            return await SendAsync<K>(new APIRequest()
            {
                APIType = Director.Utillity.StaticDetails.APIType.POST,
                Data = entity,
                Url = GetApiUrl(),
            });
        }



        public async Task<K> LoginAsync<K>(T entity)
        {

            return await SendAsync<K>(new APIRequest()
            {
                APIType = Director.Utillity.StaticDetails.APIType.POST,
                Data = entity,
                Url = GetApiUrl(),
            });
        }




        public async  Task<K> DeleteAsync<K>(string id)
        {
            return await SendAsync<K>(new APIRequest()
            {
                APIType = Director.Utillity.StaticDetails.APIType.DELETE,
                Url = GetApiUrl() + "id?id=" + id,
            });
        }




        public async  Task<K> UpdateAsync<K>(T entity)
        {
            return await SendAsync<K>(new APIRequest()
            {
                APIType = Director.Utillity.StaticDetails.APIType.PUT,
                Data = entity,
                Url = GetApiUrl(),
            });
        }

    }
}
