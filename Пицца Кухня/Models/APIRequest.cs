using Пицца_Кухня.Utility;

namespace Пицца_Кухня.Models
{
    public class APIRequest
    {
        public APIType APIType { get; set; } = APIType.Get;

        public string? Url { get; set; }
        public object? Data { get; set; }
        public string? Token { get; set; }
    }
}
