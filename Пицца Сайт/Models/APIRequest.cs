using static Пицца_Сайт.Utillity.StaticDetails;

namespace Пицца_Сайт.Models
{
    public class APIRequest
    {
        public APIType APIType { get; set; } = APIType.GET;

        public string? Url { get; set; }
        public object? Data { get; set; }
    }
}
