namespace Пицца_Офис.Models.Authorization
{
    public class LoginResponseDTO
    {
        public WorkerDTO? User { get; set; }
        public string? Token { get; set; }
    }
}
