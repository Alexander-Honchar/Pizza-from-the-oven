using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text;
using Пицца_Кухня.Models;
using Пицца_Кухня.Utility;

namespace Пицца_Кухня.Services
{
    public class BaseServices : IBaseServices
    {
        public IHttpClientFactory httpClient;
        readonly IHttpContextAccessor _httpContextAccessor;
        readonly string? _token;


        public BaseServices(IHttpClientFactory httpClient, IHttpContextAccessor httpContextAccessor)
        {
            this.httpClient = httpClient;
            _httpContextAccessor = httpContextAccessor;
            _token = _httpContextAccessor.HttpContext.Session.GetString(StaticDetails.SessionToken);
        }



        public async Task<T> SendRequstAsync<T>(APIRequest modelAPIRequest)
        {
            try
            {
                HttpRequestMessage requestMessage= new HttpRequestMessage();
                var client = httpClient.CreateClient("ApiClient");
                requestMessage.Headers.Add("Accept", "application/json");
                requestMessage.RequestUri=new Uri(modelAPIRequest.Url);


                // Data
                if (modelAPIRequest.Data != null)
                {
                    requestMessage.Content = new StringContent(JsonConvert.SerializeObject(modelAPIRequest.Data), Encoding.UTF8, "application/json");
                }


                // APIType
                switch (modelAPIRequest.APIType)
                {
                    case Utility.APIType.Put:
                        requestMessage.Method=HttpMethod.Put;
                        break;
                    case Utility.APIType.Post:
                        requestMessage.Method = HttpMethod.Post;
                        break;
                    default:
                        requestMessage.Method=HttpMethod.Get;
                        break;
                }


                HttpResponseMessage? responseMessage = null;


                if (!string.IsNullOrEmpty(_token))
                {
                    client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", _token);
                }



                responseMessage =await client.SendAsync(requestMessage);
                var responseContent=await responseMessage.Content.ReadAsStringAsync();
                var deserilaizeResponse= JsonConvert.DeserializeObject<T>(responseContent);
                return deserilaizeResponse;


            }
            catch (Exception ex)
            {
                var errorRespons = new APIResponse()
                {
                    IsSuccess= false,
                    ErrorsMessages = {ex.Message}
                };

                var response=JsonConvert.SerializeObject(errorRespons);
                var apiRespons=JsonConvert.DeserializeObject<T>(response);
                return apiRespons;
            }
        }
    }
}
