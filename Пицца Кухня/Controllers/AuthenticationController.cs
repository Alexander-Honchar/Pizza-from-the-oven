using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Пицца_Кухня.Models.Authorization;
using Пицца_Кухня.Services.Metods;
using System.IdentityModel.Tokens.Jwt;
using Пицца_Кухня.Utility;

namespace Пицца_Кухня.Controllers
{
    public class AuthenticationController : Controller
    {
        readonly IMetods _metods;

        public LoginRequestDTO LoginRequest { get; set; }


        public AuthenticationController(IMetods metods)
        {
            _metods = metods;
            LoginRequest= new LoginRequestDTO();
        }






        #region Login Get-Post

        [HttpGet]
        public IActionResult Login()
        {
            return View(LoginRequest);
        }




        [HttpPost]
        public async Task<IActionResult> Login(LoginRequestDTO modelLogin)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var response = await _metods.GetTokenForUserAsync(modelLogin);

                    if (response != null && !string.IsNullOrEmpty(response.Token))
                    {
                        var handler = new JwtSecurityTokenHandler();
                        var jwt = handler.ReadJwtToken(response.Token);

                        // Authentication user in project,User and Role зашифрованы в токене
                        var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
                        identity.AddClaim(new Claim(ClaimTypes.Name, jwt.Claims.FirstOrDefault(u => u.Type == "name").Value));
                        identity.AddClaim(new Claim(ClaimTypes.GivenName, jwt.Claims.FirstOrDefault(u => u.Type == "given_name").Value));
                        identity.AddClaim(new Claim(ClaimTypes.Surname, jwt.Claims.FirstOrDefault(u => u.Type == "family_name").Value));
                        identity.AddClaim(new Claim(ClaimTypes.Role, jwt.Claims.FirstOrDefault(u => u.Type == "role").Value));
                        identity.AddClaim(new Claim(ClaimTypes.SerialNumber, response.User.Id));



                        var principal = new ClaimsPrincipal(identity);
                        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);


                        HttpContext.Session.SetString(StaticDetails.SessionToken, response.Token);
                        TempData["Success"] = "Вы вошли в систему";
                        return RedirectToAction("Index","Home");
                    }
                    TempData["Error"] = "Логин или пароль неверные";
                    return Forbid();
                }
                TempData["Error"] = "Данные не валидны";
                return Unauthorized();

            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.ToString();
                return BadRequest(ex.Message);
            }
        }
        #endregion



        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            HttpContext.Session.SetString(StaticDetails.SessionToken, "");
            return RedirectToAction("Index", "Home");
        }


        [HttpGet]
        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
