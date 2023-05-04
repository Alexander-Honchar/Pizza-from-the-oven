using Director.Models;

namespace Director.Services
{
    public interface IBaseServices
    {
        Task<T> SendAsync<T>(APIRequest modelRequest);
    }
}
