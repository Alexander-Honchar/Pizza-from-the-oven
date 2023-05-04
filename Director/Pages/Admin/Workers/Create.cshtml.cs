using Director.Models;
using Director.Models.Authorization;
using Director.Services.GenericServiceis;
using Director.Services.Metods;
using Director.Utillity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Data;

namespace Director.Pages.Admin.Workers
{
    [Authorize(Roles = StaticDetails.DirectorRole)]
    [BindProperties]
    public class CreateModel : PageModel
    {
        readonly IGenericServices<RegistrationRequestDTo> _registerServices;
        readonly IMetods _metods;

        public  RegistrationRequestDTo modelregistration { get; set; }

        

        public CreateModel(IGenericServices<RegistrationRequestDTo> registerServices, IMetods metods)
        {
            _registerServices= registerServices;    
            _metods= metods;
            modelregistration = new();  
        }


        public async Task OnGet()
        {
            modelregistration = await GetAllRolesAsync();
        }


        public async Task<IActionResult> OnPost() 
        {
            if (ModelState.IsValid)
            {
                var response= await _registerServices.RegisterAsync<APIResponse> (modelregistration);
                if (response.IsSuccess)
                {
                    TempData["Success"] = "Сотрудник добавлен в базу";
                    return RedirectToPage("Index");
                }

                TempData["Error"] = response.ErrorsMessages[0].ToString();
                return RedirectToPage("");
            }
            TempData["Error"] = "Форма заполнена не корекно";
            return RedirectToPage("");
        }



        #region Metods

        private async Task<RegistrationRequestDTo> GetAllRolesAsync()
        {
            IEnumerable<SelectListItem> result = new List<SelectListItem>();
            RegistrationRequestDTo newModel= new RegistrationRequestDTo();

            var lislRoles = await _metods.GetListRolesAsync();
            if (lislRoles != null)
            {

                result = lislRoles.Select(u => new SelectListItem()
                {
                    Text = u.Name,
                    Value = u.Id.ToString(),
                });
                newModel.RoleList = result;

                return newModel;
            }

            return null;
        }

        #endregion


    }
}
