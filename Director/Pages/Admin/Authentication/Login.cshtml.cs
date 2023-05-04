using Director.Models.Authorization;
using Director.Services.GenericServiceis;
using Director.Services.Metods;
using Director.Utillity;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Director.Pages.Admin.Authentication
{
    [BindProperties]
    public class LoginModel : PageModel
    {
        readonly IMetods _metods;

        public LoginRequestDTO LoginRequest { get; set; }

        public LoginModel(IMetods metods)
        {
            _metods = metods;

            LoginRequest = new LoginRequestDTO();
        }


        public void OnGet()
        {
            
        }
        

        public async Task<IActionResult> OnPost()
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var response = await _metods.GetTokenForUserAsync(LoginRequest);

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
                        //identity.AddClaim(new Claim(ClaimTypes.SerialNumber, response.User.Id));

                        #region тоже что сверху  но проще
                        // Authentication user in project
                        //var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
                        //identity.AddClaim(new Claim(ClaimTypes.Name, response.User.UserName));
                        //identity.AddClaim(new Claim(ClaimTypes.Role,response.User.Role));
                        #endregion


                        var principal =new ClaimsPrincipal(identity);
                        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);


                        HttpContext.Session.SetString(StaticDetails.SessionToken, response.Token);
                        TempData["Success"] = "Вы вошли в систему";
                        return Redirect("/admin/Home/Index");
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
    }
}
