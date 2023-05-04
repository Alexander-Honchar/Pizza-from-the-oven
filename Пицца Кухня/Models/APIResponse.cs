using System.Net;

namespace Пицца_Кухня.Models
{
    public class APIResponse
    {
        public HttpStatusCode StatusCode { get; set; }
        public bool IsSuccess { get; set; }
        public List<string> ErrorsMessages { get; set; } = new List<string>();
        public object? Result { get; set; }
    }
}
