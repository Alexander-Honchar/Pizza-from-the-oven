namespace Pizza_WebAPI.Models.DTO.Authorization
{
    public class LoginResponseDTO
    {
        public WorkerDTO? User { get; set; }
        public string? Token { get; set; }
    }
}
