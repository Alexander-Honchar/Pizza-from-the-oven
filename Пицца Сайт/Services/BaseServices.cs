using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using System.Text.Json.Serialization;
using Пицца_Сайт.Models;
using Пицца_Сайт.Models.DTO;

namespace Пицца_Сайт.Services
{
    public class BaseServices : IBaseServices
    {
        

        public APIResponse responseModel { get; set; }
        public IHttpClientFactory httpClient;



        public BaseServices(IHttpClientFactory httpClient)
        {
            responseModel = new APIResponse();
            this.httpClient = httpClient;
            
        }

        public async  Task<T> SendAsync<T>(APIRequest requestModel)
        {
            try
            {
                HttpRequestMessage requestMessage= new HttpRequestMessage();

                var client = httpClient.CreateClient("PizzaAPI");
                requestMessage.Headers.Add("Accept", "application/json");
                requestMessage.RequestUri = new Uri(requestModel.Url);
                if (requestModel.Data!=null)
                {
                    requestMessage.Content=new StringContent(JsonConvert.SerializeObject(requestModel.Data),Encoding.UTF8, "application/json");
                }


                switch (requestModel.APIType)
                {
                    case Utillity.StaticDetails.APIType.GET:
                        break;
                    case Utillity.StaticDetails.APIType.POST:
                        requestMessage.Method = HttpMethod.Post;
                        break;
                    case Utillity.StaticDetails.APIType.PUT:
                        requestMessage.Method = HttpMethod.Put;
                        break;
                    case Utillity.StaticDetails.APIType.DELETE:
                        requestMessage.Method= HttpMethod.Delete;
                        break;
                    case Utillity.StaticDetails.APIType.PATH:
                        requestMessage.Method = HttpMethod.Patch;
                        break;
                    default:
                        break;
                }


                HttpResponseMessage? responseMessage = null;

                
                responseMessage=await client.SendAsync(requestMessage);

                var responseContent =await responseMessage.Content.ReadAsStringAsync();
                var responseDeserialize = JsonConvert.DeserializeObject<T>(responseContent);
                return responseDeserialize;
            }
            catch (Exception ex)
            {
                var dto = new APIResponse()
                {
                    ErrorsMessages = new List<string> { ex.Message },
                    IsSuccess = false,
                };

                var res = JsonConvert.SerializeObject(dto);
                var APIResponse = JsonConvert.DeserializeObject<T>(res);
                return APIResponse;
            }
        }

    }
}
