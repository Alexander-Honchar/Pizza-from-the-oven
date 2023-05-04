using Newtonsoft.Json;
using System.Text;
using Director.Models;
using Director.Services;
using Director.Utillity;

namespace Director.Services
{
    public class BaseServices : IBaseServices
    {
        public IHttpClientFactory httpClient;
        readonly IHttpContextAccessor _httContext;
        readonly string? _token;


        public BaseServices(IHttpClientFactory httpClient, IHttpContextAccessor httContext)
        {
            this.httpClient = httpClient;
            _httContext = httContext;
            _token= _httContext.HttpContext.Session.GetString(StaticDetails.SessionToken);
        }



        public async Task<T> SendAsync<T>(APIRequest modelRequest)
        {
            try
            {
                HttpRequestMessage requestMessage= new HttpRequestMessage();
                var client = httpClient.CreateClient("API");
                requestMessage.Headers.Add("Accept", "application/json");
                requestMessage.RequestUri = new Uri(modelRequest.Url);

                if (modelRequest.Data!=null)
                {
                    requestMessage.Content=new StringContent(JsonConvert.SerializeObject(modelRequest.Data),Encoding.UTF8, "application/json");
                }


                switch (modelRequest.APIType)
                {
                    case Utillity.StaticDetails.APIType.GET:
                        requestMessage.Method=HttpMethod.Get;
                        break;
                    case Utillity.StaticDetails.APIType.POST:
                        requestMessage.Method = HttpMethod.Post;
                        break;
                    case Utillity.StaticDetails.APIType.PUT:
                        requestMessage.Method=HttpMethod.Put;
                        break;
                    case Utillity.StaticDetails.APIType.DELETE:
                        requestMessage.Method=HttpMethod.Delete;
                        break;
                    
                }

                


                HttpResponseMessage responseMessage = null;

                if (!string.IsNullOrEmpty(_token))
                {
                    client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", _token);
                }



                responseMessage = await client.SendAsync(requestMessage);
                var responseContent=await responseMessage.Content.ReadAsStringAsync();
                var responseDeserialize=JsonConvert.DeserializeObject<T>(responseContent);
                return responseDeserialize;
                

            }
            catch (Exception ex)
            {
                var dto = new APIResponse()
                {
                    ErrorsMessages = new List<string>() { ex.Message },
                    IsSuccess = false,
                };

                var repons=JsonConvert.SerializeObject(dto);
                var APIResponse=JsonConvert.DeserializeObject<T>(repons);
                return APIResponse;
            }
        }
    }
}
