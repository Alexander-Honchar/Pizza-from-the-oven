using static Пицца_Офис.Utillity.StaticDetails;

namespace Пицца_Офис.Models
{
    public class APIRequest
    {
        public APIType APIType { get; set; } = APIType.GET;

        public string? Url { get; set; }
        public object? Data { get; set; }
    }
}
