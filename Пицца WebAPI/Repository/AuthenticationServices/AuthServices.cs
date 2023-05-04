using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Pizza_WebAPI.Data;
using Pizza_WebAPI.Models;
using Pizza_WebAPI.Models.DTO;
using Pizza_WebAPI.Models.DTO.Authorization;
using Pizza_WebAPI.Repository.UnitOfWork;
using Pizza_WebAPI.Utillity;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Pizza_WebAPI.Repository.AuthenticationServices
{
    public class AuthServices : IAuthServices
    {
        ApplicationDbContext _dbContext;
        readonly RoleManager<IdentityRole> _roleManager;
        readonly IUnitOfWork _unitOfWork;
        readonly UserManager<Workers> _user;
        readonly IMapper _mapper;


        readonly IConfiguration _configuration;
        readonly string? _secretKey;



        LoginResponseDTO loginResponse;




        public AuthServices(ApplicationDbContext dbContext, IUnitOfWork unitOfWork,
                                                                  UserManager<Workers> user,
                                                                  IConfiguration configuration,
                                                                  IMapper mapper,
                                                                  RoleManager<IdentityRole> roleManager) 
        {
            _roleManager = roleManager;
            _dbContext = dbContext;
            _unitOfWork = unitOfWork;
            _user = user;
            _configuration = configuration;
            _mapper = mapper;
            _secretKey = _configuration.GetValue<string>("ApiJWTToken:SecretKey");
            loginResponse = new LoginResponseDTO();
        }

        public bool IsUniqueUser(string username)
        {
            var user = _unitOfWork.Workers.GetOne(u => u.UserName == username);
            if (user.PasswordHash == null && user.UserName == null)
            {
                return true;
            }
            return false;
        }



        public async Task<LoginResponseDTO> LoginAsync(LoginRequestDTO loginRequest)
        {
            var workerFromDb = _unitOfWork.Workers.GetOne(u => u.NormalizedUserName == loginRequest.Username.ToUpper());

            bool isValid = await _user.CheckPasswordAsync(workerFromDb, loginRequest.Password);

            if (workerFromDb == null || !isValid)
            {
                return loginResponse;
            }
            else
            {
                
                var roles = await _user.GetRolesAsync(workerFromDb);
                
                var tokenHandler = new JwtSecurityTokenHandler();
                var keyInBytes = Encoding.ASCII.GetBytes(_secretKey);

                // определяем наш токен
                var tokenDescriptor = new SecurityTokenDescriptor()
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                            new Claim(ClaimTypes.Name,workerFromDb.UserName.ToString()),
                            new Claim(ClaimTypes.GivenName,workerFromDb.FirstName.ToString()),
                            new Claim(ClaimTypes.Surname,workerFromDb.LastName.ToString()),
                            new Claim(ClaimTypes.Role,roles.FirstOrDefault()),
                    }),
                    Expires = DateTime.UtcNow.AddDays(7),
                    SigningCredentials = new(new SymmetricSecurityKey(keyInBytes), SecurityAlgorithms.HmacSha256Signature) //вставляем ключ
                };

                var token = tokenHandler.CreateToken(tokenDescriptor);

                loginResponse.Token = tokenHandler.WriteToken(token);
                loginResponse.User = _mapper.Map<WorkerDTO>(workerFromDb);
                loginResponse.User.Role = "";


                return loginResponse;

            }
        }



        public async Task<Workers> RegisterAsync(RegistrationRequestDTo registrationRequest)
        {
            if (!await _roleManager.RoleExistsAsync(StaticDetails.DirectorRole))
            {
                await _roleManager.CreateAsync(new IdentityRole(StaticDetails.DirectorRole));
                await _roleManager.CreateAsync(new IdentityRole(StaticDetails.AdministratorRole));
                await _roleManager.CreateAsync(new IdentityRole(StaticDetails.CookRole));
                await _roleManager.CreateAsync(new IdentityRole(StaticDetails.ManagerRole));
            }


            Workers newWorker = new Workers()
            {
                UserName = registrationRequest.UserName,
                FirstName = registrationRequest.FirstName,
                LastName = registrationRequest.LastName,

            };

            var result = await _user.CreateAsync(newWorker, registrationRequest.Password);
            if (result.Succeeded)
            {

                if (registrationRequest.UserName == "Director")
                {
                    await _user.AddToRoleAsync(newWorker, StaticDetails.DirectorRole);
                }

                var roleForNewWorker = _dbContext.Roles.FirstOrDefault(x => x.Id == registrationRequest.RoleId);
                if (roleForNewWorker != null)
                {
                    switch (roleForNewWorker.Name)
                    {
                        case StaticDetails.AdministratorRole:
                            await _user.AddToRoleAsync(newWorker, StaticDetails.AdministratorRole);
                            break;
                        case StaticDetails.CookRole:
                            await _user.AddToRoleAsync(newWorker, StaticDetails.CookRole);
                            break;
                        case StaticDetails.ManagerRole:
                            await _user.AddToRoleAsync(newWorker, StaticDetails.ManagerRole);
                            break;
                        default:
                            break;
                    }

                }
                return newWorker;
            }

            return null;
        }
    }
}
