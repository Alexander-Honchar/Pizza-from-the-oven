namespace Пицца_Кухня.Models.Authorization
{
    public class LoginResponseDTO
    {
        public WorkerDTO? User { get; set; }
        public string? Token { get; set; }
    }
}
