using Director.Models;
using Director.Models.Authorization;
using Director.Services.GenericServiceis;
using Director.Utillity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Director.Pages.Admin.Authentication
{
    [BindProperties]
    public class RegisterModel : PageModel
    {
        readonly IGenericServices<RegistrationRequestDTo> _registrationRequestServices;


        public RegistrationRequestDTo ModelRegister { get; set; }


        public RegisterModel(IGenericServices<RegistrationRequestDTo> registrationRequestServices)
        {
            _registrationRequestServices= registrationRequestServices;

            ModelRegister= new RegistrationRequestDTo();
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
                    var response = await _registrationRequestServices.RegisterAsync<APIResponse>(ModelRegister);

                    if (response!=null && response.IsSuccess)
                    {
                        TempData["Success"] = "Вы зарегистрировались";
                        return RedirectToPage("Login");
                    }
                    TempData["Error"] = "Упс.Что-то пошло не так";
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
