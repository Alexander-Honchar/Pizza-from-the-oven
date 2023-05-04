using Пицца_Кухня.Models;

namespace Пицца_Кухня.Services
{
    public interface IBaseServices
    {
        Task<T> SendRequstAsync<T>(APIRequest modelAPIRequest);
    }
}
