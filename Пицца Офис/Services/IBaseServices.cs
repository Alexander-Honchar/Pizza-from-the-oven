using Пицца_Офис.Models;

namespace Пицца_Офис.Services
{
    public interface IBaseServices
    {
        Task<T> SendAsync<T>(APIRequest modelRequest);
    }
}
