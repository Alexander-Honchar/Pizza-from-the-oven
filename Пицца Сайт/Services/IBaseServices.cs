using Пицца_Сайт.Models;

namespace Пицца_Сайт.Services
{
    public interface IBaseServices
    {
        public APIResponse responseModel { get; set; }

        Task<T> SendAsync<T>(APIRequest requestModel);

        
    }
}
