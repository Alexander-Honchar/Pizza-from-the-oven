using Pizza_WebAPI.Models;
using Pizza_WebAPI.Models.DTO.Authorization;

namespace Pizza_WebAPI.Repository.AuthenticationServices
{
    public interface IAuthServices
    {
        bool IsUniqueUser(string username);
        Task<LoginResponseDTO> LoginAsync(LoginRequestDTO loginRequest);
        Task<Workers> RegisterAsync(RegistrationRequestDTo registrationRequest);
    }
}
